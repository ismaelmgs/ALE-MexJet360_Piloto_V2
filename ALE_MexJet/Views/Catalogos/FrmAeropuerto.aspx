﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="FrmAeropuerto.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ALE_MexJet.Views.Catalogos.FrmAeropuerto" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="~/../../../JS/jquery/jquery-1.8.3.js"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            txtTextoBusquedaHabilitar();
            $('.combo').change(txtTextoBusquedaHabilitar);
        };
        function txtTextoBusquedaHabilitar() {
            var filtro = $(".combo").find(':selected').val();
            if (filtro == 0 || filtro == 5 || filtro == 6) {
                $(".txtBusqueda").attr('disabled', '-1');
                $(".txtBusqueda").val('');
            }
            if (filtro == 2 || filtro == 3 || filtro == 4)
                $(".txtBusqueda").removeAttr('disabled');
        }
        function OnSaveClick(s, e) {
            gvAeropuerto.UpdateEdit();
        }
    </script>
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span class="FTitulo">&nbsp;&nbsp;Aeropuerto</span>
                    </div>
                </div>
                <div class="well-g">
                    <div class="row">
                        <div class="col-md-12">
                            <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                            <fieldset class="Personal">
                                <legend>
                                    <span style="font-family: arial; text-align: center;">Búsqueda General</span>
                                </legend>
                                <div class="col-sm-12">
                                    <div class="col-lg-4">
                                      <asp:TextBox ID="txtTextoBusqueda" CssClass="txtBusqueda" placeholder ="Ingrese la información a buscar" runat="server" Width="180px"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4">                                   
                                             
                                                     <asp:DropDownList runat="server" CssClass="combo" ID="ddlTipoBusqueda">
                                                        <asp:ListItem Text="[Sin Filtro]" Value="0" Selected="true"></asp:ListItem>
                                                        <asp:ListItem Text="Aeropuerto" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="IATA" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="ICAO" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="Solo Activos" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="Solo Inactivos" Value="6"></asp:ListItem>
                                                    </asp:DropDownList>
                                           </div>
                                         <div class="col-lg-4">
                                        <dx:ASPxButton CssClass="FBotton" ID="btnBusqueda" Text="Buscar" Theme="Office2010Black" runat="server" OnClick="btnBuscar_Click"></dx:ASPxButton>
                                    </div>
                                  
                                
                              </div>
                            </fieldset>
                        </div>
                    </div>
                   <br />
                    <div class="row">
                        <div class="col-md-12" style="text-align: right;">
                            <dx:ASPxLabel CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                            &nbsp;<dx:ASPxButton CssClass="FBotton" ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportar_Click"></dx:ASPxButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                              <table class="table" style="margin-left: -15px; width: 100%;>
                                <tr>
    	                            <td>
                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload ="Unnamed_Unload" style="margin-left: -15px; width: 100%;">
                                <ContentTemplate>
                                    <div class="col-sm-12">
                                        <dx:ASPxGridView CssClass="FGrid" ID="gvAeropuerto" runat="server" AutoGenerateColumns="False" 
                                            ClientInstanceName="gvAeropuerto" EnableTheming="True" KeyFieldName="idAeropuert"
                                            OnCellEditorInitialize="gvAeropuerto_CellEditorInitialize" OnRowDeleting="gvAeropuerto_RowDeleting"
                                            OnRowInserting="gvAeropuerto_RowInserting" OnRowUpdating="gvAeropuerto_RowUpdating"
                                            OnStartRowEditing="gvAeropuerto_StartRowEditing" OnRowValidating="gvAeropuerto_RowValidating" Styles-Header-HorizontalAlign ="Center" OnCommandButtonInitialize="gvAeropuerto_CommandButtonInitialize"
                                            Theme="Office2010Black" Width="100%" StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                            OnCancelRowEditing="gvAeropuerto_CancelRowEditing">
                                            <ClientSideEvents EndCallback="function (s, e) {
                                        if (s.cpShowPopup)
                                        {
                                            delete s.cpShowPopup;
                                            lbl.SetText(s.cpText);
                                            popup.Show();
                                        }
                                    }" />
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="Aeropuerto" FieldName="Descripcion" ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly="true">
                                                    <PropertiesTextEdit DisplayFormatInEditMode="True">
                                                    </PropertiesTextEdit>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="IATA" FieldName="AeropuertoIATA" ShowInCustomizationForm="True" VisibleIndex="2">
                                                    <PropertiesTextEdit DisplayFormatInEditMode="True"></PropertiesTextEdit>
                                                    <EditFormSettings Visible="False"></EditFormSettings>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ICAO" FieldName="AeropuertoICAO" ShowInCustomizationForm="True" VisibleIndex="3">
                                                    <PropertiesTextEdit DisplayFormatInEditMode="True"></PropertiesTextEdit>
                                                    <EditFormSettings Visible="False"></EditFormSettings>
                                                </dx:GridViewDataTextColumn>
                                               
                                                <dx:GridViewDataTextColumn Caption="País" FieldName="Pais" ShowInCustomizationForm="True" VisibleIndex="5">
                                                    <EditFormSettings Visible="False"></EditFormSettings>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Estado" FieldName="Estado" ShowInCustomizationForm="True" VisibleIndex="6">
                                                    <EditFormSettings Visible="False"></EditFormSettings>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Ciudad" FieldName="Ciudad" ShowInCustomizationForm="True" VisibleIndex="7">
                                                    <EditFormSettings Visible="False"></EditFormSettings>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataComboBoxColumn FieldName="TipoDestino" Caption="Tipo Destino" VisibleIndex="8">
                                                    <PropertiesComboBox DisplayFormatInEditMode="True">
                                                        <Items>
                                                            <dx:ListEditItem Text="N" Value="N"></dx:ListEditItem>
                                                            <dx:ListEditItem Text="E" Value="E"></dx:ListEditItem>
                                                            <dx:ListEditItem Text="F" Value="F"></dx:ListEditItem>
                                                        </Items>
                                                    </PropertiesComboBox>
                                                    <EditFormSettings Visible="True"></EditFormSettings>
                                                </dx:GridViewDataComboBoxColumn>
                                                <dx:GridViewDataComboBoxColumn FieldName="TipoAeropuerto" Caption="Tipo Aeropuerto" VisibleIndex="8" >
                                                <PropertiesComboBox DisplayFormatInEditMode="True">
                                                    <Items>
                                                        <dx:ListEditItem Text="N" Value="1"></dx:ListEditItem>
                                                        <dx:ListEditItem Text="I" Value="2"></dx:ListEditItem>
                                                    </Items>
                                                </PropertiesComboBox>
                                                <EditFormSettings Visible="True"></EditFormSettings>
                                                </dx:GridViewDataComboBoxColumn>
                                                <dx:GridViewDataTextColumn Caption="Tarifa Helipuerto" FieldName="AeropuertoHelipuertoTarifa" ShowInCustomizationForm="True" VisibleIndex="9">
                                                    <PropertiesTextEdit DisplayFormatInEditMode="True">
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings Visible="True"></EditFormSettings>
                                                </dx:GridViewDataTextColumn>

                                                <dx:GridViewDataCheckColumn Caption="¿Se cobra aterrizaje?" FieldName="SeCobraAterrizaje" ShowInCustomizationForm="True" VisibleIndex="9">
                                                    <PropertiesCheckEdit ValueChecked="1" ValueType="System.Int32" ValueUnchecked="0">
                                                    </PropertiesCheckEdit>
                                                    <EditFormSettings Visible="True"></EditFormSettings>
                                                </dx:GridViewDataCheckColumn>

                                                <dx:GridViewDataTextColumn Caption="Aterrizaje Nal" FieldName="AterrizajeNal" ShowInCustomizationForm="True" VisibleIndex="9">
                                                    <PropertiesTextEdit DisplayFormatInEditMode="True">
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings Visible="True"></EditFormSettings>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Aterrizaje Int" FieldName="AterrizajeInt" ShowInCustomizationForm="True" VisibleIndex="9">
                                                    <PropertiesTextEdit DisplayFormatInEditMode="True">
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings Visible="True"></EditFormSettings>
                                                </dx:GridViewDataTextColumn>

                                                <dx:GridViewDataCheckColumn Caption="¿Base?" FieldName="Base" ShowInCustomizationForm="True" VisibleIndex="10">
                                                    <PropertiesCheckEdit ValueChecked="1" ValueType="System.Int32" ValueUnchecked="0">
                                                    </PropertiesCheckEdit>
                                                    <EditFormSettings Visible="True"></EditFormSettings>
                                                </dx:GridViewDataCheckColumn>
                                                <dx:GridViewDataCheckColumn Caption="¿Helipuerto?" FieldName="AeropuertoHelipuerto" ShowInCustomizationForm="True" VisibleIndex="11">
                                                    <PropertiesCheckEdit ValueChecked="1" ValueType="System.Int32" ValueUnchecked="0">
                                                    </PropertiesCheckEdit>
                                                    <EditFormSettings Visible="True"></EditFormSettings>
                                                </dx:GridViewDataCheckColumn>
                                                <dx:GridViewDataCheckColumn Caption="¿Activo?" FieldName="Status" ShowInCustomizationForm="True" VisibleIndex="12">
                                                    <PropertiesCheckEdit ValueChecked="1" ValueType="System.Int32" ValueUnchecked="0">
                                                    </PropertiesCheckEdit>
                                                </dx:GridViewDataCheckColumn>
                                                <dx:GridViewCommandColumn ButtonType="Button" Caption="Acciones" ShowEditButton="True" ShowInCustomizationForm="True" VisibleIndex="13">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewCommandColumn>
                                            </Columns>
                                            <SettingsBehavior ConfirmDelete="True" />
                                            <StylesPager  Pager-CssClass="FNumPag"></StylesPager>
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                                            <Settings ShowGroupPanel="True" />
                                            <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                            <SettingsPopup>
                                                <EditForm HorizontalAlign="Center" VerticalAlign="Above" Modal="true" CloseOnEscape="True" Width="400px" />
                                            </SettingsPopup>
                                            <SettingsSearchPanel Visible="true" />
                                            <SettingsCommandButton>
                                                <NewButton ButtonType="Link" >
                                                    <Image ToolTip="New">
                                                    </Image>
                                                </NewButton>
                                                <UpdateButton Text="Guardar"></UpdateButton>
                                                <CancelButton></CancelButton>
                                                <EditButton Styles-Style-CssClass="FBotton">
                                                    <Image Height="20px" ToolTip="Modificar" Width="20px">
                                                    </Image>                                                    
                                                </EditButton>
                                                <DeleteButton Styles-Style-CssClass="FBotton">
                                                    <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                                    </Image>
                                                </DeleteButton>
                                            </SettingsCommandButton>
                                             <Templates>
                                                <EditForm>
                                                    <dx:ASPxPanel runat="server" DefaultButton="Update">
                                                        <PanelCollection>
                                                            <dx:PanelContent>
                                                                <dx:ASPxGridViewTemplateReplacement ID="EditFormEditors" ReplacementType="EditFormEditors" runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                                <div class="row">
                                                                    <table>
                                                                        <tr>
                                                                            <td width="50%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxButton runat="server" CssClass="UpdatePersonal" ID="Update" Theme="Office2010Black" UseSubmitBehavior="true" AutoPostBack="false" Text="Guardar" style="padding:2px; margin-right:2px; margin-left: 12px; font-size:12px;">
                                                                                    <ClientSideEvents Click="function (s, e) { OnSaveClick(s, e); }" />
                                                                                </dx:ASPxButton>
                                                                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                                            </td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td colspan="2">
                                                                                &nbsp;
                                                                            </td>
                                                                          </tr>
                                                                    </table>
                                                                </div>
                                                                </ContentTemplate>
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxPanel>
                                                </EditForm>
                                            </Templates>
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvAeropuerto">
                                        </dx:ASPxGridViewExporter>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnExcel" />
                                    <asp:PostBackTrigger ControlID="ASPxButton2" />
                                </Triggers>
                            </asp:UpdatePanel>
                            </td>
                            </tr>
                          </table>
                        </div>
                        </div>
                    </div>
                    <div class="row" style="margin-right:8px;">
                        <div class="col-sm-12" style="text-align: right;">
                            <dx:ASPxLabel CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                            &nbsp;<dx:ASPxButton CssClass="FBotton" ID="ASPxButton2" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportar_Click"></dx:ASPxButton>
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
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>