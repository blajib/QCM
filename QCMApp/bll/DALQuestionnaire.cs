using Microsoft.SqlServer.Server;
using QCMApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace QCMApp.bll
{
    public class DALQuestionnaire
    {
       
        public static List<Questionnaires> SelectAll()
        {
            List<Questionnaires> listeQuestionnaires = new List<Questionnaires>();
            using (var context = new QCMAppBDDEntities())
            {
                listeQuestionnaires = context.Questionnaires.Include("Site").ToList();
            }

            return listeQuestionnaires;
        }
        public static Questionnaires FindById(int id)
        {
            Questionnaires questionnaire = new Questionnaires();
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    questionnaire = context.Questionnaires.Find(id);
                    //questionnaire.Elements = context.Elements.Where(e=>e.questionnaire_id == id).Select(e=>e).ToList();

                    
                }
                catch (SqlException e)
                {

                    throw;
                }

            }

            return questionnaire;
        }
        public static Questionnaires FindByIntitule(String intitule)
        {
            Questionnaires questionnaire = new Questionnaires();
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    questionnaire = context.Questionnaires.Where(q => q.intitule == intitule).Select(q=>q).FirstOrDefault();

                }
                catch (SqlException e)
                {

                    throw;
                }

            }

            return questionnaire;
        }
        public static void InsertQuestionnaire(Questionnaires questionnaire)
        {
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    context.Questionnaires.Add(questionnaire);
                    context.SaveChanges();
                }
                catch (SqlException e)
                {

                    throw e;
                }

            }

        }
        public static void DeleteQuestionnaire(int id)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("Debut DeleteQuestionnaire({0})", id));
            using (var context = new QCMAppBDDEntities())
            {
                Questionnaires questionnaire = context.Questionnaires.Find(id);
                try
                {
                    context.Questionnaires.Remove(questionnaire);
                    context.SaveChanges();
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
        public static void UpdateQuestionnaire(Questionnaires questionnaire)
        {
            using (var context = new QCMAppBDDEntities())
            {

                try
                {
                    //context.Questionnaires.AddOrUpdate(questionnaire);
                    //Questionnaires questionnaireData = context.Questionnaires.Find(questionnaire.Id);
                    //questionnaireData.intitule = questionnaire.intitule;
                    //questionnaireData.note = questionnaire.note;
                    //questionnaireData.actif = questionnaire.actif;
                    context.Questionnaires.AddOrUpdate(questionnaire);
                    context.SaveChanges();
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }
        public static void AddElement(Questionnaires questionnaire,Elements element )
        {
            using (var context = new QCMAppBDDEntities())
            {

                try
                {
                    context.Questionnaires.Find();
                    context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        internal static ViewModelQuestionnaireSite searchFilterQuestionnaire(ViewModelQuestionnaireFiltre qf)
        {
            QCMAppBDDEntities qd = new QCMAppBDDEntities();
            ViewModelQuestionnaireSite modelRetour = new ViewModelQuestionnaireSite();
            //var results = qd.Questionnaires.Where(x => x.date >= qf.dateDepart && x.date <= qf.dateFin).ToList();
            //voir pour le site car ce n'est pas dans la meme table. Faire un inner join
                        
            // datedepart-dateFin-actif-idSite
            if (qf.dateDepart != null && qf.dateFin != null && qf.actif == true && qf.idSite!= null)
            {
                modelRetour.questionnaires = qd.Questionnaires.Where(x => x.date >= qf.dateDepart && x.date <= qf.dateFin && x.actif == true).ToList();
            }
            // datedepart-dateFin-actif
            if (qf.dateDepart != null && qf.dateFin != null && qf.actif == true && qf.idSite == null)
            {
                modelRetour.questionnaires = qd.Questionnaires.Where(x => x.date >= qf.dateDepart && x.date <= qf.dateFin && x.actif == true).ToList();
            }
            // dateDepart-dateRetour
            if (qf.dateDepart != null && qf.dateFin != null && qf.actif == false && qf.idSite == null)
            {
                modelRetour.questionnaires = qd.Questionnaires.Where(x => x.date >= qf.dateDepart && x.date <= qf.dateFin).ToList();
            }
            //
            if (qf.dateDepart == null && qf.dateFin == null && qf.actif == false && qf.idSite == null)
            {
                modelRetour.questionnaires = SelectAll();
            }
            // actif
            if (qf.dateDepart == null && qf.dateFin == null && qf.actif == true && qf.idSite == null)
            {
                modelRetour.questionnaires = modelRetour.questionnaires = qd.Questionnaires.Where(x => x.actif == true).ToList();
            }
            // actif-site
            if (qf.dateDepart == null && qf.dateFin == null && qf.actif == true && qf.idSite != null)
            {
                List<Questionnaires> questionnaireTrie = new List<Questionnaires>();
                //var questionnaires = SelectAll();
                var questionnaires = qd.Questionnaires.Where(x => x.actif == true).ToList();
                foreach (var questionnaire in questionnaires)
                {
                    foreach (var site in questionnaire.Site)
                    {
                        if (site.Id == qf.idSite)
                        {
                            questionnaireTrie.Add(questionnaire);
                        }
                    }
                }
                modelRetour.questionnaires = questionnaireTrie;

                //modelRetour.questionnaires.AddRange(questionnaires.Where(q => q.Site.Where(s => s.Id == qf.idSite).Any()).Select(q => q));

            }
            //site
            if (qf.dateDepart == null && qf.dateFin == null && qf.actif == false && qf.idSite != null)
            {
                List<Questionnaires> questionnaireTrie = new List<Questionnaires>();
                var questionnaires = SelectAll();
                foreach (var questionnaire in questionnaires)
                {
                    foreach (var site in questionnaire.Site)
                    {
                        if (site.Id == qf.idSite)
                        {
                            questionnaireTrie.Add(questionnaire);
                        }
                    }
                }
                modelRetour.questionnaires = questionnaireTrie;
            }
            modelRetour.dateDepart = (DateTime)qf.dateDepart.GetValueOrDefault();
            modelRetour.dateFin = (DateTime)qf.dateFin.GetValueOrDefault();
            modelRetour.actif = (Boolean)qf.actif.GetValueOrDefault();
            modelRetour.idSite = (int)qf.idSite.GetValueOrDefault();
            if (qf.idSite.HasValue)
            {
                modelRetour.nomSite = DALSite.FindById(modelRetour.idSite).nom;
            }

            //if (qf.idSite.HasValue)
            //    modelRetour.siteFiltre = SiteManager.FindById((int)qf.idSite);
            
            return modelRetour;
        }

        internal static int CopyQuestionnaire(int id, string uriImage, string uriVideo)
        {
            Questionnaires newQ;

            // récupération questionnaire d'origine
            using (var context = new QCMAppBDDEntities())
            {
                var q = context.Questionnaires.Where(e => e.Id == id).FirstOrDefault();

                // mise à jour des infos
                bool isInt = true;                
                int index = 1;
                int increment = 0;
                while(isInt)
                {
                    if(int.TryParse(q.intitule.Substring(q.intitule.Length - index, index), out increment))
                    {
                        index++;
                    }
                    else
                    {
                        isInt = false;
                    }
                }

                newQ = new Questionnaires() {
                    actif = false,
                    couleur = q.couleur,
                    date = DateTime.Now,
                    intitule = ((q.intitule.Length < 100-(index +1) ? q.intitule : q.intitule.Substring(0, 100-(index+1))) + "_" + (increment+1).ToString()),
                    note = q.note,
                    Site = q.Site                    
                };

                newQ = context.Questionnaires.Add(newQ);
                context.SaveChanges();

                // Copy des éléments
                foreach (var element in q.Elements)
                {
                    Elements newElement = new Elements() { 
                        intitule = element.intitule,
                        ordre = element.ordre,
                        TypeElement_Id = element.TypeElement_Id,
                        texte = element.texte,
                        questionnaire_id = newQ.Id
                    };

                    newElement = context.Elements.Add(newElement);
                    context.SaveChanges();

                    foreach (var image in element.Images)
                    {
                        var newImage = CopyImage(image, newElement.Id, null, uriImage);
                        newImage = context.Images.Add(newImage);
                        context.SaveChanges();
                    }

                    foreach (var choix in element.Choixes)
                    {
                        // copie du choix
                        Choixes newChoix = new Choixes()
                        {
                            element_id = newElement.Id,
                            intitule = choix.intitule,
                            statut = choix.statut
                        };
                        newChoix = context.Choixes.Add(newChoix);
                        context.SaveChanges();

                        // copie de l'image du choix si elle existe
                        if (choix.image_id.HasValue)
                        {
                            var image = context.Images.Where(i => i.Id == choix.image_id).Select(i => i).FirstOrDefault();
                            var newImage = CopyImage(image, null, newChoix.Id, uriImage);
                            newImage = context.Images.Add(newImage);
                            context.SaveChanges();
                        }
                    }

                    foreach (var video in element.Videos)
                    {
                        var newVideo = CopyVideo(video, newElement.Id, uriVideo);
                        newVideo = context.Videos.Add(newVideo);
                        context.SaveChanges();
                    }

                    context.SaveChanges();

                }

                return newQ.Id;
            }
        }

        private static Images CopyImage(Images image,int? idElement, int? idChoix, string uriImage)
        {
            // copy de l'image                        
            string pathImage = uriImage + image.nom + "." + image.format;
            string newFileName = Guid.NewGuid().ToString() + "_" + image.nom.Split('_')[1].Split('.')[0];
            string pathNewImage = uriImage + newFileName + "." + image.format;
            System.IO.File.Copy(pathImage, pathNewImage);

            Images newImage = new Images()
            {
                format = image.format,
                idchoix = idChoix,
                idelement = idElement,
                nom = image.nom,
                ordre = image.ordre,
                position = image.position
            };

            return newImage;

            
        }

        private static Videos CopyVideo(Videos video,int? idElement, string uriVideo)
        {
            // copy de l'image                        
            string pathImage = uriVideo + video.nom + "." + video.format;
            string newFileName = Guid.NewGuid().ToString() + "_" + video.nom.Split('_')[1].Split('.')[0];
            string pathNewVideo = uriVideo + newFileName + "." + video.format;
            System.IO.File.Copy(pathImage, pathNewVideo);

            Videos newVideo = new Videos()
            {
                format = video.format,
                idelement = idElement,
                nom = video.nom,
                position = video.position
            };

            return newVideo;


        }

        public static List<Questionnaires> FindQuestionnairesByIdSite(int idSite)
        {
            Site site = null;
            List<Questionnaires> questionnaires = new List<Questionnaires>();
            using (var context = new QCMAppBDDEntities())
            {
                site = context.Site.Include("Questionnaires").Where(s => s.Id == idSite ).Select(s => s).FirstOrDefault();
                
            }

            if (site != null)
            {
                foreach (var questionnaire in site.Questionnaires)
                {
                    questionnaires.Add(questionnaire);
                }
            }
            
            return questionnaires;
        }
        public static List<Questionnaires> FindQuestionnairesActifByIdSite(int idSite)
        {
            Site site = null;
            List<Questionnaires> questionnaires = new List<Questionnaires>();
            using (var context = new QCMAppBDDEntities())
            {
                site = context.Site.Include("Questionnaires").Where(s => s.Id == idSite).Select(s => s).FirstOrDefault();

            }

            if (site != null)
            {
                foreach (var questionnaire in site.Questionnaires)
                {
                    if (questionnaire.actif == true)
                    {
                        questionnaires.Add(questionnaire);

                    }
                }
            }

            return questionnaires;
        }
    }
}