using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Opposition_Generateur.Models
{
    public class oppsearch
    {
        public string num_op { get; set; }
        public string marque_c { get; set; }
        public string ImageC { get; set; } = "Empty.png";
        public string ImageA { get; set; } = "Empty.png";
        public string marq_a { get; set; } 
        public string decision { get; set; }
        public string Statut { get; set; } = "unknown";



    }
}