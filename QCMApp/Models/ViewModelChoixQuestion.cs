using System;
using System.Collections.Generic;

namespace QCMApp.Models
{
    public class ViewModelChoixQuestion
    {
        public int idElement { set; get; }
        public String intituleChoix { set; get; }
        public int idQuestionnaire { set; get; }
        public String intituleQuestion { set; get; }
        public List<Choixes> listeChoix { set; get; }
        public String texteQuestion { set; get; }
        public int idChoix { set; get; }
        public Elements element { set; get; }
        public String mediaChoix { set; get; }
        public Boolean statutChoix { set; get; }
        public List<Images> listeImagesUp { set; get; }
        public String messageErreur { set; get; }

    }
}