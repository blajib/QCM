using QCMApp.bll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QCMApp.Models;
using QCMApp.Tools;

namespace QCMApp.Controllers
{// les éléments sont les descriptions !!!!!!
    public class DescriptionController : Controller
    {      
        
        /**
         * Amène à la page description
         */
        public ActionResult PageCreateDescription(int id)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("PageCreateDescription.Description.Entrée(idQuestionnaire : {0})", id));

            Questionnaires questionnaire = new Questionnaires();
            try
            {
                questionnaire = DALQuestionnaire.FindById(id);

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("PageCreateDescription.Description.Exception(exception : {0})", e));
                return RedirectToAction("PageCreateQuestionnaire", "Questionnaire",new {id = id, error = ErrorList.pageCreateDescription});
            }

            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("PageCreateDescription.Description.Sortie(questionnaire : {0})", Tools.JsonHelper.Serialize(questionnaire, typeof(Questionnaires))));
            return View(questionnaire);
        }
        /**
         * Créer la description puis amène à la page UpdateDescription
         */
        public ActionResult CreateDescription(string intitule, string texte,int idQuestionnaire)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("CreateDescription.Description.Entrée(idQuestionnaire : {0})", idQuestionnaire));

            ViewModelQuestionnaireElements model = new ViewModelQuestionnaireElements();
            Elements element = new Elements();
            Questionnaires questionnaire = new Questionnaires();
            int count;
            try
            {
                count = DALElement.SelectAllFromQuestionnaire(idQuestionnaire).Count;
                element.intitule = intitule;
                element.texte = texte;
                element.questionnaire_id = idQuestionnaire;
                element.TypeElement_Id = 1;
                element.ordre = count + 2;
                DALElement.InsertElement(element);
                model.idElement = element.Id;
                model.idQuestionnaire = idQuestionnaire;
                model.texte = texte;
                model.intituleElement = intitule;

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("PageCreateDescription.Description.Exception(Exception: {0})", e));
                return RedirectToAction("PageCreateQuestionnaire", "Questionnaire",new{id = idQuestionnaire, error = ErrorList.createDescription});
            }


            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("PageCreateDescription.Description.Sortie(idQuestionnaire : {0})", model.idQuestionnaire));

            return RedirectToAction("PageUpdateDescription", "Description",model);
        }
        /**
         * Va effacer un élément puis réactualiser la page createQuestionnaire. Possible amélioration en
         * la mettant en Asyn avec Jquery
         */
        public ActionResult DeleteElement(int id)
        {
            MediaController med = new MediaController();
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("DeleteDescription.Description.Entrée(idQuestionnaire : {0})", id));
            var pothImage = Path.Combine(Server.MapPath(ConfigHelper._CST_DIRECTORY_IMAGE));
            var pothVideo = Path.Combine(Server.MapPath(ConfigHelper._CST_DIRECTORY_VIDEO));
            Elements element = new Elements();
            try
            {
                element = DALElement.FindById(id);
                if (DALMedia.SelectVeryAllFromElement(id) != null)
                {
                    foreach (var image in DALMedia.SelectVeryAllFromElement(id))
                    {
                        med.DeleteImage(image.Id, pothImage);
                    }
                }

                if (DALMedia.SelectVideoFromElement(id) != null)
                {
                    med.DeleteVideo(DALMedia.SelectVideoFromElement(id).Id, pothVideo);
                }
                DALElement.DeleteElement(element.Id);

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("DeleteDescription.Description.Exception(Exception : {0})", e));
                return RedirectToAction("PageCreateQuestionnaire", "Questionnaire", new{id = id, error = ErrorList.deleteDescription});

            }

            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("DeleteDescription.Description.Sortie(idQuestionnaire : {0})", element.questionnaire_id));

            return RedirectToAction("PageCreateQuestionnaire", "Questionnaire", new { id = element.questionnaire_id});
        }

        public ActionResult PageUpdateDescription(ViewModelQuestionnaireElements model) /* idQuestionnaire , texte , intituleElement, idElement*/
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("PageUpdateDescription.Description.Entrée(idQuestionnaire : {0}, idElement{1})", model.idQuestionnaire,model.idElement));

            Elements element = new Elements();
            try
            {
                element = DALElement.FindById(model.idElement);
                model.idQuestionnaire = (int)element.questionnaire_id;
                model.texte = element.texte;
                model.intituleElement = element.intitule;
                model.idElement = element.Id;
                model.listeImagesUp = DALMedia.SelectAllFromElement(element.Id);
                model.listeImagesDown = DALMedia.SelectAllFromElementDown(element.Id);
                model.video = DALMedia.SelectVideoFromElement(element.Id);

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("PageUpdateDescription.Description.Exception(exception: {0})", e));
                return RedirectToAction("PageCreateQuestionnaire", "Questionnaire",new{id = model.idQuestionnaire, error = ErrorList.pageUpdateDescription});

            }
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("PageUpdateDescription.Description.Sortie(idQuestionnaire : {0}, idElement{1})", model.idQuestionnaire,model.idElement));
            
            return View(model);
        }
        /**
         * Fonction en Async qui ramene jsuque à la pag CreateQuestionnaire
         */
        public JsonResult UpdateDescriptionAsyn(ViewModelQuestionnaireElements model)/*idElement*/
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("UpdateDescriptionAsyn.Description.Entrée(idElement : {0})", model.idElement));

            var element = new Elements();
            try
            {
                element = DALElement.FindById(model.idElement);
                element.intitule = model.intituleElement;
                element.texte = model.texte;
                DALElement.UpdateElement(element);
            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("UpdateDescriptionAsyn.Description.Exception(Exception : {0})", e));
                return Json(ErrorList.UpdateDescriptionAsyn);

            }

            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("UpdateDescriptionAsyn.Description.Sortie(idQuestionnaire: {0})", element.questionnaire_id));
            
            return Json(element.questionnaire_id);

        }
    }
}