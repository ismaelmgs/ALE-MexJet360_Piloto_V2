<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmAudit.aspx.cs" Inherits="ALE_MexJet.Views.Consultas.frmAudit" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="row header1">
            <div class="col-md-12">
                <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Audit</span>
            </div>
        </div>
    <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
    <br />
    <dx:ASPxPageControl ID="pcPaginaControl"  ActiveTabIndex="0" runat="server" Width="100%" Height="350px" TabAlign="Justify" EnableTabScrolling="true">
        <TabStyle Paddings-PaddingLeft="50px" Paddings-PaddingRight="50px" />
        <ContentStyle>
            <Paddings PaddingLeft="12px" />
        </ContentStyle>

        <%--inicia tab 01--%>
         <TabPages>
            <dx:TabPage Text ="Finanzas" Enabled ="true">
                <ContentCollection>
                    <dx:ContentControl>
                         <fieldset class="Personal1" style="min-height: 350px;">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Indicadores</span>
                            </legend>

                             <div class="row">
                                <div class="col-md-12">
                                    <fieldset class="Personal">
                                        <legend>
                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                                        </legend>
                                        
                                        <div class="col-sm-12">

                                        <table border="0" style="width:80%">
                                             <tr>
                                                 <td>
                                                     <dx:ASPxDateEdit ID="deFechaInicialFinanza" Caption="Desde" runat="server" Theme="Office2010Black" NullText="Seleccione una opción" >
                                                         <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                             <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                         </ValidationSettings>
                                                     </dx:ASPxDateEdit>
                                                 </td>
                                                 <td>
                                                     <dx:ASPxDateEdit ID="deFechaFinalFinanza" Caption="Hasta" runat="server" Theme="Office2010Black" NullText="Seleccione una opción" >
                                                         <DateRangeSettings StartDateEditID="deFechaInicialFinanza"></DateRangeSettings>
                                                         <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                             <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                         </ValidationSettings>
                                                     </dx:ASPxDateEdit>
                                                 </td>                                                                          
                                                 <td>&nbsp;</td> 
                                                 <td>
                                                    <dx:ASPxButton ID="btnBuscarIndicadorFinanza" runat="server" Text="Buscar" Theme="Office2010Black" OnClick="btnBuscarIndicadorFinanza_Click"></dx:ASPxButton>
                                                 </td>                                    
                                             </tr>                                 
                                        </table>
                                        <br />                                     
                                        <br />                                                  
                                        </div>

                                    </fieldset>
                                </div>
                            </div>
                                                           
                             <%--Indicadores Finanzas--%>   
                             <div class="row">
                                <div class="col-md-12">
                                    <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="gvIndicadores" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                ClientInstanceName="gvIndicadores" EnableTheming="True" Styles-Header-HorizontalAlign ="Left"
                                                Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center" OnRowCommand="gvIndicadores_RowCommand" >
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="" FieldName="Indicadores" VisibleIndex="0" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                 <dx:GridViewBandColumn Caption="Vencido" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                                                     <Columns>
                                                            <dx:GridViewDataColumn Caption="0 - 10 Dias" FieldName="Vencido0a10Dias" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                            <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                     </Columns>
                                                 </dx:GridViewBandColumn>
                                                 <dx:GridViewBandColumn Caption="No Vencido" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                                                     <Columns>
                                                            <dx:GridViewDataColumn Caption="11 - 20 Dias" FieldName="NoVencido11a20Dias" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                                                                <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn Caption="+ 21 Dias" FieldName="NoVencidoMas21Dias" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                                                                <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                     </Columns>
                                                 </dx:GridViewBandColumn>
                                                
                                                
                                                <dx:GridViewDataColumn Caption="Canceladas" FieldName="Canceladas" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Generadas" FieldName="Generadas" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn Caption="Acciones" Width="400px" CellStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <DataItemTemplate>
                                                            <dx:ASPxButton Text="Detalle" Theme="Office2010Black" ID="btnDetalle" runat="server" CommandArgument='<%# Eval("IdDetalle") %>' CommandName="Detalle" AutoPostBack="true" ToolTip="Detalle">
                                                            </dx:ASPxButton>
                                                        </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowGroupPanel="True" ShowFooter="True" />
                                            <SettingsPager Position="TopAndBottom" Visible="True" PageSize="20">
                                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                    </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="true" />
                                           
                                            <TotalSummary>
                                                
                                                <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                            </TotalSummary>
                                            <Styles>
                                                <GroupRow HorizontalAlign="Left" />
                                            </Styles>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvUsuarioAudit">
                                        </dx:ASPxGridViewExporter>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportar" />
                                    </Triggers>
                                    </asp:UpdatePanel>      
                                </div>
                                 
                             </div>
                             <br />
                            
                             <div class="row">
                                 <div class =" col-md-12">
                                    <div class="col-sm-12">

                                        <%-- Condonaciones y Descuentos--%>

                                        <div class="col-lg-6" style="color:black; font-size:18px; ">
                                            <span >&nbsp;&nbsp;Condonaciones y Descuentos</span>
                                            <br />

                                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="gvCondonaciones" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                        ClientInstanceName="gvCondonaciones" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                                        Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center" OnRowCommand="gvCondonaciones_RowCommand" >
                                                    <Columns>
                                                        <dx:GridViewDataColumn Caption="Tipo" FieldName="Tipo" VisibleIndex="0" HeaderStyle-HorizontalAlign="Left">
                                                            <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                        </dx:GridViewDataColumn>
                                                        
                                                        <dx:GridViewDataTextColumn Caption="Importe" FieldName="Importe" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">                                                            
                                                            <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit>                                                                                                             
                                                            <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataColumn Caption="Contratos" FieldName="Contratos" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                                                            <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                        </dx:GridViewDataColumn>   
                                                        
                                                        <dx:GridViewDataColumn Caption="Acciones" CellStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <DataItemTemplate>
                                                                <dx:ASPxButton Text="Detalle" Theme="Office2010Black" ID="btnDetalle" runat="server" CommandArgument='<%# Eval("Remision") %>' CommandName="Detalle" AutoPostBack="true" ToolTip="Detalle">
                                                                </dx:ASPxButton>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>                                               
                                                    </Columns>
                                                    <Settings ShowGroupPanel="false" ShowFooter="True" />
                                                    <SettingsPager Position="TopAndBottom" Visible="True" PageSize="20">
                                                            <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                            </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsSearchPanel Visible="false" />
                                           
                                                    <TotalSummary>
                                                
                                                        <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                                    </TotalSummary>
                                                    <Styles>
                                                        <GroupRow HorizontalAlign="Left" />
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server" GridViewID="gvUsuarioAudit">
                                                </dx:ASPxGridViewExporter>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnExportar" />
                                            </Triggers>
                                            </asp:UpdatePanel>

                                        </div>

                                        <%-- Estatus Contratos--%>
                                        <div class="col-lg-6" style="color:black; font-size:18px; ">
                                            <span >&nbsp;&nbsp;Estatus de Contratos</span>
                                            <br />

                                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="gvEstatusContratos" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                ClientInstanceName="gvEstatusContratos" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                                Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center" OnRowCommand="gvEstatusContratos_RowCommand" >
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Tipos de contrato" FieldName="TipoContrato" VisibleIndex="0" HeaderStyle-HorizontalAlign="Left">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Activo" FieldName="Activos" VisibleIndex="1">
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Cancelados" FieldName="Cancelados" VisibleIndex="2" >
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn>  
                                                <dx:GridViewDataColumn Caption="Finalizados" FieldName="Finalizados" VisibleIndex="3" >
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn> 
                                                <dx:GridViewDataColumn Caption="Acciones" CellStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <DataItemTemplate>
                                                            <dx:ASPxButton Text="Detalle" Theme="Office2010Black" ID="btnDetalle" runat="server" CommandArgument='<%# Eval("IdTipoContrato") %>' CommandName="Detalle" AutoPostBack="true" ToolTip="Detalle">
                                                            </dx:ASPxButton>
                                                        </DataItemTemplate>
                                                </dx:GridViewDataColumn>                                               
                                            </Columns>
                                            <Settings ShowGroupPanel="false" ShowFooter="True" />
                                            <SettingsPager Position="TopAndBottom" Visible="True" PageSize="10">
                                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                    </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="false" />
                                           
                                            <TotalSummary>
                                                
                                                <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                            </TotalSummary>
                                            <Styles>
                                                <GroupRow HorizontalAlign="Left" />
                                            </Styles>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter3" runat="server" GridViewID="gvUsuarioAudit">
                                        </dx:ASPxGridViewExporter>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportar" />
                                    </Triggers>
                                    </asp:UpdatePanel>

                                        </div>
                                    </div>    
                                 </div>                                 
                             </div>
                            
                         </fieldset>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <%--fin tab 01--%>

        <%--inicia tab 02--%>
         <TabPages>
           <dx:TabPage Text ="Usuarios" Enabled ="true">
                <ContentCollection>
                    <dx:ContentControl>
                         <fieldset class="Personal1">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"><%-- Texto --%></span>
                            </legend>
                                 <%--inicia contenido tab 02--%>
                              <div class="row">
                                <div class="col-md-12">

                                    <fieldset class="Personal">
<%--                                        <legend>
                                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                                        </legend>--%>
                                        <div class="col-sm-12">
                                            <div class="col-lg-4">
                                                <asp:Label ID="lblRolNum" Text="Numero de roles: " Theme="Office2010Black" runat="server" Width="180px"></asp:Label> 
                                            </div>
                                            <div class="col-lg-4">
                                                <asp:Label ID="lblUsuNum" Text="Numero de usuarios: " Theme="Office2010Black" runat="server" Width="180px"></asp:Label>  
                                            </div>
                                            <div class="col-lg-4">
                                                <dx:ASPxButton ID="btnActividadUsuario" runat="server" Text="Actividad Usuario" Theme="Office2010Black" OnClick="btnActividadUsuario_Click"></dx:ASPxButton>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                                                                                                                                                                
                             <br />
                                                          
                             <div class="row">
                                 <div class =" col-md-12">                                    
                                        <%-- Roles por Modulo--%>   
                                            <br />                                    
                                            <div class="col-lg-6" style="color:black; font-size:18px; ">
                                                <span >&nbsp;&nbsp;Roles por Modulo</span>
                                            </div>                                                                                                                                    
                                            <br />
                                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="gvUsuarioAudit" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                        ClientInstanceName="gvUsuarioAudit" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                                        Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center" >
                                                    <Columns>                                                        
                                                        
                                                        <dx:GridViewDataColumn FieldName="RolDescripcion" Caption="Rol" VisibleIndex="0"  >
                                                                <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="Modulo" Caption="Modulo" VisibleIndex="1" >
                                                                <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                            </dx:GridViewDataColumn>                                                
                                                            <dx:GridViewDataColumn FieldName="Accion" Caption="Acciones" VisibleIndex="3">
                                                                <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                                                                     
                                                    </Columns>
                                                    <Settings ShowGroupPanel="true" ShowFooter="True" />
                                                    <SettingsPager Position="TopAndBottom" Visible="True" PageSize="10">
                                                            <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                            </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsSearchPanel Visible="true" />
                                           
                                                    <GroupSummary>
                                                            <dx:ASPxSummaryItem FieldName="RolDescripcion" SummaryType="Custom" DisplayFormat="Total Roles: {0}" ShowInColumn="RolDescripcion"/>
                                                            <dx:ASPxSummaryItem FieldName="Modulo" SummaryType="Custom" DisplayFormat="Total Modulos: {0}" ShowInColumn="RolDescripcion" />                                               
                                                            <dx:ASPxSummaryItem FieldName="Accion" SummaryType="Custom" DisplayFormat="Total Acciones: {0}" ShowInColumn="Usuario" />
                                                        </GroupSummary>
                                                        <TotalSummary>
                                                            <dx:ASPxSummaryItem FieldName="Modulo" SummaryType="Count"/>
                                                            <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                                    </TotalSummary>
                                                    <Styles>
                                                        <GroupRow HorizontalAlign="Left" />
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvUsuarioAudit">
                                                </dx:ASPxGridViewExporter>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnExportar" />
                                            </Triggers>
                                            </asp:UpdatePanel>                                                                                                                        
                                 </div>                                 
                             </div>

                             <br />
                             <div class="row">                                
                                 <div class =" col-md-12">
                                     <table width="100%">
                                     <tr>
                                        <td style="text-align:right;"><br />
                                             <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                                            &nbsp;<dx:ASPxButton ID="btnExportar" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportar_Click"></dx:ASPxButton>
                                        </td>
                                    </tr>
                                 </table>
                                 </div>                                                                  
                             </div>

                             <br />

                             <div class="row">
                                 <div class =" col-md-12">
                                     <%-- Roles por usuario--%>
                                            <br />                                    
                                            <div class="col-lg-6" style="color:black; font-size:18px; ">
                                                <span >&nbsp;&nbsp;Rol por usuario </span>
                                            </div>                                                                                                                                    
                                            <br />                                                                                
                                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="gvRolUsuarioRolAudit" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                ClientInstanceName="gvRolUsuarioRolAudit" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                                Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center"  >
                                            <Columns>
                                                 <dx:GridViewDataColumn FieldName="Usuario" Caption="Usuario" VisibleIndex="0">
                                                                <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="Rol" Caption="Rol" VisibleIndex="1" >
                                                                <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                            </dx:GridViewDataColumn>                                              
                                            </Columns>
                                            <Settings ShowGroupPanel="true" ShowFooter="True" />
                                            <SettingsPager Position="TopAndBottom" Visible="True" PageSize="10">
                                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                    </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="true" />
                                           
                                            <TotalSummary>
                                                
                                                <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                            </TotalSummary>
                                            <Styles>
                                                <GroupRow HorizontalAlign="Left" />
                                            </Styles>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="ExporterRolUsuarioRolAudit" runat="server" GridViewID="gvRolUsuarioRolAudit">
                                        </dx:ASPxGridViewExporter>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportarRolUsuarioRolAudit" />
                                    </Triggers>
                                    </asp:UpdatePanel>                                       
                                 </div>
                             </div>

                             <br />
                             <div class="row">                                
                                 <div class =" col-md-12">
                                     <table width="100%">
                                     <tr>
                                        <td style="text-align:right;"><br />
                                             <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                                            &nbsp;<dx:ASPxButton ID="btnExportarRolUsuarioRolAudit" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarRolUsuarioRolAudit_Click" ></dx:ASPxButton>
                                        </td>
                                    </tr>
                                 </table>
                                 </div>                                                                  
                             </div>

                             <br />
                                 
                         </fieldset>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <%--fin tab 02--%>

        <%--inicia tab 03--%>
        <TabPages>
            <dx:TabPage Text ="Operación" Enabled ="true">
                <ContentCollection>
                    <dx:ContentControl>
                         <fieldset class="Personal1">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Indicadores</span>
                            </legend>

                           <div class="row">
                                <div class="col-md-12">
                                    <fieldset class="Personal">
                                        <legend>
                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                                        </legend>
                                        <div class="col-sm-12">
                                           <div class="col-lg-4" style="color:black; font-size:18px; ">
                                               <dx:ASPxDateEdit ID="deFechaInicial" runat="server" Caption="Fecha Inicial: " Theme="Office2010Black" NullText="Seleccione una Fecha"></dx:ASPxDateEdit>
                                           </div> 
                                            <div class="col-lg-4" style="color:black; font-size:18px; ">
                                                <dx:ASPxDateEdit ID="deFechaFinal" runat="server" Caption="Fecha Final: " Theme="Office2010Black" NullText="Seleccione una Fecha"></dx:ASPxDateEdit>
                                           </div> 
                                           <div class="col-lg-4" style="color:black; font-size:18px; ">
                                               <dx:ASPxButton ID="btnBuscar" runat="server" Text="Buscar" Theme="Office2010Black"></dx:ASPxButton>
                                           </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <br />

                            <div class="row" >
                                <div class="col-md-12">
                                    <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxGridView Visible="false" ID="gvIndicadorOperacion" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                ClientInstanceName="gvIndicadorOperacion" EnableTheming="True" Styles-Header-HorizontalAlign ="Left"
                                                Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center" >
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Estatus de Vuelo" FieldName="EstatusVuelo" VisibleIndex="0" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Solicitud de Servicio" FieldName="Solicitudes"  VisibleIndex="0" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Cliente" FieldName="Cliente"  VisibleIndex="1">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Viabilidad de Despacho" FieldName="ViabilidadDespacho"  VisibleIndex="2" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Comisariato Especial" FieldName="ComisariatoEspecial"  VisibleIndex="3" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Comisariato Preferencias" FieldName="ComisariatoPreferencias"  VisibleIndex="3" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Confirmación de Vuelo" FieldName="ConfirmacionVuelo"  VisibleIndex="3" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowGroupPanel="True" ShowFooter="True" />
                                            <SettingsPager Position="TopAndBottom" Visible="True" PageSize="20">
                                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                    </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="true" />
                                           
                                            <TotalSummary>
                                                
                                                <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                            </TotalSummary>
                                            <Styles>
                                                <GroupRow HorizontalAlign="Left" />
                                            </Styles>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter4" runat="server" GridViewID="gvUsuarioAudit">
                                        </dx:ASPxGridViewExporter>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportar" />
                                    </Triggers>
                                    </asp:UpdatePanel>      
                                </div>
                                 
                             </div>

                             <br />
                             <div class="row">
                                 <div class =" col-md-12">
                                    <div class="col-sm-12">
                                        <div class="col-lg-4" style="color:black; font-size:18px; ">
                                            <span >&nbsp;&nbsp;Intercambios</span>
                                            <br />

                                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="gvIntercambio" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                        ClientInstanceName="gvIntercambio" EnableTheming="True" Styles-Header-HorizontalAlign ="Left"
                                                        Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center" OnRowCommand="gvIntercambio_RowCommand" >
                                                    <Columns>
                                                        <dx:GridViewDataColumn Caption="Cliente" FieldName="Matricula" VisibleIndex="0" >
                                                            <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn Caption="Total Horas" FieldName="TotalHoras" VisibleIndex="1">
                                                            <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                        </dx:GridViewDataColumn> 
                                                        <dx:GridViewDataColumn Caption="Acciones" CellStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <DataItemTemplate>
                                                                <dx:ASPxButton Text="Detalle" Theme="Office2010Black" ID="btnDetalle" runat="server" CommandArgument='<%# Eval("IdContrato") %>' CommandName="Detalle" AutoPostBack="true" ToolTip="Detalle">
                                                                </dx:ASPxButton>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>                                                                                                       
                                                    </Columns>
                                                    <Settings ShowGroupPanel="false" ShowFooter="True" />
                                                    <SettingsPager Position="TopAndBottom" Visible="True" PageSize="20">
                                                            <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                            </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsSearchPanel Visible="false" />
                                           
                                                    <TotalSummary>
                                                
                                                        <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                                    </TotalSummary>
                                                    <Styles>
                                                        <GroupRow HorizontalAlign="Left" />
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter5" runat="server" GridViewID="gvUsuarioAudit">
                                                </dx:ASPxGridViewExporter>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnExportar" />
                                            </Triggers>
                                            </asp:UpdatePanel>

                                        </div>
                                        <div class="col-lg-4" style="color:black; font-size:18px; ">
                                            <span >&nbsp;&nbsp;Rentas Aeronaves</span>
                                            <br />

                                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="gvRentasAeronaves" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                ClientInstanceName="gvRentasAeronaves" EnableTheming="True" Styles-Header-HorizontalAlign ="Left"
                                                Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center" OnRowCommand="gvRentasAeronaves_RowCommand" >
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Matricula"  FieldName="Matricula" VisibleIndex="0" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Numero de Rentas" FieldName="NumeroRentas" VisibleIndex="1">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Acciones" CellStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <DataItemTemplate>
                                                                <dx:ASPxButton Text="Detalle" Theme="Office2010Black" ID="btnDetalle" runat="server" CommandArgument='<%# Eval("IdContrato") %>' CommandName="Detalle" AutoPostBack="true" ToolTip="Detalle">
                                                                </dx:ASPxButton>
                                                        </DataItemTemplate>
                                                </dx:GridViewDataColumn>                                                                                                  
                                            </Columns>
                                            <Settings ShowGroupPanel="false" ShowFooter="True" />
                                            <SettingsPager Position="TopAndBottom" Visible="True" PageSize="20">
                                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                    </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="false" />
                                           
                                            <TotalSummary>
                                                
                                                <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                            </TotalSummary>
                                            <Styles>
                                                <GroupRow HorizontalAlign="Left" />
                                            </Styles>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter6" runat="server" GridViewID="gvUsuarioAudit">
                                        </dx:ASPxGridViewExporter>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportar" />
                                    </Triggers>
                                    </asp:UpdatePanel>

                                        </div>                                        
                                        <div class="col-lg-4" style="color:black; font-size:18px; ">
                                            <span >&nbsp;&nbsp;Vuelos Ventas</span>
                                            <br />
                                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="gvVuelosVentas" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                ClientInstanceName="gvVuelosVentas" EnableTheming="True" Styles-Header-HorizontalAlign ="Left"
                                                Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center" OnRowCommand="gvVuelosVentas_RowCommand" >
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Cliente" FieldName="Cliente" VisibleIndex="0" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Vuelos" FieldName="Vuelos" VisibleIndex="1">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn> 
                                                <dx:GridViewDataColumn Caption="Acciones" CellStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <DataItemTemplate>
                                                                <dx:ASPxButton Text="Detalle" Theme="Office2010Black" ID="btnDetalle" runat="server" CommandArgument='<%# Eval("IdContrato") %>' CommandName="Detalle" AutoPostBack="true" ToolTip="Detalle">
                                                                </dx:ASPxButton>
                                                        </DataItemTemplate>
                                                </dx:GridViewDataColumn>                                                                                                                                                
                                            </Columns>
                                            <Settings ShowGroupPanel="false" ShowFooter="True" />
                                            <SettingsPager Position="TopAndBottom" Visible="True" PageSize="20">
                                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                    </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="false" />
                                           
                                            <TotalSummary>
                                                
                                                <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                            </TotalSummary>
                                            <Styles>
                                                <GroupRow HorizontalAlign="Left" />
                                            </Styles>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter7" runat="server" GridViewID="gvUsuarioAudit">
                                        </dx:ASPxGridViewExporter>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportar" />
                                    </Triggers>
                                    </asp:UpdatePanel>
                                        </div>

                                    </div>    
                                 </div>                                 
                             </div>                           


                         </fieldset>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <%--fin tab 03--%>

        <%--inicia tab 04--%>
         <TabPages>
            <dx:TabPage Text ="Reporteador" Enabled ="true">
                <ContentCollection>
                    <dx:ContentControl>
                         <fieldset class="Personal1">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"></span>
                            </legend>

                             <div class="row">
                                <div class="col-md-12">
                                    <fieldset class="Personal">
                                        <legend>
                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                                        </legend>
                                        
                                        <div class="col-sm-12">

                                <table border="0" style="width:80%">
                                 <tr>
                                     <td>
                                         <dx:ASPxDateEdit ID="deReporteadorFechaInicial" Caption="Desde" runat="server" Theme="Office2010Black" NullText="Seleccione una opción" >
                                                <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                                                </ValidationSettings>
                                         </dx:ASPxDateEdit>
                                     </td>
                                     <td>
                                         <dx:ASPxDateEdit ID="deReporteadorFechaFinal" Caption="Hasta" runat="server" Theme="Office2010Black" NullText="Seleccione una opción" >
                                             <DateRangeSettings StartDateEditID="deReporteadorFechaInicial"></DateRangeSettings>
                                                <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                                                </ValidationSettings>
                                         </dx:ASPxDateEdit>
                                     </td>                                                                          
                                     <td>&nbsp;</td>                                     
                                 </tr>
                                 <tr>
                                     <td colspan="2">
                                         <dx:ASPxRadioButtonList ID="rblReporteador" runat="server" Caption="Tipo de busqueda" Theme="Office2010Black" RepeatColumns="6" RepeatLayout="Table" >
                                             <Items>
                                                 <dx:ListEditItem Text="Contrato" Value="1" Selected="true" />
                                                 <dx:ListEditItem Text="Remisión" Value="2"/>
                                                 <dx:ListEditItem Text="Solicitudes de vuelo" Value="3" />
                                                 <dx:ListEditItem Text="Bitacoras" Value="4"/>
                                                 <dx:ListEditItem Text="Prefacturas" Value="5"/>
                                                 <dx:ListEditItem Text="Comisariatos" Value="6"/>
                                             </Items>
                                             <CaptionSettings Position="Top" />
                                         </dx:ASPxRadioButtonList>  
                                     </td>
                                     <td>
                                         <dx:ASPxButton ID="btnBuscarReporteador" runat="server" Text="Buscar" Theme="Office2010Black" OnClick="btnBuscarReporteador_Click"></dx:ASPxButton>
                                     </td>
                                     
                                 </tr>

                                </table>
                                <br />
                                     
                                 <br />                                                  
                             </div>

                                    </fieldset>
                                </div>
                            </div>
                              <br />
                            <div class="row">
                                <div class="col-sm-6">
                           
                                </div>
                                <div class="col-sm-6" style="text-align: right;">
                                    <dx:ASPxLabel CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                                    &nbsp;<dx:ASPxButton Visible="true" CssClass="FBotton" ID="btnExportarRepContrato1" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarRepContrato_Click"></dx:ASPxButton>
                                    &nbsp;<dx:ASPxButton Visible="false" CssClass="FBotton" ID="btnExportarRepRemisiones1" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarRepRemisiones_Click" ></dx:ASPxButton>
                                    &nbsp;<dx:ASPxButton Visible="false" CssClass="FBotton" ID="btnExportarRepSolicitudVuelo1" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarRepSolicitudVuelo_Click" ></dx:ASPxButton>
                                    &nbsp;<dx:ASPxButton Visible="false" CssClass="FBotton" ID="btnExportarRepBitacoras1" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarRepBitacoras_Click" ></dx:ASPxButton>
                                    &nbsp;<dx:ASPxButton Visible="false" CssClass="FBotton" ID="btnExportarRepPrefactura1" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarRepPrefactura_Click" ></dx:ASPxButton>
                                    &nbsp;<dx:ASPxButton Visible="false" CssClass="FBotton" ID="btnExportarRepComisariato1" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarRepComisariato_Click" ></dx:ASPxButton>
                                </div>
                            </div>
                            <br />
                                                                                          
                             <%--ReporteadorContratos--%>
                             <div class="row" >
                                <div class="col-md-12">
                                    <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxGridView Visible="true" ID="gvReporteadorContratos" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                ClientInstanceName="gvReporteadorContratos" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                                Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center" >
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Contrato" FieldName="IdContrato" VisibleIndex="0" Visible="false" Width="100px" >
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn>     
                                                <dx:GridViewDataColumn Caption="Contrato" FieldName="ClaveContrato" VisibleIndex="1" Width="100px">
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn> 
                                                <dx:GridViewDataColumn Caption="Cliente" FieldName="CodigoCliente" VisibleIndex="2" Width="100px">
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn> 
                                                <dx:GridViewDataColumn Caption="Fecha <br/> Contrato" FieldName="FechaContrato" VisibleIndex="3" Width="130px">
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn> 
                                                <dx:GridViewDataColumn Caption="Fecha <br/> Inicio Vuelo" FieldName="FechaInicioVuelo" VisibleIndex="4" Width="140px">
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn> 
                                                <dx:GridViewDataColumn Caption="Años <br/> Contratados" FieldName="AniosContratados" VisibleIndex="5" Width="100px">
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn> 
                                                <dx:GridViewDataColumn Caption="Meses <br/> Gracia" FieldName="MesesGracia" VisibleIndex="6" Width="100px" >
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn> 
                                                <dx:GridViewDataColumn Caption="Total <br/> Horas Contratadas" FieldName="HorasContratadasTotal" VisibleIndex="7" Width="150px">
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn> 
                                                <dx:GridViewDataColumn Caption="Horas <br/> Contratadas Año" FieldName="HorasContratadasAnio" VisibleIndex="8" Width="150px">
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn> 
                                                <dx:GridViewDataColumn Caption="Horas No Usadas" FieldName="HorasNoUsadas" VisibleIndex="9" Width="150px">
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn>  
                                                
                                                <dx:GridViewDataTextColumn Caption="Anticipo Inicial" FieldName="AnticipoInicial" VisibleIndex="11" Width="150px">                                                    
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit>                                                    
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Fijo Anual" FieldName="FijoAnual" VisibleIndex="12" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit>
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Renovación" FieldName="Renovacion" VisibleIndex="13" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit>
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>                                              
                                                <dx:GridViewDataColumn Caption="Usuario Creación" FieldName="UsuarioCreacion" VisibleIndex="15" Width="150px">
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn>                                                 
                                                <dx:GridViewDataDateColumn Caption="Fecha Creación" FieldName="FechaCreacion" VisibleIndex="16" Width="200px" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>                                                
                                                <dx:GridViewDataColumn Caption="Usuario Modificación" FieldName="UsuarioModificacion" VisibleIndex="17" Width="150px">
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn> 
                                                <dx:GridViewDataDateColumn Caption="Fecha Modificación" FieldName="FechaModificacion" VisibleIndex="18" Width="200px" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                                                                          
                                            </Columns>

                                                                                      
                                            <Settings HorizontalScrollBarMode="Auto" />
                                            <Settings ShowGroupPanel="True" ShowFooter="True" />
                                            <SettingsPager Position="TopAndBottom" Visible="True" PageSize="20">
                                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                    </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="true" />
                                           
                                            <TotalSummary>
                                                
                                                <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                            </TotalSummary>
                                            <Styles>
                                                <GroupRow HorizontalAlign="Left" />
                                            </Styles>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="ExporterReporteadorContrato" runat="server" GridViewID="gvReporteadorContratos">
                                        </dx:ASPxGridViewExporter>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportarRepContrato" />
                                        <asp:PostBackTrigger ControlID="btnExportarRepContrato1" />
                                    </Triggers>
                                    </asp:UpdatePanel>      
                                </div>
                                 
                             </div>
                              
                             <%--ReporteadorRemisiones--%>
                             <div class="row" >
                                <div class="col-md-12">
                                    <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxGridView Visible="false" ID="gvReporteadorRemisiones" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                ClientInstanceName="gvReporteadorRemisiones" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                                Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center" >
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Remisión" FieldName="IdRemision" VisibleIndex="0" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                               <dx:GridViewDataColumn Caption="Cliente" FieldName="CodigoCliente" VisibleIndex="1" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Contrato" FieldName="ClaveContrato" VisibleIndex="2" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Fecha" FieldName="Fecha" VisibleIndex="3" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataTextColumn Caption="Total Remision Pesos" FieldName="TotalRemisionPesos" VisibleIndex="4" >                                                    
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit>                                                           
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Total Remision Dlls" FieldName="TotalRemisionDlls" VisibleIndex="5" >
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit>  
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataColumn Caption="Usuario Creación" FieldName="UsuarioCreacion" VisibleIndex="6" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                

                                                <dx:GridViewDataDateColumn Caption="Fecha Creación" FieldName="FechaCreacion" VisibleIndex="7" Width="200px" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>

                                                <dx:GridViewDataColumn Caption="Usuario Modificación" FieldName="UsuarioModificacion" VisibleIndex="8" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                
                                                <dx:GridViewDataDateColumn Caption="Fecha Modificación" FieldName="FechaModificacion" VisibleIndex="9" Width="200px" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                            </Columns>

                                                                                                                                

                                            <Settings ShowGroupPanel="True" ShowFooter="True" />
                                            <SettingsPager Position="TopAndBottom" Visible="True" PageSize="20">
                                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                    </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="true" />
                                           
                                            <TotalSummary>
                                                
                                                <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                            </TotalSummary>
                                            <Styles>
                                                <GroupRow HorizontalAlign="Left" />
                                            </Styles>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="ExporterReporteadorRemision" runat="server" GridViewID="gvReporteadorRemisiones">
                                        </dx:ASPxGridViewExporter>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportarRepRemisiones" />
                                         <asp:PostBackTrigger ControlID="btnExportarRepRemisiones1" />
                                    </Triggers>
                                    </asp:UpdatePanel>      
                                </div>
                                 
                             </div>

                             <%--ReporteadorSolicitudVuelo--%>
                             <div class="row" >
                                <div class="col-md-12">
                                    <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxGridView Visible="false" ID="gvReporteadorSolicitudVuelo" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                ClientInstanceName="gvReporteadorSolicitudVuelo" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                                Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center" >
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Solicitud Vuelo" FieldName="IdSolicitud" VisibleIndex="0" Width="150px" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Contrato" FieldName="ClaveContrato" VisibleIndex="1" Width="150px">
                                                    <CellStyle HorizontalAlign ="Center"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                                                                
                                                <dx:GridViewDataColumn Caption="Grupo Modelo" FieldName="GrupoModelo" VisibleIndex="5" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Estatus Solicitud" FieldName="EstatusSolicitud" VisibleIndex="6" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Usuario Creación" FieldName="UsuarioCreacion" VisibleIndex="7" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>                                                

                                                <dx:GridViewDataDateColumn Caption="Fecha Creación" FieldName="FechaCreacion" VisibleIndex="8" Width="200px" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>

                                                <dx:GridViewDataColumn Caption="Usuario Modificación" FieldName="UsuarioModificacion" VisibleIndex="9" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>                                               

                                                <dx:GridViewDataDateColumn Caption="Fecha Modificación" FieldName="FechaModificacion" VisibleIndex="10" Width="200px" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>

                                            </Columns>
                                             
                                            <Settings HorizontalScrollBarMode="Auto" />                                                       
                                            <Settings ShowGroupPanel="True" ShowFooter="True" />
                                            <SettingsPager Position="TopAndBottom" Visible="True" PageSize="20">
                                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                    </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="true" />
                                           
                                            <TotalSummary>
                                                
                                                <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                            </TotalSummary>
                                            <Styles>
                                                <GroupRow HorizontalAlign="Left" />
                                            </Styles>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="ExporterReporteadorSolicitudVuelo" runat="server" GridViewID="gvReporteadorSolicitudVuelo">
                                        </dx:ASPxGridViewExporter>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportarRepSolicitudVuelo" />
                                        <asp:PostBackTrigger ControlID="btnExportarRepSolicitudVuelo1" />
                                    </Triggers>
                                    </asp:UpdatePanel>      
                                </div>
                                 
                             </div>

                             <%--ReporteadorBitacoras--%>
                             <div class="row" >
                                <div class="col-md-12">
                                    <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxGridView Visible="false" ID="gvReporteadorBitacoras" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                ClientInstanceName="gvReporteadorBitacoras" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                                Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center" >
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Bitacora" FieldName="IdBitacora" VisibleIndex="0" Visible="false" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Bitacora" FieldName="FolioReal" VisibleIndex="0" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                
                                                <dx:GridViewDataColumn Caption="Matricula" FieldName="AeronaveMatricula" VisibleIndex="2" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>                                                
                                                <dx:GridViewDataColumn Caption="Cliente" FieldName="VueloClienteId" VisibleIndex="4" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Contrato" FieldName="VueloContratoId" VisibleIndex="5" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Piloto" FieldName="PilotoId" VisibleIndex="6" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Copiloto" FieldName="CopilotoId" VisibleIndex="7" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                
                                                <dx:GridViewDataColumn Caption="Origen" FieldName="Origen" VisibleIndex="9" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Destino" FieldName="Destino" VisibleIndex="10" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                                                                
                                                <dx:GridViewDataDateColumn Caption="Origen Vuelo" FieldName="OrigenVuelo" VisibleIndex="11" Width="200px" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataDateColumn Caption="Origen Calzo" FieldName="OrigenCalzo" VisibleIndex="12" Width="200px" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataDateColumn Caption="Destino Vuelo" FieldName="DestinoVuelo" VisibleIndex="13" Width="200px" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataDateColumn Caption="Destino Calzo" FieldName="DestinoCalzo" VisibleIndex="14" Width="200px" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>

                                                <dx:GridViewDataColumn Caption="Cant Pax" FieldName="CantPax" VisibleIndex="15" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="LogNum" FieldName="LogNum" VisibleIndex="16" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Numero Trip" FieldName="TripNum" VisibleIndex="17" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Pierna" FieldName="Leg_Num" VisibleIndex="18" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Remisionado" FieldName="Remisionado" VisibleIndex="19" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Usuario Creación" FieldName="UsuarioCreacion" VisibleIndex="20" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>                                                

                                                <dx:GridViewDataDateColumn Caption="Fecha Creación" FieldName="FechaCreacion" VisibleIndex="21" Width="200px" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>

                                                <dx:GridViewDataColumn Caption="Usuario Modificación" FieldName="UsuarioModificacion" VisibleIndex="22" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>                                                

                                                <dx:GridViewDataDateColumn Caption="Fecha Modificación" FieldName="FechaModificacion" VisibleIndex="23" Width="200px" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>

                                            </Columns>
                                                                                                                  
                                            <Settings HorizontalScrollBarMode="Auto" />
                                            <Settings ShowGroupPanel="True" ShowFooter="True" />
                                            <SettingsPager Position="TopAndBottom" Visible="True" PageSize="20">
                                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                    </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="true" />
                                           
                                            <TotalSummary>
                                                
                                                <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                            </TotalSummary>
                                            <Styles>
                                                <GroupRow HorizontalAlign="Left" />
                                            </Styles>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="ExporterReporteadorBitacoras" runat="server" GridViewID="gvReporteadorBitacoras">
                                        </dx:ASPxGridViewExporter>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportarRepBitacoras" />
                                        <asp:PostBackTrigger ControlID="btnExportarRepBitacoras1" />
                                    </Triggers>
                                    </asp:UpdatePanel>      
                                </div>
                                 
                             </div>

                             <%--ReporteadorPrefactura--%>
                             <div class="row" >
                                <div class="col-md-12">
                                    <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxGridView Visible="false" ID="gvReporteadorPrefactura" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                ClientInstanceName="gvReporteadorPrefactura" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                                Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center" >
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Prefactura" FieldName="IdPrefactura" VisibleIndex="0" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Contrato" FieldName="ClaveContrato" VisibleIndex="1" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Facturante Vuelo" FieldName="CveFacturanteVuelo" VisibleIndex="2" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Tipo Cambio" FieldName="TipoCambio" VisibleIndex="3" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Facturante Servicio" FieldName="CveFacturanteServicio" VisibleIndex="4" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataTextColumn Caption="SubTotal DLLS Vuelo" FieldName="SubTotalDLLV" VisibleIndex="5" Width="150px">                                                    
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="SubTotal MXN Vuelo" FieldName="SubTotalMXNV" VisibleIndex="6" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="SubTotal Hrs Vuelo" FieldName="SubTotalHrsV" VisibleIndex="7" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="IVA Vuelo" FieldName="IVAV" VisibleIndex="8" Visible="false" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="IVA DLLS Vuelo" FieldName="IVADLLSV" VisibleIndex="9" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="IVA MXN Vuelo" FieldName="IVAMXNV" VisibleIndex="10" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Total DLLS Vuelo" FieldName="TotalDLLSV" VisibleIndex="11" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Total MXN Vuelo" FieldName="TotalMXNV" VisibleIndex="12" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="SubTotal DLLS Servicio" FieldName="SubTotalDLLSC" VisibleIndex="13" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="SubTotal MXN Servicio" FieldName="SubTotalMXNC" VisibleIndex="14" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="IVA Servicio" FieldName="IVAC" VisibleIndex="15" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="IVA DLLS Servicio" FieldName="IVADLLSC" VisibleIndex="16" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="IVA MXN Servicio" FieldName="IVAMXNC" VisibleIndex="17" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Total DLLS Servicio" FieldName="TotalDLLSC" VisibleIndex="18" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Total MXN Servicio" FieldName="TotalMXNC" VisibleIndex="19" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit> 
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataColumn Caption="Cobro Vuelo" FieldName="CobroVuelo" VisibleIndex="20" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Cobro ServiciCargo" FieldName="CobroServiciCargo" VisibleIndex="21" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Cobro Ambos" FieldName="CobroAmbos" VisibleIndex="22" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Porcentaje Avance" FieldName="PorcentajeAvance" VisibleIndex="23" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Usuario Creación" FieldName="UsuarioCreacion" VisibleIndex="24" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>                                                

                                                <dx:GridViewDataDateColumn Caption="Fecha Creación" FieldName="FechaCreacion" VisibleIndex="25" Width="200px" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>

                                                <dx:GridViewDataColumn Caption="Usuario Modificación" FieldName="UsuarioModificacion" VisibleIndex="26" Width="150px">
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                
                                                <dx:GridViewDataDateColumn Caption="Fecha Modificación" FieldName="FechaModificacion" VisibleIndex="26" Width="200px" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>

                                            </Columns>

                                            <Settings HorizontalScrollBarMode="Auto" />                     
                                            <Settings ShowGroupPanel="True" ShowFooter="True" />
                                            <SettingsPager Position="TopAndBottom" Visible="True" PageSize="20">
                                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                    </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="true" />
                                           
                                            <TotalSummary>
                                                
                                                <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                            </TotalSummary>
                                            <Styles>
                                                <GroupRow HorizontalAlign="Left" />
                                            </Styles>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="ExporterReporteadorPrefactura" runat="server" GridViewID="gvReporteadorPrefactura">
                                        </dx:ASPxGridViewExporter>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportarRepPrefactura" />
                                         <asp:PostBackTrigger ControlID="btnExportarRepPrefactura1" />
                                    </Triggers>
                                    </asp:UpdatePanel>      
                                </div>
                                 
                             </div>

                             <%--ReporteadorComisariato--%>
                             <div class="row" >
                                <div class="col-md-12">
                                    <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxGridView Visible="false" ID="gvReporteadorComisariato" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                ClientInstanceName="gvReporteadorComisariato" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                                Theme="Office2010Black" Width="100%" Styles-Cell-HorizontalAlign="Center" >
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Comisariato" FieldName="IdComisariato" VisibleIndex="0" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Fecha Vuelo" FieldName="FechaVuelo" VisibleIndex="1" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Hora Vuelo" FieldName="HoraVuelo" VisibleIndex="2" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Trip" FieldName="Trip" VisibleIndex="3" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Cliente" FieldName="CodigoCliente" VisibleIndex="4" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Matricula" FieldName="Matricula" VisibleIndex="5" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Origen" FieldName="Origen" VisibleIndex="6" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>                                               
                                                <dx:GridViewDataColumn Caption="Descripción" FieldName="ComisariatoDesc" VisibleIndex="8" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Preferencias" FieldName="Preferencias" VisibleIndex="9" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Proveedor" FieldName="Proveedor" VisibleIndex="10" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Estatus" FieldName="Estaus" VisibleIndex="11" >
                                                    <CellStyle HorizontalAlign ="Left"></CellStyle>
                                                </dx:GridViewDataColumn>                                                                                                                                         

                                            </Columns>
                                                                                                                                      
                                            <Settings ShowGroupPanel="True" ShowFooter="True" />
                                            <SettingsPager Position="TopAndBottom" Visible="True" PageSize="20">
                                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                    </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="true" />
                                           
                                            <TotalSummary>
                                                
                                                <%--<dx:ASPxSummaryItem FieldName="Usuario" SummaryType="Sum" />--%>
                                            </TotalSummary>
                                            <Styles>
                                                <GroupRow HorizontalAlign="Left" />
                                            </Styles>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="ExporterReporteadorComisariato" runat="server" GridViewID="gvReporteadorComisariato">
                                        </dx:ASPxGridViewExporter>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportarRepComisariato" />
                                        <asp:PostBackTrigger ControlID="btnExportarRepComisariato1" />
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
                                    &nbsp;<dx:ASPxButton Visible="true" CssClass="FBotton" ID="btnExportarRepContrato" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarRepContrato_Click"></dx:ASPxButton>
                                    &nbsp;<dx:ASPxButton Visible="false" CssClass="FBotton" ID="btnExportarRepRemisiones" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarRepRemisiones_Click" ></dx:ASPxButton>
                                    &nbsp;<dx:ASPxButton Visible="false" CssClass="FBotton" ID="btnExportarRepSolicitudVuelo" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarRepSolicitudVuelo_Click" ></dx:ASPxButton>
                                    &nbsp;<dx:ASPxButton Visible="false" CssClass="FBotton" ID="btnExportarRepBitacoras" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarRepBitacoras_Click" ></dx:ASPxButton>
                                    &nbsp;<dx:ASPxButton Visible="false" CssClass="FBotton" ID="btnExportarRepPrefactura" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarRepPrefactura_Click" ></dx:ASPxButton>
                                    &nbsp;<dx:ASPxButton Visible="false" CssClass="FBotton" ID="btnExportarRepComisariato" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarRepComisariato_Click" ></dx:ASPxButton>
                                </div>
                            </div>
                            <br />

                         </fieldset>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <%--fin tab 04--%>
    </dx:ASPxPageControl>

    <dx:ASPxPopupControl ID="popupActividadUsuario" runat="server" ClientInstanceName="popupActividadUsuario" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black" 
        PopupHorizontalAlign="WindowCenter" ScrollBars="Vertical" PopupVerticalAlign="Above" HeaderText="Actividades" AllowDragging="true" ShowCloseButton="true" Width="1200" Height="590">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel2" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-md-12">

                                    <fieldset class="Personal">
                                        <br />
                                        <div class="col-sm-12">

                                             <table border="0" style="width:100%">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxLabel ID="lblModulo" runat="server" Text="Modulo: " Theme="Office2010Black"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxComboBox ID="cmbModulo" runat="server" Theme="Office2010Black" NullText="Seleccione una opción" ></dx:ASPxComboBox>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <dx:ASPxLabel ID="lblUsuarioActividad" runat="server" Text="Usuario: " Theme="Office2010Black"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxComboBox ID="cmbUsuariosActividad" runat="server" Theme="Office2010Black" NullText="Seleccione una opción" ></dx:ASPxComboBox>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxLabel ID="lblAccion" runat="server" Text="Acción: " Theme="Office2010Black"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxComboBox ID="cmbAccion" runat="server" Theme="Office2010Black" NullText="Seleccione una opción" >
                                                                <Items>
                                                                    <dx:ListEditItem Value="-1" Text="Todas las acciones" />
                                                                    <dx:ListEditItem Value="2" Text="Insertar" />
                                                                    <dx:ListEditItem Value="3" Text="Actualizar" />
                                                                    <dx:ListEditItem Value="4" Text="Eliminar" />
                                                                </Items>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <dx:ASPxLabel ID="lblFecha" runat="server" Text="Fecha: " Theme="Office2010Black"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxDateEdit ID="deFechaCreacion" runat="server" Theme="Office2010Black" NullText="Seleccione una opción" ></dx:ASPxDateEdit>                                                
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnBuscarUsuariosActividad" runat="server" Text="Buscar" Theme="Office2010Black" OnClick="btnBuscarUsuariosActividad_Click"></dx:ASPxButton>
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
                                 <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                     <ContentTemplate>
                                         <dx:ASPxGridView ID="gvActividadUsuario" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                ClientInstanceName="gvActividadUsuario" EnableTheming="True" KeyFieldName="IdPax"
                                Styles-Header-HorizontalAlign="Center"  StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                Theme="Office2010Black" Width="100%" >                                                       
                                <Columns>
                                    
                                    <dx:GridViewDataColumn FieldName="IdActividad" Caption="IdActividad" VisibleIndex="0" Visible="true">                                                                
                                        <EditFormSettings Visible="True"></EditFormSettings>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Actividad" Caption="Actividad" VisibleIndex="1" Visible="true">                                                                
                                        <EditFormSettings Visible="True"></EditFormSettings>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Modulo" Caption="Modulo" VisibleIndex="2" Visible="true">                                                                
                                        <EditFormSettings Visible="True"></EditFormSettings>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="Operacion" Caption="Operación" VisibleIndex="3" Visible="true">                                                                
                                        <EditFormSettings Visible="True"></EditFormSettings>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="UsuarioCreacion" Caption="Usuario" VisibleIndex="4" Visible="true">                                                                
                                        <EditFormSettings Visible="True"></EditFormSettings>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataDateColumn FieldName="FechaCreacion" Caption="Fecha" VisibleIndex="5" Visible="true">
                                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy hh:mm tt"></PropertiesDateEdit>
                                    </dx:GridViewDataDateColumn>                                    
                                 </Columns>
                                 <Settings ShowGroupPanel="True" ShowFooter="True"/>
                                 <SettingsPager Position="TopAndBottom" Visible="True" PageSize="10">
                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                    </PageSizeItemSettings>
                                 </SettingsPager>
                                 <SettingsSearchPanel Visible="true" />
                                 <SettingsBehavior ConfirmDelete="True" />
                                 <SettingsEditing Mode="Inline"></SettingsEditing>
                                 <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                 <SettingsCommandButton>
                                     <UpdateButton Text="Guardar">
                                     </UpdateButton>
                                     <CancelButton></CancelButton>
                                     <EditButton>
                                         <Image Height="20px" ToolTip="Modificar" Width="20px">
                                         </Image>
                                     </EditButton>
                                     <DeleteButton>
                                         <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                         </Image>
                                     </DeleteButton>
                                 </SettingsCommandButton>
                                 </dx:ASPxGridView>

                                         <dx:ASPxGridViewExporter ID="ExporterActividadUsuario" runat="server" GridViewID="gvActividadUsuario">
                                        </dx:ASPxGridViewExporter>

                                     </ContentTemplate>
                                     <Triggers>
                                         <asp:PostBackTrigger ControlID="btnExportExcelUsuario" />
                                     </Triggers>
                                 </asp:UpdatePanel>                               
                                
                                </div>
                                <br />
                                <br />
                                <table width="100%">
                                     <tr>
                                         <td style="padding-left:20px;">
                                             <br />
                                             <dx:ASPxButton Theme="Office2010Black" ID="btOK" runat="server" Text="Salir" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popupActividadUsuario.Hide(); }" />
                                        </dx:ASPxButton>
                                         </td>
                                        <td style="text-align:right; padding-right:20px;">
                                            <br />
                                             <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                                            &nbsp;<dx:ASPxButton ID="btnExportExcelUsuario" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportExcelUsuario_Click"></dx:ASPxButton>
                                        </td>
                                    </tr>
                                 </table>

                            </div>                                                                                   
                            <br />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <%--inicia el detalle de los indicadores de finanzas en modales--%>

    <dx:ASPxPopupControl ID="popupVuelosSinBitacora" runat="server" ClientInstanceName="popupVuelosSinBitacora" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black" 
        PopupHorizontalAlign="WindowCenter" ScrollBars="Vertical" PopupVerticalAlign="Above" HeaderText="Vuelos sin Bitácora" AllowDragging="true" ShowCloseButton="true" Width="1200" Height="590">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-md-12">

                                    <fieldset class="Personal">
                                        <br />
                                        <div class="col-sm-12">                                                                                                                                 
                                        </div>                                                                                                                                                              
                                    </fieldset>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">                                    
                                 <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                     <ContentTemplate>
                                         <dx:ASPxGridView ID="gvVuelosSinBitacora" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                            ClientInstanceName="gvVuelosSinBitacora" EnableTheming="True" KeyFieldName="IdPax" Styles-Cell-HorizontalAlign="Center"
                                            Styles-Header-HorizontalAlign="Center"  StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                            Theme="Office2010Black" Width="100%" >                                                       
                                        <Columns>
                                    
                                            <dx:GridViewDataColumn Caption="Numero de Vuelo" FieldName="NoVuelo" VisibleIndex="0" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Fecha" FieldName="Fecha" VisibleIndex="1" Visible="true" CellStyle-HorizontalAlign="Right">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Bitácora" FieldName="NoBitacora" VisibleIndex="2" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Cliente" FieldName="Cliente" VisibleIndex="3" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Contrato" FieldName="Contrato" VisibleIndex="4" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Matrícula" FieldName="Matricula" VisibleIndex="5" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Estatus" FieldName="EstatusD" VisibleIndex="5" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                         </Columns>
                                 <Settings ShowGroupPanel="True" ShowFooter="True"/>
                                 <SettingsPager Position="TopAndBottom" Visible="True" PageSize="10">
                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                    </PageSizeItemSettings>
                                 </SettingsPager>
                                 <SettingsSearchPanel Visible="true" />
                                 <SettingsBehavior ConfirmDelete="True" />
                                 <SettingsEditing Mode="Inline"></SettingsEditing>
                                 <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                 <SettingsCommandButton>
                                     <UpdateButton Text="Guardar">
                                     </UpdateButton>
                                     <CancelButton></CancelButton>
                                     <EditButton>
                                         <Image Height="20px" ToolTip="Modificar" Width="20px">
                                         </Image>
                                     </EditButton>
                                     <DeleteButton>
                                         <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                         </Image>
                                     </DeleteButton>
                                 </SettingsCommandButton>
                                 </dx:ASPxGridView>

                                         <dx:ASPxGridViewExporter ID="ExporterVueloSinBitacora" runat="server" GridViewID="gvVuelosSinBitacora">
                                        </dx:ASPxGridViewExporter>

                                     </ContentTemplate>
                                     <Triggers>
                                         <asp:PostBackTrigger ControlID="btnExportarVueloSinBitacora" />
                                     </Triggers>
                                 </asp:UpdatePanel>                               
                                
                                </div>
                                <br />
                                <br />
                                <table width="100%">
                                     <tr>
                                         <td style="padding-left:20px;">
                                             <br />
                                             <dx:ASPxButton Theme="Office2010Black" ID="btnSalirVueloSinBitacora" runat="server" Text="Salir" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popupVuelosSinBitacora.Hide(); }" />
                                        </dx:ASPxButton>
                                         </td>
                                        <td style="text-align:right; padding-right:20px;">
                                            <br />
                                             <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                                            &nbsp;<dx:ASPxButton ID="btnExportarVueloSinBitacora" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarVueloSinBitacora_Click" ></dx:ASPxButton>
                                        </td>
                                    </tr>
                                 </table>

                            </div>                                                                                   
                            <br />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="popupVencimientoContratos" runat="server" ClientInstanceName="popupVencimientoContratos" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black" 
        PopupHorizontalAlign="WindowCenter" ScrollBars="Vertical" PopupVerticalAlign="Above" HeaderText="Vencimiento de Contratos" AllowDragging="true" ShowCloseButton="true" Width="1200" Height="590">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-md-12">

                                    <fieldset class="Personal">
                                        <br />
                                        <div class="col-sm-12">                                                                                                                                 
                                        </div>                                                                                                                                                              
                                    </fieldset>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">                                    
                                 <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                     <ContentTemplate>
                                         <dx:ASPxGridView ID="gvVencimientoContratos" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                            ClientInstanceName="gvVencimientoContratos" EnableTheming="True" KeyFieldName="IdPax" Styles-Cell-HorizontalAlign="Center"
                                            Styles-Header-HorizontalAlign="Center"  StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                            Theme="Office2010Black" Width="100%" >                                                       
                                        <Columns>
                                    
                                            <dx:GridViewDataColumn Caption="Cliente" FieldName="Cliente" VisibleIndex="0" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Contrato" FieldName="Contrato" VisibleIndex="1" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Fecha de Inicio" FieldName="FechaInicio" VisibleIndex="2" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Fecha de Vencimiento" FieldName="FechaVencimiento" VisibleIndex="3" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Dias Restantes <br/> Fin de Contrato" FieldName="DiasRestanFinContrato" VisibleIndex="4" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>                                            
                                         </Columns>
                                 <Settings ShowGroupPanel="True" ShowFooter="True"/>
                                 <SettingsPager Position="TopAndBottom" Visible="True" PageSize="10">
                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                    </PageSizeItemSettings>
                                 </SettingsPager>
                                 <SettingsSearchPanel Visible="true" />
                                 <SettingsBehavior ConfirmDelete="True" />
                                 <SettingsEditing Mode="Inline"></SettingsEditing>
                                 <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                 <SettingsCommandButton>
                                     <UpdateButton Text="Guardar">
                                     </UpdateButton>
                                     <CancelButton></CancelButton>
                                     <EditButton>
                                         <Image Height="20px" ToolTip="Modificar" Width="20px">
                                         </Image>
                                     </EditButton>
                                     <DeleteButton>
                                         <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                         </Image>
                                     </DeleteButton>
                                 </SettingsCommandButton>
                                 </dx:ASPxGridView>

                                         <dx:ASPxGridViewExporter ID="ExporterVencimientoContratos" runat="server" GridViewID="gvVencimientoContratos">
                                        </dx:ASPxGridViewExporter>

                                     </ContentTemplate>
                                     <Triggers>
                                         <asp:PostBackTrigger ControlID="btnExportarVencimientoContratos" />
                                     </Triggers>
                                 </asp:UpdatePanel>                               
                                
                                </div>
                                <br />
                                <br />
                                <table width="100%">
                                     <tr>
                                         <td style="padding-left:20px;">
                                             <br />
                                             <dx:ASPxButton Theme="Office2010Black" ID="ASPxButton1" runat="server" Text="Salir" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popupVencimientoContratos.Hide(); }" />
                                        </dx:ASPxButton>
                                         </td>
                                        <td style="text-align:right; padding-right:20px;">
                                            <br />
                                             <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                                            &nbsp;<dx:ASPxButton ID="btnExportarVencimientoContratos" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarVencimientoContratos_Click" ></dx:ASPxButton>
                                        </td>
                                    </tr>
                                 </table>

                            </div>                                                                                   
                            <br />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="popupBitacorasSinRemisionar" runat="server" ClientInstanceName="popupBitacorasSinRemisionar" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black" 
        PopupHorizontalAlign="WindowCenter" ScrollBars="Vertical" PopupVerticalAlign="Above" HeaderText="Bitácoras Sin Remisionar" AllowDragging="true" ShowCloseButton="true" Width="1200" Height="590">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel4" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-md-12">

                                    <fieldset class="Personal">
                                        <br />
                                        <div class="col-sm-12">                                                                                                                                 
                                        </div>                                                                                                                                                              
                                    </fieldset>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">                                    
                                 <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                     <ContentTemplate>
                                         <dx:ASPxGridView ID="gvBitatorasSinRemisionar" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                            ClientInstanceName="gvBitatorasSinRemisionar" EnableTheming="True" KeyFieldName="IdPax" Styles-Cell-HorizontalAlign="Center"
                                            Styles-Header-HorizontalAlign="Center"  StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                            Theme="Office2010Black" Width="100%" >                                                       
                                        <Columns>
                                    
                                            <dx:GridViewDataColumn Caption="Bitácora" FieldName="Bitacora" VisibleIndex="0" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Fecha" FieldName="Fecha" VisibleIndex="1" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Cliente" FieldName="Cliente" VisibleIndex="2" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>                                            
                                            <dx:GridViewDataColumn Caption="Matrícula" FieldName="Matricula" VisibleIndex="5" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Estatus" FieldName="Estatus" VisibleIndex="5" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                         </Columns>
                                 <Settings ShowGroupPanel="True" ShowFooter="True"/>
                                 <SettingsPager Position="TopAndBottom" Visible="True" PageSize="10">
                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                    </PageSizeItemSettings>
                                 </SettingsPager>
                                 <SettingsSearchPanel Visible="true" />
                                 <SettingsBehavior ConfirmDelete="True" />
                                 <SettingsEditing Mode="Inline"></SettingsEditing>
                                 <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                 <SettingsCommandButton>
                                     <UpdateButton Text="Guardar">
                                     </UpdateButton>
                                     <CancelButton></CancelButton>
                                     <EditButton>
                                         <Image Height="20px" ToolTip="Modificar" Width="20px">
                                         </Image>
                                     </EditButton>
                                     <DeleteButton>
                                         <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                         </Image>
                                     </DeleteButton>
                                 </SettingsCommandButton>
                                 </dx:ASPxGridView>

                                         <dx:ASPxGridViewExporter ID="ExporterBitatorasSinRemisionar" runat="server" GridViewID="gvBitatorasSinRemisionar">
                                        </dx:ASPxGridViewExporter>

                                     </ContentTemplate>
                                     <Triggers>
                                         <asp:PostBackTrigger ControlID="btnExportarBitatorasSinRemisionar" />
                                     </Triggers>
                                 </asp:UpdatePanel>                               
                                
                                </div>
                                <br />
                                <br />
                                <table width="100%">
                                     <tr>
                                         <td style="padding-left:20px;">
                                             <br />
                                             <dx:ASPxButton Theme="Office2010Black" ID="ASPxButton2" runat="server" Text="Salir" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popupBitacorasSinRemisionar.Hide(); }" />
                                        </dx:ASPxButton>
                                         </td>
                                        <td style="text-align:right; padding-right:20px;">
                                            <br />
                                             <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                                            &nbsp;<dx:ASPxButton ID="btnExportarBitatorasSinRemisionar" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarBitatorasSinRemisionar_Click" ></dx:ASPxButton>
                                        </td>
                                    </tr>
                                 </table>

                            </div>                                                                                   
                            <br />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="popupRemisionesSinPrefacturar" runat="server" ClientInstanceName="popupRemisionesSinPrefacturar" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black" 
        PopupHorizontalAlign="WindowCenter" ScrollBars="Vertical" PopupVerticalAlign="Above" HeaderText="Remisiones sin Prefacturar" AllowDragging="true" ShowCloseButton="true" Width="1200" Height="590">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel5" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-md-12">

                                    <fieldset class="Personal">
                                        <br />
                                        <div class="col-sm-12">                                                                                                                                 
                                        </div>                                                                                                                                                              
                                    </fieldset>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">                                    
                                 <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                     <ContentTemplate>
                                         <dx:ASPxGridView ID="gvRemisionesSinPrefacturar" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                            ClientInstanceName="gvRemisionesSinPrefacturar" EnableTheming="True" KeyFieldName="IdPax" Styles-Cell-HorizontalAlign="Center"
                                            Styles-Header-HorizontalAlign="Center"  StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                            Theme="Office2010Black" Width="100%" >                                                       
                                        <Columns>
                                    
                                            <dx:GridViewDataColumn Caption="Cliente" FieldName="Clientes" VisibleIndex="0" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Contrato" FieldName="Contrato" VisibleIndex="1" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="NoFlightpack" FieldName="NoFlightpack" VisibleIndex="2" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Solicitud de Vuelo" FieldName="NoSolVuelo" VisibleIndex="3" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Fecha de Vuelo" FieldName="FechaVuelo" VisibleIndex="4" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Bitácora" FieldName="Bitacora" VisibleIndex="5" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Remision" FieldName="Remision" VisibleIndex="5" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Estatus" FieldName="Estatus" VisibleIndex="5" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="EstatusD" FieldName="EstatusD" VisibleIndex="5" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                         </Columns>
                                 <Settings ShowGroupPanel="True" ShowFooter="True"/>
                                 <SettingsPager Position="TopAndBottom" Visible="True" PageSize="10">
                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                    </PageSizeItemSettings>
                                 </SettingsPager>
                                 <SettingsSearchPanel Visible="true" />
                                 <SettingsBehavior ConfirmDelete="True" />
                                 <SettingsEditing Mode="Inline"></SettingsEditing>
                                 <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                 <SettingsCommandButton>
                                     <UpdateButton Text="Guardar">
                                     </UpdateButton>
                                     <CancelButton></CancelButton>
                                     <EditButton>
                                         <Image Height="20px" ToolTip="Modificar" Width="20px">
                                         </Image>
                                     </EditButton>
                                     <DeleteButton>
                                         <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                         </Image>
                                     </DeleteButton>
                                 </SettingsCommandButton>
                                 </dx:ASPxGridView>

                                         <dx:ASPxGridViewExporter ID="ExporterRemisionesSinPrefacturar" runat="server" GridViewID="gvRemisionesSinPrefacturar">
                                        </dx:ASPxGridViewExporter>

                                     </ContentTemplate>
                                     <Triggers>
                                         <asp:PostBackTrigger ControlID="btnExportarRemisionesSinPrefacturar" />
                                     </Triggers>
                                 </asp:UpdatePanel>                               
                                
                                </div>
                                <br />
                                <br />
                                <table width="100%">
                                     <tr>
                                         <td style="padding-left:20px;">
                                             <br />
                                             <dx:ASPxButton Theme="Office2010Black" ID="ASPxButton3" runat="server" Text="Salir" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popupRemisionesSinPrefacturar.Hide(); }" />
                                        </dx:ASPxButton>
                                         </td>
                                        <td style="text-align:right; padding-right:20px;">
                                            <br />
                                             <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                                            &nbsp;<dx:ASPxButton ID="btnExportarRemisionesSinPrefacturar" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarRemisionesSinPrefacturar_Click" ></dx:ASPxButton>
                                        </td>
                                    </tr>
                                 </table>

                            </div>                                                                                   
                            <br />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="popupPrefacturasSinFacturar" runat="server" ClientInstanceName="popupPrefacturasSinFacturar" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black" 
        PopupHorizontalAlign="WindowCenter" ScrollBars="Vertical" PopupVerticalAlign="Above" HeaderText="Prefacturas sin Facturar" AllowDragging="true" ShowCloseButton="true" Width="1200" Height="590">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel6" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-md-12">

                                    <fieldset class="Personal">
                                        <br />
                                        <div class="col-sm-12">                                                                                                                                 
                                        </div>                                                                                                                                                              
                                    </fieldset>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">                                    
                                 <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                     <ContentTemplate>
                                         <dx:ASPxGridView ID="gvPrefacturasSinFacturar" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                            ClientInstanceName="gvPrefacturasSinFacturar" EnableTheming="True" KeyFieldName="IdPax" Styles-Cell-HorizontalAlign="Center"
                                            Styles-Header-HorizontalAlign="Center"  StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                            Theme="Office2010Black" Width="100%" >                                                       
                                        <Columns>
                                    
                                            <dx:GridViewDataColumn Caption="Cliente" FieldName="Cliente" VisibleIndex="0" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Contrato" FieldName="Contrato" VisibleIndex="1" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Vuelo" FieldName="Vuelo" VisibleIndex="2" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Bitacora" FieldName="Bitacora" VisibleIndex="3" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Prefactura" FieldName="Prefactura" VisibleIndex="4" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Fecha" FieldName="Fecha" VisibleIndex="5" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Importe" FieldName="Importe" VisibleIndex="6" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="EstatusD" FieldName="EstatusD" VisibleIndex="5" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                         </Columns>
                                 <Settings ShowGroupPanel="True" ShowFooter="True"/>
                                 <SettingsPager Position="TopAndBottom" Visible="True" PageSize="10">
                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                    </PageSizeItemSettings>
                                 </SettingsPager>
                                 <SettingsSearchPanel Visible="true" />
                                 <SettingsBehavior ConfirmDelete="True" />
                                 <SettingsEditing Mode="Inline"></SettingsEditing>
                                 <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                 <SettingsCommandButton>
                                     <UpdateButton Text="Guardar">
                                     </UpdateButton>
                                     <CancelButton></CancelButton>
                                     <EditButton>
                                         <Image Height="20px" ToolTip="Modificar" Width="20px">
                                         </Image>
                                     </EditButton>
                                     <DeleteButton>
                                         <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                         </Image>
                                     </DeleteButton>
                                 </SettingsCommandButton>
                                 </dx:ASPxGridView>

                                         <dx:ASPxGridViewExporter ID="ExporterPrefacturasSinFacturar" runat="server" GridViewID="gvPrefacturasSinFacturar">
                                        </dx:ASPxGridViewExporter>

                                     </ContentTemplate>
                                     <Triggers>
                                         <asp:PostBackTrigger ControlID="btnExportarPrefacturasSinFacturar" />
                                     </Triggers>
                                 </asp:UpdatePanel>                               
                                
                                </div>
                                <br />
                                <br />
                                <table width="100%">
                                     <tr>
                                         <td style="padding-left:20px;">
                                             <br />
                                             <dx:ASPxButton Theme="Office2010Black" ID="ASPxButton4" runat="server" Text="Salir" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popupPrefacturasSinFacturar.Hide(); }" />
                                        </dx:ASPxButton>
                                         </td>
                                        <td style="text-align:right; padding-right:20px;">
                                            <br />
                                             <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                                            &nbsp;<dx:ASPxButton ID="btnExportarPrefacturasSinFacturar" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarPrefacturasSinFacturar_Click" ></dx:ASPxButton>
                                        </td>
                                    </tr>
                                 </table>

                            </div>                                                                                   
                            <br />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <%--inicia el detalle de los indicadores de finanzas en modales para Descuentos--%>


     <dx:ASPxPopupControl ID="popupFinanzaDescuentos" runat="server" ClientInstanceName="popupFinanzaDescuentos" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black" 
        PopupHorizontalAlign="WindowCenter" ScrollBars="Vertical" PopupVerticalAlign="Above" HeaderText="Descuentos" AllowDragging="true" ShowCloseButton="true" Width="1200" Height="590">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel11" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-md-12">

                                    <fieldset class="Personal">
                                        <br />
                                        <div class="col-sm-12">                                                                                                                                 
                                        </div>                                                                                                                                                              
                                    </fieldset>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">                                    
                                 <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                     <ContentTemplate>
                                         <dx:ASPxGridView ID="gvFinanzaDescuentos" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                            ClientInstanceName="gvFinanzaDescuentos" EnableTheming="True" KeyFieldName="IdPax" Styles-Cell-HorizontalAlign="Center"
                                            Styles-Header-HorizontalAlign="Center"  StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                            Theme="Office2010Black" Width="100%" >                                                       
                                        <Columns>
                                    
                                            <dx:GridViewDataColumn Caption="Tipo" FieldName="Tipo" VisibleIndex="0" Visible="false">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Remision" FieldName="Remision" VisibleIndex="1" Visible="true" CellStyle-HorizontalAlign="Right">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                                <CellStyle HorizontalAlign="Center"></CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Cliente" FieldName="Cliente" VisibleIndex="2" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Contratos" FieldName="Contratos" VisibleIndex="3" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTimeEditColumn Caption="FechaVuelo" FieldName="FechaVuelo" VisibleIndex="4" Visible="true">                                                                                                                
                                                <PropertiesTimeEdit DisplayFormatString="dd-MM-yyyy HH:mm"></PropertiesTimeEdit>
                                            </dx:GridViewDataTimeEditColumn>                                             
                                            <dx:GridViewDataColumn Caption="Horas" FieldName="Horas" VisibleIndex="5" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn Caption="Importe" FieldName="Importe" VisibleIndex="6" Visible="true" HeaderStyle-HorizontalAlign="Center">                                                                
                                                <PropertiesTextEdit DisplayFormatString="{0:c2}"></PropertiesTextEdit>                                                                                                             
                                                <CellStyle HorizontalAlign ="Center"></CellStyle>
                                            </dx:GridViewDataTextColumn>                                           
                                            <dx:GridViewDataColumn Caption="Vendedor" FieldName="Vendedor" VisibleIndex="7" Visible="false">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="NivelAutorizacion" FieldName="NivelAutorizacion" VisibleIndex="8" Visible="false">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Notas" FieldName="Notas" VisibleIndex="9" Visible="false">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                         </Columns>
                                 <Settings ShowGroupPanel="True" ShowFooter="True"/>
                                 <SettingsPager Position="TopAndBottom" Visible="True" PageSize="10">
                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                    </PageSizeItemSettings>
                                 </SettingsPager>
                                 <SettingsSearchPanel Visible="true" />
                                 <SettingsBehavior ConfirmDelete="True" />
                                 <SettingsEditing Mode="Inline"></SettingsEditing>
                                 <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                 <SettingsCommandButton>
                                     <UpdateButton Text="Guardar">
                                     </UpdateButton>
                                     <CancelButton></CancelButton>
                                     <EditButton>
                                         <Image Height="20px" ToolTip="Modificar" Width="20px">
                                         </Image>
                                     </EditButton>
                                     <DeleteButton>
                                         <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                         </Image>
                                     </DeleteButton>
                                 </SettingsCommandButton>
                                 </dx:ASPxGridView>

                                         <dx:ASPxGridViewExporter ID="ExporterFinanzaDescuentos" runat="server" GridViewID="gvFinanzaDescuentos">
                                        </dx:ASPxGridViewExporter>

                                     </ContentTemplate>
                                     <Triggers>
                                         <asp:PostBackTrigger ControlID="btnExportaFinanzaDescuentos" />
                                     </Triggers>
                                 </asp:UpdatePanel>                               
                                
                                </div>
                                <br />
                                <br />
                                <table width="100%">
                                     <tr>
                                         <td style="padding-left:20px;">
                                             <br />
                                             <dx:ASPxButton Theme="Office2010Black" ID="ASPxButton7" runat="server" Text="Salir" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popupFinanzaDescuentos.Hide(); }" />
                                        </dx:ASPxButton>
                                         </td>
                                        <td style="text-align:right; padding-right:20px;">
                                            <br />
                                             <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                                            &nbsp;<dx:ASPxButton ID="btnExportaFinanzaDescuentos" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportaFinanzaDescuentos_Click" ></dx:ASPxButton>
                                        </td>
                                    </tr>
                                 </table>

                            </div>                                                                                   
                            <br />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>


    <%--Inicia el detalle de contratos--%>

     <dx:ASPxPopupControl ID="popupEstatusContratosDetalle" runat="server" ClientInstanceName="popupEstatusContratosDetalle" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black" 
        PopupHorizontalAlign="WindowCenter" ScrollBars="Vertical" PopupVerticalAlign="Above" HeaderText="Estatus de Contratos" AllowDragging="true" ShowCloseButton="true" Width="1200" Height="590">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel7" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-md-12">

                                    <fieldset class="Personal">
                                        <br />
                                        <div class="col-sm-12">                                                                                                                                 
                                        </div>                                                                                                                                                              
                                    </fieldset>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">                                    
                                 <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                     <ContentTemplate>
                                         <dx:ASPxGridView ID="gvEstatusContratosDetalle" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                            ClientInstanceName="gvEstatusContratosDetalle" EnableTheming="True" KeyFieldName="IdPax" Styles-Cell-HorizontalAlign="Center"
                                            Styles-Header-HorizontalAlign="Center"  StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                            Theme="Office2010Black" Width="100%" >                                                       
                                        <Columns>
                                    
                                            <dx:GridViewDataColumn Caption="Cliente" FieldName="Cliente" VisibleIndex="0" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Contrato" FieldName="Contrato" VisibleIndex="1" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Estatus" FieldName="Estatus" VisibleIndex="2" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Fecha de Vencimiento" FieldName="FechaVencimiento" VisibleIndex="3" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Base" FieldName="Base" VisibleIndex="4" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>                                            
                                         </Columns>
                                 <Settings ShowGroupPanel="True" ShowFooter="True"/>
                                 <SettingsPager Position="TopAndBottom" Visible="True" PageSize="10">
                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                    </PageSizeItemSettings>
                                 </SettingsPager>
                                 <SettingsSearchPanel Visible="true" />
                                 <SettingsBehavior ConfirmDelete="True" />
                                 <SettingsEditing Mode="Inline"></SettingsEditing>
                                 <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                 <SettingsCommandButton>
                                     <UpdateButton Text="Guardar">
                                     </UpdateButton>
                                     <CancelButton></CancelButton>
                                     <EditButton>
                                         <Image Height="20px" ToolTip="Modificar" Width="20px">
                                         </Image>
                                     </EditButton>
                                     <DeleteButton>
                                         <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                         </Image>
                                     </DeleteButton>
                                 </SettingsCommandButton>
                                 </dx:ASPxGridView>

                                         <dx:ASPxGridViewExporter ID="ExporterEstatusContratosDetalle" runat="server" GridViewID="gvEstatusContratosDetalle">
                                        </dx:ASPxGridViewExporter>

                                     </ContentTemplate>
                                     <Triggers>
                                         <asp:PostBackTrigger ControlID="btnExportarEstatusContratosDetalle" />
                                     </Triggers>
                                 </asp:UpdatePanel>                               
                                
                                </div>
                                <br />
                                <br />
                                <table width="100%">
                                     <tr>
                                         <td style="padding-left:20px;">
                                             <br />
                                             <dx:ASPxButton Theme="Office2010Black" ID="ASPxButton5" runat="server" Text="Salir" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popupEstatusContratosDetalle.Hide(); }" />
                                        </dx:ASPxButton>
                                         </td>
                                        <td style="text-align:right; padding-right:20px;">
                                            <br />
                                             <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                                            &nbsp;<dx:ASPxButton ID="btnExportarEstatusContratosDetalle" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarEstatusContratosDetalle_Click" ></dx:ASPxButton>
                                        </td>
                                    </tr>
                                 </table>

                            </div>                                                                                   
                            <br />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>



     <%--Inicia el detalle de Operaciones--%>

     <dx:ASPxPopupControl ID="popupOperacionesIntercambios" runat="server" ClientInstanceName="popupOperacionesIntercambios" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black" 
        PopupHorizontalAlign="WindowCenter" ScrollBars="Vertical" PopupVerticalAlign="Above" HeaderText="Intercambios" AllowDragging="true" ShowCloseButton="true" Width="1200" Height="590">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel8" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-md-12">

                                    <fieldset class="Personal">
                                        <br />
                                        <div class="col-sm-12">                                                                                                                                 
                                        </div>                                                                                                                                                              
                                    </fieldset>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">                                    
                                 <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                     <ContentTemplate>
                                         <dx:ASPxGridView ID="gvOperacionesIntercambios" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                            ClientInstanceName="gvOperacionesIntercambios" EnableTheming="True" KeyFieldName="IdPax" Styles-Cell-HorizontalAlign="Center"
                                            Styles-Header-HorizontalAlign="Center"  StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                            Theme="Office2010Black" Width="100%" >                                                       
                                        <Columns>
                                    
                                            <dx:GridViewDataColumn Caption="FlightPack" FieldName="FlightPack" VisibleIndex="0" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Matrícula" FieldName="Matricula" VisibleIndex="1" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                                                                      
                                         </Columns>
                                 <Settings ShowGroupPanel="True" ShowFooter="True"/>
                                 <SettingsPager Position="TopAndBottom" Visible="True" PageSize="10">
                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                    </PageSizeItemSettings>
                                 </SettingsPager>
                                 <SettingsSearchPanel Visible="true" />
                                 <SettingsBehavior ConfirmDelete="True" />
                                 <SettingsEditing Mode="Inline"></SettingsEditing>
                                 <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                 <SettingsCommandButton>
                                     <UpdateButton Text="Guardar">
                                     </UpdateButton>
                                     <CancelButton></CancelButton>
                                     <EditButton>
                                         <Image Height="20px" ToolTip="Modificar" Width="20px">
                                         </Image>
                                     </EditButton>
                                     <DeleteButton>
                                         <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                         </Image>
                                     </DeleteButton>
                                 </SettingsCommandButton>
                                 </dx:ASPxGridView>

                                         <dx:ASPxGridViewExporter ID="ExporterOperacionesIntercambios" runat="server" GridViewID="gvOperacionesIntercambios">
                                        </dx:ASPxGridViewExporter>

                                     </ContentTemplate>
                                     <Triggers>
                                         <asp:PostBackTrigger ControlID="btnExportarOperacionesIntercambios" />
                                     </Triggers>
                                 </asp:UpdatePanel>                               
                                
                                </div>
                                <br />
                                <br />
                                <table width="100%">
                                     <tr>
                                         <td style="padding-left:20px;">
                                             <br />
                                             <dx:ASPxButton Theme="Office2010Black" ID="ASPxButton6" runat="server" Text="Salir" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popupOperacionesIntercambios.Hide(); }" />
                                        </dx:ASPxButton>
                                         </td>
                                        <td style="text-align:right; padding-right:20px;">
                                            <br />
                                             <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                                            &nbsp;<dx:ASPxButton ID="btnExportarOperacionesIntercambios" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarOperacionesIntercambios_Click" ></dx:ASPxButton>
                                        </td>
                                    </tr>
                                 </table>

                            </div>                                                                                   
                            <br />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

     <dx:ASPxPopupControl ID="popupOperacionRentasAeronaves" runat="server" ClientInstanceName="popupOperacionRentasAeronaves" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black" 
        PopupHorizontalAlign="WindowCenter" ScrollBars="Vertical" PopupVerticalAlign="Above" HeaderText="Rentas Aeronaves" AllowDragging="true" ShowCloseButton="true" Width="1200" Height="590">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel9" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-md-12">

                                    <fieldset class="Personal">
                                        <br />
                                        <div class="col-sm-12">                                                                                                                                 
                                        </div>                                                                                                                                                              
                                    </fieldset>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">                                    
                                 <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                     <ContentTemplate>
                                         <dx:ASPxGridView ID="gvOperacionRentas" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                            ClientInstanceName="gvOperacionRentas" EnableTheming="True" KeyFieldName="IdPax" Styles-Cell-HorizontalAlign="Center"
                                            Styles-Header-HorizontalAlign="Center"  StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                            Theme="Office2010Black" Width="100%" >                                                       
                                        <Columns>
                                    
                                            <dx:GridViewDataColumn Caption="FlightPack" FieldName="FlightPack" VisibleIndex="0" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Cliente" FieldName="Cliente" VisibleIndex="1" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>                                          
                                         </Columns>
                                 <Settings ShowGroupPanel="True" ShowFooter="True"/>
                                 <SettingsPager Position="TopAndBottom" Visible="True" PageSize="10">
                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                    </PageSizeItemSettings>
                                 </SettingsPager>
                                 <SettingsSearchPanel Visible="true" />
                                 <SettingsBehavior ConfirmDelete="True" />
                                 <SettingsEditing Mode="Inline"></SettingsEditing>
                                 <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                 <SettingsCommandButton>
                                     <UpdateButton Text="Guardar">
                                     </UpdateButton>
                                     <CancelButton></CancelButton>
                                     <EditButton>
                                         <Image Height="20px" ToolTip="Modificar" Width="20px">
                                         </Image>
                                     </EditButton>
                                     <DeleteButton>
                                         <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                         </Image>
                                     </DeleteButton>
                                 </SettingsCommandButton>
                                 </dx:ASPxGridView>

                                         <dx:ASPxGridViewExporter ID="ExporterOperacionRentas" runat="server" GridViewID="gvOperacionRentas">
                                        </dx:ASPxGridViewExporter>

                                     </ContentTemplate>
                                     <Triggers>
                                         <asp:PostBackTrigger ControlID="btnExpartarOperacionRentas" />
                                     </Triggers>
                                 </asp:UpdatePanel>                               
                                
                                </div>
                                <br />
                                <br />
                                <table width="100%">
                                     <tr>
                                         <td style="padding-left:20px;">
                                             <br />
                                             <dx:ASPxButton Theme="Office2010Black" ID="ASPxButton8" runat="server" Text="Salir" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popupOperacionRentasAeronaves.Hide(); }" />
                                        </dx:ASPxButton>
                                         </td>
                                        <td style="text-align:right; padding-right:20px;">
                                            <br />
                                             <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                                            &nbsp;<dx:ASPxButton ID="btnExpartarOperacionRentas" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExpartarOperacionRentas_Click"></dx:ASPxButton>
                                        </td>
                                    </tr>
                                 </table>

                            </div>                                                                                   
                            <br />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
     
     <dx:ASPxPopupControl ID="popupVueloRentas" runat="server" ClientInstanceName="popupVueloRentas" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black" 
        PopupHorizontalAlign="WindowCenter" ScrollBars="Vertical" PopupVerticalAlign="Above" HeaderText="Vuelos Ventas" AllowDragging="true" ShowCloseButton="true" Width="1200" Height="590">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel10" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-md-12">

                                    <fieldset class="Personal">
                                        <br />
                                        <div class="col-sm-12">                                                                                                                                 
                                        </div>                                                                                                                                                              
                                    </fieldset>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">                                    
                                 <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                     <ContentTemplate>
                                         <dx:ASPxGridView ID="gvVueloRentas" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                            ClientInstanceName="gvVueloRentas" EnableTheming="True" KeyFieldName="IdPax" Styles-Cell-HorizontalAlign="Center"
                                            Styles-Header-HorizontalAlign="Center"  StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                            Theme="Office2010Black" Width="100%" >                                                       
                                        <Columns>
                                    
                                            <dx:GridViewDataColumn Caption="FlightPack" FieldName="FlightPack" VisibleIndex="0" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Matrícula" FieldName="Matricula" VisibleIndex="1" Visible="true">                                                                
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataColumn>                                            
                                         </Columns>
                                 <Settings ShowGroupPanel="True" ShowFooter="True"/>
                                 <SettingsPager Position="TopAndBottom" Visible="True" PageSize="10">
                                    <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                    </PageSizeItemSettings>
                                 </SettingsPager>
                                 <SettingsSearchPanel Visible="true" />
                                 <SettingsBehavior ConfirmDelete="True" />
                                 <SettingsEditing Mode="Inline"></SettingsEditing>
                                 <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                 <SettingsCommandButton>
                                     <UpdateButton Text="Guardar">
                                     </UpdateButton>
                                     <CancelButton></CancelButton>
                                     <EditButton>
                                         <Image Height="20px" ToolTip="Modificar" Width="20px">
                                         </Image>
                                     </EditButton>
                                     <DeleteButton>
                                         <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                         </Image>
                                     </DeleteButton>
                                 </SettingsCommandButton>
                                 </dx:ASPxGridView>

                                         <dx:ASPxGridViewExporter ID="ExporterVueloRentas" runat="server" GridViewID="gvVueloRentas">
                                        </dx:ASPxGridViewExporter>

                                     </ContentTemplate>
                                     <Triggers>
                                         <asp:PostBackTrigger ControlID="btnExportarVueloRentas" />
                                     </Triggers>
                                 </asp:UpdatePanel>                               
                                
                                </div>
                                <br />
                                <br />
                                <table width="100%">
                                     <tr>
                                         <td style="padding-left:20px;">
                                             <br />
                                             <dx:ASPxButton Theme="Office2010Black" ID="ASPxButton10" runat="server" Text="Salir" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popupVueloRentas.Hide(); }" />
                                        </dx:ASPxButton>
                                         </td>
                                        <td style="text-align:right; padding-right:20px;">
                                            <br />
                                             <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                                            &nbsp;<dx:ASPxButton ID="btnExportarVueloRentas" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportarVueloRentas_Click" ></dx:ASPxButton>
                                        </td>
                                    </tr>
                                 </table>

                            </div>                                                                                   
                            <br />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>


</asp:Content>
