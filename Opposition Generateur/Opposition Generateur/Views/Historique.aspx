<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Historique.aspx.cs" Inherits="Opposition_Generateur.Pages.Historique" %>

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

    <link rel="icon" href="../assets/images/logo/IP_LOGO_BLACK.png" />
    <title>IP Platform</title>

    <!-- GOOGLE FONT LINK -->
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet" />

    <!-- FONT AWESOME -->
    <script src="https://kit.fontawesome.com/272eed3989.js" crossorigin="anonymous"></script>

    <!-- TAILWINDCSS OUTPUT -->
    <link href="../Assets/css/output.css" rel="stylesheet" />


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

    <style>
        * {
            margin: 0px;
            padding: 0px;
            box-sizing: border-box;
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

                #page-top #container > .header > h2 {
                    font-family: 'Poppins', sans-serif;
                    text-align: center;
                    color: darkslategray;
                    background-color: #EFEEF0;
                    padding: 10px 0px;
                    border-radius: 10px;
                    font-size: 14px;
                }

        .header img {
            width: 90px;
            height: 90px;
        }

        #user_account img {
            outline: 3px solid white;
            outline-offset: 3px;
        }

        #user_account {
            background-color: #1E5095;
            width: 160px;
            height: 100vh;
            position: fixed;
            z-index: 7;
        }

        .box {
            min-width: 160px;
            height: 100vh;
        }

        #profile_pic {
            border-radius: 50%;
            display: block;
            margin: auto;
        }

        #user_account p {
            color: white;
            font-family: 'Poppins';
        }

        #user_account__username {
            font-size: 15px;
        }

        #user_account__role {
            font-size: 12px;
        }

        #user_account .btn_menu:hover {
            background-color: rgb(219,221,223, 0.4);
        }

        #user_account .btn_dropdownitem:hover {
            background-color: rgb(219,221,223, 0.4);
        }

        .btn_menu {
            width: 100%;
            border: 0px;
            outline: 0px;
            padding: 5px 0px;
            text-align: left;
            background-color: #ffffff1c;
            margin-bottom: 5px;
            border-radius: 5px;
            color: white;
            font-size: 12px;
            padding-left: 13px;
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

        .fa-sort-down {
            padding-right: 5px;
        }

        .dropdown {
            height: 0px;
            overflow-y: hidden;
        }

        .show {
            height: auto;
        }

        #btn_Deconnecter {
            width: 100%;
            border: 0px;
            outline: 0px;
            padding: 5px 0px;
            text-align: left;
            background-color: #ffffff1c;
            border-radius: 5px;
            color: white;
            font-size: 12px;
            padding-left: 13px;
            font-family: 'Poppins';
            position: absolute;
            bottom: 0;
            left: 0;
        }

        #Args {
            width: 100%;
            height: 100%;
            position: absolute;
            top: -1900px;
            left: 50%;
            transform: translateX(-50%);
            background-color: white;
            padding: 2px 5px;
            transition: cubic-bezier(0.05, 0.82, 1, 0.24);
        }

            #Args textarea {
                width: 100%;
                padding-left: 5px;
            }

        #Pagination___ {
            float: right;
            display: flex;
            gap: 3px;
            align-items: center;
            padding-right: 35px;
        }

        #Precedent {
            border: 1px solid rgb(116, 116, 116, 0.78);
            background-color: #dd7973;
            padding: 3px 5px;
            border-radius: 5px;
            font-family: 'Poppins';
            border-radius: 50%;
            font-size: 13px;
            color: white;
        }

        #Suivant {
            border: 1px solid rgb(116, 116, 116, 0.78);
            background-color: #dd7973;
            padding: 3px 5px;
            border-radius: 5px;
            font-family: 'Poppins';
            border-radius: 50%;
            color: white;
            font-size: 13px;
            width: 79px;
        }
    </style>
    <link rel="stylesheet" href="../Assets/Css/Sidemenustyle.css" />
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
                   
                </ul>

            </div>


            <!-- SIDEBAR END -->

            <main class="lg:ml-[220px] w-full min-h-screen">
                <!-- HEADER START -->
                <header class="bg-slate-900">
                    <div class="flex justify-between items-center md:px-8 px-4 border-b border-gray-700">
                        <a href="home.aspx" class="py-4 md:text-2xl text-sm font-bold capitalize text-white"><i class="fa-solid fa-arrow-left md:text-lg text-sm md:pr-4 pr-2"></i>Historique</a>
                        <a href="home.aspx" class="flex items-center">
                            <img src="../Assets/Img/IP_LOGO_FULL.png" alt="logo" class="md:h-8 h-6"/>
                        </a>
                       <%-- <button onclick="displayToggle(sidebar)" class="lg:hidden block md:text-2xl text-xl text-white">
                            <i onclick="displayToggle(openSidebar); displayToggle(closeSidebar)" id="openSidebar" class="fa-solid fa-bars"></i>
                            <i onclick="displayToggle(openSidebar); displayToggle(closeSidebar)" id="closeSidebar" class="hidden fa-solid fa-xmark"></i>
                        </button>--%>
                    </div>
                </header>
                <br />
                <div id="Pagination___">
                    <asp:ImageButton ID="ImageButton1" runat="server" OnClick="Precedent_Click" Height="30px" ImageUrl="~/Assets/icon/back-removebg-preview.ico" Width="30px" />
                    <asp:Label ID="index" runat="server" Text="Label">1 / 1</asp:Label>
                    <asp:ImageButton ID="ImageButton2" runat="server" OnClick="Suivant_Click" Height="30px" ImageUrl="~/Assets/icon/next.png" Width="30px" />
                </div>
                <asp:GridView ShowHeaderWhenEmpty="True" class="table lg:w-full w-max" ID="GridView1" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#0EA5E9" Font-Names="Arial" Font-Size="8pt">

                    <Columns>
                        <asp:BoundField HeaderText="Opposition id" DataField="Opposition_id" />
                        <asp:BoundField HeaderText="Creer par" DataField="Creer_par" />
                        <asp:BoundField HeaderText="N° Marque anterieure" DataField="N_depot_marque_anterieure" />
                        <asp:BoundField HeaderText="Nom marque" DataField="Nom_marque_anterieure" />
                        <asp:BoundField HeaderText="Deposant" DataField="Deposant_marque_anterieure" />
                        <asp:BoundField HeaderText="Nature marque" DataField="Nature_marque_anterieure" />

                        <asp:BoundField HeaderText="N° Marque contester" DataField="N_depot_marque_contester" />
                        <asp:BoundField HeaderText="Nom marque" DataField="Nom_marque_contester" />
                        <asp:BoundField HeaderText="Deposant" DataField="Deposant_marque_contester" />
                        <asp:BoundField HeaderText="Nature marque" DataField="Nature_marque_contester" />
                        <asp:BoundField HeaderText="Date" DataField="Date_creation" DataFormatString="{0:dd/MM/yyyy}" />



                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button ID="Delete" CssClass="bg-green-500 py-2 px-3" runat="server" Text="Supprimer" OnClick="Delete_Click" />
                                <asp:Button ID="Viewarg" CssClass="bg-green-500 py-2 px-3" runat="server" Text="Arguments" OnClick="Viewarg_Click" />
                                <asp:Button ID="Download" CssClass="bg-green-500 py-2 px-3" runat="server" Text="Telecharger" OnClick="Download_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <HeaderStyle Font-Size="10pt" Font-Names="Arial" />
                </asp:GridView>
                <br />
                <div id="Args" runat="server">
                    <div class="close" style="cursor: pointer; display: flex; justify-content: end; border-radius: 5px; background-color: #f1f1f1; width: 100%; margin-bottom: 5px; margin-top: 5px; padding: 3px 0px;">
                        <i class="fas fa-times" onclick="HideArgs()" style="margin-right: 5px;"></i>
                    </div>

                    <h2 class="display-6" style="font-size: 13px; margin-top: 8px; background-color: whitesmoke; font-family: 'Poppins'; padding: 5px 0px; border-radius: 5px;"><b>Cas d'identité :</b></h2>
                    <textarea id="cas_identite" style="outline: 0px;" readonly="readonly" runat="server"></textarea>
                    <h2 class="display-6" style="font-size: 13px; margin-top: 8px; background-color: whitesmoke; font-family: 'Poppins'; padding: 5px 0px; border-radius: 5px;"><b>Cas d'inclusion :</b></h2>
                    <textarea id="cas_inclusion" style="outline: 0px;" readonly="readonly" runat="server"></textarea>
                    <h2 class="display-6" style="font-size: 13px; margin-top: 8px; background-color: whitesmoke; font-family: 'Poppins'; padding: 5px 0px; border-radius: 5px;"><b>Cas de complémentarité :</b></h2>
                    <textarea id="cas_complementarite" style="outline: 0px;" readonly="readonly" runat="server"></textarea>
                    <h2 class="display-6" style="font-size: 13px; margin-top: 8px; background-color: whitesmoke; font-family: 'Poppins'; padding: 5px 0px; border-radius: 5px;"><b>Comparaison signes :</b></h2>
                    <textarea id="comp_signe" style="outline: 0px;" readonly="readonly" runat="server"></textarea>
                    <h2 class="display-6" style="font-size: 13px; margin-top: 8px; background-color: whitesmoke; font-family: 'Poppins'; padding: 5px 0px; border-radius: 5px;"><b>Appréciation générale risque confusion :</b></h2>
                    <textarea id="appre_gen" style="outline: 0px;" readonly="readonly" runat="server"></textarea>
                </div>

            </main>

        </div>

    </form>

    <script type="text/javascript">
        function HideArgs() {
            var box = document.getElementById('Args');
            box.style.top = -1900 + "px";
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
    <script src="../assets/js/app.js"></script>
</body>
</html>
