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
using OpenQA.Selenium.Support;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;


namespace Opposition_Generateur.Views
{
    public partial class strongvalid : System.Web.UI.Page
    {

        public List<Marque_TmOmpicModel> list_marque = new List<Marque_TmOmpicModel>();
        public List<Marque_TmOmpicModel> Empty_list = new List<Marque_TmOmpicModel>();
        SqlConnection con = new SqlConnection(@"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=Ipp;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["Account_id"] != null && Session["Role"] != null && (int)Session["Account_id"] != -1 && Session["Role"].ToString() != "")
                {
                    Page.MaintainScrollPositionOnPostBack = false;
                    HttpCookie httpCookie = Request.Cookies["Userinfo"];
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
                    Session.Remove("Oppschecked");
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

                }
                else
                {
                    Response.Redirect("Authentification.aspx");
                }
            }
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
           
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }
        protected void Rech_bd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche Bd.aspx");
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
        protected void Archive_Click(object sender, EventArgs e)
        {
            Response.Redirect("archive.aspx");
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

        public string classnice(string a)
        {
            string finalstring = "";
            int i = 0;
            int j = 10;
            string[] authorsList = a.Split(',');
            foreach (string classe in authorsList)
            {
                finalstring += classe + ",";
                if (i == j)
                {
                    finalstring += classe + ", \n";
                    j += 10;
                }

                i++;
            }




            return finalstring;
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            try
            {

                SqlCommand command = new SqlCommand();
                command.Connection = con;
                //command.CommandText = "select * from Oppositions where  ";
                List<string> list = new List<string>();

                if (!string.IsNullOrWhiteSpace(Request.Form["num_op"]))
                {
                    list.Add("Opposition_id like @num_op");
                    command.Parameters.AddWithValue("@num_op", "%" + Request.Form["num_op"].Trim() + "%");
                }

                if (!string.IsNullOrWhiteSpace(Request.Form["nom_marqcont"]))
                {
                    list.Add("Nom_marque_contester like @nomC");
                    command.Parameters.AddWithValue("@nomC", "%" + Request.Form["nom_marqcont"].Trim() + "%");
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["deposantcon"]))
                {
                    list.Add("Deposant_marque_contester like @deposantcon");
                    command.Parameters.AddWithValue("@deposantcon", "%" + Request.Form["deposantcon"].Trim() + "%");
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["Classe_nicecon"]))
                {
                    list.Add("NiceClassCN like @Classe_nicecon");
                    command.Parameters.AddWithValue("@Classe_nicecon", "%" + Request.Form["Classe_nicecon"].Trim() + "%");
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["nom_marqant"]))
                {
                    list.Add("Nom_marque_anterieure like @nom_marqant");
                    command.Parameters.AddWithValue("@nom_marqant", "%" + Request.Form["nom_marqant"].Trim() + "%");
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["deposantant"]))
                {
                    list.Add("Deposant_marque_anterieure like @deposantant");
                    command.Parameters.AddWithValue("@deposantant", "%" + Request.Form["deposantant"].Trim() + "%");
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["Classe_niceant"]))
                {
                    list.Add("NiceClassAN like @Classe_niceant");
                    command.Parameters.AddWithValue("@Classe_niceant", "%" + Request.Form["Classe_niceant"].Trim() + "%");
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["num_marqcont"]))
                {
                    list.Add("N_depot_marque_contester like @num_marqcont");
                    command.Parameters.AddWithValue("@num_marqcont", "%" + Request.Form["num_marqcont"].Trim() + "%");
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["mandatairecont"]))
                {
                    list.Add("MondataireCN like @mandatairecont");
                    command.Parameters.AddWithValue("@mandatairecont", "%" + Request.Form["mandatairecont"].Trim() + "%");
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["num_marqant"]))
                {
                    list.Add("N_depot_marque_anterieure like @num_marqant");
                    command.Parameters.AddWithValue("@num_marqant", "%" + Request.Form["num_marqant"].Trim() + "%");
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["mandataireant"]))
                {
                    list.Add("MondataireAN like @mandataireant");
                    command.Parameters.AddWithValue("@mandataireant", "%" + Request.Form["mandataireant"].Trim() + "%");
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["Etatcont"]))
                {
                    list.Add("Statut like @Etatcont");
                    command.Parameters.AddWithValue("@Etatcont", "%" + Request.Form["Etatcont"].Trim() + "%");
                }

                var Ompicquery = "select Opposition_id,N_depot_marque_contester,Nom_marque_contester,Deposant_marque_contester,NiceClassCN,MondataireCN,N_depot_marque_anterieure,Nom_marque_anterieure,Deposant_marque_anterieure,NiceClassAN,MondataireAN,Decision,Statut from Oppositions where ";

                if (list.Count > 0)
                {

                    for (int i = 0; i < list.Count; i++)
                    {
                        if ((list.Count - 1) == i)
                        {

                            Ompicquery += list[i];
                        }
                        else
                        {
                            Ompicquery += list[i] + " and ";
                        }
                    }
                }
                command.CommandText = Ompicquery;

                con.Open();

                var rdr = command.ExecuteReader();
                DataTable db = new DataTable();
                db.Load(rdr);
                con.Close();
                WebClient webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");
                if (db.Rows.Count != 0)
                {
                    List<oppsearch> Models = new List<oppsearch>();
                    foreach (DataRow row in db.Rows)
                    {
                        oppsearch op = new oppsearch();
                        op.num_op = row[0].ToString();
                        string classniceC = classnice(row[4].ToString());
                        string classniceA = classnice(row[9].ToString());
                        op.marque_c = "<strong>Nom Marque : </strong>" + row[2].ToString() + "<br/>" + "<strong>Numero Marque : </strong>" + row[1].ToString() + "<br/>" + "<strong>Déposent : </strong>" + row[3].ToString() + "<br/>" + "<strong>Mondataire : </strong>" + row[5].ToString() + "<br/>" + "<strong>Classe nice : </strong>" + /*row[4].ToString()*/classniceC;
                        op.marq_a = "<strong>Nom Marque : </strong>" + row[7].ToString() + "<br/>" + "<strong>Numero Marque : </strong>" + row[6].ToString() + "<br/>" + "<strong>Déposent : </strong>" + row[8].ToString() + "<br/>" + "<strong>Mondataire : </strong>" + row[10].ToString() + "<br/>" + "<strong>Classe nice : </strong>" + /*row[9].ToString()*/classniceA;
                        con.Open();
                        command.CommandText = "select  count(*) from Marques_Ompic where NumeroTitre = '" + row[1].ToString().Trim() + "'";
                        int ix = int.Parse(command.ExecuteScalar().ToString());
                        con.Close();
                        con.Open();
                        command.CommandText = "select  count(*) from Marques_Tm where ST13  = '" + row[1].ToString().Trim() + "'";
                        int ixe = int.Parse(command.ExecuteScalar().ToString());
                        con.Close();
                        if (ix != 0 || ixe != 0)
                        {
                            try
                            {
                                if (!string.IsNullOrWhiteSpace(row[1].ToString().Trim()))
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[1].ToString().Trim()}.jpg"))
                                    {
                                        op.ImageC = $@"{row[1].ToString().Trim()}.jpg";
                                    }
                                    else
                                    {
                                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[1].ToString().Trim()}.JPG"))
                                        {
                                            op.ImageC = $@"{row[1].ToString().Trim()}.JPG";
                                        }
                                        else
                                        {
                                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[1].ToString().Trim()}.jpeg"))
                                            {
                                                op.ImageC = $@"{row[1].ToString().Trim()}.jpeg";
                                            }
                                            else
                                            {
                                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[1].ToString().Trim()}.png"))
                                                {
                                                    op.ImageC = $@"{row[1].ToString().Trim()}.png";
                                                }
                                                else
                                                {
                                                    webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{row[1].ToString().Trim()}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{row[1].ToString().Trim()}.jpg");
                                                    op.ImageC = $@"{row[1].ToString().Trim()}.jpg";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception) { }
                        }
                        else
                        {
                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{op.ImageA = "t-" + row[1].ToString().Trim()}.jpg"))
                            {
                                op.ImageC = "t-" + row[1].ToString().Trim() + ".jpg";
                            }

                        }
                        con.Open();
                        command.CommandText = "select  count(*) from Marques_Ompic where NumeroTitre = '" + row[6].ToString().Trim() + "'";
                        int ixx = int.Parse(command.ExecuteScalar().ToString());
                        con.Close();
                        con.Open();
                        command.CommandText = "select  count(*) from Marques_Tm where ST13  = '" + row[6].ToString().Trim() + "'";
                        int ixxe = int.Parse(command.ExecuteScalar().ToString());
                        con.Close();
                        if (ixx != 0 || ixxe != 0)
                        {
                            //try
                            //{
                            if (!string.IsNullOrWhiteSpace(row[6].ToString().Trim()))
                            {
                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[6].ToString().Trim()}.jpg"))
                                {
                                    op.ImageA = $@"{row[6].ToString().Trim()}.jpg";
                                }
                                else
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[6].ToString().Trim()}.JPG"))
                                    {
                                        op.ImageA = $@"{row[6].ToString().Trim()}.JPG";
                                    }
                                    else
                                    {
                                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[6].ToString().Trim()}.jpeg"))
                                        {
                                            op.ImageA = $@"{row[6].ToString().Trim()}.jpeg";
                                        }
                                        else
                                        {
                                            //if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\t-{row[6].ToString().Trim()}.png"))
                                            //{
                                            //    op.ImageA = $@"{row[6].ToString().Trim()}.png";
                                            //}
                                            //else
                                            //{

                                            webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{row[1].ToString().Trim()}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{row[6].ToString().Trim()}.jpg");
                                            op.ImageA = $@"{row[6].ToString().Trim()}.jpg";


                                            //}
                                        }
                                    }
                                }
                            }
                            //}

                            //catch (Exception) { }
                        }

                        else
                        {
                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{op.ImageA = "t-" + row[6].ToString().Trim()}.jpg"))
                            {
                                op.ImageA = "t-" + row[6].ToString().Trim() + ".jpg";
                            }
                            else
                            {
                                try
                                {
                                    webClient.DownloadFile($"https://www3.wipo.int/madrid/monitor/jsp/data.jsp?KEY=ROM.{row[6].ToString().Trim()}&TYPE=jpg&qi=1-PS2TOI9DFDkXQAToGMd8/SAKSvCctEImcTosQOWerQs=", $@"C:\Users\IPPERFORMANCE\source\repos\filloppositions\filloppositions\Assets\t-{row[6].ToString().Trim()}.jpg");
                                    op.ImageA = $@"{row[1].ToString().Trim()}.jpg";
                                }
                                catch (Exception ex)
                                {
                                    op.ImageA = "Empty.png";
                                }

                            }
                        }

                        op.decision = row[11].ToString();
                        op.Statut = row[12].ToString();
                        Models.Add(op);
                    }
                    con.Close();
                    Session["oppositions"] = db;
                    Session["BDOPPS"] = Models;
                    GridView1.DataSource = Models;
                    GridView1.DataBind();
                }












            }
            catch (Exception ex)
            {
            }
        }
        protected void Precedent_Click(object sender, EventArgs e)
        {
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
        }

        protected void Suivant_Click(object sender, EventArgs e)
        {
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
        }

        protected void Afficher_Masquer_Click(object sender, EventArgs e)
        {
            if (Request.Form["columns_dropdown"] == "Nom_marque") { if (GridView1.Columns[2].Visible == false) { GridView1.Columns[2].Visible = true; } else { GridView1.Columns[2].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Date_depot") { if (GridView1.Columns[5].Visible == false) { GridView1.Columns[5].Visible = true; } else { GridView1.Columns[5].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Date_expiration") { if (GridView1.Columns[6].Visible == false) { GridView1.Columns[6].Visible = true; } else { GridView1.Columns[6].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Applicant_name") { if (GridView1.Columns[3].Visible == false) { GridView1.Columns[3].Visible = true; } else { GridView1.Columns[3].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Applicant_address") { if (GridView1.Columns[9].Visible == false) { GridView1.Columns[9].Visible = true; } else { GridView1.Columns[9].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Pays") { if (GridView1.Columns[10].Visible == false) { GridView1.Columns[10].Visible = true; } else { GridView1.Columns[10].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Representative_name") { if (GridView1.Columns[4].Visible == false) { GridView1.Columns[4].Visible = true; } else { GridView1.Columns[4].Visible = false; } }

            if (Request.Form["columns_dropdown"] == "Representative_address") { if (GridView1.Columns[11].Visible == false) { GridView1.Columns[11].Visible = true; } else { GridView1.Columns[11].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Representative_countryCode") { if (GridView1.Columns[12].Visible == false) { GridView1.Columns[12].Visible = true; } else { GridView1.Columns[12].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Type") { if (GridView1.Columns[13].Visible == false) { GridView1.Columns[13].Visible = true; } else { GridView1.Columns[13].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Statut") { if (GridView1.Columns[8].Visible == false) { GridView1.Columns[8].Visible = true; } else { GridView1.Columns[8].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Email") { if (GridView1.Columns[14].Visible == false) { GridView1.Columns[14].Visible = true; } else { GridView1.Columns[14].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Telephone") { if (GridView1.Columns[15].Visible == false) { GridView1.Columns[15].Visible = true; } else { GridView1.Columns[15].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "ClasseNice") { if (GridView1.Columns[7].Visible == false) { GridView1.Columns[7].Visible = true; } else { GridView1.Columns[7].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Opposition_applicant_name") { if (GridView1.Columns[16].Visible == false) { GridView1.Columns[16].Visible = true; } else { GridView1.Columns[16].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Opposition_earlierMark_applicationNumber") { if (GridView1.Columns[17].Visible == false) { GridView1.Columns[17].Visible = true; } else { GridView1.Columns[17].Visible = false; } }
            if (Request.Form["columns_dropdown"] == "Nombre_opposition") { if (GridView1.Columns[18].Visible == false) { GridView1.Columns[18].Visible = true; } else { GridView1.Columns[18].Visible = false; } }
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


        }
        public string retext(string db)
        {


            db = db.Replace(@"</strong>", "");
            db = db.Replace(@"<strong>", "");
            //db = db.Replace(@"<strong>Déposent : </strong>", "");
            //db = db.Replace(@"<strong>Mondataire : </strong>", "");
            //db = db.Replace(@"<strong>Classe nice : </strong>", "");
            db = db.Replace("\n", "");
            db = db.Replace(@"<br/>", "\n");
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
        //protected void Button1_Click1(object sender, EventArgs e)
        //{
        //    DataTable listdg = Session["oppositions"] as DataTable;
        //    Response.Write("hellllo");
        //    List<string> toupdate = new List<string>();
        //    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Data Source=DESKTOP-SCOF8PR\MSSQLSERVER01;Initial Catalog=Ipp2;Integrated Security=True");
        //    System.Data.SqlClient.SqlCommand sQLiteCommand = new System.Data.SqlClient.SqlCommand();
        //    System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
        //    sQLiteCommand.Connection = con;
        //    WebClient webClient = new WebClient();
        //    webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");
        //    sQLiteCommand.CommandText = "insert into Marques_Ompic(Nommarque,NumeroTitre,Datedepot,Type,Loi,Statut,NumeroPublication,Deposant,Mandataire,Dateexpiration,ClasseNice,Classedetails,MappingId,Adresse,Pays) values(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15)";
        //    ChromeDriver Driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions());
        //    OpenQA.Selenium.Support.UI.WebDriverWait wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, TimeSpan.FromSeconds(60));
        //    if (listdg.Rows.Count != 0)
        //    {
        //        Response.Write("hellllo");
        //        foreach (DataRow row in listdg.Rows)
        //        {
        //            //toupdate.Add(listdg.Rows[0][1].ToString().Trim());
        //            string marque = listdg.Rows[0][1].ToString().Trim();
        //            Driver.Navigate().GoToUrl($"http://www.directompic.ma/fr/renouv-marque");
        //            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[1]/div/input")));
        //            Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[1]/div/input")).SendKeys(marque);
        //            Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[2]/div/select")).Click();
        //            System.Threading.Thread.Sleep(1000);
        //            //if (!marque.Contains("-"))
        //            //{
        //            //    int index = int.Parse(marque);
        //            //    if (index > 99999)
        //            //    {
        //                    Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[2]/div/select/option[2]")).Click();
        //                    Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[7]/button")).Click();
        //            //    }
        //            //}

        //        }


        //        //List<oppsearch> Models = new List<oppsearch>();
        //        //foreach (DataRow row in listdg.Rows)
        //        //{
        //        //    oppsearch op = new oppsearch();
        //        //    op.num_op = row[0].ToString();
        //        //    string classniceC = classnice(row[4].ToString());
        //        //    string classniceA = classnice(row[9].ToString());
        //        //    op.marque_c = "<strong>Nom Marque : </strong>" + row[2].ToString() + "<br/>" + "<strong>Numero Marque : </strong>" + row[1].ToString() + "<br/>" + "<strong>Déposent : </strong>" + row[3].ToString() + "<br/>" + "<strong>Mondataire : </strong>" + row[5].ToString() + "<br/>" + "<strong>Classe nice : </strong>" + /*row[4].ToString()*/classniceC;
        //        //    op.marq_a = "<strong>Nom Marque : </strong>" + row[7].ToString() + "<br/>" + "<strong>Numero Marque : </strong>" + row[6].ToString() + "<br/>" + "<strong>Déposent : </strong>" + row[8].ToString() + "<br/>" + "<strong>Mondataire : </strong>" + row[10].ToString() + "<br/>" + "<strong>Classe nice : </strong>" + /*row[9].ToString()*/classniceA;
        //        //    con.Open();
        //        //    command.CommandText = "select  count(*) from Marques_Ompic where NumeroTitre = '" + row[1].ToString().Trim() + "'";
        //        //    int ix = int.Parse(command.ExecuteScalar().ToString());
        //        //    con.Close();
        //        //    if (ix != 0)
        //        //    {
        //        //        try
        //        //        {
        //        //            if (!string.IsNullOrWhiteSpace(row[1].ToString().Trim()))
        //        //            {
        //        //                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[1].ToString().Trim()}.jpg"))
        //        //                {
        //        //                    op.ImageC = $@"{row[1].ToString().Trim()}.jpg";
        //        //                }
        //        //                else
        //        //                {
        //        //                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[1].ToString().Trim()}.JPG"))
        //        //                    {
        //        //                        op.ImageC = $@"{row[1].ToString().Trim()}.JPG";
        //        //                    }
        //        //                    else
        //        //                    {
        //        //                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[1].ToString().Trim()}.jpeg"))
        //        //                        {
        //        //                            op.ImageC = $@"{row[1].ToString().Trim()}.jpeg";
        //        //                        }
        //        //                        else
        //        //                        {
        //        //                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[1].ToString().Trim()}.png"))
        //        //                            {
        //        //                                op.ImageC = $@"{row[1].ToString().Trim()}.png";
        //        //                            }
        //        //                            else
        //        //                            {
        //        //                                webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{row[1].ToString().Trim()}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{row[1].ToString().Trim()}.jpg");
        //        //                                op.ImageC = $@"{row[1].ToString().Trim()}.jpg";
        //        //                            }
        //        //                        }
        //        //                    }
        //        //                }
        //        //            }
        //        //        }
        //        //        catch (Exception) { }
        //        //    }
        //        //    else
        //        //    {
        //        //        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{op.ImageA = "t-" + row[1].ToString().Trim()}.jpg"))
        //        //        {
        //        //            op.ImageC = "t-" + row[1].ToString().Trim() + ".jpg";
        //        //        }

        //        //    }
        //        //    con.Open();
        //        //    command.CommandText = "select  count(*) from Marques_Ompic where NumeroTitre = '" + row[6].ToString().Trim() + "'";
        //        //    int ixx = int.Parse(command.ExecuteScalar().ToString());
        //        //    con.Close();
        //        //    if (ixx != 0)
        //        //    {
        //        //        try
        //        //        {
        //        //            if (!string.IsNullOrWhiteSpace(row[6].ToString().Trim()))
        //        //            {
        //        //                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[6].ToString().Trim()}.jpg"))
        //        //                {
        //        //                    op.ImageA = $@"{row[6].ToString().Trim()}.jpg";
        //        //                }
        //        //                else
        //        //                {
        //        //                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[6].ToString().Trim()}.JPG"))
        //        //                    {
        //        //                        op.ImageA = $@"{row[6].ToString().Trim()}.JPG";
        //        //                    }
        //        //                    else
        //        //                    {
        //        //                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[6].ToString().Trim()}.jpeg"))
        //        //                        {
        //        //                            op.ImageA = $@"{row[6].ToString().Trim()}.jpeg";
        //        //                        }
        //        //                        else
        //        //                        {
        //        //                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\t-{row[6].ToString().Trim()}.png"))
        //        //                            {
        //        //                                op.ImageA = $@"{row[6].ToString().Trim()}.png";
        //        //                            }
        //        //                            else
        //        //                            {
        //        //                                try
        //        //                                {
        //        //                                    webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{row[1].ToString().Trim()}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{row[6].ToString().Trim()}.jpg");
        //        //                                    op.ImageA = $@"{row[6].ToString().Trim()}.jpg";
        //        //                                }
        //        //                                catch (Exception ex)
        //        //                                {
        //        //                                    op.ImageA = "Empty.png";
        //        //                                }

        //        //                            }
        //        //                        }
        //        //                    }
        //        //                }
        //        //            }
        //        //        }
        //        //        catch (Exception) { }
        //        //    }
        //        //    else
        //        //    {
        //        //        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{op.ImageA = "t-" + row[6].ToString().Trim()}.jpg"))
        //        //        {
        //        //            op.ImageA = "t-" + row[6].ToString().Trim() + ".jpg";
        //        //        }
        //        //        else
        //        //        {
        //        //            try
        //        //            {
        //        //                webClient.DownloadFile($"https://www3.wipo.int/madrid/monitor/jsp/data.jsp?KEY=ROM.{row[6].ToString().Trim()}&TYPE=jpg&qi=1-PS2TOI9DFDkXQAToGMd8/SAKSvCctEImcTosQOWerQs=", $@"C:\Users\IPPERFORMANCE\source\repos\filloppositions\filloppositions\Assets\t-{row[6].ToString().Trim()}.jpg");
        //        //                op.ImageA = $@"{row[1].ToString().Trim()}.jpg";
        //        //            }
        //        //            catch (Exception ex)
        //        //            {
        //        //                op.ImageA = "Empty.png";
        //        //            }

        //        //        }
        //        //    }
        //        //    op.decision = row[11].ToString();
        //        //    op.Statut = row[12].ToString();
        //        //    Models.Add(op);
        //        //}
        //        //con.Close();

        //        //GridView1.DataSource = Models;
        //        //GridView1.DataBind();









        //    }


        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable listdg = Session["oppositions"] as DataTable;

                List<string> toupdate = new List<string>();
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=Ipp;Integrated Security=True");
                System.Data.SqlClient.SqlCommand sQLiteCommand = new System.Data.SqlClient.SqlCommand();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                sQLiteCommand.Connection = con;
                WebClient webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");

                ChromeDriver Driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions());
                OpenQA.Selenium.Support.UI.WebDriverWait wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, TimeSpan.FromSeconds(60));
                if (listdg.Rows.Count != 0)
                {

                    //    Driver.Navigate().GoToUrl($"https://www.google.com/");
                    //    Driver.Navigate().GoToUrl($"http://www.directompic.ma/fr");
                    //    System.Threading.Thread.Sleep(10000);
                    //}
                    //catch(Exception ed)
                    //{
                    //    Driver.Navigate().GoToUrl($"http://www.directompic.ma/fr");
                    //}

                    try
                    {
                        Driver.Navigate().GoToUrl($"http://www.directompic.ma/");


                    }
                    catch (Exception)
                    {
                        Page.RegisterStartupScript("Key", "<script type='text/javascript'>window.onload = function(){alert('Update faled');return false;}</script>");

                    }





                    foreach (DataRow row in listdg.Rows)
                    {
                        try
                        {
                            //toupdate.Add(listdg.Rows[0][1].ToString().Trim());
                            string marque = row[1].ToString().Trim();




                            Driver.Navigate().GoToUrl($"http://www.directompic.ma/fr/renouv-marque");

                            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[1]/div/input")));


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
                                    row[12] = Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/fieldset/div[2]/table/tbody/tr/td[5]")).Text;
                                    command.CommandText = "UPDATE Oppositions SET Statut = '" + row[12].ToString() + "' where Opposition_id ='" + row[0].ToString() + "'";
                                    con.Open();
                                    command.ExecuteNonQuery();
                                    command.CommandText = "UPDATE Marques_Ompic SET Statut = '" + row[12].ToString() + "' where NumeroTitre ='" + row[1].ToString().Trim() + "'";
                                    command.ExecuteNonQuery();
                                    con.Close();
                                }
                                else
                                {
                                    try
                                    {
                                        Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[2]/div/select/option[3]")).Click();
                                        Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[7]/button")).Click();
                                        row[12] = Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/fieldset/div[2]/table/tbody/tr/td[5]")).Text;
                                        command.CommandText = "UPDATE Oppositions SET Statut = '" + row[12].ToString() + "' where Opposition_id ='" + row[0].ToString() + "'";
                                        con.Open();
                                        command.ExecuteNonQuery();
                                        command.CommandText = "UPDATE Marques_Ompic SET Statut = '" + row[12].ToString() + "' where NumeroTitre ='" + row[1].ToString().Trim() + "'";
                                        command.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[2]/div/select/option[4]")).Click();
                                            Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[7]/button")).Click();
                                            row[12] = Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/fieldset/div[2]/table/tbody/tr/td[5]")).Text;
                                            command.CommandText = "UPDATE Oppositions SET Statut = '" + row[12].ToString() + "' where Opposition_id ='" + row[0].ToString() + "'";
                                            con.Open();
                                            command.ExecuteNonQuery();
                                            command.CommandText = "UPDATE Marques_Ompic SET Statut = '" + row[12].ToString() + "' where NumeroTitre ='" + row[1].ToString().Trim() + "'";
                                            command.ExecuteNonQuery();
                                            con.Close();
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
                                    row[12] = Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/fieldset/div[2]/table/tbody/tr/td[5]")).Text;
                                    command.CommandText = "UPDATE Oppositions SET Statut = '" + row[12].ToString() + "' where Opposition_id ='" + row[0].ToString() + "'";
                                    con.Open();
                                    command.ExecuteNonQuery();
                                    command.CommandText = "UPDATE Marques_Ompic SET Statut = '" + row[12].ToString() + "' where NumeroTitre ='" + row[1].ToString().Trim() + "'";
                                    command.ExecuteNonQuery();
                                    con.Close();
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[2]/div/select/option[4]")).Click();
                                        Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/div/fieldset/form/div[7]/button")).Click();
                                        row[12] = Driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/div/div/div/div[2]/fieldset/div[2]/table/tbody/tr/td[5]")).Text;
                                        command.CommandText = "UPDATE Oppositions SET Statut = '" + row[12].ToString() + "' where Opposition_id ='" + row[0].ToString() + "'";
                                        con.Open();
                                        command.ExecuteNonQuery();
                                        command.CommandText = "UPDATE Marques_Ompic SET Statut = '" + row[12].ToString() + "' where NumeroTitre ='" + row[1].ToString().Trim() + "'";
                                        command.ExecuteNonQuery();
                                        con.Close();

                                    }
                                    catch (Exception exs)
                                    {
                                        continue;
                                    }
                                }
                            }





                        }
                        catch (Exception ett)
                        {

                        }

                    }
                    Driver.Close();


                    List<oppsearch> Models = new List<oppsearch>();
                    foreach (DataRow row in listdg.Rows)
                    {
                        oppsearch op = new oppsearch();
                        op.num_op = row[0].ToString();
                        string classniceC = classnice(row[4].ToString());
                        string classniceA = classnice(row[9].ToString());
                        op.marque_c = "<strong>Nom Marque : </strong>" + row[2].ToString() + "<br/>" + "<strong>Numero Marque : </strong>" + row[1].ToString() + "<br/>" + "<strong>Déposent : </strong>" + row[3].ToString() + "<br/>" + "<strong>Mondataire : </strong>" + row[5].ToString() + "<br/>" + "<strong>Classe nice : </strong>" + /*row[4].ToString()*/classniceC;
                        op.marq_a = "<strong>Nom Marque : </strong>" + row[7].ToString() + "<br/>" + "<strong>Numero Marque : </strong>" + row[6].ToString() + "<br/>" + "<strong>Déposent : </strong>" + row[8].ToString() + "<br/>" + "<strong>Mondataire : </strong>" + row[10].ToString() + "<br/>" + "<strong>Classe nice : </strong>" + /*row[9].ToString()*/classniceA;
                        con.Open();
                        command.CommandText = "select  count(*) from Marques_Ompic where NumeroTitre = '" + row[1].ToString().Trim() + "'";
                        int ix = int.Parse(command.ExecuteScalar().ToString());
                        con.Close();
                        con.Open();
                        command.CommandText = "select  count(*) from Marques_Tm where ST13  = '" + row[1].ToString().Trim() + "'";
                        int ixe = int.Parse(command.ExecuteScalar().ToString());
                        con.Close();
                        if (ix != 0 || ixe != 0)
                        {
                            try
                            {
                                if (!string.IsNullOrWhiteSpace(row[1].ToString().Trim()))
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[1].ToString().Trim()}.jpg"))
                                    {
                                        op.ImageC = $@"{row[1].ToString().Trim()}.jpg";
                                    }
                                    else
                                    {
                                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[1].ToString().Trim()}.JPG"))
                                        {
                                            op.ImageC = $@"{row[1].ToString().Trim()}.JPG";
                                        }
                                        else
                                        {
                                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[1].ToString().Trim()}.jpeg"))
                                            {
                                                op.ImageC = $@"{row[1].ToString().Trim()}.jpeg";
                                            }
                                            else
                                            {
                                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[1].ToString().Trim()}.png"))
                                                {
                                                    op.ImageC = $@"{row[1].ToString().Trim()}.png";
                                                }
                                                else
                                                {
                                                    webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{row[1].ToString().Trim()}.jpg", Server.MapPath("~") + $@"C:\inetpub\wwwroot\IppApp\Assets\Brand_image\{row[1].ToString().Trim()}.jpg");
                                                    op.ImageC = $@"{row[1].ToString().Trim()}.jpg";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception) { }
                        }
                        else
                        {
                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{op.ImageA = "t-" + row[1].ToString().Trim()}.jpg"))
                            {
                                op.ImageC = "t-" + row[1].ToString().Trim() + ".jpg";
                            }

                        }
                        con.Open();
                        command.CommandText = "select  count(*) from Marques_Ompic where NumeroTitre = '" + row[6].ToString().Trim() + "'";
                        int ixx = int.Parse(command.ExecuteScalar().ToString());
                        con.Close();
                        con.Open();
                        command.CommandText = "select  count(*) from Marques_Tm where ST13  = '" + row[6].ToString().Trim() + "'";
                        int ixxe = int.Parse(command.ExecuteScalar().ToString());
                        con.Close();
                        if (ixx != 0 || ixxe != 0)
                        {
                            try
                            {
                                if (!string.IsNullOrWhiteSpace(row[6].ToString().Trim()))
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[6].ToString().Trim()}.jpg"))
                                    {
                                        op.ImageA = $@"{row[6].ToString().Trim()}.jpg";
                                    }
                                    else
                                    {
                                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[6].ToString().Trim()}.JPG"))
                                        {
                                            op.ImageA = $@"{row[6].ToString().Trim()}.JPG";
                                        }
                                        else
                                        {
                                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{row[6].ToString().Trim()}.jpeg"))
                                            {
                                                op.ImageA = $@"{row[6].ToString().Trim()}.jpeg";
                                            }
                                            else
                                            {
                                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\t-{row[6].ToString().Trim()}.png"))
                                                {
                                                    op.ImageA = $@"{row[6].ToString().Trim()}.png";
                                                }
                                                else
                                                {
                                                    try
                                                    {
                                                        webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{row[1].ToString().Trim()}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{row[6].ToString().Trim()}.jpg");
                                                        op.ImageA = $@"{row[6].ToString().Trim()}.jpg";
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        op.ImageA = "Empty.png";
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception) { }
                        }
                        else
                        {
                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{op.ImageA = "t-" + row[6].ToString().Trim()}.jpg"))
                            {
                                op.ImageA = "t-" + row[6].ToString().Trim() + ".jpg";
                            }
                            else
                            {
                                try
                                {
                                    webClient.DownloadFile($"https://www3.wipo.int/madrid/monitor/jsp/data.jsp?KEY=ROM.{row[6].ToString().Trim()}&TYPE=jpg&qi=1-PS2TOI9DFDkXQAToGMd8/SAKSvCctEImcTosQOWerQs=", $@"C:\Users\IPPERFORMANCE\source\repos\filloppositions\filloppositions\Assets\t-{row[6].ToString().Trim()}.jpg");
                                    op.ImageA = $@"{row[1].ToString().Trim()}.jpg";
                                }
                                catch (Exception ex)
                                {
                                    op.ImageA = "Empty.png";
                                }

                            }
                        }
                        op.decision = row[11].ToString();
                        op.Statut = row[12].ToString();
                        Models.Add(op);
                    }
                    con.Close();

                    GridView1.DataSource = Models;
                    GridView1.DataBind();


                }
            }
            catch (Exception ex)
            {
                Page.RegisterStartupScript("Key", "<script type='text/javascript'>window.onload = function(){alert('failed to update');return false;}</script>");
            }
        }

        protected void PDF_Click(object sender, EventArgs e)
        {

            List<oppsearch> Models = Session["BDOPPS"] as List<oppsearch>;


            System.Data.DataSet ds = new System.Data.DataSet("DataTable1");
            DataTable table = new DataTable("Dataopp");

            table.Columns.Add("Numopposition");
            table.Columns.Add("marqueconteste");
            table.Columns.Add("imagecontes");
            table.Columns.Add("imageant");
            table.Columns.Add("marqueant");
            table.Columns.Add("desicion");
            table.Columns.Add("statut");

            table.Columns["imagecontes"].DataType = System.Type.GetType("System.Byte[]");
            table.Columns["imageant"].DataType = System.Type.GetType("System.Byte[]");

            foreach (oppsearch marque in Models)
            {
                var ms = new MemoryStream();
                var mt = new MemoryStream();
                DataRow row = table.NewRow();




                string imgC = marque.ImageC;
                string imgA = marque.ImageA;


                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgC))
                {
                    System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgC).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\Empty.png").Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgA))
                {
                    System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgA).Save(mt, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\Empty.png").Save(mt, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                row["Numopposition"] = marque.num_op;
                row["imagecontes"] = ms.ToArray();
                row["imageant"] = mt.ToArray();
                row["marqueconteste"] = retext(marque.marque_c);
                row["marqueant"] = retext(marque.marq_a);
                row["desicion"] = marque.decision;
                row["statut"] = marque.Statut;
                table.Rows.Add(row);
            }
            ds.Tables.Add(table);
            CrystalReport4 report = new CrystalReport4();
            report.Database.Tables["Dataopp"].SetDataSource(ds);
            string filename = $"opps_export.pdf";
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

        protected void PDFSELECT_Click(object sender, EventArgs e)
        {

            if (Session["Oppschecked"] == null)
            {
                List<string> oppsidchecked = new List<string>();



                foreach (GridViewRow row in GridView1.Rows)
                {
                    //if (item.checkede == "true")
                    //{
                    //    gh++;
                    //}


                    CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                    if (cb.Checked == true)
                    {
                        if (!oppsidchecked.Contains(row.Cells[1].Text))
                        {
                            oppsidchecked.Add(row.Cells[1].Text);
                        }

                    }
                    else
                    {
                        if (oppsidchecked.Contains(row.Cells[1].Text))
                        {
                            oppsidchecked.Remove(row.Cells[1].Text);
                        }
                    }



                }
                Session["Oppschecked"] = oppsidchecked;
            }
            else
            {
                List<string> oppsidchecked = Session["Oppschecked"] as List<string>;


                foreach (GridViewRow row in GridView1.Rows)
                {
                    //if (item.checkede == "true")
                    //{
                    //    gh++;
                    //}


                    CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                    if (cb.Checked == true)
                    {
                        if (!oppsidchecked.Contains(row.Cells[1].Text))
                        {
                            oppsidchecked.Add(row.Cells[1].Text);
                        }

                    }
                    else
                    {
                        if (oppsidchecked.Contains(row.Cells[1].Text))
                        {
                            oppsidchecked.Remove(row.Cells[1].Text);
                        }
                    }



                }
            }

            List<oppsearch> Models = Session["BDOPPS"] as List<oppsearch>;

            List<string> oppsidcheckedd = Session["Oppschecked"] as List<string>;
            System.Data.DataSet ds = new System.Data.DataSet("DataTable1");
            DataTable table = new DataTable("Dataopp");

            table.Columns.Add("Numopposition");
            table.Columns.Add("marqueconteste");
            table.Columns.Add("imagecontes");
            table.Columns.Add("imageant");
            table.Columns.Add("marqueant");
            table.Columns.Add("desicion");
            table.Columns.Add("statut");

            table.Columns["imagecontes"].DataType = System.Type.GetType("System.Byte[]");
            table.Columns["imageant"].DataType = System.Type.GetType("System.Byte[]");

            foreach (oppsearch marque in Models)
            {
                if (oppsidcheckedd.Contains(marque.num_op))
                {
                    var ms = new MemoryStream();
                    var mt = new MemoryStream();
                    DataRow row = table.NewRow();




                    string imgC = marque.ImageC;
                    string imgA = marque.ImageA;


                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgC))
                    {
                        System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgC).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else
                    {
                        System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\Empty.png").Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgA))
                    {
                        System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgA).Save(mt, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else
                    {
                        System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\Empty.png").Save(mt, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    row["Numopposition"] = marque.num_op;
                    row["imagecontes"] = ms.ToArray();
                    row["imageant"] = mt.ToArray();
                    row["marqueconteste"] = retext(marque.marque_c);
                    row["marqueant"] = retext(marque.marq_a);
                    row["desicion"] = marque.decision;
                    row["statut"] = marque.Statut;
                    table.Rows.Add(row);
                }
            }
            ds.Tables.Add(table);
            CrystalReport4 report = new CrystalReport4();
            report.Database.Tables["Dataopp"].SetDataSource(ds);
            string filename = $"opps_export.pdf";
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


        //protected void PDFex(object sender, EventArgs e)
        //{

        //}
    }
}
