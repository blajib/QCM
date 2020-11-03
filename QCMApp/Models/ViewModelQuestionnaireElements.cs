using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QCMApp.Models
{
    public class ViewModelQuestionnaireElements
    {
        public string intituleQuestionnaire { get; set; }
        public int idQuestionnaire { get; set; }
        public List<Elements> elements { get; set; }
        public Questionnaires questionnaire { get; internal set; }
        public int idElement { set; get; }
        public String intituleElement { set; get; }
        public String texte { set; get; }
        public List<Images> listeImagesUp { set; get; }
        public List<Images> listeImagesDown { set; get; }
        public int noteQuestionnaire { set; get; }
        public Videos video { set; get; }
    }
}