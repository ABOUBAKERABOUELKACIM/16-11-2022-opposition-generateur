using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Opposition_Generateur.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Opposition_Generateur
{
    public partial class Resultat : System.Web.UI.Page
    {
        public string indiceform;
        public List<FormulaireOpposition> Empty_list = new List<FormulaireOpposition>();
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

                    if (Session["List formulaire Opposition"] != null)
                    {
                        GridView1.DataSource = Session["List formulaire Opposition"] as List<FormulaireOpposition>;
                        GridView1.DataBind();
                    }
                    else
                    {
                        GridView1.DataSource = Empty_list;
                        GridView1.DataBind();
                    }

                    string[] files = Directory.GetFiles(Server.MapPath("~") + "\\WordDocs");
                    //foreach (string file in files)
                    //{
                    //    if (!file.Contains("M7.docx"))
                    //    {
                    //        File.Delete(file);
                    //    }
                    //}

                }
                else
                {
                    Response.Redirect("Authentification.aspx");
                }

            }
        }

        protected void ShowArgs(object sender, EventArgs e)
        {

            List<FormulaireOpposition> List_formulaire_Opposition = Session["List formulaire Opposition"] as List<FormulaireOpposition>;
            Button ShowArgs = sender as Button;
            GridViewRow gridViewRow = ShowArgs.NamingContainer as GridViewRow;
            ViewState["gridrowIndex"] = gridViewRow.RowIndex.ToString();
            Arguments.Attributes.Add("style", "top: 0px;");
            indiceform = GridView1.Rows[gridViewRow.RowIndex].Cells[0].Text;
            Session["indiceform"] = indiceform;
            Response.Write(indiceform.ToString());
            if (List_formulaire_Opposition[gridViewRow.RowIndex].Nature_marque_anterieure == "nationale")
            {
                img_ant.Src = $"~/Assets/Brand_image/{List_formulaire_Opposition[gridViewRow.RowIndex].Image_marque_anterieure}";
                ant_deposant.InnerText = List_formulaire_Opposition[gridViewRow.RowIndex].Deposant_marque_anterieure;
                ant_date_depot.InnerText = List_formulaire_Opposition[gridViewRow.RowIndex].marque_anterieur_Date_depot;
                ant_date_exp.InnerText = List_formulaire_Opposition[gridViewRow.RowIndex].Date_Exp_marque_anterieure;
            }
            else
            {
                img_ant.Src = $"~/Assets/Brand_image/{List_formulaire_Opposition[gridViewRow.RowIndex].Image_marque_anterieure}";
                ant_deposant.InnerText = List_formulaire_Opposition[gridViewRow.RowIndex].Deposant_marque_anterieure;
                ant_date_depot.InnerText = List_formulaire_Opposition[gridViewRow.RowIndex].marque_anterieur_Date_depot;
                ant_date_exp.InnerText = List_formulaire_Opposition[gridViewRow.RowIndex].Date_Exp_marque_anterieure;
            }
            if (List_formulaire_Opposition[gridViewRow.RowIndex].Nature_marque_contester == "nationale")
            {
                img_cont.Src = $"~/Assets/Brand_image/{List_formulaire_Opposition[gridViewRow.RowIndex].Image_marque_contester}";
                cont_deposant.InnerText = List_formulaire_Opposition[gridViewRow.RowIndex].Deposant_marque_contester;
                cont_date_depot.InnerText = List_formulaire_Opposition[gridViewRow.RowIndex].marque_contester_Date_depot;
                cont_date_exp.InnerText = List_formulaire_Opposition[gridViewRow.RowIndex].Date_Exp_marque_contester;
            }
            else
            {
                img_cont.Src = $"~/Assets/Brand_image/{List_formulaire_Opposition[gridViewRow.RowIndex].Image_marque_contester}";
                cont_deposant.InnerText = List_formulaire_Opposition[gridViewRow.RowIndex].Deposant_marque_contester;
                cont_date_depot.InnerText = List_formulaire_Opposition[gridViewRow.RowIndex].marque_contester_Date_depot;
                cont_date_exp.InnerText = List_formulaire_Opposition[gridViewRow.RowIndex].Date_Exp_marque_contester;
            }
            int i = 1;
            foreach (var kvp in List_formulaire_Opposition[gridViewRow.RowIndex].Classe_nice_anterieure_kvp)
            {
                System.Web.UI.WebControls.ListItem listItem = new System.Web.UI.WebControls.ListItem(kvp.Key, kvp.Value);
                AntDropDownList.Items.Add(listItem);
                if (i == 1)
                {
                    Ant_classe_text.InnerText = kvp.Value;
                }
                i++;
            }
            i = 1;
            foreach (var kvp in List_formulaire_Opposition[gridViewRow.RowIndex].Classe_nice_contester_kvp)
            {
                System.Web.UI.WebControls.ListItem listItem = new System.Web.UI.WebControls.ListItem(kvp.Key, kvp.Value);
                ContDropDownList.Items.Add(listItem);
                if (i == 1)
                {
                    Cont_classe_text.InnerText = kvp.Value;
                }
                i++;
            }

        }
        protected void Download_Word_Click(object sender, EventArgs e)
        {
           

            string[] files = Directory.GetFiles(Server.MapPath("~") + "\\WordDocs");
            //foreach (string file in files)
            //{
            //    if (!files.Contains("M7.docx"))
            //    {
            //        File.Delete(file);
            //    }
            //}

            List<FormulaireOpposition> List_formulaire_Opposition = Session["List formulaire Opposition"] as List<FormulaireOpposition>;

            File.Copy(Server.MapPath("~") + "\\WordDocs\\M7.docx", Server.MapPath("~") + $"\\WordDocs\\{List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_anterieure}-{List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester}.docx", true);
            using (var doc = WordprocessingDocument.Open(Server.MapPath("~") + $"\\WordDocs\\{List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_anterieure}-{List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester}.docx", true))
            {
                var FFDList = doc.MainDocumentPart.Document.Body.Descendants<FormFieldData>();
                Response.Write(FFDList);
                foreach (var FFD in FFDList)
                {
                    FormFieldName FFN = FFD.Descendants<FormFieldName>().FirstOrDefault();


                    if (FFN.Val.Value == "Sec1_Denom_sociale")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_denomination_sociale);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_denomination_sociale;
                        }

                    }
                    else if (FFN.Val.Value == "Sec1_Ice")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_Ice);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_Ice;
                        }
                    }
                    else if (FFN.Val.Value == "Sec1_Rc")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_Rc);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_Rc;
                        }
                    }
                    else if (FFN.Val.Value == "Sec1_Tribunal")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_tribunal);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_tribunal;
                        }
                    }
                    else if (FFN.Val.Value == "Sec1_Adresse")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_adresse);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_adresse;
                        }
                    }
                    else if (FFN.Val.Value == "Sec1_Ville")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_tribunal);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_tribunal;
                        }
                    }
                    else if (FFN.Val.Value == "Sec4_Case1")
                    {
                        if (List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nature_droit_anterieure.Contains("case-1"))
                        {
                            DocumentFormat.OpenXml.Wordprocessing.CheckBox Cb = FFD.Descendants<DocumentFormat.OpenXml.Wordprocessing.CheckBox>().FirstOrDefault();
                            if (Cb != null)
                            {
                                DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState DCFFS = Cb.Descendants<DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState>().FirstOrDefault();
                                if (DCFFS != null)
                                {
                                    DCFFS.Val.Value = true;
                                }
                            }
                        }
                    }
                    else if (FFN.Val.Value == "Sec4_Case2")
                    {
                        if (List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nature_droit_anterieure.Contains("case-2"))
                        {
                            DocumentFormat.OpenXml.Wordprocessing.CheckBox Cb = FFD.Descendants<DocumentFormat.OpenXml.Wordprocessing.CheckBox>().FirstOrDefault();
                            if (Cb != null)
                            {
                                DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState DCFFS = Cb.Descendants<DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState>().FirstOrDefault();
                                if (DCFFS != null)
                                {
                                    DCFFS.Val.Value = true;
                                }
                            }
                        }
                    }
                    else if (FFN.Val.Value == "Sec4_Case3")
                    {
                        if (List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nature_droit_anterieure.Contains("case-3"))
                        {
                            DocumentFormat.OpenXml.Wordprocessing.CheckBox Cb = FFD.Descendants<DocumentFormat.OpenXml.Wordprocessing.CheckBox>().FirstOrDefault();
                            if (Cb != null)
                            {
                                DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState DCFFS = Cb.Descendants<DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState>().FirstOrDefault();
                                if (DCFFS != null)
                                {
                                    DCFFS.Val.Value = true;
                                }
                            }
                        }
                    }
                    else if (FFN.Val.Value == "Sec4_Case4")
                    {
                        if (List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nature_droit_anterieure.Contains("case-4"))
                        {
                            DocumentFormat.OpenXml.Wordprocessing.CheckBox Cb = FFD.Descendants<DocumentFormat.OpenXml.Wordprocessing.CheckBox>().FirstOrDefault();
                            if (Cb != null)
                            {
                                DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState DCFFS = Cb.Descendants<DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState>().FirstOrDefault();
                                if (DCFFS != null)
                                {
                                    DCFFS.Val.Value = true;
                                }
                            }
                        }
                    }
                    else if (FFN.Val.Value == "Sec4_Case5")
                    {
                        if (List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nature_droit_anterieure.Contains("case-5"))
                        {
                            DocumentFormat.OpenXml.Wordprocessing.CheckBox Cb = FFD.Descendants<DocumentFormat.OpenXml.Wordprocessing.CheckBox>().FirstOrDefault();
                            if (Cb != null)
                            {
                                DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState DCFFS = Cb.Descendants<DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState>().FirstOrDefault();
                                if (DCFFS != null)
                                {
                                    DCFFS.Val.Value = true;
                                }
                            }
                        }
                    }
                    else if (FFN.Val.Value == "Sec4_Case6")
                    {
                        if (List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nature_droit_anterieure.Contains("case-6"))
                        {
                            DocumentFormat.OpenXml.Wordprocessing.CheckBox Cb = FFD.Descendants<DocumentFormat.OpenXml.Wordprocessing.CheckBox>().FirstOrDefault();
                            if (Cb != null)
                            {
                                DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState DCFFS = Cb.Descendants<DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState>().FirstOrDefault();
                                if (DCFFS != null)
                                {
                                    DCFFS.Val.Value = true;
                                }
                            }
                        }
                    }
                    else if (FFN.Val.Value == "Sec5_N_depot")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_anterieure);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_anterieure;
                        }
                    }
                    else if (FFN.Val.Value == "Sec5_marq_nationale")
                    {
                        if (List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nature_marque_anterieure == "nationale")
                        {
                            DocumentFormat.OpenXml.Wordprocessing.CheckBox Cb = FFD.Descendants<DocumentFormat.OpenXml.Wordprocessing.CheckBox>().FirstOrDefault();
                            if (Cb != null)
                            {
                                DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState DCFFS = Cb.Descendants<DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState>().FirstOrDefault();
                                if (DCFFS != null)
                                {
                                    DCFFS.Val.Value = true;
                                }
                            }
                        }
                    }
                    else if (FFN.Val.Value == "Sec5_marq_internatio")
                    {
                        if (List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nature_marque_anterieure == "internationale")
                        {
                            DocumentFormat.OpenXml.Wordprocessing.CheckBox Cb = FFD.Descendants<DocumentFormat.OpenXml.Wordprocessing.CheckBox>().FirstOrDefault();
                            if (Cb != null)
                            {
                                DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState DCFFS = Cb.Descendants<DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState>().FirstOrDefault();
                                if (DCFFS != null)
                                {
                                    DCFFS.Val.Value = true;
                                }
                            }
                        }
                    }
                    else if (FFN.Val.Value == "Sec5_Date_depot")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_Date_depot);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_Date_depot;
                        }
                    }
                    else if (FFN.Val.Value == "Sec6_N_depot")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester;
                        }
                    }
                    else if (FFN.Val.Value == "Sec6_marq_nationale")
                    {
                        if (List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nature_marque_contester == "nationale")
                        {
                            DocumentFormat.OpenXml.Wordprocessing.CheckBox Cb = FFD.Descendants<DocumentFormat.OpenXml.Wordprocessing.CheckBox>().FirstOrDefault();
                            if (Cb != null)
                            {
                                DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState DCFFS = Cb.Descendants<DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState>().FirstOrDefault();
                                if (DCFFS != null)
                                {
                                    DCFFS.Val.Value = true;
                                }
                            }
                        }
                    }
                    else if (FFN.Val.Value == "Sec6_marq_internatio")
                    {
                        if (List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nature_marque_contester == "internationale")
                        {
                            DocumentFormat.OpenXml.Wordprocessing.CheckBox Cb = FFD.Descendants<DocumentFormat.OpenXml.Wordprocessing.CheckBox>().FirstOrDefault();
                            if (Cb != null)
                            {
                                DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState DCFFS = Cb.Descendants<DocumentFormat.OpenXml.Wordprocessing.DefaultCheckBoxFormFieldState>().FirstOrDefault();
                                if (DCFFS != null)
                                {
                                    DCFFS.Val.Value = true;
                                }
                            }
                        }
                    }
                    else if (FFN.Val.Value == "Sec6_Date_depot")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_contester_Date_depot);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_contester_Date_depot;
                        }
                    }
                    else if (FFN.Val.Value == "Sec6_Num_pub")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_contester_num_publication);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_contester_num_publication;
                        }
                    }
                    else if (FFN.Val.Value == "ClasseNiceMC1")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            string txt = "";
                            foreach (KeyValuePair<string, string> kvp in List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Classe_nice_contester_kvp)
                            {
                                txt += kvp.Key + ";";
                            }
                            Text texT = new Text(txt);
                            xmlElement.AppendChild(texT);
                        }
                        else
                        {
                            string txt = "";
                            foreach (KeyValuePair<string, string> kvp in List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Classe_nice_contester_kvp)
                            {
                                txt += kvp.Key + ";";
                            }
                            text.Text = txt;
                        }
                    }
                    else if (FFN.Val.Value == "ClasseNiceMC2")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            string txt = "";
                            foreach (KeyValuePair<string, string> kvp in List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Classe_nice_contester_kvp)
                            {
                                txt += kvp.Key + ";";
                            }
                            Text texT = new Text(txt);
                            xmlElement.AppendChild(texT);
                        }
                        else
                        {
                            string txt = "";
                            foreach (KeyValuePair<string, string> kvp in List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Classe_nice_contester_kvp)
                            {
                                txt += kvp.Key + ";";
                            }
                            text.Text = txt;
                        }
                    }
                    else if (FFN.Val.Value == "ClasseNiceMC3")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            string txt = "";
                            foreach (KeyValuePair<string, string> kvp in List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Classe_nice_contester_kvp)
                            {
                                txt += kvp.Key + ";";
                            }
                            Text texT = new Text(txt);
                            xmlElement.AppendChild(texT);
                        }
                        else
                        {
                            string txt = "";
                            foreach (KeyValuePair<string, string> kvp in List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Classe_nice_contester_kvp)
                            {
                                txt += kvp.Key + ";";
                            }
                            text.Text = txt;
                        }
                    }
                    else if (FFN.Val.Value == "NomMC1")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nom_marque_contester);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nom_marque_contester;
                        }
                    }
                    else if (FFN.Val.Value == "NomMC2")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nom_marque_contester);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nom_marque_contester;
                        }
                    }
                    else if (FFN.Val.Value == "NomMC3")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nom_marque_contester);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nom_marque_contester;
                        }
                    }
                    else if (FFN.Val.Value == "NumMC1")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester;
                        }
                    }
                    else if (FFN.Val.Value == "NumMC2")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester;
                        }
                    }
                    else if (FFN.Val.Value == "NumMC3")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester;
                        }
                    }
                    else if (FFN.Val.Value == "NumMA1")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_anterieure);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_anterieure;
                        }
                    }
                    else if (FFN.Val.Value == "NumMA2")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_anterieure);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_anterieure;
                        }
                    }
                    else if (FFN.Val.Value == "DateMA")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_Date_depot);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_Date_depot;
                        }
                    }
                    else if (FFN.Val.Value == "NomMA")
                    {
                        Run run = FFD.Parent.Parent as Run;
                        IEnumerable<OpenXmlElement> openXmlElements = run.ElementsAfter();
                        OpenXmlElement xmlElement = openXmlElements.ElementAt(3);
                        Text text = xmlElement.Descendants<Text>().FirstOrDefault();
                        if (text == null)
                        {
                            Text txt = new Text(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nom_marque_anterieure);
                            xmlElement.AppendChild(txt);
                        }
                        else
                        {
                            text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nom_marque_anterieure;
                        }
                    }
                }
                var Runs = doc.MainDocumentPart.Document.Descendants<Run>();
                foreach (var run in Runs)
                {
                    var text = run.Descendants<Text>().FirstOrDefault();
                    if (text != null && text.Text == "PSC")
                    {
                        text.Text = "";
                        foreach (var kvp in List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Classe_nice_contester_kvp)
                        {
                            Text key = new Text();
                            key.Text = kvp.Key + " :";
                            Text value = new Text();
                            value.Text = kvp.Value;
                            run.AppendChild<Text>(key);
                            run.AppendChild(new Break());
                            run.AppendChild<Text>(value);
                            run.AppendChild(new Break());
                        }

                    }
                    else if (text != null && text.Text == "PSA")
                    {
                        text.Text = "";
                        foreach (var kvp in List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Classe_nice_anterieure_kvp)
                        {
                            Text key = new Text();
                            key.Text = kvp.Key + " :";
                            Text value = new Text();
                            value.Text = kvp.Value;
                            run.AppendChild<Text>(key);
                            run.AppendChild(new Break());
                            run.AppendChild<Text>(value);
                            run.AppendChild(new Break());
                        }

                    }
                    else if (text != null && text.Text == "MarqueTypeCont")
                    {
                        text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nature_marque_contester;
                    }
                    else if (text != null && text.Text == "MarqueTypeAnt")
                    {
                        text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nature_marque_anterieure;
                    }
                    else if (text != null && text.Text == "NomMarqAnt")
                    {
                        text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nom_marque_anterieure;
                    }
                    else if (text != null && text.Text == "NumMarqAnt")
                    {
                        text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_anterieure;
                    }
                    else if (text != null && text.Text == "DatedepotMarqAnt")
                    {
                        text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_anterieur_Date_depot;
                    }
                    else if (text != null && text.Text == "NumMarqCont")
                    {
                        text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester;
                    }
                    else if (text != null && text.Text == "DatedepotMarqCont")
                    {
                        text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].marque_contester_Date_depot;
                    }
                    else if (text != null && text.Text == "NomMarqCont")
                    {
                        text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nom_marque_contester;
                    }
                    else if (text != null && text.Text == "NomDeposantMarqAnt")
                    {
                        text.Text = List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Deposant_marque_anterieure;
                    }
                    else if (text != null && text.Text == "Domaine")
                    {
                        text.Text = domaine.InnerText;
                    }
                    else if (text != null && text.Text == "PSidentique")
                    {
                        text.Text = PS_identique.InnerText;
                    }
                    else if (text != null && text.Text == "PSidentiqueMarqAnt")
                    {
                        text.Text = Ps_identique_Marque_Ant.InnerText;
                    }
                    else if (text != null && text.Text == "PS_ant_inclus_cont")
                    {
                        text.Text = PS_ant_inclus_cont.InnerText;
                    }
                    else if (text != null && text.Text == "PS_cont_inclus_ant")
                    {
                        text.Text = PS_cont_inclus_ant.InnerText;
                    }
                    else if (text != null && text.Text == "PS_ant_comp_cont")
                    {
                        text.Text = PS_ant_comp_cont.InnerText;
                    }
                    else if (text != null && text.Text == "PS_cont_comp_ant")
                    {
                        text.Text = PS_cont_comp_ant.InnerText;
                    }
                    else if (text != null && text.Text == "Num_classe_comp_cont")
                    {
                        text.Text = Num_classe_comp_cont.InnerText;
                    }
                    else if (text != null && text.Text == "Num_classe_comp_ant")
                    {
                        text.Text = Num_classe_comp_ant.InnerText;
                    }
                    else if (text != null && text.Text == "Critere_similarite")
                    {
                        text.Text = Critere_similarite.InnerText;
                    }
                    else if (text != null && text.Text == "arg_visuelle")
                    {
                        text.Text = arg_visuelle.InnerText;
                    }
                    else if (text != null && text.Text == "arg_phonetique")
                    {
                        text.Text = arg_phonetique.InnerText;
                    }
                    else if (text != null && text.Text == "arg_conceptuelle")
                    {
                        text.Text = arg_conceptuelle.InnerText;
                    }
                    else if (text != null && text.Text == "appre_gen")
                    {
                        text.Text = appre_gen.InnerText;
                    }
                    else if (text != null && text.Text == "ClasseMarqAnt")
                    {
                        string nices = "";
                        foreach (var kvp in List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Classe_nice_anterieure_kvp)
                        {
                            nices += kvp.Key + ";";
                        }
                        text.Text = nices;
                    }
                    else if (text != null && text.Text == "ClasseMarqCont")
                    {
                        string nices = "";
                        foreach (var kvp in List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Classe_nice_contester_kvp)
                        {
                            nices += kvp.Key + ";";
                        }
                        text.Text = nices;
                    }
                    else { }
                }


                try
                {

                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Image_marque_anterieure}"))
                    {
                        OpenXmlPart imagePart = doc.MainDocumentPart.GetPartById("rId9");
                        byte[] bytes = File.ReadAllBytes(Server.MapPath("~") + $@"\Assets\Brand_image\{List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Image_marque_anterieure}");
                        MemoryStream ms = new MemoryStream(bytes);
                        imagePart.FeedData(ms);
                    }
                    else
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
                        g.DrawString(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nom_marque_anterieure, new System.Drawing.Font("Tahoma", 40), Brushes.Black, rectf, format);
                        g.Flush();
                        MemoryStream ms = new MemoryStream();
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        OpenXmlPart imagePart = doc.MainDocumentPart.GetPartById("rId9");
                        imagePart.FeedData(ms);

                    }

                }
                catch (Exception) { }
                try
                {

                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Image_marque_contester}"))
                    {
                        OpenXmlPart imagePart = doc.MainDocumentPart.GetPartById("rId10");
                        byte[] bytes = File.ReadAllBytes(Server.MapPath("~") + $@"\Assets\Brand_image\{List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Image_marque_contester}");
                        MemoryStream ms = new MemoryStream(bytes);
                        imagePart.FeedData(ms);
                    }
                    else
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
                        g.DrawString(List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nom_marque_contester, new System.Drawing.Font("Tahoma", 40), Brushes.Black, rectf, format);
                        g.Flush();
                        MemoryStream ms = new MemoryStream();
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        OpenXmlPart imagePart = doc.MainDocumentPart.GetPartById("rId10");
                        imagePart.FeedData(ms);
                    }

                }
                catch (Exception) { }

                doc.Save();
            }
            string cs = @"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=Ipp;Integrated Security=True";
            SqlConnection sQLiteConnection = new SqlConnection(cs);
            sQLiteConnection.Open();
            SqlCommand sQLiteCommand = new SqlCommand();
            sQLiteCommand.Connection = sQLiteConnection;
            sQLiteCommand.CommandText = "insert into Oppositions(N_depot_marque_anterieure,Nom_marque_anterieure,Deposant_marque_anterieure,Nature_marque_anterieure,N_depot_marque_contester,Nom_marque_contester,Deposant_marque_contester,Nature_marque_contester,Date_creation,Cas_identite,Cas_inclusion,Cas_complementarite,Comparaison_signe,Appreciation_generale_risque_confusion,User_id) values(@N_depot_marque_anterieure,@Nom_marque_anterieure,@Deposant_marque_anterieure,@Nature_marque_anterieure,@N_depot_marque_contester,@Nom_marque_contester,@Deposant_marque_contester,@Nature_marque_contester,@Date_creation,@Cas_identite,@Cas_inclusion,@Cas_complementarite,@Comparaison_signe,@Appreciation_generale_risque_confusion,@User_id)";
            sQLiteCommand.Parameters.AddWithValue("@N_depot_marque_anterieure", List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_anterieure);
            sQLiteCommand.Parameters.AddWithValue("@Nom_marque_anterieure", List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nom_marque_anterieure);
            sQLiteCommand.Parameters.AddWithValue("@Deposant_marque_anterieure", List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Deposant_marque_anterieure);
            sQLiteCommand.Parameters.AddWithValue("@Nature_marque_anterieure", List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nature_marque_anterieure);
            sQLiteCommand.Parameters.AddWithValue("@N_depot_marque_contester", List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester);
            sQLiteCommand.Parameters.AddWithValue("@Nom_marque_contester", List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nom_marque_contester);
            sQLiteCommand.Parameters.AddWithValue("@Deposant_marque_contester", List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Deposant_marque_contester);
            sQLiteCommand.Parameters.AddWithValue("@Nature_marque_contester", List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].Nature_marque_contester);
            sQLiteCommand.Parameters.AddWithValue("@Date_creation", DateTime.Now);
            sQLiteCommand.Parameters.AddWithValue("@Cas_identite", domaine.Value + Environment.NewLine + PS_identique.Value + Environment.NewLine + Ps_identique_Marque_Ant.Value);
            sQLiteCommand.Parameters.AddWithValue("@Cas_inclusion", PS_ant_inclus_cont.Value + Environment.NewLine + PS_cont_inclus_ant.Value);
            sQLiteCommand.Parameters.AddWithValue("@Cas_complementarite", PS_ant_comp_cont.Value + Environment.NewLine + PS_cont_comp_ant.Value + Environment.NewLine + Num_classe_comp_cont.Value + Environment.NewLine + Num_classe_comp_ant.Value + Environment.NewLine + Critere_similarite.Value);
            sQLiteCommand.Parameters.AddWithValue("@Comparaison_signe", arg_visuelle.Value + Environment.NewLine + arg_phonetique.Value + Environment.NewLine + arg_conceptuelle.Value);
            sQLiteCommand.Parameters.AddWithValue("@Appreciation_generale_risque_confusion", appre_gen.Value);
            sQLiteCommand.Parameters.AddWithValue("@User_id", int.Parse(Session["Account_id"].ToString()));
            //byte[] data = File.WriteAllBytes(Server.MapPath("~") + $"\\WordDocs\\{List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_anterieure}-{List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester}.docx");
            //sQLiteCommand.Parameters.AddWithValue("@data", data);
            sQLiteCommand.ExecuteNonQuery();

            //sQLiteCommand.ExecuteNonQuery();
            sQLiteConnection.Close();
            sQLiteConnection.Open();
            HttpCookie httpCookie = Request.Cookies["Userinfo"];
            sQLiteCommand.CommandText = "UPDATE FormulaireOppositiontb SET Status = 'closed', IduserT = '" + int.Parse(httpCookie["Iduser"].ToString()) + "'WHERE IdFO=" + int.Parse(Session["indiceform"].ToString()) + "";
            
            sQLiteCommand.ExecuteNonQuery();
            sQLiteConnection.Close();

            Response.AddHeader("Content-Disposition", $"attachment; filename={List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_anterieure}-{List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester}.docx");
            Response.ContentType = "application/msword";
            Response.TransmitFile(Server.MapPath("~") + $"\\WordDocs\\{List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_anterieure}-{List_formulaire_Opposition[int.Parse(ViewState["gridrowIndex"].ToString())].N_depot_marque_contester}.docx");
            Response.End();



        }






        protected void AntDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<FormulaireOpposition> List_formulaire_Opposition = Session["List formulaire Opposition"] as List<FormulaireOpposition>;
            Ant_classe_text.InnerText = List_formulaire_Opposition[int.Parse((string)ViewState["gridrowIndex"])].Classe_nice_anterieure_kvp[AntDropDownList.SelectedItem.Text];

        }

        protected void ContDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<FormulaireOpposition> List_formulaire_Opposition = Session["List formulaire Opposition"] as List<FormulaireOpposition>;
            Cont_classe_text.InnerText = List_formulaire_Opposition[int.Parse((string)ViewState["gridrowIndex"])].Classe_nice_contester_kvp[ContDropDownList.SelectedItem.Text];

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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}