<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmReporteVuelos.aspx.cs" Inherits="ALE_MexJet.Views.Operaciones.frmReporteVuelos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .MyCalendar .ajax__calendar_container {
            border: 1px solid #646464;
            background-color: lemonchiffon;
            color: red;
        }
    </style>
    <script type="text/javascript">
        function checkUncheckSelectableRowsOnPage(isChecked) {
            var selectableRowIndexes = gvVuelos.cp_SelectableRows;
            var grdStartIndex = gvVuelos.visibleStartIndex;
            var grdEndIndex = grdStartIndex + gvVuelos.pageRowCount;

            if (selectableRowIndexes != null && selectableRowIndexes != '') {
                var rowIdxes = selectableRowIndexes.split("#");
                var selectedRowsCount = 0;
                if (rowIdxes != null) {
                    try {
                        for (var i = 0; i < rowIdxes.length; i++) {
                            if (rowIdxes[i] != "") {
                                var rowIndex = parseInt(rowIdxes[i]);
                                if (rowIndex != NaN && rowIndex >= 0 && rowIndex >= grdStartIndex && rowIndex < grdEndIndex) {
                                    if (ASPxClientControl.GetControlCollection().GetByName("cbCheck" + rowIdxes[i]) != null) {
                                        if (isChecked) {
                                            gvVuelos.SelectRowOnPage(rowIdxes[i]);
                                            selectedRowsCount++;
                                        }
                                        else
                                            gvVuelos.UnselectRowOnPage(rowIdxes[i]);
                                        ASPxClientControl.GetControlCollection().GetByName("cbCheck" + rowIdxes[i]).SetChecked(isChecked);
                                    }
                                }
                            }
                        }
                        //updateSelectedKeys();   // Can be used if the selected keys needs to be saved separately in a Hidden field
                        gvVuelos.cp_SelectedRowsCount = selectedRowsCount;
                        currentSelectedRowsCount = selectedRowsCount;
                    }
                    finally {
                    }
                }
            }
        }
        var currentSelectedRowsCount = 0;
        function updateSelectedKeys(isChecked) {
            var selKeys = ASPxGridView1.GetSelectedKeysOnPage();
            if (selKeys != null) {
                var cpIDsList = '';
                try {
                    for (var i = 0; i < selKeys.length; i++) {
                        cpIDsList += selKeys[i] + ',';
                    }
                }
                finally {
                }
                //$("#hdnSelectedCatProdIDs").val(cpIDsList);
                if (isChecked) {
                    currentSelectedRowsCount++;
                    cbPageSelectAll.SetChecked(selKeys.length == gvVuelos.cp_SelectedRowsCount);
                }
                else {
                    cbPageSelectAll.SetChecked(selKeys.length == currentSelectedRowsCount);
                    currentSelectedRowsCount--;
                }
            }
        }
        function OnGridCallBackBegin(s, e) {
            currentSelectedRowsCount = gvVuelos.cp_SelectedRowsCount;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<cc1:ToolkitScriptManager ID="toolkit1" runat="server"></cc1:ToolkitScriptManager>--%>
    <div class="row">
        <div class="col-md-12">
            <br />
            <fieldset class="Personal">
                <legend>
                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda de Vuelos</span>
                </legend>
                <div class="col-sm-12">


                    <table width="50%" style="text-align: left; margin:0 auto;">
                        <tr>
                            <td></td>
                            <td>                   
                                <dx:ASPxDateEdit ID="date1" runat="server" EditFormat="Custom" Width="190" Caption="Desde" ClientInstanceName="Fecha1"
                                    DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true">
                                </dx:ASPxDateEdit>

                            </td>
                            <td></td>
                            <td>                   
                                <dx:ASPxDateEdit ID="date2" runat="server" EditFormat="Custom" Width="190" Caption="Hasta" ClientInstanceName="Fecha2"
                                    DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true">
                                </dx:ASPxDateEdit>

                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtTrip" runat="server" Caption="No. Trip" Theme="Office2010Black"></dx:ASPxTextBox>
                            </td>
                            <td align="left" valign="bottom">&nbsp;
                                <dx:ASPxButton ID="btnConsultaVuelos" runat="server" Text="Consulta vuelos" Theme="Office2010Black" OnClick="btnConsultaVuelos_Click"></dx:ASPxButton>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td align="left" valign="bottom">&nbsp;
                                
                            </td>
                        </tr>
                    </table>

                </div>
            </fieldset>
        </div>
    </div>
    <br />
    <asp:Panel ID="pnlVuelos" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-12" style="margin-left: -15px; width: 102%;">
                <asp:UpdatePanel  ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-sm-12">

                            <dx:ASPxGridView ID="gvVuelos" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                ClientInstanceName="gvVuelos" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                Theme="Office2010Black" Width="100%" OnRowCommand="gvVuelos_RowCommand" KeyFieldName ="vuelo"
                                
                                OnPageIndexChanged="gvVuelos_PageIndexChanged"
                                OnCommandButtonInitialize ="gvVuelos_CommandButtonInitialize"   
                                OnCustomButtonInitialize ="gvVuelos_CustomButtonInitialize" 
                                OnCustomJSProperties="gvVuelos_CustomJSProperties">                                                        
                                    <ClientSideEvents EndCallback="function (s, e) {
                                        if (s.cpShowPopup)
                                        {
                                            delete s.cpShowPopup;
                                            lbl.SetText(s.cpText);
                                            popup.Show();
                                        }
                                    }" />
                                <SettingsSearchPanel Visible="true" />
                                    <Columns>
                                        <%--<dx:GridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="1">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <DataItemTemplate>

                                                <dx:ASPxCheckBox ID="chkSelecciona" runat="server" Theme="Office2010Black" ToolTip="Seleccionar vuelo"></dx:ASPxCheckBox>

                                            </DataItemTemplate>
                                            <EditFormSettings Visible="false" />
                                        </dx:GridViewDataColumn>--%>

                                        <%--<dx:GridViewDataCheckColumn FieldName="vuelo" Caption="#" VisibleIndex="1">
                                            <DataItemTemplate>
                                                <dx:ASPxCheckBox ID="chkSelecciona" runat="server">
                                                </dx:ASPxCheckBox>
                                            </DataItemTemplate>
                                        </dx:GridViewDataCheckColumn>--%>

                                        <dx:GridViewDataTextColumn Caption="#" VisibleIndex="0">
                                            <DataItemTemplate>
                                                <dx:ASPxCheckBox ID="cbCheck" runat="server" AutoPostBack="false" CssClass="chkSelDgProdRow"/>
                                            </DataItemTemplate>
                                            <HeaderTemplate>
                                                <dx:ASPxCheckBox ID="cbPageSelectAll" runat="server" ToolTip="Select/Unselect all rows on the page" ClientInstanceName="cbPageSelectAll"
                                                    ClientSideEvents-CheckedChanged="function(s, e) { checkUncheckSelectableRowsOnPage(s.GetChecked()); }" OnLoad="cbPageSelectAll_Load" />
                                            </HeaderTemplate>
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn Caption="Trip" FieldName="vuelo" ShowInCustomizationForm="True" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Clave Contrato" FieldName="claveContrato" ShowInCustomizationForm="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Matrícula" FieldName="Matricula" ShowInCustomizationForm="True" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Origen" FieldName="Origen" ShowInCustomizationForm="True" VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Destino" FieldName="Destino" ShowInCustomizationForm="True" VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="País Origen" FieldName="PaisOrigen" ShowInCustomizationForm="True" VisibleIndex="7">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="País Destino" FieldName="PaisDestino" ShowInCustomizationForm="True" VisibleIndex="8">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataColumn FieldName="Estatus"  VisibleIndex="9" Caption="Viabilidad" HeaderStyle-HorizontalAlign="Center" Width="120px"> 
                                            <DataItemTemplate>
                                                <dx:ASPxImage runat="server" ID="imgTemplate" Width="15px" Height="15px" ImageAlign="AbsMiddle"
                                                    ImageUrl='<%# "~/img/iconos/" + Eval("EstatusImg") %>'
                                                    ToolTip='<%# Eval("Tooltip") %>'>
                                                </dx:ASPxImage>
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn Caption="Fecha Origen" FieldName="FechaHoraOrigen" ShowInCustomizationForm="True" VisibleIndex="10">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Fecha Destino" FieldName="FechaHoraDestino" ShowInCustomizationForm="True" VisibleIndex="11">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="legid" ShowInCustomizationForm="False" VisibleIndex="12" EditFormSettings-Visible="False" Visible="false">
                                        </dx:GridViewDataTextColumn>

                                        <%--<dx:GridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="8">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <DataItemTemplate>

                                                <dx:ASPxButton Text="Ajuste" Theme="Office2010Black" ID="btnAjuste" runat="server" CommandArgument='<%# Eval("IdRemision") %>' CommandName="Ajuste" AutoPostBack="true" 
                                                    ToolTip="Agregar Ajuste"></dx:ASPxButton>

                                            </DataItemTemplate>
                                            <EditFormSettings Visible="false" />
                                        </dx:GridViewDataColumn>--%>

                                    </Columns>
                                    <SettingsBehavior ConfirmDelete="True" />
                                    <SettingsPager Position="TopAndBottom">
                                        <PageSizeItemSettings Items="20, 50, 100">
                                        </PageSizeItemSettings>
                                    </SettingsPager>
                                    <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                                    <Settings ShowGroupPanel="True" />
                                    <%--<SettingsText ConfirmDelete="¿Desea eliminar?" />--%>
                                    <SettingsPopup>
                                        <EditForm HorizontalAlign="Center" VerticalAlign="Below" Width="400px" />
                                    </SettingsPopup>
                                   <%-- <SettingsCommandButton>
                                        <NewButton ButtonType="Link">
                                            <Image ToolTip="New">
                                            </Image>
                                        </NewButton>
                                        <UpdateButton Text="Guardar"></UpdateButton>
                                        <CancelButton Text ="Cancelar"></CancelButton>
                                        <EditButton Text="Editar"></EditButton>
                                    </SettingsCommandButton>--%>
                                </dx:ASPxGridView>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row" style="padding-top:30px;">
            <div class="col-md-12" align="center">
                 <dx:ASPxButton ID="btnProcesar" runat="server" Text="Procesar Vuelos" Theme="Office2010Black" OnClick="btnProcesar_Click"></dx:ASPxButton>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
