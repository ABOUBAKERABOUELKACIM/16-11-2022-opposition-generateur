using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opposition_Generateur.Models;

namespace Opposition_Generateur.Views
{
    public partial class Details : System.Web.UI.Page
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

                    if (!string.IsNullOrWhiteSpace(Request["ST13"]))
                    {
                        SqlConnection con = new SqlConnection(@"Data Source=IPSERVER\SQLEXPRESS;Initial Catalog=Ipp;Integrated Security=True");
                        con.Open();

                        if (con.State == ConnectionState.Open)
                        {
                            if (!string.IsNullOrWhiteSpace(Request["ST13"]))
                            {
                                if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{Request["ST13"]}.jpg"))
                                {
                                    detailImage.Src = $"http://192.168.2.2/IppApp/Assets/Brand_image/{Request["ST13"]}.jpg";
                                }
                                else
                                {
                                    if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{Request["ST13"]}.JPG"))
                                    {
                                        detailImage.Src = $"http://192.168.2.2/IppApp/Assets/Brand_image/{Request["ST13"]}.JPG";
                                    }
                                    else
                                    {
                                        if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{Request["ST13"]}.jpeg"))
                                        {
                                            detailImage.Src = $"http://192.168.2.2/IppApp/Assets/Brand_image/{Request["ST13"]}.jpeg";
                                        }
                                        else
                                        {
                                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\{Request["ST13"]}.png"))
                                            {
                                                detailImage.Src = $"http://192.168.2.2/IppApp/Assets/Brand_image/{Request["ST13"]}.png";
                                            }

                                        }
                                    }
                                }
                            }
                            if (File.Exists(Server.MapPath("~") + $@"\Assets\Brand_image\temp_{Request["ST13"]}.jpg"))
                            {
                                detailImage.Src = $@"../Assets/Brand_image/temp_{Request["ST13"]}.jpg";

                            }


                            ///new details page backend
                            ///start
                            SqlCommand command = new SqlCommand();
                            SqlCommand commande = new SqlCommand();
                            SqlCommand commandee = new SqlCommand();
                            command.Connection = con;
                            commande.Connection = con;
                            commandee.Connection = con;
                            command.CommandType = CommandType.Text;
                            command.CommandText = "select * from Marques_Ompic where NumeroTitre = @st";

                            command.Parameters.Clear();
                            DataTable TM = new DataTable();
                            DataTable OMPIC = new DataTable();
                            MarqueDetails marque = new MarqueDetails();
                            command.Parameters.AddWithValue("@st", Request["ST13"]);
                            var rdrss = command.ExecuteReader();

                            if (rdrss.HasRows)
                            {
                                OMPIC.Load(rdrss);
                            }
                            rdrss.Close();
                            commandee.CommandText = "select * from Marques_Tm where ST13 = @st";
                            commandee.Parameters.Clear();
                            commandee.Parameters.AddWithValue("@st", Request["ST13"]);
                           
                            var rdre = commandee.ExecuteReader();
                            


                            if (rdre.HasRows)
                            {
                                TM.Load(rdre);
                            }
                            rdre.Close();
                           if(TM.Rows.Count != 0 || OMPIC.Rows.Count != 0)
                            {
                                if (TM.Rows.Count != 0 && OMPIC.Rows.Count != 0)
                                {
                                    if (TM.Rows[0][10].ToString().Trim() != OMPIC.Rows[0][0].ToString().Trim())
                                    {
                                        marque.Deposant = $"<span class='ompic_value'>" + OMPIC.Rows[0][0].ToString().Trim() + "</span>" + "<br/>" + "<span class='tm_value'>" + TM.Rows[0][10].ToString().Trim() + "</span>";
                                    }
                                    else
                                    {
                                        marque.Deposant = OMPIC.Rows[0][0].ToString().Trim();
                                    }
                                    if (TM.Rows[0][16].ToString().Trim() != OMPIC.Rows[0][9].ToString().Trim())
                                    {
                                        marque.Mandataire = $"<span class='ompic_value'>" + OMPIC.Rows[0][9].ToString().Trim() + "</span>" + "<br/>" + "<span class='tm_value'>" + TM.Rows[0][16].ToString().Trim() + "</span>";
                                    }
                                    else
                                    {
                                        marque.Mandataire = OMPIC.Rows[0][9].ToString().Trim();
                                    }
                                    if (OMPIC.Rows[0][11].ToString().Trim() != TM.Rows[0][13].ToString().Trim())
                                    {
                                        marque.Deposantadresse = $"<span class='ompic_value'>" + OMPIC.Rows[0][11].ToString().Trim() + "</span>" + "<br/>" + "<span class='tm_value'>" + TM.Rows[0][13].ToString().Trim() + "</span>";
                                    }
                                    else
                                    {
                                        marque.Deposantadresse = OMPIC.Rows[0][11].ToString().Trim();
                                    }
                                    if (OMPIC.Rows[0][10].ToString().Trim() != TM.Rows[0][1].ToString().Trim())
                                    {
                                        marque.NomMarque = $"<span class='ompic_value'>" + OMPIC.Rows[0][10].ToString().Trim() + "</span>" + "<br/>" + "<span class='tm_value'>" + TM.Rows[0][2].ToString().Trim() + "</span>";
                                    }
                                    else
                                    {
                                        marque.NomMarque = OMPIC.Rows[0][10].ToString().Trim();
                                    }
                                    if (OMPIC.Rows[0][1].ToString().Trim() != TM.Rows[0][3].ToString().Trim())
                                    {
                                        marque.datedepot = $"<span class='ompic_value'>" + OMPIC.Rows[0][1].ToString().Split(' ').FirstOrDefault().Trim() + "</span>" + "<br/>" + "<span class='tm_value'>" + TM.Rows[0][3].ToString().Split(' ').FirstOrDefault().Trim() + "</span>";
                                    }
                                    else
                                    {
                                        marque.datedepot = OMPIC.Rows[0][1].ToString().Split(' ').FirstOrDefault().Trim();
                                    }
                                    if (OMPIC.Rows[0][2].ToString().Trim() != TM.Rows[0][5].ToString().Trim())
                                    {
                                        marque.dateexpir = $"<span class='ompic_value'>" + OMPIC.Rows[0][2].ToString().Split(' ').FirstOrDefault().Trim() + "</span>" + "<br/>" + "<span class='tm_value'>" + TM.Rows[0][5].ToString().Split(' ').FirstOrDefault().Trim() + "</span>";
                                    }
                                    else
                                    {
                                        marque.dateexpir = OMPIC.Rows[0][2].ToString().Split(' ').FirstOrDefault().Trim();
                                    }
                                    if (!string.IsNullOrWhiteSpace(OMPIC.Rows[0][2].ToString()))
                                    {

                                        marque.NumPubllication = OMPIC.Rows[0][2].ToString().Trim();
                                    }
                                    if (!string.IsNullOrWhiteSpace(OMPIC.Rows[0][3].ToString()))
                                    {

                                        marque.deposantepays = OMPIC.Rows[0][3].ToString();
                                    }
                                    if (!string.IsNullOrWhiteSpace(OMPIC.Rows[0][12].ToString()))
                                    {

                                        marque.Statut = OMPIC.Rows[0][12].ToString().Trim();
                                    }

                                    if (!string.IsNullOrWhiteSpace(OMPIC.Rows[0][5].ToString()))
                                    {


                                        string nice = OMPIC.Rows[0][5].ToString();
                                        if (nice != null)
                                        {
                                            List<string> list = new List<string>();
                                            var Nicearray = nice.Split(',');
                                            foreach (var item in Nicearray)
                                            {
                                                if (!list.Contains(item.Trim()))
                                                {
                                                    list.Add(item.Trim());
                                                }
                                            }
                                            string str = "";
                                            foreach (var item in list)
                                            {
                                                str += item + ",";
                                            }
                                            if (!string.IsNullOrWhiteSpace(str))
                                            {
                                                marque.classnice = str.Remove(str.LastIndexOf(','));
                                            }
                                        }
                                    }

                                    if (!string.IsNullOrWhiteSpace(OMPIC.Rows[0][6].ToString()))
                                    {

                                        string ps = OMPIC.Rows[0][6].ToString();
                                        string contentPS = "";

                                        contentPS += $"<tr><th width='10%' style='padding:15px 0px;color:#2D4D62;font-weight:bold;'></th><td width='90%' style='color:#2D4D62;padding:15px 0px;text-align:justify;'>{ps}</td></tr>";
                                        marque.Produits_Services = $"<table><tbody>{contentPS}</tbody></table>";
                                    }

                                    if (!string.IsNullOrWhiteSpace(OMPIC.Rows[0][3].ToString()))
                                    {

                                        marque.deposantepays = OMPIC.Rows[0][3].ToString();
                                    }
                                    if (!string.IsNullOrWhiteSpace(TM.Rows[0][18].ToString()))
                                    {

                                        marque.Mandataireadresse = TM.Rows[0][18].ToString();
                                    }
                                    if (!string.IsNullOrWhiteSpace(OMPIC.Rows[0][8].ToString()))
                                    {

                                        marque.typemarque = OMPIC.Rows[0][8].ToString();
                                    }
                                    var deposantsNationality = TM.Rows[0][12]?.ToString().Split('\n');
                                    if (deposantsNationality != null)
                                    {
                                        foreach (var depNationality in deposantsNationality)
                                        {
                                            marque.deposantnatio += depNationality + "<br/>";
                                        }
                                    }
                                    marque.Mandataireadresse = TM.Rows[0][18]?.ToString();

                                    marque.Mandatairepays = TM.Rows[0][20]?.ToString();
                                    marque.Publicationsection = TM.Rows[0][22]?.ToString();
                                    marque.NumMarque = OMPIC.Rows[0][7]?.ToString();


                                    //applicationNumber.InnerText = rdr[7]?.ToString();
                                    //typeMarque.InnerText = rdr[8]?.ToString();
                                    //mandatairee.InnerText = rdr[9]?.ToString();
                                    //nomMarque.InnerText = rdr[10]?.ToString();
                                    //deposantAddress.InnerText = rdr[11]?.ToString();
                                    //statutMarque.InnerText = rdr[12]?.ToString();

                                    //numeroPublication.InnerText = rdr[15]?.ToString();

                                    deposantt.InnerHtml = marque.Deposant;
                                    dateDepot.InnerHtml = marque.datedepot;
                                    expiryDate.InnerHtml = marque.dateexpir;
                                    deposantPays.InnerHtml = marque.deposantepays.ToString();
                                    Produits_Services.InnerHtml = marque.Produits_Services;
                                    applicationNumber.InnerHtml = marque.NumMarque.ToString();
                                    typeMarque.InnerHtml = marque.typemarque.ToString();
                                    mandatairee.InnerHtml = marque.Mandataire.ToString();
                                    nomMarque.InnerHtml = marque.NomMarque?.ToString();
                                    deposantAddress.InnerHtml = marque.Deposantadresse.ToString();
                                    statutMarque.InnerHtml = marque.Statut.ToString();
                                    numeroPublication.InnerHtml = marque.NumPubllication.ToString();
                                    niceClassNumbers.InnerHtml = marque.classnice;                                   
                                    deposantNationality.InnerHtml =marque.deposantnatio;
                                    mandataireNationality.InnerHtml = marque.mandatairenatio;
                                    mandataireAddress.InnerHtml = marque.Mandataireadresse;                                                                       
                                    mandatairePays.InnerHtml = marque.Mandatairepays;
                                    sectionPublication.InnerHtml = marque.Publicationsection;
                                    






                                }
                                else if (TM.Rows.Count == 0)
                                {
                                    deposantt.InnerText = OMPIC.Rows[0][0]?.ToString();
                                    dateDepot.InnerText = OMPIC.Rows[0][1]?.ToString().Split(' ').FirstOrDefault().Trim();
                                    expiryDate.InnerText = OMPIC.Rows[0][2]?.ToString().Split(' ').FirstOrDefault().Trim();
                                    deposantPays.InnerText = OMPIC.Rows[0][3]?.ToString();
                                    string nice = OMPIC.Rows[0][5]?.ToString();
                                    if (nice != null)
                                    {
                                        List<string> list = new List<string>();
                                        var Nicearray = nice.Split(',');
                                        foreach (var item in Nicearray)
                                        {
                                            if (!list.Contains(item.Trim()))
                                            {
                                                list.Add(item.Trim());
                                            }
                                        }
                                        string str = "";
                                        foreach (var item in list)
                                        {
                                            str += item + ",";
                                        }
                                        if (!string.IsNullOrWhiteSpace(str))
                                        {
                                            niceClassNumbers.InnerText = str.Remove(str.LastIndexOf(','));
                                        }
                                    }
                                    string ps = OMPIC.Rows[0][6]?.ToString();
                                    string contentPS = "";

                                    contentPS += $"<tr><th width='15%' style='padding:15px 0px;color:#2D4D62;font-weight:bold;'>Toutes les classes</th><td width='85%' style='color:#2D4D62;padding:15px 0px;text-align:justify;'>{ps}</td></tr>";

                                    Produits_Services.InnerHtml = $"<table><tbody>{contentPS}</tbody></table>";
                                    applicationNumber.InnerText = OMPIC.Rows[0][7]?.ToString();
                                    typeMarque.InnerText = OMPIC.Rows[0][8]?.ToString();
                                    mandatairee.InnerText = OMPIC.Rows[0][9]?.ToString();
                                    nomMarque.InnerText = OMPIC.Rows[0][10]?.ToString();
                                    deposantAddress.InnerText = OMPIC.Rows[0][11]?.ToString();
                                    statutMarque.InnerText = OMPIC.Rows[0][12]?.ToString();

                                    numeroPublication.InnerText = OMPIC.Rows[0][15]?.ToString();
                                }
                                else if (OMPIC.Rows.Count == 0)
                                {
                                    //elementVerbal.InnerText = rdr[1]?.ToString();
                                    nomMarque.InnerText = TM.Rows[0][1]?.ToString();
                                    applicationNumber.InnerText = TM.Rows[0][2]?.ToString();
                                    dateDepot.InnerText = TM.Rows[0][3]?.ToString().Split(' ').FirstOrDefault().Trim();
                                    //numRegistration.InnerText = rdr[4]?.ToString();

                                    expiryDate.InnerText = TM.Rows[0][5]?.ToString().Split(' ').FirstOrDefault().Trim();

                                    if (!string.IsNullOrWhiteSpace(TM.Rows[0][6]?.ToString()) && TM.Rows[0][6]?.ToString() == "Combined")
                                    {
                                        typeMarque.InnerText = "Mixte";
                                    }
                                    else if (!string.IsNullOrWhiteSpace(TM.Rows[0][6]?.ToString()) && TM.Rows[0][6]?.ToString() == "Sound")
                                    {
                                        typeMarque.InnerText = "Sonore";
                                    }
                                    else if (!string.IsNullOrWhiteSpace(TM.Rows[0][6]?.ToString()) && TM.Rows[0][6]?.ToString() == "Other")
                                    {
                                        typeMarque.InnerText = "Autres";
                                    }
                                    else
                                    {
                                        typeMarque.InnerText = TM.Rows[0][6]?.ToString();
                                    }

                                    if (!string.IsNullOrWhiteSpace(TM.Rows[0][8]?.ToString()) && TM.Rows[0][8]?.ToString() == "Application refused")
                                    {
                                        statutMarque.InnerText = "REJETEE";
                                    }
                                    else if (!string.IsNullOrWhiteSpace(TM.Rows[0][8]?.ToString()) && TM.Rows[0][8]?.ToString() == "Registration surrendered")
                                    {
                                        statutMarque.InnerText = "RENONCEE";
                                    }
                                    else if (!string.IsNullOrWhiteSpace(TM.Rows[0][8]?.ToString()) && TM.Rows[0][8]?.ToString() == "Application filed")
                                    {
                                        statutMarque.InnerText = "EN INSTANCE DE PUBLICATION";
                                    }
                                    else if (!string.IsNullOrWhiteSpace(TM.Rows[0][8]?.ToString()) && TM.Rows[0][8]?.ToString() == "Renewed")
                                    {
                                        statutMarque.InnerText = "RENOUVELEE";
                                    }
                                    else if (!string.IsNullOrWhiteSpace(TM.Rows[0][8]?.ToString()) && TM.Rows[0][8]?.ToString() == "Appeal pending")
                                    {
                                        statutMarque.InnerText = "EN POURSUITE DE PROCEDURE";
                                    }
                                    else if (!string.IsNullOrWhiteSpace(TM.Rows[0][8]?.ToString()) && TM.Rows[0][8]?.ToString() == "Application opposed")
                                    {
                                        statutMarque.InnerText = "OPPOSITION EN COURS";
                                    }
                                    else if (!string.IsNullOrWhiteSpace(TM.Rows[0][8]?.ToString()) && TM.Rows[0][8]?.ToString() == "Registration cancelled")
                                    {
                                        statutMarque.InnerText = "RADIEE";
                                    }
                                    else if (!string.IsNullOrWhiteSpace(TM.Rows[0][8]?.ToString()) && TM.Rows[0][8]?.ToString() == "Registered")
                                    {
                                        statutMarque.InnerText = "ENREGISTREE";
                                    }
                                    else if (!string.IsNullOrWhiteSpace(TM.Rows[0][8]?.ToString()) && TM.Rows[0][8]?.ToString() == "Application withdrawn")
                                    {
                                        statutMarque.InnerText = "RETIREE";
                                    }
                                    else if (!string.IsNullOrWhiteSpace(TM.Rows[0][8]?.ToString()) && TM.Rows[0][8]?.ToString() == "Application published")
                                    {
                                        statutMarque.InnerText = "PUBLIEE";
                                    }
                                    else if (!string.IsNullOrWhiteSpace(TM.Rows[0][8]?.ToString()) && TM.Rows[0][8]?.ToString() == "Expired")
                                    {
                                        statutMarque.InnerText = "EXPIREE";
                                    }
                                    else
                                    {
                                        statutMarque.InnerText = TM.Rows[0][8]?.ToString();
                                    }

                                    var deposants = TM.Rows[0][10]?.ToString().Split('\n');
                                    if (deposants != null)
                                    {
                                        foreach (var deposant in deposants)
                                        {
                                            deposantt.InnerHtml += deposant + "<br/>";
                                        }
                                    }
                                    var deposantsNationality = TM.Rows[0][12]?.ToString().Split('\n');
                                    if (deposantsNationality != null)
                                    {
                                        foreach (var depNationality in deposantsNationality)
                                        {
                                            deposantNationality.InnerHtml += depNationality + "<br/>";
                                        }
                                    }
                                    var deposantsAddress = TM.Rows[0][13]?.ToString().Split('\n');
                                    if (deposantsAddress != null)
                                    {
                                        foreach (var depAddress in deposantsAddress)
                                        {
                                            deposantAddress.InnerHtml += depAddress + "<br/>";
                                        }
                                    }
                                    var deposantsCity = TM.Rows[0][14]?.ToString().Split('\n');
                                    if (deposantsCity != null)
                                    {
                                        foreach (var depCity in deposantsCity)
                                        {
                                            deposantCity.InnerHtml += depCity + "<br/>";
                                        }
                                    }
                                    var deposantsPays = TM.Rows[0][15]?.ToString().Split('\n');
                                    if (deposantsPays != null)
                                    {
                                        foreach (var depPays in deposantsPays)
                                        {
                                            deposantPays.InnerHtml += depPays + "<br/>";

                                        }
                                    }
                                    mandatairee.InnerText = TM.Rows[0][16]?.ToString();
                                    mandataireNationality.InnerText = TM.Rows[0][17]?.ToString();

                                    mandataireAddress.InnerText = TM.Rows[0][18]?.ToString();
                                    mandataireCity.InnerText = TM.Rows[0][19]?.ToString();
                                    mandatairePays.InnerText = TM.Rows[0][20]?.ToString();

                                    numeroPublication.InnerText = TM.Rows[0][21]?.ToString();

                                    sectionPublication.InnerText = TM.Rows[0][22]?.ToString();
                                    datePublication.InnerText = TM.Rows[0][23]?.ToString().Split(' ').FirstOrDefault().Trim();
                                    niceClassNumbers.InnerText = TM.Rows[0][9]?.ToString();
                                    string ps = TM.Rows[0][32]?.ToString();
                                    ps = ps == null ? "" : ps;
                                    var NiceClassarray = ps.Split('\n');

                                    string contentPS = "";
                                    foreach (var item in NiceClassarray)
                                    {
                                        contentPS += $"<tr><th width='10%' style='padding:15px 0px;color:#2D4D62;font-weight:bold;'>{item.Split(':').FirstOrDefault()}</th><td width='90%' style='color:#2D4D62;padding:15px 0px;text-align:justify;'>{item.Split(':').LastOrDefault()}</td></tr>";
                                    }

                                    Produits_Services.InnerHtml = $"<table><tbody>{contentPS}</tbody></table>";

                                    string contentOpp = "";
                                    if (string.IsNullOrWhiteSpace(TM.Rows[0][24]?.ToString()))
                                    {

                                        
                                    }

                                    if (!string.IsNullOrWhiteSpace(TM.Rows[0][24]?.ToString()))
                                    {
                                        var array = TM.Rows[0][24]?.ToString().Split('\n');
                                        if (array.Length > 0)
                                        {
                                            Oppositionss.InnerHtml = "Oppositions";
                                            string str = "";
                                            foreach (var item in array)
                                            {
                                                str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item.Split(' ').FirstOrDefault().Trim()}</td>";
                                            }
                                            str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Opposition date </th>" + str + "</tr>";
                                            contentOpp += str;
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(TM.Rows[0][25]?.ToString()))
                                    {
                                        var array = TM.Rows[0][25]?.ToString().Split('\n');
                                        if (array.Length > 0)
                                        {
                                            string str = "";
                                            foreach (var item in array)
                                            {
                                                str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item}</td>";
                                            }
                                            str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Droit anterieure opposition </th>" + str + "</tr>";
                                            contentOpp += str;
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(TM.Rows[0][26]?.ToString()))
                                    {
                                        var array = TM.Rows[0][26]?.ToString().Split('\n');
                                        if (array.Length > 0)
                                        {
                                            string str = "";
                                            foreach (var item in array)
                                            {
                                                str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item}</td>";
                                            }
                                            str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Opposant </th>" + str + "</tr>";
                                            contentOpp += str;
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(TM.Rows[0][27]?.ToString()))
                                    {
                                        var array = TM.Rows[0][27]?.ToString().Split('\n');
                                        if (array.Length > 0)
                                        {
                                            string str = "";
                                            foreach (var item in array)
                                            {
                                                str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item}</td>";
                                            }
                                            str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Opposant entité légale </th>" + str + "</tr>";
                                            contentOpp += str;
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(TM.Rows[0][28]?.ToString()))
                                    {
                                        var array = TM.Rows[0][28]?.ToString().Split('\n');
                                        if (array.Length > 0)
                                        {
                                            string str = "";
                                            foreach (var item in array)
                                            {
                                                str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item}</td>";
                                            }
                                            str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Opposant nationalité code </th>" + str + "</tr>";
                                            contentOpp += str;
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(TM.Rows[0][29]?.ToString()))
                                    {
                                        var array = TM.Rows[0][29]?.ToString().Split('\n');
                                        if (array.Length > 0)
                                        {
                                            string str = "";
                                            foreach (var item in array)
                                            {
                                                str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item}</td>";
                                            }
                                            str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Opposant adresse </th>" + str + "</tr>";
                                            contentOpp += str;
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(TM.Rows[0][30]?.ToString()))
                                    {
                                        var array = TM.Rows[0][30]?.ToString().Split('\n');
                                        if (array.Length > 0)
                                        {
                                            string str = "";
                                            foreach (var item in array)
                                            {
                                                str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item}</td>";
                                            }
                                            str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Opposant ville </th>" + str + "</tr>";
                                            contentOpp += str;
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(TM.Rows[0][31]?.ToString()))
                                    {
                                        var array = TM.Rows[0][31]?.ToString().Split('\n');
                                        if (array.Length > 0)
                                        {
                                            string str = "";
                                            foreach (var item in array)
                                            {
                                                str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item}</td>";
                                            }
                                            str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Opposant pays code </th>" + str + "</tr>";
                                            contentOpp += str;
                                        }
                                    }
                                    Oppositions.InnerHtml = $"<table style='border:1px solid #2D4D62;border-collapse:collapse;'><tbody>{contentOpp}</tbody></table>";
                                }

                                
                                //historiqueP.InnerHtml = "testhistoIF";
                                commande.CommandText = "select * from Marque_historique where NumeroTitre = @st";
                                commande.Parameters.AddWithValue("@st", Request["ST13"]);
                                var rdrs = commande.ExecuteReader();

                                if (!rdrs.HasRows)
                                {

                                    historiqueP.InnerHtml = "";
                                }
                                else
                                {


                                    DataTable tb = new DataTable();
                                    tb.Load(rdrs);
                                    string contenthis = "";
                                    string strk = "";

                                    contenthis += "<tr><th style='background-color: #92a8d1;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Nom Champ </th><th style='background-color: #92a8d1;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Ancienne Valeur </th><th style='background-color: #92a8d1;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Nouvelle valeur </th><th style='background-color: #92a8d1;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Date Modification </th></tr>";


                                    foreach (DataRow roxw in tb.Rows)
                                    {
                                        strk += $"<tr><td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{roxw[1]}</td><td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{roxw[2]}</td><td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{roxw[3]}</td><td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{roxw[4]}</td></tr>";
                                    }

                                    contenthis += strk;

                                    historique.InnerHtml = $"<table style='border:1px solid #2D4D62;border-collapse:collapse;'><tbody>{contenthis}</tbody></table>";
                                }
                                rdrs.Close();
                                rdrss.Close();


                            }
                            SqlCommand commandeee = new SqlCommand();
                            commandeee.Connection = con;
                            SqlDataReader rdrex;
                            string emails = "";
                            string tels = "";
                            List<string> tel = new List<string>();
                            List<string> email = new List<string>();
                            List<string> telM = new List<string>();
                            List<string> emailM = new List<string>();



                            if (OMPIC.Rows.Count != 0)
                            {
                                if (!string.IsNullOrEmpty(OMPIC.Rows[0][0].ToString().Trim()))
                                {
                                    commandeee.CommandText = "select * from emails where DEPOSANT ='" + OMPIC.Rows[0][0].ToString().Trim() + "'  ";

                                    //TM.Rows[0][16].ToString().Trim() != 


                                    con.Close();
                                    con.Open();
                                    rdrex = commandeee.ExecuteReader();


                                    if (rdrex.HasRows)
                                    {
                                        while (rdrex.Read())
                                        {
                                            if (!email.Contains(rdrex[1].ToString().Trim()))
                                            {
                                                email.Add(rdrex[1].ToString().Trim());
                                            }

                                            if (!tel.Contains(rdrex[2].ToString().Trim()))
                                            {
                                                tel.Add(rdrex[2].ToString().Trim());
                                            }
                                        }
                                        //Contact.InnerHtml = $"<div>Emails :<br/>{emails} </div><div>Téléphones : <br/>{tels} </div>";

                                    }

                                }
                                if (!string.IsNullOrEmpty(OMPIC.Rows[0][9].ToString().Trim()))
                                {
                                    commandeee.CommandText = "select * from emails where DEPOSANT ='" + OMPIC.Rows[0][9].ToString().Trim() + "'  ";

                                    //TM.Rows[0][16].ToString().Trim() != 


                                    con.Close();
                                    con.Open();
                                    rdrex = commandeee.ExecuteReader();


                                    if (rdrex.HasRows)
                                    {
                                        while (rdrex.Read())
                                        {
                                            if (!emailM.Contains(rdrex[1].ToString().Trim()))
                                            {
                                                emailM.Add(rdrex[1].ToString().Trim());
                                            }

                                            if (!telM.Contains(rdrex[2].ToString().Trim()))
                                            {
                                                telM.Add(rdrex[2].ToString().Trim());
                                            }
                                        }
                                        //Contact.InnerHtml = $"<div>Emails :<br/>{emails} </div><div>Téléphones : <br/>{tels} </div>";

                                    }

                                }




                            }
                            if (TM.Rows.Count!=0)
                            {
                                if (!string.IsNullOrEmpty(TM.Rows[0][10].ToString().Trim()))
                                {
                                    commandeee.CommandText = "select * from emails where DEPOSANT ='" + TM.Rows[0][10].ToString().Trim() + "' ";

                                    con.Close();
                                    con.Open();
                                    rdrex = commandeee.ExecuteReader();
                                    if (rdrex.HasRows)
                                    {
                                        while (rdrex.Read())
                                        {
                                            if (!email.Contains(rdrex[1].ToString().Trim()))
                                            {
                                                email.Add(rdrex[1].ToString().Trim());
                                            }

                                            if (!tel.Contains(rdrex[2].ToString().Trim()))
                                            {
                                                tel.Add(rdrex[2].ToString().Trim());
                                            }
                                        }
                                        //Contact.InnerHtml = $"<div>Emails :<br/>{emails} </div><div>Téléphones : <br/>{tels} </div>";
                                    }
                                }
                                if (!string.IsNullOrEmpty(TM.Rows[0][16].ToString().Trim()))
                                {
                                    commandeee.CommandText = "select * from emails where DEPOSANT ='" + TM.Rows[0][16].ToString().Trim() + "' ";

                                    con.Close();
                                    con.Open();
                                    rdrex = commandeee.ExecuteReader();
                                    if (rdrex.HasRows)
                                    {
                                        while (rdrex.Read())
                                        {
                                            if (!emailM.Contains(rdrex[1].ToString().Trim()))
                                            {
                                                emailM.Add(rdrex[1].ToString().Trim());
                                            }

                                            if (!telM.Contains(rdrex[2].ToString().Trim()))
                                            {
                                                telM.Add(rdrex[2].ToString().Trim());
                                            }
                                        }
                                        //Contact.InnerHtml = $"<div>Emails :<br/>{emails} </div><div>Téléphones : <br/>{tels} </div>";
                                    }
                                }
                            }
                            

                            if (email.Count()!=0|| emailM.Count() != 0 || tel.Count() != 0 || telM.Count() != 0)
                            {
                                foreach(string fl in email)
                                {
                                    emails+="(D)"+fl+"-";
                                }
                                foreach (string fl in emailM)
                                {
                                    emails += "(M)" + fl + "-";
                                }
                                foreach (string fl in tel)
                                {
                                    tels += "(D)" + fl + "-";
                                }
                                foreach (string fl in telM)
                                {
                                    tels += "(M)" + fl + "-";
                                }



                                Contact.InnerHtml = $"<div>Emails :<br/>{emails} </div><div>Téléphones : <br/>{tels} </div>";
                            }
                            else
                            {
                                Contact.InnerHtml = "<div>Pas d'informations sur ce Déposant ou Mandataire a ce moment </div>";
                            }

                            
                            con.Close();



















                                ///end
















































                                //if (rdr.HasRows)
                                //{
                                //    rdr.Close();
                                //    //historiqueP.InnerHtml = "testhistoIF";
                                //    commande.CommandText = "select * from Marque_historique where NumeroTitre = @st";
                                //    commande.Parameters.AddWithValue("@st", Request["ST13"]);
                                //    var rdrs = commande.ExecuteReader();

                                //    if (!rdrs.HasRows)
                                //    {

                                //        historiqueP.InnerHtml = "";
                                //    }
                                //    else
                                //    {


                                //        DataTable tb = new DataTable();
                                //        tb.Load(rdrs);
                                //        string contenthis = "";
                                //        string strk = "";

                                //        contenthis += "<tr><th style='background-color: #92a8d1;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Nom Champ </th><th style='background-color: #92a8d1;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Ancient Valeur </th><th style='background-color: #92a8d1;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Nouvelle valeur </th><th style='background-color: #92a8d1;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Date Modification </th></tr>";


                                //        foreach (DataRow roxw in tb.Rows)
                                //        {
                                //            strk += $"<tr><td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{roxw[1]}</td><td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{roxw[2]}</td><td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{roxw[3]}</td><td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{roxw[4]}</td></tr>";
                                //        }

                                //        contenthis += strk;

                                //        historique.InnerHtml = $"<table style='border:1px solid #2D4D62;border-collapse:collapse;'><tbody>{contenthis}</tbody></table>";
                                //    }
                                //    rdrs.Close();
                                //    command.CommandText = "select * from Marques_Tm where ST13 = @st";

                                //    command.Parameters.Clear();
                                //    command.Parameters.AddWithValue("@st", Request["ST13"]);
                                //    rdr = command.ExecuteReader();

                                //    rdr.Read();
                                //    if (string.IsNullOrWhiteSpace(rdr[1]?.ToString()) && string.IsNullOrWhiteSpace(rdr[3]?.ToString()) && string.IsNullOrWhiteSpace(rdr[5]?.ToString()))
                                //    {
                                //        rdr.Close();
                                //        command.CommandText = "select * from Marques_Ompic where NumeroTitre = @st";
                                //        rdr = command.ExecuteReader();
                                //        if (rdr.HasRows)
                                //        {
                                //            //historiqueP.InnerHtml = "testhistoIFIF";
                                //            rdr.Read();
                                //            deposantt.InnerText = rdr[0]?.ToString();
                                //            dateDepot.InnerText = rdr[1]?.ToString().Split(' ').FirstOrDefault().Trim();
                                //            expiryDate.InnerText = rdr[2]?.ToString().Split(' ').FirstOrDefault().Trim();
                                //            deposantPays.InnerText = rdr[3]?.ToString();
                                //            string nice = rdr[5]?.ToString();
                                //            if (nice != null)
                                //            {
                                //                List<string> list = new List<string>();
                                //                var Nicearray = nice.Split(',');
                                //                foreach (var item in Nicearray)
                                //                {
                                //                    if (!list.Contains(item.Trim()))
                                //                    {
                                //                        list.Add(item.Trim());
                                //                    }
                                //                }
                                //                string str = "";
                                //                foreach (var item in list)
                                //                {
                                //                    str += item + ",";
                                //                }
                                //                if (!string.IsNullOrWhiteSpace(str))
                                //                {
                                //                    niceClassNumbers.InnerText = str.Remove(str.LastIndexOf(','));
                                //                }
                                //            }
                                //            string ps = rdr[6]?.ToString();
                                //            string contentPS = "";

                                //            contentPS += $"<tr><th width='15%' style='padding:15px 0px;color:#2D4D62;font-weight:bold;'>Toutes les classes</th><td width='85%' style='color:#2D4D62;padding:15px 0px;text-align:justify;'>{ps}</td></tr>";

                                //            Produits_Services.InnerHtml = $"<table><tbody>{contentPS}</tbody></table>";
                                //            applicationNumber.InnerText = rdr[7]?.ToString();
                                //            typeMarque.InnerText = rdr[8]?.ToString();
                                //            mandatairee.InnerText = rdr[9]?.ToString();
                                //            nomMarque.InnerText = rdr[10]?.ToString();
                                //            deposantAddress.InnerText = rdr[11]?.ToString();
                                //            statutMarque.InnerText = rdr[12]?.ToString();

                                //            numeroPublication.InnerText = rdr[15]?.ToString();



                                //            //elementVerbal.InnerText = rdr[10]?.ToString();
                                //            rdr.Close();


                                //            //}
                                //        }
                                //    }
                                //    else
                                //    {

                                //        //elementVerbal.InnerText = rdr[1]?.ToString();
                                //        nomMarque.InnerText = rdr[1]?.ToString();
                                //        applicationNumber.InnerText = rdr[2]?.ToString();
                                //        dateDepot.InnerText = rdr[3]?.ToString().Split(' ').FirstOrDefault().Trim();
                                //        //numRegistration.InnerText = rdr[4]?.ToString();

                                //        expiryDate.InnerText = rdr[5]?.ToString().Split(' ').FirstOrDefault().Trim();

                                //        if (!string.IsNullOrWhiteSpace(rdr[6]?.ToString()) && rdr[6]?.ToString() == "Combined")
                                //        {
                                //            typeMarque.InnerText = "Mixte";
                                //        }
                                //        else if (!string.IsNullOrWhiteSpace(rdr[6]?.ToString()) && rdr[6]?.ToString() == "Sound")
                                //        {
                                //            typeMarque.InnerText = "Sonore";
                                //        }
                                //        else if (!string.IsNullOrWhiteSpace(rdr[6]?.ToString()) && rdr[6]?.ToString() == "Other")
                                //        {
                                //            typeMarque.InnerText = "Autres";
                                //        }
                                //        else
                                //        {
                                //            typeMarque.InnerText = rdr[6]?.ToString();
                                //        }

                                //        if (!string.IsNullOrWhiteSpace(rdr[8]?.ToString()) && rdr[8]?.ToString() == "Application refused")
                                //        {
                                //            statutMarque.InnerText = "REJETEE";
                                //        }
                                //        else if (!string.IsNullOrWhiteSpace(rdr[8]?.ToString()) && rdr[8]?.ToString() == "Registration surrendered")
                                //        {
                                //            statutMarque.InnerText = "RENONCEE";
                                //        }
                                //        else if (!string.IsNullOrWhiteSpace(rdr[8]?.ToString()) && rdr[8]?.ToString() == "Application filed")
                                //        {
                                //            statutMarque.InnerText = "EN INSTANCE DE PUBLICATION";
                                //        }
                                //        else if (!string.IsNullOrWhiteSpace(rdr[8]?.ToString()) && rdr[8]?.ToString() == "Renewed")
                                //        {
                                //            statutMarque.InnerText = "RENOUVELEE";
                                //        }
                                //        else if (!string.IsNullOrWhiteSpace(rdr[8]?.ToString()) && rdr[8]?.ToString() == "Appeal pending")
                                //        {
                                //            statutMarque.InnerText = "EN POURSUITE DE PROCEDURE";
                                //        }
                                //        else if (!string.IsNullOrWhiteSpace(rdr[8]?.ToString()) && rdr[8]?.ToString() == "Application opposed")
                                //        {
                                //            statutMarque.InnerText = "OPPOSITION EN COURS";
                                //        }
                                //        else if (!string.IsNullOrWhiteSpace(rdr[8]?.ToString()) && rdr[8]?.ToString() == "Registration cancelled")
                                //        {
                                //            statutMarque.InnerText = "RADIEE";
                                //        }
                                //        else if (!string.IsNullOrWhiteSpace(rdr[8]?.ToString()) && rdr[8]?.ToString() == "Registered")
                                //        {
                                //            statutMarque.InnerText = "ENREGISTREE";
                                //        }
                                //        else if (!string.IsNullOrWhiteSpace(rdr[8]?.ToString()) && rdr[8]?.ToString() == "Application withdrawn")
                                //        {
                                //            statutMarque.InnerText = "RETIREE";
                                //        }
                                //        else if (!string.IsNullOrWhiteSpace(rdr[8]?.ToString()) && rdr[8]?.ToString() == "Application published")
                                //        {
                                //            statutMarque.InnerText = "PUBLIEE";
                                //        }
                                //        else if (!string.IsNullOrWhiteSpace(rdr[8]?.ToString()) && rdr[8]?.ToString() == "Expired")
                                //        {
                                //            statutMarque.InnerText = "EXPIREE";
                                //        }
                                //        else
                                //        {
                                //            statutMarque.InnerText = rdr[8]?.ToString();
                                //        }

                                //        var deposants = rdr[10]?.ToString().Split('\n');
                                //        if (deposants != null)
                                //        {
                                //            foreach (var deposant in deposants)
                                //            {
                                //                deposantt.InnerHtml += deposant + "<br/>";
                                //            }
                                //        }
                                //        var deposantsNationality = rdr[12]?.ToString().Split('\n');
                                //        if (deposantsNationality != null)
                                //        {
                                //            foreach (var depNationality in deposantsNationality)
                                //            {
                                //                deposantNationality.InnerHtml += depNationality + "<br/>";
                                //            }
                                //        }
                                //        var deposantsAddress = rdr[13]?.ToString().Split('\n');
                                //        if (deposantsAddress != null)
                                //        {
                                //            foreach (var depAddress in deposantsAddress)
                                //            {
                                //                deposantAddress.InnerHtml += depAddress + "<br/>";
                                //            }
                                //        }
                                //        var deposantsCity = rdr[14]?.ToString().Split('\n');
                                //        if (deposantsCity != null)
                                //        {
                                //            foreach (var depCity in deposantsCity)
                                //            {
                                //                deposantCity.InnerHtml += depCity + "<br/>";
                                //            }
                                //        }
                                //        var deposantsPays = rdr[15]?.ToString().Split('\n');
                                //        if (deposantsPays != null)
                                //        {
                                //            foreach (var depPays in deposantsPays)
                                //            {
                                //                deposantPays.InnerHtml += depPays + "<br/>";

                                //            }
                                //        }
                                //        mandatairee.InnerText = rdr[16]?.ToString();
                                //        mandataireNationality.InnerText = rdr[17]?.ToString();

                                //        mandataireAddress.InnerText = rdr[18]?.ToString();
                                //        mandataireCity.InnerText = rdr[19]?.ToString();
                                //        mandatairePays.InnerText = rdr[20]?.ToString();

                                //        numeroPublication.InnerText = rdr[21]?.ToString();

                                //        sectionPublication.InnerText = rdr[22]?.ToString();
                                //        datePublication.InnerText = rdr[23]?.ToString().Split(' ').FirstOrDefault().Trim();
                                //        niceClassNumbers.InnerText = rdr[9]?.ToString();
                                //        string ps = rdr[32]?.ToString();
                                //        ps = ps == null ? "" : ps;
                                //        var NiceClassarray = ps.Split('\n');

                                //        string contentPS = "";
                                //        foreach (var item in NiceClassarray)
                                //        {
                                //            contentPS += $"<tr><th width='10%' style='padding:15px 0px;color:#2D4D62;font-weight:bold;'>{item.Split(':').FirstOrDefault()}</th><td width='90%' style='color:#2D4D62;padding:15px 0px;text-align:justify;'>{item.Split(':').LastOrDefault()}</td></tr>";
                                //        }

                                //        Produits_Services.InnerHtml = $"<table><tbody>{contentPS}</tbody></table>";

                                //        string contentOpp = "";
                                //        if (string.IsNullOrWhiteSpace(rdr[24]?.ToString()))
                                //        {

                                //                Oppositionss.InnerHtml = "";
                                //        }

                                //            if (!string.IsNullOrWhiteSpace(rdr[24]?.ToString()))
                                //        {
                                //            var array = rdr[24]?.ToString().Split('\n');
                                //            if (array.Length > 0)
                                //            {
                                //                string str = "";
                                //                foreach (var item in array)
                                //                {
                                //                    str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item.Split(' ').FirstOrDefault().Trim()}</td>";
                                //                }
                                //                str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Opposition date </th>" + str + "</tr>";
                                //                contentOpp += str;
                                //            }
                                //        }
                                //        if (!string.IsNullOrWhiteSpace(rdr[25]?.ToString()))
                                //        {
                                //            var array = rdr[25]?.ToString().Split('\n');
                                //            if (array.Length > 0)
                                //            {
                                //                string str = "";
                                //                foreach (var item in array)
                                //                {
                                //                    str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item}</td>";
                                //                }
                                //                str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Droit anterieure opposition </th>" + str + "</tr>";
                                //                contentOpp += str;
                                //            }
                                //        }
                                //        if (!string.IsNullOrWhiteSpace(rdr[26]?.ToString()))
                                //        {
                                //            var array = rdr[26]?.ToString().Split('\n');
                                //            if (array.Length > 0)
                                //            {
                                //                string str = "";
                                //                foreach (var item in array)
                                //                {
                                //                    str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item}</td>";
                                //                }
                                //                str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Opposant </th>" + str + "</tr>";
                                //                contentOpp += str;
                                //            }
                                //        }
                                //        if (!string.IsNullOrWhiteSpace(rdr[27]?.ToString()))
                                //        {
                                //            var array = rdr[27]?.ToString().Split('\n');
                                //            if (array.Length > 0)
                                //            {
                                //                string str = "";
                                //                foreach (var item in array)
                                //                {
                                //                    str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item}</td>";
                                //                }
                                //                str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Opposant entité légale </th>" + str + "</tr>";
                                //                contentOpp += str;
                                //            }
                                //        }
                                //        if (!string.IsNullOrWhiteSpace(rdr[28]?.ToString()))
                                //        {
                                //            var array = rdr[28]?.ToString().Split('\n');
                                //            if (array.Length > 0)
                                //            {
                                //                string str = "";
                                //                foreach (var item in array)
                                //                {
                                //                    str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item}</td>";
                                //                }
                                //                str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Opposant nationalité code </th>" + str + "</tr>";
                                //                contentOpp += str;
                                //            }
                                //        }
                                //        if (!string.IsNullOrWhiteSpace(rdr[29]?.ToString()))
                                //        {
                                //            var array = rdr[29]?.ToString().Split('\n');
                                //            if (array.Length > 0)
                                //            {
                                //                string str = "";
                                //                foreach (var item in array)
                                //                {
                                //                    str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item}</td>";
                                //                }
                                //                str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Opposant adresse </th>" + str + "</tr>";
                                //                contentOpp += str;
                                //            }
                                //        }
                                //        if (!string.IsNullOrWhiteSpace(rdr[30]?.ToString()))
                                //        {
                                //            var array = rdr[30]?.ToString().Split('\n');
                                //            if (array.Length > 0)
                                //            {
                                //                string str = "";
                                //                foreach (var item in array)
                                //                {
                                //                    str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item}</td>";
                                //                }
                                //                str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Opposant ville </th>" + str + "</tr>";
                                //                contentOpp += str;
                                //            }
                                //        }
                                //        if (!string.IsNullOrWhiteSpace(rdr[31]?.ToString()))
                                //        {
                                //            var array = rdr[31]?.ToString().Split('\n');
                                //            if (array.Length > 0)
                                //            {
                                //                string str = "";
                                //                foreach (var item in array)
                                //                {
                                //                    str += $"<td style='padding:15px 5px;color:#2D4D62;border:1px solid #2D4D62;border-collapse:collapse;'>{item}</td>";
                                //                }
                                //                str = "<tr><th style='background-color: antiquewhite;padding:15px 5px;color:#2D4D62;font-weight:bold;border:1px solid #2D4D62;border-collapse:collapse;'>Opposant pays code </th>" + str + "</tr>";
                                //                contentOpp += str;
                                //            }
                                //        }
                                //        Oppositions.InnerHtml = $"<table style='border:1px solid #2D4D62;border-collapse:collapse;'><tbody>{contentOpp}</tbody></table>";
                                //        rdr.Close();
                                //    }
                                //}
                                //else
                                //{
                                //    //historiqueP.InnerHtml = "testhistoELSE";
                                //    command.CommandText = "select * from Marques_Ompic where NumeroTitre = @st";
                                //    rdr.Close();
                                //    rdr = command.ExecuteReader();
                                //    if (rdr.HasRows)
                                //    {
                                //        rdr.Read();
                                //        deposantt.InnerText = rdr[0]?.ToString();
                                //        dateDepot.InnerText = rdr[1]?.ToString().Split(' ').FirstOrDefault().Trim();
                                //        expiryDate.InnerText = rdr[2]?.ToString().Split(' ').FirstOrDefault().Trim();
                                //        deposantPays.InnerText = rdr[3]?.ToString();
                                //        string nice = rdr[5]?.ToString();
                                //        if (nice != null)
                                //        {
                                //            List<string> list = new List<string>();
                                //            var Nicearray = nice.Split(',');
                                //            foreach (var item in Nicearray)
                                //            {
                                //                if (!list.Contains(item.Trim()))
                                //                {
                                //                    list.Add(item.Trim());
                                //                }
                                //            }
                                //            string str = "";
                                //            foreach (var item in list)
                                //            {
                                //                str += item + ",";
                                //            }
                                //            if (!string.IsNullOrWhiteSpace(str))
                                //            {
                                //                niceClassNumbers.InnerText = str.Remove(str.LastIndexOf(','));
                                //            }
                                //        }
                                //        string ps = rdr[6]?.ToString();
                                //        string contentPS = "";

                                //        contentPS += $"<tr><th width='15%' style='padding:15px 0px;color:#2D4D62;font-weight:bold;'>Toutes les classes</th><td width='85%' style='color:#2D4D62;padding:15px 0px;text-align:justify;'>{ps}</td></tr>";


                                //        Produits_Services.InnerHtml = $"<table><tbody>{contentPS}</tbody></table>";
                                //        applicationNumber.InnerText = rdr[7]?.ToString();
                                //        typeMarque.InnerText = rdr[8]?.ToString();
                                //        mandatairee.InnerText = rdr[9]?.ToString();
                                //        nomMarque.InnerText = rdr[10]?.ToString();
                                //        deposantAddress.InnerText = rdr[11]?.ToString();
                                //        statutMarque.InnerText = rdr[12]?.ToString();

                                //        numeroPublication.InnerText = rdr[15]?.ToString();

                                //        //elementVerbal.InnerText = rdr[10]?.ToString();
                                //        rdr.Close();
                                //    }
                                //}
                                con.Close();
                           
                        }
                    }
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
        protected void btn_validation_Click(object sender, EventArgs e)
        {
            Response.Redirect("Validation.aspx");
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

        protected void btn_parametre_v2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Parametre.aspx");
        }

        protected void btn_parametre_v1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Parametre.aspx");
        }

        protected void Rech_bd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recherche Bd.aspx");
        }
        protected void btn_notification_Click(object sender, EventArgs e)
        {
            Response.Redirect("notification.aspx");
        }
       
        protected void btn_strongvalid_Click(object sender, EventArgs e)
        {
            Response.Redirect("strongvalid.aspx");
        }
        protected void Bnt_gestion_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestion_not.Aspx");
        }
        protected void Archive_Click(object sender, EventArgs e)
        {
            Response.Redirect("archive.aspx");
        }
    }
}