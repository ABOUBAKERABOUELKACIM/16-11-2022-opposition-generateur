using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opposition_Generateur.Models
{
    [Serializable]
    public class Opposition
    {
        public int Opposition_id { get; set; }
        public string Creer_par { get; set; }
        public string N_depot_marque_anterieure { get; set; }
        public string Nom_marque_anterieure { get; set; }
        public string Deposant_marque_anterieure { get; set; }
        public string Nature_marque_anterieure { get; set; }

        public string N_depot_marque_contester { get; set; }
        public string Nom_marque_contester { get; set; }
        public string Deposant_marque_contester { get; set; }
        public string Nature_marque_contester { get; set; }
        public DateTime Date_creation { get; set; }

        public string Cas_identite { get; set; }
        public string Cas_inclusion { get; set; }
        public string Cas_complementarite { get; set; }
        public string Comparaison_signe { get; set; }
        public string Appreciation_generale_risque_confusion { get; set; }
        public byte[] Fichier { get; set; } 

    }
}