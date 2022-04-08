<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAutorizacion.aspx.cs" Inherits="ALE_MexJet.Views.CreditoCobranza.frmAutorizacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

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
</head>
<body role="document">
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
        </cc1:ToolkitScriptManager>
        <div>

            <asp:UpdatePanel ID="upaAutorizar" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <div class="">
                        <h4>Datos de Ajuste de Remisión</h4>
                        <asp:HiddenField ID="hdnIdRemision" runat="server" />
                        <table>
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblCargoAbono" runat="server" Text="Cargo/Abono:"></asp:Label>
                                </td>              
                                <td width="80%">
                                    <asp:Label ID="rdCargoAbono" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblHoras" runat="server" Text="Horas:"></asp:Label>
                                </td>              
                                <td width="80%">
                                    <asp:Label ID="rdHoras" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblComentarios" runat="server" Text="Comentarios:"></asp:Label>
                                </td>              
                                <td width="80%">
                                    <asp:Label ID="rdComentarios" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <dx:ASPxButton ID="btnAutorizar" runat="server" Text="Autorizar" OnClick="btnAutorizar_Click"></dx:ASPxButton>
                                    &nbsp
                                    <dx:ASPxButton ID="btnRechazar" runat="server" Text="Rechazar" OnClick="btnRechazar_Click"></dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>


            <%--MODAL PARA MENSAJES--%>
            <dx:ASPxPopupControl ID="ppAlert" runat="server" ClientInstanceName="ppAlert" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
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
            </dx:ASPxPopupControl>

        </div>
    </form>
</body>
</html>
