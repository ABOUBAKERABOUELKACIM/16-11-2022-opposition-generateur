<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Formulaire.aspx.cs" Inherits="Opposition_Generateur.Formulaire" %>

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
<title>Formulaire</title>
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
                    
<h2 class="container-header-title">Formulaire d'opposition a une demande d'enregistrement de marque</h2>
                    
                    
                </div><br />

                <div class="container-fluid" >
                    <div class="row" >
                <div class="col-12 col-md-6" style="margin-bottom:20px;">
              <div class="nature-droit-anterieur-container">
                    <h4>Nature du droit anterieure :</h4><br />

                    <div class="form-group">
                    <input type="checkbox" id="case-1" name ="case-1" class="form-check-input" />
                    <label for="case-1">Propriétaire d’une demande d’enregistrement d’une marque antérieurement déposée.</label>
                    </div>
                    <br />

                    <div class="form-group">
                    <input type="checkbox" id="case-2" name ="case-2" class="form-check-input" />
                    <label for="case-2">Propriétaire d’une marque protégée.</label>
                     </div>
                    <br />

                    <div class="form-group">
                    <input type="checkbox" id="case-3" name ="case-3" class="form-check-input" />
                    <label for="case-3">Propriétaire d’une marque bénéficiant d’une date de priorité antérieure.</label>      
                    </div>
                    <br />

                    <div class="form-group">
                    <input type="checkbox" id="case-4" name ="case-4" class="form-check-input" />
                    <label for="case-4">Propriétaire d’une marque antérieure notoirement connue.</label>
                    </div>
                    <br />

                    <div class="form-group">
                    <input type="checkbox" id="case-5" name ="case-5" class="form-check-input" />
                    <label for="case-5">Bénéficiaire d’une licence exclusive d’exploitation.</label>         
                    </div>
                    <br />
                    <div class="form-group">
                    <input type="checkbox" id="case-6" name ="case-6"  class="form-check-input"/>
                    <label for="case-6">Titulaire d’une indication géographique ou appellation d’origine protégée ou antérieurement déposée.</label>             
                    </div>
                </div>
                    </div>

                <div class="col-12 col-md-6" style="margin-bottom:5px;">
                <div class="marque-anterieure-container">
                    <h4>Reference du droit anterieure :</h4><br />
                    <div class="form-group">
                    <label for="n-deopt-anterieure" style="margin-right:5px;">Numéro de dépot de la marque anterieure :</label>  
                    <input type="text" id="n-deopt-anterieure" class="n-depot form-control" name ="n-deopt-anterieure" />                  
                    </div>
                    <br />
                    <div class="form-group">
                    <label for="">Nature de la marque : </label>
                    <div>
                    <input type="checkbox" class="marque-nature marque-nature-anterieure form-check-input" id="marque-nationale-anterieure"  name ="marque-nationale-anterieure" onclick="Func(this,'marque-nature-anterieure')" /><label for="marque-nationale-anterieure">marque nationale </label>
                    <input type="checkbox" class="marque-nature marque-nature-anterieure form-check-input" id="marque-internationale-anterieure" name="marque-internationale-anterieure"  onclick="Func(this,'marque-nature-anterieure')" /><label for="marque-internationale-anterieure">marque internationale</label>                               
                    </div>
                    </div>
                </div><br />
                 <div class="marque-contester-container">
                    <h4>Reference du droit contester :</h4><br />
                    <div class="form-group">
                    <label for="n-deopt-contester" style="margin-right:5px;">Numéro de dépot de la marque contester :</label>  
                    <input type="text" id="n-deopt-contester" class="n-depot form-control" name ="n-deopt-contester" />                  
                    </div>
                    <br />
                    <div class="form-group">
                    <label for="">Nature de la marque : </label>
                    <div>
                    <input type="checkbox" class="marque-nature marque-nature-contester form-check-input" id="marque-nationale-contester" name ="marque-nationale-contester" onclick="Func(this,'marque-nature-contester')" /><label for="marque-nationale-contester">marque nationale </label>
                    <input type="checkbox" class="marque-nature marque-nature-contester form-check-input" id="marque-internationale-contester" name="marque-internationale-contester"  onclick="Func(this,'marque-nature-contester')" /><label for="marque-internationale-contester">marque internationale</label>                               
                    </div>
                    </div>
                </div><br />
                <asp:Button OnClick="Button1_Click" CssClass="AddForm" ID="Button1" runat="server" Text="Ajouter le formulaire" /><asp:Button CssClass="ViewForms" OnClick="Button2_Click" ID="Button2" runat="server" Text="Voir les formulaires" />
           </div>
                        </div>
                    </div>
            
          </div>               
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
                      </script>
</body>
</html>
