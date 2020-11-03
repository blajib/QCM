using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QCMApp.bll;
using QCMApp.Models;
using QCMApp.Tools;

namespace QCMApp.Controllers
{
    public class UserQuestionnaireController : Controller
    {
        // GET: UserQuestionnaire
        public ActionResult Index(String error)
        {
            if (error != null)
            {
                ViewBag.Error = error;
            }
            return View();
        }


        public JsonResult IndexJquery(ViewModelUserQuestionnaire model)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("InsertJquery.UserQuestionnaire.Entrée(idQuestionnaire : {0},nom candidat : {1}, prenom candidat{2})", model.idQuestionnaire, model.nom, model.prenom));

            try
            {
                DALSession.questionnaire = DALQuestionnaire.FindById(model.idQuestionnaire);
                DALSession.listeElements = DALElement.SelectAllFromQuestionnaire(DALSession.questionnaire.Id);
                var utilisateurExistant = DALUtilisateur.FindUtilisateurByNomPrenom(model.nom, model.prenom);
                if (utilisateurExistant != null)
                {
                    DALSession.idUtilisateur = utilisateurExistant.Id;
                }
                DALSession.nom = model.nom;
                DALSession.prenom = model.prenom;


                Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("InsertJquery.UserQuestionnaire.Sortie(nom session: {0},prenom session : {1})", DALSession.nom, DALSession.prenom));

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("InsertJquery.UserQuestionnaire.Exception(Exception: {0})", e));
                return Json(ErrorList.IndexJqueryUserQuestionnaire);
            }
            return Json(new
            {
                redirectUrl = Url.Action("Index", "UserQuestionnaire"/*, new { idQuestionnaire = idQuestionnaire }*/),
                isRedirect = true
                
            });
        }



        public ActionResult CommencerQuestionnaire()
        {
            Elements element = new Elements();
            if (DALSession.sessionStart == 1)
            {
                return RedirectToAction("RetourElementEnCours", "UserQuestionnaire");
            }

            try
            {

                DALSession.nombreElements = DALElement.SelectAllFromQuestionnaire(DALSession.questionnaire.Id).Count;
                DALSession.lastElement = DALElement.SelectLastElement(DALSession.questionnaire.Id);
                DALSession.sessionStart = 1;
                element = DALElement.FindElementByOrdre(DALSession.numElement, DALSession.questionnaire.Id);
                DALSession.element = element;
                DALSession.dateDebut = DateTime.Now;
                DALSession.listeIdElement.Add(DALSession.element.Id);
                DALSession.nombreQuestionsQuestionnaire = DALElement.SelectAllQuestionFromQuestionnaire(DALSession.questionnaire.Id).Count;

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("CommencerQuestionnaire.UserQuestionnaire.Exception(Exception: {0})", e));
                return RedirectToAction("Index", "UserQuestionnaire", new {error = ErrorList.commencerQuestionnaire});

            }
            if (element.TypeElement_Id == 1)
            {
                return RedirectToAction("SuiteDescription", "UserQuestionnaire");

            }
            else
            {
                return RedirectToAction("SuiteQuestion", "UserQuestionnaire");
            }

        }

        public ActionResult CheckNextElement(int idElement)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("checkNextElement.UserQuestionnaire.Entrée(idElement : {0})", idElement));

            Elements element = DALElement.FindById(idElement);


            if (DALSession.element.ordre == DALSession.lastElement.ordre && DALSession.nombreQuestionsRepondu == DALSession.nombreQuestionsQuestionnaire)
            {
                return RedirectToAction("ResultatQuestionnaire", "UserQuestionnaire");
            }
            else
            {
                foreach (var id in DALSession.listeIdElement)
                {

                    try
                    {
                        if (DALElement.elementOrdreApres(element).Id != null)
                        {
                            if (id == DALElement.elementOrdreApres(element).Id)
                                return RedirectToAction("RetourElementEnCours", "UserQuestionnaire");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return RedirectToAction("ResultatQuestionnaire", "UserQuestionnaire");
                    }

                }

                DALSession.element = DALElement.elementOrdreApres(DALSession.element);
                List<int> list = DALSession.listeIdElement;
                list.Add(DALSession.element.Id);
                DALSession.listeIdElement = list;

                Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("checkNextElement.UserQuestionnaire.Sortie(idElement: {0}, idElementApres{1})", idElement, DALSession.element.Id));
            }

            if (DALSession.element.TypeElement_Id == 1)
            {
                return RedirectToAction("SuiteDescription", "UserQuestionnaire");
            }
            else
            {
                return RedirectToAction("SuiteQuestion","UserQuestionnaire");
            }

        }
        public ActionResult SuiteDescription()
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("SuiteDescription.UserQuestionnaire.Entrée(idElementSession: {0})", DALSession.element.Id));
            ViewModelUserQuestionnaire model = new ViewModelUserQuestionnaire();
            try
            {
                model.imagesUp = DALMedia.SelectAllFromElement(DALSession.element.Id);
                model.imagesDown = DALMedia.SelectAllFromElementDown(DALSession.element.Id);
                model.video = DALMedia.SelectVideoFromElement(DALSession.element.Id);
                model.element = DALSession.element;

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("SuiteDescription.UserQuestionnaire.Exception(Excpetion : {0})", e));
                ViewBag.Error = ErrorList.suiteDescription;

            }


            return View(model);
        }


        public ActionResult SuiteQuestion()
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("SuiteQuestion.UserQuestionnaire.Entrée(idElementSession: {0})", DALSession.element.Id));
            ViewModelUserQuestionnaire model = new ViewModelUserQuestionnaire();
            try
            {
                model.imagesUp = DALMedia.SelectAllFromElement(DALSession.element.Id);
                model.listChoix = DALChoix.SelectAllByElement(DALSession.element.Id);
                model.numeroQuestion = DALElement.FindNumeroQuestion(DALSession.element.Id);
                model.element = DALSession.element;
            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("SuiteQuestion.UserQuestionnaire.Exception(Excpetion : {0})", e));
                ViewBag.Error = ErrorList.suiteQuestion;

            }

            return View(model);
        }

        public ActionResult ResultatQuestionnaire()
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("resultatQuestionnaire.UserQuestionnaire.Entrée(resultatSession: {0})", DALSession.ResultatQuestionnaire));

            var resultat = false;
            DALSession.sessionFinish = 1;
            float resultatFinal = 0;
            try
            {
                if (DALSession.nombreBonneReponses == 0)
                {
                    resultatFinal = 0;
                }
                else
                {
                    resultatFinal =
                        (float)(DALSession.nombreBonneReponses * 20 / DALElement.SelectAllQuestionFromQuestionnaire(DALSession.questionnaire.Id).Count);
                    DALSession.noteFinale = resultatFinal;
                }
                if (resultatFinal >= DALSession.questionnaire.note)
                {
                    DALSession.ResultatQuestionnaire = 1;
                }
                else
                {
                    DALSession.ResultatQuestionnaire = 0;
                }
                Utilisateurs utilisateur = new Utilisateurs();
                utilisateur.nom = DALSession.nom.ToUpper();
                utilisateur.prenom = DALSession.prenom.ToUpper();
                if (DALSession.idUtilisateur != 0)
                {
                    utilisateur.Id = DALSession.idUtilisateur;
                    DALUtilisateur.UpdateUtilisateur(utilisateur);
                }
                else
                {
                    DALUtilisateur.InsertUtilisateur(utilisateur);
                }
                Resultat result = new Resultat();
                result.noteObtenue = resultatFinal;
                result.tempsPassage = Int32.Parse((DateTime.Now - DALSession.dateDebut).Minutes.ToString());
                result.idUtilisateur = utilisateur.Id;
                result.idQuestionnaire = DALSession.questionnaire.Id;
                result.datePassage = DALSession.dateDebut;
                result.intituleQuestionnaire = DALSession.questionnaire.intitule;
                DALResultat.InsertResultat(result);

                foreach (var r in DALSession.reponses)
                {
                    Reponses reponse = new Reponses();
                    reponse.statut = r.statut;
                    reponse.idElement = r.idElement;
                    reponse.idResultat = result.Id;
                    reponse.intitueleElement = r.intituleElement;
                    reponse.texteQuestion = r.texteQuestion;
                    DALReponse.InsertReponse(reponse);
                }
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("resultatQuestionnaire.UserQuestionnaire.Entrée(idResultatInsert : {0}, idUtilisateurInsert{1})", result.Id, utilisateur.Id));

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("resultatQuestionnaire.UserQuestionnaire.Exception(Exception : {0})", e));
                ViewBag.Error = ErrorList.resultatQuestionnaire;

            }

            return View(resultat);
        }


        public ActionResult RetourDescription(int idElement)
        {

            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("retourDescription.UserQuestionnaire.Entrée(idElement : {0})", idElement));
            ViewModelUserQuestionnaire model = new ViewModelUserQuestionnaire();
            Elements element = new Elements();
            try
            {
                var el = DALElement.FindById(idElement);

                if (el.ordre <= 2)
                {
                    return RedirectToAction("Index", "UserQuestionnaire");

                }
                else
                {

                    //List<Elements> elements = ElementManager.SelectAllRetourDescriptionDesc(SessionManager.questionnaire.Id,ordre);
                    element = DALElement.SelectElementRetourDescription(el);
                    model.element = element;
                    model.imagesUp = DALMedia.SelectAllFromElement(element.Id);
                    model.imagesDown = DALMedia.SelectAllFromElementDown(element.Id);
                    model.video = DALMedia.SelectVideoFromElement(element.Id);
                }

                Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("retourDescription.UserQuestionnaire.Sortie(idElement retour : {0})", element.Id));

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("retourDescription.UserQuestionnaire.Exception(Exception : {0})", e));
                ViewBag.Error = ErrorList.retourDescription;
            }

            return View(model);
        }
        public ActionResult ApresDescription(int idElement)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("apresDescription.UserQuestionnaire.Entrée(idElement : {0})", idElement));
            
            ViewModelUserQuestionnaire model = new ViewModelUserQuestionnaire();
            try
            {
                var element = DALElement.FindById(idElement);
                Elements descriptionApres = DALElement.SelectElementApresDescription(element);
                Elements elementApres = DALElement.elementOrdreApres(element);
                if (elementApres.Id == DALSession.element.Id || descriptionApres.Id == DALSession.element.Id)
                {
                    return RedirectToAction("RetourElementEnCours", "UserQuestionnaire");
                }
                else
                {
                    model.element = descriptionApres;
                    model.imagesUp = DALMedia.SelectAllFromElement(descriptionApres.Id);
                    model.imagesDown = DALMedia.SelectAllFromElementDown(descriptionApres.Id);
                }
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("apresDescription.UserQuestionnaire.Sortie(idElement description Apres: {0})", descriptionApres.Id));

            }
            catch (Exception e)
            {

                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("apresDescription.UserQuestionnaire.Excpetion(Exception : {0})", e));
                ViewBag.Error = ErrorList.apresDescription;
            }


            return View(model);
        }

        public ActionResult RetourElementEnCours()
        {
            if (DALSession.element.TypeElement_Id == 1)
            {
                return RedirectToAction("SuiteDescription", "UserQuestionnaire");
            }
            else
            {
                return RedirectToAction("SuiteQuestion", "UserQuestionnaire");
            }
        }
        public JsonResult ReponseQuestion(Boolean resultat,int idQuestion)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("reponseQuestion.UserQuestionnaire.Entrée(idQuestion : {0}, resultatQuestion : {1})", idQuestion,resultat));
            var dejaRepondu = false;
            try
            {
                
                foreach (var id in DALSession.listeIdQuestionsRepondu)
                {

                    if (idQuestion == id)
                    {
                        dejaRepondu = true;
                    }
                }

                if (dejaRepondu != true)
                {
                    ViewModelReponse model = new ViewModelReponse();
                    model.idElement = DALSession.element.Id;
                    model.intituleElement = DALSession.element.intitule;
                    model.idQuestionnaire = DALSession.questionnaire.Id;
                    model.intituleQuestionnaire = DALSession.questionnaire.intitule;
                    model.statut = resultat;
                    model.texteQuestion = DALSession.element.texte;
                    DALSession.nombreQuestionsRepondu++;
                    if (resultat == true)
                    {
                        DALSession.nombreBonneReponses++;
                    }
                    List<int> i = DALSession.listeIdQuestionsRepondu;
                    i.Add(DALSession.element.Id);
                    DALSession.listeIdQuestionsRepondu = i;

                    List<ViewModelReponse> r = DALSession.reponses;
                    r.Add(model);
                    DALSession.reponses = r;
                }
            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("reponseQuestion.UserQuestionnaire.Exception(Exception : {0})", e));
                return Json(ErrorList.reponseQuestion);

            }



            return Json(dejaRepondu);
        }

        public ActionResult Refresh()
        {
            DALSession.sessionFinish = 0;
            DALSession.sessionStart = 0;
            DALSession.listeIdElement = new List<int>();
            DALSession.listeIdQuestionsRepondu = new List<int>();
            DALSession.nombreBonneReponses = 0;
            DALSession.nombreQuestionsRepondu = 0;
            DALSession.ResultatQuestionnaire = 0;
            DALSession.noteFinale = 0;
            
            return RedirectToAction("Login", "UserHome");
        }
        //La méthode avec le Jquery



        //public JsonResult CheckNextElementJquery()
        //{
        //    var type = ElementManager.FindElementByOrdre((int) SessionManager.numElement + 1).TypeElement_Id;
        //    SessionManager.element = ElementManager.FindElementByOrdre((int) SessionManager.numElement+ 1);
        //    return Json(type);
        //}
        //public JsonResult SuiteDescriptionJquery()
        //{
        //    return Json(new
        //    {
        //        redirectUrl = Url.Action("SuiteDescription", "UserQuestionnaire"),
        //        isRedirect = true
        //    });
        //}
        //public JsonResult SuiteQuestionJquery()
        //{
        //    ViewModelUserQuestionnaire model = new ViewModelUserQuestionnaire();
        //    Videos video;
        //    model.imagesUp = MediaManager.SelectAllFromElement(SessionManager.element.Id);
        //    if (MediaManager.SelectVideoFromElement(SessionManager.element.Id) != null)
        //    {
        //        model.video = MediaManager.SelectVideoFromElement(SessionManager.element.Id);
        //    }
        //    return Json(new
        //    {
        //        redirectUrl = Url.Action("SuiteQuestion", "UserQuestionnaire", new { model = model }),
        //        isRedirect = true
        //    });
        //}
    }

}