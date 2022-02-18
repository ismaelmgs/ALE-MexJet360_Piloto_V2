<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmNotaCredito.aspx.cs" Inherits="ALE_MexJet.Views.CreditoCobranza.frmNotaCredito" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowLoginWindow(mensaje) {
            lbl.SetText(mensaje);
            ppAlert.Show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection >
            <dx:PanelContent>
                <div class="row header1" >
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family :Helvetica, Arial, sans-serif; font-size:20px;" >&nbsp;&nbsp;Nota de Crédito </span >
                    </div>
                </div >
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <div class="row">
                    <div  class="col-sm-12"><br />
                        <fieldset class="Personal">
                            <legend>
                                <span style="font-family : Helvetica, arial, sans-serif; text-align:center;" >Búsqueda Folio de Remisión</span>
                            </legend>
                            <div class="col-sm-12">
                                <table style="width:100%">
                                    <tr>
                                        <td width="25%" align="center">&nbsp;</td>
                                        <td width="25%" align="center">
                                            <asp:Panel ID="ptxtBuscaFolio" runat="server" DefaultButton="btnBuscar">
                                            <dx:ASPxTextBox ID="txtBuscaFolio"  ToolTip="Buscar" runat="server" Theme="Office2010Black" NullText="Ingrese Folio Remisión">
                                                <ValidationSettings>
                                                    <RegularExpression  ValidationExpression="^[a-zA-Z0-9]*$"  ErrorText="Solo se permiten números y letras"/>
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                                </asp:Panel>
                                        </td>
                                        <td width="25%" align="center">
                                            <dx:ASPxButton ID="btnBuscar" ToolTip="Buscar" runat="server" Theme="Office2010Black" Text="Buscar" OnClick="btnBuscar_Click">
                                                <%--<ClientSideEvents  Click ="function(){btnAgregar.SetVisible(true);}"/>--%>
                                            </dx:ASPxButton>
                                        </td>
                                        <td width="25%" align="center">&nbsp;</td> 
                                    </tr>
                                </table>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-6">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server"   OnUnload ="UpdatePanel1_Unload" UpdateMode="Conditional"> 
                            <ContentTemplate>
                                <dx:ASPxButton ID="btnAgregar" ClientInstanceName="btnAgregar" ToolTip="Nuevo Elemento" runat="server" Text="Nuevo" Theme="Office2010Black"  OnClick="btnAgregar_Click" >
                                   <%-- <ClientSideEvents  Init="function(){btnAgregar.SetVisible(false);}"/>--%>
                                </dx:ASPxButton>
                                </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger  ControlID="btnBuscar" EventName="Click"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <br/>
                <div class="row">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server"   OnUnload ="UpdatePanel1_Unload" UpdateMode="Conditional"> 
                            <ContentTemplate>
                                <div >
                                    <dx:ASPxGridView ID="gvRemision" runat="server" AutoGenerateColumns="false" ToolTip="Resultado" Visible="false"
                                       Font_Size="small" ClientInstanceName="gvRemision" EnableTheming="true" Styles-Header-HorizontalAlign ="Center"
                                       Theme="Office2010Black" Width="100%" StylesPopup-EditForm-ModalBackground-Opacity="90"  >
                                        <columns>
                                            <dx:GridViewDataTextColumn Caption="Folio Remision" FieldName="IdRemision" visible="false" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Cliente" FieldName="CodigoCliente" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Contrato" FieldName="ClaveContrato" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Ruta" FieldName="Ruta" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Importe" FieldName="Importe" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                        </columns>
                                          <SettingsSearchPanel Visible="true" />
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                        <ClientSideEvents Init="function(){btnAgregar.SetVisible(true);}" />
                                    </dx:ASPxGridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger  ControlID="btnBuscar" EventName="Click"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                 <br />
  </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>



    <dx:ASPxPopupControl ID="ppAlert" runat="server" ClientInstanceName="ppAlert" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Aviso" AllowDragging="true" ShowCloseButton ="true"  Width ="300">
        <ClientSideEvents />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" >
                <dx:ASPxPanel ID="ASPxPanel2" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxImage ID="ASPxImage2" runat="server" ShowLoadingImage="true"  ImageUrl="~/img/iconos/Information2.ico" ></dx:ASPxImage>
                                        <dx:ASPxTextBox ID ="tbLogin" ReadOnly ="true" Border-BorderStyle ="None"  Height ="1px" runat="server" Width="1px" ClientInstanceName="tbLogin"></dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbl" runat="server" ClientInstanceName="lbl" Text="ASPxLabel" ></dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr >
                                    <td>
                                         <dx:ASPxButton ID="btOK" runat="server" Text="OK" Theme="Office2010Black" Width="80px" AutoPostBack="false" style="float: left; margin-right: 8px" TabIndex ="0">
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
     
     <dx:ASPxPopupControl ClientInstanceName="ppAgregar" Width="350px" Height="200px" Theme="Office2010Black"
         ID="ppAgregar" runat="server"  Modal="True"   HeaderText="Nuevo elemento" AllowDragging="true" >
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <asp:Panel ID="Panel1" runat="server">
                        <table style="width:100%">
                            <tr ">
                                <td style="width:50%; text-align:center;"> 
                                    <dx:ASPxLabel ID="lblTipoNotaCredito" ClientInstanceName="lblTipoNotaCredito" runat="server"  Text="Tipo Nota Crédito"></dx:ASPxLabel>
                                </td>
                                <td style="width:50%; text-align:center;"> 
                              <dx:ASPxRadioButtonList ID="rblTipoNotaCredito" runat="server" Theme="Office2010Black" ToolTip="Tipo Nota Credito"
                                   ClientInstanceName="rblTipoNotaCredito" >
                                  <ClientSideEvents SelectedIndexChanged="function() {
                                             if (rblTipoNotaCredito.GetValue() == 1)
                                                {
                                                    lblCantidad.SetVisible(false);          
                                                    spCantidad.SetVisible(false);          
                                                    lblTiempo.SetVisible(true);          
                                                    txtTiempo.SetVisible(true);
                                      
                                                    txtTiempo.SetText('');
                                                    spCantidad.SetText('');          
                                                }
                                                else if (rblTipoNotaCredito.GetValue() == 0)
                                                {
                                                    lblCantidad.SetVisible(true);          
                                                    spCantidad.SetVisible(true);          
                                                    lblTiempo.SetVisible(false);          
                                                    txtTiempo.SetVisible(false);          

                                                    txtTiempo.SetText('');
                                                    spCantidad.SetText('');          
                                                }
                                                }"
                                      />
                                  <Items>
                                      <dx:ListEditItem  Text="Tiempo Vuelo" Value ="1"/>
                                      <dx:ListEditItem  Text="Pernocta" Value ="0"/>
                                  </Items>
                                </dx:ASPxRadioButtonList>
                                </td>
                            </tr>
                            <tr style="height:15px">
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <td style="width:50%; text-align:center;"> 
                                    <dx:ASPxLabel ID="lblCantidad"  ClientInstanceName="lblCantidad" runat="server" Text="Cantidad" Width="100%"></dx:ASPxLabel>
                                </td>
                                <td style="width:50%; text-align:center;"> 
                                    <dx:ASPxTextBox ID="spCantidad"  ClientInstanceName="spCantidad" runat="server" ToolTip="Ingrese Cantidad" Width="78%">
                                        <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la información ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                   <RegularExpression ValidationExpression="\d+(\.)?\d*" ErrorText="El campo permite solo números."></RegularExpression>
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                    <%--<dx:ASPxSpinEdit ID="spCantidad"  ClientInstanceName="spCantidad" runat="server" ToolTip="Ingrese Cantidad" MinValue="1" MaxValue="50" Width="78%">
                                    </dx:ASPxSpinEdit>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:50%; text-align:center;"> 
                                    <dx:ASPxLabel ID="lblTiempo" ClientInstanceName="lblTiempo" runat="server" Text="Tiempo" Width="100%"></dx:ASPxLabel>                                    
                                </td>
                                <td style="width:50%; text-align:center;">  
                                    <dx:ASPxTextBox ID="txtTiempo"  ClientInstanceName="txtTiempo" runat="server"  Width="145%" ToolTip="Ingrese Tiempo">
                                        <MaskSettings Mask="HH:mm" />
                                    </dx:ASPxTextBox>
                                </td>                 
                            </tr>
                            <tr style="height:15px">
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <td style="width:50%; text-align:center;"> 
                                            <dx:ASPxButton ID="btnguardar"  runat="server" Theme="Office2010Black" Text="Guardar" OnClick="btnguardar_Click" ToolTip="Guardar"></dx:ASPxButton>
                                </td>          
                                <td style="width:50%; text-align:center;"> 
                                            <dx:ASPxButton ID="btnCancelar" runat="server" Theme="Office2010Black" Text="Cancelar" AutoPostBack="false" ToolTip="Cancelar">
                                                <ClientSideEvents Click="function(s, e) {ppAgregar.Hide(); spCantidad.SetText(''); txtTiempo.SetText(''); }"  />
                                            </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                </asp:Panel>
            </dx:PopupControlContentControl>
        </ContentCollection>
         <ClientSideEvents  Init="function() {  lblCantidad.SetVisible(false);          
                                                spCantidad.SetVisible(false);          
                                                lblTiempo.SetVisible(false);          
                                                txtTiempo.SetVisible(false);
                                                txtTiempo.SetText('');
                                                spCantidad.SetText('');
                                                }"/>
    </dx:ASPxPopupControl>
           
</asp:Content>