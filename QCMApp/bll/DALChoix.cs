using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;


namespace QCMApp.bll
{
    public class DALChoix
    {
        public static void InsertChoix(Choixes choix)
        {
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    context.Choixes.Add(choix);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;

                }

            }
        }
        public static Choixes FindById(int id)
        {
            Choixes choix = new Choixes();
            using (var context = new QCMAppBDDEntities())
            {
                try
                {

                }
                catch (Exception e)
                {
                    throw e;
                }
                choix = context.Choixes.Find(id);
            }
                return choix;
        }
        public static List<Choixes> SelectAllByElement(int id)
        {
            List<Choixes> listeChoix = null;
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    listeChoix = context.Choixes.Where(c => c.element_id == id)
                        .Select(c => c)
                        .ToList();
                }
                catch (Exception e)
                {
                    throw e;

                }

            }

            return listeChoix;
        }
        public static void UpdateChoix(Choixes choix)
        {
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    var choice = context.Choixes.Find(choix.Id);
                    choice.intitule = choix.intitule;
                    choice.statut = choix.statut;
                    choice.imagePath = choix.imagePath;
                    choice.image_id = choix.image_id;


                    context.Choixes.AddOrUpdate(choice);
                    context.SaveChanges();
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        public static void DeleteChoix(int idChoix)
        {

            using (var context = new QCMAppBDDEntities())
            {
                //Choixes choix = new Choixes();
                try
                {
                    
                    var choix = context.Choixes.Find(idChoix);
                    context.Choixes.Remove(choix);
                    context.SaveChanges();
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

    }
}