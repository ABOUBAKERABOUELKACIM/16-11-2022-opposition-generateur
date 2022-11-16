<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Opposition_Generateur.Views.Details" %>

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
<script src="../Scripts/jquery-3.6.0.js"></script>
    <script src="../Scripts/printThis.js"></script>
<link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous"/>
<title>Details</title>
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
            overflow-x:hidden;
        }
        #page-top #container{
            width:100%;
            padding:0px 4px;
            border:2px solid whitesmoke;
            position:relative;
        }
        #page-top #container > .header > h2{
            font-family: 'Poppins', sans-serif;
            text-align:center;
            color:darkslategray;
            background-color:#EFEEF0;
            padding:10px 0px;
            border-radius:10px;
            font-size:14px;
        }
             .header img{
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
         
        .label{
            color:#2D4D62;
        }
        .value{
            font-weight:bold;
            color:#2D4D62;
        }
        #detailImage{
            object-fit: contain;
            display:block;
            margin:auto;
            vertical-align: middle;
        }
        .info{
             font-size: 19px;
             border-bottom: 2px solid #E0E0E0;
             font-weight: bold;
             color: #2D4D62;
             opacity: 0.8;
             margin-bottom: 20px;
        } 
        .Produits_Services , .Oppositions{
                         font-size: 19px;
             border-bottom: 2px solid #E0E0E0;
             font-weight: bold;
             color: #2D4D62;
             opacity: 0.8;
             margin-bottom: 20px;
        }
        #print-btn{
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
    width:130px;
        }
        @media print {
  @page {
    margin: 20px 20px;
  }
}
        .br2{
            display:none;
        }
        #detailDocHeader{
            border-bottom:2px solid black;
            margin:0px 10px;
            display:none;
        }
      
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
        #page-top > form #container{
            width:100%;
            padding:0px 4px;
            border:2px solid whitesmoke;
            height:100vh;
        }
        #page-top > form #container > .header > h2{
            font-family: 'Poppins', sans-serif;
            text-align:center;
            color:darkslategray;
            background-color:#EFEEF0;
            padding:10px 0px;
            border-radius:10px;
            font-size:14px;
        }

        .header img{
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
         #alerte{
             display:flex;
             flex-direction:column;
         }
         #alerte > div > label{
             font-size:13px;
         }
         #Aj_alerte{
            outline: 0px;
    border: 0px;
    border-radius: 8px;
    background-color: #e94444;
    color: white;
    border-radius: 5px;
    font-family: 'Poppins';
    padding: 5px 5px;
    transition: 0.3s ease-in-out;
    width: 100px;
    font-size: 14px;
         }
         #Aj_alerte:hover{
             background-color: white;
    color: red;
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
        @media (max-width:600px){
            #page-top {
                display:block;
            }

                   #page-top > form #container > .header img{
                    width:70px;
                    height:70px;
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
                    <img src="../Assets/Img/clarity_details-linedetails.png" style="display:block;margin:auto;"/>
                    <h2 class="container-header-title">Details</h2>
                </div><br />
                                                             <a href="#" id="print-btn">Generer pdf</a>    
                                                 <div id="detail" runat="server">
                                                     <div id="detailDocHeader">
                                                         <div style="display:flex;flex-grow:1;">
                                                         <img src="../Assets/Img/Ipplogo.png" width="150" height="150" />
                                                             <div style="margin-top:20px;">
                                                                  <h5 style="font-weight:bold;letter-spacing:2px;margin:0px;">IP PERFORMANCE</h5>
                                                                  <h7 style="color:red;letter-spacing:2px;font-size:12px;">LA PERFORMANCE PAR L'IMMATERIÉL</h7><br />
                                                                  <br /><div style="width:230px;font-weight:500;font-size:12px;">
                                                                  <p>236 BD ABDELMOUMEN ESCALIER B2
                                                                     ETAGE 1 APPARTEMENT 4 ANGLE RUE
                                                                     PASQUIER CASABLANCA</p></div>
                                                             </div>
                                                         </div>
                                                         <div style="font-weight:500;font-size:12px;margin-top:-5px;flex:2;">
                                                             <br /><br /><br /><br /><br />
                                                             <p>Tel. No.: (+212) 05 20 30 55 50<br />
                                                                Email: Contact@ipperformance.ma<br />
                                                                Siteweb: www.ipperformance.ma</p>
                                                         </div>
                                                     </div>
                             <div style="padding: 10px 10px;">
                                 <div>
                                     <div>
                            
                             <h3 style="text-align: center" >FICHE DETAILS :</h3>
                                         </div>
                             <img src="" runat="server" id="detailImage" width="300" height="300"  />
                                     </div><div class="br1"><br /><br /><br /></div>
                                 <div class="info">Informations sur la marque</div>
                                 <div class="row">
                                     <div class="col-4 col-col-md-12">
                                         <div>
                                             <span class="label">Nom Marque :</span><br />
                                             <span id="nomMarque" class="value" runat="server"></span>
                                         </div><br />
                                         <div>
                                             <span class="label">Numero Marque :</span><br />
                                             <span id="applicationNumber" class="value" runat="server"></span>
                                         </div><br />
                                         <div>
                                             <span class="label">Date Depot :</span><br />
                                             <span id="dateDepot" class="value" runat="server"></span>
                                         </div><br />
                                         <div>
                                             <span class="label">Mandataire pays :</span><br />
                                             <span id="mandatairePays" class="value" runat="server"></span>
                                         </div><br />
                                         <div>
                                             <span class="label">Numero publication :</span><br />
                                             <span id="numeroPublication" class="value" runat="server"></span>
                                         </div><br />
                                         <div>
                                             <span class="label">Statut Marque :</span><br />
                                             <span id="statutMarque" class="value" runat="server"></span>
                                         </div><br />
                                        <div>
                                             <span class="label">Deposant nationalité :</span><br />
                                             <span id="deposantNationality" class="value" runat="server"></span>
                                         </div>
                                     </div>
                                     <div class="col-4 col-col-md-12">
                                         <div>
                                             <span class="label">Deposant :</span><br />
                                             <span id="deposantt" class="value" runat="server"></span>
                                         </div><br />
                                         <div>
                                             <span class="label">Deposant adresse :</span><br />
                                             <span id="deposantAddress" class="value" runat="server"></span>
                                         </div>
                                         <br />
                                         <div>
                                             <span class="label">Date Expiration :</span><br />
                                             <span id="expiryDate" class="value" runat="server"></span>
                                         </div><br />
                                         <div>
                                             <span class="label">Deposant pays :</span><br />
                                             <span id="deposantPays" class="value" runat="server"></span>
                                         </div><br />
                                         <div>
                                             <span class="label">Publication date :</span><br />
                                             <span id="datePublication" class="value" runat="server"></span>
                                         </div><br />
                                         <div>
                                             <span class="label">Publication section :</span><br />
                                             <span id="sectionPublication" class="value" runat="server"></span>
                                         </div><br />
                                         <div>
                                             <span class="label">Mandataire nationalité :</span><br />
                                             <span id="mandataireNationality" class="value" runat="server"></span>
                                         </div>
                                     </div>
                                     <div class="col-4 col-col-md-12">
                                         <div>
                                             <span class="label">Mandataire :</span><br />
                                             <span id="mandatairee" class="value" runat="server"></span>
                                         </div><br />
                                         <div>
                                             <span class="label">Mandataire adresse :</span><br />
                                             <span id="mandataireAddress" class="value" runat="server"></span>
                                         </div><br />
                                         
                                         <div>
                                             <span class="label">Type Marque :</span><br />
                                             <span id="typeMarque" class="value" runat="server"></span>
                                         </div><br />
                                         <div>
                                             <span class="label">Mandataire ville :</span><br />
                                             <span id="mandataireCity" class="value" runat="server"></span>
                                         </div><br />
                                         <div>
                                             <span class="label">Deposant ville :</span><br />
                                             <span id="deposantCity" class="value" runat="server"></span>
                                         </div><br />
                                         <div>
                                             <span class="label">Classe de nice :</span><br />
                                             <span id="niceClassNumbers" class="value" runat="server"></span>
                                         </div>

                                     </div>

                                 </div><br /><div class="br2"><br /><br /></div>
                                 <p class="Oppositions" id="Oppositionss" runat="server">Oppositions</p>
                                 <div id="Oppositions" runat="server"></div>
                                 <br />
                                 <p class="Produits_Services">Produits et services</p>
                                 <div id="Produits_Services" runat="server"></div>
                             </div>
                         </div>
          </div>               
          </form>
     </div>
                      <script>
                          $(document).ready(function () {
                              $('#print-btn').click(function () {
                                  $('#detail').printThis({
                                      importCSS: true,
                                      importStyle: true,
                                      loadCSS: "../Assets/Css/DetailsStyle.css",
                                      copyTagClasses: true,
                                      printDelay: 2000,
                                      canvas: true,
                                      header: null,
                                      footer: null,
                                      pageTitle: "",
                                  });
                              });
                          });

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
