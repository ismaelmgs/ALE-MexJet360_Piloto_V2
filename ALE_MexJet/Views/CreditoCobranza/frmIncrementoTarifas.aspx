<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmIncrementoTarifas.aspx.cs" Inherits="ALE_MexJet.Views.CreditoCobranza.frmIncrementoTarifas" UICulture="es" Culture="es-MX" MaintainScrollPositionOnPostback="true" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 25px;">&nbsp;&nbsp;Incremento de Tarifas</span>
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
                                        <td colspan="3">
                                            <dx:ASPxRadioButtonList ID="rblIncrementoTarifa" ClientInstanceName="rblIncrementoTarifa" runat="server" SelectedIndex="0"
                                                OnValueChanged="rblIncrementoTarifa_ValueChanged" AutoPostBack="true" RepeatDirection="Horizontal" >
                                                <Items>
                                                    <dx:ListEditItem Text="Especificados en enero" Value="1"  Selected="true"/>
                                                    <dx:ListEditItem Text="Especificados en Aniversario" Value="2" />                                                                                
                                                </Items>
                                            </dx:ASPxRadioButtonList> 
                                        </td>
                                        <td style="width: 20%"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%"></td>
                                        <td style="width: 12%; text-align:left" >
                                            <dx:ASPxLabel ID="lblMesIncremento" runat="server" Text="Incremento del mes:" Theme="Office2010Black" Visible="false"></dx:ASPxLabel>
                                        </td>
                                        <td style="width:2%"></td>
                                        <td style="width: 36%; text-align:left">
                                            <dx:ASPxComboBox ID="ddlMesIncremento" runat="server" Theme="Office2010Black" Visible="false">
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td style="width: 20%"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"></td>
                                        <td style="width: 20%">
                                            <dx:ASPxButton CssClass="FBotton" ID="btnCalcular" runat="server"  Text="Calcular" 
                                                OnClick="btnCalcular_Click" Theme="Office2010Black">
                                            </dx:ASPxButton>
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
                            &nbsp;<dx:ASPxButton CssClass="FBotton" ID="btnExportar" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportar_Click"  ></dx:ASPxButton>
                        </div>
                    </div>

                <div class="well-g">
                    <br />
                    <div class="row">
                        <div class="col-md-12" >
                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                <ContentTemplate>
                                    <div class="col-sm-12">
                                        <dx:ASPxGridView ID="gvIncrementoTarifas" runat="server" AutoGenerateColumns="False" KeyFieldName="Id"
                                            ClientInstanceName="gvIncrementoTarifas" EnableTheming="True" 
                                            Styles-Header-HorizontalAlign="Center" Theme="Office2010Black" Width="100%" >
                                            <Columns>
                                                <dx:GridViewCommandColumn  ShowSelectCheckbox="True" SelectAllCheckboxMode="AllPages" VisibleIndex="0" Caption="Seleccione">                                                    
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataColumn FieldName="ClaveCliente" Caption="Cliente" VisibleIndex="1" Visible="true" >
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="ClaveContrato" Caption="Contrato" VisibleIndex="2" Visible="true" >
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Concepto" Caption="Concepto" VisibleIndex="3" Visible="true" >
                                                </dx:GridViewDataColumn>                                                                                               
                                                <dx:GridViewDataColumn FieldName="ImporteO" Caption="Importe Actual" VisibleIndex="4" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="InflacionDesc" Caption="Inflacion a Aplicar" VisibleIndex="5" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="MasPuntos" Caption="Mas Puntos" VisibleIndex="6" Visible="true" >
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Tope" Caption="Tope" VisibleIndex="7" Visible="true" >
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Inflacion" Caption="% Inflacion" VisibleIndex="8" Visible="true" >
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="ImporteN" Caption="Nuevo Importe" VisibleIndex="9" Visible="true">
                                                </dx:GridViewDataColumn>                                                                                     
                                                <dx:GridViewDataColumn FieldName="IncrementoAplicado" Caption="Incremento Aplicado" VisibleIndex="9" Visible="true" CellStyle-HorizontalAlign="Center">                                                    
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
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvIncrementoTarifas" ></dx:ASPxGridViewExporter>
                                    </div>
                                </ContentTemplate>
                                <Triggers>                                    
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
                            <dx:ASPxLabel CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:" Visible="false"></dx:ASPxLabel>
                            &nbsp;<dx:ASPxButton CssClass="FBotton" ID="btnAplicarTarifas" runat="server" Text="Aplicar Tarifas" Theme="Office2010Black" OnClick="btnAplicarTarifas_Click" ></dx:ASPxButton>
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

    <dx:ASPxPopupControl ID="popupMsgConfirmacion" runat="server" ClientInstanceName="popupMsgConfirmacion" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true" Width="350">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxImage ID="ASPxImage1" runat="server" ShowLoadingImage="true" ImageUrl="~/img/iconos/Information2.ico"></dx:ASPxImage>
                                        <dx:ASPxTextBox ID="ASPxTextBox1" ReadOnly="true" Border-BorderStyle="None" Height="1px" runat="server" Width="1px" ClientInstanceName="tbLogin"></dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lblMensajeTarifa" runat="server" ClientInstanceName="lblMensajeTarifa" Text="¿ Realmente está seguro de aplicar tarifas a los elementos seleccionados ?"></dx:ASPxLabel>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAceptarIncrementoTarifas" runat="server" Text="Aceptar" Width="80px" Theme="Office2010Black" OnClick="btnAceptarIncrementoTarifas_Click" AutoPostBack="true" Style="float: left; margin-right: 8px" TabIndex="0">                                          
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnCancelaIncrementoTarifas" runat="server" Text="Cancelar" Width="80px" Theme="Office2010Black" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popupMsgConfirmacion.Hide(); ppRecibido.Hide(); }" />
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

    <dx:ASPxPopupControl ID="popupTarifaAplicada" runat="server" ClientInstanceName="popupTarifaAplicada" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true" Width="350">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel4" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxImage ID="ASPxImage3" runat="server" ShowLoadingImage="true" ImageUrl="~/img/iconos/Information2.ico"></dx:ASPxImage>
                                        <dx:ASPxTextBox ID="ASPxTextBox2" ReadOnly="true" Border-BorderStyle="None" Height="1px" runat="server" Width="1px" ClientInstanceName="tbLogin"></dx:ASPxTextBox>
                                    </td>
                                    <td>   
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblMensajeTarifa" Text="Incremento de tarifas aplicadas exitosamente."></dx:ASPxLabel>                                     
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAceptarConfirmacionTarifa" runat="server" Text="Aceptar" Width="80px" Theme="Office2010Black"  AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                          <ClientSideEvents Click="function(s, e) {popupMsgConfirmacion.Hide(); popupTarifaAplicada.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>                                    
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
