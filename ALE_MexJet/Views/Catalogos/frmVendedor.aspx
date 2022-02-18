<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmVendedor.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ALE_MexJet.Views.Catalogos.frmVendedor" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="~/../../../JS/jquery/jquery-1.8.3.js"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            txtTextoBusquedaHabilitar();
            $('.combo').change(txtTextoBusquedaHabilitar);
        };
        function txtTextoBusquedaHabilitar() {
            var filtro = $(".combo").find(':selected').val();
            if (filtro == 0 || filtro == 8 || filtro == 9) {
                $(".txtBusqueda").attr('disabled', '-1');
                $(".txtBusqueda").val('');
            }
            if (filtro == 1 || filtro == 2 || filtro == 3 || filtro == 4 || filtro == 5 || filtro == 6 || filtro == 7)
                $(".txtBusqueda").removeAttr('disabled');
        }
        function OnSaveClick(s, e) {
            gvVendedor.UpdateEdit();
        }
    </script>
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                <div class="col-md-12">
                    <span class="FTitulo">&nbsp;&nbsp; Vendedor</span>
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
                                    <div class="col-lg-4">
                                        <asp:TextBox ID="txtTextoBusqueda" runat="server" Width="180px" CssClass="txtBusqueda" placeholder="Ingrese la información a buscar"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:DropDownList runat="server" CssClass="combo" ID="ddlTipoBusqueda">
                                            <asp:ListItem Text="[Sin Filtro]" Value="0" Selected="true"></asp:ListItem>
                                            <asp:ListItem Text="Nombre" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Zona" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Unidad de Negocio" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Login" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Codigo Unidad" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Email" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="Base" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="Solo Activos" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="Solo Inactivos" Value="9"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <dx:ASPxButton CssClass="FBotton" ID="btnBusqueda" Text="Buscar" runat="server" Theme="Office2010Black" OnClick="btnBuscar_Click"></dx:ASPxButton>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                <br />
                <div class="row">
                    <div class="col-md-6"><dx:ASPxButton CssClass="FBotton" ID="btnNuevo" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click"></dx:ASPxButton></div>
                    <div class="col-md-6" style="text-align:right;"><dx:ASPxLabel CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>&nbsp;<dx:ASPxButton CssClass="FBotton" ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportar_Click"></dx:ASPxButton></div>
                </div>
                <br />
                <div class="row">
                <div class="col-md-12" style="margin-left:-15px;width:103%;">
                    <asp:UpdatePanel runat="server" UpdateMode="Always" id="UpdVendedor" OnUnload="Unnamed_Unload">
                        <ContentTemplate>
                                <div class="col-sm-12">
                                    <dx:ASPxGridView CssClass="FGrid"  ID="gvVendedor" runat="server" AutoGenerateColumns="False" 
                                        ClientInstanceName="gvVendedor" EnableTheming="True" KeyFieldName="IdVendedor"
                                        OnCellEditorInitialize="gvVendedor_CellEditorInitialize" OnRowDeleting="gvVendedor_RowDeleting"
                                        OnRowInserting="gvVendedor_RowInserting" OnRowUpdating="gvVendedor_RowUpdating" OnCancelRowEditing ="gvVendedor_CancelRowEditing"
                                        OnStartRowEditing="gvVendedor_StartRowEditing" OnRowValidating="gvVendedor_RowValidating" OnCommandButtonInitialize="gvVendedor_CommandButtonInitialize"
                                        Theme="Office2010Black" Width="100%" StylesPopup-EditForm-ModalBackground-Opacity="90">
                                        <ClientSideEvents EndCallback="function (s, e) {
                                        if (s.cpShowPopup)
                                        {
                                            delete s.cpShowPopup;
                                            lbl.SetText(s.cpText);
                                            popup.Show();
                                        }
                                    }" />
                                        <StylesPopup>
                                            <EditForm>
                                                <ModalBackground Opacity="90">
                                                </ModalBackground>
                                            </EditForm>
                                        </StylesPopup>
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Nombre" FieldName="Nombre" ShowInCustomizationForm="True" VisibleIndex="1" CellStyle-HorizontalAlign="Left">
                                                <PropertiesTextEdit MaxLength="200"  >
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#242;n ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        <RegularExpression ValidationExpression="[a-zA-Z ñÑáéíóúÁÉÍÓÚ]*[0-9]*" ErrorText="El campo contiene informaci&#243;n inv&#225;lida."></RegularExpression>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Visible="True" VisibleIndex="1" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Zona" FieldName="Zona" ShowInCustomizationForm="True" VisibleIndex="2" CellStyle-HorizontalAlign="Left">
                                                <PropertiesTextEdit MaxLength="200"  >
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#242;n ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        <RegularExpression ValidationExpression="[a-zA-Z ñÑáéíóúÁÉÍÓÚ]*[0-9]*" ErrorText="El campo contiene informaci&#243;n inv&#225;lida."></RegularExpression>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Visible="True" VisibleIndex="2" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Unidad de Negocio" FieldName="unit4" VisibleIndex="3" Visible="False">
                                                 <EditFormSettings Visible="True" Caption="Unidad de Negocio" VisibleIndex="3"></EditFormSettings>
                                                 <PropertiesComboBox NullText="Seleccione una opci&#243;n" NullDisplayText="Seleccione una opci&#243;n">
                                                     <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n cargada." ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="El campo es requerido"></RequiredField>
                                                    </ValidationSettings>
                                                 </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Unidad de Negocio" FieldName="UnidadNegocio" ShowInCustomizationForm="True" VisibleIndex="4" CellStyle-HorizontalAlign="Left">
                                                <PropertiesTextEdit MaxLength="200"  >
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#242;n ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        <RegularExpression ValidationExpression="[a-zA-Z ñÑáéíóúÁÉÍÓÚ]*[0-9]*" ErrorText="El campo contiene informaci&#243;n inv&#225;lida."></RegularExpression>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Visible="False" VisibleIndex="4" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Login" FieldName="Login" ShowInCustomizationForm="True" VisibleIndex="5" CellStyle-HorizontalAlign="Left">
                                                <PropertiesTextEdit MaxLength="200"  >
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#242;n ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        <RegularExpression ValidationExpression="[a-zA-Z|.,-_]*" ErrorText="El campo contiene informaci&#243;n inv&#225;lida."></RegularExpression>
                                                        <RequiredField IsRequired="True" ErrorText="El campo es requerido"></RequiredField>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Visible="True" VisibleIndex="5" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Codigo Unidad" FieldName="unit1" VisibleIndex="7" Visible="False">
                                                 <EditFormSettings Visible="True" Caption="Codigo Unidad"></EditFormSettings>
                                                 <PropertiesComboBox NullText="Seleccione una opci&#243;n" NullDisplayText="Seleccione una opci&#243;n">
                                                     <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n cargada." ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="El campo es requerido"></RequiredField>
                                                    </ValidationSettings>
                                                 </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Codigo Unidad" FieldName="DescripcionUnidad" ShowInCustomizationForm="True" VisibleIndex="8" CellStyle-HorizontalAlign="Left">
                                                <PropertiesTextEdit MaxLength="200"  >
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#242;n ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        <RegularExpression ValidationExpression="[a-zA-Z ñÑáéíóúÁÉÍÓÚ]*[0-9]*" ErrorText="El campo contiene informaci&#243;n inv&#225;lida."></RegularExpression>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Visible="False" VisibleIndex="8" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Email" FieldName="CorreoElectronico" ShowInCustomizationForm="True" VisibleIndex="9" CellStyle-HorizontalAlign="Left">
                                                <PropertiesTextEdit MaxLength="200"  >
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        <RegularExpression ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$" ErrorText="El campo solo permite correos electrónicos en minúsculas."></RegularExpression>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Visible="True" VisibleIndex="9" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Base" FieldName="AeropuertoIATA" ShowInCustomizationForm="True" VisibleIndex="10" CellStyle-HorizontalAlign="Left">
                                                <PropertiesTextEdit MaxLength="200"  >
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#242;n ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                        <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        <RegularExpression ValidationExpression="[a-zA-Z ñÑáéíóúÁÉÍÓÚ]*[0-9]*" ErrorText="El campo contiene informaci&#243;n inv&#225;lida."></RegularExpression>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Visible="False" VisibleIndex="10" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Base" FieldName="IdBase" VisibleIndex="11" Visible="False">
                                                 <EditFormSettings Visible="True" Caption="Base" VisibleIndex="11"></EditFormSettings>
                                                 <PropertiesComboBox TextField="Base"  ValueField="IdBase" ValueType="System.Int32" NullText="Seleccione una opci&#243;n." NullDisplayText="Seleccione una opci&#243;n.">
                                                     <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n cargada." ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="El campo es requerido"></RequiredField>
                                                    </ValidationSettings>
                                                 </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataCheckColumn Caption="¿Activo?" FieldName="Status" ShowInCustomizationForm="True" VisibleIndex="12">
                                                    <PropertiesCheckEdit ValueChecked="1" ValueType="System.Int32" ValueUnchecked="0">
                                                    </PropertiesCheckEdit>
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button" Caption="Acciones" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" VisibleIndex="12">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </dx:GridViewCommandColumn>
                                        </Columns>
                                        <SettingsBehavior ConfirmDelete="True" />
                                        <StylesPager  Pager-CssClass="FNumPag"></StylesPager>
                                        <SettingsPager Position="TopAndBottom">
                                            <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                            </PageSizeItemSettings>
                                        </SettingsPager>
                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" ></SettingsEditing>
                                        <Settings ShowGroupPanel="True"  />
                                        <SettingsText ConfirmDelete="¿Desea eliminar?" />
                                       <SettingsPopup>
                                           <EditForm HorizontalAlign="Center" VerticalAlign="Above" Modal="true" CloseOnEscape="True" Width="400px" />
                                       </SettingsPopup>
                                        <SettingsSearchPanel Visible="true" />
                                        <SettingsCommandButton>
                                            <NewButton ButtonType="Link">
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
                                    <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvVendedor">
                                    </dx:ASPxGridViewExporter>
                                </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnNuevo2" EventName="Click" />
                            <asp:PostBackTrigger ControlID="ASPxButton2" />
                            <asp:PostBackTrigger ControlID="btnExcel" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-6"><dx:ASPxButton CssClass="FBotton" ID="btnNuevo2" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click"></dx:ASPxButton></div>
                    <div class="col-sm-6" style="text-align:right;"><dx:ASPxLabel CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>&nbsp;<dx:ASPxButton CssClass="FBotton" ID="ASPxButton2" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportar_Click"></dx:ASPxButton></div>
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

