﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmConsultaSolicitudes.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ALE_MexJet.Views.Consultas.frmConsultaSolicitudes" %>


<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Consulta Solicitudes</span>
                    </div>
                </div>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />


                <div class="well-g">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="Personal">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                                </legend>
                                <div class="col-sm-12">
                                    <table cellpsdding="5" width="99%" border="0">
                                        <tr>
                                            <td width="2%">&nbsp;
                                            </td>
                                            <td width="23%" style="text-align: left;">
                                                <dx:ASPxLabel runat="server" Text="Clave de Cliente:"></dx:ASPxLabel>
                                            </td>
                                            <td width="23%">
                                                <dx:ASPxComboBox ToolTip="Clave Cliente" ID="ddlClaveCliente" runat="server" Theme="Office2010Black" EnableTheming="True" NullText="Seleccione un Cliente"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlClaveCliente_SelectedIndexChanged" ValueType="System.Int32" ValueField="ClaveCliente">
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td width="4%">&nbsp;
                                            </td>
                                            <td width="46%" colspan="2">
                                                <dx:ASPxLabel Font-Size="Larger" runat="server" Text="Rango de fecha de Creación"></dx:ASPxLabel>
                                            </td>
                                            <td width="2%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="2%">&nbsp;
                                            </td>
                                            <td width="23%" style="text-align: left;">
                                                <dx:ASPxLabel runat="server" Text="Clave Contrato:"></dx:ASPxLabel>
                                            </td>
                                            <td width="23%">
                                                <dx:ASPxComboBox ToolTip="Clave Contrato" ID="ddlContrato" runat="server" Theme="Office2010Black" EnableTheming="True" NullText="Seleccione un Contrato"
                                                    AutoPostBack="false" ValueType="System.Int32" ValueField="ClaveContrato">
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td width="4%">&nbsp;
                                            </td>
                                            <td width="23%">
                                                <dx:ASPxDateEdit Caption="Desde:" ID="cFechaIni" ClientInstanceName="cFechaIni" NullText="Fecha Inicial" runat="server" ToolTip="Fecha Inicial" Theme="Office2010Black">
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td width="23%">
                                                <dx:ASPxDateEdit Caption="Hasta:" ID="cFechaFin" ClientInstanceName="cFechaFin" runat="server" Theme="Office2010Black" ToolTip="Fecha Final" NullText="Fecha Final">
                                                    <DateRangeSettings StartDateEditID="cFechaIni"></DateRangeSettings>
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td width="2%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="2%">&nbsp;
                                            </td>
                                            <td width="23%" style="text-align: left;">
                                                <dx:ASPxLabel runat="server" Text="Número de Trip de FPK:"></dx:ASPxLabel>
                                            </td>
                                            <td width="23%">
                                                <dx:ASPxTextBox ID="txtTrip" runat="server" Theme="Office2010Black" NullText="Ingrese la información a buscar" Width="190px">
                                                    <ValidationSettings RegularExpression-ValidationExpression="[a-zA-Z ñÑáéíóúÁÉÍÓÚ]*[0-9]*" ErrorText="Sólo se permiten números y letras"></ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td width="4%">&nbsp;
                                            </td>
                                            <td width="46%" colspan="2">
                                                <dx:ASPxLabel Font-Size="Larger" runat="server" Text="Rango de fecha de Vuelo"></dx:ASPxLabel>
                                            </td>
                                            <td width="2%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="2%">&nbsp;
                                            </td>
                                            <td width="23%" style="text-align: left;">
                                                <dx:ASPxLabel runat="server" Text="Usuario:"></dx:ASPxLabel>
                                            </td>
                                            <td width="23%">
                                                <dx:ASPxTextBox ID="txtUsuario" runat="server" Theme="Office2010Black" NullText="Ingrese la información a buscar" Width="190px" />
                                            </td>
                                            <td width="4%">&nbsp;
                                            </td>
                                            <td width="23%">
                                                <dx:ASPxDateEdit Caption="Desde:" ID="vFechaIni" ClientInstanceName="vFechaIni" NullText="Fecha Inicial" runat="server" ToolTip="Fecha Inicial" Theme="Office2010Black">
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td width="23%">
                                                <dx:ASPxDateEdit Caption="Hasta:" ID="vFechaFin" ClientInstanceName="vFechaFin" runat="server" Theme="Office2010Black" ToolTip="Fecha Final" NullText="Fecha Final">
                                                    <DateRangeSettings StartDateEditID="vFechaIni"></DateRangeSettings>
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td width="2%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="2%">&nbsp;
                                            </td>
                                            <td width="23%" style="text-align: left;">
                                                <dx:ASPxLabel runat="server" Text="Estatus:"></dx:ASPxLabel>
                                            </td>
                                            <td width="23%">
                                                <dx:ASPxComboBox ToolTip="Estatus de la Solicitud" ID="ddlEstatusSolicitud" runat="server" Theme="Office2010Black" EnableTheming="True" NullText="Seleccione un Estatus" AutoPostBack="False" ValueType="System.Int32" ValueField="IdStatus">
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td width="4%">&nbsp;
                                            </td>
                                            <td width="23%" colspan="2" align="center">
                                                <dx:ASPxButton ID="btnBusqueda" Text="Buscar" runat="server" Theme="Office2010Black" OnClick="btnBusqueda_Click"></dx:ASPxButton>
                                            </td>
                                            <td width="2%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7"></td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <br />
                    <div class="row" style="text-align: right">
                        <br />
                        <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a: "></dx:ASPxLabel>
                        &nbsp;<dx:ASPxButton ID="btnExportar2" runat="server" Text="Excel" Theme="Office2010Black" ToolTip="Exportar a Excel" OnClick="btnExportar_Click"></dx:ASPxButton>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="UpdatePanel1_Unload">
                                <ContentTemplate>
                                    <div class="col-sm-12">
                                        <dx:ASPxGridView ID="gvConsultaSolicitudes" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvConsultaSolicitudes" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign="Center"
                                            Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="IdSolicitud" Caption="Solicitud" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" Width="80px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ClaveCliente" Caption="Cliente" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" Width="80px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ClaveContrato" Caption="Contrato" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" Width="80px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Trips" Caption="Número de Trip de FPK" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" Width="180px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FechaCreacion" Caption="Fecha de creación" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" Width="200px" PropertiesTextEdit-DisplayFormatString="dd/MM/yyyy">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Estatus" Caption="Estatus" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="OrigenDestino" Caption="Origen Destino" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" Width="130px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FechaHoraVuelo" Caption="Fecha de Vuelo" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" Width="200px" PropertiesTextEdit-DisplayFormatString="dd/MM/yyyy">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="UsuarioCreacion" Caption="Usuario" VisibleIndex="9" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FechaModificacion" Caption="Última modificación" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center" Width="200px" PropertiesTextEdit-DisplayFormatString="dd/MM/yyyy">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsSearchPanel Visible="true" />
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvConsultaSolicitudes">
                                        </dx:ASPxGridViewExporter>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnExportar2" />
                                    <asp:PostBackTrigger ControlID="btnExcel" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div style="height:10px">
                            </div>
                            <div class="row" style="text-align: right">
                                <br />
                                <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a: "></dx:ASPxLabel>
                                &nbsp;<dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" ToolTip="Exportar a Excel" OnClick="btnExportar_Click"></dx:ASPxButton>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                </div>

                <br />

                <asp:UpdatePanel runat="server" ID="UpdatePanel3" OnUnload="UpdatePanel1_Unload">
                    <ContentTemplate>
                        <dx:ASPxHiddenField ID="hftxtClaveCliente" runat="server" />
                        <dx:ASPxHiddenField ID="hftxtContrato" runat="server" />
                        <dx:ASPxHiddenField ID="hftxtTrip" runat="server" />
                        <dx:ASPxHiddenField ID="hftxtUsuario" runat="server" />
                        <dx:ASPxHiddenField ID="hftxtEstatus" runat="server" />
                        <dx:ASPxHiddenField ID="hfcFechaIni" runat="server" />
                        <dx:ASPxHiddenField ID="hfcFechaFin" runat="server" />
                        <dx:ASPxHiddenField ID="hfvFechaIni" runat="server" />
                        <dx:ASPxHiddenField ID="hfvFechaFin" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>

            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>
