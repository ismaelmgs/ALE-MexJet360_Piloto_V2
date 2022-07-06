<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmConsultaBitacoras.aspx.cs" Inherits="ALE_MexJet.Views.Operaciones.frmConsultaBitacoras" UICulture="es" Culture="es-MX" %>

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
    <style>
        .hiddenRow {
            visibility:hidden !important;
        }
        .centerCell {
            text-align:center !important;
            color: #337ab7 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="pnlBusqueda" runat="server" Visible="false">
         <div class="row">
            <div class="col-md-12">
                <br />
                <fieldset class="Personal">
                    <legend>
                        <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda de Vuelos</span>
                    </legend>
                    <div class="row">
                        <div class="col-sm-2">&nbsp;</div>
                        <div class="col-sm-2">&nbsp;</div>
                        <div class="col-sm-2">&nbsp;</div>
                        <div class="col-sm-2">&nbsp;</div>
                        <div class="col-sm-2">&nbsp;</div>
                        <div class="col-sm-2">&nbsp;</div>
                    </div>
                </fieldset>
            </div>
        </div>
    </asp:Panel>
   
    <br />
    <asp:Panel ID="pnlBitacoras" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-12" style="margin-left: -15px; width: 102%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-sm-12">

                            <dx:BootstrapGridView ID="gvBitacoras" runat="server" KeyFieldName="idBitacora"
                                OnRowCommand="gvBitacoras_RowCommand" OnPageIndexChanged="gvBitacoras_PageIndexChanged">
                                <SettingsSearchPanel Visible="true" ShowApplyButton="true" />
                                <Settings ShowGroupPanel="True" ShowFilterRowMenu="true" />
                                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                <SettingsPager PageSize="20"></SettingsPager>
                                <Columns>
                                    <dx:BootstrapGridViewDataColumn Caption="Leg Id" FieldName="leg_id" VisibleIndex="1" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Folio" FieldName="folio" VisibleIndex="2" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Flight Off" FieldName="flight_off" VisibleIndex="3" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Flight On" FieldName="flight_on" VisibleIndex="4" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Flight Diff" FieldName="flight_diff" VisibleIndex="5" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Calzo In" FieldName="calzo_in" VisibleIndex="6" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Calzo Out" FieldName="calzo_out" VisibleIndex="7" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Calzo Diff" FieldName="calzo_diff" VisibleIndex="8" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Fuel In" FieldName="fuel_in" VisibleIndex="9" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Fuel Out" FieldName="fuel_out" VisibleIndex="10" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Fuel Diff" FieldName="fuel_diff" VisibleIndex="11" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Quién autorizó" FieldName="autorizacion" VisibleIndex="12" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Cuando autorizó" FieldName="fecha_autorizacion" VisibleIndex="13" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Estatus" FieldName="desestatus" VisibleIndex="14" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn FieldName="estatus" VisibleIndex="15" HorizontalAlign="Center" CssClasses-DataCell="hiddenRow" CssClasses-HeaderCell="centerCell" 
                                        HeaderBadge-CssClass="hiddenRow" Visible="false" CssClasses-EditCell="centerCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="16" HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <div>
                                                <dx:BootstrapButton Text="Actualizar" ID="btnActualiza" runat="server" CommandArgument='<%# Eval("idBitacora") %>' CommandName="Actualiza" AutoPostBack="true" 
                                                    ToolTip="Actualiza" SettingsBootstrap-RenderOption="Primary"></dx:BootstrapButton>
                                            </div>
                                        </DataItemTemplate>
                                    </dx:BootstrapGridViewDataColumn>

                                </Columns>
                            </dx:BootstrapGridView>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>
    <br />

    <asp:Panel ID="pnlActualizaBitacora" runat="server" Visible="false">
        <div class="row">

            <dx:BootstrapFormLayout runat="server">
                <Items>
                    <dx:BootstrapLayoutGroup Caption="Información de Bitácora">
                        <Items>
                            <dx:BootstrapLayoutItem HorizontalAlign="Right" ShowCaption="False" ColSpanMd="12">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapButton ID="btnVerImagen" runat="server" Text="Ver imagen" SettingsBootstrap-RenderOption="Primary" AutoPostBack="true" OnClick="btnVerImagen_Click" />
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>
                        </Items>
                        <Items>
                            <dx:BootstrapLayoutItem Caption="LegID" ColSpanMd="3">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapTextBox ID="txtLegId" runat="server" Text="" Enabled="false" />
                                        <asp:HiddenField ID="hdnIdBitacora" runat="server" />
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>
                            <dx:BootstrapLayoutItem Caption="Folio" ColSpanMd="3">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapTextBox ID="txtFolio" runat="server" Text="" Enabled="false" />
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>
                            <dx:BootstrapLayoutItem Caption="Flight Off" ColSpanMd="3">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapTextBox ID="txtFlightOff" runat="server" Text="" />
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>
                            <dx:BootstrapLayoutItem Caption="Flight On" ColSpanMd="3">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapTextBox ID="txtFlightOn" runat="server" Text="" />
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>
                        </Items>
                        <Items>
                            <dx:BootstrapLayoutItem Caption="Flight Diff" ColSpanMd="3">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapTextBox ID="txtFlightDiff" runat="server" Text="" />
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>
                            <dx:BootstrapLayoutItem Caption="Calzo In" ColSpanMd="3">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapTextBox ID="txtCalzoIn" runat="server" Text="" />
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>
                            <dx:BootstrapLayoutItem Caption="Calzo Out" ColSpanMd="3">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapTextBox ID="txtCalzoOut" runat="server" Text="" />
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>
                            <dx:BootstrapLayoutItem Caption="Calzo Diff" ColSpanMd="3">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapTextBox ID="txtCalzoDiff" runat="server" Text="" />
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>
                        </Items>
                        <Items>
                            <dx:BootstrapLayoutItem Caption="Fuel In" ColSpanMd="3">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapTextBox ID="txtFuelIn" runat="server" Text="" />
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>
                            <dx:BootstrapLayoutItem Caption="Fuel Out" ColSpanMd="3">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapTextBox ID="txtFuelOut" runat="server" Text="" />
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>
                            <dx:BootstrapLayoutItem Caption="Fuel Diff" ColSpanMd="3">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapTextBox ID="txtFuelDiff" runat="server" Text="" />
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>

                        </Items>
                    </dx:BootstrapLayoutGroup>
                    
                    <dx:BootstrapLayoutItem HorizontalAlign="Right" ShowCaption="False" ColSpanMd="12">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:BootstrapButton ID="btnActualizar" runat="server" Text="Actualizar" SettingsBootstrap-RenderOption="Primary" AutoPostBack="true" OnClick="btnActualizar_Click" />
                                <dx:BootstrapButton ID="btnAutorizar" runat="server" Text="Autorizar" SettingsBootstrap-RenderOption="Success" AutoPostBack="true" OnClick="btnAutorizar_Click" />
                                <dx:BootstrapButton ID="btnCancelar" runat="server" Text="Cancelar" SettingsBootstrap-RenderOption="Warning" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e) { document.location.reload(); }" />
                                </dx:BootstrapButton>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:BootstrapLayoutItem>
                </Items>
            </dx:BootstrapFormLayout>


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

    <%--MODAL PARA VER IMAGEN--%>
    <dx:ASPxPopupControl ID="ppVerImagen" runat="server" ClientInstanceName="ppVerImagen" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true" Width="300">
        <ClientSideEvents />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

</asp:Content>
