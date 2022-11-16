<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Authentification.aspx.cs" Inherits="Opposition_Generateur.Authentification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="preconnect" href="https://fonts.googleapis.com"/>
<link rel="preconnect" href="https://fonts.gstatic.com" />
<link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300&display=swap" rel="stylesheet"/>
    
    <title></title>
        <style>
            *{margin:0px;padding:0px;}
            #form1{
                height:100vh;
                position:relative;
            }
            #form_auth{
                background-color:white;
                width:400px;
                position:absolute;
                top:20%;
                left:50%;
                transform:translateX(-50%);
                border:1px solid #DEE0E6;
                box-shadow:0px 0px 25px #DEE0E6;
                border-radius:5px;
                padding:5px 5px;
            }
        .signin_signup_header{
            
            font-family: 'Poppins', sans-serif;
            
            font-size:22px;
            border-radius:5px;
            display:flex;
            flex-direction:column;
            justify-content:center;
            align-items:center;
        }


         #sign_up{
           font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
           font-size:12px;
           color:#76848E;
           text-decoration:underline;
         }
         #sign_in{
                        font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
           font-size:12px;
           color:#76848E;
           text-decoration:underline;
         }
             
         #signin{
             display:flex;
             flex-direction:column;
         }
         #signup{
                          display:flex;
             flex-direction:column;
         }
         label{
           font-family: 'Poppins', sans-serif;
           font-size:12px;
           margin-bottom:5px;
         }
         input{
             padding:5px 0px;
             outline:0px;
             border:1px solid #DEE0E6;
             border-radius:2px;
             padding-left:3px;
             color:rgb(0, 0, 0 , 0.69);
         }

         .btn_submit{
             font-family: 'Poppins', sans-serif;
             padding:8px 0px;
             cursor:pointer;
             
             outline:0px;
             background-color: #fff;
             color:black;
             border-radius:5px;
             transition:0.3s ease-in-out;
             width:130px;
         }
         .btn_submit:hover{
                   background-color:#ff003b;
         }
         a{cursor:pointer;}
            #box {
             height:210px;
             overflow-y:hidden;
            }
            #signin_signup{
                transition:0.2s ease-out;
            }
            #error_msg{
                display:flex;
                justify-content:center;
                align-items:center;
                color:white;
                font-family:'Poppins';
                font-size:16px;
                background-color: rgb(209 97 107 / 87%);
                border: 1px solid #e54556;
                width:400px;
                margin:auto;
                transform:translateY(-200px);
            }
            .formstyle{
                background: linear-gradient(to bottom right, red, black);
            }
            .signinrow{
                display:flex;
                flex-direction:row;
            }
            #toBeZoomedOut {
   background-color: black;
   border: 1px solid #AAAAAA;
   color: white;
   width: 100px;
   height: 100px;
   margin-left: auto;
   margin-right: auto;
   -webkit-transition: all 1s ease-in-out;
   -moz-transition: all 1s ease-in-out;
   transition: all 1s ease-in-out;
}
#toBeZoomedOut div, #toBeZoomedOut img {
   width: 1200%;
   font-size: 20px;
}
#toBeZoomedOut img {
   height: 120%;
   width: 120%;
}

#toBeZoomedOut:hover {
   zoom: 0.6;
}
            
    </style>
</head>

<body>
    <form id="form1" runat="server"  class="formstyle">
    <div id="error_msg" runat="server">Erreur</div>
    <div id="form_auth">
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
    </div>
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
