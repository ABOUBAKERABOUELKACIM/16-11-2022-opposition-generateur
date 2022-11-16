using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Opposition_Generateur.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Opposition_Generateur.Views
{
    public partial class Recherche_ompic : System.Web.UI.Page
    {
        public List<Marque> Empty_list = new List<Marque>();
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
                    Session.Remove("Rech_ompic_list_marque");
                    Session.Remove("marques similaire");
                    Session.Remove("marques ip report");
                    Session.Remove("alerte");
                    Session.Remove("index");
                    Session.Remove("pages");
                    Session.Remove("Old_marques_ipreport");
                    Session.Remove("Old_marques_similaire");

                    GridView1.DataSource = Empty_list;
                    GridView1.DataBind();
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
        protected void Rech_Opps_Click(object sender, EventArgs e)
        {
            Response.Redirect("Rechercheopps.aspx");
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
        protected void btn_Historique_Click(object sender, EventArgs e)
        {
            Response.Redirect("Historique.aspx");
        }

        protected void btn_ajouter_alerte_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ajouter alerte.aspx");
        }

        protected void btn_generer_doc_Click(object sender, EventArgs e)
        {
            Response.Redirect("Generer pdf.aspx");
        }

        protected void btn_Deconnecter_Click(object sender, EventArgs e)
        {
            Session["Account_id"] = -1;
            Session["Role"] = "";
            Response.Cookies.Clear();
            Response.Redirect("Authentification.aspx");
        }
        protected void Archive_Click(object sender, EventArgs e)
        {
            Response.Redirect("archive.aspx");
        }

        protected async void Search_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    WebClient webClient = new WebClient();
                    webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");
                    string type_rech = "1";
                    if (Request.Form["Rech_exacte"] == "on")
                    {
                        type_rech = "1";
                    }
                    if (Request.Form["Rech_phonetique"] == "on")
                    {
                        type_rech = "0";
                    }
                    var values = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/x-www-form-urlencoded" },
                        { "nomMarque", Request.Form["nom_marq"] },
                        { "typeRech", type_rech },
                        { "codeNice", Request.Form["classe_nice"] },
                        { "numeroDepot", Request.Form["num_depot"] },
                        { "dateDepotDeb", Request.Form["date_depot_debut"] },
                        { "dateDepotFin", Request.Form["date_depot_fin"] },
                        { "etatMarque", Request.Form["etatMarque"] },
                        { "refPriorite", Request.Form["numero_priorite"] },
                        { "nomDeposant", Request.Form["titulaire"] },
                        { "inCaptchaChars", "1171" },
                        { "hidCaptchaID", "E5D420F44FEB498C8824216E929960AE" }
                    };
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");
                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync("http://search.ompic.ma/web/pages/rechercheMarque.do", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    List<string> list = new List<string>();
                    var sr = new StringReader(responseString);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("consulterDetail") && line.Contains("new Array"))
                        {
                            list.Add(line.Split(',')[0].Split('(')[1].Replace('"', ' ').Trim());
                        }
                    }

                    if (list.Count > 0)
                    {
                        List<Marque> list_marque = new List<Marque>();
                        ChromeDriver Driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions(), TimeSpan.FromSeconds(120));
                        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                        DateTime dt = new DateTime();
                        foreach (var item in list)
                        {
                            try
                            {
                                Driver.Navigate().GoToUrl($"http://search.ompic.ma/web/pages/consulterMarque.do?id={item}");
                                string[] tab;
                                List<string> list_nice = new List<string>();
                                string temp = "";
                                Marque marque = new Marque();
                                marque.Id = int.Parse(item);
                                marque.NomMarque = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[1]/td[2]")).Text;
                                marque.NumeroTitre = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[2]/td[2]")).Text;
                                if (DateTime.TryParse(Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[3]/td[2]")).Text,out dt)) { marque.DateDepot = DateTime.Parse(Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[3]/td[2]")).Text); } else { marque.DateDepot = new DateTime(3077, 07, 07); }
                                marque.Etat = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[6]/td[2]")).Text;
                                temp = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text;
                                tab = temp != null ? temp.Split(',') : null;
                                if (tab != null && tab.Length > 0)
                                {
                                    marque.Titulaire = tab[0];
                                }
                                if (DateTime.TryParse(Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[11]/td[2]")).Text, out dt)) { marque.DateExpiration = DateTime.Parse(Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[11]/td[2]")).Text); } else { marque.DateExpiration = new DateTime(3077, 07, 07); }
                                for (int p = 1; p <= Driver.FindElements(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr")).Count; p++)
                                {
                                    temp = Driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{p}]/td[1]")).Text.Trim();
                                    if (list_nice.Contains(temp))
                                    {

                                    }
                                    else
                                    {
                                        list_nice.Add(temp);
                                    }
                                }
                                marque.Classe_nice = "";
                                foreach (var nice in list_nice)
                                {
                                    marque.Classe_nice += nice + ";";
                                }


                                try
                                {
                                    if (!string.IsNullOrWhiteSpace(marque.NumeroTitre))
                                    {
                                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.NumeroTitre}.jpg"))
                                        {
                                            marque.Image = $@"{marque.NumeroTitre}.jpg";
                                        }
                                        else
                                        {
                                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.NumeroTitre}.JPG"))
                                            {
                                                marque.Image = $@"{marque.NumeroTitre}.JPG";
                                            }
                                            else
                                            {
                                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.NumeroTitre}.jpeg"))
                                                {
                                                    marque.Image = $@"{marque.NumeroTitre}.jpeg";
                                                }
                                                else
                                                {
                                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.NumeroTitre}.png"))
                                                    {
                                                        marque.Image = $@"{marque.NumeroTitre}.png";
                                                    }
                                                    else
                                                    {
                                                        webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{marque.NumeroTitre}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{marque.NumeroTitre}.jpg");
                                                        marque.Image = $@"{marque.NumeroTitre}.jpg";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception) { }

                                list_marque.Add(marque);
                            }
                            catch (Exception) { }
                        }
                        Driver.Quit();
                        Session["Rech_ompic_list_marque"] = list_marque;
                        GridView1.DataSource = list_marque.Take(8);
                        GridView1.DataBind();
                        ViewState["index"] = 0;
                        double result = list_marque.Count / 8.0;
                        int pages = int.Parse(Math.Ceiling(result).ToString());
                        index1.Text = 1 + " / " + (pages == 0 ? 1 : pages);
                        ViewState["Id_Sort_dir"] = "ASC";
                        ViewState["NomMarque_Sort_dir"] = "ASC";
                        ViewState["Etat_Sort_dir"] = "ASC";
                        ViewState["Titulaire_Sort_dir"] = "ASC";
                        ViewState["DateDepot_Sort_dir"] = "ASC";
                        ViewState["DateExp_Sort_dir"] = "ASC";
                    }
                }
            }
            catch (Exception) { }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            GridView gridView = (GridView)sender;
            foreach (GridViewRow row in gridView.Rows)
            {
                if (row.Cells[3].Text == "07/07/3077")
                {
                    row.Cells[3].Text = "";
                }
                if (row.Cells[6].Text == "07/07/3077")
                {
                    row.Cells[6].Text = "";
                }
            }
           
        }

        protected void Precedent_Click(object sender, EventArgs e)
        {
            if (Session["Rech_ompic_list_marque"] != null)
            {
                List<Marque> marques = Session["Rech_ompic_list_marque"] as List<Marque>;
                int i = int.Parse(ViewState["index"].ToString());
                double result = marques.Count / 8.0;
                int pages = int.Parse(Math.Ceiling(result).ToString());
                if (i > 0)
                {
                    i--;
                    GridView1.DataSource = marques.Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    index1.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["index"] = i;
                }
                Page.SetFocus(Precedent1);
            }
        }

        protected void Suivant_Click(object sender, EventArgs e)
        {
            if (Session["Rech_ompic_list_marque"] != null)
            {
                List<Marque> marques = Session["Rech_ompic_list_marque"] as List<Marque>;
                int i = int.Parse(ViewState["index"].ToString());
                double result = marques.Count / 8.0;
                int pages = int.Parse(Math.Ceiling(result).ToString());
                if (i < pages - 1)
                {
                    i++;
                    GridView1.DataSource = marques.Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    index1.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["index"] = i;
                }
                Page.SetFocus(Suivant1);
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (e.SortExpression == "Id")
            {
                if (ViewState["Id_Sort_dir"] != null && ViewState["Id_Sort_dir"].ToString() == "ASC")
                {
                    List<Marque> marques = Session["Rech_ompic_list_marque"] as List<Marque>;
                    Session["Rech_ompic_list_marque"] = marques.OrderByDescending((mrq) => mrq.Id).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Rech_ompic_list_marque"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index1.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["Id_Sort_dir"] = "DESC";
                }
                else if (ViewState["Id_Sort_dir"] != null && ViewState["Id_Sort_dir"].ToString() == "DESC")
                {
                    List<Marque> marques = Session["Rech_ompic_list_marque"] as List<Marque>;
                    Session["Rech_ompic_list_marque"] = marques.OrderBy((mrq) => mrq.Id).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Rech_ompic_list_marque"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index1.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["Id_Sort_dir"] = "ASC";
                }
            }
            else if (e.SortExpression == "NomMarque")
            {
                if (ViewState["NomMarque_Sort_dir"] != null && ViewState["NomMarque_Sort_dir"].ToString() == "ASC")
                {
                    List<Marque> marques = Session["Rech_ompic_list_marque"] as List<Marque>;
                    Session["Rech_ompic_list_marque"] = marques.OrderByDescending((mrq) => mrq.NomMarque).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Rech_ompic_list_marque"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index1.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["NomMarque_Sort_dir"] = "DESC";
                }
                else if (ViewState["NomMarque_Sort_dir"] != null && ViewState["NomMarque_Sort_dir"].ToString() == "DESC")
                {
                    List<Marque> marques = Session["Rech_ompic_list_marque"] as List<Marque>;
                    Session["Rech_ompic_list_marque"] = marques.OrderBy((mrq) => mrq.NomMarque).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Rech_ompic_list_marque"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index1.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["NomMarque_Sort_dir"] = "ASC";
                }
            }
            else if (e.SortExpression == "Etat")
            {
                if (ViewState["Etat_Sort_dir"] != null && ViewState["Etat_Sort_dir"].ToString() == "ASC")
                {
                    List<Marque> marques = Session["Rech_ompic_list_marque"] as List<Marque>;
                    Session["Rech_ompic_list_marque"] = marques.OrderByDescending((mrq) => mrq.Etat).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Rech_ompic_list_marque"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index1.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["Etat_Sort_dir"] = "DESC";
                }
                else if (ViewState["Etat_Sort_dir"] != null && ViewState["Etat_Sort_dir"].ToString() == "DESC")
                {
                    List<Marque> marques = Session["Rech_ompic_list_marque"] as List<Marque>;
                    Session["Rech_ompic_list_marque"] = marques.OrderBy((mrq) => mrq.Etat).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Rech_ompic_list_marque"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index1.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["Etat_Sort_dir"] = "ASC";
                }
            }
            else if (e.SortExpression == "Titulaire")
            {
                if (ViewState["Titulaire_Sort_dir"] != null && ViewState["Titulaire_Sort_dir"].ToString() == "ASC")
                {
                    List<Marque> marques = Session["Rech_ompic_list_marque"] as List<Marque>;
                    Session["Rech_ompic_list_marque"] = marques.OrderByDescending((mrq) => mrq.Titulaire).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Rech_ompic_list_marque"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index1.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["Titulaire_Sort_dir"] = "DESC";
                }
                else if (ViewState["Titulaire_Sort_dir"] != null && ViewState["Titulaire_Sort_dir"].ToString() == "DESC")
                {
                    List<Marque> marques = Session["Rech_ompic_list_marque"] as List<Marque>;
                    Session["Rech_ompic_list_marque"] = marques.OrderBy((mrq) => mrq.Titulaire).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Rech_ompic_list_marque"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index1.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["Titulaire_Sort_dir"] = "ASC";
                }
            }
            else if (e.SortExpression == "DateDepot")
            {
                if (ViewState["DateDepot_Sort_dir"] != null && ViewState["DateDepot_Sort_dir"].ToString() == "ASC")
                {
                    List<Marque> marques = Session["Rech_ompic_list_marque"] as List<Marque>;
                    Session["Rech_ompic_list_marque"] = marques.OrderByDescending((mrq) => mrq.DateDepot).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Rech_ompic_list_marque"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index1.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["DateDepot_Sort_dir"] = "DESC";
                }
                else if (ViewState["DateDepot_Sort_dir"] != null && ViewState["DateDepot_Sort_dir"].ToString() == "DESC")
                {
                    List<Marque> marques = Session["Rech_ompic_list_marque"] as List<Marque>;
                    Session["Rech_ompic_list_marque"] = marques.OrderBy((mrq) => mrq.DateDepot).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Rech_ompic_list_marque"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index1.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["DateDepot_Sort_dir"] = "ASC";
                }
            }
            else if (e.SortExpression == "DateExpiration")
            {
                if (ViewState["DateExp_Sort_dir"] != null && ViewState["DateExp_Sort_dir"].ToString() == "ASC")
                {
                    List<Marque> marques = Session["Rech_ompic_list_marque"] as List<Marque>;
                    Session["Rech_ompic_list_marque"] = marques.OrderByDescending((mrq) => mrq.DateExpiration).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Rech_ompic_list_marque"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index1.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["DateExp_Sort_dir"] = "DESC";
                }
                else if (ViewState["DateExp_Sort_dir"] != null && ViewState["DateExp_Sort_dir"].ToString() == "DESC")
                {
                    List<Marque> marques = Session["Rech_ompic_list_marque"] as List<Marque>;
                    Session["Rech_ompic_list_marque"] = marques.OrderBy((mrq) => mrq.DateExpiration).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Rech_ompic_list_marque"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index1.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["DateExp_Sort_dir"] = "ASC";
                }
            }
        }
        protected void Rech_marque_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche marque.aspx");
        }

        protected void Rech_ompic_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche ompic.aspx");
        }

        protected void Num_depot_Validator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int res;
            if(int.TryParse(args.Value,out res))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void Date_depot_debut_Validator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime dt = new DateTime();
            if(DateTime.TryParse(args.Value,out dt))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void Date_depot_fin_Validator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime dt = new DateTime();
            if (DateTime.TryParse(args.Value, out dt))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void Num_priorite_Validator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int res;
            if (int.TryParse(args.Value, out res))
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

        protected void btn_parametre_v1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Parametre.aspx");
        }

        protected void btn_parametre_v2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Parametre.aspx");
        }

        protected void Visit_link_Click(object sender, EventArgs e)
        {
            Button Visitbtn = sender as Button;
            GridViewRow gridViewRow = Visitbtn.NamingContainer as GridViewRow;
            List<Marque> Rech_ompic_list_marque = Session["Rech_ompic_list_marque"] as List<Marque>;
            Response.Write($"<script language='javascript'>window.open('http://search.ompic.ma/web/pages/consulterMarque.do?id={Rech_ompic_list_marque[(int.Parse(ViewState["index"].ToString()) * 8 + gridViewRow.RowIndex)].Id}','_blank');</script>");
        }

        protected void Rech_bd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche Bd.aspx");
        }
    }
}