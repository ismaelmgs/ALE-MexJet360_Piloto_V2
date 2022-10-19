<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmCalculoPagos.aspx.cs" Inherits="ALE_MexJet.Views.viaticos.frmCalculoPagos" 
    UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Styles/Controls.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <style>
        .container-main {
            min-height: 740px;
        }
        .hiddenRow {
            visibility: hidden !important;
        }

        .centerCell {
            text-align: center !important;
            color: #337ab7 !important;
        }

        .centerTxt {
            text-align: center !important;
        }

        .dirTB {
            text-align: center !important;
        }

        .dataCell {
            font-size: 9pt;
        }

        .form-control[disabled], .form-control[readonly], fieldset[disabled] .form-control {
            cursor: not-allowed !important;
            background-color: #eee !important;
            opacity: 1 !important;
            text-align: center !important;
        }

        .form-control {
            text-align: center !important;
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
            /*font-size:10pt !important;*/
            /*background-color:#A3C0E8;*/
            font-weight: bold !important;
            color: #337ab7 !important;
            text-align: center !important;
            font-size:11pt;
        }

        .tdderecha {
            text-align: right !important;
        }

        .tdcentro {
            text-align: center !important;
        }


        /*Card styles*/
        .card_vi {
            --bs-card-spacer-y: 1rem;
            --bs-card-spacer-x: 1rem;
            --bs-card-title-spacer-y: 0.5rem;
            --bs-card-border-width: 1px;
            --bs-card-border-color: var(--bs-border-color-translucent);
            --bs-card-border-radius: 0.375rem;
            --bs-card-box-shadow:;
            --bs-card-inner-border-radius: calc(0.375rem - 1px);
            --bs-card-cap-padding-y: 0.5rem;
            --bs-card-cap-padding-x: 1rem;
            --bs-card-cap-bg: rgba(0, 0, 0, 0.03);
            --bs-card-cap-color:;
            --bs-card-height:;
            --bs-card-color:;
            --bs-card-bg: #fff;
            --bs-card-img-overlay-padding: 1rem;
            --bs-card-group-margin: 0.75rem;
            position: relative;
            display: flex;
            flex-direction: column;
            min-width: 0;
            height: var(--bs-card-height);
            word-wrap: break-word;
            background-color: var(--bs-card-bg);
            background-clip: border-box;
            /*border: var(--bs-card-border-width) solid var(--bs-card-border-color);*/
            border-radius: var(--bs-card-border-radius);
            border: 1px solid #cccccc;
            border-style: dotted;
        }

            .card_vi > hr {
                margin-right: 0;
                margin-left: 0
            }

            .card_vi > .list-group {
                border-top: inherit;
                border-bottom: inherit
            }

                .card_vi > .list-group:first-child {
                    border-top-width: 0;
                    border-top-left-radius: var(--bs-card-inner-border-radius);
                    border-top-right-radius: var(--bs-card-inner-border-radius)
                }

                .card_vi > .list-group:last-child {
                    border-bottom-width: 0;
                    border-bottom-right-radius: var(--bs-card-inner-border-radius);
                    border-bottom-left-radius: var(--bs-card-inner-border-radius)
                }

                .card_vi > .card-header_vi + .list-group, .card_vi > .list-group + .card-footer {
                    border-top: 0
                }

        .card-body {
            flex: 1 1 auto;
            padding: var(--bs-card-spacer-y) var(--bs-card-spacer-x);
            color: var(--bs-card-color)
        }

        .card-title {
            margin-bottom: var(--bs-card-title-spacer-y)
        }

        .card-subtitle {
            margin-top: calc(-.5 * var(--bs-card-title-spacer-y));
            margin-bottom: 0
        }

        .card-text:last-child {
            margin-bottom: 0
        }

        .card-link + .card-link {
            margin-left: var(--bs-card-spacer-x)
        }

        .card-header_vi {
            padding: var(--bs-card-cap-padding-y) var(--bs-card-cap-padding-x);
            margin-bottom: 0;
            color: var(--bs-card-cap-color);
            background-color: var(--bs-card-cap-bg);
            border-bottom: var(--bs-card-border-width) solid var(--bs-card-border-color)
        }

            .card-header_vi:first-child {
                border-radius: var(--bs-card-inner-border-radius) var(--bs-card-inner-border-radius) 0 0
            }

        .card-footer {
            padding: var(--bs-card-cap-padding-y) var(--bs-card-cap-padding-x);
            color: var(--bs-card-cap-color);
            background-color: var(--bs-card-cap-bg);
            border-top: var(--bs-card-border-width) solid var(--bs-card-border-color)
        }

            .card-footer:last-child {
                border-radius: 0 0 var(--bs-card-inner-border-radius) var(--bs-card-inner-border-radius)
            }

        h5, h6 {
            margin-top: 0px !important;
            margin-bottom: 10px;
        }

        /*Card*/
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="pnlBusqueda" runat="server" Visible="true">
        <div class="row">
            <div class="col-md-12">
                <br />
                <fieldset class="Personal">
                    <%--<legend>
                        <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda</span>
                    </legend>--%>
                    <div class="row">
                        <div class="col-sm-2">&nbsp;</div>
                        <div class="col-sm-2">
                            <dx:BootstrapDateEdit id="date1" runat="server" editformat="Custom" width="100%" caption="Desde" clientinstancename="Fecha1" displayformatstring="dd/MM/yyyy" editformatstring="dd/MM/yyyy" usemaskbehavior="true">
                                <calendarproperties showweeknumbers="false"></calendarproperties>
                            </dx:BootstrapDateEdit>
                        </div>
                        <div class="col-sm-2">
                            <dx:bootstrapdateedit id="date2" runat="server" editformat="Custom" width="100%" caption="Hasta" clientinstancename="Fecha2"
                                displayformatstring="dd/MM/yyyy" editformatstring="dd/MM/yyyy" usemaskbehavior="true">
                                <calendarproperties showweeknumbers="false"></calendarproperties>
                            </dx:bootstrapdateedit>

                        </div>
                        <div class="col-sm-2">
                            <dx:bootstraptextbox id="txtParametro" runat="server" caption="Piloto" width="100%"></dx:bootstraptextbox>
                        </div>
                        <div class="col-sm-2" style="vertical-align: bottom">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <dx:bootstrapbutton id="btnBuscar" runat="server" text="Buscar" onclick="btnBuscar_Click" width="100%" style="margin-top:4px;">
                            <settingsbootstrap renderoption="Success" />
                        </dx:bootstrapbutton>
                        </div>
                        <div class="col-sm-2">&nbsp;</div>
                    </div>
                </fieldset>
            </div>
        </div>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlVuelos" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-12" style="margin-left: -15px; width: 102%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-sm-12">
                            <dx:BootstrapGridView ID="gvCalculo" runat="server" KeyFieldName="IdFolio" OnRowCommand="gvCalculo_RowCommand" OnHtmlDataCellPrepared="gvCalculo_HtmlDataCellPrepared">
                                <SettingsSearchPanel Visible="true" ShowApplyButton="true" />
                                <Settings ShowGroupPanel="true" ShowFilterRowMenu="true" ShowTitlePanel="true" />
                                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                <SettingsPager PageSize="20" PageSizeItemSettings-Caption="Páginas" NextPageButton-Text="Siguiente" PrevPageButton-Text="Anterior"></SettingsPager>
                                <SettingsBehavior AllowDragDrop="true" />
                                <columns>
                                    <%--<dx:BootstrapGridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="AllPages" ShowClearFilterButton="true" />--%>

                                    <dx:bootstrapgridviewdatacolumn caption="Clave" fieldname="CrewCode" visibleindex="1" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="Piloto" fieldname="Piloto" visibleindex="2" horizontalalign="Center" cssclasses-datacell="dataCell" width="22%" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />

                                    <dx:bootstrapgridviewdatacolumn caption="Desayuno Nac." fieldname="DesayunosNal" visibleindex="3" horizontalalign="Center" cssclasses-datacell="dataCell" width="8%" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False"  />
                                    <dx:bootstrapgridviewdatacolumn caption="Desayuno Int." fieldname="DesayunosInt" visibleindex="4" horizontalalign="Center" cssclasses-datacell="dataCell" width="8%" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="Comida Nac." fieldname="ComidasNal" visibleindex="5" horizontalalign="Center" cssclasses-datacell="dataCell" width="8%" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="Comida Int." fieldname="ComidasInt" visibleindex="6" horizontalalign="Center" cssclasses-datacell="dataCell" width="8%" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="Cena Nac." fieldname="CenasNal" visibleindex="7" horizontalalign="Center" cssclasses-datacell="dataCell" width="8%" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="Cena Int." fieldname="CenasInt" visibleindex="8" horizontalalign="Center" cssclasses-datacell="dataCell" width="8%" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />

                                    <dx:bootstrapgridviewdatacolumn visibleindex="9" caption="Estatus" fieldname="Estatus" horizontalalign="Center" CssClasses-DataCell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False">
                                        <dataitemtemplate>
                                            <asp:Label ID="readEstatus" runat="server" Text="" CssClass="dataCell"></asp:Label>
                                        </dataitemtemplate>
                                    </dx:bootstrapgridviewdatacolumn>

                                    <%--<dx:BootstrapGridViewDataColumn Caption="Estatus" FieldName="Estatus" VisibleIndex="5" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />--%>
                                    <dx:BootstrapGridViewDataColumn Visibleindex="10" Caption="Acciones" FieldName="IdFolio" HorizontalAlign="Center" width="20%" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False">
                                        <DataItemTemplate>

                                            <div class="row">
                                                <div class="col-md-12" align="center">
                                                    <asp:UpdatePanel ID="upaReporte" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                    <dx:BootstrapButton Text="Ver viáticos" ID="btnVerViaticos" runat="server" CommandArgument='<%# Eval("IdFolio") %>' CommandName="Ver" Autopostback="true"
                                                        Tooltip="Calcular viáticos" SettingsBootstrap-RenderOption="Primary" CausesValidation="false">
                                                    </dx:BootstrapButton>
                                                    &nbsp;
                                                <%--</div>
                                                <div class="col-md-4">--%>
                                                    <dx:BootstrapButton text="Ajustes" id="btnVerAjustes" runat="server" commandargument='<%# Eval("CrewCode") %>' commandname="Ajustes" autopostback="true"
                                                        tooltip="Mostrar Ajustes" settingsbootstrap-renderoption="Primary" CausesValidation="false">
                                                    </dx:BootstrapButton>
                                                    &nbsp;
                                                <%--</div>
                                                <div class="col-md-4" align="left">--%>
                                                    
                                                            <dx:BootstrapButton Text="Reporte" ID="btnReporte" runat="server" CommandArgument='<%# Eval("CrewCode") %>' CommandName="Reporte" AutoPostback="true"
                                                                Tooltip="Imprimir Reporte de Viáticos" SettingsBootstrap-RenderOption="Primary" CausesValidation="false">
                                                            </dx:BootstrapButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnReporte" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>


                                        </DataItemTemplate>
                                    </dx:BootstrapGridViewDataColumn>

                                    <dx:bootstrapgridviewdatacolumn fieldname="FechaInicio" visible="false" visibleindex="11" cssclasses-datacell="hideColumn" cssclasses-headercell="hideColumn" horizontalalign="Center" />
                                    <dx:bootstrapgridviewdatacolumn fieldname="FechaFin" visible="false" visibleindex="12" cssclasses-datacell="hideColumn" cssclasses-headercell="hideColumn" horizontalalign="Center" />
                                    <%--<dx:BootstrapGridViewDataColumn FieldName="Estatus_Img" Visible="false" VisibleIndex="15" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />--%>
                                </columns>
                                <settingsbehavior confirmdelete="True" />
                                <settingspager position="Bottom">
                                    <pagesizeitemsettings items="20, 50, 100"></pagesizeitemsettings>
                                </settingspager>
                                <settingsediting mode="PopupEditForm"></settingsediting>
                                <settings showgrouppanel="True" />
                                <settingspopup>
                                    <editform horizontalalign="Center" verticalalign="Below" width="400px" />
                                </settingspopup>
                            </dx:BootstrapGridView>
                        </div>
                        <br />
                        <div class="col-lg-12" align="right">
                            <dx:bootstrapbutton id="btnAprobar" runat="server" text="Aprobar" settingsbootstrap-renderoption="Primary" autopostback="true" onclick="btnAprobar_Click" Visible="false" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>


    
        
          <asp:Panel ID="pnlDatosPiloto" runat="server" Visible="false" style="padding-bottom: 10px;">

              <div class="card_vi" style="margin:0 auto 0 auto; width:98%;">
                  <h5 class="card-header_vi" style="font-weight:700;">DATOS DE PERIODO Y PILOTO</h5>
                  <div class="card-body">
                        <div style="width: 90%; margin: 0 auto 0 auto;">
                            <div class="row">
                                <div class="col-sm-2">
                                    <label>Clave de Piloto:</label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="readCvePiloto" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:HiddenField ID="hdnFechaInicio" runat="server" />
                                    <asp:HiddenField ID="hdnFechaFinal" runat="server" />
                                </div>
                                <div class="col-sm-1">
                                    <label>Período:</label>
                                </div>
                                <div class="col-sm-4">
                                    <asp:Label ID="readPeríodo" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-2">
                                    <label>Piloto:</label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="readPiloto" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-sm-7">&nbsp;&nbsp;&nbsp;</div>
                            </div>
                        </div>
                      </div>
                </div>

          </asp:Panel>
        
      

    

    <asp:Panel ID="pnlCalcularViaticos" runat="server" Visible="false" style="padding-bottom: 10px;">

        <div class="card_vi" style="margin:0 auto 0 auto; width:98%;">
            <h5 class="card-header_vi" style="font-weight:700;">VIÁTICOS Y HORARIOS</h5>
            <div class="card-body">
                <div style="width: 90%; margin: 0 auto 0 auto;">

                    <div class="row">
                        <div class="col-md-6">
                             <dx:bootstrapgridview id="gvMXNUSD" runat="server">
                                <settingssearchpanel visible="false" showapplybutton="false" />
                                <settings showgrouppanel="false" showfilterrowmenu="false" />
                                <settingsadaptivity adaptivitymode="HideDataCells" allowonlyoneadaptivedetailexpanded="true"></settingsadaptivity>
                                <columns>
                                    <dx:bootstrapgridviewdatacolumn caption="CONCEPTO" fieldname="CONCEPTO" visibleindex="1" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="NACIONAL" fieldname="NACIONAL" visibleindex="2" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="INTERNACIONAL" fieldname="INTERNACIONAL" visibleindex="3" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                </columns>
                            </dx:bootstrapgridview>
                        </div>
                        <div class="col-md-6">
                            <dx:bootstrapgridview id="gvHorarios" runat="server" keyfieldname="IdConcepto">
                                <settingssearchpanel visible="false" showapplybutton="false" />
                                <settings showgrouppanel="false" showfilterrowmenu="false" />
                                <settingsadaptivity adaptivitymode="HideDataCells" allowonlyoneadaptivedetailexpanded="true"></settingsadaptivity>
                                <columns>
                                    <dx:bootstrapgridviewdatacolumn caption="CONCEPTO" fieldname="DesConcepto" visibleindex="1" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="HORARIOS" fieldname="Horario" visibleindex="2" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="NACIONAL" fieldname="MontoMXN" visibleindex="3" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="INTERNACIONAL" fieldname="MontoUSD" visibleindex="4" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                </columns>
                            </dx:bootstrapgridview>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="row" style="display:none;">
            <div class="col-md-4">
                <dx:bootstrapgridview id="gvNacionales" runat="server">
                    <settingssearchpanel visible="false" showapplybutton="false" />
                    <settings showgrouppanel="false" showfilterrowmenu="false" />
                    <settingsadaptivity adaptivitymode="HideDataCells" allowonlyoneadaptivedetailexpanded="true"></settingsadaptivity>
                    <columns>

                        <dx:bootstrapgridviewdatacolumn caption="CONCEPTO" fieldname="CONCEPTO" visibleindex="1" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                        <dx:bootstrapgridviewdatacolumn caption="NACIONAL" fieldname="NACIONAL" visibleindex="2" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />

                    </columns>
                </dx:bootstrapgridview>
            </div>
            <div class="col-md-4">
                
            </div>
            <div class="col-md-4">
                <dx:bootstrapgridview id="gvInternacionales" runat="server">
                    <settingssearchpanel visible="false" showapplybutton="false" />
                    <settings showgrouppanel="false" showfilterrowmenu="false" />
                    <settingsadaptivity adaptivitymode="HideDataCells" allowonlyoneadaptivedetailexpanded="true"></settingsadaptivity>
                    <columns>

                        <dx:bootstrapgridviewdatacolumn caption="CONCEPTO" fieldname="CONCEPTO" visibleindex="1" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                        <dx:bootstrapgridviewdatacolumn caption="INTERNACIONAL" fieldname="INTERNACIONAL" visibleindex="2" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />

                    </columns>
                </dx:bootstrapgridview>
            </div>
        </div>


        <div class="card_vi" style="margin: 10px auto 0 auto; width:98%;">
            <h5 class="card-header_vi" style="font-weight:700;">VIÁTICOS POR DÍA</h5>
            <div class="card-body">
                <div style="width: 90%; margin: 0 auto 0 auto;">

                    <div class="row">
                        <div class="col-md-12">
                             <dx:BootstrapGridView ID="gvDiasViaticos" runat="server" width="100%">
                                <SettingsSearchPanel Visible="false" ShowApplyButton="false" />
                                <Settings ShowGroupPanel="false" ShowFilterRowMenu="false" />
                                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                <Columns>

                                    <dx:BootstrapGridViewDataColumn Caption="FECHA" FieldName="Fecha" VisibleIndex="1" HorizontalAlign="Center" CssClasses-DataCell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:BootstrapGridViewDataColumn Caption="DESAYUNO" FieldName="Desayuno" VisibleIndex="3" HorizontalAlign="Center" CssClasses-DataCell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:BootstrapGridViewDataColumn Caption="COMIDA" FieldName="Comida" VisibleIndex="4" HorizontalAlign="Center" CssClasses-DataCell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:BootstrapGridViewDataColumn Caption="CENA" FieldName="Cena" VisibleIndex="5" HorizontalAlign="Center" CssClasses-DataCell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />

                                </Columns>
                            </dx:BootstrapGridView>
                
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="card_vi" style="margin:10px auto 0 auto; width:98%;">
            <h5 class="card-header_vi" style="font-weight:700;">LISTADO DE AJUSTES</h5>
            <div class="card-body">
                <div style="width: 90%; margin: 0 auto 0 auto;">

                    <div class="row">
                        <div class="col-md-12" style="max-height:300px; overflow-y:scroll;">
                            <dx:bootstrapgridview id="gvAjustesPiloto" runat="server" keyfieldname="IdAdicional" width="100%">
                                <settingssearchpanel visible="false" showapplybutton="false" />
                                <settings showgrouppanel="false" showfilterrowmenu="false" />
                                <settingsadaptivity adaptivitymode="HideDataCells" allowonlyoneadaptivedetailexpanded="true" />
                                <columns>
                                    <dx:bootstrapgridviewdatacolumn caption="CONCEPTO MANUAL" fieldname="DesConcepto" visibleindex="1" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="MONEDA" fieldname="Moneda" visibleindex="2" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:BootstrapGridViewTextColumn caption="IMPORTE" fieldname="Valor" visibleindex="3" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False">
                                        <PropertiesTextEdit DisplayFormatString="c"></PropertiesTextEdit>
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:bootstrapgridviewdatacolumn caption="COMENTARIOS" fieldname="Comentarios" visibleindex="4" horizontalalign="Left" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                </columns>
                            </dx:bootstrapgridview>
            
                            <dx:bootstrapgridview id="gvConteoDias" runat="server" OnPageIndexChanged="gvConteoDias_PageIndexChanged" Visible="false">
                                <settingssearchpanel visible="false" showapplybutton="false" />
                                <settings showgrouppanel="false" showfilterrowmenu="false" />
                                <settingsadaptivity adaptivitymode="HideDataCells" allowonlyoneadaptivedetailexpanded="true"></settingsadaptivity>
                                <columns>

                                    <dx:bootstrapgridviewdatacolumn caption="FECHA" fieldname="Fecha" visibleindex="1" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="MONEDA" fieldname="Moneda" visibleindex="2" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="DESAYUNO" fieldname="Desayuno" visibleindex="3" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="COMIDA" fieldname="Comida" visibleindex="4" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="CENA" fieldname="Cena" visibleindex="5" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                    <dx:bootstrapgridviewdatacolumn caption="TOTAL" fieldname="Total" visibleindex="6" horizontalalign="Center" cssclasses-datacell="dataCell" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />

                                </columns>
                            </dx:bootstrapgridview>
                
               
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div id="divViaticos" runat="server"></div>

        <div class="card_vi" style="margin:10px auto 0 auto; width:98%;">
            <h5 class="card-header_vi" style="font-weight:700;">VUELOS DEL PERÍODO</h5>
            <div class="card-body">
                <div style="width: 90%; margin: 0 auto 0 auto;">

                    <div class="row">
                        <div class="col-md-12" style="max-height:400px; overflow-y:scroll;">
                            <asp:UpdatePanel ID="upaVuelos" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                        <dx:bootstrapgridview id="gvVuelos" runat="server" keyfieldname="LegId" Width="100%">
                                            <SettingsSearchPanel Visible="false" ShowApplyButton="false" />
                                            <Settings ShowGroupPanel="false" ShowFilterRowMenu="false" />
                                            <settingsadaptivity adaptivitymode="HideDataCells" allowonlyoneadaptivedetailexpanded="true" />
                                            <%--<settingspager pagesize="20" />--%>
                                            <columns>
                                                <dx:bootstrapgridviewdatacolumn caption="Trip" fieldname="Trip" visible="true" visibleindex="1" horizontalalign="Center" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False">
                                                    <dataitemtemplate>
                                                        <div>
                                                            <asp:Label ID="readTrip" runat="server" Text='<%# Eval("Trip") %>' CssClass="dataCell"></asp:Label>
                                                        </div>
                                                    </dataitemtemplate>
                                                    <cssclasses headercell="spa" />
                                                </dx:bootstrapgridviewdatacolumn>
                                                <dx:bootstrapgridviewdatacolumn caption="Origen" fieldname="POD" visible="true" visibleindex="2" horizontalalign="Center" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False">
                                                    <dataitemtemplate>
                                                        <div>
                                                            <asp:Label ID="readPOD" runat="server" Text='<%# Eval("POD") %>' CssClass="dataCell"></asp:Label>
                                                        </div>
                                                    </dataitemtemplate>
                                                    <cssclasses headercell="spa" />
                                                </dx:bootstrapgridviewdatacolumn>

                                                <dx:bootstrapgridviewdatacolumn caption="Destino" fieldname="POA" visible="true" visibleindex="3" horizontalalign="Center" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False">
                                                    <dataitemtemplate>
                                                        <div>
                                                            <asp:Label ID="readPOA" runat="server" Text='<%# Eval("POA") %>' CssClass="dataCell"></asp:Label>
                                                        </div>
                                                    </dataitemtemplate>
                                                    <cssclasses headercell="spa" />
                                                </dx:bootstrapgridviewdatacolumn>


                                                <dx:bootstrapgridviewdatacolumn caption="Fecha de Salida" fieldname="FechaSalida" visible="true" visibleindex="4" horizontalalign="Center" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False">
                                                    <dataitemtemplate>
                                                        <div>
                                                            <asp:Label ID="readFechaSalida" runat="server" Text='<%# Eval("FechaSalida") %>' CssClass="dataCell"></asp:Label>
                                                        </div>
                                                    </dataitemtemplate>
                                                    <cssclasses headercell="spa" />
                                                </dx:bootstrapgridviewdatacolumn>
                                                <dx:bootstrapgridviewdatacolumn caption="Fecha de Llegada" fieldname="FechaLlegada" visible="true" visibleindex="5" horizontalalign="Center" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False">
                                                    <dataitemtemplate>
                                                        <div>
                                                            <asp:Label ID="readFechaLlegada" runat="server" Text='<%# Eval("FechaLlegada") %>' CssClass="dataCell"></asp:Label>
                                                        </div>
                                                    </dataitemtemplate>
                                                    <cssclasses headercell="spa" />
                                                </dx:bootstrapgridviewdatacolumn>

                                                <dx:bootstrapgridviewdatacolumn caption="CheckIn" fieldname="CheckIn" visible="true" visibleindex="6" horizontalalign="Center" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False">
                                                    <dataitemtemplate>
                                                        <div>
                                                            <asp:Label ID="readCheckIn" runat="server" Text='<%# Eval("CheckIn") %>' CssClass="dataCell"></asp:Label>
                                                        </div>
                                                    </dataitemtemplate>
                                                    <cssclasses headercell="spa" />
                                                </dx:bootstrapgridviewdatacolumn>
                                                <dx:bootstrapgridviewdatacolumn caption="CheckOut" fieldname="CheckOut" visible="true" visibleindex="6" horizontalalign="Center" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False">
                                                    <dataitemtemplate>
                                                        <div>
                                                            <asp:Label ID="readCheckOut" runat="server" Text='<%# Eval("CheckOut") %>' CssClass="dataCell"></asp:Label>
                                                        </div>
                                                    </dataitemtemplate>
                                                    <cssclasses headercell="spa" />
                                                </dx:bootstrapgridviewdatacolumn>
                                            </columns>
                                            <settingspager position="Bottom">
                                                <pagesizeitemsettings items="20, 50, 100"></pagesizeitemsettings>
                                            </settingspager>
                                            <settingsediting mode="PopupEditForm"></settingsediting>
                                            <settings showgrouppanel="True" />
                                        </dx:bootstrapgridview>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <br />
        <div class="row" style="margin-top:10px;">
            <dx:bootstrapformlayout runat="server">
                <items>
                    <dx:bootstraplayoutitem horizontalalign="Right" showcaption="False" colspanmd="12">
                        <contentcollection>
                            <dx:contentcontrol>
                                <dx:bootstrapbutton id="btnGuardarPeriodo" runat="server" text="Guardar Período" settingsbootstrap-renderoption="Primary" autopostback="true" onclick="btnGuardarPeriodo_Click" />
                                <dx:bootstrapbutton id="btnCancelar" runat="server" text="Regresar" settingsbootstrap-renderoption="Warning" autopostback="false" onclick="btnCancelar_Click" />
                            </dx:contentcontrol>
                        </contentcollection>
                    </dx:bootstraplayoutitem>
                </items>
            </dx:bootstrapformlayout>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlAjuste" runat="server" Visible="false">

        <div class="row" style="padding-bottom: 20px;">
            <div class="col-md-3"></div>
            <div class="col-md-6" align="right">
                <dx:bootstrapbutton id="btnAgregarAjuste" runat="server" text="Nuevo ajuste" settingsbootstrap-renderoption="Success" autopostback="false" onclick="btnAgregarAjuste_Click" />
            </div>
            <div class="col-md-3"></div>
        </div>
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-6" align="center">
                <dx:bootstrapgridview id="gvAjustes" runat="server" keyfieldname="IdAdicional" width="100%" OnRowCommand="gvAjustes_RowCommand">
                    <settingssearchpanel visible="false" showapplybutton="false" />
                    <settings showgrouppanel="false" showfilterrowmenu="false" />
                    <settingsadaptivity adaptivitymode="HideDataCells" allowonlyoneadaptivedetailexpanded="true" />
                    <columns>
                        <dx:bootstrapgridviewdatacolumn caption="CONCEPTO MANUAL" fieldname="DesConcepto" visibleindex="1" horizontalalign="Center" cssclasses-datacell="dataCell" />
                        <dx:bootstrapgridviewdatacolumn caption="MONEDA" fieldname="Moneda" visibleindex="2" horizontalalign="Center" cssclasses-datacell="dataCell" />
                        <dx:BootstrapGridViewTextColumn caption="IMPORTE" fieldname="Valor" visibleindex="3" horizontalalign="Center" cssclasses-datacell="dataCell">
                            <PropertiesTextEdit DisplayFormatString="c"></PropertiesTextEdit>
                        </dx:BootstrapGridViewTextColumn>
                        <dx:bootstrapgridviewdatacolumn caption="COMENTARIOS" fieldname="Comentarios" visibleindex="4" horizontalalign="Left" cssclasses-datacell="dataCell" />

                        <dx:BootstrapGridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="4" HorizontalAlign="Center" Width="20%">
                            <DataItemTemplate>
                                <div>

                                    <asp:Button ID="btnEliminar" runat="server" CommandArgument='<%# Eval("IdAdicional") %>' CommandName="Eliminar" ToolTip="Elimina" 
                                        CssClass="btn btn-danger" Text="Eliminar" />

                                </div>
                            </DataItemTemplate>
                            <CssClasses HeaderCell="spa" />
                        </dx:BootstrapGridViewDataColumn>

                    </columns>
                </dx:bootstrapgridview>
            </div>
            <div class="col-md-3"></div>
        </div>
        <div class="row">
            <dx:bootstrapformlayout runat="server">
                <items>
                    <dx:bootstraplayoutitem horizontalalign="Right" showcaption="False" colspanmd="12">
                        <contentcollection>
                            <dx:contentcontrol>
                                <dx:bootstrapbutton id="btnRegresar" runat="server" text="Regresar" settingsbootstrap-renderoption="Warning" autopostback="false" onclick="btnRegresar_Click" />
                            </dx:contentcontrol>
                        </contentcollection>
                    </dx:bootstraplayoutitem>
                </items>
            </dx:bootstrapformlayout>
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


    <%-- MODAL PARA AJUSTES --%>
    <dx:bootstrappopupcontrol id="ppAjustes" runat="server" clientinstancename="ppAjustes"
        closeanimationtype="Fade" popupanimationtype="Fade"
        popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter" width="500px"
        closeaction="CloseButton" closeonescape="true" allowresize="true"
        headertext="Ajustes" allowdragging="true" showclosebutton="true">
        <contentcollection>
            <dx:contentcontrol>

                <div style="width: 100%;">
                    <table style="width: 95%; margin: 0 auto;" border="0">
                        <tr>
                            <td style="padding: 4px;">
                                <label>Concepto:</label>
                                <dx:bootstrapcombobox id="ddlConceptoAdicional" runat="server" nulltext="Selecciona Concepto">
                                    <ValidationSettings RequiredField-ErrorText="Se requiere seleccionar concepto" ErrorDisplayMode="Text" ValidationGroup="gpAjuste" RequiredField-IsRequired="true"></ValidationSettings>
                                </dx:bootstrapcombobox>
                                <asp:HiddenField ID="hdnIdPeriodo" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 4px;">
                                <label>Moneda:</label>
                                <dx:bootstrapcombobox id="ddlMoneda" runat="server" nulltext="Selecciona moneda">
                                    <items>
                                        <dx:bootstraplistedititem value="MXN" text="MXN"></dx:bootstraplistedititem>
                                        <dx:bootstraplistedititem value="USD" text="USD"></dx:bootstraplistedititem>
                                    </items>
                                    <ValidationSettings ErrorDisplayMode="Text" RequiredField-ErrorText="Se requiere seleccionar moneda" ValidationGroup="gpAjuste" RequiredField-IsRequired="true"></ValidationSettings>
                                </dx:bootstrapcombobox>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 4px;">
                                <label>Importe:</label>
                                <dx:bootstraptextbox id="txtImporte" runat="server" nulltext="Importe">
                                    <ValidationSettings RequiredField-ErrorText="Se requiere importe" ErrorDisplayMode="Text" ValidationGroup="gpAjuste" RequiredField-IsRequired="true"></ValidationSettings>
                                </dx:bootstraptextbox>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 4px;">
                                <label>Comentarios:</label>
                                <dx:bootstrapmemo id="txtComentarios" runat="server" nulltext="Comentarios" rows="6" width="100%">
                                </dx:bootstrapmemo>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div class="row">
                    <dx:bootstrapformlayout runat="server">
                        <items>
                            <dx:bootstraplayoutitem horizontalalign="Right" showcaption="False" colspanmd="12">
                                <contentcollection>
                                    <dx:contentcontrol>
                                        <dx:aspxbutton id="btnCerrar" runat="server" text="Cerrar" cssclass="btn btn-warning" width="80px" height="40px" autopostback="false" tabindex="0">
                                            <clientsideevents click="function(s, e) {ppAjustes.Hide(); }" />
                                        </dx:aspxbutton>
                                        &nbsp;
                                        <dx:bootstrapbutton id="btnGuardarAdicional" runat="server" text="Guardar" settingsbootstrap-renderoption="Success" autopostback="false" onclick="btnGuardarAdicional_Click" ValidationGroup="gpAjuste" />
                                    </dx:contentcontrol>
                                </contentcollection>
                            </dx:bootstraplayoutitem>
                        </items>
                    </dx:bootstrapformlayout>
                </div>

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

</asp:Content>
