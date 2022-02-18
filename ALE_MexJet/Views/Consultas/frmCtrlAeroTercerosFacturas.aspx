<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmCtrlAeroTercerosFacturas.aspx.cs" Inherits="ALE_MexJet.Views.Consultas.frmCtrlAeroTercerosFacturas" UICulture="es" Culture="es-MX" ValidateRequest="false" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span class="FTitulo">&nbsp;&nbsp;Control de Rentas de Terceros</span>
                    </div>
                </div>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />

                <div class="row">
                    <div class="col-md-12">
                        <br />
                        <fieldset class="Personal">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                            </legend>

                            <div class="col-sm-12">

                                <table width="100%" style="text-align: left;">
                                    <tr>
                                        <td style="text-align: center" width="23%">
                                            <dx:ASPxDateEdit CssClass="FFecha" Caption="Desde:" ID="dFechaIni" ClientInstanceName="dFechaIni" NullText="Fecha Inicial" runat="server" ToolTip="Fecha Inicial" Theme="Office2010Black">
                                                <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td style="text-align: center" width="23%">
                                            <dx:ASPxDateEdit Caption="Hasta:" ID="dFechaFin" ClientInstanceName="dFechaFin" runat="server" Theme="Office2010Black" ToolTip="Fecha Final" NullText="Fecha Final">
                                                <DateRangeSettings StartDateEditID="dFechaIni"></DateRangeSettings>
                                                <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td>
                                            <dx:ASPxButton CssClass="FBotton" ID="btnBuscar" runat="server" Text="Buscar" Theme="Office2010Black" Visible="true"
                                                OnClick="btnBuscar_Click">
                                            </dx:ASPxButton>
                                        </td>


                                    </tr>
                                </table>

                            </div>
                        </fieldset>
                    </div>
                </div>
                <br />
                <div class="row">

                    <div class="col-sm-6">
                        <dx:ASPxButton CssClass="FBotton" ID="btnNuevo" runat="server" Text="Nuevo" Theme="Office2010Black" Visible="false"></dx:ASPxButton>
                    </div>

                    <div class="col-md-6" style="text-align: right;">
                        <dx:ASPxLabel CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                        &nbsp;<dx:ASPxButton CssClass="FBotton" ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExcel_Click"></dx:ASPxButton>
                    </div>
                </div>

                <br />
                <div class="row">
                    <div class="col-md-12" style="margin-left: -15px; width: 103%;">
                        <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                            <ContentTemplate>
                                <div class="col-sm-12">
                                    <dx:ASPxGridView ID="gvReporteFactura" runat="server" AutoGenerateColumns="false"
                                        ClientInstanceName="gvReporteFactura" EnableTheming="True" Styles-Header-HorizontalAlign="Center"
                                        Theme="Office2010Black" Width="100%">
                                        <Columns>
                                            <dx:GridViewDataDateColumn FieldName="FechaVuelo" Caption="Fecha de <br/> vuelo" ShowInCustomizationForm="true" CellStyle-HorizontalAlign="Center" Visible="true" VisibleIndex="0">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Date"></PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn FieldName="FechaFactura" Caption="Fecha de <br/> factura" ShowInCustomizationForm="true" CellStyle-HorizontalAlign="Center" Visible="true" VisibleIndex="1">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Date"></PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn FieldName="Solicitud" Caption="Solicitud" ShowInCustomizationForm="true" Visible="true" VisibleIndex="2">
                                                <PropertiesTextEdit MaxLength="200"></PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Factura" Caption="Factura" ShowInCustomizationForm="true" Visible="true" VisibleIndex="3">
                                                <PropertiesTextEdit MaxLength="200"></PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Matricula" Caption="Matrícula" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4">
                                                <PropertiesTextEdit MaxLength="200"></PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Equipo" Caption="Equipo" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4" Width="200px">
                                                <PropertiesTextEdit MaxLength="200"></PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Ruta" Caption="Ruta" Visible="true" VisibleIndex="5" Width="250px">
                                                <PropertiesTextEdit MaxLength="400"></PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="TiempoVuelo" Caption="Tiempo de <br/> Vuelo" Visible="true" VisibleIndex="6">
                                                <PropertiesTextEdit MaxLength="300"></PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="TiempoVueloRemision" Caption="Tiempo de <br/> Vuelo Remisión" Visible="true" VisibleIndex="6" Width="150px">
                                                <PropertiesTextEdit MaxLength="300"></PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Pernocta" Caption="Pernocta" ShowInCustomizationForm="true" Visible="true" VisibleIndex="7">
                                                <PropertiesTextEdit MaxLength="200"></PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Proveedor" Caption="Proveedor" ShowInCustomizationForm="true" Visible="true" VisibleIndex="8" Width="250px">
                                                <PropertiesTextEdit MaxLength="200"></PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewBandColumn Caption="Facturado Proveedor" VisibleIndex="9">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="ProveedorTotalUSD" Caption="USD" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1" Width="150px">
                                                        <PropertiesTextEdit MaxLength="200" DisplayFormatString="{0:C4}"></PropertiesTextEdit>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ProveedorTotalMXN" Caption="MXN" ShowInCustomizationForm="true" Visible="true" VisibleIndex="2" Width="150px">
                                                        <PropertiesTextEdit MaxLength="200" DisplayFormatString="{0:C4}"></PropertiesTextEdit>
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ProveedorTipoCambio" Caption="Tipo de <br/> Cambio" ShowInCustomizationForm="true" Visible="true" VisibleIndex="10" Width="150px">
                                                <PropertiesTextEdit MaxLength="200" DisplayFormatString="{0:C4}"></PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewBandColumn Caption="Facturado ALE" VisibleIndex="11">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="AleTotalUSD" Caption="USD" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1" Width="150px">
                                                        <PropertiesTextEdit MaxLength="200" DisplayFormatString="{0:C4}"></PropertiesTextEdit>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="AleTotalMXN" Caption="MXN" ShowInCustomizationForm="true" Visible="true" VisibleIndex="2" Width="150px">
                                                        <PropertiesTextEdit MaxLength="200" DisplayFormatString="{0:C4}"></PropertiesTextEdit>
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewDataTextColumn FieldName="AleTipoCambio" Caption="Tipo de <br/> Cambio" ShowInCustomizationForm="true" Visible="true" VisibleIndex="12" Width="150px">
                                                <PropertiesTextEdit MaxLength="200" DisplayFormatString="{0:C4}"></PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Width="150" FieldName="DiferenciaAcargoAfavor" Caption="Diferencia <br/>A cargo / A favor" ShowInCustomizationForm="true" Visible="true" VisibleIndex="13">
                                                <PropertiesTextEdit MaxLength="200" DisplayFormatString="{0:C4}"></PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>

                                            <dx:GridViewBandColumn Caption="Cliente" VisibleIndex="14">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="ClienteRenta" Caption="Contrato" Visible="true" VisibleIndex="2" CellStyle-HorizontalAlign="Center">
                                                        <PropertiesTextEdit MaxLength="250" DisplayFormatString="{0:C4}"></PropertiesTextEdit>
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                        </Columns>
                                        <Settings HorizontalScrollBarMode="Auto" ShowGroupPanel="True" />
                                        <Settings ShowGroupPanel="True" />
                                        <SettingsSearchPanel Visible="true" />
                                        <SettingsPager Position="TopAndBottom" Visible="true">
                                            <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                            </PageSizeItemSettings>
                                        </SettingsPager>
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvReporteFactura">
                                    </dx:ASPxGridViewExporter>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExportar" />
                                <asp:PostBackTrigger ControlID="btnExcel" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-6">
                        <dx:ASPxButton CssClass="FBotton" ID="btnNuevo2" runat="server" Text="Nuevo" Theme="Office2010Black" Visible="false"></dx:ASPxButton>
                    </div>
                    <div class="col-sm-6" style="text-align: right;">
                        <dx:ASPxLabel CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                        &nbsp;<dx:ASPxButton CssClass="FBotton" ID="btnExportar" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExcel_Click"></dx:ASPxButton>
                    </div>
                </div>
                <br />
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

    <dx:ASPxPopupControl ID="popup" runat="server" ClientInstanceName="popup" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true" Width="300">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
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
                                        <dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popup.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <div>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>
