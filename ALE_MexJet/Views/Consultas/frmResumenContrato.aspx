<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmResumenContrato.aspx.cs" EnableEventValidation="false" UICulture="es" Culture="es-MX" Inherits="ALE_MexJet.Views.Consultas.frmResumenContrato" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style lang="cs" type="text/css">
        .bordes td {
            border:solid;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Resumen de Contrato</span>
                    </div>
                </div>
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
                                                <dx:ASPxLabel runat="server" ID="lblCliente" Text="Cliente" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td style="text-align: left;">
                                                <dx:ASPxComboBox runat="server" ID="ddlCiente" NullText="Seleccione" OnSelectedIndexChanged="ddlCiente_SelectedIndexChanged" AutoPostBack="true" Theme="Office2010Black"></dx:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <dx:ASPxLabel runat="server" ID="lblContrato" Text="Contrato" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td style="text-align: left;">
                                                <dx:ASPxComboBox runat="server" ID="ddlContrato" NullText="Seleccione" Theme="Office2010Black"></dx:ASPxComboBox>
                                            </td>
                                            <td colspan="6"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7"></td>
                                            <td>
                                                <dx:ASPxButton runat="server" ID="btnGenerar" Text="Generar" OnClick ="btnGenerar_Click" Theme="Office2010Black"></dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <table style="width:100%">
                            <tr>
                                <td colspan="3" style="text-align:right">
                                    <dx:ASPxButton ID="btnPDF2" runat="server" Text="PDF" OnClick="btnPDF2_Click"    Theme="Office2010Black"></dx:ASPxButton>
                                    <dx:ASPxButton ID="btnExcelR2" runat="server" Text="Excel" OnClick="btnExcel_Click" Theme="Office2010Black"></dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        
                        <asp:UpdatePanel ID="upaReport" runat="server" OnUnload="upaReport_Unload">
                            <ContentTemplate>
                                <asp:Panel ID="pnlReporteTab" runat="server" ScrollBars="Horizontal" Visible="false" Width="100%">
                                    <table style="width:100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:Image ID="imgMexJet" runat="server" Width="150px" Height="80px"  ImageUrl="~/img/mexjet_p.png" />
                                            </td>
                                            <td colspan="10">
                                                <asp:Label ID="lblAle" runat="server" Text="Aerolineas Ejecutivas S.A. de C.V." Font-Size="X-Large"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Image ID="imgAle" runat="server" Width="150px" Height="80px"  ImageUrl="~/img/colors/blue/logo-ale.png" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="text-align:center">
                                                <asp:Label ID="lblRepo" runat="server" Text="RESUMEN DE CONTRATO" Font-Size="Large"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <table style="width:100%;">
                                        <tr>
                                            <td style="width:10%"></td>
                                            <td style="text-align:left; width:10%">
                                                <asp:Label ID="lblClienteHea" runat="server" Text="Cliente:" Font-Bold="true" Font-Size="Larger"></asp:Label>
                                            </td>
                                            <td style="text-align:left; width:10%">
                                                <asp:Label ID="lblClienteResp" runat="server" Font-Size="Medium"></asp:Label>
                                            </td>
                                            <td colspan="5">
                                                <asp:Label ID="lblRazonSocial" runat="server" Font-Size="Medium"></asp:Label>
                                            </td>
                                            <td style="width:10%"></td>
                                        </tr>

                                        <tr>    
                                            <td style="width:10%"></td>
                                            <td style="width:10%">
                                                <asp:Label ID="lblContratoHea" runat="server" Font-Bold="true" Font-Size="Large" Text="Contrato:"></asp:Label>
                                            </td>
                                            <td style="width:10%">
                                                <asp:Label ID="lblContratoResp" runat="server" Font-Size="Medium"></asp:Label>
                                            </td>
                                            <td style="width:10%"></td>
                                            <td style="width:10%">
                                                <asp:Label ID="lblBase" runat="server" Font-Size="Large" Font-Bold="true" Text="Base:"></asp:Label>
                                                <asp:Label ID="lblBaseResp" runat="server" Font-Size="Medium"></asp:Label>
                                            </td>
                                            <td style="width:10%">

                                            </td>
                                            <td style="width:20%" colspan="2">
                                                <asp:Label ID="lblEqupo" runat="server" Font-Size="Large" Font-Bold="true" Text="Equipo:"></asp:Label>
                                                <asp:Label ID="lblEquipoResp" runat="server" Font-Size="Medium"></asp:Label>
                                            </td>
                                            <td style="width:10%"></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table style="width:100%">
                                        <tr>
                                            <td style="text-align:left; vertical-align:top">
                                                <asp:GridView ID="gvDatosContrato" runat="server" AutoGenerateColumns="false" CssClass="table"
                                                    style="border-top: 1px solid #484848; border-left: 1px solid #484848;border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                    ShowFooter="true">
                                                    <HeaderStyle CssClass="celda2" />
                                                    <RowStyle CssClass="celda6" Height="16px" />
                                                    <FooterStyle CssClass="celda3" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                                                        <asp:BoundField DataField="FechaContratacion" HeaderText="Detalle" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No se encontró información para mostrar
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                            <td style="width:20%">

                                            </td>
                                            <td style="width:40%;text-align:right; vertical-align:top">
                                                <asp:GridView ID="gvTarifas" runat="server" AutoGenerateColumns="false" CssClass="table"
                                                    style="border-top: 1px solid #484848; border-left: 1px solid #484848;border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                    ShowFooter="true">
                                                    <HeaderStyle CssClass="celda2" />
                                                    <RowStyle CssClass="celda6" Height="16px" />
                                                    <FooterStyle CssClass="celda3" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                                                        <asp:BoundField DataField="CostoDirectoNalV" HeaderText="Nacional" DataFormatString="{0:C}" />
                                                        <asp:BoundField DataField="CostoDirectoIntV" HeaderText="Internacional" DataFormatString="{0:C}" />
                                                        <asp:BoundField DataField="Horas" HeaderText="Horas" ItemStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No se encontró información para mostrar
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                    <br />
                                    <asp:GridView ID="gvResumen" runat="server" AutoGenerateColumns="false" CssClass="table" DataKeyNames="IdResumen"
                                        style="border-top: 1px solid #484848; border-left: 1px solid #484848;border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                        ShowFooter="true" OnRowDataBound="gvResumen_RowDataBound">
                                        <HeaderStyle CssClass="celda2" />
                                        <RowStyle CssClass="celda6" Height="16px" />
                                        <FooterStyle CssClass="celda3" />
                                        <Columns>
                                            
                                            <asp:BoundField DataField="NoPeriodo" HeaderText="Periodo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="FechaPorPeriodo" HeaderText="Fecha por Periodo" />
                                            <asp:BoundField DataField="FechaDeVuelos" HeaderText="Fecha de Vuelos" />
                                            <asp:BoundField DataField="HorasContratadas" HeaderText="Horas Contratadas" ItemStyle-HorizontalAlign="Center" />
                                            
                                            <asp:TemplateField HeaderText="Horas Trapasadas" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="upaTraspasos" runat="server">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="lkbTraspasos" runat="server" BackColor="Transparent" Text='<%# Bind("HorasTraspasadas") %>'
                                                                OnClick="lkbTraspasos_Click" ></asp:LinkButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="lkbTraspasos" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="HorasDisponibles" HeaderText="Horas por Volar" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="HorasVoladas" HeaderText="Horas Voladas" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="HorasNoVoladas" HeaderText="Horas no Voladas y/o Excedidas" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Porcentaje" HeaderText="% Acumular" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="HoraAcumular" HeaderText="Horas a Acumular" ItemStyle-HorizontalAlign="Center" />

                                            <asp:TemplateField HeaderText="Anualidades">
                                                <ItemTemplate>
                                                    <dx:ASPxTextBox ID="txtAnualidades" runat="server" Width="80px" Theme="Office2010Black"></dx:ASPxTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No. Factura">
                                                <ItemTemplate>
                                                    <dx:ASPxTextBox ID="txtNoFactura" runat="server" Width="80px" Theme="Office2010Black"></dx:ASPxTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No se encontró información para mostrar
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <br />
                                    <table style="width:40%">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvImporteContrato" runat="server" AutoGenerateColumns="false" CssClass="table"
                                                    style="border-top: 1px solid #484848; border-left: 1px solid #484848;border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                    ShowFooter="true">
                                                    <HeaderStyle CssClass="celda2" />
                                                    <RowStyle CssClass="celda6" Height="16px" />
                                                    <FooterStyle CssClass="celda3" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                                                        <asp:BoundField DataField="Importe" HeaderText="Tarifa" DataFormatString="{0:C}" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No se encontró información para mostrar
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExcel" />
                                <asp:PostBackTrigger ControlID="btnExcelR2" />
                                <asp:PostBackTrigger ControlID="btnPDF2" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <br />
                        <table style="width:100%">
                            <tr>
                                <td colspan="3" style="text-align:right">
                                    <dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" Theme="Office2010Black"></dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        
                    </div>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>


    <%--MODAL PARA VER LOS TRASPASOS DE HORAS ENTRE CONTRATOS --%>
    <dx:ASPxPopupControl ID="ppTransferencias" runat="server" ClientInstanceName="ppTransferencias" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" 
        Theme="Office2010Black" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Transferencias de Horas" 
        AllowDragging="true" ShowCloseButton="true" Width="400">
        <ClientSideEvents />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <asp:Panel ID="Panel2" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="Personal1">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"></span>
                                </legend>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <table style="width:100%">
                                            <tr>
                                                <td style="text-align: center; width:10%">
                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel2" Text="Transferencias entre Contratos" Font-Size="X-Large"></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxGridView ID="gvTransferencias" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                        ClientInstanceName="gvTransferencias" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                                        Theme="Office2010Black" Width="100%" SettingsPager-PageSize="3">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="Contrato Origen" FieldName="ContratoOrigen" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Horas Transferidad" FieldName="HorasTransferidas" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Contrato Destino" FieldName="ContratoDestino" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Fecha Transferencia" FieldName="FechaTransferencia" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsPager Position="Bottom" PageSize="3"></SettingsPager>
                                                    </dx:ASPxGridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </fieldset>

                            <dx:ASPxButton ID="btnSalirTraspasos" runat="server" Text="Salir" Theme="Office2010Black">
                                 <ClientSideEvents Click="function(s, e) {ppTransferencias.Hide(); }" />
                            </dx:ASPxButton>
                        </div>
                    </div>
                </asp:Panel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

</asp:Content>
