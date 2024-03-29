﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmVuelosReasignados.aspx.cs" Inherits="ALE_MexJet.Views.Reportes.frmVuelosReasignados" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Vuelos Reasignados</span>
                    </div>
                </div>
                <div class="well-g">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="Personal">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                                </legend>
                                <div class="col-sm-12">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: center;" colspan="3">
                                                <dx:ASPxLabel Theme="Office2010Black" runat="server" Text="Rango de Fechas" Font-Bold="True" Font-Size="Small"></dx:ASPxLabel>
                                            </td>
                                            <td colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;" colspan="5">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <dx:ASPxLabel runat="server" ID="lblFechaDesde" Text="Desde" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td style="text-align: left;">
                                                <dx:ASPxDateEdit runat="server" ID="dtFechaDesde" NullText="Seleccione" Theme="Office2010Black" ValidationSettings-ErrorDisplayMode="Text">
                                                    <DropDownButton>
                                                        <Image IconID="scheduling_calendar_16x16"></Image>
                                                    </DropDownButton>
                                                    <ValidationSettings>
                                                        <RequiredField IsRequired="true" ErrorText="El campo es requerido"/>
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td style="text-align: left;">
                                                <dx:ASPxLabel runat="server" ID="lblHasta" Text="Hasta" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td style="text-align: left;">
                                                <dx:ASPxDateEdit runat="server" ID="dtFechaHasta" NullText="Seleccione" Theme="Office2010Black" ValidationSettings-ErrorDisplayMode="Text">
                                                    <DateRangeSettings StartDateEditID="dtFechaDesde"></DateRangeSettings>
                                                    <DropDownButton>
                                                        <Image IconID="scheduling_calendar_16x16"></Image>
                                                    </DropDownButton>
                                                    <ValidationSettings>
                                                        <RequiredField  IsRequired="true" ErrorText="El campo es requerido"/>
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                                <td>
                                                <dx:ASPxButton runat="server" ID="btnBuscar" Text="Buscar" Theme="Office2010Black"  OnClick="btnBuscar_Click"></dx:ASPxButton>
                                            </td>
                                        
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="upaReport" runat="server" OnUnload="upaReport_Unload">
                            <ContentTemplate>
                                <div>
                                     <div id="VuelosReasignados" runat="server"></div>
                                          <%-- <dx:ASPxGridView ID="gvVuelosreasignados" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvVuelosreasignados" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign ="Center"
                                            Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="ClienteId"   Caption="IdCliente" VisibleIndex="1"  HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ContratoId" Caption="IdContrato" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FechaVuelo" Caption="FechaVuelo" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="NumeroRemision" Caption="NoRemision" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Matricula" Caption="Matricula" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Ruta" Caption="Ruta" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Pax" Caption="Pax"  VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TiempoVuelo" Caption="TiempoVuelo"  VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PernoctaNacional" Caption="PernoctaNacional"  VisibleIndex="9" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PernoctaInternacional" Caption="PernoctaInternacional"  VisibleIndex="10" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TiempoEsperaNacional" Caption="TiempoEsperaNacional"  VisibleIndex="11" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TiempoEsperaInternacional" Caption="TiempoEsperaInternacional"  VisibleIndex="12" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ImporteTotal" Caption="ImporteTotal"  VisibleIndex="13" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings HorizontalScrollBarMode="Auto" ShowGroupPanel="True" />
                                            <SettingsSearchPanel Visible="true"  />
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                        </dx:ASPxGridView>--%>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExcel" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                        <table style="width:100%">
                            <tr>
                                <td colspan="3" style="text-align:right">
                                    <dx:ASPxButton ID="btnExcel" runat="server" Text="Excel"  Theme="Office2010Black" OnClick="btnExcel_Click"></dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

</asp:Content>
