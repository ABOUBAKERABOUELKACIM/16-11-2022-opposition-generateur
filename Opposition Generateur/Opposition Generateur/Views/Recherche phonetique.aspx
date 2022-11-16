<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Recherche phonetique.aspx.cs" Inherits="Opposition_Generateur.Views.Recherche_phonetique" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<!DOCTYPE html>

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <!-- FAVICON -->
    <link rel="icon" href="../assets/Img/IP_LOGO_BLACK.png">
    <title>IP Platform</title>

    <!-- GOOGLE FONT LINK -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet">

    <!-- FONT AWESOME -->
    <script src="https://kit.fontawesome.com/272eed3989.js" crossorigin="anonymous"></script>

    <!-- TAILWINDCSS OUTPUT -->
    <link href="../Assets/css/output.css" rel="stylesheet">
    <style>
        .ompic_value {
            color: black;
        }

        .tm_value {
            color: #292997;
        }

        .hidden_search_field {
            display: none;
        }

        .show {
            display: block;
        }

        .Image__item {
            transition: 0.4s cubic-bezier(0.71, -0.27, 0.06, 1.6);
            object-fit: contain;
        }

            .Image__item:hover {
                z-index: 10;
                transform: scale(2);
            }

        .table {
            width: 100%;
            max-width: 100%;
            margin-bottom: 1rem;
        }

            .table th,
            .table td {
                padding: 0.75rem;
                vertical-align: top;
                border-top: 1px solid #eceeef;
            }

            .table td {
                padding: 0.75rem;
                text-align: center;
                border-top: 1px solid #eceeef;
                inline-size: 150px;
                overflow-wrap: break-word;
                hyphens: manual;
            }

            .table thead th {
                vertical-align: bottom;
                border-bottom: 2px solid #eceeef;
            }

            .table tbody + tbody {
                border-top: 2px solid #eceeef;
            }

            .table .table {
                background-color: #fff;
            }

        .table-sm th,
        .table-sm td {
            padding: 0.3rem;
        }

        .table-bordered {
            border: 1px solid #eceeef;
        }

            .table-bordered th,
            .table-bordered td {
                border: 1px solid #eceeef;
            }

            .table-bordered thead th,
            .table-bordered thead td {
                border-bottom-width: 2px;
            }

        .table-striped tbody tr:nth-of-type(odd) {
            background-color: rgba(0, 0, 0, 0.05);
        }

        .table-hover tbody tr:hover {
            background-color: rgba(0, 0, 0, 0.075);
        }

        .table-active,
        .table-active > th,
        .table-active > td {
            background-color: rgba(0, 0, 0, 0.075);
        }

        .table-hover .table-active:hover {
            background-color: rgba(0, 0, 0, 0.075);
        }

            .table-hover .table-active:hover > td,
            .table-hover .table-active:hover > th {
                background-color: rgba(0, 0, 0, 0.075);
            }

        .table-success,
        .table-success > th,
        .table-success > td {
            background-color: #dff0d8;
        }

        .table-hover .table-success:hover {
            background-color: #d0e9c6;
        }

            .table-hover .table-success:hover > td,
            .table-hover .table-success:hover > th {
                background-color: #d0e9c6;
            }

        .table-info,
        .table-info > th,
        .table-info > td {
            background-color: #d9edf7;
        }

        .table-hover .table-info:hover {
            background-color: #c4e3f3;
        }

            .table-hover .table-info:hover > td,
            .table-hover .table-info:hover > th {
                background-color: #c4e3f3;
            }

        .table-warning,
        .table-warning > th,
        .table-warning > td {
            background-color: #fcf8e3;
        }

        .table-hover .table-warning:hover {
            background-color: #faf2cc;
        }

            .table-hover .table-warning:hover > td,
            .table-hover .table-warning:hover > th {
                background-color: #faf2cc;
            }

        .table-danger,
        .table-danger > th,
        .table-danger > td {
            background-color: #f2dede;
        }

        #search_field {
            width: 500px;
        }

        .search_field {
            padding: 0px;
            width: 300px;
        }

        .table-hover .table-danger:hover {
            background-color: #ebcccc;
        }

            .table-hover .table-danger:hover > td,
            .table-hover .table-danger:hover > th {
                background-color: #ebcccc;
            }

        .thead-inverse th {
            color: #fff;
            background-color: #292b2c;
        }

        #Search__container {
            display: flex;
            gap: 3px;
            align-items: center;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .thead-default th {
            color: #464a4c;
            background-color: #eceeef;
        }

        .table-inverse {
            color: #fff;
            background-color: #292b2c;
        }

            .table-inverse th,
            .table-inverse td,
            .table-inverse thead th {
                border-color: #fff;
            }

            .table-inverse.table-bordered {
                border: 0;
            }

        .table-responsive {
            display: block;
            width: 100%;
            overflow-x: auto;
            -ms-overflow-style: -ms-autohiding-scrollbar;
        }

            .table-responsive.table-bordered {
                border: 0;
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
          .red{
             background-color:red;
             color:white;
             padding: 0px 3px;
             border-radius: 5px;
          }
          .green{
             background-color:green;
             color:white;
             padding: 0px 3px;
             border-radius: 5px;
          }
    </style>
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


            <!-- SIDEBAR END -->

            <!-- MAIN START -->
            <main class="lg:ml-[220px] w-full min-h-screen">
                <!-- HEADER START -->
                <header class="bg-slate-900">
                    <div class="flex justify-between items-center md:px-8 px-4 border-b border-gray-700">
                        <a href="home.aspx" class="py-4 md:text-2xl text-sm font-bold capitalize text-white"><i class="fa-solid fa-arrow-left md:text-lg text-sm md:pr-4 pr-2"></i>Recherche phonetique</a>
                        <a href="home.aspx" class="flex items-center">
                            <img src="../assets/img/IP_LOGO_FULL.png" alt="logo" class="md:h-8 h-6">
                        </a>
                       <%-- <button onclick="displayToggle(sidebar)" class="lg:hidden block md:text-2xl text-xl text-white">
                            <i onclick="displayToggle(openSidebar); displayToggle(closeSidebar)" id="openSidebar" class="fa-solid fa-bars"></i>
                            <i onclick="displayToggle(openSidebar); displayToggle(closeSidebar)" id="closeSidebar" class="hidden fa-solid fa-xmark"></i>
                        </button>--%>
                    </div>
                </header>
                <!-- HEADER END -->

                <div class="p-6">
                    <div class="rounded border border-gray-400">
                        <!-- FORM START -->
                        <section class="p-4">

                            <div class="flex flex-wrap md:mt-4">
                                <div class="md:basis-1/2 basis-full">
                                    <div class="mb-4 md:px-2">
                                        <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
                                            <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Numéro de publication :</legend>
                                            <input type="text" runat="server" class="lg:w-4/5 w-full md:text-base text-sm bg-transparent border-b border-gray-500" id="num_publication" />
                                            <asp:CustomValidator ID="NumPubValidator" runat="server" ErrorMessage="Numéro de publication." ControlToValidate="num_publication" Display="Dynamic" ForeColor="#CC3300" OnServerValidate="NumPubValidator_ServerValidate" ValidateEmptyText="true">Numéro de publication.</asp:CustomValidator>

                                        </fieldset>
                                    </div>

                                    <div class="mb-4 md:px-2">
                                        <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">

                                            <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Méthodes :</legend>
                                            <div class="flex flex-wrap items-center">

                                                <div>
                                                    <label class="pr-2 lg:text-base text-sm font-medium" for="Soundex">Soundex :</label>
                                                    <input type="checkbox" name="Soundex" runat="server" class="md:text-base text-sm bg-transparent border-b border-gray-500" value="Soundex" id="Soundex" />
                                                </div>

                                                <div class="pl-2">
                                                    <label class="pr-2 lg:text-base text-sm font-medium" for="checkbox2">Contains: </label>
                                                    <input type="checkbox" name="Contains" runat="server" class="md:text-base text-sm bg-transparent border-b border-gray-500" value="Contains" id="Contains" />
                                                </div>

                                                <div class="pl-2">
                                                    <label class="pr-2 lg:text-base text-sm font-medium" for="checkbox3">Paramétre: </label>
                                                    <input type="checkbox" name="Parametre" runat="server" class="md:text-base text-sm bg-transparent border-b border-gray-500" value="Parametre" id="Parametre" />
                                                </div>
                                                <div class="pl-2">
                                                    <label class="pr-2 lg:text-base text-sm font-medium" for="checkbox4">Différence: </label>
                                                    <input type="checkbox" name="Difference" runat="server" class="md:text-base text-sm bg-transparent border-b border-gray-500" value="Difference" id="Difference" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <input type="text" hidden="hidden" name="options" class="form-control" runat="server" id="options" />
                                <asp:CustomValidator ID="Validator" runat="server" ErrorMessage="Cocher au moins une case." ControlToValidate="options" Display="Dynamic" ForeColor="#CC3300" OnServerValidate="Validator_ServerValidate" ValidateEmptyText="true">Cocher au moins une case.</asp:CustomValidator>



                                <div class="md:basis-1/2 basis-full">
                                    <div class="mb-4 md:px-2">
                                        <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
                                            <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Importer Ip Report :</legend>
                                            <input type="file" name="IpreportUpload" class="lg:w-4/5 w-full md:text-base text-sm bg-transparent" runat="server" id="IpreportUpload" />
                                            <asp:CustomValidator ID="IpreportUploadValidator" runat="server" ErrorMessage="Importer le document ip report ou le fichier excel." ControlToValidate="IpreportUpload" Display="Dynamic" ForeColor="#CC3300" OnServerValidate="IpreportUploadValidator_ServerValidate" ValidateEmptyText="true">Importer le document ip report ou le fichier excel.</asp:CustomValidator>
                                        </fieldset>
                                    </div>


                                    <div class="mb-4 md:px-2">
                                        <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
                                            <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Importer Portefeuille :</legend>
                                            <input type="file" name="PortefeuilleUpload" class="lg:w-4/5 w-full md:text-base text-sm bg-transparent" runat="server" id="PortefeuilleUpload" />
                                            <asp:CustomValidator ID="PortefeuilleUploadValidator" runat="server" ErrorMessage="Importer le portefeuille." ControlToValidate="PortefeuilleUpload" Display="Dynamic" ForeColor="#CC3300" OnServerValidate="PortefeuilleUploadValidator_ServerValidate" ValidateEmptyText="true">Importer le portefeuille.</asp:CustomValidator>
                                        </fieldset>
                                    </div>

                                    <div class="mb-4 md:px-2">
                                        <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
                                            <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Importer images gazette :</legend>
                                            <asp:FileUpload class="lg:w-4/5 w-full md:text-base text-sm bg-transparent" runat="server" AllowMultiple="true" ID="ImagesgazetteUpload" />
                                        </fieldset>
                                    </div>

                                </div>

                                <div class="md:basis-1/2 basis-full">
                                    <div class="md:px-2">
                                        <asp:Button class="md:px-8 px-4 md:text-base text-sm mt-2 py-2 rounded-md font-medium text-white bg-sky-500 hover:bg-sky-600 sl-animated" ID="Filtrer" runat="server" Text="Filtrer" OnClick="Filtrer_Click" />
                                        <asp:Button class="md:px-8 px-4 md:text-base text-sm mt-2 py-2 rounded-md font-medium text-white bg-sky-500 hover:bg-sky-600 sl-animated" ID="MaxFiltrer" runat="server" Text="MaxFiltrer" OnClick="MaxFiltrer_Click" />
                                        <asp:Button class="mt-2 md:px-8 px-4 md:text-base text-sm mt-2 py-2 rounded-md font-medium text-white bg-sky-500 hover:bg-sky-600 sl-animated" ID="Downloadipreport" runat="server" Text="Telecharger ipreport(Excel)" OnClick="Downloadipreport_Click" CausesValidation="false" />
                                        <asp:Button class="mt-2 md:px-8 px-4 md:text-base text-sm mt-2 py-2 rounded-md font-medium text-white bg-sky-500 hover:bg-sky-600 sl-animated" ID="Classe_commun" runat="server" Text="Résultat ayant Cl com" OnClick="Classe_commun_Click" CausesValidation="false" Style="margin-top: 5px" />
                                        <asp:Button class="mt-2 md:px-8 px-4 md:text-base text-sm mt-2 py-2 rounded-md font-medium text-white bg-sky-500 hover:bg-sky-600 sl-animated" ID="Reinitialiser" runat="server" Text="Réinitialiser" OnClick="Reinitialiser_Click" CausesValidation="false" />
                                    </div>

                                </div>
                            </div>
                            <br />


                            <div class="container___Result">
                                <p>Resultat :</p>

                            </div>

                            <div id="Result___">
                               
                                <asp:GridView ShowHeaderWhenEmpty="True" HeaderStyle-BackColor="#0EA5E9" class="table lg:w-full w-max" ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">

                                    <Columns>

                                        <asp:BoundField HeaderText="Détails ip report marque" DataField="DetailsMarqueIpReport" HtmlEncode="false" ItemStyle-Width="400">

                                            <ItemStyle Width="400px"></ItemStyle>

                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Ip report marque" DataField="MarqueIpReport" HtmlEncode="false" ItemStyle-Width="200">

                                            <ItemStyle Width="200px"></ItemStyle>

                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Image ipreport marque">
                                            <ItemTemplate>
                                                <asp:Image CssClass="Image__item" Height="90" Width="90" runat="server" ImageUrl='<%# "~/Assets/Brand_image/"+Eval("ImageMarqueIpReport").ToString() %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Image marque similaire">
                                            <ItemTemplate>
                                                <asp:Image CssClass="Image__item" Height="90" Width="90" runat="server" ImageUrl='<%# "~/Assets/Brand_image/"+Eval("ImageMarqueSimilaire").ToString() %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Portefeuille marque similaire" DataField="MarqueSimilaire" HtmlEncode="false" ItemStyle-Width="200">

                                            <ItemStyle Width="200px"></ItemStyle>

                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Détails marque similaire" DataField="DetailsMarqueSimilaire" HtmlEncode="false" ItemStyle-Width="400">


                                            <ItemStyle Width="400px"></ItemStyle>


                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="Generate_doc" CssClass="bg-green-500 py-2 px-3" runat="server" Text="Générer Alerte" OnClick="Generate_doc_Click" />
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:BoundField DataField="crystalIpReport" />
                                        <asp:BoundField DataField="crystalMarqueSimilaire" />

                                    </Columns>

                                    <HeaderStyle Font-Size="10pt" Font-Names="Arial" />
                                </asp:GridView>
                                 <div id="Pagination___">
                                    <asp:ImageButton ID="ImageButton1" runat="server" OnClick="Precedent_Click" Height="30px" ImageUrl="~/Assets/icon/back-removebg-preview.ico" Width="30px" />
                                    <asp:Label ID="index" runat="server" Text="Label">1 / 1</asp:Label>
                                    <asp:ImageButton ID="ImageButton2" runat="server" OnClick="Suivant_Click" Height="30px" ImageUrl="~/Assets/icon/next.png" Width="30px" />
                                </div>
                            </div>
                        </section>
                    </div>

                </div>
            </main>
        </div>

    </form>

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
            <script src="../assets/js/app.js"></script>

</body>
</html>
