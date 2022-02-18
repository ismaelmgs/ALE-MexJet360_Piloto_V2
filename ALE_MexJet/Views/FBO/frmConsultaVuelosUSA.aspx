<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmConsultaVuelosUSA.aspx.cs" 
    Inherits="ALE_MexJet.Views.FBO.frmConsultaVuelosUSA" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%--<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Src="~/ControlesUsuario/ucClienteContrato.ascx" TagPrefix="uc2" TagName="ucContratos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        th {
            text-align: center;
        }
        td, th {
            padding: 5px;
        }
        .gvTable {
            background-color: White;
            border-collapse: separate!important;
            overflow: hidden;
            border-radius: 3px 4px;
        }
        .gvHeader {
            cursor: pointer;
            border: 1px solid #484848;
            background: #6d6c6e url(/DXR.axd?r=0_3538-fCYSi) repeat-x left top;
            overflow: hidden;
            font-weight: normal;
            color: #ffffff;
            text-align: center;
            font: 11px Verdana, Geneva, sans-serif;
            border-top-width:0px;border-left-width:0px;
        }

        .gvRows {
            overflow: hidden;
            border-bottom: 1px solid #d2d2d2;
            border-right: 1px solid #d2d2d2;
            border-top-width: 0;
            border-left-width: 0;
            font: 11px Verdana, Geneva, sans-serif;
            padding: 10px 6px;
        }

        .btnDefault {
	        color: #3c3c3c;
	        font: 11px Verdana, Geneva, sans-serif;
	        border: 1px solid #6a6a6a;
	        background: #a09fa0 url('/DXR.axd?r=0_3507-fCYSi') repeat-x left top;
	        padding: 1px;
        }

        .btnDefault:hover {
	        color: #3c3c3c;
	        background: #fcf8e5 url('/DXR.axd?r=0_3508-fCYSi') repeat-x left top;
	        border: 1px solid #eac656;
        }
    </style>
                
    <asp:UpdatePanel ID="upaGeneral" runat="server" style="border-radius: 21px;">
        <ContentTemplate>
            <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />

                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 25px;">&nbsp;&nbsp;Consulta Vuelos</span>
                    </div>
                </div>

                    <div class="row">
                    <div class="col-md-12">
                        <fieldset class="Personal">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                            </legend>

                            <div class="col-sm-12">
                                <table width="99%" border="0">
                                    <tr>
                                        <td align="right" width="50%">
                                            <asp:TextBox ID="txtBusqueda" runat="server" CssClass="dxeEditArea_Office2010Black" type="date" placeholder="Ingrese la información a buscar" Width="300px" Height="29px" style="border-radius: 3px 3px;"></asp:TextBox>
                                        </td>
                                        <td align="left" width="50%" style="padding-left:20px;">
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btnDefault" OnClick="btnBuscar_Click" Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-12" align="center" style="padding-bottom:50px; padding-top:50px;">
                                <asp:UpdatePanel ID="upaResultadoVuelos" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlVuelos" runat="server">

                                            <div style="overflow-y:auto; max-height:400px; padding-bottom: 50px; width: 98%; margin: 0 auto 0 auto;">
                                                <asp:GridView ID="gvVuelosUSA" AutoGenerateColumns="false" runat="server" Width="100%" 
                                                    CssClass="gvTable" Font-Size="Small">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <label>Sel.</label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkVuelo" runat="server"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="numerovuelo" HeaderText="Num. Vuelo" ControlStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="estado" HeaderText="Estado" ControlStyle-Width="10%" />
                                                        <asp:BoundField DataField="ciudadorigen" HeaderText="Ciudad Origen" ControlStyle-Width="10%" />
                                                        <asp:BoundField DataField="ciudaddestino" HeaderText="Ciudad Destino" ControlStyle-Width="10%" />
                                                        <asp:BoundField DataField="origen" HeaderText="Origen" ControlStyle-Width="5%" />
                                                        <asp:BoundField DataField="destino" HeaderText="Destino" ControlStyle-Width="5%" />
                                                        <asp:BoundField DataField="origencalzo" HeaderText="Origen Calzo" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="5%" />
                                                        <asp:BoundField DataField="destinocalzo" HeaderText="Destino Calzo" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="5%" />
                                                        <asp:BoundField DataField="cantpax" HeaderText="Pasajeros" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="5%" />

                                                        <asp:BoundField DataField="matricula" HeaderText="Matrícula" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="5%" />
                                                        <asp:BoundField DataField="tipoavion" HeaderText="Tipo Avión" ControlStyle-Width="20%" />
                                                        <asp:BoundField DataField="piloto" HeaderText="Piloto" ControlStyle-Width="10%" />
                                                        <asp:BoundField DataField="contrato" HeaderText="Contrato" ControlStyle-Width="5%" />
                                                    </Columns>
                                                    <HeaderStyle CssClass="gvHeader" />
                                                    <%--<AlternatingRowStyle CssClass="gvRows" />--%>
                                                    <RowStyle CssClass="gvRows" />
                                                    <FooterStyle CssClass="gvFooter" />
                                                    <PagerStyle CssClass="gvFooter" />
                                                    <EmptyDataTemplate>
                                                        Sin Vuelos.
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>

                                            <div class="col-md-12" align="center" style="padding-top:30px;">
                                                <asp:Button ID="btnGenerarReporte" runat="server" Text="Generar Reporte" CssClass="btnDefault" OnClick="btnGenerarReporte_Click" Width="120px" Visible="false" />
                                                <CR:CrystalReportViewer ID="crv" runat="server" AutoDataBind="true" />
                                            </div>

                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnGenerarReporte" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            
                            

                        </fieldset>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
