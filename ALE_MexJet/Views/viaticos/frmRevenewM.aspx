<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmRevenewM.aspx.cs" Inherits="ALE_MexJet.Views.viaticos.frmRevenewM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Styles/Controls.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
  
    <style>
        .hiddenRow {
            visibility:hidden !important;
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
            font-size:9pt;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

                            <div style="max-height:400px; overflow-y:auto; text-align:center;">

                                <dx:BootstrapGridView ID="gvConceptos" runat="server" KeyFieldName="IdConcepto">
                                    <SettingsSearchPanel Visible="true" ShowApplyButton="true" />
                                    <Settings ShowGroupPanel="True" ShowFilterRowMenu="true" />
                                    <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                    <SettingsPager PageSize="20"></SettingsPager>
                                    <SettingsBehavior AllowSort="true" />
                                    
                                    <Columns>

                                        <dx:BootstrapGridViewDataColumn Caption="Concepto" FieldName="Concepto" VisibleIndex="1" HorizontalAlign="Center" CssClasses-HeaderCell="centerCell" CssClasses-DataCell="dataCell" SortIndex="0" SortOrder="Ascending" />
                                        <dx:BootstrapGridViewDataColumn Caption="Horario Inicial" Visible="true" VisibleIndex="2" HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <div>
                                                    <dx:BootstrapTextBox ID="txtHoraIni" runat="server" CssClasses-Control="timepicker inputStyle" Text="09:00"></dx:BootstrapTextBox>
                                                </div>
                                            </DataItemTemplate>
                                            <CssClasses HeaderCell="spa" />
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Caption="Horario Inicial" Visible="true" VisibleIndex="2" HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <div>
                                                    <dx:BootstrapTextBox ID="txtHoraIni" runat="server" CssClasses-Control="timepicker inputStyle" Text="09:00"></dx:BootstrapTextBox>
                                                </div>
                                            </DataItemTemplate>
                                            <CssClasses HeaderCell="spa" />
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Caption="Horario Final" Visible="true" VisibleIndex="3" HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <div>
                                                    <dx:BootstrapTextBox ID="txtHoraFin" runat="server" CssClasses-Control="timepicker inputStyle" Text="10:00"></dx:BootstrapTextBox>
                                                </div>
                                            </DataItemTemplate>
                                            <CssClasses HeaderCell="spa" />
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Caption="MXN" Visible="true" VisibleIndex="4" HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <div>
                                                    <dx:BootstrapTextBox ID="txtMontoMXN" runat="server" CssClasses-Control="timepicker inputStyle" Text=""></dx:BootstrapTextBox>
                                                </div>
                                            </DataItemTemplate>
                                            <CssClasses HeaderCell="spa" />
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Caption="USD" Visible="true" VisibleIndex="4" HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <div>
                                                    <dx:BootstrapTextBox ID="txtMontoUSD" runat="server" CssClasses-Control="timepicker inputStyle" Text=""></dx:BootstrapTextBox>
                                                </div>
                                            </DataItemTemplate>
                                            <CssClasses HeaderCell="spa" />
                                        </dx:BootstrapGridViewDataColumn>
                                    </Columns>
                                </dx:BootstrapGridView>

                            </div>


                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel1" runat="server" Visible="true">
        <div class="row">
            <div class="col-md-12">
                <br />
                <!-- Gastos de Hotel -->
                <fieldset class="Personal">
                    <legend>
                        <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Gastos de Hotel</span>
                    </legend>
                    <div class="row">
                        <div class="col-md-6">
                            <dx:ASPxLabel ID="lblTerritorioNal" Text="Territorio Nacional" Theme="iOS" runat="server"></dx:ASPxLabel><br />
                            <div class="row">
                                <div class="col-md-4" style="text-align:right;">
                                    <dx:ASPxLabel ID="lblMontoMaximo" Text="Monto Máximo" Theme="iOS" runat="server"></dx:ASPxLabel>
                                </div>
                                <div class="col-md-4">
                                    <dx:BootstrapTextBox ID="txtMontoMaximo" runat="server" CssClasses-Control="tdcentro" Width="100%" Text=""></dx:BootstrapTextBox>
                                </div>
                                <div class="col-md-4" style="text-align:left;">
                                    MXN
                                </div>
                            </div><br />
                            <dx:ASPxLabel ID="lblTerritotioExtranjero" Text="Territorio Extranjero" Theme="Office2010Black" runat="server"></dx:ASPxLabel><br />
                            <div class="row">
                                <div class="col-md-4" style="text-align:right;">
                                    <dx:ASPxLabel ID="lblUSA" Text="USA" Theme="Office2010Black" runat="server"></dx:ASPxLabel>&nbsp;&nbsp;<dx:ASPxLabel ID="lblMontoMaximoUSA" Text="Monto Máximo" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                </div>
                                <div class="col-md-4">
                                    <dx:BootstrapTextBox ID="txtMontoMaximoUSA" runat="server" CssClasses-Control="tdcentro" Width="100%" Text=""></dx:BootstrapTextBox>
                                </div>
                                <div class="col-md-4" style="text-align:left;">
                                    USD
                                </div>
                            </div><br />
                            <div class="row">
                                <div class="col-md-4" style="text-align:right;">
                                    <dx:ASPxLabel ID="lblCanadaCaribe" Text="Canadá y Caribe" Theme="Office2010Black" runat="server"></dx:ASPxLabel>&nbsp;&nbsp;<dx:ASPxLabel ID="ASPxLabel17" Text="Monto Máximo" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                </div>
                                <div class="col-md-4">
                                    <dx:BootstrapTextBox ID="txtCanadaCaribe" runat="server" CssClasses-Control="tdcentro" Width="100%" Text=""></dx:BootstrapTextBox>
                                </div>
                                <div class="col-md-4" style="text-align:left;">
                                    USD
                                </div>
                            </div>

                        </div>
                        <div class="col-md-6">
                            
                            <dx:ASPxLabel ID="lblParametrosGenerales" Text="Parametros Generales" Theme="Office2010Black" runat="server"></dx:ASPxLabel><br />

                            <div class="table-responsive" style="height: 150px;">
                                
                              <table class="table table-bordered table-striped table-hover" style="background-color:#ffffff; border:1px solid #00000030;">
                                  <tr>
                                      <th style="width:20%;">
                                          &nbsp;
                                      </th>
                                      <th style="width:20%;">
                                          <dx:ASPxLabel ID="lblFechaParametrosG" Text="Fecha" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </th>
                                      <th style="width:40%;">
                                          <dx:ASPxLabel ID="lblDescripcionParametrosG" Text="Descripción" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </th>
                                      <th style="width:20%;">
                                          <dx:ASPxLabel ID="lblVaorParametrosG" Text="Valor" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </th>
                                  </tr>
                                  <tr>
                                      <td>
                                          <dx:BootstrapButton ID="btnEditar" runat="server" Text="Editar" Width="100%">
                                               <SettingsBootstrap RenderOption="Primary" />
                                           </dx:BootstrapButton>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="lblFecha" Text="1001" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="lblDescripcion" Text="Valor" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="lblValor" Text="0000" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <dx:BootstrapButton ID="btnEditar2" runat="server" Text="Editar" Width="100%">
                                               <SettingsBootstrap RenderOption="Primary" />
                                           </dx:BootstrapButton>  
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel1" Text="1001" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel2" Text="Valor" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel3" Text="0000" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <dx:BootstrapButton ID="btnEditar3" runat="server" Text="Editar" Width="100%">
                                               <SettingsBootstrap RenderOption="Primary" />
                                           </dx:BootstrapButton> 
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel4" Text="1001" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel5" Text="Valor" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel6" Text="0000" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <dx:BootstrapButton ID="btnEditar4" runat="server" Text="Editar" Width="100%">
                                               <SettingsBootstrap RenderOption="Primary" />
                                           </dx:BootstrapButton> 
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel7" Text="1001" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel8" Text="Valor" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel9" Text="0000" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <dx:BootstrapButton ID="btnEditar5" runat="server" Text="Editar" Width="100%">
                                               <SettingsBootstrap RenderOption="Primary" />
                                           </dx:BootstrapButton>  
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel10" Text="1001" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel11" Text="Valor" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel12" Text="0000" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <tr>
                                      <td>
                                          <dx:BootstrapButton ID="btnEditar6" runat="server" Text="Editar" Width="100%">
                                               <SettingsBootstrap RenderOption="Primary" />
                                           </dx:BootstrapButton>  
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel13" Text="1001" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel14" Text="Valor" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="ASPxLabel15" Text="0000" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                  </tr>
                                  </tr>
                              </table>

                        </div>
                    </div>
                  </div>
                </fieldset>
                
            </div>
        </div>
        <asp:Panel ID="Panel2" runat="server" Visible="true">
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
                                <dx:BootstrapUploadControl ID="uplCargaDocumento" runat="server">
                                    <UploadButton Text="Cargar" />
                                </dx:BootstrapUploadControl> 
                            </div>
                            <div class="col-md-3" style="text-align:left;">
                                <%--<dx:ASPxButton ID="lblCargarArchivo" Text="Cargar Archivo" Theme="Office2010Black" runat="server"></dx:ASPxButton>--%> 
                                <dx:BootstrapButton ID="btnCargarArchivo" runat="server" Text="Cargar Archivo" Width="100%">
                                    <SettingsBootstrap RenderOption="Success" />
                                </dx:BootstrapButton>
                            </div>
                            <div class="col-md-3" style="text-align:right;"></div>
                        </div>
                        <br />
                        <dx:ASPxLabel ID="lblCuentaPilotosRegistradas" Text="Cuentas de Pilotos Registradas" Theme="Office2010Black" runat="server"></dx:ASPxLabel><br />

                        <dx:BootstrapGridView ID="BootstrapGridView1" runat="server" Style="width:99%;">
                            <SettingsSearchPanel Visible="true" ShowApplyButton="true" />
                            <Settings ShowGroupPanel="True" ShowFilterRowMenu="true" />
                            <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                            <SettingsPager PageSize="20"></SettingsPager>
                            <SettingsBehavior AllowSort="true" />
                                <Columns>
                                    <%--<dx:BootstrapGridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true" ShowNewButtonInHeader="true" />--%>
                                    <dx:BootstrapGridViewDataColumn FieldName="Titular" />
                                    <dx:BootstrapGridViewDataColumn FieldName="Cuenta" />
                                    <dx:BootstrapGridViewDataColumn FieldName="Tarjeta" />
                                    <dx:BootstrapGridViewDateColumn FieldName="Estado del Corte" />
                                    <dx:BootstrapGridViewDataColumn FieldName="Cuarta Línea" />
                                    <dx:BootstrapGridViewDataColumn FieldName="Clave Piloto" />
                                </Columns>
                          </dx:BootstrapGridView>

                    </fieldset>
                </div>
            </div>
        </asp:Panel>

       


    </asp:Panel>


</asp:Content>
