<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmReporteFl3xx.aspx.cs" Inherits="ALE_MexJet.Views.bitacoras.frmReporteFl3xx" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" type="text/css" href="../../Styles/Controls.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>--%>
  
    <style>
       /* .hiddenRow {
            visibility:hidden !important;
        }*/
        .centerCell {
            text-align:center !important;
            color: #337ab7 !important;
        }
        /*.centerTxt {
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
        }*/
        /*.spa {
            font-weight: bold !important;
            color: #337ab7 !important;
            text-align: center !important;
            font-size:9pt;
        }*/
       /* .validateTxt {
            border-color: crimson !important;
        }*/
        th {
            /*font-size:10pt !important;*/
            font-weight: bold !important;
            color: #337ab7 !important;
            text-align: center !important;
            font-size:11pt;
        }
        /*.inputText {
            font-family: inherit !important;
            font-size: inherit !important;
            line-height: inherit !important;
            background-color: #FFFFFF !important;
            padding: 4px 4px 5px 4px !important;
            border: 1px solid #ccc !important;
            border-radius: 4px;
            color: #555;
            -webkit-transition: 0.5s;
            
        }
        .inputText textarea[type=text]:focus {
            box-shadow: inset 0 1px 1px rgb(0 0 0 / 8%) !important;
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s !important;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s !important;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s !important;
            outline:auto;
        }

        .modalBitacora {
            z-index: 12000 !important;
            visibility: visible !important;
            display: table !important;
            position: absolute !important;
            left: 125px !important;
            top: 2px !important;
        }*/

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <dx:PanelContent ID="ASPxPanel1" runat="server" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span class="FTitulo">&nbsp;&nbsp; Reporte FL3XX</span>
                    </div>
                </div>
            </dx:PanelContent>
        <PanelCollection>
    </dx:PanelContent>
    
    <asp:Panel ID="pnlBusqueda" runat="server" Visible="true">
        <div class="row">
            <div class="col-md-12">
                <br />
                <fieldset class="Personal">
                    <%--<legend>
                        <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda</span>
                    </legend>--%>
                    <div class="row">
                        <div class="col-sm-3">&nbsp;</div>
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
                        <div class="col-sm-2" style="vertical-align: bottom">
                            <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  --%>    
                        <dx:bootstrapbutton ID="btnBuscar" runat="server" text="Buscar" width="100%" style="margin-top:4px;" OnClick="btnBuscar_Click">
                            <settingsbootstrap renderoption="Success" />
                        </dx:bootstrapbutton>
                        </div>
                        <div class="col-sm-3">&nbsp;</div>
                    </div>
                </fieldset>
            </div>
        </div>
    </asp:Panel>

    

    <asp:Panel ID="pnlReporteFl3xx" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-12" style="margin-left: -15px; width: 102%;">
                <asp:UpdatePanel ID="upaFl3xx" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12" style="text-align: right;">
                                <dx:ASPxButton CssClass="btn btn-success" ID="btnExportar" runat="server" Text="Exportar" OnClick="btnExportar_Click"></dx:ASPxButton>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-sm-12">

                                <dx:BootstrapGridView ID="gvFl3xx" runat="server" KeyFieldName="flightId" SettingsBehavior-AllowDragDrop="false" OnPageIndexChanged="gvFl3xx_PageIndexChanged">
                                    <SettingsSearchPanel Visible="false" ShowApplyButton="false" />
                                    <Settings ShowGroupPanel="false" ShowFilterRowMenu="false" ShowTitlePanel="false" />
                                    <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                    <SettingsPager PageSize="20" PageSizeItemSettings-Caption="Registros por Página" NextPageButton-Text="Siguiente" PrevPageButton-Text="Anterior"></SettingsPager>
                                    <SettingsBehavior AllowDragDrop="false" />
                                    <Columns>

                                        <dx:BootstrapGridViewDataColumn Caption="tail_nmbr" FieldName="tail_nmbr" VisibleIndex="1" HorizontalAlign="Center" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False"/>
                                        <dx:BootstrapGridViewDataColumn Caption="activedate" FieldName="activedate" VisibleIndex="2" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="locdep" FieldName="locdep" VisibleIndex="3" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="depicao_id" FieldName="depicao_id" VisibleIndex="4" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="locarr" FieldName="locarr" VisibleIndex="5" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="arricao_id" FieldName="arricao_id" VisibleIndex="6" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="requestor" FieldName="requestor" VisibleIndex="7" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />

                                        <dx:BootstrapGridViewDataColumn Caption="cat_code" FieldName="cat_code" VisibleIndex="8" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="elp_time" FieldName="elp_time" VisibleIndex="9" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="pax_total" FieldName="pax_total" VisibleIndex="10" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="pic_code" FieldName="pic_code" VisibleIndex="11" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />

                                        <dx:BootstrapGridViewDataColumn Caption="sic_code" FieldName="sic_code" VisibleIndex="12" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="orig_nmbr" FieldName="orig_nmbr" VisibleIndex="13" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="legid" FieldName="legid" VisibleIndex="14" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />
                                        <dx:BootstrapGridViewDataColumn Caption="type_code" FieldName="type_code" VisibleIndex="15" SortOrder="None" Settings-AllowDragDrop="False" Settings-AllowSort="False" />

                                        <%--<dx:BootstrapGridViewDataColumn FieldName="Estatus" VisibleIndex="9" Caption="Estatus" Width="120px">
                                            <DataItemTemplate>
                                                <dx:BootstrapImage runat="server" ID="imgTemplate" Width="15px" Height="15px" ImageAlign="AbsMiddle"
                                                    ImageUrl='<%# "~/img/iconos/" + Eval("EstatusImg") %>'
                                                    ToolTip='<%# Eval("Tooltip") %>'>
                                                </dx:BootstrapImage>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDataColumn>--%>
                                        <%--<dx:BootstrapGridViewDataColumn FieldName="leg_num" Visible="false" VisibleIndex="12" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />--%>
                                    

                                    </Columns>
                                    <SettingsBehavior ConfirmDelete="True" />
                                    <SettingsPager Position="Bottom">
                                        <PageSizeItemSettings Items="20, 50, 100"></PageSizeItemSettings>
                                    </SettingsPager>
                                    <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                                    <Settings ShowGroupPanel="True" />
                                    <SettingsPopup>
                                        <EditForm HorizontalAlign="Center" VerticalAlign="Below" Width="400px" />
                                    </SettingsPopup>
                                </dx:BootstrapGridView>

                            </div>

                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExportar" />
                    </Triggers>
                </asp:UpdatePanel>
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

</asp:Content>
