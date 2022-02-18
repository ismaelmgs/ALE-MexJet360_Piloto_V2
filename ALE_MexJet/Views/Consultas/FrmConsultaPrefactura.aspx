<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" UICulture="es" Culture="es-MX" AutoEventWireup="true" CodeBehind="FrmConsultaPrefactura.aspx.cs" Inherits="ALE_MexJet.Views.Consultas.FrmConsultaPrefactura" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
    <dx:PanelContent>
        <div class="row header1">
            <div class="col-md-12">
                <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Consulta Prefactura</span>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <br />
                <fieldset class="Personal">
                    <legend>
                        <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                    </legend>
                    <table width="100%" border="0">
                        <tr>
                            <td width="10%" style="text-align: center;">&nbsp;
                            </td>
                            <td width="20%" style="text-align: center;">
                                <dx:ASPxLabel runat="server" Text="Clave Cliente:"></dx:ASPxLabel>
                            </td>
                            <td width="20%" style="text-align: center;">
                                <dx:ASPxComboBox ID="cboCveCliente" runat="server" Theme="Office2010Black" NullText="Seleccionar" AutoPostBack="true" OnSelectedIndexChanged="cboCveCliente_SelectedIndexChanged" ValueType="System.Int32"></dx:ASPxComboBox>
                            </td>
                            <td width="20%" style="text-align: center;">
                                <dx:ASPxLabel runat="server" Text="Clave Contrato:"></dx:ASPxLabel>
                            </td>
                            <td width="20%" style="text-align: center;">
                                <dx:ASPxComboBox ID="ClaveContrato" runat="server" Theme="Office2010Black" NullText="Seleccionar"></dx:ASPxComboBox>
                            </td>
                            <td width="10%" style="text-align: center;">&nbsp;
                            </td>
                            <tr>
                                <td colspan="6">&nbsp;</td>
                            </tr>
                        </tr>
                        <tr>
                            <td width="10%" style="text-align: center;">&nbsp;
                            </td>
                            <td width="20%" style="text-align: center;">
                                <dx:ASPxLabel runat="server" Text="Folio:"></dx:ASPxLabel>
                            </td>
                            <td width="20%" style="text-align: center;">
                                <dx:ASPxTextBox ID="Folio" runat="server" Theme="Office2010Black"></dx:ASPxTextBox>
                            </td>
                            <td width="20%" style="text-align: center;">&nbsp;
                            </td>
                            <td width="20%" style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<dx:ASPxButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" Theme ="Office2010Black"></dx:ASPxButton>
                            </td>
                            <td width="10%" style="text-align: center;">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">&nbsp;</td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click"></dx:ASPxButton>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="table-responsive">
                    <table class="table">
                        <dx:ASPxGridView ID="gvPrefactura" runat="server" Theme="Office2010Black" Width="100%" KeyFieldName="IdPrefactura"
                            AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" OnCommandButtonInitialize="gvPrefactura_CommandButtonInitialize" 
                            OnCustomButtonCallback="gvPrefactura_CustomButtonCallback" OnRowDeleting ="gvPrefactura_RowDeleting" 
                            OnCustomButtonInitialize ="gvPrefactura_CustomButtonInitialize"
                            >
                            <ClientSideEvents EndCallback="function (s, e) {
                                        if (s.cpShowPopup)
                                        {
                                            delete s.cpShowPopup;
                                            lbl.SetText(s.cpText);
                                            popup.Show();
                                        }
                                    }" />
                            <SettingsBehavior AllowDragDrop="true" AllowSort="true"  ConfirmDelete ="true"/>
                            <SettingsText ConfirmDelete ="¿Desea cancelar la prefactura?" />
                            <SettingsSearchPanel Visible="true" />
                            <SettingsPager Position="TopAndBottom">
                                <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                </PageSizeItemSettings>
                            </SettingsPager>
                            <Settings ShowGroupPanel="True" />
                            <Columns>
                                <%--<dx:GridViewDataTextColumn FieldName="" Caption="&nbsp;" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="IdPrefactura" Caption="Folio" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ClaveCliente" Caption="Cliente" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ClaveContrato" Caption="Contrato" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="FechaCreacion" Caption="Fecha Prefactura" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IdFactura" Caption="No. Factura Vuelo" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SubTotalDLLV" Caption="Subtotal Vuelo USD" VisibleIndex="1" PropertiesTextEdit-DisplayFormatString="{0:c4}">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IdFacturaSCC" Caption="No. Factura Servicio" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SubTotalMXNC" Caption="Subtotal SCC MXN   " VisibleIndex="1" PropertiesTextEdit-DisplayFormatString="{0:c4}">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataProgressBarColumn FieldName ="PorcentajeAvance" Caption ="Porcentaje" VisibleIndex="1"></dx:GridViewDataProgressBarColumn>
                                <%--<dx:GridViewDataTextColumn FieldName="PorcentajeAvance" Caption="Porcentaje" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>--%>
                                <%--<dx:GridViewDataTextColumn FieldName="Estatus" Caption="Estatus" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>--%>
                                <dx:GridViewCommandColumn ButtonType="Button" ShowInCustomizationForm="True" Caption="Acciones" VisibleIndex="6" ShowDeleteButton ="true">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="btnEdit" Text="Editar"></dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                            </Columns>
                            
                            <SettingsCommandButton>
                                <DeleteButton Text ="Cancelar"></DeleteButton>
                            </SettingsCommandButton>
                        </dx:ASPxGridView>
                    </table>
                </div>
            </div>
        </div>
        <br />
    </dx:PanelContent>

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

