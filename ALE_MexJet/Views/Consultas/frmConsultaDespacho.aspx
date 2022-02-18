﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmConsultaDespacho.aspx.cs" Inherits="ALE_MexJet.Views.Consultas.frmConsultaDespacho" UICulture="es" Culture="es-MX"  %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Src="~/ControlesUsuario/ucClienteContrato.ascx" TagPrefix="uc2" TagName="ucContratos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
               <%-- <asp:Timer runat="server" ID="UpdateTimer" Interval="35000" OnTick="UpdateTimer_Tick" />--%>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 25px;">&nbsp;&nbsp;Consulta de Despacho</span>
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
                                        <td style="width:25%; ">
                                            <uc2:ucContratos ID="cuClienteContrato" runat="server" Theme="Office2010Black"/>                                            
                                        </td>
                                        <td>
                                           <dx:ASPxDateEdit ID="deFechaInicial" runat="server" Caption ="Desde " NullText="Seleccione una fecha" Theme="Office2010Black"></dx:ASPxDateEdit>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="deFechaFinal" runat="server" Caption="Hasta " NullText="Seleccione una fecha" Theme="Office2010Black"></dx:ASPxDateEdit>
                                        </td>
                                        <td style="width: 20% ">
                                            <dx:ASPxButton CssClass="FBotton" ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" Theme="Office2010Black">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>   
                                    <tr>
                                        <td style="width:25%; ">
                                            <br />                                            
                                            <dx:ASPxComboBox ID="cmdEstatus" runat="server" Font-Bold="false" Theme="Office2010Black" NullText="Seleccione un estatus" Caption="Estatus:" DropDownStyle="DropDownList">
                                            <Items>  
                                                
                                                <dx:ListEditItem Text="Todos" Value="4" />                                              
                                                <dx:ListEditItem Text="Viable" Value="1" />
                                                <dx:ListEditItem Text="No Viable" Value="2" />
                                                <dx:ListEditItem Text="Restringido" value="3"/>
                                                <dx:ListEditItem Text="Cancelado" value="0"/>                                                
                                               
                                            </Items>
                                            </dx:ASPxComboBox>                                                
                                        </td>
                                        <td>
                                            
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
                            <dx:ASPxLabel CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                            &nbsp;<dx:ASPxButton CssClass="FBotton" ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExcel_Click" ></dx:ASPxButton>
                        </div>
                    </div>
                 

                <div class="well-g">
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="margin-left: -15px; width: 103%;">
                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="UpdatePanel1_Unload">
                                <ContentTemplate>
                                    <div class="col-sm-12">
                                        <dx:ASPxGridView CssClass="FGrid" ID="gvConsultaDespacho" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvConsultaDespacho" EnableTheming="True"
                                            Styles-Header-HorizontalAlign="Center" Theme="Office2010Black" Width="100%"  KeyFieldName="Solicitud"  OnRowCommand="gvConsultaDespacho_RowCommand">
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Solicitud" FieldName="Solicitud" VisibleIndex="0" Visible="true" CellStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Contrato" FieldName="Contrato" VisibleIndex="1" Visible="true" CellStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Tipo <br /> Aeronave" FieldName="TipoAeronave" VisibleIndex="2" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="BASE" FieldName="BASE" VisibleIndex="3" Visible="true" CellStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Origen <br /> Solicitud" FieldName="OrigenSolicitud" VisibleIndex="4" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Usuario <br /> Registro" FieldName="UsuarioValido" VisibleIndex="5" Visible="false">
                                                </dx:GridViewDataColumn>                                                                                              
                                                 <dx:GridViewDataColumn FieldName="Estatus"  VisibleIndex="6" Caption="Estatus" HeaderStyle-HorizontalAlign="Center"> 
                                                <DataItemTemplate>
                                                <dx:ASPxImage runat="server" ID="imgTemplate" Width="15px" Height="15px" ImageAlign="AbsMiddle"
                                                    ImageUrl='<%# "~/img/iconos/" + Eval("EstatusImg") %>'>
                                                </dx:ASPxImage>                                                 
                                                <dx:ASPxLabel runat="server" ID="lblStatus" Text='<%#Eval("Estatus")%>' CssClass="FGrid" ></dx:ASPxLabel>
                                                </DataItemTemplate>
                                                </dx:GridViewDataColumn>                                                                                                   
                                                <dx:GridViewDataDateColumn Caption="Fecha Creacion" FieldName="FechaCreacion" VisibleIndex="7" Visible="true">
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn> 
                                                <dx:GridViewDataDateColumn Caption="Fecha <br /> Respuesta" FieldName="FechaRespuesta" VisibleIndex="8" Visible="true">
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>             
                                                <dx:GridViewDataColumn Caption="Tiempo <br /> Respuesta" FieldName="TiempoRespuesta" VisibleIndex="9" Visible="true">
                                                </dx:GridViewDataColumn>                                         
                                                <dx:GridViewDataColumn Caption="Usuario <br /> Viabilidad" FieldName="UsuarioViabilidad" VisibleIndex="10" Visible="true">
                                                </dx:GridViewDataColumn>                                                                                              
                                                <dx:GridViewDataColumn Caption="Acciones" VisibleIndex="11" Width="100px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxButton Text="Editar" Theme="Office2010Black" ID="btnEditar" ClientInstanceName="btnEditar" CommandArgument='<%# Eval("Solicitud") %>' runat="server" CommandName="Editar" AutoPostBack="true" ToolTip="Editar">
                                                        </dx:ASPxButton>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>                                            
                                            <StylesPager  Pager-CssClass="FNumPag"></StylesPager>
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="true" />
                                            <Settings ShowGroupPanel="True" />
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvConsultaDespacho" ></dx:ASPxGridViewExporter>
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
    
    <dx:ASPxPopupControl ID="ppDictamen" runat="server" ClientInstanceName="ppDictamen" Text="" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true" Width="700px">
        <ClientSideEvents />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div>
                                <table>
                                     <tr>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Contacto:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo runat="server" ID="txtContacto" Theme="Office2010Black"></dx:ASPxMemo>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Motivo:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo runat="server" ID="txtMotivo" Theme="Office2010Black"></dx:ASPxMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 15px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Tipo Equipo:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo runat="server" ID="txtTipoEquipo" Theme="Office2010Black" ></dx:ASPxMemo>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Matricula:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo runat="server" ID="txtMatricula" Theme="Office2010Black" ></dx:ASPxMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 15px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Notas Solicitud:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo ID="mNotasSolicitud" ClientInstanceName="mNotasSolicitud" runat="server" Height="100px" Width="90%" ></dx:ASPxMemo>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="No Solicitud: " Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel runat="server" ID="lblSolicitud"  Font-Bold="true" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 15px"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <dx:ASPxGridView ID="gvPiernaDic" ClientInstanceName="gvPiernaDic" runat="server"
                                                KeyFieldName="idSolicitud" Width="650px" Theme="Office2010Black">
                                                <Columns>
                                                    <dx:GridViewDataColumn Caption="Origen" FieldName="Origen" VisibleIndex="0" />
                                                    <dx:GridViewDataColumn Caption="Destino" FieldName="Destino" VisibleIndex="1" />
                                                    <dx:GridViewDataColumn Caption="Fecha" FieldName="Fecha" VisibleIndex="2" />
                                                    <dx:GridViewDataColumn Caption="ETD" FieldName="HoraVuelo" VisibleIndex="3" />
                                                    <dx:GridViewDataColumn Caption="No. Pasajeros" FieldName="NoPax" VisibleIndex="4" />
                                                    <dx:GridViewDataColumn Caption="Ultima Area que agrego Pax" FieldName="AreaAgregaPax" VisibleIndex="5" />
                                                </Columns>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 15px"></td>
                                    </tr>
                                </table>
                            </div>

                            <table width="100%" border="0">
                                <tr>
                                    <td style="text-align: left;">
                                        <dx:ASPxLabel runat="server" Text="Es viable el vuelo:" Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td style="text-align: left;">
                                        <dx:ASPxRadioButtonList ID="rblDictamen" runat="server" Theme="Office2010Black" ToolTip="Otorgado"
                                            ClientInstanceName="rblDictamen" RepeatDirection="Horizontal">
                                            <Items>
                                                <dx:ListEditItem Text="Si" Value="1" />
                                                <dx:ListEditItem Text="No" Value="2" />
                                                <dx:ListEditItem Text="Restringido" Value="3" />
                                            </Items>
                                        </dx:ASPxRadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 15px"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel runat="server" Text="Observaciones:" Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxMemo ID="mObservaiones" ClientInstanceName="mObservaiones" runat="server" Height="100px" Width="100%"></dx:ASPxMemo>
                                    </td>
                                </tr>
                            </table>
                            <div class="col-md-12">
                                <table width="100%">
                                    <tr>
                                        <td style="height: 15px"></td>
                                        <td width="50%" align="center">
                                            <dx:ASPxButton runat="server" ID="btnGuardarD" Text="Guardar" Theme="Office2010Black" OnClick="btnGuardarD_Click">
                                                <ClientSideEvents Click="function(){  ppDictamen.Hide(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td width="50%" align="center">
                                            <dx:ASPxButton runat="server" ClientInstanceName="btnSalir" Text="Salir" Theme="Office2010Black" ID="btnSalir">
                                                <ClientSideEvents Click="function(){  ppDictamen.Hide(); popup.Hide();  ppNotas.Hide();  mNotas.SetText('');  mObservaiones.SetText(''); rblDictamen.SetValue(-1); }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <br />

                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

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
                                            <ClientSideEvents Click="function(){  ppDictamen.Hide(); popup.Hide();  ppNotas.Hide();  mNotas.SetText('');  mObservaiones.SetText(''); rblDictamen.SetValue(-1); }" />
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
