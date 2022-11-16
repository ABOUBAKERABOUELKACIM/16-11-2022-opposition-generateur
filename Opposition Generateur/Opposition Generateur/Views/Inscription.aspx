<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inscription.aspx.cs" Inherits="Opposition_Generateur.Views.Inscription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="UTF-8" />
     <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
     <meta name="viewport" content="width=device-width, initial-scale=1.0"/>

     <!-- FAVICON -->
     <link rel="icon" href="../Assets/Img/HLogo.png"/>
     <title>IP Platform</title>

     <!-- GOOGLE FONT LINK -->
     <link rel="preconnect" href="https://fonts.googleapis.com" />
     <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
     <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet" />

     <!-- FONT AWESOME -->
     <script src="https://kit.fontawesome.com/272eed3989.js" crossorigin="anonymous"></script>

     <!-- TAILWINDCSS OUTPUT -->
     <link href="../Assets/Css/output.css" rel="stylesheet" />
        
</head>

<body>
    <form id="form2" runat="server"  class="formstyle">
    <div id="error_msg" runat="server"></div>
     <main class="w-screen h-screen flex items-center justify-center gradient-light">
          <section class="md:w-[30rem] w-[95%] md:p-10 p-4 shadow-lg bg-white rounded">
               <img class="md:h-14 h-10 mx-auto mb-6" src="../Assets/Img/NewLogo.png"alt="logo"/>
               
               
 <input type="file"  runat="server" name="profile_picture" id="profile_picture" accept="image/*" />              
              <input type="text" placeholder="Username*" runat="server" id="signup_username"  class="border border-gray-300 hover:border-sky-500 focus:border-sky-500 md:py-4 py-3 md:px-3 px-2 md:text-base text-sm rounded-sm mb-4 w-full"/>     
                    <input type="password" placeholder="Password*" runat="server"  id="signup_password" class="border border-gray-300 hover:border-sky-500 focus:border-sky-500 md:py-4 py-3 md:px-3 px-2 md:text-base text-sm rounded-sm mb-4 w-full"/>
                    <asp:Button CssClass="bg-sky-500 hover:bg-sky-600 text-white block w-full md:py-4 py-3 md:text-xl text-lg font-bold rounded" ID="btn_signup" runat="server" Text="S'Inscrire" OnClick="btn_signup_Clickc"  />
               
               <p class="text-center mt-2 md:text-base text-sm">Already have an account? <a class="text-sky-500 hover:text-sky-600" href="http://localhost:62249/Views/Authentification.aspx">Login account</a></p>
          </section>
     </main>
        
        
        
        
        <%--<div id="form_auth">
        <div class="signin_signup_header" ><img src="../Assets/Img/logowhiteversion.png" id="toBeZoomedOut" width="100" height="100" style="border:0" /><b>Se connecter</b></div>
        <br />
        <div id="box">
        <div id="signin_signup">
        <div id="signin">
            <br />
            <div  class="signinrow">
                <label for="signin_username" style="padding-right:25px ;padding-top:5px " >Nom d'utilisateur :</label>
        <input type="text" runat="server" name="username" id="signin_username" size="25" />

            </div>
        
        <br />
            <br />
        <div class="signinrow">
            <label for="signin_password" style="padding-right:42px ;padding-top:5px">Mot de passe :</label>
        <input type="password" runat="server" name="password" id="signin_password"  style="width: 206px;"/>

        </div>
         
        <br />
        <asp:Button CssClass="btn_submit" ID="btn_signin" runat="server" Text="Se connecter" OnClick="btn_signin_Click" />
        
        <div class="create-account"><a id="sign_up">S'inscrire.</a></div>
        </div>
        <br />
            <br />
            <br />

            <div id="signup">
        <label for="profile_picture">Profile image :</label>
        <input type="file" runat="server" name="profile_picture" id="profile_picture" />
        <br />
        <label for="signup_username">Nom d'utilisateur :</label>
        <input type="text" runat="server" name="username" id="signup_username" />
        <br />
        <label for="signup_password">Mot de passe :</label>
        <input type="password" runat="server" name="password" id="signup_password" />
        <br />
        <asp:Button CssClass="btn_submit" ID="btn_signup" runat="server" Text="S'inscrire" OnClick="btn_signup_Click" />

        <br />
        <div class="create-account"><a id="sign_in">Se connecter.</a></div>
        </div>
            </div>
            </div>
    </div>--%>
        </form>
        <script>
            var signin_signup_header = document.querySelector(".signin_signup_header > b");
            var box = document.getElementById("box");
            var signin_signup = document.getElementById("signin_signup");
            var sign_up = document.getElementById("sign_up");
            sign_up.addEventListener('click', function () {
                box.style.height = "350px";
                signin_signup.style.transform = "translateY(-210px)";
                signin_signup_header.innerHTML = "S'inscrire";
            });
            var sign_in = document.getElementById("sign_in");
            sign_in.addEventListener('click', function () {
                box.style.height = "210px";
                signin_signup.style.transform = "translateY(0px)";
                signin_signup_header.innerHTML = "Se connecter";
            });
        </script>
</body>
</html>
