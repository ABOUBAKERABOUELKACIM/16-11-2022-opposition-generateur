using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Opposition_Generateur.Models
{
    public class tmopmic
    {
        public string Numero_titre { get; set; }
        public string Nom_marque { get; set; }
        public string Numero_publication { get; set; }
        public string Date_depot { get; set; }
        public string Date_expiration { get; set; }
        public string Updated_date { get; set; }
        public string MappingId { get; set; }
        public string Telephone { get; set; }
        public string ClasseNice { get; set; }
        public string ClasseDetails { get; set; }
        public string Type { get; set; }
        public string Statut { get; set; }
        public string Email { get; set; }
        public string Loi { get; set; }
        public string Pays { get; set; }

        public string Publication_identifier { get; set; }
        public string Publication_section { get; set; }
        public string Publication_date { get; set; }

        public string Applicant_name { get; set; }
        public string Applicant_legalentity { get; set; }
        public string Applicant_nationalityCode { get; set; }
        public string Applicant_address { get; set; }
        public string Applicant_city { get; set; }
        public string Applicant_countryCode { get; set; }

        public string Representative_name { get; set; }
        public string Representative_nationalityCode { get; set; }
        public string Representative_address { get; set; }
        public string Representative_city { get; set; }
        public string Representative_countryCode { get; set; }

        public string OppositionDate { get; set; }
        public string Opposition_earlierMark_applicationNumber { get; set; }
        public string Opposition_applicant_name { get; set; }
        public string Opposition_applicant_legalentity { get; set; }
        public string Opposition_nationaliyCode { get; set; }
        public string Opposition_applicant_address { get; set; }
        public string Opposition_applicant_city { get; set; }
        public string Opposition_applicant_countryCode { get; set; }
        public string Deposant { get; set; }
        public string Mondataire { get; set; }
        public string Nombre_opposition { get; set; }
    }
}   