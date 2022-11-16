using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opposition_Generateur.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Opposition_Generateur.Pages
{
    public partial class Historique : System.Web.UI.Page
    {
        public List<Opposition> oppositions = new List<Opposition>();
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

                    if (Session["Role"].ToString() == "user")
                    {
                        int acc_id = int.Parse(Session["Account_id"].ToString());
                        string cs = @"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=Ipp;Integrated Security=True";
                        SqlConnection sQLiteConnection = new SqlConnection(cs);
                        sQLiteConnection.Open();
                        SqlCommand sQLiteCommand = new SqlCommand();
                        sQLiteCommand.Connection = sQLiteConnection;
                        sQLiteCommand.CommandText = "select Opposition_id , N_depot_marque_anterieure ,Nom_marque_anterieure, Deposant_marque_anterieure, Nature_marque_anterieure,N_depot_marque_contester,Nom_marque_contester,Deposant_marque_contester,Nature_marque_contester,Date_creation,Cas_identite,Cas_inclusion,Cas_complementarite,Comparaison_signe,Appreciation_generale_risque_confusion , Fullname , Docfile from Oppositions op inner join Users us on us.User_id = op.User_id where op.User_id = @user_id";
                        sQLiteCommand.Parameters.AddWithValue("@user_id", acc_id);
                        var reader = sQLiteCommand.ExecuteReader();
                        List<Opposition> list = new List<Opposition>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Opposition opp = new Opposition();
                                opp.Opposition_id = int.Parse(reader[0].ToString());
                                opp.N_depot_marque_anterieure = reader[1].ToString();
                                opp.Nom_marque_anterieure = reader[2].ToString();
                                opp.Deposant_marque_anterieure = reader[3].ToString();
                                opp.Nature_marque_anterieure = reader[4].ToString();
                                opp.N_depot_marque_contester = reader[5].ToString();
                                opp.Nom_marque_contester = reader[6].ToString();
                                opp.Deposant_marque_contester = reader[7].ToString();
                                opp.Nature_marque_contester = reader[8].ToString();

                                opp.Date_creation = ((DateTime)reader[9]);
                                opp.Cas_identite = reader[10].ToString();
                                opp.Cas_inclusion = reader[11].ToString();
                                opp.Cas_complementarite = reader[12].ToString();
                                opp.Comparaison_signe = reader[13].ToString();
                                opp.Appreciation_generale_risque_confusion = reader[14].ToString();
                                opp.Creer_par = reader[15].ToString();

                                list.Add(opp);
                            }
                            reader.Close();
                        }
                        sQLiteConnection.Close();
                        Session["Historique"] = list;
                        GridView1.DataSource = list.Take(8);
                        GridView1.DataBind();
                        ViewState["index"] = 0;
                        double result = list.Count / 8.0;
                        int pages = int.Parse(Math.Ceiling(result).ToString());
                        index.Text = 1 + " / " + (pages == 0 ? 1 : pages);
                    }
                    else
                    {
                        int acc_id = int.Parse(Session["Account_id"].ToString());
                        string cs = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
                        SqlConnection sQLiteConnection = new SqlConnection(cs);
                        sQLiteConnection.Open();
                        SqlCommand sQLiteCommand = new SqlCommand();
                        sQLiteCommand.Connection = sQLiteConnection;
                        sQLiteCommand.CommandText = "select Opposition_id , N_depot_marque_anterieure ,Nom_marque_anterieure, Deposant_marque_anterieure, Nature_marque_anterieure,N_depot_marque_contester,Nom_marque_contester,Deposant_marque_contester,Nature_marque_contester,Date_creation,Cas_identite,Cas_inclusion,Cas_complementarite,Comparaison_signe,Appreciation_generale_risque_confusion , Fullname , Docfile from Oppositions op inner join Users us on us.User_id = op.User_id";

                        var reader = sQLiteCommand.ExecuteReader();
                        List<Opposition> list = new List<Opposition>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Opposition opp = new Opposition();
                                opp.Opposition_id = int.Parse(reader[0].ToString());
                                opp.N_depot_marque_anterieure = reader[1].ToString();
                                opp.Nom_marque_anterieure = reader[2].ToString();
                                opp.Deposant_marque_anterieure = reader[3].ToString();
                                opp.Nature_marque_anterieure = reader[4].ToString();
                                opp.N_depot_marque_contester = reader[5].ToString();
                                opp.Nom_marque_contester = reader[6].ToString();
                                opp.Deposant_marque_contester = reader[7].ToString();
                                opp.Nature_marque_contester = reader[8].ToString();

                                opp.Date_creation = ((DateTime)reader[9]);
                                opp.Cas_identite = reader[10].ToString();
                                opp.Cas_inclusion = reader[11].ToString();
                                opp.Cas_complementarite = reader[12].ToString();
                                opp.Comparaison_signe = reader[13].ToString();
                                opp.Appreciation_generale_risque_confusion = reader[14].ToString();
                                opp.Creer_par = reader[15].ToString();
                                opp.Fichier = (byte[])reader[16];
                                list.Add(opp);
                            }
                            reader.Close();
                        }
                        sQLiteConnection.Close();
                        Session["Historique"] = list;
                        GridView1.DataSource = list.Take(8);
                        GridView1.DataBind();
                        ViewState["index"] = 0;
                        double result = list.Count / 8.0;
                        int pages = int.Parse(Math.Ceiling(result).ToString());
                        index.Text = 1 + " / " + (pages == 0 ? 1 : pages);
                    }

                }
                else
                {
                    Response.Redirect("Authentification.aspx");
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

        protected void btn_Resultat_Click(object sender, EventArgs e)
        {
            Response.Redirect("Resultat.aspx");
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
        protected void Archive_Click(object sender, EventArgs e)
        {
            Response.Redirect("archive.aspx");
        }
        protected void btn_Historique_Click(object sender, EventArgs e)
        {
            Response.Redirect("Historique.aspx");
        }
        protected void Rech_Opps_Click(object sender, EventArgs e)
        {
            Response.Redirect("Rechercheopps.aspx");
        }
        protected void btn_Deconnecter_Click(object sender, EventArgs e)
        {
            Session["Account_id"] = -1;
            Session["Role"] = "";
            Response.Cookies.Clear();
            Response.Redirect("Authentification.aspx");
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            Button Delete = sender as Button;
            GridViewRow gridViewRow = Delete.NamingContainer as GridViewRow;
            string cs = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            SqlConnection sQLiteConnection = new SqlConnection(cs);
            sQLiteConnection.Open();
            SqlCommand sQLiteCommand = new SqlCommand();
            sQLiteCommand.Connection = sQLiteConnection;
            sQLiteCommand.CommandText = "delete from Oppositions where Opposition_id = @opp_id";
            sQLiteCommand.Parameters.AddWithValue("@opp_id", int.Parse(gridViewRow.Cells[0].Text));
            sQLiteCommand.ExecuteNonQuery();
            sQLiteConnection.Close();
            Response.Redirect("Historique.aspx");
        }

        protected void Viewarg_Click(object sender, EventArgs e)
        {
            Button ShowArgs = sender as Button;
            GridViewRow gridViewRow = ShowArgs.NamingContainer as GridViewRow;
            ViewState["gridrowIndex"] = (int.Parse(ViewState["index"].ToString()) * 8 + gridViewRow.RowIndex).ToString();
            List<Opposition> list = Session["Historique"] as List<Opposition>;
            cas_identite.Value = list[int.Parse(ViewState["gridrowIndex"].ToString())].Cas_identite;
            cas_inclusion.Value = list[int.Parse(ViewState["gridrowIndex"].ToString())].Cas_inclusion;
            cas_complementarite.Value = list[int.Parse(ViewState["gridrowIndex"].ToString())].Cas_complementarite;
            comp_signe.Value = list[int.Parse(ViewState["gridrowIndex"].ToString())].Comparaison_signe;
            appre_gen.Value = list[int.Parse(ViewState["gridrowIndex"].ToString())].Appreciation_generale_risque_confusion;
            Args.Attributes.Add("style", "top: 0px;");

        }

        protected void Download_Click(object sender, EventArgs e)
        {
            List<Opposition> list = Session["Historique"] as List<Opposition>;
            Button ShowArgs = sender as Button;
            GridViewRow gridViewRow = ShowArgs.NamingContainer as GridViewRow;
            ViewState["gridrowIndex"] = (int.Parse(ViewState["index"].ToString()) * 8 + gridViewRow.RowIndex).ToString();
            Response.AddHeader("Content-Length", list[int.Parse(ViewState["gridrowIndex"].ToString())].Fichier.Length.ToString());
            Response.AddHeader("Content-Disposition", $"attachment; filename={list[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_anterieure}-{list[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester}.docx");
            Response.OutputStream.Write(list[int.Parse(ViewState["gridrowIndex"].ToString())].Fichier, 0, list[int.Parse(ViewState["gridrowIndex"].ToString())].Fichier.Length);
            Response.End();
        }

        protected void Precedent_Click(object sender, EventArgs e)
        {
            if (Session["Historique"] != null)
            {
                List<Opposition> Oppositions = Session["Historique"] as List<Opposition>;
                int i = int.Parse(ViewState["index"].ToString());
                double result = Oppositions.Count / 8.0;
                int pages = int.Parse(Math.Ceiling(result).ToString());
                if (i > 0)
                {
                    i--;
                    GridView1.DataSource = Oppositions.Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["index"] = i;
                }
                Args.Style["top"] = "-1900px";
            }
        }

        protected void Suivant_Click(object sender, EventArgs e)
        {
            if (Session["Historique"] != null)
            {
                List<Opposition> Oppositions = Session["Historique"] as List<Opposition>;
                int i = int.Parse(ViewState["index"].ToString());
                double result = Oppositions.Count / 8.0;
                int pages = int.Parse(Math.Ceiling(result).ToString());
                if (i < pages - 1)
                {
                    i++;
                    GridView1.DataSource = Oppositions.Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["index"] = i;
                }
                Args.Style["top"] = "-1900px";
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

        protected void btn_recherche_phonetique_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche phonetique.aspx");
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