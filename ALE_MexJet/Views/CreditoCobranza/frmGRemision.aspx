<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmGRemision.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ALE_MexJet.Views.CreditoCobranza.frmGRemision" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ControlesUsuario/ucPizarraElectronica.ascx" TagPrefix="ucP" TagName="ucPizarraElectronica" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Styles/Controls.css" />
    <style type="text/css">
        .FormatRadioButtonList label {
            margin-right: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $('.txtFechaSalida').mask('00/00/0000 00:00:00',
            {
                'translation': { 0: { pattern: /[0-9*]/ } }
            });

        function RedireccionaWizard(Index) {
            pcPrin.SetActiveTabIndex(4);
        }

        function Redirecciona(cad) {
            location.href = cad;
        }

        function HidePopUp() {
            var p = window.parent;
            var popup = p.window["CallbackPanelOffers"];
            popup.Hide();
        }

        function SetPCVisible(value) {
            debugger;
            var popupControl = GetPopupControl();
            if (!popupControl.isCollapsed)
                popupControl.Show();
            else
                popupControl.Hide();

            return false;
        }

        function GetPopupControl() {
            return ASPxPopupClientControl;
        }

        function alerta() {
            alert("Mensaje de prueba");
        }

        function OcultaError() {
            ppMensaje.Hide();
        }

        function onSelectionGridViewAction(s, e) {
            $("#selectionLabel").html("Total rows selected: " + s.GetSelectedRowCount());
        }
    </script>

    <dx:ASPxPanel ID="pnlPrincipal" runat="server" Width="100%" BackColor="White" style="border-radius: 14px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Generación de Remisiones</span>
                    </div>
                </div>
                <br />
                
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <ucP:ucPizarraElectronica id="ucPizarraElectronica" runat="server" />

                <asp:HiddenField ID="hdFactorIntercambio" runat="server" Value="0" />
                <asp:HiddenField ID="hdFactorGiraHorario" runat="server" Value="0" />
                <asp:HiddenField ID="hdFactorGiraEspera" runat="server" Value="0" />
                <asp:HiddenField ID="hdFactorFechaPico" runat="server" Value="0" />
                <asp:HiddenField ID="hdFactorVueloSimul" runat="server" Value="0" />
                <asp:HiddenField ID="hdFactorTramoNal" runat="server" Value="0" />
                <asp:HiddenField ID="hdFactorTramoInt" runat="server" Value="0" />

                <asp:UpdatePanel ID="upaPrincipal" runat="server" OnUnload="UpdatePanel1_Unload">
                    <ContentTemplate>
                        <div class="col-md-12" style="text-align:right">
                            <dx:ASPxButton ID ="btnPizarra" Theme="Office2010Black" runat="server" Text="Pizarra electrónica" OnClick="btnPizarra_Click" AutoPostBack ="true"></dx:ASPxButton>
                            <dx:ASPxButton ID ="btnButton" Theme="Office2010Black" runat="server" Text="Datos de Contrato" OnClick="btnButton_Click" AutoPostBack ="true" Visible="false">
                                <%--<ClientSideEvents Click="function(s, e) {                                
                                    SetPCVisible();
                            }"></ClientSideEvents>--%>
                        </dx:ASPxButton>
                        </div>
                        <dx:ASPxPageControl ID="pcRemision" runat="server" Width="100%" Height="350px" ClientInstanceName="pcPrin" 
                            EnableClientSideAPI="true" TabAlign="Justify" ActiveTabIndex="0" EnableTabScrolling="true" Theme="DevEx">
                            <TabStyle Paddings-PaddingLeft="50px" Paddings-PaddingRight="50px"/>
                            <ContentStyle>
                                <Paddings PaddingLeft="40px"/>
                            </ContentStyle>
                            <TabPages>
                                <dx:TabPage Text="1.- Alta de Remisión">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <fieldset class="Personal1" style="min-height: 350px;">
                                                <legend>
                                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Alta de Remisión</span>
                                            </legend>
                                                <br /><br />
                                            <table style="width: 100%; text-align:left" border="0" >
                                                <tr> 
                                                    <td style="width: 20%">
                                                        <dx:ASPxLabel ID="lblFecha" runat="server" Text="Fecha:"></dx:ASPxLabel>
                                                    </td>
                                                    <td style="width: 20%;" align="left">
                                                        <dx:ASPxDateEdit ID="detFecha" runat="server" NullText="dd/MM/AAAA"
                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="180px">
                                                            <DropDownButton>
                                                                <Image IconID="scheduling_calendar_16x16"></Image>
                                                            </DropDownButton>
                                                            <ValidationSettings ValidationGroup="VGRemision" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                                <RequiredField ErrorText="El campo es requerido" IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td style="width: 20%"></td>
                                                    <td style="width: 20%"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="lblCliente" runat="server" Text="Cliente:"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <%--<asp:DropDownList ID="ddlCliente" runat="server" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged" CssClass="combo"></asp:DropDownList>--%>
                                                        <dx:ASPxComboBox ID="ddlCliente" runat="server" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged" Theme="Office2010Black">
                                                            <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="VGRemision">
                                                                <RequiredField ErrorText="El campo es requerido" IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxLabel ID="lblContrato" runat="server" Text="Contrato:"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <%--<asp:DropDownList ID="ddlContrato" runat="server" CssClass="combo"></asp:DropDownList>--%>
                                                        <dx:ASPxComboBox ID="ddlContrato" runat="server" Theme="Office2010Black">
                                                            <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="VGRemision">
                                                                <RequiredField ErrorText="El campo es requerido" IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="lblAplicaIntercambio" runat="server" Text="Aplica Intercambio:"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="chkAplicaIntercambio" runat="server"></dx:ASPxCheckBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxLabel ID="lblFactorEspecial" runat="server" Text="Factor Especial:"></dx:ASPxLabel>
                                                    </td>
                                                    <td align="left">
                                                        <dx:ASPxTextBox ID="txtFactorEspecial" runat="server" ValidationSettings-Display="Static" Width="180px"
                                                            ValidationSettings-SetFocusOnError="True" ValidationSettings-ErrorTextPosition="Bottom" ValidationSettings-ErrorDisplayMode="Text">
                                                            <ValidationSettings>
                                                                <RegularExpression ValidationExpression="(-?[0-9]+(\.[0-9]+)?)" ErrorText="Error en la información ingresada." />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="center">
                                                        <dx:ASPxButton ID="btnCancelarAR" runat="server" Text="Cancelar" Theme="Office2010Black" OnClick="btnCancelarAR_Click"></dx:ASPxButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <dx:ASPxButton ID="btnGuardarAR" runat="server" Text="Guardar" ValidationGroup="VGRemision" OnClick="btnGuardarAR_Click" Theme="Office2010Black"></dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                           </fieldset>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="2.- Selección de Tramos" Enabled="false">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">
                                            <fieldset class="Personal">
                                                <legend>
                                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Datos de la Remisión</span>
                                            </legend>
                                                <div class="col-sm-12">

                                                        <div class="panel-body">
                                                            <div class="col-lg-2">
                                                                <dx:ASPxLabel runat="server" ID="lblClienteP" Text="Cliente:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespClienteP" ></dx:ASPxLabel>
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <dx:ASPxLabel runat="server" ID="lblContratoP" Text="Contrato:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespContratoP"></dx:ASPxLabel>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <dx:ASPxLabel runat="server" ID="lblTipoEquipoP" Text="Tipo de Equipo:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespTipoEquipoP" ></dx:ASPxLabel>
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <dx:ASPxLabel runat="server" ID="lblFactorEspecialP" Text="Factor Especial:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespFactorEspecialP" ></dx:ASPxLabel>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <dx:ASPxLabel runat="server" ID="lblAPlicaIntercambioP" Text="Aplica Intercambio:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespAplicaIntercambioP"></dx:ASPxLabel>
                                                            </div>
                                                        </div>

                                                </div><br />
                                            </fieldset>
                                            <p>&nbsp;</p><br />
                                            <div class="col-sm-12">
                                                <asp:Panel ID="pnlTramos" runat="server" ScrollBars="Auto" >

                                                    <asp:Timer ID="tmRecarga" runat="server" Enabled="false" Interval="100" OnTick="tmRecarga_Tick"></asp:Timer>
                                                    <asp:UpdatePanel ID="upaPiernas" runat="server" OnUnload="UpdatePanel1_Unload">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvTramos" runat="server" AutoGenerateColumns="false" CssClass="table"
                                                                style="border-top: 1px solid #484848; border-left: 1px solid #484848;border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                                OnRowDataBound="gvTramos_RowDataBound" DataKeyNames="IdBitacora" ShowFooter="true">
                                                                <HeaderStyle CssClass="celda2" />
                                                                <RowStyle CssClass="celda6" Height="16px" />
                                                                <FooterStyle CssClass="celda3" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:UpdatePanel ID="upaSeleccione" runat="server" UpdateMode="Conditional" OnUnload="UpdatePanel1_Unload">
                                                                                <ContentTemplate>
                                                                                    <asp:CheckBox ID="chbSeleccione" runat="server"/>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="FolioReal" HeaderText="No. Bitácora" />
                                                                    <asp:BoundField DataField="IdSolicitud" HeaderText="No. Solicitud" />
                                                                    <asp:BoundField DataField="TripNum" HeaderText="Trip" />
                                                                    <asp:BoundField DataField="AeronaveMatricula" HeaderText="Matrícula" />
                                                                    <asp:BoundField DataField="Origen" HeaderText="Origen" />
                                                                    <asp:BoundField DataField="Destino" HeaderText="Destino" />
                                                                    <asp:BoundField DataField="OrigenCalzo" HeaderText="Fecha Salida Calzo" />
                                                                    <asp:BoundField DataField="DestinoCalzo" HeaderText="Fecha Llegado Calzo" />
                                                                    <asp:BoundField DataField="CantPax" HeaderText="Pax" />
                                                                    <asp:TemplateField HeaderText="Tipo Pierna">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlTipoPierna" runat="server" CssClass="combo" EnableViewState="true"></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo Pierna FPK" ItemStyle-HorizontalAlign="Center"/>
                                                                    <asp:TemplateField HeaderText="¿Se Cobra?" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSeCobra" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Acciones">
                                                                        <ItemTemplate>
                                                                            <dx:ASPxButton ID="btnPreferencias" runat="server" Theme="Office2010Black" Text="Notas" OnClick="btnPreferencias_Click" CommandArgument=<%# DataBinder.Eval(Container.DataItem,"IdBitacora") %>></dx:ASPxButton >
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                        <%--<Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="tmRecarga" EventName="Tick" />
                                                        </Triggers>--%>
                                                    </asp:UpdatePanel>
                                                </asp:Panel>
                                            </div>
                                            <div class="col-sm-10" style="text-align:right">&nbsp;</div>
                                            <div class="col-sm-1" style="text-align:right">
                                                <dx:ASPxButton ID="btnRegresarTramos" runat="server" Theme="Office2010Black" Text="Regresar" OnClick="btnRegresarTramos_Click" />
                                            </div>
                                                &nbsp;<dx:ASPxButton ID="btnSiguienteTramos" runat="server" Text="Siguiente"  OnClick="btnSiguienteTramos_Click" Theme="Office2010Black"></dx:ASPxButton>
                                           
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="3.- Tramos o Pernoctas" Enabled="false">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl3" runat="server">

                                            <asp:Timer ID="tmRecarga2" runat="server" Enabled="false" Interval="100" OnTick="tmRecarga2_Tick"></asp:Timer>

                                            <div style="max-width:1100px; margin:0 auto; padding-left:-40px;">
                                            <fieldset class="Personal">
                                                <legend>
                                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Datos de la Remisión</span>
                                            </legend>
                                                <div class="col-sm-12">
 
                                                        <div class="panel-body">
                                                            <div class="col-lg-2">
                                                                <dx:ASPxLabel runat="server" ID="ASPxLabel11" Text="Cliente:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespClienteTP" ></dx:ASPxLabel>
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <dx:ASPxLabel runat="server" ID="ASPxLabel13" Text="Contrato:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespContratoTP"></dx:ASPxLabel>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <dx:ASPxLabel runat="server" ID="ASPxLabel15" Text="Tipo de Equipo:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespTipoETP" ></dx:ASPxLabel>
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <dx:ASPxLabel runat="server" ID="ASPxLabel17" Text="Factor Especial:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespFactorTP" ></dx:ASPxLabel>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <dx:ASPxLabel runat="server" ID="ASPxLabel19" Text="Aplica Intercambio:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespInterTP"></dx:ASPxLabel>
                                                            </div>
                                                        </div>

                                                </div>
                                            </fieldset>
                                            
                                            <table>
                                                <tr>
                                                    <td style="height:15px"></td>
                                                </tr>
                                            </table>
                                            <div class="container" style="width:100%">
                                                <div class="row">
                                                    <div class="col-lg-12 col-lg-12">
                                                        <div class="panel-group" id="accordion">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne"><span class="glyphicon glyphicon-folder-close">
                                                                        </span>Ferrys Virtuales</a>
                                                                    </h4>
                                                                </div>
                                                                <table>
                                                                    <tr>
                                                                        <td style="height:5px"></td>
                                                                    </tr>
                                                                </table>
                                                                <div id="collapseOne" class="panel-collapse collapse in">
                                                                    <div class="panel-body">
                                                                        <table class="table" style="width:100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="lblTotalTiempoVueloReal" runat="server" Text="Total tiempo de Vuelo Real:"></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="lblRespTiempoVueloReal" runat="server" ></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="lblTotalVueloCobrar" runat="server" Text="Total Vuelo a Cobrar:"></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="lblRespTotalVueloCobrar" runat="server" ></dx:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="lblTotalTiempoCalzoReal" runat="server" Text="Total tiempo de Calzo Real:"></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="lblRespTotalTiempoCalzo" runat="server" ></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxLabel Visible="false" ID="lblFactoresAplicados" runat="server" Text="Factores Aplicados:"></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBoxList ID="chkList" runat="server" Visible="false"
                                                                                        CellPadding="5" TextAlign="Right"
                                                                                        CellSpacing="5" RepeatColumns="2" CssClass="FormatRadioButtonList"
                                                                                        RepeatDirection="Vertical" RepeatLayout="Table">
                                                                                        <asp:ListItem Text="Factor Especial" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="Intercambio" Value="2"></asp:ListItem>
                                                                                        <asp:ListItem Text="Gira Espera" Value="3"></asp:ListItem>
                                                                                        <asp:ListItem Text="Gira Horario" Value="4"></asp:ListItem>
                                                                                        <asp:ListItem Text="Fecha Pico" Value="5"></asp:ListItem>
                                                                                        <asp:ListItem Text="Vuelo Simultaneo" Value="6"></asp:ListItem>
                                                                                        <asp:ListItem Text="Factor Vuelo Nacional" Value="7"></asp:ListItem>
                                                                                        <asp:ListItem Text="Factor Vuelo Internacional" Value="8"></asp:ListItem>
                                                                                    </asp:CheckBoxList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <br />
                                                                        
                                                                        <div class="bs-example" data-example-id="simple-responsive-table">
                                                                            <div class="table-responsive">
                                                                                
                                                                                <dx:ASPxButton ID="btnAgregarFerry" runat="server" Text="Agregar Ferry" Theme="Office2010Black" OnClick="btnAgregarFerry_Click"></dx:ASPxButton>
                                                                                <p>  </p>
                                                                                <%--<asp:UpdatePanel ID="upaOpcion1" runat="server" UpdateMode="Conditional" OnUnload="UpdatePanel1_Unload">
                                                                                    <ContentTemplate>--%>

                                                                                        <asp:GridView ID="gvTramosOpc1" runat="server" AutoGenerateColumns="false" ShowFooter="true" CssClass="table"
                                                                                            style="border-top: 1px solid #484848; border-left: 1px solid #484848;border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                                                            OnRowDataBound="gvTramosOpc1_RowDataBound">
                                                                                            <HeaderStyle CssClass="celda2" />
                                                                                            <RowStyle CssClass="celda6" Height="16px" />
                                                                                            <FooterStyle CssClass="celda3" />
                                                                                            <Columns>
                                                                                                <%--<asp:BoundField HeaderText="Id Tramo" DataField="IdTramo">
                                                                                                </asp:BoundField>--%>
                                                                                                <asp:BoundField HeaderText="Matrícula" DataField="Matricula" >
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Origen" DataField="Origen" ItemStyle-HorizontalAlign="Center">
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Destino" DataField="Destino" ItemStyle-HorizontalAlign="Center" >
                                                                                                </asp:BoundField>
                                                                                                <asp:TemplateField HeaderText="Fecha Salida"  ControlStyle-Width="10px" ItemStyle-Width="10px">
                                                                                                    <ItemTemplate>
                                                                                                        <dx:ASPxDateEdit ID="txtFechaSalida" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom"
                                                                                                            OnDateChanged="txtFechaSalida_DateChanged" Theme="Office2010Black" AutoPostBack="true"
                                                                                                            TimeSectionProperties-Visible="true" EditFormatString="dd/MM/yyyy hh:mm tt">
                                                                                                            <DropDownButton>
                                                                                                                <Image IconID="scheduling_calendar_16x16"></Image>
                                                                                                            </DropDownButton>
                                                                                                        </dx:ASPxDateEdit>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Fecha Llegada" HeaderStyle-Width="10px" ItemStyle-Width="10px" FooterStyle-Width="10px">
                                                                                                    <ItemTemplate>
                                                                                                        <dx:ASPxDateEdit ID="txtFechaLlegada" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom"
                                                                                                            TimeSectionProperties-Visible="true" EditFormatString="dd/MM/yyyy hh:mm tt" Theme="Office2010Black" AutoPostBack="true"
                                                                                                            OnDateChanged="txtFechaLlegada_DateChanged">
                                                                                                            <DropDownButton>
                                                                                                                <Image IconID="scheduling_calendar_16x16"></Image>
                                                                                                            </DropDownButton>
                                                                                                        </dx:ASPxDateEdit>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField HeaderText="Pax" DataField="CantPax" ItemStyle-HorizontalAlign="Center">
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Tiempo Calzo" DataField="TotalTiempoCalzo">
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Tiempo Vuelo" DataField="TotalTiempoVuelo">
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Tiempo a Cobrar" DataField="TiempoCobrar">
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Tiempo Espera" DataField="TiempoEspera">
                                                                                                </asp:BoundField>
                                                                                                <asp:TemplateField HeaderText="Tipo Pierna">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:DropDownList ID="ddlTipoPierna" runat="server" CssClass="combo"></asp:DropDownList>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="¿Se Cobra?" ItemStyle-HorizontalAlign="Center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblSeCobra" runat="server" Text='<%# (Eval("SeCobra").ToString() == "1") ? "Si" : "No" %>'>
                                                                                                        </asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField HeaderText="Real-Virtual" DataField="RealVirtual">
                                                                                                </asp:BoundField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:UpdatePanel ID="upaDelete" runat="server" OnUnload="UpdatePanel1_Unload">
                                                                                                            <ContentTemplate>
                                                                                                                <asp:ImageButton ID="imbDelete" runat="server" ImageUrl="~/img/iconos/delete.png" Width="24" Height="24" 
                                                                                                                    ToolTip="Elimina el registro." OnClick="imbDelete_Click" />
                                                                                                            </ContentTemplate>
                                                                                                            <Triggers>
                                                                                                                <asp:AsyncPostBackTrigger ControlID="imbDelete" EventName="Click" />
                                                                                                            </Triggers>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    <%--</ContentTemplate>
                                                                                </asp:UpdatePanel>--%>
                                                                            </div>
                                                                        </div>
                                                                        <br />
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:GridView ID="gvConceptos" runat="server" AutoGenerateColumns="false" ShowFooter="true" CssClass="table"
                                                                                style="border-top: 1px solid #484848; border-left: 1px solid #484848;border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                                                OnRowDataBound="gvConceptos_RowDataBound">
                                                                                <HeaderStyle CssClass="celda2" />
                                                                                <RowStyle CssClass="celda6" />
                                                                                <FooterStyle CssClass="celda3" />
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Concepto" DataField="Concepto">
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" ItemStyle-HorizontalAlign="Center" >
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Tarifa Dlls" DataField="TarifaDlls" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" >
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Importe" DataField="Importe" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}">
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Hrs. a Descontar" DataField="HrDescontar" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                                                    </asp:BoundField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                        <div class="col-lg-12" style="text-align:right"> 
                                                                            <dx:ASPxButton ID="btnSiguienteSC" runat="server" Text="Seleccionar" Theme="Office2010Black" OnClick="btnSiguienteSC_Click"></dx:ASPxButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo"><span class="glyphicon glyphicon-th">
                                                                        </span>Cálculo con pernoctas</a>
                                                                    </h4>
                                                                </div>
                                                                <div id="collapseTwo" class="panel-collapse collapse">
                                                                    <div class="panel-body">
                                                                        <table class="table" style="width:100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="lblTotalTiempoVueloRealF" runat="server" Text="Total tiempo de Vuelo Real:"></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="lblRespTiempoVueloRealF" runat="server" ></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="lblTotalVueloCobrarF" runat="server" Text="Total Vuelo a Cobrar:"></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="lblRespTotalVueloCobrarF" runat="server" ></dx:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="lblTotalTiempoCalzoRealF" runat="server" Text="Total tiempo de Calzo Real:"></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="lblRespTotalTiempoCalzoF" runat="server" ></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxLabel ID="lblFactoresAplicadosF" runat="server" Text="Factores Aplicados:"></dx:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBoxList ID="chkListF" runat="server"
                                                                                        CellPadding="5" TextAlign="Right"
                                                                                        CellSpacing="5" RepeatColumns="2" CssClass="FormatRadioButtonList"
                                                                                        RepeatDirection="Vertical" RepeatLayout="Table">
                                                                                        <asp:ListItem Text="Factor Especial&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="Intercambio" Value="2"></asp:ListItem>
                                                                                        <asp:ListItem Text="Gira Espera" Value="3"></asp:ListItem>
                                                                                        <asp:ListItem Text="Gira Horario" Value="4"></asp:ListItem>
                                                                                        <asp:ListItem Text="Fecha Pico" Value="5"></asp:ListItem>
                                                                                        <asp:ListItem Text="Vuelo Simultaneo" Value="6"></asp:ListItem>
                                                                                        <asp:ListItem Text="Factor Vuelo Nacional" Value="7"></asp:ListItem>
                                                                                        <asp:ListItem Text="Factor Vuelo Internacional" Value="8"></asp:ListItem>
                                                                                    </asp:CheckBoxList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <br />
                                                                        <div class="bs-example" data-example-id="simple-responsive-table">
                                                                            <div class="table-responsive">
                                                                                
                                                                                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Agregar Ferry" Theme="Office2010Black" OnClick="btnAgregarFerry_Click"></dx:ASPxButton>
                                                                                <p>  </p>
                                                                                <asp:GridView ID="gvTramosOpc2" runat="server" AutoGenerateColumns="false" ShowFooter="true" CssClass="table"
                                                                                    style="border-top: 1px solid #484848; border-left: 1px solid #484848;border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                                                    OnRowDataBound="gvTramosOpc2_RowDataBound">
                                                                                    <HeaderStyle CssClass="celda2" />
                                                                                    <RowStyle CssClass="celda6" Height="16px" />
                                                                                    <FooterStyle CssClass="celda3" />
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="Matrícula" DataField="Matricula" >
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Origen" DataField="Origen" ItemStyle-HorizontalAlign="Center">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Destino" DataField="Destino" ItemStyle-HorizontalAlign="Center" >
                                                                                        </asp:BoundField>
                                                                                        <asp:TemplateField HeaderText="Fecha Salida"  ControlStyle-Width="10px" ItemStyle-Width="10px">
                                                                                            <ItemTemplate>
                                                                                                <dx:ASPxDateEdit ID="txtFechaSalida" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom" Theme="Office2010Black" AutoPostBack="true"
                                                                                                    TimeSectionProperties-Visible="true" EditFormatString="dd/MM/yyyy hh:mm tt">
                                                                                                    <DropDownButton>
                                                                                                        <Image IconID="scheduling_calendar_16x16"></Image>
                                                                                                    </DropDownButton>
                                                                                                </dx:ASPxDateEdit>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Fecha Llegada" HeaderStyle-Width="10px" ItemStyle-Width="10px" FooterStyle-Width="10px">
                                                                                            <ItemTemplate>
                                                                                                <dx:ASPxDateEdit ID="txtFechaLlegada" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom"
                                                                                                    TimeSectionProperties-Visible="true" EditFormatString="dd/MM/yyyy hh:mm tt" Theme="Office2010Black" AutoPostBack="true">
                                                                                                    <DropDownButton>
                                                                                                        <Image IconID="scheduling_calendar_16x16"></Image>
                                                                                                    </DropDownButton>
                                                                                                </dx:ASPxDateEdit>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField HeaderText="Pax" DataField="CantPax" ItemStyle-HorizontalAlign="Center">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Tiempo Calzo" DataField="TotalTiempoCalzo">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Tiempo Vuelo" DataField="TotalTiempoVuelo">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Tiempo a Cobrar" DataField="TiempoCobrar">
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Tiempo Espera" DataField="TiempoEspera">
                                                                                        </asp:BoundField>
                                                                                        <asp:TemplateField HeaderText="Tipo Pierna">
                                                                                            <ItemTemplate>
                                                                                                <asp:DropDownList ID="ddlTipoPierna" runat="server" CssClass="combo"></asp:DropDownList>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="¿Se Cobra?" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblSeCobra" runat="server" Text='<%# (Eval("SeCobra").ToString() == "1") ? "Si" : "No" %>'>
                                                                                                </asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField HeaderText="Real-Virtual" DataField="RealVirtual">
                                                                                        </asp:BoundField>
                                                                                        <%--<asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="imbDelete" runat="server" ImageUrl="~/img/iconos/delete.png" Width="24" Height="24" ToolTip="Elimina el registro."
                                                                                                    OnClick="imbDelete_Click" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>--%>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                        <br />
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:GridView ID="gvConceptos2" runat="server" AutoGenerateColumns="false" ShowFooter="true" CssClass="table"
                                                                                style="border-top: 1px solid #484848; border-left: 1px solid #484848;border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                                                OnRowDataBound="gvConceptos2_RowDataBound">
                                                                                <HeaderStyle CssClass="celda2" />
                                                                                <RowStyle CssClass="celda6" />
                                                                                <FooterStyle CssClass="celda3" />
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Concepto" DataField="Concepto">
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" ItemStyle-HorizontalAlign="Center" >
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Tarifa Dlls" DataField="TarifaDlls" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" >
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Importe" DataField="Importe" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}">
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Hrs. a Descontar" DataField="HrDescontar" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                                                    </asp:BoundField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                        <div class="col-lg-12" style="text-align:right"> 
                                                                            <dx:ASPxButton ID="btnSiguienteOpc2" runat="server" Text="Seleccionar" Theme="Office2010Black" OnClick="btnSiguienteOpc2_Click"></dx:ASPxButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12" style="text-align:right">
                                                        <dx:ASPxButton ID="btnRegresarSC" runat="server" Text="Regresar" Theme="Office2010Black" OnClick="btnRegresarSC_Click"></dx:ASPxButton>
                                                    </div>
                                                </div>
                                            </div>
                                                </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="4.- Servicios con Cargo" Enabled="false">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl4" runat="server">
                                            <fieldset class="Personal">
                                                <legend>
                                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Datos de la Remisión</span>
                                            </legend>
                                                <div class="col-sm-12">

                                                        
                                                        <div class="panel-body">
                                                            <div class="col-lg-2">
                                                                <dx:ASPxLabel runat="server" ID="ASPxLabel1" Text="Cliente:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespClienteSC" ></dx:ASPxLabel>
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <dx:ASPxLabel runat="server" ID="ASPxLabel3" Text="Contrato:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespContratoSC"></dx:ASPxLabel>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <dx:ASPxLabel runat="server" ID="ASPxLabel5" Text="Tipo de Equipo:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespTipoESC" ></dx:ASPxLabel>
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <dx:ASPxLabel runat="server" ID="ASPxLabel7" Text="Factor Especial:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespFactorSC" ></dx:ASPxLabel>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <dx:ASPxLabel runat="server" ID="ASPxLabel9" Text="Aplica Intercambio:"></dx:ASPxLabel>
                                                                <dx:ASPxLabel runat="server" ID="lblRespInterSC"></dx:ASPxLabel>
                                                            </div

                                                    </div>
                                                </div>
                                            </fieldset>
                                            
                                            <p></p>

                                            <div class="col-lg-1">
                                            </div>
                                            <div class="col-lg-10">
                                                <asp:GridView ID="gvSC" runat="server" AutoGenerateColumns="false" CssClass="table" Width="100%"
                                                    style="border-top: 1px solid #484848; border-left: 1px solid #484848;border-right: 1px solid #484848; border-bottom: 1px solid #484848;">
                                                    <HeaderStyle CssClass="celda2" />
                                                    <RowStyle CssClass="celda6" />
                                                    <FooterStyle CssClass="celda3" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Concepto" DataField="Concepto" ItemStyle-Width="200px">
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Costo Directo" DataField="CostoDirecto" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}">
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Costo Combustible" DataField="TarifaVuelo" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}">
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Tarifa Dlls" DataField="TarifaDlls" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" >
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" >
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Importe Dlls" DataField="ImporteDlls" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}">
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Hrs. a Descontar" DataField="HrDescontar" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-1">
                                            </div>


                                            <div class="col-lg-4">
                                                <dx:ASPxCheckBox ID="chkTiempoAdi" runat="server" Text="¿Desea agregar tiempo adicional?" TextAlign="Left"
                                                    OnCheckedChanged="chkTiempoAdi_CheckedChanged" Theme="Office2010Black" AutoPostBack="true"></dx:ASPxCheckBox>
                                            </div>
                                            <div class="col-lg-1">
                                                <dx:ASPxLabel ID="lblTiempoAdi" runat="server" Text="Tiempo:" Theme="Office2010Black" Visible="false"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-lg-1">
                                                <dx:ASPxTextBox ID="txttiempoAdi" runat="server" Theme="Office2010Black" Width="80px" Visible="false"></dx:ASPxTextBox>
                                            </div>
                                            <div class="col-lg-1">
                                                <dx:ASPxLabel ID="lblConceptoAdi" runat="server" Text="Concepto:" Theme="Office2010Black" Visible="false"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-lg-3">
                                                <dx:ASPxComboBox ID="ddlConceptoAdi" runat="server" Theme="Office2010Black" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlConceptoAdi_SelectedIndexChanged"></dx:ASPxComboBox>
                                            </div>
                                            <div class="col-lg-2" style="text-align:right;">
                                                <dx:ASPxButton ID="btnAgregarTiempo" runat="server" Text="Agregar" Theme="Office2010Black" Visible="false" OnClick="btnAgregarTiempo_Click"></dx:ASPxButton>
                                            </div>

                                            <h1></h1>

                                            <div class="col-lg-1">
                                            </div>
                                            <div class="col-lg-12" style="text-align:right">
                                                <br />
                                                <dx:ASPxButton ID="btnAgregarSC" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnAgregarSC_Click"></dx:ASPxButton>
                                                <br />
                                                <asp:GridView ID="gvServiciosC" runat="server" AutoGenerateColumns="false" CssClass="table" Width="100%" DataKeyNames="IdServicioConCargo"
                                                    style="border-top: 1px solid #484848; border-left: 1px solid #484848;border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                    OnRowDataBound="gvServiciosC_RowDataBound" OnRowCommand="gvServiciosC_RowCommand">
                                                    <HeaderStyle CssClass="celda2"/>
                                                    <RowStyle CssClass="celda6" />
                                                    <FooterStyle CssClass="celda3" />
                                                    <Columns>
                                                        <asp:BoundField DataField="ServicioConCargoDescripcion" HeaderText="Servicio con Cargo" ItemStyle-Width="60%" />
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <dx:ASPxLabel ID="lblHead" runat="server" Text="(MXN)" Theme="Office2010Black"></dx:ASPxLabel>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <dx:ASPxTextBox ID="txtImporte" runat="server" Theme="Office2010Black" Width="150px"></dx:ASPxTextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Acciones">
                                                            <ItemTemplate>
                                                                <dx:ASPxButton ID="btnEliminarSC" runat="server" Text="Eliminar" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Theme="Office2010Black"></dx:ASPxButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <br /><dx:ASPxButton ID="btnRecalcular" runat="server" Text="Recalcular" Theme="Office2010Black" OnClick="btnRecalcular_Click"></dx:ASPxButton>
                                            </div>
                                            <div class="col-lg-5" style="vertical-align:middle">
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <dx:ASPxLabel ID="lblTotalPesos" runat="server" Text="" Font-Bold="true" Theme="Office2010Black"></dx:ASPxLabel>
                                                <br />
                                                <br />
                                                <dx:ASPxLabel ID="lblTotalDlls" runat="server" Text="" Font-Bold="true" Theme="Office2010Black"></dx:ASPxLabel>
                                            </div>
                                            
                                            <div class="col-lg-1">
                                            </div>
                                            <div class="col-lg-12" style="text-align:right">
                                                <dx:ASPxButton ID="btnFinalizar" runat="server" Text="Finalizar" Theme="Office2010Black" OnClick="btnFinalizar_Click"></dx:ASPxButton>
                                                <dx:ASPxButton ID="btnFinalizarSCC" runat="server" Text="Finalizar con conversión a Tiempo" Theme="Office2010Black" OnClick="btnFinalizarSCC_Click" Visible="false"></dx:ASPxButton>
                                            </div>
                                            <div class="col-lg-1">
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>

            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

    <%--MODAL PARA MENSAJES--%>
    <dx:ASPxPopupControl ID="ppAlert" runat="server" ClientInstanceName="ppAlert" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true" Width="300">
        <ClientSideEvents />
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
                                        <dx:ASPxButton ID="btOK" runat="server" Text="OK" Theme="PlasticBlue" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {ppAlert.Hide(); }" />
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

    <%--MODAL PARA AGREGAR SERVICIOS CON CARGO--%>
    <dx:ASPxPopupControl ID="ppServicios" runat="server" ClientInstanceName="ppServicios" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Servicios con cargo" AllowDragging="true" ShowCloseButton="true" Width="350">
        <ClientSideEvents />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btnGuardarSC">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table style="width:100%">
                                <tr>
                                    <td style="width:50%">
                                        <dx:ASPxLabel ID="lblServicioCargo" runat="server" ClientInstanceName="lblServicioCargo" Text="Servicio con cargo:"></dx:ASPxLabel>
                                    </td>
                                    <td style="width:50%">
                                        <dx:ASPxComboBox ID="ddlServiciosCargo" runat="server" Theme="Office2010Black" NullText="Seleccione una opción" TabIndex="0"
                                            ClientInstanceName="ddlServiciosCargo">
                                            <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="VGServicio">
                                                <RequiredField ErrorText="El campo es requerido." IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height:5px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:right">
                                        <dx:ASPxButton ID="btnGuardarSC" runat="server" Text="Agregar" Theme="Office2010Black" Width="80px" AutoPostBack="true" 
                                            Style="float: left; margin-right: 8px" TabIndex="1" OnClick="btnGuardarSC_Click" ValidationGroup="VGServicio">
                                            <ClientSideEvents Click="function(s, e) {ppServicios.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="text-align:left">
                                        <dx:ASPxButton ID="btnCancelarSC" runat="server" Text="Cancelar" Theme="Office2010Black" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="2">
                                            <ClientSideEvents Click="function(s, e) {ppServicios.Hide(); }" />
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

    <%--MODAL PARA AGREGAR PIERNAS VIRTUALES--%>
    <dx:ASPxPopupControl ID="ppVirtuales" runat="server" ClientInstanceName="ppVirtuales" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Alta de Piernas Virtuales" AllowDragging="true" ShowCloseButton="true" Width="350">
        <ClientSideEvents />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btnGuardarSC">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <%--<asp:UpdatePanel ID="upaFV" runat="server">
                                <ContentTemplate>--%>
                                    <table style="width:100%">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lblPosicionFV" runat="server" Text="Posición del Ferry" Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxRadioButtonList ID="rblPosicionFV" runat="server" Theme="Office2010Black" RepeatDirection="Horizontal"
                                            OnValueChanged="rblPosicionFV_ValueChanged" AutoPostBack="true">
                                            <Items>
                                                <dx:ListEditItem Text="Arriba" Value="1" />
                                                <dx:ListEditItem Text="Abajo" Value="2" Selected="true" />
                                            </Items>
                                        </dx:ASPxRadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:50%">
                                        <dx:ASPxLabel ID="lblOrigenFV" runat="server" ClientInstanceName="lblOrigenFV" Text="Origen:"></dx:ASPxLabel>
                                    </td>
                                    <td style="width:50%">
                                        <dx:ASPxComboBox ID="ddlOrigenFV" runat="server" Theme="Office2010Black" NullText="Seleccione una opción" TabIndex="0"
                                            ClientInstanceName="ddlOrigenFV" OnItemsRequestedByFilterCondition="ddlOrigenFV_ItemsRequestedByFilterCondition"
                                            OnItemRequestedByValue="ddlOrigenFV_ItemRequestedByValue" EnableCallbackMode="true">
                                            <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="VGFerrysV">
                                                <RequiredField ErrorText="El campo es requerido." IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:50%">
                                        <dx:ASPxLabel ID="lblDestinoFV" runat="server" ClientInstanceName="lblDestinoFV" Text="Destino:"></dx:ASPxLabel>
                                    </td>
                                    <td style="width:50%">
                                        <dx:ASPxComboBox ID="ddlDestinoFV" runat="server" Theme="Office2010Black" NullText="Seleccione una opción" TabIndex="0"
                                            ClientInstanceName="ddlDestinoFV" OnItemsRequestedByFilterCondition="ddlDestinoFV_ItemsRequestedByFilterCondition"
                                            OnItemRequestedByValue="ddlDestinoFV_ItemRequestedByValue" EnableCallbackMode="true">
                                            <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="VGFerrysV">
                                                <RequiredField ErrorText="El campo es requerido." IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:50%">
                                        <dx:ASPxLabel ID="lblFechaSalidaFV" runat="server" ClientInstanceName="lblFechaSalidaFV" Text="Fecha Salida:"></dx:ASPxLabel>
                                    </td>
                                    <td style="width:50%">
                                        <dx:ASPxDateEdit ID="txtFechaSalidaFV" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom"
                                            OnDateChanged="txtFechaSalidaFV_DateChanged" Theme="Office2010Black" AutoPostBack="true"
                                            TimeSectionProperties-Visible="true" EditFormatString="dd/MM/yyyy hh:mm tt">
                                            <DropDownButton>
                                                <Image IconID="scheduling_calendar_16x16"></Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="VGFerrysV">
                                                <RequiredField IsRequired="true" ErrorText="El campo es requerido." />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:50%">
                                        <dx:ASPxLabel ID="lblFechaLlegadaFV" runat="server" ClientInstanceName="lblFechaLlegadaFV" Text="Fecha Llegada:"></dx:ASPxLabel>
                                    </td>
                                    <td style="width:50%">
                                        <dx:ASPxDateEdit ID="txtFechaLlegadaFV" runat="server" TimeSectionProperties-TimeEditProperties-EditFormat="Custom"
                                            TimeSectionProperties-Visible="true" EditFormatString="dd/MM/yyyy hh:mm tt" Theme="Office2010Black" AutoPostBack="true"
                                            OnDateChanged="txtFechaLlegadaFV_DateChanged">
                                            <DropDownButton>
                                                <Image IconID="scheduling_calendar_16x16"></Image>
                                            </DropDownButton>
                                            <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="VGFerrysV">
                                                <RequiredField IsRequired="true" ErrorText="El campo es requerido." />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:50%">
                                        <dx:ASPxLabel ID="lblTiempoVueloFV" runat="server" ClientInstanceName="lblTiempoVueloFV" Text="Tiempo de Vuelo:"></dx:ASPxLabel>
                                    </td>
                                    <td style="width:50%">
                                        <dx:ASPxTextBox ID="txtTiempoVueloFV" runat="server" Theme="Office2010Black" MaskSettings-Mask="00:00">
                                            <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="VGFerrysV">
                                                <RequiredField IsRequired="true" ErrorText="El campo es requerido." />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:50%">
                                        <dx:ASPxLabel ID="lblTipoPiernaFV" runat="server" ClientInstanceName="lblTipoPiernaFV" Text="Tipo Pierna:"></dx:ASPxLabel>
                                    </td>
                                    <td style="width:50%">
                                        <asp:DropDownList ID="ddlTipoPiernaFV" runat="server" CssClass="combo"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height:5px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:right">
                                        <dx:ASPxButton ID="btnAgregarFV" runat="server" Text="Agregar" Theme="Office2010Black" Width="80px" AutoPostBack="true" 
                                            Style="float: left; margin-right: 8px" TabIndex="1" OnClick="btnAgregarFV_Click" ValidationGroup="VGFerrysV">
                                            <ClientSideEvents Click="function(s, e) { ppVirtuales.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="text-align:left">
                                        <dx:ASPxButton ID="btnCancelarFV" runat="server" Text="Cancelar" Theme="Office2010Black" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="2">
                                            <ClientSideEvents Click="function(s, e) { ppVirtuales.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                                <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>


    <%-- MODAL PARA MOSTRAR DATOS GENERALES DE LA REMISION --%>
    <%--<dx:ASPxPopupControl ID="pcPizarra" runat="server" ClientInstanceName="pcPizarra" Width="1200px" Height="350px"
        MaxWidth="1000px" MaxHeight="1000px" MinHeight="150px" MinWidth="150px" AllowDragging="true" AllowResize="false"
        ShowFooter="false" HeaderText="" CloseOnEscape="true"  CloseAction ="CloseButton"
        EnableViewState="false" PopupHorizontalAlign="LeftSides" PopupVerticalAlign="Below" EnableHierarchyRecreation="True">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <asp:Panel ID="Panel3" runat="server">
                    <div class="row">
                        <div class="col-md-12"> 
                            <fieldset class="Personal1">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"></span>
                                </legend>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblPizClaveCliente" Text="Clave del Cliente:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblSnapClaveCliente"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblPizClaveContrato" Text="Clave del Contrato:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblSnapClaveContrato"></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblPizTipoContrato" Text="Tipo de Contrato:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblSnapTipoContrato"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblPizGrupoModelo" Text="Grupo Modelo:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblSnapGrupoModelo"></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="row">
                        <dx:ASPxPageControl ID="ASPxPageControl2" Theme="Office2010Black" runat="server" Width="100%" Height="100%"
                            TabAlign="Justify" ActiveTabIndex="0" EnableTabScrolling="true">
                            <TabStyle Paddings-PaddingLeft="50px" Paddings-PaddingRight="50px" />
                            <ContentStyle>
                                <Paddings PaddingLeft="40px" />
                            </ContentStyle>
                            <TabPages>
                                <dx:TabPage Text="Datos Generales" Enabled="true">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <dx:ASPxPanel Theme="Office2010Black" runat="server" Width="100%" BackColor="White">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <div class="row">
                                                            <div class="col-md-12 table">
                                                               <table class="table-bordered table-hover" style="width:100%; border: 0px solid #efefef;">
                                                                   <tr>
                                                                       <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:18px;">&#x1f5b9;</span> <dx:ASPxLabel runat="server" ID="lblPizFolioRemision" Text="Folio Remision:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapFolioRem" Text="000" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:16px;padding:2px;">&#x1f551;&#xfe0e;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel48" Text="Tiempo a Cobrar:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapTiempoCobrar" Text="Vuelo" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                          <span style="font-size:18px;">&#x1f551;&#xfe0e;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel50" Text="Más Minutos:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapMasMinutos" Text="10" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                        <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:20px;">&#x2bd1;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel52" Text="¿Aplica tramos pactados?:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapAplicaTramPact" Text="No" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:18px;">&#x1f551;&#xfe0e;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel56" Text="Horas Contratadas Total:"></dx:ASPxLabel>
                                                                       </td>
                                                                        <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapHorasContratadasTotal" Text="1500" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                        <td colspan="2"></td>
                                                                       
                                                                       <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:18px;">&#x1f551;&#xfe0e;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel58" Text="Horas contratadas por año:"></dx:ASPxLabel>
                                                                        </td>
                                                                        <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapHorasContratadasAnio" Text="300" style="font-weight:bold;"></dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        
                                                                        <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                          <span style="font-size:20px;">&#x2bd1;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel60" Text="¿Se cobra combustible?:"></dx:ASPxLabel>
                                                                        </td>
                                                                        <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapSeCobraComb" Text="Si" style="font-weight:bold;"></dx:ASPxLabel>
                                                                        </td>
                                                                        <td colspan="2"></td>
                                                                        
                                                                        <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:18px;">&#x1f4b3;&#xfe0e;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel62" Text="Forma de Cobro Combustible:"></dx:ASPxLabel>
                                                                        </td>
                                                                        <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblFormaCobroCombustible" Text="Promedio" style="font-weight:bold;"></dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        
                                                                        <td style="text-align:left;background-color:#efefef;padding:4px;">
                                                                           <span style="font-size:18px;">&#x2b86;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel64" Text="Factor de Tramos Nacionales:"></dx:ASPxLabel>
                                                                        </td>
                                                                        <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapFactorTramosNales" Text="0" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                        <td colspan="2"></td>
                                                                        <td style="text-align:left;background-color:#efefef;">
                                                                           <span style="font-size:18px;">&#x2b86;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel66" Text="Factor de tramos interacionales:"></dx:ASPxLabel>
                                                                       </td>
                                                                        <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapFactorTramosInter" Text="0" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td style="text-align:left;background-color:#efefef;">
                                                                           <span style="font-weight:bold;border:1px solid #000000;border-radius:50%;font-size:10px;padding: 1px;padding-bottom: 2px;">&nbsp;$ </span>&nbsp;<dx:ASPxLabel runat="server" ID="ASPxLabel68" Text="Costo Directo de vuelo Nacional:"></dx:ASPxLabel>
                                                                       </td>
                                                                        <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapCostoDirVueloNal" Text="1234.00" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                        <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;">
                                                                           <span style="font-weight:bold;border:1px solid #000000;border-radius:50%;font-size:10px;padding: 1px;padding-bottom: 2px;">&nbsp;$ </span>&nbsp;<dx:ASPxLabel runat="server" ID="ASPxLabel118" Text="Costo Directo de vuelo internacional:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapCostoDirVueloInt" Text="1234.00" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                            </div>
                                                        </div>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Tarifas de Vuelo" Enabled="true">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <dx:ASPxPanel Theme="Office2010Black" runat="server" Width="100%" BackColor="White">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <div class="row">
                                                            <div class="col-md-12 table">
                                                               <table class="table-bordered table-hover" style="width:100%; border: 0px solid #efefef;">
                                                                   <tr>
                                                                       <td style="text-align:left;background-color:#efefef;padding:4px;">
                                                                           <span style="font-size:20px;">&#x2bd1;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel22" Text="¿Se cobra tiempo de espera?:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapSeCobraTiempoEspera" Text="si" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;">
                                                                           <span style="font-weight:bold;border:1px solid #000000;border-radius:50%;font-size:10px;padding: 1px;padding-bottom: 2px;">&nbsp;$ </span>&nbsp;<dx:ASPxLabel runat="server" ID="ASPxLabel24" Text="Tarifa de espera nacional:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapTarifaEspNal" Text="385.33" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td style="text-align:left;background-color:#efefef;padding:4px;">
                                                                           <span style="font-weight:bold;border:1px solid #000000;border-radius:50%;font-size:10px;padding: 1px;padding-bottom: 2px;">&nbsp;$ </span>&nbsp;<dx:ASPxLabel runat="server" ID="ASPxLabel26" Text="Tarifa de espera internacional:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapTarifaEspInt" Text="385.33" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;">
                                                                           <span style="font-weight:bold;border:1px solid #000000;border-radius:50%;font-size:10px;padding: 1px;padding-bottom: 2px;">%</span> <dx:ASPxLabel runat="server" ID="ASPxLabel28" Text="Porcentaje de tarifa de espera nacional:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapPorcentajeTarEspNal" Text="0" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td style="text-align:left;background-color:#efefef;padding:4px;">
                                                                           <span style="font-weight:bold;border:1px solid #000000;border-radius:50%;font-size:10px;padding: 1px;padding-bottom: 2px;">%</span> <dx:ASPxLabel runat="server" ID="ASPxLabel30" Text="Porcentaje de tarifa de espera internacional:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapPorcentajeTarEspInt" Text="0" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                        <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;">
                                                                           <span style="font-size:20px;">&#x2bd1;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel36" Text="¿Se cobran pernoctas?:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapSeCobranPernoctas" Text="Si" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td style="text-align:left;background-color:#efefef;padding:4px;">
                                                                           <span style="font-weight:bold;border:1px solid #000000;border-radius:50%;font-size:10px;padding: 1px;padding-bottom: 2px;">&nbsp;$ </span>&nbsp;<dx:ASPxLabel runat="server" ID="ASPxLabel38" Text="Tarifa en Dlls de pernocta nacional:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapTarifaDllsPerNal" Text="12345.00" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                        <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;">
                                                                           <span style="font-weight:bold;border:1px solid #000000;border-radius:50%;font-size:10px;padding: 1px;padding-bottom: 2px;">%</span> <dx:ASPxLabel runat="server" ID="ASPxLabel40" Text="Porcentaje de tarifa en Dlls de pernocta nacional:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapPorcentajeTarDllsPerNal" Text="0" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td style="text-align:left;background-color:#efefef;padding:4px;">
                                                                           <span style="font-weight:bold;border:1px solid #000000;border-radius:50%;font-size:10px;padding: 1px;padding-bottom: 2px;">&nbsp;$ </span>&nbsp;<dx:ASPxLabel runat="server" ID="ASPxLabel42" Text="Tarifa end Dlls de pernocta internacional:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapTarifaDllsPerInt" Text="12345.00" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                        <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;">
                                                                           <span style="font-weight:bold;border:1px solid #000000;border-radius:50%;font-size:10px;padding: 1px;padding-bottom: 2px;">%</span> <dx:ASPxLabel runat="server" ID="ASPxLabel44" Text="Porcentaje de tarifa en Dlls de pernocta internacional:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapPorcentajeTarDllsPerInt" Text="1234.00" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                            </div>
                                                        </div>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Cobros y Descuentos" Enabled="true">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <dx:ASPxPanel Theme="Office2010Black" runat="server" Width="100%" BackColor="White">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <div class="row">
                                                            <div class="col-md-12 table">
                                                               <table class="table-bordered table-hover" style="width:100%; border: 0px solid #efefef;">
                                                                   <tr>
                                                                       <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:20px;">&#x2bd1;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel32" Text="¿Aplica vuelo simultaneo?:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapAplicaVloSimultaneo" Text="no" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                        <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:22px;">&#x2058;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel34" Text="Cuantos vuelos simultaneos:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapCuantosVuelosSim" Text="0" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:20px;">&#x21c9;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel70" Text="Factor de vuelos simultaneos:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapFactorVuelosSim" Text="1" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                        <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:20px;">&#x2bd1;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel72" Text="¿Se descuenta espera nacional?:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapSeDescuentaEspNal" Text="no" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:20px;">&#x2bd1;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel74" Text="¿Se descuenta espera internacional?:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapSeDescuentaEspInt" Text="no" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                       
                                                                        <td colspan="2"></td>
                                                                        <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:20px;">&#x1f6c9;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel86" Text="Factor de espera por hora de vuelo nacional:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapFactorEsperaHoraVueloNal" Text="0" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                       
                                                                   </tr>
                                                                   <tr>
                                                                       <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:20px;">&#x1f6c9;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel76" Text="Factor de espera por hora de vuelo internacional:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapFactorEsperaHoraVueloInt" Text="0" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:20px;">&#x2bd1;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel78" Text="¿Se descuenta pernocta nacional?:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapSeDescuentaPernoctaNal" Text="no" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                       
                                                                       
                                                                    </tr>
                                                                   <tr>
                                                                       
                                                                       <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:20px;">&#x2bd1;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel80" Text="¿Se descuenta pernocta internacional?:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapSeDescuentaPernoctaInt" Text="no" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:20px;">&#x1d1b8;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel82" Text="Factor de pernocta en hora de vuelo nacional:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapFactorPerHoraVueloNal" Text="0" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                       
                                                                    </tr>
                                                                   <tr>
                                                                       <td style="text-align:left;background-color:#efefef;padding:2px;">
                                                                           <span style="font-size:20px;">&#x1d1b8 </span><dx:ASPxLabel runat="server" ID="ASPxLabel84" Text="Factor de pernocta en hora de vuelo internacional:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;padding:2px;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapFactorPerHoraVueloInt" Text="0" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td colspan="2"></td>
                                                                       <td colspan="2" style="background-color:#efefef;padding:2px;"></td>
                                                                   </tr>
                                                               </table>
                                                            </div>
                                                        </div>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Factores Aplicados a la Remisión" Enabled="true">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <dx:ASPxPanel Theme="Office2010Black" runat="server" Width="100%" BackColor="White">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <div class="row">
                                                            <div class="col-md-12 table">
                                                                <dx:BootstrapGridView ID="gvFactoresRem" runat="server" KeyFieldName="iNoTramo">
                                                                    <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                                                    <Columns>
                                                                        <dx:BootstrapGridViewDataColumn FieldName="sMatricula" Caption="Matricula" VisibleIndex="0" HorizontalAlign="Center" Width="25" />
                                                                        <dx:BootstrapGridViewDataColumn FieldName="sOrigen" Caption="Origen" VisibleIndex="1" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="False" />
                                                                        <dx:BootstrapGridViewDataColumn FieldName="sDestino" Caption="Destino" VisibleIndex="2" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                                        <dx:BootstrapGridViewDataColumn FieldName="sTiempoOriginal" Caption="Tiempo original" VisibleIndex="3" />
                                                                        <dx:BootstrapGridViewDataColumn FieldName="sTiempoFinal" Caption="Tiempo final" VisibleIndex="4" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="False" />
                                                                        
                                                                    </Columns>
                                                                    <Templates>
                                                                    <DetailRow>
                                                                        <dx:BootstrapGridView ID="gvFactoresDetalle" runat="server" KeyFieldName="iNoTramo"
                                                                            OnBeforePerformDataSelect="gvFactoresDetalle_BeforePerformDataSelect">
                                                                            <Columns>
                                                                                <dx:BootstrapGridViewDataColumn FieldName="dFactorEspeciaRem" Caption="Factor especial rem" VisibleIndex="1" CssClasses-DataCell="hideColumn" HorizontalAlign="Center" />
                                                                                <dx:BootstrapGridViewDataColumn FieldName="dAplicaFactorTramoNacional" Caption="Factor tramo nal." VisibleIndex="3" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                                                <dx:BootstrapGridViewDataColumn FieldName="dAplicaFactorTramoInternacional" Caption="Factor tramo int." VisibleIndex="2" SortIndex="0" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                                                <dx:BootstrapGridViewDataColumn FieldName="dAplicoIntercambio" Caption="Factor intercambio" VisibleIndex="4" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                                                <dx:BootstrapGridViewDataColumn FieldName="dAplicaGiraEspera" Caption="Factor gira espera" VisibleIndex="5" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                                                <dx:BootstrapGridViewDataColumn FieldName="dAplicaGiraHorario" Caption="Factor gira horario" VisibleIndex="6" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                                                <dx:BootstrapGridViewDataColumn FieldName="dAplicaFactorFechaPico" Caption="Factor fecha pico" VisibleIndex="7" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                                                <dx:BootstrapGridViewDataColumn FieldName="dAplicaFactorVueloSimultaneo" Caption="Factor vlo simultaneo" VisibleIndex="8" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                                            </Columns>
                                                                        </dx:BootstrapGridView>
                                                                    </DetailRow>
                                                                </Templates>
                                                                <SettingsDetail ShowDetailRow="true" />
                                                                <ClientSideEvents Init="onSelectionGridViewAction" SelectionChanged="onSelectionGridViewAction" EndCallback="onSelectionGridViewAction" />
                                                                <SettingsPager NumericButtonCount="4">
                                                                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
                                                                </SettingsPager>
                                                                </dx:BootstrapGridView>
                                                               
                                                            </div>
                                                        </div>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Cobros Espera y Pernoctas" Enabled="true">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <dx:ASPxPanel Theme="Office2010Black" runat="server" Width="100%" BackColor="White">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <div class="row">
                                                            <div class="col-md-12 table">
                                                                <table class="table-bordered table-hover" style="width:100%; border: 0px solid #efefef;">
                                                                    <tr>
                                                                       <td style="text-align:left;background-color:#efefef;">
                                                                           <span style="font-size:20px;">&#x1f6c9;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel39" Text="¿Se cobra tiempo de espera?:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapSeCobraTiempoEspera2" Text="0" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;">
                                                                           <span style="font-size:20px;">&#x2bd1;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel43" Text="Horas de pernocta:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapHorasPernocta" Text="no" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                    </tr>
                                                                   <tr>
                                                                       <td style="text-align:left;background-color:#efefef;">
                                                                           <span style="font-size:20px;">&#x2bd1;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel8" Text="Tiempo de espera Total:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapTiempoEsperaGeneral" Text="no" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                        <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;">
                                                                           <span style="font-size:22px;">&#x2058;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel18" Text="Tiempo de vuelo total:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapTiempoVueloGeneral" Text="0" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td style="text-align:left;background-color:#efefef;">
                                                                           <span style="font-size:20px;">&#x21c9;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel23" Text="Factor de Hr de vuelo (Tiempo de espera):"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapFactorHrVuelo" Text="1" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                        <td colspan="2"></td>
                                                                       <td style="text-align:left;background-color:#efefef;">
                                                                           <span style="font-size:20px;">&#x2bd1;</span> <dx:ASPxLabel runat="server" ID="ASPxLabel27" Text="Total tiempo de espera cobrar:"></dx:ASPxLabel>
                                                                       </td>
                                                                       <td style="text-align:center;">
                                                                           <dx:ASPxLabel runat="server" ID="lblSnapTotalTiempoEsperaCobrar" Text="no" style="font-weight:bold;"></dx:ASPxLabel>
                                                                       </td>
                                                                    </tr>
                                                               </table>
                                                            </div>
                                                        </div>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </div>
                </asp:Panel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>--%>

    <%--MODAL PARA MOSTRAR DATOS GENERALES DEL CONTRATO--%>
    <dx:ASPxPopupControl ClientInstanceName="ASPxPopupClientControl" Width="700px" Height="300px"
        MaxWidth="1000px" MaxHeight="1000px" MinHeight="150px" MinWidth="150px" ID="pcMain" AllowDragging="true" AllowResize="false"
        ShowFooter="false" HeaderText="" CloseOnEscape="true"  CloseAction ="CloseButton"
        runat="server" EnableViewState="false" PopupHorizontalAlign="LeftSides" PopupVerticalAlign="Below" EnableHierarchyRecreation="True">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <asp:Panel ID="Panel1" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="Personal1">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"></span>
                                </legend>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblppdDescClaveCleinte" Text="Clave del Cliente:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblppdClaveCliente"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblppdDescClaveContrato" Text="Clave del Contrato:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblppdClaveContrato"></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblppdDescTipoContrato" Text="Tipo de Contrato:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblppdTipoContrato"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblppdDescGrupoModelo" Text="Grupo Modelo:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="lblppdGrupoModelo"></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="row">
                        <dx:ASPxPageControl ID="ASPxPageControl1" Theme="Office2010Black" runat="server" Width="100%" Height="100%"
                            TabAlign="Justify" ActiveTabIndex="0" EnableTabScrolling="true">
                            <TabStyle Paddings-PaddingLeft="50px" Paddings-PaddingRight="50px" />
                            <ContentStyle>
                                <Paddings PaddingLeft="40px" />
                            </ContentStyle>
                            <TabPages>
                                <dx:TabPage Text="Generales" Enabled="true">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <dx:ASPxPanel Theme="Office2010Black" runat="server" Width="100%" BackColor="White">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: left; width:25%">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescFechaInicio" Text="Fecha de Inicio:"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left; width:25%">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdFechaInicio"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left; width:25%">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescHorasContratadas" Text="Horas Contratadas:"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left; width:25%">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdHorasContratadas"></dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left; width:25%">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescHorasAño" Text="Horas por año:"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left; width:25%">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdHorasAño"></dx:ASPxLabel>
                                                                </td>
                                                                <td colspan="2" style="width:50%"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left; width:100%" colspan="4">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescBases" Text="Bases:"></dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left; width:50%" colspan="2">
                                                                    <dx:ASPxGridView ID="gvBases" runat="server" Theme="Office2010Black">
                                                                        <Columns>
                                                                            <dx:GridViewDataTextColumn FieldName="AeropuertoIATA" Caption="Aeropuerto"></dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="IdTipoBaseDesc" Caption="Tipo de Base"></dx:GridViewDataTextColumn>
                                                                        </Columns>
                                                                    </dx:ASPxGridView>

                                                                </td>
                                                                <td colspan="2" style="width:50%"></td>
                                                            </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Importes" Enabled="true">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <dx:ASPxPanel Theme="Office2010Black" runat="server" Width="100%" BackColor="White">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescEspacioVacio"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescNacional" Text="Nacional"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescCInt" Text="Internacional"></dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescCostoDirectro" Text="Costo Directo:"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdCDN"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdCDI"></dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescTiempoEspera" Text="Tiempo de Espera:"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdTEN"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdTEI"></dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescPernoctas" Text="Pernocta:"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdPernoctasN"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdPernoctasI"></dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Cobros" Enabled="true">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <dx:ASPxPanel Theme="Office2010Black" runat="server" Width="100%" BackColor="White">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: left; width:20%">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescCobroCombustible" Text="Cobra Combustible:"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left; width:20%">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdCobroCombustible"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left; width:10%">
                                                                </td>
                                                                <td style="text-align: left; width:20%">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescCobroFerrys" Text="Cobra Ferrys:"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left; width:20%">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdCobroFerrys"></dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescCobroTipoTiempo" Text="Tiempo a Cobrar:"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdCobroTipoTiempo"></dx:ASPxLabel>
                                                                </td>
                                                                <td></td>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescCobroSCC" Text="Mas Minutos:"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdCobroTipoTiempoMinutos"></dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescCobroGiras" Text="Giras:"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdCobroGiras"></dx:ASPxLabel>
                                                                </td>
                                                                <td></td>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescTiempoEsperaLibre" Text="Tiempo Espera Libre:"></dx:ASPxLabel>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdTiempoEsperaLibre"></dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;" colspan="5">
                                                                    <dx:ASPxLabel runat="server" ID="lblppdDescIntercambio" Text="Intercambios:"></dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <dx:ASPxGridView ID="gvIntercambios" runat="server" Theme="Office2010Black">
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="Descripcion" Caption="Descripción" />
                                                                        <dx:GridViewDataTextColumn FieldName="Factor" Caption="Factor" />
                                                                        <dx:GridViewDataTextColumn FieldName="TarifaNal" Caption="Tarifa Nal." PropertiesTextEdit-DisplayFormatString="{0:c4}" />
                                                                        <dx:GridViewDataTextColumn FieldName="TarifaInt" Caption="Tarifa Int." PropertiesTextEdit-DisplayFormatString="{0:c4}" />
                                                                        <dx:GridViewDataTextColumn FieldName="Galones" Caption="Galones" />
                                                                        <dx:GridViewDataTextColumn FieldName="CostoDirectoNal" Caption="Costo Directo Nal." PropertiesTextEdit-DisplayFormatString="{0:c4}" />
                                                                        <dx:GridViewDataTextColumn FieldName="CostoDirectoInt" Caption="Costo Directo Int." PropertiesTextEdit-DisplayFormatString="{0:c4}" />
                                                                    </Columns>
                                                                    </dx:ASPxGridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </div>
                </asp:Panel>



            </dx:PopupControlContentControl>
        </ContentCollection>
        <ClientSideEvents CloseUp="function(s, e) { SetImageState(false); }" PopUp="function(s, e) { SetImageState(true); }" />
    </dx:ASPxPopupControl>

    <%--MODAL PARA VER LOS CASOS DEL CONTRATO--%>
    <dx:ASPxPopupControl ID="ppNotas" runat="server" ClientInstanceName="ppNotas" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" 
        Theme="Office2010Black" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Notas del TRIP y Casos" 
        AllowDragging="true" ShowCloseButton="true" Width="400">
        <ClientSideEvents />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <asp:Panel ID="Panel2" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="Personal1">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"></span>
                                </legend>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: left; width:10%">
                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel2" Text="Alertas TRIP:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left; width:90%">
                                                    <dx:ASPxMemo ID="txtAlertasTrip" runat="server" Native="false" Width="100%" Theme="Office2003Blue" 
                                                        Height="80px" ReadOnly="true"></dx:ASPxMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height:10px"></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width:10%">
                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel4" Text="Notas TRIP:"></dx:ASPxLabel>
                                                </td>
                                                <td style="text-align: left; width:90%">
                                                    <dx:ASPxMemo ID="txtNotasTrip" runat="server" Native="false" Width="100%" Theme="Office2003Blue" 
                                                        Height="80px" ReadOnly="true"></dx:ASPxMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height:10px"></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;" colspan="2">
                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel10" Text="Casos de Contrato:"></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height:10px"></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;" colspan="2">
                                                    <dx:ASPxGridView ID="gvCasos" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                        ClientInstanceName="gvCasos" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                                        Theme="Office2010Black" Width="100%" SettingsPager-PageSize="3">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="Folio Caso" FieldName="IdCaso" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Folio Solicitud" FieldName="IdSolicitud" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Trip" FieldName="Trips" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tramo" FieldName="Tramo" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tipo de Caso" FieldName="TipoCaso" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Motivo" FieldName="Motivo" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Solicitud Especial" FieldName="SolicitudEspecial" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsPager Position="Bottom" PageSize="3"></SettingsPager>
                                                    </dx:ASPxGridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </fieldset>

                            <dx:ASPxButton ID="btnSalirNotas" runat="server" Text="Salir" Theme="Office2010Black">
                                 <ClientSideEvents Click="function(s, e) {ppNotas.Hide(); }" />
                            </dx:ASPxButton>
                        </div>
                    </div>
                </asp:Panel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <%--MODAL PARA CONFIRMAR SI DESEA UTILIZAR COTIZACIÓN ASOCIADA--%>
    <dx:ASPxPopupControl ID="ppConfirmacionSolEx" runat="server" ClientInstanceName="ppConfirmacionSolEx" CloseAction="CloseButton" Width="400px" CloseOnEscape="true" Modal="true" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Confirmación">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel5" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />                                    
                                    <table>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <dx:ASPxLabel CssClass="FExport" ID="lblConfirmacionSolEx" runat="server" Text=""></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                    </tr>                                    
                                    </table>                                    
                                    <br />
                                </div>
                            </div>
                            <div>
                                
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />
                                    
                                    <dx:ASPxButton CssClass="FBotton" ClientInstanceName="btnConfirmarSolEx" ID="btnConfirmarSolEx" runat="server" Text="Aceptar" Theme ="Office2010Black" OnClick="btnConfirmarSolEx_Click">
                                        <ClientSideEvents Click="function() {ppConfirmacionSolEx.Hide(); }" />
                                    </dx:ASPxButton>

                                    <dx:ASPxButton CssClass="FBotton" ClientInstanceName="btnSalirSolEx" runat="server" Text="Cancelar" Theme="Office2010Black">
                                        <ClientSideEvents Click="function() {ppConfirmacionSolEx.Hide(); }" />
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
                                        <td></td>
                                        <td>
                                            <dx:ASPxLabel CssClass="FExport" ID="lblConfirmacion" runat="server" Text=""></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblRentas" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblRentas_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="No" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Si" ></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td colspan="3">
                                            <dx:ASPxLabel ID="lblFolioCotizacion" runat="server" Text="Folio de cotización:   " Theme="Office2010Black" Visible="false"></dx:ASPxLabel>
                                            <dx:ASPxTextBox ID="txtFolioCotizacion" runat="server" Theme="Office2010Black" Visible="false">
                                                <ValidationSettings EnableCustomValidation="True" ValidationGroup="VGFolio" SetFocusOnError="True"
                                                    ErrorDisplayMode="Text" ErrorTextPosition="Bottom" CausesValidation="True">
                                                    <RequiredField ErrorText="El campo es requerido" IsRequired="True" />
                                                    <ErrorFrameStyle Font-Size="10px">
                                                        <ErrorTextPaddings PaddingLeft="0px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <dx:ASPxLabel ID="lblMensajeVal" runat="server" ForeColor="Red" Visible="false"></dx:ASPxLabel>
                                        </td>
                                    </tr>                                   
                                    </table>                                    
                                    <br />
                                </div>
                            </div>
                            <div>
                                
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />
                                    
                                    <dx:ASPxButton  ID="btnConfirmar" runat="server" Text="Aceptar" ClientInstanceName="btnConfirmar" OnClick="btnConfirmar_Click" Theme ="Office2010Black">
                                        <ClientSideEvents Click="function(s, e) { if(ASPxClientEdit.ValidateGroup('VGFolio')) ppConfirmacion.Hide(); }" />
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

</asp:Content>
