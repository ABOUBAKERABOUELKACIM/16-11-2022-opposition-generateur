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
        }
        #page-top > form{
            display:flex;
            overflow-x:hidden;
        }
        #page-top #container{
            width:100%;
            padding:0px 80px;
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
             background-color: #04a3ff;
             color: black;
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
            padding:0px 70px;
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
         .ompic_value{
            color:#2D4D62;
            font-weight:bold;
        }
        .tm_value{
            color:#292997;
            font-weight:bold;
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
        .hidden_search_field {
            display: none;
        }
        .hidden_search_fields {
            display: none;
        }

        .show {
            display: block;
        }
            .ace {
                box-shadow: inset 0 0 0 0 #54b3d6;
                color: red;
                
                transition: color .3s ease-in-out, box-shadow .3s ease-in-out;
            }

                .ace:hover {
                    box-shadow: inset 100px 0 0 0 #54b3d6;
                    color: dodgerblue;
                }
        
    </style>
  <link href="../Assets/Css/output.css" rel="stylesheet" />
</head>

<body>

      
    <div id="page-top">
 <form id="form1" runat="server">
        <div class="flex flex-row relative w-full max-h-full">

            <div id="sidebar" class="lg:block hidden fixed overflow-y-scroll z-10 left-0 top-0 w-[220px] h-screen bg-slate-900">
                <div class="fixed top-0 w-[220px] border-b border-r border-gray-700 bg-slate-900 z-10">
                    <div class="md:p-4 p-2">
                        <img id="profile_pic" runat="server" class="md:h-24 h-20 md:w-24 w-20 rounded-full mx-auto" src="../Assets/Img/picprofile.png" alt="profile picture" />
                    </div>
                    <div class="md:pb-4 pb-2 text-center">
                        <p runat="server" id="user_account__username" class="text-sm font-medium text-white" style="text-align: center;"><b>Nom d'utilisateur</b></p>
                        <p runat="server" id="user_account__role" class="text-sm font-medium text-white" style="text-align: center;">Role</p>
                    </div>
                </div>
                <ul class="md:p-2 md:mt-[170px] md:mb-[40px] mt-[129px] mb-[36px]">
                    <li>
                        <asp:Button CssClass="flex justify-between items-center w-full md:py-3 md:px-2 p-2 mt-1 rounded text-sm capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-700 focus:text-white sl-animated" runat="server" ID="Rech_bd" Text="Recherche Bd" OnClick="Rech_bd_Click" /></li>
                    <li>
                        <asp:Button CssClass="flex justify-between items-center w-full md:py-3 md:px-2 p-2 mt-1 rounded text-sm capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-700 focus:text-white sl-animated" runat="server" ID="Rech_marque" Text="Recherche marque" OnClick="Rech_marque_Click" /></li>
                    <li>
                        <asp:Button CssClass="flex justify-between items-center w-full md:py-3 md:px-2 p-2 mt-1 rounded text-sm capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-700 focus:text-white sl-animated" runat="server" ID="rechercheopps" Text="Recherche Oppositions" OnClick="Rech_Opps_Click" />
                    </li>
                    <li>
                        <asp:Button CssClass="flex justify-between items-center w-full md:py-3 md:px-2 p-2 mt-1 rounded text-sm capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-700 focus:text-white sl-animated" runat="server" ID="Rech_ompic" Text="Recherche ompic" OnClick="Rech_ompic_Click" /></li>
                    <li>
                        <button type="button" class="flex justify-between items-center w-full md:py-3 md:px-2 p-2 mt-1 rounded text-sm capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-700 focus:text-white sl-animated" onclick="displayToggle(subMenu4)" id="btn_valide_toggle">Validation et Notification <i class="fas fa-sort-down"></i></button>
                        <ul id="subMenu4" class="hidden py-2 md:pl-6 pl-4 pr-2 bg-gray-700">
                           <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="Button1" Text="Validation Phonetique" OnClick="btn_strongvalid_Click"  />
                            <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="Button2" Text="Validation des conflits" OnClick="btn_validation_Click" />
                            <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="Button3" Text="Notification des conflits" OnClick="btn_notification_Click" />
                            <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="Button4" Text="Gestion des conflits" OnClick="Bnt_gestion_Click" />

                        </ul>
                    </li>
                    <li>
                        <button type="button" class="flex justify-between items-center w-full md:py-3 md:px-2 p-2 mt-1 rounded text-sm capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-700 focus:text-white sl-animated" onclick="displayToggle(subMenu1)" id="btn_opp_toggle">Opposition<i class="fas fa-sort-down"></i></button>
                        <ul id="subMenu1" class="hidden py-2 md:pl-6 pl-4 pr-2 bg-gray-700">
                            <il class="dropdown dropdown_opp">
                                <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="btn_Formulaire" Text="Formulaire" OnClick="btn_Formulaire_Click" />
                                <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="btn_Formulaires" Text="Formulaires" OnClick="btn_Formulaires_Click" />
                                <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="btn_Resultat" Text="Resultat" OnClick="btn_Resultat_Click" />
                                <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="btn_Historique" Text="Historique" OnClick="btn_Historique_Click" />
                            </il>

                        </ul>
                    </li>
                    <li>
                        <button type="button" class="flex justify-between items-center w-full md:py-3 md:px-2 p-2 mt-1 rounded text-sm capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-700 focus:text-white sl-animated" onclick="displayToggle(subMenu2)" id="btn_appv1_toggle">App V1<i class="fas fa-sort-down"></i></button>
                        <ul id="subMenu2" class="hidden py-2 md:pl-6 pl-4 pr-2 bg-gray-700">
                            <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="btn_ajouter_alerte" Text="Ajouter alerte" OnClick="btn_ajouter_alerte_Click" />
                            <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="btn_generer_doc" Text="Génerer document" OnClick="btn_generer_doc_Click" />
                            <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="btn_parametre_v1" Text="Paramétre" OnClick="btn_parametre_v1_Click" />
                        </ul>
                    </li>
                    <li>
                        <button type="button" class="flex justify-between items-center w-full md:py-3 md:px-2 p-2 mt-1 rounded text-sm capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-700 focus:text-white sl-animated" onclick="displayToggle(subMenu3)" id="btn_appv2_toggle">App V2<i class="fas fa-sort-down"></i></button>
                        <ul id="subMenu3" class="hidden py-2 md:pl-6 pl-4 pr-2 bg-gray-700">
                            <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="btn_recherche_phonetique" Text="Recherche phonétique" OnClick="btn_recherche_phonetique_Click" />
                            <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="btn_parametre_v2" Text="Paramétre" OnClick="btn_parametre_v2_Click" />
                        </ul>
                    </li>

                    <li>
                        <asp:Button CssClass="fixed bottom-0 left-0 w-[220px] p-2 text-sm font-semibold bg-sky-500 hover:bg-sky-600 text-white sl-animated" runat="server" ID="btn_Deconnecter" Text="Déconnecter" OnClick="btn_Deconnecter_Click" /></li>
                     <li>
                        <asp:Button CssClass="flex justify-between items-center w-full md:py-3 md:px-2 p-2 mt-1 rounded text-sm capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-700 focus:text-white sl-animated" runat="server" ID="Button5" Text="Archive" OnClick="Archive_Click" /></li>
                </ul>

            </div>



            <!-- SIDEBAR END -->

                        <div class="box"></div>
        <div id="container">
                 <main class="lg:ml-[70px] w-full min-h-screen">
                <div class="header">
                    <img src="../Assets/Img/clarity_details-linedetails.png" style="display:block;margin:auto;"/>
                 
                </div><br />
                                                             <a href="#" id="print-btn">Generer pdf</a>    
                                                 <div id="detail" runat="server">
                                                     <div id="detailDocHeader">
                                                         <div style="display:flex;flex-grow:1;">
                                                         <img src="http://192.168.2.126/hostipp/Assets/Img/Ipplogo.png" width="150" height="150" />
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
                            
                             <h3 style="text-align: center" >FICHE DÉTAILLÉE DE LA MARQUE :</h3>
                                         </div>
                             <img src="" runat="server" id="detailImage" width="300" height="300"  />
                                     </div><div class="br1"><br /><br /><br /></div>
                                 <div class="info">Informations sur la marque</div>
                                 <div class="row">
                                     <div class="col-4 col-col-md-12">
                                         <div>
                                             <span class="label">Nom Marque :</span><br />
                                             <div id="nomMarque" class="value" runat="server"></div>
                                         </div><br />
                                         <div>
                                             <span class="label">Numero Marque :</span><br />
                                             <div id="applicationNumber" class="value" runat="server"></div>
                                         </div><br />
                                         <div>
                                             <span class="label">Date Depot :</span><br />
                                             <div id="dateDepot" class="value" runat="server"></div>
                                         </div><br />
                                         <div>
                                             <span class="label">Mandataire pays :</span><br />
                                             <div id="mandatairePays" class="value" runat="server"></div>
                                         </div><br />
                                         <div>
                                             <span class="label">Numero publication :</span><br />
                                             <div id="numeroPublication" class="value" runat="server"></div>
                                         </div><br />
                                         <div>
                                             <span class="label">Statut Marque :</span><br />
                                             <div id="statutMarque" class="value" runat="server"></div>
                                         </div><br />
                                        <div>
                                             <span class="label">Deposant nationalité :</span><br />
                                             <div id="deposantNationality" class="value" runat="server"></div>
                                         </div>
                                     </div>
                                     <div class="col-4 col-col-md-12">
                                         <div>
                                             <span class="label">Deposant :</span><br />
                                             <div id="deposantt" class="value" runat="server"></div>
                                         </div><br />
                                         <div>
                                             <span class="label">Deposant adresse :</span><br />
                                             <div id="deposantAddress" class="value" runat="server"></div>
                                         </div>
                                         <br />
                                         <div>
                                             <span class="label">Date Expiration :</span><br />
                                             <div id="expiryDate" class="value" runat="server"></div>
                                         </div><br />
                                         <div>
                                             <span class="label">Deposant pays :</span><br />
                                             <div id="deposantPays" class="value" runat="server"></div>
                                         </div><br />
                                         <div>
                                             <span class="label">Publication date :</span><br />
                                             <div id="datePublication" class="value" runat="server"></div>
                                         </div><br />
                                         <div>
                                             <span class="label">Publication section :</span><br />
                                             <div id="sectionPublication" class="value" runat="server"></div>
                                         </div><br />
                                         <div>
                                             <span class="label">Mandataire nationalité :</span><br />
                                             <div id="mandataireNationality" class="value" runat="server"></div>
                                         </div>
                                     </div>
                                     <div class="col-4 col-col-md-12">
                                         <div>
                                             <span class="label">Mandataire :</span><br />
                                             <div id="mandatairee" class="value" runat="server"></div>
                                         </div><br />
                                         <div>
                                             <span class="label">Mandataire adresse :</span><br />
                                             <div id="mandataireAddress" class="value" runat="server"></div>
                                         </div><br />
                                         
                                         <div>
                                             <span class="label">Type Marque :</span><br />
                                             <div id="typeMarque" class="value" runat="server"></div>
                                         </div><br />
                                         <div>
                                             <span class="label">Mandataire ville :</span><br />
                                             <div id="mandataireCity" class="value" runat="server"></div>
                                         </div><br />
                                         <div>
                                             <span class="label">Deposant ville :</span><br />
                                             <div id="deposantCity" class="value" runat="server"></div>
                                         </div><br />
                                         <div>
                                             <span class="label">Classe de nice :</span><br />
                                             <div id="niceClassNumbers" class="value" runat="server"></div>
                                         </div>
                                         <div>
                                             <a onclick="func()" class="ace">+  </a><span class="label">  Contact : </span><br />
                                             <div id="Contact" class="hidden_search_field value" runat="server"></div>
                                         </div>

                                     </div>

                                 </div><br /><div class="br2"><br /><br /></div>
                                 <p class="Oppositions" id="Oppositionss" runat="server"></p>
                                 <div id="Oppositions" runat="server"></div>
                                 <br />
                                 <p class="Oppositions" id="historiqueP" runat="server">Historique</p>
                                 <div id="historique" runat="server"></div>
                                 <br />
                                 <a onclick="funcs()" class="ace Produits_Services">+ </a><span class="label">  Produits et services : </span><br />
                                 
                                 <div id="Produits_Services" class="hidden_search_fields value" runat="server"></div>
                             </div>
                         </div></main>
          </div>               
            
            </div>
          </form>
     </div>
                      <script>
                          function func()
                          {
                              var hidden_search_field = Array.from(document.getElementsByClassName("hidden_search_field"));
                              hidden_search_field.forEach((item) => {
                                  item.classList.toggle("show");
                              })
                              
                          }
                          function funcs()
                          {
                              var hidden_search_field = Array.from(document.getElementsByClassName("hidden_search_fields"));
                              hidden_search_field.forEach((item) => {
                                  item.classList.toggle("show");
                              })
                          }
                          $(document).ready(function () {
                              $('#print-btn').click(function () {
                                  $('#detail').printThis({
                                      importCSS: true,
                                      importStyle: true,
                                      loadCSS: "http://192.168.2.126/hostipp/Assets/Css/DetailsStyle.css",
                                      copyTagClasses: true,
                                      printDelay: 2000,
                                      canvas: true,
                                      header: false,
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
            <script src="../assets/js/app.js"></script>


</body>
</html>
