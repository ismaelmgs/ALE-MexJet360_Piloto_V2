<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="AltaBitacora.aspx.cs" Inherits="ALE_MexJet.Views.bitacoras.AltaBitacora" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" type="text/css" href="../../Styles/Controls.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
  
    <style>
        .hiddenRow {
            visibility:hidden !important;
        }
        .centerCell {
            text-align:center !important;
            color: #337ab7 !important;
        }
        .centerTxt {
            text-align:center !important;
        }
        .dirTB{  
            text-align:center !important;  
        }
        .dataCell {
            font-size:9pt;
        }
        .form-control[disabled], .form-control[readonly], fieldset[disabled] .form-control {
            cursor: not-allowed !important;
            background-color: #eee !important;
            opacity: 1 !important;
            text-align:center !important;
        }
        .form-control {
            text-align:center !important;
        }
        .spa {
            font-weight: bold !important;
            color: #337ab7 !important;
            text-align: center !important;
            font-size:9pt;
        }
        .validateTxt {
            border-color: crimson !important;
        }
        th {
            font-size:10pt !important;
        }
        .inputText {
            font-family: inherit !important;
            font-size: inherit !important;
            line-height: inherit !important;
            background-color: #FFFFFF !important;
            padding: 4px 4px 5px 4px !important;
            border: 1px solid #ccc !important;
            border-radius: 4px;
            color: #555;
            -webkit-transition: 0.5s;
            
        }
        .inputText textarea[type=text]:focus {
            box-shadow: inset 0 1px 1px rgb(0 0 0 / 8%) !important;
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s !important;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s !important;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s !important;
            outline:auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:PanelContent ID="ASPxPanel1" runat="server" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span class="FTitulo">&nbsp;&nbsp; Alta Bitácoras</span>
                    </div>
                </div>
            </dx:PanelContent>
        <PanelCollection>
    </dx:PanelContent>
        <asp:Panel ID="pnlBusqueda" runat="server" Visible="true">
         <div class="row">
            <div class="col-md-12">
                <br />
                <fieldset class="Personal">
                    <legend>
                        <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda de Bitacoras</span>
                    </legend>
                    <div class="row">
                        <div class="col-sm-2">&nbsp;</div>
                        <%--<div class="col-sm-2">&nbsp;</div>--%>
                        <div class="col-sm-3" align="right">Búsqueda por:</div>
                        <div class="col-sm-2" align="center">
                            <dx:BootstrapComboBox ID="ddlBusquedaBitacora" runat="server">
                                <Items>
                                    <dx:BootstrapListEditItem Value="" Text=".:Seleccione:." Selected="true"></dx:BootstrapListEditItem>
                                    <dx:BootstrapListEditItem Value="1" Text="Por Autorizar"></dx:BootstrapListEditItem>
                                    <dx:BootstrapListEditItem Value="2" Text="Autorizados"></dx:BootstrapListEditItem>
                                    <dx:BootstrapListEditItem Value="0" Text="Todos"></dx:BootstrapListEditItem>
                                </Items>
                            </dx:BootstrapComboBox> 
                        </div>
                        <div class="col-sm-2" align="left">
                            <dx:BootstrapButton ID="btnBuscarBitacora" runat="server" Text="Buscar" SettingsBootstrap-RenderOption="Primary" />
                        </div>
                        <div class="col-sm-3">&nbsp;</div>
                    </div>
                </fieldset>
            </div>
        </div>
    </asp:Panel>
    <br />
        <div class="row">
            <div class="col-md-6">
                <dx:ASPxButton CssClass="FBotton" ID="btnNuevaBitacora" runat="server" Text="Nueva Bitácora" Theme="Office2010Black"></dx:ASPxButton>
            </div>
            <div class="col-md-6" style="text-align: right;">
                <dx:ASPxLabel CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                &nbsp;<dx:ASPxButton CssClass="FBotton" ID="btnExcelBitacora" runat="server" Text="Excel" Theme="Office2010Black"></dx:ASPxButton>
            </div>
        </div>
    <br />
        <div class="row">
            <div class="col-md-12" style="margin-left: 0px; width: 100%;">
                <fieldset class="Personal">
                    <legend>
                        <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Resultado Bitácoras</span>
                    </legend>

                --- Grid ---

                </fieldset>
            </div>
        </div>

    <br />

    <!-- inicio modal -->
    <br />
    --- Modal ---
        <div class="row card">
            <div class="col-md-12" style="margin-left: 0px; width: 102%;">
                <div style="width:100%;text-align:center;"><h3>Alta Bitacora</h3></div><br />

                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Matrícula:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtMatricula" CssClass="" placeholder ="Matrícula" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="TripNum:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtTripNum" CssClass="" placeholder ="TripNum" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Leg_Num:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtLegNum" CssClass="" placeholder ="Leg_Num" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Folio Real:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtFolioReal" CssClass="" placeholder ="Folio Real" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="VueloContratoId:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtVuelocontratoId" CssClass="" placeholder ="VueloContratoId" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="PilotoId:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtPilotoId" CssClass="" placeholder ="TripNum" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="CopiliotoId:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtCopilotoId" CssClass="" placeholder ="CopilotoId" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Fecha:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtFechaBitacoras" CssClass="" placeholder ="Fecha" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Origen:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtOrigen" CssClass="" placeholder ="Origen" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Destino:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtDestino" CssClass="" placeholder ="Destino" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Origen Vuelo:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtOrigenVuelo" CssClass="" placeholder ="Origen Vuelo" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Destino Vuelo:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtDestinoVuelo" CssClass="" placeholder ="Destino Vuelo" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Origen Calzo:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtOrigewnCalzo" CssClass="" placeholder ="Origen Calzo" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Destino Calzo:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtDestinoCalzo" CssClass="" placeholder ="Destino Calzo" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Consumo Origen:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="TextBox11" CssClass="" placeholder ="Leg_Num" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Consumo Destino:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtConsumoDestino" CssClass="" placeholder ="Consumo Destino" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="CantPax:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtCantPax" CssClass="" placeholder ="CantPax" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-6">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:30%;">
                                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Tipo:"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:70%;">
                                            <asp:TextBox ID="txtTipo" CssClass="" placeholder ="Tipo" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-8">
                                &nbsp;
                            </div>
                            <div class="col-md-2">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="text-align:center;">
                                            <dx:BootstrapButton ID="btnGuardarBitacora" runat="server" Text="Guardar" SettingsBootstrap-RenderOption="Success" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-2">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="text-align:center;">
                                            <dx:BootstrapButton ID="btnCerrarModal" runat="server" Text="Cerrar" SettingsBootstrap-RenderOption="Warning" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    <br />
    <!-- fin modal -->

</asp:Content>
