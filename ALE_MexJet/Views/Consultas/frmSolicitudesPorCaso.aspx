<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmSolicitudesPorCaso.aspx.cs" Inherits="ALE_MexJet.Views.Consultas.frmSolicitudesPorCaso" UICulture="es" Culture="es-MX" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v18.1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="~/../../../JS/jquery/jquery-1.8.3.js"></script>
     
    <script type="text/javascript">                      
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 25px;">&nbsp;&nbsp;Solicitudes Por Caso</span>
                    </div>
                </div>
                <div class="well-g">
                    <div class="row">
                        <div class="col-md-12">

                            <fieldset class="Personal">
                                 <legend>
                                       <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                                </legend>
                                                                
                                    <uc1:ucModalMensaje ID="mpeMensaje" runat="server"/>                                                                        
                                
                                <div class="col-sm-12">                                    
                                    <div class="col-lg-4" style="text-align:right;">
                                        <table id="miTabla">
                                         <tr>  
                                             <td>
                                                 <dx:ASPxLabel ID="lblFechaInicial" runat="server" Text="Fecha Inicial:" Theme="Office2010Black"></dx:ASPxLabel>
                                             </td>                                      
                                            <td>
                                                <dx:ASPxDateEdit ID="dFechaIni" ClientInstanceName="dFechaIni" NullText="Fecha Inicial" 
                                                    runat="server" ToolTip="Fecha Inicial" Theme="Office2010Black" Width="200" EditFormat="Custom"  
                                                    EditFormatString="dd/MM/yyyy hh:mm tt" >  
                                                     <TimeSectionProperties>
                                                        <TimeEditProperties EditFormatString="hh:mm tt" />
                                                    </TimeSectionProperties>                                                                                          
                                                    </dx:ASPxDateEdit>                                                
                                            </td>
                                        </tr>
                                            <tr><td colspan="2" style="height:15px"></td></tr>
                                        <tr>  
                                            <td>
                                                <dx:ASPxLabel ID="lblFechaFinal" runat="server" Text="Fecha Final:" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>                                        
                                            <td>
                                                    <dx:ASPxDateEdit ID="dFechaFin" ClientInstanceName="dFechaFin"  NullText="Fecha Final"
                                                        runat="server" ToolTip="Fecha Final" Theme="Office2010Black"  Width="200" EditFormat="Custom"
                                                        EditFormatString="dd/MM/yyyy hh:mm tt" >
                                                  <TimeSectionProperties>
                                                      <TimeEditProperties EditFormatString="hh:mm tt" />
                                                   </TimeSectionProperties>
                                                    </dx:ASPxDateEdit>
                                            </td>                                                                                  
                                        </tr>                                                                                                                            
                                        </table> 
                                    </div>

                                    <div class="col-lg-4" style="text-align:left">
                                        <table id="Mitable2">
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lblNumeroTrip" runat="server" Text="Numero Trip:" Theme="Office2010Black"></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextbox ID="txtNumeroTrip" runat="server" Theme="Office2010Black" EnableTheming="true"></dx:ASPxTextbox>
                                                </td>
                                            </tr>
                                            <tr><td colspan="2" style="height:15px"></td></tr> 
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lblTipoCaso" runat="server" Text="Caso:" Theme="Office2010Black"></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxComboBox ID ="cmbTipoCaso" runat="server" Theme="Office2010Black" EnableTheming="true"></dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>                                                                                                                        
                                    </div>
                                                                   
                                    <div class="col-lg-4">
                                        <dx:ASPxButton ID="btnBusqueda" Text="Buscar" Theme="Office2010Black" runat="server" OnClick="btnBusqueda_Click" EnableTheming="true" ></dx:ASPxButton>
                                    </div>
                                </div>                                                                                                                    
                                <br />                                                                   
                            </fieldset>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" Theme="Office2010Black" Visible="false" ></dx:ASPxButton>
                        </div>
                        <div class="col-md-6" style="text-align: right;">
                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                            &nbsp;<dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExcel_Click" ></dx:ASPxButton>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="margin-left: -15px; width: 103%;">
                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                <ContentTemplate>
                                    <div class="col-sm-12">
                                        <dx:ASPxGridView ID="gvSolicitudPorCaso" runat="server" AutoGenerateColumns="False" 
                                            ClientInstanceName="gvSolicitudPorCaso" EnableTheming="True" KeyFieldName="IdAeroave"                                            
                                            Styles-Header-HorizontalAlign ="Center" Theme="Office2010Black" Width="100%" >
                                            <ClientSideEvents EndCallback="function (s, e) {
                                            if (s.cpShowPopup)
                                            {
                                                delete s.cpShowPopup;
                                                lbl.SetText(s.cpText);
                                                popup.Show();
                                            }
                                        }" />
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="Solicitud" FieldName="IdSolicitud" ShowInCustomizationForm="True" VisibleIndex="1">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Fecha y Hora" FieldName="Fecha" ShowInCustomizationForm="True" VisibleIndex="2" Width="220">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Cliente" FieldName="CodigoCliente" ShowInCustomizationForm="True" VisibleIndex="3">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Contacto" FieldName="Contacto" ShowInCustomizationForm="True" VisibleIndex="4" Width="200">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Estado" FieldName="Estado" ShowInCustomizationForm="True" VisibleIndex="5">                                                    
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Numero de Trip" FieldName="NumeroTrip" ShowInCustomizationForm="True" VisibleIndex="6">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior ConfirmDelete="True" />
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="2"></SettingsEditing>
                                            <SettingsSearchPanel Visible="true" />
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvSolicitudPorCaso">
                                        </dx:ASPxGridViewExporter>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnExcel" />
                                    <asp:PostBackTrigger ControlID="btnExcel2" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-6">
                            <dx:ASPxButton ID="btnNuevo2" runat="server" Text="Nuevo" Theme="Office2010Black" Visible="false" ></dx:ASPxButton>
                        </div>
                        <div class="col-sm-6" style="text-align: right;">
                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                            &nbsp;<dx:ASPxButton ID="btnExcel2" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExcel_Click" ></dx:ASPxButton>
                        </div>
                    </div>
                    <br />
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

    <dx:ASPxPopupControl ID="popup" runat="server" ClientInstanceName="popup" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
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
                                        <dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popup.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <div>
                            </div>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

</asp:Content>