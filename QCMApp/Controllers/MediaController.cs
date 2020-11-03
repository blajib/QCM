using QCMApp.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Web.Mvc;
using QCMApp.bll;
using QCMApp.Models;

namespace QCMApp.Controllers
{
    public class MediaController : Controller
    {

        

        /**
         *  la position de l'image au dessus le texte est le 1 dans la colonne position
         *  public JsonResult AddImageUp(HttpPostedFileBase  file, int IdElement)
         */
        public JsonResult AddImageUp(HttpPostedFileBase file)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("AddImageUp.Media.Entrée(nom du fichier : {0})", file.FileName));

            var newFileName = ""; 
            var imagePath = Path.Combine(Server.MapPath(Tools.ConfigHelper._CST_DIRECTORY_IMAGE));
            var image = new Images();
            var idImage = 0;
            ViewModelMedia model = new ViewModelMedia();

            if (file != null)
            {
                try
                {
                    //La je dois recup l'Id de l'élément et je fais un substring sur le nom du fichier
                    var y = file.FileName;
                    string idElement = y.Substring(0, y.IndexOf("."));
                    newFileName = Guid.NewGuid().ToString() + "_" +
                                  Path.GetFileName(file.FileName);
                    image.format = y.Substring(idElement.Length + 1);
                    image.nom = newFileName.Substring(0, newFileName.IndexOf("."));
                    image.idelement = int.Parse(idElement);
                    var nbrImages = DALMedia.SelectAllFromElement(int.Parse(idElement)).Count();
                    image.ordre = nbrImages + 2;
                    image.position = 1;
                    DALMedia.AddMediaUp(image);

                    imagePath = imagePath + image.nom + "." + image.format;
                    file.SaveAs(imagePath);
                    /**
                     *test avec la récup du nom de l'appli mais mes images ne sont pu afficher dans mon navigateur
                     */
                    //imagePath = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + @"\Content\images\" + image.nom + "." + image.format;
                    imagePath = Url.Content(Tools.ConfigHelper._CST_DIRECTORY_IMAGE + image.nom + "." + image.format);

                    model.imagePath = imagePath;
                    model.idImage = image.Id;
                    idImage = image.Id;
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("AddImageUp.Media.Sortie(imagePath : {0}, idImage : {1})", model.imagePath, model.idImage));

                }
                catch (Exception e)
                {
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("AddImageUp.Media.Exception(Exception : {0})", e));
                    return Json(ErrorList.addImageUp);
                }

            }

            return Json(model);
        }
        /**
         * La position de l'image est 2
         */
        public JsonResult AddImageDown(HttpPostedFileBase file)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("AddImageDown.Media.Entrée(fileName : {0})", file.FileName));

            var newFileName = "";
            var imagePath = Path.Combine(Server.MapPath(Tools.ConfigHelper._CST_DIRECTORY_IMAGE));
            var image = new Images();
            var idImage = 0;
            ViewModelMedia model = new ViewModelMedia();

            if (file != null)
            {
                try
                {
                    //La je dois recup l'Id de l'élément et je fais un substring sur le nom du fichier
                    var y = file.FileName;
                    string idElement = y.Substring(0, y.IndexOf("."));
                    newFileName = Guid.NewGuid().ToString() + "_" +
                                  Path.GetFileName(file.FileName);
                    image.format = y.Substring(idElement.Length + 1);
                    image.nom = newFileName.Substring(0, newFileName.IndexOf("."));
                    image.idelement = int.Parse(idElement);
                    var nbrImages = DALMedia.SelectAllFromElement(int.Parse(idElement)).Count();
                    image.ordre = nbrImages + 2;
                    image.position = 2;
                    DALMedia.AddMediaUp(image);

                    imagePath = imagePath + image.nom + "." + image.format;
                    file.SaveAs(imagePath);
                    imagePath = Url.Content(Tools.ConfigHelper._CST_DIRECTORY_IMAGE + image.nom + "." + image.format);

                    model.imagePath = imagePath;
                    model.idImage = image.Id;
                    idImage = image.Id;
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("AddImageDown.Media.Sortie(imagePath : {0}, idImage : {1})", model.imagePath, model.idImage));
                }
                catch (Exception e)
                {
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("AddImage.Media.Exception(exception : {0})", e));
                    return Json(ErrorList.addImageDown);

                }

                

            }

            return Json(model);
        }
        /**
         * Position image choix est le 3. Mais ça n'a pas grand intéret
         */
        public JsonResult AddImageChoix(HttpPostedFileBase file, int id)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("AddImageChoix.Media.Entrée(fileName : {0})", file.FileName));

            var newFileName = "";
            var imagePath = Path.Combine(Server.MapPath(Tools.ConfigHelper._CST_DIRECTORY_IMAGE));
            var image = new Images();
            var idImage = 0;
            ViewModelMedia model = new ViewModelMedia();

            if (file != null)
            {
                try
                {
                    //La je dois recup l'Id de l'élément et je fais un substring sur le nom du fichier
                    var y = file.FileName;
                    string idChoix = y.Substring(0, y.IndexOf("."));
                    newFileName = Guid.NewGuid().ToString() + "_" +
                                  Path.GetFileName(file.FileName);
                    image.format = y.Substring(idChoix.Length + 1);
                    image.nom = newFileName.Substring(0, newFileName.IndexOf("."));
                    image.idchoix = int.Parse(idChoix);
                    image.position = 3;
                    DALMedia.AddMediaUp(image);
                    //TODO A voir le format du lien car ne marche pas
                    imagePath = imagePath + image.nom + "." + image.format;
                    file.SaveAs(imagePath);
                    imagePath = Url.Content(Tools.ConfigHelper._CST_DIRECTORY_IMAGE + image.nom + "." + image.format);
                    var choix = DALChoix.FindById(int.Parse(idChoix));
                    if (choix.image_id != null)
                    {
                        DeleteImage((int)choix.image_id);
                    }

                    choix.imagePath = Url.Content(Tools.ConfigHelper._CST_DIRECTORY_IMAGE + image.nom + "." + image.format);
                    choix.image_id = image.Id;
                    DALChoix.UpdateChoix(choix);
                    model.imagePath = imagePath;
                    model.idImage = image.Id;
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("AddImageChoix.Media.Sortie(imagePath : {0}, idImage : {1})", model.imagePath, model.idImage));

                }
                catch (Exception e)
                {
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("AddImageChoix.Media.Exception(Exception : {0})", e));
                    return Json(ErrorList.addImageChoix);

                }

            }

            return Json(model);
        }

        /**
         * Pour les fonction backward et forward j'inverse juste les numéro d'ordre des deux
         * image. Le reste est gérer en Jquery sur la partie front
         */
        public JsonResult BackwardImage(int idImage)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("backwardImage.Media.Entrée(idImage: {0})", idImage));
            try
            {
                var image = DALMedia.FindImageById(idImage);
                var imageAvant = DALMedia.ImageAvant(image);
                var transfertOrdre = image.ordre;
                image.ordre = imageAvant.ordre;
                imageAvant.ordre = transfertOrdre;
                DALMedia.UpdateImage(image);
                DALMedia.UpdateImage(imageAvant);
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("backwardImage.Media.Sortie(imageAvantNouvalleOrdre {0}, imageNouvelleOrdre{1})", imageAvant.ordre, image.ordre));

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("backwardImage.Media.Exception(Exception : {0})", e));
                

            }


            return Json("hey");
        }

        public JsonResult ForwardImage(int idImage)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("forwrdImage.Media.Entrée(idImage: {0})", idImage));

            try
            {
                var image = DALMedia.FindImageById(idImage);
                var imageApres = DALMedia.ImageApres(image);
                var transfertOrdre = image.ordre;
                image.ordre = imageApres.ordre;
                imageApres.ordre = transfertOrdre;
                DALMedia.UpdateImage(image);
                DALMedia.UpdateImage(imageApres);

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("forwrdImage.Media.Exception(Exception : {0})", e));

            }

            return Json("hey");
        }
        /**
         * La fonction delete va supprimer l'image en base de donnée et dans le fichier sur le serveur
         * La fonction gère les images Up,Down et choix.
         */
        public JsonResult DeleteImage(int idImage,string poth=null)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("deleteImage.Media.Entrée(idImage: {0})", idImage));
            string imagePath = null;
            String pathImage = null;
            if (poth != null)
            {
                imagePath = poth;
            }
            else
            {
                imagePath = Path.Combine(Server.MapPath(Tools.ConfigHelper._CST_DIRECTORY_IMAGE));
            }
            


            var image = DALMedia.FindImageById(idImage);
            try
            {
                if (image.idelement != null)
                {
                    if (DALMedia.SelectAllFromElement((int)image.idelement).Count == 1)
                    {
                        pathImage = imagePath + image.nom + "." + image.format;
                        DALMedia.DeleteImage(image.Id);
                        if (System.IO.File.Exists(pathImage))
                        {
                            System.IO.File.Delete(pathImage);
                        }
                    }
                    else
                    {
                        pathImage = imagePath + image.nom + "." + image.format;
                        DALMedia.DeleteImage(image.Id);
                        if (System.IO.File.Exists(pathImage))
                        {
                            System.IO.File.Delete(pathImage);
                        }

                        //var images = MediaManager.ListeApresImage(imageBis);
                        var images = DALMedia.ListeApresImage(image);
                        DALMedia.ReorderAfterDelete(images, (int)image.ordre);
                    }

                }
            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("deleteImage.Media.Exeption(exception : {0})", e));
                return Json(ErrorList.deleteImage);

            }


            try
            {
                if (image.idchoix != null)
                {
                    var choix = DALChoix.FindById((int)image.idchoix);
                    choix.imagePath = null;
                    choix.image_id = null;
                    DALChoix.UpdateChoix(choix);
                    pathImage = imagePath + image.nom + "." + image.format;
                    DALMedia.DeleteImage(image.Id);
                    if (System.IO.File.Exists(pathImage))
                    {
                        System.IO.File.Delete(pathImage);
                    }

                }

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("deleteImageChoix.Media.Exeption(exception : {0})", e));
                return Json(ErrorList.deleteImageChoix);
            }
            
            return Json(false);
        }
        public JsonResult DeleteImageWithPath(int idImage, string poth)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("deleteImage.Media.Entrée(idImage: {0})", idImage));

            string pathImage = null;
            var imagePath = poth;


            var image = DALMedia.FindImageById(idImage);
            try
            {
                if (image.idelement != null)
                {
                    if (DALMedia.SelectAllFromElement((int)image.idelement).Count == 1)
                    {
                        pathImage = imagePath + image.nom + "." + image.format;
                        DALMedia.DeleteImage(image.Id);
                        if (System.IO.File.Exists(pathImage))
                        {
                            System.IO.File.Delete(pathImage);
                        }
                    }
                    else
                    {
                        pathImage = imagePath + image.nom +"."+ image.format;
                        DALMedia.DeleteImage(image.Id);
                        if (System.IO.File.Exists(pathImage))
                        {
                            System.IO.File.Delete(pathImage);
                        }

                        //var images = MediaManager.ListeApresImage(imageBis);
                        var images = DALMedia.ListeApresImage(image);
                        DALMedia.ReorderAfterDelete(images, (int)image.ordre);
                    }

                }
            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("deleteImage.Media.Exeption(exception : {0})", e));
                return Json(ErrorList.deleteImage);

            }


            try
            {
                if (image.idchoix != null)
                {
                    var choix = DALChoix.FindById((int)image.idchoix);
                    choix.imagePath = null;
                    choix.image_id = null;
                    DALChoix.UpdateChoix(choix);
                    pathImage = imagePath + image.nom + "." + image.format;
                    DALMedia.DeleteImage(image.Id);
                    if (System.IO.File.Exists(pathImage))
                    {
                        System.IO.File.Delete(pathImage);
                    }

                }

            }
            catch (Exception e)
            {
                Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("deleteImageChoix.Media.Exeption(exception : {0})", e));
                return Json(ErrorList.deleteImageChoix);
            }

            return Json(false);
        }
        /**
         * Fonction pour ajouter les vidéos
         */
        public JsonResult AddVideo(HttpPostedFileBase file)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("addVideo.Media.Entrée(fileName : {0})", file.FileName));

            var newFileName = "";
            var videoPath = Path.Combine(Server.MapPath(Tools.ConfigHelper._CST_DIRECTORY_VIDEO));

            var video = new Videos();
            var idImage = 0;
            ViewModelMedia model = new ViewModelMedia();

            if (file != null)
            {
                try
                {
                    //La je dois recup l'Id de l'élément et je fais un substring sur le nom du fichier
                    var y = file.FileName;
                    string idElement = y.Substring(0, y.IndexOf("."));
                    newFileName = Guid.NewGuid().ToString() + "_" +
                                  Path.GetFileName(file.FileName);

                    video.format = y.Substring(idElement.Length + 1);
                    video.nom = newFileName.Substring(0, newFileName.IndexOf("."));
                    video.idelement = int.Parse(idElement);
                    videoPath = videoPath + video.nom + "." + video.format;
                    file.SaveAs(videoPath);
                    DALMedia.AddVideoUp(video);
                    videoPath = Url.Content(Tools.ConfigHelper._CST_DIRECTORY_VIDEO + video.nom + "." + video.format);


                    model.videoPath = videoPath;
                    model.idVideo = video.Id;
                    model.formatVideo = video.format;
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("addVideo.Media.Sortie(idVideo : {0})", model.idVideo));

                }
                catch (Exception e)
                {
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("addVideo.Media.Exeption(exception : {0})", e));
                    return Json(ErrorList.addVideo);
                }
                
            }

            return Json(model);
        }
        /**
         * Efface la vidéo dans la base de donnée et dans le fichier sur le serveur
         */
        public JsonResult DeleteVideo(int idVideo,string poth=null)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("deleteVideo.Media.Entrée(idVideo: {0})", idVideo));
            string videoPath = null;
            if (poth != null)
            {
                videoPath = null;
            }
            else
            {
                videoPath = Path.Combine(Server.MapPath(Tools.ConfigHelper._CST_DIRECTORY_VIDEO));
            }

            var video = DALMedia.FindVideoById(idVideo);
            if (video != null)
            {
                try
                {
                    videoPath = videoPath + video.nom + "." + video.format;
                    DALMedia.DeleteVideo(video.Id);
                    if (System.IO.File.Exists(videoPath))
                    {
                        System.IO.File.Delete(videoPath);
                    }
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("deleteVideo.Media.Sortie(videoPath: {0})", videoPath));

                }
                catch (Exception e)
                {
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("deleteVideo.Media.Exception(Exception: {0})", e));
                    return Json(ErrorList.deleteVideo);
                }

            }

            return Json(1);
        }
        public JsonResult DeleteVideoWithPath(int idVideo,String poth)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("deleteVideo.Media.Entrée(idVideo: {0})", idVideo));

            var videoPath = poth;

            var video = DALMedia.FindVideoById(idVideo);
            if (video != null)
            {
                try
                {
                    videoPath = videoPath + video.nom + "." + video.format;
                    DALMedia.DeleteVideo(video.Id);
                    if (System.IO.File.Exists(videoPath))
                    {
                        System.IO.File.Delete(videoPath);
                    }
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("deleteVideo.Media.Sortie(videoPath: {0})", videoPath));

                }
                catch (Exception e)
                {
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Erreur, string.Format("deleteVideo.Media.Exception(Exception: {0})", e));
                    return Json(ErrorList.deleteVideo);
                }

            }

            return Json(1);
        }


    }
}