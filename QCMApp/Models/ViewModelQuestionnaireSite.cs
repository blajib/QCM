using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace QCMApp.Models
{
    public class ViewModelQuestionnaireSite
    {
        public List<Questionnaires> questionnaires { get; set; }
        public List<Site> sites { get; set; }

        public DateTime dateDepart { get; set; }
        public DateTime dateFin { get; set; }
        public Boolean actif { get; set; }
        public int idSite { get; set; }
        public Site siteFiltre { get; set; }
        public String nomSite { set; get; }
    }
}