<%@ Page Language="C#"  MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmConsultaContratos.aspx.cs" Inherits="ALE_MexJet.Views.Consultas.frmConsultaContratos" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">

        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Consulta Contratos</span>
                    </div>
                </div>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <div class="row">
                    <div class="col-md-12"><br />
                        <fieldset class="Personal">
                            <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda por Cliente</span>
                            </legend>
       
                            <div class="col-sm-12">
                                
                                 <table width="100%" style="text-align:left;">
                                        <tr>
                                            <td>
                                                Cliente:<dx:ASPxComboBox ID="ddlCliente" runat="server" Theme="Office2010Black" EnableTheming="True" 
                                                    NullText="Cliente" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith" EnableSynchronization="False" 
                                                   OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged" AutoPostBack="true"
                                                    >
                                                   
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td>
                                                Contrato:<dx:ASPxComboBox ID="ddlContrato" runat="server" Theme="Office2010Black" EnableTheming="True" AutoPostBack="true" NullText="Contrato" 
                                                    ClientInstanceName="cmdContrato"  DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" EnableSynchronization="False"
                                                    OnSelectedIndexChanged="ddlContrato_SelectedIndexChanged">
                                                   
                                                </dx:ASPxComboBox>
                                            </td>

                                            
                                        </tr>
                                    </table>
                                    
                            </div>
                        </fieldset>
                    </div>
                </div>
                <br />
                    <div class="row">                                              

                        <div class="col-sm-6">
                            <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click" ></dx:ASPxButton>
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
                                    <dx:ASPxGridView ID="gvConsultaContratos" runat="server" AutoGenerateColumns="false" Font-Size="Small"
                                         ClientInstanceName="gvConsultaContratos" EnableTheming="True" KeyFieldName="GastoInternoFolio"
                                         OnCellEditorInitialize="ggvConsultaContratos_CellEditorInitialize" Styles-Header-HorizontalAlign ="Center"
                                         Theme="Office2010Black" Width="100%" StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                         OnRowInserting="gvConsultaContratos_RowInserting" OnRowUpdating="gvConsultaContratos_RowUpdating" 
                                         OnStartRowEditing="gvConsultaContratos_StartRowEditing" OnRowDeleting="gvConsultaContratos_RowDeleting"
                                        >
                                        <ClientSideEvents EndCallback="function (s, e) {
                                                if (s.cpShowPopup)
                                                {
                                                    delete s.cpShowPopup;
                                                    lbl.SetText(s.cpText);
                                                    popup.Show();
                                                }
                                            }" />

                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Folio" FieldName="GastoInternoFolio" ShowInCustomizationForm="true" VisibleIndex="1">
                                                <PropertiesTextEdit MaxLength="200">
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>

                                                        <RegularExpression ValidationExpression="[a-zA-Z ñÑáéíóúÁÉÍÓÚ]*[0-9]*" ErrorText="El campo contiene información inválida."></RegularExpression>

                                                        <RequiredField IsRequired="True" ErrorText="El campo es requerido"></RequiredField>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Caption="Folio" Visible="False" />
                                            </dx:GridViewDataTextColumn>

                                            <dx:GridViewDataTextColumn FieldName="GastoInternoConcepto" ShowInCustomizationForm="True" Visible="true" Caption="Concepto" VisibleIndex="2">
                                                <EditFormSettings Visible="False" Caption="Concepto"></EditFormSettings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="IdConcepto" Visible="false" VisibleIndex="2">
                                                <EditFormSettings Visible="True" Caption="Concepto"></EditFormSettings>
                                            </dx:GridViewDataComboBoxColumn>

                                            <dx:GridViewDataComboBoxColumn Caption="Tipo Movimiento" FieldName="GastoInternoTipoMovimiento" ShowInCustomizationForm="true" VisibleIndex="3">
                                                <PropertiesComboBox MaxLength="200" TextField="CategoryName" ValueField="GastoInternoTipoMovimiento" ValueType="System.Int32">
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom"></ValidationSettings>
                                                    <Items>                                                        
                                                        <dx:ListEditItem Text="Cargo" Value="0" />
                                                        <dx:ListEditItem Text="Reverso" Value="1" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>

                                            <dx:GridViewDataTextColumn Caption="Importe" FieldName="GastoInternoImporte" ShowInCustomizationForm="true" VisibleIndex="4" >
                                                <PropertiesTextEdit MaxLength="200">
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>

                                                        <RegularExpression ValidationExpression="([0-9]|,)*" ErrorText="El campo contiene información inválida."></RegularExpression>

                                                        <RequiredField IsRequired="True" ErrorText="El campo es requerido"></RequiredField>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Caption="Importe" />
                                            </dx:GridViewDataTextColumn>

                                            <dx:GridViewDataTextColumn FieldName="GastoInternoTipoMoneda" ShowInCustomizationForm="True" Visible="true" Caption="Tipo Moneda" VisibleIndex="5">
                                                <EditFormSettings Visible="False" Caption="Concepto"></EditFormSettings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="IdTipoMoneda" PropertiesComboBox-ValueField="IdTipoMoneda" Caption="Tipo Moneda" Visible="false" VisibleIndex="5">
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                            </dx:GridViewDataComboBoxColumn>


                                            <dx:GridViewCommandColumn ButtonType="Button" Caption="Acciones" ShowDeleteButton="True" ShowEditButton="True" ShowNewButton="false" VisibleIndex="6">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                            </dx:GridViewCommandColumn>
                                            
                                        </Columns>
                                        
                                        <SettingsBehavior ConfirmDelete="True" />
                                        <SettingsPager Position="TopAndBottom">
                                            <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                            </PageSizeItemSettings>
                                        </SettingsPager>
                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                                        <Settings ShowGroupPanel="True" />
                                        <SettingsText ConfirmDelete="¿Desea eliminar?" />
                                        <SettingsPopup>
                                            <EditForm HorizontalAlign="Center" VerticalAlign="Above" Modal="true" CloseOnEscape="True" Width="400px" />
                                        </SettingsPopup>
                                        <SettingsSearchPanel Visible="true" />

                                        <SettingsCommandButton>
                                            <UpdateButton Text="Guardar"></UpdateButton>
                                            <CancelButton></CancelButton>
                                            <EditButton>
                                                <Image Height="20px" ToolTip="Modificar" Width="20px"></Image>
                                            </EditButton>
                                            <DeleteButton>
                                                <Image Height="20px" ToolTip="Eliminar" Width="20px"></Image>
                                            </DeleteButton>
                                        </SettingsCommandButton>
                                        <StylesPopup>
                                            <EditForm>
                                                <ModalBackground Opacity="90">
                                                </ModalBackground>
                                            </EditForm>
                                        </StylesPopup>

                                  
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvGastosInternos">
                                        </dx:ASPxGridViewExporter>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnNuevo2" EventName="Click" />
                                </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <br />
                    <div class="row">
                        <div class="col-sm-6">
                            <dx:ASPxButton ID="btnNuevo2" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click"></dx:ASPxButton>
                        </div>
                        <div class="col-sm-6" style="text-align: right;">
                            <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                            &nbsp;<dx:ASPxButton ID="btnExportar" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExcel_Click"></dx:ASPxButton>
                        </div>
                    </div>
                    <br />
            </dx:PanelContent>

        </PanelCollection>

    </dx:ASPxPanel>

     <dx:ASPxPopupControl ID="popup" runat="server" ClientInstanceName="popup" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" HeaderText="Aviso" AllowDragging="true" ShowCloseButton ="true"  Width ="300">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
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
                                         <dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px" AutoPostBack="false" style="float: left; margin-right: 8px" TabIndex ="0">
                                             <ClientSideEvents Click="function(s, e) {popup.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <div >
                                
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>


