<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmServicioCargo.aspx.cs" Inherits="ALE_MexJet.Views.Consultas.frmServicioCargo" EnableEventValidation="false" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style lang="cs" type="text/css">
        .FooterX {
            border-top:solid;
            border-width:1px;
        }

        .HeaderX {
            
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Servicios con Cargo</span>
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
                                    <table style="width: 100%;" border="0">
                                        <tr>
                                            <td width="2%">&nbsp;
                                            </td>
                                            <td style="text-align: left" width="23%">
                                                <dx:ASPxLabel runat="server" Text="Cliente:"></dx:ASPxLabel>
                                            </td>
                                            <td width="23%">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel2" OnUnload="UpdatePanel1_Unload">
                                                    <ContentTemplate>
                                                        <dx:ASPxComboBox ToolTip="Cliente" ID="ddlClientes" runat="server" Theme="Office2010Black" EnableTheming="True" NullText="Seleccione un Cliente" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged" AutoPostBack="true">
                                                            <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="true" ErrorText="Seleccione un Cliente" />
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td width="4%">&nbsp;
                                            </td>
                                            <td colspan="2" width="46%">
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
                                            <td style="text-align: left" width="23%">
                                                <dx:ASPxLabel runat="server" Text="Contrato:"></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 25%;" width="23%">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" OnUnload="UpdatePanel1_Unload" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <dx:ASPxComboBox ToolTip="Contrato" ID="ddlContrato" NullText="Seleccione un Contrato" runat="server" Theme="Office2010Black" EnableTheming="True">
                                                            <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="true" ErrorText="Seleccione un Contrato" />
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlClientes" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td width="4%">&nbsp;
                                            </td>
                                            <td style="text-align: center" width="23%">
                                                <dx:ASPxDateEdit Caption="Desde:" ID="dFechaIni" ClientInstanceName="dFechaIni" NullText="Fecha Inicial" runat="server" ToolTip="Fecha Inicial" Theme="Office2010Black">
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="Text" RequiredField-IsRequired="true" ErrorTextPosition="Bottom">
														<RequiredField IsRequired="True" ErrorText="Elija una Fecha"></RequiredField>
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td style="text-align: center" width="23%">
                                                <dx:ASPxDateEdit Caption="Hasta:" ID="dFechaFin" ClientInstanceName="dFechaFin" runat="server" Theme="Office2010Black" ToolTip="Fecha Final" NullText="Fecha Final">
                                                    <DateRangeSettings StartDateEditID="dFechaIni"></DateRangeSettings>
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="Text" RequiredField-IsRequired="true" ErrorTextPosition="Bottom">
														<RequiredField IsRequired="True" ErrorText="Elija una Fecha"></RequiredField>
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
                                            <td style="text-align: left" width="23%"></td>
                                            <td></td>
                                            <td width="4%">&nbsp;
                                            </td>
                                            <td colspan="2" style="text-align: center" width="46%">
                                                <dx:ASPxButton ID="btnBusqueda" Text="Buscar" runat="server" Theme="Office2010Black" OnClick="btnBusqueda_Click"></dx:ASPxButton>
                                            </td>
                                            <td width="2%">&nbsp;
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
                            <asp:UpdatePanel runat="server" ID="upGv" OnUnload="UpdatePanel1_Unload" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlServiciosC" runat="server" ScrollBars="Horizontal" Width="100%">
                                        <table style="width:1830px">
                                            <tr>
                                                <td style="width:10%"  rowspan="3">
                                                    <asp:Image ID="imgMexJet" runat="server" Width="150px" Height="80px"  ImageUrl="~/img/mexjet_p.png" />
                                                </td>
                                                <td style="width:80%; text-align:center" colspan="15">
                                                    <asp:Label ID="lblAle" runat="server" Text="Aerolíneas Ejecutivas S.A. de C.V." Font-Size="X-Large"></asp:Label>
                                                </td>
                                                <td style="width:10%" rowspan="3">
                                                    <asp:Image ID="imgAle" runat="server" Width="150px" Height="80px"  ImageUrl="~/img/colors/blue/logo-ale.png" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center" colspan="15">
                                                    <asp:Label ID="lblRepo" runat="server" Text="Reporte de Servicios con Cargo" Font-Size="Large"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center" colspan="15">
                                                    <asp:Label ID="lblPer" runat="server" Text="" Font-Size="Small"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="15"></td>
                                                <td>
                                                    <asp:Label ID="lblFechaRep" runat="server" Font-Size="Small"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="17" style="text-align: left">
                                                    <asp:Label ID="lblClienteR" runat="server" Text="" Font-Size="Small"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="17" style="text-align: left">
                                                    <asp:Label ID="lblContratoR" runat="server" Text="" Font-Size="Small"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="15"></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                        <asp:GridView ID="gvServiciosC" runat="server" ShowFooter="true" GridLines="None" Width="1830px" 
                                            BackColor="White" AutoGenerateColumns="false" OnRowDataBound="gvServiciosC_RowDataBound">
                                            <HeaderStyle Font-Bold="true" Font-Names="Times New Roman" Font-Size="9pt" HorizontalAlign="Center" CssClass="HeaderX" />
                                            <RowStyle Font-Names="Times New Roman" Font-Size="9pt" HorizontalAlign="Center" />
                                            <FooterStyle Font-Bold="true" Font-Names="Times New Roman" CssClass="HeaderX" Font-Size="9pt" HorizontalAlign="Right" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:80px; text-align:left; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblFechaT" runat="server" Text="FECHA" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:80px; text-align:left">
                                                            <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("FECHA")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:80px; text-align:left; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblRemisionT" runat="server" Text="REMISION" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:80px; text-align:left">
                                                            <asp:Label ID="lblRemision" runat="server" Text='<%# Eval("REMISION")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:100px; text-align:left; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblMatriculaT" runat="server" Text="MATRICULA" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:100px; text-align:left">
                                                            <asp:Label ID="lblMatricula" runat="server" Text='<%# Eval("MATRICULA")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:250px; text-align:left; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblRutaT" runat="server" Text="RUTA" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:250px; text-align:left">
                                                            <asp:Label ID="lblRuta" runat="server" Text='<%# Eval("RUTA")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:80px; text-align:right; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblDsmT" runat="server" Text="DSM" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:80px; text-align:right">
                                                            <asp:Label ID="lblDsm" runat="server" Text='<%# Eval("DSM")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:90px; text-align:right; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblSerMigT" runat="server" Text="SERV. MIGR" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:90px; text-align:right">
                                                            <asp:Label ID="lblServMig" runat="server" Text='<%# Eval("MIGRATORIO")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:80px; text-align:right; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblTuaIntT" runat="server" Text="TUA INT." Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:80px; text-align:right">
                                                            <asp:Label ID="lblTuaIntT" runat="server" Text='<%# Eval("TUAINT")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:80px; text-align:right; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblTuaNalT" runat="server" Text="TUA NAC." Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:80px; text-align:right">
                                                            <asp:Label ID="lblTuaNac" runat="server" Text='<%# Eval("TUANAC")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:80px; text-align:right; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblSubtotal" runat="server" Text="APHIS" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:80px; text-align:right">
                                                            <asp:Label ID="lblAphis" runat="server" Text='<%# Eval("APHIS")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:80px; text-align:right; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblSubtotal" runat="server" Text="SENEAM" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:80px; text-align:right">
                                                            <asp:Label ID="lblSeneam" runat="server" Text='<%# Eval("SENEAM")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:110px; text-align:right; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblSubtotal" runat="server" Text="COMISARIATO" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:110px; text-align:right">
                                                            <asp:Label ID="lblComi" runat="server" Text='<%# Eval("COMISARIATO")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:110px; text-align:right; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblSubtotal" runat="server" Text="ATERRIZAJE" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:110px; text-align:right">
                                                            <asp:Label ID="lblAte" runat="server" Text='<%# Eval("ATERRIZAJE")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:200px; text-align:right; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblSubtotal" runat="server" Text="OTROS" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:200px; text-align:right">
                                                            <asp:Label ID="lblOtros" runat="server" Text='<%# Eval("OTROS")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:110px; text-align:right; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblSubtotal" runat="server" Text="INTEGRACION" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:110px; text-align:right">
                                                            <asp:Label ID="lblIntegracion" runat="server" Text='<%# Eval("INTEGRACION")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:110px; text-align:right; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblSubtotal" runat="server" Text="SUBTOTAL" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:100px; text-align:right;">
                                                            <asp:Label ID="lblSubtotal" runat="server" Text='<%# Eval("SUBTOTAL")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:100px; text-align:right; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblIva" runat="server" Text="IVA" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:100px; text-align:right">
                                                            <asp:Label ID="lblIva" runat="server" Text='<%# Eval("IVA")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div style="width:100px; text-align:right; border-top:solid; border-bottom:solid; border-width:1px">
                                                            <asp:Label ID="lblIva" runat="server" Text="TOTAL" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="width:100px; text-align:right">
                                                            <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("TOTAL")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                    
                                    <%--<div id="dvServiciosC" runat="server" style="width:2000px"></div>
                                    
                                    <div>
                                        <dx:ASPxGridView ID="gvServicio" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvServicio" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign="Center"
                                            Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="FECHA" Caption="FECHA" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="IdRemision" Caption="REMISIÓN" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AeronaveMatricula" Caption="MATRICULA" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Ruta" Caption="RUTA" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CantPax" Caption="PASAJEROS" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="DMS" Caption="D.S.M." VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="N2" >
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="MIGRATORIO" Caption="SERV. MIGR" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="N2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TUAINT" Caption="TUA INT" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="N2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TUANAC" Caption="TUA NAC" VisibleIndex="9" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="N2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="APHIS" Caption="APHIS" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="N2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SENEAM" Caption="SENEAM" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="N2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="COMISARIATO" Caption="COMISARIATO" VisibleIndex="12" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="N2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ATERRIZAJE" Caption="ATERRIZAJE" VisibleIndex="13" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="N2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="OTROS" Caption="OTROS" VisibleIndex="14" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="N2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="INTEGRACION" Caption="INTEGRACIÓN" VisibleIndex="15" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="N2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Subtotal" Caption="SUBTOTAL" VisibleIndex="16" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="N2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="IVA" Caption="IVA" VisibleIndex="17" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="N2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Total" Caption="TOTAL" VisibleIndex="18" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="N2">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings HorizontalScrollBarMode="Auto" />
                                            <SettingsSearchPanel Visible="true" />
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                            <Settings ShowFooter="True" />
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem FieldName="CantPax" SummaryType="Sum"  DisplayFormat="#" ValueDisplayFormat="N2"/>
                                                <dx:ASPxSummaryItem FieldName="DMS" DisplayFormat="N2" SummaryType="Sum" ValueDisplayFormat="N2"/>
                                                <dx:ASPxSummaryItem FieldName="MIGRATORIO" DisplayFormat="N2" SummaryType="Sum" ValueDisplayFormat="N2"/>
                                                <dx:ASPxSummaryItem FieldName="TUAINT" DisplayFormat="N2" SummaryType="Sum" ValueDisplayFormat="N2" />
                                                <dx:ASPxSummaryItem FieldName="TUANAC" DisplayFormat="N2" SummaryType="Sum" ValueDisplayFormat="N2"/>
                                                <dx:ASPxSummaryItem FieldName="APHIS" DisplayFormat="N2" SummaryType="Sum" ValueDisplayFormat="N2"/>
                                                <dx:ASPxSummaryItem FieldName="SENEAM" DisplayFormat="N2" SummaryType="Sum" ValueDisplayFormat="N2"/>
                                                <dx:ASPxSummaryItem FieldName="COMISARIATO" DisplayFormat="N2" SummaryType="Sum" ValueDisplayFormat="N2"/>
                                                <dx:ASPxSummaryItem FieldName="ATERRIZAJE" DisplayFormat="N2" SummaryType="Sum" ValueDisplayFormat="N2"/>
                                                <dx:ASPxSummaryItem FieldName="OTROS" DisplayFormat="N2" SummaryType="Sum" ValueDisplayFormat="N2"/>
                                                <dx:ASPxSummaryItem FieldName="INTEGRACION" DisplayFormat="N2" SummaryType="Sum" ValueDisplayFormat="N2"/>
                                                <dx:ASPxSummaryItem FieldName="Subtotal" DisplayFormat="N2" SummaryType="Sum" ValueDisplayFormat="N2"/>
                                                <dx:ASPxSummaryItem FieldName="IVA" DisplayFormat="N2" SummaryType="Sum" ValueDisplayFormat="N2"/>
                                                <dx:ASPxSummaryItem FieldName="Total" DisplayFormat="N2" SummaryType="Sum" ValueDisplayFormat="N2"/>
                                            </TotalSummary>
                                        </dx:ASPxGridView>
                                    </div>--%>
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
                                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional" OnUnload="UpdatePanel1_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a: "></dx:ASPxLabel>
                                        &nbsp;<dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" ToolTip="Exportar a Excel" OnClick="btnExcel_Click"></dx:ASPxButton>
                                        &nbsp;<dx:ASPxButton ID="btnPDF" runat="server" Text="PDF" Theme="Office2010Black" ToolTip="Exportar a PDF" OnClick="btnPDF_Click"></dx:ASPxButton>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExcel" />
                                        <asp:PostBackTrigger ControlID="btnPDF" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel3" OnUnload="UpdatePanel1_Unload">
                    <ContentTemplate>
                        <dx:ASPxHiddenField ID="hfFolio" runat="server" />
                        <dx:ASPxHiddenField ID="hfCliente" runat="server" />
                        <dx:ASPxHiddenField ID="hfContrato" runat="server" />
                        <dx:ASPxHiddenField ID="hfFechaInicial" runat="server" />
                        <dx:ASPxHiddenField ID="hfFechaFinal" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>
