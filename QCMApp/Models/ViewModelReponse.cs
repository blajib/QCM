using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QCMApp.Models
{
    public class ViewModelReponse
    {
        public int idQuestionnaire { set; get; }
        public int idElement { set; get; }
        public int idChoix { set; get; }
        public String intituleQuestionnaire { set; get; }
        public String intituleElement { set; get; }
        public String intituleChoix { set; get; }
        public Boolean statut { set; get; }
        public DateTime date { set; get; }
        public String texteQuestion { set; get; }

    }
}