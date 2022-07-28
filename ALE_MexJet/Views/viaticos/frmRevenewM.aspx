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
                            
                            <div class="table-responsive">
                              <table class="table table-bordered table-striped table-hover" style="background-color:#ffffff; border:1px solid #00000030;">
                                  <tr>
                                      <th style="width:20%;">
                                          Concepto
                                      </th>
                                      <th style="width:20%;">
                                          Horario Inicial
                                      </th>
                                      <th style="width:20%;">
                                          Horario Final
                                      </th>
                                      <th style="width:20%;">
                                          MXN
                                      </th>
                                      <th style="width:20%;">
                                          USD
                                      </th>
                                  </tr>
                                  <tr>
                                      <td>
                                          <dx:ASPxLabel ID="lblDesayuno" Text="Desayuno" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="txtDesayunoInicial" Theme="Office2010Black" runat="server" Style="width:70%;margin:0 auto;" CssClass="tdcentro"></dx:ASPxTextBox>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="txtDesayunoFinal" Theme="Office2010Black" runat="server" Style="width:70%;margin:0 auto;" CssClass="tdcentro"></dx:ASPxTextBox>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="txtDesayunoMXN" Theme="Office2010Black" runat="server" Style="width:70%;margin:0 auto;" CssClass="tdderecha"></dx:ASPxTextBox>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="txtDesayunoUSD" Theme="Office2010Black" runat="server" Style="width:70%;margin:0 auto;" CssClass="tdderecha"></dx:ASPxTextBox>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <dx:ASPxLabel ID="lblComida" Text="Comida" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="txtComidaInicial" Theme="Office2010Black" runat="server" Style="width:70%;margin:0 auto;" CssClass="tdcentro"></dx:ASPxTextBox>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="txtComidaFinal" Theme="Office2010Black" runat="server" Style="width:70%;margin:0 auto;" CssClass="tdcentro"></dx:ASPxTextBox>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="txtComidaMXN" Theme="Office2010Black" runat="server" Style="width:70%;margin:0 auto;" CssClass="tdderecha"></dx:ASPxTextBox>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="txtComidaUSD" Theme="Office2010Black" runat="server" Style="width:70%;margin:0 auto;" CssClass="tdderecha"></dx:ASPxTextBox>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <dx:ASPxLabel ID="lblCena" Text="Cena" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="txtCenaInicial" Theme="Office2010Black" runat="server" Style="width:70%;margin:0 auto;" CssClass="tdcentro"></dx:ASPxTextBox>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="txtCenaFinal" Theme="Office2010Black" runat="server" Style="width:70%;margin:0 auto;" CssClass="tdcentro"></dx:ASPxTextBox>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="txtCenaMXN" Theme="Office2010Black" runat="server" Style="width:70%;margin:0 auto;" CssClass="tdderecha"></dx:ASPxTextBox>
                                      </td>
                                      <td>
                                          <dx:ASPxTextBox ID="txtCenaUSD" Theme="Office2010Black" runat="server" Style="width:70%;margin:0 auto;" CssClass="tdderecha"></dx:ASPxTextBox>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td colspan="2">
                                          &nbsp;
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="lblTota" Text="TOTAL" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="lblTotalMXN" Text="$00000.00" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                      <td>
                                          <dx:ASPxLabel ID="lblTotalUSD" Text="$00000.00" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                      </td>
                                  </tr>
                              </table>
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
                            <dx:ASPxLabel ID="lblTerritorioNal" Text="Territorio Nacional" Theme="Office2010Black" runat="server"></dx:ASPxLabel><br />
                            <div class="row">
                                <div class="col-md-4" style="text-align:right;">
                                    <dx:ASPxLabel ID="lblMontoMaximo" Text="Monto Máximo" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                                </div>
                                <div class="col-md-4">
                                    <dx:ASPxTextBox ID="txtMontoMaximo" Theme="Office2010Black" runat="server" Style="width:100%;margin:0 auto;" CssClass="tdcentro"></dx:ASPxTextBox>
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
                                    <dx:ASPxTextBox ID="txtMontoMaximoUSA" Theme="Office2010Black" runat="server" Style="width:100%;margin:0 auto;" CssClass="tdcentro"></dx:ASPxTextBox>
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
                                    <dx:ASPxTextBox ID="txtCanadaCaribe" Theme="Office2010Black" runat="server" Style="width:100%;margin:0 auto;" CssClass="tdcentro"></dx:ASPxTextBox>
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
                                          <dx:ASPxButton ID="btnEditar" Text="Editar" Theme="Office2010Black" runat="server"></dx:ASPxButton>  
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
                                          <dx:ASPxButton ID="ASPxButton1" Text="Editar" Theme="Office2010Black" runat="server"></dx:ASPxButton>  
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
                                          <dx:ASPxButton ID="ASPxButton2" Text="Editar" Theme="Office2010Black" runat="server"></dx:ASPxButton>  
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
                                          <dx:ASPxButton ID="ASPxButton3" Text="Editar" Theme="Office2010Black" runat="server"></dx:ASPxButton>  
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
                                          <dx:ASPxButton ID="ASPxButton4" Text="Editar" Theme="Office2010Black" runat="server"></dx:ASPxButton>  
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
                                          <dx:ASPxButton ID="ASPxButton5" Text="Editar" Theme="Office2010Black" runat="server"></dx:ASPxButton>  
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
                            <div class="col-md-5" style="text-align:right;">
                                <dx:ASPxLabel ID="lblSubirDocumento" Text="Seleccionar Archivo" Theme="Office2010Black" runat="server"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3" style="text-align:left;">
                                <dx:ASPxUploadControl ID="upDocumento" runat="server" Theme="Office2010Black" Style="width:100%;"></dx:ASPxUploadControl>
                            </div>
                            <div class="col-md-4" style="text-align:left;">
                                <dx:ASPxButton ID="lblCargarArchivo" Text="Cargar Archivo" Theme="Office2010Black" runat="server"></dx:ASPxButton> 
                            </div>
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
