

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resultat.aspx.cs" Inherits="Opposition_Generateur.Resultat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto:ital,wght@1,300&display=swap" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins&family=Roboto:ital,wght@1,300&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.min.js" integrity="sha384-QJHtvGhmr9XOIpI6YVutG+2QOK9T+ZnN4kzFN1RtK3zEFEIsxhlmWl5/YESvpZ13" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet" />


    <title>IP Platform</title>

    <script src="https://kit.fontawesome.com/272eed3989.js" crossorigin="anonymous"></script>



    <style>



        * {
            margin: 0px;
            padding: 0px;
            box-sizing: border-box;
        }
         .cent
    {
        height: 50px;
        width: 50px;
        background-color: black;      
    }
        #page-top {
            width: 100%;
        }

            #page-top > form {
                display: flex;
                overflow-x: hidden;
            }

            #page-top #container {
                width: 100%;
                padding: 0px 4px;
                border: 2px solid whitesmoke;
                position: relative;
                height: 100vh;
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
            #Arguments{          
                position:absolute;
                top:-1900px;
                left:190px;
                right:0px;
                background-color:white;
                transition:cubic-bezier(0.05, 0.82, 1, 0.24);    
                padding:0px 80px;
            }
           .close > .fas{
                cursor : pointer;
                font-size:18px;
                color:rgb(26, 52, 77 , 0.90);
            }
           .close > .fas:hover{
                color:black;
            }
            #Download{
                cursor : pointer;
                text-decoration:none;
            }
             .header img{
                    width:90px;
                    height:90px;
             }
           #container p{
            line-height:0.4;
           }h6{
                padding:3px 4px;
                border-radius:5px;
                margin-bottom:10px;
            }
            #AntDropDownList , #ContDropDownList{
                background-color:rgb(48, 72, 94, 0.70);
                color:white;
                outline:0px;
                border:0px;
                padding:0px 5px;
                cursor:pointer;
                border-radius:2px;
                transition:0.3s ease-in-out;
                font-size:12px;
            }
             #ContDropDownList:hover{
                background-color:rgb(48, 72, 94);
             }
               #AntDropDownList:hover{
                background-color:rgb(48, 72, 94);
             }
         .classe_text {
             width: 100%;
             opacity:0;
             height:0px;
             max-height: 100px;
             overflow: auto;
             border: 1px solid rgb(48, 72, 94, 0.70);
             border-radius: 2px;
             padding-top: 5px;
             padding-left: 5px;
             transition:0.3s ease-in-out;
             font-size:10px;
         }

                .classe_text::-webkit-scrollbar {
                  width: 5px;
                }
                .classe_text::-webkit-scrollbar-track {
                  border-radius: 10px;
                  background-color:rgb(48, 72, 94, 0.70);
                }

              
                .classe_text::-webkit-scrollbar-thumb {
                  background: #535353; 
                  border-radius: 10px;
                }

                
                .classe_text::-webkit-scrollbar-thumb:hover {
                  background: black; 
                }

                                #Arguments::-webkit-scrollbar {
                  width: 5px;
                }
                #Arguments::-webkit-scrollbar-track {
                  border-radius: 10px;
                  background-color:rgb(48, 72, 94, 0.50);
                }

              
                #Arguments::-webkit-scrollbar-thumb {
                  background: rgb(83 ,83 ,83 , 0.85);
                  border-radius: 10px;
                }

                
                #Arguments::-webkit-scrollbar-thumb:hover {
                   background: rgb(83 ,83 ,83 , 0.90);
                }
                #Btn_Toggle_Ant{
                    display:flex;
                    width:100%;
                    background-color:#2F4551;
                    color:white;
                    justify-content:space-between;
                    border:0px;
                    outline:0px;
                    padding:2px 5px;
                    border-radius:3px;
                    font-size:13px;
                }
                                #Btn_Toggle_Cont{
                    display:flex;
                    width:100%;
                    background-color:#2F4551;
                    color:white;
                    justify-content:space-between;
                    border:0px;
                    outline:0px;
                    padding:2px 5px;
                    border-radius:3px;
                   font-size:13px;
                }
                .Show{
                    opacity:1;
                    height:80px;
                    
                }
                .fa-sort-down{
                   color:white;
                }
                .row1 b{
                    font-size:12px;
                }
                .row1 span{
                    font-size:11px;
                }

                .btn-togl{
                    border:0px;
                    outline:0px;
                    display:block;
                    border-radius:5px;
                    color:white;
                    background-color:#2F4551;
                    font-size:12px;
                    text-align:left;
                    padding:0px 2px;
                    width:100%;
                    display:flex;
                    justify-content:space-between;
                    align-items:center;
                    padding:2px 5px;
                }
                .box > label{
                    font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
                    font-size:10px;
                }
                .box  > textarea{
                    display:block;
                    width:100%;
                }
                .fa-minus{
                    color:white;
                }
                #identite_box{
                    height:0px;
                    overflow:hidden;
                    transition:0.3s ease-in-out;
                }
                #inclusion_box{
                    height:0px;
                    overflow:hidden;
                    transition:0.3s ease-in-out;
                }
                #complementarite_box{
                    height:0px;
                    overflow:hidden;
                    transition:0.3s ease-in-out;
                }
                #comparaison_box{
                    height:0px;
                    overflow:hidden;
                    transition:0.3s ease-in-out;
                }
                #appreciation_box{
                    height:0px;
                    overflow:hidden;
                    transition:0.3s ease-in-out;
                }
                #args{
                    display:flex;
                    flex-direction:column;
                    margin-bottom:10px;
                }
                #args .row{
                    margin:0px;
                    padding:0px;
                    margin-bottom:10px;
                }

                                    .download{
                                        background-color:forestgreen;
                                        color:white;
                                        font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
                                        font-size:12px;
                                        border:0px;
                                        outline:0px;
                                        padding:5px 5px;
                                        border-radius:5px;
                                        transition:0.3s ease-in-out;
                                        margin-bottom:10px;
                                    }
                                     .download:hover {
                                         background-color: #1c7e1c;
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
         .boxx{
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
         @media (max-width:550px) {
             #page-top {
                 display: block;
             }
                     #page-top  #container > .header img {
                         width: 70px;
                         height: 70px;
                     }
             #Arguments {
                 padding:0px 3px;
             }
         }

         </style>
    <link href="../assets/css/output.css" rel="stylesheet"/>
   
</head>
<body>
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
                    <li>
                </ul>

            </div>

                    
        <main class="lg:ml-[220px] w-full min-h-screen ">
                 
               <header class="bg-slate-900">
                    <div class="flex justify-between items-center md:px-8 px-4 border-b border-gray-700">
                         <a href="home.aspx" class="py-4 md:text-2xl text-sm font-bold capitalize text-white"><i class="fa-solid fa-arrow-left md:text-lg text-sm md:pr-4 pr-2"></i>Formulaires</a>
                         <a href="home.aspx" class="flex items-center">
                             <img src="../Assets/Img/IP_LOGO_FULL.png" alt="logo" class="md:h-8 h-6"/>
                         </a>
                       <%--  <button onclick="displayToggle(sidebar)" class="lg:hidden block md:text-2xl text-xl text-white">
                              <i onclick="displayToggle(openSidebar); displayToggle(closeSidebar)" id="openSidebar" class="fa-solid fa-bars"></i>
                              <i onclick="displayToggle(openSidebar); displayToggle(closeSidebar)" id="closeSidebar" class="hidden fa-solid fa-xmark"></i>
                         </button>--%>
                    </div>
               </header><br />

                                  <asp:GridView ShowHeaderWhenEmpty="True" class="table container-fluid" ID="GridView1" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#0EA5E9" Font-Names="Arial" Font-Size="8pt" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    
                    <Columns>
                        <asp:BoundField DataField="ID_form" HeaderText="ID" />
                        <asp:BoundField HeaderText="N° Marque anterieure" DataField="N_depot_marque_anterieure" />
                        <asp:BoundField HeaderText="Nom marque" DataField="Nom_marque_anterieure" />
                        <asp:BoundField HeaderText="Deposant" DataField="Deposant_marque_anterieure" />
                        <asp:BoundField HeaderText="Nature marque" DataField="Nature_marque_anterieure" />

                        <asp:BoundField HeaderText="N° Marque contester" DataField="N_depot_marque_contester" />
                        <asp:BoundField HeaderText="Nom marque" DataField="Nom_marque_contester" />
                        <asp:BoundField HeaderText="Deposant" DataField="Deposant_marque_contester" />
                        <asp:BoundField HeaderText="Nature marque" DataField="Nature_marque_contester" />

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button ID="ShowArgs" CssClass="btn-success bg-green-500 py-2 px-3" runat="server"  Text="Traiter" OnClick="ShowArgs" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    
                    <HeaderStyle Font-Size="10pt" Font-Names="Arial" />
                </asp:GridView><br />
             
            </main> </div>
         <div   >
        <div  runat="server" id="Arguments" >
            <div class="close" style="display:flex;justify-content:end; border-radius:5px;background-color:#f1f1f1;width:100%;margin-bottom:5px;margin-top:5px;padding:3px 0px;">
                <i class="fas fa-times" onclick="HideArgs()" style="margin-right:5px;"></i>
            </div>

             <div class="row row1">
                 <div class="col-12 col-md-6 ">
                 <h6 style="color:white;background-color:#2F4551;" >Marque anterieure</h6>
                 <img class="img" runat="server" id="img_ant" src="." alt="marque anterieure" height="100" style="margin-bottom:5px;" />
                     <p><b>Desposant :</b> <span runat="server" id="ant_deposant">Desposant</span></p>
                     <p><b>Date depot :</b> <span runat="server" id="ant_date_depot">Date depot</span></p>
                     <p><b>Date expiration :</b> <span runat="server" id="ant_date_exp">Date expiration</span></p>
                     <p><b>Classe nice :</b> <asp:DropDownList ID="AntDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="AntDropDownList_SelectedIndexChanged"></asp:DropDownList> </p>
                      <button type="button" id="Btn_Toggle_Ant"><b>Produits et services :</b> <i class="fas fa-sort-down"></i></button>
                     <div class="classe_text" runat="server" id="Ant_classe_text"></div>
                     </div>
                     <div class="col-12 col-md-6">
                 <h6 style="color:white;background-color:#2F4551;" >Marque contester</h6>
                 <img class="img" runat="server" id="img_cont" src="." alt="marque anterieure" height="100" style="margin-bottom:5px;"/>
                     <p><b>Desposant :</b> <span runat="server" id="cont_deposant">Desposant</span></p>
                     <p><b>Date depot :</b> <span runat="server" id="cont_date_depot">Date depot</span></p>
                     <p><b>Date expiration :</b> <span runat="server" id="cont_date_exp">Date expiration</span></p>
                     <p><b>Classe nice :</b> <asp:DropDownList ID="ContDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ContDropDownList_SelectedIndexChanged"></asp:DropDownList> </p>
                      <button type="button" id="Btn_Toggle_Cont" ><b>Produits et services : </b><i class="fas fa-sort-down"></i></button>
                     <div class="classe_text" runat="server" id="Cont_classe_text"></div>
                 </div>
                 </div>

            <div id="args">
                <h2 class="display-6" style="font-size:13px;margin-top:8px;background-color:whitesmoke;font-family:'Poppins';padding:5px 0px;border-radius: 5px;"><b>Comparaison produits et services :</b></h2>
             <div class="row">
                  <div>
                      <button type="button" id="identite" class="btn-togl"><b>Cas d'identité :</b></button>
                     <div id="identite_box" class="box" runat="server" >
                         <label>Domaine a preciser , analyser et comparer :</label>
                         <textarea  id="domaine" runat="server" ></textarea>
                         <label>Produits et services identique :</label>
                         <textarea id="PS_identique" runat="server" ></textarea>
                         <label>Produits et services identiques de la marque antérieure :</label>
                         <textarea id="Ps_identique_Marque_Ant" runat="server" ></textarea>
                     </div>
                 </div>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
             </div>
 
                        <div class="row">
                  <div>
                      <button type="button" id="inclusion" class="btn-togl"><b>Cas d'inclusion :</b></button>
                     <div id="inclusion_box" runat="server" class="box">
                         <label>Produits / services de la marque antérieure incluant ceux de la marque à contester :</label>
                         <textarea id="PS_ant_inclus_cont" runat="server" ></textarea>
                         <label>Produits / services de la marque à contester inclus dans ceux de la marque antérieure :</label>
                         <textarea id="PS_cont_inclus_ant" runat="server" ></textarea>
                     </div>
                 </div>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              

                 
             </div>
                                    <div class="row">
                  <div>
                      <button type="button" id="complementarite" class="btn-togl"><b>Cas de complémentarité :</b></button>
                     <div id="complementarite_box" runat="server" class="box">
                         <label>Produits / services de la marque antérieure complémentaire à ceux de la marque à contester :</label>
                         <textarea id="PS_ant_comp_cont" runat="server" ></textarea>
                         <label>Produits / services de la marque à contester complémentaire à ceux de la marque antérieure :</label>
                         <textarea id="PS_cont_comp_ant" runat="server" ></textarea>
                         <label>Numero de la classe complémentaire de la marque à contester :</label>
                         <textarea id="Num_classe_comp_cont" runat="server" ></textarea>
                         <label>Numero de la classe complémentaire de la marque antérieure :</label>
                         <textarea id="Num_classe_comp_ant" runat="server" ></textarea>
                          <label>Critére de similarité :</label>
                         <textarea id="Critere_similarite" runat="server" ></textarea>
                     </div>
                 </div>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              

                 
             </div>
                       <h2 class="display-6" style="font-size:13px;margin-top:8px;background-color:whitesmoke;font-family:'Poppins';padding:5px 0px;border-radius: 5px;"><b>Comparaison des signes :</b></h2>
                    <div class="row">
                  <div>
                      <button type="button" id="comparaison" class="btn-togl"><b>Comparaison des signes :</b></button>
                     <div id="comparaison_box" runat="server" class="box">
                         <label>Argumentaire de la similarité visuelle :</label>
                         <textarea id="arg_visuelle" runat="server" ></textarea>
                         <label>Argumentaire de la similarité phonétique :</label>
                         <textarea id="arg_phonetique" runat="server" ></textarea>
                         <label>Argumentaire de la similarité conceptuelle :</label>
                         <textarea id="arg_conceptuelle" runat="server" ></textarea>
                     </div>
                 </div>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              

                 
             </div>
                       <h2 class="display-6" style="font-size:13px;margin-top:8px;background-color:whitesmoke;font-family:'Poppins';padding:5px 0px;border-radius: 5px;"><b>Appréciation globale :</b></h2>
                   <div class="row">
                  <div>
                      <button type="button" id="appreciation" class="btn-togl"><b>Appréciation générale du risque de confusion :</b></button>
                     <div id="appreciation_box" runat="server"  class="box">
                         <label>Appréciation générale du risque de confusion :</label>
                         <textarea id="appre_gen" runat="server" ></textarea>
                     </div>
                 </div>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              

                 </div>
             
                </div>
                <asp:Button ID="Download_word" CssClass="download" runat="server"  Text="Telecharger word" OnClick="Download_Word_Click" />
            
        </div>
            
            
            
           </div>
                  </form>
    
        
                      <script type="text/javascript">
                          function HideArgs() {
                              var box = document.getElementById('Arguments');
                              box.style.top = -1900 + "px";
                              box.style.left = "190px";
                          }

                          var Ant_classe_text = document.getElementById("Ant_classe_text");
                          var Btn_Toggle_Ant = document.getElementById("Btn_Toggle_Ant");
                          Btn_Toggle_Ant.addEventListener("click", function () {
                              Ant_classe_text.classList.toggle("Show");
                          });

                          var Cont_classe_text = document.getElementById("Cont_classe_text");
                          var Btn_Toggle_Cont = document.getElementById("Btn_Toggle_Cont");
                          Btn_Toggle_Cont.addEventListener("click", function () {
                              Cont_classe_text.classList.toggle("Show");
                          });

                          var identite = document.getElementById("identite");
                          var identite_box = document.getElementById("identite_box");
                          identite.addEventListener("click", function () {
                              if (identite_box.style.height == "auto") {
                                  identite_box.style.height = "0px";
                                  pagination.style.marginTop = "0px";
                              } else {
                                  identite_box.style.height = "auto";
                              }
                          });

                          var inclusion = document.getElementById("inclusion");
                          var inclusion_box = document.getElementById("inclusion_box");
                          inclusion.addEventListener("click", function () {
                              if (inclusion_box.style.height == "auto") {
                                  inclusion_box.style.height = "0px";
                                  pagination.style.marginTop = "0px";
                              } else {
                                  inclusion_box.style.height = "auto";
                              }
                          });

                          var complementarite = document.getElementById("complementarite");
                          var complementarite_box = document.getElementById("complementarite_box");
                          complementarite.addEventListener("click", function () {
                              if (complementarite_box.style.height == "auto") {
                                  complementarite_box.style.height = "0px";
                                  pagination.style.marginTop = "0px";
                              } else {
                                  complementarite_box.style.height = "auto";
                              }
                          });

                          var comparaison = document.getElementById("comparaison");
                          var comparaison_box = document.getElementById("comparaison_box");
                          comparaison.addEventListener("click", function () {
                              if (comparaison_box.style.height == "auto") {
                                  comparaison_box.style.height = "0px";
                                  pagination.style.marginTop = "0px";
                              } else {
                                  comparaison_box.style.height = "auto";
                              }
                          });

                          var comparaison = document.getElementById("appreciation");
                          var appreciation_box = document.getElementById("appreciation_box");
                          appreciation.addEventListener("click", function () {
                              if (appreciation_box.style.height == "auto") {
                                  appreciation_box.style.height = "0px";
                                  pagination.style.marginTop = "0px";
                              } else {
                                  appreciation_box.style.height = "auto";
                              }
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
