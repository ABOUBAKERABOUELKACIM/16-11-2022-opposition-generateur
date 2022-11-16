<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Recherche phonetique.aspx.cs" Inherits="Opposition_Generateur.Views.Recherche_phonetique" %>

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
<title>Recherche phonétique</title>
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
            height:100vh;
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

         #Pagination___{
             float:right;
             display:flex;
             gap:3px;
             align-items:center;
         }
         #Precedent{
                          border: 1px solid rgb(116, 116, 116 , 0.78);
            background-color: transparent;
            padding: 3px 5px;
            border-radius: 5px;
            font-family: 'Poppins';
            font-size: 13px;
         }
         #Suivant{
                          border: 1px solid rgb(116, 116, 116 , 0.78);
            background-color: transparent;
            padding: 3px 5px;
            border-radius: 5px;
            font-family: 'Poppins';
            font-size: 13px;
         }
         #index{
            font-family: 'Poppins';
            font-size: 13px;
         }
                  .Image__item{
             transition:0.4s cubic-bezier(0.71, -0.27, 0.06, 1.6);
             object-fit:contain;
         }
         .Image__item:hover{
             z-index:10;
             transform:scale(2);
         }
         #Filtrer{
           outline: 0px;
    border: 0px;
    border-radius: 8px;
    background-color: #f53b3b;
    color: white;
    border-radius: 5px;
    font-family: 'Poppins';
    padding: 5px 5px;
    transition: 0.3s ease-in-out;
    width: 100px;
    font-size: 14px;
         }
         #Filtrer:hover{
            background-color: indianred;
             color:black;
         }
         #Downloadipreport{
            outline: 0px;
    border: 0px;
    border-radius: 8px;
    background-color: #e94444;
    color: white;
    border-radius: 5px;
    font-family: 'Poppins';
    padding: 5px 5px;
    transition: 0.3s ease-in-out;
    width: 250px;
    font-size: 14px;
         }
         #Downloadipreport:hover{
            background-color: indianred;
             color:black;
         }
         td{
             font-family: 'Poppins';
             font-size:13px;
         }
         #Classe_commun{
            outline: 0px;
    border: 0px;
    border-radius: 8px;
    background-color: #e94444;
    color: white;
    border-radius: 5px;
    font-family: 'Poppins';
    padding: 5px 5px;
    transition: 0.3s ease-in-out;
    width: 250px;
    font-size: 14px
         }
         #Classe_commun:hover{
           background-color: indianred;
             color:black;
         }
         #Reinitialiser{
            outline: 0px;
    border: 0px;
    border-radius: 8px;
    background-color: #f53b3b;
    color: white;
    border-radius: 5px;
    font-family: 'Poppins';
    padding: 5px 5px;
    transition: 0.3s ease-in-out;
    width: 100px;
    font-size: 14px;
         }
         #Reinitialiser:hover{
           background-color: indianred;
             color:black;
         }

             .blue {
                background-color:darkblue;
                color:white;
                padding: 0px 3px;
                border-radius: 5px;
             }
              .orange{
                background-color:orange;
                color:white;
                padding: 0px 3px;
                border-radius: 5px;
              }
          .purple{
             background-color:purple;
             color:white;
             padding: 0px 3px;
             border-radius: 5px;
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
        </div>
                    <div class="box"></div>
        <div id="container">
                 
                <div class="header">
                    <img src="../Assets/Img/ic_round-searchsearch.png" style="display:block;margin:auto;"/>
                    <img src="../Assets/Img/cropped-logo-2.png" style="position: absolute; top: 0px; right: 0px;width:250px;height:80px"/>
                    <h2 class="container-header-title">Recherche phonétique</h2>
                </div><br />
                 <div class="container-fluid">
                     <div class="row">
                    <div class="col-12 col-md-6 col-lg-6" style="display:flex;flex-direction:column;">

                    <label for="num_publication">Numéro de publication :</label>
                    <input type="text" runat="server" class="form-control" id="num_publication"/>
                    <asp:CustomValidator ID="NumPubValidator" runat="server"  ErrorMessage="Numéro de publication." ControlToValidate="num_publication" Display="Dynamic" ForeColor="#CC3300" OnServerValidate="NumPubValidator_ServerValidate" ValidateEmptyText="true">Numéro de publication.</asp:CustomValidator>
                    <div>
                    <br />
                    <h6>Méthodes :</h6>
                    <label for="Soundex">Soundex :</label>
                    <input type="checkbox" name="Soundex" runat="server" class="form-check-input" value="Soundex" id="Soundex" />
                    <label for="Contains">Contains :</label>
                    <input type="checkbox" name="Contains" runat="server" class="form-check-input" value="Contains" id="Contains" />
                    <label for="Parametre">Paramétre :</label>
                    <input type="checkbox" name="Parametre" runat="server" class="form-check-input" value="Parametre" id="Parametre" /> 
                    <label for="Difference">Différence :</label>
                    <input type="checkbox" name="Difference" runat="server" class="form-check-input" value="Difference" id="Difference" />                        
                    </div>
                    <input type="text" hidden="hidden" name="options" class="form-control" runat="server" id="options" />
                    <asp:CustomValidator ID="Validator" runat="server"  ErrorMessage="Cocher au moins une case." ControlToValidate="options" Display="Dynamic" ForeColor="#CC3300" OnServerValidate="Validator_ServerValidate" ValidateEmptyText="true">Cocher au moins une case.</asp:CustomValidator>
                    <br />
                    </div>

                    <div class="col-12 col-md-6 col-lg-6" style="display:flex;flex-direction:column;">

                    <label for="IpreportUpload">Importer Ip Report :</label>
                    <input type="file" name="IpreportUpload" class="form-control" runat="server" id="IpreportUpload" />
                    <asp:CustomValidator ID="IpreportUploadValidator" runat="server"  ErrorMessage="Importer le document ip report ou le fichier excel." ControlToValidate="IpreportUpload" Display="Dynamic" ForeColor="#CC3300" OnServerValidate="IpreportUploadValidator_ServerValidate" ValidateEmptyText="true">Importer le document ip report ou le fichier excel.</asp:CustomValidator>
                     <br />
                    <label for="PortefeuilleUpload">Importer Portefeuille :</label>
                    <input type="file" name="PortefeuilleUpload" class="form-control" runat="server" id="PortefeuilleUpload" />
                    <asp:CustomValidator ID="PortefeuilleUploadValidator" runat="server"  ErrorMessage="Importer le portefeuille." ControlToValidate="PortefeuilleUpload" Display="Dynamic" ForeColor="#CC3300" OnServerValidate="PortefeuilleUploadValidator_ServerValidate" ValidateEmptyText="true">Importer le portefeuille.</asp:CustomValidator>
                    <br />
                    <label for="ImagesgazetteUpload">Importer images gazette :</label>
                    <asp:FileUpload class="form-control"  runat="server" AllowMultiple="true" ID="ImagesgazetteUpload" />

                    <br />
                    
                    </div>
                    </div>
                     <asp:Button ID="Filtrer"  runat="server" Text="Filtrer" OnClick="Filtrer_Click" />
                     <asp:Button ID="Downloadipreport"  runat="server" Text="Telecharger ipreport(Excel)" OnClick="Downloadipreport_Click" CausesValidation="false" />
                     <br />
                     <asp:Button ID="Classe_commun"  runat="server" Text="Résultat ayant Cl com" OnClick="Classe_commun_Click" CausesValidation="false" style="margin-top:5px" />
                     <asp:Button ID="Reinitialiser"  runat="server" Text="Réinitialiser" OnClick="Reinitialiser_Click"  CausesValidation="false" />
                </div>
            
            <br />
            

                                <div class ="container___Result">
                                     <p>Resultat :</p>
                     <div id="Pagination___">
                <asp:Button ID="Precedent" runat="server" Text="Précedent" OnClick="Precedent_Click" CausesValidation="false" />
                <asp:Label ID="index" runat="server" Text="Label">1 / 1</asp:Label>
                <asp:Button ID="Suivant" runat="server" Text="Suivant" OnClick="Suivant_Click" CausesValidation="false" />
                </div>
                    </div>
                                    <div id="Result___">
                   <asp:GridView ShowHeaderWhenEmpty="True" class="table container-fluid" ID="GridView1" runat="server" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" ForeColor="Black" Font-Names="Arial" Font-Size="8pt" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                    
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    
                    <Columns>

                        <asp:BoundField HeaderText="Détails ip report marque" DataField="DetailsMarqueIpReport" HtmlEncode="false" ItemStyle-Width="400" >

<ItemStyle Width="400px"></ItemStyle>

                        </asp:BoundField>
                        <asp:BoundField HeaderText="Ip report marque" DataField="MarqueIpReport" HtmlEncode="false" ItemStyle-Width="200">

<ItemStyle Width="200px"></ItemStyle>

                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Image ipreport marque">
                            <ItemTemplate>
                            <asp:Image CssClass="Image__item" Height = "90" Width = "90" runat="server" ImageUrl='<%# "~/Assets/Brand_image/"+Eval("ImageMarqueIpReport").ToString() %>' />                           
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Image marque similaire">
                            <ItemTemplate>
                            <asp:Image CssClass="Image__item" Height = "90" Width = "90" runat="server" ImageUrl='<%# "~/Assets/Brand_image/"+Eval("ImageMarqueSimilaire").ToString() %>' />                           
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Portefeuille marque similaire" DataField="MarqueSimilaire" HtmlEncode="false" ItemStyle-Width="200">

<ItemStyle Width="200px"></ItemStyle>

                        </asp:BoundField>
                        <asp:BoundField HeaderText="Détails marque similaire" DataField="DetailsMarqueSimilaire" HtmlEncode="false" ItemStyle-Width="400" >


<ItemStyle Width="400px"></ItemStyle>


                        </asp:BoundField>

                           <asp:TemplateField HeaderText="Action">                              
                            <ItemTemplate>                            
                                <asp:Button ID="Generate_doc" CssClass="btn-success border-0 my-1 py-1 px-1 rounded-2 btn-action" runat="server"  Text="Générer Alerte" OnClick="Generate_doc_Click" />
                            </ItemTemplate>

                        </asp:TemplateField>

                    </Columns>
                    
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" ForeColor="White" Font-Size="10pt" Font-Bold="True" Font-Names="Arial" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
                   </div>
                
         </div>

                </form>
                </div>
             <script type="text/javascript">
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
