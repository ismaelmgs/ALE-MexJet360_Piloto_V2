<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmBitacoraPeticion.aspx.cs" Inherits="ALE_MexJet.Views.bitacoras.frmBitacoraPeticion" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .centerCell {
            text-align:center !important;
            color: #337ab7 !important;
        }

        th {
            /*font-size:10pt !important;*/
            font-weight: bold !important;
            color: #337ab7 !important;
            text-align: center !important;
            font-size:11pt;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <dx:PanelContent ID="ASPxPanel1" runat="server" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span class="FTitulo">&nbsp;&nbsp; Peticion de Bitacora</span>
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
                    <div class="row">
                        <div class="col-sm-2">&nbsp;</div>
                        <div class="col-sm-2">
                            <dx:BootstrapDateEdit id="fechaInicio" runat="server" editformat="Custom" width="100%" caption="Desde" clientinstancename="Fecha1" displayformatstring="dd/MM/yyyy" editformatstring="dd/MM/yyyy" usemaskbehavior="true">
                                <calendarproperties showweeknumbers="false"></calendarproperties>
                            </dx:BootstrapDateEdit>
                        </div>
                        <div class="col-sm-2">
                            <dx:bootstrapdateedit id="fechaFin" runat="server" editformat="Custom" width="100%" caption="Hasta" clientinstancename="Fecha2"
                                displayformatstring="dd/MM/yyyy" editformatstring="dd/MM/yyyy" usemaskbehavior="true">
                                <calendarproperties showweeknumbers="false"></calendarproperties>
                            </dx:bootstrapdateedit>

                        </div>
                        <div class="col-sm-2">
                             <dx:BootstrapTextBox ID="tripNumber" runat="server" caption="tripNumber"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-sm-2" style="vertical-align: bottom">
                        <dx:bootstrapbutton ID="btnBuscar" runat="server" text="Buscar" width="100%" style="margin-top:28px; height:35px;" OnClick="btnBuscar_Click">
                            <settingsbootstrap renderoption="Success" />
                        </dx:bootstrapbutton>
                        </div>
                        <div class="col-sm-2">&nbsp;</div>
                    </div>
                </fieldset>
            </div>
        </div>
    </asp:Panel>

    

    <asp:Panel ID="pnlReporteFl3xx" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-12" style="margin-left: -15px; width: 102%;">
                <asp:UpdatePanel ID="upaFl3xx" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12" style="text-align: right;">
                                <dx:ASPxButton CssClass="btn btn-success" ID="btnProcesar" runat="server" Text="Procesar Bitacora" OnClick="btnProcesar_Click"></dx:ASPxButton>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-sm-12">

                                <dx:BootstrapGridView ID="gvBitacora" runat="server" KeyFieldName="flightId" SettingsBehavior-AllowDragDrop="false" OnPageIndexChanged="gvBitacora_PageIndexChanged">
                                    <SettingsSearchPanel Visible="false" ShowApplyButton="false" />
                                    <Settings ShowGroupPanel="false" ShowFilterRowMenu="false" ShowTitlePanel="false" />
                                    <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                    <SettingsPager PageSize="20" PageSizeItemSettings-Caption="Registros por Página" NextPageButton-Text="Siguiente" PrevPageButton-Text="Anterior"></SettingsPager>
                                    <SettingsBehavior AllowDragDrop="false" />
                                    <Columns>

                                        <dx:BootstrapGridViewDataColumn Caption="Matricula" FieldName="registrationNumber" VisibleIndex="1" HorizontalAlign="Center" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False"/>
                                        <dx:BootstrapGridViewDataColumn Caption="TripNum" FieldName="tripNumber" VisibleIndex="2" SortOrder="None" HorizontalAlign="Center" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="Contrato Vuelo" FieldName="customerFirstname" VisibleIndex="3" HorizontalAlign="Center" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="Fecha" FieldName="blockOnEstUTC" VisibleIndex="4" SortOrder="None" HorizontalAlign="Center" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="Origen" FieldName="airportFrom" VisibleIndex="5" SortOrder="None" HorizontalAlign="Center" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="Destino" FieldName="airportTo" VisibleIndex="6" SortOrder="None" HorizontalAlign="Center" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="Cantidad Pax" FieldName="paxNumber" VisibleIndex="7" SortOrder="None" HorizontalAlign="Center" Settings-AllowDragDrop="False" Settings-AllowSort="False" />

                                    </Columns>
                                    <SettingsBehavior ConfirmDelete="True" />
                                    <SettingsPager Position="Bottom">
                                        <PageSizeItemSettings Items="20, 50, 100"></PageSizeItemSettings>
                                    </SettingsPager>
                                    <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                                    <Settings ShowGroupPanel="True" />
                                    <SettingsPopup>
                                        <EditForm HorizontalAlign="Center" VerticalAlign="Below" Width="400px" />
                                    </SettingsPopup>
                                </dx:BootstrapGridView>

                            </div>

                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnProcesar" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
       
    </asp:Panel>

     <%--MODAL PARA MENSAJES--%>
    <dx:bootstrappopupcontrol id="ppAlert" runat="server" clientinstancename="ppAlert" closeanimationtype="Fade" popupanimationtype="Fade"
        popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
        closeaction="CloseButton" closeonescape="true" allowresize="true"
        headertext="Listo" allowdragging="true" showclosebutton="true" width="300" height="200">
        <clientsideevents />
        <contentcollection>
            <dx:contentcontrol>
                <table>
                    <tr>
                        <td>
                            <dx:aspximage id="ASPxImage2" runat="server" showloadingimage="true" imageurl="~/img/iconos/Information2.ico"></dx:aspximage>
                            <dx:aspxtextbox id="tbLogin" readonly="true" border-borderstyle="None" height="1px" runat="server" width="1px" clientinstancename="tbLogin"></dx:aspxtextbox>
                        </td>
                        <td>
                            <dx:aspxlabel id="lbl" runat="server" clientinstancename="lbl" text="ASPxLabel"></dx:aspxlabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:bootstrapbutton id="btOK" runat="server" text="OK" width="80px" settingsbootstrap-renderoption="Primary" autopostback="false" style="float: left; margin-right: 8px" tabindex="0">
                                <clientsideevents click="function(s, e) {ppAlert.Hide(); }" />
                            </dx:bootstrapbutton>
                        </td>
                    </tr>
                </table>
            </dx:contentcontrol>
        </contentcollection>
    </dx:bootstrappopupcontrol>

</asp:Content>
