using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QCMApp.Models
{
    public class ViewModelMedia
    {
        public String imagePath { set; get; }
        public int idImage { set; get; }
        public int idVideo { set; get; }
        public String videoPath { set; get; }
        public String formatVideo { set; get; }
        public int value { get; set; } = 1 * 1024 * 1024 * 1024;
    }
}