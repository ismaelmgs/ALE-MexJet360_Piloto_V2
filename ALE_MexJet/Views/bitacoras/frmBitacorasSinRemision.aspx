<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmBitacorasSinRemision.aspx.cs" Inherits="ALE_MexJet.Views.bitacoras.frmBitacorasSinRemision"
    UICulture="es" Culture="es-MX" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
            font-size: 11pt;
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

        .btn-primary {
            height: 34px !important;
            /*width: 80px !important;*/
        }

        .btnSize.disabled, .btnSize:disabled, .btnSize[disabled] {
            height: 34px !important;
            width: 80px !important;
        }

        .btn-primary.disabled, .btn-primary:disabled, .btn-primary[disabled] {
            height: 34px !important;
            /*width: 80px !important;*/
        }
        .noView{
            display:none;
        }
        /*Card*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="pnlGeneral" runat="server" Visible="true">

        <asp:UpdatePanel ID="upaGeneral" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="row">
                    <div class="col-md-12" align="center">
                        <dx:bootstrapgridview id="gvBitacorasSinRemision" runat="server" keyfieldname="IdBitacora" width="100%"
                            onpageindexchanged="gvBitacorasSinRemision_PageIndexChanged"
                            OnBeforePerformDataSelect="gvBitacorasSinRemision_BeforePerformDataSelect"
                            OnLoad="gvBitacorasSinRemision_Load">
                             <settingssearchpanel visible="true" showapplybutton="true" />
                            <settings showgrouppanel="true" showfilterrowmenu="true" showtitlepanel="true" />
                            <settingsadaptivity adaptivitymode="HideDataCells" allowonlyoneadaptivedetailexpanded="true"></settingsadaptivity>
                            <settingsbehavior allowdragdrop="true" />
                            <SettingsPager PageSize="10" pagesizeitemsettings-caption="Páginas" nextpagebutton-text="Siguiente" prevpagebutton-text="Anterior">
                                <PageSizeItemSettings Items="5, 10, 20, 50" Visible="True" ShowAllItem="true">
                                </PageSizeItemSettings>
                            </SettingsPager>

                            <%--<columns>--%>
                                <%--<dx:bootstrapgridviewdatacolumn caption="ID" fieldname="IdEspecial" visibleindex="1" horizontalalign="Center" cssclasses-datacell="dataCell" />
                                <dx:bootstrapgridviewdatacolumn caption="CLAVE AEROPUERTO" fieldname="POA" visibleindex="2" horizontalalign="Center" cssclasses-datacell="dataCell" />
                                <dx:bootstrapgridviewdatacolumn caption="NOMBRE" fieldname="DesAeropuerto" visibleindex="3" horizontalalign="Left" cssclasses-datacell="dataCell" />

                                <dx:bootstrapgridviewdatacolumn caption="ACCIONES" visible="true" visibleindex="4" horizontalalign="Center" width="20%">
                                    <dataitemtemplate>
                                        <div>

                                            <asp:Button ID="btnEliminar" runat="server" CommandArgument='<%# Eval("IdEspecial") %>' CommandName="Eliminar" ToolTip="Elimina"
                                                CssClass="btn btn-danger" Text="Eliminar" />

                                        </div>
                                    </dataitemtemplate>
                                    <cssclasses headercell="spa" />
                                </dx:bootstrapgridviewdatacolumn>--%>

                            <%--</columns>--%>
                            <settingsbehavior confirmdelete="True" />
                            <settingsediting mode="PopupEditForm"></settingsediting>
                            <settings showgrouppanel="True" />
                            <settingspopup>
                                <editform horizontalalign="Center" verticalalign="Below" width="400px" />
                            </settingspopup>
                        </dx:bootstrapgridview>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </asp:Panel>

</asp:Content>
