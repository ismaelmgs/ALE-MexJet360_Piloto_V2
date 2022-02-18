<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmDetalleVueloCliente.aspx.cs" Inherits="ALE_MexJet.Views.Reportes.frmDetalleVueloCliente" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Detalle de Vuelos</span>
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
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: left;">
                                                <dx:ASPxLabel Theme="Office2010Black" runat="server" Text="Seleccion:" Font-Bold="True"></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 45%;">
                                                <dx:ASPxRadioButtonList ID="rbSeleccion" runat="server" Theme="Office2010Black" ToolTip="Seleccion" OnSelectedIndexChanged="rbSeleccion_SelectedIndexChanged"
                                                    ClientInstanceName="rbSeleccion" RepeatDirection="Horizontal" AutoPostBack="true">
                                                    <Items>
                                                        <dx:ListEditItem Text="Cliente" Value="0" />
                                                        <dx:ListEditItem Text="Matricula" Value="1" />
                                                    </Items>
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="Grupo1">
                                                        <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                    </ValidationSettings>
                                                </dx:ASPxRadioButtonList>
                                            </td>
                                            <td style="text-align: center" colspan="2"></td>
                                        </tr>
                                        <tr style="height: 10px;">
                                            <td colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <dx:ASPxLabel Theme="Office2010Black" runat="server" Text="Cliente:" Font-Bold="True"></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 45%;">
                                                <dx:ASPxComboBox ToolTip="Cliente" ID="ddlClientes" runat="server" Theme="Office2010Black" EnableTheming="True" NullText="Seleccione un Cliente" AutoPostBack="true" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged">
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="Text" ValidationGroup="Grupo1">
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="text-align: center" colspan="2">
                                                <dx:ASPxLabel Theme="Office2010Black" runat="server" Text="Rango de Fechas" Font-Bold="True" Font-Size="Small"></dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr style="height: 10px;">
                                            <td colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <dx:ASPxLabel Theme="Office2010Black" Font-Bold="True" runat="server" Text="Contrato:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional" OnUnload="UpdatePanel1_Unload">
                                                    <ContentTemplate>
                                                        <dx:ASPxComboBox ToolTip="Contrato" ID="ddlContrato" NullText="Seleccione un Contrato" runat="server" Theme="Office2010Black" EnableTheming="True">
                                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="Text" ValidationGroup="Grupo1">
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlClientes" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <dx:ASPxDateEdit HeaderText="Desde:" ID="dFechaIni" ClientInstanceName="dFechaIni" NullText="Fecha Inicial" runat="server" ToolTip="Fecha Inicial" Theme="Office2010Black" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Es necesario fecha inicial">
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="Text" ValidationGroup="Grupo1">
                                                        <RequiredField IsRequired="True" ErrorText="Es necesario fecha inicial"></RequiredField>
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td style="text-align: right">
                                                <dx:ASPxDateEdit HeaderText="Hasta:" ID="dFechaFin" ClientInstanceName="dFechaFin" runat="server" Theme="Office2010Black" ToolTip="Fecha Final" NullText="Fecha Final" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Es necesario fecha final">
                                                    <DateRangeSettings StartDateEditID="dFechaIni"></DateRangeSettings>
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="Text" ValidationGroup="Grupo1">

                                                        <RequiredField IsRequired="True" ErrorText="Es necesario fecha final"></RequiredField>

                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                        </tr>
                                        <tr style="height: 10px;">
                                            <td colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <dx:ASPxLabel Theme="Office2010Black" Font-Bold="True" runat="server" Text="Matricula:"></dx:ASPxLabel>
                                            </td>
                                            <td style="text-align: left">
                                                <dx:ASPxTextBox ID="txtMatricula" runat="server" Theme="Office2010Black" NullText="Matricula">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td></td>
                                            <td style="text-align: right">
                                                <dx:ASPxButton ID="btnBusqueda" Text="Buscar" runat="server" Theme="Office2010Black" ValidationGroup="Grupo1" OnClick="btnBusqueda_Click">
                                                </dx:ASPxButton>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div style="width: auto; overflow-x: scroll;" id="scrol" runat="server">
                            <div class="col-md-12">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel9" UpdateMode="Always" OnUnload="UpdatePanel1_Unload">
                                    <ContentTemplate>
                                        <div id="dVueloCliente" runat="server"></div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnBusqueda" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div style="width: auto; overflow-x: scroll;" id="Div1" runat="server">
                            <div class="col-md-12">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always" OnUnload="UpdatePanel1_Unload">
                                    <ContentTemplate>
                                        <div id="dVueloMatricula" runat="server"></div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnBusqueda" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div style="text-align: right">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional" OnUnload="UpdatePanel1_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a: "></dx:ASPxLabel>
                                        &nbsp;<dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" ToolTip="Exportar a Excel" OnClick="btnExcel_Click"></dx:ASPxButton>
                                        
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExcel" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel5" OnUnload="UpdatePanel1_Unload">
                    <ContentTemplate>
                        <dx:ASPxHiddenField ID="hfCliente" runat="server" />
                        <dx:ASPxHiddenField ID="hfContrato" runat="server" />
                        <dx:ASPxHiddenField ID="hfFechaInicial" runat="server" />
                        <dx:ASPxHiddenField ID="hfFechaFinal" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

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
                                        <dx:ASPxButton ID="btOK" runat="server" Text="OK" Theme="Office2010Black" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
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

</asp:Content>
