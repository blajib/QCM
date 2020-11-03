using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QCMApp.Models
{
    public class ViewModelQuestion
    {
        public int idElement { set; get; }
        public int idQuestionnaire { set; get; }
        public String intituleQuestion { set; get; }
        public String texteQuestion { set; get; }
    }
}