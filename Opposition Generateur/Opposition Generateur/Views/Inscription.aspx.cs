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


namespace Opposition_Generateur.Views
{
    public partial class Inscription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_signup_Clickc(object sender, EventArgs e)
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