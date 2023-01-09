<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmRevenewM.aspx.cs" Inherits="ALE_MexJet.Views.viaticos.frmRevenewM" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Styles/Controls.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
  
    <style>
        .hiddenRow {
            /*visibility:hidden !important;*/
            display:none;
        }
        .centerCell {
            text-align:center !important;
            color: #337ab7 !important;
        }
        .centerTxt {
            text-align:center !important;
        }
        .dirTB{  
            text-align:center !important;  
        }
        .dataCell {
            font-size:11pt;
        }
        .form-control[disabled], .form-control[readonly], fieldset[disabled] .form-control {
            cursor: not-allowed !important;
            background-color: #eee !important;
            opacity: 1 !important;
            text-align:center !important;
        }
        .form-control {
            text-align:center !important;
        }
        .spa {
            font-weight: bold !important;
            color: #337ab7 !important;
            text-align: center !important;
        }
        .validateTxt {
            border-color: crimson !important;
        }
        th {
            background-color:#efefef;
            color:#3974be;
            border:1px solid #cccccc !important;
            text-align:center;
        }
        .tdderecha {
            text-align:right !important;
        }
        .tdcentro {
            text-align:center !important;
        }

        .modal-dialog {
            width: 470px !important;
            margin: 30px auto;
        }
        .modal-body {
            position: relative;
            padding: 15px;
            height: auto !important;
        }
        .inputText {
            font-family: inherit !important;
            font-size: inherit !important;
            line-height: inherit !important;
            background-color: #FFFFFF !important;
            padding: 4px 4px 5px 4px !important;
            border: 1px solid #CEECF5 !important;
            border-radius: 4px;
            color: #555;
            -webkit-transition: 0.5s;
            
        }
        .inputStyle{
            border-radius: 3px 3px; 
            text-align:center;
            height:20px;
            border: 1px solid #17c671;
        }
        .inputText:focus {
            outline: none !important;
            border:1px solid red;
            box-shadow: 0 0 10px #719ECE;
        }
        html input[disabled] {
            cursor: default;
            padding: 3px 3px 4px 3px;
            border: 1px solid #6A6A6A;
            opacity: 0.2;
            color: #152138 !important;
            background-color: #ccc !important;
        }
        .validInput {
            color: red;
            font-size: 9pt;
        }
        .gvRows {
            background-color: #FFFFFF;
        }

        .errorValid {
            border: 1px solid red !important;
            border-radius: 3px 3px !important;
        }
        .errorValid:focus {
            outline: none !important;
            border:1px solid red;
            box-shadow: 0 0 10px #719ECE;
        }

    </style>
    <script type="text/javascript">
        function ShowPopup() {
            $("#myModalConceptos").modal({
                show: true,
                backdrop: 'static'
            });
        }
        function HidePopup() {
            $("#myModalConceptos").modal({
                show: false,
                backdrop: 'static'
            });
        }
        function ShowPopupParam() {
            $("#myModalParametros").modal();
        }
        function ShowPopupParamAd() {
            $("#myModalParametrosAd").modal();
        }
        function alpha(e) { var k; document.all ? k = e.keyCode : k = e.which; return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57)); }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upaGeneral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            
            <asp:Panel ID="pnlAdminConceptos" runat="server" Visible="true">
                <div class="row">
                    <div class="col-md-12">
                        <br />
                        <!-- Administrador de Conceptos -->
                        <fieldset class="Personal">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Administrador de Conceptos</span>
                            </legend>
                            <div class="row">
                                <div class="col-md-12">

                                    <asp:UpdatePanel ID="upaConceptos" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>

                                            <%--<div class="row" style="padding-bottom:15px;">
                                                <div class="col-lg-12" align="right">
                                                    <asp:Button ID="btnNuevoConcepto" runat="server" Text="Agregar Nuevo" CssClass="btn btn-success" OnClick="btnNuevoConcepto_Click" />
                                                </div>
                                            </div>--%>

                                            <div style="height:auto; overflow-y:auto; text-align:center;">
                                                
                                                <dx:BootstrapGridView ID="gvConceptos" runat="server" KeyFieldName="IdConcepto" OnRowCommand="gvConceptos_RowCommand">
                                                    <SettingsSearchPanel Visible="true" ShowApplyButton="true" />
                                                    <Settings ShowGroupPanel="True" ShowFilterRowMenu="true" />
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                                    <SettingsBehavior AllowSort="true" />
                                            
                                                    <Columns>

                                                        <dx:BootstrapGridViewDataColumn Caption="Concepto" FieldName="Concepto" VisibleIndex="1" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" SortIndex="0" SortOrder="None" Width="40%" />
                                                        <dx:BootstrapGridViewDataColumn Caption="Horario Inicial" FieldName="HoraIni" VisibleIndex="2" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" Width="10%" />
                                                        <dx:BootstrapGridViewDataColumn Caption="Horario Final" FieldName="HoraFin" VisibleIndex="3" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" Width="10%" />
                                                
                                                        <dx:BootstrapGridViewTextColumn Caption="Monto MXN" FieldName="MontoMXN" VisibleIndex="4" HorizontalAlign="Right" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" Width="10%">
                                                            <PropertiesTextEdit DisplayFormatString="c"></PropertiesTextEdit>
                                                        </dx:BootstrapGridViewTextColumn>
                                                        <dx:BootstrapGridViewTextColumn Caption="Monto USD" FieldName="MontoUSD" VisibleIndex="5" HorizontalAlign="Right" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" Width="10%">
                                                            <PropertiesTextEdit DisplayFormatString="c"></PropertiesTextEdit>
                                                        </dx:BootstrapGridViewTextColumn>

                                                        <dx:BootstrapGridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="6" HorizontalAlign="Center" Width="20%">
                                                            <DataItemTemplate>
                                                                <div>
                                                                    <asp:UpdatePanel ID="upaConActualiza" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <dx:BootstrapButton Text="Actualizar" ID="BootstrapButton1" runat="server" CommandArgument='<%# Eval("IdConcepto") %>' CommandName="Actualiza" AutoPostBack="true" 
                                                                                ToolTip="Actualiza" SettingsBootstrap-RenderOption="Primary"></dx:BootstrapButton>
                                                                            <%--<asp:Button ID="btnEliminar" runat="server" CommandArgument='<%# Eval("IdConcepto") %>' CommandName="Eliminar" ToolTip="Elimina" 
                                                                                CssClass="btn btn-danger" Text="Eliminar" OnClientClick="return confirm('¿Desea eliminar el concepto?');" />--%>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    
                                                                </div>
                                                            </DataItemTemplate>
                                                            <CssClasses HeaderCell="spa" />
                                                        </dx:BootstrapGridViewDataColumn>

                                                        <dx:BootstrapGridViewDataColumn FieldName="IdConcepto" VisibleIndex="7" CssClasses-DataCell="hiddenRow" HeaderBadge-CssClass="hiddenRow" Visible="false" />
                                                    </Columns>
                                                </dx:BootstrapGridView>

                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlConfiguracionParametrosAdicionales" runat="server" Visible="true">
                <div class="row">
                    <div class="col-md-12">
                        <br />
                        <fieldset class="Personal">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Conceptos Adicionales</span>
                            </legend>
                            <div class="row">

                                <div class="col-md-12">

                                    <asp:UpdatePanel ID="upaParametrosAdicionales" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>

                                            <div class="row" style="padding-bottom:15px;">
                                                <div class="col-lg-12" align="right">
                                                    <asp:Button ID="btnNuevoParametroAdicional" runat="server" Text="Agregar Nuevo" CssClass="btn btn-success" OnClick="btnNuevoParametroAdicional_Click" />
                                                </div>
                                            </div>

                                            <div class="table-responsive" style="height: auto;">


                                                 <dx:BootstrapGridView ID="gvParametrosAdicionales" runat="server" KeyFieldName="IdParametro" OnRowCommand="gvParametrosAdicionales_RowCommand">
                                                    <SettingsSearchPanel Visible="true" ShowApplyButton="true" />
                                                    <Settings ShowGroupPanel="True" ShowFilterRowMenu="true" />
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                                    <SettingsBehavior AllowSort="true" />
                                            
                                                    <Columns>

                                                        <dx:BootstrapGridViewDataColumn Caption="Clave" FieldName="Clave" VisibleIndex="1" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" SortIndex="0" SortOrder="None" Width="20%" />
                                                        <dx:BootstrapGridViewDataColumn Caption="Descripción" FieldName="Descripcion" VisibleIndex="2" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" Width="40%" />
                                                        <dx:BootstrapGridViewDataColumn Caption="Valor" FieldName="Valor" VisibleIndex="3" CssClasses-DataCell="hiddenRow" HeaderBadge-CssClass="hiddenRow" Visible="false" />

                                                        <dx:BootstrapGridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="4" HorizontalAlign="Center" Width="20%">
                                                            <DataItemTemplate>
                                                                <div>
                                                                    <asp:UpdatePanel ID="upaParamAcciones" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>

                                                                            <dx:BootstrapButton Text="Actualizar" ID="BootstrapButton2" runat="server" CommandArgument='<%# Eval("IdParametro") %>' CommandName="Actualiza" AutoPostBack="true" 
                                                                                ToolTip="Actualiza" SettingsBootstrap-RenderOption="Primary"></dx:BootstrapButton>

                                                                            <dx:bootstrapbutton ID="BootstrapButton3" runat="server" autopostback="true" CommandArgument='<%# Eval("IdParametro") %>' CommandName="Eliminar" 
                                                                                SettingsBootstrap-RenderOption="Danger" Text="Eliminar"></dx:bootstrapbutton>

                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </div>
                                                            </DataItemTemplate>
                                                            <CssClasses HeaderCell="spa" />
                                                        </dx:BootstrapGridViewDataColumn>

                                                        <dx:BootstrapGridViewDataColumn FieldName="IdParametro" VisibleIndex="5" CssClasses-DataCell="hiddenRow" HeaderBadge-CssClass="hiddenRow" Visible="false" />
                                                    </Columns>
                                                </dx:BootstrapGridView>



                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                          </div>
                        </fieldset>
                
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlConfiguracionParametros" runat="server" Visible="true">
                <div class="row">
                    <div class="col-md-12">
                        <br />
                        <fieldset class="Personal">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Parametros Generales</span>
                            </legend>
                            <div class="row">

                                <div class="col-md-12">

                                    <asp:UpdatePanel ID="upaParametros" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>

                                            <div class="table-responsive" style="height: auto;">

                                                 <dx:BootstrapGridView ID="gvParametros" runat="server" KeyFieldName="IdParametro" OnRowCommand="gvParametros_RowCommand1">
                                                    <SettingsSearchPanel Visible="true" ShowApplyButton="true" />
                                                    <Settings ShowGroupPanel="True" ShowFilterRowMenu="true" />
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                                    <SettingsBehavior AllowSort="true" />
                                            
                                                    <Columns>

                                                        <dx:BootstrapGridViewDataColumn Caption="Clave" FieldName="Clave" VisibleIndex="1" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" SortIndex="0" SortOrder="None" Width="20%" />
                                                        <dx:BootstrapGridViewDataColumn Caption="Descripción" FieldName="Descripcion" VisibleIndex="2" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" Width="50%" />
                                                        
                                                        <dx:BootstrapGridViewDataColumn Caption="Valor" FieldName="DesValor" VisibleIndex="3" HorizontalAlign="Right" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" Width="20%" />
                                                        <dx:BootstrapGridViewDataColumn FieldName="Valor" VisibleIndex="4" CssClasses-DataCell="hiddenRow" HeaderBadge-CssClass="hiddenRow" Visible="false" />
                                                        <dx:BootstrapGridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="5" HorizontalAlign="Center" Width="10%">
                                                            <DataItemTemplate>
                                                                <div>
                                                                    <dx:BootstrapButton Text="Actualizar" ID="btnActualiza" runat="server" CommandArgument='<%# Eval("IdParametro") %>' CommandName="Actualiza" AutoPostBack="true" 
                                                                        ToolTip="Actualiza" SettingsBootstrap-RenderOption="Primary"></dx:BootstrapButton>
                                                                </div>
                                                            </DataItemTemplate>
                                                            <CssClasses HeaderCell="spa" />
                                                        </dx:BootstrapGridViewDataColumn>

                                                        <dx:BootstrapGridViewDataColumn FieldName="IdParametro" VisibleIndex="6" CssClasses-DataCell="hiddenRow" HeaderBadge-CssClass="hiddenRow" Visible="false" />
                                                    </Columns>
                                                </dx:BootstrapGridView>



                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                          </div>
                        </fieldset>
                
                    </div>
                </div>
            </asp:Panel>

            <%--MODAL PARA MENSAJES--%>
            <dx:bootstrappopupcontrol id="ppAlert" runat="server" clientinstancename="ppAlert" closeanimationtype="Fade" popupanimationtype="Fade"
                popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
                closeaction="CloseButton" closeonescape="true" allowresize="true"
                headertext="Listo" allowdragging="true" showclosebutton="true" width="300" height="200">
                <clientsideevents />
                <contentcollection>
                    <dx:contentcontrol>
                        <table>
                            <tr>
                                <td>
                                    <dx:aspximage id="ASPxImage2" runat="server" showloadingimage="true" imageurl="~/img/iconos/Information2.ico"></dx:aspximage>
                                    <dx:aspxtextbox id="tbLogin" readonly="true" border-borderstyle="None" height="1px" runat="server" width="1px" clientinstancename="tbLogin"></dx:aspxtextbox>
                                </td>
                                <td>
                                    <dx:aspxlabel id="lbl" runat="server" clientinstancename="lbl" text="ASPxLabel"></dx:aspxlabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:bootstrapbutton id="btOK" runat="server" text="OK" width="80px" settingsbootstrap-renderoption="Primary" autopostback="false" style="float: left; margin-right: 8px" tabindex="0">
                                        <clientsideevents click="function(s, e) {ppAlert.Hide(); }" />
                                    </dx:bootstrapbutton>
                                </td>
                            </tr>
                        </table>
                    </dx:contentcontrol>
                </contentcollection>
            </dx:bootstrappopupcontrol>

           <%--MODAL PARA MENSAJES CONFIRMACION--%>
            <dx:bootstrappopupcontrol id="ppAlertConfirm" runat="server" clientinstancename="ppAlertConfirm" closeanimationtype="Fade" popupanimationtype="Fade"
                popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
                closeaction="CloseButton" closeonescape="true" allowresize="true"
                headertext="Confirmación" allowdragging="true" showclosebutton="true" width="300" height="200">
                <clientsideevents />
                <contentcollection>
                    <dx:contentcontrol>
                        <table style="width:100%; margin:0 auto 0 auto;">
                            <tr>
                                <td>
                                    <dx:aspximage id="ASPxImage1" runat="server" showloadingimage="true" imageurl="~/img/iconos/Information2.ico"></dx:aspximage>
                                    <dx:aspxtextbox id="Aspxtextbox1" readonly="true" border-borderstyle="None" height="1px" runat="server" width="1px" clientinstancename="tbLogin"></dx:aspxtextbox>
                                </td>
                                <td>
                                    <dx:aspxlabel id="Aspxlabel1" runat="server" clientinstancename="lbl" text="¿Desea eliminar el registro?"></dx:aspxlabel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right" style="text-align:right;"><br />
                                    <dx:bootstrapbutton id="btnCancel" runat="server" text="Cancelar" width="80px" settingsbootstrap-renderoption="Warning" autopostback="false">
                                        <clientsideevents click="function(s, e) {ppAlertConfirm.Hide(); }" />
                                    </dx:bootstrapbutton>
                                    <dx:bootstrapbutton id="btnAccept" runat="server" text="Aceptar" settingsbootstrap-renderoption="Success" autopostback="false" onclick="btnAccept_Click" />
                                </td>
                            </tr>
                        </table>
                    </dx:contentcontrol>
                </contentcollection>
            </dx:bootstrappopupcontrol>
            

            <!--Modal Conceptos-->
            <div class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" id="myModalConceptos" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">

                        

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblTitulo" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body">
                    
                            <%--<div class="row" style="padding:5px 2px 5px 2px;">
                                <div class="col-lg-6" align="right"><asp:Label ID="lblConcepto" runat="server" Text="Concepto: " style="font-weight:200; color:#555;"></asp:Label></div>
                                <div class="col-lg-6" align="left"><asp:Label ID="readConcepto" runat="server" style="font-weight:200; color:#555;"></asp:Label></div>
                            </div>--%>

                            <div class="row">
                                <div class="col-md-1">&nbsp;&nbsp;&nbsp;</div>
                                <div class="col-md-3" align="right" style="vertical-align:middle; padding:5px 2px 5px 2px;">
                                    <span>Concepto:</span>
                                </div>
                                <div class="col-md-5" align="left" style="padding:5px 2px 5px 2px;">
                                    <asp:TextBox ID="txtConcepto" runat="server" Width="100%" placeholder="Ingresa Concepto" style="text-align:left;" CssClass="inputText" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                                <div class="col-md-3">&nbsp;&nbsp;&nbsp;</div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">&nbsp;&nbsp;&nbsp;</div>
                                <div class="col-md-3" align="right" style="vertical-align:middle; padding:5px 2px 5px 2px;">
                                    <span>Horario Inicial:</span>
                                </div>
                                <div class="col-md-3" align="left" style="padding:5px 2px 5px 2px;">
                                    <asp:TextBox ID="txtHorarioInicial" runat="server" Width="100%" MaxLength="5" placeholder="00:00" style="text-align:center;" CssClass="inputText" 
                                        OnTextChanged="txtHorarioInicial_TextChanged" AutoPostBack="true"></asp:TextBox>
                                  <%--<cc1:MaskedEditExtender ID="mskHorarioIni" runat="server" AcceptAMPM="false" MaskType="Time" Mask="99:99" ErrorTooltipEnabled="true"
                                        InputDirection="RightToLeft" CultureName="es-ES" TargetControlID="txtHorarioInicial" MessageValidatorTip="true"></cc1:MaskedEditExtender>
                                    <cc1:MaskedEditValidator ID="mskValidHorarioIni" runat="server" ToolTip="Error de formato en hora" ErrorMessage="*" ControlExtender="mskHorarioIni"
                                        ControlToValidate="txtHorarioInicial" InvalidValueMessage="Registre Hora" TooltipMessage="Ejemplo: HH:MM"></cc1:MaskedEditValidator>--%>

                                </div>
                                <div class="col-md-3">&nbsp;&nbsp;&nbsp;</div>
                            </div>

                            <div id="divHorarioIni" runat="server" class="row" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="rqHoraIni" runat="server" CssClass="validInput"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">&nbsp;&nbsp;&nbsp;</div>
                                <div class="col-md-3" align="right" style="vertical-align:middle; padding:5px 2px 5px 2px;">
                                    <span>Horario Final:</span>
                                </div>
                                <div class="col-md-3" align="left" style="padding:5px 2px 5px 2px;">
                                    <asp:TextBox ID="txtHorarioFinal" runat="server" Width="100%" MaxLength="5" placeholder="00:00" style="text-align:center;" CssClass="inputText" 
                                        OnTextChanged="txtHorarioFinal_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-3">&nbsp;&nbsp;&nbsp;</div>
                            </div>

                            <div id="divHorarioFin" runat="server" class="row" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="rqHoraFin" runat="server" CssClass="validInput"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">&nbsp;&nbsp;&nbsp;</div>
                                <div class="col-md-3" align="right" style="vertical-align:middle; padding:5px 2px 5px 2px;">
                                    <span>Monto MXN:</span>
                                </div>
                                <div class="col-md-3" align="left" style="padding:5px 2px 5px 2px;">
                                    <asp:TextBox ID="txtMontoMXN" runat="server" Width="100%" placeholder="MXN" style="text-align:right;" CssClass="inputText" 
                                        OnTextChanged="txtMontoMXN_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-3">&nbsp;&nbsp;&nbsp;</div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">&nbsp;&nbsp;&nbsp;</div>
                                <div class="col-md-3" align="right" style="vertical-align:middle; padding:5px 2px 5px 2px;">
                                    <span>Monto USD:</span>
                                </div>
                                <div class="col-md-3" align="left" style="padding:5px 2px 5px 2px;">
                                    <asp:TextBox ID="txtMontoUSD" runat="server" Width="100%" placeholder="USD" style="text-align:right;" CssClass="inputText" 
                                        OnTextChanged="txtMontoUSD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-3">&nbsp;&nbsp;&nbsp;</div>
                            </div>
                            <div class="row">
                                <asp:HiddenField ID="hdnIdConcepto" runat="server"></asp:HiddenField>
                            </div>

                            <div id="divValidGral" runat="server" class="row" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="rqGral" runat="server" CssClass="validInput"></asp:Label>
                                </div>
                            </div>
                            <div id="divValidMontosMXN" runat="server" class="row" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="rqMontosMXN" runat="server" CssClass="validInput"></asp:Label>
                                </div>
                            </div>
                            <div id="divValidMontosUSD" runat="server" class="row" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="rqMontosUSD" runat="server" CssClass="validInput"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">

                            <button type="button" class="btn btn-warning" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnGuardarConcepto" runat="server" CssClass="btn btn-success" Text="Actualizar" OnClick="btnGuardarConcepto_Click"  OnClientClick="return alert(CompararHoras());" />

                        </div>
                            
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <!--Modal Parametros Generales-->
            <div class="modal fade" id="myModalParametros">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">
                                <asp:Label ID="TituloParam" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body">
                    
                            <div class="row" style="padding:5px 2px 5px 2px;">
                                <div class="col-lg-6" align="right"><asp:Label ID="lblDesParam" runat="server" Text="Descripción Parametro: " style="font-weight:200; color:#555;"></asp:Label></div>
                                <div class="col-lg-6" align="left"><asp:Label ID="readDesParametro" runat="server" style="font-weight:200; color:#555;"></asp:Label></div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">&nbsp;&nbsp;&nbsp;</div>
                                <div class="col-md-3" align="right" style="vertical-align:middle; padding:5px 2px 5px 2px;">
                                    <span>Valor:</span>
                                </div>
                                <div class="col-md-3" align="left" style="padding:5px 2px 5px 2px;">
                                    <asp:TextBox ID="txtValor" runat="server" Width="100%" style="text-align:right;" CssClass="inputText"></asp:TextBox>
                                </div>
                                <div class="col-md-3">&nbsp;&nbsp;&nbsp;</div>
                            </div>

                            <div class="row">
                                <asp:HiddenField ID="hdnIdParametro" runat="server"></asp:HiddenField>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-warning" data-dismiss="modal">
                                Cancelar</button>
                            <asp:Button ID="btnGuardarParametro" runat="server" CssClass="btn btn-success" Text="Actualizar" OnClick="btnGuardarParametro_Click" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <!--Modal Parametros Adicionales-->
            <div class="modal fade" id="myModalParametrosAd">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblTituloAdicionales" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body">
                    
                             <div class="row">
                                <div class="col-md-2">&nbsp;&nbsp;&nbsp;</div>
                                <div class="col-md-3" align="right" style="vertical-align:middle; padding:5px 2px 5px 2px;">
                                    <span>Clave:</span>
                                </div>
                                <div class="col-md-4" align="left" style="padding:5px 2px 5px 2px;">
                                    <asp:TextBox ID="txtClave" runat="server" Width="100%" style="text-align:center;" CssClass="inputText"></asp:TextBox>
                                </div>
                                <div class="col-md-3">&nbsp;&nbsp;&nbsp;</div>
                            </div>
                             <div class="row">
                                <div class="col-md-2">&nbsp;&nbsp;&nbsp;</div>
                                <div class="col-md-3" align="right" style="vertical-align:middle; padding:5px 2px 5px 2px;">
                                    <span>Descripción:</span>
                                </div>
                                <div class="col-md-6" align="left" style="padding:5px 2px 5px 2px;">
                                    <asp:TextBox ID="txtDescripcionParaAd" runat="server" Width="100%" style="text-align:left;" CssClass="inputText" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                </div>
                                <div class="col-md-1">&nbsp;&nbsp;&nbsp;</div>
                            </div>
                            <div class="row" style="display:none;">
                                <div class="col-md-2">&nbsp;&nbsp;&nbsp;</div>
                                <div class="col-md-3" align="right" style="vertical-align:middle; padding:5px 2px 5px 2px;">
                                    <span>Valor:</span>
                                </div>
                                <div class="col-md-6" align="left" style="padding:5px 2px 5px 2px;">
                                    <asp:TextBox ID="txtValorParaAd" runat="server" Width="100%" style="text-align:right;" CssClass="inputText" data-error-msg="Ingrese el Valor"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegexDecimal" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$" ErrorMessage="Ingrese un valor decimal ó entero" 
                                        ControlToValidate="txtValorParaAd" ForeColor="Red" Font-Size="9pt" Enabled="false" />
                                </div>
                                <div class="col-md-1">&nbsp;&nbsp;&nbsp;</div>
                            </div>

                            <div class="row">
                                <asp:HiddenField ID="hdnIdParaAd" runat="server"></asp:HiddenField>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-warning" data-dismiss="modal">
                                Cancelar</button>
                            <asp:Button ID="btnGuardarParaAd" runat="server" CssClass="btn btn-success" Text="" OnClick="btnGuardarParaAd_Click" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <asp:Panel ID="pnlCuentasPilotos" runat="server" Visible="true">
                <div class="row">
                    <div class="col-md-12">
                        <br />
                        <!-- Cargar Cuentas de Pilotos -->
                        <fieldset class="Personal">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Cargar Cuentas de Pilotos</span>
                            </legend>
                            <div class="row">
                                <div class="col-md-3" style="text-align:right;">&nbsp;&nbsp;&nbsp;</div>
                                <div class="col-md-3" style="text-align:right;">
                                    <%--<dx:BootstrapUploadControl ID="uplCargaDocumento1" runat="server">
                                        <UploadButton Text="Cargar" />
                                    </dx:BootstrapUploadControl> --%>

                                    <asp:FileUpload ID="fluArchivo" runat="server" CssClass="btn btn-primary" style="width:400px !important;" EnableViewState="true" />

                                </div>
                                <div class="col-md-3" style="text-align:center;">
                                    <%--<dx:ASPxButton ID="lblCargarArchivo" Text="Cargar Archivo" Theme="Office2010Black" runat="server"></dx:ASPxButton>--%> 
                                    <dx:BootstrapButton ID="btnCargarArchivo" runat="server" Text="Cargar Archivo" Width="50%" OnClick="btnCargarArchivo_Click">
                                        <SettingsBootstrap RenderOption="Success" />
                                    </dx:BootstrapButton>
                                </div>
                                <div class="col-md-3" style="text-align:right;"></div>
                            </div>
                            <br />
                            <div class="col-md-12" style="height:50px; text-align:center;">
                                <asp:Label ID="lblRegModificar" runat="server" Text="Registros a modificar" CssClass="card-title" Font-Bold="true" Visible="false"></asp:Label>
                            </div>

                            <div style="height:auto; text-align:center; padding-bottom: 5px;">
                                <asp:UpdatePanel ID="upaCambios" runat="server">
                                    <ContentTemplate>
                                        <%--Tabla de cambios encontrados--%>

                                        <%--<dx:BootstrapGridView ID="gvCambios2" runat="server" Style="width:99%;">
                                            <SettingsSearchPanel Visible="false" ShowApplyButton="false" />
                                            <Settings ShowGroupPanel="false" ShowFilterRowMenu="false" />
                                            <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                            <SettingsPager PageSize="20"></SettingsPager>
                                            <SettingsBehavior AllowSort="false" />
                                            <Columns>
                                                <dx:BootstrapGridViewDataColumn Caption="Titular" FieldName="Titular" VisibleIndex="1" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" SortIndex="0" SortOrder="None" Width="30%" />
                                                <dx:BootstrapGridViewDataColumn Caption="Cuenta" FieldName="Cuenta" VisibleIndex="2" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" Width="20%" />
                                                <dx:BootstrapGridViewDataColumn Caption="Tarjeta" FieldName="Tarjeta" VisibleIndex="3" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" Width="20%" />
                                                <dx:BootstrapGridViewDateColumn Caption="Estado al Corte" FieldName="EstadoCorte" VisibleIndex="4" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" Width="10%" />
                                                <dx:BootstrapGridViewDataColumn Caption="Cuarta Linea" FieldName="CuartaLinea" VisibleIndex="5" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" Width="10%" />
                                                <dx:BootstrapGridViewDataColumn Caption="Clave Piloto" FieldName="CvePiloto" VisibleIndex="6" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" Width="10%" />
                                            </Columns>
                                        </dx:BootstrapGridView>--%>


                                        <asp:GridView ID="gvCambios" runat="server" style="border: 1px solid #ffb400;" class="table table-bordered table-condensed"
                                            AutoGenerateColumns="false" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="Titular" HeaderText="Titular" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%" />
                                                <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" />
                                                <asp:BoundField DataField="Tarjeta" HeaderText="Tarjeta" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" />
                                                <asp:BoundField DataField="EstadoCorte" HeaderText="Estado al Corte" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" />
                                                <asp:BoundField DataField="CuartaLinea" HeaderText="Cuarta Línea" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" />
                                                <asp:BoundField DataField="CveCuenta" HeaderText="Clave Piloto" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" />
                                            </Columns>
                                            <HeaderStyle CssClass="gvHeader" />
                                            <RowStyle CssClass="gvRows" />
                                            <FooterStyle CssClass="gvFooter" />
                                            <PagerStyle CssClass="gvFooter" />
                                            <EmptyDataTemplate>
                                                Sin Cambios.
                                            </EmptyDataTemplate>
                                        </asp:GridView>


                                    </ContentTemplate>
                                </asp:UpdatePanel>  
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="form-group col-md">
                                    <div class="alert alert-danger" role="alert" id="msgError" runat="server"  style="width:95%; height:auto;" visible="false">
                                        <strong>Error!&nbsp;</strong><asp:Label ID="lblError" runat="server" Text="" Font-Size="9pt" ForeColor="White"></asp:Label>
                                    </div>
                                    <div class="col-md-12" style="height:50px; text-align:center;">
                                        <asp:Label ID="Label4" runat="server" Text="Información a cargar" CssClass="card-title" Font-Bold="true" Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-md-12" style="text-align:left;">
                                        <asp:Label ID="lblNota" runat="server" Text="Nota: Información del archivo a cargar con los cambios marcados en color amarillo." CssClass="card-title" Font-Bold="true" Visible="false"></asp:Label>
                                    </div>
                                    <div class="alert alert-warning" role="alert" id="msgWarning" runat="server" style="width:100%; height:auto;" visible="false">
                                        <strong>
                                            <img src="../../img/iconos/warning_icon.png" width="25" height="25" />
                                            &nbsp; Cambios Encontrados!&nbsp;</strong><asp:Label ID="lblWarning" runat="server" Text="" Font-Size="9pt"></asp:Label>
                                    </div>

                                    
                                        <asp:UpdatePanel ID="upaCarga" runat="server">
                                            <ContentTemplate>

                                                <div style="text-align:center !important; margin-top: -14px !important;">

                                                    <asp:GridView ID="gvCargaCuentas" runat="server" style="border:2px solid #f1eded;" class="table table-bordered table-condensed"
                                                        AutoGenerateColumns="false" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="Titular" HeaderText="Titular" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" />
                                                            <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="Tarjeta" HeaderText="Tarjeta" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="EstadoCorte" HeaderText="Estado al Corte" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="CuartaLinea" HeaderText="Cuarta Línea" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" />

                                                            <asp:TemplateField HeaderText="Clave Piloto" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCvePiloto" runat="server" CssClass="inputText" Placeholder="Clave Piloto" Text='<%#Eval("CvePiloto") %>' style="text-align:center; text-transform: uppercase;"
                                                                        onkeypress="return alpha(event)" MaxLength="4"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <HeaderStyle CssClass="gvHeader" />
                                                        <RowStyle CssClass="gvRows" />
                                                        <FooterStyle CssClass="gvFooter" />
                                                        <PagerStyle CssClass="gvFooter" />
                                                        <EmptyDataTemplate>
                                                            Sin Registros.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>

                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                </div>
                            </div>

                            <div class="row" align="center" style="border:0px solid #000000; padding-top:5px; padding-bottom:5px;">
                                <div class="col-lg-12" style="margin:0 auto 0 auto;">
                                    <asp:Button ID="btnGuardarCuentas" runat="server" Text="Guardar Cuentas" CssClass="btn btn-success" OnClick="btnGuardarCuentas_Click" Visible="false" />
                                </div>
                            </div>

                        </fieldset>
                    </div>
                </div>
            
            </asp:Panel>


        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnGuardarConcepto" />--%>
            <asp:PostBackTrigger ControlID="btnGuardarParametro" />
            <asp:PostBackTrigger ControlID="btnGuardarParaAd" />
            <asp:PostBackTrigger ControlID="btnCargarArchivo" />

            <asp:AsyncPostBackTrigger ControlID="txtHorarioInicial" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtHorarioFinal" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtMontoMXN" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtMontoUSD" EventName="TextChanged" />

        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function CompararHoras() {

            sHora1 = document.getElementById('txtHorarioInicial');
            sHora2 = document.getElementById('txtHorarioFinal');

            var arHora1 = sHora1.split(":");
            var arHora2 = sHora2.split(":");

            // Obtener horas y minutos (hora 1)
            var hh1 = parseInt(arHora1[0], 10);
            var mm1 = parseInt(arHora1[1], 10);

            // Obtener horas y minutos (hora 2)
            var hh2 = parseInt(arHora2[0], 10);
            var mm2 = parseInt(arHora2[1], 10);

            // Comparar
            if (hh1 < hh2 || (hh1 == hh2 && mm1 < mm2))
                return "HorarioInicial ES MENOR HorarioFinal";
            else if (hh1 > hh2 || (hh1 == hh2 && mm1 > mm2))
                return "HorarioInicial ES MAYOR HorarioFinal";
            else
                return "HorarioInicial IGUAL HorarioFinal";
        }
    </script>

    

</asp:Content>
