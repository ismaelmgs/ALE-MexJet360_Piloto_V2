<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmPubFerrys.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ALE_MexJet.Views.JetSmart.frmPubFerrys" %>

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

        function OcultaModalConfirmacion()
        {
            ppConfirm.Hide();
        }

        function OcultaModalMensaje()
        {
            ppMensaje.Hide();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="pnlPrincipal" runat="server" Width="100%" BackColor="White" Style="border-radius: 14px;">
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxHiddenField ClientInstanceName="hfNacional" ID="hfNacional" runat="server" />
                <dx:ASPxHiddenField ClientInstanceName="hfExtranjero" ID="hfExtranjero" runat="server" />
                <dx:ASPxHiddenField ClientInstanceName="hfNalExtFor" ID="hfNalExtFor" runat="server" />
                
                <dx:ASPxHiddenField ClientInstanceName="hfNacionalFV" ID="hfNacionalFV" runat="server" />
                <dx:ASPxHiddenField ClientInstanceName="hfExtranjeroFV" ID="hfExtranjeroFV" runat="server" />
                <dx:ASPxHiddenField ClientInstanceName="hfNalExtForFV" ID="hfNalExtForFV" runat="server" />

                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <br />
                <%--<div class="row">
                            <div class="col-md-12">
                                <dx:BootstrapButton ID="btnAddFerry" runat="server" Text="Agregar Ferry" OnClick="btnAddFerry_Click">
                                    <SettingsBootstrap RenderOption="Info" />
                                </dx:BootstrapButton>
                            </div>
                        </div>--%>
                <div class="row">
                    <div class="col-lg-1"></div>
                    <div class="col-lg-10">
                        <fieldset>
                            <header>
                                Datos generales
                            </header>
                            <div class="row">
                                <div class="col-lg-1"></div>
                                <div class="col-lg-3">
                                    <dx:BootstrapTextBox ID="txtTrip" runat="server" Caption="Trip:" TabIndex="1">
                                        <ValidationSettings ValidationGroup="FerryP" RequiredField-IsRequired="true" RequiredField-ErrorText="El trip es requerido"></ValidationSettings>
                                    </dx:BootstrapTextBox>
                                </div>
                                <div class="col-lg-8">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-1"></div>
                                <div class="col-lg-3">
                                    <dx:BootstrapComboBox ID="ddlOrigen" runat="server" Caption="Origen:" NullText="Seleccione una opción" TabIndex="2"
                                        ClientInstanceName="ddlOrigen" OnItemsRequestedByFilterCondition="ddlOrigen_ItemsRequestedByFilterCondition"
                                        OnItemRequestedByValue="ddlOrigen_ItemRequestedByValue" EnableCallbackMode="true">
                                        <ValidationSettings ValidationGroup="FerryP" RequiredField-IsRequired="true" RequiredField-ErrorText="El aeropuerto origen es requerido"></ValidationSettings>
                                    </dx:BootstrapComboBox>
                                </div>
                                <div class="col-lg-3">
                                    <dx:BootstrapComboBox ID="ddlDestino" runat="server" Caption="Destino:" NullText="Seleccione una opción" TabIndex="3"
                                        ClientInstanceName="ddlDestino" OnItemsRequestedByFilterCondition="ddlDestino_ItemsRequestedByFilterCondition"
                                        OnItemRequestedByValue="ddlDestino_ItemRequestedByValue" EnableCallbackMode="true">
                                        <ValidationSettings ValidationGroup="FerryP" RequiredField-IsRequired="true" RequiredField-ErrorText="El aeropuerto destino es requerido"></ValidationSettings>
                                    </dx:BootstrapComboBox>
                                </div>
                                <div class="col-lg-3">
                                    <dx:BootstrapComboBox ID="ddlMatricula" runat="server" NullText="Seleccione una opción" TabIndex="4" Caption="Matricula:" ClientInstanceName="ddlMatricula"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlMatricula_SelectedIndexChanged">
                                        <ValidationSettings ValidationGroup="FerryP">
                                            <RequiredField IsRequired="true" ErrorText="La matricula es requerida" />
                                        </ValidationSettings>
                                    </dx:BootstrapComboBox>
                                </div>
                                <div class="col-lg-2"></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-1"></div>
                                <div class="col-lg-3">
                                    <dx:BootstrapTextBox ID="txtTiempoVuelo" runat="server" ClientInstanceName="txtTiempoVuelo" TabIndex="5" AutoPostBack="true"
                                        OnTextChanged="txtTiempoVuelo_TextChanged" ValidationSettings-ValidationGroup="FerryP" Caption="Tiempo de vuelo:">
                                        <ClientSideEvents Validation="OnValidation" />
                                        <MaskSettings Mask="00:00" />
                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="El tiempo de vuelo es requerido"></ValidationSettings>
                                    </dx:BootstrapTextBox>
                                </div>
                                <div class="col-lg-3">
                                    <dx:BootstrapSpinEdit  ID="txtNoPasajeros" runat="server" Caption="No. pasajeros:" TabIndex="6"></dx:BootstrapSpinEdit>
                                </div>
                                <div class="col-lg-3">
                                    <dx:BootstrapTextBox ID="txtGrupoModelo" runat="server" Enabled="false" Caption="Grupo modelo:" TabIndex="7"></dx:BootstrapTextBox>
                                </div>
                                <div class="col-lg-2"></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-7"></div>
                                <div class="col-lg-3" style="text-align: right; vertical-align: bottom">
                                    <asp:Label ID="lblAddFerryHijo" runat="server" Text="Agregar ferry hijo" Font-Italic="true"></asp:Label>
                                </div>
                                <div class="col-lg-2" style="border: none; vertical-align: top">
                                    <asp:ImageButton ID="imbAddFerryHijo" runat="server" BorderStyle="None" Width="64px" Height="64px" TabIndex="8"
                                        ImageUrl="~/img/iconos/add_blue.png" ToolTip="Clic para agregar un ferry hijo"
                                        onmouseover="this.src='../../img/iconos/add_green.png'" onmouseout="this.src='../../img/iconos/add_blue.png'"
                                        OnClick="imbAddFerryHijo_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-1"></div>
                                <div class="col-lg-10">
                                    <dx:BootstrapGridView ID="gvFerrysHijos" runat="server" KeyFieldName="IdFerry" Caption="Listado de ferrys hijo" TabIndex="17"
                                        OnRowCommand="gvFerrysHijos_RowCommand">
                                        <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                        <Columns>
                                            <dx:BootstrapGridViewDataColumn FieldName="IdIndex" Caption="Index" Visible="false" VisibleIndex="1" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" SortIndex="0" SortOrder="Descending" />
                                            <dx:BootstrapGridViewDataColumn FieldName="Trip" Caption="TRIP" VisibleIndex="2" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                            <dx:BootstrapGridViewDataColumn FieldName="Origen" Caption="Origen" VisibleIndex="3" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                            <dx:BootstrapGridViewDataColumn FieldName="Destino" Caption="Destino" VisibleIndex="4" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                            <dx:BootstrapGridViewDataColumn FieldName="Matricula" Caption="Matricula" VisibleIndex="6" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                            <dx:BootstrapGridViewDataColumn FieldName="TiempoVuelo" Caption="Tiempo vuelo" VisibleIndex="7" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                            <dx:BootstrapGridViewDataColumn FieldName="Costo" Caption="Costo vuelo" VisibleIndex="7" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                            <dx:BootstrapGridViewDataColumn FieldName="Iva" Caption="Iva vuelo" VisibleIndex="8" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                            <dx:BootstrapGridViewDataColumn VisibleIndex="9" HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <dx:BootstrapButton ID="btnEliminarFerry" runat="server" Text="Eliminar" CommandName="EliminarFerryHijo"
                                                        CommandArgument='<%# Eval("IdIndex").ToString()%>'></dx:BootstrapButton>
                                                </DataItemTemplate>
                                            </dx:BootstrapGridViewDataColumn>
                                        </Columns>
                                        <SettingsPager Position="Bottom">
                                            <PageSizeItemSettings Items="10, 20, 50" />
                                        </SettingsPager>
                                    </dx:BootstrapGridView>
                                </div>
                                <div class="col-lg-1"></div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="col-lg-1"></div>
                </div>

                <div class="row">
                    <div class="col-lg-1"></div>
                    <div class="col-lg-10">
                        <fieldset>
                            <header>
                                Vigencia
                            </header>
                            <div class="row">
                                <div class="col-lg-1"></div>
                                <div class="col-lg-3">
                                    <dx:BootstrapDateEdit ID="txtFechaInicio" runat="server" Caption="Inicio:" TabIndex="9"></dx:BootstrapDateEdit>
                                </div>
                                <div class="col-lg-3">
                                    <dx:BootstrapTimeEdit ID="txtHoraInicio" runat="server" Caption="." EditFormat="Custom" EditFormatString="HH:mm" TabIndex="10"></dx:BootstrapTimeEdit>
                                </div>
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-2"></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-1"></div>
                                <div class="col-lg-3">
                                    <dx:BootstrapDateEdit ID="txtFechaFin" runat="server" Caption="Fin:" TabIndex="11"></dx:BootstrapDateEdit>
                                </div>
                                <div class="col-lg-3">
                                    <dx:BootstrapTimeEdit ID="txtHoraFin" runat="server" Caption="." EditFormat="Custom" EditFormatString="HH:mm" TabIndex="12"></dx:BootstrapTimeEdit>
                                </div>
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-2"></div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-1"></div>
                    <div class="col-lg-10">
                        <fieldset>
                            <header>
                                Costo
                            </header>
                            <div class="row">
                                <div class="col-lg-1"></div>
                                <div class="col-lg-3">
                                    <dx:BootstrapTextBox ID="txtCosto" runat="server" ClientInstanceName="txtCosto" CssClasses-Control="recaudado" Caption="Vuelo:" TabIndex="13" AutoPostBack="true" OnTextChanged="txtCosto_TextChanged">
                                        <ClientSideEvents KeyUp="function(s,e)
                                                                            { 
                                                                                var iva;
                                                                                var costo;
                                                                                var suma;
                                                                                if(hfNalExtFor.Get('hfNalExtFor') == 'F' || hfNalExtFor.Get('hfNalExtFor') == 'N')
                                                                                {
                                                                                    iva = hfNacional.Get('hfNacional'); 
                                                                                    iva = iva * txtCosto.GetValue();
                                                                                    txtIvaCosto.SetText(iva);
                                                                                }
                                                                                else
                                                                                { 
                                                                                    iva = hfExtranjero.Get('hfExtranjero'); 
                                                                                    iva = iva * txtCosto.GetValue();
                                                                                    txtIvaCosto.SetText(iva);
                                                                                }

                                                                                costo = txtCosto.GetValue();
                                                                                suma = parseFloat(costo) + parseFloat(iva);
                                                                                txtTotalCosto.SetText(suma);

                                                                            }" />
                                        <ValidationSettings ErrorDisplayMode="Text" SetFocusOnError="True" ValidationGroup="FerryP">
                                            <RequiredField ErrorText="El campo es requerido" IsRequired="true" />
                                            <RegularExpression ErrorText="Error en la informacion ingresada costo." ValidationExpression="^[0-9]*(\.[0-9]+)?$" />
                                        </ValidationSettings>
                                    </dx:BootstrapTextBox>
                                </div>
                                <div class="col-lg-2">
                                    <dx:BootstrapTextBox ID="txtIvaCosto" runat="server" ClientInstanceName="txtIvaCosto" CssClasses-Control="recaudado" ReadOnly="true" Caption="Iva:" TabIndex="14">
                                        <ValidationSettings ValidationGroup="FerryP" RequiredField-IsRequired="true" RequiredField-ErrorText="El iva es requerido"></ValidationSettings>
                                    </dx:BootstrapTextBox>
                                </div>
                                <div class="col-lg-2">
                                    <dx:BootstrapTextBox ID="txtTotalCosto" runat="server" ClientInstanceName="txtTotalCosto" Caption="Total:" TabIndex="15" ReadOnly="true"></dx:BootstrapTextBox>
                                </div>
                                <div class="col-lg-4"></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-1"></div>
                                <div class="col-lg-3"></div>
                                <div class="col-lg-3">
                                    <div style="margin-top: 10px">
                                        <dx:BootstrapGridView ID="gvMembresias" runat="server" Caption="Listado de membresias:" TabIndex="18">
                                            <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                            <Columns>
                                                <dx:BootstrapGridViewDataColumn FieldName="Nombre" Caption="Nombre" VisibleIndex="1" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" SortIndex="0" SortOrder="Ascending" />
                                                <dx:BootstrapGridViewDataColumn FieldName="Descuento" Caption="Descuento" VisibleIndex="2" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                <dx:BootstrapGridViewDataColumn FieldName="Precio" Caption="Precio" VisibleIndex="3" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                            </Columns>
                                            <SettingsPager Position="Bottom">
                                                <PageSizeItemSettings Items="10, 20, 50" />
                                            </SettingsPager>
                                        </dx:BootstrapGridView>
                                    </div>
                                </div>
                                <div class="col-lg-3"></div>
                                <div class="col-lg-2"></div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-1"></div>
                    <div class="col-lg-5" style="text-align:right">
                        <dx:BootstrapButton ID="btnLimpiar" runat="server" Text="Limpiar / Cancelar" OnClick="btnLimpiar_Click" TabIndex="16">
                            <SettingsBootstrap RenderOption="Warning" />
                        </dx:BootstrapButton>
                    </div>
                    <div class="col-lg-5" style="text-align:left">
                        <dx:BootstrapButton ID="btnGuardarFerryPadre" runat="server" Text="Guardar" ValidationGroup="FerryP" OnClick="btnGuardarFerryPadre_Click" TabIndex="15">
                            <SettingsBootstrap RenderOption="Success" />
                        </dx:BootstrapButton>
                    </div>
                    <div class="col-lg-1"></div>
                </div>
                <div class="row">
                    <div class="col-lg-12">

                        <%--<asp:Button ID="Button1" runat="server" Text="NO" CssClass="btn btn-default" />
                        <asp:Button ID="Button2" runat="server" Text="NO" CssClass="btn btn-default" />
                        <asp:Button ID="Button3" runat="server" Text="NO" CssClass="btn btn-warning" />
                        <asp:Button ID="Button4" runat="server" Text="NO" CssClass="btn btn-success" />
                        <asp:Button ID="Button5" runat="server" Text="NO" CssClass="btn btn-success" />--%>

                        <%--<dx:ASPxButton ID="ASPxButton1" runat="server" Text="SI" Theme="MetropolisBlue"/>
                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="SI" Theme="Aqua"/>
                        <dx:ASPxButton ID="ASPxButton3" runat="server" Text="SI" Theme="BlackGlass"/>
                        <dx:ASPxButton ID="ASPxButton4" runat="server" Text="SI" Theme="iOS"/>
                        <dx:ASPxButton ID="ASPxButton5" runat="server" Text="SI" Theme="Metropolis"/>
                        <dx:ASPxButton ID="ASPxButton6" runat="server" Text="SI" Theme="Moderno"/>
                        <dx:ASPxButton ID="ASPxButton7" runat="server" Text="SI" Theme="Mulberry"/>
                        <dx:ASPxButton ID="ASPxButton8" runat="server" Text="SI" Theme="Office2010Silver"/>
                        <dx:ASPxButton ID="ASPxButton9" runat="server" Text="SI" Theme="Office365"/>
                        <dx:ASPxButton ID="ASPxButton10" runat="server" Text="SI" Theme="SoftOrange"/>--%>
                    </div>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

    <%--MODAL PARA AGREGAR FERRYS HIJOS AL FERRY PRINCIPAL--%>
    <dx:BootstrapPopupControl ID="ppTramos" runat="server" ClientInstanceName="ppTramos" PopupElementCssSelector="#info-popup-control"
        CloseAnimationType="Fade" PopupAnimationType="Fade"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="350px" Height="450px" HeaderText="Alta de vuelos"
        AllowDragging="true" Modal="true" CloseAction="CloseButton" ShowCloseButton="true" AllowResize="true">
        <ContentCollection>
            <dx:ContentControl>

                <div class="row">
                    <div class="col-sm-6">
                        <dx:BootstrapTextBox ID="txtNoTripFV" runat="server" Caption="No. TRIP:" TabIndex="0" Enabled="false">
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
                        <dx:BootstrapComboBox ID="ddlOrigenFV" runat="server" Caption="Origen:" NullText="Seleccione una opción" TabIndex="1" AutoPostBack="true"
                            ClientInstanceName="ddlOrigenFV" OnItemsRequestedByFilterCondition="ddlOrigenFV_ItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ddlOrigenFV_ItemRequestedByValue" EnableCallbackMode="true">
                            <ValidationSettings ValidationGroup="VGPierna" RequiredField-IsRequired="true" RequiredField-ErrorText="El aeropuerto origen es requerido"></ValidationSettings>
                        </dx:BootstrapComboBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <%--<asp:Timer ID="tmDestino" runat="server" Interval="500" OnTick="tmDestino_Tick" Enabled="false"></asp:Timer>--%>
                        <dx:BootstrapComboBox ID="ddlDestinoFV" runat="server" Caption="Destino:" NullText="Seleccione una opción" TabIndex="2" AutoPostBack="true"
                            ClientInstanceName="ddlDestinoFV" OnItemsRequestedByFilterCondition="ddlDestinoFV_ItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ddlDestinoFV_ItemRequestedByValue" OnValueChanged="ddlDestinoFV_ValueChanged" EnableCallbackMode="true">
                            <ValidationSettings ValidationGroup="VGPierna" RequiredField-IsRequired="true" RequiredField-ErrorText="El aeropuerto destino es requerido"></ValidationSettings>
                        </dx:BootstrapComboBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">

                        <dx:BootstrapComboBox ID="ddlMatriculaFV" runat="server" Caption="Matricula:" Enabled="false" NullText="Seleccione una opción" TabIndex="3" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlMatriculaFV_SelectedIndexChanged" ClientInstanceName="ddlMatricula">
                            <ValidationSettings ValidationGroup="VGPierna" RequiredField-IsRequired="true" RequiredField-ErrorText="La matricula es requerida"></ValidationSettings>
                        </dx:BootstrapComboBox>

                    </div>
                </div>
                <%--<div class="row">
                    <div class="col-md-12">
                        <dx:BootstrapDateEdit ID="txtFechaSalidaFV" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom"
                            ClientInstanceName="txtFechaSalidaFV" EditFormatString="dd/MM/yyyy HH:mm" AutoPostBack="true"
                            OnDateChanged="txtFechaSalidaFV_DateChanged" Caption="Fecha salida:" TabIndex="4">
                            <ValidationSettings ValidationGroup="VGPierna">
                                <RequiredField IsRequired="true" ErrorText="La fecha de salida es requerida" />
                            </ValidationSettings>
                        </dx:BootstrapDateEdit>
                    </div>
                </div>--%>
                <div class="row">
                    <div class="col-md-8">
                        <dx:BootstrapTextBox ID="txtTiempoVueloFV" runat="server" ClientInstanceName="txtTiempoVuelo" TabIndex="4" AutoPostBack="true"
                            OnTextChanged="txtTiempoVueloFV_TextChanged" ValidationSettings-ValidationGroup="VGPierna" Caption="Tiempo de vuelo:">
                            <MaskSettings Mask="00:00" />
                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="El tiempo de vuelo es requerido"></ValidationSettings>
                        </dx:BootstrapTextBox>
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
                <div class="row">
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <dx:BootstrapTextBox ID="txtCostoFV" runat="server" Caption="Costo:" ClientInstanceName="txtCostoFV" TabIndex="6">
                            <ClientSideEvents KeyUp="function(s,e)
                                                                { 
                                                                    var iva;
                                                                    var costo;
                                                                    var suma;
                                                                    if(hfNalExtForFV.Get('hfNalExtForFV') == 'F' || hfNalExtForFV.Get('hfNalExtForFV') == 'N')
                                                                    {
                                                                        iva = hfNacional.Get('hfNacional'); 
                                                                        iva = iva * txtCostoFV.GetValue();
                                                                        txtIvaCostoFV.SetText(iva);
                                                                    }
                                                                    else
                                                                    { 
                                                                        iva = hfExtranjero.Get('hfExtranjero'); 
                                                                        iva = iva * txtCostoFV.GetValue();
                                                                        txtIvaCostoFV.SetText(iva);
                                                                    }

                                                                    costo = txtCostoFV.GetValue();
                                                                    suma = parseFloat(costo) + parseFloat(iva);
                                                                    txtTotalCostoFV.SetText(suma);
                                                                }" />
                            <ValidationSettings ErrorDisplayMode="Text" SetFocusOnError="True">
                                <RequiredField ErrorText="El campo es requerido" IsRequired="true" />
                                <RegularExpression ErrorText="Error en la informacion ingresada costo." ValidationExpression="^[0-9]*(\.[0-9]+)?$" />
                            </ValidationSettings>
                        </dx:BootstrapTextBox>
                    </div>
                    <div class="col-md-6">
                        <dx:BootstrapTextBox ID="txtIvaCostoFV" runat="server" ClientInstanceName="txtIvaCostoFV" Caption="Iva:" ReadOnly="true" TabIndex="7"></dx:BootstrapTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <dx:BootstrapTextBox ID="txtTotalCostoFV" ClientInstanceName="txtTotalCostoFV" runat="server" Caption="Total costo:" ReadOnly="true" TabIndex="8"></dx:BootstrapTextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div style="text-align: center">
                        <dx:BootstrapButton ID="btnAgregarFV" runat="server" Text="Agregar" TabIndex="9" ValidationGroup="VGPierna" AutoPostBack="true"
                            OnClick="btnAgregarFV_Click">
                            <SettingsBootstrap RenderOption="Success" />
                        </dx:BootstrapButton>

                        <dx:BootstrapButton ID="btnCancelarFV" runat="server" Text="Cancelar" TabIndex="10" ValidationGroup="VGPierna" AutoPostBack="true"
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

    <%-- MENSAJE DE CONFIRMACION PARA PUBLICACION --%>
    <dx:BootstrapPopupControl ID="ppConfirm" runat="server" ClientInstanceName="ppConfirm" CloseAnimationType="Fade" PopupAnimationType="Fade"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="350px" Height="150px" HeaderText="Confirmación"
        AllowDragging="true" Modal="true" CloseAction="CloseButton" ShowFooter="true" ShowCloseButton="true" AllowResize="false">
        <ContentCollection>
            <dx:ContentControl>
                <div class="row">
                    <div class="col-lg-12">
                        <dx:ASPxLabel ID="lblTextoConfirmacion" runat="server" Theme="Office2010Black"></dx:ASPxLabel>
                    </div>
                </div>
            </dx:ContentControl>
        </ContentCollection>
        <FooterTemplate>
            <div class="row">
                <div class="col-lg-6" style="text-align:right">
                    <asp:Button ID="btnNoConfirm" runat="server" Text="NO" CssClass="btn btn-warning" OnClientClick="OcultaModalConfirmacion();" OnClick="btnNoConfirmacion_Click"/>
                </div>
                <div class="col-lg-6" style="text-align:left">
                    <asp:Button ID="btnSiConfirm" runat="server" Text="SI" CssClass="btn btn-success" OnClientClick="OcultaModalConfirmacion();" OnClick="btnSiConfirmacion_Click"/>
                </div>
            </div>
        </FooterTemplate>
    </dx:BootstrapPopupControl>

    <%-- MENSAJE --%>
    <dx:BootstrapPopupControl ID="ppMensaje" runat="server" ClientInstanceName="ppMensaje" CloseAnimationType="Fade" PopupAnimationType="Fade"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="350px" Height="200px" HeaderText="Confirmación"
        AllowDragging="true" Modal="true" CloseAction="CloseButton" ShowCloseButton="true" AllowResize="false" ShowHeader="true" ShowFooter="true">
        <ContentCollection>
            <dx:ContentControl>
                <div class="row">
                    <div class="col-sm-12">
                        <dx:ASPxLabel ID="lblMensaje" runat="server" Theme="Office2010Black"></dx:ASPxLabel>
                    </div>
                </div>
            </dx:ContentControl>
        </ContentCollection>
        <FooterTemplate>
            <asp:Button ID="btnCerrarMensaje" runat="server" Text="Cerrar" CssClass="btn" OnClientClick="OcultaModalMensaje();"></asp:Button>
        </FooterTemplate>
    </dx:BootstrapPopupControl>
</asp:Content>


