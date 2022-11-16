using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Opposition_Generateur.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using OpenQA.Selenium.Support.UI;
using ExcelDataReader;
using System.Data;
using ClosedXML.Excel;
using System.Text;
using Opposition_Generateur.Crystal_report;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Net;
using System.Data.SqlClient;


using System.Drawing.Imaging;

using System.Configuration;

using OpenQA.Selenium.Firefox;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support;
using SeleniumExtras.WaitHelpers;


namespace Opposition_Generateur.Views
{
    public partial class Recherche_phonetique : System.Web.UI.Page
    {
        public List<Model> Empty_list = new List<Model>();
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
                    GridView1.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
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

        protected void btn_Historique_Click(object sender, EventArgs e)
        {
            Response.Redirect("Historique.aspx");
        }

        protected void btn_ajouter_alerte_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ajouter alerte.aspx");
        }
        protected void Archive_Click(object sender, EventArgs e)
        {
            Response.Redirect("archive.aspx");
        }
        protected void btn_generer_doc_Click(object sender, EventArgs e)
        {
            Response.Redirect("Generer pdf.aspx");
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

        protected void Filtrer_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                List<Marque_Model_App_V2> list_marque_ipreport = new List<Marque_Model_App_V2>();
                List<Marque_Model_App_V2> list_marque_portefeuille = new List<Marque_Model_App_V2>();

                List<List<Marque_Model_App_V2>> list_marques_similaire = new List<List<Marque_Model_App_V2>>();


                List<string> list_letter_à_ignorer = new List<string>();
                ReadWordslistFile(list_letter_à_ignorer);

                bool SoundexChk = Soundex.Checked;
                bool ParametreChk = Parametre.Checked;
                bool ContainsChk = Contains.Checked;
                bool DifferenceChk = Difference.Checked;
                string numPub = num_publication.Value;
                if (ImagesgazetteUpload.PostedFiles.Count > 0)
                {
                    foreach (var postedfile in ImagesgazetteUpload.PostedFiles)
                    {
                        if (!string.IsNullOrWhiteSpace(postedfile.FileName))
                        {
                            postedfile.SaveAs(Server.MapPath("~") + $"\\Assets\\Brand_image\\{postedfile.FileName}");
                        }
                    }
                }
                if (IpreportUpload.PostedFile.FileName.Contains("xls") || IpreportUpload.PostedFile.FileName.Contains("xlsx"))
                {
                    DateTime dt = new DateTime();
                    IpreportUpload.PostedFile.SaveAs(Server.MapPath("~") + "\\ipreport.xlsx");
                    var fileName = Server.MapPath("~") + "\\ipreport.xlsx";

                    FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);

                    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                    var result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                    {

                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }

                    });

                    DataTable datatable = new DataTable();

                    datatable = result.Tables[0];

                    for (int index = 1; index < datatable.Rows.Count; index++)
                    {
                        Marque_Model_App_V2 marque_Model_App_V2 = new Marque_Model_App_V2();
                        byte[] tempBytes = null;
                        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][0].ToString().ToUpper());
                        marque_Model_App_V2.Nom = System.Text.Encoding.UTF8.GetString(tempBytes);
                        tempBytes = null;
                        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][1].ToString().ToUpper());
                        marque_Model_App_V2.Numero = System.Text.Encoding.UTF8.GetString(tempBytes);
                        tempBytes = null;
                        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][2].ToString().ToUpper());
                        marque_Model_App_V2.Titulaire = System.Text.Encoding.UTF8.GetString(tempBytes);

                        bool bl = DateTime.TryParse(datatable.Rows[index][3].ToString().ToUpper(), out dt);
                        if (bl)
                        {
                            marque_Model_App_V2.Date_depot = dt.ToShortDateString();
                        }
                        else
                        {
                            marque_Model_App_V2.Date_depot = "";
                        }
                        tempBytes = null;
                        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][4].ToString().ToUpper());
                        marque_Model_App_V2.Classe_nice = System.Text.Encoding.UTF8.GetString(tempBytes);
                        tempBytes = null;
                        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][5].ToString().ToUpper());
                        marque_Model_App_V2.Etat_marque = System.Text.Encoding.UTF8.GetString(tempBytes);
                        list_marque_ipreport.Add(marque_Model_App_V2);
                    }
                    stream.Dispose();
                    File.Delete(fileName);
                }
                else
                {
                    IpreportUpload.PostedFile.SaveAs(Server.MapPath("~") + "\\doc.html");
                    IWebDriver driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions(), TimeSpan.FromSeconds(120));
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                    driver.Navigate().GoToUrl(Server.MapPath("~") + "\\doc.html");

                    var childrens = driver.FindElements(By.XPath("//*[@id='content']/table/tbody/tr"));
                    byte[] tempBytes = null;
                    for (int i = 2; i <= childrens.Count; i++)
                    {

                        IWebElement ele = driver.FindElement(By.XPath($"//*[@id='content']/table/tbody/tr[{i}]"));
                        IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        executor.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
                        Marque_Model_App_V2 marque_Model = new Marque_Model_App_V2();
                        marque_Model.Numero = driver.FindElement(By.XPath($"//*[@id='content']/table/tbody/tr[{i}]/td[2]")).Text;
                        marque_Model.Date_depot = driver.FindElement(By.XPath($"//*[@id='content']/table/tbody/tr[{i}]/td[3]")).Text;
                        marque_Model.Classe_nice = driver.FindElement(By.XPath($"//*[@id='content']/table/tbody/tr[{i}]/td[4]")).Text != null ? driver.FindElement(By.XPath($"//*[@id='content']/table/tbody/tr[{i}]/td[4]")).Text.Replace(" ", "") : "";
                        if (!string.IsNullOrWhiteSpace(driver.FindElement(By.XPath($"//*[@id='content']/table/tbody/tr[{i}]/td[5]")).Text))
                        {
                            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(driver.FindElement(By.XPath($"//*[@id='content']/table/tbody/tr[{i}]/td[5]")).Text.ToUpper());
                            marque_Model.Titulaire = System.Text.Encoding.UTF8.GetString(tempBytes);
                        }
                        else
                        {
                            marque_Model.Titulaire = "";
                        }
                        list_marque_ipreport.Add(marque_Model);
                        tempBytes = null;
                    }


                    try
                    {
                        driver.Navigate().GoToUrl("http://www.directompic.ma/fr/renouv-marque");

                    }
                    catch (Exception)
                    {
                        try
                        {
                            driver.Navigate().GoToUrl("http://www.directompic.ma/fr/renouv-marque");

                        }
                        catch (Exception)
                        {

                        }
                    }
                    for (int p = 0; p < list_marque_ipreport.Count; p++)
                    {
                        tempBytes = null;
                        try
                        {

                            IWebElement Dropdown = driver.FindElement(By.XPath("//*[@id='search-form_loi']"));
                            IJavaScriptExecutor exec = (IJavaScriptExecutor)driver;
                            exec.ExecuteScript("arguments[0].scrollIntoView(true)", Dropdown);
                            SelectElement selectElement = new SelectElement(Dropdown);
                            selectElement.SelectByIndex(1);
                            IWebElement search_field1 = driver.FindElement(By.XPath("//*[@id='search-form_numeroTitre']"));
                            string temp_marq_anter7 = list_marque_ipreport[p].Numero;
                            search_field1.Clear();
                            search_field1.SendKeys(temp_marq_anter7);
                            IWebElement btn = driver.FindElement(By.XPath("//*[@id='search-form']/div[7]/button"));
                            btn.Click();
                            string nom = driver.FindElement(By.XPath("//*[@id='rowClasses']/tbody/tr[1]/td[4]/span")).Text;
                            try
                            {
                                if (nom != null)
                                {
                                    tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(nom.ToUpper());
                                    list_marque_ipreport[p].Nom = System.Text.Encoding.UTF8.GetString(tempBytes);
                                }
                            }
                            catch (Exception)
                            {

                            }

                        }
                        catch (Exception)
                        {
                            try
                            {
                                driver.Navigate().GoToUrl("http://www.directompic.ma/fr/renouv-marque");
                                IWebElement Dropdown = driver.FindElement(By.XPath("//*[@id='search-form_loi']"));
                                IJavaScriptExecutor exec = (IJavaScriptExecutor)driver;
                                exec.ExecuteScript("arguments[0].scrollIntoView(true)", Dropdown);
                                SelectElement selectElement = new SelectElement(Dropdown);
                                selectElement.SelectByIndex(1);
                                IWebElement search_field1 = driver.FindElement(By.XPath("//*[@id='search-form_numeroTitre']"));
                                string temp_marq_anter7 = list_marque_ipreport[p].Numero;
                                search_field1.Clear();
                                search_field1.SendKeys(temp_marq_anter7);
                                IWebElement btn = driver.FindElement(By.XPath("//*[@id='search-form']/div[7]/button"));
                                btn.Click();
                                string nom = driver.FindElement(By.XPath("//*[@id='rowClasses']/tbody/tr[1]/td[4]/span")).Text;
                                try
                                {
                                    if (nom != null)
                                    {
                                        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(nom.ToUpper());
                                        list_marque_ipreport[p].Nom = System.Text.Encoding.UTF8.GetString(tempBytes);
                                    }
                                }
                                catch (Exception)
                                {

                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }

                    driver.Quit();
                    driver.Dispose();
                    File.Delete(Server.MapPath("~") + "\\doc.html");
                }

                if (PortefeuilleUpload.PostedFile.FileName.Contains("xls") || PortefeuilleUpload.PostedFile.FileName.Contains("xlsx"))
                {
                    DateTime dt = new DateTime();
                    PortefeuilleUpload.PostedFile.SaveAs(Server.MapPath("~") + "\\portefeuille.xlsx");
                    var fileName = Server.MapPath("~") + "\\portefeuille.xlsx";

                    FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);

                    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                    var result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                    {

                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }

                    });

                    DataTable datatable = new DataTable();

                    datatable = result.Tables[0];

                    for (int index = 2; index < datatable.Rows.Count - 1; index++)
                    {
                        Marque_Model_App_V2 marque_Model_App_V2 = new Marque_Model_App_V2();
                        byte[] tempBytes = null;
                        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][1].ToString().ToUpper());
                        marque_Model_App_V2.Titulaire = System.Text.Encoding.UTF8.GetString(tempBytes);
                        tempBytes = null;
                        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][2].ToString().ToUpper());
                        marque_Model_App_V2.Nom = System.Text.Encoding.UTF8.GetString(tempBytes);
                        tempBytes = null;
                        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][3].ToString().ToUpper());
                        marque_Model_App_V2.Numero = System.Text.Encoding.UTF8.GetString(tempBytes);

                        bool bl = DateTime.TryParse(datatable.Rows[index][4].ToString().ToUpper(), out dt);
                        if (bl)
                        {
                            marque_Model_App_V2.Date_depot = dt.ToShortDateString();
                        }
                        else
                        {
                            marque_Model_App_V2.Date_depot = "";
                        }

                        bl = DateTime.TryParse(datatable.Rows[index][5].ToString().ToUpper(), out dt);
                        if (bl)
                        {
                            marque_Model_App_V2.Date_Expiration = dt.ToShortDateString();
                        }
                        else
                        {
                            marque_Model_App_V2.Date_Expiration = "";
                        }

                        tempBytes = null;
                        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][6].ToString().ToUpper());
                        marque_Model_App_V2.Classe_nice = System.Text.Encoding.UTF8.GetString(tempBytes);
                        tempBytes = null;
                        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][7].ToString().ToUpper());
                        marque_Model_App_V2.Etat_marque = System.Text.Encoding.UTF8.GetString(tempBytes);
                        list_marque_portefeuille.Add(marque_Model_App_V2);
                    }
                    stream.Dispose();
                    File.Delete(fileName);
                }
                WebClient webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");
                var ImagesBdPath = Server.MapPath("~") + "\\Assets\\Brand_image";
                string[] Files_array = Directory.GetFiles(ImagesBdPath);
                var Files_list = new List<string>();
                Files_list.AddRange(Files_array);

                for (int w = 0; w < list_marque_ipreport.Count; w++)
                {
                    if (!string.IsNullOrWhiteSpace(list_marque_ipreport[w].Numero))
                    {
                        try
                        {
                            if (Files_list.Contains($@"{ImagesBdPath}\{list_marque_ipreport[w].Numero}.jpg"))
                            {
                                list_marque_ipreport[w].Image = $@"{list_marque_ipreport[w].Numero}.jpg";
                            }
                            else
                            {
                                if (Files_list.Contains($@"{ImagesBdPath}\{list_marque_ipreport[w].Numero}.JPG"))
                                {
                                    list_marque_ipreport[w].Image = $@"{list_marque_ipreport[w].Numero}.JPG";
                                }
                                else
                                {
                                    if (Files_list.Contains($@"{ImagesBdPath}\{list_marque_ipreport[w].Numero}.jpeg"))
                                    {
                                        list_marque_ipreport[w].Image = $@"{list_marque_ipreport[w].Numero}.jpeg";
                                    }
                                    else
                                    {
                                        if (Files_list.Contains($@"{ImagesBdPath}\{list_marque_ipreport[w].Numero}.png"))
                                        {
                                            list_marque_ipreport[w].Image = $@"{list_marque_ipreport[w].Numero}.png";
                                        }
                                        else
                                        {
                                            if (Files_list.Contains($@"{ImagesBdPath}\{list_marque_ipreport[w].Numero}.tiff"))
                                            {
                                                list_marque_ipreport[w].Image = $@"{list_marque_ipreport[w].Numero}.tiff";
                                            }
                                            else
                                            {
                                                webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{list_marque_ipreport[w].Numero}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{list_marque_ipreport[w].Numero}.jpg");
                                                list_marque_ipreport[w].Image = $@"{list_marque_ipreport[w].Numero}.jpg";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception) { }

                    }

                }

                for (int w = 0; w < list_marque_portefeuille.Count; w++)
                {
                    if (!string.IsNullOrWhiteSpace(list_marque_portefeuille[w].Numero))
                    {
                        try
                        {
                            if (Files_list.Contains($@"{ImagesBdPath}\{list_marque_portefeuille[w].Numero}.jpg"))
                            {
                                list_marque_portefeuille[w].Image = $@"{list_marque_portefeuille[w].Numero}.jpg";
                            }
                            else
                            {
                                if (Files_list.Contains($@"{ImagesBdPath}\{list_marque_portefeuille[w].Numero}.JPG"))
                                {
                                    list_marque_portefeuille[w].Image = $@"{list_marque_portefeuille[w].Numero}.JPG";
                                }
                                else
                                {
                                    if (Files_list.Contains($@"{ImagesBdPath}\{list_marque_portefeuille[w].Numero}.jpeg"))
                                    {
                                        list_marque_portefeuille[w].Image = $@"{list_marque_portefeuille[w].Numero}.jpeg";
                                    }
                                    else
                                    {
                                        if (Files_list.Contains($@"{ImagesBdPath}\{list_marque_portefeuille[w].Numero}.png"))
                                        {
                                            list_marque_portefeuille[w].Image = $@"{list_marque_portefeuille[w].Numero}.png";
                                        }
                                        else
                                        {
                                            if (Files_list.Contains($@"{ImagesBdPath}\{list_marque_portefeuille[w].Numero}.tiff"))
                                            {
                                                list_marque_portefeuille[w].Image = $@"{list_marque_portefeuille[w].Numero}.tiff";
                                            }
                                            else
                                            {
                                                webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{list_marque_portefeuille[w].Numero}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{list_marque_portefeuille[w].Numero}.jpg");
                                                list_marque_portefeuille[w].Image = $@"{list_marque_portefeuille[w].Numero}.jpg";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception) { }
                    }

                }

                for (int i = 0; i < list_marque_ipreport.Count; i++)
                {
                    list_marques_similaire.Add(new List<Marque_Model_App_V2>());
                }

                if (SoundexChk)
                {
                    string marque = "";
                    string[] arr;
                    bool meme_marque = false;
                    bool exist = false;
                    for (int i = 0; i < list_marque_ipreport.Count; i++)
                    {

                        arr = new string[] { };
                        marque = "";
                        if (!string.IsNullOrWhiteSpace(list_marque_ipreport[i].Nom))
                        {
                            marque = list_marque_ipreport[i].Nom.ToUpper();
                            arr = marque.Split(' ');
                        }

                        foreach (var item in arr)
                        {
                            if (!string.IsNullOrWhiteSpace(item) && item.Length > 2)
                            {

                                if (list_letter_à_ignorer.Contains(item))
                                {
                                    continue;
                                }

                                for (int l = 0; l < list_marque_portefeuille.Count; l++)
                                {
                                    meme_marque = false;
                                    exist = false;
                                    if (SOUNDEX(item) == SOUNDEX(list_marque_portefeuille[l].Nom))
                                    {
                                        if (list_marque_ipreport[i].Numero == list_marque_portefeuille[l].Numero)
                                        {
                                            meme_marque = true;
                                            break;
                                        }

                                        if (!meme_marque)
                                        {
                                            for (int x = 0; x < list_marques_similaire[i].Count; x++)
                                            {
                                                if (list_marques_similaire[i][x].Numero == list_marque_portefeuille[l].Numero)
                                                {
                                                    exist = true;
                                                    break;
                                                }
                                            }

                                            if (!exist)
                                            {
                                                Marque_Model_App_V2 marque_Model1 = new Marque_Model_App_V2() { Nom = list_marque_portefeuille[l].Nom, Numero = list_marque_portefeuille[l].Numero, Titulaire = list_marque_portefeuille[l].Titulaire, Date_depot = list_marque_portefeuille[l].Date_depot, Date_Expiration = list_marque_portefeuille[l].Date_Expiration, Classe_nice = list_marque_portefeuille[l].Classe_nice, Etat_marque = list_marque_portefeuille[l].Etat_marque, Image = null };
                                                list_marques_similaire[i].Add(marque_Model1);
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                if (DifferenceChk)
                {
                    string ip_Report_marque = "";
                    string[] arr;
                    bool meme_marque = false;
                    bool exist = false;
                    for (int i = 0; i < list_marque_ipreport.Count; i++)
                    {


                        ip_Report_marque = "";
                        arr = new string[] { };

                        if (!string.IsNullOrWhiteSpace(list_marque_ipreport[i].Nom))
                        {
                            ip_Report_marque = list_marque_ipreport[i].Nom.ToUpper();
                            arr = ip_Report_marque.Split(' ');
                        }


                        foreach (var item in arr)
                        {
                            if (!string.IsNullOrWhiteSpace(item) && item.Length > 2)
                            {
                                if (list_letter_à_ignorer.Contains(item))
                                {
                                    continue;
                                }

                                for (int l = 0; l < list_marque_portefeuille.Count; l++)
                                {
                                    meme_marque = false;
                                    exist = false;
                                    if (DIFFERENCE(item, list_marque_portefeuille[l].Nom) == 4)
                                    {

                                        if (list_marque_ipreport[i].Numero == list_marque_portefeuille[l].Numero)
                                        {
                                            meme_marque = true;
                                            break;
                                        }

                                        if (!meme_marque)
                                        {
                                            for (int x = 0; x < list_marques_similaire[i].Count; x++)
                                            {
                                                if (list_marques_similaire[i][x].Numero == list_marque_portefeuille[l].Numero)
                                                {
                                                    exist = true;
                                                    break;
                                                }
                                            }

                                            if (!exist)
                                            {
                                                Marque_Model_App_V2 marque_Model1 = new Marque_Model_App_V2() { Nom = list_marque_portefeuille[l].Nom, Numero = list_marque_portefeuille[l].Numero, Titulaire = list_marque_portefeuille[l].Titulaire, Date_depot = list_marque_portefeuille[l].Date_depot, Date_Expiration = list_marque_portefeuille[l].Date_Expiration, Classe_nice = list_marque_portefeuille[l].Classe_nice, Etat_marque = list_marque_portefeuille[l].Etat_marque, Image = null };
                                                list_marques_similaire[i].Add(marque_Model1);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (ContainsChk)
                {
                    bool exist = false;
                    bool meme_marque = false;
                    string temp = "";
                    string[] arr;
                    for (int i = 0; i < list_marque_ipreport.Count; i++)
                    {

                        temp = "";
                        arr = new string[] { };

                        if (!string.IsNullOrWhiteSpace(list_marque_ipreport[i].Nom))
                        {
                            temp = list_marque_ipreport[i].Nom.ToUpper();
                            arr = temp.Split(' ');
                        }


                        foreach (var item in arr)
                        {
                            if (!string.IsNullOrWhiteSpace(item) && item.Length >= 3)
                            {

                                if (list_letter_à_ignorer.Contains(item))
                                {
                                    continue;
                                }

                                for (int w = 0; w < list_marque_portefeuille.Count; w++)
                                {
                                    if (!string.IsNullOrWhiteSpace(list_marque_portefeuille[w].Nom))
                                    {
                                        meme_marque = false;
                                        exist = false;

                                        if (list_marque_portefeuille[w].Nom.ToUpper().Contains(item))
                                        {

                                            if (list_marque_ipreport[i].Numero == list_marque_portefeuille[w].Numero)
                                            {
                                                meme_marque = true;
                                                break;
                                            }

                                            if (!meme_marque)
                                            {
                                                for (int x = 0; x < list_marques_similaire[i].Count; x++)
                                                {
                                                    if (list_marques_similaire[i][x].Numero == list_marque_portefeuille[w].Numero)
                                                    {
                                                        exist = true;
                                                        break;
                                                    }
                                                }

                                                if (!exist)
                                                {
                                                    Marque_Model_App_V2 marque_Model1 = new Marque_Model_App_V2() { Nom = list_marque_portefeuille[w].Nom, Numero = list_marque_portefeuille[w].Numero, Titulaire = list_marque_portefeuille[w].Titulaire, Date_depot = list_marque_portefeuille[w].Date_depot, Date_Expiration = list_marque_portefeuille[w].Date_Expiration, Classe_nice = list_marque_portefeuille[w].Classe_nice, Etat_marque = list_marque_portefeuille[w].Etat_marque, Image = null };
                                                    list_marques_similaire[i].Add(marque_Model1);
                                                }
                                            }

                                        }
                                        else
                                        {
                                            /*
                                            int p = 0;
                                            int m = 0;
                                            string marq_excel = list_marque_portefeuille[w].Nom.ToUpper();
                                            string str = marq_excel.Length <= item.Length ? marq_excel : item;
                                            for (int v = 0; v < str.Length; v++)
                                            {
                                                if (str == item)
                                                {
                                                    if (marq_excel[v] == str[v])
                                                    {
                                                        p++;
                                                    }
                                                    else
                                                    {
                                                        m++;
                                                    }
                                                }
                                                else
                                                {
                                                    if (item[v] == str[v])
                                                    {
                                                        p++;
                                                    }
                                                    else
                                                    {
                                                        m++;
                                                    }
                                                }
                                            }
                                            if (p > m)
                                            {
                                                meme_marque = false;
                                                exist = false;

                                                if (list_marque_ipreport[i].Numero == list_marque_portefeuille[w].Numero)
                                                {
                                                    meme_marque = true;
                                                    break;
                                                }

                                                if (!meme_marque)
                                                {
                                                    for (int x = 0; x < list_marques_similaire[i].Count; x++)
                                                    {
                                                        if (list_marques_similaire[i][x].Numero == list_marque_portefeuille[w].Numero)
                                                        {
                                                            exist = true;
                                                            break;
                                                        }
                                                    }

                                                    if (!exist)
                                                    {
                                                        Marque_Model_App_V2 marque_Model1 = new Marque_Model_App_V2() { Nom = list_marque_portefeuille[w].Nom, Numero = list_marque_portefeuille[w].Numero, Titulaire = list_marque_portefeuille[w].Titulaire, Date_depot = list_marque_portefeuille[w].Date_depot, Date_Expiration = list_marque_portefeuille[w].Date_Expiration, Classe_nice = list_marque_portefeuille[w].Classe_nice, Etat_marque = list_marque_portefeuille[w].Etat_marque, Image = null };
                                                        Marque_Model_App_V2 marque_Model2 = new Marque_Model_App_V2() { Nom = list_marque_portefeuille[w].Nom, Numero = list_marque_portefeuille[w].Numero, Titulaire = list_marque_portefeuille[w].Titulaire, Date_depot = list_marque_portefeuille[w].Date_depot, Date_Expiration = list_marque_portefeuille[w].Date_Expiration, Classe_nice = list_marque_portefeuille[w].Classe_nice, Etat_marque = list_marque_portefeuille[w].Etat_marque, Image = null };
                                                        list_marques_similaire[i].Add(marque_Model1);
                                                        list_resultat_contains[i].Add(marque_Model2);
                                                    }
                                                }
                                            }
                                            */
                                        }
                                    }

                                }

                            }
                        }

                    }
                }

                if (ParametreChk)
                {
                    DataTable dt = new DataTable("Setting phonétique search");
                    dt.Columns.Add("Valeur 1", typeof(string));
                    dt.Columns.Add("Valeur 2", typeof(string));
                    string path = Server.MapPath("~") + "\\Setting\\" + "Setting phonétique search.xml";
                    dt.ReadXml(path);
                    string temp = "";
                    string[] arr1;
                    bool exist = false;
                    bool meme_marque = false;
                    for (int i = 0; i < list_marque_ipreport.Count; i++)
                    {

                        temp = "";
                        arr1 = new string[] { };

                        if (!string.IsNullOrWhiteSpace(list_marque_ipreport[i].Nom))
                        {

                            temp = list_marque_ipreport[i].Nom.ToUpper();
                            arr1 = temp.Split(' ');
                        }

                        foreach (var item in arr1)
                        {
                            for (int w = 0; w < list_marque_portefeuille.Count; w++)
                            {

                                if (!string.IsNullOrWhiteSpace(item) && item.Length > 3)
                                {

                                    if (list_letter_à_ignorer.Contains(item))
                                    {
                                        continue;
                                    }

                                    if (!string.IsNullOrWhiteSpace(list_marque_portefeuille[w].Nom))
                                    {
                                        meme_marque = false;
                                        exist = false;

                                        if (list_marque_portefeuille[w].Nom.ToUpper().Contains(item))
                                        {

                                            if (list_marque_ipreport[i].Numero == list_marque_portefeuille[w].Numero)
                                            {
                                                meme_marque = true;
                                                break;
                                            }

                                            if (!meme_marque)
                                            {
                                                for (int x = 0; x < list_marques_similaire[i].Count; x++)
                                                {
                                                    if (list_marques_similaire[i][x].Numero == list_marque_portefeuille[w].Numero)
                                                    {
                                                        exist = true;
                                                        break;
                                                    }
                                                }

                                                if (!exist)
                                                {
                                                    Marque_Model_App_V2 marque_Model1 = new Marque_Model_App_V2() { Nom = list_marque_portefeuille[w].Nom, Numero = list_marque_portefeuille[w].Numero, Titulaire = list_marque_portefeuille[w].Titulaire, Date_depot = list_marque_portefeuille[w].Date_depot, Date_Expiration = list_marque_portefeuille[w].Date_Expiration, Classe_nice = list_marque_portefeuille[w].Classe_nice, Etat_marque = list_marque_portefeuille[w].Etat_marque, Image = null };
                                                    list_marques_similaire[i].Add(marque_Model1);
                                                }
                                            }

                                        }
                                    }
                                }

                            }
                        }

                        string[] arr;
                        foreach (DataRow row in dt.Rows)
                        {
                            arr = new string[] { };
                            if (!string.IsNullOrWhiteSpace(temp))
                            {
                                temp = temp.Replace(row[0].ToString().ToUpper(), row[1].ToString().ToUpper());
                                arr = temp.Split(' ');
                            }

                            foreach (var item in arr)
                            {
                                for (int w = 0; w < list_marque_portefeuille.Count; w++)
                                {
                                    if (!string.IsNullOrWhiteSpace(item) && item.Length > 3)
                                    {

                                        if (list_letter_à_ignorer.Contains(item))
                                        {
                                            continue;
                                        }
                                        if (!string.IsNullOrWhiteSpace(list_marque_portefeuille[w].Nom))
                                        {
                                            meme_marque = false;
                                            exist = false;

                                            if (list_marque_portefeuille[w].Nom.ToUpper().Contains(item))
                                            {

                                                if (list_marque_ipreport[i].Numero == list_marque_portefeuille[w].Numero)
                                                {
                                                    meme_marque = true;
                                                    break;
                                                }

                                                if (!meme_marque)
                                                {
                                                    for (int x = 0; x < list_marques_similaire[i].Count; x++)
                                                    {
                                                        if (list_marques_similaire[i][x].Numero == list_marque_portefeuille[w].Numero)
                                                        {
                                                            exist = true;
                                                            break;
                                                        }
                                                    }

                                                    if (!exist)
                                                    {
                                                        Marque_Model_App_V2 marque_Model1 = new Marque_Model_App_V2() { Nom = list_marque_portefeuille[w].Nom, Numero = list_marque_portefeuille[w].Numero, Titulaire = list_marque_portefeuille[w].Titulaire, Date_depot = list_marque_portefeuille[w].Date_depot, Date_Expiration = list_marque_portefeuille[w].Date_Expiration, Classe_nice = list_marque_portefeuille[w].Classe_nice, Etat_marque = list_marque_portefeuille[w].Etat_marque, Image = null };
                                                        list_marques_similaire[i].Add(marque_Model1);
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }

                        foreach (DataRow row in dt.Rows)
                        {
                            arr = new string[] { };
                            if (!string.IsNullOrWhiteSpace(temp))
                            {
                                temp = temp.Replace(row[1].ToString().ToUpper(), row[0].ToString().ToUpper());
                                arr = temp.Split(' ');
                            }
                            foreach (var item in arr)
                            {
                                for (int w = 0; w < list_marque_portefeuille.Count; w++)
                                {
                                    if (!string.IsNullOrWhiteSpace(item) && item.Length > 3)
                                    {
                                        if (list_letter_à_ignorer.Contains(item))
                                        {
                                            continue;
                                        }
                                        if (!string.IsNullOrWhiteSpace(list_marque_portefeuille[w].Nom))
                                        {
                                            meme_marque = false;
                                            exist = false;

                                            if (list_marque_portefeuille[w].Nom.ToUpper().Contains(item))
                                            {

                                                if (list_marque_ipreport[i].Numero == list_marque_portefeuille[w].Numero)
                                                {
                                                    meme_marque = true;
                                                    break;
                                                }

                                                if (!meme_marque)
                                                {
                                                    for (int x = 0; x < list_marques_similaire[i].Count; x++)
                                                    {
                                                        if (list_marques_similaire[i][x].Numero == list_marque_portefeuille[w].Numero)
                                                        {
                                                            exist = true;
                                                            break;
                                                        }
                                                    }

                                                    if (!exist)
                                                    {
                                                        Marque_Model_App_V2 marque_Model1 = new Marque_Model_App_V2() { Nom = list_marque_portefeuille[w].Nom, Numero = list_marque_portefeuille[w].Numero, Titulaire = list_marque_portefeuille[w].Titulaire, Date_depot = list_marque_portefeuille[w].Date_depot, Date_Expiration = list_marque_portefeuille[w].Date_Expiration, Classe_nice = list_marque_portefeuille[w].Classe_nice, Etat_marque = list_marque_portefeuille[w].Etat_marque, Image = null };
                                                        list_marques_similaire[i].Add(marque_Model1);

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

                for (int i = 0; i < list_marques_similaire.Count; i++)
                {
                    for (int o = 0; o < list_marques_similaire[i].Count; o++)
                    {
                        for (int p = 0; p < list_marque_portefeuille.Count; p++)
                        {
                            if (list_marques_similaire[i][o].Numero == list_marque_portefeuille[p].Numero)
                            {
                                list_marques_similaire[i][o].Image = list_marque_portefeuille[p].Image;
                                break;
                            }
                        }
                    }
                }


                string[] tab = GetDate(numPub);
                Alerte alerte = new Alerte();
                alerte.Num_pub = numPub;
                alerte.Date_debut = tab[0];
                alerte.Date_fin = tab[1];

                Session["marques similaire"] = list_marques_similaire;
                Session["marques ip report"] = list_marque_ipreport;
                Session["index"] = 0;
                Session["alerte"] = alerte;

                var Ipreport = list_marque_ipreport.Take(8).ToList();
                var MarqueSimilaire = list_marques_similaire.Take(8).ToList();

                double nb = list_marque_ipreport.Count / 8.0;
                int pages = int.Parse(Math.Ceiling(nb).ToString());
                Session["pages"] = pages;
                List<Model> Models = new List<Model>();

                string date_depot1 = "";
                string date_depot2 = "";
                string titulaire1 = "";
                string titulaire2 = "";
                string etat_marque = "";
                bool etat = false;

                for (int i = 0; i < MarqueSimilaire.Count; i++)
                {
                    etat = false;
                    for (int c = 0; c < MarqueSimilaire[i].Count; c++)
                    {

                        if (!string.IsNullOrWhiteSpace(MarqueSimilaire[i][c].Date_depot) && !string.IsNullOrWhiteSpace(Ipreport[i].Date_depot) && DateTime.Compare(DateTime.Parse(MarqueSimilaire[i][c].Date_depot), DateTime.Parse(Ipreport[i].Date_depot)) > 0)
                        {
                            date_depot1 = "<strong>Date dépot : </strong>" + $"<span class='orange'>{Ipreport[i].Date_depot}</span>";
                            date_depot2 = "<strong>Date dépot : </strong>" + $"<span class='orange'>{MarqueSimilaire[i][c].Date_depot}</span>";
                        }
                        else
                        {
                            date_depot1 = "<strong>Date dépot : </strong>" + Ipreport[i].Date_depot;
                            date_depot2 = "<strong>Date dépot : </strong>" + MarqueSimilaire[i][c].Date_depot;
                        }
                        if (MarqueSimilaire[i][c].Titulaire == Ipreport[i].Titulaire)
                        {
                            titulaire1 = "<strong>Titulaire : </strong>" + $"<span class='blue'>{Ipreport[i].Titulaire}</span>";
                            titulaire2 = "<strong>Titulaire : </strong>" + $"<span class='blue'>{MarqueSimilaire[i][c].Titulaire}</span>";
                        }
                        else
                        {
                            titulaire1 = "<strong>Titulaire : </strong>" + Ipreport[i].Titulaire;
                            titulaire2 = "<strong>Titulaire : </strong>" + MarqueSimilaire[i][c].Titulaire;
                        }
                        etat = MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("motifs") || MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("absolus") ||
                        MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("opposition") || MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("retiree") ||
                        MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("rejetee") ? true : false;
                        if (etat)
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='purple'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }

                        if (MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("rejetee"))
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='red'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }
                        else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("EN EXAMEN DES MOTIFS ABSOLUS"))
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='orange'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }
                        else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("ENREGISTREE"))
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='green'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }
                        else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("PUBLIEE"))
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='blue'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }
                        else
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + MarqueSimilaire[i][c].Etat_marque;
                        }
                        Model model = new Model();
                        model.MarqueIpReport = Ipreport[i].Nom;
                        model.MarqueSimilaire = MarqueSimilaire[i][c].Nom;
                        model.DetailsMarqueIpReport = "<strong>Nom Marque : </strong>" + Ipreport[i].Nom + "<br/>" + "<strong>Numero Marque : </strong>" + Ipreport[i].Numero + "<br/>" + titulaire1 + "<br/>" + date_depot1 + "<br/>" + "<strong>Classe nice : </strong>" + Ipreport[i].Classe_nice;
                        model.DetailsMarqueSimilaire = "<strong>Nom Marque : </strong>" + MarqueSimilaire[i][c].Nom + "<br/>" + "<strong>Numero Marque : </strong>" + MarqueSimilaire[i][c].Numero + "<br/>" + titulaire2 + "<br/>" + date_depot2 + "<br/>" + "<strong>Date éxpiration : </strong>" + MarqueSimilaire[i][c].Date_Expiration + "<br/>" + etat_marque + "<br/>" + "<strong>Classe nice : </strong>" + MarqueSimilaire[i][c].Classe_nice;
                        model.ImageMarqueIpReport = Ipreport[i].Image;
                        model.ImageMarqueSimilaire = MarqueSimilaire[i][c].Image;
                        Models.Add(model);
                    }

                }

                GridView1.DataSource = Models;
                GridView1.DataBind();
                index.Text = 1 + " / " + (pages == 0 ? 1 : pages);

            }
        }

        protected void Precedent_Click(object sender, EventArgs e)
        {
            if (Session["marques similaire"] != null && Session["marques ip report"] != null && Session["pages"] != null && Session["index"] != null)
            {
                int v = int.Parse(Session["index"].ToString());

                if (v > 0)
                {
                    v--;
                    var list_marques_similaire = Session["marques similaire"] as List<List<Marque_Model_App_V2>>;
                    var list_marque_ipreport = Session["marques ip report"] as List<Marque_Model_App_V2>;
                    var Ipreport = list_marque_ipreport.Skip(v * 8).Take(8).ToList();
                    var MarqueSimilaire = list_marques_similaire.Skip(v * 8).Take(8).ToList();
                    int pages = int.Parse(Session["pages"].ToString());
                    List<Model> Models = new List<Model>();

                    string date_depot1 = "";
                    string date_depot2 = "";
                    string titulaire1 = "";
                    string titulaire2 = "";
                    string etat_marque = "";
                    bool etat = false;

                    for (int i = 0; i < MarqueSimilaire.Count; i++)
                    {
                        etat = false;
                        for (int c = 0; c < MarqueSimilaire[i].Count; c++)
                        {

                            if (!string.IsNullOrWhiteSpace(MarqueSimilaire[i][c].Date_depot) && !string.IsNullOrWhiteSpace(Ipreport[i].Date_depot) && DateTime.Compare(DateTime.Parse(MarqueSimilaire[i][c].Date_depot), DateTime.Parse(Ipreport[i].Date_depot)) > 0)
                            {
                                date_depot1 = "<strong>Date dépot : </strong>" + $"<span class='orange'>{Ipreport[i].Date_depot}</span>";
                                date_depot2 = "<strong>Date dépot : </strong>" + $"<span class='orange'>{MarqueSimilaire[i][c].Date_depot}</span>";
                            }
                            else
                            {
                                date_depot1 = "<strong>Date dépot : </strong>" + Ipreport[i].Date_depot;
                                date_depot2 = "<strong>Date dépot : </strong>" + MarqueSimilaire[i][c].Date_depot;
                            }
                            if (MarqueSimilaire[i][c].Titulaire == Ipreport[i].Titulaire)
                            {
                                titulaire1 = "<strong>Titulaire : </strong>" + $"<span class='blue'>{Ipreport[i].Titulaire}</span>";
                                titulaire2 = "<strong>Titulaire : </strong>" + $"<span class='blue'>{MarqueSimilaire[i][c].Titulaire}</span>";
                            }
                            else
                            {
                                titulaire1 = "<strong>Titulaire : </strong>" + Ipreport[i].Titulaire;
                                titulaire2 = "<strong>Titulaire : </strong>" + MarqueSimilaire[i][c].Titulaire;
                            }
                            etat = MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("motifs") || MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("absolus") ||
                            MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("opposition") || MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("retiree") ||
                            MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("rejetee") ? true : false;
                            if (etat)
                            {
                                etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='purple'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                            }

                            if (MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("rejetee"))
                            {
                                etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='red'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                            }
                            else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("EN EXAMEN DES MOTIFS ABSOLUS"))
                            {
                                etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='orange'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                            }
                            else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("ENREGISTREE"))
                            {
                                etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='green'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                            }
                            else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("PUBLIEE"))
                            {
                                etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='blue'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                            }
                            else
                            {
                                etat_marque = "<strong>Etat de marque : </strong>" + MarqueSimilaire[i][c].Etat_marque;
                            }
                            Model model = new Model();
                            model.MarqueIpReport = Ipreport[i].Nom;
                            model.MarqueSimilaire = MarqueSimilaire[i][c].Nom;
                            model.DetailsMarqueIpReport = "<strong>Nom Marque : </strong>" + Ipreport[i].Nom + "<br/>" + "<strong>Numero Marque : </strong>" + Ipreport[i].Numero + "<br/>" + titulaire1 + "<br/>" + date_depot1 + "<br/>" + "<strong>Classe nice : </strong>" + Ipreport[i].Classe_nice;
                            model.DetailsMarqueSimilaire = "<strong>Nom Marque : </strong>" + MarqueSimilaire[i][c].Nom + "<br/>" + "<strong>Numero Marque : </strong>" + MarqueSimilaire[i][c].Numero + "<br/>" + titulaire2 + "<br/>" + date_depot2 + "<br/>" + "<strong>Date éxpiration : </strong>" + MarqueSimilaire[i][c].Date_Expiration + "<br/>" + etat_marque + "<br/>" + "<strong>Classe nice : </strong>" + MarqueSimilaire[i][c].Classe_nice;
                            model.ImageMarqueIpReport = Ipreport[i].Image;
                            model.ImageMarqueSimilaire = MarqueSimilaire[i][c].Image;
                            Models.Add(model);
                        }

                    }
                    GridView1.DataSource = Models;
                    GridView1.DataBind();
                    index.Text = v + 1 + " / " + (pages == 0 ? 1 : pages);
                    Session["index"] = v;
                }
            }
        }
        protected void Suivant_Click(object sender, EventArgs e)
        {
            if (Session["marques similaire"] != null && Session["marques ip report"] != null && Session["pages"] != null && Session["index"] != null)
            {
                int v = int.Parse(Session["index"].ToString());

                if (v < int.Parse(Session["pages"].ToString()) - 1)
                {
                    v++;
                    var list_marques_similaire = Session["marques similaire"] as List<List<Marque_Model_App_V2>>;
                    var list_marque_ipreport = Session["marques ip report"] as List<Marque_Model_App_V2>;
                    var Ipreport = list_marque_ipreport.Skip(v * 8).Take(8).ToList();
                    var MarqueSimilaire = list_marques_similaire.Skip(v * 8).Take(8).ToList();
                    int pages = int.Parse(Session["pages"].ToString());
                    List<Model> Models = new List<Model>();

                    string date_depot1 = "";
                    string date_depot2 = "";
                    string titulaire1 = "";
                    string titulaire2 = "";
                    string etat_marque = "";
                    bool etat = false;

                    for (int i = 0; i < MarqueSimilaire.Count; i++)
                    {
                        etat = false;
                        for (int c = 0; c < MarqueSimilaire[i].Count; c++)
                        {

                            if (!string.IsNullOrWhiteSpace(MarqueSimilaire[i][c].Date_depot) && !string.IsNullOrWhiteSpace(Ipreport[i].Date_depot) && DateTime.Compare(DateTime.Parse(MarqueSimilaire[i][c].Date_depot), DateTime.Parse(Ipreport[i].Date_depot)) > 0)
                            {
                                date_depot1 = "<strong>Date dépot : </strong>" + $"<span class='orange'>{Ipreport[i].Date_depot}</span>";
                                date_depot2 = "<strong>Date dépot : </strong>" + $"<span class='orange'>{MarqueSimilaire[i][c].Date_depot}</span>";
                            }
                            else
                            {
                                date_depot1 = "<strong>Date dépot : </strong>" + Ipreport[i].Date_depot;
                                date_depot2 = "<strong>Date dépot : </strong>" + MarqueSimilaire[i][c].Date_depot;
                            }
                            if (MarqueSimilaire[i][c].Titulaire == Ipreport[i].Titulaire)
                            {
                                titulaire1 = "<strong>Titulaire : </strong>" + $"<span class='blue'>{Ipreport[i].Titulaire}</span>";
                                titulaire2 = "<strong>Titulaire : </strong>" + $"<span class='blue'>{MarqueSimilaire[i][c].Titulaire}</span>";
                            }
                            else
                            {
                                titulaire1 = "<strong>Titulaire : </strong>" + Ipreport[i].Titulaire;
                                titulaire2 = "<strong>Titulaire : </strong>" + MarqueSimilaire[i][c].Titulaire;
                            }
                            etat = MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("motifs") || MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("absolus") ||
                            MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("opposition") || MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("retiree") ||
                            MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("rejetee") ? true : false;
                            if (etat)
                            {
                                etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='purple'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                            }

                            if (MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("rejetee"))
                            {
                                etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='red'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                            }
                            else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("EN EXAMEN DES MOTIFS ABSOLUS"))
                            {
                                etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='orange'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                            }
                            else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("ENREGISTREE"))
                            {
                                etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='green'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                            }
                            else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("PUBLIEE"))
                            {
                                etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='blue'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                            }
                            else
                            {
                                etat_marque = "<strong>Etat de marque : </strong>" + MarqueSimilaire[i][c].Etat_marque;
                            }
                            Model model = new Model();
                            model.MarqueIpReport = Ipreport[i].Nom;
                            model.MarqueSimilaire = MarqueSimilaire[i][c].Nom;
                            model.DetailsMarqueIpReport = "<strong>Nom Marque : </strong>" + Ipreport[i].Nom + "<br/>" + "<strong>Numero Marque : </strong>" + Ipreport[i].Numero + "<br/>" + titulaire1 + "<br/>" + date_depot1 + "<br/>" + "<strong>Classe nice : </strong>" + Ipreport[i].Classe_nice;
                            model.DetailsMarqueSimilaire = "<strong>Nom Marque : </strong>" + MarqueSimilaire[i][c].Nom + "<br/>" + "<strong>Numero Marque : </strong>" + MarqueSimilaire[i][c].Numero + "<br/>" + titulaire2 + "<br/>" + date_depot2 + "<br/>" + "<strong>Date éxpiration : </strong>" + MarqueSimilaire[i][c].Date_Expiration + "<br/>" + etat_marque + "<br/>" + "<strong>Classe nice : </strong>" + MarqueSimilaire[i][c].Classe_nice;
                            model.ImageMarqueIpReport = Ipreport[i].Image;
                            model.ImageMarqueSimilaire = MarqueSimilaire[i][c].Image;
                            Models.Add(model);
                        }

                    }
                    GridView1.DataSource = Models;
                    GridView1.DataBind();
                    index.Text = v + 1 + " / " + (pages == 0 ? 1 : pages);
                    Session["index"] = v;
                }
            }
        }
        protected void NumPubValidator_ServerValidate(object source, ServerValidateEventArgs args)
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

        protected void IpreportUploadValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (IpreportUpload.PostedFile.FileName.Contains("html") || IpreportUpload.PostedFile.FileName.Contains("htm") || IpreportUpload.PostedFile.FileName.Contains("xls") || IpreportUpload.PostedFile.FileName.Contains("xlsx"))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void PortefeuilleUploadValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (PortefeuilleUpload.PostedFile.FileName.Contains("xls") || PortefeuilleUpload.PostedFile.FileName.Contains("xlsx"))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }

        }

        protected void Validator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!Soundex.Checked && !Parametre.Checked && !Contains.Checked && !Difference.Checked)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
        public string SOUNDEX(string data)
        {
            StringBuilder result = new StringBuilder();
            if (data != null && data.Length > 0)
            {
                string previousCode, currentCode;
                result.Append(Char.ToUpper(data[0]));
                previousCode = string.Empty;
                for (int i = 1; i < data.Length; i++)
                {
                    currentCode = EncodeChar(data[i]);
                    if (currentCode != previousCode)
                        result.Append(currentCode);

                    if (result.Length == 4) break;
                    if (!currentCode.Equals(string.Empty))
                        previousCode = currentCode;
                }
            }
            if (result.Length < 4)
                result.Append(new String('0', 4 - result.Length));
            return result.ToString();
        }
        private string EncodeChar(char c)
        {
            switch (Char.ToLower(c))
            {
                case 'b':
                case 'f':
                case 'p':
                case 'v':
                    return "1";
                case 'c':
                case 'g':
                case 'j':
                case 'k':
                case 'q':
                case 's':
                case 'x':
                case 'z':
                    return "2";
                case 'd':
                case 't':
                    return "3";
                case 'l':
                    return "4";
                case 'm':
                case 'n':
                    return "5";
                case 'r':
                    return "6";
                default:
                    return string.Empty;
            }
        }
        // 0 means Not Matched
        // 4 means Strongly Matched
        public int DIFFERENCE(string data1, string data2)
        {
            int result = 0;
            if (data1.Equals(string.Empty) || data2.Equals(string.Empty))
                return 0;
            string soundex1 = SOUNDEX(data1);
            string soundex2 = SOUNDEX(data2);
            if (soundex1.Equals(soundex2))
                result = 4;
            else
            {
                if (soundex1[0] == soundex2[0])
                    result = 1;
                string sub1 = soundex1.Substring(1, 3); //characters 2, 3, 4
                if (soundex2.IndexOf(sub1) > -1)
                {
                    result += 3;
                    return result;
                }
                string sub2 = soundex1.Substring(2, 2); //characters 3, 4
                if (soundex2.IndexOf(sub2) > -1)
                {
                    result += 2;
                    return result;
                }
                string sub3 = soundex1.Substring(1, 2); //characters 2, 3
                if (soundex2.IndexOf(sub3) > -1)
                {
                    result += 2;
                    return result;
                }
                char sub4 = soundex1[1];
                if (soundex2.IndexOf(sub4) > -1)
                    result++;
                char sub5 = soundex1[2];
                if (soundex2.IndexOf(sub5) > -1)
                    result++;
                char sub6 = soundex1[3];
                if (soundex2.IndexOf(sub6) > -1)
                    result++;
            }
            return result;
        }
        public void ReadWordslistFile(List<string> listWords)
        {
            string PATHH = Server.MapPath("~") + "\\Setting\\" + "Words list.txt";
            if (File.Exists(PATHH))
            {
                // Open the file to read from.
                using (StreamReader sr = File.OpenText(PATHH))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        listWords.Add(s.Trim().ToUpper());
                    }
                }
            }
            else
            {
                StreamWriter sw = File.CreateText(PATHH);
                sw.Close();
                sw.Dispose();
            }
        }

        protected void Generate_doc_Click(object sender, EventArgs e)
        {
            //CrystalReport1 report = new CrystalReport1();
            //List<Marque_Model_App_V1> temp_list1 = new List<Marque_Model_App_V1>();
            //List<Marque_Model_App_V1> temp_list2 = new List<Marque_Model_App_V1>();
            //List<Alerte> temp_list3 = new List<Alerte>();
            //int i = int.Parse(Session["index"].ToString());
            //var list_marques_similaire = (Session["marques similaire"] as List<List<Marque_Model_App_V2>>).Skip(i * 8).Take(8).ToList();
            //var list_marque_ipreport = (Session["marques ip report"] as List<Marque_Model_App_V2>).Skip(i * 8).Take(8).ToList();
            //Button generer_doc = sender as Button;
            //GridViewRow gridViewRow = generer_doc.NamingContainer as GridViewRow;
            //int counter = -1;
            //for (int s = 0; s < list_marques_similaire.Count; s++)
            //{
            //    for (int w = 0; w < list_marques_similaire[s].Count; w++)
            //    {
            //        counter++;
            //        if (gridViewRow.RowIndex == counter)
            //        {
            //            Marque_Model_App_V1 marque_à_contester = new Marque_Model_App_V1();
            //            marque_à_contester.Numero = list_marque_ipreport[s].Numero;
            //            marque_à_contester.Nom = list_marque_ipreport[s].Nom;
            //            marque_à_contester.Date_depot = list_marque_ipreport[s].Date_depot;
            //            marque_à_contester.Titulaire = list_marque_ipreport[s].Titulaire;
            //            marque_à_contester.Classe_nice = list_marque_ipreport[s].Classe_nice;
            //            if (!string.IsNullOrWhiteSpace(list_marque_ipreport[s].Image))
            //            {
            //                try
            //                {
            //                    MemoryStream ms = new MemoryStream();
            //                    System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("~") + "\\Assets\\Brand_image\\" + list_marque_ipreport[s].Image);
            //                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //                    marque_à_contester.Image = ms.ToArray();
            //                }
            //                catch (Exception) { }
            //            }
            //            Marque_Model_App_V1 marque_anterieure = new Marque_Model_App_V1();
            //            marque_anterieure.Numero = list_marques_similaire[s][w].Numero;
            //            marque_anterieure.Nom = list_marques_similaire[s][w].Nom;
            //            marque_anterieure.Date_depot = list_marques_similaire[s][w].Date_depot;
            //            marque_anterieure.Titulaire = list_marques_similaire[s][w].Titulaire;
            //            marque_anterieure.Classe_nice = list_marques_similaire[s][w].Classe_nice;
            //            if (!string.IsNullOrWhiteSpace(list_marques_similaire[s][w].Image))
            //            {
            //                try
            //                {
            //                    MemoryStream ms = new MemoryStream();
            //                    System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("~") + "\\Assets\\Brand_image\\" + list_marques_similaire[s][w].Image);
            //                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //                    marque_anterieure.Image = ms.ToArray();
            //                }
            //                catch (Exception) { }
            //            }
            //            if (marque_anterieure.Image == null)
            //            {
            //                Bitmap bmp = new Bitmap(600, 600);
            //                RectangleF rectf = new RectangleF(0, 0, bmp.Width, bmp.Height);
            //                Graphics g = Graphics.FromImage(bmp);
            //                g.SmoothingMode = SmoothingMode.AntiAlias;
            //                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            //                StringFormat format = new StringFormat()
            //                {
            //                    Alignment = StringAlignment.Center,
            //                    LineAlignment = StringAlignment.Center
            //                };
            //                g.FillRectangle(Brushes.White, rectf);
            //                g.DrawString(marque_anterieure.Nom, new System.Drawing.Font("Tahoma", 40), Brushes.Black, rectf, format);
            //                g.Flush();
            //                MemoryStream ms = new MemoryStream();
            //                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //                marque_anterieure.Image = ms.ToArray();
            //            }
            //            if (marque_à_contester.Image == null)
            //            {
            //                Bitmap bmp = new Bitmap(600, 600);
            //                RectangleF rectf = new RectangleF(0, 0, bmp.Width, bmp.Height);
            //                Graphics g = Graphics.FromImage(bmp);
            //                g.SmoothingMode = SmoothingMode.AntiAlias;
            //                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            //                StringFormat format = new StringFormat()
            //                {
            //                    Alignment = StringAlignment.Center,
            //                    LineAlignment = StringAlignment.Center
            //                };
            //                g.FillRectangle(Brushes.White, rectf);
            //                g.DrawString(marque_à_contester.Nom, new System.Drawing.Font("Tahoma", 40), Brushes.Black, rectf, format);
            //                g.Flush();
            //                MemoryStream ms = new MemoryStream();
            //                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //                marque_à_contester.Image = ms.ToArray();
            //            }
            //            Alerte alerte = Session["alerte"] as Alerte;
            //            if (alerte == null)
            //            {
            //                alerte = new Alerte();
            //            }
            //            temp_list1.Clear();
            //            temp_list2.Clear();
            //            temp_list3.Clear();
            //            temp_list1.Add(marque_à_contester);
            //            temp_list2.Add(marque_anterieure);
            //            temp_list3.Add(alerte);
            //            report.Database.Tables["Marque_contester"].SetDataSource(temp_list1.AsEnumerable());
            //            report.Database.Tables["Marque_anterieure"].SetDataSource(temp_list2.AsEnumerable());
            //            report.Database.Tables["Alerte"].SetDataSource(temp_list3.AsEnumerable());
            //            string filename = $"[DEADLINE {alerte.Date_fin.Replace('/', '-')}] - ALERTE DEPOT FRAUDULEUX DE LA MARQUE {marque_à_contester.Nom} VS {marque_anterieure.Nom} - {marque_anterieure.Titulaire} - GAZETTE N°{alerte.Num_pub.Replace('/', '-')}.pdf";
            //            filename = filename.Replace('/', ' ').Replace('\\', ' ').Replace(":", " ").Replace("*", "").Replace("\"", "").Replace("?", "").Replace("|", "");
            //            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //            report.Close();
            //            report.Dispose();
            //            MemoryStream memoryS = new MemoryStream();
            //            stream.CopyTo(memoryS);
            //            byte[] buffer = memoryS.ToArray();

            //            Response.AddHeader("Content-Length", buffer.Length.ToString());
            //            Response.AddHeader("Content-Disposition", $"attachment; filename={filename}");
            //            Response.OutputStream.Write(buffer, 0, buffer.Length);
            //            Response.End();
            //        }
            //    }
            //}




            ///// new MEthode 11-11-2022
            ///
            string numPub = num_publication.Value;
            Button Visitbtn = sender as Button;
            var models = Session["Modelsmax"] as List<Model>;
            
            CrystalReport1 report = new CrystalReport1();
            List<Marque_Model_App_V1> temp_list1 = new List<Marque_Model_App_V1>();
            List<Marque_Model_App_V1> temp_list2 = new List<Marque_Model_App_V1>();
            List<Alerte> temp_list3 = new List<Alerte>();
            GridViewRow gridViewRow = Visitbtn.NamingContainer as GridViewRow;
            //List<Marque> marques = Session["Marques"] as List<Marque>;
            //Response.Write($"<script language='javascript'>window.open('http://search.ompic.ma/web/pages/consulterMarque.do?id={marques[(int.Parse(ViewState["index"].ToString()) * 8 + gridViewRow.RowIndex)].Id}','_blank');</script>");
            Model ml = models[gridViewRow.RowIndex];
            string ipreport = ml.crystalIpReport ;
            string marquesimilaire = ml.crystalMarqueSimilaire;
            String[] ipmarque = ipreport.Split('|');
            String[] marquesimi = marquesimilaire.Split('|');
            Marque_Model_App_V1 marque_à_contester = new Marque_Model_App_V1();
            marque_à_contester.Numero = ipmarque[1];
            marque_à_contester.Nom = ipmarque[0];
            marque_à_contester.Date_depot = ipmarque[2];
            marque_à_contester.Titulaire = ipmarque[3];
            marque_à_contester.Classe_nice = ipmarque[4];
            
                try
                {
                    MemoryStream ms = new MemoryStream();
                    System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("~") + "\\Assets\\Brand_image\\" + ipmarque[1]+".jpg");
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    marque_à_contester.Image = ms.ToArray();
                }
                catch (Exception) { }
            
            Marque_Model_App_V1 marque_anterieure = new Marque_Model_App_V1();
            marque_anterieure.Numero = marquesimi[1];
            marque_anterieure.Nom = marquesimi[0];
            marque_anterieure.Date_depot = marquesimi[2].Replace("00:00:00"," ");
            marque_anterieure.Titulaire = marquesimi[3];
            marque_anterieure.Classe_nice = marquesimi[4];
            
                try
                {
                    MemoryStream ms = new MemoryStream();
                    System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("~") + "\\Assets\\Brand_image\\" + marquesimi[1]+".jpg");
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    marque_anterieure.Image = ms.ToArray();
                }
                catch (Exception) { }
            
            if (marque_anterieure.Image == null)
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
                g.DrawString(marque_anterieure.Nom, new System.Drawing.Font("Tahoma", 40), Brushes.Black, rectf, format);
                g.Flush();
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                marque_anterieure.Image = ms.ToArray();
            }
            if (marque_à_contester.Image == null)
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
                g.DrawString(marque_à_contester.Nom, new System.Drawing.Font("Tahoma", 40), Brushes.Black, rectf, format);
                g.Flush();
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                marque_à_contester.Image = ms.ToArray();
            }
            SqlConnection con = new SqlConnection(@"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=data;Integrated Security=True");
            SqlCommand cmdd = new SqlCommand();
            cmdd.Connection = con;
            con.Open();
            int nb;
            cmdd.CommandText = $"select count(*) from article";
            nb = int.Parse(cmdd.ExecuteScalar().ToString());
            nb = 1000 + nb;

            DataTable dt = new DataTable("Gazette");
            dt.Columns.Add("Num_pub", typeof(string));
            dt.Columns.Add("Date", typeof(DateTime));
            string path = Server.MapPath("~") + "\\Setting\\" + "Setting.xml";
            dt.ReadXml(path);
            string date_fin = "";
            string date_debut = "";
            if (!string.IsNullOrWhiteSpace(numPub))
            {

                foreach (DataRow row in dt.Rows)
                {
                    if (row[0].ToString().Trim() == numPub.Trim())
                    {
                        date_debut = DateTime.Parse(row[1].ToString()).ToShortDateString();
                        DateTime temp = DateTime.Parse(date_debut);
                        DateTime dateTime = temp.AddMonths(2);
                        date_fin = dateTime.ToShortDateString();
                    }

                }
            }




            Alerte alerte = new Alerte();
                alerte.Num_pub = numPub;
                alerte.refalert = nb.ToString();
                alerte.Date_debut = date_debut;
                alerte.Date_fin = date_fin; 
                
            
          
            temp_list1.Clear();
            temp_list2.Clear();
            temp_list3.Clear();
            temp_list1.Add(marque_à_contester);
            temp_list2.Add(marque_anterieure);
            temp_list3.Add(alerte);
            DateTime df = DateTime.Now.Date;
            report.Database.Tables["Marque_contester"].SetDataSource(temp_list1.AsEnumerable());
            report.Database.Tables["Marque_anterieure"].SetDataSource(temp_list2.AsEnumerable());
            report.Database.Tables["Alerte"].SetDataSource(temp_list3.AsEnumerable());
            string filename = $"[DEADLINE {alerte.Date_fin.Replace('/', '-')}] - ALERTE DEPOT FRAUDULEUX DE LA MARQUE {marque_à_contester.Nom} VS {marque_anterieure.Nom} - {marque_anterieure.Titulaire} - GAZETTE N°{alerte.Num_pub.Replace('/', '-')}.pdf";
            filename = filename.Replace('/', ' ').Replace('\\', ' ').Replace(":", " ").Replace("*", "").Replace("\"", "").Replace("?", "").Replace("|", "");
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




            //foreach (GridViewRow marque in GridView1)
            //{
            //    var ms = new MemoryStream();
            //    DataRow row = table.NewRow();
            //    string imgname = "";

            //    row["NumeroTitre"] = marque.Numero_titre;

            //    if (!string.IsNullOrWhiteSpace(marque.Numero_titre))
            //    {
            //        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.jpg"))
            //        {
            //            imgname = $@"{marque.Numero_titre}.jpg";
            //        }
            //        else
            //        {
            //            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.JPG"))
            //            {
            //                imgname = $@"{marque.Numero_titre}.JPG";
            //            }
            //            else
            //            {
            //                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.jpeg"))
            //                {
            //                    imgname = $@"{marque.Numero_titre}.jpeg";
            //                }
            //                else
            //                {
            //                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marque.Numero_titre}.png"))
            //                    {
            //                        imgname = $@"{marque.Numero_titre}.png";
            //                    }
            //                    //else
            //                    //{
            //                    //    webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{marque.NumeroTitre}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{marque.NumeroTitre}.jpg");
            //                    //    marque.Image = $@"{marque.NumeroTitre}.jpg";
            //                    //}
            //                }
            //            }
            //        }
            //    }

            //    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgname))
            //    {
            //        System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\" + imgname).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //    }
            //    else
            //    {
            //        System.Drawing.Image.FromFile(Server.MapPath("~") + $@"\Assets\Brand_image\Empty.png").Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //    }

            //    row["image"] = ms.ToArray();
            //    row["Nommarque"] = marque.Nom_marque;
            //    row["Datedepot"] = marque.Date_depot;
            //    row["Dateexpiration"] = marque.Date_expiration;
            //    row["Deposant"] = marque.Applicant_name;

            //    row["Mandataire"] = marque.Representative_name;

            //    row["Statut"] = marque.Statut;
            //    row["ClasseNice"] = marque.ClasseNice;
            //    table.Rows.Add(row);



            //}
            //foreach (DataRow rt in table.Rows)
            //{

            //    rt[5] = retext(rt[5].ToString());
            //    rt[8] = retext(rt[8].ToString());
            //    rt[4] = retext(rt[4].ToString());
            //    rt[3] = retext(rt[3].ToString());
            //    rt[2] = retext(rt[2].ToString());
            //    rt[6] = retext(rt[6].ToString());
            //    rt[7] = retext(rt[7].ToString());

            //}
            //ds.Tables.Add(table);


            //CrystalReport3 report = new CrystalReport3();
            //report.Database.Tables["DataTable1"].SetDataSource(ds);
            //string filename = $"resultat_export.pdf";
            //Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //report.Close();
            //report.Dispose();
            //MemoryStream memoryS = new MemoryStream();
            //stream.CopyTo(memoryS);
            //byte[] buffer = memoryS.ToArray();


            //Response.AddHeader("Content-Length", buffer.Length.ToString());
            //Response.AddHeader("Content-Disposition", $"attachment; filename={filename}");
            //Response.OutputStream.Write(buffer, 0, buffer.Length);
            //Response.End();

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

        protected void btn_parametre_v2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Parametre.aspx");
        }

        protected void btn_parametre_v1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Parametre.aspx");
        }


        protected void Downloadipreport_Click(object sender, EventArgs e)
        {

            var list_marque_ipreport = (Session["marques ip report"] as List<Marque_Model_App_V2>);

            if (list_marque_ipreport != null)
            {
                var wbook = new XLWorkbook();

                var ws = wbook.AddWorksheet("Sheet1");

                ws.Cell(1, 1).Value = "Nom";
                ws.Cell(1, 2).Value = "Numéro";
                ws.Cell(1, 3).Value = "Titulaire";

                ws.Cell(1, 4).Value = "Date dépot";
                ws.Cell(1, 5).Value = "Classe nice";
                ws.Cell(1, 6).Value = "Etat marque";

                for (int i = 0; i < list_marque_ipreport.Count; i++)
                {
                    ws.Cell(i + 2, 1).Value = list_marque_ipreport[i].Nom;
                    ws.Cell(i + 2, 2).Value = list_marque_ipreport[i].Numero;
                    ws.Cell(i + 2, 3).Value = list_marque_ipreport[i].Titulaire;

                    ws.Cell(i + 2, 4).Value = list_marque_ipreport[i].Date_depot;
                    ws.Cell(i + 2, 5).Value = list_marque_ipreport[i].Classe_nice;
                    ws.Cell(i + 2, 6).Value = list_marque_ipreport[i].Etat_marque;
                }
                MemoryStream memoryS = new MemoryStream();
                wbook.SaveAs(memoryS);
                byte[] buffer = memoryS.ToArray();

                Response.AddHeader("Content-Length", buffer.Length.ToString());
                Response.AddHeader("Content-Disposition", $"attachment; filename=Gazette.xlsx");
                Response.OutputStream.Write(buffer, 0, buffer.Length);
                Response.End();
            }

        }

        protected void Classe_commun_Click(object sender, EventArgs e)
        {
            if (Session["marques similaire"] != null && Session["marques ip report"] != null)
            {

                var list_marques_similaire = (Session["marques similaire"] as List<List<Marque_Model_App_V2>>);
                var list_marque_ipreport = (Session["marques ip report"] as List<Marque_Model_App_V2>);

                var Old_marques_ipreport = new List<Marque_Model_App_V2>();
                var Old_marques_similaire = new List<List<Marque_Model_App_V2>>();
                for (int i = 0; i < list_marque_ipreport.Count; i++)
                {
                    Old_marques_ipreport.Add(new Marque_Model_App_V2() { Nom = list_marque_ipreport[i].Nom, Numero = list_marque_ipreport[i].Numero, Titulaire = list_marque_ipreport[i].Titulaire, Date_Expiration = list_marque_ipreport[i].Date_Expiration, Date_depot = list_marque_ipreport[i].Date_depot, Etat_marque = list_marque_ipreport[i].Etat_marque, Classe_nice = list_marque_ipreport[i].Classe_nice, Image = list_marque_ipreport[i].Image });
                    List<Marque_Model_App_V2> list = new List<Marque_Model_App_V2>();
                    for (int s = 0; s < list_marques_similaire[i].Count; s++)
                    {
                        list.Add(new Marque_Model_App_V2() { Nom = list_marques_similaire[i][s].Nom, Numero = list_marques_similaire[i][s].Numero, Titulaire = list_marques_similaire[i][s].Titulaire, Date_Expiration = list_marques_similaire[i][s].Date_Expiration, Date_depot = list_marques_similaire[i][s].Date_depot, Etat_marque = list_marques_similaire[i][s].Etat_marque, Classe_nice = list_marques_similaire[i][s].Classe_nice, Image = list_marques_similaire[i][s].Image });
                    }
                    Old_marques_similaire.Add(list);
                }
                Session["Old_marques_ipreport"] = Old_marques_ipreport;
                Session["Old_marques_similaire"] = Old_marques_similaire;

                List<string> list_nice1 = new List<string>();
                List<string> list_nice2 = new List<string>();
                List<int> list_a = new List<int>();
                List<int> list_b = new List<int>();
                bool contains = false;
                for (int i = 0; i < list_marque_ipreport.Count; i++)
                {
                    list_nice1.Clear();
                    string[] array1 = list_marque_ipreport[i].Classe_nice.Split(';');
                    foreach (var item in array1)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            list_nice1.Add(item.Trim());
                        }
                    }
                    for (int b = 0; b < list_marques_similaire[i].Count; b++)
                    {
                        list_nice2.Clear();
                        string[] array2 = list_marques_similaire[i][b].Classe_nice.Split(';');
                        foreach (var item in array2)
                        {
                            if (!string.IsNullOrWhiteSpace(item))
                            {
                                list_nice2.Add(item.Trim());

                            }
                        }
                        contains = false;
                        foreach (var item in list_nice1)
                        {
                            if (list_nice2.Contains(item))
                            {
                                contains = true;
                                break;
                            }
                        }
                        if (!contains)
                        {
                            list_a.Add(i);
                            list_b.Add(b);
                        }
                    }
                }
                for (int i = list_a.Count - 1; i >= 0; i--)
                {
                    list_marques_similaire[list_a[i]].RemoveAt(list_b[i]);
                }
                list_nice1.Clear();
                list_nice2.Clear();
                list_a.Clear();
                list_b.Clear();



                Session["index"] = 0;

                var Ipreport = list_marque_ipreport.Take(8).ToList();
                var MarqueSimilaire = list_marques_similaire.Take(8).ToList();

                double nb = list_marque_ipreport.Count / 8.0;
                int pages = int.Parse(Math.Ceiling(nb).ToString());
                Session["pages"] = pages;
                List<Model> Models = new List<Model>();

                string date_depot1 = "";
                string date_depot2 = "";
                string titulaire1 = "";
                string titulaire2 = "";
                string etat_marque = "";
                bool etat = false;

                for (int i = 0; i < MarqueSimilaire.Count; i++)
                {
                    etat = false;
                    for (int c = 0; c < MarqueSimilaire[i].Count; c++)
                    {

                        if (!string.IsNullOrWhiteSpace(MarqueSimilaire[i][c].Date_depot) && !string.IsNullOrWhiteSpace(Ipreport[i].Date_depot) && DateTime.Compare(DateTime.Parse(MarqueSimilaire[i][c].Date_depot), DateTime.Parse(Ipreport[i].Date_depot)) > 0)
                        {
                            date_depot1 = "<strong>Date dépot : </strong>" + $"<span class='orange'>{Ipreport[i].Date_depot}</span>";
                            date_depot2 = "<strong>Date dépot : </strong>" + $"<span class='orange'>{MarqueSimilaire[i][c].Date_depot}</span>";
                        }
                        else
                        {
                            date_depot1 = "<strong>Date dépot : </strong>" + Ipreport[i].Date_depot;
                            date_depot2 = "<strong>Date dépot : </strong>" + MarqueSimilaire[i][c].Date_depot;
                        }
                        if (MarqueSimilaire[i][c].Titulaire == Ipreport[i].Titulaire)
                        {
                            titulaire1 = "<strong>Titulaire : </strong>" + $"<span class='blue'>{Ipreport[i].Titulaire}</span>";
                            titulaire2 = "<strong>Titulaire : </strong>" + $"<span class='blue'>{MarqueSimilaire[i][c].Titulaire}</span>";
                        }
                        else
                        {
                            titulaire1 = "<strong>Titulaire : </strong>" + Ipreport[i].Titulaire;
                            titulaire2 = "<strong>Titulaire : </strong>" + MarqueSimilaire[i][c].Titulaire;
                        }
                        etat = MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("motifs") || MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("absolus") ||
                        MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("opposition") || MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("retiree") ||
                        MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("rejetee") ? true : false;
                        if (etat)
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='purple'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }

                        if (MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("rejetee"))
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='red'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }
                        else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("EN EXAMEN DES MOTIFS ABSOLUS"))
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='orange'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }
                        else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("ENREGISTREE"))
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='green'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }
                        else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("PUBLIEE"))
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='blue'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }
                        else
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + MarqueSimilaire[i][c].Etat_marque;
                        }
                        Model model = new Model();
                        model.MarqueIpReport = Ipreport[i].Nom;
                        model.MarqueSimilaire = MarqueSimilaire[i][c].Nom;
                        model.DetailsMarqueIpReport = "<strong>Nom Marque : </strong>" + Ipreport[i].Nom + "<br/>" + "<strong>Numero Marque : </strong>" + Ipreport[i].Numero + "<br/>" + titulaire1 + "<br/>" + date_depot1 + "<br/>" + "<strong>Classe nice : </strong>" + Ipreport[i].Classe_nice;
                        model.DetailsMarqueSimilaire = "<strong>Nom Marque : </strong>" + MarqueSimilaire[i][c].Nom + "<br/>" + "<strong>Numero Marque : </strong>" + MarqueSimilaire[i][c].Numero + "<br/>" + titulaire2 + "<br/>" + date_depot2 + "<br/>" + "<strong>Date éxpiration : </strong>" + MarqueSimilaire[i][c].Date_Expiration + "<br/>" + etat_marque + "<br/>" + "<strong>Classe nice : </strong>" + MarqueSimilaire[i][c].Classe_nice;
                        model.ImageMarqueIpReport = Ipreport[i].Image;
                        model.ImageMarqueSimilaire = MarqueSimilaire[i][c].Image;
                        Models.Add(model);
                    }

                }

                GridView1.DataSource = Models;
                GridView1.DataBind();
                index.Text = 1 + " / " + (pages == 0 ? 1 : pages);

            }
        }

        protected void Reinitialiser_Click(object sender, EventArgs e)
        {
            if (Session["Old_marques_ipreport"] != null && Session["Old_marques_similaire"] != null)
            {

                var list_marques_similaire = (Session["Old_marques_similaire"] as List<List<Marque_Model_App_V2>>);
                var list_marque_ipreport = (Session["Old_marques_ipreport"] as List<Marque_Model_App_V2>);

                Session["marques similaire"] = list_marques_similaire;
                Session["marques ip report"] = list_marque_ipreport;

                Session["index"] = 0;

                var Ipreport = list_marque_ipreport.Take(8).ToList();
                var MarqueSimilaire = list_marques_similaire.Take(8).ToList();

                double nb = list_marque_ipreport.Count / 8.0;
                int pages = int.Parse(Math.Ceiling(nb).ToString());
                Session["pages"] = pages;
                List<Model> Models = new List<Model>();

                string date_depot1 = "";
                string date_depot2 = "";
                string titulaire1 = "";
                string titulaire2 = "";
                string etat_marque = "";
                bool etat = false;

                for (int i = 0; i < MarqueSimilaire.Count; i++)
                {
                    etat = false;
                    for (int c = 0; c < MarqueSimilaire[i].Count; c++)
                    {

                        if (!string.IsNullOrWhiteSpace(MarqueSimilaire[i][c].Date_depot) && !string.IsNullOrWhiteSpace(Ipreport[i].Date_depot) && DateTime.Compare(DateTime.Parse(MarqueSimilaire[i][c].Date_depot), DateTime.Parse(Ipreport[i].Date_depot)) > 0)
                        {
                            date_depot1 = "<strong>Date dépot : </strong>" + $"<span class='orange'>{Ipreport[i].Date_depot}</span>";
                            date_depot2 = "<strong>Date dépot : </strong>" + $"<span class='orange'>{MarqueSimilaire[i][c].Date_depot}</span>";
                        }
                        else
                        {
                            date_depot1 = "<strong>Date dépot : </strong>" + Ipreport[i].Date_depot;
                            date_depot2 = "<strong>Date dépot : </strong>" + MarqueSimilaire[i][c].Date_depot;
                        }
                        if (MarqueSimilaire[i][c].Titulaire == Ipreport[i].Titulaire)
                        {
                            titulaire1 = "<strong>Titulaire : </strong>" + $"<span class='blue'>{Ipreport[i].Titulaire}</span>";
                            titulaire2 = "<strong>Titulaire : </strong>" + $"<span class='blue'>{MarqueSimilaire[i][c].Titulaire}</span>";
                        }
                        else
                        {
                            titulaire1 = "<strong>Titulaire : </strong>" + Ipreport[i].Titulaire;
                            titulaire2 = "<strong>Titulaire : </strong>" + MarqueSimilaire[i][c].Titulaire;
                        }
                        etat = MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("motifs") || MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("absolus") ||
                        MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("opposition") || MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("retiree") ||
                        MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("rejetee") ? true : false;
                        if (etat)
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='purple'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }

                        if (MarqueSimilaire[i][c].Etat_marque.ToLower().Contains("rejetee"))
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='red'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }
                        else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("EN EXAMEN DES MOTIFS ABSOLUS"))
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='orange'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }
                        else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("ENREGISTREE"))
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='green'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }
                        else if (MarqueSimilaire[i][c].Etat_marque.ToUpper().Contains("PUBLIEE"))
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='blue'>{MarqueSimilaire[i][c].Etat_marque}</span>";
                        }
                        else
                        {
                            etat_marque = "<strong>Etat de marque : </strong>" + MarqueSimilaire[i][c].Etat_marque;
                        }
                        Model model = new Model();
                        model.MarqueIpReport = Ipreport[i].Nom;
                        model.MarqueSimilaire = MarqueSimilaire[i][c].Nom;
                        model.DetailsMarqueIpReport = "<strong>Nom Marque : </strong>" + Ipreport[i].Nom + "<br/>" + "<strong>Numero Marque : </strong>" + Ipreport[i].Numero + "<br/>" + titulaire1 + "<br/>" + date_depot1 + "<br/>" + "<strong>Classe nice : </strong>" + Ipreport[i].Classe_nice;
                        model.DetailsMarqueSimilaire = "<strong>Nom Marque : </strong>" + MarqueSimilaire[i][c].Nom + "<br/>" + "<strong>Numero Marque : </strong>" + MarqueSimilaire[i][c].Numero + "<br/>" + titulaire2 + "<br/>" + date_depot2 + "<br/>" + "<strong>Date éxpiration : </strong>" + MarqueSimilaire[i][c].Date_Expiration + "<br/>" + etat_marque + "<br/>" + "<strong>Classe nice : </strong>" + MarqueSimilaire[i][c].Classe_nice;
                        model.ImageMarqueIpReport = Ipreport[i].Image;
                        model.ImageMarqueSimilaire = MarqueSimilaire[i][c].Image;
                        Models.Add(model);
                    }

                }

                GridView1.DataSource = Models;
                GridView1.DataBind();
                index.Text = 1 + " / " + (pages == 0 ? 1 : pages);
            }
        }

        protected void Rech_bd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche Bd.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void MaxFiltrer_Click(object sender, EventArgs e)
        {
            List<Marque_Model_App_V2> list_marque_ipreport = new List<Marque_Model_App_V2>();
            List<Marque_Model_App_V2> list_marque_portefeuille = new List<Marque_Model_App_V2>();

            List<List<Marque_Model_App_V2>> list_marques_similaire = new List<List<Marque_Model_App_V2>>();
            List<Model> Models = new List<Model>();


            List<string> list_letter_à_ignorer = new List<string>();
            if (IpreportUpload.PostedFile.FileName.Contains("xls") || IpreportUpload.PostedFile.FileName.Contains("xlsx"))
            {
                DateTime dt = new DateTime();
                IpreportUpload.PostedFile.SaveAs(Server.MapPath("~") + "\\ipreport.xlsx");
                var fileName = Server.MapPath("~") + "\\ipreport.xlsx";

                FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);

                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                var result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                {

                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }

                });

                DataTable datatable = new DataTable();

                datatable = result.Tables[0];

                for (int index = 1; index < datatable.Rows.Count; index++)
                {
                    Marque_Model_App_V2 marque_Model_App_V2 = new Marque_Model_App_V2();
                    byte[] tempBytes = null;
                    tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][0].ToString().ToUpper());
                    marque_Model_App_V2.Nom = System.Text.Encoding.UTF8.GetString(tempBytes);
                    tempBytes = null;
                    tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][1].ToString().ToUpper());
                    marque_Model_App_V2.Numero = System.Text.Encoding.UTF8.GetString(tempBytes);
                    tempBytes = null;
                    tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][2].ToString().ToUpper());
                    marque_Model_App_V2.Titulaire = System.Text.Encoding.UTF8.GetString(tempBytes);

                    bool bl = DateTime.TryParse(datatable.Rows[index][3].ToString().ToUpper(), out dt);
                    if (bl)
                    {
                        marque_Model_App_V2.Date_depot = dt.ToShortDateString();
                    }
                    else
                    {
                        marque_Model_App_V2.Date_depot = "";
                    }
                    tempBytes = null;
                    tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][4].ToString().ToUpper());
                    marque_Model_App_V2.Classe_nice = System.Text.Encoding.UTF8.GetString(tempBytes);
                    tempBytes = null;
                    tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(datatable.Rows[index][5].ToString().ToUpper());
                    marque_Model_App_V2.Etat_marque = System.Text.Encoding.UTF8.GetString(tempBytes);
                    list_marque_ipreport.Add(marque_Model_App_V2);
                }
                stream.Dispose();
                File.Delete(fileName);
            }
            else
            {
                IpreportUpload.PostedFile.SaveAs(Server.MapPath("~") + "\\doc.html");
                IWebDriver driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), new ChromeOptions(), TimeSpan.FromSeconds(120));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                driver.Navigate().GoToUrl(Server.MapPath("~") + "\\doc.html");

                var childrens = driver.FindElements(By.XPath("//*[@id='content']/table/tbody/tr"));
                byte[] tempBytes = null;
                for (int i = 2; i <= childrens.Count; i++)
                {

                    IWebElement ele = driver.FindElement(By.XPath($"//*[@id='content']/table/tbody/tr[{i}]"));
                    IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                    executor.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
                    Marque_Model_App_V2 marque_Model = new Marque_Model_App_V2();
                    marque_Model.Numero = driver.FindElement(By.XPath($"//*[@id='content']/table/tbody/tr[{i}]/td[2]")).Text;
                    marque_Model.Date_depot = driver.FindElement(By.XPath($"//*[@id='content']/table/tbody/tr[{i}]/td[3]")).Text;
                    marque_Model.Classe_nice = driver.FindElement(By.XPath($"//*[@id='content']/table/tbody/tr[{i}]/td[4]")).Text != null ? driver.FindElement(By.XPath($"//*[@id='content']/table/tbody/tr[{i}]/td[4]")).Text.Replace(" ", "") : "";
                    if (!string.IsNullOrWhiteSpace(driver.FindElement(By.XPath($"//*[@id='content']/table/tbody/tr[{i}]/td[5]")).Text))
                    {
                        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(driver.FindElement(By.XPath($"//*[@id='content']/table/tbody/tr[{i}]/td[5]")).Text.ToUpper());
                        marque_Model.Titulaire = System.Text.Encoding.UTF8.GetString(tempBytes);
                    }
                    else
                    {
                        marque_Model.Titulaire = "";
                    }
                    list_marque_ipreport.Add(marque_Model);
                    tempBytes = null;
                }


                try
                {
                    driver.Navigate().GoToUrl("http://www.directompic.ma/fr/renouv-marque");

                }
                catch (Exception)
                {
                    try
                    {
                        driver.Navigate().GoToUrl("http://www.directompic.ma/fr/renouv-marque");

                    }
                    catch (Exception)
                    {

                    }
                }
                for (int p = 0; p < list_marque_ipreport.Count; p++)
                {
                    tempBytes = null;
                    try
                    {

                        IWebElement Dropdown = driver.FindElement(By.XPath("//*[@id='search-form_loi']"));
                        IJavaScriptExecutor exec = (IJavaScriptExecutor)driver;
                        exec.ExecuteScript("arguments[0].scrollIntoView(true)", Dropdown);
                        SelectElement selectElement = new SelectElement(Dropdown);
                        selectElement.SelectByIndex(1);
                        IWebElement search_field1 = driver.FindElement(By.XPath("//*[@id='search-form_numeroTitre']"));
                        string temp_marq_anter7 = list_marque_ipreport[p].Numero;
                        search_field1.Clear();
                        search_field1.SendKeys(temp_marq_anter7);
                        IWebElement btn = driver.FindElement(By.XPath("//*[@id='search-form']/div[7]/button"));
                        btn.Click();
                        string nom = driver.FindElement(By.XPath("//*[@id='rowClasses']/tbody/tr[1]/td[4]/span")).Text;
                        try
                        {
                            if (nom != null)
                            {
                                tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(nom.ToUpper());
                                list_marque_ipreport[p].Nom = System.Text.Encoding.UTF8.GetString(tempBytes);
                            }
                        }
                        catch (Exception)
                        {

                        }

                    }
                    catch (Exception)
                    {
                        try
                        {
                            driver.Navigate().GoToUrl("http://www.directompic.ma/fr/renouv-marque");
                            IWebElement Dropdown = driver.FindElement(By.XPath("//*[@id='search-form_loi']"));
                            IJavaScriptExecutor exec = (IJavaScriptExecutor)driver;
                            exec.ExecuteScript("arguments[0].scrollIntoView(true)", Dropdown);
                            SelectElement selectElement = new SelectElement(Dropdown);
                            selectElement.SelectByIndex(1);
                            IWebElement search_field1 = driver.FindElement(By.XPath("//*[@id='search-form_numeroTitre']"));
                            string temp_marq_anter7 = list_marque_ipreport[p].Numero;
                            search_field1.Clear();
                            search_field1.SendKeys(temp_marq_anter7);
                            IWebElement btn = driver.FindElement(By.XPath("//*[@id='search-form']/div[7]/button"));
                            btn.Click();
                            string nom = driver.FindElement(By.XPath("//*[@id='rowClasses']/tbody/tr[1]/td[4]/span")).Text;
                            try
                            {
                                if (nom != null)
                                {
                                    tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(nom.ToUpper());
                                    list_marque_ipreport[p].Nom = System.Text.Encoding.UTF8.GetString(tempBytes);
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }

                driver.Quit();
                driver.Dispose();
                File.Delete(Server.MapPath("~") + "\\doc.html");
            }

            if (list_marque_ipreport.Count != 0)
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString);
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                List<string> list = new List<string>();
                WebClient webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");


                foreach (Marque_Model_App_V2 marq in list_marque_ipreport)
                {
                    DataTable suspects = new DataTable();

                    command.CommandText = "";

                    command.Parameters.Clear();

                    string nommarque = marq.Nom.Trim().Replace("e", "ù").Replace("E", "ù").Replace("i", "ù").Replace("I", "ù").Replace("a", "ù").Replace("A", "ù").Replace("o", "ù").Replace("O", "ù").Replace("u", "ù").Replace("U", "ù").Replace("y", "ù").Replace("Y", "ù")/*.Replace("ù", "[aeiuoy]")*/;
                    string nommarv = null;



                    //if (nommarque[nommarque.Length - 1] == 'ù' && nommarque.Length != 1)
                    //{

                    //    nommarv = nommarque/*.Remove(nommarque.Length - 1)*/;
                    //    nommarv = nommarv.Replace("ù", "[aeiuoy]").Replace(' ', '%');
                    //    list.Add("NOM_MARQUE like @Nommarque or NOM_MARQUE like @Nommarquedouble or NOM_MARQUE like @Nommarquedoublz");
                    //    //if (nommarque[0] == 'L' || nommarque[0] == 'l')
                    //    //{
                    //    //    command.Parameters.AddWithValue("@Nommarquedoublz", "%" + nommarv.Remove(0, 1) + "%");
                    //    //}
                    //    //else
                    //    //{
                    //    command.Parameters.AddWithValue("@Nommarquedoublz", "%" + nommarv + "%");
                    //    //}


                    //}
                    //else
                    //{
                    list.Add("NOM_MARQUE like @Nommarque or NOM_MARQUE like @Nommarquedouble ");
                    //}
                    if (!string.IsNullOrWhiteSpace(marq.Nom))
                    {
                        nommarque = marq.Nom.Trim().Trim().Replace("e", "ù").Replace("E", "ù").Replace("i", "ù").Replace("I", "ù").Replace("a", "ù").Replace("A", "ù").Replace("o", "ù").Replace("O", "ù").Replace("u", "ù").Replace("U", "ù").Replace("y", "ù").Replace("Y", "ù").Replace("ù", "[aeiuoy]").Replace(' ', '%');
                        string nommarquedouble = marq.Nom.Trim().Replace("e", "ù").Replace("E", "ù").Replace("i", "ù").Replace("I", "ù").Replace("a", "ù").Replace("A", "ù").Replace("o", "ù").Replace("O", "ù").Replace("u", "ù").Replace("U", "ù").Replace("y", "ù").Replace("Y", "ù").Replace("ù", "[aeiuoy][aeiuoy]").Replace(' ', '%');

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
                        if (list.Count > 0)
                        {

                            var Ompicquery = "select * from MarqueIP where ";
                            for (int i = 0; i < list.Count; i++)
                            {
                                if ((list.Count - 1) == i)
                                {

                                    Ompicquery += " " + list[i] + " ";
                                }
                                else
                                {
                                    Ompicquery += " " + list[i] + " and ";
                                }
                            }
                            command.CommandText = Ompicquery;
                            con.Close();
                            con.Open();

                            var reader = command.ExecuteReader();
                            suspects.Load(reader);
                            con.Close();
                            if (suspects.Rows.Count != 0)
                            {


                                foreach (DataRow rowx in suspects.Rows)
                                {

                                    string date_depot1 = "";
                                    string date_depot2 = "";
                                    string titulaire1 = "";
                                    string titulaire2 = "";
                                    string etat_marque = "";
                                    bool etat = false;
                                    if (!string.IsNullOrWhiteSpace(rowx[4].ToString()) && !string.IsNullOrWhiteSpace(marq.Date_depot) && DateTime.Compare(DateTime.Parse(rowx[4].ToString()), DateTime.Parse(marq.Date_depot)) > 0)
                                    {
                                        date_depot1 = "<strong>Date dépot : </strong>" + $"<span class='orange'>{marq.Date_depot}</span>";
                                        date_depot2 = "<strong>Date dépot : </strong>" + $"<span class='orange'>{rowx[4].ToString()}</span>";
                                    }
                                    else
                                    {
                                        date_depot1 = "<strong>Date dépot : </strong>" + marq.Date_depot;
                                        date_depot2 = "<strong>Date dépot : </strong>" + rowx[4].ToString();
                                    }
                                    if (rowx[1].ToString() == marq.Titulaire)
                                    {
                                        titulaire1 = "<strong>Titulaire : </strong>" + $"<span class='blue'>{marq.Titulaire}</span>";
                                        titulaire2 = "<strong>Titulaire : </strong>" + $"<span class='blue'>{rowx[1].ToString()}</span>";
                                    }
                                    else
                                    {
                                        titulaire1 = "<strong>Titulaire : </strong>" + marq.Titulaire;
                                        titulaire2 = "<strong>Titulaire : </strong>" + rowx[1].ToString();
                                    }
                                    etat = rowx[7].ToString().ToLower().Contains("motifs") || rowx[7].ToString().ToLower().Contains("absolus") ||
                                    rowx[7].ToString().ToLower().Contains("opposition") || rowx[7].ToString().ToLower().Contains("retirée") ||
                                    rowx[7].ToString().ToLower().Contains("rejetée") ? true : false;
                                    if (etat)
                                    {
                                        etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='purple'>{rowx[7].ToString()}</span>";
                                    }

                                    if (rowx[7].ToString().ToLower().Contains("rejetée"))
                                    {
                                        etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='red'>{rowx[7].ToString()}</span>";
                                    }
                                    else if (rowx[7].ToString().ToLower().Contains("en examen des motifs absolus"))
                                    {
                                        etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='orange'>{rowx[7].ToString()}</span>";
                                    }
                                    else if (rowx[7].ToString().ToLower().Contains("enregistrée"))
                                    {
                                        etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='green'>{rowx[7].ToString()}</span>";
                                    }
                                    else if (rowx[7].ToString().ToLower().Contains("publiée"))
                                    {
                                        etat_marque = "<strong>Etat de marque : </strong>" + $"<span class='blue'>{rowx[7].ToString()}</span>";
                                    }
                                    else
                                    {
                                        etat_marque = "<strong>Etat de marque : </strong>" + rowx[7].ToString();
                                    }
                                    Model model = new Model();
                                    model.crystalIpReport = marq.Nom + "|" + marq.Numero + "|" + marq.Date_depot + "|" + marq.Titulaire + "|" + marq.Classe_nice;

                                    model.crystalMarqueSimilaire= rowx[2].ToString()+"|"+ rowx[3].ToString() + "|" + rowx[4].ToString() + "|" + rowx[1].ToString()+"|"+ rowx[6].ToString();
                                   
                                    model.DetailsMarqueIpReport = "<strong>Nom Marque : </strong>" + marq.Nom + "<br/>" + "<strong>Numero Marque : </strong>" + marq.Numero + "<br/>" + titulaire1 + "<br/>" + date_depot1 + "<br/>" + "<strong>Classe nice : </strong>" + marq.Classe_nice;
                                    model.DetailsMarqueSimilaire = "<strong>Nom Marque : </strong>" + rowx[2].ToString() + "<br/>" + "<strong>Numero Marque : </strong>" + rowx[3].ToString() + "<br/>" + titulaire2 + "<br/>" + date_depot2 + "<br/>" + "<strong>Date éxpiration : </strong>" + rowx[5].ToString() + "<br/>" + etat_marque + "<br/>" + "<strong>Classe nice : </strong>" + rowx[6].ToString();
                                    string marquesimilaireimg = "";
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marq.Numero}.jpg"))
                                    {
                                        marq.Image = $@"{marq.Numero}.jpg";
                                    }
                                    else
                                    {
                                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marq.Numero}.JPG"))
                                        {
                                            marq.Image = $@"{marq.Numero}.JPG";
                                        }
                                        else
                                        {
                                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marq.Numero}.jpeg"))
                                            {
                                                marq.Image = $@"{marq.Numero}.jpeg";
                                            }
                                            else
                                            {
                                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{marq.Numero}.png"))
                                                {
                                                    marq.Image = $@"{marq.Numero}.png";
                                                }
                                                else
                                                {
                                                    try
                                                    {
                                                        webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{marq.Numero}.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{marq.Numero}.jpg");
                                                        marq.Image = $@"{marq.Numero}.jpg";
                                                    }
                                                    catch (Exception exd)
                                                    {
                                                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\temp_{marq.Numero}.jpg"))
                                                        {
                                                            marq.Image = $@"temp_{marq.Numero}.jpg";
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
                                                            g.DrawString("**", new System.Drawing.Font("Tahoma", 60), Brushes.Red, rectf, format2);
                                                            g.DrawString(marq.Nom, new System.Drawing.Font("Tahoma", 60), Brushes.Black, rectf, format);
                                                            g.Flush();

                                                            MemoryStream ms = new MemoryStream();

                                                            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                                            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                                                            img.Save(Server.MapPath("~") + $@"\Assets\Brand_image\temp_" + marq.Numero + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                                            marq.Image = $@"temp_{marq.Numero}.jpg";
                                                        }

                                                    }









                                                }
                                            }
                                        }
                                    }
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{ rowx[3].ToString().Trim() }.jpg"))
                                    {
                                        marquesimilaireimg = $@"{ rowx[3].ToString().Trim() }.jpg";
                                    }
                                    else
                                    {
                                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{ rowx[3].ToString().Trim() }.JPG"))
                                        {
                                            marquesimilaireimg = $@"{ rowx[3].ToString().Trim() }.JPG";
                                        }
                                        else
                                        {
                                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{ rowx[3].ToString().Trim() }.jpeg"))
                                            {
                                                marquesimilaireimg = $@"{ rowx[3].ToString().Trim() }.jpeg";
                                            }
                                            else
                                            {
                                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{ rowx[3].ToString().Trim() }.png"))
                                                {
                                                    marquesimilaireimg = $@"{ rowx[3].ToString().Trim() }.png";
                                                }
                                                else
                                                {
                                                    try
                                                    {
                                                        webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{ rowx[3].ToString().Trim() }.jpg", Server.MapPath("~") + $@"\Assets\Brand_image\{ rowx[3].ToString().Trim() }.jpg");
                                                        marquesimilaireimg = $@"{ rowx[3].ToString().Trim() }.jpg";
                                                    }
                                                    catch (Exception exd)
                                                    {
                                                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\temp_{ rowx[3].ToString().Trim() }.jpg"))
                                                        {
                                                            marquesimilaireimg = $@"temp_{ rowx[3].ToString().Trim() }.jpg";
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
                                                            g.DrawString("**", new System.Drawing.Font("Tahoma", 60), Brushes.Red, rectf, format2);
                                                            g.DrawString(marq.Nom, new System.Drawing.Font("Tahoma", 60), Brushes.Black, rectf, format);
                                                            g.Flush();

                                                            MemoryStream ms = new MemoryStream();

                                                            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                                            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                                                            img.Save(Server.MapPath("~") + $@"\Assets\Brand_image\temp_" + rowx[3].ToString().Trim() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                                            marquesimilaireimg = $@"temp_{ rowx[3].ToString().Trim() }.jpg";
                                                        }

                                                    }









                                                }
                                            }
                                        }
                                    }
                                    model.ImageMarqueIpReport = marq.Image;
                                    model.ImageMarqueSimilaire = marquesimilaireimg;
                                    Models.Add(model);

                                }
                            }

                        }

                    }
                }

                if (Models.Count != 0)
                {
                    Session["marques ip report"] = list_marque_ipreport;
                    GridView1.DataSource = Models;
                    Session["Modelsmax"] = Models;
                    GridView1.Columns[7].Visible = false;
                    GridView1.Columns[8].Visible = false;

                    GridView1.DataBind();

                }
            }



        }
    }
}