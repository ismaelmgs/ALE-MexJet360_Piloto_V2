<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="frmRolAccion.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ALE_MexJet.Views.Catalogos.frmRolAccion" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        var editIndex;
        function OnBatchStartEditingGrid(s, e) {
            editIndex = e.visibleIndex;
        }

        function OnCheckContactChanged(s, e) {

            var changedValue = s.GetValue();
            var RowsVisibles = gvRolAccion.GetVisibleRowsOnPage();
            if (changedValue) {
                for (var i = 0; i < RowsVisibles ; i++) {
                    if (editIndex == i)
                        gvRolAccion.batchEditApi.SetCellValue(i, "Acceso", false);
                }
            }
        }
    </script>
  

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                <div class="col-md-12">
                    <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Permisos por Rol</span>
                </div>
                </div>
                <div class="well-g">
                <div class="row">
                    <div class="col-md-12">
                        <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                            <fieldset class="Personal">
                                <legend>
                                    <span style="font-family:Helvetica, Arial,sans-serif; text-align:center;">Selección de Rol</span>
                                </legend>
                                <div class="col-sm-12">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxComboBox ID="ddlRol" runat="server" Theme="Office2010Black" EnableTheming="True" AutoPostBack="true" OnSelectedIndexChanged="ddlRol_SelectedIndexChanged" Width="50%">
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td>
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
                <div class="col-md-12" style="margin-left:-15px;width:103%;">
                    <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload" >
                        <ContentTemplate>
                                <div class="col-sm-12">
                                    <dx:ASPxGridView ID="gvRolAccion" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                        ClientInstanceName="gvRolAccion" EnableTheming="True" KeyFieldName="ModuloId" OnCellEditorInitialize="gvRolAccion_CellEditorInitialize" Styles-Header-HorizontalAlign ="Center"
                                        Theme="Office2010Black" Width="100%" EnableRowsCache="false" OnBatchUpdate="gvRolAccion_BatchUpdate" OnRowUpdating="gvRolAccion_RowUpdating">
                                        <ClientSideEvents BatchEditStartEditing="OnBatchStartEditingGrid" EndCallback ="function (s, e) {
                                        if (s.cpShowPopup)
                                        {
                                            delete s.cpShowPopup;
                                            lbl.SetText(s.cpText);
                                            popup.Show();
                                        }
                                    }" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Descripción Módulo" FieldName="DescripcionModulo" ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataCheckColumn Caption="Acceso" FieldName="Acceso" VisibleIndex="2">
                                                <PropertiesCheckEdit AllowGrayed="true" AllowGrayedByClick="false" ValueChecked="1" ValueUnchecked="0" ValueType="System.Byte"></PropertiesCheckEdit>
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataCheckColumn Caption="Consultar" FieldName="Consultar"  VisibleIndex="3" >
                                                <PropertiesCheckEdit AllowGrayed="true" AllowGrayedByClick="false" ValueChecked="1" ValueUnchecked="0" ValueType="System.Byte"></PropertiesCheckEdit>
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataCheckColumn Caption="Insertar" FieldName="Insertar"  VisibleIndex="4" >
                                                <PropertiesCheckEdit AllowGrayed="true" AllowGrayedByClick="false" ValueChecked="1" ValueUnchecked="0" ValueType="System.Byte"></PropertiesCheckEdit>
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataCheckColumn Caption="Actualizar" FieldName="Actualizar" VisibleIndex="5" >
                                                <PropertiesCheckEdit AllowGrayed="true" AllowGrayedByClick="false" ValueChecked="1" ValueUnchecked="0" ValueType="System.Byte"></PropertiesCheckEdit>
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataCheckColumn Caption="Eliminar" FieldName="Eliminar"  VisibleIndex="6" >
                                                <PropertiesCheckEdit AllowGrayed="true" AllowGrayedByClick="false" ValueChecked="1" ValueUnchecked="0" ValueType="System.Byte"></PropertiesCheckEdit>
                                            </dx:GridViewDataCheckColumn>
                                        </Columns>
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <SettingsPopup>
                                            <EditForm HorizontalAlign="WindowCenter" VerticalAlign="Middle" Width="400px" />
                                            <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="Middle"/>
                                        </SettingsPopup>
                                        <SettingsEditing Mode="Batch" BatchEditSettings-EditMode="Cell" BatchEditSettings-StartEditAction="Click"></SettingsEditing>
                                        <SettingsSearchPanel Visible="True"/>
                                        <SettingsText CommandBatchEditUpdate="Guardar Configuración" CommandBatchEditCancel="Revertir Cambios" />
                                    </dx:ASPxGridView>
                                </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlRol" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </div>
                </div>
                <br />
                <br />  
              </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

    <dx:ASPxPopupControl ID="popup" runat="server" ClientInstanceName="popup" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Registro Modificado" AllowDragging="True">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">

                <dx:ASPxPanel ID="ASPxPanel2" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxImage ID="ASPxImage2" runat="server" ShowLoadingImage="true" ImageUrl="~/img/iconos/Information2.ico"></dx:ASPxImage>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbl" runat="server" ClientInstanceName="lbl" Text="ASPxLabel"></dx:ASPxLabel>
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
