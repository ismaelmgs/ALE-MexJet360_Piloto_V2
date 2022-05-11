<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmAjustes.aspx.cs" Inherits="ALE_MexJet.Views.CreditoCobranza.frmAjustes" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%--<asp:UpdatePanel ID="upaPrincipal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>

    
    <%--<dx:ASPxPanel ID="pnlPrincipal" runat="server" Width="100%" BackColor="White" style="border-radius: 14px;">
        <PanelCollection>
            <dx:PanelContent>--%>

            <div class="row">
                <div class="col-md-12">
                    <br />
                    <fieldset class="Personal">
                        <legend>
                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Ajustes de Horas JetCard</span>
                        </legend>
                        <div class="col-sm-12">


                            <table width="50%" style="text-align: left;">
                                <tr>
                                    <td colspan="2">Contrato:<dx:ASPxComboBox ID="ddlContrato" runat="server" Theme="Office2010Black" NullText="- Selecciona -"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlContrato_SelectedIndexChanged" Width="95%">
                                    </dx:ASPxComboBox>                    

                                    </td>
                                    <td align="left" valign="bottom">&nbsp;
                                        <dx:ASPxButton ID="btnSolicitarAjuste" runat="server" Text="Solicitar Ajuste" Theme="Office2010Black" OnClick="btnSolicitarAjuste_Click"></dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </fieldset>
                </div>
            </div>
            <br />
            <asp:Panel ID="pnlRemisiones" runat="server" Visible="false">
                <div class="row">
                    <div class="col-md-12" style="margin-left: -15px; width: 102%;">
                        <asp:UpdatePanel  ID="UpdatePanel1" runat="server" UpdateMode="Always" OnUnload="UpdatePanel1_Unload">
                            <ContentTemplate>
                                <div class="col-sm-12">
                                    <%--<dx:ASPxGridView ID="gvRemisiones" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                        EnableTheming="True"  Styles-Header-HorizontalAlign="Center" ClientInstanceName="gvRemisiones"
                                        Theme="Office2010Black" Width="100%" StylesPopup-EditForm-ModalBackground-Opacity="90"
                                        KeyFieldName="IdRemision"   
                                        OnRowCommand="gvRemisiones_RowCommand">
                                        <ClientSideEvents EndCallback="function (s, e) {
                                                    if (s.cpShowPopup)
                                                    {
                                                        delete s.cpShowPopup;
                                                        lbl.SetText(s.cpText);
                                                        popup.Show();
                                                    }
                                                }" />
                                        <Columns>
                                           
                                            <dx:GridViewDataTextColumn Caption="Folio Remisión" FieldName="IdRemision" ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Fecha Remisión" FieldName="FechaRemision" ShowInCustomizationForm="True" VisibleIndex="2">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Cliente" FieldName="ClaveCliente" ShowInCustomizationForm="True" VisibleIndex="3">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Fecha de Vuelo" FieldName="FechaVuelo" ShowInCustomizationForm="True" VisibleIndex="4">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Matrícula" FieldName="Matricula" ShowInCustomizationForm="True" VisibleIndex="5">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Ruta" FieldName="Ruta" ShowInCustomizationForm="True" VisibleIndex="6">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Contrato" FieldName="ClaveContrato" ShowInCustomizationForm="False" VisibleIndex="7" EditFormSettings-Visible="False">
                                            </dx:GridViewDataTextColumn>
                                           
                                            <dx:GridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="8">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <DataItemTemplate>

                                                    <dx:ASPxButton Text="Ajuste" Theme="Office2010Black" ID="btnAjuste" runat="server" CommandArgument='<%# Eval("IdRemision") %>' CommandName="Ajuste" AutoPostBack="true" 
                                                        ToolTip="Agregar Ajuste"></dx:ASPxButton>

                                                </DataItemTemplate>
                                                <EditFormSettings Visible="false" />
                                            </dx:GridViewDataColumn>

                                        </Columns>
                                        <SettingsPager Position="TopAndBottom">
                                            <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                            </PageSizeItemSettings>
                                        </SettingsPager>
                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                                        <Settings ShowGroupPanel="True" />
                                        <SettingsPopup>
                                            <EditForm HorizontalAlign="Center" VerticalAlign="Below" Width="400px" />
                                        </SettingsPopup>
                                    </dx:ASPxGridView>--%>



                                    <dx:ASPxGridView ID="gvRemisiones" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                        ClientInstanceName="gvRemisiones" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                                        Theme="Office2010Black" Width="100%" OnRowCommand="gvRemisiones_RowCommand" KeyFieldName ="IdRemision"
                                                        OnCommandButtonInitialize ="gvRemisiones_CommandButtonInitialize"   
                                                        OnCustomButtonInitialize ="gvRemisiones_CustomButtonInitialize"                                                         
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
                                                            <dx:GridViewDataTextColumn Caption="Folio Remisión" FieldName="IdRemision" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Fecha Remisión" FieldName="FechaRemision" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Cliente" FieldName="ClaveCliente" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Fecha de Vuelo" FieldName="FechaVuelo" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Matrícula" FieldName="Matricula" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Ruta" FieldName="Ruta" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Contrato" FieldName="ClaveContrato" ShowInCustomizationForm="False" VisibleIndex="7" EditFormSettings-Visible="False">
                                                            </dx:GridViewDataTextColumn>                                                           

                                                            <dx:GridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="8">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <DataItemTemplate>

                                                                    <dx:ASPxButton Text="Ajuste" Theme="Office2010Black" ID="btnAjuste" runat="server" CommandArgument='<%# Eval("IdRemision") %>' CommandName="Ajuste" AutoPostBack="true" 
                                                                        ToolTip="Agregar Ajuste"></dx:ASPxButton>

                                                                </DataItemTemplate>
                                                                <EditFormSettings Visible="false" />
                                                            </dx:GridViewDataColumn>

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
                                                            <EditForm HorizontalAlign="Center" VerticalAlign="Below" Width="400px" />
                                                        </SettingsPopup>
                                                        <SettingsSearchPanel Visible="true" />
                                                        <SettingsCommandButton>

                                                            <NewButton ButtonType="Link">
                                                                <Image ToolTip="New">
                                                                </Image>
                                                            </NewButton>
                                                            <UpdateButton Text="Guardar"></UpdateButton>
                                                            <CancelButton Text ="Cancelar"></CancelButton>
                                                            <EditButton Text="Editar"></EditButton>
                                                        </SettingsCommandButton>
                                                    </dx:ASPxGridView>





                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlAjuste" runat="server" Visible="false">
                <div class="row">
                    <div class="col-md-12" style="margin-left: -15px; width: 102%;">
                        <asp:UpdatePanel runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div style="width:50%; margin:0 auto 0 auto;">
                                    <div class="row" style="padding:0 10px 0 10px;">
                                        <div class="col-lg-12" align="center">
                                            <h4>
                                                Solicitud de Ajuste
                                            </h4>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <dx:ASPxLabel ID="lblContrato" runat="server" Text="Contrato"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-md-9">
                                            <dx:ASPxLabel ID="readContrato" runat="server" Text=""></dx:ASPxLabel>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <dx:ASPxLabel ID="lblTipo" runat="server" Text="Tipo"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-md-9">
                                            <dx:ASPxComboBox ID="ccbTipo" runat="server" NullText="- Selecciona -">
                                                <Items>
                                                    <dx:ListEditItem Value="1" Text="Cargo" />
                                                    <dx:ListEditItem Value="2" Text="Abono" />
                                                </Items>
                                            </dx:ASPxComboBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <dx:ASPxLabel ID="lblMotivo" runat="server" Text="Motivo"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-md-9">
                                            <dx:ASPxComboBox ID="ccbMotivo" runat="server" NullText="- Selecciona -" AutoPostBack="true" OnSelectedIndexChanged="ccbMotivo_SelectedIndexChanged"></dx:ASPxComboBox>
                                        </div>
                                    </div>

                                    <asp:Panel ID="pnlAgregarRem" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <dx:ASPxLabel ID="lblNumRemision" runat="server" Text="No. Remisión"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-md-9">
                                               <%-- <dx:ASPxLabel ID="readNumRemision" runat="server" Text=""></dx:ASPxLabel>--%>
                                                <dx:ASPxComboBox ID="ccbListaRemisiones" runat="server" NullText="- Selecciona -"></dx:ASPxComboBox>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    

                                    <div class="row">
                                        <div class="col-md-3">
                                            <dx:ASPxLabel ID="lblHoras" runat="server" Text="Horas"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-md-3">
                                            <dx:ASPxTextBox ID="txtHoras" runat="server">
                                                <MaskSettings Mask="HH:mm" IncludeLiterals="All" ShowHints="true" />
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="ValidHr">
                                                    <RequiredField IsRequired="true" ErrorText="Formato incorrecto" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </div>
                                        <div class="col-md-6" style="padding-left: 30px;">
                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="HH:MM"></dx:ASPxLabel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <dx:ASPxLabel ID="lblComentarios" runat="server" Text="Comentarios"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-md-9">
                                            <dx:ASPxMemo ID="txtComentarios" runat="server" Width="100%" Rows="3"></dx:ASPxMemo>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:20px;">
                                        <div class="col-md-6" align="right">
                                            <asp:HiddenField ID="hdnIdRemision" runat="server" />
                                            <dx:ASPxButton ID="btnAceptar" runat="server" Text="Aceptar" Theme="Office2010Black" OnClick="btnAceptar_Click"></dx:ASPxButton>
                                        </div>
                                        <div class="col-md-6" align="left">
                                            <dx:ASPxButton ID="btnCancelar" runat="server" Text="Cancelar" Theme="Office2010Black" OnClick="btnCancelar_Click"></dx:ASPxButton>
                                        </div>
                                    </div>
                                </div>

                                <%--MODAL PARA MENSAJES--%>
                                <dx:ASPxPopupControl ID="msgAlert" 
                                    runat="server" 
                                    Theme="Office2010Black"
                                    HeaderText="Aviso"
                                    CloseOnEscape="true"
                                    PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter"
                                    AllowResize="true"
                                    CloseAction="CloseButton"
                                    DisappearAfter="100"
                                    Width="300px"
                                    Height="100px"
                                    Modal="true"
                                    ShowFooter="false"
                                    AllowDragging="true"   
                                    ShowCloseButton="true" >
                                    <ClientSideEvents />
                                    <ContentCollection>
                                        <dx:PopupControlContentControl runat="server">
                                            <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="bt_OK">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxImage ID="ASPxImage1" runat="server" ShowLoadingImage="true" ImageUrl="~/img/iconos/Information2.ico"></dx:ASPxImage>
                                                                    <dx:ASPxTextBox ID="ASPxTextBox1" ReadOnly="true" Border-BorderStyle="None" Height="1px" runat="server" Width="1px" ClientInstanceName="tbLogin"></dx:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxLabel ID="lblMsg" runat="server" ClientInstanceName="lbl" Text="ASPxLabel"></dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxButton ID="bt_OK" runat="server" Text="OK" Theme="PlasticBlue" Width="80px" Style="float: left; margin-right: 8px" TabIndex="0" AutoPostBack="true" OnClick="bt_OK_Click">
                                                                        <%--<ClientSideEvents Click="function(s, e) {msgAlert.Hide(); }" />--%>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </asp:Panel>
            

        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
           <%-- </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>--%>


    
</asp:Content>
