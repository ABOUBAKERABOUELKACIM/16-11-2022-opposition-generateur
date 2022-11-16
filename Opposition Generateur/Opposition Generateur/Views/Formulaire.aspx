<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Formulaire.aspx.cs" Inherits="Opposition_Generateur.Formulaire" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">


<head>
     <meta charset="UTF-8"/>
     <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
     <meta name="viewport" content="width=device-width, initial-scale=1.0"/>

     <!-- FAVICON -->
     <link rel="icon" href="../Assets/Img/IP_LOGO_BLACK.png"/>
     <title>IP Platform</title>

     <!-- GOOGLE FONT LINK -->
     <link rel="preconnect" href="https://fonts.googleapis.com"/>
     <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin/>
     <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet"/>

     <!-- FONT AWESOME -->
     <script src="https://kit.fontawesome.com/272eed3989.js" crossorigin="anonymous"></script>

     <!-- TAILWINDCSS OUTPUT -->
     <link href="../Assets/Css/output.css" rel="stylesheet" />
</head>
    
     <body >
    <div class="flex flex-row relative">
        
                        <form id="form1" runat="server">
                    
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
                           <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="Button3" Text="Validation Phonetique" OnClick="btn_strongvalid_Click"  />
                            <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="Button4" Text="Validation des conflits" OnClick="btn_validation_Click" />
                            <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="Button5" Text="Notification des conflits" OnClick="btn_notification_Click" />
                            <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="Button6" Text="Gestion des conflits" OnClick="Bnt_gestion_Click" />

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
                        <asp:Button CssClass="flex justify-between items-center w-full md:py-3 md:px-2 p-2 mt-1 rounded text-sm capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-700 focus:text-white sl-animated" runat="server" ID="Button7" Text="Archive" OnClick="Archive_Click" /></li>
                    
                </ul>

            </div>

                <main class="lg:ml-[220px] w-full min-h-screen ">
               <!-- HEADER START -->
               <header class="bg-slate-900">
                    <div class="flex justify-between items-center md:px-8 px-4 border-b border-gray-700">

                    <a href="home.aspx" class="py-4 md:text-2xl text-sm font-bold capitalize text-white"><i class="fa-solid fa-arrow-left md:text-lg text-sm md:pr-4 pr-2"></i>Formulaire </a>
                         <a href="home.aspx" class="flex items-center">
                              <img src="../Assets/Img/IP_LOGO_FULL.png" alt="logo" class="md:h-8 h-6" />
                         </a>
                <%--        <button onclick="displayToggle(sidebar)" runat="server" class="lg:hidden block md:text-2xl text-xl text-white">
                              <i onclick="displayToggle(openSidebar)"  id="openSidebar" class="fa-solid fa-bars"></i>
                              
                         </button>--%>
                        </div>

                    
<nav>
                         <h4 class="md:px-8 px-4 pt-3 pb-2 md:text-base text-sm font-semibold text-center capitalize text-sky-500">Formulaire d'opposition a une demande d'enregistrement de
                              marque</h4>
                    </nav>
               </header>
                    
                    
               
         

                <div class="p-6">
                    <div class="p-4 rounded border border-gray-400">
                         <!-- FORM START -->
                        <section>
                         
                              <h4 class="md:text-xl text-lg font-bold">Remplie ces champs par des valeurs valides: </h4>
    
       <div class="flex flex-wrap mt-4">
      <div class="md:basis-1/2 basis-full mb-4">
          <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
             <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Nature du droit anterieue :</legend>

                    <div class="mb-4 px-2">
                    <input type="checkbox" id="case-1" name ="case-1" class="form-check-input" />
                    <label class="pr-2 lg:text-base text-sm" for="case-1">Propriétaire d’une demande d’enregistrement d’une marque antérieurement déposée.</label>
                    </div>
                    

                    <div class="mb-4 px-2">
                    <input type="checkbox" id="case-2" name ="case-2" class="form-check-input" />
                    <label class="pr-2 lg:text-base text-sm" for="case-2">Propriétaire d’une marque protégée.</label>
                     </div>
                    

                    <div class="mb-4 px-2">
                    <input type="checkbox" id="case-3" name ="case-3" class="form-check-input" />
                    <label class="pr-2 lg:text-base text-sm" for="case-3">Propriétaire d’une marque bénéficiant d’une date de priorité antérieure.</label>      
                    </div>
                    

                    <div class="mb-4 px-2">
                    <input type="checkbox" id="case-4" name ="case-4" class="form-check-input" />
                    <label class="pr-2 lg:text-base text-sm" for="case-4">Propriétaire d’une marque antérieure notoirement connue.</label>
                    </div>
                    

                    <div class="mb-4 px-2">
                    <input type="checkbox" id="case-5" name ="case-5" class="form-check-input" />
                    <label class="pr-2 lg:text-base text-sm" for="case-5">Bénéficiaire d’une licence exclusive d’exploitation.</label>         
                    </div>
                   
                   
                    <div class="mb-4 px-2">
                    <input type="checkbox" id="case-6" name ="case-6"  class="form-check-input"/>
                    <label class="pr-2 lg:text-base text-sm" for="case-6">Titulaire d’une indication géographique ou appellation d’origine protégée ou antérieurement déposée.</label>             
                    </div>
              </fieldset>
                </div>

           <div class="md:basis-1/2 basis-full"" >
                <div class="mb-4 md:px-2">
                    <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
                          <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Reference du droit anterieur :</legend>
                    <div class="form-group">
                    <label for="n-deopt-anterieure" style="margin-right:5px;" class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500"">Numéro de dépot de la marque anterieure :</label>  
                    <input type="text" id="n-deopt-anterieure" class="lg:w-4/5 w-full md:text-base text-sm bg-transparent border-b border-gray-500" placeholder="Numero de depot de la marque anterieure" name ="n-deopt-anterieure" />                  
                    </div>
                    
                    <div class="form-group">
                    <label for=""class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Nature de la marque : </label>
                    <div>
                    <input type="checkbox" class="marque-nature marque-nature-anterieure form-check-input" id="marque-nationale-anterieure"  name ="marque-nationale-anterieure" onclick="Func(this,'marque-nature-anterieure')" /><label for="marque-nationale-anterieure">marque nationale </label>
                    <input type="checkbox" class="marque-nature marque-nature-anterieure form-check-input" id="marque-internationale-anterieure" name="marque-internationale-anterieure"  onclick="Func(this,'marque-nature-anterieure')" /><label for="marque-internationale-anterieure">marque internationale</label>                               
                    </div>
                    </div>
                </fieldset></div>
                    
                 <div class="mb-4 md:px-2">
                    <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
                    <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Reference du droit anterieur :</legend>
                    <div class="form-group">
                    <label for="n-deopt-contester" class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500" style="margin-right:5px;">Numéro de dépot de la marque contester :</label>  
                    <input type="text" id="n-deopt-contester" class="lg:w-4/5 w-full md:text-base text-sm bg-transparent border-b border-gray-500" placeholder="Numero de depot de la marque contester" name ="n-deopt-contester" />                  
                    </div>
                    
                    <div class="form-group">
                    <label class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500" for="">Nature de la marque : </label>
                    <div>
                    <input type="checkbox" class="marque-nature marque-nature-contester form-check-input" id="marque-nationale-contester" name ="marque-nationale-contester" onclick="Func(this,'marque-nature-contester')" /><label for="marque-nationale-contester">marque nationale </label>
                    <input type="checkbox" class="marque-nature marque-nature-contester form-check-input" id="marque-internationale-contester" name="marque-internationale-contester"  onclick="Func(this,'marque-nature-contester')" /><label for="marque-internationale-contester">marque internationale</label>                               
                    </div>
                    </div>
                        </fieldset>
                </div>
                    <div class="md:px-2">                <asp:Button OnClick="Button1_Click" CssClass="md:px-8 px-4 md:text-base text-sm mt-2 py-2 rounded-md font-medium text-white bg-sky-500 hover:bg-sky-600 sl-animated" ID="Button1" runat="server" Text="Ajouter le formulaire" />&nbsp&nbsp<asp:Button CssClass="md:px-8 px-4 md:text-base text-sm mt-2 py-2 rounded-md font-medium text-white bg-sky-500 hover:bg-sky-600 sl-animated" OnClick="Button2_Click" ID="Button2" runat="server" Text="Voir les formulaires" />
</div>
           </div>
                    </div>
                                  

                
                    
                         </section>
                         <!-- FORM END -->
                    </div>
               </div>
               <!-- MAIN CONTAINER END -->
          </main>
          <!-- MAIN END -->
     </form>
                            </div>
         
                      <script>
                          function Func(checkbox, name) {
                              var checkboxes = Array.from(document.getElementsByClassName(name));
                              checkboxes.forEach((item) => {
                                  if (item !== checkbox) item.checked = false
                              })
                          };
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
                          function displayToggle(id) {
                              if (id.classList.contains("hidden")) {
                                  id.classList.remove("hidden");
                              } else {
                                  id.classList.add("hidden");
                              }
                          }
                          function accordionToggle(current, attr) {
                              const allCheckboxes = document.querySelectorAll(`[role="${attr}"]`);
                              for (let i = 0; i < allCheckboxes.length; i++) {
                                  allCheckboxes[i].checked = false;
                              }
                              current.checked = true;
                          }

                          function checkToggle(current, attr) {
                              const allCheckboxes = document.querySelectorAll(`[role="${attr}"]`);
                              for (let i = 0; i < allCheckboxes.length; i++) {
                                  allCheckboxes[i].checked = !allCheckboxes[i].checked;
                              }

                              if (current.checked == false) {
                                  current.checked = true;
                              } else if (current.checked == true) {
                                  current.checked = false;
                              }

                          }
                      </script>
</body>
</html>
