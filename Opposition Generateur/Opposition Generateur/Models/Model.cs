using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opposition_Generateur.Models
{
    public class Model
    {
        public string MarqueIpReport { get; set; }
        public string MarqueSimilaire { get; set; }
        public string DetailsMarqueIpReport { get; set; }
        public string DetailsMarqueSimilaire { get; set; }
        public string ImageMarqueIpReport { get; set; } = "";
        public string ImageMarqueSimilaire { get; set; } = "";
        public string crystalIpReport { get; set; }="";
        public string crystalMarqueSimilaire { get; set; }="";
    }
}