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
    public partial class archive : System.Web.UI.Page
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

            SqlConnection con = new SqlConnection(@"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=data;Integrated Security=True");
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            con.Open();

            List<string> list = new List<string>();
            SqlDataReader dr;
            DataTable docs = new DataTable();
            
            List<Marque> marques = new List<Marque>();
            if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_debut"]) && !string.IsNullOrWhiteSpace(Request.Form["date_depot_fin"]))
            {
                list.Add("datec >= @date_depot_debut and datec <= @date_depot_fin");
                command.Parameters.AddWithValue("@date_depot_debut", DateTime.Parse(Request.Form["date_depot_debut"]));
                command.Parameters.AddWithValue("@date_depot_fin", DateTime.Parse(Request.Form["date_depot_fin"]));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_debut"]))
                {
                    list.Add("datec = @date_depot_debut");
                    command.Parameters.AddWithValue("@date_depot_debut", DateTime.Parse(Request.Form["date_depot_debut"]));
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["date_depot_fin"]))
                {
                    list.Add("datec = @date_depot_fin");
                    command.Parameters.AddWithValue("@date_depot_fin", DateTime.Parse(Request.Form["date_depot_fin"]));
                }
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["search_field"]))
            {
                list.Add("Ref like @Ref");
                command.Parameters.AddWithValue("@Ref",   Request.Form["search_field"].Trim()  );
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["user"]))
            {
                list.Add("users like @users");
                command.Parameters.AddWithValue("@users", "%" + Request.Form["user"].Trim() + "%");
            }
            if (!string.IsNullOrWhiteSpace(Request.Form["Type"]))
            {
                list.Add("type like @Type");
                command.Parameters.AddWithValue("@type", "%" + Request.Form["Type"].Trim() + "%");
            }
            if (list.Count > 0)
            {

                var Ompicquery = "select * from article where ";
                for (int i = 0; i < list.Count; i++)
                {
                    if ((list.Count - 1) == i)
                    {

                        Ompicquery += list[i] ;
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
                docs.Load(reader);
                con.Close();

            }
            foreach (DataRow marq in docs.Rows)
            {
                try
                {
                    Marque marque = new Marque();
                    
                    marque.NumeroTitre = marq[2].ToString();
                    marque.NomMarque = marq[3].ToString();
                    marque.Etat = marq[4].ToString();
                    marque.Titulaire = marq[0].ToString();





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


        protected void Visit_link_Click(object sender, EventArgs e)
        {
            Button Visitbtn = sender as Button;
            GridViewRow gridViewRow = Visitbtn.NamingContainer as GridViewRow;
            //List<Marque> marques = Session["Marques"] as List<Marque>;
            //Response.Write($"<script language='javascript'>window.open('http://search.ompic.ma/web/pages/consulterMarque.do?id={marques[(int.Parse(ViewState["index"].ToString()) * 8 + gridViewRow.RowIndex)].Id}','_blank');</script>");
            string indexx = GridView1.Rows[gridViewRow.RowIndex].Cells[0].Text;
            DataTable table = new DataTable();
           

            SqlConnection con = new SqlConnection(@"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=data;Integrated Security=True");
            SqlCommand cmdd = new SqlCommand();
            cmdd.Connection = con;
            con.Open();
            cmdd.CommandText = "select * from article where Ref ='" + indexx + "'";
            var reader = cmdd.ExecuteReader();
            table.Load(reader);
            con.Close();
            byte[] buffer =(byte[]) table.Rows[0][1];
            string filename = $"Ref_"+ table.Rows[0][0] + "_"+ table.Rows[0][4] + ".pdf";
            Response.AddHeader("Content-Length", buffer.Length.ToString());
            Response.AddHeader("Content-Disposition", $"attachment; filename={filename}");
            Response.OutputStream.Write(buffer, 0, buffer.Length);
            Response.End();



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
        protected void Archive_Click(object sender, EventArgs e)
        {
            Response.Redirect("archive.aspx");
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

            //GridView gridView = (GridView)sender;
            //foreach (GridViewRow row in gridView.Rows)
            //{
            //    if (row.Cells[6].Text == "07/07/3077")
            //    {
            //        row.Cells[6].Text = "";
            //    }
            //    if (row.Cells[7].Text == "07/07/3077")
            //    {
            //        row.Cells[7].Text = "";
            //    }
            //}

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
                        if (item.Id.ToString() == row.Cells[1].Text)
                        {


                            CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
                            if (cb.Checked == false)
                            {
                                if (checkeds.Contains(item.Id.ToString()))
                                {
                                    checkeds.Remove(row.Cells[1].Text);
                                }
                            }
                            if (cb.Checked == true & checkeds.Contains(item.Id.ToString()) == false)
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
