using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opposition_Generateur.Models
{
    [Serializable]
    public class Alerte
    {
        public string Marque_anterieure { get; set; }
        public string Marque_contester { get; set; }
        public string Marque_anterieure_reference { get; set; }
        public string Marque_contester_reference { get; set; }
        public string Num_pub { get; set; } = "";
        public string Date_debut { get; set; } = "";
        public string Date_fin { get; set; } = "";
        public string Nature_marque_anterieure { get; set; }
        public string Nature_marque_contester { get; set; }
        public string refalert { get; set; } = "";
    }
}