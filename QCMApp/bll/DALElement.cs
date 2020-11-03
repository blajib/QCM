using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace QCMApp.bll
{
    public class DALElement
    {
        private static List<Elements> listeElements = new List<Elements>();
        public static void InsertElement(Elements element)
        {
            using (var context = new QCMAppBDDEntities())
            {
               
                try
                {
                    context.Elements.Add(element);
                    context.SaveChanges();
                }
                catch (Exception e)
                {

                    throw e;

                }
            }
        }
        public static Elements FindById(int id)
        {
            Elements element = new Elements();
            using (var context = new QCMAppBDDEntities())
            {
                

                try
                {
                    element = context.Elements.Find(id);
                    
                }
                catch (Exception e)
                {

                    throw e;

                }
            }
            return element;
        }
        public static List<Elements> SelectAll()
        {
            List<Elements> listeElements = null;
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    listeElements = context.Elements.ToList();
                }
                catch (Exception e)
                {
                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("Erreur SelectAllElements({0})", e));
                }
                
            }

            return listeElements;
        }
        public static List<Elements> SelectAllFromQuestionnaire(int id)
        {

                Questionnaires questionnaire = new Questionnaires();
                using (var context = new QCMAppBDDEntities())
                {
                    try
                    {

                        questionnaire.Elements = context.Elements.Where(e => e.questionnaire_id == id)
                        .Select(e => e)
                        .OrderBy(e => e.ordre)
                        .ToList();


                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                }
                return (List<Elements>)questionnaire.Elements;
        }
        public static Elements SelectLastElement(int id)
        {

            Elements element = new Elements();
            using (var context = new QCMAppBDDEntities())
            {
                try
                {

                    element = context.Elements.Where(e => e.questionnaire_id == id)
                        .Select(e => e)
                        .OrderByDescending(e => e.ordre)
                        .FirstOrDefault();


                }
                catch (Exception e)
                {

                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("Erreur SelectAllFromQuestionnaire elements({0})", e));
                }

            }
            return element;
        }
        public static List<Elements> SelectAllFromQuestionnaireDesc(int id)
        {

            Questionnaires questionnaire = new Questionnaires();
            using (var context = new QCMAppBDDEntities())
            {
                try
                {

                    questionnaire.Elements = context.Elements.Where(e => e.questionnaire_id == id)
                        .Select(e => e)
                        .OrderByDescending(e => e.ordre)
                        .ToList();


                }
                catch (Exception e)
                {

                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("Erreur SelectAllFromQuestionnaire elements({0})", e));
                }

            }
            return (List<Elements>)questionnaire.Elements;
        }
        public static Elements SelectElementRetourDescription(Elements element)
        {

            Elements elementRetour = new Elements();
            using (var context = new QCMAppBDDEntities())
            {
                try
                {

                    //questionnaire.Elements = context.Elements.Where(e => e.questionnaire_id == idQuestionnaire && e.ordre < ordre && e.TypeElement_Id == 1)
                    //    .Select(e => e)
                    //    .OrderByDescending(e => e.ordre)
                    //    .ToList();
                    elementRetour = context.Elements.Where(e =>
                            e.questionnaire_id == element.questionnaire_id && e.ordre < element.ordre && e.TypeElement_Id == 1)
                        .Select(e => e)
                        .OrderByDescending(e => e.ordre)
                        .FirstOrDefault();


                }
                catch (Exception e)
                {

                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("Erreur SelectAllFromQuestionnaire elements({0})", e));
                }

            }
            return elementRetour;
        }
        public static Elements SelectElementApresDescription(Elements element)
        {

            Elements elementRetour = new Elements();
            using (var context = new QCMAppBDDEntities())
            {
                try
                {

                    //questionnaire.Elements = context.Elements.Where(e => e.questionnaire_id == idQuestionnaire && e.ordre < ordre && e.TypeElement_Id == 1)
                    //    .Select(e => e)
                    //    .OrderByDescending(e => e.ordre)
                    //    .ToList();
                    elementRetour = context.Elements.Where(e =>
                            e.questionnaire_id == element.questionnaire_id && e.ordre > element.ordre && e.TypeElement_Id == 1)
                        .Select(e => e)
                        .OrderBy(e => e.ordre)
                        .FirstOrDefault();


                }
                catch (Exception e)
                {

                    Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("Erreur SelectAllFromQuestionnaire elements({0})", e));
                }

            }
            return elementRetour;
        }
        public static void DeleteElement(int id)
        {
            Tools.Logger.Ecrire(Tools.Logger.Niveau.Info, string.Format("Debut DeleteElement({0})", id));
            using (var context = new QCMAppBDDEntities())
            {
                var element = context.Elements.Find(id);
                try
                {
                    int ordre = (int)element.ordre;
                    int idQuestionnaire = (int)element.questionnaire_id;
                    context.Elements.Remove(element);               
                    context.SaveChanges();
                    elementsApresReorder(ordre,idQuestionnaire);
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        public static void UpdateElement(Elements element)
        {
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    context.Elements.AddOrUpdate(element);
                    context.SaveChanges();
                }
                catch (Exception e)
                {

                    throw e;
                }

            }
        }
        public static void UpdateListeElements(List<Elements> elements)
        {
            using (var context = new QCMAppBDDEntities())
            {
                try
                {
                    //context.Questionnaires.AddOrUpdate(elements);
                    context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
        public static void elementUp(int idElement)
        {
            Elements element = FindById(idElement);
            Elements elementAvant = elementOrdreAvant(element);
            int ordreElementOrigin = (int)element.ordre;
            element.ordre = elementAvant.ordre;
            elementAvant.ordre = ordreElementOrigin;
            UpdateElement(element);
            UpdateElement(elementAvant);

            
        }
        public static void elementDown(int idElement)
        {
            Elements element = FindById(idElement);
            Elements elementApres = elementOrdreApres(element);
            int ordreElementOrigin = (int)element.ordre;
            element.ordre = elementApres.ordre;
            elementApres.ordre = ordreElementOrigin;
            UpdateElement(element);
            UpdateElement(elementApres);
        }
        public static Elements elementOrdreAvant(Elements element)
        {
            Elements elementAvant = new Elements();
            using (var context = new QCMAppBDDEntities())
            {
                elementAvant = context.Elements.Where(e => e.ordre < element.ordre && e.questionnaire_id == element.questionnaire_id)
                    .Select(e => e).OrderByDescending(e => e.ordre)
                    .FirstOrDefault();

            }
            return elementAvant;
        }
        public static Elements elementOrdreApres(Elements element)
        {
            Elements elementApres = new Elements();
            using (var context = new QCMAppBDDEntities())
            {
                elementApres = context.Elements.Where(e => e.ordre > element.ordre && e.questionnaire_id == element.questionnaire_id)
                    .Select(e => e).OrderBy(e => e.ordre)
                    .FirstOrDefault();

            }
            return elementApres;
        }
        public static void elementsApresReorder(int suprimElement,int idQuestionnaire)
        {
            
            using (var context = new QCMAppBDDEntities())
            {
                List<Elements> elements = context.Elements.Where(e => e.ordre > suprimElement && e.questionnaire_id == idQuestionnaire)
                    .Select(e => e).OrderBy(e => e.ordre)
                    .ToList();

                foreach (var item in elements)
                {
                    item.ordre = suprimElement;
                    UpdateElement(item);
                    suprimElement++;
                }
            }


           
        }

        public static Elements FindElementByOrdre(int numElement, int idQuestionnaire)
        {
            Elements element = null;
            using (var context = new QCMAppBDDEntities())
            {
                element = context.Elements.Where(e => e.ordre == numElement && e.questionnaire_id == idQuestionnaire).
                    Select(e=>e).
                    FirstOrDefault();
            }
            return element;
        }

        public static int FindNumeroQuestion(int idElement)
        {
            List<Elements> elements = new List<Elements>();
            Elements element = FindById(idElement);
            using (var context = new QCMAppBDDEntities())
            {
                    elements = context.Elements
                    .Where(e => e.TypeElement_Id == 2 && e.questionnaire_id == element.questionnaire_id)
                    .Select(e => e)
                    .OrderBy(e => e.ordre)
                    .ToList();
            }

            var resultat = 1;
            foreach (var y in elements)
            {
                if (element.Id == y.Id)
                {
                    break;
                }

                resultat++;
            }
            return resultat;
        }
        public static List<Elements> SelectAllQuestionFromQuestionnaire(int idQuestionnaire)
        {
            List<Elements> questions = new List<Elements>();
            using (var context = new QCMAppBDDEntities())
            {
                questions = context.Elements.Where(e =>e.questionnaire_id == idQuestionnaire && e.TypeElement_Id == 2).
                    Select(e => e).
                    ToList();
            }
            return questions;
        }
    }

}