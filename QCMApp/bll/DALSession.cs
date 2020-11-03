using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QCMApp.Models;

namespace QCMApp.bll
{
    public class DALSession
    {

        public static Questionnaires questionnaire
        {
            get
            {
                Questionnaires result = null;
                if (HttpContext.Current.Session["questionnaire"] != null)
                {
                    result = ((Questionnaires) HttpContext.Current.Session["questionnaire"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["questionnaire"] = value; }

        }
        public static Elements element
        {
            get
            {
                Elements result = null;
                if (HttpContext.Current.Session["element"] != null)
                {
                    result = ((Elements)HttpContext.Current.Session["element"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["element"] = value; }

        }
        public static Elements lastElement
        {
            get
            {
                Elements result = null;
                if (HttpContext.Current.Session["lastElement"] != null)
                {
                    result = ((Elements)HttpContext.Current.Session["lastElement"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["lastElement"] = value; }

        }

        public static int numElement
        {
            get
            {
                int result = 2;
                if (HttpContext.Current.Session["numElement"] != null)
                {
                    result = ((int)HttpContext.Current.Session["numElement"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["numElement"] = value; }

        }
        public static int sessionStart
        {
            get
            {
                int result = 0;
                if (HttpContext.Current.Session["sessionStart"] != null)
                {
                    result = ((int)HttpContext.Current.Session["sessionStart"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["sessionStart"] = value; }

        }
        public static int sessionFinish
        {
            get
            {
                int result = 0;
                if (HttpContext.Current.Session["sessionFinish"] != null)
                {
                    result = ((int)HttpContext.Current.Session["sessionFinish"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["sessionFinish"] = value; }

        }
        public static List<Elements> listeElements
        {
            get
            {
                List<Elements> result = null;
                if (HttpContext.Current.Session["listeElements"] != null)
                {
                    result = ((List<Elements>)HttpContext.Current.Session["listeElements"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["listesElements"] = value; }

        }
        public static String nom
        {
            get
            {
                String result = null;
                if (HttpContext.Current.Session["nom"] != null)
                {
                    result = ((String)HttpContext.Current.Session["nom"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["nom"] = value; }

        }
        public static String prenom
        {
            get
            {
                String result = null;
                if (HttpContext.Current.Session["prenom"] != null)
                {
                    result = ((String)HttpContext.Current.Session["prenom"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["prenom"] = value; }

        }
        public static int nombreElements
        {
            get
            {
                int result = 0;
                if (HttpContext.Current.Session["nombreElements"] != null)
                {
                    result = ((int)HttpContext.Current.Session["nombreElements"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["nombreElements"] = value; }

        }
        public static int numeroQuestion
        {
            get
            {
                int result = 1;
                if (HttpContext.Current.Session["numeroQuestion"] != null)
                {
                    result = ((int)HttpContext.Current.Session["numeroQuestion"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["numeroQuestion"] = value; }

        }
        public static List<ViewModelReponse> reponses
        {
            get
            {
                List<ViewModelReponse> result = new List<ViewModelReponse>();
                if (HttpContext.Current.Session["reponses"] != null)
                {
                    result = ((List<ViewModelReponse>)HttpContext.Current.Session["reponses"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["reponses"] = value; }

        }
        public static int nombreBonneReponses
        {
            get
            {
                int result = 0;
                if (HttpContext.Current.Session["nombreBonneReponses"] != null)
                {
                    result = ((int)HttpContext.Current.Session["nombreBonneReponses"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["nombreBonneReponses"] = value; }

        }
        public static int ResultatQuestionnaire
        {
            get
            {
                int result = 0;
                if (HttpContext.Current.Session["ResultatQuestionnaire"] != null)
                {
                    result = ((int)HttpContext.Current.Session["ResultatQuestionnaire"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["ResultatQuestionnaire"] = value; }

        }
        public static DateTime dateDebut
        {
            get
            {
                DateTime result = DateTime.Now;
                if (HttpContext.Current.Session["dateDebut"] != null)
                {
                    result = ((DateTime)HttpContext.Current.Session["dateDebut"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["dateDebut"] = value; }

        }
        public static List<int> listeIdElement
        {
            get
            {
                List<int> result = new List<int>();
                if (HttpContext.Current.Session["listeIdElement"] != null)
                {
                    result = ((List<int>)HttpContext.Current.Session["listeIdElement"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["listeIdElement"] = value; }

        }
        public static List<int> listeIdQuestionsRepondu
        {
            get
            {
                List<int> result = new List<int>();
                if (HttpContext.Current.Session["listeIdQuestionsRepondu"] != null)
                {
                    result = ((List<int>)HttpContext.Current.Session["listeIdQuestionsRepondu"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["listeIdQuestionsRepondu"] = value; }

        }
        public static int nombreQuestionsRepondu
        {
            get
            {
                int result = 0;
                if (HttpContext.Current.Session["nombreQuestionsRepondu"] != null)
                {
                    result = ((int)HttpContext.Current.Session["nombreQuestionsRepondu"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["nombreQuestionsRepondu"] = value; }

        }
        public static int nombreQuestionsQuestionnaire
        {
            get
            {
                int result = 0;
                if (HttpContext.Current.Session["nombreQuestionsQuestionnaire"] != null)
                {
                    result = ((int)HttpContext.Current.Session["nombreQuestionsQuestionnaire"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["nombreQuestionsQuestionnaire"] = value; }

        }
        public static float noteFinale
        {
            get
            {
                float result = 0;
                if (HttpContext.Current.Session["noteFinale"] != null)
                {
                    result = ((float)HttpContext.Current.Session["noteFinale"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["noteFinale"] = value; }

        }
        public static int idUtilisateur
        {
            get
            {
                int result = 0;
                if (HttpContext.Current.Session["idUtilisateur"] != null)
                {
                    result = ((int)HttpContext.Current.Session["idUtilisateur"]);
                }

                return result;
            }
            set { HttpContext.Current.Session["idUtilisateur"] = value; }

        }






    }
}