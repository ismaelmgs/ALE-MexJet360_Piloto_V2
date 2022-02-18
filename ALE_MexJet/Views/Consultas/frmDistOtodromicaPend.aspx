<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmDistOtodromicaPend.aspx.cs" Inherits="ALE_MexJet.Views.Consultas.frmDistOtodromicaPend" UICulture="es" Culture="es-MX" %>

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
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Distancias Ortodromicas Pendientes</span>
                    </div>
                </div>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <div class="well-g">
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel runat="server" ID="upGv" OnUnload="UpdatePanel1_Unload" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div>
                                        <dx:ASPxGridView ID="gvDisOrto" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvDisOrto" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign="Center"
                                            Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="OrigenICAO" Caption="Origen ICAO" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DestinoICAO" Caption="Destino ICAO" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="OrigenIATA" Caption="Origen IATA" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DestinoIATA" Caption="Destino IATA" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center"  Width="300px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CiudadOrigen" Caption="Ciudad Origen" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center"  Width="300px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CiudadDestino" Caption="Ciudad Destino" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center"  Width="300px">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings ShowGroupPanel="True" />
                                            <SettingsSearchPanel Visible="true" />
                                            <SettingsPager Position="TopAndBottom" Visible="true">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvDisOrto">
                                        </dx:ASPxGridViewExporter>
                                    </div>
                                    <div class="row" style="text-align: right">
                                        <br />
                                        <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a: "></dx:ASPxLabel>
                                        &nbsp;<dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" ToolTip="Exportar a Excel" OnClick="btnExcel_Click"></dx:ASPxButton>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnExcel" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <br />

                    <br />
                </div>
                <br />
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>
