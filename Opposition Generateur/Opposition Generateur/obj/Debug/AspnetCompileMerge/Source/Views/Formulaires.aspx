<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Formulaires.aspx.cs" Inherits="Opposition_Generateur.Formulaires" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width" />
<link rel="preconnect" href="https://fonts.googleapis.com"/>
<link rel="preconnect" href="https://fonts.gstatic.com" />
<link href="https://fonts.googleapis.com/css2?family=Roboto:ital,wght@1,300&display=swap" rel="stylesheet" />
<link href="https://fonts.googleapis.com/css2?family=Poppins&family=Roboto:ital,wght@1,300&display=swap" rel="stylesheet" />
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.min.js" integrity="sha384-QJHtvGhmr9XOIpI6YVutG+2QOK9T+ZnN4kzFN1RtK3zEFEIsxhlmWl5/YESvpZ13" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
<link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous"/>
<title>Formulaires</title>
     <style>
        *{
            margin:0px;
            padding:0px;
            box-sizing:border-box;
        }

        #page-top{
            width:100%;
        }
        #page-top > form{
            display:flex;
        }
        #page-top  #container{
            padding:0px 4px;
            border:2px solid whitesmoke;
            width:100%;
            height:100vh;
        }
        #page-top  #container > .header > h2{
            font-family: 'Poppins', sans-serif;
            text-align:center;
            color:darkslategray;
            background-color:#EFEEF0;
            padding:10px 0px;
            border-radius:10px;
            font-size:14px;
        }
        .GenerateDocs{
            border-style: none;
             border-color: inherit;
             border-width: 0px;
             outline: 0px;
             border-radius: 8px;
             background-color: #e94444;
             color: white;
             font-family: 'Poppins';
             padding: 5px 5px;
             transition:0.3s ease-in-out;
             font-size:14px;   
            width:200px;
            display:inline-block;
            margin:0px 4px;
        
        }
         .GenerateDocs:hover{
          background-color: indianred;
             color:black;
         }
         
            .Resultat{
     border-style: none;
             border-color: inherit;
             border-width: 0px;
             outline: 0px;
             border-radius: 8px;
             background-color: #e94444;
             color: white;
             font-family: 'Poppins';
             padding: 5px 5px;
             transition:0.3s ease-in-out;
             font-size:14px;
            width:100px;
            
            font-size:14px;
        }
         .Resultat:hover{
           background-color: indianred;
             color:black;
         }
         
          .header  img{
                    width:90px;
                    height:90px;
          }
                       #user_account img{
                 outline:3px solid white;
                 outline-offset:3px;
             }
         #user_account{
             background-color:#1E5095;
             width:160px;
             height:100vh;
             position:fixed;
             z-index:7;
         }
         .box{
             min-width:160px;
             height:100vh;
         }
         #profile_pic{
            border-radius:50%;
            display:block;
            margin:auto;
         }
         #user_account  p{
             color:white;
             font-family: 'Poppins';
         }
         #user_account__username{
             font-size:15px;
         }
         #user_account__role{
               font-size:12px;
         }
         #user_account .btn_menu:hover{
             background-color:rgb(219,221,223 , 0.4);
         }
         #user_account .btn_dropdownitem:hover{
             background-color:rgb(219,221,223 , 0.4);
         }
         .btn_menu{
             width:100%;
             border:0px;
             outline:0px;
             padding:5px 0px;
             text-align:left;
             background-color: #ffffff1c;
             margin-bottom:5px;
             border-radius: 5px;
             color:white;
             font-size:12px;
             padding-left:13px;
             font-family: 'Poppins';
             display: flex;
             justify-content: space-between;
         }
            .btn_dropdownitem {
                width: 100%;
                border: 0px;
                outline: 0px;
                padding: 5px 0px;
                text-align: left;
                background-color: #ffffff1c;
                margin-bottom: 5px;
                border-radius: 5px;
                color: white;
                font-size: 10px;
                padding-left: 30px;
                font-family: 'Poppins';
            }
         .fa-sort-down{
             padding-right:5px;
         }
         .dropdown{
             height:0px;
             overflow-y:hidden;
         }
         .show{
             height:auto;
         }
                  #btn_Deconnecter {
             width:100%;
             border:0px;
             outline:0px;
             padding:5px 0px;
             text-align:left;
             background-color: #ffffff1c;
             border-radius: 5px;
             color:white;
             font-size:12px;
             padding-left:13px;
             font-family: 'Poppins';
             position:absolute;
             bottom:0;
             left:0;
         }
         @media (max-width:600px) {
             #page-top {
                 display: block;
             }
                     #page-top  #container > .header img {
                         width: 70px;
                         height: 70px;
                     }
                    
       
                     .Resultat{
                      width:165px;
                     }
         }
              @media (max-width:459px) {              
                        .GenerateDocs{
                    display:block;
                  }
                     .Resultat{

                      display:block;
                      margin-top:10px;
                      margin-left:0px;
                     }
            }
         </style>
    <link rel="stylesheet" href="../Assets/Css/Sidemenustyle.css" />
</head>
<body>
    <div id="page-top">
            <form id="form1" runat="server">
        <div id="user_account"><br />
            <img src="../Assets/Img/676-6764065_default-image-png.png" runat="server" id="profile_pic" width="50" height="50" />
            <br />
            <p runat="server" id="user_account__username" style="text-align:center;"><b>Nom d'utilisateur</b></p>
            <p runat="server" id="user_account__role" style="text-align:center;">Role</p><br />

             <asp:button CssClass="btn_menu" runat="server" Id="Rech_bd" Text="Recherche Bd" OnClick="Rech_bd_Click" />
             <asp:button CssClass="btn_menu" runat="server" Id="Rech_marque" Text="Recherche marque" OnClick="Rech_marque_Click" />
             <asp:button CssClass="btn_menu" runat="server" Id="Rech_ompic" Text="Recherche ompic" OnClick="Rech_ompic_Click" />
             <button type="button" class="btn_menu" id="btn_opp_toggle">Opposition<i class="fas fa-sort-down"></i></button>
            <div class="dropdown dropdown_opp">
             <asp:button CssClass="btn_dropdownitem" runat="server" Id="btn_Formulaire" Text="Formulaire" OnClick="btn_Formulaire_Click" />
             <asp:button CssClass="btn_dropdownitem" runat="server" Id="btn_Formulaires"  Text="Formulaires" OnClick="btn_Formulaires_Click" />
             <asp:button CssClass="btn_dropdownitem" runat="server" Id="btn_Resultat" Text="Resultat" OnClick="btn_Resultat_Click" />
             <asp:button CssClass="btn_dropdownitem" runat="server" Id="btn_Historique" Text="Historique" OnClick="btn_Historique_Click" />
             </div>
             <button type="button" class="btn_menu" id="btn_appv1_toggle">App V1<i class="fas fa-sort-down"></i></button>
            <div class="dropdown dropdown_appv1">
             <asp:button CssClass="btn_dropdownitem" runat="server" Id="btn_ajouter_alerte" Text="Ajouter alerte" OnClick="btn_ajouter_alerte_Click" />
             <asp:button CssClass="btn_dropdownitem" runat="server" Id="btn_generer_doc"  Text="Génerer document" OnClick="btn_generer_doc_Click" />
             <asp:button CssClass="btn_dropdownitem" runat="server" Id="btn_parametre_v1"  Text="Paramétre" OnClick="btn_parametre_v1_Click" />
             </div>

                         <button type="button" class="btn_menu" id="btn_appv2_toggle">App V2<i class="fas fa-sort-down"></i></button>
             <div class="dropdown dropdown_appv2">
             <asp:button CssClass="btn_dropdownitem" runat="server" Id="btn_recherche_phonetique" Text="Recherche phonétique" OnClick="btn_recherche_phonetique_Click" />
                 <asp:button CssClass="btn_dropdownitem" runat="server" Id="btn_parametre_v2"  Text="Paramétre" OnClick="btn_parametre_v2_Click" />
             </div>

                         <asp:button CssClass="btn_menu"  runat="server" Id="btn_Deconnecter" Text="Déconnecter" OnClick="btn_Deconnecter_Click" />
        </div><div class="box"></div>
        <div id="container">
                 
                <div class="header">
                    <img src="../Assets/Img/clarity_form-linelogo.png" style="display:block;margin:auto;"/>
                    <img src="../Assets/Img/cropped-logo-2.png" style="position: absolute; top: 0px; right: 0px;width:250px;height:80px"/>
                    <h2 class="container-header-title">Formulaires</h2>
                </div><br />
                          <asp:GridView ShowHeaderWhenEmpty="True" OnRowDeleting="GridView1_RowDeleting" class="table container-fluid" ID="GridView1" runat="server" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" ForeColor="Black" Font-Names="Arial" Font-Size="8pt" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                    
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    
                    <Columns>
                        <asp:BoundField DataField="ID_form" HeaderText="ID"></asp:BoundField>
                        <asp:BoundField HeaderText="N° Marque anterieure" DataField="N_depot_marque_anterieure" />
                        <asp:BoundField HeaderText="Nature marque" DataField="Nature_marque_anterieure" />
                        <asp:BoundField HeaderText="N° Marque contester" DataField="N_depot_marque_contester" />
                        <asp:BoundField HeaderText="Nature marque" DataField="Nature_marque_contester" />
                        <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn-danger border-0 py-2 px-2 rounded-2" ShowDeleteButton="true" DeleteText="Supprimer" HeaderText="Action" >
                        <ControlStyle CssClass="btn-danger border-0 py-2 px-2 rounded-2"></ControlStyle>
                        </asp:CommandField>
                    </Columns>
                    
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" ForeColor="White" Font-Size="10pt" Font-Bold="True" Font-Names="Arial" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView><br />
        <div style="margin-bottom:5px;">
                <asp:Button OnClick="Button1_Click" CssClass="GenerateDocs" ID="Button1" runat="server" Text="Générer les documents" /><asp:Button OnClick="Button2_Click" CssClass="Resultat" ID="Button2" runat="server" Text="Resultat" />
    </div>
           
            </div>
                 </form>
        </div>
                          <script>
                              var btn_opp_toggle = document.getElementById("btn_opp_toggle");
                              var dropdown_opp = document.getElementsByClassName("dropdown_opp")[0];
                              btn_opp_toggle.addEventListener('click', function () {
                                  dropdown_opp.classList.toggle("show");
                              });
                              var btn_appv1_toggle = document.getElementById("btn_appv1_toggle");
                              var dropdown_appv1 = document.getElementsByClassName("dropdown_appv1")[0];
                              btn_appv1_toggle.addEventListener('click', function () {
                                  dropdown_appv1.classList.toggle("show");
                              });
                              var btn_appv2_toggle = document.getElementById("btn_appv2_toggle");
                              var dropdown_appv2 = document.getElementsByClassName("dropdown_appv2")[0];
                              btn_appv2_toggle.addEventListener('click', function () {
                                  dropdown_appv2.classList.toggle("show");
                              });
                          </script>
</body>
</html>
