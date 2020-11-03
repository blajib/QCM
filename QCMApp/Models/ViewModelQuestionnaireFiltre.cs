using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QCMApp.Models
{
    public class ViewModelQuestionnaireFiltre
    {
        public DateTime? dateDepart { get; set; }
        public DateTime? dateFin { get; set; }
        public Boolean? actif { get; set; }
        public int? idSite { get; set; }
        public String nomSite { set; get; }
    }
}