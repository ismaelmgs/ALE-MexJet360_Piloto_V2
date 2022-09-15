<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmCalculoPagos.aspx.cs" Inherits="ALE_MexJet.Views.viaticos.frmCalculoPagos" UICulture="es" Culture="es-MX" %>

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
            font-size:9pt !important;
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

    <asp:Panel ID="pnlBusqueda" runat="server" Visible="true">
         <div class="row">
            <div class="col-md-12">
                <br />
                <fieldset class="Personal">
                    <legend>
                        <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda</span>
                    </legend>
                    <div class="row">
                    <div class="col-sm-2">&nbsp;</div>
                    <div class="col-sm-2">
                        <dx:BootstrapDateEdit ID="date1" runat="server" EditFormat="Custom" Width="100%" Caption="Desde" ClientInstanceName="Fecha1"
                            DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true">
                            <CalendarProperties ShowWeekNumbers="false"></CalendarProperties>
                        </dx:BootstrapDateEdit>
                    </div>
                    <div class="col-sm-2">
                        <dx:BootstrapDateEdit ID="date2" runat="server" EditFormat="Custom" Width="100%" Caption="Hasta" ClientInstanceName="Fecha2"
                            DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" UseMaskBehavior="true">
                            <CalendarProperties ShowWeekNumbers="false"></CalendarProperties>
                        </dx:BootstrapDateEdit>
                    </div>
                    <div class="col-sm-2">
                        <dx:BootstrapTextBox ID="txtParametro" runat="server" Caption="Piloto" Width="100%">
                            <%--<MaskSettings Mask="999999999999999" />--%>
                        </dx:BootstrapTextBox>
                    </div>
                    <div class="col-sm-2" style="vertical-align:bottom">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <dx:BootstrapButton ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Width="100%">
                            <SettingsBootstrap RenderOption="Success" />
                        </dx:BootstrapButton>
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
                            <dx:BootstrapGridView ID="gvCalculo" runat="server" KeyFieldName="IdFolio" OnRowCommand="gvCalculo_RowCommand">
                                <SettingsSearchPanel Visible="true" ShowApplyButton="true" />
                                <Settings ShowGroupPanel="True" ShowFilterRowMenu="true" />
                                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                <SettingsPager PageSize="20"></SettingsPager>
                                <Columns>
                                    <%--<dx:BootstrapGridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="AllPages" ShowClearFilterButton="true" />--%>

                                    <dx:BootstrapGridViewDataColumn Caption="Clave" FieldName="CrewCode" VisibleIndex="1" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Piloto" FieldName="Piloto" VisibleIndex="2" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    
                                    <dx:BootstrapGridViewDataColumn Caption="Desayuno Nac." FieldName="DesayunosNal" VisibleIndex="3" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Desayuno Int." FieldName="DesayunosInt" VisibleIndex="4" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Comida Nac." FieldName="ComidasNal" VisibleIndex="5" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Comida Int." FieldName="ComidasInt" VisibleIndex="6" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Cena Nac." FieldName="CenasNal" VisibleIndex="7" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    <dx:BootstrapGridViewDataColumn Caption="Cena Int." FieldName="CenasInt" VisibleIndex="8" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                                    
                                    <%--<dx:BootstrapGridViewDataColumn Caption="Estatus" FieldName="Estatus" VisibleIndex="5" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />--%>
                                    <dx:BootstrapGridViewDataColumn VisibleIndex="9" Caption="Acciones" Width="120px" FieldName="IdFolio">
                                        <DataItemTemplate>
                                            <%--<dx:BootstrapImage runat="server" ID="imgTemplate" Width="15px" Height="15px" ImageAlign="AbsMiddle"
                                                ImageUrl='<%# "~/img/iconos/" + Eval("EstatusImg") %>'
                                                ToolTip='<%# Eval("Tooltip") %>'>
                                            </dx:BootstrapImage>--%>

                                            <dx:BootstrapButton Text="Ver viáticos" ID="btnVerViaticos" runat="server" CommandArgument='<%# Eval("IdFolio") %>' CommandName="Ver" AutoPostBack="true" 
                                                ToolTip="Calcular viáticos" SettingsBootstrap-RenderOption="Primary"></dx:BootstrapButton>

                                        </DataItemTemplate>
                                    </dx:BootstrapGridViewDataColumn>
                                    
                                    <dx:BootstrapGridViewDataColumn FieldName="FechaInicio" Visible="false" VisibleIndex="10" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <dx:BootstrapGridViewDataColumn FieldName="FechaFin" Visible="false" VisibleIndex="11" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                    <%--<dx:BootstrapGridViewDataColumn FieldName="Estatus_Img" Visible="false" VisibleIndex="15" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />--%>

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
                        <br />
                        <div class="col-lg-12" align="right">
                            <dx:BootstrapButton ID="btnAprobar" runat="server" Text="Aprobar" SettingsBootstrap-RenderOption="Primary" AutoPostBack="true" OnClick="btnAprobar_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlCalcularViaticos" runat="server" Visible="false">

        <div class="row">
            <div class="col-sm-2"><label>Clave de Piloto:</label></div>
            <div class="col-sm-3"><asp:Label ID="readCvePiloto" runat="server" Text=""></asp:Label></div>
            <div class="col-sm-2">&nbsp;&nbsp;&nbsp;
                <asp:HiddenField ID="hdnFechaInicio" runat="server" />
                <asp:HiddenField ID="hdnFechaFinal" runat="server" />
            </div>
            <div class="col-sm-2"><label>Período:</label></div>
            <div class="col-sm-3"><asp:Label ID="readPeríodo" runat="server" Text=""></asp:Label></div>
        </div>
        <div class="row">
            <div class="col-sm-2"><label>Piloto:</label></div>
            <div class="col-sm-3"><asp:Label ID="readPiloto" runat="server" Text=""></asp:Label></div>
            <div class="col-sm-7">&nbsp;&nbsp;&nbsp;</div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <dx:BootstrapGridView ID="gvNacionales" runat="server">
                    <SettingsSearchPanel Visible="false" ShowApplyButton="false" />
                    <Settings ShowGroupPanel="false" ShowFilterRowMenu="false" />
                    <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                    <Columns>
                                    
                        <dx:BootstrapGridViewDataColumn Caption="CONCEPTO" FieldName="CONCEPTO" VisibleIndex="1" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                        <dx:BootstrapGridViewDataColumn Caption="NACIONAL" FieldName="NACIONAL" VisibleIndex="2" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />

                    </Columns>
                </dx:BootstrapGridView>
            </div>
            <div class="col-md-4">
                <dx:BootstrapGridView ID="gvHorarios" runat="server" KeyFieldName="IdConcepto">
                    <SettingsSearchPanel Visible="false" ShowApplyButton="false" />
                    <Settings ShowGroupPanel="false" ShowFilterRowMenu="false" />
                    <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                    <Columns>        
                        <dx:BootstrapGridViewDataColumn Caption="CONCEPTO" FieldName="DesConcepto" VisibleIndex="1" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                        <dx:BootstrapGridViewDataColumn Caption="HORARIOS" FieldName="Horario" VisibleIndex="2" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                        <dx:BootstrapGridViewDataColumn Caption="NACIONAL" FieldName="MontoMXN" VisibleIndex="3" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                        <dx:BootstrapGridViewDataColumn Caption="INTERNACIONAL" FieldName="MontoUSD" VisibleIndex="4" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                    </Columns>
                </dx:BootstrapGridView>
            </div>
            <div class="col-md-4">
                 <dx:BootstrapGridView ID="gvInternacionales" runat="server">
                    <SettingsSearchPanel Visible="false" ShowApplyButton="false" />
                    <Settings ShowGroupPanel="false" ShowFilterRowMenu="false" />
                    <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                    <Columns>
                                    
                        <dx:BootstrapGridViewDataColumn Caption="CONCEPTO" FieldName="CONCEPTO" VisibleIndex="1" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                        <dx:BootstrapGridViewDataColumn Caption="INTERNACIONAL" FieldName="INTERNACIONAL" VisibleIndex="2" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />

                    </Columns>
                </dx:BootstrapGridView>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <dx:BootstrapGridView ID="gvAdicionales" runat="server" KeyFieldName="IdParametro">
                    <SettingsSearchPanel Visible="false" ShowApplyButton="false" />
                    <Settings ShowGroupPanel="false" ShowFilterRowMenu="false" />
                    <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                    <Columns>        
                        <dx:BootstrapGridViewDataColumn Caption="CONCEPTO MANUAL" FieldName="DesConcepto" VisibleIndex="1" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                        <dx:BootstrapGridViewDataColumn Caption="MONEDA" FieldName="Valor" VisibleIndex="2" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                        <dx:BootstrapGridViewDataColumn Caption="IMPORTE" FieldName="Importe" VisibleIndex="3" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                    </Columns>
                </dx:BootstrapGridView>
            </div>
            <div class="col-md-6">
                <dx:BootstrapGridView ID="gvConteoDias" runat="server">
                    <SettingsSearchPanel Visible="false" ShowApplyButton="false" />
                    <Settings ShowGroupPanel="false" ShowFilterRowMenu="false" />
                    <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                    <Columns>
                                    
                        <dx:BootstrapGridViewDataColumn Caption="DIA" FieldName="Dia" VisibleIndex="1" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                        <dx:BootstrapGridViewDataColumn Caption="DESAYUNO" FieldName="Desayuno" VisibleIndex="2" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                        <dx:BootstrapGridViewDataColumn Caption="COMIDA" FieldName="Comida" VisibleIndex="3" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />
                        <dx:BootstrapGridViewDataColumn Caption="CENA" FieldName="Cena" VisibleIndex="4" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />

                    </Columns>
                </dx:BootstrapGridView>
            </div>
        </div>


        <br />
        <div id="divViaticos" runat="server"></div>
        <br />
        <div class="row">
            <div class="col-md-12" style="margin-left: -15px; width: 102%;">
                <asp:UpdatePanel ID="upaVuelos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-sm-12">
                            <dx:BootstrapGridView ID="gvVuelos" runat="server" KeyFieldName="LegId">
                                <SettingsSearchPanel Visible="false" ShowApplyButton="false" />
                                <Settings ShowGroupPanel="false" ShowFilterRowMenu="false" />
                                <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                <SettingsPager PageSize="20"></SettingsPager>
                                <Columns>
                                    <%--<dx:BootstrapGridViewDataColumn Caption="Trip" FieldName="Trip" VisibleIndex="1" HorizontalAlign="Center" CssClasses-DataCell="dataCell" />--%>
                                    <%--<dx:BootstrapGridViewDataColumn Caption="LegId" FieldName="LegId" VisibleIndex="1" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>--%>
                                    <%--<dx:BootstrapGridViewDataColumn Caption="Origen" FieldName="POD" VisibleIndex="2" HorizontalAlign="Center" CssClasses-DataCell="dataCell">
                                    </dx:BootstrapGridViewDataColumn>--%>
                                    <%--<dx:BootstrapGridViewDataColumn Caption="Destino" FieldName="POA" VisibleIndex="3" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>--%>

                                    <dx:BootstrapGridViewDataColumn Caption="Trip" FieldName="Trip" Visible="true" VisibleIndex="1" HorizontalAlign="Center" SortOrder="Ascending">
                                        <DataItemTemplate>
                                            <div>
                                                <asp:Label ID="readTrip" runat="server" Text='<%# Eval("Trip") %>' CssClass="dataCell"></asp:Label>
                                            </div>
                                        </DataItemTemplate>
                                        <CssClasses HeaderCell="spa"/>
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn Caption="Origen" FieldName="POD" Visible="true" VisibleIndex="2" HorizontalAlign="Center" SortOrder="Ascending">
                                        <DataItemTemplate>
                                            <div>
                                                <asp:Label ID="readPOD" runat="server" Text='<%# Eval("POD") %>' CssClass="dataCell"></asp:Label>
                                            </div>
                                        </DataItemTemplate>
                                        <CssClasses HeaderCell="spa"/>
                                    </dx:BootstrapGridViewDataColumn>

                                    <dx:BootstrapGridViewDataColumn Caption="Destino" FieldName="POA" Visible="true" VisibleIndex="3" HorizontalAlign="Center" SortOrder="Ascending">
                                        <DataItemTemplate>
                                            <div>
                                                <asp:Label ID="readPOA" runat="server" Text='<%# Eval("POA") %>' CssClass="dataCell"></asp:Label>
                                            </div>
                                        </DataItemTemplate>
                                        <CssClasses HeaderCell="spa" />
                                    </dx:BootstrapGridViewDataColumn>


                                     <dx:BootstrapGridViewDataColumn Caption="Fecha de Salida" FieldName="FechaSalida" Visible="true" VisibleIndex="4" HorizontalAlign="Center" SortOrder="Ascending">
                                        <DataItemTemplate>
                                            <div>
                                                <asp:Label ID="readFechaSalida" runat="server" Text='<%# Eval("FechaSalida") %>' CssClass="dataCell"></asp:Label>
                                            </div>
                                        </DataItemTemplate>
                                        <CssClasses HeaderCell="spa" />
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn Caption="Fecha de Llegada" FieldName="FechaLlegada" Visible="true" VisibleIndex="5" HorizontalAlign="Center" SortOrder="Ascending">
                                        <DataItemTemplate>
                                            <div>
                                                <asp:Label ID="readFechaLlegada" runat="server" Text='<%# Eval("FechaLlegada") %>' CssClass="dataCell"></asp:Label>
                                            </div>
                                        </DataItemTemplate>
                                        <CssClasses HeaderCell="spa" />
                                    </dx:BootstrapGridViewDataColumn>

                                    <dx:BootstrapGridViewDataColumn Caption="CheckIn" FieldName="CheckIn" Visible="true" VisibleIndex="6" HorizontalAlign="Center" SortOrder="Ascending">
                                        <DataItemTemplate>
                                            <div>
                                                <asp:Label ID="readCheckIn" runat="server" Text='<%# Eval("CheckIn") %>' CssClass="dataCell"></asp:Label>
                                            </div>
                                        </DataItemTemplate>
                                        <CssClasses HeaderCell="spa" />
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn Caption="CheckOut" FieldName="CheckOut" Visible="true" VisibleIndex="6" HorizontalAlign="Center" SortOrder="Ascending">
                                        <DataItemTemplate>
                                            <div>
                                                <asp:Label ID="readCheckOut" runat="server" Text='<%# Eval("CheckOut") %>' CssClass="dataCell"></asp:Label>
                                            </div>
                                        </DataItemTemplate>
                                        <CssClasses HeaderCell="spa" />
                                    </dx:BootstrapGridViewDataColumn>


                                    <%--<dx:BootstrapGridViewDataColumn Caption="Fecha de Salida" FieldName="FechaSalida" VisibleIndex="4" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>
                                    <dx:BootstrapGridViewDataColumn Caption="Fecha de Llegada" FieldName="FechaLlegada" VisibleIndex="5" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>--%>

                                    <%--<dx:BootstrapGridViewDataColumn Caption="Check In" FieldName="CheckIn" VisibleIndex="7" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>
                                    <dx:BootstrapGridViewDataColumn Caption="Check Out" FieldName="CheckOut" VisibleIndex="8" HorizontalAlign="Center" CssClasses-DataCell="dataCell"/>--%>
                                </Columns>
                                <SettingsPager Position="Bottom">
                                    <PageSizeItemSettings Items="20, 50, 100"></PageSizeItemSettings>
                                </SettingsPager>
                                <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                                <Settings ShowGroupPanel="True" />
                            </dx:BootstrapGridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <dx:BootstrapFormLayout runat="server">
                <Items>                 
                    <dx:BootstrapLayoutItem HorizontalAlign="Right" ShowCaption="False" ColSpanMd="12">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:BootstrapButton ID="btnGuardarPeriodo" runat="server" Text="Guardar Período" SettingsBootstrap-RenderOption="Primary" AutoPostBack="true" OnClick="btnGuardarPeriodo_Click" />
                                <%--<dx:BootstrapButton ID="btnAutorizar" runat="server" Text="Autorizar" SettingsBootstrap-RenderOption="Success" AutoPostBack="true" OnClick="btnAutorizar_Click" />--%>
                                <dx:BootstrapButton ID="btnCancelar" runat="server" Text="Regresar" SettingsBootstrap-RenderOption="Warning" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e) { document.location.reload(); }" />
                                </dx:BootstrapButton>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:BootstrapLayoutItem>
                </Items>
            </dx:BootstrapFormLayout>
        </div>
    </asp:Panel>


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

</asp:Content>
