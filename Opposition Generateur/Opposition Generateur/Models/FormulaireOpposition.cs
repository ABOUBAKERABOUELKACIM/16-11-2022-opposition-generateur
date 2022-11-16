using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opposition_Generateur.Models
{
    [Serializable]
    public class FormulaireOpposition
    {
        public List<string> Nature_droit_anterieure = new List<string>();
        public int ID_form { get; set; }
        public string N_depot_marque_anterieure { get; set; } = "";
        public string Nature_marque_anterieure { get; set; } = "";
        public string N_depot_marque_contester { get; set; } = "";
        public string Nature_marque_contester { get; set; } = "";
        public string Date_Exp_marque_anterieure { get; set; } = "";
        public string Date_Exp_marque_contester { get; set; } = "";
        public string Deposant_marque_contester { get; set; } = "";
        public string Deposant_marque_anterieure { get; set; } = "";
        public string marque_contester_Date_depot { get; set; } = "";
        public string marque_contester_num_publication { get; set; } = "";
        public string marque_anterieur_Date_depot { get; set; } = "";
        public string marque_anterieur_denomination_sociale { get; set; } = "";
        public string marque_anterieur_tribunal { get; set; } = "";
        public string marque_anterieur_Ice { get; set; } = "";
        public string marque_anterieur_Rc { get; set; } = "";
        public string marque_anterieur_adresse { get; set; } = "";
        public string Nom_marque_contester { get; set; } = "";
        public string Nom_marque_anterieure { get; set; } = "";
        public string Image_marque_anterieure { get; set; } = "";
        public string Image_marque_contester { get; set; } = "";

        public Dictionary<string, string> Classe_nice_contester_kvp = new Dictionary<string, string>();

        public Dictionary<string, string> Classe_nice_anterieure_kvp = new Dictionary<string, string>();
    }
}