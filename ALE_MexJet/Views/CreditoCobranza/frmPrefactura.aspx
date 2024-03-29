﻿        <%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" UICulture="es" Culture="es-MX" CodeBehind="frmPrefactura.aspx.cs" Inherits="ALE_MexJet.Views.CreditoCobranza.frmPrefactura" %>

    <%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
    <%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
        <asp:HiddenField ID="hdTipoFactura" runat="server" Value="0" />
        <div class="row header1">
            <div class="col-md-12">
                <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Prefactura</span>
            </div>
        </div>
        <br />
        <dx:ASPxPageControl ID="ASPxPageControl1" ActiveTabIndex="0" runat="server" Width="100%" Height="350px" TabAlign="Justify" EnableTabScrolling="True">
            <TabStyle Paddings-PaddingLeft="50px" Paddings-PaddingRight="50px" />
            <ContentStyle>
                <Paddings PaddingLeft="12px" />
            </ContentStyle>

            <%--inicia tab 01--%>
            <TabPages>
                <dx:TabPage Text="1. Nueva prefactura" Enabled="true">
                    <ContentCollection>
                        <dx:ContentControl>
                            <fieldset class="Personal1" style="min-height: 350px;">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Datos del cliente</span>
                                </legend>
                                <%--inicia contenido tab 01--%>
                                <table width="100%" style="margin-top: 40px;">
                                    <tr>
                                        <td colspan="7">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">&nbsp;
                                        </td>
                                        <td style="width: 19%" align="left">
                                            <dx:ASPxLabel ID="lblClaveCliente" runat="server" Text="Clave de cliente:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td style="width: 19%" align="center">
                                            <dx:ASPxComboBox runat="server" ID="cboClaveCliente" NullText="Seleccionar" Theme="Office2010Black"  AutoPostBack="true" OnSelectedIndexChanged="cboClaveCliente_SelectedIndexChanged">
                                                <ValidationSettings ValidateOnLeave ="true" ErrorDisplayMode ="Text" ErrorTextPosition ="Bottom" ValidationGroup ="btnSigNuevoG">
                                                    <RequiredField IsRequired="true" ErrorText="El campo es requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td style="width: 4%">&nbsp;
                                        </td>
                                        <td style="width: 19%" align="left">
                                            <dx:ASPxLabel ID="lblClaveContrato" runat="server" Text=" Clave de contrato:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td style="width: 19%" align="center">
                                            <dx:ASPxComboBox runat="server" ID="cboClaveContrato" NullText="Seleccionar" Theme="Office2010Black" AutoPostBack="true" OnValueChanged="cboClaveContrato_ValueChanged">
                                                <ValidationSettings ValidateOnLeave ="true" ErrorDisplayMode ="Text" ErrorTextPosition ="Bottom" ValidationGroup ="btnSigNuevoG">
                                                    <RequiredField IsRequired="true" ErrorText="El campo es requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td style="width: 10%">&nbsp;
                                        </td>
                                        <td style="width: 10%"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">&nbsp;
                                        </td>
                                        <td style="width: 19%" align="left">
                                            <dx:ASPxLabel ID="lblMonedaVuelo" runat="server" Text=" Moneda de Factura para vuelo:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td style="width: 19%" align="center">
                                            <dx:ASPxComboBox runat="server" ID="cboMonedaVuelo" NullText="Seleccionar" Theme="Office2010Black">
                                                <Items>
                                                    <dx:ListEditItem Text="MXN" Value="1" />
                                                    <dx:ListEditItem Text="USD" Value="2" Selected="true" />
                                                </Items>
                                                <ValidationSettings ValidateOnLeave ="true" ErrorDisplayMode ="Text" ErrorTextPosition ="Bottom" ValidationGroup ="btnSigNuevoG">
                                                    <RequiredField IsRequired="true" ErrorText="El campo es requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td style="width: 4%">&nbsp;
                                        </td>
                                        <td style="width: 19%" align="left">
                                            <dx:ASPxLabel ID="lblFacturanteVuelo" runat="server" Text="Facturante vuelo:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td style="width: 19%" align="center">
                                            <dx:ASPxComboBox runat="server" ID="cboFacturanteVuelo" NullText="Seleccionar" Theme="Office2010Black">
                                                <ValidationSettings ValidateOnLeave ="true" ErrorDisplayMode ="Text" ErrorTextPosition ="Bottom" ValidationGroup ="btnSigNuevoG">
                                                    <RequiredField IsRequired="true" ErrorText="El campo es requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td style="width: 10%">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">&nbsp;
                                        </td>
                                        <td style="width: 19%" align="left">
                                            <dx:ASPxLabel ID="lblMonedaServiciosCargo" runat="server" Text="Moneda de Factura para servicios con cargo:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td style="width: 19%" align="center">
                                            <dx:ASPxComboBox runat="server" ID="cboMonedaServiciosCargo" NullText="Seleccionar" Theme="Office2010Black">
                                                <Items>
                                                    <dx:ListEditItem Text="MXN" Value="1" Selected="true" />
                                                    <dx:ListEditItem Text="USD" Value="2" />
                                                </Items>
                                                <ValidationSettings ValidateOnLeave ="true" ErrorDisplayMode ="Text" ErrorTextPosition ="Bottom" ValidationGroup ="btnSigNuevoG">
                                                    <RequiredField IsRequired="true" ErrorText="El campo es requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td style="width: 4%">&nbsp;
                                        </td>
                                        <td style="width: 19%" align="left">
                                            <dx:ASPxLabel ID="lblFacturanteServiciosCargo" runat="server" Text="Facturante servicios con cargo:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td style="width: 19%" align="center">
                                            <dx:ASPxComboBox runat="server" ID="cboFacturanteServiciosCargo" NullText="Seleccionar" Theme="Office2010Black">
                                                <ValidationSettings ValidateOnLeave ="true" ErrorDisplayMode ="Text" ErrorTextPosition ="Bottom" ValidationGroup ="btnSigNuevoG">
                                                    <RequiredField IsRequired="true" ErrorText="El campo es requerido" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td style="width: 10%">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="text-align: center;">
                                            <br />
                                            <dx:ASPxButton runat="server" Text="Aceptar" Theme="Office2010Black" ID="btnSigNueva" OnClick="btnSigNueva_Click" ValidationGroup ="btnSigNuevoG"></dx:ASPxButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<dx:ASPxButton ID="btnCancelarNueva" OnClick="btnCancelarNueva_Click" runat="server" Text="Cancelar" Theme="Office2010Black" ></dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                                <%--fin contenido tab 01--%>
                            </fieldset>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
            <%--fin tab 01--%>

            <%--inicia tab 02--%>
            <TabPages>
                <dx:TabPage Text="2. Selección remisiones" Enabled="false">
                    <ContentCollection>
                        <dx:ContentControl>
                            <fieldset class="Personal1">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"><%-- Texto --%></span>
                                </legend>
                                <%--inicia contenido tab 02--%>
                                <div style="margin: 0 auto; text-align: center;">
                                    <table style="background-color: #B7B7B7; border-radius: 10px; margin: 0 auto;">
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblSeleccionTipoContratoD" runat="server" Text="Tipo de contrato:"></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 19%">
                                                <dx:ASPxLabel ID="lblSeleccionTipoContrato" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 4%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblSeleccionTipoCambioD" runat="server" Text="Tipo de cambio:"></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 19%">
                                                <dx:ASPxLabel ID="lblSeleccionTipoCambio" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp;</td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblSeleccionClienteD" runat="server" Text="Cliente:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblSeleccionCliente" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 4%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblSeleccionContratoD" runat="server" Text="Contrato:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblSeleccionContrato" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblSeleccionMonedaVueloD" runat="server" Text="Moneda vuelo:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblSeleccionMonedaVuelo" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 4%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblSeleccionFacturanteVD" runat="server" Text="Facturante vuelo:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblSeleccionFacturanteV" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblSeleccionMSCD" runat="server" Text="Moneda servicios con cargo:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblSeleccionMSC" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 4%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblSeleccionFacturanteSCD" runat="server" Text="Facturante servicios con cargo:"></dx:ASPxLabel>

                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblSeleccionFacturanteSC" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <dx:ASPxGridView ID="gvRemisionesDeVuelo" 
                                            runat="server" 
                                            Theme="Office2010Black" AutoGenerateColumns="false" KeyFieldName="IdRemision" Width="100%">
                                            <SettingsPager Visible="True"  ></SettingsPager>
                                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick ="true" 
                                                AllowSelectSingleRowOnly ="false"  
                                                AllowDragDrop="false" ConfirmDelete="True"></SettingsBehavior>
                                            <Columns>
                                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" VisibleIndex="0" Caption="Seleccione"></dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn Caption="No. Remisión" FieldName="IdRemision" VisibleIndex="1"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataDateColumn Caption="Fecha Remisión" VisibleIndex="2" FieldName="Fecha"></dx:GridViewDataDateColumn>
                                                <dx:GridViewDataDateColumn Caption="Fecha Salida Vuelo" VisibleIndex="3" FieldName="FechaSalidaVuelo"></dx:GridViewDataDateColumn>
                                                <dx:GridViewDataTextColumn Caption="Matrícula" VisibleIndex="4" FieldName="Matricula"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Tipo de Iva" VisibleIndex="5" FieldName="IVA">
                                                    <PropertiesTextEdit ></PropertiesTextEdit>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Ruta" VisibleIndex="6" FieldName="Ruta"></dx:GridViewDataTextColumn>
                                            </Columns>
                                            
                                            <SettingsPager Position="TopAndBottom" >
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100">                                                    
                                                </PageSizeItemSettings>                                                
                                            </SettingsPager>
                                        </dx:ASPxGridView>
                                    </div>
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: center;">
                                                <br />
                                                <dx:ASPxButton runat="server" ID="btnSigRem" OnClick="btnSigRem_Click1" Text="Aceptar" Theme="Office2010Black"></dx:ASPxButton>
                                                &nbsp;&nbsp;&nbsp;&nbsp;<dx:ASPxButton ID ="btnCancelarRem" OnClick ="btnCancelarRem_Click" runat="server" Text="Cancelar" Theme="Office2010Black"></dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <%--fin contenido tab 02--%>
                            </fieldset>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
            <%--fin tab 02--%>

            <%--inicia tab 03--%>
            <TabPages>
                <dx:TabPage Text="3. Consulta remisiones" Enabled="false">
                    <ContentCollection>
                        <dx:ContentControl>
                            <fieldset class="Personal1">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"><%-- Texto --%></span>
                                </legend>
                                <%--inicia contenido tab 03--%>
                                <div style="margin: 0 auto; text-align: center;">
                                    <table style="background-color: #B7B7B7; border-radius: 10px; margin: 0 auto;">
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblConsultaTipoContratoD" runat="server" Text="Tipo de contrato:"></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 19%">
                                                <dx:ASPxLabel ID="lblConsultaTipoContrato" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 4%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblConsultaTipoCambioD" runat="server" Text="Tipo de cambio:"></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 19%">
                                                <dx:ASPxLabel ID="lblConsultaTipoCambio" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp;</td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblConsultaClienteD" runat="server" Text="Cliente:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblConsultaCliente" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 4%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblConsultaContratoD" runat="server" Text="Contrato:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblConsultaContrato" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblConsultaMonedaVueloD" runat="server" Text="Moneda vuelo:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblConsultaMonedaVuelo" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 4%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblConsultaFacturanteVD" runat="server" Text="Facturante vuelo:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblConsultaFacturanteV" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblConsultaMSCD" runat="server" Text="Moneda servicios con cargo:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblConsultaMSC" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 4%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID="lblConsultaFacturanteSCD" runat="server" Text="Facturante servicios con cargo:"></dx:ASPxLabel>

                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblConsultaFacturanteSC" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <dx:ASPxGridView ID="gvDetallesRemision" runat="server" Theme="Office2010Black"
                                                    Styles-Header-HorizontalAlign="Center"
                                                    Styles-Header-Wrap ="True"
                                                 AutoGenerateColumns="false" Width="100%">
                                                <SettingsPager Visible="False"></SettingsPager>
                                                <SettingsBehavior AllowGroup="False"  
                                                    AllowDragDrop="false" 
                                                    ConfirmDelete="True"
                                                    ></SettingsBehavior>
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Caption="No. Consecutivo Remisión" VisibleIndex="0" FieldName ="Id"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="No. Remisión" VisibleIndex="0" FieldName="IdRemision"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Ruta" VisibleIndex="1" FieldName="Ruta"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataDateColumn Caption="Fecha Salida" VisibleIndex="2" FieldName="FechaSalidaVuelo"></dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataDateColumn Caption="Fecha Llegada" VisibleIndex="3" FieldName="FechaLlegadaVuelo"></dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataTextColumn Caption="Matrícula" VisibleIndex="4" FieldName="Matricula"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Subtotal Vuelo" VisibleIndex="5" FieldName="SubVuelo" PropertiesTextEdit-DisplayFormatString="{0:c4}"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Subtotal SCC" VisibleIndex="6" FieldName="SubCargo" PropertiesTextEdit-DisplayFormatString="{0:c4}"></dx:GridViewDataTextColumn>
                                                </Columns>
                                            </dx:ASPxGridView>
                                        </div>
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <br />
                                                    <dx:ASPxButton ID="btnSigRemDet" OnClick="btnSigRemDet_Click" runat="server" Text="Aceptar" Theme="Office2010Black"></dx:ASPxButton>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<dx:ASPxButton ID="btnCancelarRemDet" OnClick ="btnCancelarRemDet_Click" runat="server" Text="Cancelar" Theme="Office2010Black"></dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <%--fin contenido tab 03--%>
                            </fieldset>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
            <%--fin tab 03--%>

            <%--inicia tab 04--%>
            <TabPages>
                <dx:TabPage Text="4. Prefactura" Enabled="false"    >
                    <ContentCollection>
                        <dx:ContentControl>
                            <fieldset class="Personal1">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"><%-- Texto --%></span>
                                </legend>
                                <%--inicia contenido tab 04--%>
                                <div style="margin: 0 auto; text-align: center;">
                                    <table style="background-color: #B7B7B7; border-radius: 10px; margin: 0 auto;">
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID ="lblPrefacturaTipoContratoD" runat="server" text ="Tipo de contrato:"></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 19%"><dx:ASPxLabel ID ="lblPrefacturaTipoContrato" runat="server" Text=""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 4%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID ="lblPrefacturaTipoCambioD" runat="server" text ="Tipo de cambio:"></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 19%">
                                                <dx:ASPxLabel ID ="lblPrefacturaTipoCambio" runat="server" text =""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp;</td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID ="lblPrefacturaClienteD" runat="server" text ="Cliente:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID ="lblPrefacturaCliente" runat="server" text =""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 4%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID ="lblPrefacturaContratoD" runat="server" text ="Contrato:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID ="lblPrefacturaContrato" runat="server" text =""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID ="lblPrefacturaMonedaVueloD" runat="server" text ="Moneda vuelo:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID ="lblPrefacturaMonedaVuelo" runat="server" text =""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 4%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID ="lblPrefacturaFacturanteVD" runat="server" text ="Facturante vuelo:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID ="lblPrefacturaFacturanteV" runat="server" text =""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID ="lblPrefacturaMSCD" runat="server" text ="Moneda servicios con cargo:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID ="lblPrefacturaMSC" runat="server" text =""></dx:ASPxLabel>                                            
                                            </td>
                                            <td style="width: 4%">&nbsp;
                                            </td>
                                            <td style="width: 19%" align="left">
                                                <dx:ASPxLabel ID ="lblPrefacturaFacturanteSCD" runat="server" text ="Facturante servicios con cargo:"></dx:ASPxLabel>
                                            
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID ="lblPrefacturaFacturanteSC" runat="server" text =""></dx:ASPxLabel>
                                            </td>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 20%">&nbsp;
                                        </td>
                                        <td style="width: 20%" align="center">
                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text=""></dx:ASPxLabel>
                                            <dx:ASPxRadioButton runat="server" ID="rdlServicioVuelo" Text="Vuelo" Checked="False" GroupName="VueloServCargo" Theme="Office2010Black"></dx:ASPxRadioButton>
                                        </td>
                                        <td style="width: 20%" align="center">
                                            <dx:ASPxRadioButton runat="server" ID="rdlServicioCargo" Text="Servicio con cargo" Checked="False" GroupName="VueloServCargo" Theme="Office2010Black"></dx:ASPxRadioButton>
                                        </td>
                                        <td style="width: 20%" align="center">
                                            <dx:ASPxRadioButton runat="server" ID="rdlServiciosAmbos" Text="Ambos" Checked="true" GroupName="VueloServCargo" Theme="Office2010Black"></dx:ASPxRadioButton>
                                        </td>
                                        <td style="width: 20%">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <div class="row">
                                    <br />
                                </div>

                                <div class="row">
                                    <div class ="col-md-2 "></div>
                                    <div class ="col-md-8 ">
                                        <dx:ASPxRadioButtonList runat="server"  ID ="rdlListCantidadFacturas" Caption ="Cantidad de facturas" RepeatDirection ="Horizontal" Visible ="false">
                                            <Items>
                                                <dx:ListEditItem Text ="Dos" Value ="0" Selected ="true" />
                                                <dx:ListEditItem Text ="Una" Value ="1"/>
                                            </Items>
                                        </dx:ASPxRadioButtonList>
                                    </div>
                                    <div class ="col-md-2 "></div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <dx:ASPxGridView ID="gvServiciovuelos" runat="server" Theme="Office2010Black" AutoGenerateColumns="false" Width="100%">
                                            <SettingsPager Visible="false" Mode ="ShowAllRecords"></SettingsPager>
                                            <SettingsBehavior AllowGroup="False" AllowDragDrop="false" ConfirmDelete="True"></SettingsBehavior>
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="Servicios de vuelo" VisibleIndex="0" FieldName="Descripcion"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Cantidad" VisibleIndex="1" FieldName="Cantidad"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Importe en Dlls" VisibleIndex="2" FieldName="ImporteDlls" PropertiesTextEdit-DisplayFormatString="{0:c2}"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Importe Mxm" VisibleIndex="3" FieldName="ImporteMXN" PropertiesTextEdit-DisplayFormatString="{0:c2}"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Horas a descontar" VisibleIndex="4" FieldName="HrDescontar"></dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings ShowFooter="True" />
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem SummaryType="Sum" ShowInColumn="Cantidad" Tag="SubTotal" DisplayFormat="Subtotal: {0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" ShowInColumn="Cantidad" Tag="Descuento" DisplayFormat="Descuento: {0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" ShowInColumn="Cantidad" Tag="IVA" DisplayFormat="IVA (Nal o Int) %: {0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" ShowInColumn="Cantidad" Tag="TOTAL" DisplayFormat="TOTAL: {0:c2}"></dx:ASPxSummaryItem>

                                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="ImporteDlls" ShowInColumn="Importe en Dlls" Tag="SubTotal" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="ImpDescuentoDlls" ShowInColumn="Importe en Dlls" Tag="Descuento" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="IVADLL" ShowInColumn="Importe en Dlls" Tag="IVA" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="TotalDLL" ShowInColumn="Importe en Dlls" Tag="TOTAL" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>

                                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="ImporteMXN" ShowInColumn="Importe Mxm" Tag="SubTotal" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="ImpDescuentoMXN" ShowInColumn="Importe Mxm" Tag="Descuento" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="IVAMXN" ShowInColumn="Importe Mxm" Tag="IVA" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="TotalMXN" ShowInColumn="Importe Mxm" Tag="TOTAL" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>

                                                <dx:ASPxSummaryItem SummaryType="Max" FieldName="TotHoras" ShowInColumn="Horas a descontar" Tag="TOTAL" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>


                                            </TotalSummary>
                                        </dx:ASPxGridView>
                                    </div>
                                </div>
                                <div class="row">
                                    <br />
                                    <div class="col-md-12">
                                        <dx:ASPxGridView ID="ASPxGridView3" runat="server" Theme="Office2010Black" AutoGenerateColumns="false" KeyFieldName="IdServicioConCargo" Width="60%">
                                            <SettingsPager Visible="false" Mode ="ShowAllRecords"></SettingsPager>
                                            <SettingsBehavior AllowGroup="False" AllowDragDrop="false" ConfirmDelete="True"></SettingsBehavior>
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="Servicios con cargo" VisibleIndex="0" FieldName="ServicioConCargoDescripcion"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Importe en Dlls" VisibleIndex="1" FieldName="SubtotalUSD" PropertiesTextEdit-DisplayFormatString="{0:c2}"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Importe Mxm" VisibleIndex="2" FieldName="SubtotalMXN" PropertiesTextEdit-DisplayFormatString="{0:c2}"></dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings ShowFooter="True" />
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem SummaryType="Sum" ShowInColumn="Servicios con cargo" Tag="SubTotal" DisplayFormat="Subtotal: {0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" ShowInColumn="Servicios con cargo" Tag="IVA" DisplayFormat="IVA (Nal o Int) %: {0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" ShowInColumn="Servicios con cargo" Tag="TOTAL" DisplayFormat="TOTAL: {0:c2}"></dx:ASPxSummaryItem>

                                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="SubtotalUSD" ShowInColumn="Importe en Dlls" Tag="SubTotal" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="IVAUSD" ShowInColumn="Importe en Dlls" Tag="IVA" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="TotalUSD" ShowInColumn="Importe en Dlls" Tag="TOTAL" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>

                                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="SubtotalMXN" ShowInColumn="Importe Mxm" Tag="SubTotal" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="IVAMXN" ShowInColumn="Importe Mxm" Tag="IVA" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>
                                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="TotalMXN" ShowInColumn="Importe Mxm" Tag="TOTAL" DisplayFormat="{0:c2}"></dx:ASPxSummaryItem>

                                            </TotalSummary>
                                        </dx:ASPxGridView>
                                    </div>
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: center;">
                                                <br />
                                                <dx:ASPxButton runat="server" ID ="btnAceptar" Text="Guardar" OnClick ="btnAceptar_Click" Theme="Office2010Black"></dx:ASPxButton>
                                                &nbsp;&nbsp;&nbsp;&nbsp;<dx:ASPxButton runat="server" ID="btnEnviaPrefactura" OnClick ="btnEnviaPrefactura_Click" Text="Enviar Factura" Theme="Office2010Black"></dx:ASPxButton>
                                                &nbsp;&nbsp;&nbsp;&nbsp;<dx:ASPxButton runat="server" ID="btnCancelarPref" OnClick ="btnCancelarPref_Click" Text="Cancelar" Theme="Office2010Black"></dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <%--fin contenido tab 04--%>
                            </fieldset>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
            <%--fin tab 04--%>
        </dx:ASPxPageControl>


        <%--MODAL PARA CONFIRMAR SI DESEA UTILIZAR COTIZACIÓN NO ASOCIADA --%>
        <dx:ASPxPopupControl ID="ppConfirmacion" runat="server" ClientInstanceName="ppConfirmacion" CloseAction="CloseButton" Width="500px" CloseOnEscape="true" Modal="true" Theme="Office2010Black"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Confirmación">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxPanel ID="ASPxPanel4" runat="server" DefaultButton="btOK">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <br />                                    
                                        <table>
                                            <tr>
                                                <td style="width:2%"></td>
                                                <td style="width:20%">
                                                    <dx:ASPxImage ID="ASPxImage2" runat="server" ShowLoadingImage="true" ImageUrl="~/img/iconos/Information2.ico"></dx:ASPxImage>
                                                </td>
                                                <td style="width:76%">
                                                    <dx:ASPxLabel CssClass="FExport" ID="lblConfirmacion" runat="server" Text=""></dx:ASPxLabel>
                                                </td>
                                                <td style="width:2%"></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                        </table>                                    
                                        <br />
                                    </div>
                                </div>
                                <div>
                                
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" style="text-align:right">
                                        <br />
                                    
                                        <dx:ASPxButton  ID="btnConfirmar" runat="server" Text="Aceptar" ClientInstanceName="btnConfirmar" OnClick="btnConfirmar_Click" Theme ="Office2010Black">
                                            <ClientSideEvents Click="function(s, e) { ppConfirmacion.Hide(); }" />
                                        </dx:ASPxButton>

                                        <dx:ASPxButton ID="btnSalirConfirm" runat="server" Text="Cancelar" ClientInstanceName="btnSalirConfirm" AutoPostBack="false" Theme="Office2010Black">
                                            <ClientSideEvents Click="function() { ppConfirmacion.Hide(); }" />
                                        </dx:ASPxButton>
                                    
                                        <br />

                                    </div>
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <%--VALIDACION DE CAMPOS FACTURA 3.3--%>
        <dx:ASPxPopupControl ID="ppConfirmacionFact" runat="server" ClientInstanceName="ppConfirmacionFact" CloseAction="CloseButton" Width="500px" CloseOnEscape="true" Modal="true" Theme="Office2010Black"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Confirmación">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxPanel ID="pnlFacturacion" runat="server" DefaultButton="btOK">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <br />                                    
                                        <table>
                                            <tr>
                                                <td style="width:2%"></td>
                                                <td style="width:20%">
                                                    <dx:ASPxImage ID="ASPxImage1" runat="server" ShowLoadingImage="true" ImageUrl="~/img/iconos/Information2.ico"></dx:ASPxImage>
                                                </td>
                                                <td style="width:76%">
                                                    <dx:ASPxLabel CssClass="FExport" ID="lblConfirmacionFactura" runat="server" Text=""></dx:ASPxLabel>
                                                </td>
                                                <td style="width:2%"></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                        </table>                                    
                                        <br />
                                    </div>
                                </div>
                                <div>
                                
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" style="text-align:right">
                                        <br />
                                    
                                        <dx:ASPxButton  ID="btnIrContrato" runat="server" Text="Ir al contrato..." ClientInstanceName="btnConfirmar" OnClick="btnIrContrato_Click" Theme ="Office2010Black">
                                            <ClientSideEvents Click="function(s, e) { ppConfirmacionFact.Hide(); }" />
                                        </dx:ASPxButton>

                                        <dx:ASPxButton ID="btnCancelarConfirmFact" runat="server" Text="Cancelar" ClientInstanceName="btnCancelarConfirmFact" AutoPostBack="false" Theme="Office2010Black">
                                            <ClientSideEvents Click="function() { ppConfirmacionFact.Hide(); }" />
                                        </dx:ASPxButton>
                                    
                                        <br />

                                    </div>
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </asp:Content>
