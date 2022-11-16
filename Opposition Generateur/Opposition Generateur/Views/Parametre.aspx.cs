using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

namespace Opposition_Generateur
{
    public partial class Parametre : System.Web.UI.Page
    {
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

                    DataTable dt = new DataTable("Gazette");
                    dt.Columns.Add("Num_pub", typeof(string));
                    dt.Columns.Add("Date", typeof(DateTime));
                    string path = Server.MapPath("~") + "\\Setting\\" + "Setting.xml";
                    dt.ReadXml(path);
                    List<Gazette> gazettes = new List<Gazette>();

                    foreach (DataRow row in dt.Rows)
                    {
                        gazettes.Add(new Gazette() { Num_pub = row[0].ToString(), Date = DateTime.Parse(row[1].ToString()) });
                    }
                    ViewState["Gazettes"] = gazettes;
                    GridView1.DataSource = gazettes;
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

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.  
            GridView1.EditIndex = e.NewEditIndex;
            var gazettes = ViewState["Gazettes"] as List<Gazette>;
            GridView1.DataSource = gazettes;
            GridView1.DataBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var gazettes = ViewState["Gazettes"] as List<Gazette>;
            gazettes[e.RowIndex].Num_pub = (GridView1.Rows[e.RowIndex].FindControl("TxtBox_Num_pub") as TextBox).Text;
            gazettes[e.RowIndex].Date = DateTime.Parse((GridView1.Rows[e.RowIndex].FindControl("TxtBox_date") as TextBox).Text);
            DataTable dt = new DataTable("Gazette");
            dt.Columns.Add("Num_pub", typeof(string));
            dt.Columns.Add("Date", typeof(DateTime));
            string path = Server.MapPath("~") + "\\Setting\\" + "Setting.xml";
            File.Delete(path);
            foreach (var item in gazettes)
            {
                dt.Rows.Add(item.Num_pub, item.Date);
            }
            dt.WriteXml(path);
            ViewState["Gazettes"] = gazettes;
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView1.EditIndex = -1;
            GridView1.DataSource = gazettes;
            GridView1.DataBind();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView1.EditIndex = -1;
            var gazettes = ViewState["Gazettes"] as List<Gazette>;
            GridView1.DataSource = gazettes;
            GridView1.DataBind();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            Button Delete = sender as Button;
            GridViewRow gridViewRow = Delete.NamingContainer as GridViewRow;
            var gazettes = ViewState["Gazettes"] as List<Gazette>;
            gazettes.RemoveAt(gridViewRow.RowIndex);
            DataTable dt = new DataTable("Gazette");
            dt.Columns.Add("Num_pub", typeof(string));
            dt.Columns.Add("Date", typeof(DateTime));
            string path = Server.MapPath("~") + "\\Setting\\" + "Setting.xml";
            File.Delete(path);
            foreach (var item in gazettes)
            {
                dt.Rows.Add(item.Num_pub, item.Date);
            }
            dt.WriteXml(path);
            ViewState["Gazettes"] = gazettes;
            GridView1.DataSource = gazettes;
            GridView1.DataBind();

        }

        protected void btn_ajouter_alerte_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ajouter alerte.aspx");
        }

        protected void btn_generer_doc_Click(object sender, EventArgs e)
        {
            Response.Redirect("Generer pdf.aspx");
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
        protected void btn_Insert_Click(object sender, EventArgs e)
        {
            var gazettes = ViewState["Gazettes"] as List<Gazette>;
            gazettes.Add(new Gazette() { Num_pub = (GridView1.FooterRow.FindControl("TxtBox_Num_pub_footer") as TextBox).Text, Date = DateTime.Parse((GridView1.FooterRow.FindControl("TxtBox_date_footer") as TextBox).Text) });
            DataTable dt = new DataTable("Gazette");
            dt.Columns.Add("Num_pub", typeof(string));
            dt.Columns.Add("Date", typeof(DateTime));
            string path = Server.MapPath("~") + "\\Setting\\" + "Setting.xml";
            File.Delete(path);
            foreach (var item in gazettes)
            {
                dt.Rows.Add(item.Num_pub, item.Date);
            }
            dt.WriteXml(path);
            ViewState["Gazettes"] = gazettes;
            GridView1.DataSource = gazettes;
            GridView1.DataBind();
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

