<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmConsultaCasos.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ALE_MexJet.Views.Consultas.frmConsultaCasos" %>


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
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Consulta Casos</span>
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
                                    <table cellpsdding="5" width="99%" border="0">
                                        <tr>
                                            <td width="2%">&nbsp;
                                            </td>
                                            <td width="23%" style="text-align: left;">
                                                <dx:ASPxLabel runat="server" Text="Número de Solicitud:"></dx:ASPxLabel>
                                            </td>
                                            <td width="23%">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel5" OnUnload="UpdatePanel1_Unload" style="width: 250px;">
                                                    <ContentTemplate>
                                                        <dx:ASPxTextBox ID="txtSolicitud" runat="server" Theme="Office2010Black" NullText="Ingrese la solicitud a buscar">
                                                            <ValidationSettings RegularExpression-ValidationExpression="[a-zA-Z ñÑáéíóúÁÉÍÓÚ]*[0-9]*" ErrorText="Sólo se permiten números y letras"></ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td width="2%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="2%">&nbsp;
                                            </td>
                                            <td width="23%" style="text-align: left;">
                                                <dx:ASPxLabel runat="server" Text="Área:"></dx:ASPxLabel>
                                            </td>

                                            <td width="23%">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel4" OnUnload="UpdatePanel1_Unload">
                                                    <ContentTemplate>
                                                        <dx:ASPxComboBox ToolTip="Area" ID="ddlArea" runat="server" Theme="Office2010Black" EnableTheming="True" NullText="Seleccione un Área" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                                        </dx:ASPxComboBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td width="4%">&nbsp;
                                            </td>
                                            <td width="46%" colspan="2">
                                                <dx:ASPxLabel Font-Size="Larger" runat="server" Text="Rango de Fechas"></dx:ASPxLabel>
                                            </td>
                                            <td width="2%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="2%">&nbsp;
                                            </td>
                                            <td width="23%" style="text-align: left;">
                                                <dx:ASPxLabel runat="server" Text="Cliente:"></dx:ASPxLabel>
                                            </td>
                                            <td width="23%">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel2" OnUnload="UpdatePanel1_Unload">
                                                    <ContentTemplate>
                                                        <dx:ASPxComboBox ToolTip="Cliente" ID="ddlClientes" runat="server" Theme="Office2010Black" EnableTheming="True" NullText="Seleccione un Cliente" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged">
                                                        </dx:ASPxComboBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td width="4%">&nbsp;
                                            </td>
                                            <td width="23%">
                                                <dx:ASPxDateEdit Caption="Desde:" ID="dFechaIni" ClientInstanceName="dFechaIni" NullText="Fecha Inicial" runat="server" ToolTip="Fecha Inicial" Theme="Office2010Black">
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td width="23%">
                                                <dx:ASPxDateEdit Caption="Hasta:" ID="dFechaFin" ClientInstanceName="dFechaFin" runat="server" Theme="Office2010Black" ToolTip="Fecha Final" NullText="Fecha Final">
                                                    <DateRangeSettings StartDateEditID="dFechaIni"></DateRangeSettings>
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td width="2%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="2%">&nbsp;
                                            </td>
                                            <td width="23%" style="text-align: left;">
                                                <dx:ASPxLabel runat="server" Text="Tipo de Caso:"></dx:ASPxLabel>
                                            </td>
                                            <td width="23%">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel6" OnUnload="UpdatePanel1_Unload">
                                                    <ContentTemplate>
                                                        <dx:ASPxComboBox ToolTip="IdTipoCaso" ID="ddlTipoCaso" runat="server" Theme="Office2010Black" OnSelectedIndexChanged="ddlTipoCaso_SelectedIndexChanged" EnableTheming="True" NullText="Seleccione un Tipo de Caso" AutoPostBack="true">
                                                        </dx:ASPxComboBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td width="4%">&nbsp;
                                            </td>
                                            <td width="23%">&nbsp;
                                            </td>
                                            <td width="23%">&nbsp;
                                            </td>
                                            <td width="2%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="2%">&nbsp;
                                            </td>
                                            <td width="23%" style="text-align: left;">
                                                <dx:ASPxLabel runat="server" Text="Motivo:"></dx:ASPxLabel>
                                            </td>
                                            <td width="23%">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel7" OnUnload="UpdatePanel1_Unload">
                                                    <ContentTemplate>
                                                        <dx:ASPxComboBox ToolTip="IdMotivo" ID="ddlMotivo" runat="server" Theme="Office2010Black" EnableTheming="True" NullText="Seleccione un Tipo de Demora" AutoPostBack="true">
                                                        </dx:ASPxComboBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td width="4%">&nbsp;
                                            </td>
                                            <td width="23%" colspan="2" align="center">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" OnUnload="UpdatePanel1_Unload" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlClientes" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <dx:ASPxButton ID="btnBusqueda" Text="Buscar" runat="server" Theme="Office2010Black" OnClick="btnBusqueda_Click"></dx:ASPxButton>
                                            </td>
                                            <td width="2%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7"></td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel runat="server" ID="upGv" OnUnload="UpdatePanel1_Unload" UpdateMode="Always">
                                <ContentTemplate>
                                    <div>
                                        <dx:ASPxGridView ID="gvConsultaCasos" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvConsultaCasos" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign="Center"
                                            Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="IdCaso" Caption="No. Caso" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="IdSolicitud" Caption="No. Solicitud" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" Width="110px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DesTipoCaso" Caption="Tipo de Caso" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CodigoCliente" Caption="Cliente" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ClaveContrato" Caption="Contrato" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" Width="130px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DesArea" Caption="Área" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                
                                                <dx:GridViewDataColumn Caption="Descripción" FieldName="Detalle" VisibleIndex="7" Visible="true" Width="150px">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxTextBox Text='<%#Eval("Detalle") %>' runat="server" Theme="Office2010Black" MaxLength="10">
                                                        </dx:ASPxTextBox>
                                                    </DataItemTemplate>
                                                    <EditFormSettings Visible="false" />
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataTextColumn FieldName="DesMotivoCaso" Caption="Motivo" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Minutos" Caption="Minutos de Demora" VisibleIndex="13" HeaderStyle-HorizontalAlign="Center" Width="150px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DescOtorgado" Caption="Otorgado" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center" Width="110px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FechaCreacion" Caption="Fecha de Caso" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="UsuarioCreacion" Caption="Usuario" VisibleIndex="13" HeaderStyle-HorizontalAlign="Center" Width="210px">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsSearchPanel Visible="true" />
                                            <SettingsPager Position="TopAndBottom" Visible="true">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvConsultaCasos">
                                        </dx:ASPxGridViewExporter>
                                    </div>
                                    <div class="row" style="text-align: right">
                                        <br />
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Conditional" OnUnload="UpdatePanel1_Unload">
                                            <ContentTemplate>
                                                <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a: "></dx:ASPxLabel>
                                                &nbsp;<dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" ToolTip="Exportar a Excel" OnClick="btnExportar_Click"></dx:ASPxButton>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnExcel" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <br />

                    <br />
                </div>
                <br />


                <asp:UpdatePanel runat="server" ID="UpdatePanel3" OnUnload="UpdatePanel1_Unload">
                    <ContentTemplate>
                        <dx:ASPxHiddenField ID="hfCliente" runat="server" />
                        <dx:ASPxHiddenField ID="hfTipoCaso" runat="server" />
                        <dx:ASPxHiddenField ID="hfSolicitud" runat="server" />
                        <dx:ASPxHiddenField ID="hfArea" runat="server" />
                        <dx:ASPxHiddenField ID="hfMotivo" runat="server" />
                        <dx:ASPxHiddenField ID="hfFechaInicial" runat="server" />
                        <dx:ASPxHiddenField ID="hfFechaFinal" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>
