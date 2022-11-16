using Opposition_Generateur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using Opposition_Generateur.Crystal_report;
using System.Configuration;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net.Http;
using OpenQA.Selenium.Firefox;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support;
using SeleniumExtras.WaitHelpers;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;


namespace Opposition_Generateur.Views
{
    public partial class Recherche_Bd : System.Web.UI.Page
    {
        public List<Marque_TmOmpicModel> list_marque = new List<Marque_TmOmpicModel>();
        public List<Marque_TmOmpicModel> Empty_list = new List<Marque_TmOmpicModel>();
        //SqlConnection con = new SqlConnection(@"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=Ipp2;Integrated Security=True");
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString);
        public class Marque
        {
            public string NumeroTitre { get; set; }
            public string Nommarque { get; set; }
            public string Etat { get; set; }
            public string Titulaire { get; set; }
            public string Mandataire { get; set; }
            public string Type { get; set; }
            public string Loi { get; set; }
            public string NumeroPub { get; set; }
            public string DateDepot { get; set; }
            public string Adresse { get; set; }
            public string Pays { get; set; }
            public string DateExpiration { get; set; }
            public string Classe_nice { get; set; }
            public string Classe_details { get; set; }
            public string MappingId { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["Account_id"] != null && Session["Role"] != null && (int)Session["Account_id"] != -1 && Session["Role"].ToString() != "")
                {
                    Page.MaintainScrollPositionOnPostBack = false;
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
                    Session.Remove("Marqueschecked");
                    Session.Remove("Rech_ompic_list_marque");
                    Session.Remove("marques similaire");
                    Session.Remove("marques ip report");
                    Session.Remove("alerte");
                    Session.Remove("index");
                    Session.Remove("pages");

                    Session.Remove("BDMarques");
                    Session.Remove("Old_marques_ipreport");
                    Session.Remove("Old_marques_similaire");
                    GridView1.DataSource = Empty_list;
                    GridView1.DataBind();
                    //if (Session["BDMarques"] != null)
                    //{
                    //    var list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    //    ViewState["index"] = 0;
                    //    GridView1.DataSource = list_marque.Take(100);
                    //    GridView1.DataBind();
                    //    double result = list_marque.Count / 100.0;
                    //    int pages = int.Parse(Math.Ceiling(result).ToString());
                    //    index.Text = 1 + " / " + (pages == 0 ? 1 : pages);
                    //}
                    //else
                    //{
                    //    GridView1.DataSource = Empty_list;
                    //    GridView1.DataBind();
                    //}
                    GridView1.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    GridView1.Columns[19].Visible = false;
                    GridView1.Columns[10].Visible = false;
                    GridView1.Columns[11].Visible = false;
                    GridView1.Columns[12].Visible = false;
                    GridView1.Columns[13].Visible = false;
                    GridView1.Columns[14].Visible = false;
                    GridView1.Columns[15].Visible = false;
                    GridView1.Columns[16].Visible = false;
                    GridView1.Columns[17].Visible = false;
                    GridView1.Columns[18].Visible = false;
                }
                else
                {
                    Response.Redirect("Authentification.aspx");
                }
            }
        }

        protected void Rech_bd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche Bd.aspx");
        }

        protected void btn_Formulaire_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formulaire.aspx");
        }

        protected void btn_Formulaires_Click(object sender, EventArgs e)
        {
            Response.Redirect("Formulaires.aspx");
        }

        protected void btn_Resultat_Click(object sender, EventArgs e)
        {
            Response.Redirect("Resultat.aspx");
        }

        protected void btn_Historique_Click(object sender, EventArgs e)
        {
            Response.Redirect("Historique.aspx");
        }
        protected void Archive_Click(object sender, EventArgs e)
        {
            Response.Redirect("archive.aspx");
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
        protected void Rech_Opps_Click(object sender, EventArgs e)
        {
            Response.Redirect("Rechercheopps.aspx");
        }

        protected void btn_validation_Click(object sender, EventArgs e)
        {
            Response.Redirect("Validation.aspx");
        }
        protected void btn_notification_Click(object sender, EventArgs e)
        {
            Response.Redirect("notification.aspx");
        }
       
        protected void btn_strongvalid_Click(object sender, EventArgs e)
        {
            Response.Redirect("strongvalid.aspx");
        }
        protected void Bnt_gestion_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestion_not.Aspx");
        }
        protected void Rech_ompic_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche ompic.aspx");
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
        protected void Search_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString);
            List<Marque_TmOmpicModel> list_ompic = new List<Marque_TmOmpicModel>();
            int cleartest = 0;
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            string staut = "";

            if (!string.IsNullOrWhiteSpace(Request.Form["type_marqueOmpic"]))
            {
                list.Add("Type like @Type");
                command.Parameters.AddWithValue("@Type", "%" + Request.Form["type_marqueOmpic"].Trim().ToUpper() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["etatMarqueOmpic"]))
            {
                staut = Request.Form["etatMarqueOmpic"];
                list.Add("Statut like @Statut");
                command.Parameters.AddWithValue("@Statut", Request.Form["etatMarqueOmpic"].Trim());
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["num_marq"]))
            {
                list.Add("NumeroTitre like @NumeroTitre");
                command.Parameters.AddWithValue("@NumeroTitre", "%" + Request.Form["num_marq"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["mandataire"]))
            {
                list.Add("Mandataire like @Mandataire");
                command.Parameters.AddWithValue("@Mandataire", "%" + Request.Form["mandataire"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["email"]))
            {
                list.Add("Email like @Email");
                command.Parameters.AddWithValue("@Email", "%" + Request.Form["email"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["nom_marq"]))
            {
                list.Add("Nommarque like @Nommarque");
                command.Parameters.AddWithValue("@Nommarque", "%" + Request.Form["nom_marq"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["deposant"]))
            {
                list.Add("Deposant like @Deposant");
                command.Parameters.AddWithValue("@Deposant", "%" + Request.Form["deposant"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["adresse_deposant"]))
            {
                list.Add("Adresse like @Adresse");
                command.Parameters.AddWithValue("@Adresse", "%" + Request.Form["adresse_deposant"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Pays_deposant"]))
            {
                list.Add("Pays like @Pays");
                command.Parameters.AddWithValue("@Pays", "%" + Request.Form["Pays_deposant"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_debut"]) && !string.IsNullOrWhiteSpace(Request.Form["date_depot_fin"]))
            {
                list.Add("Datedepot >= @date_depot_debut and Datedepot <= @date_depot_fin");
                command.Parameters.AddWithValue("@date_depot_debut", DateTime.Parse(Request.Form["date_depot_debut"]));
                command.Parameters.AddWithValue("@date_depot_fin", DateTime.Parse(Request.Form["date_depot_fin"]));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_debut"]))
                {
                    list.Add("Datedepot = @date_depot_debut");
                    command.Parameters.AddWithValue("@date_depot_debut", DateTime.Parse(Request.Form["date_depot_debut"]));
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_fin"]))
                {
                    list.Add("Datedepot = @date_depot_fin");
                    command.Parameters.AddWithValue("@date_depot_fin", DateTime.Parse(Request.Form["date_depot_fin"]));
                }
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["date_exp_debut"]) && !string.IsNullOrWhiteSpace(Request.Form["date_exp_fin"]))
            {
                list.Add("Dateexpiration >= @date_exp_debut and Dateexpiration <= @date_exp_fin");
                command.Parameters.AddWithValue("@date_exp_debut", DateTime.Parse(Request.Form["date_exp_debut"]));
                command.Parameters.AddWithValue("@date_exp_fin", DateTime.Parse(Request.Form["date_exp_fin"]));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["date_exp_debut"]))
                {
                    list.Add("Datedepot = @date_exp_debut");
                    command.Parameters.AddWithValue("@date_exp_debut", DateTime.Parse(Request.Form["date_exp_debut"]));
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["date_exp_fin"]))
                {
                    list.Add("Datedepot = @date_exp_fin");
                    command.Parameters.AddWithValue("@date_exp_fin", DateTime.Parse(Request.Form["date_exp_fin"]));
                }
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["type_marqueTm"]))
            {
                if (Request.Form["type_marqueTm"] == "combined")
                {
                    list.Add("Type like @Type");
                    command.Parameters.AddWithValue("@Type", "%" + "mixte".ToUpper() + "%");
                }
                if (Request.Form["type_marqueTm"] == "sound")
                {
                    list.Add("Type like @Type");
                    command.Parameters.AddWithValue("@Type", "%" + "sonore".ToUpper() + "%");
                }
                if (Request.Form["type_marqueTm"] == "other")
                {
                    list.Add("Type like @Type");
                    command.Parameters.AddWithValue("@Type", "%" + "autres".ToUpper() + "%");
                }
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["etatMarqueTm"]))
            {

                if (Request.Form["etatMarqueTm"] == "registered")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "enregistree".ToUpper() + "%");
                }
                if (Request.Form["etatMarqueTm"] == "registration cancelled")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "radiee".ToUpper() + "%");
                }
                //if (Request.Form["etatMarqueTm"] == "application opposed")
                //{
                //    list.Add("Statut like @Opp_S or Statut like @Opp_C  or Statut like @Opp ");
                //    command.Parameters.AddWithValue("@Opp_S", "%" + "opposition suspendue".ToUpper() + "%");
                //    command.Parameters.AddWithValue("@Opp_C", "%" + "opposition en cours".ToUpper() + "%");
                //    command.Parameters.AddWithValue("@Opp", "%" + "opposition".ToUpper() + "%");
                //}
                if (Request.Form["etatMarqueTm"] == "application refused")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "rejetee".ToUpper() + "%");
                }
                if (Request.Form["etatMarqueTm"] == "expired")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "expiree".ToUpper() + "%");
                }
                //if (Request.Form["etatMarqueTm"] == "application withdrawn")
                //{
                //    list.Add("Statut like @CCR or Statut like @R ");
                //    command.Parameters.AddWithValue("@CCR", "%" + "consideree comme retiree".ToUpper() + "%");
                //    command.Parameters.AddWithValue("@R", "%" + "retiree".ToUpper() + "%");
                //}
                if (Request.Form["etatMarqueTm"] == "application published")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "publiee".ToUpper() + "%");
                }
                if (Request.Form["etatMarqueTm"] == "appeal pending")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "en poursuite de procedure".ToUpper() + "%");
                }
                if (Request.Form["etatMarqueTm"] == "renewed")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "renouvlee".ToUpper() + "%");
                }
                if (Request.Form["etatMarqueTm"] == "registration surrendered")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "renoncee".ToUpper() + "%");
                }
            }

            if (!string.IsNullOrWhiteSpace(Request.Form["Classe_nice"]))
            {

                string classnices = "";
                List<string> nices = new List<string>();
                nices = Request.Form["Classe_nice"].Split(',').ToList<string>();
                if (nices.Count != 0)
                {
                    for (int i = 0; i < nices.Count; i++)
                    {
                        if ((nices.Count - 1) == i)
                        {

                            classnices += "%" + nices[i].Trim() + "%";
                        }
                        else
                        {
                            classnices += "%" + nices[i].Trim() + ",";
                        }
                    }
                    //classnices.TrimEnd(',');


                    list.Add("ClasseNice like @ClasseNice");
                    command.Parameters.AddWithValue("@ClasseNice", classnices + "%");
                }

            }
            DataTable ompic = new DataTable();
            DataTable TM = new DataTable();
            List<Marque_TmOmpicModel> list_marque = new List<Marque_TmOmpicModel>();
            if (list.Count > 0)
            {

                var Ompicquery = "select * from Marques_Ompic where ";
                for (int i = 0; i < list.Count; i++)
                {
                    if ((list.Count - 1) == i)
                    {

                        Ompicquery += list[i] + " order by Datedepot desc";
                    }
                    else
                    {
                        Ompicquery += list[i] + " and ";
                    }
                }
                command.CommandText = Ompicquery;
                con.Close();
                con.Open();
                var reader = command.ExecuteReader();
                ompic.Load(reader);
                con.Close();

            }
            else
            {
                cleartest++;
            }
            list.Clear();

            command.Parameters.Clear();

            if (!string.IsNullOrWhiteSpace(Request.Form["etatMarqueTm"]))
            {
                list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + Request.Form["etatMarqueTm"].Trim() + "%");
            }
            else
            {
                staut = staut.ToUpper();
                if (!string.IsNullOrWhiteSpace(staut))
                {

                    if (staut == "ENREGISTREE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Registered" + "%");
                    }
                    if (staut == "RADIEE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Registration cancelled" + "%");
                    }
                    if (staut == "OPPOSITION SUSPENDUE" || staut == "OPPOSITION EN COURS" || staut == "OPPOSITION")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application opposed" + "%");
                    }
                    if (staut.Trim() == "REJETEE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "APPLICATION REFUSED" + "%");
                    }
                    if (staut == "EXPIREE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Expired" + "%");
                    }
                    if (staut == "CONSIDEREE COMME RETIREE" || staut == "RETIREE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application withdrawn" + "%");
                    }
                    if (staut == "EN COURS D'EXAMEN" || staut == "EN EXAMEN DE FORME" || staut == "EN INSTANCE DE REGULARISATION" || staut == "EN EXAMEN DES MOTIFS ABSOLUS" || staut == "PUBLICATION PROGRAMMEE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application filed" + "%");
                    }
                    if (staut == "PUBLIEE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application published" + "%");
                    }
                    if (staut == "EN POURSUITE DE PROCEDURE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Appeal pending" + "%");
                    }
                    if (staut == "RENOUVELEE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Renewed" + "%");
                    }
                    if (staut == "RENONCEE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Registration surrendered" + "%");
                    }
                    //}
                }
            }



            if (!string.IsNullOrWhiteSpace(Request.Form["num_marq"]))
            {
                list.Add("ST13 = @ST13");
                command.Parameters.AddWithValue("@ST13", Request.Form["num_marq"].Trim());
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["mandataire"]))
            {
                list.Add("Representative_name like @Representative_name");
                command.Parameters.AddWithValue("@Representative_name", "%" + Request.Form["mandataire"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["nom_marq"]))
            {
                list.Add("TmName like @TmName");
                command.Parameters.AddWithValue("@TmName", "%" + Request.Form["nom_marq"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["deposant"]))
            {
                list.Add("Applicant_name like @Applicant_name");
                command.Parameters.AddWithValue("@Applicant_name", "%" + Request.Form["deposant"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["adresse_deposant"]))
            {
                list.Add("Applicant_address like @Applicant_address");
                command.Parameters.AddWithValue("@Applicant_address", "%" + Request.Form["adresse_deposant"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_debut"]) && !string.IsNullOrWhiteSpace(Request.Form["date_depot_fin"]))
            {
                list.Add("ApplicationDate >= @date_depot_debut and ApplicationDate <= @date_depot_fin");
                command.Parameters.AddWithValue("@date_depot_debut", DateTime.Parse(Request.Form["date_depot_debut"]));
                command.Parameters.AddWithValue("@date_depot_fin", DateTime.Parse(Request.Form["date_depot_fin"]));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_debut"]))
                {
                    list.Add("ApplicationDate = @date_depot_debut");
                    command.Parameters.AddWithValue("@date_depot_debut", DateTime.Parse(Request.Form["date_depot_debut"]));
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_fin"]))
                {
                    list.Add("ApplicationDate = @date_depot_fin");
                    command.Parameters.AddWithValue("@date_depot_fin", DateTime.Parse(Request.Form["date_depot_fin"]));
                }
            }
            int r;
            if (!string.IsNullOrWhiteSpace(Request.Form["OppositionNbrMin"]) && int.TryParse(Request.Form["OppositionNbrMin"], out r))
            {
                list.Add("Number_of_opposition >= @Number_of_opposition");
                command.Parameters.AddWithValue("@Number_of_opposition", r);
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Da_opposition"]))
            {
                list.Add("Opposition_earlierMark_applicationNumber like @Opposition_earlierMark_applicationNumber");
                command.Parameters.AddWithValue("@Opposition_earlierMark_applicationNumber", "%" + Request.Form["Da_opposition"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Opposant"]))
            {
                list.Add("Opposition_applicant_name like @Opposition_applicant_name");
                command.Parameters.AddWithValue("@Opposition_applicant_name", "%" + Request.Form["Opposant"].Trim() + "%");
            }

            if (!string.IsNullOrWhiteSpace(Request.Form["date_exp_debut"]) && !string.IsNullOrWhiteSpace(Request.Form["date_exp_fin"]))
            {
                list.Add("ExpiryDate >= @date_exp_debut and ExpiryDate <= @date_exp_fin");
                command.Parameters.AddWithValue("@date_exp_debut", DateTime.Parse(Request.Form["date_exp_debut"]));
                command.Parameters.AddWithValue("@date_exp_fin", DateTime.Parse(Request.Form["date_exp_fin"]));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["date_exp_debut"]))
                {
                    list.Add("ExpiryDate = @date_exp_debut");
                    command.Parameters.AddWithValue("@date_exp_debut", DateTime.Parse(Request.Form["date_exp_debut"]));
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["date_exp_fin"]))
                {
                    list.Add("ExpiryDate = @date_exp_fin");
                    command.Parameters.AddWithValue("@date_exp_fin", DateTime.Parse(Request.Form["date_exp_fin"]));
                }
            }
            //if (!string.IsNullOrWhiteSpace(staut))
            //{

            //    if (staut == "ENREGISTREE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Registered" + "%");
            //    }
            //    if (staut == "RADIEE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Registration cancelled" + "%");
            //    }
            //    if (staut == "OPPOSITION SUSPENDUE" || staut == "OPPOSITION EN COURS" || staut == "OPPOSITION")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application opposed" + "%");
            //    }
            //    if (staut.Trim() == "REJETEE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "APPLICATION REFUSED" + "%");
            //    }
            //    if (staut == "EXPIREE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Expired" + "%");
            //    }
            //    if (staut == "CONSIDEREE COMME RETIREE" || staut == "RETIREE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application withdrawn" + "%");
            //    }
            //    if (staut == "EN COURS D'EXAMEN" || staut == "EN EXAMEN DE FORME" || staut == "EN INSTANCE DE REGULARISATION" || staut == "EN EXAMEN DES MOTIFS ABSOLUS" || staut == "PUBLICATION PROGRAMMEE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application filed" + "%");
            //    }
            //    if (staut == "PUBLIEE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application published" + "%");
            //    }
            //    if (staut == "EN POURSUITE DE PROCEDURE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Appeal pending" + "%");
            //    }
            //    if (staut == "RENOUVELEE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Renewed" + "%");
            //    }
            //    if (staut == "RENONCEE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Registration surrendered" + "%");
            //    }
            //    //}
            //}
            if (!string.IsNullOrWhiteSpace(Request.Form["type_marqueOmpic"]))
            {
                if (Request.Form["type_marqueOmpic"] == "Mixte")
                {
                    list.Add("MarkFeature like @MarkFeature");
                    command.Parameters.AddWithValue("@MarkFeature", "%" + "COMBINED" + "%");
                }
                if (Request.Form["type_marqueOmpic"] == "Sonore")
                {
                    list.Add("MarkFeature like @MarkFeature");
                    command.Parameters.AddWithValue("@MarkFeature", "%" + "SOUND" + "%");
                }
                if (Request.Form["type_marqueOmpic"] == "Autres")
                {
                    list.Add("MarkFeature like @MarkFeature");
                    command.Parameters.AddWithValue("@MarkFeature", "%" + "OTHER" + "%");
                }
                else
                {
                    list.Add("MarkFeature like @MarkFeature");
                    command.Parameters.AddWithValue("@MarkFeature", "%" + "OTHER" + "%");
                }
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["type_marqueTm"]))
            {
                list.Add("MarkFeature like @MarkFeature");
                command.Parameters.AddWithValue("@MarkFeature", "%" + Request.Form["type_marqueTm"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Classe_nice"]))
            {

                string classnices = "";
                List<string> nices = new List<string>();
                nices = Request.Form["Classe_nice"].Split(',').ToList<string>();
                if (nices.Count != 0)
                {

                    for (int i = 0; i < nices.Count; i++)
                    {
                        if ((nices.Count - 1) == i)
                        {

                            classnices += "%" + nices[i].Trim() + "%";
                        }
                        else
                        {
                            classnices += "%" + nices[i].Trim() + ",";
                        }
                    }
                    classnices.TrimEnd(',');


                    list.Add("NiceClassNumbers like @NiceClassNumbers");
                    command.Parameters.AddWithValue("@NiceClassNumbers", classnices + "%");
                }

            }

            if (list.Count > 0)
            {
                var Tmquery = "select * from Marques_Tm where ";
                for (int i = 0; i < list.Count; i++)
                {
                    if ((list.Count - 1) == i)
                    {

                        Tmquery += list[i] + "  order by ApplicationDate desc";
                    }
                    else
                    {
                        Tmquery += list[i] + " and ";
                    }
                }

                command.CommandText = Tmquery;
                //Response.Write(Tmquery);
                //Response.Write("      :::    "+staut);
                con.Close();
                con.Open();
                var reader = command.ExecuteReader();
                TM.Load(reader);
                con.Close();
            }
            else
            {
                cleartest++;
            }
            if (cleartest == 2)
            {
                Response.Redirect("Recherche Bd.aspx");
            }
            if (ompic.Rows.Count != 0 && TM.Rows.Count != 0)
            {

                foreach (DataRow row in ompic.Rows)
                {
                    var marque = new Marque_TmOmpicModel()
                    {
                        Applicant_name = row[0] != null ? row[0].ToString() : "",
                        Date_depot = row[1] != null ? row[1].ToString().Split(' ').FirstOrDefault().Trim() : "",
                        Date_expiration = row[2] != null ? row[2].ToString().Split(' ').FirstOrDefault().Trim() : "",
                        Pays = row[3] != null ? row[3].ToString() : "",
                        Telephone = row[4] != null ? row[4].ToString() : "",
                        ClasseNice = row[5] != null ? row[5].ToString() : "",
                        ClasseDetails = row[6] != null ? row[6].ToString() : "",
                        Numero_titre = row[7] != null ? row[7].ToString() : "",
                        Type = row[8] != null ? row[8].ToString() : "",
                        Representative_name = row[9] != null ? row[9].ToString() : "",
                        Nom_marque = row[10] != null ? row[10].ToString() : "",
                        Applicant_address = row[11] != null ? row[11].ToString() : "",
                        Statut = row[12] != null ? row[12].ToString() : "",
                        Email = row[13] != null ? row[13].ToString() : "",
                        Loi = row[14] != null ? row[14].ToString() : "",
                        Numero_publication = row[15] != null ? row[15].ToString() : "",
                        Updated_date = row[16] != null ? row[16].ToString().Split(' ').FirstOrDefault().Trim() : "",
                        MappingId = row[17] != null ? row[17].ToString() : ""
                    };

                    DataRow rowtar = null;
                    foreach (DataRow rowx in TM.Rows)
                    {

                        if (rowx[0].ToString() == row[7].ToString())
                        {

                            if (rowx[10]?.ToString().Trim() != marque.Applicant_name.Trim())
                            {
                                marque.Applicant_name = "<span class='ompic_value'>" + marque.Applicant_name + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[10].ToString() + "</span>";
                            }


                            if (rowx[3]?.ToString().Split(' ').FirstOrDefault().Trim() != marque.Date_depot.Trim())
                            {
                                marque.Date_depot = "<span class='ompic_value'>" + marque.Date_depot + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[3].ToString().Split(' ').FirstOrDefault().Trim() + "</span>";
                            }










                            if (rowx[5]?.ToString().Split(' ').FirstOrDefault().Trim() != marque.Date_expiration.Trim())
                            {
                                marque.Date_expiration = "<span class='ompic_value'>" + marque.Date_expiration + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[5].ToString().Split(' ').FirstOrDefault().Trim() + "</span>";
                            }

                            if (rowx[15] != null)
                            {
                                foreach (var line in File.ReadAllLines(Server.MapPath("~") + @"\Setting\Country Codes.txt"))
                                {
                                    foreach (var item in line.Split(',').LastOrDefault().Split('/'))
                                    {
                                        if (item.Trim().ToUpper() == rowx[15].ToString().Trim().ToUpper())
                                        {
                                            if (line.Split(',').FirstOrDefault().Trim().ToUpper() != marque.Pays.ToUpper().Trim())
                                            {
                                                marque.Pays = "<span class='ompic_value'>" + marque.Pays + "</span>" + "<br/>" + "<span class='tm_value'>" + line.Split(',').FirstOrDefault().Trim() + "</span>";
                                            }
                                        }
                                    }
                                }
                            }

                            if (rowx[9] != null)
                            {

                                var nice_array_ompic = marque.ClasseNice.TrimEnd(',').Split(',');
                                var nice_array_tm = rowx[9].ToString().TrimEnd(',').Split(',');
                                if (nice_array_ompic.Length > 0)
                                {
                                    foreach (var item in nice_array_ompic)
                                    {
                                        if (!string.IsNullOrWhiteSpace(item))
                                        {
                                            if (!nice_array_tm.Contains(item.Trim()))
                                            {
                                                marque.ClasseNice = "<span class='ompic_value'>" + marque.ClasseNice + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[9].ToString() + "</span>";
                                            }
                                        }
                                    }
                                }

                                else
                                {
                                    if (nice_array_tm.Length > 0)
                                    {
                                        foreach (var item in nice_array_tm)
                                        {
                                            if (!string.IsNullOrWhiteSpace(item))
                                            {
                                                if (!nice_array_ompic.Contains(item.Trim()))
                                                {
                                                    marque.ClasseNice = "<span class='ompic_value'>" + marque.ClasseNice + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[9].ToString() + "</span>";
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (rowx[0]?.ToString().Trim() != marque.Numero_titre.Trim())
                            {
                                marque.Numero_titre = "<span class='ompic_value'>" + marque.Numero_titre + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[0].ToString() + "</span>";
                            }


                            if ((marque.Type.Trim() == "Mixte" && rowx[6]?.ToString().Trim() != "Combined") || (marque.Type.Trim() == "Sonore" && rowx[6]?.ToString().Trim() != "Sound") || (marque.Type.Trim() == "Autres" && rowx[6]?.ToString().Trim() != "Other"))
                            {
                                marque.Type = "<span class='ompic_value'>" + marque.Type + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[6].ToString() + "</span>";
                            }


                            if (rowx[16]?.ToString().Trim() != marque.Representative_name.Trim())
                            {
                                marque.Representative_name = "<span class='ompic_value'>" + marque.Representative_name + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[16].ToString() + "</span>";
                            }




                            if (rowx[1]?.ToString().Trim() != marque.Nom_marque.Trim())
                            {
                                marque.Nom_marque = "<span class='ompic_value'>" + marque.Nom_marque + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[1].ToString() + "</span>";
                            }




                            if (rowx[8] != null)
                            {
                                if (marque.Statut.ToUpper().Trim() == "ENREGISTREE" && rowx[8].ToString().Trim() != "Registered")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if (marque.Statut.ToUpper().Trim() == "RADIEE" && rowx[8].ToString().Trim() != "Registration cancelled")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if ((rowx[8].ToString().Trim() == "Application opposed") && ((marque.Statut.ToUpper().Trim() != "OPPOSITION SUSPENDUE") && (marque.Statut.ToUpper().Trim() != "OPPOSITION EN COURS") && (marque.Statut.ToUpper().Trim() != "OPPOSITION")))
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if (marque.Statut.ToUpper().Trim() == "REJETEE" && rowx[8].ToString().Trim() != "Application refused")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if (marque.Statut.ToUpper().Trim() == "EXPIREE" && rowx[8].ToString().Trim() != "Expired")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if ((marque.Statut.ToUpper().Trim() != "CONSIDEREE COMME RETIREE" && marque.Statut.ToUpper().Trim() != "RETIREE") && (rowx[8].ToString().Trim() == "Application withdrawn"))
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if ((marque.Statut.ToUpper().Trim() != "EN COURS D'EXAMEN" && marque.Statut.ToUpper().Trim() != "EN EXAMEN DE FORME" && marque.Statut.ToUpper().Trim() != "EN INSTANCE DE REGULARISATION" && marque.Statut.ToUpper().Trim() != "EN EXAMEN DES MOTIFS ABSOLUS" && marque.Statut.ToUpper().Trim() != "PUBLICATION PROGRAMMEE") && (rowx[8].ToString().Trim() == "Application filed"))
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if (marque.Statut.ToUpper().Trim() == "PUBLIEE" && rowx[8].ToString().Trim() != "Application published")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if (marque.Statut.ToUpper().Trim() == "EN POURSUITE DE PROCEDURE" && rowx[8].ToString().Trim() != "Appeal pending")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if (marque.Statut.ToUpper().Trim() == "RENOUVELEE" && rowx[8].ToString().Trim() != "Renewed")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if (marque.Statut.ToUpper().Trim() == "RENONCEE" && rowx[8].ToString().Trim() != "Registration surrendered")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                rowtar = rowx;



                            }


                        }
                    }
                    if (rowtar != null)
                    {
                        TM.Rows.Remove(rowtar);
                    }
                    list_marque.Add(marque);
                }
                if (TM.Rows.Count != 0)
                {
                    foreach (DataRow rows in TM.Rows)
                    {
                        var marque = new Marque_TmOmpicModel();
                        marque.Numero_titre = rows[0]?.ToString();
                        marque.Nom_marque = rows[1]?.ToString();
                        marque.Date_depot = rows[3]?.ToString().Split(' ').FirstOrDefault().Trim();
                        marque.Date_expiration = rows[5]?.ToString().Split(' ').FirstOrDefault().Trim();
                        marque.ClasseNice = rows[9]?.ToString();
                        marque.ClasseDetails = rows[32]?.ToString();
                        marque.Type = rows[6]?.ToString();
                        marque.Statut = rows[8]?.ToString();

                        marque.Publication_identifier = rows[21]?.ToString();
                        marque.Publication_section = rows[22]?.ToString();
                        marque.Publication_date = rows[23]?.ToString().Split(' ').FirstOrDefault().Trim();

                        marque.Applicant_name = rows[10]?.ToString();
                        marque.Applicant_legalentity = rows[11]?.ToString();
                        marque.Applicant_nationalityCode = rows[12]?.ToString();
                        marque.Applicant_address = rows[13]?.ToString();
                        marque.Applicant_city = rows[14]?.ToString();
                        marque.Applicant_countryCode = rows[15]?.ToString();

                        marque.Representative_name = rows[16]?.ToString();
                        marque.Representative_nationalityCode = rows[17]?.ToString();
                        marque.Representative_address = rows[18]?.ToString();
                        marque.Representative_city = rows[19]?.ToString();
                        marque.Representative_countryCode = rows[20]?.ToString();

                        marque.OppositionDate = rows[24]?.ToString();
                        marque.Opposition_earlierMark_applicationNumber = rows[25]?.ToString();
                        marque.Opposition_applicant_name = rows[26]?.ToString();
                        marque.Opposition_applicant_legalentity = rows[27]?.ToString();
                        marque.Opposition_nationaliyCode = rows[28]?.ToString();
                        marque.Opposition_applicant_address = rows[29]?.ToString();
                        marque.Opposition_applicant_city = rows[30]?.ToString();
                        marque.Opposition_applicant_countryCode = rows[31]?.ToString();


                        marque.Nombre_opposition = rows[33]?.ToString();
                        list_marque.Add(marque);
                    }
                }

            }
            else if (TM.Rows.Count == 0 && ompic.Rows.Count != 0)
            {
                foreach (DataRow row in ompic.Rows)
                {
                    var marque = new Marque_TmOmpicModel()
                    {
                        Applicant_name = row[0] != null ? row[0].ToString() : "",
                        Date_depot = row[1] != null ? row[1].ToString().Split(' ').FirstOrDefault().Trim() : "",
                        Date_expiration = row[2] != null ? row[2].ToString().Split(' ').FirstOrDefault().Trim() : "",
                        Pays = row[3] != null ? row[3].ToString() : "",
                        Telephone = row[4] != null ? row[4].ToString() : "",
                        ClasseNice = row[5] != null ? row[5].ToString() : "",
                        ClasseDetails = row[6] != null ? row[6].ToString() : "",
                        Numero_titre = row[7] != null ? row[7].ToString() : "",
                        Type = row[8] != null ? row[8].ToString() : "",
                        Representative_name = row[9] != null ? row[9].ToString() : "",
                        Nom_marque = row[10] != null ? row[10].ToString() : "",
                        Applicant_address = row[11] != null ? row[11].ToString() : "",
                        Statut = row[12] != null ? row[12].ToString() : "",
                        Email = row[13] != null ? row[13].ToString() : "",
                        Loi = row[14] != null ? row[14].ToString() : "",
                        Numero_publication = row[15] != null ? row[15].ToString() : "",
                        Updated_date = row[16] != null ? row[16].ToString().Split(' ').FirstOrDefault().Trim() : "",
                        MappingId = row[17] != null ? row[17].ToString() : ""
                    };
                    list_marque.Add(marque);
                }
            }
            else if (TM.Rows.Count != 0 && ompic.Rows.Count == 0)
            {
                foreach (DataRow rows in TM.Rows)
                {
                    var marque = new Marque_TmOmpicModel();
                    marque.Numero_titre = rows[0]?.ToString();
                    marque.Nom_marque = rows[1]?.ToString();
                    marque.Date_depot = rows[3]?.ToString().Split(' ').FirstOrDefault().Trim();
                    marque.Date_expiration = rows[5]?.ToString().Split(' ').FirstOrDefault().Trim();
                    marque.ClasseNice = rows[9]?.ToString();
                    marque.ClasseDetails = rows[32]?.ToString();
                    marque.Type = rows[6]?.ToString();
                    marque.Statut = rows[8]?.ToString();

                    marque.Publication_identifier = rows[21]?.ToString();
                    marque.Publication_section = rows[22]?.ToString();
                    marque.Publication_date = rows[23]?.ToString().Split(' ').FirstOrDefault().Trim();

                    marque.Applicant_name = rows[10]?.ToString();
                    marque.Applicant_legalentity = rows[11]?.ToString();
                    marque.Applicant_nationalityCode = rows[12]?.ToString();
                    marque.Applicant_address = rows[13]?.ToString();
                    marque.Applicant_city = rows[14]?.ToString();
                    marque.Applicant_countryCode = rows[15]?.ToString();

                    marque.Representative_name = rows[16]?.ToString();
                    marque.Representative_nationalityCode = rows[17]?.ToString();
                    marque.Representative_address = rows[18]?.ToString();
                    marque.Representative_city = rows[19]?.ToString();
                    marque.Representative_countryCode = rows[20]?.ToString();

                    marque.OppositionDate = rows[24]?.ToString();
                    marque.Opposition_earlierMark_applicationNumber = rows[25]?.ToString();
                    marque.Opposition_applicant_name = rows[26]?.ToString();
                    marque.Opposition_applicant_legalentity = rows[27]?.ToString();
                    marque.Opposition_nationaliyCode = rows[28]?.ToString();
                    marque.Opposition_applicant_address = rows[29]?.ToString();
                    marque.Opposition_applicant_city = rows[30]?.ToString();
                    marque.Opposition_applicant_countryCode = rows[31]?.ToString();


                    marque.Nombre_opposition = rows[33]?.ToString();
                    list_marque.Add(marque);
                }
            }
            else
            {
                Response.Redirect("Recherche Bd.aspx");
            }
            if (list_marque.Count != 0)
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");
                foreach (var item in list_marque)
                {

                    if (!string.IsNullOrWhiteSpace(item.Numero_titre))
                    {
                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{item.Numero_titre}.jpg"))
                        {
                            item.Image = $@"{item.Numero_titre}.jpg";
                        }
                        else
                        {
                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{item.Numero_titre}.JPG"))
                            {
                                item.Image = $@"{item.Numero_titre}.JPG";
                            }
                            else
                            {
                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{item.Numero_titre}.jpeg"))
                                {
                                    item.Image = $@"{item.Numero_titre}.jpeg";
                                }
                                else
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{item.Numero_titre}.png"))
                                    {
                                        item.Image = $@"{item.Numero_titre}.png";
                                    }
                                    else
                                    {
                                        try
                                        {
                                            webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{item.Numero_titre}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{item.Numero_titre}.jpg");
                                            item.Image = $@"{item.Numero_titre}.jpg";
                                        }
                                        catch (Exception exd)
                                        {
                                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\temp_{item.Numero_titre}.jpg"))
                                            {
                                                item.Image = $@"temp_{item.Numero_titre}.jpg";
                                            }
                                            else
                                            {
                                                Bitmap bmp = new Bitmap(600, 600);
                                                RectangleF rectf = new RectangleF(0, 0, bmp.Width, bmp.Height);
                                                Graphics g = Graphics.FromImage(bmp);
                                                RectangleF rectff = new RectangleF(0, 600, 15, 15);
                                             

                                                g.SmoothingMode = SmoothingMode.AntiAlias;
                                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                                                StringFormat format = new StringFormat()
                                                {
                                                    Alignment = StringAlignment.Center,
                                                    LineAlignment = StringAlignment.Center
                                                };
                                                StringFormat format2 = new StringFormat()
                                                {
                                                    Alignment = StringAlignment.Near,
                                                    LineAlignment = StringAlignment.Near
                                                };
                                                g.FillRectangle(Brushes.White, rectf);
                                                g.DrawString("**", new Font("Tahoma", 60), Brushes.Red, rectf, format2);
                                                g.DrawString( item.Nom_marque, new Font("Tahoma", 60), Brushes.Black, rectf, format);
                                                g.Flush();
                                                MemoryStream ms = new MemoryStream();

                                                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                                                img.Save(Server.MapPath("~") + $@"\Assets\Brand_image\temp_" + item.Numero_titre + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                                item.Image = $@"temp_{item.Numero_titre}.jpg";
                                            }

                                        }


                                    }
                                }
                            }
                        }
                    }


                    //Bitmap bmp = new Bitmap(600, 600);
                    //RectangleF rectf = new RectangleF(0, 0, bmp.Width, bmp.Height);
                    //Graphics g = Graphics.FromImage(bmp);

                    //g.SmoothingMode = SmoothingMode.AntiAlias;
                    //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    //g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    //StringFormat format = new StringFormat()
                    //{
                    //    Alignment = StringAlignment.Center,
                    //    LineAlignment = StringAlignment.Center
                    //};
                    //g.FillRectangle(Brushes.White, rectf);
                    //g.DrawString(item.Nom_marque, new Font("Tahoma", 40), Brushes.Black, rectf, format);
                    //g.Flush();
                    //MemoryStream ms = new MemoryStream();

                    //bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    //img.Save(Server.MapPath("~") + $@"\Assets\Brand_image\temp_" + item.Numero_titre + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    //bmp.Save(ms,)
                    //item.Image = ms.ToArray();

                }
                bool col1 = GridView1.Columns[1].Visible;
                bool col2 = GridView1.Columns[2].Visible;
                bool col3 = GridView1.Columns[3].Visible;
                bool col4 = GridView1.Columns[4].Visible;
                bool col5 = GridView1.Columns[5].Visible;
                bool col6 = GridView1.Columns[6].Visible;
                bool col7 = GridView1.Columns[7].Visible;
                bool col8 = GridView1.Columns[8].Visible;
                bool col9 = GridView1.Columns[9].Visible;
                bool col10 = GridView1.Columns[10].Visible;
                bool col11 = GridView1.Columns[11].Visible;
                bool col12 = GridView1.Columns[12].Visible;
                bool col13 = GridView1.Columns[13].Visible;
                bool col14 = GridView1.Columns[14].Visible;
                bool col15 = GridView1.Columns[15].Visible;
                bool col16 = GridView1.Columns[16].Visible;
                bool col17 = GridView1.Columns[17].Visible;
                bool col18 = GridView1.Columns[18].Visible;
                bool col19 = GridView1.Columns[19].Visible;
                GridView1.Columns[1].Visible = true;
                GridView1.Columns[2].Visible = true;
                GridView1.Columns[3].Visible = true;
                GridView1.Columns[4].Visible = true;
                GridView1.Columns[5].Visible = true;
                GridView1.Columns[6].Visible = true;
                GridView1.Columns[7].Visible = true;
                GridView1.Columns[8].Visible = true;
                GridView1.Columns[9].Visible = true;
                GridView1.Columns[10].Visible = true;
                GridView1.Columns[11].Visible = true;
                GridView1.Columns[12].Visible = true;
                GridView1.Columns[13].Visible = true;
                GridView1.Columns[14].Visible = true;
                GridView1.Columns[15].Visible = true;
                GridView1.Columns[16].Visible = true;
                GridView1.Columns[17].Visible = true;
                GridView1.Columns[18].Visible = true;
                GridView1.Columns[19].Visible = true;
                Session["BDMarques"] = list_marque;
                resultalbl.InnerText = list_marque.Count.ToString();
                GridView1.DataSource = list_marque.Take(100);
                GridView1.DataBind();
                ViewState["index"] = 0;
                double result = list_marque.Count / 100.0;
                int pages = int.Parse(Math.Ceiling(result).ToString());
                index.Text = 1 + " / " + (pages == 0 ? 1 : pages);
                GridView1.Columns[1].Visible = col1;
                GridView1.Columns[2].Visible = col2;
                GridView1.Columns[3].Visible = col3;
                GridView1.Columns[4].Visible = col4;
                GridView1.Columns[5].Visible = col5;
                GridView1.Columns[6].Visible = col6;
                GridView1.Columns[7].Visible = col7;
                GridView1.Columns[8].Visible = col8;
                GridView1.Columns[9].Visible = col9;
                GridView1.Columns[10].Visible = col10;
                GridView1.Columns[11].Visible = col11;
                GridView1.Columns[12].Visible = col12;
                GridView1.Columns[13].Visible = col13;
                GridView1.Columns[14].Visible = col14;
                GridView1.Columns[15].Visible = col15;
                GridView1.Columns[16].Visible = col16;
                GridView1.Columns[17].Visible = col17;
                GridView1.Columns[18].Visible = col18;
                GridView1.Columns[19].Visible = col19;
                ViewState["Id_Sort_dir"] = "ASC";
                ViewState["nom"] = "ASC";
                ViewState["deposant"] = "ASC";
                ViewState["datede"] = "ASC";
                ViewState["dateexp"] = "ASC";
                ViewState["statut"] = "ASC";
                ViewState["mondataire"] = "ASC";

            }






        }
        protected void Precedent_Click(object sender, EventArgs e)
        {


            if (Session["Marqueschecked"] == null)
            {
                List<string> marquesidchecked = new List<string>();



                foreach (GridViewRow row in GridView1.Rows)
                {
                    //if (item.checkede == "true")
                    //{
                    //    gh++;
                    //}


                    CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                    if (cb.Checked == true)
                    {
                        if (!marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Add(row.Cells[1].Text);
                        }

                    }
                    else
                    {
                        if (marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Remove(row.Cells[1].Text);
                        }
                    }



                }
                Session["Marqueschecked"] = marquesidchecked;
            }
            else
            {
                List<string> marquesidchecked = Session["Marqueschecked"] as List<string>;


                foreach (GridViewRow row in GridView1.Rows)
                {
                    //if (item.checkede == "true")
                    //{
                    //    gh++;
                    //}


                    CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                    if (cb.Checked == true)
                    {
                        if (!marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Add(row.Cells[1].Text);
                        }

                    }
                    else
                    {
                        if (marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Remove(row.Cells[1].Text);
                        }
                    }



                }
                Session["Marqueschecked"] = marquesidchecked;
            }


            if (Session["BDMarques"] != null)
            {
                List<Marque_TmOmpicModel> marques = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                int i = int.Parse(ViewState["index"].ToString());
                double result = marques.Count / 100.0;
                int pages = int.Parse(Math.Ceiling(result).ToString());
                if (i > 0)
                {
                    i--;
                    GridView1.DataSource = marques.Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["index"] = i;
                }
            }

            List<string> marquesidcheckedd = Session["Marqueschecked"] as List<string>;
            if (marquesidcheckedd.Count != 0)
            {
                foreach (string id in marquesidcheckedd)
                {
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (marquesidcheckedd.Contains(row.Cells[1].Text))
                        {


                            ((CheckBox)GridView1.Rows[row.RowIndex].FindControl("CheckBox1")).Checked = true;







                        }
                    }

                }
            }

        }

        protected void Suivant_Click(object sender, EventArgs e)
        {
            if (Session["Marqueschecked"] == null)
            {
                List<string> marquesidchecked = new List<string>();



                foreach (GridViewRow row in GridView1.Rows)
                {
                    //if (item.checkede == "true")
                    //{
                    //    gh++;
                    //}


                    CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                    if (cb.Checked == true)
                    {
                        if (!marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Add(row.Cells[1].Text);
                        }

                    }
                    else
                    {
                        if (marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Remove(row.Cells[1].Text);
                        }
                    }



                }
                Session["Marqueschecked"] = marquesidchecked;
            }
            else
            {
                List<string> marquesidchecked = Session["Marqueschecked"] as List<string>;


                foreach (GridViewRow row in GridView1.Rows)
                {
                    //if (item.checkede == "true")
                    //{
                    //    gh++;
                    //}


                    CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                    if (cb.Checked == true)
                    {
                        if (!marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Add(row.Cells[1].Text);
                        }

                    }
                    else
                    {
                        if (marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Remove(row.Cells[1].Text);
                        }
                    }



                }
                Session["Marqueschecked"] = marquesidchecked;
            }


            if (Session["BDMarques"] != null)
            {
                List<Marque_TmOmpicModel> marques = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                int i = int.Parse(ViewState["index"].ToString());
                double result = marques.Count / 100.0;
                int pages = int.Parse(Math.Ceiling(result).ToString());
                if (i < pages - 1)
                {
                    i++;
                    GridView1.DataSource = marques.Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["index"] = i;
                }
            }
            List<string> marquesidcheckedd = Session["Marqueschecked"] as List<string>;
            if (marquesidcheckedd.Count != 0)
            {
                foreach (string id in marquesidcheckedd)
                {
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (marquesidcheckedd.Contains(row.Cells[1].Text))
                        {


                            ((CheckBox)GridView1.Rows[row.RowIndex].FindControl("CheckBox1")).Checked = true;







                        }
                    }

                }
            }
        }

        protected void Afficher_Masquer_Click(object sender, EventArgs e)
        {
            if (Request.Form["columns_dropdown"] == "Nom_marque") { if (GridView1.Columns[3].Visible == false) { GridView1.Columns[3].Visible = true; } else { GridView1.Columns[3].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Date_depot") { if (GridView1.Columns[6].Visible == false) { GridView1.Columns[6].Visible = true; } else { GridView1.Columns[6].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Date_expiration") { if (GridView1.Columns[7].Visible == false) { GridView1.Columns[7].Visible = true; } else { GridView1.Columns[7].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Applicant_name") { if (GridView1.Columns[4].Visible == false) { GridView1.Columns[4].Visible = true; } else { GridView1.Columns[4].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Applicant_address") { if (GridView1.Columns[10].Visible == false) { GridView1.Columns[10].Visible = true; } else { GridView1.Columns[10].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Pays") { if (GridView1.Columns[11].Visible == false) { GridView1.Columns[11].Visible = true; } else { GridView1.Columns[11].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Representative_name") { if (GridView1.Columns[5].Visible == false) { GridView1.Columns[5].Visible = true; } else { GridView1.Columns[5].Visible = false; } }

            if (Request.Form["columns_dropdown"] == "Representative_address") { if (GridView1.Columns[12].Visible == false) { GridView1.Columns[12].Visible = true; } else { GridView1.Columns[12].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Representative_countryCode") { if (GridView1.Columns[13].Visible == false) { GridView1.Columns[13].Visible = true; } else { GridView1.Columns[13].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Type") { if (GridView1.Columns[14].Visible == false) { GridView1.Columns[14].Visible = true; } else { GridView1.Columns[14].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Statut") { if (GridView1.Columns[9].Visible == false) { GridView1.Columns[9].Visible = true; } else { GridView1.Columns[9].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Email") { if (GridView1.Columns[15].Visible == false) { GridView1.Columns[15].Visible = true; } else { GridView1.Columns[15].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Telephone") { if (GridView1.Columns[16].Visible == false) { GridView1.Columns[16].Visible = true; } else { GridView1.Columns[16].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "ClasseNice") { if (GridView1.Columns[8].Visible == false) { GridView1.Columns[8].Visible = true; } else { GridView1.Columns[8].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Opposition_applicant_name") { if (GridView1.Columns[17].Visible == false) { GridView1.Columns[17].Visible = true; } else { GridView1.Columns[17].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Opposition_earlierMark_applicationNumber") { if (GridView1.Columns[18].Visible == false) { GridView1.Columns[18].Visible = true; } else { GridView1.Columns[18].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Nombre_opposition") { if (GridView1.Columns[19].Visible == false) { GridView1.Columns[19].Visible = true; } else { GridView1.Columns[19].Visible = false; } }
        }

        protected void ShowDetails_Click(object sender, EventArgs e)
        {

            Button ShowArgs = sender as Button;
            GridViewRow gridViewRow = ShowArgs.NamingContainer as GridViewRow;
            string st = gridViewRow.Cells[1].Text;
            Response.Write($"<script language='javascript'>window.open('Details.aspx?ST13={st}','_blank');</script>");
        }

        protected void Export_pdf_Click(object sender, EventArgs e)
        {

            List<Marque_TmOmpicModel> marques = Session["BDMarques"] as List<Marque_TmOmpicModel>;
            List<tmopmic> test = new List<tmopmic>();
            //List<Marque_TmOmpicModel> test2 = Session["list_marques_searched"] as List<Marque_TmOmpicModel>;
            //foreach (var item in list_marque)
            //{
            //    test2.Add(new Marque_TmOmpicModel { Numero_titre = item.Numero_titre, Nom_marque = item.Nom_marque, Date_depot = item.Date_depot, Date_expiration = item.Date_expiration, Applicant_name = item.Applicant_name, Representative_name = item.Representative_name, ClasseNice = item.ClasseNice });
            //}
            System.Data.DataSet ds = new System.Data.DataSet("DataTable1");
            DataTable table = new DataTable("DataTable1");

            table.Columns.Add("NumeroTitre");
            table.Columns.Add("image");

            table.Columns.Add("Nommarque");
            table.Columns.Add("Datedepot");
            table.Columns.Add("Dateexpiration");
            table.Columns.Add("Deposant");
            table.Columns.Add("Mandataire");
            table.Columns.Add("Statut");
            table.Columns.Add("ClasseNice");
            table.Columns["image"].DataType = System.Type.GetType("System.Byte[]");

            foreach (Marque_TmOmpicModel marque in marques)
            {
                var ms = new MemoryStream();
                DataRow row = table.NewRow();
                string imgname = "";

                row["NumeroTitre"] = marque.Numero_titre;

                if (!string.IsNullOrWhiteSpace(marque.Numero_titre))
                {
                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.jpg"))
                    {
                        imgname = $@"{marque.Numero_titre}.jpg";
                    }
                    else
                    {
                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.JPG"))
                        {
                            imgname = $@"{marque.Numero_titre}.JPG";
                        }
                        else
                        {
                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.jpeg"))
                            {
                                imgname = $@"{marque.Numero_titre}.jpeg";
                            }
                            else
                            {
                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.png"))
                                {
                                    imgname = $@"{marque.Numero_titre}.png";
                                }
                                //else
                                //{
                                //    webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{marque.NumeroTitre}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{marque.NumeroTitre}.jpg");
                                //    marque.Image = $@"{marque.NumeroTitre}.jpg";
                                //}
                            }
                        }
                    }
                }

                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgname))
                {
                    System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgname).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\Empty.png").Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                row["image"] = ms.ToArray();
                row["Nommarque"] = marque.Nom_marque;
                row["Datedepot"] = marque.Date_depot;
                row["Dateexpiration"] = marque.Date_expiration;
                row["Deposant"] = marque.Applicant_name;

                row["Mandataire"] = marque.Representative_name;

                row["Statut"] = marque.Statut;
                row["ClasseNice"] = marque.ClasseNice;
                table.Rows.Add(row);



            }
            foreach (DataRow rt in table.Rows)
            {

                rt[5] = retext(rt[5].ToString());
                rt[8] = retext(rt[8].ToString());
                rt[4] = retext(rt[4].ToString());
                rt[3] = retext(rt[3].ToString());
                rt[2] = retext(rt[2].ToString());
                rt[6] = retext(rt[6].ToString());
                rt[7] = retext(rt[7].ToString());

            }
            ds.Tables.Add(table);


            CrystalReport3 report = new CrystalReport3();
            report.Database.Tables["DataTable1"].SetDataSource(ds);
            string filename = $"resultat_export.pdf";
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            report.Close();
            report.Dispose();
            MemoryStream memoryS = new MemoryStream();
            stream.CopyTo(memoryS);
            byte[] buffer = memoryS.ToArray();
            

            Response.AddHeader("Content-Length", buffer.Length.ToString());
            Response.AddHeader("Content-Disposition", $"attachment; filename={filename}");
            Response.OutputStream.Write(buffer, 0, buffer.Length);
            Response.End();



            ///////////////////////////////////////////////////////////////////////////////////////////
            //StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            //pdfDoc.Close();
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Write(pdfDoc);
            //Response.End();
        }
        public string retext(string db)
        {


            db = db.Replace(@"<span class='ompic_value'>", "");
            db = db.Replace(@"</span>", "");
            db = db.Replace(@"<br/>", "\n\n");
            db = db.Replace(@"<span class='tm_value'>", "");
            db = db.Replace(@"&nbsp;", "-");


            return db;

        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            if (this.GridView1.Rows.Count > 0)
            {
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                //gvEmployees.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void SELECT_pdf_Click(object sender, EventArgs e)
        {



            if (Session["Marqueschecked"] == null)
            {
                List<string> marquesidchecked = new List<string>();



                foreach (GridViewRow row in GridView1.Rows)
                {
                    //if (item.checkede == "true")
                    //{
                    //    gh++;
                    //}


                    CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                    if (cb.Checked == true)
                    {
                        if (!marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Add(row.Cells[1].Text);
                        }

                    }
                    else
                    {
                        if (marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Remove(row.Cells[1].Text);
                        }
                    }



                }
                Session["Marqueschecked"] = marquesidchecked;
            }
            else
            {
                List<string> marquesidchecked = Session["Marqueschecked"] as List<string>;


                foreach (GridViewRow row in GridView1.Rows)
                {
                    //if (item.checkede == "true")
                    //{
                    //    gh++;
                    //}


                    CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                    if (cb.Checked == true)
                    {
                        if (!marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Add(row.Cells[1].Text);
                        }

                    }
                    else
                    {
                        if (marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Remove(row.Cells[1].Text);
                        }
                    }



                }
            }

            List<string> marquesidcheckedd = Session["Marqueschecked"] as List<string>;
            if (marquesidcheckedd.Count != 0)
            {
                foreach (string id in marquesidcheckedd)
                {
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (marquesidcheckedd.Contains(row.Cells[1].Text))
                        {


                            ((CheckBox)GridView1.Rows[row.RowIndex].FindControl("CheckBox1")).Checked = true;







                        }
                    }

                }
            }




            List<Marque_TmOmpicModel> marques = Session["BDMarques"] as List<Marque_TmOmpicModel>;
            List<tmopmic> test = new List<tmopmic>();
            //List<Marque_TmOmpicModel> test2 = Session["list_marques_searched"] as List<Marque_TmOmpicModel>;
            //foreach (var item in list_marque)
            //{
            //    test2.Add(new Marque_TmOmpicModel { Numero_titre = item.Numero_titre, Nom_marque = item.Nom_marque, Date_depot = item.Date_depot, Date_expiration = item.Date_expiration, Applicant_name = item.Applicant_name, Representative_name = item.Representative_name, ClasseNice = item.ClasseNice });
            //}
            System.Data.DataSet ds = new System.Data.DataSet("DataTable1");
            DataTable table = new DataTable("DataTable1");

            table.Columns.Add("NumeroTitre");
            table.Columns.Add("image");

            table.Columns.Add("Nommarque");
            table.Columns.Add("Datedepot");
            table.Columns.Add("Dateexpiration");
            table.Columns.Add("Deposant");
            table.Columns.Add("Mandataire");
            table.Columns.Add("Statut");
            table.Columns.Add("ClasseNice");
            table.Columns["image"].DataType = System.Type.GetType("System.Byte[]");

            foreach (Marque_TmOmpicModel marque in marques)
            {
                if (marquesidcheckedd.Contains(marque.Numero_titre))
                {
                    var ms = new MemoryStream();
                    DataRow row = table.NewRow();
                    string imgname = "";

                    row["NumeroTitre"] = marque.Numero_titre;

                    if (!string.IsNullOrWhiteSpace(marque.Numero_titre))
                    {
                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.jpg"))
                        {
                            imgname = $@"{marque.Numero_titre}.jpg";
                        }
                        else
                        {
                            if (File.Exists(Server.MapPath("~") + $@"\IppApp\Assets\Brand_image\{marque.Numero_titre}.JPG"))
                            {
                                imgname = $@"{marque.Numero_titre}.JPG";
                            }
                            else
                            {
                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.jpeg"))
                                {
                                    imgname = $@"{marque.Numero_titre}.jpeg";
                                }
                                else
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.png"))
                                    {
                                        imgname = $@"{marque.Numero_titre}.png";
                                    }
                                    //else
                                    //{
                                    //    webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{marque.NumeroTitre}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{marque.NumeroTitre}.jpg");
                                    //    marque.Image = $@"{marque.NumeroTitre}.jpg";
                                    //}
                                }
                            }
                        }
                    }

                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgname))
                    {
                        System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgname).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else
                    {
                        System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\Empty.png").Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    row["image"] = ms.ToArray();
                    row["Nommarque"] = marque.Nom_marque;
                    row["Datedepot"] = marque.Date_depot;
                    row["Dateexpiration"] = marque.Date_expiration;
                    row["Deposant"] = marque.Applicant_name;

                    row["Mandataire"] = marque.Representative_name;

                    row["Statut"] = marque.Statut;
                    row["ClasseNice"] = marque.ClasseNice;
                    table.Rows.Add(row);
                }



            }
            foreach (DataRow rt in table.Rows)
            {

                rt[5] = retext(rt[5].ToString());
                rt[8] = retext(rt[8].ToString());
                rt[4] = retext(rt[4].ToString());
                rt[3] = retext(rt[3].ToString());
                rt[2] = retext(rt[2].ToString());
                rt[6] = retext(rt[6].ToString());
                rt[7] = retext(rt[7].ToString());

            }
            ds.Tables.Add(table);


            CrystalReport3 report = new CrystalReport3();
            report.Database.Tables["DataTable1"].SetDataSource(ds);
            string filename = $"resultat_export.pdf";
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            report.Close();
            report.Dispose();
            MemoryStream memoryS = new MemoryStream();
            stream.CopyTo(memoryS);
            byte[] buffer = memoryS.ToArray();

            Response.AddHeader("Content-Length", buffer.Length.ToString());
            Response.AddHeader("Content-Disposition", $"attachment; filename={filename}");
            Response.OutputStream.Write(buffer, 0, buffer.Length);
            Response.End();


        }
        public void redate(string dt)
        {
            List<string> list = dt.Split('-').ToList();
            dt = list[2] + "/" + list[1] + "/" + list[0];
        }
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            ////Response.Write("SortExpression" + e.SortExpression.ToString());
            ////Response.Write("<br/>");
            ////Response.Write("sortdirection" + e.SortDirection.ToString());

            if (e.SortExpression == "Numero_titre")
            {
                if (ViewState["Id_Sort_dir"] != null && ViewState["Id_Sort_dir"].ToString() == "ASC")
                {
                    List<Marque_TmOmpicModel> list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    Session["BDMarques"] = list_marque.OrderBy((mrq) => mrq.Numero_titre).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["BDMarques"] as List<Marque_TmOmpicModel>).Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    double result = list_marque.Count / 100.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["Id_Sort_dir"] = "DESC";
                }
                else if (ViewState["Id_Sort_dir"] != null && ViewState["Id_Sort_dir"].ToString() == "DESC")
                {
                    List<Marque_TmOmpicModel> list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    Session["BDMarques"] = list_marque.OrderByDescending((mrq) => mrq.Numero_titre).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["BDMarques"] as List<Marque_TmOmpicModel>).Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    double result = list_marque.Count / 100.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["Id_Sort_dir"] = "ASC";
                }
            }
            if (e.SortExpression == "Nom_Marque")
            {
                if (ViewState["nom"] != null && ViewState["nom"].ToString() == "ASC")
                {
                    List<Marque_TmOmpicModel> list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    Session["BDMarques"] = list_marque.OrderByDescending((mrq) => mrq.Nom_marque).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["BDMarques"] as List<Marque_TmOmpicModel>).Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    double result = list_marque.Count / 100.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["nom"] = "DESC";
                }
                else if (ViewState["nom"] != null && ViewState["nom"].ToString() == "DESC")
                {
                    List<Marque_TmOmpicModel> list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    Session["BDMarques"] = list_marque.OrderBy((mrq) => mrq.Nom_marque).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["BDMarques"] as List<Marque_TmOmpicModel>).Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    double result = list_marque.Count / 100.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["nom"] = "ASC";
                }
            }
            if (e.SortExpression == "Deposant")
            {
                if (ViewState["deposant"] != null && ViewState["deposant"].ToString() == "ASC")
                {
                    List<Marque_TmOmpicModel> list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    Session["BDMarques"] = list_marque.OrderByDescending((mrq) => mrq.Applicant_name).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["BDMarques"] as List<Marque_TmOmpicModel>).Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    double result = list_marque.Count / 100.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["deposant"] = "DESC";
                }
                else if (ViewState["deposant"] != null && ViewState["deposant"].ToString() == "DESC")
                {
                    List<Marque_TmOmpicModel> list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    Session["BDMarques"] = list_marque.OrderBy((mrq) => mrq.Applicant_name).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["BDMarques"] as List<Marque_TmOmpicModel>).Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    double result = list_marque.Count / 100.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["deposant"] = "ASC";
                }
            }
            if (e.SortExpression == "Mandataire")
            {
                if (ViewState["mondataire"] != null && ViewState["mondataire"].ToString() == "ASC")
                {
                    List<Marque_TmOmpicModel> list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    Session["BDMarques"] = list_marque.OrderByDescending((mrq) => mrq.Publication_identifier).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["BDMarques"] as List<Marque_TmOmpicModel>).Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    double result = list_marque.Count / 100.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["mondataire"] = "DESC";
                }
                else if (ViewState["mondataire"] != null && ViewState["mondataire"].ToString() == "DESC")
                {
                    List<Marque_TmOmpicModel> list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    Session["BDMarques"] = list_marque.OrderBy((mrq) => mrq.Publication_identifier).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["BDMarques"] as List<Marque_TmOmpicModel>).Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    double result = list_marque.Count / 100.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["mondataire"] = "ASC";
                }
            }
            if (e.SortExpression == "Date_depot")
            {
                if (ViewState["datede"] != null && ViewState["datede"].ToString() == "ASC")
                {
                    List<Marque_TmOmpicModel> list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    Session["BDMarques"] = list_marque.OrderByDescending((mrq) => mrq.Date_depot).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["BDMarques"] as List<Marque_TmOmpicModel>).Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    double result = list_marque.Count / 100.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["datede"] = "DESC";
                }
                else if (ViewState["datede"] != null && ViewState["datede"].ToString() == "DESC")
                {
                    List<Marque_TmOmpicModel> list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    Session["BDMarques"] = list_marque.OrderBy((mrq) => mrq.Date_depot).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["BDMarques"] as List<Marque_TmOmpicModel>).Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    double result = list_marque.Count / 100.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["datede"] = "ASC";
                }
            }
            if (e.SortExpression == "Date_expiration")
            {
                if (ViewState["dateexp"] != null && ViewState["dateexp"].ToString() == "ASC")
                {
                    List<Marque_TmOmpicModel> list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    Session["BDMarques"] = list_marque.OrderByDescending((mrq) => mrq.Date_expiration).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["BDMarques"] as List<Marque_TmOmpicModel>).Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    double result = list_marque.Count / 100.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["dateexp"] = "DESC";
                }
                else if (ViewState["dateexp"] != null && ViewState["dateexp"].ToString() == "DESC")
                {
                    List<Marque_TmOmpicModel> list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    Session["BDMarques"] = list_marque.OrderBy((mrq) => mrq.Date_expiration).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["BDMarques"] as List<Marque_TmOmpicModel>).Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    double result = list_marque.Count / 100.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["dateexp"] = "ASC";
                }
            }
            if (e.SortExpression == "Statut")
            {
                if (ViewState["statut"] != null && ViewState["statut"].ToString() == "ASC")
                {
                    List<Marque_TmOmpicModel> list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    Session["BDMarques"] = list_marque.OrderByDescending((mrq) => mrq.Statut).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["BDMarques"] as List<Marque_TmOmpicModel>).Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    double result = list_marque.Count / 100.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["statut"] = "DESC";
                }
                else if (ViewState["statut"] != null && ViewState["statut"].ToString() == "DESC")
                {
                    List<Marque_TmOmpicModel> list_marque = Session["BDMarques"] as List<Marque_TmOmpicModel>;
                    Session["BDMarques"] = list_marque.OrderBy((mrq) => mrq.Statut).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["BDMarques"] as List<Marque_TmOmpicModel>).Skip(i * 100).Take(100);
                    GridView1.DataBind();
                    double result = list_marque.Count / 100.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["statut"] = "ASC";
                }
            }
        }
        public static string retextplus(string db)
        {
            if (db != null)
            {
                List<string> list;
                db = db.Replace(@"<span class='ompic_value'>", "");
                db = db.Replace(@"</span>", "");

                db = db.Replace(@"<span class='tm_value'>", "");
                db = db.Replace(@"&nbsp;", "-");
                if (db.Contains("<br/>"))
                {

                    db = db.Replace(@"<br/>", "=");
                    list = db.Split('=').ToList();
                    db = list[0];
                    return db;
                }
                else
                {

                    return db;
                }
            }
            else
            {
                return "";
            }
        }


        protected void Button1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                List<Marque_TmOmpicModel> marques = Session["BDMarques"] as List<Marque_TmOmpicModel>;

                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=Ipp;Integrated Security=True");
                System.Data.SqlClient.SqlCommand sQLiteCommand = new System.Data.SqlClient.SqlCommand();
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
                sQLiteCommand.Connection = con;
                command.Connection = con;
                sQLiteCommand.CommandText = "insert into Marque_historique VALUES (@1,@2,@3,@4,@5)";

                if (marques != null)
                {
                    var options = new ChromeOptions();
                    options.AddArgument("no-sandbox");
                    ChromeDriver Driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(3));
                    //Driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(30));
                    foreach (Marque_TmOmpicModel marquet in marques)
                    {
                        if (marquet.MappingId == "" || marquet.MappingId == null)
                        {
                            command.Parameters.Clear();
                            System.Threading.Thread.Sleep(1000);
                            string item = marquet.Numero_titre;
                            Driver.Navigate().GoToUrl($"http://search.ompic.ma/web/pages/consulterMarqueTMView.do?refReglementationTitreLabel=97-{item}");
                            if (Driver.Title != "Page error")
                            {
                                if (Driver.Title != "ERREUR : l'URL demandée n'a pas pu être chargée")
                                {
                                    //if (Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[2]/td[2]")).Text != "")
                                    //{
                                    //    if (Driver.FindElement(By.XPath("/html/body/div/table[1]/tbody/tr[3]/td/table/tbody/tr[1]/td[2]")).Text.Trim() != "")
                                    //    {
                                    string marquee = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[1]/td[2]")).Text;
                                    string fulldep = Driver.FindElement(By.XPath("/html/body/div/table[1]/tbody/tr[3]/td/table/tbody/tr[8]/td[2]")).Text;
                                    List<string> list = fulldep.Split(',').ToList();
                                    string deposent = list[0].ToString();
                                    //Console.WriteLine(fulldep);
                                    //Console.WriteLine(deposent);

                                    string marque = marquee + " " + deposent.TrimEnd();
                                    Driver.Navigate().GoToUrl($"https://www.directinfo.ma/");
                                    //System.Threading.Thread.Sleep(7000);
                                    OpenQA.Selenium.Support.UI.WebDriverWait wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, TimeSpan.FromSeconds(60));


                                    // get the "Add Item" element
                                    wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("/html/body/app-root/app-home/app-header-hp/header/div[2]/div/form/input[1]")));
                                    Driver.FindElement(By.XPath("/html/body/app-root/app-home/app-header-hp/header/div[2]/div/form/input[1]")).SendKeys(marque);
                                    Driver.FindElement(By.XPath("/html/body/app-root/app-home/app-header-hp/header/div[2]/div/form/input[2]")).Click();
                                    wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[1]/ul/li[2]")));
                                    string g = Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[1]/ul/li[2]/span/div/span")).Text;
                                    //Console.WriteLine(g);
                                    g = g.Replace("(", "");
                                    g = g.Replace(")", "");


                                    int nb = int.Parse(g);
                                    if (nb != 0)
                                    {
                                        int type = 0;
                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[1]/ul/li[2]")).Click();
                                        for (int i = 1; i < nb + 1; i++)
                                        {
                                            int j = i;

                                            if (i > 10)
                                            {
                                                if (type == 0)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[2]")).Click();
                                                    type = 2;
                                                }

                                                j = i - 10;

                                            }
                                            if (i > 20)
                                            {
                                                if (type == 2)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[3]")).Click();
                                                    type = 3;
                                                }

                                                j = i - 20;
                                            }
                                            if (i > 30)
                                            {
                                                if (type == 3)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[4]")).Click();
                                                    type = 4;
                                                }

                                                j = i - 30;
                                            }
                                            if (i > 40)
                                            {
                                                if (type == 4)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[5]")).Click();
                                                    type = 5;
                                                }

                                                j = i - 40;
                                            }
                                            if (i > 50)
                                            {
                                                if (type == 5)
                                                {
                                                    try
                                                    {
                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[6]")).Click();
                                                        type = 6;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        continue;
                                                    }
                                                }

                                                j = i - 50;

                                            }
                                            if (i > 60)
                                            {
                                                if (type == 6)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[7]")).Click();
                                                    type = 7;
                                                }


                                                j = i - 60;
                                            }
                                            if (i > 70)
                                            {
                                                if (type == 7)
                                                {

                                                    if (nb > 100)
                                                    {
                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                    }
                                                    else
                                                    {
                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[8]")).Click();
                                                    }

                                                    type = 8;
                                                }

                                                j = i - 70;
                                            }
                                            if (i > 80)
                                            {
                                                if (type == 8)
                                                {
                                                    if (nb > 100)
                                                    {
                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                    }
                                                    else
                                                    {
                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[9]")).Click();
                                                    }
                                                    type = 9;
                                                }
                                                j = i - 80;
                                            }
                                            if (i > 90)
                                            {
                                                if (type == 9)
                                                {
                                                    if (nb > 100)
                                                    {
                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                    }
                                                    else
                                                    {
                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[10]")).Click();
                                                    }
                                                    type = 10;
                                                }

                                                j = i - 90;
                                            }
                                            if (i > 100)
                                            {
                                                if (type == 10)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                    type = 11;
                                                }

                                                j = i - 100;
                                            }
                                            if (i > 110)
                                            {
                                                if (type == 11)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                    type = 12;
                                                }

                                                j = i - 110;

                                            }
                                            if (i > 120)
                                            {
                                                if (type == 12)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                    type = 13;
                                                }

                                                j = i - 120;
                                            }
                                            if (i > 130)
                                            {
                                                if (type == 13)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                    type = 14;
                                                }

                                                j = i - 140;
                                            }
                                            if (i > 140)
                                            {
                                                if (type == 14)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                    type = 15;
                                                }

                                                j = i - 140;
                                            }
                                            if (i > 150)
                                            {
                                                if (type == 15)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                    type = 16;
                                                }

                                                j = i - 150;

                                            }
                                            if (i > 160)
                                            {
                                                if (type == 16)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                    type = 17;
                                                }

                                                j = i - 160;
                                            }
                                            if (i > 170)
                                            {
                                                if (type == 17)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                    type = 18;
                                                }

                                                j = i - 170;
                                            }
                                            if (i > 180)
                                            {
                                                if (type == 18)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                    type = 19;
                                                }

                                                j = i - 180;
                                            }
                                            if (i > 190)
                                            {
                                                if (type == 19)
                                                {
                                                    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                    type = 20;
                                                }

                                                j = i - 190;
                                            }
                                            if (i > 200)
                                            {
                                                //if (type == 10)
                                                //{
                                                //    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                //}
                                                //type = 11;
                                                //j = i - 100;
                                                continue;
                                            }
                                            //if (i == nb)
                                            //{
                                            //    //if (type == 10)
                                            //    //{
                                            //    //    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                            //    //}
                                            //    //type = 11;
                                            //    //j = i - 100;
                                            //    break;
                                            //}


                                            //Console.WriteLine(marque + "==" + Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page[1]/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/ul/li[" + i.ToString() + "]/h2")).Text);
                                            //Console.WriteLine(item + "==" + Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page[1]/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/ul/li[" + i.ToString() + "]/div/p[1]")).Text);
                                            try
                                            {
                                                string bg = Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page[1]/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/ul/li[" + j.ToString() + "]/div/p[1]")).Text;
                                                bg = bg.Replace("Numéro Titre : ", "");
                                                if (marquet.Numero_titre == bg.Trim())
                                                {

                                                    //Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page[1]/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/ul/li["+i.ToString()+"]/a")).Click();
                                                    string link = Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page[1]/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/ul/li[" + j.ToString() + "]/a")).GetAttribute("href");
                                                    Driver.Navigate().GoToUrl(link);


                                                    //string bfbf = Console.ReadLine();
                                                    if (Driver.Title != "Page error")
                                                    {
                                                        if (Driver.Title != "ERREUR : l'URL demandée n'a pas pu être chargée")
                                                        {
                                                            if (Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[2]/td[2]")).Text != "")
                                                            {
                                                                if (Driver.FindElement(By.XPath("/html/body/div/table[1]/tbody/tr[3]/td/table/tbody/tr[1]/td[2]")).Text.Trim() != "")
                                                                {
                                                                    command.Parameters.Clear();
                                                                    con.Close();
                                                                    Marque marrque = new Marque();
                                                                    string[] tab;
                                                                    List<string> list_nice = new List<string>();
                                                                    string temp = "";
                                                                    marrque.Nommarque = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[1]/td[2]")).Text;
                                                                    marrque.NumeroTitre = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[2]/td[2]")).Text;
                                                                    marrque.DateDepot = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[3]/td[2]")).Text;
                                                                    marrque.Type = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[4]/td[2]")).Text;
                                                                    marrque.Loi = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[5]/td[2]")).Text;
                                                                    marrque.Etat = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[6]/td[2]")).Text;
                                                                    marrque.NumeroPub = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[7]/td[2]")).Text;
                                                                    temp = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text;
                                                                    tab = temp != null ? temp.Split(',') : null;
                                                                    if (tab != null)
                                                                    {
                                                                        marrque.Titulaire = tab.Length > 0 ? tab[0] : "";
                                                                        marrque.Adresse = temp.Replace(tab[0], "").Replace(tab.Last(), "").Replace(",", "");
                                                                        marrque.Pays = tab.Last();
                                                                    }
                                                                    marrque.Mandataire = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[9]/td[2]")).Text;
                                                                    marrque.DateExpiration = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[11]/td[2]")).Text;
                                                                    marrque.Classe_details = "";
                                                                    for (int p = 1; p <= Driver.FindElements(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr")).Count; p++)
                                                                    {
                                                                        marrque.Classe_details += Driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{p}]/td[1]")).Text.Trim() + " : " + Driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{p}]/td[2]")).Text.Trim() + Environment.NewLine;
                                                                        temp = Driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{p}]/td[1]")).Text.Trim();
                                                                        if (list_nice.Contains(temp))
                                                                        {

                                                                        }
                                                                        else
                                                                        {
                                                                            list_nice.Add(temp);
                                                                        }
                                                                    }
                                                                    marrque.Classe_nice = "";
                                                                    foreach (var nice in list_nice)
                                                                    {
                                                                        marrque.Classe_nice += nice + ",";
                                                                    }

                                                                    marquet.Nom_marque = retextplus(marquet.Nom_marque);
                                                                    marquet.Applicant_name = retextplus(marquet.Applicant_name);
                                                                    marquet.Date_expiration = retextplus(marquet.Date_expiration);
                                                                    marquet.Date_depot = retextplus(marquet.Date_depot);
                                                                    marquet.Pays = retextplus(marquet.Pays);
                                                                    marquet.Type = retextplus(marquet.Type);
                                                                    marquet.Loi = retextplus(marquet.Loi);
                                                                    marquet.Statut = retextplus(marquet.Statut);
                                                                    marquet.Applicant_address = retextplus(marquet.Applicant_address);
                                                                    marquet.Representative_name = retextplus(marquet.Representative_name);





                                                                    marrque.MappingId = link.Replace("http://search.ompic.ma/web/pages/consulterMarque.do?id=", "");
                                                                    List<string> liste = new List<string>();
                                                                    command.Parameters.Clear();
                                                                    if (marrque.Nommarque.Trim() != marquet.Nom_marque.Trim())
                                                                    {
                                                                        liste.Add("Nommarque = @Nommarque");
                                                                        DateTime dateTime = DateTime.Now;
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        command.Parameters.AddWithValue("@Nommarque", marrque.Nommarque.Trim());
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        sQLiteCommand.Parameters.AddWithValue("@1", marrque.NumeroTitre);
                                                                        sQLiteCommand.Parameters.AddWithValue("@2", "Nommarque");
                                                                        sQLiteCommand.Parameters.AddWithValue("@3", marquet.Nom_marque.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@4", marrque.Nommarque.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                        con.Open();

                                                                        sQLiteCommand.ExecuteNonQuery();
                                                                        con.Close();

                                                                    }
                                                                    if (marrque.Titulaire.Trim() != marquet.Applicant_name.Trim())
                                                                    {
                                                                        liste.Add("Deposant = @Deposant");
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        DateTime dateTime = DateTime.Now;
                                                                        command.Parameters.AddWithValue("@Deposant", marrque.Titulaire.Trim());
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                        sQLiteCommand.Parameters.AddWithValue("@2", "Deposant");
                                                                        sQLiteCommand.Parameters.AddWithValue("@3", marquet.Applicant_name.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@4", marrque.Titulaire.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                        con.Open();
                                                                        sQLiteCommand.ExecuteNonQuery();
                                                                        con.Close();
                                                                    }
                                                                    if (marrque.Pays.Trim() != marquet.Pays.Trim())
                                                                    {
                                                                        liste.Add("Pays = @Pays");
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        DateTime dateTime = DateTime.Now;
                                                                        command.Parameters.AddWithValue("@Pays", marrque.Pays.Trim());
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                        sQLiteCommand.Parameters.AddWithValue("@2", "Pays");
                                                                        sQLiteCommand.Parameters.AddWithValue("@3", marquet.Pays.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@4", marrque.Pays.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                        con.Open();
                                                                        sQLiteCommand.ExecuteNonQuery();
                                                                        con.Close();
                                                                    }
                                                                    if (marrque.DateDepot.Trim() != marquet.Date_depot.Trim())
                                                                    {
                                                                        liste.Add("DateDepot = @DateDepot");
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        DateTime dateTime = DateTime.Now;
                                                                        command.Parameters.AddWithValue("@DateDepot", marrque.DateDepot.Trim());
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                        sQLiteCommand.Parameters.AddWithValue("@2", "DateDepot");
                                                                        sQLiteCommand.Parameters.AddWithValue("@3", marquet.Date_depot.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@4", marrque.DateDepot.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                        marquet.Date_depot = marrque.DateDepot.Trim();
                                                                        con.Open();
                                                                        sQLiteCommand.ExecuteNonQuery();
                                                                        con.Close();
                                                                    }
                                                                    if (marrque.DateExpiration.Trim() != marquet.Date_expiration.Trim())
                                                                    {
                                                                        liste.Add("DateDepot = @DateDepot");
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        DateTime dateTime = DateTime.Now;
                                                                        command.Parameters.AddWithValue("@DateDepot", marrque.DateExpiration.Trim());
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        sQLiteCommand.Parameters.AddWithValue("@1", marquet.Date_expiration);
                                                                        sQLiteCommand.Parameters.AddWithValue("@2", "DateExpiration");
                                                                        sQLiteCommand.Parameters.AddWithValue("@3", marquet.Date_expiration.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@4", marrque.DateExpiration.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                        marquet.Date_expiration = marrque.DateExpiration.Trim();
                                                                        con.Open();
                                                                        sQLiteCommand.ExecuteNonQuery();
                                                                        con.Close();
                                                                    }
                                                                    if (marrque.Type.Trim() != marquet.Type.Trim())
                                                                    {
                                                                        liste.Add("Type = @Type");
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        DateTime dateTime = DateTime.Now;
                                                                        command.Parameters.AddWithValue("@DateDepot", marrque.Type.Trim());
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                        sQLiteCommand.Parameters.AddWithValue("@2", "Type");
                                                                        sQLiteCommand.Parameters.AddWithValue("@3", marquet.Type.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@4", marrque.Type.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                        marquet.Type = marrque.Type.Trim();
                                                                        con.Open();
                                                                        sQLiteCommand.ExecuteNonQuery();
                                                                        con.Close();
                                                                    }
                                                                    if (marrque.Loi.Trim() != marquet.Loi.Trim())
                                                                    {
                                                                        liste.Add("Loi = @Loi");
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        DateTime dateTime = DateTime.Now;
                                                                        command.Parameters.AddWithValue("@Loi", marrque.Loi.Trim());
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                        sQLiteCommand.Parameters.AddWithValue("@2", "Loi");
                                                                        sQLiteCommand.Parameters.AddWithValue("@3", marquet.Loi.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@4", marrque.Loi.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                        marquet.Loi = marrque.Loi.Trim();
                                                                        con.Open();
                                                                        sQLiteCommand.ExecuteNonQuery();
                                                                        con.Close();
                                                                    }
                                                                    if (marrque.Etat.Trim() != marquet.Statut.Trim())
                                                                    {
                                                                        liste.Add("Statut = @Statut");
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        DateTime dateTime = DateTime.Now;
                                                                        command.Parameters.AddWithValue("@Statut", marrque.Etat.Trim());
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                        sQLiteCommand.Parameters.AddWithValue("@2", "Statut");
                                                                        sQLiteCommand.Parameters.AddWithValue("@3", marquet.Statut.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@4", marrque.Etat.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                        marquet.Statut = marrque.Etat.Trim();
                                                                        con.Open();
                                                                        sQLiteCommand.ExecuteNonQuery();
                                                                        con.Close();
                                                                    }
                                                                    if (marrque.Adresse.Trim() != marquet.Applicant_address.Trim())
                                                                    {
                                                                        liste.Add("Adresse = @Adresse");
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        DateTime dateTime = DateTime.Now;
                                                                        command.Parameters.AddWithValue("@Adresse", marrque.Adresse.Trim());
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                        sQLiteCommand.Parameters.AddWithValue("@2", "Adresse");
                                                                        sQLiteCommand.Parameters.AddWithValue("@3", marquet.Applicant_address.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@4", marrque.Adresse.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                        marquet.Numero_publication = marrque.NumeroPub.Trim();
                                                                        con.Open();
                                                                        sQLiteCommand.ExecuteNonQuery();
                                                                        con.Close();
                                                                    }
                                                                    if (marrque.Mandataire.Trim() != marquet.Representative_name.Trim())
                                                                    {
                                                                        liste.Add("Mandataire = @Mondataire");
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        DateTime dateTime = DateTime.Now;
                                                                        command.Parameters.AddWithValue("@Mondataire", marrque.Mandataire.Trim());
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                        sQLiteCommand.Parameters.AddWithValue("@2", "Mondataire");
                                                                        sQLiteCommand.Parameters.AddWithValue("@3", marquet.Representative_name.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@4", marrque.Mandataire.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                        marquet.Representative_name = marrque.Mandataire.Trim();
                                                                        con.Open();
                                                                        sQLiteCommand.ExecuteNonQuery();
                                                                        con.Close();
                                                                    }

                                                                    if (marrque.MappingId.Trim() != "")
                                                                    {
                                                                        liste.Add("MappingId = @MappingId");
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        DateTime dateTime = DateTime.Now;

                                                                        command.Parameters.AddWithValue("@MappingId", marrque.MappingId.Trim());
                                                                        sQLiteCommand.Parameters.Clear();
                                                                        sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                        sQLiteCommand.Parameters.AddWithValue("@2", "MappingId");
                                                                        sQLiteCommand.Parameters.AddWithValue("@3", "");
                                                                        sQLiteCommand.Parameters.AddWithValue("@4", marrque.MappingId.Trim());
                                                                        sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                        //marquet.MappingId = marrque.MappingId.Trim();
                                                                        con.Open();
                                                                        sQLiteCommand.ExecuteNonQuery();

                                                                    }


                                                                    if (liste.Count > 0)
                                                                    {
                                                                        var Ompicquery = "Update  Marques_Ompic   SET  ";
                                                                        for (int ix = 0; ix < liste.Count; ix++)
                                                                        {

                                                                            if ((liste.Count - 1) == ix)
                                                                            {

                                                                                Ompicquery += liste[ix];
                                                                            }
                                                                            else
                                                                            {
                                                                                Ompicquery += liste[ix] + " , ";
                                                                            }
                                                                        }

                                                                        Ompicquery += " where NumeroTitre ='" + marquet.Numero_titre + "'";
                                                                        command.CommandText = Ompicquery;


                                                                        //try
                                                                        //{
                                                                        con.Open();

                                                                        command.ExecuteNonQuery();

                                                                        con.Close();
                                                                        //}
                                                                        //catch (Exception ex)
                                                                        //{

                                                                        //    }
                                                                        //}
                                                                        //sQLiteCommand.Parameters.Clear();
                                                                        //sQLiteCommand.Parameters.AddWithValue("@1", marrque.Nommarque);
                                                                        //sQLiteCommand.Parameters.AddWithValue("@2", marrque.NumeroTitre);
                                                                        //sQLiteCommand.Parameters.AddWithValue("@3", marrque.DateDepot);
                                                                        //sQLiteCommand.Parameters.AddWithValue("@4", marrque.Type);
                                                                        //sQLiteCommand.Parameters.AddWithValue("@5", marrque.Loi);
                                                                        //sQLiteCommand.Parameters.AddWithValue("@6", marrque.Etat);
                                                                        //sQLiteCommand.Parameters.AddWithValue("@7", marrque.NumeroPub);
                                                                        //sQLiteCommand.Parameters.AddWithValue("@8", marrque.Titulaire);
                                                                        //sQLiteCommand.Parameters.AddWithValue("@9", marrque.Mandataire);
                                                                        //sQLiteCommand.Parameters.AddWithValue("@10", marrque.DateExpiration);
                                                                        //sQLiteCommand.Parameters.AddWithValue("@11", marrque.Classe_nice);
                                                                        //sQLiteCommand.Parameters.AddWithValue("@12", marrque.Classe_details);
                                                                        //sQLiteCommand.Parameters.AddWithValue("@13", marrque.MappingId);
                                                                        //sQLiteCommand.Parameters.AddWithValue("@14", marrque.Adresse);
                                                                        //sQLiteCommand.Parameters.AddWithValue("@15", marrque.Pays);


                                                                    }







                                                                }

                                                            }
                                                            else
                                                            {
                                                                Driver.Navigate().GoToUrl($"https://www.directinfo.ma/");
                                                            }

                                                            //Driver.Close();
                                                        }

                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                continue;
                                            }

                                        }
                                    }
                                }
                                //    }
                                //}
                            }
                        }

                        else
                        {


                            command.Parameters.Clear();

                            command.CommandText = "";
                            System.Threading.Thread.Sleep(1000);
                            string item = marquet.Numero_titre;
                            Driver.Navigate().GoToUrl($"http://search.ompic.ma/web/pages/consulterMarque.do?id={marquet.MappingId}");
                            con.Close();

                            if (Driver.Title != "Page error")
                            {
                                if (Driver.Title != "ERREUR : l'URL demandée n'a pas pu être chargée")
                                {

                                    if (Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[2]/td[2]")).Text != "")
                                    {
                                        //if (Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[2]/td[2]")).Text != "")
                                        //{

                                        Marque marrque = new Marque();
                                        string[] tab;
                                        List<string> list_nice = new List<string>();
                                        string temp = "";
                                        marrque.Nommarque = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[1]/td[2]")).Text;
                                        marrque.NumeroTitre = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[2]/td[2]")).Text;
                                        marrque.DateDepot = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[3]/td[2]")).Text;
                                        marrque.Type = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[4]/td[2]")).Text;
                                        marrque.Loi = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[5]/td[2]")).Text;
                                        marrque.Etat = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[6]/td[2]")).Text;
                                        marrque.NumeroPub = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[7]/td[2]")).Text;
                                        temp = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text;
                                        tab = temp != null ? temp.Split(',') : null;
                                        if (tab != null)
                                        {
                                            marrque.Titulaire = tab.Length > 0 ? tab[0] : "";
                                            marrque.Adresse = temp.Replace(tab[0], "").Replace(tab.Last(), "").Replace(",", "");
                                            marrque.Pays = tab.Last();
                                        }
                                        marrque.Mandataire = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[9]/td[2]")).Text;
                                        marrque.DateExpiration = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[11]/td[2]")).Text;
                                        marrque.Classe_details = "";
                                        for (int p = 1; p <= Driver.FindElements(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr")).Count; p++)
                                        {
                                            marrque.Classe_details += Driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{p}]/td[1]")).Text.Trim() + " : " + Driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{p}]/td[2]")).Text.Trim() + Environment.NewLine;
                                            temp = Driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{p}]/td[1]")).Text.Trim();
                                            if (list_nice.Contains(temp))
                                            {

                                            }
                                            else
                                            {
                                                list_nice.Add(temp);
                                            }
                                        }
                                        marrque.Classe_nice = "";
                                        foreach (var nice in list_nice)
                                        {
                                            marrque.Classe_nice += nice + ",";
                                        }

                                        marquet.Nom_marque = retextplus(marquet.Nom_marque);
                                        marquet.Applicant_name = retextplus(marquet.Applicant_name);
                                        marquet.Date_expiration = retextplus(marquet.Date_expiration);
                                        marquet.Date_depot = retextplus(marquet.Date_depot);
                                        marquet.Pays = retextplus(marquet.Pays);
                                        marquet.Type = retextplus(marquet.Type);
                                        marquet.Loi = retextplus(marquet.Loi);
                                        marquet.Statut = retextplus(marquet.Statut);
                                        marquet.Applicant_address = retextplus(marquet.Applicant_address);
                                        marquet.Representative_name = retextplus(marquet.Representative_name);





                                        //marrque.MappingId = link.Replace("http://search.ompic.ma/web/pages/consulterMarque.do?id=", "");
                                        List<string> liste = new List<string>();
                                        command.Parameters.Clear();
                                        if (marrque.Nommarque.Trim() != marquet.Nom_marque.Trim())
                                        {
                                            liste.Add("Nommarque = @Nommarque");
                                            DateTime dateTime = DateTime.Now;
                                            sQLiteCommand.Parameters.Clear();
                                            command.Parameters.AddWithValue("@Nommarque", marrque.Nommarque.Trim());
                                            sQLiteCommand.Parameters.Clear();
                                            sQLiteCommand.Parameters.AddWithValue("@1", marrque.NumeroTitre);
                                            sQLiteCommand.Parameters.AddWithValue("@2", "Nommarque");
                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Nom_marque.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Nommarque.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                            con.Open();

                                            sQLiteCommand.ExecuteNonQuery();
                                            con.Close();

                                        }
                                        if (marrque.Titulaire.Trim() != marquet.Applicant_name.Trim())
                                        {
                                            liste.Add("Deposant = @Deposant");
                                            sQLiteCommand.Parameters.Clear();
                                            DateTime dateTime = DateTime.Now;
                                            command.Parameters.AddWithValue("@Deposant", marrque.Titulaire.Trim());
                                            sQLiteCommand.Parameters.Clear();
                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                            sQLiteCommand.Parameters.AddWithValue("@2", "Deposant");
                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Applicant_name.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Titulaire.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                            con.Open();
                                            sQLiteCommand.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        if (marrque.Pays.Trim() != marquet.Pays.Trim())
                                        {
                                            liste.Add("Pays = @Pays");
                                            sQLiteCommand.Parameters.Clear();
                                            DateTime dateTime = DateTime.Now;
                                            command.Parameters.AddWithValue("@Pays", marrque.Pays.Trim());
                                            sQLiteCommand.Parameters.Clear();
                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                            sQLiteCommand.Parameters.AddWithValue("@2", "Pays");
                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Pays.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Pays.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                            con.Open();
                                            sQLiteCommand.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        if (marrque.DateDepot.Trim() != marquet.Date_depot.Trim())
                                        {
                                            liste.Add("DateDepot = @DateDepot");
                                            sQLiteCommand.Parameters.Clear();
                                            DateTime dateTime = DateTime.Now;
                                            command.Parameters.AddWithValue("@DateDepot", marrque.DateDepot.Trim());
                                            sQLiteCommand.Parameters.Clear();
                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                            sQLiteCommand.Parameters.AddWithValue("@2", "DateDepot");
                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Date_depot.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.DateDepot.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                            marquet.Date_depot = marrque.DateDepot.Trim();
                                            con.Open();
                                            sQLiteCommand.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        if (marrque.DateExpiration.Trim() != marquet.Date_expiration.Trim())
                                        {
                                            liste.Add("DateExpiration = @DateExpiration");
                                            sQLiteCommand.Parameters.Clear();
                                            DateTime dateTime = DateTime.Now;
                                            command.Parameters.AddWithValue("@DateExpiration", marrque.DateExpiration.Trim());
                                            sQLiteCommand.Parameters.Clear();
                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Date_expiration);
                                            sQLiteCommand.Parameters.AddWithValue("@2", "DateExpiration");
                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Date_expiration.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.DateExpiration.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                            marquet.Date_expiration = marrque.DateExpiration.Trim();
                                            con.Open();
                                            sQLiteCommand.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        if (marrque.Type.Trim() != marquet.Type.Trim())
                                        {
                                            liste.Add("Type = @Type");
                                            sQLiteCommand.Parameters.Clear();
                                            DateTime dateTime = DateTime.Now;
                                            command.Parameters.AddWithValue("@Type", marrque.Type.Trim());
                                            sQLiteCommand.Parameters.Clear();
                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                            sQLiteCommand.Parameters.AddWithValue("@2", "Type");
                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Type.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Type.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                            marquet.Type = marrque.Type.Trim();
                                            con.Open();
                                            sQLiteCommand.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        if (marrque.Loi.Trim() != marquet.Loi.Trim())
                                        {
                                            liste.Add("Loi = @Loi");
                                            sQLiteCommand.Parameters.Clear();
                                            DateTime dateTime = DateTime.Now;
                                            command.Parameters.AddWithValue("@Loi", marrque.Loi.Trim());
                                            sQLiteCommand.Parameters.Clear();
                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                            sQLiteCommand.Parameters.AddWithValue("@2", "Loi");
                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Loi.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Loi.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                            marquet.Loi = marrque.Loi.Trim();
                                            con.Open();
                                            sQLiteCommand.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        if (marrque.Etat.Trim() != marquet.Statut.Trim())
                                        {
                                            liste.Add("Statut = @Statut");
                                            sQLiteCommand.Parameters.Clear();
                                            DateTime dateTime = DateTime.Now;
                                            command.Parameters.AddWithValue("@Statut", marrque.Etat.Trim());
                                            sQLiteCommand.Parameters.Clear();
                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                            sQLiteCommand.Parameters.AddWithValue("@2", "Statut");
                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Statut.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Etat.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                            marquet.Statut = marrque.Etat.Trim();
                                            con.Open();
                                            sQLiteCommand.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        if (marrque.Adresse.Trim() != marquet.Applicant_address.Trim())
                                        {
                                            liste.Add("Adresse = @Adresse");
                                            sQLiteCommand.Parameters.Clear();
                                            DateTime dateTime = DateTime.Now;
                                            command.Parameters.AddWithValue("@Adresse", marrque.Adresse.Trim());
                                            sQLiteCommand.Parameters.Clear();
                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                            sQLiteCommand.Parameters.AddWithValue("@2", "Adresse");
                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Applicant_address.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Adresse.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                            marquet.Numero_publication = marrque.NumeroPub.Trim();
                                            con.Open();
                                            sQLiteCommand.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        if (marrque.Mandataire.Trim() != marquet.Representative_name.Trim())
                                        {
                                            liste.Add("Mandataire = @Mondataire");
                                            sQLiteCommand.Parameters.Clear();
                                            DateTime dateTime = DateTime.Now;
                                            command.Parameters.AddWithValue("@Mondataire", marrque.Mandataire.Trim());
                                            sQLiteCommand.Parameters.Clear();
                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                            sQLiteCommand.Parameters.AddWithValue("@2", "Mondataire");
                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Representative_name.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Mandataire.Trim());
                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                            marquet.Representative_name = marrque.Mandataire.Trim();
                                            con.Open();
                                            sQLiteCommand.ExecuteNonQuery();
                                            con.Close();
                                        }

                                        var Ompicquery = "Update  Marques_Ompic SET   ";

                                        if (liste.Count > 0)
                                        {

                                            for (int ix = 0; ix < liste.Count; ix++)
                                            {

                                                if ((liste.Count - 1) == ix)
                                                {

                                                    Ompicquery += liste[ix];
                                                }
                                                else
                                                {
                                                    Ompicquery += liste[ix] + " , ";
                                                }
                                            }

                                            Ompicquery += " where NumeroTitre ='" + marquet.Numero_titre + "'";
                                            command.CommandText = Ompicquery;


                                            //try
                                            //{
                                            //Response.Write(marrque.Adresse);
                                            //Response.Write(marquet.Applicant_address);
                                            con.Open();

                                            command.ExecuteNonQuery();

                                            con.Close();
                                            //}
                                            //catch (Exception ex)
                                            //{

                                            //}

                                        }



                                        //sQLiteCommand.Parameters.Clear();
                                        //sQLiteCommand.Parameters.AddWithValue("@1", marrque.Nommarque);
                                        //sQLiteCommand.Parameters.AddWithValue("@2", marrque.NumeroTitre);
                                        //sQLiteCommand.Parameters.AddWithValue("@3", marrque.DateDepot);
                                        //sQLiteCommand.Parameters.AddWithValue("@4", marrque.Type);
                                        //sQLiteCommand.Parameters.AddWithValue("@5", marrque.Loi);
                                        //sQLiteCommand.Parameters.AddWithValue("@6", marrque.Etat);
                                        //sQLiteCommand.Parameters.AddWithValue("@7", marrque.NumeroPub);
                                        //sQLiteCommand.Parameters.AddWithValue("@8", marrque.Titulaire);
                                        //sQLiteCommand.Parameters.AddWithValue("@9", marrque.Mandataire);
                                        //sQLiteCommand.Parameters.AddWithValue("@10", marrque.DateExpiration);
                                        //sQLiteCommand.Parameters.AddWithValue("@11", marrque.Classe_nice);
                                        //sQLiteCommand.Parameters.AddWithValue("@12", marrque.Classe_details);
                                        //sQLiteCommand.Parameters.AddWithValue("@13", marrque.MappingId);
                                        //sQLiteCommand.Parameters.AddWithValue("@14", marrque.Adresse);
                                        //sQLiteCommand.Parameters.AddWithValue("@15", marrque.Pays);





                                    }
                                    else
                                    {

                                        command.Parameters.Clear();
                                        System.Threading.Thread.Sleep(1000);
                                        string itemm = marquet.Numero_titre;
                                        Driver.Navigate().GoToUrl($"http://search.ompic.ma/web/pages/consulterMarqueTMView.do?refReglementationTitreLabel=97-{item}");
                                        if (Driver.Title != "Page error")
                                        {
                                            if (Driver.Title != "ERREUR : l'URL demandée n'a pas pu être chargée")
                                            {
                                                if (Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[2]/td[2]")).Text != "")
                                                {
                                                    if (Driver.FindElement(By.XPath("/html/body/div/table[1]/tbody/tr[3]/td/table/tbody/tr[1]/td[2]")).Text.Trim() != "")
                                                    {

                                                        string marquee = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[1]/td[2]")).Text;
                                                        string fulldep = Driver.FindElement(By.XPath("/html/body/div/table[1]/tbody/tr[3]/td/table/tbody/tr[8]/td[2]")).Text;
                                                        List<string> list = fulldep.Split(',').ToList();
                                                        string deposent = list[0].ToString();
                                                        //Console.WriteLine(fulldep);
                                                        //Console.WriteLine(deposent);

                                                        string marque = marquee + " " + deposent.TrimEnd();
                                                        Driver.Navigate().GoToUrl($"https://www.directinfo.ma/");
                                                        //System.Threading.Thread.Sleep(7000);
                                                        OpenQA.Selenium.Support.UI.WebDriverWait wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, TimeSpan.FromSeconds(60));


                                                        // get the "Add Item" element
                                                        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("/html/body/app-root/app-home/app-header-hp/header/div[2]/div/form/input[1]")));
                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-home/app-header-hp/header/div[2]/div/form/input[1]")).SendKeys(marque);
                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-home/app-header-hp/header/div[2]/div/form/input[2]")).Click();
                                                        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[1]/ul/li[2]")));
                                                        string g = Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[1]/ul/li[2]/span/div/span")).Text;
                                                        //Console.WriteLine(g);
                                                        g = g.Replace("(", "");
                                                        g = g.Replace(")", "");


                                                        int nb = int.Parse(g);
                                                        if (nb != 0)
                                                        {
                                                            int type = 0;
                                                            Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[1]/ul/li[2]")).Click();
                                                            for (int i = 1; i < nb + 1; i++)
                                                            {
                                                                int j = i;

                                                                if (i > 10)
                                                                {
                                                                    if (type == 0)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[2]")).Click();
                                                                        type = 2;
                                                                    }

                                                                    j = i - 10;

                                                                }
                                                                if (i > 20)
                                                                {
                                                                    if (type == 2)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[3]")).Click();
                                                                        type = 3;
                                                                    }

                                                                    j = i - 20;
                                                                }
                                                                if (i > 30)
                                                                {
                                                                    if (type == 3)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[4]")).Click();
                                                                        type = 4;
                                                                    }

                                                                    j = i - 30;
                                                                }
                                                                if (i > 40)
                                                                {
                                                                    if (type == 4)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[5]")).Click();
                                                                        type = 5;
                                                                    }

                                                                    j = i - 40;
                                                                }
                                                                if (i > 50)
                                                                {
                                                                    if (type == 5)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[6]")).Click();
                                                                        type = 6;
                                                                    }

                                                                    j = i - 50;

                                                                }
                                                                if (i > 60)
                                                                {
                                                                    if (type == 6)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[7]")).Click();
                                                                        type = 7;
                                                                    }


                                                                    j = i - 60;
                                                                }
                                                                if (i > 70)
                                                                {
                                                                    if (type == 7)
                                                                    {

                                                                        if (nb > 100)
                                                                        {
                                                                            Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                        }
                                                                        else
                                                                        {
                                                                            Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[8]")).Click();
                                                                        }

                                                                        type = 8;
                                                                    }

                                                                    j = i - 70;
                                                                }
                                                                if (i > 80)
                                                                {
                                                                    if (type == 8)
                                                                    {
                                                                        if (nb > 100)
                                                                        {
                                                                            Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                        }
                                                                        else
                                                                        {
                                                                            Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[9]")).Click();
                                                                        }
                                                                        type = 9;
                                                                    }
                                                                    j = i - 80;
                                                                }
                                                                if (i > 90)
                                                                {
                                                                    if (type == 9)
                                                                    {
                                                                        if (nb > 100)
                                                                        {
                                                                            Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                        }
                                                                        else
                                                                        {
                                                                            Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[10]")).Click();
                                                                        }
                                                                        type = 10;
                                                                    }

                                                                    j = i - 90;
                                                                }
                                                                if (i > 100)
                                                                {
                                                                    if (type == 10)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                        type = 11;
                                                                    }

                                                                    j = i - 100;
                                                                }
                                                                if (i > 110)
                                                                {
                                                                    if (type == 11)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                        type = 12;
                                                                    }

                                                                    j = i - 110;

                                                                }
                                                                if (i > 120)
                                                                {
                                                                    if (type == 12)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                        type = 13;
                                                                    }

                                                                    j = i - 120;
                                                                }
                                                                if (i > 130)
                                                                {
                                                                    if (type == 13)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                        type = 14;
                                                                    }

                                                                    j = i - 140;
                                                                }
                                                                if (i > 140)
                                                                {
                                                                    if (type == 14)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                        type = 15;
                                                                    }

                                                                    j = i - 140;
                                                                }
                                                                if (i > 150)
                                                                {
                                                                    if (type == 15)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                        type = 16;
                                                                    }

                                                                    j = i - 150;

                                                                }
                                                                if (i > 160)
                                                                {
                                                                    if (type == 16)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                        type = 17;
                                                                    }

                                                                    j = i - 160;
                                                                }
                                                                if (i > 170)
                                                                {
                                                                    if (type == 17)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                        type = 18;
                                                                    }

                                                                    j = i - 170;
                                                                }
                                                                if (i > 180)
                                                                {
                                                                    if (type == 18)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                        type = 19;
                                                                    }

                                                                    j = i - 180;
                                                                }
                                                                if (i > 190)
                                                                {
                                                                    if (type == 19)
                                                                    {
                                                                        Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                        type = 20;
                                                                    }

                                                                    j = i - 190;
                                                                }
                                                                if (i > 200)
                                                                {
                                                                    //if (type == 10)
                                                                    //{
                                                                    //    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                    //}
                                                                    //type = 11;
                                                                    //j = i - 100;
                                                                    continue;
                                                                }
                                                                //if (i == nb)
                                                                //{
                                                                //    //if (type == 10)
                                                                //    //{
                                                                //    //    Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/div/ul/li[12]")).Click();
                                                                //    //}
                                                                //    //type = 11;
                                                                //    //j = i - 100;
                                                                //    break;
                                                                //}


                                                                //Console.WriteLine(marque + "==" + Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page[1]/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/ul/li[" + i.ToString() + "]/h2")).Text);
                                                                //Console.WriteLine(item + "==" + Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page[1]/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/ul/li[" + i.ToString() + "]/div/p[1]")).Text);
                                                                try
                                                                {
                                                                    string bg = Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page[1]/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/ul/li[" + j.ToString() + "]/div/p[1]")).Text;
                                                                    bg = bg.Replace("Numéro Titre : ", "");
                                                                    if (marquet.Numero_titre == bg.Trim())
                                                                    {

                                                                        //Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page[1]/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/ul/li["+i.ToString()+"]/a")).Click();
                                                                        string link = Driver.FindElement(By.XPath("/html/body/app-root/app-resultat-search-mot-cle-page[1]/app-resultat-search-mot-cle/section/div/div[1]/div[1]/div[2]/div[2]/ul/li[" + j.ToString() + "]/a")).GetAttribute("href");
                                                                        Driver.Navigate().GoToUrl(link);


                                                                        //string bfbf = Console.ReadLine();
                                                                        if (Driver.Title != "Page error")
                                                                        {
                                                                            if (Driver.Title != "ERREUR : l'URL demandée n'a pas pu être chargée")
                                                                            {
                                                                                if (Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[2]/td[2]")).Text != "")
                                                                                {
                                                                                    if (Driver.FindElement(By.XPath("/html/body/div/table[1]/tbody/tr[3]/td/table/tbody/tr[1]/td[2]")).Text.Trim() != "")
                                                                                    {
                                                                                        command.Parameters.Clear();
                                                                                        con.Close();
                                                                                        Marque marrque = new Marque();
                                                                                        string[] tab;
                                                                                        List<string> list_nice = new List<string>();
                                                                                        string temp = "";
                                                                                        marrque.Nommarque = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[1]/td[2]")).Text;
                                                                                        marrque.NumeroTitre = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[2]/td[2]")).Text;
                                                                                        marrque.DateDepot = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[3]/td[2]")).Text;
                                                                                        marrque.Type = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[4]/td[2]")).Text;
                                                                                        marrque.Loi = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[5]/td[2]")).Text;
                                                                                        marrque.Etat = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[6]/td[2]")).Text;
                                                                                        marrque.NumeroPub = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[7]/td[2]")).Text;
                                                                                        temp = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text;
                                                                                        tab = temp != null ? temp.Split(',') : null;
                                                                                        if (tab != null)
                                                                                        {
                                                                                            marrque.Titulaire = tab.Length > 0 ? tab[0] : "";
                                                                                            marrque.Adresse = temp.Replace(tab[0], "").Replace(tab.Last(), "").Replace(",", "");
                                                                                            marrque.Pays = tab.Last();
                                                                                        }
                                                                                        marrque.Mandataire = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[9]/td[2]")).Text;
                                                                                        marrque.DateExpiration = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[11]/td[2]")).Text;
                                                                                        marrque.Classe_details = "";
                                                                                        for (int p = 1; p <= Driver.FindElements(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr")).Count; p++)
                                                                                        {
                                                                                            marrque.Classe_details += Driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{p}]/td[1]")).Text.Trim() + " : " + Driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{p}]/td[2]")).Text.Trim() + Environment.NewLine;
                                                                                            temp = Driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{p}]/td[1]")).Text.Trim();
                                                                                            if (list_nice.Contains(temp))
                                                                                            {

                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                list_nice.Add(temp);
                                                                                            }
                                                                                        }
                                                                                        marrque.Classe_nice = "";
                                                                                        foreach (var nice in list_nice)
                                                                                        {
                                                                                            marrque.Classe_nice += nice + ",";
                                                                                        }

                                                                                        marquet.Nom_marque = retextplus(marquet.Nom_marque);
                                                                                        marquet.Applicant_name = retextplus(marquet.Applicant_name);
                                                                                        marquet.Date_expiration = retextplus(marquet.Date_expiration);
                                                                                        marquet.Date_depot = retextplus(marquet.Date_depot);
                                                                                        marquet.Pays = retextplus(marquet.Pays);
                                                                                        marquet.Type = retextplus(marquet.Type);
                                                                                        marquet.Loi = retextplus(marquet.Loi);
                                                                                        marquet.Statut = retextplus(marquet.Statut);
                                                                                        marquet.Applicant_address = retextplus(marquet.Applicant_address);
                                                                                        marquet.Representative_name = retextplus(marquet.Representative_name);





                                                                                        marrque.MappingId = link.Replace("http://search.ompic.ma/web/pages/consulterMarque.do?id=", "");
                                                                                        List<string> liste = new List<string>();
                                                                                        command.Parameters.Clear();
                                                                                        if (marrque.Nommarque.Trim() != marquet.Nom_marque.Trim())
                                                                                        {
                                                                                            liste.Add("Nommarque = @Nommarque");
                                                                                            DateTime dateTime = DateTime.Now;
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            command.Parameters.AddWithValue("@Nommarque", marrque.Nommarque.Trim());
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            sQLiteCommand.Parameters.AddWithValue("@1", marrque.NumeroTitre);
                                                                                            sQLiteCommand.Parameters.AddWithValue("@2", "Nommarque");
                                                                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Nom_marque.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Nommarque.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                                            con.Open();

                                                                                            sQLiteCommand.ExecuteNonQuery();
                                                                                            con.Close();

                                                                                        }
                                                                                        if (marrque.Titulaire.Trim() != marquet.Applicant_name.Trim())
                                                                                        {
                                                                                            liste.Add("Deposant = @Deposant");
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            DateTime dateTime = DateTime.Now;
                                                                                            command.Parameters.AddWithValue("@Deposant", marrque.Titulaire.Trim());
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                                            sQLiteCommand.Parameters.AddWithValue("@2", "Deposant");
                                                                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Applicant_name.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Titulaire.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                                            con.Open();
                                                                                            sQLiteCommand.ExecuteNonQuery();
                                                                                            con.Close();
                                                                                        }
                                                                                        if (marrque.Pays.Trim() != marquet.Pays.Trim())
                                                                                        {
                                                                                            liste.Add("Pays = @Pays");
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            DateTime dateTime = DateTime.Now;
                                                                                            command.Parameters.AddWithValue("@Pays", marrque.Pays.Trim());
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                                            sQLiteCommand.Parameters.AddWithValue("@2", "Pays");
                                                                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Pays.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Pays.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                                            con.Open();
                                                                                            sQLiteCommand.ExecuteNonQuery();
                                                                                            con.Close();
                                                                                        }
                                                                                        if (marrque.DateDepot.Trim() != marquet.Date_depot.Trim())
                                                                                        {
                                                                                            liste.Add("DateDepot = @DateDepot");
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            DateTime dateTime = DateTime.Now;
                                                                                            command.Parameters.AddWithValue("@DateDepot", marrque.DateDepot.Trim());
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                                            sQLiteCommand.Parameters.AddWithValue("@2", "DateDepot");
                                                                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Date_depot.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.DateDepot.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                                            marquet.Date_depot = marrque.DateDepot.Trim();
                                                                                            con.Open();
                                                                                            sQLiteCommand.ExecuteNonQuery();
                                                                                            con.Close();
                                                                                        }
                                                                                        if (marrque.DateExpiration.Trim() != marquet.Date_expiration.Trim())
                                                                                        {
                                                                                            liste.Add("DateDepot = @DateDepot");
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            DateTime dateTime = DateTime.Now;
                                                                                            command.Parameters.AddWithValue("@DateDepot", marrque.DateExpiration.Trim());
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Date_expiration);
                                                                                            sQLiteCommand.Parameters.AddWithValue("@2", "DateExpiration");
                                                                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Date_expiration.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.DateExpiration.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                                            marquet.Date_expiration = marrque.DateExpiration.Trim();
                                                                                            con.Open();
                                                                                            sQLiteCommand.ExecuteNonQuery();
                                                                                            con.Close();
                                                                                        }
                                                                                        if (marrque.Type.Trim() != marquet.Type.Trim())
                                                                                        {
                                                                                            liste.Add("Type = @Type");
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            DateTime dateTime = DateTime.Now;
                                                                                            command.Parameters.AddWithValue("@DateDepot", marrque.Type.Trim());
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                                            sQLiteCommand.Parameters.AddWithValue("@2", "Type");
                                                                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Type.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Type.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                                            marquet.Type = marrque.Type.Trim();
                                                                                            con.Open();
                                                                                            sQLiteCommand.ExecuteNonQuery();
                                                                                            con.Close();
                                                                                        }
                                                                                        if (marrque.Loi.Trim() != marquet.Loi.Trim())
                                                                                        {
                                                                                            liste.Add("Loi = @Loi");
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            DateTime dateTime = DateTime.Now;
                                                                                            command.Parameters.AddWithValue("@Loi", marrque.Loi.Trim());
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                                            sQLiteCommand.Parameters.AddWithValue("@2", "Loi");
                                                                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Loi.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Loi.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                                            marquet.Loi = marrque.Loi.Trim();
                                                                                            con.Open();
                                                                                            sQLiteCommand.ExecuteNonQuery();
                                                                                            con.Close();
                                                                                        }
                                                                                        if (marrque.Etat.Trim() != marquet.Statut.Trim())
                                                                                        {
                                                                                            liste.Add("Statut = @Statut");
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            DateTime dateTime = DateTime.Now;
                                                                                            command.Parameters.AddWithValue("@Statut", marrque.Etat.Trim());
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                                            sQLiteCommand.Parameters.AddWithValue("@2", "Statut");
                                                                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Statut.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Etat.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                                            marquet.Statut = marrque.Etat.Trim();
                                                                                            con.Open();
                                                                                            sQLiteCommand.ExecuteNonQuery();
                                                                                            con.Close();
                                                                                        }
                                                                                        if (marrque.Adresse.Trim() != marquet.Applicant_address.Trim())
                                                                                        {
                                                                                            liste.Add("Adresse = @Adresse");
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            DateTime dateTime = DateTime.Now;
                                                                                            command.Parameters.AddWithValue("@Adresse", marrque.Adresse.Trim());
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                                            sQLiteCommand.Parameters.AddWithValue("@2", "Adresse");
                                                                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Applicant_address.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Adresse.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                                            marquet.Numero_publication = marrque.NumeroPub.Trim();
                                                                                            con.Open();
                                                                                            sQLiteCommand.ExecuteNonQuery();
                                                                                            con.Close();
                                                                                        }
                                                                                        if (marrque.Mandataire.Trim() != marquet.Representative_name.Trim())
                                                                                        {
                                                                                            liste.Add("Mandataire = @Mondataire");
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            DateTime dateTime = DateTime.Now;
                                                                                            command.Parameters.AddWithValue("@Mondataire", marrque.Mandataire.Trim());
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                                            sQLiteCommand.Parameters.AddWithValue("@2", "Mondataire");
                                                                                            sQLiteCommand.Parameters.AddWithValue("@3", marquet.Representative_name.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.Mandataire.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                                            marquet.Representative_name = marrque.Mandataire.Trim();
                                                                                            con.Open();
                                                                                            sQLiteCommand.ExecuteNonQuery();
                                                                                            con.Close();
                                                                                        }

                                                                                        if (marrque.MappingId.Trim() != "")
                                                                                        {
                                                                                            liste.Add("MappingId = @MappingId");
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            DateTime dateTime = DateTime.Now;

                                                                                            command.Parameters.AddWithValue("@MappingId", marrque.MappingId.Trim());
                                                                                            sQLiteCommand.Parameters.Clear();
                                                                                            sQLiteCommand.Parameters.AddWithValue("@1", marquet.Numero_titre);
                                                                                            sQLiteCommand.Parameters.AddWithValue("@2", "MappingId");
                                                                                            sQLiteCommand.Parameters.AddWithValue("@3", "");
                                                                                            sQLiteCommand.Parameters.AddWithValue("@4", marrque.MappingId.Trim());
                                                                                            sQLiteCommand.Parameters.AddWithValue("@5", dateTime);
                                                                                            //marquet.MappingId = marrque.MappingId.Trim();
                                                                                            con.Open();
                                                                                            sQLiteCommand.ExecuteNonQuery();

                                                                                        }


                                                                                        if (liste.Count > 0)
                                                                                        {
                                                                                            var Ompicquery = "Update  Marques_Ompic   ";
                                                                                            for (int ix = 0; ix < liste.Count; ix++)
                                                                                            {
                                                                                                if (ix == 0)
                                                                                                {
                                                                                                    Ompicquery += "SET " + liste[ix];
                                                                                                }
                                                                                                else if ((liste.Count - 1) == ix)
                                                                                                {

                                                                                                    Ompicquery += liste[ix];
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    Ompicquery += liste[ix] + " , ";
                                                                                                }
                                                                                            }

                                                                                            Ompicquery += " where NumeroTitre ='" + marquet.Numero_titre + "'";
                                                                                            command.CommandText = Ompicquery;


                                                                                            //try
                                                                                            //{
                                                                                            con.Open();

                                                                                            command.ExecuteNonQuery();

                                                                                            con.Close();
                                                                                            //}
                                                                                            //catch (Exception ex)
                                                                                            //{
                                                                                            //Response.Write(Ompicquery);
                                                                                            //    }
                                                                                            //}
                                                                                            //sQLiteCommand.Parameters.Clear();
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@1", marrque.Nommarque);
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@2", marrque.NumeroTitre);
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@3", marrque.DateDepot);
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@4", marrque.Type);
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@5", marrque.Loi);
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@6", marrque.Etat);
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@7", marrque.NumeroPub);
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@8", marrque.Titulaire);
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@9", marrque.Mandataire);
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@10", marrque.DateExpiration);
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@11", marrque.Classe_nice);
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@12", marrque.Classe_details);
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@13", marrque.MappingId);
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@14", marrque.Adresse);
                                                                                            //sQLiteCommand.Parameters.AddWithValue("@15", marrque.Pays);


                                                                                        }






                                                                                    }

                                                                                }
                                                                                else
                                                                                {
                                                                                    Driver.Navigate().GoToUrl($"https://www.directinfo.ma/");
                                                                                }

                                                                                //Driver.Close();
                                                                            }

                                                                        }
                                                                    }
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    continue;
                                                                }

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }





                                }

                            }

                        }




                    }

                    Page.RegisterStartupScript("Key", "<script type='text/javascript'>window.onload = function(){alert('successfully updated');return false;}</script>");

                    Driver.Close();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterStartupScript("Key", "<script type='text/javascript'>window.onload = function(){alert('Update faled');return false;}</script>");

            }

        }
        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
        protected void Alert_gen(object sender, EventArgs e)
        {

            if (Session["Marqueschecked"] == null)
            {
                List<string> marquesidchecked = new List<string>();



                foreach (GridViewRow row in GridView1.Rows)
                {
                    //if (item.checkede == "true")
                    //{
                    //    gh++;
                    //}


                    CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                    if (cb.Checked == true)
                    {
                        if (!marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Add(row.Cells[1].Text);
                        }

                    }
                    else
                    {
                        if (marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Remove(row.Cells[1].Text);
                        }
                    }



                }
                Session["Marqueschecked"] = marquesidchecked;
            }
            else
            {
                List<string> marquesidchecked = Session["Marqueschecked"] as List<string>;


                foreach (GridViewRow row in GridView1.Rows)
                {
                    //if (item.checkede == "true")
                    //{
                    //    gh++;
                    //}


                    CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                    if (cb.Checked == true)
                    {
                        if (!marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Add(row.Cells[1].Text);
                        }

                    }
                    else
                    {
                        if (marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Remove(row.Cells[1].Text);
                        }
                    }



                }
            }

            List<string> marquesidcheckedd = Session["Marqueschecked"] as List<string>;
            if (marquesidcheckedd.Count != 0)
            {
                foreach (string id in marquesidcheckedd)
                {
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (marquesidcheckedd.Contains(row.Cells[1].Text))
                        {


                            ((CheckBox)GridView1.Rows[row.RowIndex].FindControl("CheckBox1")).Checked = true;







                        }
                    }

                }
            }




            List<Marque_TmOmpicModel> marques = Session["BDMarques"] as List<Marque_TmOmpicModel>;
            List<tmopmic> test = new List<tmopmic>();
            //List<Marque_TmOmpicModel> test2 = Session["list_marques_searched"] as List<Marque_TmOmpicModel>;
            //foreach (var item in list_marque)
            //{
            //    test2.Add(new Marque_TmOmpicModel { Numero_titre = item.Numero_titre, Nom_marque = item.Nom_marque, Date_depot = item.Date_depot, Date_expiration = item.Date_expiration, Applicant_name = item.Applicant_name, Representative_name = item.Representative_name, ClasseNice = item.ClasseNice });
            //}
            System.Data.DataSet ds = new System.Data.DataSet("DataTable1");
            DataTable table = new DataTable("DataTable1");

            table.Columns.Add("NumeroTitre");
            table.Columns.Add("image");
            table.Columns.Add("Nommarque");
            table.Columns.Add("Applicant_name");
            table.Columns.Add("Datedepot");
            table.Columns.Add("Dateexpiration");
            table.Columns.Add("NiceClass");
            table.Columns.Add("Adresse");
            table.Columns["image"].DataType = System.Type.GetType("System.Byte[]");

            foreach (Marque_TmOmpicModel marque in marques)
            {
                if (marquesidcheckedd.Contains(marque.Numero_titre))
                {
                    var ms = new MemoryStream();
                    DataRow row = table.NewRow();
                    string imgname = "";

                    row["NumeroTitre"] = marque.Numero_titre;

                    if (!string.IsNullOrWhiteSpace(marque.Numero_titre))
                    {
                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.jpg"))
                        {
                            imgname = $@"{marque.Numero_titre}.jpg";
                        }
                        else
                        {
                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.JPG"))
                            {
                                imgname = $@"{marque.Numero_titre}.JPG";
                            }
                            else
                            {
                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.jpeg"))
                                {
                                    imgname = $@"{marque.Numero_titre}.jpeg";
                                }
                                else
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.png"))
                                    {
                                        imgname = $@"{marque.Numero_titre}.png";
                                    }
                                    //else
                                    //{
                                    //    webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{marque.NumeroTitre}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{marque.NumeroTitre}.jpg");
                                    //    marque.Image = $@"{marque.NumeroTitre}.jpg";
                                    //}
                                }
                            }
                        }
                    }

                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgname))
                    {
                        System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgname).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else
                    {
                        System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\\Empty.png").Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    row["image"] = ms.ToArray();
                    row["Nommarque"] = marque.Nom_marque;
                    row["Datedepot"] = marque.Date_depot;
                    row["Dateexpiration"] = marque.Date_expiration;
                    row["Applicant_name"] = marque.Applicant_name;
                    row["Adresse"] = marque.Applicant_address;
                    row["NiceClass"] = marque.ClasseNice;


                    table.Rows.Add(row);
                }



            }
            foreach (DataRow rt in table.Rows)
            {

                rt[5] = retext(rt[5].ToString());
                //rt[8] = retext(rt[8].ToString());
                rt[4] = retext(rt[4].ToString());
                rt[3] = retext(rt[3].ToString());
                rt[2] = retext(rt[2].ToString());
                rt[6] = retext(rt[6].ToString());
                //rt[7] = retext(rt[7].ToString());

            }
            ds.Tables.Add(table);


            CrystalReport5111 report = new CrystalReport5111();
            report.Database.Tables["DataTable1"].SetDataSource(ds);
            string filename = $"Alert_export.pdf";
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            report.Close();
            report.Dispose();
            MemoryStream memoryS = new MemoryStream();
            stream.CopyTo(memoryS);
            byte[] buffer = memoryS.ToArray();
            HttpCookie httpCookie = Request.Cookies["Userinfo"];
            string name = httpCookie["Username"];
            string type = "Alert";
            storepdf(buffer, name, type);

            Response.AddHeader("Content-Length", buffer.Length.ToString());
            Response.AddHeader("Content-Disposition", $"attachment; filename={filename}");
            Response.OutputStream.Write(buffer, 0, buffer.Length);
            Response.End();

        }

        protected void Update_Click(object sender, ImageClickEventArgs e)
        {
            try
            {


                if (Session["Marqueschecked"] == null)
                {
                    List<string> marquesidchecked = new List<string>();



                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        //if (item.checkede == "true")
                        //{
                        //    gh++;
                        //}


                        CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                        if (cb.Checked == true)
                        {
                            if (!marquesidchecked.Contains(row.Cells[1].Text))
                            {
                                marquesidchecked.Add(row.Cells[1].Text);
                            }

                        }
                        else
                        {
                            if (marquesidchecked.Contains(row.Cells[1].Text))
                            {
                                marquesidchecked.Remove(row.Cells[1].Text);
                            }
                        }



                    }
                    Session["Marqueschecked"] = marquesidchecked;
                }
                else
                {
                    List<string> marquesidchecked = Session["Marqueschecked"] as List<string>;


                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        //if (item.checkede == "true")
                        //{
                        //    gh++;
                        //}


                        CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                        if (cb.Checked == true)
                        {
                            if (!marquesidchecked.Contains(row.Cells[1].Text))
                            {
                                marquesidchecked.Add(row.Cells[1].Text);
                            }

                        }
                        else
                        {
                            if (marquesidchecked.Contains(row.Cells[1].Text))
                            {
                                marquesidchecked.Remove(row.Cells[1].Text);
                            }
                        }



                    }
                }

                List<string> marquesidcheckedd = Session["Marqueschecked"] as List<string>;
                if (marquesidcheckedd.Count != 0)
                {
                    foreach (string id in marquesidcheckedd)
                    {
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            if (marquesidcheckedd.Contains(row.Cells[1].Text))
                            {


                                ((CheckBox)GridView1.Rows[row.RowIndex].FindControl("CheckBox1")).Checked = true;







                            }
                        }

                    }
                }
                if (marquesidcheckedd.Count != 0)
                {
                    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=Ipp;Integrated Security=True");
                    System.Data.SqlClient.SqlCommand sQLiteCommand = new System.Data.SqlClient.SqlCommand();
                    SqlCommand command = new SqlCommand();
                    command.Connection = con;
                    sQLiteCommand.Connection = con;
                    WebClient webClient = new WebClient();
                    webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");
                    var options = new ChromeOptions();

                    ChromeDriver Driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options);
                    //try
                    //{

                    OpenQA.Selenium.Support.UI.WebDriverWait wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, TimeSpan.FromSeconds(60));



                    Driver.Navigate().GoToUrl($"http://www.directompic.ma/");

                    Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(45);






                    foreach (string row in marquesidcheckedd)
                    {

                        //toupdate.Add(listdg.Rows[0][1].ToString().Trim());
                        string marque = row.ToString().Trim();




                        Driver.Navigate().GoToUrl($"http://www.directompic.ma/fr/renouv-marque");

                        //wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[1]/div/input")));


                        Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[2]/div/select")).Click();
                        Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[1]/div/input")).SendKeys(marque);


                        Page.RegisterStartupScript("Key", "<script type='text/javascript'>window.onload = function(){alert('successfully updated');return false;}</script>");

                        if (!marque.Contains("-"))
                        {
                            int index = int.Parse(marque);
                            if (index > 99999)
                            {

                                Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[2]/div/select/option[2]")).Click();


                                Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[7]/button")).Click();
                                string fb = Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/fieldset/div[2]/table/tbody/tr/td[5]")).Text;


                                con.Open();
                                //try
                                //{
                                command.CommandText = "UPDATE Marques_Ompic SET Statut = '" + fb + "' where NumeroTitre ='" + row + "'";
                                command.ExecuteNonQuery();
                                command.CommandText = " UPDATE Marques_Tm set MarkCurrentStatusCode = '" + fb + "' where ST13 ='" + row + "'";
                                command.ExecuteNonQuery();
                                con.Close();
                                if (fb == "ENREGISTREE")
                                {
                                    Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/fieldset/div[2]/table/tbody/tr/td[1]/a")).Click();
                                    con.Open();
                                    //try
                                    //{
                                    string db = Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/fieldset/table/tbody/tr[7]/td[2]")).Text;
                                    DateTime dbt = DateTime.Parse(db).Date;
                                    command.CommandText = "UPDATE Marques_Ompic SET Dateexpiration = '" + dbt + "' where NumeroTitre ='" + row + "'";
                                    command.ExecuteNonQuery();
                                    command.CommandText = " UPDATE Marques_Tm set ExpiryDate = '" + dbt + "' where ST13 ='" + row + "'";
                                    command.ExecuteNonQuery();
                                    con.Close();

                                }
                                //}
                                //catch (Exception ex)
                                //{

                                //}

                            }
                            else
                            {
                                try
                                {
                                    Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[2]/div/select/option[3]")).Click();
                                    Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[7]/button")).Click();
                                    Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[7]/button")).Click();
                                    string fb = Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/fieldset/div[2]/table/tbody/tr/td[5]")).Text;

                                    con.Open();
                                    try
                                    {
                                        command.CommandText = "UPDATE Marques_Ompic SET Statut = '" + fb + "' where NumeroTitre ='" + row + "'";
                                        command.ExecuteNonQuery();
                                        command.CommandText = " UPDATE Marques_Tm set MarkCurrentStatusCode = '" + fb + "' where ST13 ='" + row + "'";
                                        command.ExecuteNonQuery();

                                        con.Close();
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[2]/div/select/option[4]")).Click();
                                        Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[7]/button")).Click();
                                        Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[7]/button")).Click();
                                        string fb = Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/fieldset/div[2]/table/tbody/tr/td[5]")).Text;

                                        con.Open();
                                        try
                                        {
                                            command.CommandText = "UPDATE Marques_Ompic SET Statut = '" + fb + "' where NumeroTitre ='" + row + "'";
                                            command.ExecuteNonQuery();
                                            command.CommandText = " UPDATE Marques_Tm set MarkCurrentStatusCode = '" + fb + "' where ST13 ='" + row + "'";
                                            command.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        catch (Exception exsss)
                                        {

                                        }
                                    }
                                    catch (Exception exs)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[2]/div/select/option[3]")).Click();
                                Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[7]/button")).Click();
                                Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[7]/button")).Click();
                                string fb = Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/fieldset/div[2]/table/tbody/tr/td[5]")).Text;

                                con.Open();
                                try
                                {
                                    command.CommandText = "UPDATE Marques_Ompic SET Statut = '" + fb + "' where NumeroTitre ='" + row + "'";
                                    command.ExecuteNonQuery();
                                    command.CommandText = " UPDATE Marques_Tm set MarkCurrentStatusCode = '" + fb + "' where ST13 ='" + row + "'";
                                    command.ExecuteNonQuery();
                                    con.Close();
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            catch (Exception ex)
                            {
                                try
                                {
                                    Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[2]/div/select/option[4]")).Click();
                                    Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[7]/button")).Click();
                                    Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[7]/button")).Click();
                                    string fb = Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/fieldset/div[2]/table/tbody/tr/td[5]")).Text;

                                    con.Open();
                                    try
                                    {
                                        command.CommandText = "UPDATE Marques_Ompic SET Statut = '" + fb + "' where NumeroTitre ='" + row + "'";
                                        command.ExecuteNonQuery();
                                        command.CommandText = " UPDATE Marques_Tm set MarkCurrentStatusCode = '" + fb + "' where ST13 ='" + row + "'";
                                        command.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    catch (Exception exss)
                                    {

                                    }
                                }
                                catch (Exception exs)
                                {
                                    continue;
                                }
                            }
                        }
                    }


                    Page.RegisterStartupScript("Key", "<script type='text/javascript'>window.onload = function(){alert('successfully updated');return false;}</script>");


                    Driver.Close();
                }
            }
            catch (Exception exx)
            {
                Page.RegisterStartupScript("Key", "<script type='text/javascript'>window.onload = function(){alert('Update faled');return false;}</script>");

            }







        }

        protected void Alert_gen_eng(object sender, ImageClickEventArgs e)
        {


            if (Session["Marqueschecked"] == null)
            {
                List<string> marquesidchecked = new List<string>();



                foreach (GridViewRow row in GridView1.Rows)
                {
                    //if (item.checkede == "true")
                    //{
                    //    gh++;
                    //}


                    CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                    if (cb.Checked == true)
                    {
                        if (!marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Add(row.Cells[1].Text);
                        }

                    }
                    else
                    {
                        if (marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Remove(row.Cells[1].Text);
                        }
                    }



                }
                Session["Marqueschecked"] = marquesidchecked;
            }
            else
            {
                List<string> marquesidchecked = Session["Marqueschecked"] as List<string>;


                foreach (GridViewRow row in GridView1.Rows)
                {
                    //if (item.checkede == "true")
                    //{
                    //    gh++;
                    //}


                    CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                    if (cb.Checked == true)
                    {
                        if (!marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Add(row.Cells[1].Text);
                        }

                    }
                    else
                    {
                        if (marquesidchecked.Contains(row.Cells[1].Text))
                        {
                            marquesidchecked.Remove(row.Cells[1].Text);
                        }
                    }



                }
            }

            List<string> marquesidcheckedd = Session["Marqueschecked"] as List<string>;
            if (marquesidcheckedd.Count != 0)
            {
                foreach (string id in marquesidcheckedd)
                {
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (marquesidcheckedd.Contains(row.Cells[1].Text))
                        {

                            ((CheckBox)GridView1.Rows[row.RowIndex].FindControl("CheckBox1")).Checked = true;

                        }
                    }

                }
            }




            List<Marque_TmOmpicModel> marques = Session["BDMarques"] as List<Marque_TmOmpicModel>;
            List<tmopmic> test = new List<tmopmic>();
            //List<Marque_TmOmpicModel> test2 = Session["list_marques_searched"] as List<Marque_TmOmpicModel>;
            //foreach (var item in list_marque)
            //{
            //    test2.Add(new Marque_TmOmpicModel { Numero_titre = item.Numero_titre, Nom_marque = item.Nom_marque, Date_depot = item.Date_depot, Date_expiration = item.Date_expiration, Applicant_name = item.Applicant_name, Representative_name = item.Representative_name, ClasseNice = item.ClasseNice });
            //}
            System.Data.DataSet ds = new System.Data.DataSet("DataTable1");
            DataTable table = new DataTable("DataTable1");

            table.Columns.Add("NumeroTitre");
            table.Columns.Add("image");
            table.Columns.Add("Nommarque");
            table.Columns.Add("Applicant_name");
            table.Columns.Add("Datedepot");
            table.Columns.Add("Dateexpiration");
            table.Columns.Add("NiceClass");
            table.Columns["image"].DataType = System.Type.GetType("System.Byte[]");
            table.Columns.Add("Adresse");

            foreach (Marque_TmOmpicModel marque in marques)
            {
                if (marquesidcheckedd.Contains(marque.Numero_titre))
                {
                    var ms = new MemoryStream();
                    DataRow row = table.NewRow();
                    string imgname = "";

                    row["NumeroTitre"] = marque.Numero_titre;

                    if (!string.IsNullOrWhiteSpace(marque.Numero_titre))
                    {

                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.jpg"))
                        {
                            imgname = $@"{marque.Numero_titre}.jpg";
                        }
                        else
                        {
                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.JPG"))
                            {
                                imgname = $@"{marque.Numero_titre}.JPG";
                            }
                            else
                            {
                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.jpeg"))
                                {
                                    imgname = $@"{marque.Numero_titre}.jpeg";
                                }
                                else
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.png"))
                                    {
                                        imgname = $@"{marque.Numero_titre}.png";
                                    }
                                    //else
                                    //{
                                    //    webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{marque.NumeroTitre}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{marque.NumeroTitre}.jpg");
                                    //    marque.Image = $@"{marque.NumeroTitre}.jpg";
                                    //}
                                }
                            }
                        }
                    }

                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgname))
                    {
                        System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgname).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else
                    {
                        System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\Empty.png").Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    row["image"] = ms.ToArray();
                    row["Nommarque"] = marque.Nom_marque;
                    row["Datedepot"] = marque.Date_depot;
                    row["Dateexpiration"] = marque.Date_expiration;
                    row["Applicant_name"] = marque.Applicant_name;
                    row["Adresse"] = marque.Applicant_address;

                    row["NiceClass"] = marque.ClasseNice;


                    table.Rows.Add(row);
                }



            }
            foreach (DataRow rt in table.Rows)
            {

                rt[5] = retext(rt[5].ToString());
                //rt[8] = retext(rt[8].ToString());
                rt[4] = retext(rt[4].ToString());
                rt[3] = retext(rt[3].ToString());
                rt[2] = retext(rt[2].ToString());
                rt[6] = retext(rt[6].ToString());
                //rt[7] = retext(rt[7].ToString());

            }
            ds.Tables.Add(table);


            CrystalReport7 report = new CrystalReport7();
            report.Database.Tables["DataTable1"].SetDataSource(ds);
            string filename = $"Alert_export.pdf";
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            report.Close();
            report.Dispose();
            MemoryStream memoryS = new MemoryStream();
            stream.CopyTo(memoryS);
            byte[] buffer = memoryS.ToArray();

            HttpCookie httpCookie = Request.Cookies["Userinfo"];
            string name = httpCookie["Username"];
            string type = "Alert";
            storepdf(buffer, name, type);
            Response.AddHeader("Content-Length", buffer.Length.ToString());
            Response.AddHeader("Content-Disposition", $"attachment; filename={filename}");
            Response.OutputStream.Write(buffer, 0, buffer.Length);
            Response.End();
            


        }
        public static void  storepdf(byte[] buffer ,string name,string type)
        {
            
            DateTime dsate = DateTime.Now;
            DateTime dcate = DateTime.Now.Date;


            SqlConnection con = new SqlConnection(@"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=data;Integrated Security=True");
            SqlCommand cmdd = new SqlCommand();
            cmdd.Connection = con;
            con.Open();
            int nb;
            cmdd.CommandText = $"select count(*) from article";
            nb = int.Parse(cmdd.ExecuteScalar().ToString());
            nb=1000+nb;
            cmdd.CommandText = $"insert into article (Ref,Doc,users,datecre,type,datec) values(@ref,@Doc,@user,@date,@type,@datec)";
            cmdd.Parameters.Clear();
            cmdd.Parameters.AddWithValue("@ref", nb.ToString());
            cmdd.Parameters.AddWithValue("@Doc", SqlDbType.VarBinary).Value = buffer;
            cmdd.Parameters.AddWithValue("@user", name);
            cmdd.Parameters.AddWithValue("@date", dsate);
            cmdd.Parameters.AddWithValue("@type", type);
            cmdd.Parameters.AddWithValue("@datec", dcate);
            cmdd.ExecuteNonQuery();
            con.Close();
        }

        protected void Searchpheo_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString);
            List<Marque_TmOmpicModel> list_ompic = new List<Marque_TmOmpicModel>();
            int cleartest = 0;
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            string staut = "";

            if (!string.IsNullOrWhiteSpace(Request.Form["type_marqueOmpic"]))
            {
                list.Add("Type like @Type");
                command.Parameters.AddWithValue("@Type", "%" + Request.Form["type_marqueOmpic"].Trim().ToUpper() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["etatMarqueOmpic"]))
            {
                staut = Request.Form["etatMarqueOmpic"];
                list.Add("Statut like @Statut");
                command.Parameters.AddWithValue("@Statut", Request.Form["etatMarqueOmpic"].Trim());
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["num_marq"]))
            {
                list.Add("NumeroTitre like @NumeroTitre");
                command.Parameters.AddWithValue("@NumeroTitre", "%" + Request.Form["num_marq"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["mandataire"]))
            {
                list.Add("Mandataire like @Mandataire");
                command.Parameters.AddWithValue("@Mandataire", "%" + Request.Form["mandataire"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["email"]))
            {
                list.Add("Email like @Email");
                command.Parameters.AddWithValue("@Email", "%" + Request.Form["email"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["nom_marq"]))
            {
                
                    list.Add("Nommarque like @Nommarque or Nommarque like @Nommarquedouble ");
                string nommarque = Request.Form["nom_marq"].Trim().Replace("e", "ù").Replace("E", "ù").Replace("i", "ù").Replace("I", "ù").Replace("a", "ù").Replace("A", "ù").Replace("o", "ù").Replace("O", "ù").Replace("u", "ù").Replace("U", "ù").Replace("y", "ù").Replace("Y", "ù")/*.Replace("ù", "[aeiuoy]")*/;
                string nommarv = null;

                

                if (nommarque[nommarque.Length - 1] == 'ù')
                {
                    list.Remove("Nommarque like @Nommarque or Nommarque like @Nommarquedouble ");
                    nommarv=nommarque.Remove(nommarque.Length-1);
                    nommarv = nommarv.Replace("ù", "[aeiuoy]").Replace(' ', '%');
                    list.Add("Nommarque like @Nommarque or Nommarque like @Nommarquedouble or Nommarque like @Nommarquedoubl");
                    if (nommarque[0] == 'L' || nommarque[0] == 'l')
                    {
                        command.Parameters.AddWithValue("@Nommarquedoubl", "%" + nommarv.Remove(0, 1) + "%");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Nommarquedoubl", "%" + nommarv + "%");
                    }
                        
                        
                }
                nommarque = Request.Form["nom_marq"].Trim().Replace("e", "ù").Replace("E", "ù").Replace("i", "ù").Replace("I", "ù").Replace("a", "ù").Replace("A", "ù").Replace("o", "ù").Replace("O", "ù").Replace("u", "ù").Replace("U", "ù").Replace("y", "ù").Replace("Y", "ù").Replace("ù", "[aeiuoy]").Replace(' ', '%');
                string nommarquedouble = Request.Form["nom_marq"].Trim().Replace("e", "ù").Replace("E", "ù").Replace("i", "ù").Replace("I", "ù").Replace("a", "ù").Replace("A", "ù").Replace("o", "ù").Replace("O", "ù").Replace("u", "ù").Replace("U", "ù").Replace("y", "ù").Replace("Y", "ù").Replace("ù", "[aeiuoy][aeiuoy]").Replace(' ', '%');

                if (nommarque[0] == 'L' || nommarque[0] == 'l')
                {
                    command.Parameters.AddWithValue("@Nommarque", "%" + nommarque.Remove(0,1) + "%");
                    command.Parameters.AddWithValue("@Nommarquedouble", "%" + nommarquedouble.Remove(0, 1) + "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Nommarque", "%" + nommarque + "%");
                    command.Parameters.AddWithValue("@Nommarquedouble", "%" + nommarquedouble + "%");
                }
               

            }
            if (!string.IsNullOrWhiteSpace(Request.Form["deposant"]))
            {
                list.Add("Deposant like @Deposant");
                command.Parameters.AddWithValue("@Deposant", "%" + Request.Form["deposant"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["adresse_deposant"]))
            {
                list.Add("Adresse like @Adresse");
                command.Parameters.AddWithValue("@Adresse", "%" + Request.Form["adresse_deposant"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Pays_deposant"]))
            {
                list.Add("Pays like @Pays");
                command.Parameters.AddWithValue("@Pays", "%" + Request.Form["Pays_deposant"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_debut"]) && !string.IsNullOrWhiteSpace(Request.Form["date_depot_fin"]))
            {
                list.Add("Datedepot >= @date_depot_debut and Datedepot <= @date_depot_fin");
                command.Parameters.AddWithValue("@date_depot_debut", DateTime.Parse(Request.Form["date_depot_debut"]));
                command.Parameters.AddWithValue("@date_depot_fin", DateTime.Parse(Request.Form["date_depot_fin"]));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_debut"]))
                {
                    list.Add("Datedepot = @date_depot_debut");
                    command.Parameters.AddWithValue("@date_depot_debut", DateTime.Parse(Request.Form["date_depot_debut"]));
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_fin"]))
                {
                    list.Add("Datedepot = @date_depot_fin");
                    command.Parameters.AddWithValue("@date_depot_fin", DateTime.Parse(Request.Form["date_depot_fin"]));
                }
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["date_exp_debut"]) && !string.IsNullOrWhiteSpace(Request.Form["date_exp_fin"]))
            {
                list.Add("Dateexpiration >= @date_exp_debut and Dateexpiration <= @date_exp_fin");
                command.Parameters.AddWithValue("@date_exp_debut", DateTime.Parse(Request.Form["date_exp_debut"]));
                command.Parameters.AddWithValue("@date_exp_fin", DateTime.Parse(Request.Form["date_exp_fin"]));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["date_exp_debut"]))
                {
                    list.Add("Datedepot = @date_exp_debut");
                    command.Parameters.AddWithValue("@date_exp_debut", DateTime.Parse(Request.Form["date_exp_debut"]));
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["date_exp_fin"]))
                {
                    list.Add("Datedepot = @date_exp_fin");
                    command.Parameters.AddWithValue("@date_exp_fin", DateTime.Parse(Request.Form["date_exp_fin"]));
                }
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["type_marqueTm"]))
            {
                if (Request.Form["type_marqueTm"] == "combined")
                {
                    list.Add("Type like @Type");
                    command.Parameters.AddWithValue("@Type", "%" + "mixte".ToUpper() + "%");
                }
                if (Request.Form["type_marqueTm"] == "sound")
                {
                    list.Add("Type like @Type");
                    command.Parameters.AddWithValue("@Type", "%" + "sonore".ToUpper() + "%");
                }
                if (Request.Form["type_marqueTm"] == "other")
                {
                    list.Add("Type like @Type");
                    command.Parameters.AddWithValue("@Type", "%" + "autres".ToUpper() + "%");
                }
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["etatMarqueTm"]))
            {

                if (Request.Form["etatMarqueTm"] == "registered")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "enregistree".ToUpper() + "%");
                }
                if (Request.Form["etatMarqueTm"] == "registration cancelled")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "radiee".ToUpper() + "%");
                }
                //if (Request.Form["etatMarqueTm"] == "application opposed")
                //{
                //    list.Add("Statut like @Opp_S or Statut like @Opp_C  or Statut like @Opp ");
                //    command.Parameters.AddWithValue("@Opp_S", "%" + "opposition suspendue".ToUpper() + "%");
                //    command.Parameters.AddWithValue("@Opp_C", "%" + "opposition en cours".ToUpper() + "%");
                //    command.Parameters.AddWithValue("@Opp", "%" + "opposition".ToUpper() + "%");
                //}
                if (Request.Form["etatMarqueTm"] == "application refused")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "rejetee".ToUpper() + "%");
                }
                if (Request.Form["etatMarqueTm"] == "expired")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "expiree".ToUpper() + "%");
                }
                //if (Request.Form["etatMarqueTm"] == "application withdrawn")
                //{
                //    list.Add("Statut like @CCR or Statut like @R ");
                //    command.Parameters.AddWithValue("@CCR", "%" + "consideree comme retiree".ToUpper() + "%");
                //    command.Parameters.AddWithValue("@R", "%" + "retiree".ToUpper() + "%");
                //}
                if (Request.Form["etatMarqueTm"] == "application published")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "publiee".ToUpper() + "%");
                }
                if (Request.Form["etatMarqueTm"] == "appeal pending")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "en poursuite de procedure".ToUpper() + "%");
                }
                if (Request.Form["etatMarqueTm"] == "renewed")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "renouvlee".ToUpper() + "%");
                }
                if (Request.Form["etatMarqueTm"] == "registration surrendered")
                {
                    list.Add("Statut like @Statut");
                    command.Parameters.AddWithValue("@Statut", "%" + "renoncee".ToUpper() + "%");
                }
            }

            if (!string.IsNullOrWhiteSpace(Request.Form["Classe_nice"]))
            {

                string classnices = "";
                List<string> nices = new List<string>();
                nices = Request.Form["Classe_nice"].Split(',').ToList<string>();
                if (nices.Count != 0)
                {
                    for (int i = 0; i < nices.Count; i++)
                    {
                        if ((nices.Count - 1) == i)
                        {

                            classnices += "%" + nices[i].Trim() + "%";
                        }
                        else
                        {
                            classnices += "%" + nices[i].Trim() + ",";
                        }
                    }
                    //classnices.TrimEnd(',');


                    list.Add("ClasseNice like @ClasseNice");
                    command.Parameters.AddWithValue("@ClasseNice", classnices + "%");
                }

            }
            DataTable ompic = new DataTable();
            DataTable TM = new DataTable();
            List<Marque_TmOmpicModel> list_marque = new List<Marque_TmOmpicModel>();
            if (list.Count > 0)
            {

                var Ompicquery = "select * from Marques_Ompic where ";
                for (int i = 0; i < list.Count; i++)
                {
                    if ((list.Count - 1) == i)
                    {

                        Ompicquery += list[i] + "  order by Nommarque";
                    }
                    else
                    {
                        Ompicquery += list[i] + " and ";
                    }
                }
                command.CommandText = Ompicquery;
                con.Close();
                con.Open();

                var reader = command.ExecuteReader();
                ompic.Load(reader);
                con.Close();

            }
            else
            {
                cleartest++;
            }
            list.Clear();

            command.Parameters.Clear();

            if (!string.IsNullOrWhiteSpace(Request.Form["etatMarqueTm"]))
            {
                list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + Request.Form["etatMarqueTm"].Trim() + "%");
            }
            else
            {
                staut = staut.ToUpper();
                if (!string.IsNullOrWhiteSpace(staut))
                {

                    if (staut == "ENREGISTREE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Registered" + "%");
                    }
                    if (staut == "RADIEE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Registration cancelled" + "%");
                    }
                    if (staut == "OPPOSITION SUSPENDUE" || staut == "OPPOSITION EN COURS" || staut == "OPPOSITION")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application opposed" + "%");
                    }
                    if (staut.Trim() == "REJETEE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "APPLICATION REFUSED" + "%");
                    }
                    if (staut == "EXPIREE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Expired" + "%");
                    }
                    if (staut == "CONSIDEREE COMME RETIREE" || staut == "RETIREE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application withdrawn" + "%");
                    }
                    if (staut == "EN COURS D'EXAMEN" || staut == "EN EXAMEN DE FORME" || staut == "EN INSTANCE DE REGULARISATION" || staut == "EN EXAMEN DES MOTIFS ABSOLUS" || staut == "PUBLICATION PROGRAMMEE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application filed" + "%");
                    }
                    if (staut == "PUBLIEE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application published" + "%");
                    }
                    if (staut == "EN POURSUITE DE PROCEDURE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Appeal pending" + "%");
                    }
                    if (staut == "RENOUVELEE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Renewed" + "%");
                    }
                    if (staut == "RENONCEE")
                    {
                        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
                        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Registration surrendered" + "%");
                    }
                    //}
                }
            }



            if (!string.IsNullOrWhiteSpace(Request.Form["num_marq"]))
            {
                list.Add("ST13 = @ST13");
                command.Parameters.AddWithValue("@ST13", Request.Form["num_marq"].Trim());
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["mandataire"]))
            {
                list.Add("Representative_name like @Representative_name");
                command.Parameters.AddWithValue("@Representative_name", "%" + Request.Form["mandataire"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["nom_marq"]))
            {
                list.Add("TmName like @Nommarque or TmName like @Nommarquedouble");
                string nommarque = Request.Form["nom_marq"].Trim().Replace("e", "ù").Replace("E", "ù").Replace("i", "ù").Replace("I", "ù").Replace("a", "ù").Replace("A", "ù").Replace("o", "ù").Replace("O", "ù").Replace("u", "ù").Replace("U", "ù").Replace("y", "ù").Replace("Y", "ù")/*.Replace("ù", "[aeiuoy]")*/;
                string nommarv = null;
               if(nommarque[0] == 'L' || nommarque[0] == 'l')
                {
                    nommarque= nommarque.Remove(0, 1);
                }
                if (nommarque[nommarque.Length - 1] == 'ù')
                {
                    list.Remove("TmName like @Nommarque or TmName like @Nommarquedouble");
                    nommarv = nommarque.Remove(nommarque.Length - 1);
                    nommarv = nommarv.Replace("ù", "[aeiuoy]").Replace(' ', '%');
                    list.Add("TmName like @Nommarque or TmName like @Nommarquedouble or TmName like @Nommarquedoubl");
                    if (nommarque[0] == 'L' || nommarque[0] == 'l')
                    {
                        command.Parameters.AddWithValue("@Nommarquedoubl", "%" + nommarv.Remove(0, 1) + "%");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Nommarquedoubl", "%" + nommarv + "%");
                    }
                }
                nommarque = Request.Form["nom_marq"].Trim().Replace("e", "ù").Replace("E", "ù").Replace("i", "ù").Replace("I", "ù").Replace("a", "ù").Replace("A", "ù").Replace("o", "ù").Replace("O", "ù").Replace("u", "ù").Replace("U", "ù").Replace("y", "ù").Replace("Y", "ù").Replace("ù", "[aeiuoy]").Replace(' ', '%');
                string nommarquedouble = Request.Form["nom_marq"].Trim().Replace("e", "ù").Replace("E", "ù").Replace("i", "ù").Replace("I", "ù").Replace("a", "ù").Replace("A", "ù").Replace("o", "ù").Replace("O", "ù").Replace("u", "ù").Replace("U", "ù").Replace("y", "ù").Replace("Y", "ù").Replace("ù", "[aeiuoy][aeiuoy]").Replace(' ', '%');
                if (nommarque[0] == 'L' || nommarque[0] == 'l')
                {
                    command.Parameters.AddWithValue("@Nommarque", "%" + nommarque.Remove(0, 1) + "%");
                    command.Parameters.AddWithValue("@Nommarquedouble", "%" + nommarquedouble.Remove(0, 1) + "%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Nommarque", "%" + nommarque + "%");
                    command.Parameters.AddWithValue("@Nommarquedouble", "%" + nommarquedouble + "%");
                }
                //list.Add("TmName like @TmName");
                string testph = nommarque + " " + nommarquedouble + " " + nommarv;
                //Response.Write(testph);

                //string nommarque = Request.Form["nom_marq"].Trim().Replace("e", "ù").Replace("E", "ù").Replace("i", "ù").Replace("I", "ù").Replace("a", "ù").Replace("A", "ù").Replace("o", "ù").Replace("O", "ù").Replace("u", "ù").Replace("U", "ù").Replace("y", "ù").Replace("Y", "ù").Replace("ù", "[aeiuoy]");
                //command.Parameters.AddWithValue("@TmName", "%" + nommarque + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["deposant"]))
            {
                list.Add("Applicant_name like @Applicant_name");
                command.Parameters.AddWithValue("@Applicant_name", "%" + Request.Form["deposant"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["adresse_deposant"]))
            {
                list.Add("Applicant_address like @Applicant_address");
                command.Parameters.AddWithValue("@Applicant_address", "%" + Request.Form["adresse_deposant"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_debut"]) && !string.IsNullOrWhiteSpace(Request.Form["date_depot_fin"]))
            {
                list.Add("ApplicationDate >= @date_depot_debut and ApplicationDate <= @date_depot_fin");
                command.Parameters.AddWithValue("@date_depot_debut", DateTime.Parse(Request.Form["date_depot_debut"]));
                command.Parameters.AddWithValue("@date_depot_fin", DateTime.Parse(Request.Form["date_depot_fin"]));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_debut"]))
                {
                    list.Add("ApplicationDate = @date_depot_debut");
                    command.Parameters.AddWithValue("@date_depot_debut", DateTime.Parse(Request.Form["date_depot_debut"]));
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_fin"]))
                {
                    list.Add("ApplicationDate = @date_depot_fin");
                    command.Parameters.AddWithValue("@date_depot_fin", DateTime.Parse(Request.Form["date_depot_fin"]));
                }
            }
            int r;
            if (!string.IsNullOrWhiteSpace(Request.Form["OppositionNbrMin"]) && int.TryParse(Request.Form["OppositionNbrMin"], out r))
            {
                list.Add("Number_of_opposition >= @Number_of_opposition");
                command.Parameters.AddWithValue("@Number_of_opposition", r);
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Da_opposition"]))
            {
                list.Add("Opposition_earlierMark_applicationNumber like @Opposition_earlierMark_applicationNumber");
                command.Parameters.AddWithValue("@Opposition_earlierMark_applicationNumber", "%" + Request.Form["Da_opposition"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Opposant"]))
            {
                list.Add("Opposition_applicant_name like @Opposition_applicant_name");
                command.Parameters.AddWithValue("@Opposition_applicant_name", "%" + Request.Form["Opposant"].Trim() + "%");
            }

            if (!string.IsNullOrWhiteSpace(Request.Form["date_exp_debut"]) && !string.IsNullOrWhiteSpace(Request.Form["date_exp_fin"]))
            {
                list.Add("ExpiryDate >= @date_exp_debut and ExpiryDate <= @date_exp_fin");
                command.Parameters.AddWithValue("@date_exp_debut", DateTime.Parse(Request.Form["date_exp_debut"]));
                command.Parameters.AddWithValue("@date_exp_fin", DateTime.Parse(Request.Form["date_exp_fin"]));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["date_exp_debut"]))
                {
                    list.Add("ExpiryDate = @date_exp_debut");
                    command.Parameters.AddWithValue("@date_exp_debut", DateTime.Parse(Request.Form["date_exp_debut"]));
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["date_exp_fin"]))
                {
                    list.Add("ExpiryDate = @date_exp_fin");
                    command.Parameters.AddWithValue("@date_exp_fin", DateTime.Parse(Request.Form["date_exp_fin"]));
                }
            }
            //if (!string.IsNullOrWhiteSpace(staut))
            //{

            //    if (staut == "ENREGISTREE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Registered" + "%");
            //    }
            //    if (staut == "RADIEE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Registration cancelled" + "%");
            //    }
            //    if (staut == "OPPOSITION SUSPENDUE" || staut == "OPPOSITION EN COURS" || staut == "OPPOSITION")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application opposed" + "%");
            //    }
            //    if (staut.Trim() == "REJETEE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "APPLICATION REFUSED" + "%");
            //    }
            //    if (staut == "EXPIREE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Expired" + "%");
            //    }
            //    if (staut == "CONSIDEREE COMME RETIREE" || staut == "RETIREE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application withdrawn" + "%");
            //    }
            //    if (staut == "EN COURS D'EXAMEN" || staut == "EN EXAMEN DE FORME" || staut == "EN INSTANCE DE REGULARISATION" || staut == "EN EXAMEN DES MOTIFS ABSOLUS" || staut == "PUBLICATION PROGRAMMEE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application filed" + "%");
            //    }
            //    if (staut == "PUBLIEE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Application published" + "%");
            //    }
            //    if (staut == "EN POURSUITE DE PROCEDURE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Appeal pending" + "%");
            //    }
            //    if (staut == "RENOUVELEE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Renewed" + "%");
            //    }
            //    if (staut == "RENONCEE")
            //    {
            //        list.Add("MarkCurrentStatusCode like @MarkCurrentStatusCode");
            //        command.Parameters.AddWithValue("@MarkCurrentStatusCode", "%" + "Registration surrendered" + "%");
            //    }
            //    //}
            //}
            if (!string.IsNullOrWhiteSpace(Request.Form["type_marqueOmpic"]))
            {
                if (Request.Form["type_marqueOmpic"] == "Mixte")
                {
                    list.Add("MarkFeature like @MarkFeature");
                    command.Parameters.AddWithValue("@MarkFeature", "%" + "COMBINED" + "%");
                }
                if (Request.Form["type_marqueOmpic"] == "Sonore")
                {
                    list.Add("MarkFeature like @MarkFeature");
                    command.Parameters.AddWithValue("@MarkFeature", "%" + "SOUND" + "%");
                }
                if (Request.Form["type_marqueOmpic"] == "Autres")
                {
                    list.Add("MarkFeature like @MarkFeature");
                    command.Parameters.AddWithValue("@MarkFeature", "%" + "OTHER" + "%");
                }
                else
                {
                    list.Add("MarkFeature like @MarkFeature");
                    command.Parameters.AddWithValue("@MarkFeature", "%" + "OTHER" + "%");
                }
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["type_marqueTm"]))
            {
                list.Add("MarkFeature like @MarkFeature");
                command.Parameters.AddWithValue("@MarkFeature", "%" + Request.Form["type_marqueTm"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Classe_nice"]))
            {

                string classnices = "";
                List<string> nices = new List<string>();
                nices = Request.Form["Classe_nice"].Split(',').ToList<string>();
                if (nices.Count != 0)
                {

                    for (int i = 0; i < nices.Count; i++)
                    {
                        if ((nices.Count - 1) == i)
                        {

                            classnices += "%" + nices[i].Trim() + "%";
                        }
                        else
                        {
                            classnices += "%" + nices[i].Trim() + ",";
                        }
                    }
                    classnices.TrimEnd(',');


                    list.Add("NiceClassNumbers like @NiceClassNumbers");
                    command.Parameters.AddWithValue("@NiceClassNumbers", classnices + "%");
                }

            }

            if (list.Count > 0)
            {
                var Tmquery = "select * from Marques_Tm where ";
                for (int i = 0; i < list.Count; i++)
                {
                    if ((list.Count - 1) == i)
                    {

                        Tmquery += list[i]+" "+ "  order by TmName ";
                    }
                    else
                    {
                        Tmquery += list[i] + " and ";
                    }
                }

                command.CommandText = Tmquery;
                //Response.Write(Tmquery);
                //Response.Write("      :::    "+staut);
                con.Close();
                con.Open();
                var reader = command.ExecuteReader();
                TM.Load(reader);
                con.Close();
            }
            else
            {
                cleartest++;
            }
            if (cleartest == 2)
            {
                Response.Redirect("Recherche Bd.aspx");
            }
            if (ompic.Rows.Count != 0 && TM.Rows.Count != 0)
            {

                foreach (DataRow row in ompic.Rows)
                {
                    var marque = new Marque_TmOmpicModel()
                    {
                        Applicant_name = row[0] != null ? row[0].ToString() : "",
                        Date_depot = row[1] != null ? row[1].ToString().Split(' ').FirstOrDefault().Trim() : "",
                        Date_expiration = row[2] != null ? row[2].ToString().Split(' ').FirstOrDefault().Trim() : "",
                        Pays = row[3] != null ? row[3].ToString() : "",
                        Telephone = row[4] != null ? row[4].ToString() : "",
                        ClasseNice = row[5] != null ? row[5].ToString() : "",
                        ClasseDetails = row[6] != null ? row[6].ToString() : "",
                        Numero_titre = row[7] != null ? row[7].ToString() : "",
                        Type = row[8] != null ? row[8].ToString() : "",
                        Representative_name = row[9] != null ? row[9].ToString() : "",
                        Nom_marque = row[10] != null ? row[10].ToString() : "",
                        Applicant_address = row[11] != null ? row[11].ToString() : "",
                        Statut = row[12] != null ? row[12].ToString() : "",
                        Email = row[13] != null ? row[13].ToString() : "",
                        Loi = row[14] != null ? row[14].ToString() : "",
                        Numero_publication = row[15] != null ? row[15].ToString() : "",
                        Updated_date = row[16] != null ? row[16].ToString().Split(' ').FirstOrDefault().Trim() : "",
                        MappingId = row[17] != null ? row[17].ToString() : ""
                    };

                    DataRow rowtar = null;
                    foreach (DataRow rowx in TM.Rows)
                    {

                        if (rowx[0].ToString() == row[7].ToString())
                        {

                            if (rowx[10]?.ToString().Trim() != marque.Applicant_name.Trim())
                            {
                                marque.Applicant_name = "<span class='ompic_value'>" + marque.Applicant_name + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[10].ToString() + "</span>";
                            }


                            if (rowx[3]?.ToString().Split(' ').FirstOrDefault().Trim() != marque.Date_depot.Trim())
                            {
                                marque.Date_depot = "<span class='ompic_value'>" + marque.Date_depot + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[3].ToString().Split(' ').FirstOrDefault().Trim() + "</span>";
                            }










                            if (rowx[5]?.ToString().Split(' ').FirstOrDefault().Trim() != marque.Date_expiration.Trim())
                            {
                                marque.Date_expiration = "<span class='ompic_value'>" + marque.Date_expiration + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[5].ToString().Split(' ').FirstOrDefault().Trim() + "</span>";
                            }

                            if (rowx[15] != null)
                            {
                                foreach (var line in File.ReadAllLines(Server.MapPath("~") + @"\Setting\Country Codes.txt"))
                                {
                                    foreach (var item in line.Split(',').LastOrDefault().Split('/'))
                                    {
                                        if (item.Trim().ToUpper() == rowx[15].ToString().Trim().ToUpper())
                                        {
                                            if (line.Split(',').FirstOrDefault().Trim().ToUpper() != marque.Pays.ToUpper().Trim())
                                            {
                                                marque.Pays = "<span class='ompic_value'>" + marque.Pays + "</span>" + "<br/>" + "<span class='tm_value'>" + line.Split(',').FirstOrDefault().Trim() + "</span>";
                                            }
                                        }
                                    }
                                }
                            }

                            if (rowx[9] != null)
                            {

                                var nice_array_ompic = marque.ClasseNice.TrimEnd(',').Split(',');
                                var nice_array_tm = rowx[9].ToString().TrimEnd(',').Split(',');
                                if (nice_array_ompic.Length > 0)
                                {
                                    foreach (var item in nice_array_ompic)
                                    {
                                        if (!string.IsNullOrWhiteSpace(item))
                                        {
                                            if (!nice_array_tm.Contains(item.Trim()))
                                            {
                                                marque.ClasseNice = "<span class='ompic_value'>" + marque.ClasseNice + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[9].ToString() + "</span>";
                                            }
                                        }
                                    }
                                }

                                else
                                {
                                    if (nice_array_tm.Length > 0)
                                    {
                                        foreach (var item in nice_array_tm)
                                        {
                                            if (!string.IsNullOrWhiteSpace(item))
                                            {
                                                if (!nice_array_ompic.Contains(item.Trim()))
                                                {
                                                    marque.ClasseNice = "<span class='ompic_value'>" + marque.ClasseNice + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[9].ToString() + "</span>";
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (rowx[0]?.ToString().Trim() != marque.Numero_titre.Trim())
                            {
                                marque.Numero_titre = "<span class='ompic_value'>" + marque.Numero_titre + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[0].ToString() + "</span>";
                            }


                            if ((marque.Type.Trim() == "Mixte" && rowx[6]?.ToString().Trim() != "Combined") || (marque.Type.Trim() == "Sonore" && rowx[6]?.ToString().Trim() != "Sound") || (marque.Type.Trim() == "Autres" && rowx[6]?.ToString().Trim() != "Other"))
                            {
                                marque.Type = "<span class='ompic_value'>" + marque.Type + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[6].ToString() + "</span>";
                            }


                            if (rowx[16]?.ToString().Trim() != marque.Representative_name.Trim())
                            {
                                marque.Representative_name = "<span class='ompic_value'>" + marque.Representative_name + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[16].ToString() + "</span>";
                            }




                            if (rowx[1]?.ToString().Trim() != marque.Nom_marque.Trim())
                            {
                                marque.Nom_marque = "<span class='ompic_value'>" + marque.Nom_marque + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[1].ToString() + "</span>";
                            }




                            if (rowx[8] != null)
                            {
                                if (marque.Statut.ToUpper().Trim() == "ENREGISTREE" && rowx[8].ToString().Trim() != "Registered")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if (marque.Statut.ToUpper().Trim() == "RADIEE" && rowx[8].ToString().Trim() != "Registration cancelled")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if ((rowx[8].ToString().Trim() == "Application opposed") && ((marque.Statut.ToUpper().Trim() != "OPPOSITION SUSPENDUE") && (marque.Statut.ToUpper().Trim() != "OPPOSITION EN COURS") && (marque.Statut.ToUpper().Trim() != "OPPOSITION")))
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if (marque.Statut.ToUpper().Trim() == "REJETEE" && rowx[8].ToString().Trim() != "Application refused")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if (marque.Statut.ToUpper().Trim() == "EXPIREE" && rowx[8].ToString().Trim() != "Expired")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if ((marque.Statut.ToUpper().Trim() != "CONSIDEREE COMME RETIREE" && marque.Statut.ToUpper().Trim() != "RETIREE") && (rowx[8].ToString().Trim() == "Application withdrawn"))
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if ((marque.Statut.ToUpper().Trim() != "EN COURS D'EXAMEN" && marque.Statut.ToUpper().Trim() != "EN EXAMEN DE FORME" && marque.Statut.ToUpper().Trim() != "EN INSTANCE DE REGULARISATION" && marque.Statut.ToUpper().Trim() != "EN EXAMEN DES MOTIFS ABSOLUS" && marque.Statut.ToUpper().Trim() != "PUBLICATION PROGRAMMEE") && (rowx[8].ToString().Trim() == "Application filed"))
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if (marque.Statut.ToUpper().Trim() == "PUBLIEE" && rowx[8].ToString().Trim() != "Application published")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if (marque.Statut.ToUpper().Trim() == "EN POURSUITE DE PROCEDURE" && rowx[8].ToString().Trim() != "Appeal pending")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if (marque.Statut.ToUpper().Trim() == "RENOUVELEE" && rowx[8].ToString().Trim() != "Renewed")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                if (marque.Statut.ToUpper().Trim() == "RENONCEE" && rowx[8].ToString().Trim() != "Registration surrendered")
                                {
                                    marque.Statut = "<span class='ompic_value'>" + marque.Statut.ToUpper() + "</span>" + "<br/>" + "<span class='tm_value'>" + rowx[8].ToString() + "</span>";
                                }
                                rowtar = rowx;



                            }


                        }
                    }
                    if (rowtar != null)
                    {
                        TM.Rows.Remove(rowtar);
                    }
                    list_marque.Add(marque);
                }
                if (TM.Rows.Count != 0)
                {
                    foreach (DataRow rows in TM.Rows)
                    {
                        var marque = new Marque_TmOmpicModel();
                        marque.Numero_titre = rows[0]?.ToString();
                        marque.Nom_marque = rows[1]?.ToString();
                        marque.Date_depot = rows[3]?.ToString().Split(' ').FirstOrDefault().Trim();
                        marque.Date_expiration = rows[5]?.ToString().Split(' ').FirstOrDefault().Trim();
                        marque.ClasseNice = rows[9]?.ToString();
                        marque.ClasseDetails = rows[32]?.ToString();
                        marque.Type = rows[6]?.ToString();
                        marque.Statut = rows[8]?.ToString();

                        marque.Publication_identifier = rows[21]?.ToString();
                        marque.Publication_section = rows[22]?.ToString();
                        marque.Publication_date = rows[23]?.ToString().Split(' ').FirstOrDefault().Trim();

                        marque.Applicant_name = rows[10]?.ToString();
                        marque.Applicant_legalentity = rows[11]?.ToString();
                        marque.Applicant_nationalityCode = rows[12]?.ToString();
                        marque.Applicant_address = rows[13]?.ToString();
                        marque.Applicant_city = rows[14]?.ToString();
                        marque.Applicant_countryCode = rows[15]?.ToString();

                        marque.Representative_name = rows[16]?.ToString();
                        marque.Representative_nationalityCode = rows[17]?.ToString();
                        marque.Representative_address = rows[18]?.ToString();
                        marque.Representative_city = rows[19]?.ToString();
                        marque.Representative_countryCode = rows[20]?.ToString();

                        marque.OppositionDate = rows[24]?.ToString();
                        marque.Opposition_earlierMark_applicationNumber = rows[25]?.ToString();
                        marque.Opposition_applicant_name = rows[26]?.ToString();
                        marque.Opposition_applicant_legalentity = rows[27]?.ToString();
                        marque.Opposition_nationaliyCode = rows[28]?.ToString();
                        marque.Opposition_applicant_address = rows[29]?.ToString();
                        marque.Opposition_applicant_city = rows[30]?.ToString();
                        marque.Opposition_applicant_countryCode = rows[31]?.ToString();


                        marque.Nombre_opposition = rows[33]?.ToString();
                        list_marque.Add(marque);
                    }
                }

            }
            else if (TM.Rows.Count == 0 && ompic.Rows.Count != 0)
            {
                foreach (DataRow row in ompic.Rows)
                {
                    var marque = new Marque_TmOmpicModel()
                    {
                        Applicant_name = row[0] != null ? row[0].ToString() : "",
                        Date_depot = row[1] != null ? row[1].ToString().Split(' ').FirstOrDefault().Trim() : "",
                        Date_expiration = row[2] != null ? row[2].ToString().Split(' ').FirstOrDefault().Trim() : "",
                        Pays = row[3] != null ? row[3].ToString() : "",
                        Telephone = row[4] != null ? row[4].ToString() : "",
                        ClasseNice = row[5] != null ? row[5].ToString() : "",
                        ClasseDetails = row[6] != null ? row[6].ToString() : "",
                        Numero_titre = row[7] != null ? row[7].ToString() : "",
                        Type = row[8] != null ? row[8].ToString() : "",
                        Representative_name = row[9] != null ? row[9].ToString() : "",
                        Nom_marque = row[10] != null ? row[10].ToString() : "",
                        Applicant_address = row[11] != null ? row[11].ToString() : "",
                        Statut = row[12] != null ? row[12].ToString() : "",
                        Email = row[13] != null ? row[13].ToString() : "",
                        Loi = row[14] != null ? row[14].ToString() : "",
                        Numero_publication = row[15] != null ? row[15].ToString() : "",
                        Updated_date = row[16] != null ? row[16].ToString().Split(' ').FirstOrDefault().Trim() : "",
                        MappingId = row[17] != null ? row[17].ToString() : ""
                    };
                    list_marque.Add(marque);
                }
            }
            else if (TM.Rows.Count != 0 && ompic.Rows.Count == 0)
            {
                foreach (DataRow rows in TM.Rows)
                {
                    var marque = new Marque_TmOmpicModel();
                    marque.Numero_titre = rows[0]?.ToString();
                    marque.Nom_marque = rows[1]?.ToString();
                    marque.Date_depot = rows[3]?.ToString().Split(' ').FirstOrDefault().Trim();
                    marque.Date_expiration = rows[5]?.ToString().Split(' ').FirstOrDefault().Trim();
                    marque.ClasseNice = rows[9]?.ToString();
                    marque.ClasseDetails = rows[32]?.ToString();
                    marque.Type = rows[6]?.ToString();
                    marque.Statut = rows[8]?.ToString();

                    marque.Publication_identifier = rows[21]?.ToString();
                    marque.Publication_section = rows[22]?.ToString();
                    marque.Publication_date = rows[23]?.ToString().Split(' ').FirstOrDefault().Trim();

                    marque.Applicant_name = rows[10]?.ToString();
                    marque.Applicant_legalentity = rows[11]?.ToString();
                    marque.Applicant_nationalityCode = rows[12]?.ToString();
                    marque.Applicant_address = rows[13]?.ToString();
                    marque.Applicant_city = rows[14]?.ToString();
                    marque.Applicant_countryCode = rows[15]?.ToString();

                    marque.Representative_name = rows[16]?.ToString();
                    marque.Representative_nationalityCode = rows[17]?.ToString();
                    marque.Representative_address = rows[18]?.ToString();
                    marque.Representative_city = rows[19]?.ToString();
                    marque.Representative_countryCode = rows[20]?.ToString();

                    marque.OppositionDate = rows[24]?.ToString();
                    marque.Opposition_earlierMark_applicationNumber = rows[25]?.ToString();
                    marque.Opposition_applicant_name = rows[26]?.ToString();
                    marque.Opposition_applicant_legalentity = rows[27]?.ToString();
                    marque.Opposition_nationaliyCode = rows[28]?.ToString();
                    marque.Opposition_applicant_address = rows[29]?.ToString();
                    marque.Opposition_applicant_city = rows[30]?.ToString();
                    marque.Opposition_applicant_countryCode = rows[31]?.ToString();


                    marque.Nombre_opposition = rows[33]?.ToString();
                    list_marque.Add(marque);
                }
            }
            else
            {
                Response.Redirect("Recherche Bd.aspx");
            }
            if (list_marque.Count != 0)
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");
                foreach (var item in list_marque)
                {

                    if (!string.IsNullOrWhiteSpace(item.Numero_titre))
                    {
                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{item.Numero_titre}.jpg"))
                        {
                            item.Image = $@"{item.Numero_titre}.jpg";
                        }
                        else
                        {
                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{item.Numero_titre}.JPG"))
                            {
                                item.Image = $@"{item.Numero_titre}.JPG";
                            }
                            else
                            {
                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{item.Numero_titre}.jpeg"))
                                {
                                    item.Image = $@"{item.Numero_titre}.jpeg";
                                }
                                else
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{item.Numero_titre}.png"))
                                    {
                                        item.Image = $@"{item.Numero_titre}.png";
                                    }
                                    else
                                    {
                                        try
                                        {
                                            webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{item.Numero_titre}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{item.Numero_titre}.jpg");
                                            item.Image = $@"{item.Numero_titre}.jpg";
                                        }
                                        catch (Exception exd)
                                        {
                                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\temp_{item.Numero_titre}.jpg"))
                                            {
                                                item.Image = $@"temp_{item.Numero_titre}.jpg";
                                            }
                                            else
                                            {
                                                Bitmap bmp = new Bitmap(600, 600);
                                                RectangleF rectf = new RectangleF(0, 0, bmp.Width, bmp.Height);
                                                Graphics g = Graphics.FromImage(bmp);
                                                RectangleF rectff = new RectangleF(0, 600, 15, 15);

                                                g.SmoothingMode = SmoothingMode.AntiAlias;
                                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;


                                                StringFormat format = new StringFormat()
                                                {
                                                    Alignment = StringAlignment.Center,
                                                    LineAlignment = StringAlignment.Center
                                                };
                                                StringFormat format2 = new StringFormat()
                                                {
                                                    Alignment = StringAlignment.Near,
                                                    LineAlignment = StringAlignment.Near
                                                };
                                                g.FillRectangle(Brushes.White, rectf);
                                                g.DrawString("**", new Font("Tahoma", 60), Brushes.Red, rectf, format2);
                                                g.DrawString(item.Nom_marque, new Font("Tahoma", 60), Brushes.Black, rectf, format);
                                                g.Flush();
                                                
                                                MemoryStream ms = new MemoryStream();

                                                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                                                img.Save(Server.MapPath("~") + $@"\Assets\Brand_image\temp_" + item.Numero_titre + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                                item.Image = $@"temp_{item.Numero_titre}.jpg";
                                            }

                                        }









                                    }
                                }
                            }
                        }
                    }


                    //Bitmap bmp = new Bitmap(600, 600);
                    //RectangleF rectf = new RectangleF(0, 0, bmp.Width, bmp.Height);
                    //Graphics g = Graphics.FromImage(bmp);

                    //g.SmoothingMode = SmoothingMode.AntiAlias;
                    //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    //g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    //StringFormat format = new StringFormat()
                    //{
                    //    Alignment = StringAlignment.Center,
                    //    LineAlignment = StringAlignment.Center
                    //};
                    //g.FillRectangle(Brushes.White, rectf);
                    //g.DrawString(item.Nom_marque, new Font("Tahoma", 40), Brushes.Black, rectf, format);
                    //g.Flush();
                    //MemoryStream ms = new MemoryStream();

                    //bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    //img.Save(Server.MapPath("~") + $@"\Assets\Brand_image\temp_" + item.Numero_titre + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    //bmp.Save(ms,)
                    //item.Image = ms.ToArray();

                }
                bool col1 = GridView1.Columns[1].Visible;
                bool col2 = GridView1.Columns[2].Visible;
                bool col3 = GridView1.Columns[3].Visible;
                bool col4 = GridView1.Columns[4].Visible;
                bool col5 = GridView1.Columns[5].Visible;
                bool col6 = GridView1.Columns[6].Visible;
                bool col7 = GridView1.Columns[7].Visible;
                bool col8 = GridView1.Columns[8].Visible;
                bool col9 = GridView1.Columns[9].Visible;
                bool col10 = GridView1.Columns[10].Visible;
                bool col11 = GridView1.Columns[11].Visible;
                bool col12 = GridView1.Columns[12].Visible;
                bool col13 = GridView1.Columns[13].Visible;
                bool col14 = GridView1.Columns[14].Visible;
                bool col15 = GridView1.Columns[15].Visible;
                bool col16 = GridView1.Columns[16].Visible;
                bool col17 = GridView1.Columns[17].Visible;
                bool col18 = GridView1.Columns[18].Visible;
                bool col19 = GridView1.Columns[19].Visible;
                GridView1.Columns[1].Visible = true;
                GridView1.Columns[2].Visible = true;
                GridView1.Columns[3].Visible = true;
                GridView1.Columns[4].Visible = true;
                GridView1.Columns[5].Visible = true;
                GridView1.Columns[6].Visible = true;
                GridView1.Columns[7].Visible = true;
                GridView1.Columns[8].Visible = true;
                GridView1.Columns[9].Visible = true;
                GridView1.Columns[10].Visible = true;
                GridView1.Columns[11].Visible = true;
                GridView1.Columns[12].Visible = true;
                GridView1.Columns[13].Visible = true;
                GridView1.Columns[14].Visible = true;
                GridView1.Columns[15].Visible = true;
                GridView1.Columns[16].Visible = true;
                GridView1.Columns[17].Visible = true;
                GridView1.Columns[18].Visible = true;
                GridView1.Columns[19].Visible = true;
                Session["BDMarques"] = list_marque;
                resultalbl.InnerText = list_marque.Count.ToString();
                GridView1.DataSource = list_marque.Take(100);
                GridView1.DataBind();
                ViewState["index"] = 0;
                double result = list_marque.Count / 100.0;
                int pages = int.Parse(Math.Ceiling(result).ToString());
                index.Text = 1 + " / " + (pages == 0 ? 1 : pages);
                GridView1.Columns[1].Visible = col1;
                GridView1.Columns[2].Visible = col2;
                GridView1.Columns[3].Visible = col3;
                GridView1.Columns[4].Visible = col4;
                GridView1.Columns[5].Visible = col5;
                GridView1.Columns[6].Visible = col6;
                GridView1.Columns[7].Visible = col7;
                GridView1.Columns[8].Visible = col8;
                GridView1.Columns[9].Visible = col9;
                GridView1.Columns[10].Visible = col10;
                GridView1.Columns[11].Visible = col11;
                GridView1.Columns[12].Visible = col12;
                GridView1.Columns[13].Visible = col13;
                GridView1.Columns[14].Visible = col14;
                GridView1.Columns[15].Visible = col15;
                GridView1.Columns[16].Visible = col16;
                GridView1.Columns[17].Visible = col17;
                GridView1.Columns[18].Visible = col18;
                GridView1.Columns[19].Visible = col19;
                ViewState["Id_Sort_dir"] = "ASC";
                ViewState["nom"] = "ASC";
                ViewState["deposant"] = "ASC";
                ViewState["datede"] = "ASC";
                ViewState["dateexp"] = "ASC";
                ViewState["statut"] = "ASC";
                ViewState["mondataire"] = "ASC";
                

            }

        }
    }
}

                //Driver.Cloqe();






            




















           

       
    
