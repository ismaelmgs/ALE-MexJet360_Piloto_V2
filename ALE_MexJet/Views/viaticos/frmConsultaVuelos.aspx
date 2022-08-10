<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmConsultaVuelos.aspx.cs" Inherits="ALE_MexJet.Views.viaticos.frmConsultaVuelos" UICulture="es" Culture="es-MX" %>

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
        }
        .validateTxt {
            border-color: crimson !important;
        }
        th {
            background-color:#efefef;
            color:#3974be;
            border:1px solid #cccccc !important;
            text-align:center;
            font-size:9pt !important;
        }
        .tdderecha {
            text-align:right !important;
        }
        .tdcentro {
            text-align:center !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="pnlBusqueda" runat="server" Visible="true">
         <div class="row">
            <div class="col-md-12">
                <br />
                <fieldset class="Personal">
                    <legend>
                        <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda de Vuelos</span>
                    </legend>
                    <div class="row">
                    <div class="col-sm-2">&nbsp;</div>
                    <div class="col-sm-2">
                        <dx:BootstrapDateEdit ID="date1" runat="server" EditFormat="Custom" Width="100%" Caption="Desde" ClientInstanceName="Fecha1"
                            DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true">
                            <CalendarProperties ShowWeekNumbers="false"></CalendarProperties>
                        </dx:BootstrapDateEdit>
                    </div>
                    <div class="col-sm-2">
                        <dx:BootstrapDateEdit ID="date2" runat="server" EditFormat="Custom" Width="100%" Caption="Hasta" ClientInstanceName="Fecha2"
                            DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true">
                            <CalendarProperties ShowWeekNumbers="false"></CalendarProperties>
                        </dx:BootstrapDateEdit>
                    </div>
                    <div class="col-sm-2">
                        <dx:BootstrapTextBox ID="txtParametro" runat="server" Caption="Piloto" Width="100%">
                            <%--<MaskSettings Mask="999999999999999" />--%>
                        </dx:BootstrapTextBox>
                    </div>
                    <div class="col-sm-2" style="vertical-align:bottom">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <dx:BootstrapButton ID="btnConsultaVuelos" runat="server" Text="Consulta vuelos" OnClick="btnConsultaVuelos_Click" Width="100%">
                            <SettingsBootstrap RenderOption="Success" />
                        </dx:BootstrapButton>
                    </div>
                    <div class="col-sm-2">&nbsp;</div>
                </div>
                </fieldset>
            </div>
        </div>
    </asp:Panel>
     <br />
    <asp:Panel ID="pnlVuelos" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-12" style="margin-left: -15px; width: 102%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-sm-12">

                            <dx:BootstrapGridView ID="gvVuelos" runat="server" KeyFieldName="idBitacora">
                                <SettingsSearchPanel Visible="true" ShowApplyButton="true" />
                                <Settings ShowGroupPanel="True" ShowFilterRowMenu="true" />
                                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                <SettingsPager PageSize="20"></SettingsPager>
                                <Columns>
                                    <%--<dx:BootstrapGridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="AllPages" ShowClearFilterButton="true" />--%>

                                    <dx:BootstrapGridViewDataColumn Caption="Trip" FieldName="trip" VisibleIndex="1" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Tipo Duty" FieldName="TipoDuty" VisibleIndex="2" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>
                                    <dx:BootstrapGridViewDataColumn Caption="CheckIn" FieldName="CheckIn" VisibleIndex="3" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>
                                    <dx:BootstrapGridViewDataColumn Caption="CheckOut" FieldName="CheckOut" VisibleIndex="4" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>
                                    <dx:BootstrapGridViewDataColumn Caption="Fecha de Salida" FieldName="FechaSalida" VisibleIndex="5" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>
                                    <dx:BootstrapGridViewDataColumn Caption="Fecha de Llegada" FieldName="FechaLlegada" VisibleIndex="6" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>
                                    <dx:BootstrapGridViewDataColumn Caption="Origen" FieldName="Origen" VisibleIndex="7" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Destino" FieldName="Destino" VisibleIndex="8" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Matrícula" FieldName="matricula" VisibleIndex="9" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Piloto" FieldName="Piloto" VisibleIndex="10" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Copiloto" FieldName="Copiloto" VisibleIndex="11" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Estatus" FieldName="Estatus" VisibleIndex="12" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <%--<dx:BootstrapGridViewDataColumn FieldName="Estatus" VisibleIndex="9" Caption="Estatus" Width="120px">
                                        <DataItemTemplate>
                                            <dx:BootstrapImage runat="server" ID="imgTemplate" Width="15px" Height="15px" ImageAlign="AbsMiddle"
                                                ImageUrl='<%# "~/img/iconos/" + Eval("EstatusImg") %>'
                                                ToolTip='<%# Eval("Tooltip") %>'>
                                            </dx:BootstrapImage>
                                        </DataItemTemplate>
                                    </dx:BootstrapGridViewDataColumn>--%>
                                    
                                    <dx:BootstrapGridViewDataColumn FieldName="legid" Visible="false" VisibleIndex="13" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <%--<dx:BootstrapGridViewDataColumn FieldName="Tooltip" Visible="false" VisibleIndex="14" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="Estatus_Img" Visible="false" VisibleIndex="15" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />--%>

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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
