<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="HRIS_ePayroll.login" %>
<!DOCTYPE html>
<html>
<head runat="server">
      <meta charset="utf-8">
      <meta http-equiv="X-UA-Compatible" content="IE=edge">
      <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
      <meta name="description" content="">
      <meta name="author" content="">
      <title>HRIS ePayroll</title>

    <script type="text/javascript">
        function DisableBackButton() {
        window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function(evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function() { void (0) }
    </script>

      <!-- Bootstrap core CSS-->
      <link rel="stylesheet"  href="~/vendor/bootstrap/css/bootstrap.min.css">
      <!-- Custom fonts for this template-->
      <link  rel="stylesheet" href="~/vendor/font-awesome/css/font-awesome.min.css">
      <!-- Page level plugin CSS-->
      <link  rel="stylesheet" href="~/vendor/datatables/dataTables.bootstrap4.css">
      <!-- Custom styles for this template-->
      <link href="~/css/sb-admin.css" rel="stylesheet">
      <link href="~/css/common.css" rel="stylesheet">
      <style type="text/css">
          
          #message_loger {
              text-align:center !important;
          }
          .show-caps {
            font-size:14px;
            font-weight:bold;
            color:darkred;
            visibility:hidden;
          }
        .body-login {
            padding: 5%;
            background-image: url(ResourceImages/DDO%20Building%20Blur.jpg);
            background-size: cover;
        }

        .screen-login {
            background: url(ResourceImages/Davao%20de%20Oro%20Provincial%20Capitol%20Building.JPG) no-repeat;
            background-size: contain;
            box-shadow: 5px 10px 20px 0px #606060;
            padding: 15px !important;
            background-color: #f3f3f3;
            border-radius: 10px;
            border: 1px solid #f5f5f5;
        }
        .middle-box {
          max-width: 400px;
          z-index: 100;
          margin: 0 auto;
          padding-top: 40px;
        }
         .loginscreen.middle-box {
            width: 300px;
        }
         .logo-name {
          color: #e6e6e6;
          font-size: 180px;
          font-weight: 800;
          letter-spacing: -10px;
          margin-bottom: 0;
        }
      </style>
</head>
<body class="body-login" >

    <div class="middle-box text-center loginscreen screen-login" >
        <div style="margin-bottom:5px;">
            <h1 class="logo-name">
                <img src="ResourceImages/ddo%20logo.png" height="200" width="200" />
            </h1>
        </div>
        <div style="font-size:14px;color:darkgoldenrod;margin-top:10px;" id="welcome_message" runat="server">
            <small style="font-size:16px;font-weight:bolder" >Welcome to</small> 
            <small style="font-size:16px;font-weight:bolder" id="appName" runat="server"> </small>
        </div>
        <hr style="margin-top:4px;margin-bottom:4px;border-color:darkgoldenrod;" />
        <h4 style="margin-top:3px;"><strong>HRIS - PAYROLL </strong></h4>
        <hr style="margin-top:4px;margin-bottom:0px;border-color:darkgoldenrod;" />
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-12 text-center">
                <asp:UpdatePanel ID="message_loger" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="msg_logre" Cssclass="col-form-label" runat="server" Text=""></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_login" />
                        <asp:AsyncPostBackTrigger ControlID="txtb_username" />
                        <asp:AsyncPostBackTrigger ControlID="txtb_password" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-12 mt-1">
                <asp:UpdatePanel ID="pannel_username" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:TextBox ID="txtb_username" runat="server" onInput="go_postBack(event)" OnTextChanged="txtb_username_TextChanged" AutoPostBack="false" CssClass="form-control form-control-sm" Width="100%" placeholder="User Name"></asp:TextBox>
                        <script type="text/javascript">
                            function go_postBack(key) {
                                if (key.which != 13) {
                                    __doPostBack("<%= txtb_username.ClientID %>", "");
                                }
                                                            
                            }
                        </script>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-12 mt-1" >
                <asp:UpdatePanel ID="pannel_password" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:TextBox ID="txtb_password" type="password" TextMode="Password" onInput="go_postBack2(event)" onKeyPress="capLock(event)" OnTextChanged="txtb_password_TextChanged" AutoPostBack="false"  runat="server" CssClass="form-control form-control-sm" Width="100%" placeholder="Password"></asp:TextBox>
                        <span class="fa fa-eye-slash pull-right" runat="server" id="show_pass_icn" onclick="showpassword()" style="margin-top:-28px;margin-right:2px;z-index:1000;background-color:white;padding:5px;cursor:pointer;position:relative"></span>
                        <%--<span class="fa fa-eye-slash pull-right" id="show_pass_icn123123" onclick="showpassword()" style="margin-top:-28px;margin-right:2px;z-index:1000;background-color:white;padding:5px;cursor:pointer;position:relative"></span>--%>
                        <asp:Label ID="show_caps" CssClass="show-caps" runat="server">Capslock is on.</asp:Label>
                        <script type="text/javascript">
                            function go_postBack2(key)
                            {
                                if (key.which != 13)
                                {
                                    __doPostBack("<%= txtb_password.ClientID %>", "");
                                }
                            }

                            function showpassword()
                            {
                                var x = document.getElementById("<%= txtb_password.ClientID %>");
                                if (x.type === "password")
                                  {
                                    x.type = "text";
                                    $("#<%= show_pass_icn.ClientID %>").removeClass('fa-eye-slash');
                                    $("#<%= show_pass_icn.ClientID %>").addClass('fa-eye');
                                  }
                                    else
                                  {
                                    $("#<%= show_pass_icn.ClientID %>").removeClass('fa-eye');
                                    $("#<%= show_pass_icn.ClientID %>").addClass('fa-eye-slash');
                                    x.type = "password";
                                  }
                            }
                        </script>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-sm-12" style="margin-top:-15px !important">
                <asp:LinkButton ID="btn_login"  CssClass="btn btn-primary btn-block btn-sm"  OnCommand="btn_login_Command" runat="server"><i class="fa fa-fw fa-sign-out"></i>Login</asp:LinkButton>
                <p class="mt-0"> <small class="text-muted smaller">Provincial Government of Davao de Oro &copy <%: DateTime.Now.Year %></small> </p>
            </div>
        </div>
    </form>
    </div>

</body>
<script src='<%= ResolveUrl("~/vendor/jquery/jquery.min.js") %>'></script>
   
<script src='<%= ResolveUrl("~/vendor/popper/popper.min.js") %>'></script>
<script src='<%= ResolveUrl("~/vendor/bootstrap/js/bootstrap.min.js") %>'></script>
<!-- Core plugin JavaScript-->
<script src='<%= ResolveUrl("~/vendor/jquery-easing/jquery.easing.min.js") %>'></script>
<!-- Page level plugin JavaScript-->
<script src='<%= ResolveUrl("~/vendor/datatables/jquery.dataTables.js") %>' ></script>
<script src='<%= ResolveUrl("~/vendor/datatables/dataTables.bootstrap4.js") %>'></script>
<!-- Custom scripts for all pages-->
<script src='<%= ResolveUrl("~/js/sb-admin.min.js") %>'></script>
<!-- Custom scripts for this page-->
<script src='<%= ResolveUrl("~/js/sb-admin-datatables.min.js") %>'></script>
<script type="text/javascript">
    document.addEventListener("keydown", function (event)
    {
        if (event.which == 13) {
            __doPostBack("<%= btn_login.ClientID %>", "");
        }
        else  if (event.which == 20) {
            capLock(event);
        }
    });

    function capLock(e)
    {
      var kc = e.keyCode ? e.keyCode : e.which;
        var sk = e.shiftKey ? e.shiftKey : kc === 16;
        //console.log(kc + " -- " + sk);
        if (e.which == 13) {
             __doPostBack("<%= btn_login.ClientID %>", "");
        }
        else
        {
            var visibility = ((kc >= 65 && kc <= 90) && !sk) || 
            ((kc >= 97 && kc <= 122) && sk) ? 'visible' : 'hidden';
            //console.log(visibility);
            $("#<%: show_caps.ClientID%>").css("visibility", visibility);
        }
      
    }
</script>
</html>