<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="frmParametros.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ALE_MexJet.Views.Catalogos.frmParametros" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript">
         function pageLoad(sender, args) {
             txtTextoBusquedaHabilitar();
             $('.combo').change(txtTextoBusquedaHabilitar);
         };
         function txtTextoBusquedaHabilitar() {
             var filtro = $(".combo").find(':selected').val();
             if (filtro == 0 || filtro == 4 || filtro == 5) {
                 $(".txtBusqueda").attr('disabled', '-1');
                 $(".txtBusqueda").val('');
             }
             else
                 $(".txtBusqueda").removeAttr('disabled');
         }
         function OnSaveClick(s, e) {
             gvParametros.UpdateEdit();
         }
    </script>
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                <div class="col-md-12">
                    <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Parametros</span>
                </div>
                </div>
                <div class="well-g">
                <div class="row">
                    <div class="col-md-12">
                        <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                            <fieldset class="Personal">
                                <legend>
                                    <span style="font-family:Helvetica, Arial,sans-serif; text-align:center;">Búsqueda General</span>
                                </legend>
                                <div class="col-sm-12">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtTextoBusqueda" CssClass="txtBusqueda"  runat="server" Width="180px" placeholder="Ingrese la información a buscar"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList  ID="ddlTipoBusqueda" CssClass="combo" runat="server">
                                                    <Items>
                                                        <asp:ListItem Text="[Sin Filtro]" Value="0" Selected="true"></asp:ListItem>
                                                        <asp:ListItem Text="Clave" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Descripcion" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Valor" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Solo Activos" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="Solo Inactivos" Value="5"></asp:ListItem>
                                                    </Items>
                                            </asp:DropDownList>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnBusqueda" Text="Buscar" runat="server" OnClick="btnBuscar_Click"></dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                <br />
                <div class="row">
                    <div class="col-md-6"><dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" Theme="Office2010Black" Visible="false" OnClick="btnNuevo_Click"></dx:ASPxButton></div>
                    <div class="col-md-6" style="text-align:right;"><dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>&nbsp;<dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportar_Click"></dx:ASPxButton></div>
                </div>
                <br />
                <div class="row">
                <div class="col-md-12" style="margin-left:-15px;width:103%;">
                    <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload ="Unnamed_Unload">
                        <ContentTemplate>
                                <div class="col-sm-12">
                                    <dx:ASPxGridView ID="gvParametros" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                        ClientInstanceName="gvParametros" EnableTheming="True" KeyFieldName="IdParametro"
                                        OnCellEditorInitialize="gvParametros_CellEditorInitialize" OnRowDeleting="gvParametros_RowDeleting"
                                        OnRowInserting="gvParametros_RowInserting" OnRowUpdating="gvParametros_RowUpdating" OnCommandButtonInitialize="gvParametros_CommandButtonInitialize"
                                        OnStartRowEditing="gvParametros_StartRowEditing" OnRowValidating="gvParametros_RowValidating" Styles-Header-HorizontalAlign ="Center"
                                        Theme="Office2010Black" Width="100%" StylesPopup-EditForm-ModalBackground-Opacity="90" OnCancelRowEditing="gvParametros_CancelRowEditing">
                                        <ClientSideEvents EndCallback="function (s, e) {
                                        if (s.cpShowPopup)
                                        {
                                            delete s.cpShowPopup;
                                            lbl.SetText(s.cpText);
                                            popup.Show();
                                        }
                                    }" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Clave" FieldName="Clave" ShowInCustomizationForm="True" VisibleIndex="0">
                                                <PropertiesTextEdit>
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" CausesValidation="True" ErrorText="Error en la informaci&#243;n ingresada" SetFocusOnError="True">
                                                        <RequiredField IsRequired="True" ErrorText="El campo es requerido"></RequiredField>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Descripción" FieldName="Descripcion" ShowInCustomizationForm="True" VisibleIndex="1">
                                                <PropertiesTextEdit>
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom"></ValidationSettings>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Valor" FieldName="Valor" ShowInCustomizationForm="True" VisibleIndex="2">
                                                <PropertiesTextEdit>
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom"></ValidationSettings>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Estatus" FieldName="Status" VisibleIndex="4">
                                                <PropertiesComboBox TextField="Estatus" ValueField="Status" ValueType="System.Int32">
                                                    <items>
                                                        <dx:ListEditItem Text="Inactivo" Value="0" Selected="true"/>
                                                        <dx:ListEditItem Text="Activo" Value="1" />
                                                    </items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button" Caption="Acciones" ShowDeleteButton="False" ShowEditButton="True" ShowNewButton="False" ShowInCustomizationForm="True" VisibleIndex="6">
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
                                        <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                        <SettingsPopup>
                                            <EditForm HorizontalAlign="Center" VerticalAlign="Above" Modal="true" CloseOnEscape="True" Width="400px" />
                                        </SettingsPopup>
                                        <SettingsSearchPanel Visible="true" />
                                        <SettingsCommandButton>
                                            <NewButton>
                                                <Image Height="20px" ToolTip="Nuevo" Width="20px">
                                                </Image>
                                            </NewButton>
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
                                    <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvParametros">
                                    </dx:ASPxGridViewExporter>
                                </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnNuevo2" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnBusqueda" EventName="Click" />
                            <asp:PostBackTrigger ControlID="btnExcel" />
                            <asp:PostBackTrigger ControlID="btnExportar" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-6"><dx:ASPxButton ID="btnNuevo2" runat="server" Text="Nuevo" Theme="Office2010Black" Visible="false" OnClick="btnNuevo_Click"></dx:ASPxButton></div>
                    <div class="col-sm-6" style="text-align:right;"><dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>&nbsp;<dx:ASPxButton ID="btnExportar" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportar_Click"></dx:ASPxButton></div>
                </div>
                <br />  
                    </div>
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
