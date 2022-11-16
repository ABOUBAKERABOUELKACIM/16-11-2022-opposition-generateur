using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opposition_Generateur.Models
{
    public class MarqueDetails
    {
        public string NomMarque { get; set; } = "";
        public string Deposant { get; set; } = "";
        public string Mandataire { get; set; } = "";
        public string NumMarque { get; set; } = "";
        public string Deposantadresse { get; set; } = "";

        public string Mandataireadresse { get; set; } = "";
        public string datedepot { get; set; } = "";
        public string dateexpir { get; set; } = "";
        public string typemarque { get; set; } = "";
        public string Mandatairepays { get; set; } = "";
        public string deposantepays { get; set; } = "";
        public string NumPubllication { get; set; } = "";
        public string Publicationsection { get; set; } = "";
        public string classnice { get; set; } = "";
        public string deposantnatio { get; set; } = "";
        public string mandatairenatio { get; set; } = "";
        public string Statut { get; set; } = "";
        public string Produits_Services { get; set; } = "";
    }
}