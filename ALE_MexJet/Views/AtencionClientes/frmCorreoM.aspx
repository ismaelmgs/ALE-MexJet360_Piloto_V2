<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmCorreoM.aspx.cs" Inherits="ALE_MexJet.Views.AtencionClientes.frmCorreoM" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>

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
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Envio de Correo</span>
                    </div>
                </div>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <div class="well-g">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="Personal">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"></span>
                                </legend>
                                <div class="col-sm-12">
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" OnUnload="UpdatePanel1_Unload" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div>
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 15%; text-align: center">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Motivo : "></dx:ASPxLabel>
                                                </td>
                                                <td style="width: 65%">
                                                    <dx:ASPxTextBox ID="txtMotivo" runat="server" Theme="Office2010Black" NullText="Motivo" Width="100%">
                                                        <ValidationSettings ErrorDisplayMode="Text" ValidationGroup="Validacion">
                                                            <RequiredField  IsRequired="true" ErrorText="El campo es requerido."/>
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="width: 20%; text-align: center"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 15px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; text-align: center">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Asunto : "></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox ID="txtAsunto" runat="server" Theme="Office2010Black" NullText="Asunto" Width="100%">
                                                    <ValidationSettings ErrorDisplayMode="Text" ValidationGroup="Validacion">
                                                            <RequiredField  IsRequired="true" ErrorText="El campo es requerido."/>
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 15px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; text-align: center">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Para : "></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox ID="txtPara" runat="server" Theme="Office2010Black" NullText="Para" Width="100%">
                                                        <ValidationSettings ErrorDisplayMode="Text" ValidationGroup="Validacion">
                                                            <RequiredField  IsRequired="true" ErrorText="El campo es requerido."/>
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" OnUnload="UpdatePanel1_Unload">
                                                        <ContentTemplate>
                                                            <dx:ASPxButton ID="btnSeleccioneP" runat="server" Theme="Office2010Black" Text="Seleccione" OnClick="btnSeleccioneP_Click">
                                                            </dx:ASPxButton>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 15px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; text-align: center">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Cc : "></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox ID="txtCc" runat="server" Theme="Office2010Black" NullText="Cc" Width="100%">
                                                        
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3" OnUnload="UpdatePanel1_Unload">
                                                        <ContentTemplate>
                                                            <dx:ASPxButton ID="btnSeleccionCc" runat="server" Theme="Office2010Black" Text="Seleccione"  OnClick="btnSeleccionCc_Click">
                                                            </dx:ASPxButton>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:CheckBox ID="chNombre" runat="server"  OnCheckedChanged="chNombre_CheckedChanged" AutoPostBack="true" Text="Nombre" />
                            <br />
                            <asp:CheckBox ID="chContrato" runat="server"  OnCheckedChanged="chContrato_CheckedChanged" AutoPostBack="true" Text="Cliente" />
                            <br />
                            <asp:CheckBox ID="chRazonSocial" runat="server"  OnCheckedChanged="chRazonSocial_CheckedChanged" AutoPostBack="true" Text="Razón Social" />
                            <br />
                            <dx:ASPxHtmlEditor ID="DemoHtmlEditor"  runat="server" Width="100%" Settings-AllowHtmlView="true" Theme="Office2010Black" Height="700px" 
                                Settings-AllowDesignView="true" Settings-AllowPreview="false">
                               <Settings AllowPreview ="false" />

                                <SettingsDialogs>
                                    <InsertImageDialog>
                                        <SettingsImageSelector>
                                            <ToolbarSettings ShowCreateButton="False" ShowRenameButton="False" ShowMoveButton="False" ShowDeleteButton="False" ShowRefreshButton="False" ShowFilterBox="False" ShowDownloadButton="False" ShowCopyButton="False"></ToolbarSettings>
                                        </SettingsImageSelector>
                                    </InsertImageDialog>
                                </SettingsDialogs>
                            </dx:ASPxHtmlEditor>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div style="text-align: right">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional" OnUnload="UpdatePanel1_Unload">
                                    <ContentTemplate>
                                        <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Guardar : "></dx:ASPxLabel>
                                        &nbsp;<dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar " Theme="Office2010Black" ToolTip="Enviar" OnClick="btnGuardar_Click" ValidationGroup="Validacion">
                                              </dx:ASPxButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Enviar : "></dx:ASPxLabel>
                                        &nbsp;<dx:ASPxButton ID="btnEnviar" runat="server" Text="Enviar" Theme="Office2010Black" ToolTip="Enviar" OnClick="btnEnviar_Click" ValidationGroup="Validacion">
                                              </dx:ASPxButton>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnEnviar" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

    <dx:ASPxPopupControl ID="ppContactos" runat="server" ClientInstanceName="ppContactos" Width="400px" Height="200px" CloseAction="CloseButton" CloseOnEscape="true" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Contactos">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel5" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <dx:ASPxGridView ID="gvcontactos" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                            ClientInstanceName="gvcontactos" EnableTheming="True" KeyFieldName="IdContacto"
                                            Styles-Header-HorizontalAlign="Center" Theme="Office2010Black" Width="100%">
                                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="false"
                                                AllowDragDrop="false" ConfirmDelete="True"></SettingsBehavior>
                                            <Columns>
                                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" VisibleIndex="0" Caption="Seleccione"></dx:GridViewCommandColumn>

                                                <dx:GridViewDataTextColumn FieldName="Nombre" Caption="Nombre" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CorreoElectronico" Caption="CorreoElectronico" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CodigoCliente" Caption="Cliente" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TipoContactoDescripcion" Caption="Tipo Contacto" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TelOficina" Caption="TelOficina" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TelMovil" Caption="TelMovil" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="OtroTel" Caption="OtroTel" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsPager Position="TopAndBottom" Visible="true" PageSize="5">
                                                <PageSizeItemSettings Items="5, 10, 20, 30, 40, 50" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="true" />
                                        </dx:ASPxGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height:15px"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAceptar" runat="server" Theme="Office2010Black" Text="Aceptar" OnClick="btnAceptar_Click">
                                            <ClientSideEvents Click="function() { ppContactos.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnCancelaP" runat="server" Theme="Office2010Black" OnClick="btnCancelaP_Click" Text="Cancelar">
                                            <ClientSideEvents Click="function() { ppContactos.Hide();} " />
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
