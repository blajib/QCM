using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using WebGrease.Css.Extensions;

namespace QCMApp.bll
{
    public class DALMedia
    {
        public static void AddMediaUp(Images image)
        {
            //Le 1 d'ordre indique que cela sera sité en haut de l'élément. Haut dessus le texte
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    context.Images.Add(image);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("Erreur AddMediaUp MediaManager({0})", e));

                }

            }
        }
        public static List<Images> SelectAllFromElement(int idElement)
        {
            List<Images> listeImages = null;
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    listeImages = context.Images.Where(i => i.idelement == idElement && i.position == 1 )
                        .Select(i => i)
                        .OrderBy(i => i.ordre)
                        .ToList();
                }
                catch (Exception e)
                {
                    throw e;

                }

            }

            return listeImages;
        }
        public static List<Images> SelectVeryAllFromElement(int idElement)
        {
            List<Images> listeImages = null;
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    listeImages = context.Images.Where(i => i.idelement == idElement)
                        .Select(i => i)
                        .OrderBy(i => i.ordre)
                        .ToList();
                }
                catch (Exception e)
                {
                    throw e;

                }

            }

            return listeImages;
        }
        public static List<Images> SelectAllFromElementDown(int idElement)
        {
            List<Images> listeImages = null;
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    listeImages = context.Images.Where(i => i.idelement == idElement && i.position == 2)
                        .Select(i => i)
                        .OrderBy(i => i.ordre)
                        .ToList();
                }
                catch (Exception e)
                {
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("Erreur SelectAllFromElementDown MediaManager({0})", e));

                }

            }
            

            return listeImages;
        }


        public static Images FindImageById(int idImage)
        {
            Images image = null;
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    image = context.Images.Find(idImage);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }


            }

            return image;
        }

        public static void UpdateImage(Images image)
        {
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    context.Images.AddOrUpdate(image);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
        }



        public static Images ImageAvant(Images image)
        {
            Images imageRetour = new Images();
            using (var context = new QCMAppBDDEntities())
            {
                imageRetour = context.Images.Where(i => i.ordre < image.ordre && i.idelement == image.idelement && i.position == 1)
                    .Select(i => i).OrderByDescending(i => i.ordre)
                    .FirstOrDefault();
            }

            return imageRetour;
        }

        public static Images ImageApres(Images image)
        {
            Images imageApres = new Images();
            using (var context = new QCMAppBDDEntities())
            {
                imageApres = context.Images.Where(i => i.ordre > image.ordre && i.idelement == image.idelement && i.position == 1)
                    .Select(i => i).OrderBy(i => i.ordre)
                    .FirstOrDefault();
            }

            return imageApres;
        }


        public static void DeleteImage(int idImage)
        {
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    Images image = context.Images.Find(idImage);
                    context.Images.Remove(image);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        public static List<Images> ListeApresImage(Images imageBis)
        {
            List<Images> images;
            using (var context = new QCMAppBDDEntities())
            {
                     images = context.Images.Where(i => i.ordre > imageBis.ordre && i.idelement == imageBis.idelement)
                    .Select(i => i).OrderBy(i => i.ordre)
                    .ToList();
            }

            return images;
        }

        public static void ReorderAfterDelete(List<Images> images, int ordreImageDelete)
        {
            foreach (var image in images)
            {
                image.ordre = ordreImageDelete;
                UpdateImage(image);
                ordreImageDelete++;
            }
        }

        public static void AddVideoUp(Videos video)
        {
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    context.Videos.Add(video);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                
            }
        }

        public static Videos FindVideoById(int idVideo)
        {
            Videos video;
            using (var context = new QCMAppBDDEntities())
            {
                video = context.Videos.Find(idVideo);
            }

            return video;
        }

        public static Videos SelectVideoFromElement(int idElement)
        {
            Videos video;
            using (var context = new QCMAppBDDEntities())
            {
                video = context.Videos.Where(v => v.idelement == idElement)
                    .Select(v => v)
                    .FirstOrDefault();
            }

            return video;
        }

        public static void DeleteVideo(int idVideo)
        {
            

            using (var context = new QCMAppBDDEntities())
            {
                var video = context.Videos.Find(idVideo);
                context.Videos.Remove(video);
                context.SaveChanges();
            }
        }


    }
}