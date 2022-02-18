<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmFacturaProveedor.aspx.cs" Inherits="ALE_MexJet.Views.CreditoCobranza.frmFacturaProveedor" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="~/../../../JS/jquery/jquery-1.8.3.js"></script>
    <script type="text/javascript">

        var lastCountry = null;

        function OnCountryChanged(cmbCountry) {
            document.getElementById("<%=HiddenField1.ClientID%>").value = cmbCountry.GetValue().toString();
            if (gvBitacora.GetEditor("FolioReal").InCallback())
                lastCountry = cmbCountry.GetValue().toString();
            else
                gvBitacora.GetEditor("FolioReal").PerformCallback(cmbCountry.GetValue().toString());
        }

        function OnEndCallback(s, e) {
            if (lastCountry) {
                gvBitacora.GetEditor("FolioReal").PerformCallback(lastCountry);
                lastCountry = null;
            }
        }

        var lastFlota = null;

        function OnFlotaChanged(cmbFlota) {
            document.getElementById("<%=HiddenField2.ClientID%>").value = cmbFlota.GetValue().toString();
            if (gvBitacora.GetEditor("FolioReal").InCallback())
                lastCountry = cmbFlota.GetValue().toString();
            else
                gvBitacora.GetEditor("FolioReal").PerformCallback(cmbFlota.GetValue().toString());
        }

        function OnSaveClick(s, e) {
            gvBitacora.UpdateEdit();
        }

        function Salir(s, e)
        {
            ppFactura.Hide();
        }

        function suma(s, e)
        {
            var IVA;
            var Subtotal;
            if (txtIVA.GetValue() == null)
                IVA = 0;
            else
                IVA = parseFloat(txtIVA.GetValue());

            if (txtSubtotal.GetValue() == null)
                Subtotal = 0;
            else
                Subtotal = parseFloat(txtSubtotal.GetValue());

            var Total = IVA + Subtotal;
            txtTotal.SetText(Total);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Factura Proveedor</span>
                    </div>
                </div>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
                <div class="row">
                    <br />
                    <div class="col-md-12">
                        <fieldset class="Personal">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                            </legend>
                            <div class="col-md-12">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 25%; text-align: center;">
                                            <dx:ASPxLabel runat="server" Text="Proveedor: " Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td style="width: 25%; text-align: center;">
                                            <dx:ASPxTextBox ID="txtProveedor" runat="server" Theme="Office2010Black" NullText="Ingrese Proveedor."></dx:ASPxTextBox>
                                        </td>
                                        <td style="width: 25%; text-align: center;">
                                            <dx:ASPxLabel runat="server" Text="Cliente: " Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td style="width: 25%; text-align: center;">
                                            <dx:ASPxComboBox ID="ddlCliente" runat="server" Theme="Office2010Black" NullText="Seleccione una opción."></dx:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 15px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; text-align: center;">
                                            <dx:ASPxLabel runat="server" Text="Fecha: " Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td style="width: 25%; text-align: center;">
                                            <dx:ASPxDateEdit ID="deFecha" runat="server" Theme="Office2010Black">
                                                <ValidationSettings Display="Dynamic" SetFocusOnError="True">
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td style="width: 25%; text-align: center;"></td>
                                        <td style="width: 25%; text-align: center;">
                                            <dx:ASPxButton ID="btnBuscar" runat="server" Text="Buscar" Theme="Office2010Black" OnClick="btnBuscar_Click"></dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="well-g">
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click"></dx:ASPxButton>
                            <br />
                            <br />
                            <asp:UpdatePanel runat="server" ID="upGv" OnUnload="UpdatePanel1_Unload" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div>
                                        
                                        <dx:ASPxGridView ID="gvFacturaProveedor" runat="server" AutoGenerateColumns="False" Font-Size="Small" ToolTip="Resultado"
                                            ClientInstanceName="gvAeronave" EnableTheming="True" Width="100%" Styles-Header-HorizontalAlign="Center" OnRowCommand="gvFacturaProveedor_RowCommand"
                                            Theme="Office2010Black" StylesPopup-EditForm-ModalBackground-Opacity="90" KeyFieldName="IdFacturaProveedor">
                                            <Columns>
                                                
                                                <dx:GridViewDataTextColumn FieldName="Provedor" Caption="Provedor" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AeronaveMatricula" Caption="Matrícula" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="VueloClienteId" Caption="Código Cliente" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="{0:dd-MM-yyyy}">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataDateColumn FieldName="Fecha" Caption="Fecha" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" >
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataTextColumn FieldName="Factura" Caption="Factura" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Total" Caption="Total" VisibleIndex="9" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TipoMoneda" Caption="Tipo Moneda" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Remisionado" Caption="Remisionado" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TotalRemisionDlls" Caption="Total Remisión" VisibleIndex="12" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                 
                                                <dx:GridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="13" CellStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxButton Theme="Office2010Black" ID="btnEditar" Text="Detalle" runat="server" CommandArgument='<%# Eval("IdFacturaProveedor") %>' CommandName="Detalle" AutoPostBack="true" HorizontalAlign="Center">
                                                        </dx:ASPxButton>
                                                        <dx:ASPxButton Theme="Office2010Black" ID="btEliminar" Text="Eliminar" runat="server" CommandArgument='<%# Eval("IdFacturaProveedor") %>' CommandName="Eliminar" AutoPostBack="true" HorizontalAlign="Center">
                                                            <ClientSideEvents Click="function(s, e){  e.processOnServer = confirm('¿Está seguro que desea eliminar el registro?');}" />
                                                        </dx:ASPxButton>
                                                    </DataItemTemplate>
                                                    <EditFormSettings Visible="false" />
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                            <SettingsPager Position="TopAndBottom" Visible="true">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel  Visible="true"/>
                                        </dx:ASPxGridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <br />
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

    <dx:ASPxPopupControl ID="popup" runat="server" ClientInstanceName="popup" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Below" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true" Width="300">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="pnlPopUp" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxImage ID="AspxImagen2" runat="server" ShowLoadingImage="true" ImageUrl="~/img/iconos/Information2.ico"></dx:ASPxImage>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbl" runat="server" ClientInstanceName="lbl" Text="AspxLabel"></dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btOK" runat="server" Text="OK" Theme="Office2010Black" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) 
                                                                 {
                                                                    popup.Hide(); 
                                                                    ppFactura.Hide();
                                                                 }" />
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

    <dx:ASPxPopupControl ID="ppFactura" runat="server" ClientInstanceName="ppFactura" Width="1020px" Height="200px" CloseAction="CloseButton" CloseOnEscape="true" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Factura Renta Proveedor" Theme="Office2010Black">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel4" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 25%">
                                        <dx:ASPxLabel runat="server" Text="Proveedor: " Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td style="width: 25%">
                                        <dx:ASPxTextBox ID="txtPProveedor" ClientInstanceName ="txtPProveedor" runat="server" Theme="Office2010Black">
                                            <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="clientContainer" Display="Dynamic">
                                                <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td style="width: 25%">
                                        <dx:ASPxLabel runat="server" Text="Factura: " Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td style="width: 25%">
                                         <dx:ASPxTextBox ID="txtFactura"  ClientInstanceName="txtFactura" runat="server" Theme="Office2010Black">
                                            <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="clientContainer" Display="Dynamic">
                                                <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                <RegularExpression ValidationExpression="[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ| |-]*" ErrorText="Solo numeros, letras o -" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                        <dx:ASPxLabel runat="server" Text="Subtotal: " Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td style="width: 25%">
                                        <dx:ASPxTextBox ID="txtSubtotal"  ClientInstanceName="txtSubtotal" runat="server" Theme="Office2010Black">
                                            <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="clientContainer" Display="Dynamic">
                                                <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                    <RegularExpression ValidationExpression="\d+(\.)?\d*" ErrorText="El campo permite solo números."></RegularExpression>
                                            </ValidationSettings>
                                            <ClientSideEvents  KeyUp="function(s,e) { suma(s,e); } "/>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td style="width: 25%">
                                        <dx:ASPxLabel runat="server" Text="IVA: " Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td style="width: 25%">
                                        <dx:ASPxTextBox ID="txtIVA" ClientInstanceName="txtIVA" runat="server" Theme="Office2010Black">
                                            <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="clientContainer" Display="Dynamic">
                                                <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                    <RegularExpression ValidationExpression="\d+(\.)?\d*" ErrorText="El campo permite solo números."></RegularExpression>
                                            </ValidationSettings>
                                            <ClientSideEvents  KeyUp="function(s,e) { suma(s,e); } "/>
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 25%">
                                        <dx:ASPxLabel runat="server" Text="Total: " Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td style="width: 25%">
                                        <dx:ASPxTextBox ID="txtTotal" ClientInstanceName="txtTotal" runat="server" Theme="Office2010Black">
                                            <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="clientContainer" Display="Dynamic">
                                                <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                    <RegularExpression ValidationExpression="\d+(\.)?\d*" ErrorText="El campo permite solo números."></RegularExpression>
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td style="width: 25%">
                                        <dx:ASPxLabel runat="server" Text="Moneda: " Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td style="width: 25%">
                                        <dx:ASPxComboBox ID="ddlMoneda"  ClientInstanceName="ddlMoneda" runat="server" Theme="Office2010Black">
                                            <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="clientContainer">
                                                <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 25%">
                                        <dx:ASPxLabel runat="server" Text="Fecha Factura: " Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td style="width: 25%">
                                        <dx:ASPxDateEdit ID="deFechaFactura" runat="server" Theme="Office2010Black">
                                                <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="True" ErrorTextPosition="Bottom" ValidationGroup="clientContainer">
                                                    <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                        
                                    </td>
                                    <td style="height: 25%">
                                        <%--<dx:ASPxLabel runat="server" Text="Tipo Cambio: " Theme="Office2010Black"></dx:ASPxLabel>--%>
                                        <dx:ASPxButton ID="btnTipoCambio" Text="Tipo de Cambio" runat="server" Theme="Office2010Black" OnClick="btnTipoCambio_Click"></dx:ASPxButton>
                                    </td>
                                    <td style="width: 25%">
                                        <dx:ASPxLabel runat="server" Id="lblTipoCambio" Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 15px"></td>
                                </tr>

                                <tr>
                                    <td colspan="4">
                                        <asp:Panel ID="PDetallefactura" runat="server">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <br />
                                                    <b>
                                                        <asp:Label ID="Label2" runat="server" Text="Alta Detalle Facuras"></asp:Label>
                                                    </b>
                                                    
                                                    <br />
                                                </div>
                                            </div>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                
                                                                <br />
                                                                <br />
                                                            </div>
                                                        </div>
                                                        <dx:ASPxGridView ID="gvBitacora" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                            ClientInstanceName="gvBitacora" EnableTheming="True" KeyFieldName="IdBitacora"
                                                            OnCellEditorInitialize="gvBitacora_CellEditorInitialize" OnRowDeleting="gvBitacora_RowDeleting"
                                                            OnRowInserting="gvBitacora_RowInserting" OnRowUpdating="gvBitacora_RowUpdating"
                                                            OnStartRowEditing="gvBitacora_StartRowEditing" OnRowValidating="gvBitacora_RowValidating"
                                                            Styles-Header-HorizontalAlign="Center"
                                                            Theme="Office2010Black" Width="100%" OnCancelRowEditing="gvBitacora_CancelRowEditing">
                                                            <ClientSideEvents EndCallback="function (s, e) {
                                                                                    if (s.cpShowPopup)
                                                                                    {
                                                                                        delete s.cpShowPopup;
                                                                                        lbl.SetText(s.cpText);
                                                                                        popup.Show();
                                                                                    }
                                                                                }" />
                                                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick ="true" 
                                                                AllowSelectSingleRowOnly ="false"  
                                                                AllowDragDrop="false" ConfirmDelete="True"></SettingsBehavior>
                                                            <Columns>
                                                               
                                                               <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" VisibleIndex="0" Caption="Seleccione"></dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn FieldName="FolioReal" Caption="No. Bitácora" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                                                                </dx:GridViewDataTextColumn>
                                                                
                                                                <dx:GridViewDataTextColumn FieldName="IdBitacora" Caption="No. Bitácora" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" Visible="false">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="AeronaveMatricula" Caption="Matrícula" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="RUTA" Caption="RUTA" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="OrigenCalzo" Caption="Origen Calzo" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="TIEMPOVUELO" Caption="Tiempo Vuelo" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="TIEMPOCALZO" Caption="Tiempo Calzo" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="TripNum" Caption="TRIP" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center">
                                                                </dx:GridViewDataTextColumn>
                                                                
                                                                <%--<dx:GridViewDataComboBoxColumn Caption="Matricula" FieldName="Matricula" ShowInCustomizationForm="True" VisibleIndex="0" Visible="true" Width="30%">
                                                                    <EditFormSettings Visible="True"></EditFormSettings>
                                                                    <PropertiesComboBox NullText="Seleccione una opci&#243;n" NullDisplayText="Seleccione una opci&#243;n" ValueField="Matricula"
                                                                        EnableSynchronization="False" IncrementalFilteringMode="StartsWith">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { OnCountryChanged(s); }" />
                                                                        <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                                            <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                                            <RegularExpression ErrorText="El campo contiene informaci&#243;n inv&#225;lida."></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </PropertiesComboBox>
                                                                </dx:GridViewDataComboBoxColumn>

                                                                <dx:GridViewDataComboBoxColumn Caption="FolioReal" FieldName="FolioReal" ShowInCustomizationForm="True" VisibleIndex="1" Visible="true" Width="30%">
                                                                    <EditFormSettings Visible="True"></EditFormSettings>
                                                                    <PropertiesComboBox NullText="Seleccione una opci&#243;n" NullDisplayText="Seleccione una opci&#243;n"
                                                                        EnableSynchronization="False" IncrementalFilteringMode="StartsWith">
                                                                        <ClientSideEvents EndCallback="OnEndCallback" />
                                                                        <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                                            <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                                            <RegularExpression ErrorText="El campo contiene informaci&#243;n inv&#225;lida."></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </PropertiesComboBox>
                                                                </dx:GridViewDataComboBoxColumn>--%>

                                                            </Columns>
                                                             <SettingsPager Position="TopAndBottom" Visible="true" PageSize="5">
                                                                <PageSizeItemSettings Items="5, 10, 20, 30, 40, 50" Visible="true">
                                                                </PageSizeItemSettings>
                                                            </SettingsPager>
                                                            <SettingsSearchPanel Visible="true" />
                                                            <Settings  VerticalScrollBarMode="Auto"/>
                                                        </dx:ASPxGridView>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="height: 15px"></td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" Theme="Office2010Black" OnClick="btnGuardar_Click" ValidationGroup="clientContainer">
                                             
                                        </dx:ASPxButton>
                                    </td>
                                    <td colspan="2" style="text-align: center">
                                        <dx:ASPxButton ID="btnSalir" runat="server" Text="Salir" Theme="Office2010Black" OnClick="btnSalir_Click">
                                            <ClientSideEvents Click=" function(s,e) { Salir(s, e);  }" />
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
