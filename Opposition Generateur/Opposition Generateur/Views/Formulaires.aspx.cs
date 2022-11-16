using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Opposition_Generateur.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Collections.ObjectModel;
using System.Net;
using System.Data.SqlClient;
using System.Data;

namespace Opposition_Generateur
{
    public partial class Formulaires : System.Web.UI.Page
    {
        public List<FormulaireOpposition> Empty_list = new List<FormulaireOpposition>();
        SqlConnection con = new SqlConnection(@"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=Ipp;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
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
                    SqlCommand cmd = new SqlCommand();
                    if (Session["List formulaire Opposition"] == null)
                    {
                        Session["List formulaire Opposition"] = new List<FormulaireOpposition>();
                    }
                    List<FormulaireOpposition> List_formulaire_Opposition = new List<FormulaireOpposition>();

                    FormulaireOpposition formulaireOpposition = new FormulaireOpposition();
                    cmd.Connection = con;
                    con.Open();
                    //cmd.CommandText = "select IdFO, ND_marque_anterieur ,Nature_marque_anterieur ,ND_marque_contester,Nature_marque_contester from FormulaireOppositiontb where Status='has been submited'";
                    //SqlDataReader dr = cmd.ExecuteReader();

                    //DataTable dt = new DataTable();
                    //dt.Load(dr);
                    //dt.Columns[0].ColumnName = "ID_form";
                    //dt.Columns[1].ColumnName = "N_depot_marque_anterieure";
                    //dt.Columns[2].ColumnName = "Nature_marque_anterieure";
                    //dt.Columns[3].ColumnName = "N_depot_marque_contester";
                    //dt.Columns[4].ColumnName = "Nature_marque_contester";
                    //GridView1.DataSource = dt;
                    //GridView1.DataBind();
                    //con.Close();

                    cmd.Connection = con;



                    //Session["List formulaire Opposition"] = List_formulaire_Opposition;

                    cmd.CommandText = "select * from FormulaireOppositiontb where Status = 'has been submited'";
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        FormulaireOpposition fs = new FormulaireOpposition();
                        fs.ID_form = int.Parse(dt.Rows[i][0].ToString());
                        fs.N_depot_marque_anterieure = dt.Rows[i][1].ToString();
                        fs.N_depot_marque_contester = dt.Rows[i][2].ToString();
                        fs.Nature_marque_anterieure = dt.Rows[i][3].ToString();
                        fs.Nature_marque_contester = dt.Rows[i][4].ToString();
                        fs.Nature_droit_anterieure.Add(dt.Rows[i][5].ToString());
                        List_formulaire_Opposition.Add(fs);

                    }
                    Session["List formulaire Opposition"] = List_formulaire_Opposition;
                    con.Close();
                    GridView1.DataSource = Session["List formulaire Opposition"] as List<FormulaireOpposition>;
                    GridView1.DataBind();

                    //if (dt.Rows.Count!=0)
                    //{

                    //}
                    //else
                    //{

                    //}
                }
                else
                {
                    Response.Redirect("Authentification.aspx");
                }

            }
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            //List<FormulaireOpposition> List_formulaire_Opposition = Session["List formulaire Opposition"] as List<FormulaireOpposition>;
            //List_formulaire_Opposition.RemoveAt(e.RowIndex);
            //Session["List formulaire Opposition"] = List_formulaire_Opposition;
            //GridView1.DataSource = Session["List formulaire Opposition"] as List<FormulaireOpposition>;
            int indexdelet = int.Parse(GridView1.Rows[e.RowIndex].Cells[0].Text);


            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "delete from FormulaireOppositiontb where IdFO=" + indexdelet + "";
            cmd.ExecuteNonQuery();



            Session["List formulaire Opposition"] = new List<FormulaireOpposition>();

            cmd.Connection = con;
            con.Close();
            con.Open();

            //Session["List formulaire Opposition"] = List_formulaire_Opposition;
            List<FormulaireOpposition> List_formulaire_Opposition = new List<FormulaireOpposition>();
            cmd.CommandText = "select * from FormulaireOppositiontb where Status = 'has been submited'";
            SqlDataReader dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(dr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FormulaireOpposition fs = new FormulaireOpposition();
                fs.ID_form = int.Parse(dt.Rows[i][0].ToString());
                fs.N_depot_marque_anterieure = dt.Rows[i][1].ToString();
                fs.N_depot_marque_contester = dt.Rows[i][2].ToString();
                fs.Nature_marque_anterieure = dt.Rows[i][3].ToString();
                fs.Nature_marque_contester = dt.Rows[i][4].ToString();
                fs.Nature_droit_anterieure.Add(dt.Rows[i][5].ToString());
                List_formulaire_Opposition.Add(fs);

            }
            Session["List formulaire Opposition"] = List_formulaire_Opposition;
            con.Close();
            GridView1.DataSource = Session["List formulaire Opposition"] as List<FormulaireOpposition>;
            GridView1.DataBind();
            con.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (true)
            {
                Dictionary<string, string> City_code_pairs = new Dictionary<string, string>();
                City_code_pairs.Add("AGADIR", "1"); City_code_pairs.Add("BOUJAAD", "615"); City_code_pairs.Add("GUERCIF", "693"); City_code_pairs.Add("MARRAKECH", "616"); City_code_pairs.Add("SALE", "871");
                City_code_pairs.Add("ALHOCEIMA", "3"); City_code_pairs.Add("BOULMANE", "13"); City_code_pairs.Add("IMINTANOUTE", "693"); City_code_pairs.Add("MEKNES", "617"); City_code_pairs.Add("SEFROU", "273");
                City_code_pairs.Add("ASILAH", "611"); City_code_pairs.Add("CASABLANCA", "81"); City_code_pairs.Add("INZEGANE", "13"); City_code_pairs.Add("MIDELT", "395"); City_code_pairs.Add("SETTAT", "59");
                City_code_pairs.Add("AZILAL", "612"); City_code_pairs.Add("CHEFCHAOUEN", "15"); City_code_pairs.Add("KALAA-SRAGHNA", "19"); City_code_pairs.Add("MOHAMEDIA", "83"); City_code_pairs.Add("SIDI BENNOUR", "175");
                City_code_pairs.Add("AZROU", "613"); City_code_pairs.Add("DAKHLA", "53"); City_code_pairs.Add("KASBA TADLA", "79"); City_code_pairs.Add("NADOR", "618"); City_code_pairs.Add("SIDI KACEM", "175");
                City_code_pairs.Add("BEN AHMED", "591"); City_code_pairs.Add("ELJADIDA", "17"); City_code_pairs.Add("KENITRA", "35"); City_code_pairs.Add("OUARZAZATE", "83"); City_code_pairs.Add("SIDI SLIMANE", "358");
                City_code_pairs.Add("BEN SLIMANE", "9"); City_code_pairs.Add("ERRACHIDIA", "21"); City_code_pairs.Add("KHEMISSET", "37"); City_code_pairs.Add("OUAZZANE", "605"); City_code_pairs.Add("SOUK LARBAA", "359");
                City_code_pairs.Add("BENGUERIR", "197"); City_code_pairs.Add("ESSAOUIRA", "23"); City_code_pairs.Add("KHENIFRA", "39"); City_code_pairs.Add("OUED ZEM", "415"); City_code_pairs.Add("TANGER", "61");
                City_code_pairs.Add("BENI MELLAL", "614"); City_code_pairs.Add("ES-SMARA", "25"); City_code_pairs.Add("KHOURIBGA", "41"); City_code_pairs.Add("OUJDA", "55"); City_code_pairs.Add("TANTAN", "63");
                City_code_pairs.Add("BERKANE", "551"); City_code_pairs.Add("FES", "27"); City_code_pairs.Add("KSAR KEBIR", "615"); City_code_pairs.Add("RABAT", "85"); City_code_pairs.Add("TAOUNATE", "65");
                City_code_pairs.Add("BERRECHID", "593"); City_code_pairs.Add("FKIH BEN SALEH", "77"); City_code_pairs.Add("LAAYOUNE", "43"); City_code_pairs.Add("ROMANI", "375"); City_code_pairs.Add("TAOURIRT", "552");
                City_code_pairs.Add("BOUARFA", "293"); City_code_pairs.Add("GUELMIM", "31"); City_code_pairs.Add("LARACHE", "44"); City_code_pairs.Add("SAFI", "57"); City_code_pairs.Add("TAROUDANTE", "66");
                City_code_pairs.Add("TATA", "67"); City_code_pairs.Add("TAZA", "69"); City_code_pairs.Add("TEMARA", "619"); City_code_pairs.Add("TETOUAN ", "71"); City_code_pairs.Add("TIZNIT", "73");
                City_code_pairs.Add("YOUSSOUFIA", "573"); City_code_pairs.Add("ZAGORA", "573");

                List<FormulaireOpposition> List_formulaire_Opposition = Session["List formulaire Opposition"] as List<FormulaireOpposition>;
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select IdFO, ND_marque_anterieur ,Nature_marque_anterieur ,ND_marque_contester,Nature_marque_contester from FormulaireOppositiontb where Status='has been submited'";
                SqlDataReader dr = cmd.ExecuteReader();

                //DataTable dt = new DataTable();
                //dt.Load(dr);

                //List<FormulaireOpposition> List_formulaire_Opposition = new List<FormulaireOpposition>();
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    var n = new FormulaireOpposition();

                //    n.N_depot_marque_anterieure = dt.Rows[i][1].ToString();
                //    n.Nature_marque_anterieure = dt.Rows[i][2].ToString();
                //    n.Nom_marque_contester = dt.Rows[i][3].ToString();
                //    n.N_depot_marque_contester = dt.Rows[i][4].ToString();
                //    List_formulaire_Opposition.Add(n);
                //}
                //con.Close();
                if (List_formulaire_Opposition.Count >= 1)
                {
                    ChromeDriver Driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions(), TimeSpan.FromSeconds(120));
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                    WebClient webClient = new WebClient();
                    webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36");
                    foreach (var formulaire in List_formulaire_Opposition)
                    {


                        if (formulaire.Nature_marque_contester == "nationale")
                        {
                            try
                            {
                                Driver.Navigate().GoToUrl($"http://search.ompic.ma/web/pages/consulterMarqueTMView.do?refReglementationTitreLabel=97-{formulaire.N_depot_marque_contester}");
                                formulaire.Nom_marque_contester = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[1]/td[2]")).Text;
                                formulaire.marque_contester_Date_depot = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[3]/td[2]")).Text;
                                formulaire.marque_contester_num_publication = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[7]/td[2]")).Text;
                                formulaire.Deposant_marque_contester = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text.Split(',')[0];
                                formulaire.Date_Exp_marque_contester = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[11]/td[2]")).Text;
                                int Count = Driver.FindElements(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr")).Count;
                                for (int i = 1; i <= Count; i++)
                                {
                                    string key = Driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{i}]/td[1]")).Text;
                                    string value = Driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{i}]/td[2]")).Text;
                                    if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
                                    {
                                        if (!formulaire.Classe_nice_contester_kvp.ContainsKey(key))
                                        {
                                            formulaire.Classe_nice_contester_kvp.Add(key, value);
                                        }
                                        else
                                        {
                                            formulaire.Classe_nice_contester_kvp[key] += " " + value;
                                        }
                                    }
                                }
                                if (string.IsNullOrWhiteSpace(formulaire.Nom_marque_contester) || string.IsNullOrWhiteSpace(formulaire.marque_contester_Date_depot))
                                {
                                    throw new Exception();
                                }
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    Driver.Navigate().GoToUrl($"https://www.tmdn.org/tmview/api/trademark/detail/MA500000200{formulaire.N_depot_marque_contester}?translate=false");
                                    string Json = Driver.FindElement(By.XPath("/html/body/pre")).Text;
                                    JObject Jobj = JObject.Parse(Json);
                                    formulaire.Nom_marque_contester = Jobj["tradeMark"]?["tmName"] != null ? Jobj["tradeMark"]?["tmName"].ToString() : "";
                                    formulaire.marque_contester_Date_depot = Jobj["tradeMark"]?["applicationDate"] != null ? DateTime.Parse(Jobj["tradeMark"]?["applicationDate"].ToString()).ToShortDateString() : "";
                                    formulaire.Date_Exp_marque_contester = Jobj["tradeMark"]?["expiryDate"] != null ? DateTime.Parse(Jobj["tradeMark"]?["expiryDate"].ToString()).ToShortDateString() : "";
                                    if (Jobj["publication"] != null && ((JArray)Jobj["publication"]).Count > 0)
                                    {
                                        formulaire.marque_contester_num_publication = ((JArray)Jobj["publication"])[0]["identifier"] != null ? ((JArray)Jobj["publication"])[0]["identifier"].ToString() : "";
                                    }
                                    if (Jobj["applicants"] != null && ((JArray)Jobj["applicants"]).Count > 0)
                                    {
                                        formulaire.Deposant_marque_contester = ((JArray)Jobj["applicants"])[0]["organizationName"] != null ? ((JArray)Jobj["applicants"])[0]["organizationName"].ToString() : ((JArray)Jobj["applicants"])[0]["fullName"] != null ? ((JArray)Jobj["applicants"])[0]["fullName"].ToString() : "";
                                    }
                                    if (Jobj["goodsAndServicesList"] != null && ((JArray)Jobj["goodsAndServicesList"]).Count > 0)
                                    {
                                        var goodAndServiceList = (JArray)(((JArray)Jobj["goodsAndServicesList"])[0]["goodAndServices"]["goodAndServiceList"]);
                                        foreach (var item in goodAndServiceList)
                                        {
                                            if (formulaire.Classe_nice_contester_kvp.ContainsKey(item["niceClass"].ToString()))
                                            {
                                                formulaire.Classe_nice_contester_kvp[item["niceClass"].ToString()] += " " + ((JArray)item["goodsAndServices"])[0]["term"].ToString();
                                            }
                                            else
                                            {
                                                formulaire.Classe_nice_contester_kvp.Add(item["niceClass"].ToString(), ((JArray)item["goodsAndServices"])[0]["term"].ToString());
                                            }
                                        }
                                    }
                                }
                                catch (Exception) { }
                            }
                            try
                            {
                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_contester}.jpg"))
                                {
                                    formulaire.Image_marque_contester = $@"{formulaire.N_depot_marque_contester}.jpg";
                                }
                                else
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_contester}.JPG"))
                                    {
                                        formulaire.Image_marque_contester = $@"{formulaire.N_depot_marque_contester}.JPG";
                                    }
                                    else
                                    {
                                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_contester}.jpeg"))
                                        {
                                            formulaire.Image_marque_contester = $@"{formulaire.N_depot_marque_contester}.jpeg";
                                        }
                                        else
                                        {
                                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_contester}.png"))
                                            {
                                                formulaire.Image_marque_contester = $@"{formulaire.N_depot_marque_contester}.png";
                                            }
                                            else
                                            {
                                                webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{formulaire.N_depot_marque_contester}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_contester}.jpg");
                                                formulaire.Image_marque_contester = $@"{formulaire.N_depot_marque_contester}.jpg";
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception) { }
                        }
                        if (formulaire.Nature_marque_contester == "internationale")
                        {
                            try
                            {
                                Driver.Navigate().GoToUrl($"https://www3.wipo.int/madrid/monitor/fr/");
                                Driver.FindElement(By.XPath("//*[@id='AUTO_input']")).SendKeys(formulaire.N_depot_marque_contester);
                                Driver.FindElement(By.XPath("//*[@id='simple_search_container_line_0']/a")).Click();
                                Driver.FindElement(By.XPath("//*[@id='results_container']/div[1]/div[2]/div[2]/span/div[1]/a[1]")).Click();
                                Driver.FindElement(By.XPath("//*[@id='gridForsearch_pane']/tbody/tr[2]")).Click();
                                formulaire.Date_Exp_marque_contester = Driver.FindElement(By.XPath("//*[@id='fragment-detail']/div[1]/div[2]/div[1]/div[2]")).Text.Replace(".", "/");
                                formulaire.Nom_marque_contester = Driver.FindElement(By.XPath("//*[@id='documentContent']/div[1]/h2")).Text.Split('-')[1].Trim();
                                formulaire.marque_contester_Date_depot = Driver.FindElement(By.XPath("//*[@id='fragment-detail']/div[1]/div[2]/div[2]/div[2]")).Text.Replace(".", "/");
                                ReadOnlyCollection<IWebElement> Collection = Driver.FindElements(By.XPath("//*[@id='fragment-detail']/div[2]/div/div[1]/div[2]/descendant::div"));
                                for (int i = 0; i < Collection.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        formulaire.Deposant_marque_contester = Collection[i].Text;
                                        break;
                                    }
                                }
                                int Count = Driver.FindElements(By.XPath($"//*[@id='fragment-detail']/div[2]/div/descendant::dt")).Count;

                                for (int i = 1; i <= Count; i++)
                                {
                                    string key = Driver.FindElement(By.XPath($"//*[@id='fragment-detail']/div[2]/div/descendant::dt[{i}]")).Text;
                                    string value = Driver.FindElement(By.XPath($"//*[@id='fragment-detail']/div[2]/div/descendant::dd[{i}]")).Text.Replace(key, "");
                                    if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
                                    {
                                        if (!formulaire.Classe_nice_contester_kvp.ContainsKey(key))
                                        {
                                            formulaire.Classe_nice_contester_kvp.Add(key, value);
                                        }
                                        else
                                        {
                                            formulaire.Classe_nice_contester_kvp[key] += " " + value;
                                        }
                                    }
                                }

                                IWebElement Registration = Driver.FindElement(By.XPath("//*[@id='fragment-detail']/div[4]/div[1]/div[1]/div"));
                                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true)", Registration);
                                if (Registration.Text.Contains("MA") && Registration.Text.Contains("Enregistrement"))
                                {
                                    formulaire.marque_contester_num_publication = Driver.FindElement(By.XPath("//*[@id='fragment-detail']/div[4]/div[1]/div[1]/div")).Text.Split(',')[0].Split(':')[1];
                                }
                                webClient.DownloadFile($"https://www3.wipo.int/madrid/monitor/jsp/data.jsp?KEY=ROM.{formulaire.N_depot_marque_contester}&TYPE=jpg&qi=1-PS2TOI9DFDkXQAToGMd8/SAKSvCctEImcTosQOWerQs=", Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_contester}.jpg");
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_contester}.jpg"))
                                {
                                    formulaire.Image_marque_contester = $@"{formulaire.N_depot_marque_contester}.jpg";
                                }
                                else
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_contester}.JPG"))
                                    {
                                        formulaire.Image_marque_contester = $@"{formulaire.N_depot_marque_contester}.JPG";
                                    }
                                    else
                                    {
                                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_contester}.jpeg"))
                                        {
                                            formulaire.Image_marque_contester = $@"{formulaire.N_depot_marque_contester}.jpeg";
                                        }
                                        else
                                        {
                                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_contester}.png"))
                                            {
                                                formulaire.Image_marque_contester = $@"{formulaire.N_depot_marque_contester}.png";
                                            }
                                            else
                                            {
                                                webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{formulaire.N_depot_marque_contester}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_contester}.jpg");
                                                formulaire.Image_marque_contester = $@"{formulaire.N_depot_marque_contester}.jpg";
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception) { }
                        }
                        if (formulaire.Nature_marque_anterieure == "nationale")
                        {

                            try
                            {
                                string Ville = "";
                                string Titulaire = "";
                                Driver.Navigate().GoToUrl($"http://search.ompic.ma/web/pages/consulterMarqueTMView.do?refReglementationTitreLabel=97-{formulaire.N_depot_marque_anterieure}");
                                formulaire.Nom_marque_anterieure = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[1]/td[2]")).Text;
                                formulaire.marque_anterieur_Date_depot = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[3]/td[2]")).Text;
                                Ville = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text.Split(',').Length == 5 ? Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text.Split(',')[3].Trim() : Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text.Split(',')[2].Trim();
                                Titulaire = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text.Split(',')[0].Trim();
                                formulaire.marque_anterieur_adresse = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text.Split(',').Length == 5 ? Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text.Split(',')[2] : Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text.Split(',')[1];
                                formulaire.Deposant_marque_anterieure = Titulaire;
                                formulaire.Date_Exp_marque_anterieure = Driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[11]/td[2]")).Text;
                                int Count = Driver.FindElements(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr")).Count;
                                for (int i = 1; i <= Count; i++)
                                {
                                    string key = Driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{i}]/td[1]")).Text;
                                    string value = Driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{i}]/td[2]")).Text;
                                    if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
                                    {
                                        if (!formulaire.Classe_nice_anterieure_kvp.ContainsKey(key))
                                        {
                                            formulaire.Classe_nice_anterieure_kvp.Add(key, value);
                                        }
                                        else
                                        {
                                            formulaire.Classe_nice_anterieure_kvp[key] += " " + value;
                                        }
                                    }
                                }
                                if (string.IsNullOrWhiteSpace(formulaire.Nom_marque_anterieure) || string.IsNullOrWhiteSpace(formulaire.marque_anterieur_Date_depot))
                                {
                                    throw new Exception();
                                }
                                try
                                {
                                    Driver.Navigate().GoToUrl($"https://www.directinfo.ma/directinfo-backend/api/queryDsl/search/{Titulaire}");
                                    string Json = Driver.FindElement(By.XPath("/html/body/pre")).Text;
                                    JArray Json_array = JArray.Parse(Json);
                                    JArray Company_array = (JArray)Json_array[0];

                                    if (Company_array.Count >= 1)
                                    {
                                        foreach (var Company in Company_array)
                                        {
                                            if (Ville == Company["libelle"]?.ToString())
                                            {
                                                formulaire.marque_anterieur_denomination_sociale = Company["denomination"] != null ? Company["denomination"].ToString() : "";
                                                formulaire.marque_anterieur_Rc = Company["numeroRc"] != null ? Company["numeroRc"].ToString() : "";
                                                formulaire.marque_anterieur_tribunal = Company["libelle"] != null ? Company["libelle"].ToString() : "";
                                                break;
                                            }
                                        }

                                        if (!string.IsNullOrWhiteSpace(formulaire.marque_anterieur_Rc))
                                        {
                                            Driver.Navigate().GoToUrl("https://r-entreprise.tax.gov.ma/rechercheentreprise/");
                                            Driver.FindElement(By.XPath("//*[@id='rcRadio']")).Click();
                                            Driver.FindElement(By.XPath("//*[@id='mCriteria']")).SendKeys(formulaire.marque_anterieur_Rc);
                                            IWebElement DropDownElement = Driver.FindElement(By.XPath("//*[@id='codeRC']"));
                                            SelectElement selectElement = new SelectElement(DropDownElement);
                                            selectElement.SelectByValue(City_code_pairs[Ville]);
                                            Driver.FindElement(By.XPath("//*[@id='btnSearch']")).Click();
                                            formulaire.marque_anterieur_Ice = Driver.FindElement(By.XPath("//input[@name=\"param['numIce']\"]")).GetAttribute("value");
                                        }
                                    }
                                }
                                catch (Exception) { }
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    string Ville = "";
                                    string Titulaire = "";
                                    Driver.Navigate().GoToUrl($"https://www.tmdn.org/tmview/api/trademark/detail/MA500000200{formulaire.N_depot_marque_anterieure}?translate=false");
                                    string Json = Driver.FindElement(By.XPath("/html/body/pre")).Text;
                                    JObject Jobj = JObject.Parse(Json);
                                    formulaire.Nom_marque_anterieure = Jobj["tradeMark"]?["tmName"] != null ? Jobj["tradeMark"]?["tmName"].ToString() : "";
                                    formulaire.marque_anterieur_Date_depot = Jobj["tradeMark"]?["applicationDate"] != null ? DateTime.Parse(Jobj["tradeMark"]?["applicationDate"].ToString()).ToShortDateString() : "";
                                    formulaire.Date_Exp_marque_anterieure = Jobj["tradeMark"]?["expiryDate"] != null ? DateTime.Parse(Jobj["tradeMark"]?["expiryDate"].ToString()).ToShortDateString() : "";
                                    if (Jobj["applicants"] != null && ((JArray)Jobj["applicants"]).Count > 0)
                                    {
                                        formulaire.marque_anterieur_adresse = ((JArray)Jobj["applicants"])[0]["fullAddress"] != null ? ((JArray)Jobj["applicants"])[0]["fullAddress"].ToString() : "";
                                        Titulaire = ((JArray)Jobj["applicants"])[0]["organizationName"] != null ? ((JArray)Jobj["applicants"])[0]["organizationName"].ToString() : ((JArray)Jobj["applicants"])[0]["fullName"] != null ? ((JArray)Jobj["applicants"])[0]["fullName"].ToString() : "";
                                        formulaire.Deposant_marque_anterieure = Titulaire;
                                    }
                                    if (Jobj["goodsAndServicesList"] != null && ((JArray)Jobj["goodsAndServicesList"]).Count > 0)
                                    {
                                        var goodAndServiceList = (JArray)(((JArray)Jobj["goodsAndServicesList"])[0]["goodAndServices"]["goodAndServiceList"]);
                                        foreach (var item in goodAndServiceList)
                                        {
                                            if (formulaire.Classe_nice_anterieure_kvp.ContainsKey(item["niceClass"].ToString()))
                                            {
                                                formulaire.Classe_nice_anterieure_kvp[item["niceClass"].ToString()] += " " + ((JArray)item["goodsAndServices"])[0]["term"].ToString();
                                            }
                                            else
                                            {
                                                formulaire.Classe_nice_anterieure_kvp.Add(item["niceClass"].ToString(), ((JArray)item["goodsAndServices"])[0]["term"].ToString());
                                            }
                                        }
                                    }
                                    Json = "";
                                    Driver.Navigate().GoToUrl($"https://www.directinfo.ma/directinfo-backend/api/queryDsl/search/{Titulaire}");
                                    Json = Driver.FindElement(By.XPath("/html/body/pre")).Text;
                                    JArray Json_array = JArray.Parse(Json);
                                    JArray Company_array = (JArray)Json_array[0];
                                    if (Company_array.Count >= 1)
                                    {
                                        foreach (var Company in Company_array)
                                        {
                                            formulaire.marque_anterieur_denomination_sociale = Company["denomination"] != null ? Company["denomination"].ToString() : "";
                                            formulaire.marque_anterieur_Rc = Company["numeroRc"] != null ? Company["numeroRc"].ToString() : "";
                                            formulaire.marque_anterieur_tribunal = Company["libelle"] != null ? Company["libelle"].ToString() : "";
                                            break;
                                        }

                                        if (!string.IsNullOrWhiteSpace(formulaire.marque_anterieur_Rc))
                                        {
                                            Driver.Navigate().GoToUrl("https://r-entreprise.tax.gov.ma/rechercheentreprise/");
                                            Driver.FindElement(By.XPath("//*[@id='rcRadio']")).Click();
                                            Driver.FindElement(By.XPath("//*[@id='mCriteria']")).SendKeys(formulaire.marque_anterieur_Rc);
                                            IWebElement DropDownElement = Driver.FindElement(By.XPath("//*[@id='codeRC']"));
                                            SelectElement selectElement = new SelectElement(DropDownElement);
                                            selectElement.SelectByValue(City_code_pairs[Ville]);
                                            Driver.FindElement(By.XPath("//*[@id='btnSearch']")).Click();
                                            formulaire.marque_anterieur_Ice = Driver.FindElement(By.XPath("//input[@name=\"param['numIce']\"]")).GetAttribute("value");
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }

                            try
                            {
                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_anterieure}.jpg"))
                                {
                                    formulaire.Image_marque_anterieure = $@"{formulaire.N_depot_marque_anterieure}.jpg";
                                }
                                else
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_anterieure}.JPG"))
                                    {
                                        formulaire.Image_marque_anterieure = $@"{formulaire.N_depot_marque_anterieure}.JPG";
                                    }
                                    else
                                    {
                                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_anterieure}.jpeg"))
                                        {
                                            formulaire.Image_marque_anterieure = $@"{formulaire.N_depot_marque_anterieure}.jpeg";
                                        }
                                        else
                                        {
                                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_anterieure}.png"))
                                            {
                                                formulaire.Image_marque_anterieure = $@"{formulaire.N_depot_marque_anterieure}.png";
                                            }
                                            else
                                            {
                                                webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{formulaire.N_depot_marque_anterieure}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_anterieure}.jpg");
                                                formulaire.Image_marque_anterieure = $@"{formulaire.N_depot_marque_anterieure}.jpg";
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception) { }
                        }
                        if (formulaire.Nature_marque_anterieure == "internationale")
                        {
                            try
                            {
                                Driver.Navigate().GoToUrl($"https://www3.wipo.int/madrid/monitor/fr/");
                                Driver.FindElement(By.XPath("//*[@id='AUTO_input']")).SendKeys(formulaire.N_depot_marque_anterieure);
                                Driver.FindElement(By.XPath("//*[@id='simple_search_container_line_0']/a")).Click();
                                Driver.FindElement(By.XPath("//*[@id='results_container']/div[1]/div[2]/div[2]/span/div[1]/a[1]")).Click();
                                Driver.FindElement(By.XPath("//*[@id='gridForsearch_pane']/tbody/tr[2]")).Click();
                                formulaire.Date_Exp_marque_anterieure = Driver.FindElement(By.XPath("//*[@id='fragment-detail']/div[1]/div[2]/div[1]/div[2]")).Text.Replace(".", "/");
                                formulaire.Nom_marque_anterieure = Driver.FindElement(By.XPath("//*[@id='documentContent']/div[1]/h2")).Text.Split('-')[1].Trim();
                                formulaire.marque_anterieur_Date_depot = Driver.FindElement(By.XPath("//*[@id='fragment-detail']/div[1]/div[2]/div[2]/div[2]")).Text.Replace(".", "/");
                                ReadOnlyCollection<IWebElement> Collection = Driver.FindElements(By.XPath("//*[@id='fragment-detail']/div[2]/div/div[1]/div[2]/descendant::div"));
                                for (int i = 0; i < Collection.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        formulaire.Deposant_marque_anterieure = Collection[i].Text;
                                    }
                                    formulaire.marque_anterieur_adresse += Collection[i].Text + " ";
                                }
                                int Count = Driver.FindElements(By.XPath($"//*[@id='fragment-detail']/div[2]/div/descendant::dt")).Count;

                                for (int i = 1; i <= Count; i++)
                                {
                                    string key = Driver.FindElement(By.XPath($"//*[@id='fragment-detail']/div[2]/div/descendant::dt[{i}]")).Text;
                                    string value = Driver.FindElement(By.XPath($"//*[@id='fragment-detail']/div[2]/div/descendant::dd[{i}]")).Text.Replace(key, "");
                                    if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
                                    {
                                        if (!formulaire.Classe_nice_anterieure_kvp.ContainsKey(key))
                                        {
                                            formulaire.Classe_nice_anterieure_kvp.Add(key, value);
                                        }
                                        else
                                        {
                                            formulaire.Classe_nice_anterieure_kvp[key] += " " + value;
                                        }
                                    }
                                }
                                webClient.DownloadFile($"https://www3.wipo.int/madrid/monitor/jsp/data.jsp?KEY=ROM.{formulaire.N_depot_marque_anterieure}&TYPE=jpg&qi=1-PS2TOI9DFDkXQAToGMd8/SAKSvCctEImcTosQOWerQs=", Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_anterieure}.jpg");
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_anterieure}.jpg"))
                                {
                                    formulaire.Image_marque_anterieure = $@"{formulaire.N_depot_marque_anterieure}.jpg";
                                }
                                else
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_anterieure}.JPG"))
                                    {
                                        formulaire.Image_marque_anterieure = $@"{formulaire.N_depot_marque_anterieure}.JPG";
                                    }
                                    else
                                    {
                                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_anterieure}.jpeg"))
                                        {
                                            formulaire.Image_marque_anterieure = $@"{formulaire.N_depot_marque_anterieure}.jpeg";
                                        }
                                        else
                                        {
                                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_anterieure}.png"))
                                            {
                                                formulaire.Image_marque_anterieure = $@"{formulaire.N_depot_marque_anterieure}.png";
                                            }
                                            else
                                            {
                                                webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{formulaire.N_depot_marque_anterieure}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{formulaire.N_depot_marque_anterieure}.jpg");
                                                formulaire.Image_marque_anterieure = $@"{formulaire.N_depot_marque_anterieure}.jpg";
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception) { }
                        }
                    }
                    Driver.Quit();
                    Session["List formulaire Opposition"] = List_formulaire_Opposition;

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

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Resultat.aspx");
        }
        protected void Archive_Click(object sender, EventArgs e)
        {
            Response.Redirect("archive.aspx");
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
        protected void Rech_bd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche Bd.aspx");
        }
    }
}