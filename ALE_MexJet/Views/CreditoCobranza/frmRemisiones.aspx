<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmRemisiones.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ALE_MexJet.Views.CreditoCobranza.frmRemisiones" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var Valor1;
        var Valor2;
        function OnValueChanged(s) {
            
            var result;
            result = confirm("¿ Realmente esta seguro de cancelar la remisión ?");
            document.getElementById("<%=hfValor.ClientID%>").value = result;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<cc1:ToolkitScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
    </cc1:ToolkitScriptManager>--%>

    <dx:ASPxPanel ID="pnlPrincipal" runat="server" Width="100%" BackColor="White" style="border-radius: 14px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Remisiones</span>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" OnUnload="UpdatePanel1_Unload">
                    <ContentTemplate>
                        
                        <div class="well-g">
                                <div class="row">
                                    <div class="col-md-12">

                                        <fieldset class="Personal">
                                            <legend>
                                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda general</span>
                                            </legend>
                                            <div class="col-sm-12">
                                                <table width="100%" style="text-align: center">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtTextoBusqueda" runat="server" Width="180px" Theme="Office2010Black" NullText="Ingrese información a buscar."></dx:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxComboBox ID="ddlTipoBusqueda" runat="server" Theme="Office2010Black" EnableTheming="True">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Folio de Remisión" Value="1" Selected="true"></dx:ListEditItem>
                                                                    <dx:ListEditItem Text="Clave de Cliente" Value="2"></dx:ListEditItem>
                                                                    <dx:ListEditItem Text="Clave de Contrato" Value="3"></dx:ListEditItem>
                                                                    <dx:ListEditItem Text="Ultimas" Value="0" Selected="true"></dx:ListEditItem>
                                                                </Items>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnBusqueda" Theme="Office2010Black" Text="Buscar" runat="server"></dx:ASPxButton>
                                                        </td>
                                                        <td>&nbsp; <asp:HiddenField ID="hfValor" runat="server" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click"></dx:ASPxButton>
                                    </div>
                                    <div class="col-md-6" style="text-align: right;">
                                        <dx:ASPxLabel runat="server" Theme="Aqua" Text="Exportar a:"></dx:ASPxLabel>
                                        &nbsp;<dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black"></dx:ASPxButton>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="margin-left: -15px; width: 102%;">

                                        <%--<asp:UpdatePanel ID="upaInicio" runat="server" UpdateMode="Always">
                                            <ContentTemplate>--%>
                                                <div class="col-sm-12">
                                                    <dx:ASPxGridView ID="gvRemisiones" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                        ClientInstanceName="gvRemisiones" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                                        Theme="Office2010Black" Width="100%" OnRowCommand="gvRemisiones_RowCommand" KeyFieldName ="FolioRemision"
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
                                                            <dx:GridViewDataTextColumn Caption="Folio Remisión" FieldName="FolioRemision" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Fecha Remisión" FieldName="FechaRemision" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Cliente" FieldName="ClaveCliente" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Contrato" FieldName="ClaveContrato" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Fecha de Vuelo" FieldName="FechaVuelo" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Matrícula" FieldName="Matricula" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Ruta" FieldName="Ruta" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataProgressBarColumn FieldName="Completado" ShowInCustomizationForm="True" VisibleIndex="8" />
                                                            <dx:GridViewDataTextColumn Caption="Estatus" FieldName="Estatus" ShowInCustomizationForm="True" VisibleIndex="9">
                                                            </dx:GridViewDataTextColumn>                                                            

                                                            <dx:GridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="10">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <DataItemTemplate>
                                                                    <dx:ASPxButton Text="Editar" Theme="Office2010Black" ID="btn" OnInit ="btn_Init" runat="server" CommandArgument='<%# Eval("FolioRemision") %>' CommandName="Detalle" AutoPostBack="true" ToolTip="Detalle">
                                                                    </dx:ASPxButton>
                                                                    <dx:ASPxButton Text="Cancelar" Theme="Office2010Black" ID="btnEliminarSolicitud" runat="server" CommandArgument='<%# Eval("FolioRemision") %>' CommandName="Eliminar" AutoPostBack="true" ToolTip="Eliminar">
                                                                    <ClientSideEvents Click="function(s, e) { OnValueChanged(s); }"/>
                                                                    </dx:ASPxButton>

                                                                    <dx:ASPxButton Text="Ajuste Remisión" Theme="Office2010Black" ID="btnAjuste" runat="server" CommandArgument='<%# Eval("FolioRemision") %>' CommandName="Ajuste" AutoPostBack="true" 
                                                                        ToolTip="Agregar Ajuste de Remisión"></dx:ASPxButton>

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
                                                    <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvRemisiones">
                                                    </dx:ASPxGridViewExporter>
                                                </div>
                                            <%--</ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>--%>

                                    </div>
                                </div>
                            <br />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnNuevo"/>
                        <asp:PostBackTrigger ControlID="btnExcel" />
                    </Triggers>
                </asp:UpdatePanel>
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





    <div class="well-g">
        <asp:Panel ID="pnlAjuste" runat="server" Visible="false">
            <asp:UpdatePanel ID="upaAjuste" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <div style="width:50%; margin:0 auto 0 auto;">
                        <div class="row" style="padding:0 10px 0 10px;">
                            <div class="col-lg-12" align="center">
                                <h4>
                                    Solicitud de Autorización de Ajuste
                                </h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <dx:ASPxLabel ID="lblCargoAbono" runat="server" Text="Cargo/Abono"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-9">
                                <dx:ASPxComboBox ID="ccbMotivo" runat="server"></dx:ASPxComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <dx:ASPxLabel ID="lblHoras" runat="server" Text="Horas"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-9">
                                <dx:ASPxTextBox ID="txtHoras" runat="server">
                                    <MaskSettings Mask="HH:mm" IncludeLiterals="All" ShowHints="true" />
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="ValidHr">
                                        <RequiredField IsRequired="true" ErrorText="Formato incorrecto" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
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
                        <div class="row">
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
        </asp:Panel>
    </div>



     
</asp:Content>
