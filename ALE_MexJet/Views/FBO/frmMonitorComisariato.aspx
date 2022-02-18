<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmMonitorComisariato.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ALE_MexJet.Views.FBO.MonitorComisariato"   %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
     <script src="~/../../../JS/jquery/jquery-1.8.3.js"></script>
     <script type="text/javascript">
         var keyValue;
         function OnMoreInfoClick(element, key) {
             debugger;
             callbackPanel.SetContentHtml("");
             popup.ShowAtElement(element);
             keyValue = key;
         }
         function popup_Shown(s, e) {
             callbackPanel.PerformCallback(keyValue);
         }
    </script>
      <script type="text/javascript">
          function ShowLoginWindow() {
              debugger;
              ppRecibido.Show();
          }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 25px;">&nbsp;&nbsp;Recepción de Comisariatos</span>
                    </div>
                </div>
                <div class="well-g">
                    <div class="row">
                        <div class="col-md-12">

                            <fieldset class="Personal">
                                 <legend>
                                       <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                                </legend>
                                <div class="col-sm-12">
                                    <table border="0" style="width:100%;">
                                        <tr>
                                            <td>
                                                <dx:ASPxDateEdit Caption="Fecha:" ID="dFecha" ClientInstanceName="dFecha" NullText="Seleccione Fecha" runat="server" ToolTip="Fecha Inicial" Theme="Office2010Black">
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithTooltip">
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit> 
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox Caption="Trip:" ID="txtTrip" runat="server" Text="" Theme="Office2010Black" NullText="Ingrese Trip"></dx:ASPxTextBox>    
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox Caption="Cliente:" ID="txtCliente" runat="server" Text="" Theme="Office2010Black" NullText="Ingrese Código Cliente"></dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnBusqueda" Text="Buscar" Theme="Office2010Black" runat="server" OnClick="btnBusqueda_Click"></dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>                                     
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <br />
                    
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="margin-left: -15px; width: 103%;">
                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                <ContentTemplate>
                                    <div class="col-sm-12">
                                        <dx:ASPxGridView ID="gvComisariato" runat="server" AutoGenerateColumns="False" OnCellEditorInitialize="gvComisariato_CellEditorInitialize"
                                            ClientInstanceName="gvComisariato" EnableTheming="True" KeyFieldName="IdComisariato" OnRowUpdating="gvComisariato_RowUpdating" OnCommandButtonInitialize="gvComisariato_CommandButtonInitialize"
                                            Styles-Header-HorizontalAlign ="Center" Theme="Office2010Black" Width="100%"  OnStartRowEditing="gvComisariato_StartRowEditing" >
                                            <ClientSideEvents EndCallback="function (s, e) {
                                            if (s.cpShowPopup)
                                            {
                                                delete s.cpShowPopup;
                                                lbl.SetText(s.cpText);
                                               
                                                if(s.cpTipo == 'Recibido')
                                                {
                                                ASPxClientEdit.ClearGroup('clientContainer'); 
                                                lblRemision.SetVisible(true);
                                                txtRemision.SetVisible(true);
                                                lblSubTotal.SetVisible(true);
                                                txtSubTotal.SetVisible(true);
                                                lblImpt.SetVisible(true);
                                                lblImptCot.SetVisible(true);
                                                rbTipo.SetVisible(true);

                                                rblRechazo.SetVisible(false);
                                                ppRecibido.SetHeaderText('Comisariato Recibido');
                                                ppRecibido.Show();
                                                
                                               
                                                txtRemision.SetText(s.cpRemision);
                                                
                                               
                                                txtSubTotal.SetText(s.cptxtSubTotal);
                                                

                                                mNotas.SetText(s.cpmNotas);
                                                lblImpt.SetText(s.cpImpC);
                                                
                                                rbTipo.SetValue(s.cprbTipo);
                                                }
                                                else if(s.cpTipo == 'Abordo')
                                                {
                                                ASPxClientEdit.ClearGroup('clientContainer'); 
                                                lblRemision.SetVisible(true);
                                                txtRemision.SetVisible(true);
                                                lblSubTotal.SetVisible(true);
                                                txtSubTotal.SetVisible(true);
                                                lblImpt.SetVisible(true);
                                                lblImptCot.SetVisible(true);
                                                rbTipo.SetVisible(true);

                                                rblRechazo.SetVisible(false);
                                                ppRecibido.SetHeaderText('Comisariato Abordo');
                                                ppRecibido.Show();
                                                
                                               
                                                txtRemision.SetText(s.cpRemision);
                                                
                                               
                                                txtSubTotal.SetText(s.cptxtSubTotal);

                                                mNotas.SetText(s.cpmNotas);
                                                lblImpt.SetText(s.cpImpC);

                                                rbTipo.SetValue(s.cprbTipo);
                                                }
                                                else if(s.cpTipo == 'Rechazado')
                                                {
                                                lblRemision.SetVisible(false);
                                                txtRemision.SetVisible(false);
                                                lblSubTotal.SetVisible(false);
                                                txtSubTotal.SetVisible(false);

                                                lblImpt.SetVisible(false);
                                                lblImptCot.SetVisible(false);

                                                rbTipo.SetVisible(false);
                                                 mNotas.SetText(s.cpmNotas);
                                                rblRechazo.SetVisible(true);
                                                ppRecibido.SetHeaderText('Comisariato Rechazado');
                                                ppRecibido.Show();
                                                } 
                                            }
                                        }" />
                                            <Columns>     
                                                
                                                <dx:GridViewDataDateColumn Caption="Fecha Vuelo" FieldName="FechaVuelo" VisibleIndex="1" Width="10%" Visible="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy">
                                                    <EditFormSettings Visible="False"/>
                                                    </dx:GridViewDataDateColumn>

                                                <dx:GridViewDataColumn Caption="HoraVuelo" FieldName="HoraVuelo" VisibleIndex="2" Visible="true" Width="10%" >
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxLabel   Text='<%#Eval("HoraVuelo") %>' runat="server" Theme="Office2010Black"  ToolTip='<%#Eval("EstatusD") %>'>
                                                        </dx:ASPxLabel>
                                                    </DataItemTemplate>
                                                    <EditFormSettings Visible="false" />
                                                </dx:GridViewDataColumn>


                                                <dx:GridViewDataColumn Caption="Trip" FieldName="Trip" VisibleIndex="3" Visible="true" Width="10%" >
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxLabel   Text='<%#Eval("Trip") %>' runat="server" Theme="Office2010Black"  ToolTip='<%#Eval("EstatusD") %>'>
                                                        </dx:ASPxLabel>
                                                    </DataItemTemplate>
                                                    <EditFormSettings Visible="false" />
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn Caption="Cliente" FieldName="CodigoCliente" VisibleIndex="4" Visible="true" Width="10%" >
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxLabel   Text='<%#Eval("CodigoCliente") %>' runat="server" Theme="Office2010Black"  ToolTip='<%#Eval("EstatusD") %>'>
                                                        </dx:ASPxLabel>
                                                    </DataItemTemplate>
                                                    <EditFormSettings Visible="false" />
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn Caption="Matrícula" FieldName="Matricula" VisibleIndex="5" Visible="true" Width="10%" >
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxLabel   Text='<%#Eval("Matricula") %>' runat="server" Theme="Office2010Black"  ToolTip='<%#Eval("EstatusD") %>'>
                                                        </dx:ASPxLabel>
                                                    </DataItemTemplate>
                                                    <EditFormSettings Visible="false" />
                                                </dx:GridViewDataColumn>
                                                                                                                                                                                                                                               
                                                <dx:GridViewDataColumn Caption="Origen" FieldName="Origen" VisibleIndex="6" Visible="true" Width="10%" >
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxLabel   Text='<%#Eval("Origen") %>' runat="server" Theme="Office2010Black"  ToolTip='<%#Eval("EstatusD") %>'>
                                                        </dx:ASPxLabel>
                                                    </DataItemTemplate>
                                                    <EditFormSettings Visible="false" />
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn Caption="Comisariato Solicitado" FieldName="ComisariatoDesc" VisibleIndex="7" Visible="true" Width="10%" >
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxLabel   Text='<%#Eval("ComisariatoDesc") %>' runat="server" Theme="Office2010Black"  ToolTip='<%#Eval("EstatusD") %>'>
                                                        </dx:ASPxLabel>
                                                    </DataItemTemplate>
                                                    <EditFormSettings Visible="false" />
                                                </dx:GridViewDataColumn>
                                                
                                                <dx:GridViewDataColumn Caption="Preferencias" FieldName="Preferencias" VisibleIndex="8" Visible="true" Width="10%" >
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxLabel   Text='<%#Eval("Preferencias") %>' runat="server" Theme="Office2010Black"  ToolTip='<%#Eval("EstatusD") %>'>
                                                        </dx:ASPxLabel>
                                                    </DataItemTemplate>
                                                    <EditFormSettings Visible="false" />
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn Caption="Proveedor" FieldName="Proveedor" VisibleIndex="9" Visible="true" Width="10%" >
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxLabel   Text='<%#Eval("Proveedor") %>' runat="server" Theme="Office2010Black"  ToolTip='<%#Eval("EstatusD") %>'>
                                                        </dx:ASPxLabel>
                                                    </DataItemTemplate>
                                                    <EditFormSettings Visible="false" />
                                                </dx:GridViewDataColumn>

                                                  <dx:GridViewDataComboBoxColumn Caption="Estatus" FieldName="Estaus" Visible="true" VisibleIndex="10" Width="10%">
                                                    <EditFormSettings Visible="True"></EditFormSettings>
                                                    <PropertiesComboBox  NullText="Seleccione una opción" NullDisplayText="Seleccione una opción" ValueField="IdEstatus">
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>

                                                <dx:GridViewCommandColumn ButtonType="Button" Caption="Acciones" ShowEditButton="True" ShowNewButton="false" VisibleIndex="11">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />                                                   
                                                </dx:GridViewCommandColumn>  

                                                 <dx:GridViewDataTextColumn Caption="EstatusD" FieldName="EstatusD" VisibleIndex="12" Visible="false">
                                                     <EditFormSettings Visible="False" />
                                                </dx:GridViewDataTextColumn>
                                                                                         
                                            </Columns>
                                            <SettingsBehavior ConfirmDelete="True" />
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsEditing Mode="Inline"></SettingsEditing>
                                            <Settings ShowGroupPanel="True" />
                                            <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                            <SettingsPopup>
                                                <EditForm HorizontalAlign="Center" VerticalAlign="WindowCenter" Width="900px" />
                                            </SettingsPopup>
                                            <SettingsSearchPanel Visible="true" />
                                            <SettingsCommandButton>
                                                <NewButton ButtonType="Link">
                                                    <Image ToolTip="New">
                                                    </Image>
                                                </NewButton>
                                                <UpdateButton Text="Guardar"></UpdateButton>
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
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvAeronave">
                                        </dx:ASPxGridViewExporter>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <br />
                  
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
                                        <dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px"  Theme="Office2010Black" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
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

    <dx:ASPxPopupControl ID="ppRecibido" runat="server" ClientInstanceName="ppRecibido" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above"  AllowDragging="true" ShowCloseButton="true" Width="800">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <asp:Panel ID="ASPxPanel3" runat="server"  DefaultButton="ASPxButton1" >
                    
                    
                        <div >
                            <table style="width:100%" border="0" cellpadding="5">
                                <tr>
                                    <td style="width:10%">
                                           <dx:ASPxLabel runat="server" Text="No Remisión:" ClientInstanceName="lblRemision" Theme="Office2010Black">
                                           </dx:ASPxLabel>
                                    </td>
                                    <td style="width:30%" align="rigth">
                                        <dx:ASPxTextBox runat="server" ID="txtRemision"  ClientInstanceName="txtRemision" Theme="Office2010Black" NullText="Ingrese No. Remisión" Width="90%">
                                            <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la información ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="clientContainer">
                                                            <ErrorFrameStyle  ForeColor="Red"></ErrorFrameStyle>
                                                            <RegularExpression ValidationExpression="[0-9]*" ErrorText="El campo contiene información inválida."></RegularExpression>
                                                <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td style="width:10%">
                                        <dx:ASPxLabel runat="server" Text="Sub Total:" ClientInstanceName="lblSubTotal" Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td style="width:30%"" align="rigth">
                                        <dx:ASPxTextBox runat="server" ID="txtSubTotal" Theme="Office2010Black" ClientInstanceName="txtSubTotal" NullText="Ingrese Sub Total" Width="90%">
                                            <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la información ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="clientContainer">
                                                            <ErrorFrameStyle  ForeColor="Red"></ErrorFrameStyle>
                                                            <RegularExpression ValidationExpression="[0-9]*" ErrorText="El campo contiene información inválida."></RegularExpression>
                                                            <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                   
                                </tr>
                                <tr>
                                     <td style="width:10%">
                                        <dx:ASPxLabel runat="server" Text="Importe Cotizado:" ClientInstanceName="lblImptCot" Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td style="width:10%"" align="rigth">
                                        <dx:ASPxLabel runat="server" ID="lblImpt"  ClientInstanceName="lblImpt" Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td  colspan="2" align="center"><br />
                                        <dx:ASPxRadioButtonList runat="server" ID="rbTipo"  ClientInstanceName="rbTipo" Theme="Office2010Black" ValueType="System.Int32"  RepeatDirection="Horizontal"  >
                                            <Items>
                                                <dx:ListEditItem Text="Completo" Value="1" />
                                                <dx:ListEditItem Text="Incompleto" Value="0" />
                                            </Items>
                                            <ValidationSettings ValidationGroup="clientContainer" ErrorDisplayMode="Text"  ErrorTextPosition="Bottom" >
                                                <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                            </ValidationSettings>
                                        </dx:ASPxRadioButtonList>
                                    </td>
                                </tr>
                                <tr >
                                    <td  colspan="4" align="center">
                                        
                                        <dx:ASPxRadioButtonList runat="server" ID="rblRechazo" ClientInstanceName="rblRechazo" Theme="Office2010Black" ValueType="System.Int32"  RepeatDirection="Horizontal" >
                                            <Items>
                                                <dx:ListEditItem Text="Por Demora" Value="0"  />
                                                <dx:ListEditItem Text="Por Presentación " Value="1" />
                                            </Items>
                                            <ValidationSettings ValidationGroup="clientContainer" ErrorDisplayMode="Text"  ErrorTextPosition="Bottom" >
                                                <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                            </ValidationSettings>
                                        </dx:ASPxRadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td  colspan="4">
                                        &nbsp;<br />
                                     </td>
                                </tr>
                                <tr>
                                    <td >
                                          <dx:ASPxLabel runat="server" Text="Notas:" Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td colspan="3">
                                        <dx:ASPxMemo ID="mNotas" ClientInstanceName="mNotas" runat="server" Theme="Office2010Black" Width="100%" Height="150px"></dx:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td  colspan="4">
                                        &nbsp;<br />
                                     </td>
                                </tr>
                                <tr>
                                <tr>
                                 <td colspan="2" align="center">
                                        <dx:ASPxButton runat="server" ID="ASPxButton1" Theme="Office2010Black" CausesValidation="true" ValidationGroup="clientContainer" Text="Guardar" OnClick="btnGuardaComisariato_Click"></dx:ASPxButton>
                                    </td>
                                    <td colspan="2" align="center">
                                        <dx:ASPxButton runat="server" ID="ASPxButton2" Text="Cancelar" Theme="Office2010Black" OnClick="btnCancelarCom_Click">
                                            <ClientSideEvents Click="function(){  ppRecibido.Hide();  ASPxClientEdit.ClearGroup('clientContainer'); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            </div>
                       
                </asp:Panel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>
