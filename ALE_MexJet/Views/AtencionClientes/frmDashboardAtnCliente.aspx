<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmDashboardAtnCliente.aspx.cs" Inherits="ALE_MexJet.Views.AtencionClientes.frmDashboardAtnCliente" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Dashboard</span>
                    </div>
                </div>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <div class="well-g">
                    <div class="row">
                        <div class="col-md-12" runat="server" visible="false">
                            <fieldset class="Personal">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                                </legend>
                                <div class="col-sm-12">
                                    <table style="width: 100%;" border="0">
                                        <tr>
                                            <td colspan="3">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" width="23%">
                                                <dx:ASPxLabel runat="server" Text="Fecha de Búsqueda:"></dx:ASPxLabel>
                                            </td>
                                            <td style="text-align: center" width="23%">
                                                <dx:ASPxDateEdit ID="deFecha" ClientInstanceName="deFecha" NullText="Fecha Búsqueda" runat="server" Theme="Office2010Black">
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnBusqueda" runat="server" Text="Buscar" Theme="Office2010Black" OnClick="btnBusqueda_Click"></dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">&nbsp;
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
                            <fieldset class="Personal">
                                <legend></legend>
                                <div class="col-sm-12">
                                    <table style="width: 100%; text-align: left;" border="0">
                                        <tr>
                                            <td>
                                                <b>
                                                <dx:ASPxLabel runat="server" Text="Solicitudes de Vuelo" Theme="Office2010Black" Font-Bold="True"></dx:ASPxLabel>
                                                </b>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td style="width: 40%">&nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxLabel runat="server" Text="Casos" Theme="Office2010Black" Font-Bold="True"></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 15%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel runat="server" Text="Estatus Nuevo:" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <asp:LinkButton runat="server" ID="lbNuevo" OnClick="lbNuevo_Click"></asp:LinkButton>
                                            </td>
                                            <td></td>
                                            <td>
                                                <dx:ASPxLabel runat="server" Text="Solicitudes Especiales:" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="hlSolEsp" runat="server" OnClick="hlSolEsp_Click"></asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel runat="server" Text="Estatus Trabajando:" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="hlTrabajando" runat="server" OnClick="hlTrabajando_Click"></asp:LinkButton>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxLabel runat="server" Text="Quejas semana:" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="hlQuejas" runat="server" OnClick="hlQuejas_Click"></asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxLabel runat="server" Text="Incidencias semana:" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="hlIncidencias" runat="server" OnClick="hlIncidencias_Click"></asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxLabel runat="server" Text="Demoras semana:" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lbDemoras" runat="server" OnClick="lbDemoras_Click"></asp:LinkButton>
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
                        <div class="col-sm-12">
                            <b>
                                <asp:Label ID="lblSeguimiento" runat="server" Text="Listado Solicitudes de Vuelo"></asp:Label>
                            </b>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel runat="server" ID="upGv" OnUnload="Unnamed_Unload" UpdateMode="Always">
                                <ContentTemplate>
                                     <div class="col-sm-12">
                                    <dx:ASPxGridView ID="gvVuelos" runat="server" AutoGenerateColumns="False"
                                        ClientInstanceName="gvVuelos" EnableTheming="True"  Styles-Header-HorizontalAlign ="Center" Theme="Office2010Black" Width="100%">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="IdTramo" Caption="Folio" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="FechaVuelo" Caption="Fecha Vuelo" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="TRIP" Caption="TRIP" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ClaveContrato" Caption="Clave Contrato" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="RUTA" Caption="Ruta" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataCheckColumn FieldName="COMISARIATO" Caption="Comisariato" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataCheckColumn FieldName="Transportacion" Caption="Transportación" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataTextColumn FieldName="StatusVuelo" Caption="Estatus Vuelo" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Equipo" Caption="Equipo" VisibleIndex="9" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior ConfirmDelete="True" />
                                            <SettingsPager Position="TopAndBottom" Visible="true">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible ="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsEditing Mode="Inline"></SettingsEditing>
                                            <SettingsPopup>
                                                <EditForm HorizontalAlign="Center" VerticalAlign="WindowCenter" Width="900px" />
                                            </SettingsPopup>
                                            <SettingsSearchPanel Visible="true" />
                                    </dx:ASPxGridView>
                                         </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <br />
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

    <dx:ASPxPopupControl ID="ppDetalle" runat="server" Theme="Office2010Black" ClientInstanceName="ppDetalle" Width="800px" Height="300px" CloseAction="CloseButton"
        CloseOnEscape="true" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Detalle">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel4" runat="server" DefaultButton="btnCerrar">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <dx:ASPxGridView ID="gvSolVuelo" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvSolVuelo" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign="Center"
                                            Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="IdTramo" Caption="Folio" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" Width="10%">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CodigoCliente" Caption="Cliente" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" Width="10%">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ClaveContrato" Caption="Contrato" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" Width="10%">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="RUTA" Caption="RUTA" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" Width="25%">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="NOMBRE" Caption="Contacto" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" Width="25%">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="EJECUTIVO" Caption="Ejecutivo" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" Width="10%">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                            <Settings VerticalScrollBarMode="Auto" VerticalScrollBarStyle="Standard" />

                                        </dx:ASPxGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxGridView ID="gvCasos" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvCasos" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign="Center"
                                            Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="IdTramo" Caption="Folio" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CodigoCliente" Caption="Cliente" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ClaveContrato" Caption="Contrato" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Motivo" Caption="Motivo" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Minutos" Caption="Minutos" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AreaDescripcion" Caption="Area" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DescEspecial" Caption="Descripccion" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataCheckColumn FieldName="Otorgado" Caption="Otorgado" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataCheckColumn>
                                                <dx:GridViewDataTextColumn FieldName="EJECUTIVO" Caption="Ejecutivo" VisibleIndex="9" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                        </dx:ASPxGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnCerrar" Text="Cerrar" runat="server" Theme="Office2010Black">
                                            <ClientSideEvents Click="function(s,e) { ppDetalle.Hide(); }" />
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
