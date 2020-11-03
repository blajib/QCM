using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QCMApp.bll
{
    public class DALResultat
    {
        public static void InsertResultat(Resultat resultat)
        {
            using (var contexte = new QCMAppBDDEntities())
            {
                contexte.Resultat.Add(resultat);
                contexte.SaveChanges();
            }
        }
        public static void deleteResultat(Resultat resultat)
        {
            using (var contexte = new QCMAppBDDEntities())
            {
                contexte.Resultat.Remove(resultat);
                contexte.SaveChanges();
            }
        }
    }
}