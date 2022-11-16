using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Opposition_Generateur
{
    public partial class Authentification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["Account_id"] != null && Session["Role"] != null && (int)Session["Account_id"] != -1 && Session["Role"].ToString() != "")
                {
                    Response.Redirect("home.aspx");
                }
                else
                {
                    Response.Cookies.Clear();
                    Session.Remove("Historique");
                    Session.Remove("Marques");
                    Session.Remove("Rech_ompic_list_marque");
                    Session.Remove("List formulaire Opposition");
                    Session.Remove("List_alerte");
                    Session.Remove("marques similaire");
                    Session.Remove("marques ip report");
                    Session.Remove("alerte");
                    Session.Remove("index");
                    Session.Remove("pages");
                    Session.Remove("Old_marques_ipreport");
                    Session.Remove("Old_marques_similaire");
                }
            }
        }
        protected void btn_signin_Click(object sender, EventArgs e)
        {

            SqlConnection conx = new SqlConnection(@"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=Ipp;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            int id = -1;
            string Role = "";
            try
            {


                cmd.Connection = conx;
                cmd.CommandText = "select Account_id from Accounts where Login = @login and Password = @pass";
                cmd.Parameters.AddWithValue("@login", signin_username.Value);
                cmd.Parameters.AddWithValue("@pass", signin_password.Value);
                //string cs = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
                //con = new SqlConnection(cs);
                //con.Open();

                //SqlCommand sQLiteCommand = new SqlCommand();
                //sQLiteCommand.Connection = con;
                //sQLiteCommand.CommandText = "select Account_id from Accounts where Login = @login and Password = @pass";
                //sQLiteCommand.Parameters.AddWithValue("@login", signin_username.Value);
                //sQLiteCommand.Parameters.AddWithValue("@pass", signin_password.Value);
                //var reader = sQLiteCommand.ExecuteReader();
                conx.Open();
                dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {

                    error_msg.InnerText = "Compte introuvable.";
                    error_msg.Style["transform"] = "translateY(0px)";

                    dr.Close();

                }
                else
                {
                    HttpCookie httpCookie = new HttpCookie("Userinfo");
                    dr.Read();
                    id = int.Parse(dr[0].ToString());
                    dr.Close();

                    cmd.CommandText = "select Profile_picture, Fullname , Role_name ,us.Account_id From Users us INNER JOIN Accounts acc ON us.Account_id = acc.Account_id INNER JOIN Roles rol on acc.Role_id = rol.Role_id where us.Account_id = @id";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", id);
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        httpCookie["Profile_pic"] = $"~/Users_profile_picture/{dr[0].ToString()}";
                        httpCookie["Username"] = dr[1].ToString();
                        Role = dr[2].ToString();
                        httpCookie["Role"] = Role;
                        httpCookie["Iduser"] = dr[3].ToString();
                        dr.Close();
                        Response.Cookies.Add(httpCookie);
                    }
                }

                conx.Close();
            }
            catch (Exception ex)
            {
                if (conx != null && conx.State == ConnectionState.Open)
                {
                    conx.Close();
                }
                error_msg.InnerText = $"{ex.Message}.";
                error_msg.Style["transform"] = "translateY(0px)";
            }
            Session["Account_id"] = id;
            Session["Role"] = Role;
            if (Session["Account_id"] != null && Session["Role"] != null && (int)Session["Account_id"] != -1 && Session["Role"].ToString() != "")
            {
                Response.Redirect("home.aspx");
            }
        }
        protected void btn_signup_Click(object sender, EventArgs e)
        {
            SqlConnection conx = new SqlConnection(@"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=Ipp;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            if (profile_picture.PostedFile.ContentType.ToLower() == "image/jpg" || profile_picture.PostedFile.ContentType.ToLower() == "image/jpeg"
             || profile_picture.PostedFile.ContentType.ToLower() == "image/png")
            {

                try
                {

                    conx.Open();
                    if (conx.State == ConnectionState.Open)
                    {
                        cmd.Connection = conx;
                        cmd.CommandText = "select Login , Password from Accounts where Login = @login";
                        cmd.Parameters.AddWithValue("@login", signup_username.Value);
                        dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            error_msg.InnerText = "Le nom d'utilisateur existe deja.";
                            error_msg.Style["transform"] = "translateY(0px)";
                            dr.Close();
                        }
                        else
                        {
                            if (signup_password.Value.Length > 0)
                            {
                                dr.Close();
                                var guid = Guid.NewGuid().ToString();
                                profile_picture.PostedFile.SaveAs(Server.MapPath("~") + $"\\Users_profile_picture\\{guid}.jpg");

                                cmd.CommandText = "insert into Accounts(Login , Password , Role_id) values(@login,@password ,1)";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@login", signup_username.Value);
                                cmd.Parameters.AddWithValue("@password", signup_password.Value);
                                cmd.ExecuteNonQuery();
                                cmd.CommandText = "select Account_id from Accounts where Login = @login";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@login", signup_username.Value);
                                int acc_id = int.Parse(cmd.ExecuteScalar().ToString());
                                cmd.CommandText = "insert into Users(Fullname , Profile_picture , Account_id) values(@fullname ,@profile_picture,@id)";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@fullname", signup_username.Value);
                                cmd.Parameters.AddWithValue("@profile_picture", $"{guid}.jpg");
                                cmd.Parameters.AddWithValue("@id", acc_id);
                                cmd.ExecuteNonQuery();

                            }
                            else
                            {
                                error_msg.InnerText = "Mot de pass est vide.";
                                error_msg.Style["transform"] = "translateY(0px)";
                            }
                        }
                        conx.Close();
                    }
                }
                catch (Exception ex)
                {

                    error_msg.InnerText = $"{ex.Message}.";
                    error_msg.Style["transform"] = "translateY(0px)";
                }
            }
            else
            {
                error_msg.InnerText = "Selectionner une image de profil.";
                error_msg.Style["transform"] = "translateY(0px)";
            }

        }
    }
}