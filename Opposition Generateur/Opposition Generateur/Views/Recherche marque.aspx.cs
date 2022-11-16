using Newtonsoft.Json.Linq;
using Opposition_Generateur.Crystal_report;
using Opposition_Generateur.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace Opposition_Generateur.Views
{
    public partial class Recherche_marque : System.Web.UI.Page
    {
        public List<Marque> Empty_list = new List<Marque>();
        public List<Marque> marques = new List<Marque>();
        
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
                        marques = Empty_list;
                        
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
                    Session.Remove("Marqueschecked");
                    Session.Remove("Marqueschecke");
                    GridView1.DataSource = Empty_list;
                    GridView1.DataBind();
                   
                }
                else
                {
                    Response.Redirect("Authentification.aspx");
                }
            }
        }

        protected void Search_btn_Click(object sender, EventArgs e)
        {
            try
            {
                var value = Request.Form["search_field"];
                WebClient webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");
                string json = webClient.DownloadString($"https://www.directinfo.ma/directinfo-backend/api/queryDsl/search/marque/{value}");
                var json_array = JArray.Parse(json);
                var list_marque = JArray.Parse(json_array[0].ToString());
                List<Marque> marques = new List<Marque>();
                foreach (var marq in list_marque)
                {
                    try
                    {
                        Marque marque = new Marque();
                        marque.Id = int.Parse(marq["id"].ToString().Trim());
                        marque.NumeroTitre = marq["numeroTitre"].Type == JTokenType.Null || marq["numeroTitre"].ToString().Trim() == "NULLE" ? "" : marq["numeroTitre"].ToString().Trim();
                        marque.NomMarque = marq["nomMarque"].Type == JTokenType.Null || marq["nomMarque"].ToString().Trim() == "NULLE" ? "" : marq["nomMarque"].ToString().Trim();
                        marque.Etat = marq["workflowState"].Type == JTokenType.Null || marq["workflowState"].ToString().Trim() == "NULLE" ? "" : marq["workflowState"].ToString().Trim();
                        marque.Titulaire = marq["titulaires"].Type == JTokenType.Null || marq["titulaires"].ToString().Trim() == "NULLE" ? "" : marq["titulaires"].ToString().Trim();


                        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0);
                        if (marq["dateDepot"].Type != JTokenType.Null && marq["dateDepot"].ToString().Trim() != "NULLE")
                        {
                            DateTime date = epoch.AddMilliseconds(double.Parse(marq["dateDepot"].ToString().Trim()));
                            marque.DateDepot = date.ToString().Split(' ')[1] == "23:00:00" ? DateTime.Parse(date.AddDays(1).ToShortDateString()) : DateTime.Parse(date.ToShortDateString());
                        }
                        else
                        {
                            marque.DateDepot = new DateTime(3077, 07, 07);
                        }
                        if (marq["dateExpiration"].Type != JTokenType.Null && marq["dateExpiration"].ToString().Trim() != "NULLE")
                        {
                            DateTime date = epoch.AddMilliseconds(double.Parse(marq["dateExpiration"].ToString().Trim()));
                            marque.DateExpiration = date.ToString().Split(' ')[1] == "23:00:00" ? DateTime.Parse(date.AddDays(1).ToShortDateString()) : DateTime.Parse(date.ToShortDateString());
                        }
                        else
                        {
                            marque.DateExpiration = new DateTime(3077, 07, 07);
                        }

                        try
                        {
                            if (!string.IsNullOrWhiteSpace(marque.NumeroTitre))
                            {
                                if (File.Exists(Server.MapPath("~") + $@"/Assets/Brand_image/{marque.NumeroTitre}.jpg"))
                                {
                                    marque.Image = $@"{marque.NumeroTitre}.jpg";
                                }
                                else
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"/Assets/Brand_image/{marque.NumeroTitre}.JPG"))
                                    {
                                        marque.Image = $@"{marque.NumeroTitre}.JPG";
                                    }
                                    else
                                    {
                                        if (File.Exists(Server.MapPath("~") + $@"/Assets/Brand_image/{marque.NumeroTitre}.jpeg"))
                                        {
                                            marque.Image = $@"{marque.NumeroTitre}.jpeg";
                                        }
                                        else
                                        {
                                            if (File.Exists(Server.MapPath("~") + $@"/Assets/Brand_image/{marque.NumeroTitre}.png"))
                                            {
                                                marque.Image = $@"{marque.NumeroTitre}.png";
                                            }
                                            else
                                            {
                                                webClient.DownloadFile($"http://online.ompic.org.ma/ompic_online/img_marque/{marque.NumeroTitre}.jpg", Server.MapPath("~") + $@"/Assets/Brand_image/{marque.NumeroTitre}.jpg");
                                                marque.Image = $@"{marque.NumeroTitre}.jpg";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception) { }
                        marques.Add(marque);
                    }
                    catch (Exception) { }
                }

                Session["Marques"] = marques;
                GridView1.DataSource = marques.Take(8);
                GridView1.DataBind();
                ViewState["index"] = 0;
                double result = marques.Count / 8.0;
                int pages = int.Parse(Math.Ceiling(result).ToString());
                index.Text = 1 + " / " + (pages == 0 ? 1 : pages);
                ViewState["Id_Sort_dir"] = "ASC";
                ViewState["NomMarque_Sort_dir"] = "ASC";
                ViewState["Etat_Sort_dir"] = "ASC";
                ViewState["Titulaire_Sort_dir"] = "ASC";
                ViewState["DateDepot_Sort_dir"] = "ASC";
                ViewState["DateExp_Sort_dir"] = "ASC";
            }
            catch (Exception) { }
        }


        protected void Visit_link_Click(object sender, EventArgs e)
        {
            Button Visitbtn = sender as Button;
            GridViewRow gridViewRow = Visitbtn.NamingContainer as GridViewRow;
            List<Marque> marques = Session["Marques"] as List<Marque>;
            Response.Write($"<script language='javascript'>window.open('http://search.ompic.ma/web/pages/consulterMarque.do?id={marques[(int.Parse(ViewState["index"].ToString()) * 8 + gridViewRow.RowIndex)].Id}','_blank');</script>");
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

        protected void Precedent_Click(object sender, EventArgs e)
        {
            int gh = 0;
            if (Session["Marques"] != null)
            {
                if (Session["Marqueschecked"] == null)
                {
                    List<string> marquesidchecked = new List<string>();
                    List<Marque> marquess = Session["Marques"] as List<Marque>;
                    List<Marque_pdf> list_marques_to_export = new List<Marque_pdf>();

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
                    List<Marque> marquess = Session["Marques"] as List<Marque>;
                    List<Marque_pdf> list_marques_to_export = new List<Marque_pdf>();
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

                
            


                if (Session["Marques"] != null)
                {
                    List<Marque> marques = Session["Marques"] as List<Marque>;
                    int i = int.Parse(ViewState["index"].ToString());
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    if (i > 0)
                    {
                        i--;
                        GridView1.DataSource = marques.Skip(i * 8).Take(8);
                        GridView1.DataBind();
                        index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                        ViewState["index"] = i;
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
        }

        protected void Suivant_Click(object sender, EventArgs e)
        {
            if (Session["Marques"] != null)
            {
                if(Session["Marqueschecked"] == null)
                {
                    List<string> marquesidchecked = new List<string>();
                    List<Marque> marquess = Session["Marques"] as List<Marque>;
                    List<Marque_pdf> list_marques_to_export = new List<Marque_pdf>();
                    
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
                    List<Marque> marquess = Session["Marques"] as List<Marque>;
                    List<Marque_pdf> list_marques_to_export = new List<Marque_pdf>();
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

                 



                    List<Marque> marques = Session["Marques"] as List<Marque>;
                    int i = int.Parse(ViewState["index"].ToString());
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    if (i < pages - 1)
                    {
                        i++;
                        GridView1.DataSource = marques.Skip(i * 8).Take(8);
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

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (e.SortExpression == "Id")
            {
                if (ViewState["Id_Sort_dir"] != null && ViewState["Id_Sort_dir"].ToString() == "ASC")
                {
                    List<Marque> marques = Session["Marques"] as List<Marque>;
                    Session["Marques"] = marques.OrderByDescending((mrq) => mrq.Id).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Marques"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["Id_Sort_dir"] = "DESC";
                }
                else if (ViewState["Id_Sort_dir"] != null && ViewState["Id_Sort_dir"].ToString() == "DESC")
                {
                    List<Marque> marques = Session["Marques"] as List<Marque>;
                    Session["Marques"] = marques.OrderBy((mrq) => mrq.Id).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Marques"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["Id_Sort_dir"] = "ASC";
                }
            }
            else if (e.SortExpression == "NomMarque")
            {
                if (ViewState["NomMarque_Sort_dir"] != null && ViewState["NomMarque_Sort_dir"].ToString() == "ASC")
                {
                    List<Marque> marques = Session["Marques"] as List<Marque>;
                    Session["Marques"] = marques.OrderByDescending((mrq) => mrq.NomMarque).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Marques"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["NomMarque_Sort_dir"] = "DESC";
                }
                else if (ViewState["NomMarque_Sort_dir"] != null && ViewState["NomMarque_Sort_dir"].ToString() == "DESC")
                {
                    List<Marque> marques = Session["Marques"] as List<Marque>;
                    Session["Marques"] = marques.OrderBy((mrq) => mrq.NomMarque).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Marques"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["NomMarque_Sort_dir"] = "ASC";
                }
            }
            else if (e.SortExpression == "Etat")
            {
                if (ViewState["Etat_Sort_dir"] != null && ViewState["Etat_Sort_dir"].ToString() == "ASC")
                {
                    List<Marque> marques = Session["Marques"] as List<Marque>;
                    Session["Marques"] = marques.OrderByDescending((mrq) => mrq.Etat).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Marques"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["Etat_Sort_dir"] = "DESC";
                }
                else if (ViewState["Etat_Sort_dir"] != null && ViewState["Etat_Sort_dir"].ToString() == "DESC")
                {
                    List<Marque> marques = Session["Marques"] as List<Marque>;
                    Session["Marques"] = marques.OrderBy((mrq) => mrq.Etat).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Marques"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["Etat_Sort_dir"] = "ASC";
                }
            }
            else if (e.SortExpression == "Titulaire")
            {
                if (ViewState["Titulaire_Sort_dir"] != null && ViewState["Titulaire_Sort_dir"].ToString() == "ASC")
                {
                    List<Marque> marques = Session["Marques"] as List<Marque>;
                    Session["Marques"] = marques.OrderByDescending((mrq) => mrq.Titulaire).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Marques"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["Titulaire_Sort_dir"] = "DESC";
                }
                else if (ViewState["Titulaire_Sort_dir"] != null && ViewState["Titulaire_Sort_dir"].ToString() == "DESC")
                {
                    List<Marque> marques = Session["Marques"] as List<Marque>;
                    Session["Marques"] = marques.OrderBy((mrq) => mrq.Titulaire).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Marques"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["Titulaire_Sort_dir"] = "ASC";
                }
            }
            else if (e.SortExpression == "DateDepot")
            {
                if (ViewState["DateDepot_Sort_dir"] != null && ViewState["DateDepot_Sort_dir"].ToString() == "ASC")
                {
                    List<Marque> marques = Session["Marques"] as List<Marque>;
                    Session["Marques"] = marques.OrderByDescending((mrq) => mrq.DateDepot).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Marques"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["DateDepot_Sort_dir"] = "DESC";
                }
                else if (ViewState["DateDepot_Sort_dir"] != null && ViewState["DateDepot_Sort_dir"].ToString() == "DESC")
                {
                    List<Marque> marques = Session["Marques"] as List<Marque>;
                    Session["Marques"] = marques.OrderBy((mrq) => mrq.DateDepot).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Marques"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["DateDepot_Sort_dir"] = "ASC";
                }
            }
            else if (e.SortExpression == "DateExpiration")
            {
                if (ViewState["DateExp_Sort_dir"] != null && ViewState["DateExp_Sort_dir"].ToString() == "ASC")
                {
                    List<Marque> marques = Session["Marques"] as List<Marque>;
                    Session["Marques"] = marques.OrderByDescending((mrq) => mrq.DateExpiration).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Marques"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["DateExp_Sort_dir"] = "DESC";
                }
                else if (ViewState["DateExp_Sort_dir"] != null && ViewState["DateExp_Sort_dir"].ToString() == "DESC")
                {
                    List<Marque> marques = Session["Marques"] as List<Marque>;
                    Session["Marques"] = marques.OrderBy((mrq) => mrq.DateExpiration).ToList();
                    int i = int.Parse(ViewState["index"].ToString());
                    GridView1.DataSource = (Session["Marques"] as List<Marque>).Skip(i * 8).Take(8);
                    GridView1.DataBind();
                    double result = marques.Count / 8.0;
                    int pages = int.Parse(Math.Ceiling(result).ToString());
                    index.Text = i + 1 + " / " + (pages == 0 ? 1 : pages);
                    ViewState["DateExp_Sort_dir"] = "ASC";
                }
            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {

            GridView gridView = (GridView)sender;
            foreach (GridViewRow row in gridView.Rows)
            {
                if (row.Cells[6].Text == "07/07/3077")
                {
                    row.Cells[6].Text = "";
                }
                if (row.Cells[7].Text == "07/07/3077")
                {
                    row.Cells[7].Text = "";
                }
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
        protected void Archive_Click(object sender, EventArgs e)
        {
            Response.Redirect("archive.aspx");
        }
        protected void btn_parametre_v2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Parametre.aspx");
        }
        struct Marque_pdf
        {
            public byte[] Image { get; set; }
            public string Numero_titre { get; set; }
            public string Nom_marque { get; set; }
            public string Etat { get; set; }
            public string Titulaire { get; set; }
            public string Date_depot { get; set; }
            public string Date_expiration { get; set; }
        }
        protected void Pdf_Download_Click(object sender, EventArgs e)
        {

            if (Session["Marques"] != null)
            {
                List<Marque> marques = Session["Marques"] as List<Marque>;
                List<Marque_pdf> list_marques_to_export = new List<Marque_pdf>();
                foreach (var item in marques)
                {
                    var ms = new MemoryStream();
                    if (item.Image != "Empty.png")
                    {
                        System.Drawing.Image.FromFile(Server.MapPath("~") + $@"/Assets/Brand_image/{item.Image}").Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    list_marques_to_export.Add(new Marque_pdf { Image = ms.ToArray(), Numero_titre = item.NumeroTitre, Nom_marque = item.NomMarque, Etat = item.Etat, Titulaire = item.Titulaire, Date_depot = item.DateDepot.ToShortDateString() == "07/07/3077" ? "" : item.DateDepot.ToShortDateString(), Date_expiration = item.DateExpiration.ToShortDateString() == "07/07/3077" ? "" : item.DateExpiration.ToShortDateString() });
                }
                CrystalReport2 reportMarque = new CrystalReport2();

                reportMarque.Database.Tables["Marque"].SetDataSource(list_marques_to_export.AsEnumerable());
                string filename = $"List des marques.pdf";
                Stream stream = reportMarque.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                reportMarque.Close();
                reportMarque.Dispose();
                MemoryStream memoryS = new MemoryStream();
                stream.CopyTo(memoryS);
                byte[] buffer = memoryS.ToArray();

                Response.AddHeader("Content-Length", buffer.Length.ToString());
                Response.AddHeader("Content-Disposition", $"attachment; filename={filename}");
                Response.OutputStream.Write(buffer, 0, buffer.Length);
                Response.End();
            }
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
        protected void expoert_select_Click(object sender, EventArgs e)
        {
            List<string> checkeds = new List<string>();
            if (Session["Marqueschecked"] == null)
            {
                
                checkeds.Add("test");
            }
            else
            {
                 checkeds = Session["Marqueschecked"] as List<string>;
            }

            if (Session["Marques"] != null)
            {
               
                List<Marque> marques = Session["Marques"] as List<Marque>;
                List<Marque_pdf> list_marques_to_export = new List<Marque_pdf>();
                
                foreach (var item in marques)
                {
                    
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (item.Id.ToString() == row.Cells[1].Text )
                        { 
                            
                              
                                CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                            if (cb.Checked == false)
                            {
                                if (checkeds.Contains(item.Id.ToString()))
                                {
                                    checkeds.Remove(row.Cells[1].Text);
                                }
                            }
                            if (cb.Checked == true & checkeds.Contains(item.Id.ToString())==false)
                            {
                                var ms = new MemoryStream();
                                if (item.Image != "Empty.png")
                                {
                                    System.Drawing.Image.FromFile(Server.MapPath("~") + $@"/Assets/Brand_image/{item.Image}").Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                                list_marques_to_export.Add(new Marque_pdf { Image = ms.ToArray(), Numero_titre = item.NumeroTitre, Nom_marque = item.NomMarque, Etat = item.Etat, Titulaire = item.Titulaire, Date_depot = item.DateDepot.ToShortDateString() == "07/07/3077" ? "" : item.DateDepot.ToShortDateString(), Date_expiration = item.DateExpiration.ToShortDateString() == "07/07/3077" ? "" : item.DateExpiration.ToShortDateString() });
                            }
                            



                        }
                        

                    }
                    if (checkeds.Contains(item.Id.ToString()))
                    {
                        var ms = new MemoryStream();
                        if (item.Image != "Empty.png")
                        {
                            System.Drawing.Image.FromFile(Server.MapPath("~") + $@"/Assets/Brand_image/{item.Image}").Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        list_marques_to_export.Add(new Marque_pdf { Image = ms.ToArray(), Numero_titre = item.NumeroTitre, Nom_marque = item.NomMarque, Etat = item.Etat, Titulaire = item.Titulaire, Date_depot = item.DateDepot.ToShortDateString() == "07/07/3077" ? "" : item.DateDepot.ToShortDateString(), Date_expiration = item.DateExpiration.ToShortDateString() == "07/07/3077" ? "" : item.DateExpiration.ToShortDateString() });
                    }
                }

                Session["Marqueschecked"] = checkeds;

                CrystalReport2 reportMarque = new CrystalReport2();

                reportMarque.Database.Tables["Marque"].SetDataSource(list_marques_to_export.AsEnumerable());
                string filename = $"List des marques.pdf";
                Stream stream = reportMarque.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                reportMarque.Close();
                reportMarque.Dispose();
                MemoryStream memoryS = new MemoryStream();
                stream.CopyTo(memoryS);
                byte[] buffer = memoryS.ToArray();

                Response.AddHeader("Content-Length", buffer.Length.ToString());
                Response.AddHeader("Content-Disposition", $"attachment; filename={filename}");
                Response.OutputStream.Write(buffer, 0, buffer.Length);

                Response.End();
            }
            
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    List<string> marquesidchecked = Session["Marqueschecked"] as List<string>;



        //    string ui = marquesidchecked.Count.ToString();
        //    Session["Marqueschecked"] = marquesidchecked;
        //    TextBox1.Text = ui;
        //    TextBox1.Text += marquesidchecked[0];
        //}
    }


}
    

