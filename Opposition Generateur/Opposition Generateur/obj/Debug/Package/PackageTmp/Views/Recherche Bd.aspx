<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recherche Bd.aspx.cs" Inherits="Opposition_Generateur.Views.Recherche_Bd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width" />
<link rel="preconnect" href="https://fonts.googleapis.com"/>
<link rel="preconnect" href="https://fonts.gstatic.com" />
    <script src="JS/jquery-1.11.1.js" type="text/javascript">  
</script>  
<link href="https://fonts.googleapis.com/css2?family=Roboto:ital,wght@1,300&display=swap" rel="stylesheet" />
<link href="https://fonts.googleapis.com/css2?family=Poppins&family=Roboto:ital,wght@1,300&display=swap" rel="stylesheet" />
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.min.js" integrity="sha384-QJHtvGhmr9XOIpI6YVutG+2QOK9T+ZnN4kzFN1RtK3zEFEIsxhlmWl5/YESvpZ13" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
<link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous"/>
  
    <script src="../Scripts/jquery-3.6.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tablesorter.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#GridView1").tablesorter();
        })
    </script>  

<title>Recherche Base de données</title>
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
          #Bd_searach_params{
             display:flex;
             flex-direction:column;
          }
             #Bd_searach_params > div > label {
                 font-size: 13px;
             }
         #Pagination___{
             float:right;
             display:flex;
             gap:3px;
             align-items:center;
             padding-right:35px;
         }
        #Precedent{
                          border: 1px solid rgb(116, 116, 116 , 0.78);
            background-color: #dd7973;
            padding: 3px 5px;
            border-radius: 5px;
            font-family: 'Poppins';
            border-radius: 50%;
            font-size: 13px;
            color:white;
         }
         #Suivant{
                          border: 1px solid rgb(116, 116, 116 , 0.78);
            background-color: #dd7973;
            padding: 3px 5px;
           
            border-radius: 5px;
            font-family: 'Poppins';
            border-radius: 50%;
            color:white;
            font-size: 13px;
             width: 79px;
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
         #Search{
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
             border:0px;           
         }
         #Search:hover{
           background-color: indianred;
             color:black;
         }
         #Afficher_Masquer{
            outline: 0px;
            border:0px;
            border-radius: 8px;
            background-color: #e94444;
            color:white ;
           border-radius: 5px;
            font-family: 'Poppins';
            padding: 5px 5px;
            transition:0.3s ease-in-out;
            width:150px;
            
            font-size:14px;
         }
         #Afficher_Masquer:hover{
          background-color: indianred;
             color:black;
         }

          #PDF{
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
         }
         #PDF:hover{
            background-color: indianred;
             color:black;
         }
             #export:hover{
            background-color: indianred;
             color:black;
         }
         .etatMarque{
            outline:0px;
            display: block;
            width: 100%;
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: 0.25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
         }
         .type_marque{
            outline:0px;
            display: block;
            width: 100%;
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: 0.25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
         }
         
         .hidden_search_field{
             display:none;
         }
        .show{
            display:block;
        }
        td{
            font-size:10px;
            color:black;
            width:10%;
        }        
        .ompic_value{
            color:black;
        }
        .tm_value{
            color:#292997;
        }
         #Afficher_Masquer0{
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
         }
           #export{
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
         }
           
         </style>
     <link rel="stylesheet" href="../Assets/Css/Sidemenustyle.css" />
</head>
<body>
  
                  <div id="page-top">
                <form id="form1" runat="server">
                    è<div id="user_account"><br />
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
                    <h2 class="container-header-title">Recherche Base de données</h2>
                </div><br />
                    <div id="Bd_searach_params" class="container-fluid">
                        <p>Remplie ces champs par des valeurs valides.</p>
                       
                        <label for="ShowOrHide">Afficher/Masquer les champs.</label>
                        <input type="checkbox" id="ShowOrHide" onclick="func()" name ="ShowOrHide" class="form-check-input" />
                    <div class="row">
                    
                    <div class="col-12 col-md-6 col-lg-6" style="display:flex;flex-direction:column;">                  
                    <label for="nom_marq">Nom de la marque :</label>
                    <input type="text" name="nom_marq" class="form-control" id="nom_marq"/>                   
                    <br />
                    <label for="deposant">Déposant :</label>
                    <input type="text" name="deposant" class="form-control" id="deposant"/>
                    <div class="hidden_search_field">
                    <br />
                 
                    <label for="adresse_deposant">Adresse deposant :</label>
                    <input type="text" name="adresse_deposant" class="form-control" id="adresse_deposant"/>                   
                    <br />                   
                    <label for="Pays_deposant">Pays deposant :</label>
                    <input type="text" name="Pays_deposant" class="form-control" id="Pays_deposant"/>

                     <br />
                
                    
                    <label for="date_depot_debut">Date dépot début :</label>
                    <input type="date" name="date_depot_debut" class="form-control"  id="date_depot_debut"/>
                    <br />
                    <label for="date_exp_debut">Date expiration début :</label>
                    <input type="date" name="date_exp_debut" class="form-control" id="date_exp_debut"/>
                    
                                            <br />
                    <label for="OppositionNbrMin">Opposition(Minimum) :</label>
                    <input type="text" name="OppositionNbrMin" class="form-control" id="OppositionNbrMin"/>
                    <br />
              <div style="display:flex;gap:10px;">                   
                  <div>
                    <label for="type_marqueOmpic">Type marque(Ompic) :</label>
                    <select name="type_marqueOmpic" id="type_marqueOmpic" class="type_marque">
                          <option value=""></option>
                          <option value="Autres">Autres</option>
                          <option value="Mixte">Mixte</option>
                          <option value="Figuratif">Figuratif</option>
                          <option value="Denominatif">Denominatif</option>
                          <option value="Tridimensionnel">Tridimensionnel</option>
                          <option value="Sonore">Sonore</option>
                    </select>
                    </div>
                  <div>
                    <label for="type_marqueTm">Type marque(Tm) :</label>
                    <select name="type_marqueTm" id="type_marqueTm" class="type_marque">
                          <option value=""></option>
                          <option value="Combined">Combined</option>
                          <option value="Sound">Sound</option>
                          <option value="Other">Other</option>
                    </select>
                  </div>
               </div>
                        <br />
                <div style="display:flex;gap:10px;">
                    <div>
                    <label for="etatMarqueOmpic"> Etat(Ompic) :</label>
                    <select name="etatMarqueOmpic" id="etatMarqueOmpic" class="etatMarque">
                          <option value=""></option>
                          <option value="OPPOSITION">OPPOSITION</option>
                          <option value="DECHUE">DECHUE</option>
                          <option value="IRRECEVABLE">IRRECEVABLE</option>
                            
                          <option value="EN COURS D'EXAMEN">EN COURS D'EXAMEN</option>
                          <option value="OPPOSITION SUSPENDUE">OPPOSITION SUSPENDUE</option>
                          <option value="REJETEE">REJETEE</option>

                          <option value="RETIREE">RETIREE</option>
                          <option value="EXPIREE">EXPIREE</option>
                          <option value="RENOUVELEE">RENOUVELEE</option>
                            
                          <option value="ENREGISTREE">ENREGISTREE</option>
                          <option value="EN EXAMEN DE FORME">EN EXAMEN DE FORME</option>

                          <option value="CONSIDEREE COMME RETIREE">CONSIDEREE COMME RETIREE</option>
                          <option value="OPPOSITION EN COURS">OPPOSITION EN COURS</option>
                          <option value="RENONCEE">RENONCEE</option>
                            
                          <option value="PUBLICATION PROGRAMMEE">PUBLICATION PROGRAMMEE</option>
                          <option value="EN INSTANCE DE REGULARISATION">EN INSTANCE DE REGULARISATION</option>
                          <option value="PUBLIEE">PUBLIEE</option>

                          <option value="EN EXAMEN DES MOTIFS ABSOLUS">EN EXAMEN DES MOTIFS ABSOLUS</option>
                          <option value="RADIEE">RADIEE</option>
                          <option value="EN POURSUITE DE PROCEDURE">EN POURSUITE DE PROCEDURE</option>
                    </select>
                       </div>
                    <div>
                    <label for="etatMarqueTm"> Etat(Tm) :</label>
                    <select name="etatMarqueTm" id="etatMarqueTm" class="etatMarque">
                          <option value=""></option>
                          <option value="Registered">Registered</option>
                          <option value="Registration cancelled">Registration cancelled</option>
                          <option value="Application opposed">Application opposed</option>
                          <option value="Registration surrendered">Registration surrendered</option>
                          <option value="Application refused">Application refused</option>
                          <option value="Expired">Expired</option>
                          <option value="Application withdrawn">Application withdrawn</option>
                          <option value="Application filed">Application filed</option>
                          <option value="Application published">Application published</option>

                          <option value="Appeal pending">Appeal pending</option>
                          <option value="Renewed">Renewed</option>

                    </select>
                        </div>
                    </div>
                            </div>
                    </div>
                        
                    <div class="col-12 col-md-6 col-lg-6" style="display:flex;flex-direction:column;" >
                    <label for="num_marq">Numero de marque :</label>
                    <input type="text" name="num_marq" class="form-control"  id="num_marq"/>
                                            <br />
              

                    <label for="mandataire">Mandataire :</label>
                    <input type="text" name="mandataire" class="form-control" id="mandataire"/>
                    
               <div class="hidden_search_field">
                    <br />
                    <label for="adresse_mandataire">Adresse mandataire :</label>
                    <input type="text" name="adresse_mandataire" class="form-control" id="adresse_mandataire"/>
                    <br />
                    <label for="Pays_mandataire">Pays mandataire :</label>
                    <input type="text" name="Pays_mandataire" class="form-control" id="Pays_mandataire"/>
                    <br />
                    <label for="date_depot_fin">Date dépot fin :</label>
                    <input type="date" name="date_depot_fin" class="form-control"  id="date_depot_fin"/>
                    <br />
                    <label for="date_exp_fin">Date expiration fin :</label>
                    <input type="date" name="date_exp_fin" class="form-control" id="date_exp_fin"/>
                    <br />
                    
                    <label for="email">Email :</label>
                    <input type="text" name="email" class="form-control" id="email"/>
                    <br />
                    <label for="Classe_nice">Classe nice :</label>
                    <input type="text" name="Classe_nice" class="form-control" id="Classe_nice"/>
                    <br />
                    <label for="Da_opposition">Droit antérieure opposition :</label>
                    <input type="text" name="Da_opposition" class="form-control" id="Da_opposition"/>
                    <br />
                    <label for="Opposant">Opposant :</label>
                    <input type="text" name="Opposant" class="form-control" id="Opposant"/>
                    </div>
                    </div>
                    </div><br />
                        <asp:Button ID="Search" runat="server" Text="Chercher" OnClick="Search_Click" />
                </div>
            
            <br /><br />
                    <label for="columns_dropdown">Afficher / Masquer Colonne :</label>
                    <select name="columns_dropdown" id="columns_dropdown">
                        <option value="Nom_marque">Nom marque</option>
                        <option value="Date_depot">Date depot</option>
                        <option value="Date_expiration">Date expiration</option>
                        <option value="Applicant_name">Deposant</option>
                        <option value="Applicant_address">Adresse Deposant</option>
                        <option value="Pays">Pays Deposant</option>
                        <option value="Representative_name">Mandataire</option>
                        <option value="Representative_address">Adresse Mandataire</option>
                        <option value="Representative_countryCode">Pays Mandataire</option>
                        <option value="Type">Type marque</option>
                        <option value="Statut">Statut marque</option>
                        <option value="Email">Email</option>
                        <option value="Telephone">Telephone</option>
                        <option value="ClasseNice">Classe nice</option>
                        <option value="Opposition_applicant_name">Opposant</option>
                        <option value="Opposition_earlierMark_applicationNumber">DA opposition</option>
                        <option value="Nombre_opposition">Nombre opposition</option>
                    </select>
               <asp:Button ID="Afficher_Masquer" runat="server" Text="Afficher/Masquer" OnClick="Afficher_Masquer_Click" />
                                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="PDF" runat="server" OnClick="Export_pdf_Click" Text="Export pdf" />
                   <div id="Pagination___">
                <asp:Button ID="Precedent" runat="server" Text="Précedent" OnClick="Precedent_Click" />
                <asp:Label ID="index" runat="server" Text="Label">1 / 1</asp:Label>
                <asp:Button ID="Suivant" runat="server" Text="Suivant" OnClick="Suivant_Click" />
                </div>
&nbsp;<div class ="container___Result">
                                     <p>Resultat :</p>

                     <div id="Result___">
                   <asp:GridView ShowHeaderWhenEmpty="True" class="table container-fluid" ID="GridView1" runat="server" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" ForeColor="Black" Font-Names="Arial" Font-Size="8pt"  ClientIDMode="Static" OnDataBound="GridView1_DataBound" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" HeaderStyle-BackColor="Blue" >
                    
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    
                    <Columns>
                        <asp:BoundField HeaderText="Numero marque" DataField="Numero_titre" HtmlEncode="false" SortExpression="Numero marque"/>
                         <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                            <asp:Image  CssClass="Image__item" Height = "90" Width = "90" runat="server" ImageUrl='<%# "~/Assets/Brand_image/"+Eval("Image").ToString() %>' />                           
                            </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:BoundField HeaderText="Nom Marque" DataField="Nom_marque" HtmlEncode="false" SortExpression="Nom Marque"/>
                        <asp:BoundField HeaderText="Deposant" DataField="Applicant_name"  HtmlEncode="false" SortExpression="Deposant"/> 
                        <asp:BoundField HeaderText="Mandataire" DataField="Representative_name" HtmlEncode="false" SortExpression="Mandataire"/>
                        <asp:BoundField HeaderText="Date depot" DataField="Date_depot" HtmlEncode="false" SortExpression="Date depot"/>
                        <asp:BoundField HeaderText="Date expiration" DataField="Date_expiration" HtmlEncode="false" SortExpression="Date expiration"/>
                        <asp:BoundField HeaderText="Classe nice" DataField="ClasseNice" HtmlEncode="false" SortExpression="Classe nice"/>
                        <asp:BoundField HeaderText="Statut marque" DataField="Statut" HtmlEncode="false" SortExpression="Statut marque"/>

                        <asp:BoundField HeaderText="Adresse Deposant" DataField="Applicant_address" HtmlEncode="false" SortExpression="Adresse Deposant"/>
                        <asp:BoundField HeaderText="Pays Deposant" DataField="Pays" HtmlEncode="false" SortExpression="Pays Deposant"/>
                        
                        <asp:BoundField HeaderText="Adresse Mandataire" DataField="Representative_address" HtmlEncode="false" SortExpression="Adresse Mandataire"/>
                        <asp:BoundField HeaderText="Pays Mandataire" DataField="Representative_countryCode" HtmlEncode="false" SortExpression="Pays Mandataire" />
                        <asp:BoundField HeaderText="Type marque" DataField="Type" HtmlEncode="false" SortExpression="Type marque"/>
                        
                        <asp:BoundField HeaderText="Email" DataField="Email" HtmlEncode="false" SortExpression="Email"/>
                        <asp:BoundField HeaderText="Telephone" DataField="Telephone" HtmlEncode="false"  SortExpression="Telephone"/>
                        
                        <asp:BoundField HeaderText="Opposant" DataField="Opposition_applicant_name" HtmlEncode="false" SortExpression="Opposant"/>
                        <asp:BoundField HeaderText="Droit anterieure opposition" DataField="Opposition_earlierMark_applicationNumber" HtmlEncode="false" SortExpression="Droit anterieure opposition"/>
                        <asp:BoundField HeaderText="Nombre opposition" DataField="Nombre_opposition" HtmlEncode="false" SortExpression="Nombre opposition"/>
                                                <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button ID="ShowDetails" CssClass="btn-success border-0 py-2 px-2 rounded-2" runat="server"  Text="Details" OnClick="ShowDetails_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                       
                    
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Red" ForeColor="White" Font-Size="10pt" Font-Bold="True" Font-Names="Arial" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
   
         
                    </div>
         </div>

                    </div>
                </form>
                </div>
             <script type="text/javascript">
                 function func() {
                     var hidden_search_field = Array.from(document.getElementsByClassName("hidden_search_field"));
                     hidden_search_field.forEach((item) => {
                         item.classList.toggle("show");
                     })
                 }
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
