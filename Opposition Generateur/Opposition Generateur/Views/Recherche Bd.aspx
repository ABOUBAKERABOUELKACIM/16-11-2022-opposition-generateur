<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recherche Bd.aspx.cs" Inherits="Opposition_Generateur.Views.Recherche_Bd" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<!DOCTYPE html>


<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <script src="JS/jquery-1.11.1.js" type="text/javascript">  
    </script>
    <link href="https://fonts.googleapis.com/css2?family=Roboto:ital,wght@1,300&display=swap" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins&family=Roboto:ital,wght@1,300&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.min.js" integrity="sha384-QJHtvGhmr9XOIpI6YVutG+2QOK9T+ZnN4kzFN1RtK3zEFEIsxhlmWl5/YESvpZ13" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <!-- FAVICON -->
    <link rel="icon" href="../assets/images/IP_LOGO_BLACK.png">

    <!-- GOOGLE FONT LINK -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet">

    <!-- FONT AWESOME -->
    <script src="https://kit.fontawesome.com/272eed3989.js" crossorigin="anonymous"></script>

    <!-- TAILWINDCSS OUTPUT -->
    <link href="../assets/css/output.css" rel="stylesheet">
    <title>Recherche Base de données</title>
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

            .table th {
                text-align: center;
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
                           <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="Button2" Text="Validation Phonetique" OnClick="btn_strongvalid_Click"  />
                            <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="Button3" Text="Validation des conflits" OnClick="btn_validation_Click" />
                            <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="Button4" Text="Notification des conflits" OnClick="btn_notification_Click" />
                            <asp:Button CssClass="flex justify-between items-center w-full p-2 mt-1 rounded text-xs capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-900 focus:text-white sl-animated" runat="server" ID="Button5" Text="Gestion des conflits" OnClick="Bnt_gestion_Click" />

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
                        <asp:Button CssClass="flex justify-between items-center w-full md:py-3 md:px-2 p-2 mt-1 rounded text-sm capitalize text-left font-bold text-slate-300 hover:bg-gray-800 hover:text-white focus:bg-gray-700 focus:text-white sl-animated" runat="server" ID="Button6" Text="Archive" OnClick="Archive_Click" /></li>
                    <li>
                </ul>

            </div>

            <!-- SIDEBAR END -->
            <main id="main" class="lg:ml-[220px] w-full min-h-screen ">
                <!-- HEADER START -->
                <header class="bg-slate-900">
                    <div class="flex justify-between items-center md:px-8 px-4 border-b border-gray-700">
                        <a href="home.aspx" class="py-4 md:text-2xl text-sm font-bold capitalize text-white"><i class="fa-solid fa-arrow-left md:text-lg text-sm md:pr-4 pr-2"></i>recherche bd</a>
                        <a href="home.aspx" class="flex items-center">
                            <img src="../assets/img/IP_LOGO_FULL.png" alt="logo" class="md:h-8 h-6">
                        </a>

                    </div>
                    <nav>
                        <h4 class="md:px-8 px-4 pt-3 pb-2 md:text-base text-sm font-semibold text-center capitalize text-sky-500">Recherche Base de donnees</h4>
                    </nav>
                </header>
                <!-- HEADER END -->

                <!-- MAIN CONTAINER START -->
                <div class="p-6">
                    <div class="rounded border border-gray-400">
                        <div class="p-4">
                            <!-- FORM START -->
                            <section>
                                <h4 class="md:text-xl text-lg font-bold">Remplie ces champs par des valeurs valides: </h4>



                                <div id="Bd_searach_params" class="container-fluid">
                                    <p>Remplie ces champs par des valeurs valides.</p>

                                    <label for="ShowOrHide">Afficher/Masquer les champs.</label>
                                    <input type="checkbox" id="ShowOrHide" onclick="func()" name="ShowOrHide" class="form-check-input" />
                                    <div class="row">

                                        <div class="col-12 col-md-6 col-lg-6" style="display: flex; flex-direction: column;">
                                            <label for="nom_marq">Nom de la marque :</label>
                                            <input type="text" name="nom_marq" class="form-control" id="nom_marq" />
                                            <br />
                                            <label for="deposant">Déposant :</label>
                                            <input type="text" name="deposant" class="form-control" id="deposant" />
                                            <div class="hidden_search_field">
                                                <br />

                                                <label for="adresse_deposant">Adresse deposant :</label>
                                                <input type="text" name="adresse_deposant" class="form-control" id="adresse_deposant" />
                                                <br />
                                                <label for="Pays_deposant">Pays deposant :</label>
                                                <input type="text" name="Pays_deposant" class="form-control" id="Pays_deposant" />

                                                <br />


                                                <label for="date_depot_debut">Date dépot début :</label>
                                                <input type="date" name="date_depot_debut" class="form-control" id="date_depot_debut" />
                                                <br />
                                                <label for="date_exp_debut">Date expiration début :</label>
                                                <input type="date" name="date_exp_debut" class="form-control" id="date_exp_debut" />

                                                <br />
                                                <label for="OppositionNbrMin">Opposition(Minimum) :</label>
                                                <input type="text" name="OppositionNbrMin" class="form-control" id="OppositionNbrMin" />
                                                <br />
                                                <div style="display: flex; gap: 10px;">
                                                    <div>
                                                        <label for="type_marqueOmpic">Type marque(Ompic) :</label>
                                                        <select name="type_marqueOmpic" id="type_marqueOmpic" class="w-full py-1 px-2 md:text-base text-sm bg-transparent border-b border-gray-500">
                                                            <option value=""></option>
                                                            <option value="autres">Autres</option>
                                                            <option value="mixte">Mixte</option>
                                                            <option value="figuratif">Figuratif</option>
                                                            <option value="dénominatif">Denominatif</option>
                                                            <option value="tridimensionnel">Tridimensionnel</option>
                                                            <option value="sonore">Sonore</option>
                                                        </select>
                                                    </div>
                                                    <div>
                                                        <label for="type_marqueTm">Type marque(Tm) :</label>
                                                        <select name="type_marqueTm" id="type_marqueTm" class="w-full py-1 px-2 md:text-base text-sm bg-transparent border-b border-gray-500">
                                                            <option value=""></option>
                                                            <option value="combined">Combined</option>
                                                            <option value="sound">Sound</option>
                                                            <option value="other">Other</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <br />
                                                <div style="display: flex; gap: 10px;">
                                                    <div>
                                                        <label for="etatMarqueOmpic">Etat(Ompic) :</label>
                                                        <select name="etatMarqueOmpic" id="etatMarqueOmpic" class="w-full py-1 px-2 md:text-base text-sm bg-transparent border-b border-gray-500">
                                                            <option value=""></option>
                                                            <option value="opposition">OPPOSITION</option>
                                                            <option value="dechue">DECHUE</option>
                                                            <option value="irrecevable">IRRECEVABLE</option>

                                                            <option value="en cours d'examen">EN COURS D'EXAMEN</option>
                                                            <option value="opposition suspendue">OPPOSITION SUSPENDUE</option>
                                                            <option value="rejetee">REJETEE</option>

                                                            <option value="retiree">RETIREE</option>
                                                            <option value="expiree">EXPIREE</option>
                                                            <option value="renouvlee">RENOUVELEE</option>

                                                            <option value="enregistree">ENREGISTREE</option>
                                                            <option value="en examen de forme">EN EXAMEN DE FORME</option>

                                                            <option value="consideree comme retiree">CONSIDEREE COMME RETIREE</option>
                                                            <option value="opposition en cours">OPPOSITION EN COURS</option>
                                                            <option value="renoncee">RENONCEE</option>

                                                            <option value="publication programmee">PUBLICATION PROGRAMMEE</option>
                                                            <option value="en instance de regularisation">EN INSTANCE DE REGULARISATION</option>
                                                            <option value="publiee">PUBLIEE</option>

                                                            <option value="en examen des motifs absolus">EN EXAMEN DES MOTIFS ABSOLUS</option>
                                                            <option value="radiee">RADIEE</option>
                                                            <option value="en poursuite de procedure">EN POURSUITE DE PROCEDURE</option>
                                                        </select>
                                                    </div>
                                                    <div>
                                                        <label for="etatMarqueTm">Etat(Tm) :</label>
                                                        <select name="etatMarqueTm" id="etatMarqueTm" class="w-full py-1 px-2 md:text-base text-sm bg-transparent border-b border-gray-500">
                                                            <option value=""></option>
                                                            <option value="registered">Registered</option>
                                                            <option value="registration cancelled">Registration cancelled</option>
                                                            <option value="application opposed">Application opposed</option>
                                                            <option value="registration surrendered">Registration surrendered</option>
                                                            <option value="application refused">Application refused</option>
                                                            <option value="expired">Expired</option>
                                                            <option value="application withdrawn">Application withdrawn</option>
                                                            <option value="application filed">Application filed</option>
                                                            <option value="application published">Application published</option>

                                                            <option value="appeal pending">Appeal pending</option>
                                                            <option value="renewed">Renewed</option>

                                                        </select>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-12 col-md-6 col-lg-6" style="display: flex; flex-direction: column;">
                                            <label for="num_marq">Numero de marque :</label>
                                            <input type="text" name="num_marq" class="form-control" id="num_marq" />
                                            <br />


                                            <label for="mandataire">Mandataire :</label>
                                            <input type="text" name="mandataire" class="form-control" id="mandataire" />

                                            <div class="hidden_search_field">
                                                <br />
                                                <label for="adresse_mandataire">Adresse mandataire :</label>
                                                <input type="text" name="adresse_mandataire" class="form-control" id="adresse_mandataire" />
                                                <br />
                                                <label for="Pays_mandataire">Pays mandataire :</label>
                                                <input type="text" name="Pays_mandataire" class="form-control" id="Pays_mandataire" />
                                                <br />
                                                <label for="date_depot_fin">Date dépot fin :</label>
                                                <input type="date" name="date_depot_fin" class="form-control" id="date_depot_fin" />
                                                <br />
                                                <label for="date_exp_fin">Date expiration fin :</label>
                                                <input type="date" name="date_exp_fin" class="form-control" id="date_exp_fin" />
                                                <br />

                                                <label for="email">Email :</label>
                                                <input type="text" name="email" class="form-control" id="email" />
                                                <br />
                                                <label for="Classe_nice">Classe nice :</label>
                                                <input type="text" name="Classe_nice" class="form-control" id="Classe_nice" />
                                                <br />
                                                <label for="Da_opposition">Droit antérieure opposition :</label>
                                                <input type="text" name="Da_opposition" class="form-control" id="Da_opposition" />
                                                <br />
                                                <label for="Opposant">Opposant :</label>
                                                <input type="text" name="Opposant" class="form-control" id="Opposant" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Button class="mt-2 md:px-8 px-4 md:text-base text-sm mt-2 py-2 rounded-md font-medium text-white bg-sky-500 hover:bg-sky-600 sl-animated" ID="Search" runat="server" Text="Chercher" OnClick="Search_Click" />
                                    &nbsp;
                                    <asp:Button class="mt-2 md:px-8 px-4 md:text-base text-sm mt-2 py-2 rounded-md font-medium text-white bg-sky-500 hover:bg-sky-600 sl-animated" ID="Button1" runat="server" Text="Recherche pho" OnClick="Searchpheo_Click" />
                                </div>

                                <br />
                                <br />

                                <label class="md:text-sm text-[11px] px-1 tracking-wide font-semibold text-sky-500" for="columns_dropdown">Afficher / Masquer Colonne :</label>
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


                                <asp:Button class="mt-2 md:px-8 px-4 md:text-base text-sm mt-2 py-2 rounded-md font-medium text-white bg-sky-500 hover:bg-sky-600 sl-animated" ID="Afficher_Masquer" runat="server" Text="Afficher/Masquer" OnClick="Afficher_Masquer_Click" /><br />

                                <asp:ImageButton ID="ImageButton3" runat="server" OnClick="Export_pdf_Click" ImageUrl="../Assets/icon/pdf.png" Height="50px" Width="50px" />
                                &nbsp;        
                                <asp:ImageButton ID="ImageButton4" runat="server" OnClick="SELECT_pdf_Click" ImageUrl="~/Assets/icon/pdf-select.png" Height="50px" Width="50px" />
                                &nbsp;<asp:ImageButton ID="ImageButton5" runat="server" OnClick="Button1_Click" ImageUrl="~/Assets/icon/upload.png" alt="PDF SELECT" Height="50px" Width="50px" />
                                &nbsp;<asp:ImageButton ID="ImageButton7" runat="server" OnClick="Update_Click" ImageUrl="~/Assets/icon/select_upload.png" alt="PDF SELECT" Height="50px" Width="50px" />
                                &nbsp;<asp:ImageButton ID="ImageButton6" runat="server" OnClick="Alert_gen" ImageUrl="../Assets/icon/thumbnail_icon-ipp-alerte-fr.png" alt="PDF SELECT" Height="50px" Width="50px" Style="padding-bottom: 5px" />
                                &nbsp;<asp:ImageButton ID="ImageButton8" runat="server" OnClick="Alert_gen_eng" ImageUrl="../Assets/icon/thumbnail_icon-ipp-alerte-eng.png" alt="PDF SELECT" Height="50px" Width="50px" Style="padding-bottom: 5px" />







                                &nbsp;<div class="container___Result">

                                    <div class="row">
                                        <div class="col-1">Resultat :</div>
                                        <div class="col-3" runat="server" id="resultalbl"></div>
                                    </div>
                                    <div id="Result___">
                                        <div id="Pagination___">
                                            <asp:ImageButton ID="ImageButton1" runat="server" OnClick="Precedent_Click" Height="30px" ImageUrl="~/Assets/icon/back-removebg-preview.ico" Width="30px" />
                                            <asp:Label ID="index" runat="server" Text="Label">1 / 1</asp:Label>
                                            <asp:ImageButton ID="ImageButton2" runat="server" OnClick="Suivant_Click" Height="30px" ImageUrl="~/Assets/icon/next.png" Width="30px" />
                                        </div>
                                    </div>
                                </div>
                                <asp:GridView ShowHeaderWhenEmpty="True" class="table lg:w-full w-max" ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" ClientIDMode="Static" OnDataBound="GridView1_DataBound" HeaderStyle-BackColor="#0EA5E9" AllowSorting="True" OnSorting="GridView1_Sorting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1">

                                    <Columns>
                                        <asp:TemplateField>

                                            <HeaderTemplate>
                                                <asp:CheckBox onclick="HeaderCheckBoxClick(this);" ID="CheckBox2" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Numero marque" DataField="Numero_titre" SortExpression="Numero_titre" HtmlEncode="False" />
                                        <asp:TemplateField HeaderText="Image">
                                            <ItemTemplate>
                                                <asp:Image CssClass="Image__item" Height="90" Width="90" runat="server" ImageUrl='<%# "~/Assets/Brand_image/"+Eval("Image").ToString() %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Nom Marque" DataField="Nom_marque" HtmlEncode="False" SortExpression="Nom_Marque" />
                                        <asp:BoundField HeaderText="Deposant" DataField="Applicant_name" HtmlEncode="False" SortExpression="Deposant" />
                                        <asp:BoundField HeaderText="Mandataire" DataField="Representative_name" HtmlEncode="False" SortExpression="Mandataire" />
                                        <asp:BoundField HeaderText="Date depot" DataField="Date_depot" HtmlEncode="False" SortExpression="Date_depot" />
                                        <asp:BoundField HeaderText="Date expiration" DataField="Date_expiration" HtmlEncode="False" SortExpression="Date_expiration" />
                                        <asp:BoundField HeaderText="Classe nice" DataField="ClasseNice" HtmlEncode="false" />
                                        <asp:BoundField HeaderText="Statut marque" DataField="Statut" HtmlEncode="False" SortExpression="Statut" />

                                        <asp:BoundField HeaderText="Adresse Deposant" DataField="Applicant_address" HtmlEncode="False" />
                                        <asp:BoundField HeaderText="Pays Deposant" DataField="Pays" HtmlEncode="false" />

                                        <asp:BoundField HeaderText="Adresse Mandataire" DataField="Representative_address" HtmlEncode="False" />
                                        <asp:BoundField HeaderText="Pays Mandataire" DataField="Representative_countryCode" HtmlEncode="False" />
                                        <asp:BoundField HeaderText="Type marque" DataField="Type" HtmlEncode="False" />

                                        <asp:BoundField HeaderText="Email" DataField="Email" HtmlEncode="False" />
                                        <asp:BoundField HeaderText="Telephone" DataField="Telephone" HtmlEncode="False" />

                                        <asp:BoundField HeaderText="Opposant" DataField="Opposition_applicant_name" HtmlEncode="False" />
                                        <asp:BoundField HeaderText="DA  opposition" DataField="Opposition_earlierMark_applicationNumber" HtmlEncode="False" />
                                        <asp:BoundField HeaderText="Numopposition" DataField="Nombre_opposition" HtmlEncode="False" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="ShowDetails" CssClass="bg-green-500 py-2 px-3" runat="server" Text="Details" OnClick="ShowDetails_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>


                                    <HeaderStyle Font-Size="10pt" Font-Names="Arial" />
                                </asp:GridView>




                            </section>
                        </div>
                    </div>
                </div>
            </main>
        </div>
    </form>

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
        function openNav() {
            document.getElementById("sidebar").style.width = "250px";
            document.getElementById("main").style.marginLeft = "250px";
        }

        function closeNav() {
            document.getElementById("sidebar").style.width = "0";
            document.getElementById("main").style.marginLeft = "0";
        }
    </script>
    <script type="text/javascript" language="javascript">
        function HeaderCheckBoxClick(checkbox) {
            var gridView = document.getElementById("GridView1");
            for (i = 1; i < gridView.rows.length; i++) {
                gridView.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked
                    = checkbox.checked;
            }
        }


    </script>

    <script src="../assets/js/app.js"></script>

</body>
</html>
