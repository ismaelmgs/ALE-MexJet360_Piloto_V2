<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmAjustesJetCard.aspx.cs" Inherits="ALE_MexJet.Views.CreditoCobranza.frmAjustesJetCard" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="~/../../../JS/jquery/jquery-1.8.3.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Ajustes de Horas Jet Card</span>
                    </div>
                </div>
                 <div class="row">
                    <br />
                    <div class="col-md-12">
                        <fieldset class="Personal">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda</span>
                            </legend>
                            <div class="col-md-12">
                                <table style="width: 80%; margin:0 auto;">
                                    <tr>
                                        <td style="text-align: center;">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: center;">
                                            <dx:ASPxLabel runat="server" Text="Texto: " Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td style="text-align: center;">
                                            <dx:ASPxTextBox ID="txtTexto" runat="server" Theme="Office2010Black" NullText="Ingrese Proveedor."></dx:ASPxTextBox>
                                        </td>
                                        <td style="text-align: center;">
                                            <dx:ASPxLabel runat="server" Text="Texto2: " Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td style="text-align: center;">
                                            <dx:ASPxComboBox ID="ddlTextoDos" runat="server" Theme="Office2010Black" NullText="Seleccione una opción."></dx:ASPxComboBox>
                                        </td>
                                        <td style="text-align: center;">
                                            <dx:ASPxButton runat="server" Text="Buscar" Theme="Office2010Black"></dx:ASPxButton>
                                        </td>
                                        <td style="text-align: center;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="well-g">
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            Grid
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-3">
                            &nbsp;
                        </div>
                        <div class="col-md-6">
                            <fieldset class="Personal">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Solicitud de autorización de ajuste</span>
                                </legend>
                                <div class="col-md-12">
                                <table style="width: 80%; margin:0 auto;">
                                    <tr>
                                        <td style="text-align: center;">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: center;">
                                            <dx:ASPxLabel runat="server" Text="Cargo/Abono: " Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td style="text-align: center;">
                                            <dx:ASPxComboBox ID="ddlCargoAbono" runat="server" Theme="Office2010Black" NullText="Seleccione una opción." Width="90%"></dx:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: center;">
                                            <dx:ASPxLabel runat="server" Text="Horas: " Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td style="text-align: center;">
                                            <dx:ASPxTextBox ID="txtHoras" runat="server" Theme="Office2010Black" NullText="Horas" Width="90%"></dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: center;">
                                            <dx:ASPxLabel runat="server" Text="Comentarios: " Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td style="text-align: center;">
                                            <dx:ASPxMemo ID="txtComentarios" ClientInstanceName="mNotasSolicitud" runat="server" Height="100px" Width="90%" NullText="Comentarios"></dx:ASPxMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            &nbsp;
                                        </td>
                                        <td colspan="2" style="text-align: center;">
                                            <dx:ASPxButton runat="server" Text="Cancelar" Theme="Office2010Black"></dx:ASPxButton>&nbsp;&nbsp;&nbsp;&nbsp;<dx:ASPxButton runat="server" Text="Cancelar" Theme="Office2010Black"></dx:ASPxButton>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </div>
                            </fieldset>
                        </div>
                        <div class="col-md-3">
                            &nbsp;
                        </div>
                    </div>
                </div>
</asp:Content>
