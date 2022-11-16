<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Recherche ompic.aspx.cs" Inherits="Opposition_Generateur.Views.Recherche_ompic" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<!DOCTYPE html>
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <!-- FAVICON -->
    <link rel="icon" href="../assets/images/IP_LOGO_BLACK.png">
    <title>IP Platform</title>

    <!-- GOOGLE FONT LINK -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet">

    <!-- FONT AWESOME -->
    <script src="https://kit.fontawesome.com/272eed3989.js" crossorigin="anonymous"></script>

    <!-- TAILWINDCSS OUTPUT -->
    <link href="../assets/css/output.css" rel="stylesheet">
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
            <main class="lg:ml-[220px] w-full min-h-screen ">
                <!-- HEADER START -->
                <header class="bg-slate-900">
                    <div class="flex justify-between items-center md:px-8 px-4 border-b border-gray-700">
                        <a href="home.aspx" class="py-4 md:text-2xl text-sm font-bold capitalize text-white"><i class="fa-solid fa-arrow-left md:text-lg text-sm md:pr-4 pr-2"></i>Recherche ompic</a>
                        <a href="home.aspx" class="flex items-center">
                            <img src="../assets/img/IP_LOGO_FULL.png" alt="logo" class="md:h-8 h-6">
                        </a>
                     <%--   <button onclick="displayToggle(sidebar)" class="lg:hidden block md:text-2xl text-xl text-white">
                            <i onclick="displayToggle(openSidebar); displayToggle(closeSidebar)" id="openSidebar" class="fa-solid fa-bars"></i>
                            <i onclick="displayToggle(openSidebar); displayToggle(closeSidebar)" id="closeSidebar" class="hidden fa-solid fa-xmark"></i>
                        </button>--%>
                    </div>
                </header>
                <!-- HEADER END -->
                <!-- MAIN CONTAINER START -->
                <div class="p-6">
                    <div class="p-4 rounded border border-gray-400">
                        <!-- FORM START -->
                        <section>
                            <h4 class="md:text-xl text-lg font-bold">Remplie ces champs par des valeurs valides: </h4>


                            <div class="flex flex-wrap mt-4">
                                <div class="md:basis-1/2 basis-full">
                                    <div class="mb-4 md:px-2">
                                        <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
                                            <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Nom de la marque :</legend>
                                            <input type="text" class="lg:w-4/5 w-full md:text-base text-sm bg-transparent border-b border-gray-500" name="nom_marq" id="nom_marq" />
                                            <div class="flex items-center flex-wrap mt-2">
                                                <label class="pr-2 lg:text-base text-sm" for="Rech_exacte">Exacte :</label>
                                                <input type="checkbox" name="Rech_exacte" class="Rech_type form-check-input" value="Exacte" id="Rech_exacte" onclick="Func(this,'Rech_type')" />
                                                <label class="lg:pl-4 pr-2 lg:text-base text-sm" for="Rech_phonetique">Phonétique :</label>
                                                <input type="checkbox" name="Rech_phonetique" class="Rech_type form-check-input" value="Phonétique" id="Rech_phonetique" onclick="Func(this,'Rech_type')" />
                                            </div>

                                        </fieldset>
                                    </div>


                                    <div class="mb-4 md:px-2">
                                        <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
                                            <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Classification de Nice :</legend>
                                            <input type="text" class="lg:w-4/5 w-full md:text-base text-sm bg-transparent border-b border-gray-500" name="classe_nice" id="classe_nice" />
                                        </fieldset>
                                    </div>


                                    <div class="mb-4 md:px-2">
                                        <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
                                            <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Numéro de dépôt :</legend>
                                            <input type="text" class="lg:w-4/5 w-full md:text-base text-sm bg-transparent border-b border-gray-500" runat="server" name="num_depot" id="num_depot" />
                                            <asp:CustomValidator ID="Num_depot_Validator" runat="server" ErrorMessage="Numéro de dépot non valide." ControlToValidate="num_depot" Display="Dynamic" ForeColor="#CC3300" OnServerValidate="Num_depot_Validator_ServerValidate" ValidateEmptyText="false">Numéro de dépot non valide.</asp:CustomValidator>
                                        </fieldset>
                                    </div>


                                    <div class="mb-4 md:px-2">
                                        <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
                                            <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Date de dépôt début :</legend>
                                            <input type="text" class="lg:w-4/5 w-full md:text-base text-sm bg-transparent border-b border-gray-500" runat="server" name="date_depot_debut" id="date_depot_debut" />
                                            <asp:CustomValidator ID="Date_depot_debut_Validator" runat="server" ErrorMessage="Date de dépot début non valide." ControlToValidate="date_depot_debut" Display="Dynamic" ForeColor="#CC3300" OnServerValidate="Date_depot_debut_Validator_ServerValidate" ValidateEmptyText="false">Date de dépot début non valide.</asp:CustomValidator>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="md:basis-1/2 basis-full">
                                    <div class="mb-4 md:px-2">
                                        <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
                                            <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Date de dépôt fin :</legend>
                                            <input type="text" class="lg:w-4/5 w-full md:text-base text-sm bg-transparent border-b border-gray-500" runat="server" name="date_depot_fin" id="date_depot_fin" />
                                            <asp:CustomValidator ID="Date_depot_fin_Validator" runat="server" ErrorMessage="Date de dépot fin non valide." ControlToValidate="date_depot_fin" Display="Dynamic" ForeColor="#CC3300" OnServerValidate="Date_depot_fin_Validator_ServerValidate" ValidateEmptyText="false">Date de dépot fin non valide.</asp:CustomValidator>
                                        </fieldset>
                                    </div>

                                    <div class="mb-4 md:px-2">
                                        <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
                                            <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Etat de la marque :</legend>

                                            <select class="w-full py-1 px-2 md:text-base text-sm bg-transparent border-b border-gray-500" name="etatMarque" id="etatMarque">
                                                <option value=""></option>
                                                <option value="EN INSTANCE DE REGULARISATION">EN COURS D'EXAMEN</option>
                                                <option value="EXPIREE">EXPIREE</option>
                                                <option value="OPPOSITION EN COURS">OPPOSITION</option>
                                                <option value="RADIEE">RADIEE</option>
                                                <option value="RENOUVELEE">RENOUVELEE</option>
                                                <option value="RENONCEE">RENONCEE</option>
                                                <option value="PUBLIEE">PUBLIEE</option>
                                                <option value="ENREGISTREE">ENREGISTREE</option>
                                                <option value="REJETEE">REJETEE</option>
                                            </select>
                                        </fieldset>
                                    </div>


                                    <div class="mb-4 md:px-2">
                                        <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
                                            <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Numéro de priorité :</legend>
                                            <input type="text" class="lg:w-4/5 w-full md:text-base text-sm bg-transparent border-b border-gray-500" runat="server" name="numero_priorite" id="numero_priorite" />
                                            <asp:CustomValidator ID="Num_priorite_Validator" runat="server" ErrorMessage="Numéro priorité non valide." ControlToValidate="numero_priorite" Display="Dynamic" ForeColor="#CC3300" OnServerValidate="Num_priorite_Validator_ServerValidate" ValidateEmptyText="false">Numéro priorité non valide.</asp:CustomValidator>
                                        </fieldset>
                                    </div>

                                    <div class="mb-4 md:px-2">
                                        <fieldset class="border border-sky-500 rounded px-3 pt-1 pb-3">
                                            <legend class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500">Titulaire :</legend>

                                            <input type="text" class="lg:w-4/5 w-full md:text-base text-sm bg-transparent border-b border-gray-500" name="titulaire" id="titulaire" />
                                        </fieldset>
                                    </div>

                                </div>
                            </div>
                        </section>
                    </div>
                    <asp:Button ID="Search" class="md:px-8 px-4 md:text-base text-sm mt-2 py-2 rounded-md font-medium text-white bg-sky-500 hover:bg-sky-600 sl-animated" runat="server" Text="Chercher" OnClick="Search_Click" />
                </div>

                <br />
                <br />

                <div class="container___Result">
                    <p>Resultat :</p>

                </div>



                <div id="Result___">
                    <div id="Pagination___" style="margin-right: 35px">
                        <asp:ImageButton ID="Precedent1" runat="server" OnClick="Precedent_Click" Height="30px" ImageUrl="~/Assets/icon/back-removebg-preview.ico" Width="30px" />
                        <asp:Label ID="index1" runat="server" Text="Label">1 / 1</asp:Label>
                        <asp:ImageButton ID="Suivant1" runat="server" OnClick="Suivant_Click" Height="30px" ImageUrl="~/Assets/icon/next.png" Width="30px" />
                    </div>

                    <asp:GridView OnSorting="GridView1_Sorting" AllowSorting="True" ShowHeaderWhenEmpty="True" HeaderStyle-BackColor="#0EA5E9" class="table lg:w-full w-max" ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" OnDataBound="GridView1_DataBound">

                        <Columns>
                            <asp:BoundField HeaderText="Id" DataField="Id" SortExpression="Id" />
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image CssClass="Image__item" Height="90" Width="90" runat="server" ImageUrl='<%# "~/Assets/Brand_image/"+Eval("Image").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Nom Marque" DataField="NomMarque" SortExpression="NomMarque" />
                            <asp:BoundField HeaderText="Numero Titre" DataField="NumeroTitre" />
                            <asp:BoundField HeaderText="Date Depot" DataField="DateDepot" SortExpression="DateDepot" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField HeaderText="Etat" DataField="Etat" SortExpression="Etat" />
                            <asp:BoundField HeaderText="Titulaire" DataField="Titulaire" SortExpression="Titulaire" />
                            <asp:BoundField HeaderText="Date Expiration" DataField="DateExpiration" SortExpression="DateExpiration" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField HeaderText="Classe nice" DataField="Classe_nice" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Button ID="Visit_link" CssClass="bg-green-500 py-2 px-3" runat="server" Text="Visiter" OnClick="Visit_link_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <HeaderStyle Font-Size="10pt" Font-Names="Arial" />
                    </asp:GridView>
                </div>
            </main>
        </div>
    </form>

    <script type="text/javascript">
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
    <!-- SCRIPT LINK -->
           <script src="../assets/js/app.js"></script>

</body>
</html>
