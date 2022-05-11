<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAutorizacion.aspx.cs" Inherits="ALE_MexJet.Views.CreditoCobranza.frmAutorizacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%--<%@ Register Assembly="DevExpress.Web.v13.1" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v13.1" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>--%>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <link rel="stylesheet" type="text/css" href="Styles/Fuentes.css" />

    <!-- Bootstrap core CSS -->
    <link rel="shortcut icon" href="~/img/colors/blue/logo-ale.png">
    <title>.: Aerolineas Ejecutivas :.</title>
    <!-- Bootstrap theme -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="css/bootstrap-theme.css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="css/animate.css">
    <link rel="stylesheet" type="text/css" href="css/default.css" />
    <link rel="stylesheet" type="text/css" href="css/component.css" />
    <link href="css/prettify.css" rel="stylesheet">
    <link href="css/estilo.css" rel="stylesheet">
    <link href="css/demos.theme.css" rel="stylesheet">
    <link href="css/jquery.parallax.css" rel="stylesheet">

        <!-- https://material.io/resources/icons/?style=baseline -->
    <link href="https://fonts.googleapis.com/css2?family=Material+Icons"
          rel="stylesheet">
    <!-- https://material.io/resources/icons/?style=outline -->
    <link href="https://fonts.googleapis.com/css2?family=Material+Icons+Outlined"
          rel="stylesheet">
    <!-- https://material.io/resources/icons/?style=round -->
    <link href="https://fonts.googleapis.com/css2?family=Material+Icons+Round"
          rel="stylesheet">
    <!-- https://material.io/resources/icons/?style=sharp -->
    <link href="https://fonts.googleapis.com/css2?family=Material+Icons+Sharp"
          rel="stylesheet">
    <!-- https://material.io/resources/icons/?style=twotone -->
    <link href="https://fonts.googleapis.com/css2?family=Material+Icons+Two+Tone"
          rel="stylesheet">

    <style type="text/css" media="screen">
        body {
            font-family:"Helvetica Neue",Helvetica,Arial,sans-serif;
            margin: 0px;
            overflow-x: hidden;
        }
        .overlayy
        {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            filter: alpha(opacity=80);
            opacity: 0.8;
            background: rgba(0,0,0,0.8);
        }
        .overlayyContent
        {
            z-index: 99;
            margin: 250px auto;
            width: 80px;
            height: 80px;
        }
        .level1 .static:hover {
            background-color:#788891 !important;
        }
        .level1 .static {
            border-radius:5px 5px 0px 0px !important;
        }
        </style>
    	<style type="text/css" media="screen, projection">
		    #port {
			    margin: 1.5em 0px;
			    overflow: hidden;
			    position: relative;
			    height: 300px;
		    }
	    </style>

</head>
<body role="document">
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
        </cc1:ToolkitScriptManager>
        <div>
            <div>    
                    <asp:UpdatePanel ID="upaAutorizar" runat="server" UpdateMode="Conditional" style="height:85vh;">
                        <ContentTemplate>
                            <div class="container">
                                <div class="row">
                                    <div class="col-md-2" id="cabeza">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img id="imgLogoHea" runat="server" src="~/img/colors/blue/logo-ale.png" width="250" />
                                    </div>
                                    <div class="col-md-5">&nbsp;</div>
                                    <div class="col-md-5">
                                        <div style="text-align: right; padding-top:6%;">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="navbar-inverse" style="height:60px;background-repeat: repeat-x; margin-top:-110px; width:100%;
                            -webkit-box-shadow: 0px 4px 28px -8px rgb(0 0 0);
                            -moz-box-shadow: 0px 4px 28px -8px rgba(0,0,0,1);
                            box-shadow: 0px 4px 28px -8px rgb(0 0 0);
                            background: #cedce7;
                            background: -moz-linear-gradient(top, #cedce7 0%, #596a72 100%);
                            background: -webkit-linear-gradient(top, #cedce7 0%,#596a72 100%);
                            background: linear-gradient(to bottom, #cedce7 0%,#596a72 100%);
                            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#cedce7', endColorstr='#596a72',GradientType=0 );
                            border: 1px solid #afbfcb;">
                       
                            </div><br /><br />
                                <div class="container" style="background-color: white;
                                    border-radius: 20px;
                                    /* margin-bottom: 20px; */
                                    /* margin-top: -20px; */
                                    -webkit-box-shadow: 0px 4px 28px -8px rgb(0 0 0);
                                    -moz-box-shadow: 0px 4px 28px -8px rgba(0,0,0,1);
                                    box-shadow: 0px 4px 28px -8px rgb(0 0 0);
                                    width: 95%;
                                    /* margin-left: -1%; */
                                    margin: 0 auto;
                                    padding: 10px;">
                                    
                                    <div class="dxpnlControl" style="background-color:White;width:100%;border-radius: 14px;">	
                                        <div class="row header1" style="background-color: #002957;color: #fff;
                                            margin:-10px;
                                            height: 55px;
                                            padding-top: 18px;
                                            font-size: 14px;
                                            border-radius: 15px 15px 0 0;
                                            border: 1px solid #1C759A;
                                            background: rgb(30,76,84);
                                            background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iIzFlNGM1NCIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjElIiBzdG9wLWNvbG9yPSIjMjY0ODZkIiBzdG9wLW9wYWNpdHk9IjEiLz4KICAgIDxzdG9wIG9mZnNldD0iNTElIiBzdG9wLWNvbG9yPSIjM2E1MDY4IiBzdG9wLW9wYWNpdHk9IjEiLz4KICAgIDxzdG9wIG9mZnNldD0iMTAwJSIgc3RvcC1jb2xvcj0iIzI2NDg2ZCIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgPC9saW5lYXJHcmFkaWVudD4KICA8cmVjdCB4PSIwIiB5PSIwIiB3aWR0aD0iMSIgaGVpZ2h0PSIxIiBmaWxsPSJ1cmwoI2dyYWQtdWNnZy1nZW5lcmF0ZWQpIiAvPgo8L3N2Zz4=);
                                            background: -moz-linear-gradient(top, rgba(30,76,84,1) 0%, rgba(38,72,109,1) 1%, rgba(58,80,104,1) 51%, rgba(38,72,109,1) 100%);
                                            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(30,76,84,1)), color-stop(1%,rgba(38,72,109,1)), color-stop(51%,rgba(58,80,104,1)), color-stop(100%,rgba(38,72,109,1)));
                                            background: -webkit-linear-gradient(top, rgba(30,76,84,1) 0%,rgba(38,72,109,1) 1%,rgba(58,80,104,1) 51%,rgba(38,72,109,1) 100%);
                                            background: -o-linear-gradient(top, rgba(30,76,84,1) 0%,rgba(38,72,109,1) 1%,rgba(58,80,104,1) 51%,rgba(38,72,109,1) 100%);
                                            background: -ms-linear-gradient(top, rgba(30,76,84,1) 0%,rgba(38,72,109,1) 1%,rgba(58,80,104,1) 51%,rgba(38,72,109,1) 100%);
                                            background: linear-gradient(to bottom, rgba(30,76,84,1) 0%,rgba(38,72,109,1) 1%,rgba(58,80,104,1) 51%,rgba(38,72,109,1) 100%);
                                            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#1e4c54', endColorstr='#26486d',GradientType=0 );
                                            -webkit-box-shadow: 0px 4px 28px -8px rgb(0 0 0);
                                            -moz-box-shadow: 0px 4px 28px -8px rgba(0,0,0,1);
                                            box-shadow: 0px 4px 28px -8px rgb(0 0 0);">
                                            <div class="col-md-12">
                                                <span style="font-family: Helvetica, Arial,sans-serif; font-size: 20px; color:#ffffff;">&nbsp;&nbsp;Datos de Ajuste de Remisión</span>
                                            </div>
                                        </div>
                                    </div>
                            <div class="">
                                <asp:HiddenField ID="hdnIdRemision" runat="server" />
                                <br /><br />
                                <div style="margin:0 auto;width:100%;">
                                    <table style="margin: 0 auto;padding: 10px; width:35%; background-color:#efefef;" cellspacing="10">
                                    <tr>
                                        <td colspan="4" align="center">
                                            <asp:Label ID="LblMsg" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false" Font-Size="12pt"></asp:Label>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label1" runat="server" Text="Cliente:"></asp:Label>
                                        </td>              
                                        <td align="center">
                                            <asp:Label ID="rdCliente" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label2" runat="server" Text="Contrato:"></asp:Label>
                                        </td>              
                                        <td align="center">
                                            <asp:Label ID="rdContrato" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label3" runat="server" Text="Ejecutivo:"></asp:Label>
                                        </td>              
                                        <td align="center">
                                            <asp:Label ID="rdEjecutivo" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label4" runat="server" Text="Vendedor:"></asp:Label>
                                        </td>              
                                        <td align="center">
                                            <asp:Label ID="rdVendedor" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblCargoAbono" runat="server" Text="Tipo de ajuste:"></asp:Label>
                                        </td>              
                                        <td align="center">
                                            <asp:Label ID="rdCargoAbono" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblMotivo" runat="server" Text="Motivo:"></asp:Label>
                                        </td>              
                                        <td align="center">
                                            <asp:Label ID="rdMotivo" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            &nbsp;
                                        </td>              
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblHoras" runat="server" Text="Tiempo:"></asp:Label>
                                        </td>              
                                        <td align="center">
                                            <asp:Label ID="rdHoras" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            &nbsp;
                                        </td>              
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblComentarios" runat="server" Text="Comentarios:"></asp:Label>
                                        </td>              
                                        <td align="center">
                                            <asp:Label ID="rdComentarios" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            &nbsp;
                                        </td>   
                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <dx:ASPxButton ID="btnAutorizar" runat="server" Text="Autorizar" OnClick="btnAutorizar_Click" CssClass="dxb">
                                                <%--<ClientSideEvents Click="function(s, e) {ppAlert.Show(); }" />--%>
                                            </dx:ASPxButton>
                                        </td>
                                        <td align="center">
                                            <dx:ASPxButton ID="btnRechazar" runat="server" Text="Rechazar" OnClick="btnRechazar_Click" CssClass="dxb"></dx:ASPxButton>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                   
                                </table>
                                <br /><br />
                                </div>
                            </div>

                            <%--MODAL PARA MENSAJES--%>
                            <dx:ASPxPopupControl ID="msgAlert" 
                                runat="server" 
                                Theme="Office2010Black"
                                HeaderText="Aviso"
                                CloseOnEscape="true"
                                PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter"
                                AllowResize="true"
                                CloseAction="CloseButton"
                                DisappearAfter="100"
                                Width="300px"
                                Height="100px"
                                Modal="true"
                                ShowFooter="false"
                                AllowDragging="true"   
                                ShowCloseButton="true" >
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
                                                                    <ClientSideEvents Click="function(s, e) {msgAlert.Hide(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxPanel>
                                    </dx:PopupControlContentControl>
                                </ContentCollection>
                            </dx:ASPxPopupControl>

                            

                        </ContentTemplate>
                    </asp:UpdatePanel>


                    



                
                    
                    
                    
                
            </div>
            <br /><br />
            <div id="footer" style="background-color: #333; width: 100%; height: 60px; color: #FFF; /* position: fixed; */ margin-bottom: -20px; border-top: solid 1px; border-top-color: #575757; padding-top: 8px;">
                <p align="center">
                   <br /> México DF: +52 (55) 4209 0200 Av. Paseo de la Reforma 2608 piso 20, 
                    &copy; <script>
                               document.write(new Date().getFullYear())
                    </script> <a href="http://www.aerolineasejecutivas.com/" target="_blank">Aerolineas Ejecutivas</a>, Todos los derechos reservados.
                </p>
            </div>
        </div>
    </form>
    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="<%= ResolveClientUrl("~/js/jquery.min.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/js/bootstrap.min.js") %>"></script>
    <!-- <script src="<%= ResolveClientUrl("~/js/slidebars.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/js/docs.min.js") %>"></script> -->
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="<%= ResolveClientUrl("~/js/ie10-viewport-bug-workaround.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/js/classie.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/js/jquery-latest.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/js/jquery.bootstrap.wizard.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/js/prettify.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/js/jquery.parallax.js") %>"></script>
</body>
</html>
