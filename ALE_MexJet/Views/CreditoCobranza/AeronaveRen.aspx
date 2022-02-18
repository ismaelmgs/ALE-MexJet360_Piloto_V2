﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="AeronaveRen.aspx.cs" Inherits="ALE_MexJet.Views.CreditoCobranza.AeronaveRen" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" style="border-radius:21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Control de Aeronaves rentadas a terceros</span>
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
                                    <table style="width:100%;" border="0">
                                        <tr>
                                            <td width="2%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right" width="23%">
                                                
                                            </td>
                                            <td width="23%">
                                                
                                            </td>
                                            <td width="4%">
                                                &nbsp;
                                            </td>
                                            <td colspan="2" width="46%">
                                                <dx:ASPxLabel Font-Size="Larger" runat="server" Text="Rango de Fechas"></dx:ASPxLabel>
                                             </td>
                                            <td width="2%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="2%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: center" width="23%">
                                                <dx:ASPxLabel runat="server" Text="Matricula:" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 25%;" width="23%">
                                                <asp:Panel ID="panelTextoBusqueda" runat="server" DefaultButton="btnBusqueda" style="width:250px;">
                                                    <dx:ASPxTextBox ID="txtTextoBusqueda" ToolTip="Matricula" runat="server" Theme="Office2010Black" NullText="Ingrese Matrícula a buscar">
                                                    </dx:ASPxTextBox>
                                                </asp:Panel>
                                            </td>
                                            <td width="4%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: center" width="23%">
                                                <dx:ASPxDateEdit Caption="Desde:" ID="dFechaIni" ClientInstanceName="dFechaIni" NullText="Fecha Inicial" runat="server" ToolTip="Fecha Inicial" Theme="Office2010Black">
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
                                            <td width="2%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="2%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: left" width="23%">
                                                
                                            </td>
                                            <td>
                                            </td>
                                            <td width="4%">
                                                &nbsp;
                                            </td>
                                            <td colspan="2" style="text-align: center" width="46%">
                                                <dx:ASPxButton ID="btnBusqueda" Text="Buscar" runat="server" Theme="Office2010Black"  OnClick="btnBusqueda_Click"></dx:ASPxButton>
                                                                                                
                                            </td>
                                            <td width="2%">
                                                
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel runat="server"  ID="upGv" OnUnload ="UpdatePanel1_Unload"  UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div>
                                        <dx:ASPxGridView ID="gvAeronave" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvAeronave" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign ="Center"
                                            Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="NumeroTrip" Caption="Número TRIP" VisibleIndex="1"  HeaderStyle-HorizontalAlign="Center" >
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TRIPSTAT" Caption="TRIP Status" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" >
                                                </dx:GridViewDataTextColumn>
                                            
                                                <dx:GridViewDataTextColumn FieldName="FechaSalidaBase" Caption="Fecha Salida Base" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="dd/MM/yyyy">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FechaLlegadaBase" Caption="Fecha Llegada Base" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="dd/MM/yyyy">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TAIL_NMBR" Caption="Matrícula" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" >
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ClaveContrato" Caption="Clave Contrato"  VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" >
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ClaveContratoCliente" Caption="Clave Contrato Cliente"  VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" >
                                                </dx:GridViewDataTextColumn>
                                                
                                               
                                                <dx:GridViewDataTextColumn FieldName="NumeroPax" Caption="Número Pax"  VisibleIndex="11" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="RUTA" Caption="RUTA"  VisibleIndex="12" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                
                                            </Columns>
                                            <SettingsSearchPanel Visible="true" />
                                            <Settings ShowGroupPanel="True" />
                                            <SettingsPager Position="TopAndBottom" Visible="true">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                        </dx:ASPxGridView>
                                          <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvAeronave">
                                        </dx:ASPxGridViewExporter>
                                    </div>  
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <br />
            <div class="row">
            <div class="col-md-12">
            <div   style="text-align:right" >
                 <asp:UpdatePanel runat="server"  ID="UpdatePanel4" UpdateMode="Conditional"  OnUnload="UpdatePanel1_Unload">
                                <ContentTemplate>
                                <dx:ASPxLabel runat="server"   Theme="Office2010Black" Text="Exportar a: "></dx:ASPxLabel>
                            &nbsp;<dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" ToolTip="Excel"  OnClick="btnExcel_Click"></dx:ASPxButton>
                                      </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExcel" />
                            </Triggers>
                            </asp:UpdatePanel>
            </div>
            </div>
            </div>
                    </div>
     </dx:PanelContent>
    </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>
