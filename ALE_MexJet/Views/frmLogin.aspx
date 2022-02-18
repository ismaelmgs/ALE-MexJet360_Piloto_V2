<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="ALE_MexJet.Views.frmLogin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous">

    <link rel="stylesheet" type="text/css" href="../css/styles_login.css" media="screen" />
    <style>
        .col, .col-1, .col-10, .col-11, .col-12, .col-2, .col-3, .col-4, .col-5, .col-6, .col-7, .col-8, .col-9, .col-auto, .col-lg, .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-lg-auto, .col-md, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-md-auto, .col-sm, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-sm-auto, .col-xl, .col-xl-1, .col-xl-10, .col-xl-11, .col-xl-12, .col-xl-2, .col-xl-3, .col-xl-4, .col-xl-5, .col-xl-6, .col-xl-7, .col-xl-8, .col-xl-9, .col-xl-auto {
        padding-right: 0px !important;
        padding-left: 0px !important;
        }
        .messagealert {
            width: 100%;
            position: fixed;
             top:0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
    </style>

    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
</head>
<body style="background-image:url('../img/coming_back2.jpg'); overflow:hidden;background-position: center center;
background-repeat: no-repeat;
background-attachment: fixed;
background-size: cover;
background-color: #669999;">

    <script type="text/javascript">

        function Redirecciona(cad) {
            location.href = cad;
        }

        function fnCheckValue() {
            var myVal = document.getElementById('<%=txtPassword.ClientID%>').value;
            var myUsu = document.getElementById('<%=txtUsuario.ClientID%>').value;

            if (myUsu = "") {
                alert("El Usuario es requerido");
                return false;
            }
            else if (myVal == "") {
                alert("La contraseña es requerida");
                return false;
            }
            else {
                return true;
            }
        }

        function EnterEvent(e) {

            if (e.keyCode == 13) {
                if (fnCheckValue()) {
                    var obj = document.getElementById('<%=btnEntrar.ClientID%>');

                    if (obj) {
                        obj.click();
                    }
                }
                else {
                    return false;
                }
            }
        }

        function Alertas() {
            $(document).ready(function () {
            window.setTimeout(function() {
                $(".alert").fadeTo(1000, 0).slideUp(1000, function(){
                    $(this).remove(); 
                });
            }, 5000);
 
            });
        }


    </script>
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#<%=ButtonLogin.ClientID %>').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

            setTimeout(() => {
              $("#alert_div").alert('close')
            }, 3000);
        }
    </script>

    <form id="form1" runat="server">
        <div runat="server" id="ButtonLogin"></div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="upaPrincipal" runat="server">
            <ContentTemplate>
                <uc1:ucModalMensaje runat="server" ID="mpeMensaje" />
                
                <div class="row" style="height:100vh;">
                    <div class="col-md-2">
                        &nbsp;
                    </div>
                    <div class="col-md-3" style="">
                        <div style="margin-left:30px;margin-right:30px; background-color:#070707b0;box-shadow:2px 2px 2px #00000030; height:100vh;">
                        <div id="logo_ale" class="" style="text-align:center;"><br /><br />
	                        <%--<asp:Image ID="imLogo" runat="server" ImageUrl="../../img/colors/blue/logo-ale.png" width="156" height="92" />--%>
                            <img id="imLogoH" runat="server" src="~/img/colors/blue/logo_ale_mexjet_bco.png" style="width:90%;" />
                        </div>
                        <div id="bg" class="">
                            <!--DIV DE LOGIN-->
                            <div id="log"><br />
                                <div style="text-align:center; color:#ffffff;">
                                    <h2 class="" style="font-family:Arial;">Bienvenido</h2>
                                </div>
                                <br />
                                <div class="row" style="margin:10px;">
                                    <div class="col-md-2" style="background-color:#002a5c; text-align:center; border-radius: 5px 0px 0px 5px; box-shadow: 2px 2px 2px #00000050; color:#ffffff;">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-person-fill" viewBox="0 0 16 16" style="margin-top:5px;">
                                          <path d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H3zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"/>
                                        </svg>
                                    </div>
                                    <div class="col-md-10">
                                        <%--<span class="" style="font-family:Arial">Usuario:</span>--%>
                                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" ClientIDMode="Static" style="font-family:Arial; border-radius: 0px 5px 5px 0px; box-shadow: 2px 2px 2px #00000050;" placeholder="Usuario"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row" style="margin:10px;">
                                    <div class="col-md-2" style="background-color:#002a5c; text-align:center; border-radius: 5px 0px 0px 5px; box-shadow: 2px 2px 2px #00000050; color:#ffffff;">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-key-fill" viewBox="0 0 16 16" style="margin-top:5px;">
                                          <path d="M3.5 11.5a3.5 3.5 0 1 1 3.163-5H14L15.5 8 14 9.5l-1-1-1 1-1-1-1 1-1-1-1 1H6.663a3.5 3.5 0 0 1-3.163 2zM2.5 9a1 1 0 1 0 0-2 1 1 0 0 0 0 2z"/>
                                        </svg>
                                    </div>
                                    <div class="col-md-10">
                                        <%--<span class="" style="font-family:Arial">Contrase&ntilde;a:</span>--%>
                                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="20" TextMode="Password" ClientIDMode="Static" onkeypress="return EnterEvent(event);"
                                        CssClass="form-control" style="font-family:Arial; border-radius: 0px 5px 5px 0px; box-shadow: 2px 2px 2px #00000050;" placeholder="Contraseña"></asp:TextBox>
                                    </div>
                                </div>

                                <div style="text-align:center;" runat="server" id="SpanToken">
                                    <span class="" style="font-family:Arial; color:#ffffff;">Token</span>
                                </div>

                                <div class="row" style="margin:10px;">

                                    <div class="col-md-2" style="background-color:#002a5c; text-align:center; border-radius: 5px 0px 0px 5px; box-shadow: 2px 2px 2px #00000050; color:#ffffff;" runat="server" id="SvgToken">
                                        
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-lock-fill" viewBox="0 0 16 16" style="margin-top:5px;">
                                          <path d="M8 1a2 2 0 0 1 2 2v4H6V3a2 2 0 0 1 2-2zm3 6V3a3 3 0 0 0-6 0v4a2 2 0 0 0-2 2v5a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V9a2 2 0 0 0-2-2z"/>
                                        </svg>
                                    </div>
                                    <div class="col-md-10">

<%--                                    <dx:BootstrapTextBox ID="BootTxtToken" runat="server" MaxLength="6" CssClasses-Caption="form-control" style="border-radius: 0px 5px 5px 0px !important; box-shadow: 2px 2px 2px #00000050;">
                                        <MaskSettings Mask="[000 - 000]" IncludeLiterals="None"/>
                                    </dx:BootstrapTextBox>--%>
                                        <asp:TextBox id="txtToken" runat="server" MaxLength="6" placeholder="___-___" Visible="false"
                                            CssClass="form-control" style="font-family:Arial; border-radius: 0px 5px 5px 0px; box-shadow: 2px 2px 2px #00000050;">
                                            <%--<ClientSideEvents ButtonClick="function(s,e) { dxbsDemo.showToast('The button has been clicked.'); }" />--%>
                                            <%--<Buttons>
                                                <dx:BootstrapEditButton />
                                            </Buttons>--%>
                                        </asp:TextBox>

                                    </div>
                                </div>
                                <br />
                               
                                <br />
                                <div id="divEntrar" runat="server">
                                    <div class="row">
                                        <div class="col-md-12" style="text-align:center;">
                                            <asp:Button ID="btnEntrar" runat="server" Text="Entrar" CssClass="btn btn-primary" ClientIDMode="Static"  style="font-family:Arial;margin-top: 6px;" OnClick="btnEntrar_Click"/>
                                        </div>
                                    </div>
                                </div>
                                
                                <div id="divToken" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-6" style="text-align:right; padding-right:10px"">
                                            <asp:Button ID="btnOK" runat="server" Text="Entrar" CssClass="btn btn-primary" Visible="false" ClientIDMode="Static"  style="font-family:Arial;margin-top: 6px;width: 150px;
    margin-right: 3px;" OnClick="btnOK_Click" />
                                        </div>
                                        <div class="col-md-6" style="text-align:left; padding-left:10px" >
                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-warning" Visible="false" ClientIDMode="Static"  style="font-family:Arial;margin-top: 6px;width: 150px;
    " OnClick="btnCancelar_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <%--<div class="row">
                                    
                                </div>--%>
                                 <div class="row">
                                    <div class="col-md-12" style="text-align:center;">
                                        <%--<linkbutton class="btn btn-success" style="font-family:Arial; margin-top:5px;">Renviar Token</linkbutton>--%>
                                        <asp:LinkButton ID="lkbReenviar" runat="server" Text="Reenviar Token" Visible="false" OnClick="lkbReenviar_Click"></asp:LinkButton>
                                        <span class="" style="font-family:Arial;"><!-- &iquest;Olvidaste tu Contrase&ntilde;a? --></span>
                                    </div>
                                </div>
                                
                            </div>
                            
                        </div>


                        </div>
                   </div>
                    <div class="alert alert-warning" role="alert" id="ErrorLogin" runat="server" visible="false" style="position:absolute; left:990px; top:0px;" onkeypress="Alertas();">
                                  Credenciales Incorrectas, favor de revizarlas.
                        <button id="btnCerrarMensaje" runat="server" type="button" class="close" data-dismiss="alert" aria-label="Close" onclick="btnCerrarMensaje_Click">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        
                    </div>  
                    <div class="col-md-4">
                        &nbsp;
                    </div>
                    <div class="col-md-3">
                        &nbsp;
                    </div>
                </div>
                

            </ContentTemplate>
        </asp:UpdatePanel>        


        <%-- MENSAJE --%>
        <%--MODAL PARA MENSAJES--%>
        <%--<dx:ASPxPopupControl ID="ppAlert" runat="server" ClientInstanceName="ppAlert" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true" Width="300">
            <ClientSideEvents />
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxPanel ID="ASPxPanel2" runat="server" DefaultButton="btOK">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxImage ID="ASPxImage2" runat="server" ShowLoadingImage="true" ImageUrl="~/img/iconos/Information2.ico"></dx:ASPxImage>
                                            <dx:ASPxTextBox ID="tbLogin" ReadOnly="true" Border-BorderStyle="None" Height="1px" runat="server" Width="1px" ClientInstanceName="tbLogin"></dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel ID="lbl" runat="server" ClientInstanceName="lbl" Text="ASPxLabel"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btOK" runat="server" Text="OK" Theme="PlasticBlue" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                                <ClientSideEvents Click="function(s, e) {ppAlert.Hide(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>--%>

    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ho+j7jyWK8fNQe+A12Hb8AhRq26LrZ/JpcUGGOn+Y7RsweNrtN/tE3MoK7ZeZDyx" crossorigin="anonymous"></script>

</body>
</html>
