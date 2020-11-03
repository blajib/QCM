using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QCMApp.Models
{
    public class ViewModelUserQuestionnaire
    {
        public List<Images> imagesUp { set; get; }
        public List<Images> imagesDown { set; get; }
        public List<Images> imageschoix { set; get; }
        public Videos video { set; get; }
        public List<Choixes> listChoix { set; get; }
        public String nom { set; get; }
        public String prenom { set; get; }
        public int idQuestionnaire { set; get; }
        public Elements element { set; get; }
        public int numeroQuestion { set; get; }

   
    }
}