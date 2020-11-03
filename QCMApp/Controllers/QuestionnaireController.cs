using QCMApp.bll;
using QCMApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using QCMApp.Tools;

namespace QCMApp.Controllers
{
    public class QuestionnaireController : Controller
    {

        private const string _CST_DIRECTORY_IMAGE = "~/Content/images/";
        private const string _CST_DIRECTORY_VIDEO = "~/Content/videos/";


        /**
         * Méthode qui dirige vers la page listeQuestionnaire
         *
         */
        public ActionResult ListeQuestionnaires(String erreur)
        {
            ViewModelQuestionnaireSite vmqs = new ViewModelQuestionnaireSite();
            if (erreur != null)
            {
                ViewBag.Error = erreur;
            }
            try
            {
                vmqs.questionnaires = DALQuestionnaire.SelectAll();
                vmqs.sites = DALSite.SelectAllSite();
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorList.listeQuestionnaire;
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("QuestionnairesController.ListeQuestionnaires.Exception(Nombres questionnaire : {0},nombre sites : {1} message erreur : {2})", vmqs.questionnaires.Count, vmqs.sites.Count, e));

            }



            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("QuestionnairesController.ListeQuestionnaires.Sortie(Nombres questionnaire : {0},nombre sites : {1})", vmqs.questionnaires.Count, vmqs.sites.Count));
            return View(vmqs);
        }
        /**
         * Dirige vers la page de création du questionnaire
         */
        public ActionResult PageCreateIntituleQuestionnaire()
        {
            return View();
        }
        /**
         * Va créer le questionnaire juste avec l'intitulé car plus facile à gérer par la suite
         * puis dirige vers la page de création du questionnaire
         */
        public ActionResult CreateIntituleQuestionnaire(string intitule)
        {
            ViewModelQuestionnaireElements vm = new ViewModelQuestionnaireElements();
            var questionnaireEntity = new Questionnaires();
            int id = 0;
            try
            {

                questionnaireEntity.intitule = intitule;
                questionnaireEntity.date = DateTime.Now;
                questionnaireEntity.note = 16;
                questionnaireEntity.actif = false;
                DALQuestionnaire.InsertQuestionnaire(questionnaireEntity);
                id = questionnaireEntity.Id;

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("CreateIntituleQuestionnaire.Questionnaire.Exception(Questionnaire créé : {0}, exception : {1})", Tools.JsonHelper.Serialize(questionnaireEntity, typeof(Questionnaires)), e));
                return RedirectToAction("ListeQuestionnaires", "Questionnaire", new { erreur = ErrorList.intituleQuestionnaire });
            }


            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("CreateIntituleQuestionnaire.Questionnaire.Sortie(Questionnaire créé : {0})", Tools.JsonHelper.Serialize(questionnaireEntity, typeof(Questionnaires))));

            return RedirectToAction("PageCreateQuestionnaire", "Questionnaire", new { id = id });
        }

        [HttpGet]
        public ActionResult PageCreateQuestionnaire(int id, String erreur)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("PageCreateQuestionnaire.Questionnaire.Entrée(idQuestionnaire : {0})", id));
            if (erreur != null)
            {
                ViewBag.Error = erreur;
            }

            var questionnaireEntity = new Questionnaires();
            ViewModelQuestionnaireElements vmqe = new ViewModelQuestionnaireElements();
            try
            {
                questionnaireEntity = DALQuestionnaire.FindById(id);
                vmqe.questionnaire = questionnaireEntity;
                vmqe.elements = DALElement.SelectAllFromQuestionnaire(id);


            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("PageCreateQuestionnaire.Questionnaire.Exception(idQuesstionnaire : {0}, exception : {1}", id, e));
                return RedirectToAction("ListeQuestionnaires", "Questionnaire", new { erreur = ErrorList.pageCreateQuestionnaire });
            }


            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("PageCreateQuestionnaire.Questionnaire.Sortie(idQuestionnaire : {0})", id));

            return View(vmqe);
        }
        /**
         * Autre méthode en Json résult qui dirige vers la méthode PageCreatQuestionnaire
         */
        public JsonResult PageCreateQuestionnaireJquery(int id)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("PageCreateQuestionnairJquery.Questionnaire.Entrée(idQuestionnaire : {0})", id));

            return Json(new
            {
                redirectUrl = Url.Action("PageCreateQuestionnaire", "Questionnaire", new { id = id }),
                isRedirect = true
            });
        }

        /**
         * Le questionnaire est déja créer avec la création de l'intitule et la on va ajouter le texte, la date de création
         * la note minim.
         */
        public ActionResult CreateQuestionnaireJquery(string intitule, int? note, int id)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("CreateQuestionnaireJquery.Questionnaire.Entrée(intitule : {0},note : {1}, id : {2})", intitule, note, id));

            var questionnaireEntity = new Questionnaires();
            questionnaireEntity.intitule = intitule;
            questionnaireEntity.note = note;
            questionnaireEntity.date = DateTime.Now;
            questionnaireEntity.Id = id;
            try
            {
                DALQuestionnaire.UpdateQuestionnaire(questionnaireEntity);
            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("CreateQuestionnaireJquery.Questionnaire.Exception(questionnaire : {0})", Tools.JsonHelper.Serialize(questionnaireEntity, typeof(Questionnaires))));
                return Json(new
                {
                    redirectUrl = Url.Action("ListeQuestionnaires", "Questionnaire", ErrorList.createQuestionnaireJquery),
                    isRedirect = true
                });
            }

            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("CreateQuestionnaireJquery.Questionnaire.Sortie(questionnaire : {0})", Tools.JsonHelper.Serialize(questionnaireEntity, typeof(Questionnaires))));

            return Json(new
            {
                redirectUrl = Url.Action("ListeQuestionnaires", "Questionnaire"),
                isRedirect = true
            });
        }

        public RedirectToRouteResult CopyQuestionnaire(int id)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("CopyQuestionnaire.Questionnaire.Entrée({0})", id));
            int? newId = null;

            try
            {
                newId = DALQuestionnaire.CopyQuestionnaire(id, Server.MapPath(Tools.ConfigHelper._CST_DIRECTORY_IMAGE), Server.MapPath(Tools.ConfigHelper._CST_DIRECTORY_VIDEO));

                Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("CopyQuestionnaire.Questionnaire.Sortie(idQuestionnaire : {0})", id));

                if (newId.HasValue)
                    return RedirectToAction("PageCreateQuestionnaire", "Questionnaire", new { id = newId.Value });
                else
                    return RedirectToAction("ListeQuestionnaires", "Questionnaire");

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("CopyQuestionnaire({0})", id), e);
                return RedirectToAction("ListeQuestionnaires", "Questionnaire");
            }

        }

        /**
         * Fonction delete appelé en Jquery
         */
        public JsonResult DeleteQuestionnaireJquery(int idQuestionnaire)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("DeleteQuestionnaire.Questionnaire.Entrée({0})", idQuestionnaire));
            MediaController med = new MediaController();
            var pothImage = Path.Combine(Server.MapPath(_CST_DIRECTORY_IMAGE));
            var pothVideo = Path.Combine(Server.MapPath(_CST_DIRECTORY_VIDEO));

            try
            {
                var questionnaire = DALQuestionnaire.FindById(idQuestionnaire);
                if (DALElement.SelectAllQuestionFromQuestionnaire(idQuestionnaire) != null)
                {
                    var elements = DALElement.SelectAllFromQuestionnaire(idQuestionnaire);
                    foreach (var element in elements)
                    {
                        if (DALMedia.SelectAllFromElement(element.Id) != null)
                        {
                            var images = DALMedia.SelectAllFromElement(element.Id);
                            foreach (var image in images)
                            {

                                med.DeleteImage(image.Id, pothImage);
                            }
                        }

                        if (DALMedia.SelectVideoFromElement(element.Id) != null)
                        {
                            med.DeleteVideo(DALMedia.SelectVideoFromElement(element.Id).Id,pothVideo);
                        }

                        if (element.TypeElement_Id == 2)
                        {
                            if (DALChoix.SelectAllByElement(element.Id) != null)
                            {
                                var choix = DALChoix.SelectAllByElement(element.Id);
                                foreach (var c in choix)
                                {
                                    if (c.image_id != null)
                                    {
                                        med.DeleteImage((int)c.image_id,pothImage);
                                    }
                                }
                            }
                        }
                    }
                }
                DALQuestionnaire.DeleteQuestionnaire(idQuestionnaire);

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("DeleteQestionnaireJquery.Questionnaire.Exception(idquestionnaire : {0})", idQuestionnaire));
                //return RedirectToAction("ListeQuestionnaires", "Questionnaire", new { erreur = ErrorList.intituleQuestionnaire });
                return Json(new
                {
                    redirectUrl = Url.Action("ListeQuestionnaires", "Questionnaire", new { erreur = ErrorList.deleteQuestionnaire }),
                    isRedirect = true
                });
            }
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("DeleteQuestionnaire.Questionnaire.Sortie(idQuestionnaire : {0})", idQuestionnaire));

            return Json(new
            {
                redirectUrl = Url.Action("ListeQuestionnaires", "Questionnaire"),
                isRedirect = true
            });
        }
        /**
         * Fonction Async pour rendre actif le questionnaire sur la liste
         */
        public JsonResult UpdateActifQuestionnaire(int idQuestionnaire, Boolean boolActif)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("UpdateActifQuestionnaire.Questionnaire.Entrée(idQuestionnaire : {0},actif ou pas : {1})", idQuestionnaire, boolActif));
            String retour = null;
            try
            {
                var questionnaire = DALQuestionnaire.FindById(idQuestionnaire);
                questionnaire.actif = boolActif;
                DALQuestionnaire.UpdateQuestionnaire(questionnaire);


            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("UpdateActifQuestionnaire.Questionnaire.Exception(idQuestionnaire : {0},actif ou pas : {1}, exception : {2})", idQuestionnaire, boolActif, e));

                return Json(ErrorList.updateActifException);
            }
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("UpdateActifQuestionnaire.Questionnaire.Sortie(idQuestionnaire : {0},actif ou pas : {1})", idQuestionnaire, boolActif));


            return Json("hey");
        }
        public ActionResult searchFilterQuestionnaire(ViewModelQuestionnaireFiltre qf)/*(DateTime? dateDepart, DateTime dateFin,Boolean? actif,int? idSite)*/
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("searchFilterQuestionnaire.Questionnaire.Entrée(date depart : {0}, dateFin : {1}, actif : {2}, idSite : {3})", qf.dateDepart, qf.dateFin, qf.actif, qf.idSite));

            ViewModelQuestionnaireSite model = new ViewModelQuestionnaireSite();
            try
            {
                model = DALQuestionnaire.searchFilterQuestionnaire(qf);
                model.sites = DALSite.SelectAllSite();
            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("searchFilterQuestionnaire.Questionnaire.Exception(date depart : {0}, dateFin : {1}, actif : {2}, idSite : {3}, exception : {4})", qf.dateDepart, qf.dateFin, qf.actif, qf.idSite, e));
                RedirectToAction("ListeQuestionnaires", "Questionnaire", new { erreur = ErrorList.searchFilterQuestionnaire });
            }
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("searchFilterQuestionnaire.Questionnaire.Sortie(nombre de questionnaire après tri{0})", model.questionnaires.Count));


            return View("ListeQuestionnaires", model);
        }

        public JsonResult ListQuestionnairesBySite(int idSite)
        {
            String erreur = null;
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("ListQuestionnairesBySite.Questionnaire.Entrée(idSite :{0})", idSite));
            List<Questionnaires> questionnaires = new List<Questionnaires>();
            try
            {
                questionnaires = DALQuestionnaire.FindQuestionnairesByIdSite(idSite);

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("ListQuestionnairesBySite.Questionnaire.Exception(idSite :{0}, exception : {1})", idSite, e));
                erreur = ErrorList.listQuestionnaireBySite;
                return Json(erreur);
            }

            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("ListQuestionnairesBySite.Questionnaire.Sortie(idSite :{0})", idSite));

            //return Json(questionnaires);
            return Json(questionnaires.Select(q => new { ID = q.Id, intitule = q.intitule }));
        }
        public JsonResult ListQuestionnairesActifBySite(int idSite)
        {
            String erreur = null;
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("ListQuestionnairesBySite.Questionnaire.Entrée(idSite :{0})", idSite));
            List<Questionnaires> questionnaires = new List<Questionnaires>();
            try
            {
                questionnaires = DALQuestionnaire.FindQuestionnairesActifByIdSite(idSite);

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("ListQuestionnairesBySite.Questionnaire.Exception(idSite :{0}, exception : {1})", idSite, e));
                erreur = ErrorList.listQuestionnaireBySite;
                return Json(erreur);
            }

            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("ListQuestionnairesBySite.Questionnaire.Sortie(idSite :{0})", idSite));

            //return Json(questionnaires);
            return Json(questionnaires.Select(q => new { ID = q.Id, intitule = q.intitule }));
        }


    }
}