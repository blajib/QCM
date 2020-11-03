using QCMApp.bll;
using QCMApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QCMApp.Tools;

namespace QCMApp.Controllers
{
    public class ChoixController : Controller
    {
        
        public JsonResult InsertChoix(ViewModelChoixQuestion model)/*idElement*/
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info,
                string.Format("InsertChoix.Choix.Entrée(idElement: {0})", model.idElement));

            int idChoixRetour = 0;
            if (model.idElement != null)
            {
                try
                {
                    Choixes choix = new Choixes();
                    choix.element_id = model.idElement;
                    choix.statut = false;
                    DALChoix.InsertChoix(choix);
                    idChoixRetour = choix.Id;
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info,
                        string.Format("insertChoix.Choix.Sortie(idChoix: {0})", choix.Id));
                }
                catch (Exception e)
                {
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, 
                        string.Format("insertChoix.Choix.Exception(exception : {0})", e));
                    return Json(ErrorList.insertChoix);
                }
            }
            return Json(idChoixRetour);
        }

        public JsonResult UpdateChoix(ViewModelChoixQuestion model)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("updateChoix.Choix.Entrée(idElement: {0})", model.idElement));

            int idChoixRetour = 0;
            try
            {

                var choix = DALChoix.FindById(model.idChoix);
                choix.intitule = model.intituleChoix;
                choix.statut = model.statutChoix;
                DALChoix.UpdateChoix(choix);
                idChoixRetour = choix.Id;
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("updateChoix.Choix.Sortie(idChoix: {0})", choix.Id));
            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("insertChoix.Choix.Exception(Exception : {0})", e));
                return Json(ErrorList.updateChoix);
            }



            return Json(idChoixRetour);
        }

        public JsonResult DeleteChoix(int idChoix)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("deleteChoix.Choix.Entrée(idChoix: {0})", idChoix));
            var pothImage = Path.Combine(Server.MapPath(ConfigHelper._CST_DIRECTORY_IMAGE));
            MediaController med = new MediaController();
            var choix = DALChoix.FindById(idChoix);
            try
            {

                if (choix.image_id != null)
                {
                    med.DeleteImage((int)choix.image_id,pothImage);
                    DALMedia.DeleteImage((int)choix.image_id);
                }
                DALChoix.DeleteChoix(idChoix);
            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("deleteChoix.Choix.Exception(Exception: {0})", e));
                return Json(ErrorList.deleteChoix);
            }



            return Json(1);
        }

    }
}