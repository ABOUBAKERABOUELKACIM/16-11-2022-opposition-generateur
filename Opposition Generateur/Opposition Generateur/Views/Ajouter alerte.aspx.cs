using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opposition_Generateur.Models;

namespace Opposition_Generateur
{
    public partial class Ajouter_alerte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (/*Session["Account_id"] != null && Session["Role"] != null && (int)Session["Account_id"] != -1 && Session["Role"].ToString() != ""*/true)
                {
                    HttpCookie httpCookie = Request.Cookies["Userinfo"];
                    if (httpCookie["Role"] != "admin")
                    {
                        rechercheopps.Visible = false;
                    }
                    if (httpCookie != null)
                    {
                        user_account__username.InnerText = httpCookie["Username"];
                        user_account__role.InnerText = httpCookie["Role"];
                        profile_pic.Src = httpCookie["Profile_pic"];
                    }
                    Session.Remove("Historique");
                    Session.Remove("Marques");
                    Session.Remove("Rech_ompic_list_marque");
                    Session.Remove("marques similaire");
                    Session.Remove("marques ip report");
                    Session.Remove("alerte");
                    Session.Remove("index");
                    Session.Remove("pages");
                    Session.Remove("Old_marques_ipreport");
                    Session.Remove("Old_marques_similaire");
                }
                else
                {
                   // Response.Redirect("Authentification.aspx");
                }
            }
        }
       

        protected void btn_Formulaire_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formulaire.aspx");
        }

        protected void btn_Formulaires_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formulaires.aspx");
        }
        protected void Rech_Opps_Click(object sender, EventArgs e)
        {
            Response.Redirect("Rechercheopps.aspx");
        }
        protected void btn_notification_Click(object sender, EventArgs e)
        {
            Response.Redirect("notification.aspx");
        }
        protected void btn_validation_Click(object sender, EventArgs e)
        {
            Response.Redirect("Validation.aspx");
        }
        protected void Archive_Click(object sender, EventArgs e)
        {
            Response.Redirect("archive.aspx");
        }
        protected void btn_strongvalid_Click(object sender, EventArgs e)
        {
            Response.Redirect("strongvalid.aspx");
        }
        protected void Bnt_gestion_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestion_not.Aspx");
        }
        protected void btn_Resultat_Click(object sender, EventArgs e)
        {
            Response.Redirect("Resultat.aspx");
        }
       

        protected void btn_Historique_Click(object sender, EventArgs e)
        {
            Response.Redirect("Historique.aspx");
        }

        protected void btn_Deconnecter_Click(object sender, EventArgs e)
        {
            Session["Account_id"] = -1;
            Session["Role"] = "";
            Response.Cookies.Clear();
            Response.Redirect("Authentification.aspx");
        }
        protected void Aj_alerte_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Session["List_alerte"] != null)
                {
                    List<Alerte> alertes = Session["List_alerte"] as List<Alerte>;
                    Alerte alerte = new Alerte();
                    alerte.Marque_anterieure_reference = marq_ant.Value;
                    alerte.Marque_contester_reference = marq_cont.Value;
                    alerte.Marque_anterieure = marq_ant_nom.Value;
                    alerte.Marque_contester = marq_cont_nom.Value;
                    alerte.Num_pub = Num_pub.Value;
                    if (marq_ant_nationale.Checked)
                    {
                        alerte.Nature_marque_anterieure = "nationale";
                    }
                    if (marq_ant_internationale.Checked)
                    {
                        alerte.Nature_marque_anterieure = "internationale";
                    }
                    if (marq_cont_nationale.Checked)
                    {
                        alerte.Nature_marque_contester = "nationale";
                    }
                    if (marq_cont_internationale.Checked)
                    {
                        alerte.Nature_marque_contester = "internationale";
                    }
                    alertes.Add(alerte);
                    Session["List_alerte"] = alertes;
                }
                else
                {
                    List<Alerte> alertes = new List<Alerte>();
                    Alerte alerte = new Alerte();
                    alerte.Marque_anterieure_reference = marq_ant.Value;
                    alerte.Marque_contester_reference = marq_cont.Value;
                    alerte.Marque_anterieure = marq_ant_nom.Value;
                    alerte.Marque_contester = marq_cont_nom.Value;
                    alerte.Num_pub = Num_pub.Value;
                    if (marq_ant_nationale.Checked)
                    {
                        alerte.Nature_marque_anterieure = "nationale";
                    }
                    if (marq_ant_internationale.Checked)
                    {
                        alerte.Nature_marque_anterieure = "internationale";
                    }
                    if (marq_cont_nationale.Checked)
                    {
                        alerte.Nature_marque_contester = "nationale";
                    }
                    if (marq_cont_internationale.Checked)
                    {
                        alerte.Nature_marque_contester = "internationale";
                    }
                    alertes.Add(alerte);
                    Session["List_alerte"] = alertes;
                }
                Response.Redirect("Ajouter alerte.aspx");
            }
        }

        protected void btn_ajouter_alerte_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ajouter alerte.aspx");
        }

        protected void btn_generer_doc_Click(object sender, EventArgs e)
        {
            Response.Redirect("Generer pdf.aspx");
        }

        protected void Rech_marque_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche marque.aspx");
        }

        protected void Rech_ompic_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche ompic.aspx");
        }

        protected void Marq_contester_ref_Validator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int res;
            if(int.TryParse(args.Value,out res) || args.Value.Contains("http://search.ompic.ma/web/pages/consulterMarque.do?id="))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void Marq_anterieure_ref_Validator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int res;
            if (int.TryParse(args.Value, out res) || args.Value.Contains("http://search.ompic.ma/web/pages/consulterMarque.do?id="))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void btn_recherche_phonetique_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche phonetique.aspx");
        }

        protected void btn_parametre_v2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Parametre.aspx");
        }

        protected void btn_parametre_v1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Parametre.aspx");
        }

        protected void Rech_bd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche Bd.aspx");
        }

        protected void Num_pubValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string value = args.Value;
            int res;
            if (value.Split('/').Length == 2 && int.TryParse(value.Split('/')[0], out res) && int.TryParse(value.Split('/')[1], out res))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }
}