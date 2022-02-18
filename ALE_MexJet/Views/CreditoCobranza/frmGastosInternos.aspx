<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmGastosInternos.aspx.cs" Inherits="ALE_MexJet.Views.CreditoCobranza.frmGastosInternos" UICulture="es" Culture="es-MX"%>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var Valor1;
        var Valor2;
        function OnValueChanged(s) {          
            var Valor = s.GetValue();

            Valor1 = gvGastosInternos.GetEditor("GastoInternoImporte").GetValue();
            Valor2 = gvGastosInternos.GetEditor("IVA").GetValue();

            if (Valor1 == null)
                Valor1 = 0;
            if (Valor2 == null)
                Valor2 = 0;

            var result = parseFloat(Valor1) + parseFloat(Valor2);

            gvGastosInternos.GetEditor("Total").SetValue(result);           
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">

        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Gastos Internos</span>
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
                                                Cliente:<dx:ASPxComboBox ID="ddlCliente" runat="server" Theme="Office2010Black" EnableTheming="True" AutoPostBack="true" NullText="Seleccione una opción"
                                                    DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" EnableSynchronization="False" 
                                                   OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged" 
                                                    >
                                                   
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td>
                                                Contrato:<dx:ASPxComboBox ID="ddlContrato" runat="server" Theme="Office2010Black" EnableTheming="True" AutoPostBack="true" NullText="Seleccione una opción" 
                                                    DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" EnableSynchronization="False"
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
                            <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click" Visible="false"></dx:ASPxButton>
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
                                    <dx:ASPxGridView ID="gvGastosInternos" runat="server" AutoGenerateColumns="false" Font-Size="Small"
                                         ClientInstanceName="gvGastosInternos" EnableTheming="True" KeyFieldName="GastoInternoFolio"
                                         OnCellEditorInitialize="gvGastosInternos_CellEditorInitialize" Styles-Header-HorizontalAlign ="Center"
                                         Theme="Office2010Black" Width="100%" StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                         OnRowInserting="gvGastosInternos_RowInserting" OnRowUpdating="gvGastosInternos_RowUpdating" 
                                         OnStartRowEditing="gvGastosInternos_StartRowEditing" OnRowDeleting="gvGastosInternos_RowDeleting"
                                         OnRowValidating="gvGastosInternos_RowValidating"
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
                                            <dx:GridViewDataTextColumn Caption="Folio" FieldName="GastoInternoFolio" ShowInCustomizationForm="true" Visible="true" VisibleIndex="0" >
                                                <PropertiesTextEdit MaxLength="200">
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        <RegularExpression ValidationExpression="[a-zA-Z ñÑáéíóúÁÉÍÓÚ]*[0-9]*" ErrorText="El campo contiene información inválida."></RegularExpression>
                                                        <RequiredField IsRequired="True" ErrorText="El campo es requerido"></RequiredField>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Caption="Folio" Visible="False" />
                                            </dx:GridViewDataTextColumn>

                                            <dx:GridViewDataComboBoxColumn Caption="Tipo de Paquete" FieldName="DescripcionPaquete" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1" >
                                                <EditFormSettings Visible="True" Caption="Tipo de Paquete" VisibleIndex="0"/>
                                                <PropertiesComboBox ReadOnlyStyle-BackColor ="#CCCCCC"></PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>       
                                                                                        
                                            <dx:GridViewDataDateColumn FieldName="FechaCreacion" Caption="Fecha" ShowInCustomizationForm="true" CellStyle-HorizontalAlign="Center" Visible="false" VisibleIndex="2" >
                                                <EditFormSettings Visible="False" />
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Date"></PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                                                                 
                                            <dx:GridViewDataTextColumn FieldName="Matricula" Caption="Matrícula" ShowInCustomizationForm="True" Visible="true" VisibleIndex="3" >
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            
                                            <dx:GridViewDataTextColumn FieldName="GastoInternoTipoMoneda" Caption="Tipo Moneda" ShowInCustomizationForm="True" Visible="true" VisibleIndex="4" CellStyle-HorizontalAlign="Center">
                                                <EditFormSettings Visible="False" ></EditFormSettings>                                                
                                            </dx:GridViewDataTextColumn>                                            

                                            <dx:GridViewDataTextColumn FieldName="GastoInternoConcepto" Caption="Concepto" ShowInCustomizationForm="True" Visible="true" VisibleIndex="5">
                                                <EditFormSettings Visible="True" Caption="Concepto" VisibleIndex="6"></EditFormSettings>
                                                <PropertiesTextEdit>
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        <RequiredField IsRequired="True" ErrorText="El campo es requerido"></RequiredField>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                                                                        
                                            <dx:GridViewDataTextColumn Caption="Importe" FieldName="GastoInternoImporte" ShowInCustomizationForm="true" VisibleIndex="6" >
                                                <EditFormSettings Caption="Importe" Visible="True" VisibleIndex="7" />
                                                <PropertiesTextEdit MaxLength="200" DisplayFormatString="{0:C}">
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        <RegularExpression ValidationExpression="([0-9]|.)*" ErrorText="El campo contiene información inválida."></RegularExpression>
                                                        <RequiredField IsRequired="True" ErrorText="El campo es requerido"></RequiredField>
                                                    </ValidationSettings>
                                                    <ClientSideEvents ValueChanged = "function(s, e) { OnValueChanged(s); }"  />
                                                </PropertiesTextEdit>                                                
                                            </dx:GridViewDataTextColumn>

                                            <dx:GridViewDataTextColumn Caption="IVA" FieldName="IVA"  ShowInCustomizationForm="true" VisibleIndex="7" >
                                                <PropertiesTextEdit MaxLength="200" DisplayFormatString="{0:C}">
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        <RegularExpression ValidationExpression="([0-9]|.)*" ErrorText="El campo contiene información inválida."></RegularExpression>
                                                        
                                                    </ValidationSettings>
                                                    <ClientSideEvents ValueChanged = "function(s, e) { OnValueChanged(s); }" />
                                                </PropertiesTextEdit>
                                                <EditFormSettings Caption="IVA" Visible="True" VisibleIndex="8"/>
                                            </dx:GridViewDataTextColumn>

                                            <dx:GridViewDataTextColumn Caption="Total" FieldName="Total"  ShowInCustomizationForm="true" VisibleIndex="8" >
                                                <PropertiesTextEdit MaxLength="200" DisplayFormatString="{0:C}" ReadOnlyStyle-BackColor="#CCCCCC">                                                   
                                                </PropertiesTextEdit>
                                                <EditFormSettings Visible="True" Caption="Total" />
                                            </dx:GridViewDataTextColumn>
                                                                                                                                                                                                                                                                       
                                            <dx:GridViewDataComboBoxColumn FieldName="IdTipoMoneda" Caption="Tipo Moneda" PropertiesComboBox-ValueField="IdTipoMoneda" Visible="false" VisibleIndex="5">
                                                <EditFormSettings Visible="True" VisibleIndex="9"></EditFormSettings>
                                                <PropertiesComboBox NullText="Seleccione una opción">
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        <RequiredField IsRequired="true" ErrorText="El campo es requerido" />
                                                    </ValidationSettings>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>

                                             <dx:GridViewDataComboBoxColumn FieldName="IdMatricula" ShowInCustomizationForm="true" Visible="false" Caption="Matrícula" VisibleIndex="7">
                                                <EditFormSettings Visible="True" Caption="Matrícula" VisibleIndex="4"/>
                                                <PropertiesComboBox NullText="Seleccione una opción"></PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>

                                            <dx:GridViewDataComboBoxColumn FieldName="IdTipoMovimiento" Caption="Tipo de Movimiento" Visible="false" ShowInCustomizationForm="true" VisibleIndex="13">
                                                <EditFormSettings Visible="True" Caption="Tipo de Movimiento" VisibleIndex="5" />
                                                <PropertiesComboBox NullText="Seleccione una opción">
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        <RequiredField IsRequired="true" ErrorText="El campo es requerido" />
                                                    </ValidationSettings>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>

                                            <dx:GridViewDataComboBoxColumn Caption="Modelo" FieldName="DescripcionGrupoModelo" ShowInCustomizationForm="true" Visible="false" VisibleIndex="1">
                                                <EditFormSettings Visible="True" Caption="Modelo" VisibleIndex="1"/>
                                                <PropertiesComboBox ReadOnlyStyle-BackColor ="#CCCCCC"></PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>                                                                                        

                                            <dx:GridViewDataDateColumn Caption="Fecha" FieldName="FechaGasto" Visible="true" VisibleIndex="2" CellStyle-HorizontalAlign="Center">
                                                <EditFormSettings Visible="True" VisibleIndex="11" />
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Date" NullText="Seleccione una fecha" >
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        <RequiredField IsRequired="true" ErrorText="El campo es requerido" />
                                                    </ValidationSettings>
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>

                                            <dx:GridViewCommandColumn ButtonType="Button" Caption="Acciones" ShowDeleteButton="True" ShowEditButton="True" ShowNewButton="false" VisibleIndex="12">
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
                                    <asp:PostBackTrigger ControlID="btnExportar" />
                                    <asp:PostBackTrigger ControlID="btnExcel"  />
                                </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <br />
                    <div class="row">
                        <div class="col-sm-6">
                            <dx:ASPxButton ID="btnNuevo2" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click" Visible="false"></dx:ASPxButton>
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
