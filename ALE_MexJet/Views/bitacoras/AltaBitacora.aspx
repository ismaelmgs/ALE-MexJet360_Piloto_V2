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

        .modalBitacora {
            z-index: 12000 !important;
            visibility: visible !important;
            display: table !important;
            position: absolute !important;
            left: 125px !important;
            top: 2px !important;
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
                        <div class="col-sm-1" align="right">Búsqueda:</div>
                        <div class="col-sm-2" align="right">
                            <dx:BootstrapTextBox ID="txtBusqueda" runat="server">
                            </dx:BootstrapTextBox>
                        </div>
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
                            <dx:BootstrapButton ID="btnBuscarBitacora" runat="server" Text="Buscar" SettingsBootstrap-RenderOption="Primary" OnClick="btnBuscarBitacora_Click" />
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
                <dx:ASPxButton CssClass="btn btn-primary" ID="btnNuevaBitacora" runat="server" Text="Nueva Bitácora" OnClick="btnNuevaBitacora_Click"></dx:ASPxButton>
            </div>
            <div class="col-md-6" style="text-align: right;">
                <dx:ASPxLabel CssClass="FExport" runat="server" Text="Exportar a:"></dx:ASPxLabel>
                &nbsp;<dx:ASPxButton CssClass="btn btn-success" ID="btnExcelBitacora" runat="server" Text="Excel"></dx:ASPxButton>
            </div>
        </div>
    <br />
        <div class="row">
            <div class="col-md-12" style="margin-left: 0px; width: 100%;">


                <asp:UpdatePanel ID="upaBitacoras" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="table-responsive" style="height: auto;">
                            <dx:BootstrapGridView ID="gvBitacoras" runat="server" KeyFieldName="IdBitacora" OnRowCommand="gvBitacoras_RowCommand">
                                <SettingsSearchPanel Visible="true" ShowApplyButton="true" />
                                <Settings ShowGroupPanel="true" ShowFilterRowMenu="true" />
                                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                <SettingsBehavior AllowSort="true" />
                                            
                                <Columns>

                                    <dx:BootstrapGridViewDataColumn Caption="Serie" FieldName="AeronaveSerie" VisibleIndex="1" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" SortIndex="0" SortOrder="None" />
                                    <dx:BootstrapGridViewDataColumn Caption="Matricula" FieldName="AeronaveMatricula" VisibleIndex="2" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="VueloClienteId" FieldName="VueloClienteId" Visible="false" VisibleIndex="3" CssClasses-DataCell="hiddenRow" HeaderBadge-CssClass="hiddenRow" />
                                    <dx:BootstrapGridViewDataColumn Caption="VueloContratoId" FieldName="VueloContratoId" VisibleIndex="4" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="PilotoId" FieldName="PilotoId" VisibleIndex="5" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="CopilotoId" FieldName="CopilotoId" VisibleIndex="6" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Fecha" FieldName="Fecha" VisibleIndex="7" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Origen" FieldName="Origen" VisibleIndex="8" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Destino" FieldName="Destino" VisibleIndex="9" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="OrigenVuelo" FieldName="OrigenVuelo" VisibleIndex="10" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="OrigenCalzo" FieldName="OrigenCalzo" VisibleIndex="11" Visible="false" HorizontalAlign="Center" CssClasses-DataCell="hiddenRow" HeaderBadge-CssClass="hiddenRow" />
                                    <dx:BootstrapGridViewDataColumn Caption="ConsumoOri" FieldName="ConsumoOri" VisibleIndex="12" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="CantPax" FieldName="CantPax" VisibleIndex="13" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Tipo" FieldName="Tipo" VisibleIndex="14" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="DestinoVuelo" FieldName="DestinoVuelo" VisibleIndex="15" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="DestinoCalzo" FieldName="DestinoCalzo" VisibleIndex="16" Visible="false" CssClasses-DataCell="hiddenRow" HeaderBadge-CssClass="hiddenRow" />
                                    <dx:BootstrapGridViewDataColumn Caption="ConsumoDes" FieldName="ConsumoDes" VisibleIndex="17" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="TripNum" FieldName="TripNum" VisibleIndex="18" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Leg_Num" FieldName="Leg_Num" VisibleIndex="19" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="LogNum" FieldName="LogNum" VisibleIndex="20" Visible="false" CssClasses-DataCell="hiddenRow" HeaderBadge-CssClass="hiddenRow" />
                                    <dx:BootstrapGridViewDataColumn Caption="LegId" FieldName="LegId" VisibleIndex="21" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="FolioReal" FieldName="FolioReal" VisibleIndex="22" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" />

                                    <%--<dx:BootstrapGridViewDataColumn Caption="Valor" FieldName="Valor" VisibleIndex="3" CssClasses-DataCell="hiddenRow" HeaderBadge-CssClass="hiddenRow" Visible="false" />--%>

                                    <dx:BootstrapGridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="23" HorizontalAlign="Center">
                                        <DataItemTemplate>

                                            <div>
                                                <dx:BootstrapButton Text="Actualizar" ID="btnActualizar" runat="server" CommandArgument='<%# Eval("IdBitacora") %>' CommandName="Actualiza" AutoPostBack="true" 
                                                    ToolTip="Actualiza" SettingsBootstrap-RenderOption="Primary"></dx:BootstrapButton>
                                            </div>

                                        </DataItemTemplate>
                                        <CssClasses HeaderCell="spa" />
                                    </dx:BootstrapGridViewDataColumn>
                                </Columns>
                            </dx:BootstrapGridView>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>


 
            </div>
        </div>

    <br />

    <!-- inicio modal -->
    <br />
    --- Modal ---
        

    <%-- MODAL PARA ALTA Y ACTUALIZACION DE BITACORAS --%>
    <dx:ASPxPopupControl ID="ppBitacora" runat="server" ClientInstanceName="ppBitacora" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true">
        <ClientSideEvents />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                

                

                    <div class="row" style="width:700px;">
                        <div class="col-md-12" style="margin-left: 0px; width: 102%;">
                            <div style="width:100%;text-align:center;"><h3>Alta Bitacora</h3></div><br />

                            <%--<div class="row">
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
                                    
                                </div>
                            </div>--%>

                        </div>
                    </div>

                <div style="width:100%;">
                    <div class="row">
                        <div class="col-md-3">
                            <label>Matrícula:</label>
                            <dx:BootstrapTextBox ID="txtMatricula" runat="server" NullText="Matrícula"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                            <label>TripNum:</label>
                            <dx:BootstrapTextBox ID="txtTrip" runat="server" NullText="Trip"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                            <label>LegNum:</label>
                            <dx:BootstrapTextBox ID="txtLegNum" runat="server" NullText="LegNum"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                            <label>Folio Real:</label>
                            <dx:BootstrapTextBox ID="txtFolioReal" runat="server" NullText="Folio"></dx:BootstrapTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>VueloContratoId:</label>
                            <dx:BootstrapTextBox ID="txtVueloContratoId" runat="server" NullText="Vuelo Contrato Id"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                            <label>PilotoId:</label>
                            <dx:BootstrapTextBox ID="txtPilotoId" runat="server" NullText="PilotoId"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                            <label>CopilotoId:</label>
                            <dx:BootstrapTextBox ID="txtCopilotoId" runat="server" NullText="CopilotoId"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                            <label>Fecha:</label>
                            <dx:BootstrapTextBox ID="txtFecha" runat="server" NullText="Fecha"></dx:BootstrapTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>Origen:</label>
                            <dx:BootstrapTextBox ID="txtOrigen" runat="server" NullText="Origen"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                            <label>Destino:</label>
                            <dx:BootstrapTextBox ID="txtDestino" runat="server" NullText="Destino"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                            <label>Origen Vuelo:</label>
                            <dx:BootstrapTextBox ID="txtOrigenVuelo" runat="server" NullText="Origen Vuelo"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                            <label>Destino Vuelo:</label>
                            <dx:BootstrapTextBox ID="txtDestinoVuelo" runat="server" NullText="Destino Vuelo"></dx:BootstrapTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>Origen Calzo:</label>
                            <dx:BootstrapTextBox ID="txtOrigenCalzo" runat="server" NullText="Origen Calzo"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                            <label>Destino Calzo:</label>
                            <dx:BootstrapTextBox ID="txtDestinoCalzo" runat="server" NullText="Destino Calzo"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                            <label>Consumo Origen:</label>
                            <dx:BootstrapTextBox ID="txtConsumoOrigen" runat="server" NullText="Consumo Origen"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                            <label>Consumo Destino:</label>
                            <dx:BootstrapTextBox ID="txtConsumoDestino" runat="server" NullText="Consumo Destino"></dx:BootstrapTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>CantPax:</label>
                            <dx:BootstrapTextBox ID="txtCantPax" runat="server" NullText="Pax"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                            <label>Tipo:</label>
                            <dx:BootstrapTextBox ID="txtTipo" runat="server" NullText="Tipo"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                            <label>LongNum:</label>
                            <dx:BootstrapTextBox ID="txtLongNum" runat="server" NullText="LongNum"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-md-3">
                             <label>LegId:</label>
                            <dx:BootstrapTextBox ID="txtLegId" runat="server" NullText="LegId"></dx:BootstrapTextBox>
                        </div>
                    </div>
                </div>
                


                <div class="row" style="padding-top:15px;">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-md-10" align="right">
                                <dx:ASPxButton ID="btnOk" runat="server" Text="Cerrar" CssClass="btn btn-warning" Width="80px" AutoPostBack="false" Style="float: right; margin-right: 8px" TabIndex="0">
                                    <ClientSideEvents Click="function(s, e) {ppBitacora.Hide(); }" />
                                </dx:ASPxButton>
                            </div>
                            <div class="col-md-1" align="left">
                                <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" Width="80px" OnClick="btnGuardar_Click"></dx:ASPxButton>
                            </div>
                        </div>
                    </div>
                    
                </div>

                       
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>


    <br />
    <!-- fin modal -->

    <%-- MODAL PARA MENSAJES --%>
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

</asp:Content>
