<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmTableroFerry.aspx.cs" EnableEventValidation="false" Inherits="ALE_MexJet.Views.JetSmart.frmTableroFerry" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="pnlPrincipal" runat="server" Width="100%" BackColor="White" style="border-radius: 14px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Tablero de Control JetSmart</span>
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
                                                        <td style="width:10%">
                                                        </td>
                                                        <td style="width:80%" colspan="3">
                                                            <dx:ASPxLabel Theme="Office2010Black" runat="server" Text="Rango de Fechas" Font-Bold="True" Font-Size="Small"></dx:ASPxLabel>
                                                        </td>
                                                        <td style="width:10%"></td>
                                                    </tr>
                                                    <tr style="height: 10px;">
                                                        <td colspan="5"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:10%;">
                                                        </td>
                                                        <td style="width:38%;">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div style="float:right">
                                                                <dx:ASPxDateEdit HeaderText="Desde:" ID="dFechaIni" ClientInstanceName="dFechaIni" NullText="Fecha Inicial" runat="server" ToolTip="Fecha Inicial" Theme="Office2010Black" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Es necesario fecha inicial">
                                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="Text" ValidationGroup="Grupo1">
                                                                        <RequiredField IsRequired="True" ErrorText="Es necesario fecha inicial"></RequiredField>
                                                                    </ValidationSettings>
                                                                </dx:ASPxDateEdit>
                                                            </div>
                                                        </td>
                                                        <td style="width:4%"></td>
                                                        <td style="width:38%;">
                                                            <div style="float:left">
                                                                <dx:ASPxDateEdit HeaderText="Hasta:" ID="dFechaFin" ClientInstanceName="dFechaFin" runat="server" Theme="Office2010Black" ToolTip="Fecha Final" NullText="Fecha Final" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Es necesario fecha final">
                                                                    <DateRangeSettings StartDateEditID="dFechaIni"></DateRangeSettings>
                                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="Text" ValidationGroup="Grupo1">
                                                                        <RequiredField IsRequired="True" ErrorText="Es necesario fecha final"></RequiredField>
                                                                    </ValidationSettings>
                                                                </dx:ASPxDateEdit>
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td style="width:10%;">
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 10px;">
                                                        <td colspan="5"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <dx:ASPxButton ID="btnBusqueda" Text="Buscar" runat="server" Theme="Office2010Black" OnClick="btnBusqueda_Click" ValidationGroup="Grupo1">
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
                                    <div class="col-md-6">
                                        
                                    </div>
                                    <div class="col-md-6" style="text-align: right;">
                                        <dx:ASPxLabel runat="server" Theme="Aqua" Text="Exportar a:"></dx:ASPxLabel>
                                        &nbsp;<dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black"></dx:ASPxButton>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="margin-left: 50px; width: 90%; text-align:center;">

                                        <div class="col-sm-12">

                                            <asp:GridView ID="gvFerrys" runat="server" AutoGenerateColumns="false" CssClass="table"
                                                style="border-top: 1px solid #484848; border-left: 1px solid #484848;border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                DataKeyNames="IdFerry" ShowFooter="true" OnRowDataBound="gvFerrys_RowDataBound"
                                                OnRowCommand="gvFerrys_RowCommand">
                                                <HeaderStyle CssClass="celda2" />
                                                <RowStyle CssClass="celda6" Height="16px" Font-Size="Small" />
                                                <FooterStyle CssClass="celda3" />
                                                <Columns>
                                                    <asp:BoundField DataField="Trip" HeaderText="Trip" />
                                                    <asp:BoundField DataField="NoPierna" HeaderText="No. Pierna" /> 
                                                    <asp:BoundField DataField="Origen" HeaderText="Origen" />
                                                    <asp:BoundField DataField="FechaSalida" HeaderText="Fecha Salida" />
                                                    <asp:BoundField DataField="Destino" HeaderText="Destino" />
                                                    <asp:BoundField DataField="FechaLlegada" HeaderText="Fecha Llegada" />
                                                    <asp:BoundField DataField="Matricula" HeaderText="Matrícula" />
                                                    <asp:BoundField DataField="GrupoModelo" HeaderText="Grupo Modelo" />

                                                    <asp:TemplateField HeaderText="JetSmarter">
                                                        <ItemTemplate>
                                                            <dx:ASPxComboBox ID="ddlSmart" runat="server" Theme="Office2010Black" Width="100px">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Nueva" Value="1" />
                                                                    <dx:ListEditItem Text="Pendiente" Value="2" />
                                                                    <dx:ListEditItem Text="Enviada" Value="3" />
                                                                    <dx:ListEditItem Text="Vendida" Value="4" />
                                                                    <dx:ListEditItem Text="Rechazada" Value="5" />
                                                                    <dx:ListEditItem Text="Cancelada" Value="0" />
                                                                    <dx:ListEditItem Text="Alerta" Value="6" />
                                                                </Items>
                                                            </dx:ASPxComboBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="App MexJet">
                                                        <ItemTemplate>
                                                            <dx:ASPxComboBox ID="ddlApp" runat="server" Theme="Office2010Black" Width="100px">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Nueva" Value="1" />
                                                                    <dx:ListEditItem Text="Pendiente" Value="2" />
                                                                    <dx:ListEditItem Text="Enviada" Value="3" />
                                                                    <dx:ListEditItem Text="Vendida" Value="4" />
                                                                    <dx:ListEditItem Text="Rechazada" Value="5" />
                                                                    <dx:ListEditItem Text="Cancelada" Value="0" />
                                                                </Items>
                                                            </dx:ASPxComboBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="EzMexJet">
                                                        <ItemTemplate>
                                                            <dx:ASPxComboBox ID="ddlEz" runat="server" Theme="Office2010Black" Width="100px">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Nueva" Value="1" />
                                                                    <dx:ListEditItem Text="Pendiente" Value="2" />
                                                                    <dx:ListEditItem Text="Enviada" Value="3" />
                                                                    <dx:ListEditItem Text="Vendida" Value="4" />
                                                                    <dx:ListEditItem Text="Rechazada" Value="5" />
                                                                    <dx:ListEditItem Text="Cancelada" Value="0" />
                                                                </Items>
                                                            </dx:ASPxComboBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Acciones">
                                                        <ItemTemplate>
                                                            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" Theme="Office2010Black"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Guardar"></dx:ASPxButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <dx:ASPxLabel ID="lblFerrysVacios" runat="server" Text="No se encontraron registros para mostrar." Theme="Office2010Black"></dx:ASPxLabel>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>
