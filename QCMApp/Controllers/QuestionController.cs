using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QCMApp.bll;
using QCMApp.Models;
using QCMApp.Tools;

namespace QCMApp.Controllers
{
    public class QuestionController : Controller
    {        

        /**
         * Methode qui amène vers la page création
         */
        public ActionResult PageCreateQuestion(ViewModelChoixQuestion model)/*idQuestionnaire*/
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("PageCreateQuestion.Question.Entrée(idQuestionnaire : {0})", model.idQuestionnaire));
            
            ViewModelChoixQuestion modelRetour = new ViewModelChoixQuestion();
            modelRetour.idQuestionnaire = model.idQuestionnaire;
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("PageCreateQuestionnaire.Question.Sortie(idQuestionnaire : {0})", modelRetour.idQuestionnaire));

            return View(modelRetour);
        }
        /**
         * Cela insert la question et dirige vers la page updateQuestion
         */
        public ActionResult CreateQuestion(ViewModelChoixQuestion model)/*intituleQuestion, texteQuestion, idQuestionnaire*/
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("CreateQuestion.Question.Entrée(intitule question : {0}, idQuestionnaire : )", model.intituleQuestion,model.idQuestionnaire));

            Elements element = new Elements();
            Questionnaires questionnaire = new Questionnaires();
            int count; 
            try
            {
                count = DALElement.SelectAllFromQuestionnaire(model.idQuestionnaire).Count;
                Choixes choix = new Choixes();
                element.intitule = model.intituleQuestion;
                element.texte = model.texteQuestion;
                element.questionnaire_id = model.idQuestionnaire;
                //Le 2 du type signifie question
                element.TypeElement_Id = 2;
                element.ordre = count + 2;
                DALElement.InsertElement(element);
            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("PageUpdateQuestion.Question.Exception(exception : {0})", e));
                return RedirectToAction("PageCreateQuestionnaire", "Questionnaire",new {erreur = ErrorList.createQuestion});
            }
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("Retour question créer puis redirect vers PageUpdateQuestion({0})", element.Id));

            return RedirectToAction("PageUpdateQuestion", "Question", new { idElement = element.Id } );
        }
        /**
         * Page direct après création de la question ou en cliquant sur le bouton update Question
         */
        public ActionResult PageUpdateQuestion(int idElement)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("PageUpdateQuestion.Question.Entrée(idElement : {0})", idElement));

            ViewModelChoixQuestion model = new ViewModelChoixQuestion();
            try
            {
                model.element = DALElement.FindById(idElement);
                model.listeImagesUp = DALMedia.SelectAllFromElement(idElement);
                model.listeChoix = DALChoix.SelectAllByElement(idElement);
            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("PageUpdateQuestion.QuestionException(idElement : {0}, exception : {1})", idElement,e));
                model.idQuestionnaire = 0;
                return RedirectToAction("PageCreateQuestionnaire", "Questionnaire",new {erreur = ErrorList.PageUpdateQuestion });
            }


            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("PageUpdateQuestion.Question.Sortie(element : {0})", Tools.JsonHelper.Serialize(model.element, typeof(Elements))));

            return View("PageUpdateQuestion", model);
        }

        /**
         *
         */
        public JsonResult UpdateQuestionJquery(ViewModelQuestion model)/*idElement, intituleQuestion, texteQuestion*/
        {

            var element = new Elements(); 
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("UpdateQuestionJquery.Question.Entrée(idElement : {0},intituleQuestion : {1})", model.idElement,model.intituleQuestion));
            try
            {
                element = DALElement.FindById(model.idElement);
                element.intitule = model.intituleQuestion;
                element.texte = model.texteQuestion;
                DALElement.UpdateElement(element);

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("UpdateQuestionJquery.Question.Entrée(Eception : {0}",e));

                return Json(new
                {
                    redirectUrl = Url.Action("PageCreateQuestionnaire", "Questionnaire", new { erreur = ErrorList.updateQuestionJquery}),
                    isRedirect = true
                });
            }
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("UpdateQuestionJquery.Question.Sortie(idQuestionnaire : {0}", element.questionnaire_id));

            return Json(new
            {
                redirectUrl = Url.Action("PageCreateQuestionnaire", "Questionnaire", new { id = element.questionnaire_id }),
                isRedirect = true
            });


        }
        /**
         *Delete question ave l'Id de la question
         */
        public ActionResult DeleteQuestion(int id)
        {
            MediaController med = new MediaController();
            var pothImage = Path.Combine(Server.MapPath(ConfigHelper._CST_DIRECTORY_IMAGE));

            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("DeleteQuestion.Question.Entrée(idElement : {0})", id));
            var idQuestionnaire = 0;
            try
            {

                Elements element = DALElement.FindById(id);
                idQuestionnaire = (int)element.questionnaire_id;
                if (DALMedia.SelectVeryAllFromElement(id) != null)
                {
                    foreach (var image in DALMedia.SelectVeryAllFromElement(id))
                    {
                        med.DeleteImage(image.Id, pothImage);
                    }
                }

                if (DALChoix.SelectAllByElement(id) != null)
                {
                    foreach (var choix in DALChoix.SelectAllByElement(id))
                    {
                        if (choix.image_id != null)
                        {
                            med.DeleteImage((int)choix.image_id,pothImage);
                        }
                    }
                }
                DALElement.DeleteElement(element.Id);
            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("DeleteQuestion.Question.Sortie(idQuestionnaire: {0})", idQuestionnaire));
                return RedirectToAction("PageCreateQuestionnaire", "Questionnaire", new { id = idQuestionnaire, erreur = ErrorList.deleteQuestion });

            }

            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("DeleteQuestion.Question.Sortie(idQuestionnaire: {0})", idQuestionnaire));

            return RedirectToAction("PageCreateQuestionnaire", "Questionnaire", new { id = idQuestionnaire });
        }



    }
}