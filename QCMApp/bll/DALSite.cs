using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace QCMApp.bll
{
    public class DALSite
    {               
        public static List<Site> SelectAllSite()
        {
            List<Site> sites = new List<Site>();
            using (var context = new QCMAppBDDEntities())
            {

                sites = context.Site.ToList();
            }
            return sites;
        }
        public static void UpdateSitesQuestionnaire(int idSite, Boolean cocher, int idQuestionnaire)
        {
            var questionnaire = new Questionnaires();
            var site = new Site();

            questionnaire = DALQuestionnaire.FindById(idQuestionnaire);
            using (var context = new QCMAppBDDEntities())
            {
                site = context.Site.Find(idSite);
                context.Questionnaires.Attach(questionnaire);
                if (cocher)
                {
                    if (!questionnaire.Site.Where(s => s.Id == site.Id).Any())
                        questionnaire.Site.Add(site);
                }
                else
                {
                    if (questionnaire.Site.Where(s => s.Id == site.Id).Any())
                        questionnaire.Site.Remove(site);
                }

                context.SaveChanges();
            }



        }
        public static Site FindById(int id)
        {
            Site site = new Site() ;
            using (var context = new QCMAppBDDEntities())
            {
                site = context.Site.Find(id);
            }
            return site;
        }
    }
}