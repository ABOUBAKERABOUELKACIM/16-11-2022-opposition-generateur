using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opposition_Generateur.Models
{
    public class Marque_Model_App_V1
    {
        public string Nom { get; set; }
        public string Numero { get; set; }
        public string Date_depot { get; set; }
        public string Titulaire { get; set; }
        public string Classe_nice { get; set; }
        public byte[] Image { get; set; }
    }
}