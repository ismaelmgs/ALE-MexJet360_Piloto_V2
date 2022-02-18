<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmMonitorAtencionClientes.aspx.cs" Inherits="ALE_MexJet.Views.FBO.frmMonitorAtencionClientes" UICulture="es" Culture="es-MX" %>

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
                <asp:Timer runat="server" ID="UpdateTimer" Interval="35000" OnTick="UpdateTimer_Tick" />
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 25px;">&nbsp;&nbsp;Monitor Atención a Clientes</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <fieldset class="Personal">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                            </legend>
                            <div class="col-sm-12">
                                <table cellpsdding="5" width="99%" border="0">
                                    <tr>
                                        <td style="width: 20%"></td>
                                        <td>
                                            <dx:ASPxLabel CssClass="FExport" runat="server" Text="Aeropuerto Base:"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" OnUnload="UpdatePanel1_Unload">
                                                <ContentTemplate>
                                                    <dx:ASPxComboBox ToolTip="Aeropuerto Base" ID="ddlBase" runat="server" Theme="Office2010Black" EnableTheming="True" NullText="Seleccione la Base"
                                                        ValueType="System.Int32" ValueField="idAeropuert">
                                                        <ValidationSettings ErrorDisplayMode="Text">
                                                            
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 20%">
                                            <dx:ASPxButton CssClass="FBotton" ID="btnBuscar" runat="server"  Text="Buscar" OnClick="btnBuscar_Click"  Theme="Office2010Black">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 15px"></td>
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
                            <dx:ASPxLabel CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                            &nbsp;<dx:ASPxButton CssClass="FBotton" ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExcel_Click"  ></dx:ASPxButton>
                        </div>
                    </div>

                <div class="well-g">
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="margin-left: -15px; width: 103%;">
                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                <ContentTemplate>
                                    <div class="col-sm-12">
                                        <dx:ASPxGridView ID="gvMonitorAtencionClientes" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvMonitorAtencionClientes" EnableTheming="True" KeyFieldName="IdSolicitud"
                                            Styles-Header-HorizontalAlign="Center" Theme="Office2010Black" Width="100%" OnRowCommand="gvMonitorAtencionClientes_RowCommand">
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Solicitud" FieldName="IdSolicitud" VisibleIndex="0" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="TRIP" FieldName="TRIP" VisibleIndex="1" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Cliente" FieldName="CodigoCliente" VisibleIndex="2" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Contrato" FieldName="ClaveContrato" VisibleIndex="3" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Solicitante" FieldName="Membresia" VisibleIndex="4" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataDateColumn Caption="Fecha  &nbsp; &nbsp; &nbsp; &nbsp; ETD" FieldName="FechaVuelo" VisibleIndex="5" Visible="true" HeaderStyle-HorizontalAlign="Center" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy HH:mm" CellStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataDateColumn>                                               
                                                <dx:GridViewDataColumn Caption="ETD" FieldName="ETD" VisibleIndex="6" Visible="false">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Grupo de Modelo" FieldName="GrupoModelo" VisibleIndex="7" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Origen <br /> Solicitud" FieldName="OrigenSol" VisibleIndex="8" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="TRIP" FieldName="TRIP" VisibleIndex="9" Visible="false" Width="15%">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Estatus" FieldName="EstatusSolicitud" VisibleIndex="10" Visible="true">
                                                </dx:GridViewDataColumn>                                                

                                                <dx:GridViewDataColumn FieldName="Estatus"  VisibleIndex="11" Caption="Despacho" HeaderStyle-HorizontalAlign="Center"> 
                                                    <DataItemTemplate>                                                
                                                        <dx:ASPxImage runat="server" ID="imgTemplate" Width="15px" Height="15px" ImageAlign="AbsMiddle"
                                                            ImageUrl='<%# "~/img/iconos/" + Eval("DespachoEstatusImg") %>'>
                                                        </dx:ASPxImage>
                                                        <dx:ASPxLabel runat="server" ID="lblStatus" Text='<%#Eval("DespachoEstatus")%>' CssClass="FGrid" ></dx:ASPxLabel>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn FieldName="Estatus"  VisibleIndex="12" Caption="Tráfico" HeaderStyle-HorizontalAlign="Center"> 
                                                    <DataItemTemplate>                                                
                                                        <dx:ASPxImage runat="server" ID="imgTemplateT" Width="15px" Height="15px" ImageAlign="AbsMiddle"
                                                            ImageUrl='<%# "~/img/iconos/" + Eval("TraficoEstatusImg") %>'>
                                                        </dx:ASPxImage>
                                                        <dx:ASPxLabel runat="server" ID="lblStatusT" Text='<%#Eval("TraficoEstatus")%>' CssClass="FGrid" ></dx:ASPxLabel>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn Caption="Acciones">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                                    <DataItemTemplate>
                                                        <dx:ASPxButton Text="Viabilidad" Theme="Office2010Black" ID="btnViabilidad" runat="server" CommandArgument='<%# Eval("IdSolicitud") %>' CommandName="Viabilidad" AutoPostBack="true" ToolTip="Consulta Viabilidad">
                                                        </dx:ASPxButton>
                                                        <dx:ASPxButton Text="Notas Trafico" Theme="Office2010Black" ID="btnNotasTrafico" CommandArgument='<%# Eval("IdSolicitud") %>' runat="server" CommandName="NotasTrafico" AutoPostBack="true" ToolTip="Notas de Trafico" Visible="false">
                                                        </dx:ASPxButton>                                                       
                                                        <dx:ASPxButton Text="Visto Bueno" Theme="Office2010Black" ID="btnVistoBueno" CommandArgument='<%# Eval("IdSolicitud") %>' runat="server" CommandName="VistoBueno" AutoPostBack="true" ToolTip="Visto Bueno">
                                                        </dx:ASPxButton>
                                                        <dx:ASPxButton Text="Rechazar" Theme="Office2010Black" ID="btnRechazo" CommandArgument='<%# Eval("IdMonitor") %>' runat="server" CommandName="Rechazo" AutoPostBack="true" ToolTip="Rechaza una solicitud y la envía a Tráfico">
                                                        </dx:ASPxButton>                                                        
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                            <Templates>
                                                <DetailRow>
                                                    <dx:ASPxGridView CssClass="FGrid" ID="gvPiernas" ClientInstanceName="gvPiernas" runat="server"
                                                        KeyFieldName="idSolicitud" Width="50%" Theme="Office2010Black" OnBeforePerformDataSelect="gvPiernas_BeforePerformDataSelect">
                                                        <Columns>
                                                            
                                                            <dx:GridViewDataColumn Caption="Origen" FieldName="Origen" VisibleIndex="1" />
                                                            <dx:GridViewDataColumn Caption="Destino" FieldName="Destino" VisibleIndex="2" />
                                                            <dx:GridViewDataColumn Caption="Fecha de vuelo" FieldName="FechaVuelo" VisibleIndex="3" />                                                            
                                                            
                                                        </Columns>
                                                    </dx:ASPxGridView>
                                                </DetailRow>
                                            </Templates>
                                            <StylesPager  Pager-CssClass="FNumPag"></StylesPager>
                                            <SettingsDetail ShowDetailRow="true" />
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="true" />
                                            <Settings ShowGroupPanel="True" />
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvMonitorAtencionClientes" ></dx:ASPxGridViewExporter>
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
                            &nbsp;<dx:ASPxButton CssClass="FBotton" ID="btnExportar" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExcel_Click"></dx:ASPxButton>
                        </div>
                    </div>
                    <br />

                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

    <dx:ASPxPopupControl ID="popup" runat="server" ClientInstanceName="popup" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true" Width="300">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
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
                                        <dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px" Theme="Office2010Black" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popup.Hide(); ppRecibido.Hide(); }" />
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

    <dx:ASPxPopupControl ID="ppViabilidad" runat="server" ClientInstanceName="ppViabilidad" CloseAction="CloseButton" Width="600px" CloseOnEscape="true" Modal="true" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Notas Viabilidad">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel4" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-sm-12">
                                   
                                    <br />
                                </div>
                            </div>
                            <div>
                                 <table>                                    

                                    <tr>
                                        <td>
                                             <dx:ASPxLabel CssClass="FExport" runat="server" ClientInstanceName="lblPreferencias" Text="Notas:"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo ID="mPreferencias" ClientInstanceName="mPreferencias" runat="server" Height="71px" Width="500px">
                                                <ClientSideEvents  Init="function(){mPreferencias.SetEnabled(false);}"/>
                                            </dx:ASPxMemo>
                                        </td>
                                    </tr>
                                    </table>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />
                                    <dx:ASPxButton CssClass="FBotton" ClientInstanceName="btnSalir" runat="server" Text="Salir" Theme="Office2010Black">
                                        <ClientSideEvents Click="function() {ppViabilidad.Hide(); }" />
                                    </dx:ASPxButton>
                                    <br />

                                </div>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="ppNotasTrafico" runat="server" ClientInstanceName="ppNotasTrafico" CloseAction="CloseButton" Width="700px" CloseOnEscape="true" Modal="true" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Notas">
         <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />
                                    
                                   
                                    
                                    <br />
                                </div>
                            </div>
                            <div>
                                
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />
                                    <dx:ASPxButton CssClass="FBotton" ClientInstanceName="btnSalir" runat="server" Text="Aceptar" Theme="Office2010Black">
                                        <ClientSideEvents Click="function() {ppNotas.Hide(); }" />
                                    </dx:ASPxButton>
                                    <br />

                                </div>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="ppVistoBueno" runat="server" ClientInstanceName="ppVistoBueno" CloseAction="CloseButton" Width="400px" CloseOnEscape="true" Modal="true" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Confirmación">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel5" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />                                    
                                    <table>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <dx:ASPxLabel CssClass="FExport" ID="lblConfirmacion" runat="server" Text="¿Realmente está seguro de confirmar la solicitud?"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                    </tr>                                    
                                    </table>                                    
                                    <br />
                                </div>
                            </div>
                            <div>
                                
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />
                                    
                                    <dx:ASPxButton CssClass="FBotton" ClientInstanceName="btnConfirmar" ID="btnConfirmar" runat="server" Text="Aceptar" Theme ="Office2010Black" OnClick="btnConfirmar_Click" >
                                        <ClientSideEvents Click="function() {ppVistoBueno.Hide(); }" />
                                    </dx:ASPxButton>

                                    <dx:ASPxButton CssClass="FBotton" ClientInstanceName="btnSalir" runat="server" Text="Cancelar" Theme="Office2010Black">
                                        <ClientSideEvents Click="function() {ppVistoBueno.Hide(); }" />
                                    </dx:ASPxButton>
                                    
                                    <br />

                                </div>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    

    <%--MODAL PARA CONFIRMAR EL RECHAZO DE UNA SOLICITUD A TRAFICO--%>
    <dx:ASPxPopupControl ID="ppConfirmacion" runat="server" ClientInstanceName="ppConfirmacion" CloseAction="CloseButton" Width="400px" CloseOnEscape="true" Modal="true" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Confirmación">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel6" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />                                    
                                    <table style="width:100%">
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Motivo Rechazo:" Theme="Office2010Black" ></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo runat="server" ID="mRechazo" Width="200px" Height="100px">
                                                <ValidationSettings ErrorDisplayMode="Text" ValidationGroup="Rechazo"  ErrorTextPosition="Bottom" >
                                                    <RequiredField IsRequired="true"  ErrorText="El campo es requerido." />
                                                </ValidationSettings>
                                            </dx:ASPxMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                    </tr>                                    
                                    </table>                                    
                                    <br />
                                </div>
                            </div>
                            <div>
                                
                            </div>
                            <div >
                                <div >
                                    <br />
                                    
                                    <dx:ASPxButton ID="btnConfirmarpp" runat="server" Text="Aceptar" ClientInstanceName="btnConfirmarpp" 
                                        OnClick="btnConfirmarpp_Click" Theme ="Office2010Black" ValidationGroup="Rechazo">
                                        <ClientSideEvents Click="function() { }" />
                                      
                                    </dx:ASPxButton>

                                    <dx:ASPxButton ClientInstanceName="btnSalir" runat="server" Text="Cancelar" Theme="Office2010Black" >
                                        <ClientSideEvents Click="function() {  ppConfirmacion.Hide();  }"  />
                                    </dx:ASPxButton>
                                    
                                    <br />

                                </div>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

</asp:Content>
