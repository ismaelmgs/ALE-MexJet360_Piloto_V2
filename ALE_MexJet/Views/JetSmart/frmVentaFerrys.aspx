<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmVentaFerrys.aspx.cs" UICulture="es" Culture="es-MX" EnableEventValidation="false" Inherits="ALE_MexJet.Views.JetSmart.frmVentaFerrys" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Styles/Controls.css" />
    <script type="text/javascript">
        function suma(s, e) {
            alert(s);

            hfNacional.properties
            //var IVA;
            //var Subtotal;
            //if (txtIVA.GetValue() == null)
            //    IVA = 0;
            //else
            //    IVA = parseFloat(txtIVA.GetValue());

            //if (txtSubtotal.GetValue() == null)
            //    Subtotal = 0;
            //else
            //    Subtotal = parseFloat(txtSubtotal.GetValue());

            //var Total = IVA + Subtotal;
            //txtTotal.SetText(Total);
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
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Consulta de ferrys</span>
                    </div>
                </div>

                <dx:ASPxHiddenField ClientInstanceName="hfNacionalFV" ID="hfNacionalFV" runat="server" />
                <dx:ASPxHiddenField ClientInstanceName="hfExtranjeroFV" ID="hfExtranjeroFV" runat="server" />
                <dx:aspxhiddenfield clientinstancename="hfNalExtForFV" id="hfNalExtForFV" runat="server" />

                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <div class="well-g">
                    <br />
                    <div class="row">
                        <div class="col-md-6" style="text-align: left;">
                        </div>
                        <div class="col-md-6" style="text-align: right;">
                            <dx:ASPxLabel runat="server" Theme="Aqua" Text="Exportar a:"></dx:ASPxLabel>
                            &nbsp;<dx:BootstrapButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click">
                            </dx:BootstrapButton>

                            <%--<dx:BootstrapButton ID="BootstrapButton1" runat="server" Text="Danger">
                                        <SettingsBootstrap RenderOption="Danger" />
                                    </dx:BootstrapButton>
                                    <dx:BootstrapButton ID="BootstrapButton2" runat="server" Text="Dark">
                                        <SettingsBootstrap RenderOption="Dark" />
                                    </dx:BootstrapButton>
                                    <dx:BootstrapButton ID="BootstrapButton3" runat="server" Text="Default">
                                        <SettingsBootstrap RenderOption="Default" />
                                    </dx:BootstrapButton>
                                    <dx:BootstrapButton ID="BootstrapButton4" runat="server" Text="Info">
                                        <SettingsBootstrap RenderOption="Info" />
                                    </dx:BootstrapButton>
                                    <dx:BootstrapButton ID="BootstrapButton5" runat="server" Text="Light">
                                        <SettingsBootstrap RenderOption="Light" />
                                    </dx:BootstrapButton>
                                    <dx:BootstrapButton ID="BootstrapButton6" runat="server" Text="Primary">
                                        <SettingsBootstrap RenderOption="Primary" />
                                    </dx:BootstrapButton>
                                    <dx:BootstrapButton ID="BootstrapButton7" runat="server" Text="Secondary">
                                        <SettingsBootstrap RenderOption="Secondary" />
                                    </dx:BootstrapButton>
                                    <dx:BootstrapButton ID="BootstrapButton8" runat="server" Text="Success">
                                        <SettingsBootstrap RenderOption="Success" />
                                    </dx:BootstrapButton>
                                    <dx:BootstrapButton ID="BootstrapButton9" runat="server" Text="Warning">
                                        <SettingsBootstrap RenderOption="Warning" />
                                    </dx:BootstrapButton>--%>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12">
                            <dx:BootstrapGridView ID="gvFerrysVenta" runat="server" KeyFieldName="IdFerry"
                                OnRowCommand="gvFerrysVenta_RowCommand" OnPageIndexChanged="gvFerrysVenta_PageIndexChanged">
                                <SettingsSearchPanel Visible="true" ShowApplyButton="true" />
                                <Settings ShowGroupPanel="True" ShowFilterRowMenu="true" />
                                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                <Columns>
                                    <dx:BootstrapGridViewDataColumn FieldName="NoHijos" Caption="Hijos" VisibleIndex="0" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" Width="25" />
                                    <dx:BootstrapGridViewDataColumn FieldName="IdFerry" Caption="Id ferry" VisibleIndex="1" Visible="false" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" SortIndex="0" SortOrder="Descending" />
                                    <dx:BootstrapGridViewDataColumn FieldName="Trip" Caption="TRIP" VisibleIndex="2" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="False" />
                                    <dx:BootstrapGridViewDataColumn FieldName="Origen" Caption="Origen" VisibleIndex="3" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="False" />
                                    <dx:BootstrapGridViewDataColumn FieldName="Destino" Caption="Destino" VisibleIndex="4" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="Matricula" Caption="Matricula" VisibleIndex="5" Visible="false" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="TiempoVuelo" Caption="Tiempo vuelo" VisibleIndex="6" Visible="false" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="GrupoModelo" Caption="Grupo modelo" VisibleIndex="7" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="NoPax" Caption="No. Pax" VisibleIndex="8" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="FechaSalida" Caption="Fecha inicio" VisibleIndex="8" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" Settings-AllowCellMerge="False" />
                                    <dx:BootstrapGridViewDataColumn FieldName="FechaLlegada" Caption="Fecha fin" VisibleIndex="9" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="Status" Caption="Estatus" Visible="false" VisibleIndex="9" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewTextColumn FieldName="CostoVuelo" Caption="Precio" VisibleIndex="9">
                                        <PropertiesTextEdit DisplayFormatString="c" />
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewDataColumn FieldName="Estatus" Caption="Estatus" VisibleIndex="10" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn VisibleIndex="11" HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <dx:BootstrapImage ID="imbSemaforo" runat="server" Width="20px" Height="20px"
                                                ImageAlign="AbsMiddle" ImageUrl='<%# "~/img/iconos/" + Eval("EstatusImg") %>'>
                                            </dx:BootstrapImage>
                                        </DataItemTemplate>
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn VisibleIndex="12" HorizontalAlign="Center" Width="450px">
                                        <DataItemTemplate>
                                            <dx:BootstrapButton ID="btnPublicarFerry" runat="server" Text="Publicar/Actualizar" CommandName="PublicarFerry" CommandArgument='<%# Eval("IdFerry").ToString()%>'
                                                ToolTip="Clic aqui para publicar o actualizar un ferry en el app.">
                                                <SettingsBootstrap RenderOption="Primary" />
                                            </dx:BootstrapButton>
                                            <dx:BootstrapButton ID="btnAgregarFerryHijo" runat="server" Text="Agregar" CommandArgument='<%# Eval("IdFerry").ToString()%>' CommandName="AgregarFerry"
                                                ToolTip="Clic aqui para agregar un ferry hijo al ferry principal">
                                                <SettingsBootstrap RenderOption="Info" />
                                            </dx:BootstrapButton>
                                            <dx:BootstrapButton ID="btnEditFerry" runat="server" Text="Editar" CommandName="Edit" CommandArgument='<%# Eval("IdFerry").ToString()%>'
                                                ToolTip="Clic aqui para editar un ferry.">
                                                <SettingsBootstrap RenderOption="Success" />
                                            </dx:BootstrapButton>
                                            <dx:BootstrapButton ID="btnEliminarFerry" runat="server" Text="Eliminar" CommandName="EliminarFerry" CommandArgument='<%# Eval("IdFerry").ToString()%>'
                                                ToolTip="Clic aqui para eliminar/cancelar un ferry en mexjet y el app.">
                                                <SettingsBootstrap RenderOption="Warning" />
                                            </dx:BootstrapButton>
                                            <dx:BootstrapButton ID="btnInfoFerry" runat="server" Text="Info." CommandName="Info" CommandArgument='<%# Eval("IdFerry").ToString()%>'
                                                ToolTip="Clic aqui para ver información especial del ferry.">
                                                <SettingsBootstrap RenderOption="Secondary" />
                                            </dx:BootstrapButton>
                                        </DataItemTemplate>
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn FieldName="FechaCancelacion" Caption="Fecha cancelacion" Visible="false" VisibleIndex="13" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="UsuarioCancelacion" Caption="usuario cancelacion" Visible="false" VisibleIndex="14" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="UsuarioCreacion" Caption="Usuario Creacion" Visible="false" VisibleIndex="15" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="FechaCreacion" Caption="Fecha Creacion" Visible="false" VisibleIndex="16" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="UsuarioPay" Caption="Usuario pago" Visible="false" VisibleIndex="17" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="FechaCompra" Caption="Fecha compra" Visible="false" VisibleIndex="18" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="ImporteFinal" Caption="Importe final" Visible="false" VisibleIndex="19" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="MetodoPago" Caption="Metodo pago" Visible="false" VisibleIndex="20" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="TipoCambio" Caption="Tipo cambio" Visible="false" VisibleIndex="21" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                </Columns>
                                <Templates>
                                    <DetailRow>
                                        <dx:BootstrapGridView ID="gvFerrysDetalle" runat="server" KeyFieldName="IdFerry"
                                            OnRowCommand="gvFerrysDetalle_RowCommand"
                                            OnBeforePerformDataSelect="gvFerrysDetalle_BeforePerformDataSelect">
                                            <Columns>
                                                <dx:BootstrapGridViewDataColumn FieldName="IdFerry" Caption="Id ferry" VisibleIndex="1" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                                <dx:BootstrapGridViewDataColumn FieldName="Trip" Caption="TRIP" VisibleIndex="2" SortIndex="0" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                <dx:BootstrapGridViewDataColumn FieldName="Origen" Caption="Origen" VisibleIndex="3" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                <dx:BootstrapGridViewDataColumn FieldName="Destino" Caption="Destino" VisibleIndex="4" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                <dx:BootstrapGridViewDataColumn FieldName="Matricula" Caption="Matricula" VisibleIndex="5" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                <dx:BootstrapGridViewDataColumn FieldName="TiempoVuelo" Caption="Tiempo vuelo" VisibleIndex="6" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                <dx:BootstrapGridViewDataColumn FieldName="GrupoModelo" Caption="Grupo modelo" VisibleIndex="7" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                <dx:BootstrapGridViewDataColumn FieldName="Estatus" Caption="Estatus" VisibleIndex="8" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                <dx:BootstrapGridViewDataColumn Caption="Acciones" VisibleIndex="10">
                                                    <DataItemTemplate>
                                                        <dx:BootstrapButton ID="btnEliminarFerryHijo" runat="server" Text="Eliminar" CommandName="EliminarFerry" CommandArgument='<%# Eval("IdFerry").ToString()%>'></dx:BootstrapButton>
                                                    </DataItemTemplate>
                                                </dx:BootstrapGridViewDataColumn>
                                            </Columns>
                                            <SettingsPager PageSize="5"></SettingsPager>
                                        </dx:BootstrapGridView>
                                    </DetailRow>
                                </Templates>
                                <SettingsDetail ShowDetailRow="true" />
                                <ClientSideEvents Init="onSelectionGridViewAction" SelectionChanged="onSelectionGridViewAction" EndCallback="onSelectionGridViewAction" />
                                <SettingsPager NumericButtonCount="4">
                                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
                                </SettingsPager>
                            </dx:BootstrapGridView>
                        </div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-6" style="text-align: right;">
                        </div>
                    </div>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

    <%--MODAL PARA AGREGAR PRECIO A LOS FERRYS--%>
    <%--<dx:BootstrapPopupControl ID="ppVenta" runat="server" ClientInstanceName="ppVenta" CloseAnimationType="Fade" PopupAnimationType="Fade"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="900px" Height="250px" HeaderText="Edición de Ferrys"
        AllowDragging="true" Modal="true" CloseAction="CloseButton" ShowCloseButton="true" AllowResize="true">
        <ContentCollection>
            <dx:ContentControl>
                <div class="row">
                    <div class="col-md-12">
                        <fieldset>
                            <div class="row" style="width: 100%;">
                                <div class="col-sm-1">
                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="TRIP:" Font-Size="Smaller" Font-Bold="true"></dx:ASPxLabel>
                                </div>
                                <div class="col-sm-2">
                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblTRIP"></dx:ASPxLabel>
                                </div>
                                <div class="col-sm-2">
                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Matricula:" Font-Size="Smaller" Font-Bold="true"></dx:ASPxLabel>
                                </div>
                                <div class="col-sm-2">
                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblMatricula"></dx:ASPxLabel>
                                </div>
                                <div class="col-sm-2" style="text-align: right">
                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Fecha inicio:" Font-Size="Smaller" Font-Bold="true"></dx:ASPxLabel>
                                </div>
                                <div class="col-sm-3">
                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblFechaSalida"></dx:ASPxLabel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-1">
                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Origen:" Font-Size="Smaller" Font-Bold="true"></dx:ASPxLabel>
                                </div>
                                <div class="col-sm-2">
                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblOrigen"></dx:ASPxLabel>
                                </div>
                                <div class="col-sm-2">
                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Destino:" Font-Size="Smaller" Font-Bold="true"></dx:ASPxLabel>
                                </div>
                                <div class="col-sm-2">
                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblDestino"></dx:ASPxLabel>
                                </div>
                                <div class="col-sm-2" style="text-align: right">
                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Fecha fin:" Font-Size="Smaller" Font-Bold="true"></dx:ASPxLabel>
                                </div>
                                <div class="col-sm-3">
                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblFechafin"></dx:ASPxLabel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-4">
                                    <dx:ASPxLabel ID="lblErrorFechaFin" runat="server" Theme="Mulberry" Visible="false"></dx:ASPxLabel>
                                </div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-3" style="text-align: right">
                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Tiempo vuelo: " Font-Size="Smaller" Font-Bold="true"></dx:ASPxLabel>
                                </div>
                                <div class="col-sm-3">
                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblTiempoVlo"></dx:ASPxLabel>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <br />
                <div class="col-md-12">
                    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                <br />
                <br />
                <div class="row" style="width: 100%">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2" style="text-align: left">
                        <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Costo vuelo: "></dx:ASPxLabel>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <dx:BootstrapTextBox ID="txtCostoVuelo" ClientInstanceName="txtCostoVuelo" runat="server">
                            <ClientSideEvents KeyUp="function(s,e)
                                                                { 
                                                                    var iva;
                                                                    if(hfNalExtFor.Get('hfNalExtFor') == 'F' || hfNalExtFor.Get('hfNalExtFor') == 'N')
                                                                    {
                                                                        iva = hfNacional.Get('hfNacional'); 
                                                                        iva = iva * txtCostoVuelo.GetValue();
                                                                        txtCostoIVA.SetText(iva);
                                                                    }
                                                                    else
                                                                    { 
                                                                        iva = hfExtranjero.Get('hfExtranjero'); 
                                                                        iva = iva * txtCostoVuelo.GetValue();
                                                                        txtCostoIVA.SetText(iva);
                                                                    }
                                                                }" />
                            <ValidationSettings ErrorDisplayMode="Text" SetFocusOnError="True" ValidationGroup="Renta">
                                <RequiredField ErrorText="El campo es requerido" IsRequired="true" />
                                <RegularExpression ErrorText="Error en la informacion ingresada costo." ValidationExpression="^[0-9]*(\.[0-9]+)?$" />
                            </ValidationSettings>
                        </dx:BootstrapTextBox>
                    </div>
                    <div class="col-sm-2" style="text-align: left">
                        <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Costo IVA: "></dx:ASPxLabel>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <dx:BootstrapTextBox ID="txtCostoIVA" ClientInstanceName="txtCostoIVA" runat="server"></dx:BootstrapTextBox>
                    </div>
                    <div class="col-sm-1"></div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-2" style="text-align: left">
                        <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Fecha fin: "></dx:ASPxLabel>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <dx:BootstrapDateEdit ID="txtFechaReserva" runat="server" Date="1.1.2017 8:00" EditFormat="DateTime" UseMaskBehavior="true"
                            OnDateChanged="txtFechaReserva_DateChanged" TimeSectionProperties-Visible="true" AutoPostBack="true">
                            <TimeSectionProperties Visible="true" />
                            <ValidationSettings ErrorDisplayMode="Text" ErrorText="La fecha fin no puede ser menor que la fecha inicio" SetFocusOnError="true"
                                ValidationGroup="Renta">
                                <RequiredField ErrorText="El campo es requerido" IsRequired="true" />
                            </ValidationSettings>
                        </dx:BootstrapDateEdit>
                    </div>
                    <div class="col-sm-2" style="text-align: left">
                        <dx:ASPxLabel ID="lblPrioridad" runat="server" Text="Prioridad:" Theme="Office2010Black"></dx:ASPxLabel>
                    </div>
                    <div class="col-sm-3" style="text-align: left">
                        <dx:BootstrapComboBox ID="ddlPrioridad" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlPrioridad_SelectedIndexChanged">
                            <Items>
                                <dx:BootstrapListEditItem Text="Baja" Value="0" Selected="true" />
                                <dx:BootstrapListEditItem Text="Alta" Value="1" />
                            </Items>
                        </dx:BootstrapComboBox>
                    </div>
                    <div class="col-sm-1"></div>
                </div>
                <br />
                <div class="row" style="width: 100%">
                    <div class="col-sm-6" style="text-align: right">
                        <dx:BootstrapButton ID="btnGuardaDetalle" runat="server" Text="Guardar" OnClick="btnGuardaDetalle_Click"
                            ValidationGroup="Renta" ValidateRequestMode="Enabled">
                            <SettingsBootstrap RenderOption="Success" />
                        </dx:BootstrapButton>
                    </div>
                    <div class="col-sm-6" style="text-align: left">
                        <dx:BootstrapButton ID="btnSalir" runat="server" Text="Salir">
                            <SettingsBootstrap RenderOption="Warning" />
                            <ClientSideEvents Click="function(s,e){ppVenta.Hide();}" />
                        </dx:BootstrapButton>
                    </div>
                </div>
            </dx:ContentControl>
        </ContentCollection>
    </dx:BootstrapPopupControl>--%>

    <%--MODAL PARA AGREGAR FERRYS HIJOS AL FERRY PRINCIPAL--%>
    <dx:BootstrapPopupControl ID="ppTramos" runat="server" ClientInstanceName="ppTramos" PopupElementCssSelector="#info-popup-control"
        CloseAnimationType="Fade" PopupAnimationType="Fade"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="350px" Height="450px" HeaderText="Alta de vuelos"
        AllowDragging="true" Modal="true" CloseAction="CloseButton" ShowCloseButton="true" AllowResize="true">
        <ContentCollection>
            <dx:ContentControl>
                <%--<asp:UpdatePanel ID="paTramosFV" runat="server">
                    <ContentTemplate>--%>
                        <div class="row">
                            <div class="col-sm-6">
                                <dx:BootstrapTextBox ID="txtNoTripFV" runat="server" Caption="No. TRIP:" TabIndex="0" ReadOnly="true">
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
                                <dx:BootstrapComboBox ID="ddlDestinoFV" runat="server" Caption="Destino:" NullText="Seleccione una opción" TabIndex="2" AutoPostBack="true"
                                    ClientInstanceName="ddlDestinoFV" OnItemsRequestedByFilterCondition="ddlDestinoFV_ItemsRequestedByFilterCondition"
                                    OnItemRequestedByValue="ddlDestinoFV_ItemRequestedByValue" EnableCallbackMode="true"
                                    OnValueChanged="ddlDestinoFV_ValueChanged">
                                    <ValidationSettings ValidationGroup="VGPierna" RequiredField-IsRequired="true" RequiredField-ErrorText="El aeropuerto destino es requerido"></ValidationSettings>
                                </dx:BootstrapComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <dx:BootstrapComboBox ID="ddlMatriculaFV" runat="server" Caption="Matricula:" NullText="Seleccione una opción" TabIndex="3" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlMatriculaFV_SelectedIndexChanged" ClientInstanceName="ddlMatricula" ReadOnly="true">
                                    <ValidationSettings ValidationGroup="VGPierna" RequiredField-IsRequired="true" RequiredField-ErrorText="La matricula es requerida"></ValidationSettings>
                                </dx:BootstrapComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <dx:BootstrapTextBox ID="txtTiempoVueloFV" runat="server" ClientInstanceName="txtTiempoVuelo" TabIndex="4" AutoPostBack="true"
                                    OnValueChanged="txtTiempoVueloFV_ValueChanged" ValidationSettings-ValidationGroup="VGPierna" Caption="Tiempo de vuelo:">
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
                                <dx:BootstrapTextBox ID="txtCostoFV" runat="server" ClientInstanceName="txtCostoFV" Caption="Costo:" TabIndex="6">
                                    <ClientSideEvents KeyUp="function(s,e)
                                                                { 
                                                                    var iva;
                                                                    var costo;
                                                                    var suma;
                                                                    if(hfNalExtForFV.Get('hfNalExtForFV') == 'F' || hfNalExtForFV.Get('hfNalExtForFV') == 'N')
                                                                    {
                                                                        iva = hfNacionalFV.Get('hfNacionalFV'); 
                                                                        iva = iva * txtCostoFV.GetValue();
                                                                        txtIvaCostoFV.SetText(iva);
                                                                    }
                                                                    else
                                                                    { 
                                                                        iva = hfExtranjeroFV.Get('hfExtranjeroFV'); 
                                                                        iva = iva * txtCostoFV.GetValue();
                                                                        txtIvaCostoFV.SetText(iva);
                                                                    }

                                                                    costo = txtCostoFV.GetValue();
                                                                    suma = parseFloat(costo) + parseFloat(iva);
                                                                    txtTotalCostoFV.SetText(suma);
                                                                }" />
                                    <ValidationSettings ErrorDisplayMode="Text" SetFocusOnError="True" ValidationGroup="Renta">
                                        <RequiredField ErrorText="El campo es requerido" IsRequired="true" />
                                        <RegularExpression ErrorText="Error en la informacion ingresada costo." ValidationExpression="^[0-9]*(\.[0-9]+)?$" />
                                    </ValidationSettings>
                                </dx:BootstrapTextBox>
                            </div>
                            <div class="col-md-6">
                                <dx:BootstrapTextBox ID="txtIvaCostoFV" runat="server" ClientInstanceName="txtIvaCostoFV" Caption="Iva:" TabIndex="7"></dx:BootstrapTextBox>
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
                                <dx:BootstrapButton ID="btnAgregarFV" runat="server" Text="Agregar" TabIndex="8" ValidationGroup="VGPierna" AutoPostBack="true"
                                    OnClick="btnAgregarFV_Click">
                                    <SettingsBootstrap RenderOption="Success" />
                                </dx:BootstrapButton>

                                <dx:BootstrapButton ID="btnCancelarFV" runat="server" Text="Cancelar" TabIndex="9" ValidationGroup="VGPierna" AutoPostBack="true">
                                    <ClientSideEvents Click="function(s, e) { ppTramos.Hide(); }" />
                                    <SettingsBootstrap RenderOption="Warning" />
                                </dx:BootstrapButton>

                                <dx:ASPxHiddenField ID="hfOrigenFV" ClientInstanceName="hfOrigenFV" runat="server"></dx:ASPxHiddenField>
                                <dx:ASPxHiddenField ID="hfIdFerry" ClientInstanceName="hfIdFerry" runat="server"></dx:ASPxHiddenField>
                            </div>
                        </div>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </dx:ContentControl>
        </ContentCollection>
    </dx:BootstrapPopupControl>


    <%--MODAL PARA VER LA INFORMACION DE UN FERRY--%>
    <dx:BootstrapPopupControl ID="ppInformacionFerry" runat="server" ClientInstanceName="ppInformacionFerry" PopupElementCssSelector="#info-popup-control"
        CloseAnimationType="Fade" PopupAnimationType="Fade"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="650px" Height="250px" HeaderText="Información ferry"
        AllowDragging="true" Modal="true" CloseAction="CloseButton" ShowCloseButton="true" AllowResize="true">
        <ContentCollection>
            <dx:ContentControl>
                    <div class="row">
                        <div class="col-sm-12">
                            <dx:BootstrapPageControl ID="tabConFacturacion" runat="server" TabAlign="Justify">
                                <TabPages>
                                    <dx:BootstrapTabPage Text="Inf. Registro">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <div class="row">
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <dx:BootstrapTextBox ID="txtUsuarioRegistro" runat="server" Caption="Usuario:" ReadOnly="true"></dx:BootstrapTextBox>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <dx:BootstrapTextBox ID="txtFechaRegistro" runat="server" Caption="Fecha:" Width="100%" ReadOnly="true"></dx:BootstrapTextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:BootstrapTabPage>
                                    <dx:BootstrapTabPage Text="Inf. Compra" >
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <dx:BootstrapTextBox ID="txtUsuarioCompra" runat="server" Caption="Usuario:" ReadOnly="true"></dx:BootstrapTextBox>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <dx:BootstrapTextBox ID="txtFechaCompra" runat="server" Caption="Fecha:" Width="100%" ReadOnly="true"></dx:BootstrapTextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <dx:BootstrapTextBox ID="txtMembresiaCliente" runat="server" Caption="Membresia:" ReadOnly="true"></dx:BootstrapTextBox>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <dx:BootstrapTextBox ID="txtPrecioCompra" runat="server" Caption="Precio compra:" Width="100%" ReadOnly="true"></dx:BootstrapTextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <dx:BootstrapTextBox ID="txtMetodoPago" runat="server" Caption="Método de pago:" ReadOnly="true"></dx:BootstrapTextBox>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <dx:BootstrapTextBox ID="txtTipoCambio" runat="server" Caption="Tipo de cambio:" Width="100%" ReadOnly="true"></dx:BootstrapTextBox>
                                                    </div>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:BootstrapTabPage>
                                    <dx:BootstrapTabPage Text="Inf. Cancelación" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <dx:BootstrapTextBox ID="txtUsuarioCancelacion" runat="server" Caption="Usuario:" ReadOnly="true"></dx:BootstrapTextBox>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <dx:BootstrapTextBox ID="txtFechaCancelacion" runat="server" Caption="Fecha:" Width="100%" ReadOnly="true"></dx:BootstrapTextBox>
                                                    </div>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:BootstrapTabPage>
                                    <dx:BootstrapTabPage Text="Modificaciones" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <dx:BootstrapGridView ID="gvLogModificaciones" runat="server" KeyFieldName="IdFerryHist" OnPageIndexChanged="gvLogModificaciones_PageIndexChanged">
                                                            <SettingsSearchPanel Visible="false" />
                                                            <Settings ShowGroupPanel="false" ShowFilterRowMenu="true" />
                                                            <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                                            <Columns>
                                                                <dx:BootstrapGridViewDataColumn FieldName="FechaSalida" Caption="Fecha inicio" VisibleIndex="1" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center"/>
                                                                <dx:BootstrapGridViewDataColumn FieldName="FechaLlegada" Caption="Fecha fin" VisibleIndex="2" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn"  />
                                                                <dx:BootstrapGridViewDataColumn FieldName="NoPax" Caption="Pax" VisibleIndex="3" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="False" />
                                                                <dx:BootstrapGridViewDataColumn FieldName="FechaModificacion" Caption="Fecha" VisibleIndex="4" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="False" />
                                                                <dx:BootstrapGridViewDataColumn FieldName="UsuarioModificacion" Caption="Usuario" VisibleIndex="5" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="False" />
                                                            </Columns>
                                                            <ClientSideEvents Init="onSelectionGridViewAction" SelectionChanged="onSelectionGridViewAction" EndCallback="onSelectionGridViewAction" />
                                                        </dx:BootstrapGridView>
                                                    </div>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:BootstrapTabPage>
                                </TabPages>
                            </dx:BootstrapPageControl>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12" style="text-align: center">
                            <dx:BootstrapButton ID="btnCancelarInfo" runat="server" Text="Cerrar" TabIndex="9" ValidationGroup="VGPierna" AutoPostBack="true">
                                <ClientSideEvents Click="function(s, e) { ppInformacionFerry.Hide(); }" />
                                <SettingsBootstrap RenderOption="Warning" />
                            </dx:BootstrapButton>

                            <%--<dx:ASPxHiddenField ID="ASPxHiddenField1" ClientInstanceName="hfOrigenFV" runat="server"></dx:ASPxHiddenField>
                            <dx:ASPxHiddenField ID="ASPxHiddenField2" ClientInstanceName="hfIdFerry" runat="server"></dx:ASPxHiddenField>--%>
                        </div>
                    </div>
            </dx:ContentControl>
        </ContentCollection>
    </dx:BootstrapPopupControl>


    <%--MODAL PARA EDITAR LA INFORMACIÓN DE UN FERRY--%>
    <dx:BootstrapPopupControl ID="ppEdicionFerry" runat="server" ClientInstanceName="ppEdicionFerry" PopupElementCssSelector="#info-popup-control"
        CloseAnimationType="Fade" PopupAnimationType="Fade"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="550px" Height="250px" HeaderText="Edición de ferry"
        AllowDragging="true" Modal="true" CloseAction="CloseButton" ShowCloseButton="true" AllowResize="true">
        <ContentCollection>
            <dx:ContentControl>
                <div class="row">
                    <div class="row">
                        <div class="col-lg-1"></div>
                        <div class="col-lg-5">
                            <dx:BootstrapDateEdit ID="txtFechaInicio" runat="server" Caption="Inicio:"></dx:BootstrapDateEdit>
                        </div>
                        <div class="col-lg-5">
                            <dx:BootstrapTimeEdit ID="txtHoraInicio" runat="server" Caption="." EditFormat="Custom" EditFormatString="HH:mm"></dx:BootstrapTimeEdit>
                        </div>
                        <div class="col-lg-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-lg-1"></div>
                        <div class="col-lg-5">
                            <dx:BootstrapDateEdit ID="txtFechaFin" runat="server" Caption="Fin:"></dx:BootstrapDateEdit>
                        </div>
                        <div class="col-lg-5">
                            <dx:BootstrapTimeEdit ID="txtHoraFin" runat="server" Caption="." EditFormat="Custom" EditFormatString="HH:mm"></dx:BootstrapTimeEdit>
                        </div>
                        <div class="col-lg-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-lg-1"></div>
                        <div class="col-lg-5">
                            <dx:BootstrapTextBox ID="txtNoPax" runat="server" Caption="No. pasajeros:"></dx:BootstrapTextBox>
                        </div>
                        <div class="col-lg-5">
                            
                        </div>
                        <div class="col-lg-1"></div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div style="text-align: center">
                        <dx:BootstrapButton ID="btnAceptarEdit" runat="server" Text="Aceptar" OnClick="btnAceptarEdit_Click">
                            <SettingsBootstrap RenderOption="Success" />
                        </dx:BootstrapButton>

                        <dx:BootstrapButton ID="btnCancelarEdit" runat="server" Text="Cancelar">
                            <ClientSideEvents Click="function(s, e) { ppEdicionFerry.Hide(); }" />
                            <SettingsBootstrap RenderOption="Warning" />
                        </dx:BootstrapButton>
                    </div>
                </div>
            </dx:ContentControl>
        </ContentCollection>
    </dx:BootstrapPopupControl>

</asp:Content>
