<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmAnalisisFerrys.aspx.cs" Inherits="ALE_MexJet.Views.Consultas.frmAnalisisFerrys" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var textSeparator = ";";
        function OnListBoxSelectionChanged(listBox, args) {
            if (args.index == 0)
                args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
            UpdateSelectAllItemState();
            UpdateText();
        }
        function UpdateSelectAllItemState() {
            IsAllSelected() ? checkListBox.SelectIndices([0]) : checkListBox.UnselectIndices([0]);
        }
        function IsAllSelected() {
            var selectedDataItemCount = checkListBox.GetItemCount() - (checkListBox.GetItem(0).selected ? 0 : 1);
            return checkListBox.GetSelectedItems().length == selectedDataItemCount;
        }
        function UpdateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(GetSelectedItemsText(selectedItems));
        }
        function SynchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = GetValuesByTexts(texts);
            checkListBox.SelectValues(values);
            UpdateSelectAllItemState();
            UpdateText(); // for remove non-existing texts
        }
        function GetSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                if (items[i].index != 0)
                    texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function GetValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Análisis de Ferrys</span>
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
                                    <table style="width: 100%;" border="0">
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lblFechaIni" runat="server" Text="Fecha Inicial" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxDateEdit ID="deFechaIni" runat="server" Theme="Office2010Black"></dx:ASPxDateEdit>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblTipoPierna" runat="server" Text="Tipo Pierna" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxDropDownEdit ID="ddeTipoPierna" ClientInstanceName="checkComboBox" runat="server" Theme="Office2010Black" AnimationType="None">
                                                    <DropDownWindowTemplate>
                                                        <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                                                            runat="server">
                                                            <ClientSideEvents SelectedIndexChanged="OnListBoxSelectionChanged" />
                                                        </dx:ASPxListBox>
                                                    </DropDownWindowTemplate>
                                                    <ClientSideEvents TextChanged="SynchronizeListBoxValues" DropDown="SynchronizeListBoxValues" />
                                                </dx:ASPxDropDownEdit>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnBusqueda" runat="server" Text="Buscar" Theme="Office2010Black" OnClick="btnBusqueda_Click"></dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always" OnUnload="UpdatePanel1_Unload">
                                <ContentTemplate>
                                    <div>
                                        <dx:ASPxGridView ID="gvPiernas" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvNotaCredito" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign="Center"
                                            Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90" KeyFieldName="IdTipoPierna">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="TipoPiernaDescripcion" Caption="TipoPiernaDescripcion" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="HorasVuelo" Caption="HorasVuelo" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="HorasCalzo" Caption="HorasCalzo" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Templates>
                                                <DetailRow>

                                                    <dx:ASPxGridView ID="gvDetall" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado" OnBeforePerformDataSelect="gvDetall_BeforePerformDataSelect"
                                                        ClientInstanceName="gvDetall" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign="Center" OnCellEditorInitialize="gvDetall_CellEditorInitialize" OnStartRowEditing="gvDetall_StartRowEditing"
                                                        Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90" KeyFieldName="IdBitacora" OnRowUpdating="gvDetall_RowUpdating">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="AeronaveSerie" Caption="AeronaveSerie" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="AeronaveMatricula" Caption="AeronaveMatricula" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Origen" Caption="Origen" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Destino" Caption="Destino" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="CantPax" Caption="CantPax" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataDateColumn FieldName="OrigenVuelo" Caption="OrigenVuelo" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" PropertiesDateEdit-DropDownButton-Enabled="false">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataDateColumn FieldName="OrigenCalzo" Caption="OrigenCalzo" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" PropertiesDateEdit-DropDownButton-Enabled="false">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataDateColumn FieldName="DestinoVuelo" Caption="DestinoVuelo" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" PropertiesDateEdit-DropDownButton-Enabled="false">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataDateColumn FieldName="DestinoCalzo" Caption="DestinoCalzo" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" PropertiesDateEdit-DropDownButton-Enabled="false">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataTextColumn FieldName="TripNum" Caption="TripNum" VisibleIndex="9" HeaderStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="FolioReal" Caption="FolioReal" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataComboBoxColumn Caption="TipoPiernaDescripcion" FieldName="TipoPiernaDescripcion" Visible="true" VisibleIndex="12">
                                                                <EditFormSettings Visible="True"></EditFormSettings>
                                                                <PropertiesComboBox NullText="Seleccione una opción" NullDisplayText="Seleccione una opción" ValueField="TipoPiernaDescripcion">
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewCommandColumn ButtonType="Button" Caption="Acciones" ShowEditButton="True" ShowNewButton="false" VisibleIndex="13">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewCommandColumn>
                                                        </Columns>
                                                        <Settings HorizontalScrollBarMode="Auto" />
                                                        <SettingsCommandButton>
                                                            <NewButton ButtonType="Link">
                                                                <Image ToolTip="New">
                                                                </Image>
                                                            </NewButton>
                                                            <UpdateButton Text="Guardar"></UpdateButton>
                                                            <CancelButton></CancelButton>
                                                            <EditButton>
                                                                <Image Height="20px" ToolTip="Modificar" Width="20px">
                                                                </Image>
                                                            </EditButton>
                                                            <DeleteButton>
                                                                <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                                                </Image>
                                                            </DeleteButton>
                                                        </SettingsCommandButton>
                                                        <SettingsPager Position="TopAndBottom">
                                                        </SettingsPager>
                                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                                    </dx:ASPxGridView>

                                                </DetailRow>
                                            </Templates>
                                            <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                            <SettingsSearchPanel Visible="true" />
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvNotaCredito">
                                        </dx:ASPxGridViewExporter>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBusqueda" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div style="text-align: right">
                            </div>
                        </div>
                    </div>
                </div>

            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>
