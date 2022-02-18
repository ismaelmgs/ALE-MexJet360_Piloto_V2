<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmOfertaFerry.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ALE_MexJet.Views.JetSmart.frmOfertaFerry" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Styles/Controls.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var textSeparator = ";";
        function OnListBoxSelectionChanged(listBox, args) {
            if (args.index == 0)
                args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
            UpdateSelectAllItemState(listBox);
            //UpdateText(listBox, dropDown);
        }

        function UpdateSelectAllItemState(listBox) {
            IsAllSelected(listBox) ? listBox.SelectIndices([0]) : listBox.UnselectIndices([0]);
        }

        function IsAllSelected(listBox) {
            var selectedDataItemCount = listBox.GetItemCount() - (listBox.GetItem(0).selected ? 0 : 1);
            return listBox.GetSelectedItems().length == selectedDataItemCount;
        }

        function UpdateText(listBox, dropDown) {
            var selectedItems = listBox.GetSelectedItems();
            dropDown.SetText(GetSelectedItemsText(selectedItems));
        }

        function SynchronizeListBoxValues(dropDown, args, listBox) {

            //var ListBox = document.getElementById(ListBoxName);

            listBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = GetValuesByTexts(texts, listBox);
            listBox.SelectValues(values);
            UpdateSelectAllItemState(listBox);
            UpdateText(listBox, dropDown); // for remove non-existing texts
        }

        function GetSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                if (items[i].index != 0)
                    texts.push(items[i].text);
            return texts.join(textSeparator);
        }

        function GetValuesByTexts(texts, listBox) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = listBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }

        /*
        Se agregan los mismos métodos para el control de SMS's
        */

        function OnListBoxSelectionChangedSMS(listBoxSMS, args, dropDown) {
            if (args.index == 0)
                args.isSelected ? listBoxSMS.SelectAll() : listBoxSMS.UnselectAll();
            UpdateSelectAllItemStateSMS(listBoxSMS);
            UpdateTextSMS(listBoxSMS, dropDown);
        }
        function UpdateSelectAllItemStateSMS(listBox) {
            IsAllSelectedSMS(listBox) ? listBox.SelectIndices([0]) : listBox.UnselectIndices([0]);
        }

        function IsAllSelectedSMS(listBox) {
            var selectedDataItemCount = listBox.GetItemCount() - (listBox.GetItem(0).selected ? 0 : 1);
            return listBox.GetSelectedItems().length == selectedDataItemCount;
        }

        function UpdateTextSMS(listBox, dropDown) {
            var selectedItems = listBox.GetSelectedItems();
            dropDown.SetText(GetSelectedItemsTextSMS(selectedItems));
        }

        function SynchronizeListBoxValuesSMS(dropDown, args, listBoxSMS) {

            listBoxSMS.UnselectAll();

            var texts = dropDown.GetText().split(textSeparator);

            var values = GetValuesByTextsSMS(texts, listBoxSMS);

            listBoxSMS.SelectValues(values);

            UpdateSelectAllItemStateSMS(listBoxSMS);

            UpdateTextSMS(listBoxSMS, dropDown); // for remove non-existing texts
        }
        function GetSelectedItemsTextSMS(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                if (items[i].index != 0)
                    texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function GetValuesByTextsSMS(texts, listBoxSMS) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = listBoxSMS.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }

        function OnValidation(s, e) {

            var hours = e.value.substring(0, 2);
            var minutes = e.value.substring(3, 5);

            if (hours > "99" || !hours || hours == "" || hours == null)
                hours = "00";

            if (minutes > "59" || !minutes || minutes == "")
                minutes = "00";

            e.value = hours + ':' + minutes;
        }

        function CerrarDropDownList(checkComboBox) {
            checkComboBox.HideDropDown();
        }

        $("[src*=max]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../../img/iconos/min.png");
        });
        $("[src*=min]").live("click", function () {
            $(this).attr("src", "../../img/iconos/max.png");
            $(this).closest("tr").next().remove();
        });

        function ObtenerValorOrigenFV(IdFerry) {
            //hfOrigenFV.Set('OrigenFV_value', OrigenFV);
            hfIdFerry.Set('IdFerry_value', IdFerry);
            return true;
        }

        function ConfirmarEliminarRegistro() {

            if (confirm("¿Está seguro que desea eliminar el registro?") == true) {
                return true;
            }
            else {
                return false;
            }
        }

        function CambiarChecks(checkbox, gridViewName) {

            var rowData = checkbox.parentNode.parentNode;

            var rowIndex = rowData.rowIndex

            var tablaPadre = document.getElementById(gridViewName);

            var gridViewNameHijo = tablaPadre.rows[rowIndex].cells[1].children[1].children[0].children[0];

            var tablaHijo = document.getElementById(gridViewNameHijo.id);

            if (tablaHijo.innerText != "No se encontraron registros para mostrar. ") {

                var filas = tablaHijo.rows.length;

                if (checkbox.checked == true) {

                    for (i = 1; i <= filas - 2; i++) {

                        if (tablaHijo.rows[i].cells[0].childNodes[1] != undefined) {

                            var checkboxHijo = tablaHijo.rows[i].cells[0].childNodes[1];

                            var check = document.getElementById(checkboxHijo.id);

                            check.checked = true;

                        }
                    }
                }
                else {

                    for (i = 1; i <= filas - 2; i++) {

                        if (tablaHijo.rows[i].cells[0].childNodes[1] != undefined) {

                            var checkboxHijo = tablaHijo.rows[i].cells[0].childNodes[1];

                            var check = document.getElementById(checkboxHijo.id);

                            check.checked = false;

                        }
                    }
                }
            }

        }

        function onSelectionGridViewAction(s, e) {
            $("#selectionLabel").html("Total rows selected: " + s.GetSelectedRowCount());
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="pnlPrincipal" runat="server" Width="100%" BackColor="White" Style="border-radius: 14px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Oferta de tramos ferrys</span>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" OnUnload="UpdatePanel1_Unload">
                    <ContentTemplate>
                        <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                        <div class="well-g">
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset class="Personal">
                                        <legend>
                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda general</span>
                                        </legend>
                                        <div class="col-sm-12">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <div class="row">
                                                            <div class="col-lg-4">
                                                                <dx:BootstrapComboBox ID="ddlEstatus" runat="server" AutoPostBack="true" OnValueChanged="ddlEstatus_ValueChanged">
                                                                    <Items>
                                                                        <dx:BootstrapListEditItem Text="Pendientes" Value="1" Selected="true"></dx:BootstrapListEditItem>
                                                                        <dx:BootstrapListEditItem Text="Enviadas" Value="2"></dx:BootstrapListEditItem>
                                                                    </Items>
                                                                </dx:BootstrapComboBox>
                                                            </div>
                                                            <div class="col-lg-4"></div>
                                                            <div class="col-lg-4"></div>
                                                        </div>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td style="text-align: right"></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <dx:BootstrapButton ID="btnNuevoFerry" runat="server" Text="Agregar Ferry" OnClick="btnNuevoFerry_Click">
                                        <SettingsBootstrap RenderOption="Info" />
                                    </dx:BootstrapButton>
                                </div>
                                <div class="col-md-6" style="text-align: right;">
                                    <dx:ASPxLabel runat="server" Theme="Aqua" Text="Exportar a:"></dx:ASPxLabel>
                                    &nbsp;
                                    <dx:BootstrapButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                        <SettingsBootstrap RenderOption="Secondary" />
                                    </dx:BootstrapButton>
                                </div>
                            </div>
                            <br />
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 95%; text-align: center">
                                        <dx:BootstrapGridView ID="gvFerrys" runat="server" KeyFieldName="IdFerry"
                                            OnRowCommand="gvFerrys_RowCommand">
                                            <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                            <Columns>
                                                <dx:BootstrapGridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="None" />
                                                <dx:BootstrapGridViewDataColumn>
                                                    <DataItemTemplate>
                                                        <dx:BootstrapImage ID="imbSemaforo" runat="server" Width="20px" Height="20px"
                                                            ImageAlign="AbsMiddle" ImageUrl='<%# "~/img/iconos/" + Eval("EstatusImg") %>'>
                                                        </dx:BootstrapImage>
                                                    </DataItemTemplate>
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn FieldName="IdFerry" Caption="Id ferry" VisibleIndex="1" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" />
                                                <dx:BootstrapGridViewDataColumn FieldName="Trip" Caption="TRIP" VisibleIndex="2" SortIndex="0" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                <dx:BootstrapGridViewDataColumn FieldName="NoPierna" Caption="No. Pierna" VisibleIndex="3" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                <dx:BootstrapGridViewDataColumn FieldName="Origen" Caption="Origen" VisibleIndex="4" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                <dx:BootstrapGridViewDataColumn FieldName="FechaSalida" Caption="Fecha salida" VisibleIndex="5" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                <dx:BootstrapGridViewDataColumn FieldName="Destino" Caption="Destino" VisibleIndex="6" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                <dx:BootstrapGridViewDataColumn FieldName="FechaLlegada" Caption="Fecha llegada" VisibleIndex="7" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" />
                                                <dx:BootstrapGridViewDataColumn FieldName="TiempoVuelo" Caption="Tiempo vuelo" VisibleIndex="8" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                <dx:BootstrapGridViewDataColumn FieldName="Matricula" Caption="Matricula" VisibleIndex="9" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                <dx:BootstrapGridViewDataColumn FieldName="GrupoModelo" Caption="Grupo modelo" VisibleIndex="10" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                <dx:BootstrapGridViewDataColumn Caption="Acciones" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center">
                                                    <DataItemTemplate>
                                                        <dx:BootstrapButton ID="btnAgregarFerryHijo" runat="server" Text="Agregar" CommandArgument='<%# Eval("Origen").ToString() + "/" + Eval("IdFerry").ToString()%>' CommandName="Agregar"></dx:BootstrapButton>
                                                        <dx:BootstrapButton ID="btnEliminarFerry" runat="server" Text="Eliminar" OnClick="btnEliminarFerry_Click" CommandName="EliminarFerry" CommandArgument='<%# Eval("IdFerry").ToString()%>'>
                                                        </dx:BootstrapButton>
                                                    </DataItemTemplate>
                                                </dx:BootstrapGridViewDataColumn>
                                            </Columns>
                                            <Templates>
                                                <DetailRow>
                                                    <dx:BootstrapGridView ID="gvFerrysDetalle" runat="server" KeyFieldName="IdFerry"
                                                        OnRowCommand="gvFerrysHijo_RowCommand"
                                                        OnBeforePerformDataSelect="gvFerrysDetalle_BeforePerformDataSelect">
                                                        <Columns>
                                                            <dx:BootstrapGridViewDataColumn FieldName="IdFerry" Caption="Id ferry" VisibleIndex="1" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" />
                                                            <dx:BootstrapGridViewDataColumn FieldName="Trip" Caption="TRIP" VisibleIndex="2" SortIndex="0" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                            <dx:BootstrapGridViewDataColumn FieldName="NoPierna" Caption="No. Pierna" VisibleIndex="3" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                            <dx:BootstrapGridViewDataColumn FieldName="Origen" Caption="Origen" VisibleIndex="4" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                            <dx:BootstrapGridViewDataColumn FieldName="FechaSalida" Caption="Fecha salida" VisibleIndex="5" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                            <dx:BootstrapGridViewDataColumn FieldName="Destino" Caption="Destino" VisibleIndex="6" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                            <dx:BootstrapGridViewDataColumn FieldName="FechaLlegada" Caption="Fecha llegada" VisibleIndex="7" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" />
                                                            <dx:BootstrapGridViewDataColumn FieldName="TiempoVuelo" Caption="Tiempo vuelo" VisibleIndex="8" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                            <dx:BootstrapGridViewDataColumn FieldName="Matricula" Caption="Matricula" VisibleIndex="9" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                            <dx:BootstrapGridViewDataColumn FieldName="GrupoModelo" Caption="Grupo modelo" VisibleIndex="10" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                            <dx:BootstrapGridViewDataColumn Caption="Acciones" VisibleIndex="11">
                                                                <DataItemTemplate>
                                                                    <dx:BootstrapButton ID="btnEliminarFerryHijo" runat="server" Text="Eliminar" OnClick="btnEliminarFerryHijo_Click" CommandName="EliminarFerry" CommandArgument='<%# Eval("IdFerry").ToString()%>'></dx:BootstrapButton>
                                                                </DataItemTemplate>
                                                            </dx:BootstrapGridViewDataColumn>
                                                        </Columns>
                                                        <SettingsPager PageSize="5"></SettingsPager>
                                                    </dx:BootstrapGridView>
                                                </DetailRow>
                                            </Templates>
                                            <SettingsDetail ShowDetailRow="true" />
                                            <ClientSideEvents Init="onSelectionGridViewAction" SelectionChanged="onSelectionGridViewAction" EndCallback="onSelectionGridViewAction" />
                                        </dx:BootstrapGridView>

                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-6" style="text-align: right;">
                                    &nbsp;
                                    <dx:BootstrapButton ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click">
                                        <SettingsBootstrap RenderOption="Success" />
                                    </dx:BootstrapButton>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>


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
                                        <%--<asp:UpdatePanel ID="upaOK" runat="server">
                                            <ContentTemplate>--%>
                                        <dx:ASPxButton ID="btOK" runat="server" Text="OK" Theme="PlasticBlue" Width="80px" OnClick="btOK_Click"
                                            AutoPostBack="true" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {ppAlert.Hide(); }" />
                                        </dx:ASPxButton>
                                        <%--</ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btOK" />
                                            </Triggers>
                                        </asp:UpdatePanel>--%>
                                    </td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <%--MODAL PARA AGREGAR PIERNAS VIRTUALES--%>
    <dx:BootstrapPopupControl ID="ppTramos" runat="server" ClientInstanceName="ppTramos" CloseAnimationType="Fade" PopupAnimationType="Fade"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="350px" Height="450px" HeaderText="Alta de vuelos"
        AllowDragging="true" Modal="true" CloseAction="CloseButton" ShowCloseButton="true" AllowResize="true">
        <ContentCollection>
            <dx:ContentControl>

                <div class="row">
                    <div class="col-sm-6">
                        <dx:BootstrapTextBox ID="txtNoTrip" runat="server" Caption="No. TRIP:" TabIndex="0">
                            <ValidationSettings ValidationGroup="VGPierna">
                                <RequiredField IsRequired="true" ErrorText="El trip es requerido" />
                            </ValidationSettings>
                        </dx:BootstrapTextBox>
                    </div>
                    <div class="col-sm-6">
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <%--<dx:BootstrapComboBox ID="ddlOrigenFV" runat="server" NullText="Seleccione una opción" TabIndex="1" 
                                                            Caption="Origen:" Width="300px" ClientInstanceName="ddlOrigenFV" EnableCallbackMode="true"
                                                            OnItemsRequestedByFilterCondition="ddlOrigenFV_ItemsRequestedByFilterCondition"
                                                            OnCustomFiltering="ddlOrigenFV_CustomFiltering" EnableViewState="true" CallbackPageSize="100">
                                                            <ValidationSettings ValidationGroup="VGPierna" RequiredField-IsRequired="true" RequiredField-ErrorText="El aeropuerto origen es requerido"></ValidationSettings>
                                                            <ClientSideEvents SelectedIndexChanged="function(e,s) { OnOrigenChanged();}" />
                                                        </dx:BootstrapComboBox>--%>

                        <dx:BootstrapComboBox ID="ddlOrigenFV" runat="server" Caption="Origen:" NullText="Seleccione una opción" TabIndex="1" AutoPostBack="true"
                            ClientInstanceName="ddlOrigenFV" OnItemsRequestedByFilterCondition="ddlOrigenFV_ItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ddlOrigenFV_ItemRequestedByValue" EnableCallbackMode="true">
                            <%--<ValidationSettings EnableCustomValidation="True" ValidationGroup="VGPierna" SetFocusOnError="True"
                                                                ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                                <RequiredField ErrorText="El campo es requerido." IsRequired="True" />
                                                            </ValidationSettings>--%>
                            <ValidationSettings ValidationGroup="VGPierna" RequiredField-IsRequired="true" RequiredField-ErrorText="El aeropuerto origen es requerido"></ValidationSettings>
                        </dx:BootstrapComboBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <%--<dx:BootstrapComboBox ID="ddlDestinoFV" runat="server" NullText="Seleccione una opción" TabIndex="2"
                                                            Caption="Destino:" Width="300px" ClientInstanceName="ddlDestinoFV" EnableCallbackMode="true"
                                                            OnCustomFiltering="ddlDestinoFV_CustomFiltering" CallbackPageSize="100">
                                                            <ValidationSettings ValidationGroup="VGPierna" RequiredField-IsRequired="true" 
                                                                RequiredField-ErrorText="El aeropuerto destino es requerido"></ValidationSettings>
                                                        </dx:BootstrapComboBox>--%>
                        <dx:BootstrapComboBox ID="ddlDestinoFV" runat="server" Caption="Destino:" NullText="Seleccione una opción" TabIndex="2" AutoPostBack="true"
                            ClientInstanceName="ddlDestinoFV" OnItemsRequestedByFilterCondition="ddlDestinoFV_ItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ddlDestinoFV_ItemRequestedByValue" EnableCallbackMode="true">
                            <%--<ValidationSettings EnableCustomValidation="True" ValidationGroup="VGPierna" SetFocusOnError="True"
                                                                ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                                <RequiredField ErrorText="El campo es requerido." IsRequired="True" />
                                                            </ValidationSettings>--%>
                            <ValidationSettings ValidationGroup="VGPierna" RequiredField-IsRequired="true" RequiredField-ErrorText="El aeropuerto destino es requerido"></ValidationSettings>
                        </dx:BootstrapComboBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">

                        <dx:BootstrapComboBox ID="ddlMatricula" runat="server" NullText="Seleccione una opción" TabIndex="3"
                            OnSelectedIndexChanged="ddlMatricula_SelectedIndexChanged" Caption="Matricula:" ClientInstanceName="ddlMatricula">
                            <ValidationSettings ValidationGroup="VGPierna">
                                <RequiredField IsRequired="true" ErrorText="La matricula es requerida" />
                            </ValidationSettings>
                        </dx:BootstrapComboBox>

                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <dx:BootstrapDateEdit ID="txtFechaSalidaFV" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom"
                            ClientInstanceName="txtFechaSalidaFV" EditFormatString="dd/MM/yyyy HH:mm" AutoPostBack="true"
                            OnDateChanged="txtFechaSalidaFV_DateChanged" Caption="Fecha salida:" TabIndex="4">
                            <ValidationSettings ValidationGroup="VGPierna">
                                <RequiredField IsRequired="true" ErrorText="La fecha de salida es requerida" />
                            </ValidationSettings>
                        </dx:BootstrapDateEdit>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-8">
                        <dx:BootstrapTextBox ID="txtTiempoVuelo" runat="server" ClientInstanceName="txtTiempoVuelo" TabIndex="6"
                            ValidationSettings-ValidationGroup="VGPierna" Caption="Tiempo de vuelo:">
                            <ClientSideEvents Validation="OnValidation" />
                            <MaskSettings Mask="00:00" />
                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="El tiempo de vuelo es requerido"></ValidationSettings>
                        </dx:BootstrapTextBox>
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <dx:ASPxDateEdit ID="txtFechaLlegadaFV" ClientInstanceName="txtFechaLlegadaFV" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom"
                            TimeSectionProperties-Visible="true" EditFormatString="dd/MM/yyyy HH:mm" AutoPostBack="true"
                            Width="160px" OnDateChanged="txtFechaLlegada_DateChanged" Visible="false" TabIndex="5">
                            <DropDownButton>
                                <Image IconID="scheduling_calendar_16x16"></Image>
                            </DropDownButton>
                            <ValidationSettings EnableCustomValidation="True" ValidationGroup="VGPierna" SetFocusOnError="True"
                                ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                <RequiredField ErrorText="El campo es requerido." IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </div>
                </div>

                <br />

                <div class="row">
                    <div style="text-align: center">
                        <dx:BootstrapButton ID="btnAgregarFV" runat="server" Text="Agregar" TabIndex="7" ValidationGroup="VGPierna" AutoPostBack="true"
                            OnClick="btnAgregarFV_Click">
                            <SettingsBootstrap RenderOption="Success" />
                        </dx:BootstrapButton>

                        <dx:BootstrapButton ID="btnCancelarFV" runat="server" Text="Cancelar" TabIndex="8" ValidationGroup="VGPierna" AutoPostBack="true"
                            OnClick="btnCancelarFV_Click">
                            <ClientSideEvents Click="function(s, e) { ppTramos.Hide(); }" />
                            <SettingsBootstrap RenderOption="Warning" />
                        </dx:BootstrapButton>

                        <dx:ASPxHiddenField ID="hfOrigenFV" ClientInstanceName="hfOrigenFV" runat="server"></dx:ASPxHiddenField>
                        <dx:ASPxHiddenField ID="hfIdFerry" ClientInstanceName="hfIdFerry" runat="server"></dx:ASPxHiddenField>
                    </div>
                </div>

            </dx:ContentControl>
        </ContentCollection>
    </dx:BootstrapPopupControl>

    <%-- MODAL PARA AGREGAR LISTAS DE DIFUSION --%>
    <%--<dx:ASPxPopupControl ID="ppListaDifusion" runat="server" ClientInstanceName="ppListaDifusion" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Seleccione una lista" AllowDragging="true" ShowCloseButton="true" Width="240" Height="300">
        <ClientSideEvents />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btnAgregarFV">
                    <PanelCollection>
                        <dx:PanelContent>
                            <asp:UpdatePanel ID="upaListaDifusion" runat="server" OnUnload="UpdatePanel1_Unload">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                
                                                <div class="col-sm-12;">
                                                    <table style="width: 100%; text-align: center">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxListBox Width="100%" ID="chklbxLista" ClientInstanceName="cklbxMail" SelectionMode="CheckColumn"
                                                                    runat="server" ValueField="IdListaDifusion" TextField="Descripcion" Height="300">
                                                                    <Border BorderStyle="None" />
                                                                </dx:ASPxListBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                
                                                            </td>
                                                            <td>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="width: 100%; text-align: center">
                                                    <dx:ASPxButton ID="btnAceptarLista" runat="server" Text="Agregar Listas" Theme="Office2010Black" TabIndex="7"
                                                        AutoPostBack="true" OnClick="btnAceptarLista_Click">
                                                        <ClientSideEvents Click="function(s, e) { ppListaDifusion.Hide(); }" />
                                                    </dx:ASPxButton>
                                                    <dx:ASPxButton ID="btnCancelarLista" runat="server" Text="Cancelar" Theme="Office2010Black" TabIndex="8" AutoPostBack="false">
                                                        <ClientSideEvents Click="function(s, e) { ppListaDifusion.Hide(); }" />
                                                    </dx:ASPxButton>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <dx:ASPxHiddenField ID="hfTipoListaDifusion" ClientInstanceName="hfTipoListaDifusion" runat="server"></dx:ASPxHiddenField>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>--%>
</asp:Content>
