<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmMonitorCambios.aspx.cs" Inherits="ALE_MexJet.Views.Notificaciones.frmMonitorCambios" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 25px;">&nbsp;&nbsp;Visas Pasaportes</span>
                    </div>
                </div>
                
                <br />
                    <div class="row">
                        <div class="col-md-6">
                            
                        </div>
                        <div class="col-md-6" style="text-align: right;">
                            <dx:ASPxLabel ID="ASPxLabel1" CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                            &nbsp;<dx:ASPxButton CssClass="FBotton" ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExcel_Click"  ></dx:ASPxButton>
                        </div>
                    </div>
                <div class="well-g">
                    <br />
                    <div class="row">
                        <div class="col-md-12" >
                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                <ContentTemplate>
                                    <div class="col-sm-12">
                                        <dx:ASPxGridView ID="gvMonitorNotificaciones" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvMonitorNotificaciones" EnableTheming="True" KeyFieldName="IdNotificacion"
                                        Styles-Header-HorizontalAlign="Center" Theme="Office2010Black" Width="100%" OnRowCommand="gvMonitorNotificaciones_RowCommand">
                                        <Columns>
                                            <dx:GridViewDataColumn Caption="Pax Code" FieldName="PaxCode" VisibleIndex="0" Visible="true">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Pasajero" FieldName="NombrePasajero" VisibleIndex="1" Visible="true">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Trip" FieldName="Trip" VisibleIndex="2" Visible="true">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Contrato" FieldName="Contrato" VisibleIndex="3" Visible="true">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Tipo" FieldName="Tipo" VisibleIndex="3" Visible="true">
                                            <DataItemTemplate>
                                                <%# Eval("Tipo").ToString() == "1" ? 
                                                              "Cambio"
                                                              : (Eval("Tipo").ToString() == "2" ?
                                                                "Alta"
                                                                : "Baja")
                                                              
                                                         %>
                           
                                            </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Acciones">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <CellStyle HorizontalAlign="Center"></CellStyle>
                                                <DataItemTemplate>
                                                    <dx:ASPxButton Text="Atendido" Theme="Office2010Black" ID="btnAtendido" runat="server" CommandArgument='<%# Eval("IdNotificacion") %>' CommandName="Atendido" AutoPostBack="true" ToolTip="Marcar como Atendido">
                                                    </dx:ASPxButton>                                                     
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <Templates>
                                            <DetailRow>
                                                <dx:ASPxGridView CssClass="FGrid" ID="gvCambios" ClientInstanceName="gvCambios" runat="server" Visible='<%# Eval("Tipo").ToString() == "1" %>'
                                                    KeyFieldName="idPassenger" Width="80%" Theme="Office2010Black" OnBeforePerformDataSelect="gvCambios_BeforePerformDataSelect">
                                                    <Columns>
                                                            
                                                        <dx:GridViewDataColumn Caption="Cambios encontrados" VisibleIndex="1">
                                                            <DataItemTemplate>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width:250px; background-color: #333; color: #fff"></td>
                                                                    <td style="background-color: #333; color: #fff">En Pasaporte</td>
                                                                    <td style="background-color: #333; color: #fff">En Base de Datos</td>
                                                                    <td style="width:100px; background-color: #333; color: #fff">Cambio</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Nombre</td>
                                                                    <td><%# Eval("Ps_First_Name") %></td>
                                                                    <td><%# Eval("Pa_First_Name") %></td>
                                                                    <td style="text-align: center"><%# Eval("Pa_First_Name") != Eval("Ps_First_Name") ? "<span style='color:red'>Sí</span>" : "No" %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Apellidos</td>
                                                                    <td><%# Eval("Ps_Last_Name") %></td>
                                                                    <td><%# Eval("Pa_Last_Name") %></td>
                                                                    <td style="text-align: center"><%# Eval("Pa_Last_Name") != Eval("Ps_Last_Name") ? "<span style='color:red'>Sí</span>" : "No" %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Fecha de Nacimiento</td>
                                                                    <td><%# Eval("Ps_Birthday") %></td>
                                                                    <td><%# Eval("Pa_Birthday") %></td>
                                                                    <td style="text-align: center"><%# Eval("Pa_Birthday") != Eval("Ps_Birthday") ? "<span style='color:red'>Sí</span>" : "No" %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Expiración</td>
                                                                    <td><%# Eval("Ps_Expiration") %></td>
                                                                    <td><%# Eval("Pa_Expiration") %></td>
                                                                    <td style="text-align: center"><%# Eval("Pa_Expiration") != Eval("Ps_Expiration") ? "<span style='color:red'>Sí</span>" : "No" %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Nacionalidad</td>
                                                                    <td><%# Eval("Ps_Nationality") %></td>
                                                                    <td><%# Eval("Pa_Nationality") %></td>
                                                                    <td style="text-align: center"><%# Eval("Pa_Nationality") != Eval("Ps_Nationality") ? "<span style='color:red'>Sí</span>" : "No" %></td>
                                                                </tr>
                                                            </table>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>                                                               
                                                            
                                                    </Columns>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridView CssClass="FGrid" ID="ASPxGridView1" ClientInstanceName="gvCambios2" runat="server" Visible='<%# Eval("Tipo").ToString() == "2" %>'
                                                    KeyFieldName="idPassenger" Width="50%" Theme="Office2010Black" OnBeforePerformDataSelect="gvCambios2_BeforePerformDataSelect">
                                                    <Columns>
                                                            
                                                        <dx:GridViewDataColumn Caption="Ident" FieldName="Ps_Identification" VisibleIndex="1" />
                                                        <dx:GridViewDataColumn Caption="Nombre" FieldName="Ps_First_Name" VisibleIndex="2" />
                                                        <dx:GridViewDataColumn Caption="Apellidos" FieldName="Ps_Last_Name" VisibleIndex="3" />  
                                                        <dx:GridViewDataColumn Caption="Fecha Nacimiento" FieldName="Ps_Birthday" VisibleIndex="4" />   
                                                        <dx:GridViewDataColumn Caption="Expiracion" FieldName="Ps_Expiration" VisibleIndex="5" />
                                                        <dx:GridViewDataColumn Caption="Nacionalidad" FieldName="Ps_Nationality" VisibleIndex="6" />                                                                
                                                            
                                                    </Columns>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridView CssClass="FGrid" ID="ASPxGridView2" ClientInstanceName="gvCambios3" runat="server" Visible='<%# Eval("Tipo").ToString() == "3" %>'
                                                    KeyFieldName="idPassenger" Width="50%" Theme="Office2010Black" OnBeforePerformDataSelect="gvCambios3_BeforePerformDataSelect">
                                                    <Columns>
                                                            
                                                        <dx:GridViewDataColumn Caption="Ident" FieldName="Pa_Identification" VisibleIndex="1" />
                                                        <dx:GridViewDataColumn Caption="Nombre" FieldName="Pa_First_Name" VisibleIndex="2" />
                                                        <dx:GridViewDataColumn Caption="Apellidos" FieldName="Pa_Last_Name" VisibleIndex="3" />  
                                                        <dx:GridViewDataColumn Caption="Fecha Nacimiento" FieldName="Pa_Birthday" VisibleIndex="4" />   
                                                        <dx:GridViewDataColumn Caption="Expiracion" FieldName="Pa_Expiration" VisibleIndex="5" />
                                                        <dx:GridViewDataColumn Caption="Nacionalidad" FieldName="Pa_Nationality" VisibleIndex="6" />                                                                
                                                            
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </DetailRow>
                                        </Templates>
                                        <StylesPager Pager-CssClass="FNumPag"></StylesPager>
                                        <SettingsDetail ShowDetailRow="true" />
                                        <SettingsPager Position="TopAndBottom">
                                            <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                            </PageSizeItemSettings>
                                        </SettingsPager>
                                    </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvMonitorNotificaciones" ></dx:ASPxGridViewExporter>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnExcel" />
                                    <asp:PostBackTrigger ControlID="btnExportar" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col-sm-6">
                           
                        </div>
                        <div class="col-sm-6" style="text-align: right;">
                            <dx:ASPxLabel CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                            &nbsp;<dx:ASPxButton CssClass="FBotton" ID="btnExportar" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExcel_Click" ></dx:ASPxButton>
                        </div>
                    </div>
                    <br />

                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

</asp:Content>
