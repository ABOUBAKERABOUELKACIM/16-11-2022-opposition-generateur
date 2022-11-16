using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Opposition_Generateur.Crystal_report;
using Opposition_Generateur.Models;
using System.Data.SqlClient;

namespace Opposition_Generateur
{
    public partial class Generer_pdf : System.Web.UI.Page
    {
        List<Alerte> Empty_list_alerte = new List<Alerte>();
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

                    if (Session["List_alerte"] != null)
                    {
                        GridView1.DataSource = Session["List_alerte"] as List<Alerte>;
                        GridView1.DataBind();
                    }
                    else
                    {
                        GridView1.DataSource = Empty_list_alerte;
                        GridView1.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("Authentification.aspx");
                }
            }
        }

        public string[] GetDate(string num_pub)
        {
            DataTable dt = new DataTable("Gazette");
            dt.Columns.Add("Num_pub", typeof(string));
            dt.Columns.Add("Date", typeof(DateTime));
            string path = Server.MapPath("~") + "\\Setting\\" + "Setting.xml";
            dt.ReadXml(path);
            string date_fin = "";
            string date_debut = "";
            if (!string.IsNullOrWhiteSpace(num_pub))
            {
                
                foreach (DataRow row in dt.Rows)
                {
                    if (row[0].ToString().Trim() == num_pub.Trim())
                    {
                        date_debut = DateTime.Parse(row[1].ToString()).ToShortDateString();
                        DateTime temp = DateTime.Parse(date_debut);
                        DateTime dateTime = temp.AddMonths(2);
                        date_fin = dateTime.ToShortDateString();
                    }

                }
            }

            return new string[] { date_debut, date_fin };
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
        protected void Archive_Click(object sender, EventArgs e)
        {
            Response.Redirect("archive.aspx");
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

        protected void Generate_doc_Click(object sender, EventArgs e)
        {
            Button ShowArgs = sender as Button;
            GridViewRow gridViewRow = ShowArgs.NamingContainer as GridViewRow;
            WebClient webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36");

            ChromeDriver driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions(), TimeSpan.FromSeconds(120));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            /*-----------------------------Le code d'extraction des données de la Marque anterieure--------------------------------------------- */
            List<string> list_nice = new List<string>();
            Marque_Model_App_V1 marque_anterieure = new Marque_Model_App_V1();
            Marque_Model_App_V1 marque_à_contester = new Marque_Model_App_V1();
            List<Alerte> alertes = Session["List_alerte"] as List<Alerte>;
            Alerte alerte = alertes[gridViewRow.RowIndex];
            bool marq_anter_completed = true;
            bool marq_contester_completed = true;
            string temp1 = "";
            string temp2 = "";
            string[] tab = null;

            if ((gridViewRow.FindControl("lbl_Nature_marque_anterieure") as Label).Text == "nationale")
            {
                try
                {

                    driver.Navigate().GoToUrl((gridViewRow.FindControl("lbl_Marque_anterieure_reference") as Label).Text);
                    marque_anterieure.Nom = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[1]/td[2]")).Text;
                    marque_anterieure.Numero = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[2]/td[2]")).Text;
                    marque_anterieure.Date_depot = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[3]/td[2]")).Text;
                    temp1 = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text;
                    tab = temp1.Split(',');
                    marque_anterieure.Titulaire = tab[0];
                    for (int p = 1; p <= driver.FindElements(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr")).Count; p++)
                    {
                        temp2 = driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{p}]/td[1]")).Text.Trim();
                        if (list_nice.Contains(temp2))
                        {

                        }
                        else
                        {
                            list_nice.Add(temp2);
                        }
                    }
                    foreach (var item in list_nice)
                    {
                        marque_anterieure.Classe_nice += item + ";";
                    }
                }
                catch (Exception)
                {
                    marq_anter_completed = false;
                }
            }
            if ((gridViewRow.FindControl("lbl_Nature_marque_anterieure") as Label).Text == "internationale")
            {

                try
                {
                    driver.Navigate().GoToUrl($"https://www3.wipo.int/madrid/monitor/fr/");
                    marque_anterieure.Numero = (gridViewRow.FindControl("lbl_Marque_anterieure_reference") as Label).Text;
                    driver.FindElement(By.XPath("//*[@id='AUTO_input']")).SendKeys((gridViewRow.FindControl("lbl_Marque_anterieure_reference") as Label).Text);
                    driver.FindElement(By.XPath("//*[@id='simple_search_container_line_0']/a")).Click();
                    driver.FindElement(By.XPath("//*[@id='results_container']/div[1]/div[2]/div[2]/span/div[1]/a[1]")).Click();
                    driver.FindElement(By.XPath("//*[@id='gridForsearch_pane']/tbody/tr[2]")).Click();
                    marque_anterieure.Nom = driver.FindElement(By.XPath("//*[@id='documentContent']/div[1]/h2")).Text.Split('-')[1].Trim();
                    marque_anterieure.Date_depot = driver.FindElement(By.XPath("//*[@id='fragment-detail']/div[1]/div[2]/div[2]/div[2]")).Text.Replace(".", "/");
                    ReadOnlyCollection<IWebElement> Collection = driver.FindElements(By.XPath("//*[@id='fragment-detail']/div[2]/div/div[1]/div[2]/descendant::div"));
                    for (int i = 0; i < Collection.Count; i++)
                    {
                        if (i == 0)
                        {
                            marque_anterieure.Titulaire = Collection[i].Text;
                        }
                    }
                    int Count = driver.FindElements(By.XPath($"//*[@id='fragment-detail']/div[2]/div/descendant::dt")).Count;
                    string key = "";
                    for (int i = 1; i <= Count; i++)
                    {
                        key += driver.FindElement(By.XPath($"//*[@id='fragment-detail']/div[2]/div/descendant::dt[{i}]")).Text + ";";
                    }
                    marque_anterieure.Classe_nice = key.Trim();

                    if (Directory.Exists(Server.MapPath("~") + "\\Assets\\Brand_image"))
                    {
                        try
                        {
                            webClient.DownloadFile($"https://www3.wipo.int/madrid/monitor/jsp/data.jsp?KEY=ROM.{(gridViewRow.FindControl("lbl_Marque_anterieure_reference") as Label).Text}&TYPE=jpg&qi=1-PS2TOI9DFDkXQAToGMd8/SAKSvCctEImcTosQOWerQs=", Server.MapPath("~") + $"\\Assets\\Brand_image\\{(gridViewRow.FindControl("lbl_Marque_anterieure_reference") as Label).Text}.jpg");
                        }
                        catch (Exception) { }
                    }
                }
                catch (Exception)
                {
                    marq_anter_completed = false;
                }
            }


            /*-----------------------------Le code d'extraction des données de la Marque à contester--------------------------------------------- */

            if ((gridViewRow.FindControl("lbl_Nature_marque_contester") as Label).Text == "nationale")
            {

                try
                {
                    list_nice.Clear();
                    temp1 = "";
                    temp2 = "";
                    tab = null;
                    driver.Navigate().GoToUrl((gridViewRow.FindControl("lbl_Marque_contester_reference") as Label).Text);
                    marque_à_contester.Nom = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[1]/td[2]")).Text;
                    marque_à_contester.Numero = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[2]/td[2]")).Text;
                    marque_à_contester.Date_depot = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[3]/td[2]")).Text;
                    if (!string.IsNullOrWhiteSpace((gridViewRow.FindControl("lbl_Num_pub") as Label).Text))
                    {
                        alerte.Num_pub = (gridViewRow.FindControl("lbl_Num_pub") as Label).Text;
                    }
                    else
                    {
                        alerte.Num_pub = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[7]/td[2]")).Text;
                    }
                    string[] array = GetDate(alerte.Num_pub);
                    alerte.Date_debut = array[0];
                    alerte.Date_fin = array[1];
                    temp1 = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text;
                    tab = temp1.Split(',');
                    marque_à_contester.Titulaire = tab[0];

                    for (int p = 1; p <= driver.FindElements(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr")).Count; p++)
                    {
                        temp2 = driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{p}]/td[1]")).Text.Trim();
                        if (list_nice.Contains(temp2))
                        {

                        }
                        else
                        {
                            list_nice.Add(temp2);
                        }
                    }
                    foreach (var item in list_nice)
                    {
                        marque_à_contester.Classe_nice += item + ";";
                    }
                }
                catch (Exception)
                {
                    marq_contester_completed = false;
                }
            }
            if ((gridViewRow.FindControl("lbl_Nature_marque_contester") as Label).Text == "internationale")
            {

                try
                {
                    list_nice.Clear();
                    temp1 = "";
                    temp2 = "";
                    tab = null;
                    driver.Navigate().GoToUrl($"https://www3.wipo.int/madrid/monitor/fr/");
                    marque_à_contester.Numero = (gridViewRow.FindControl("lbl_Marque_contester_reference") as Label).Text;
                    driver.FindElement(By.XPath("//*[@id='AUTO_input']")).SendKeys((gridViewRow.FindControl("lbl_Marque_contester_reference") as Label).Text);
                    driver.FindElement(By.XPath("//*[@id='simple_search_container_line_0']/a")).Click();
                    driver.FindElement(By.XPath("//*[@id='results_container']/div[1]/div[2]/div[2]/span/div[1]/a[1]")).Click();
                    driver.FindElement(By.XPath("//*[@id='gridForsearch_pane']/tbody/tr[2]")).Click();
                    marque_à_contester.Nom = driver.FindElement(By.XPath("//*[@id='documentContent']/div[1]/h2")).Text.Split('-')[1].Trim();
                    marque_à_contester.Date_depot = driver.FindElement(By.XPath("//*[@id='fragment-detail']/div[1]/div[2]/div[2]/div[2]")).Text.Replace(".", "/");
                    ReadOnlyCollection<IWebElement> Collection = driver.FindElements(By.XPath("//*[@id='fragment-detail']/div[2]/div/div[1]/div[2]/descendant::div"));
                    for (int i = 0; i < Collection.Count; i++)
                    {
                        if (i == 0)
                        {
                            marque_à_contester.Titulaire = Collection[i].Text;
                        }
                    }
                    int Count = driver.FindElements(By.XPath($"//*[@id='fragment-detail']/div[2]/div/descendant::dt")).Count;
                    string key = "";
                    for (int i = 1; i <= Count; i++)
                    {
                        key += driver.FindElement(By.XPath($"//*[@id='fragment-detail']/div[2]/div/descendant::dt[{i}]")).Text + ";";
                    }
                    marque_à_contester.Classe_nice = key.Trim();

                    if (!string.IsNullOrWhiteSpace((gridViewRow.FindControl("lbl_Num_pub") as Label).Text))
                    {
                        alerte.Num_pub = (gridViewRow.FindControl("lbl_Num_pub") as Label).Text;
                    }
                    else
                    {
                        IWebElement Registration = driver.FindElement(By.XPath("//*[@id='fragment-detail']/div[4]/div[1]/div[1]/div"));
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true)", Registration);
                        if (Registration.Text.Contains("MA") && Registration.Text.Contains("Enregistrement"))
                        {
                            alerte.Num_pub = driver.FindElement(By.XPath("//*[@id='fragment-detail']/div[4]/div[1]/div[1]/div")).Text.Split(',')[0].Split(':')[1].Trim().Split(' ')[0].Trim();
                        }
                    }

                    string[] array = GetDate(alerte.Num_pub);
                    alerte.Date_debut = array[0];
                    alerte.Date_fin = array[1];

                    if (Directory.Exists(Server.MapPath("~") + "\\Assets\\Brand_image"))
                    {
                        try
                        {
                            webClient.DownloadFile($"https://www3.wipo.int/madrid/monitor/jsp/data.jsp?KEY=ROM.{(gridViewRow.FindControl("lbl_Marque_contester_reference") as Label).Text}&TYPE=jpg&qi=1-PS2TOI9DFDkXQAToGMd8/SAKSvCctEImcTosQOWerQs=", Server.MapPath("~") + $"\\Assets\\Brand_image\\{(gridViewRow.FindControl("lbl_Marque_contester_reference") as Label).Text}.jpg");
                        }
                        catch (Exception) { }
                    }
                }
                catch (Exception ex)
                {
                    marq_contester_completed = false;
                }
            }
            driver.Quit();
            driver.Dispose();
            driver = null;
            if (marq_anter_completed && marq_contester_completed)
            {
                if (Directory.Exists(Server.MapPath("~") + "\\Assets\\Brand_image"))
                {
                    string[] files = Directory.GetFiles(Server.MapPath("~") + "\\Assets\\Brand_image");

                    foreach (var file in files)
                    {
                        var array = file.Split('\\', '.');

                        if (array[array.Length - 2].Equals(marque_anterieure.Numero))
                        {
                            try
                            {
                                System.Drawing.Image img = System.Drawing.Image.FromFile(file);
                                Bitmap bitmap = new Bitmap(img);
                                System.Drawing.Image image = bitmap;
                                MemoryStream ms = new MemoryStream();
                                image.Save(ms, img.RawFormat);
                                marque_anterieure.Image = ms.ToArray();
                                break;
                            }
                            catch (Exception) { break; }
                        }

                    }

                    foreach (var file in files)
                    {
                        var array = file.Split('\\', '.');

                        if (array[array.Length - 2].Equals(marque_à_contester.Numero))
                        {
                            try
                            {
                                System.Drawing.Image img = System.Drawing.Image.FromFile(file);
                                Bitmap bitmap = new Bitmap(img);
                                System.Drawing.Image image = bitmap;
                                MemoryStream ms = new MemoryStream();
                                image.Save(ms, img.RawFormat);
                                marque_à_contester.Image = ms.ToArray();
                                break;
                            }
                            catch (Exception) { break; }
                        }

                    }
                    if (marque_anterieure.Image == null)
                    {
                        try
                        {

                            webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{marque_anterieure.Numero}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{marque_anterieure.Numero}.jpg");
                            System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\{marque_anterieure.Numero}.jpg");
                            Bitmap bitmap = new Bitmap(img);
                            System.Drawing.Image image = bitmap;
                            MemoryStream memS = new MemoryStream();
                            image.Save(memS, img.RawFormat);
                            marque_anterieure.Image = memS.ToArray();


                        }
                        catch (Exception)
                        {
                            Bitmap bmp = new Bitmap(600, 600);
                            RectangleF rectf = new RectangleF(0, 0, bmp.Width, bmp.Height);
                            Graphics g = Graphics.FromImage(bmp);
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                            StringFormat format = new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            };
                            g.FillRectangle(Brushes.White, rectf);
                            g.DrawString(marque_anterieure.Nom, new Font("Tahoma", 40), Brushes.Black, rectf, format);
                            g.Flush();
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            marque_anterieure.Image = ms.ToArray();
                        }
                    }
                    if (marque_à_contester.Image == null)
                    {
                        try
                        {

                            webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{marque_à_contester.Numero}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{marque_à_contester.Numero}.jpg");
                            System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\{marque_à_contester.Numero}.jpg");
                            Bitmap bitmap = new Bitmap(img);
                            System.Drawing.Image image = bitmap;
                            MemoryStream memS = new MemoryStream();
                            image.Save(memS, img.RawFormat);
                            marque_à_contester.Image = memS.ToArray();


                        }
                        catch (Exception)
                        {
                            Bitmap bmp = new Bitmap(600, 600);
                            RectangleF rectf = new RectangleF(0, 0, bmp.Width, bmp.Height);
                            Graphics g = Graphics.FromImage(bmp);
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                            StringFormat format = new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            };
                            g.FillRectangle(Brushes.White, rectf);
                            g.DrawString(marque_à_contester.Nom, new Font("Tahoma", 40), Brushes.Black, rectf, format);
                            g.Flush();
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            marque_à_contester.Image = ms.ToArray();
                        }
                    }
                    CrystalReport1 report = new CrystalReport1();
                    List<Marque_Model_App_V1> temp_list1 = new List<Marque_Model_App_V1>();
                    List<Marque_Model_App_V1> temp_list2 = new List<Marque_Model_App_V1>();
                    List<Alerte> temp_list3 = new List<Alerte>();

                    temp_list1.Clear();
                    temp_list2.Clear();
                    temp_list3.Clear();
                    temp_list1.Add(marque_à_contester);
                    temp_list2.Add(marque_anterieure);
                    temp_list3.Add(alerte);
                    report.Database.Tables["Marque_contester"].SetDataSource(temp_list1.AsEnumerable());
                    report.Database.Tables["Marque_anterieure"].SetDataSource(temp_list2.AsEnumerable());
                    report.Database.Tables["Alerte"].SetDataSource(temp_list3.AsEnumerable());
                    string filename = $"[DEADLINE {alerte.Date_fin.Replace('/', '-')}] - ALERTE DEPOT FRAUDULEUX DE LA MARQUE {marque_à_contester.Nom} VS {marque_anterieure.Nom} - {marque_anterieure.Titulaire} - GAZETTE N°{alerte.Num_pub.Replace('/', '-')}.pdf";
                    filename = filename.Replace('/', ' ').Replace('\\', ' ').Replace(":", " ").Replace("*", "").Replace("\"", "").Replace("?", "").Replace("|", "").Replace(",", " ").Replace("(", " ").Replace(")", " "); ;
                    Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    report.Close();
                    report.Dispose();
                    MemoryStream memoryS = new MemoryStream();
                    stream.CopyTo(memoryS);
                    byte[] buffer = memoryS.ToArray();
                    HttpCookie httpCookie = Request.Cookies["Userinfo"];
                    string name = httpCookie["Username"];
                    string type = "Renouve";
                    storepdf(buffer, name, type);

                    Response.AddHeader("Content-Length", buffer.Length.ToString());
                    Response.AddHeader("Content-Disposition", $"attachment; filename={filename}");
                    Response.OutputStream.Write(buffer, 0, buffer.Length);
                    Response.End();

                }
            }
        }
        protected void Generate_doc_Ang_Click(object sender, EventArgs e)
        {
            Button ShowArgs = sender as Button;
            GridViewRow gridViewRow = ShowArgs.NamingContainer as GridViewRow;
            WebClient webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36");

            ChromeDriver driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions(), TimeSpan.FromSeconds(120));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            /*-----------------------------Le code d'extraction des données de la Marque anterieure--------------------------------------------- */
            List<string> list_nice = new List<string>();
            Marque_Model_App_V1 marque_anterieure = new Marque_Model_App_V1();
            Marque_Model_App_V1 marque_à_contester = new Marque_Model_App_V1();
            List<Alerte> alertes = Session["List_alerte"] as List<Alerte>;
            Alerte alerte = alertes[gridViewRow.RowIndex];
            bool marq_anter_completed = true;
            bool marq_contester_completed = true;
            string temp1 = "";
            string temp2 = "";
            string[] tab = null;

            if ((gridViewRow.FindControl("lbl_Nature_marque_anterieure") as Label).Text == "nationale")
            {
                try
                {

                    driver.Navigate().GoToUrl((gridViewRow.FindControl("lbl_Marque_anterieure_reference") as Label).Text);
                    marque_anterieure.Nom = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[1]/td[2]")).Text;
                    marque_anterieure.Numero = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[2]/td[2]")).Text;
                    marque_anterieure.Date_depot = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[3]/td[2]")).Text;
                    temp1 = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text;
                    tab = temp1.Split(',');
                    marque_anterieure.Titulaire = tab[0];
                    for (int p = 1; p <= driver.FindElements(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr")).Count; p++)
                    {
                        temp2 = driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{p}]/td[1]")).Text.Trim();
                        if (list_nice.Contains(temp2))
                        {

                        }
                        else
                        {
                            list_nice.Add(temp2);
                        }
                    }
                    foreach (var item in list_nice)
                    {
                        marque_anterieure.Classe_nice += item + ";";
                    }
                }
                catch (Exception)
                {
                    marq_anter_completed = false;
                }
            }
            if ((gridViewRow.FindControl("lbl_Nature_marque_anterieure") as Label).Text == "internationale")
            {

                try
                {
                    driver.Navigate().GoToUrl($"https://www3.wipo.int/madrid/monitor/fr/");
                    marque_anterieure.Numero = (gridViewRow.FindControl("lbl_Marque_anterieure_reference") as Label).Text;
                    driver.FindElement(By.XPath("//*[@id='AUTO_input']")).SendKeys((gridViewRow.FindControl("lbl_Marque_anterieure_reference") as Label).Text);
                    driver.FindElement(By.XPath("//*[@id='simple_search_container_line_0']/a")).Click();
                    driver.FindElement(By.XPath("//*[@id='results_container']/div[1]/div[2]/div[2]/span/div[1]/a[1]")).Click();
                    driver.FindElement(By.XPath("//*[@id='gridForsearch_pane']/tbody/tr[2]")).Click();
                    marque_anterieure.Nom = driver.FindElement(By.XPath("//*[@id='documentContent']/div[1]/h2")).Text.Split('-')[1].Trim();
                    marque_anterieure.Date_depot = driver.FindElement(By.XPath("//*[@id='fragment-detail']/div[1]/div[2]/div[2]/div[2]")).Text.Replace(".", "/");
                    ReadOnlyCollection<IWebElement> Collection = driver.FindElements(By.XPath("//*[@id='fragment-detail']/div[2]/div/div[1]/div[2]/descendant::div"));
                    for (int i = 0; i < Collection.Count; i++)
                    {
                        if (i == 0)
                        {
                            marque_anterieure.Titulaire = Collection[i].Text;
                        }
                    }
                    int Count = driver.FindElements(By.XPath($"//*[@id='fragment-detail']/div[2]/div/descendant::dt")).Count;
                    string key = "";
                    for (int i = 1; i <= Count; i++)
                    {
                        key += driver.FindElement(By.XPath($"//*[@id='fragment-detail']/div[2]/div/descendant::dt[{i}]")).Text + ";";
                    }
                    marque_anterieure.Classe_nice = key.Trim();

                    if (Directory.Exists(Server.MapPath("~") + "\\Assets\\Brand_image"))
                    {
                        try
                        {
                            webClient.DownloadFile($"https://www3.wipo.int/madrid/monitor/jsp/data.jsp?KEY=ROM.{(gridViewRow.FindControl("lbl_Marque_anterieure_reference") as Label).Text}&TYPE=jpg&qi=1-PS2TOI9DFDkXQAToGMd8/SAKSvCctEImcTosQOWerQs=", Server.MapPath("~") + $"\\Assets\\Brand_image\\{(gridViewRow.FindControl("lbl_Marque_anterieure_reference") as Label).Text}.jpg");
                        }
                        catch (Exception) { }
                    }
                }
                catch (Exception)
                {
                    marq_anter_completed = false;
                }
            }


            /*-----------------------------Le code d'extraction des données de la Marque à contester--------------------------------------------- */

            if ((gridViewRow.FindControl("lbl_Nature_marque_contester") as Label).Text == "nationale")
            {

                try
                {
                    list_nice.Clear();
                    temp1 = "";
                    temp2 = "";
                    tab = null;
                    driver.Navigate().GoToUrl((gridViewRow.FindControl("lbl_Marque_contester_reference") as Label).Text);
                    marque_à_contester.Nom = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[1]/td[2]")).Text;
                    marque_à_contester.Numero = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[2]/td[2]")).Text;
                    marque_à_contester.Date_depot = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[3]/td[2]")).Text;
                    if (!string.IsNullOrWhiteSpace((gridViewRow.FindControl("lbl_Num_pub") as Label).Text))
                    {
                        alerte.Num_pub = (gridViewRow.FindControl("lbl_Num_pub") as Label).Text;
                    }
                    else
                    {
                        alerte.Num_pub = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[7]/td[2]")).Text;
                    }
                    string[] array = GetDate(alerte.Num_pub);
                    alerte.Date_debut = array[0];
                    alerte.Date_fin = array[1];
                    temp1 = driver.FindElement(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[8]/td[2]")).Text;
                    tab = temp1.Split(',');
                    marque_à_contester.Titulaire = tab[0];

                    for (int p = 1; p <= driver.FindElements(By.XPath("//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr")).Count; p++)
                    {
                        temp2 = driver.FindElement(By.XPath($"//*[@id='trDetMrq']/td/table/tbody/tr[12]/td[2]/table/tbody/tr[{p}]/td[1]")).Text.Trim();
                        if (list_nice.Contains(temp2))
                        {

                        }
                        else
                        {
                            list_nice.Add(temp2);
                        }
                    }
                    foreach (var item in list_nice)
                    {
                        marque_à_contester.Classe_nice += item + ";";
                    }
                }
                catch (Exception)
                {
                    marq_contester_completed = false;
                }
            }
            if ((gridViewRow.FindControl("lbl_Nature_marque_contester") as Label).Text == "internationale")
            {

                try
                {
                    list_nice.Clear();
                    temp1 = "";
                    temp2 = "";
                    tab = null;
                    driver.Navigate().GoToUrl($"https://www3.wipo.int/madrid/monitor/fr/");
                    marque_à_contester.Numero = (gridViewRow.FindControl("lbl_Marque_contester_reference") as Label).Text;
                    driver.FindElement(By.XPath("//*[@id='AUTO_input']")).SendKeys((gridViewRow.FindControl("lbl_Marque_contester_reference") as Label).Text);
                    driver.FindElement(By.XPath("//*[@id='simple_search_container_line_0']/a")).Click();
                    driver.FindElement(By.XPath("//*[@id='results_container']/div[1]/div[2]/div[2]/span/div[1]/a[1]")).Click();
                    driver.FindElement(By.XPath("//*[@id='gridForsearch_pane']/tbody/tr[2]")).Click();
                    marque_à_contester.Nom = driver.FindElement(By.XPath("//*[@id='documentContent']/div[1]/h2")).Text.Split('-')[1].Trim();
                    marque_à_contester.Date_depot = driver.FindElement(By.XPath("//*[@id='fragment-detail']/div[1]/div[2]/div[2]/div[2]")).Text.Replace(".", "/");
                    ReadOnlyCollection<IWebElement> Collection = driver.FindElements(By.XPath("//*[@id='fragment-detail']/div[2]/div/div[1]/div[2]/descendant::div"));
                    for (int i = 0; i < Collection.Count; i++)
                    {
                        if (i == 0)
                        {
                            marque_à_contester.Titulaire = Collection[i].Text;
                        }
                    }
                    int Count = driver.FindElements(By.XPath($"//*[@id='fragment-detail']/div[2]/div/descendant::dt")).Count;
                    string key = "";
                    for (int i = 1; i <= Count; i++)
                    {
                        key += driver.FindElement(By.XPath($"//*[@id='fragment-detail']/div[2]/div/descendant::dt[{i}]")).Text + ";";
                    }
                    marque_à_contester.Classe_nice = key.Trim();

                    if (!string.IsNullOrWhiteSpace((gridViewRow.FindControl("lbl_Num_pub") as Label).Text))
                    {
                        alerte.Num_pub = (gridViewRow.FindControl("lbl_Num_pub") as Label).Text;
                    }
                    else
                    {
                        IWebElement Registration = driver.FindElement(By.XPath("//*[@id='fragment-detail']/div[4]/div[1]/div[1]/div"));
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true)", Registration);
                        if (Registration.Text.Contains("MA") && Registration.Text.Contains("Enregistrement"))
                        {
                            alerte.Num_pub = driver.FindElement(By.XPath("//*[@id='fragment-detail']/div[4]/div[1]/div[1]/div")).Text.Split(',')[0].Split(':')[1].Trim().Split(' ')[0].Trim();
                        }
                    }

                    string[] array = GetDate(alerte.Num_pub);
                    alerte.Date_debut = array[0];
                    alerte.Date_fin = array[1];

                    if (Directory.Exists(Server.MapPath("~") + "\\Assets\\Brand_image"))
                    {
                        try
                        {
                            webClient.DownloadFile($"https://www3.wipo.int/madrid/monitor/jsp/data.jsp?KEY=ROM.{(gridViewRow.FindControl("lbl_Marque_contester_reference") as Label).Text}&TYPE=jpg&qi=1-PS2TOI9DFDkXQAToGMd8/SAKSvCctEImcTosQOWerQs=", Server.MapPath("~") + $"\\Assets\\Brand_image\\{(gridViewRow.FindControl("lbl_Marque_contester_reference") as Label).Text}.jpg");
                        }
                        catch (Exception) { }
                    }
                }
                catch (Exception ex)
                {
                    marq_contester_completed = false;
                }
            }
            driver.Quit();
            driver.Dispose();
            driver = null;
            if (marq_anter_completed && marq_contester_completed)
            {
                if (Directory.Exists(Server.MapPath("~") + "\\Assets\\Brand_image"))
                {
                    string[] files = Directory.GetFiles(Server.MapPath("~") + "\\Assets\\Brand_image");

                    foreach (var file in files)
                    {
                        var array = file.Split('\\', '.');

                        if (array[array.Length - 2].Equals(marque_anterieure.Numero))
                        {
                            try
                            {
                                System.Drawing.Image img = System.Drawing.Image.FromFile(file);
                                Bitmap bitmap = new Bitmap(img);
                                System.Drawing.Image image = bitmap;
                                MemoryStream ms = new MemoryStream();
                                image.Save(ms, img.RawFormat);
                                marque_anterieure.Image = ms.ToArray();
                                break;
                            }
                            catch (Exception) { break; }
                        }

                    }

                    foreach (var file in files)
                    {
                        var array = file.Split('\\', '.');

                        if (array[array.Length - 2].Equals(marque_à_contester.Numero))
                        {
                            try
                            {
                                System.Drawing.Image img = System.Drawing.Image.FromFile(file);
                                Bitmap bitmap = new Bitmap(img);
                                System.Drawing.Image image = bitmap;
                                MemoryStream ms = new MemoryStream();
                                image.Save(ms, img.RawFormat);
                                marque_à_contester.Image = ms.ToArray();
                                break;
                            }
                            catch (Exception) { break; }
                        }

                    }
                    if (marque_anterieure.Image == null)
                    {
                        try
                        {

                            webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{marque_anterieure.Numero}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{marque_anterieure.Numero}.jpg");
                            System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\{marque_anterieure.Numero}.jpg");
                            Bitmap bitmap = new Bitmap(img);
                            System.Drawing.Image image = bitmap;
                            MemoryStream memS = new MemoryStream();
                            image.Save(memS, img.RawFormat);
                            marque_anterieure.Image = memS.ToArray();


                        }
                        catch (Exception)
                        {
                            Bitmap bmp = new Bitmap(600, 600);
                            RectangleF rectf = new RectangleF(0, 0, bmp.Width, bmp.Height);
                            Graphics g = Graphics.FromImage(bmp);
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                            StringFormat format = new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            };
                            g.FillRectangle(Brushes.White, rectf);
                            g.DrawString(marque_anterieure.Nom, new Font("Tahoma", 40), Brushes.Black, rectf, format);
                            g.Flush();
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            marque_anterieure.Image = ms.ToArray();
                        }
                    }
                    if (marque_à_contester.Image == null)
                    {
                        try
                        {

                            webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{marque_à_contester.Numero}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{marque_à_contester.Numero}.jpg");
                            System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\{marque_à_contester.Numero}.jpg");
                            Bitmap bitmap = new Bitmap(img);
                            System.Drawing.Image image = bitmap;
                            MemoryStream memS = new MemoryStream();
                            image.Save(memS, img.RawFormat);
                            marque_à_contester.Image = memS.ToArray();


                        }
                        catch (Exception)
                        {
                            Bitmap bmp = new Bitmap(600, 600);
                            RectangleF rectf = new RectangleF(0, 0, bmp.Width, bmp.Height);
                            Graphics g = Graphics.FromImage(bmp);
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                            StringFormat format = new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            };
                            g.FillRectangle(Brushes.White, rectf);
                            g.DrawString(marque_à_contester.Nom, new Font("Tahoma", 40), Brushes.Black, rectf, format);
                            g.Flush();
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            marque_à_contester.Image = ms.ToArray();
                        }
                    }
                    CrystalReport6 reportang = new CrystalReport6();
                    List<Marque_Model_App_V1> temp_list1 = new List<Marque_Model_App_V1>();
                    List<Marque_Model_App_V1> temp_list2 = new List<Marque_Model_App_V1>();
                    List<Alerte> temp_list3 = new List<Alerte>();

                    temp_list1.Clear();
                    temp_list2.Clear();
                    temp_list3.Clear();
                    temp_list1.Add(marque_à_contester);
                    temp_list2.Add(marque_anterieure);
                    temp_list3.Add(alerte);
                    reportang.Database.Tables["Marque_contester"].SetDataSource(temp_list1.AsEnumerable());
                    reportang.Database.Tables["Marque_anterieure"].SetDataSource(temp_list2.AsEnumerable());
                    reportang.Database.Tables["Alerte"].SetDataSource(temp_list3.AsEnumerable());
                    string filenamee = $"[DEADLINE {alerte.Date_fin.Replace('/', '-')}] - ALERTE DEPOT FRAUDULEUX DE LA MARQUE {marque_à_contester.Nom} VS {marque_anterieure.Nom} - {marque_anterieure.Titulaire} - GAZETTE N°{alerte.Num_pub.Replace('/', '-')}.pdf";
                    filenamee = filenamee.Replace('/', ' ').Replace('\\', ' ').Replace(":", " ").Replace("*", "").Replace("\"", "").Replace("?", "").Replace("|", "").Replace(","," ").Replace("(", " ").Replace(")", " ");
                    Stream stream = reportang.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    reportang.Close();
                    reportang.Dispose();
                    MemoryStream memoryS = new MemoryStream();
                    stream.CopyTo(memoryS);
                    byte[] buffer = memoryS.ToArray();
                    HttpCookie httpCookie = Request.Cookies["Userinfo"];
                    string name = httpCookie["Username"];
                    string type = "Renouve";
                    storepdf(buffer, name, type);

                    Response.AddHeader("Content-Length", buffer.Length.ToString());
                    Response.AddHeader("Content-Disposition", $"attachment; filename={filenamee}");
                    Response.OutputStream.Write(buffer, 0, buffer.Length);
                    Response.End();

                }
            }
        }
        public static void storepdf(byte[] buffer, string name, string type)
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
            nb = 1000 + nb;
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


        protected void ShowData()
        {
            GridView1.DataSource = Session["List_alerte"] as List<Alerte>;
            GridView1.DataBind();
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.  
            GridView1.EditIndex = e.NewEditIndex;
            ShowData();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var alertes = Session["List_alerte"] as List<Alerte>;
            alertes[e.RowIndex].Marque_anterieure = (GridView1.Rows[e.RowIndex].FindControl("TxtBox_Marque_anterieure") as TextBox).Text;
            alertes[e.RowIndex].Marque_contester = (GridView1.Rows[e.RowIndex].FindControl("TxtBox_Marque_contester") as TextBox).Text;
            alertes[e.RowIndex].Marque_anterieure_reference = (GridView1.Rows[e.RowIndex].FindControl("TxtBox_Marque_anterieure_reference") as TextBox).Text;
            alertes[e.RowIndex].Marque_contester_reference = (GridView1.Rows[e.RowIndex].FindControl("TxtBox_Marque_contester_reference") as TextBox).Text;
            alertes[e.RowIndex].Nature_marque_anterieure = (GridView1.Rows[e.RowIndex].FindControl("TxtBox_Nature_marque_anterieure") as TextBox).Text;
            alertes[e.RowIndex].Nature_marque_contester = (GridView1.Rows[e.RowIndex].FindControl("TxtBox_Nature_marque_contester") as TextBox).Text;
            alertes[e.RowIndex].Num_pub = (GridView1.Rows[e.RowIndex].FindControl("TxtBox_Num_pub") as TextBox).Text;
            Session["List_alerte"] = alertes;
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView1.EditIndex = -1;
            ShowData();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView1.EditIndex = -1;
            ShowData();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            Button Delete = sender as Button;
            GridViewRow gridViewRow = Delete.NamingContainer as GridViewRow;
            var alertes = Session["List_alerte"] as List<Alerte>;
            alertes.RemoveAt(gridViewRow.RowIndex);
            Session["List_alerte"] = alertes;
            ShowData();
        }

        protected void btn_ajouter_alerte_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ajouter alerte.aspx");
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

