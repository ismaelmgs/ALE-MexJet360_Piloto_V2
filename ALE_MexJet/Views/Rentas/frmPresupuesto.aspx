<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmPresupuesto.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ALE_MexJet.Views.Rentas.frmPresupuesto" %>

<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Styles/Controls.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function OcultaError() {
            ppMensaje.Hide();
        }

        function ValidaCampos() {

            debugger;
            var fecSalida = txtFechaSalidaFV.GetDate();
            var fecllegada = txtFechaLlegadaFV.GetDate();

            if (fecSalida == null || fecSalida == null)
            {
                txtFechaSalidaFV.isValid = false;
                txtFechaLlegadaFV.isValid = false;
                return;
            }
            if (fecSalida >= fecllegada)
            {
                txtFechaLlegadaFV.errorText = "error de fecha";
                txtFechaSalidaFV.isValid = false;
                txtFechaLlegadaFV.isValid = false;
                return;
            }

        }

        function AbreSegundoPresupuesto()
        {
            collapseOne.className = "panel-collapse collapse";
            collapseTwo.className = "panel-collapse collapse in";
        }

    </script>
    <style type="text/css">
        .bordesX td {
            border:solid;
        }

    </style>

    <asp:HiddenField ID="hdIntercambio" runat="server" Value="1.66" />
    <asp:HiddenField ID="hdFechaPico" runat="server" Value="0" />
    <asp:HiddenField ID="hdGiraEspera" runat="server" Value="0" />
    <asp:HiddenField ID="hdGiraHorario" runat="server" Value="0" />
    <asp:HiddenField ID="hdFactorTramoNal" runat="server" Value="0" />
    <asp:HiddenField ID="hdFactorTramoInt" runat="server" Value="0" />
    <asp:HiddenField ID="hdAplicaFerryInt" runat="server" Value="0" />

    <asp:HiddenField ID="hdOpcPresupuesto" runat="server" Value="0" />

    <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
    <dx:ASPxPanel ID="pnlPrincipal" Theme="Office2010Black" runat="server" Width="100%" BackColor="White">
        <PanelCollection>
            <dx:PanelContent runat="server">
                <asp:UpdatePanel ID="upaPrincipal" runat="server" OnUnload="upaConceptos_Unload">
                    <ContentTemplate>
                        <div class="row header1">
                            <div class="col-md-12">
                                <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Cotización de Vuelos</span>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset class="Personal1">
                                    <legend>
                                        <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Datos Generales</span>
                                    </legend>
                                    <div id="myDiv" class="col-sm-12">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: left;">
                                                    
                                                </td>
                                                <td style="text-align: left;">
                                                    
                                                </td>
                                                <td style="width: 10px;"></td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel6" Text="Folio Cotización:" Theme="Office2010Black"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblFolio" Text="" Theme="Office2010Black"></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblDgDescFechaPresupuesto" Text="Fecha de Presupuesto:" Theme="Office2010Black"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxDateEdit runat="server" ID="dteDgFechaPresupuesto" AllowNull="false" Theme="Office2010Black">
                                                        <DropDownButton>
                                                            <Image IconID="scheduling_calendar_16x16"></Image>
                                                        </DropDownButton>
                                                    </dx:ASPxDateEdit>
                                                </td>
                                                <td style="width: 10px;"></td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblDgDiasVigencia" Text="Días de Vigencia:" Theme="Office2010Black"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxTextBox runat="server" ID="txtDgDiasVigencia" Theme="Office2010Black"></dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblDgCliente" Theme="Office2010Black" Text="Cliente:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxComboBox runat="server" ID="cboGgClientes" AutoPostBack="true" OnSelectedIndexChanged="cboGgClientes_SelectedIndexChanged" Theme="Office2010Black">
                                                        <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="VGTramo">
                                                            <RequiredField ErrorText="El campo es requerido" IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td style="width: 10px;"></td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblDgContrato" Text="Contrato:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxComboBox runat="server" Theme="Office2010Black" ID="cboDgContrato" AutoPostBack="true" OnSelectedIndexChanged="cboDgContrato_SelectedIndexChanged">
                                                        <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="VGTramo">
                                                            <RequiredField ErrorText="El campo es requerido" IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblDgDescTipoPaquete" Text="Tipo de paquete:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblDgTipoPaquete"></dx:ASPxLabel>
                                                </td>
                                                <td style="width: 10px;"></td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblDgDescModeloContratado" Text="Modelo Contratado:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblDgGrupoModelo"></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblDgDescCompañiaReistrada" Text="Compañía Registrada:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;" colspan="4">
                                                    <dx:ASPxLabel runat="server" ID="lbldgCompañiaregisrada" Theme="Office2010Black"></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblDgDescCompañiaImpresion" Text="Compañía para Impresión:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;" colspan="4">
                                                    <dx:ASPxTextBox runat="server" ID="txtDgCompaniaImpresion" Theme="Office2010Black" Width="85%"></dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblDgDescSolicitante" Text="Solicitante:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxComboBox runat="server" ID="cboDgSolicitante" Theme="Office2010Black" AutoPostBack="true" OnSelectedIndexChanged="cboDgSolicitante_SelectedIndexChanged">
                                                        <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="VGSolicitud"
                                                            SetFocusOnError="true">
                                                            <RequiredField ErrorText="El campo es requerido." IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td style="width: 10px;"></td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblDescNombreSolicitante" Text="Nombre Solicitante:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxTextBox runat="server" Theme="Office2010Black" ID="txtDgNombreSolicitante"></dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblDgDecTelefono" Text="Teléfono:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxTextBox runat="server" Theme="Office2010Black" ID="txtDgTelefono"></dx:ASPxTextBox>
                                                </td>
                                                <td style="width: 10px;"></td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblDgDescCorreo" Text="eMail:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxTextBox runat="server" Theme="Office2010Black" ID="txtDgCorreo">
                                                        <%--<ValidationSettings  ErrorText ="El campo es requerido" ErrorDisplayMode ="Text" ErrorTextPosition ="Bottom">
                                                            <RequiredField ErrorText ="El campo es requerido" IsRequired="true" />
                                                        </ValidationSettings>--%>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblDgDescModeloSolicitado" Text="Modelo Solicitado:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxComboBox runat="server" Theme="Office2010Black" ID="cboDgModeloSolicitado" AutoPostBack="true" OnValueChanged="cboDgModeloSolicitado_ValueChanged">
                                                        <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="VGTramo">
                                                            <RequiredField ErrorText="El campo es requerido" IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td style="width: 10px;"></td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblDgDescMonedaPresupuesto" Text="Moneda del Presupuesto:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxComboBox runat="server" Theme="Office2010Black" ID="cboDgMonedaPresupueto" AutoPostBack="true" OnValueChanged="cboDgMonedaPresupueto_ValueChanged">
                                                        <Items>
                                                            <dx:ListEditItem Value="1" Text="USD" Selected="true" />
                                                            <dx:ListEditItem Value="2" Text="MXN" />
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset class="Personal1">
                                    <legend>
                                        <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Tarifas en</span>
                                    </legend>
                                    <div id="myDiv" class="col-sm-12">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblTarDescVacia"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblTarDescNal" Text="Nacional"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblTarDescInt" Text="Internacional"></dx:ASPxLabel>
                                                </td>

                                                <td style="width: 10px;"></td>

                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="ASPxLabel1" Text=""></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="ASPxLabel2" Text=""></dx:ASPxLabel>
                                                </td>

                                                <td style="width: 10px;"></td>

                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="ASPxLabel3" Text=""></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="ASPxLabel4" Text=""></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="9">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblTarDescVuelo" Text="Vuelo:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxTextBox runat="server" Theme="Office2010Black" ID="txtTarVueloNal" OnTextChanged="txtTarVueloNal_TextChanged" AutoPostBack="true"></dx:ASPxTextBox>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxTextBox runat="server" Theme="Office2010Black" ID="txtTarVueloInt" OnTextChanged="txtTarVueloNal_TextChanged" AutoPostBack="true"></dx:ASPxTextBox>
                                                </td>

                                                <td style="width: 10px;"></td>

                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblTarDescSiglas" Text="Siglas de Aeropuerto a usar:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxRadioButton runat="server" ID="rdlTarIAta" Text="IATA" Checked="true" GroupName="SiglasAeropuertos"></dx:ASPxRadioButton>
                                                </td>

                                                <td style="width: 10px;"></td>

                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblTarDescFactores" Text="Factores Aplicados:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left; vertical-align:top" rowspan="5">

                                                    <asp:CheckBoxList ID="chkList" runat="server" 
                                                        CellPadding="5" TextAlign="Right"
                                                        CellSpacing="5" RepeatColumns="2" CssClass="FormatRadioButtonList"
                                                        RepeatDirection="Vertical" RepeatLayout="Table">
                                                        <asp:ListItem Text="Intercambio" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Gira Espera" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Gira Horario" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Fecha Pico" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="Factor Vuelo Nacional" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="Factor Vuelo Internacional" Value="6"></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                    <%--<div style="display:none">
                                                        <dx:ASPxCheckBox ID="chkIntercambio" runat="server" Text="Intercambio" Enabled="false" Theme="Office2010Black"></dx:ASPxCheckBox>
                                                        <br />
                                                        <dx:ASPxCheckBox ID="chkGiraEspera" runat="server" Text="Gira Espera" Enabled="false" Theme="Office2010Black"></dx:ASPxCheckBox>
                                                        <br />
                                                        <dx:ASPxCheckBox ID="chkGiraHorario" runat="server" Text="Gira Horario" Enabled="false" Theme="Office2010Black"></dx:ASPxCheckBox>
                                                        <br />
                                                        <dx:ASPxCheckBox ID="chkFechaPico" runat="server" Text="Fecha Pico" Enabled="false" Theme="Office2010Black"></dx:ASPxCheckBox>
                                                    </div>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblTarDescEspera" Text="Espera:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxTextBox runat="server" Theme="Office2010Black" ID="txtEsperaNal" OnTextChanged="txtTarVueloNal_TextChanged" AutoPostBack="true"></dx:ASPxTextBox>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxTextBox runat="server" Theme="Office2010Black" ID="txtEsperaInt" OnTextChanged="txtTarVueloNal_TextChanged" AutoPostBack="true"></dx:ASPxTextBox>
                                                </td>

                                                <td style="width: 10px;"></td>

                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text=""></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxRadioButton runat="server" ID="rdlTarIcao" Text="ICAO" GroupName="SiglasAeropuertos"></dx:ASPxRadioButton>
                                                </td>

                                                <td style="width: 10px;"></td>

                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text=""></dx:ASPxLabel>
                                                </td>
                                                <%--<td style="text-align: left;">
                                            <dx:ASPxTextBox runat="server" Theme="Office2010Black" ID="txtTarEsperaFactor"></dx:ASPxTextBox>
                                        </td>--%>
                                            </tr>
                                            <tr>
                                                <td colspan="8">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" ID="lblTarDescPernocta" Text="Pernocta"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxTextBox runat="server" Theme="Office2010Black" ID="txtTarPernoctaNal" OnTextChanged="txtTarVueloNal_TextChanged" AutoPostBack="true"></dx:ASPxTextBox>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxTextBox runat="server" Theme="Office2010Black" ID="txtTarPernoctaInt" OnTextChanged="txtTarVueloNal_TextChanged" AutoPostBack="true"></dx:ASPxTextBox>
                                                </td>

                                                <td style="width: 10px;"></td>

                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text=""></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text=""></dx:ASPxLabel>
                                                </td>

                                                <td style="width: 10px;"></td>

                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" Theme="Office2010Black" Text=""></dx:ASPxLabel>
                                                </td>
                                                <%--<td style="text-align: left;">
                                            <dx:ASPxTextBox runat="server" Theme="Office2010Black" ID="txtTarPernoctaFactor"></dx:ASPxTextBox>
                                        </td>--%>
                                            </tr>
                                        </table>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset class="Personal1">
                                    <legend>
                                        <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Ruta de Vuelo</span>
                                    </legend>
                                    <div class="col-sm-6">
                                    </div>
                                    <div class="col-sm-6">
                                       <dx:ASPxLabel ID="lblNotaTramos" runat="server" Text="NOTA: En las cotizaciones de vuelo la ruta debe salir y regresar a base."></dx:ASPxLabel>
                                    </div>
                                    <div id="myDiv" class="col-sm-12">
                                        <dx:ASPxButton ID="btnAgregaPierna" runat="server" Text="Agregar Tramo" Theme="Office2010Black" ValidationGroup="VGTramo" OnClick="btnAgregaPierna_Click">
                                        </dx:ASPxButton>
                                        <%--<dx:ASPxButton ID="btnAgregaPierna" runat="server" Text="Agregar Tramo" Theme="Office2010Black" OnClick="btnAgregaPierna_Click"></dx:ASPxButton>--%>
                                        <br />
                                        <div style="height: 10px"></div>
                                        <asp:GridView ID="gvTramosOpc1" runat="server" AutoGenerateColumns="false" ShowFooter="true" CssClass="table"
                                            Style="border-top: 1px solid #484848; border-left: 1px solid #484848; border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                            OnRowDataBound="gvTramosOpc1_RowDataBound">
                                            <HeaderStyle CssClass="celda2" />
                                            <RowStyle CssClass="celda6" Height="16px" />
                                            <FooterStyle CssClass="celda3" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Origen" DataField="Origen" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Destino" DataField="Destino" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Pax">
                                                    <ItemTemplate>
                                                        <dx:ASPxTextBox ID="txtCantPax" runat="server" Theme="Office2010Black" Width="70" AutoPostBack="true"
                                                            OnTextChanged="txtCantPax_TextChanged"></dx:ASPxTextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Pax" DataField="CantPax" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>--%>
                                                <asp:TemplateField HeaderText="Fecha Salida" ControlStyle-Width="10px" ItemStyle-Width="10px">
                                                    <ItemTemplate>
                                                        <dx:ASPxDateEdit ID="txtFechaSalida" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom"
                                                            OnDateChanged="txtFechaSalida_DateChanged" Theme="Office2010Black" AutoPostBack="true"
                                                            TimeSectionProperties-Visible="true" EditFormatString="dd/MM/yyyy HH:mm">
                                                            <DropDownButton>
                                                                <Image IconID="scheduling_calendar_16x16"></Image>
                                                            </DropDownButton>
                                                        </dx:ASPxDateEdit>
                                                    </ItemTemplate>

                                                    <ControlStyle Width="10px"></ControlStyle>

                                                    <ItemStyle Width="10px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Llegada" HeaderStyle-Width="10px" ItemStyle-Width="10px" FooterStyle-Width="10px">
                                                    <ItemTemplate>
                                                        <dx:ASPxDateEdit ID="txtFechaLlegada" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom"
                                                            TimeSectionProperties-Visible="true" EditFormatString="dd/MM/yyyy HH:mm" Theme="Office2010Black" AutoPostBack="true"
                                                            OnDateChanged="txtFechaLlegada_DateChanged">
                                                            <DropDownButton>
                                                                <Image IconID="scheduling_calendar_16x16"></Image>
                                                            </DropDownButton>
                                                        </dx:ASPxDateEdit>
                                                    </ItemTemplate>

                                                    <FooterStyle Width="10px"></FooterStyle>

                                                    <HeaderStyle Width="10px"></HeaderStyle>

                                                    <ItemStyle Width="10px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Tiempo Vuelo Real" DataField="TiempoVuelo"></asp:BoundField>
                                                <asp:BoundField HeaderText="Tiempo Espera" DataField="TiempoEspera"></asp:BoundField>
                                                <asp:BoundField HeaderText="Tiempo Cobrar" DataField="TiempoCobrar"></asp:BoundField>
                                                <%--<asp:TemplateField>
                                                    <ItemTemplate>
                                                        <dx:ASPxTextBox ID="txtTiempoCobrar" runat="server" Theme="Office2010Black"
                                                            OnTextChanged="txtTiempoCobrar_TextChanged" MaskSettings-Mask="__:__"></dx:ASPxTextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imbDelete" runat="server" ImageUrl="~/img/iconos/delete.png" Width="24" Height="24" ToolTip="Elimina el tramo."
                                                            OnClick="imbDelete_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-6">
                                        <div style="text-align:left">
                                            <dx:ASPxButton ID="btnSelOpc1" runat="server" Text="Seleccionar" Theme ="Office2010Black" OnClick="btnSelOpc1_Click"></dx:ASPxButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div style="text-align:right;">
                                            <dx:ASPxLabel ID="lblMensajeDosPresupuestos" runat="server" Text="" ForeColor="Red"></dx:ASPxLabel>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>

                        <br />

                        <div class="row">
                            <div class="col-md-12">
                                <div class="container" style="width:100%">
                                    <div class="row">
                                        <div class="col-lg-12 col-lg-12">
                                            <div class="panel-group" id="accordion">
                                                
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <table style="width:100%">
                                                                <tr>
                                                                    <td style="width:50%; text-align:left">
                                                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne"><span class="glyphicon glyphicon-folder-close">
                                                                        </span>Cálculo con Pernoctas</a>
                                                                    </td>

                                                                    <td style="width:50%; text-align:right">
                                                                        <asp:Image ID="imbPernoctas" runat="server" ImageUrl="" Height="28px" Width="24px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </h4>
                                                    </div>
                                                    <table>
                                                        <tr>
                                                            <td style="height:5px"></td>
                                                        </tr>
                                                    </table>
                                                    <div id="collapseOne" class="panel-collapse collapse in">
                                                        <div class="panel-body">

                                                            <%--SERVICIOS CON CARGO--%>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <fieldset class="Personal1">
                                                                        <legend>
                                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Servicios con Cargo en</span>
                                                                        </legend>
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                        <div id="myDiv" class="col-sm-8">
                                                                            <asp:UpdatePanel ID="upaServicios" runat="server" OnUnload="upaServicios_Unload">
                                                                                <ContentTemplate>
                                                                                    <asp:GridView ID="gvServiciosC" runat="server" AutoGenerateColumns="false" CssClass="table" Width="100%" DataKeyNames="IdServicioConCargo"
                                                                                        Style="border-top: 1px solid #484848; border-left: 1px solid #484848; border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                                                        OnRowDataBound="gvServiciosC_RowDataBound" OnRowCommand="gvServiciosC_RowCommand">
                                                                                        <HeaderStyle CssClass="celda2" />
                                                                                        <RowStyle CssClass="celda6" />
                                                                                        <FooterStyle CssClass="celda3" />
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="ServicioConCargoDescripcion" HeaderText="Servicio con Cargo" ItemStyle-Width="60%">
                                                                                                <ItemStyle Width="60%"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                                                                                <HeaderTemplate>
                                                                                                    <dx:ASPxLabel ID="lblHead" runat="server" Text="(MXN)" Theme="Office2010Black"></dx:ASPxLabel>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <dx:ASPxTextBox ID="txtImporte" runat="server" Theme="Office2010Black" Width="150px"
                                                                                                        OnTextChanged="txtImporte_TextChanged" AutoPostBack="true">
                                                                                                    </dx:ASPxTextBox>
                                                                                                </ItemTemplate>

                                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Acciones">
                                                                                                <ItemTemplate>
                                                                                                    <dx:ASPxButton ID="btnEliminarSC" runat="server" Text="Eliminar" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Theme="Office2010Black"></dx:ASPxButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                    </fieldset>
                                                                </div>
                                                            </div>
                                                            <%--TOTALES--%>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <fieldset class="Personal1">
                                                                        <legend>
                                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Totales en</span>
                                                                        </legend>
                                                                        <div id="myDiv" class="col-sm-12">
                                                                            <div class="col-sm-12">
                                                                                <asp:Label ID="lblTipoCambio" runat="server" Text="" Font-Bold="true" Font-Size="X-Small"></asp:Label>
                                                                            </div>
                                                                            <br />
                                                                            <div class="col-sm-2">
                                                                            </div>
                                                                            <div class="col-sm-8">
                                                                                <asp:UpdatePanel ID="upaConceptos" runat="server" UpdateMode="Always" OnUnload="upaConceptos_Unload">
                                                                                    <ContentTemplate>
                                                                                        <asp:GridView ID="gvConceptos" runat="server" AutoGenerateColumns="false" ShowFooter="true" CssClass="table"
                                                                                            Style="border-top: 1px solid #484848; border-left: 1px solid #484848; border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                                                            OnRowDataBound="gvConceptos_RowDataBound" OnRowCreated="gvConceptos_RowCreated">
                                                                                            <HeaderStyle CssClass="celda2" />
                                                                                            <RowStyle CssClass="celda6" />
                                                                                            <FooterStyle CssClass="celda3" />
                                                                                            <Columns>
                                                                                                <asp:BoundField HeaderText="Concepto" DataField="Concepto"></asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                                                                <asp:TemplateField HeaderText="Unidad">
                                                                                                    <ItemTemplate>
                                                                                                        <dx:ASPxLabel ID="lblDescUnidad" runat="server" Theme="Office2010Black"></dx:ASPxLabel>
                                                                                                        <dx:ASPxTextBox ID="txtDescUnidad" runat="server" Theme="Office2010Black" Width="80" Height="17" AutoPostBack="true" OnTextChanged="txtDescUnidad_TextChanged"></dx:ASPxTextBox>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <%--<asp:BoundField HeaderText="Unidad" DataField="Unidad" ItemStyle-HorizontalAlign="Center" >
                                                                                                    </asp:BoundField>--%>
                                                                                                <%--<asp:BoundField HeaderText="Tarifa Dlls" DataField="TarifaDlls" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" >
                                                                                                    </asp:BoundField>--%>
                                                                                                <asp:BoundField HeaderText="Importe" DataField="Importe" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"></asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Hrs. a Descontar" DataField="HrDescontar" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center"></asp:BoundField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                            <div class="col-sm-2">
                                                                            </div>
                                                                        </div>
                                                                        <br />
                                                                        <div id="DivTiempo" class="col-sm-12">
                                                                            <div class="col-lg-4">
                                                                                    <dx:ASPxCheckBox ID="chkTiempoAdi" runat="server" Text="¿Desea modificar el presupuesto?" TextAlign="Left"
                                                                                        OnCheckedChanged="chkTiempoAdi_CheckedChanged" Theme="Office2010Black" AutoPostBack="true"></dx:ASPxCheckBox>
                                                                                </div>
                                                                                <div class="col-lg-1">
                                                                                    <dx:ASPxLabel ID="lblConceptoAdi" runat="server" Text="Concepto:" Theme="Office2010Black" Visible="false"></dx:ASPxLabel>
                                                                                </div>
                                                                                <div class="col-lg-3">
                                                                                    <dx:ASPxComboBox ID="ddlConceptoAdi" runat="server" Theme="Office2010Black" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlConceptoAdi_SelectedIndexChanged"></dx:ASPxComboBox>
                                                                                </div>
                                                                                <div class="col-lg-1">
                                                                                    <dx:ASPxLabel ID="lblTiempoAdi" runat="server" Text="Tiempo:" Theme="Office2010Black" Visible="false"></dx:ASPxLabel>
                                                                                </div>
                                                                                <div class="col-lg-1">
                                                                                    <dx:ASPxTextBox ID="txttiempoAdi" runat="server" Theme="Office2010Black" Width="80px" Visible="false"></dx:ASPxTextBox>
                                                                                </div>
                                                                                <div class="col-lg-2" style="text-align:right;">
                                                                                    <dx:ASPxButton ID="btnAgregarTiempo" runat="server" Text="Agregar" Theme="Office2010Black" Visible="false" OnClick="btnAgregarTiempo_Click"></dx:ASPxButton>
                                                                                </div>
                                                                        </div>
                                                                    </fieldset>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <table style="width:100%">
                                                                <tr>
                                                                    <td style="width:50%; text-align:left">
                                                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo"><span class="glyphicon glyphicon-th">
                                                                        </span>Ferrys Virtuales</a>
                                                                    </td>

                                                                    <td style="width:50%; text-align:right">
                                                                        <asp:Image ID="imbFerrys" runat="server" ImageUrl="" Height="24px" Width="24px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </h4>
                                                    </div>
                                                    <div id="collapseTwo" class="panel-collapse collapse">
                                                        <div id="divFerrys" runat="server" class="panel-body">

                                                            <%--TRAMOS SEGUNDO PRESUPUESTO--%>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <fieldset class="Personal1">
                                                                        <legend>
                                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Ruta de Vuelo</span>
                                                                        </legend>
                                                                        <div class="col-sm-6">
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                           <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="NOTA: En las cotizaciones de vuelo la ruta debe salir y regresar a base."></dx:ASPxLabel>
                                                                        </div>
                                                                        <div id="mDiv2" class="col-sm-12">
                                                                            <dx:ASPxButton ID="btnAgregaPierna2" runat="server" Text="Agregar Tramo" Theme="Office2010Black" ValidationGroup="VGTramo" OnClick="btnAgregaPierna2_Click">
                                                                            </dx:ASPxButton>
                                                                            <%--<dx:ASPxButton ID="btnAgregaPierna" runat="server" Text="Agregar Tramo" Theme="Office2010Black" OnClick="btnAgregaPierna_Click"></dx:ASPxButton>--%>
                                                                            <br />
                                                                            <div style="height: 10px"></div>
                                                                            <asp:GridView ID="gvTramosOpc2" runat="server" AutoGenerateColumns="false" ShowFooter="true" CssClass="table"
                                                                                Style="border-top: 1px solid #484848; border-left: 1px solid #484848; border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                                                OnRowDataBound="gvTramosOpc2_RowDataBound">
                                                                                <HeaderStyle CssClass="celda2" />
                                                                                <RowStyle CssClass="celda6" Height="16px" />
                                                                                <FooterStyle CssClass="celda3" />
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Origen" DataField="Origen" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Destino" DataField="Destino" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:TemplateField HeaderText="Pax">
                                                                                        <ItemTemplate>
                                                                                            <dx:ASPxTextBox ID="txtCantPax2" runat="server" Theme="Office2010Black" Width="70" AutoPostBack="true"
                                                                                                OnTextChanged="txtCantPax2_TextChanged"></dx:ASPxTextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Fecha Salida" ControlStyle-Width="10px" ItemStyle-Width="10px">
                                                                                        <ItemTemplate>
                                                                                            <dx:ASPxDateEdit ID="txtFechaSalida2" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom"
                                                                                                OnDateChanged="txtFechaSalida2_DateChanged" Theme="Office2010Black" AutoPostBack="true"
                                                                                                TimeSectionProperties-Visible="true" EditFormatString="dd/MM/yyyy HH:mm">
                                                                                                <DropDownButton>
                                                                                                    <Image IconID="scheduling_calendar_16x16"></Image>
                                                                                                </DropDownButton>
                                                                                            </dx:ASPxDateEdit>
                                                                                        </ItemTemplate>
                                                                                        <ControlStyle Width="10px"></ControlStyle>
                                                                                        <ItemStyle Width="10px"></ItemStyle>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Fecha Llegada" HeaderStyle-Width="10px" ItemStyle-Width="10px" FooterStyle-Width="10px">
                                                                                        <ItemTemplate>
                                                                                            <dx:ASPxDateEdit ID="txtFechaLlegada2" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom"
                                                                                                TimeSectionProperties-Visible="true" EditFormatString="dd/MM/yyyy HH:mm" Theme="Office2010Black" AutoPostBack="true"
                                                                                                OnDateChanged="txtFechaLlegada2_DateChanged">
                                                                                                <DropDownButton>
                                                                                                    <Image IconID="scheduling_calendar_16x16"></Image>
                                                                                                </DropDownButton>
                                                                                            </dx:ASPxDateEdit>
                                                                                        </ItemTemplate>
                                                                                        <FooterStyle Width="10px"></FooterStyle>
                                                                                        <HeaderStyle Width="10px"></HeaderStyle>
                                                                                        <ItemStyle Width="10px"></ItemStyle>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField HeaderText="Tiempo Vuelo Real" DataField="TiempoVuelo"></asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Tiempo Espera" DataField="TiempoEspera"></asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Tiempo Cobrar" DataField="TiempoCobrar"></asp:BoundField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="imbDelete2" runat="server" ImageUrl="~/img/iconos/delete.png" Width="24" Height="24" ToolTip="Elimina el tramo."
                                                                                                OnClick="imbDelete2_Click" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>

                                                                        <div class="col-sm-6">
                                                                            <div style="text-align:left">
                                                                                <dx:ASPxButton ID="btnSelOpc2" runat="server" Text="Seleccionar" Theme ="Office2010Black" OnClick="btnSelOpc2_Click"></dx:ASPxButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                        </div>
                                                                    </fieldset>
                                                                </div>
                                                            </div>
                                                            <%--SERVICIOS CON CARGO--%>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <fieldset class="Personal1">
                                                                        <legend>
                                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Servicios con Cargo en</span>
                                                                        </legend>
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                        <div id="divServiciosC" class="col-sm-8">
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" OnUnload="upaServicios_Unload">
                                                                                <ContentTemplate>
                                                                                    <asp:GridView ID="gvServiciosC2" runat="server" AutoGenerateColumns="false" CssClass="table" Width="100%" DataKeyNames="IdServicioConCargo"
                                                                                        Style="border-top: 1px solid #484848; border-left: 1px solid #484848; border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                                                        OnRowDataBound="gvServiciosC2_RowDataBound" OnRowCommand="gvServiciosC2_RowCommand">
                                                                                        <HeaderStyle CssClass="celda2" />
                                                                                        <RowStyle CssClass="celda6" />
                                                                                        <FooterStyle CssClass="celda3" />
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="ServicioConCargoDescripcion" HeaderText="Servicio con Cargo" ItemStyle-Width="60%">
                                                                                                <ItemStyle Width="60%"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                                                                                <HeaderTemplate>
                                                                                                    <dx:ASPxLabel ID="lblHead" runat="server" Text="(MXN)" Theme="Office2010Black"></dx:ASPxLabel>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <dx:ASPxTextBox ID="txtImporte2" runat="server" Theme="Office2010Black" Width="150px"
                                                                                                        OnTextChanged="txtImporte2_TextChanged" AutoPostBack="true">
                                                                                                    </dx:ASPxTextBox>
                                                                                                </ItemTemplate>

                                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Acciones">
                                                                                                <ItemTemplate>
                                                                                                    <dx:ASPxButton ID="btnEliminarSC2" runat="server" Text="Eliminar" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Theme="Office2010Black"></dx:ASPxButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                    </fieldset>
                                                                </div>
                                                            </div>
                                                            <%--TOTALES--%>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <fieldset class="Personal1">
                                                                        <legend>
                                                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Totales</span>
                                                                        </legend>
                                                                        <div id="divTotales" class="col-sm-12">
                                                                            <div class="col-sm-12">
                                                                                <asp:Label ID="Label1" runat="server" Text="" Font-Bold="true" Font-Size="X-Small"></asp:Label>
                                                                            </div>
                                                                            <br />
                                                                            <div class="col-sm-2">
                                                                            </div>
                                                                            <div class="col-sm-8">
                                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" OnUnload="upaConceptos_Unload">
                                                                                    <ContentTemplate>
                                                                                        <asp:GridView ID="gvConceptos2" runat="server" AutoGenerateColumns="false" ShowFooter="true" CssClass="table"
                                                                                            Style="border-top: 1px solid #484848; border-left: 1px solid #484848; border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                                                            OnRowDataBound="gvConceptos2_RowDataBound" OnRowCreated="gvConceptos2_RowCreated">
                                                                                            <HeaderStyle CssClass="celda2" />
                                                                                            <RowStyle CssClass="celda6" />
                                                                                            <FooterStyle CssClass="celda3" />
                                                                                            <Columns>
                                                                                                <asp:BoundField HeaderText="Concepto" DataField="Concepto"></asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                                                                <asp:TemplateField HeaderText="Unidad">
                                                                                                    <ItemTemplate>
                                                                                                        <dx:ASPxLabel ID="lblDescUnidad2" runat="server" Theme="Office2010Black"></dx:ASPxLabel>
                                                                                                        <dx:ASPxTextBox ID="txtDescUnidad2" runat="server" Theme="Office2010Black" Width="80" Height="17" AutoPostBack="true" OnTextChanged="txtDescUnidad2_TextChanged"></dx:ASPxTextBox>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField HeaderText="Importe" DataField="Importe" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"></asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Hrs. a Descontar" DataField="HrDescontar" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center"></asp:BoundField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                            <div class="col-sm-2">
                                                                            </div>
                                                                        </div>
                                                                        <br />
                                                                        <div id="divConceptos" class="col-sm-12">
                                                                            <div class="col-lg-4">
                                                                                    <dx:ASPxCheckBox ID="chkTiempoAdi2" runat="server" Text="¿Desea modificar el presupuesto?" TextAlign="Left"
                                                                                        OnCheckedChanged="chkTiempoAdi2_CheckedChanged" Theme="Office2010Black" AutoPostBack="true"></dx:ASPxCheckBox>
                                                                                </div>
                                                                                <div class="col-lg-1">
                                                                                    <dx:ASPxLabel ID="lblConceptoAdi2" runat="server" Text="Concepto:" Theme="Office2010Black" Visible="false"></dx:ASPxLabel>
                                                                                </div>
                                                                                <div class="col-lg-3">
                                                                                    <dx:ASPxComboBox ID="ddlConceptoAdi2" runat="server" Theme="Office2010Black" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlConceptoAdi2_SelectedIndexChanged"></dx:ASPxComboBox>
                                                                                </div>
                                                                                <div class="col-lg-1">
                                                                                    <dx:ASPxLabel ID="lblTiempoAdi2" runat="server" Text="Tiempo:" Theme="Office2010Black" Visible="false"></dx:ASPxLabel>
                                                                                </div>
                                                                                <div class="col-lg-1">
                                                                                    <dx:ASPxTextBox ID="txttiempoAdi2" runat="server" Theme="Office2010Black" Width="80px" Visible="false"></dx:ASPxTextBox>
                                                                                </div>
                                                                                <div class="col-lg-2" style="text-align:right;">
                                                                                    <dx:ASPxButton ID="btnAgregarTiempo2" runat="server" Text="Agregar" Theme="Office2010Black" Visible="false" OnClick="btnAgregarTiempo2_Click"></dx:ASPxButton>
                                                                                </div>
                                                                        </div>
                                                                    </fieldset>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        



                        <div class="row">
                            <div class="col-md-12">
                                <fieldset class="Personal1">
                                    <legend>
                                        <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Observaciones</span>
                                    </legend>
                                    <div id="myDiv" class="col-sm-12">
                                        <dx:ASPxMemo runat="server" Native="false" ID="txtObservaciones" Width="100%" Theme="Office2003Blue" Height="80px" MaxLength="500"></dx:ASPxMemo>
                                    </div>
                                </fieldset>
                            </div>
                        </div>

                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-8">
                            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" Theme="Office2010Black" OnClick="btnGuardar_Click" ValidationGroup="VGTramo"></dx:ASPxButton>
                            <dx:ASPxButton ID="btnImprimir" runat="server" Text="Imprimir" Theme="Office2010Black" OnClick="btnImprimir_Click" ValidationGroup="VGTramo"></dx:ASPxButton>
                            <dx:ASPxButton ID="btnCreaSol" runat="server" Text="Actualiza Solicitud" Theme="Office2010Black" OnClick="btnCreaSol_Click" ValidationGroup="VGSolicitud"></dx:ASPxButton>
                            <dx:ASPxButton ID="btnEnviar" runat="server" Text="Enviar por correo" Theme="Office2010Black" OnClick="btnEnviar_Click" ValidationGroup="VGTramo"></dx:ASPxButton>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnImprimir" />
                    </Triggers>
                </asp:UpdatePanel>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

    <%--MODAL PARA AGREGAR PIERNAS VIRTUALES--%>
    <dx:ASPxPopupControl ID="ppTramos" runat="server" ClientInstanceName="ppTramos" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Alta de Tramos" AllowDragging="true" ShowCloseButton="true" Width="380">
        <ClientSideEvents />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btnAgregarFV">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <asp:UpdatePanel ID="upaTramos" runat="server" OnUnload="upaConceptos_Unload">
                                <ContentTemplate>
                                    <table style="width: 340px;">
                                        <tr>
                                            <td style="width:155px">
                                                <dx:ASPxLabel ID="lblOrigenFV" runat="server" ClientInstanceName="lblOrigenFV" Text="Origen:" Width="150"></dx:ASPxLabel>
                                            </td>
                                            <td style="width:175px">
                                                <dx:ASPxComboBox ID="ddlOrigenFV" runat="server" Theme="Office2010Black" NullText="Seleccione una opción" TabIndex="0"
                                                    ClientInstanceName="ddlOrigenFV" OnItemsRequestedByFilterCondition="ddlOrigenFV_ItemsRequestedByFilterCondition" AutoPostBack="true"
                                                    OnItemRequestedByValue="ddlOrigenFV_ItemRequestedByValue" OnValueChanged="ddlOrigenFV_ValueChanged" EnableCallbackMode="true" Width="170px">
                                                    <ValidationSettings EnableCustomValidation="True" ValidationGroup="VGPierna" SetFocusOnError="True"
                                                        ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="El campo es requerido." IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lblDestinoFV" runat="server" ClientInstanceName="lblDestinoFV" Text="Destino:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxComboBox ID="ddlDestinoFV" runat="server" Theme="Office2010Black" NullText="Seleccione una opción" TabIndex="0"
                                                    ClientInstanceName="ddlDestinoFV" OnItemsRequestedByFilterCondition="ddlDestinoFV_ItemsRequestedByFilterCondition"
                                                    OnItemRequestedByValue="ddlDestinoFV_ItemRequestedByValue" EnableCallbackMode="true" Width="170px">
                                                    <ValidationSettings EnableCustomValidation="True" ValidationGroup="VGPierna" SetFocusOnError="True"
                                                        ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="El campo es requerido." IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lblCantaPax" runat="server" Text="Pax:" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxSpinEdit Theme="Office2010Black" runat="server" ID="spePax" AllowUserInput="true" ShowOutOfRangeWarning="false" MinValue="0" MaxValue="20" AllowNull="false" Width="170px">
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="Ingrese información válida." ErrorTextPosition="Bottom"></ValidationSettings>
                                                </dx:ASPxSpinEdit>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 15px"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lblFechaSalidaFV" runat="server" ClientInstanceName="lblFechaSalidaFV" Text="Fecha Salida:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxDateEdit ID="txtFechaSalidaFV" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom"
                                                    ClientInstanceName="txtFechaSalidaFV" Theme="Office2010Black" AutoPostBack="true" Width="170px" EditFormatString="dd/MM/yyyy HH:mm"
                                                    OnDateChanged="txtFechaSalidaFV_DateChanged" >
                                                    <DropDownButton>
                                                        <Image IconID="scheduling_calendar_16x16"></Image>
                                                    </DropDownButton>
                                                    <ValidationSettings EnableCustomValidation="True" ValidationGroup="VGPierna" 
                                                        ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="El campo es requerido." IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                                <%--CausesValidation="True"
                                                    SetFocusOnError="True"
                                                    TimeSectionProperties-Visible="true"--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lblFechaLlegadaFV" runat="server" ClientInstanceName="lblFechaLlegadaFV" Text="Fecha Llegada:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxDateEdit ID="txtFechaLlegadaFV" ClientInstanceName ="txtFechaLlegadaFV" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom"
                                                    TimeSectionProperties-Visible="true" EditFormatString="dd/MM/yyyy HH:mm" Theme="Office2010Black" AutoPostBack="true"
                                                    Width="170px" OnDateChanged="txtFechaLlegadaFV_DateChanged">
                                                    <DropDownButton>
                                                        <Image IconID="scheduling_calendar_16x16"></Image>
                                                    </DropDownButton>
                                                    <ValidationSettings EnableCustomValidation="True" ValidationGroup="VGPierna" SetFocusOnError="True"
                                                        ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="El campo es requerido." IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                                 <%--CausesValidation="True"--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lblTiempoVueloFV" runat="server" ClientInstanceName="lblTiempoVueloFV" Text="Tiempo de Vuelo:"></dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="txtTiempoVueloFV" runat="server" Theme="Office2010Black" MaskSettings-Mask="00:00" Width="80px">
                                                    <ValidationSettings EnableCustomValidation="true" ValidationGroup="VGPierna" SetFocusOnError="true"
                                                        ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="El campo es requerido." IsRequired="true" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 15px"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div style="width:100%; text-align:center">
                                                    <dx:ASPxButton ID="btnAgregarFV" runat="server" Text="Agregar" Theme="Office2010Black" TabIndex="1" 
                                                        ValidationGroup="VGPierna" AutoPostBack="true" OnClick="btnAgregarFV_Click1">
                                                        <ClientSideEvents Click="function(s, e) 
                                                                                {
                                                                                    if(ASPxClientEdit.ValidateGroup('VGPierna')) 
                                                                                    {
                                                                                        var fecSalida = txtFechaSalidaFV.GetDate();
                                                                                        var fecllegada = txtFechaLlegadaFV.GetDate();
                                                                                        
                                                                                        if (fecSalida >= fecllegada)
                                                                                        {
                                                                                            txtFechaLlegadaFV.isValid = false;
                                                                                            txtFechaLlegadaFV.errorText = 'error de fecha';
                                                                                            return;
                                                                                        }
                                                                                        else
                                                                                            ppTramos.Hide();
                                                                                    }
                                                                                    
                                                                                }" />
                                                    </dx:ASPxButton>
                                                    <dx:ASPxButton ID="btnCancelarFV" runat="server" Text="Cancelar" Theme="Office2010Black" AutoPostBack="false">
                                                        <ClientSideEvents Click="function(s, e) { ppTramos.Hide(); }" />
                                                    </dx:ASPxButton>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>
