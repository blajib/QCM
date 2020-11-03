using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QCMApp.bll
{
    public class DALReponse
    {
        public static void InsertReponse(Reponses reponse)
        {
            using (var context = new QCMAppBDDEntities())
            {
                context.Reponses.Add(reponse);
                context.SaveChanges();

            }
        }
    }
}