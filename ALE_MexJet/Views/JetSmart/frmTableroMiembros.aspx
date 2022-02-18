<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmTableroMiembros.aspx.cs" Inherits="ALE_MexJet.Views.JetSmart.frmTableroMiembros" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OcultaError() {
            ppEditarMiembro.Hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Autorización de Miembros</span>
                    </div>
                </div>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <div class="well-g">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                                </legend>
                                <div class="row">
                                    <div class="col-lg-4">
                                        <dx:BootstrapRadioButtonList ID="rblFiltro" runat="server" RepeatDirection="Horizontal">
                                            <Items>
                                                <dx:BootstrapListEditItem Text="Pendienes" Value="1" Selected="true" />
                                                <dx:BootstrapListEditItem Text="Aprobados" Value="2" />
                                                <dx:BootstrapListEditItem Text="Rechazados" Value="0" />
                                            </Items>
                                        </dx:BootstrapRadioButtonList>
                                    </div>
                                    <div class="col-lg-6" style="text-align:center; vertical-align:bottom">
                                        <dx:ASPxLabel Theme="Office2010Black" runat="server" Text="Rango de Fechas" Font-Bold="true" Font-Size="Small"></dx:ASPxLabel>
                                    </div>
                                    <div class="col-lg-2"></div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4"></div>
                                    <div class="col-lg-3" style="text-align:center; font-weight:bold">
                                        Desde:
                                    </div>
                                    <div class="col-lg-3" style="text-align:center; font-weight:bold">
                                        Hasta:
                                    </div>
                                    <div class="col-lg-2"></div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4"></div>
                                    <div class="col-lg-3" style="text-align:center">
                                        <dx:BootstrapDateEdit ID="dFechaIni" ClientInstanceName="dFechaIni" NullText="Fecha Inicial" runat="server" ToolTip="Fecha Inicial" Width="80%">
                                            <ValidationSettings ErrorDisplayMode="Text" SetFocusOnError="True" CausesValidation="True">
                                                <RegularExpression ValidationExpression =""></RegularExpression>
                                                    <%--<RequiredField ErrorText="Fecha inicial es requerida" IsRequired="True" />--%>
                                            </ValidationSettings>
                                        </dx:BootstrapDateEdit>
                                    </div>
                                    <div class="col-lg-3" style="text-align:center">
                                        <dx:BootstrapDateEdit ID="dFechaFin" ClientInstanceName="dFechaFin" runat="server" ToolTip="Fecha Final" NullText="Fecha Final" Width="80%">
                                            <ValidationSettings ErrorDisplayMode="Text" SetFocusOnError="True" CausesValidation="True" >
                                                <RegularExpression ValidationExpression =""></RegularExpression>
                                                    <%--<RequiredField ErrorText="Fecha final es requerida" IsRequired="True" />       --%>
                                            </ValidationSettings>
                                        </dx:BootstrapDateEdit>
                                    </div>
                                    <div class="col-lg-2" style="vertical-align:bottom">
                                        <dx:BootstrapButton ID="btnBusqueda" runat="server" Text="Buscar" OnClick="btnBusqueda_Click">
                                            <SettingsBootstrap RenderOption="Secondary" />
                                        </dx:BootstrapButton>
                                    </div>
                                </div>
                                <div class="row">
                                    
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel runat="server" ID="upGv" UpdateMode="Conditional" OnUnload="upGv_Unload">
                                <ContentTemplate>
                                    <div>
                                        <dx:BootstrapGridView ID="gvMiembros" runat="server" AutoGenerateColumns="false" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvMiembros" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign="Center"
                                            StylesPopup-EditForm-ModalBackground-Opacity="90">
                                            <Columns>
                                                <dx:BootstrapGridViewDataColumn FieldName="Membresia" Caption="Membresia" VisibleIndex="0" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" Width="120px" />
                                                <dx:BootstrapGridViewDataColumn FieldName="Nombre" Caption="Nombre Miembro" VisibleIndex="1" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" Width="150px" />
                                                <dx:BootstrapGridViewDataColumn FieldName="CorreoElectronico" Caption="e-mail" VisibleIndex="2" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center"/>
                                                <dx:BootstrapGridViewDataColumn FieldName="Telefono" Caption="Teléfono" VisibleIndex="3" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center"/>
                                                <dx:BootstrapGridViewDataColumn FieldName="TelefonoMovil" Caption="Móvil" VisibleIndex="4" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center"/>
                                                <dx:BootstrapGridViewDataColumn FieldName="subscripcion" Caption="Subscripción" VisibleIndex="5" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center"/>
                                                <dx:BootstrapGridViewDataColumn FieldName="DescHoras" Caption="Desc. Horas" VisibleIndex="6" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center"/>
                                                <dx:BootstrapGridViewDataColumn FieldName="CodigoInvitacion" Caption="Codigo Invitación" VisibleIndex="7" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center"/>
                                                
                                                <dx:BootstrapGridViewDataColumn VisibleIndex="8" HorizontalAlign="Center" Caption="Acciones">
                                                    <DataItemTemplate>
                                                        <dx:BootstrapButton ID="btnEditar" runat="server" Text="Editar" CommandArgument='<%# Eval("IdMiembro") %>' OnClick="btnEditar_Click">
                                                            <SettingsBootstrap RenderOption="Info" />
                                                        </dx:BootstrapButton>
                                                        <dx:BootstrapButton ID="btnAprobar" runat="server" Text="Aprobar" CommandArgument='<%# Eval("IdMiembro") %>' OnClick="btnAcepRec_Click">
                                                            <SettingsBootstrap RenderOption="Primary" />
                                                        </dx:BootstrapButton>
                                                        <dx:BootstrapButton ID="btnRechazar" runat="server" Text="Rechazar" CommandArgument='<%# Eval("IdMiembro") %>' OnClick="btnRechazar_Click">
                                                            <SettingsBootstrap RenderOption="Warning" />
                                                        </dx:BootstrapButton>
                                                    </DataItemTemplate>
                                                </dx:BootstrapGridViewDataColumn>
                                            </Columns>
                                            <SettingsSearchPanel Visible="true" />
                                            <SettingsPager Position="Bottom" Visible="true">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                        </dx:BootstrapGridView>
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvMiembros">
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
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional" OnUnload="upGv_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxLabel runat="server" Font-Bold="True" Theme="Office2010Black" Text="Exportar a: "></dx:ASPxLabel>
                                        &nbsp;<dx:BootstrapButton ID="btnExcel" ToolTip="Exportar" runat="server" Text="Excel" OnClick="btnExcel_Click">
                                                <SettingsBootstrap RenderOption="Secondary" />
                                              </dx:BootstrapButton>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExcel" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>


    <%-- MODAL EDITAR A UN MIEMBRO --%>
    <dx:BootstrapPopupControl ID="ppEditarMiembro" runat="server" ClientInstanceName="ppEditarMiembro" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Edición de Miembro" Height="350px"
        AllowDragging="true" ShowCloseButton="true" Width="700">
        <ContentCollection>
            <dx:ContentControl>
                <asp:Panel ID="Panel2" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="Personal1">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"></span>
                                </legend>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <table width="100%">
                                            <tr>
                                                <td style="width:8%"></td>
                                                <td style="width:40%"></td>
                                                <td style="width:4%"></td>
                                                <td style="width:10%"></td>
                                                <td style="width:38%"></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width:8%">
                                                    <dx:ASPxLabel runat="server" ID="lblMembresiaM" Text="Membresia:" Theme="Office2010Black"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left; width:40%">
                                                    <dx:BootstrapComboBox ID="ddlMembresiaM" runat="server" Width="100%"  
                                                        OnSelectedIndexChanged="ddlMembresiaM_SelectedIndexChanged" AutoPostBack="true"></dx:BootstrapComboBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align: left; width:48%" colspan="2">
                                                    <dx:ASPxLabel runat="server" ID="lblRazonSocial" Text="" Theme="Office2010Black"></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height:10px"></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width:8%">
                                                    <dx:ASPxLabel runat="server" ID="lblNombreM" Text="Nombre:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left; width:40%">
                                                    <dx:BootstrapTextBox ID="txtNombreM" runat="server" Width="100%" ></dx:BootstrapTextBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align:left">
                                                    <dx:ASPxLabel runat="server" ID="lblCorreoM" Text="Correo:"></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:BootstrapTextBox ID="txtCorreoM" runat="server" Width="100%"></dx:BootstrapTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height:10px"></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:left">
                                                    <dx:ASPxLabel runat="server" ID="lblTelefonoM" Text="Teléfono móvil:"></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:BootstrapTextBox ID="txtTelefonoM" runat="server" Width="100%"></dx:BootstrapTextBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align:left">
                                                    <dx:ASPxLabel runat="server" ID="lblTelefonoMo" Text="Teléfono:"></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:BootstrapTextBox ID="txtTelefonoMo" runat="server" Width="100%" ></dx:BootstrapTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height:10px"></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:left">
                                                    <dx:ASPxLabel runat="server" ID="lblSubscripcion" Text="Tipo subscripción:"></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:BootstrapComboBox ID="ddlSubscripcion" runat="server" Width="100%" ></dx:BootstrapComboBox>
                                                </td>
                                                <td></td>
                                                <td style="text-align:left">
                                                    <dx:ASPxLabel runat="server" ID="lblDescuentaHoras" Text="Descuenta Horas:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align:left">
                                                    <dx:ASPxCheckBox ID="chkDescuenta" runat="server" Theme="Office2010Black"></dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </fieldset>
                            <br />
                            <table style="width:100%">
                                <tr>
                                    <td style="width:48%; text-align:right">
                                        <dx:BootstrapButton ID="btnAceptarM" runat="server" Text="Aceptar" OnClick="btnAceptarM_Click" >
                                            <SettingsBootstrap RenderOption="Success" />
                                            <ClientSideEvents Click="function OcultaError() {
                                                                        ppEditarMiembro.Hide();
                                                                    }" />
                                        </dx:BootstrapButton>
                                    </td>
                                    <td style="width:4%"></td>
                                    <td style="width:50%; text-align:left">
                                        <dx:BootstrapButton ID="btnCancelarM" runat="server" Text="Cancelar" AutoPostBack="false">
                                            <SettingsBootstrap RenderOption="Warning" />
                                            <ClientSideEvents Click="function OcultaError() {
                                                                        ppEditarMiembro.Hide();
                                                                    }" />
                                        </dx:BootstrapButton>
                                    </td>
                                </tr>
                            </table>
                            
                        </div>
                    </div>
                </asp:Panel>
            </dx:ContentControl>
        </ContentCollection>
    </dx:BootstrapPopupControl>

</asp:Content>
