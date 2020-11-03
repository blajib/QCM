using QCMApp.bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QCMApp.Tools;

namespace QCMApp.Controllers
{
    public class ElementController : Controller
    {


        // GET: Element
        public ActionResult ElementUp(int idElement)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("elementUp.Element.Entrée(idElement: {0})", idElement));
            Elements element = new Elements();
            try
            {

                element = DALElement.FindById(idElement);
                DALElement.elementUp(idElement);
            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("elementUp.Element.Exception(Exception: {0})", e));
                ViewBag.Error = ErrorList.elementDown;
            }


            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("elementUp.Element.Sortie(idQuestionnaire: {0})", element.questionnaire_id));

            return RedirectToAction("PageCreateQuestionnaire", "Questionnaire", new { id = element.questionnaire_id });
        }
        public ActionResult ElementDown(int idElement)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("elementUp.Element.Entrée(idElement: {0})", idElement));
            Elements element = new Elements();
            try
            {

                element = DALElement.FindById(idElement);
                DALElement.elementDown(idElement);
            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("elementUp.Element.Exception(Exception: {0})", e));
                ViewBag.Error = ErrorList.elementDown;
            }


            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("elementUp.Element.Sortie(idQuestionnaire: {0})", element.questionnaire_id));

            return RedirectToAction("PageCreateQuestionnaire", "Questionnaire", new { id = element.questionnaire_id });
        }
    }
}