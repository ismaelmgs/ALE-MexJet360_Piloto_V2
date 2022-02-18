<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmBitacora.aspx.cs" Inherits="ALE_MexJet.Views.Consultas.frmBitacora" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" style="border-radius:21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Bitácora</span>
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
                                    <table style="width:100%;" border="0">
                                        <tr>
                                            <td width="2%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: left" width="23%">
                                                <dx:ASPxLabel runat="server" Text="Folio:"></dx:ASPxLabel>
                                            </td>
                                            <td width="23%">
                                                <asp:Panel ID="panelTextoBusqueda" runat="server" DefaultButton="btnBusqueda" style="width:250px;">
                                                    <dx:ASPxTextBox ID="txtTextoBusqueda" ToolTip="Folio" runat="server" Theme="Office2010Black" NullText="Ingrese el folio a buscar">
                                                        <ValidationSettings>
                                                            <RegularExpression ValidationExpression="^[a-zA-Z0-9]*$" ErrorText="Sólo se permiten números y letras" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </asp:Panel>
                                            </td>
                                            <td width="4%">
                                                &nbsp;
                                            </td>
                                            <td colspan="2" width="46%">
                                                <dx:ASPxLabel Font-Size="Larger" runat="server" Text="Rango de Fechas"></dx:ASPxLabel>
                                             </td>
                                            <td width="2%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="2%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: left" width="23%">
                                                <dx:ASPxLabel runat="server" Text="Cliente:"></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 25%;" width="23%">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel2" OnUnload="UpdatePanel2_Unload">
                                                    <ContentTemplate>
                                                        <dx:ASPxComboBox ToolTip="Cliente" ID="ddlClientes" runat="server" Theme="Office2010Black" EnableTheming="True" NullText="Seleccione un Cliente" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged" AutoPostBack="true">
                                                        </dx:ASPxComboBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td width="4%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: center" width="23%">
                                                <dx:ASPxDateEdit Caption="Desde:" ID="dFechaIni" ClientInstanceName="dFechaIni" NullText="Fecha Inicial" runat="server" ToolTip="Fecha Inicial" Theme="Office2010Black">
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td style="text-align: center" width="23%">
                                                <dx:ASPxDateEdit Caption="Hasta:" ID="dFechaFin" ClientInstanceName="dFechaFin" runat="server" Theme="Office2010Black" ToolTip="Fecha Final" NullText="Fecha Final">
                                                    <DateRangeSettings StartDateEditID="dFechaIni"></DateRangeSettings>
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td width="2%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="2%">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: left" width="23%">
                                                <dx:ASPxLabel runat="server" Text="Contrato:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" OnUnload="UpdatePanel1_Unload" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <dx:ASPxComboBox ToolTip="Contrato" ID="ddlContrato" NullText="Seleccione un Contrato" runat="server" Theme="Office2010Black" EnableTheming="True">
                                                        </dx:ASPxComboBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlClientes" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td width="4%">
                                                &nbsp;
                                            </td>
                                            <td colspan="2" style="text-align: center" width="46%">
                                                <dx:ASPxButton ID="btnBusqueda" Text="Buscar" runat="server" Theme="Office2010Black" OnClick="btnBusqueda_Click"></dx:ASPxButton>
                                                                                                
                                            </td>
                                            <td width="2%">
                                                
                                                &nbsp;
                                            </td>
                                        </tr>
                                       <td width="4%">
                                                &nbsp;
                                       </td>
                                       <table style="width:100%;" border="0">
                                            <td style="text-align:center">
                                                <br />
                                                <dx:ASPxButton ID="btnPosiblesDuplicados" Text="Buscar Duplicados" runat="server" Theme="Office2010Black" OnClick="btnPosiblesDuplicados_Click"></dx:ASPxButton>
                                            </td>
                                            <td style="text-align:center">
                                                <br />
                                                <dx:ASPxButton ID="btnBitacorasCobradas" Text="Buscar Cobradas" runat="server" Theme="Office2010Black" OnClick="btnBitacorasCobradas_Click"></dx:ASPxButton>
                                            </td>                      
                                             <td style="text-align:center">
                                                <br />
                                                <dx:ASPxButton ID="btnBitacoraPorCobrar" Text="Buscar Por Cobrar" runat="server" Theme="Office2010Black" OnClick="btnBitacoraPorCobrar_Click"></dx:ASPxButton>
                                            </td>
                                       </table>                                       
                                    </table>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="Personal">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Resumen Bitácoras</span>
                                </legend>
                                <div class="col-sm-12">
                                    <table style="width:100%;" border="0">
                                        <td style="text-align:center">
                                        <dx:ASPxLabel runat="server"   Theme="Office2010Black" Text="Total Registros: "></dx:ASPxLabel>
                                        <dx:ASPxLabel  ID="LblTotalRegistro" runat="server" Text="0"></dx:ASPxLabel>
                                        </td>
                                        <td style="text-align:center">
                                        <dx:ASPxLabel runat="server"   Theme="Office2010Black" Text="Total Bitácoras Duplicadas: "></dx:ASPxLabel>
                                        <dx:ASPxLabel  ID="LblTotalDuplicadas" runat="server" Text="0"></dx:ASPxLabel>
                                        </td>
                                        <td style="text-align:center">
                                        <dx:ASPxLabel runat="server"   Theme="Office2010Black" Text="Total Bitácoras Cobradas: "></dx:ASPxLabel>
                                        <dx:ASPxLabel  ID="LblTotalCobradas" runat="server" Text="0"></dx:ASPxLabel>
                                        </td>
                                        <td style="text-align:center">
                                        <dx:ASPxLabel runat="server"   Theme="Office2010Black" Text="Total Bitácoras Por Cobrar: "></dx:ASPxLabel>
                                        <dx:ASPxLabel  ID="LblTotalPorCobar" runat="server" Text="0"></dx:ASPxLabel>
                                        </td>
                                    </table>
                                </div>
                            </fieldset>
                         </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel runat="server"  ID="upGv" OnUnload ="UpdatePanel1_Unload"  UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div>
                                        <dx:ASPxGridView ID="gvNotaCredito" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvNotaCredito" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign ="Center"
                                            Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="IdBitacora"   Caption="Id Bitácora" VisibleIndex="1"  HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AeronaveSerie" Caption="Serie Aeronave" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AeronaveMatricula" Caption="Matrícula Aeronave" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FolioReal" Caption="Folio Real" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="VueloClienteId" Caption="Vuelo Cliente Id" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="VueloContratoId" Caption="Vuelo Contrato Id" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="VueloTramoId" Caption="Vuelo Tramo Id"  VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Origen" Caption="Origen"  VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Destino" Caption="Destino"  VisibleIndex="9" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="OrigenVuelo" Caption="Origen Vuelo"  VisibleIndex="10" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="OrigenCalzo" Caption="Origen Calzo"  VisibleIndex="11" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DestinoVuelo" Caption="Destino Vuelo"  VisibleIndex="12" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DestinoCalzo" Caption="Destino Calzo"  VisibleIndex="13" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FechaCreacion" Caption="Fecha Creación"  VisibleIndex="14" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CantPax" Caption="PAX"  VisibleIndex="15" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Remisionado" Caption="No. Remisión"  VisibleIndex="16" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TripNum" Caption="No Trip"  VisibleIndex="17" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Import" Caption="Fecha Transferencia"  VisibleIndex="18" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Categoria" Caption="Categoría"  VisibleIndex="19" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="LogNum" Caption="No. Log"  VisibleIndex="19" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings HorizontalScrollBarMode="Auto" ShowGroupPanel="True" />
                                            <SettingsSearchPanel Visible="true"  />
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvNotaCredito">
                                        </dx:ASPxGridViewExporter>
                                        <%-- Bitacoras Duplicadas --%>

                                        <dx:ASPxGridView ID="gvBitDuplicada" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvBitDuplicada" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign ="Center"
                                            Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90" OnCommandButtonInitialize="gvBitDuplicada_CommandButtonInitialize"
                                            Visible="false" KeyFieldName="IdBitacora" OnRowDeleting="gvBitDuplicada_RowDeleting"
                                            >
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="IdBitacora"   Caption="Id Bitácora" VisibleIndex="1"  HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AeronaveSerie" Caption="Serie Aeronave" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AeronaveMatricula" Caption="Matrícula Aeronave" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FolioReal" Caption="Folio Real" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="VueloClienteId" Caption="Vuelo Cliente Id" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="VueloContratoId" Caption="Vuelo Contrato Id" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="VueloTramoId" Caption="Vuelo Tramo Id"  VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Origen" Caption="Origen"  VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Destino" Caption="Destino"  VisibleIndex="9" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="OrigenVuelo" Caption="Origen Vuelo"  VisibleIndex="10" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="OrigenCalzo" Caption="Origen Calzo"  VisibleIndex="11" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DestinoVuelo" Caption="Destino Vuelo"  VisibleIndex="12" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DestinoCalzo" Caption="Destino Calzo"  VisibleIndex="13" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FechaCreacion" Caption="Fecha Creación"  VisibleIndex="14" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CantPax" Caption="PAX"  VisibleIndex="15" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Remisionado" Caption="No. Remisión"  VisibleIndex="16" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TripNum" Caption="No Trip"  VisibleIndex="17" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Import" Caption="Fecha Transferencia"  VisibleIndex="18" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Tipo" Caption="Categoría"  VisibleIndex="19" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="LogNum" Caption="No. Log"  VisibleIndex="19" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewCommandColumn   ButtonType="Button" Caption="Acciones" ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="25">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewCommandColumn>
                                            </Columns>
                                            <Settings HorizontalScrollBarMode="Auto" ShowGroupPanel="True" />
                                            <SettingsBehavior ConfirmDelete="True" />
                                            <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                            <SettingsSearchPanel Visible="true"  />
                                            <SettingsPager Position="TopAndBottom" Visible="true">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                        </dx:ASPxGridView>

                                        <%-- Bitacoras Cobradas --%>

                                        <dx:ASPxGridView ID="gvBitCobrada" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvBitCobrada" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign ="Center"
                                            Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90" OnCommandButtonInitialize="gvBitCobrada_CommandButtonInitialize"
                                            Visible="false" KeyFieldName="IdBitacora" OnRowDeleting="gvBitCobrada_RowDeleting"
                                            >
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="IdBitacora"   Caption="Id Bitácora" VisibleIndex="1"  HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AeronaveSerie" Caption="Serie Aeronave" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AeronaveMatricula" Caption="Matrícula Aeronave" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FolioReal" Caption="Folio Real" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="VueloClienteId" Caption="Vuelo Cliente Id" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="VueloContratoId" Caption="Vuelo Contrato Id" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="VueloTramoId" Caption="Vuelo Tramo Id"  VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Origen" Caption="Origen"  VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Destino" Caption="Destino"  VisibleIndex="9" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="OrigenVuelo" Caption="Origen Vuelo"  VisibleIndex="10" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="OrigenCalzo" Caption="Origen Calzo"  VisibleIndex="11" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DestinoVuelo" Caption="Destino Vuelo"  VisibleIndex="12" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DestinoCalzo" Caption="Destino Calzo"  VisibleIndex="13" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FechaCreacion" Caption="Fecha Creación"  VisibleIndex="14" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CantPax" Caption="PAX"  VisibleIndex="15" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Remisionado" Caption="No. Remisión"  VisibleIndex="16" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TripNum" Caption="No Trip"  VisibleIndex="17" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Import" Caption="Fecha Transferencia"  VisibleIndex="18" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Tipo" Caption="Categoría"  VisibleIndex="19" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="LogNum" Caption="No. Log"  VisibleIndex="19" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewCommandColumn   ButtonType="Button" Caption="Acciones" ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="25">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewCommandColumn>
                                            </Columns>
                                            <Settings HorizontalScrollBarMode="Auto" ShowGroupPanel="True" />
                                            <SettingsBehavior ConfirmDelete="True" />
                                            <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                            <SettingsSearchPanel Visible="true"  />
                                            <SettingsPager Position="TopAndBottom" Visible="true">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                        </dx:ASPxGridView>

                                        <%-- Bitacoras Por Cobrar --%>

                                        <dx:ASPxGridView ID="gvBitPorCobrar" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvBitPorCobrar" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign ="Center"
                                            Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90" OnCommandButtonInitialize="gvBitPorCobrar_CommandButtonInitialize"
                                            Visible="false" KeyFieldName="IdBitacora" OnRowDeleting="gvBitPorCobrar_RowDeleting"
                                            >
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="IdBitacora"   Caption="Id Bitácora" VisibleIndex="1"  HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AeronaveSerie" Caption="Serie Aeronave" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AeronaveMatricula" Caption="Matrícula Aeronave" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FolioReal" Caption="Folio Real" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="VueloClienteId" Caption="Vuelo Cliente Id" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="VueloContratoId" Caption="Vuelo Contrato Id" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="VueloTramoId" Caption="Vuelo Tramo Id"  VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Origen" Caption="Origen"  VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Destino" Caption="Destino"  VisibleIndex="9" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="OrigenVuelo" Caption="Origen Vuelo"  VisibleIndex="10" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="OrigenCalzo" Caption="Origen Calzo"  VisibleIndex="11" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DestinoVuelo" Caption="Destino Vuelo"  VisibleIndex="12" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DestinoCalzo" Caption="Destino Calzo"  VisibleIndex="13" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FechaCreacion" Caption="Fecha Creación"  VisibleIndex="14" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CantPax" Caption="PAX"  VisibleIndex="15" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Remisionado" Caption="No. Remisión"  VisibleIndex="16" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TripNum" Caption="No Trip"  VisibleIndex="17" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Import" Caption="Fecha Transferencia"  VisibleIndex="18" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Tipo" Caption="Categoría"  VisibleIndex="19" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="LogNum" Caption="No. Log"  VisibleIndex="19" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewCommandColumn   ButtonType="Button" Caption="Acciones" ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="25">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewCommandColumn>
                                            </Columns>
                                            <Settings HorizontalScrollBarMode="Auto" ShowGroupPanel="True" />
                                            <SettingsBehavior ConfirmDelete="True" />
                                            <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                            <SettingsSearchPanel Visible="true"  />
                                            <SettingsPager Position="TopAndBottom" Visible="true">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                        </dx:ASPxGridView>

                                    </div>  
                                </ContentTemplate>
                               <Triggers>
                                   <asp:AsyncPostBackTrigger  ControlID="btnBusqueda" EventName="Click"/>
                                   <asp:AsyncPostBackTrigger  ControlID="btnPosiblesDuplicados" EventName="Click"/>
                               </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <br />
            <div class="row">
            <div class="col-md-12">
            <div   style="text-align:right" >
                 <asp:UpdatePanel runat="server"  ID="UpdatePanel4" UpdateMode="Conditional"  OnUnload="UpdatePanel1_Unload">
                                <ContentTemplate>
                                <dx:ASPxLabel runat="server"   Theme="Office2010Black" Text="Exportar a: "></dx:ASPxLabel>
                            &nbsp;<dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" ToolTip="Exportar a Excel" OnClick="btnExportar_Click" ></dx:ASPxButton>
                                      </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExcel" />
                            </Triggers>
                            </asp:UpdatePanel>
            </div>
            </div>
            </div>
                    </div>
                <asp:UpdatePanel runat="server"  ID="UpdatePanel3" OnUnload="UpdatePanel1_Unload">
                    <ContentTemplate>
                        <dx:ASPxHiddenField ID="hfFolio" runat="server"/>
                        <dx:ASPxHiddenField ID="hfCliente" runat="server"/>
                        <dx:ASPxHiddenField ID="hfContrato" runat="server"/>
                        <dx:ASPxHiddenField ID="hfFechaInicial" runat="server"/>
                        <dx:ASPxHiddenField ID="hfFechaFinal" runat="server"/>
                    </ContentTemplate>
                </asp:UpdatePanel>
     </dx:PanelContent>
    </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>