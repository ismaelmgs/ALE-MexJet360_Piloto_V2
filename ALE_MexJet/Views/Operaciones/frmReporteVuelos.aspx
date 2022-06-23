<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmReporteVuelos.aspx.cs" Inherits="ALE_MexJet.Views.Operaciones.frmReporteVuelos" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Styles/Controls.css" />
    <script type="text/javascript">
        function onSelectionGridViewAction(s, e) {
            $("#selectionLabel").html("Total rows selected: " + s.GetSelectedRowCount());
        }
        function alerta() {
            alert("Mensaje de prueba");
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


                    <table width="50%" style="text-align: left; margin: 0 auto;">
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-sm-12">

                            <dx:BootstrapGridView ID="gvVuelos" runat="server" KeyFieldName="vuelo"
                                OnRowCommand="gvVuelos_RowCommand" OnPageIndexChanged="gvVuelos_PageIndexChanged">
                                <SettingsSearchPanel Visible="true" ShowApplyButton="true" />
                                <Settings ShowGroupPanel="True" ShowFilterRowMenu="true" />
                                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                <Columns>
                                    <dx:BootstrapGridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="AllPages" />

                                    <dx:BootstrapGridViewDataColumn Caption="Trip" FieldName="vuelo" VisibleIndex="2" HorizontalAlign="Center"/>
                                    <dx:BootstrapGridViewDataColumn Caption="Clave Contrato" FieldName="claveContrato" VisibleIndex="3" />
                                    <dx:BootstrapGridViewDataColumn Caption="Matrícula" FieldName="Matricula" VisibleIndex="4" />
                                    <dx:BootstrapGridViewDataColumn Caption="Origen" FieldName="Origen" VisibleIndex="5" />
                                    <dx:BootstrapGridViewDataColumn Caption="Destino" FieldName="Destino" VisibleIndex="6" />
                                    <dx:BootstrapGridViewDataColumn Caption="País Origen" FieldName="PaisOrigen" VisibleIndex="7" />
                                    <dx:BootstrapGridViewDataColumn Caption="País Destino" FieldName="PaisDestino" VisibleIndex="8" />
                                    <dx:BootstrapGridViewDataColumn FieldName="Estatus" VisibleIndex="9" Caption="Viabilidad" Width="120px">
                                        <DataItemTemplate>
                                            <dx:BootstrapImage runat="server" ID="imgTemplate" Width="15px" Height="15px" ImageAlign="AbsMiddle"
                                                ImageUrl='<%# "~/img/iconos/" + Eval("EstatusImg") %>'
                                                ToolTip='<%# Eval("Tooltip") %>'>
                                            </dx:BootstrapImage>
                                        </DataItemTemplate>
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn Caption="Fecha Origen" FieldName="FechaHoraOrigen" VisibleIndex="10" />
                                    <dx:BootstrapGridViewDataColumn Caption="Fecha Destino" FieldName="FechaHoraDestino" VisibleIndex="11" />

                                    <dx:BootstrapGridViewDataColumn FieldName="leg_num" Visible="false" VisibleIndex="13" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="legid" Visible="false" VisibleIndex="12" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="Tooltip" Visible="false" VisibleIndex="14" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="Estatus_Img" Visible="false" VisibleIndex="15" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />

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
        <div class="row" style="padding-top: 30px;">
            <div class="col-md-12" align="center">
                <dx:ASPxButton ID="btnProcesar" runat="server" Text="Procesar Vuelos" Theme="Office2010Black" OnClick="btnProcesar_Click"></dx:ASPxButton>
            </div>
        </div>
    </asp:Panel>

    <%--MODAL PARA MENSAJES--%>
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
