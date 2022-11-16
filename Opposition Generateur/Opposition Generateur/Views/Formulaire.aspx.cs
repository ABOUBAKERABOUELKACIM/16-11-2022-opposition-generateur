using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opposition_Generateur.Models;
using System.Data;
using System.Data.SqlClient;


namespace Opposition_Generateur
{
    public partial class Formulaire : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["Account_id"] != null && Session["Role"] != null && (int)Session["Account_id"] != -1 && Session["Role"].ToString() != "")
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
                    Response.Redirect("Authentification.aspx");
                }
               
                
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpCookie httpCookie = Request.Cookies["Userinfo"];
            if (Session["List formulaire Opposition"] == null)
            {
                Session["List formulaire Opposition"] = new List<FormulaireOpposition>();
            }
            SqlConnection conn = new SqlConnection(@"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=Ipp;Integrated Security=True");




            List<FormulaireOpposition> List_formulaire_Opposition = Session["List formulaire Opposition"] as List<FormulaireOpposition>;

            FormulaireOpposition formulaireOpposition = new FormulaireOpposition();
            string cases = "";

            if (Request.Form["case-1"] == "on")
            {

                cases = " case-1";
            }
            if (Request.Form["case-2"] == "on")
            {
                cases += " case-2";
            }
            if (Request.Form["case-3"] == "on")
            {
                cases += " case-3";
            }
            if (Request.Form["case-4"] == "on")
            {
                cases += " case-4";
            }
            if (Request.Form["case-5"] == "on")
            {
                cases += " case-5";
            }
            if (Request.Form["case-6"] == "on")
            {
                cases += " case-6";
            }

            formulaireOpposition.N_depot_marque_anterieure = Request.Form["n-deopt-anterieure"] == null ? "" : Request.Form["n-deopt-anterieure"];
            formulaireOpposition.N_depot_marque_contester = Request.Form["n-deopt-contester"] == null ? "" : Request.Form["n-deopt-contester"];

            if (Request.Form["marque-nationale-anterieure"] == "on")
            {
                formulaireOpposition.Nature_marque_anterieure = "nationale";
            }
            if (Request.Form["marque-internationale-anterieure"] == "on")
            {
                formulaireOpposition.Nature_marque_anterieure = "internationale";
            }

            if (Request.Form["marque-nationale-contester"] == "on")
            {
                formulaireOpposition.Nature_marque_contester = "nationale";
            }
            if (Request.Form["marque-internationale-contester"] == "on")
            {
                formulaireOpposition.Nature_marque_contester = "internationale";
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = "INSERT INTO FormulaireOppositiontb VALUES('" + int.Parse(formulaireOpposition.N_depot_marque_anterieure) + "','" + int.Parse(formulaireOpposition.N_depot_marque_contester) + "','" + formulaireOpposition.Nature_marque_contester + "','" + formulaireOpposition.Nature_marque_contester + "','" + cases + "','has been submited','" + int.Parse(httpCookie["Iduser"].ToString()) + "',Null) ";
            cmd.ExecuteNonQuery();

            //Session["List formulaire Opposition"] = List_formulaire_Opposition;
            conn.Close();
            //    cmd.CommandText = "select * from FormulaireOppositiontb where Status = 'has been submited'";
            //    SqlDataReader dr;
            //    dr = cmd.ExecuteReader();
            //   DataTable dt = new DataTable();
            //    dt.Load(dr);    
            //    for(int i = 0; i < dt.Rows.Count ; i++)
            //    {
            //        FormulaireOpposition fs = new FormulaireOpposition();
            //        fs.N_depot_marque_anterieure = dt.Rows[i][1].ToString();
            //        fs.N_depot_marque_contester= dt.Rows[i][2].ToString();
            //        fs.Nature_marque_anterieure= dt.Rows[i][3].ToString();  
            //        fs.Nature_marque_contester= dt.Rows[i][4].ToString();
            //        fs.Nature_droit_anterieure.Add(dt.Rows[i][5].ToString());
            //        List_formulaire_Opposition.Add(fs);

            //    }
            //    Session["List formulaire Opposition"] = List_formulaire_Opposition;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formulaires.aspx");
        }
        protected void btn_Formulaire_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formulaire.aspx");
        }

        protected void btn_Formulaires_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formulaires.aspx");
        }
        protected void Archive_Click(object sender, EventArgs e)
        {
            Response.Redirect("archive.aspx");
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
        protected void Rech_Opps_Click(object sender, EventArgs e) 
        {
            Response.Redirect("Rechercheopps.aspx");
        }
        protected void btn_recherche_phonetique_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche phonetique.aspx");
        }
        protected void btn_notification_Click(object sender, EventArgs e)
        {
            Response.Redirect("notification.aspx");
        }
        protected void btn_validation_Click(object sender, EventArgs e)
        {
            Response.Redirect("Validation.aspx");
        }
        protected void btn_strongvalid_Click(object sender, EventArgs e)
        {
            Response.Redirect("strongvalid.aspx");
        }
        protected void Bnt_gestion_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestion_not.Aspx");
        }
        protected void btn_parametre_v1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Parametre.aspx");
        }

        protected void btn_parametre_v2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Parametre.aspx");
        }

        protected void Rech_bd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche Bd.aspx");
        }
    }
}