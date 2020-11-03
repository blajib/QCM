using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QCMApp.bll;

namespace QCMApp.Controllers
{
    public class UtilisateurController : Controller
    {
        // GET: Utilisateur
        public ActionResult ListeUtilisateurs()
        {
            
            return View(DALUtilisateur.SelectAllUtilisateurs());
        }
    }
}