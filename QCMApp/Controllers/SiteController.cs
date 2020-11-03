using QCMApp.bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QCMApp.Controllers
{
    public class SiteController : Controller
    {  
        DALElement em = new DALElement();
        // GET: Lieu

        [HttpPost]
        public JsonResult UpdateSitesQuestionnaire(int idSite,Boolean cocher,int idQuestionnaire)
        {
            DALSite.UpdateSitesQuestionnaire(idSite, cocher, idQuestionnaire);
            return Json("yes");
        }

    }
}