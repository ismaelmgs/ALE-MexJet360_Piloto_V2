<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmCalculoPagos.aspx.cs" Inherits="ALE_MexJet.Views.viaticos.frmCalculoPagos" UICulture="es" Culture="es-MX" %>

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
                        <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda</span>
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
                        <dx:BootstrapButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Width="100%">
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
                            <dx:BootstrapGridView ID="gvCalculo" runat="server" KeyFieldName="idBitacora"
                                OnRowCommand="gvCalculo_RowCommand">
                                <SettingsSearchPanel Visible="true" ShowApplyButton="true" />
                                <Settings ShowGroupPanel="True" ShowFilterRowMenu="true" />
                                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                <SettingsPager PageSize="20"></SettingsPager>
                                <Columns>
                                    <%--<dx:BootstrapGridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="AllPages" ShowClearFilterButton="true" />--%>

                                    <dx:BootstrapGridViewDataColumn Caption="Piloto" FieldName="Piloto" VisibleIndex="1" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Clave Piloto" FieldName="cvePiloto" VisibleIndex="2" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />

                                    <%--<dx:BootstrapGridViewDataColumn Caption="Trip" FieldName="trip" VisibleIndex="2" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="LegID" FieldName="leg_id" VisibleIndex="3" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>--%>
                                    <dx:BootstrapGridViewDataColumn Caption="Fecha inicio de período" FieldName="Fecha1" VisibleIndex="3" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>
                                    <dx:BootstrapGridViewDataColumn Caption="Fecha final de período" FieldName="Fecha2" VisibleIndex="4" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>
                                    <%--<dx:BootstrapGridViewDataColumn Caption="Origen" FieldName="Origen" VisibleIndex="6" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Destino" FieldName="Destino" VisibleIndex="7" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />--%>
                                    <%--<dx:BootstrapGridViewDataColumn Caption="Matrícula" FieldName="matricula" VisibleIndex="9" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />--%>
                                    
                                    <%--<dx:BootstrapGridViewDataColumn Caption="Estatus" FieldName="Estatus" VisibleIndex="5" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />--%>
                                    <dx:BootstrapGridViewDataColumn VisibleIndex="5" Caption="Acciones" Width="120px" FieldName="idBitacora">
                                        <DataItemTemplate>
                                            <%--<dx:BootstrapImage runat="server" ID="imgTemplate" Width="15px" Height="15px" ImageAlign="AbsMiddle"
                                                ImageUrl='<%# "~/img/iconos/" + Eval("EstatusImg") %>'
                                                ToolTip='<%# Eval("Tooltip") %>'>
                                            </dx:BootstrapImage>--%>

                                            <dx:BootstrapButton Text="Ver viáticos" ID="btnVerViaticos" runat="server" CommandArgument='<%# Eval("idBitacora") %>' CommandName="Ver" AutoPostBack="true" 
                                                ToolTip="Calcular viáticos" SettingsBootstrap-RenderOption="Primary"></dx:BootstrapButton>

                                        </DataItemTemplate>
                                    </dx:BootstrapGridViewDataColumn>
                                    
                                    <%--<dx:BootstrapGridViewDataColumn FieldName="legid" Visible="false" VisibleIndex="13" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />--%>
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

    <asp:Panel ID="pnlCalcularViaticos" runat="server" Visible="false">
        <div class="row">
            <div class="col-lg-1">
                <asp:HiddenField ID="hdnIdBitacora" runat="server" />
                <label>Piloto:</label>
            </div>
            <div class="col-lg-2" align="left">
                <asp:Label ID="readPiloto" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-lg-2">
                <label>Clave de Piloto:</label>
            </div>
            <div class="col-lg-1" align="left">
                <asp:Label ID="readCvePiloto" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-lg-2">
                <label>Fecha Inicio de período:</label>
            </div>
            <div class="col-lg-1" align="left">
                <asp:Label ID="readFechaInicio" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-lg-2">
                <label>Fecha Fin de período:</label>
            </div>
            <div class="col-lg-1" align="left">
                <asp:Label ID="readFechaFin" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <br />
        <div id="divViaticos" runat="server"></div>
        <br />
        <div class="row">
            <div class="col-md-12" style="margin-left: -15px; width: 102%;">
                <asp:UpdatePanel ID="upaVuelos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-sm-12">
                            <dx:BootstrapGridView ID="gvVuelos" runat="server" KeyFieldName="LegId">
                                <SettingsSearchPanel Visible="false" ShowApplyButton="false" />
                                <Settings ShowGroupPanel="false" ShowFilterRowMenu="false" />
                                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                <SettingsPager PageSize="20"></SettingsPager>
                                <Columns>
                                    <%--<dx:BootstrapGridViewDataColumn Caption="Trip" FieldName="Trip" VisibleIndex="1" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />--%>
                                    <dx:BootstrapGridViewDataColumn Caption="LegId" FieldName="LegId" VisibleIndex="1" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="POD Salida" FieldName="POD" VisibleIndex="2" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>
                                    <dx:BootstrapGridViewDataColumn Caption="POA Destino" FieldName="POA" VisibleIndex="3" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>
                                    <dx:BootstrapGridViewDataColumn Caption="Fecha de Salida" FieldName="FechaSalida" VisibleIndex="4" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>
                                    <dx:BootstrapGridViewDataColumn Caption="Fecha de Llegada" FieldName="FechaLlegada" VisibleIndex="5" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>
                                    <%--<dx:BootstrapGridViewDataColumn Caption="Check In" FieldName="CheckIn" VisibleIndex="7" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>
                                    <dx:BootstrapGridViewDataColumn Caption="Check Out" FieldName="CheckOut" VisibleIndex="8" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>--%>
                                </Columns>
                                <SettingsPager Position="Bottom">
                                    <PageSizeItemSettings Items="20, 50, 100"></PageSizeItemSettings>
                                </SettingsPager>
                                <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                                <Settings ShowGroupPanel="True" />
                            </dx:BootstrapGridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <dx:BootstrapFormLayout runat="server">
                <Items>                 
                    <dx:BootstrapLayoutItem HorizontalAlign="Right" ShowCaption="False" ColSpanMd="12">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:BootstrapButton ID="btnGuardarPeriodo" runat="server" Text="Guardar Período" SettingsBootstrap-RenderOption="Primary" AutoPostBack="true" OnClick="btnGuardarPeriodo_Click" />
                                <%--<dx:BootstrapButton ID="btnAutorizar" runat="server" Text="Autorizar" SettingsBootstrap-RenderOption="Success" AutoPostBack="true" OnClick="btnAutorizar_Click" />--%>
                                <dx:BootstrapButton ID="btnCancelar" runat="server" Text="Regresar" SettingsBootstrap-RenderOption="Warning" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e) { document.location.reload(); }" />
                                </dx:BootstrapButton>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:BootstrapLayoutItem>
                </Items>
            </dx:BootstrapFormLayout>
        </div>
    </asp:Panel>

</asp:Content>
