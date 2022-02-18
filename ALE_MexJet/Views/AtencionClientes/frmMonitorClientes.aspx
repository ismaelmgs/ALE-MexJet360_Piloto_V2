<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmMonitorClientes.aspx.cs" Inherits="ALE_MexJet.Views.AtencionClientes.frmMonitorClientes" UICulture="es" Culture="es-MX" %>

<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css" media="screen">
        / Accordion / fieldset {
            border: 1px solid #757474;
        }

        .accordionContent {
            border: 1px solid #757474;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
        }

        .accordionHeader {
            border: 1px solid #472777;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            text-align: left;
            background-color: #757474;
            color: #FFFFFF;
            text-decoration: underline;
        }

            .accordionHeader a {
                text-decoration: underline;
            }

                .accordionHeader a:hover {
                    text-decoration: underline;
                }

        .accordionHeaderSelected {
            border: 1px solid #472777;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            text-align: left;
            background-color: #565656;
            color: #FFFFFF;
            text-decoration: underline;
        }

            .accordionHeaderSelected a {
                text-decoration: underline;
            }

                .accordionHeaderSelected a:hover {
                    text-decoration: underline;
                }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="~/../../../JS/jquery/jquery-1.8.3.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtTextoBusquedaHabilitar();
            $('.combo').change(txtTextoBusquedaHabilitar);
        });
        function pageLoad(sender, args) {

            txtTextoBusquedaHabilitar();
            $('.combo').change(txtTextoBusquedaHabilitar);
        };
        function txtTextoBusquedaHabilitar() {
            var filtro = $(".combo").find(':selected').val();
            if (filtro == 0) {
                $(".txtBusqueda").attr('disabled', '-1');
                $(".txtBusqueda").val('');
            }
            if (filtro == 1 || filtro == 2 || filtro == 3)
                $(".txtBusqueda").removeAttr('disabled');
        }

        function OnSaveClick(s, e) {
            gvArea.UpdateEdit();



        }


        function Confirmacion() {

            var seleccion = confirm("acepta el mensaje ?");

            if (seleccion)
                alert("se acepto el mensaje");
            else
                alert("NO se acepto el mensaje");

            //usado para que no haga postback el boton de asp.net cuando 
            //no se acepte el confirm
            return seleccion;
        }


        function Hide() { ppAlert.Hide(); }

    </script>
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Monitor de Clientes</span>
                    </div>
                </div>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <div class="well-g">
                    <div class="row">
                        <div class="col-lg-12">

                            <fieldset class="Personal">
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                                </legend>
                                <div class="col-sm-12">
                                    <div class="col-lg-4">
                                        <asp:TextBox ID="txtTextoBusqueda" CssClass="txtBusqueda" placeholder ="Ingrese la información a buscar" runat="server" Width="180px" style="font-size:12px;"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4">
                                        <!-- <img src="../../img/iconos/flecha.png" /> -->
                                        <asp:DropDownList runat="server" CssClass="combo" ID="ddlTipoBusqueda" style="font-size:12px;">
                                            <asp:ListItem Text="[Sin Filtro]" Value="0" Selected="true"></asp:ListItem>
                                            <asp:ListItem Text="Código Cliente" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Clave Contrato" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <dx:ASPxButton ID="ASPxButton1" Text="Buscar" runat="server" Theme="Office2010Black" OnClick="btnBuscar_Click"></dx:ASPxButton>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="upaAccordion" runat="server" OnUnload="Unnamed_Unload">
                        <ContentTemplate>

                            <cc1:Accordion ID="AcPrincipal" runat="Server" HeaderCssClass="accordionHeader"
                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                AutoSize="None" FadeTransitions="true" TransitionDuration="150" FramesPerSecond="40"
                                RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%" min-height="450px"
                                SelectedIndex="0">
                                <Panes>
                                    <cc1:AccordionPane ID="aBusqueda" runat="server" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                            <asp:Label ID="lblBusquedaClientes" runat="server" Text="Búsqueda de Clientes"></asp:Label>
                                        </Header>
                                        <Content>
                                            <div id="divContent">
                                                <asp:UpdatePanel ID="upaBusqCliente" runat="server" OnUnload="Unnamed_Unload">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="Panel1" runat="server" align="center">
                                                            <div>


                                                                <br />
                                                                <div class="row">
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-md-12" style="margin-left: -15px; width: 100%;">
                                                                        <div class="col-sm-12">
                                                                            <dx:ASPxGridView ID="gvMonitor" runat="server" AutoGenerateColumns="False" Font-Size="Small" KeyFieldName="IdContrato"
                                                                                Theme="Office2010Black" Width="100%" OnCustomButtonCallback="gvMonitor_CustomButtonCallback">
                                                                                <ClientSideEvents EndCallback="function (s, e) {
                                                                                                        if (s.cpShowPopup)
                                                                                                        {
                                                                                                            delete s.cpShowPopup;
                                                                                                            lbl.SetText(s.cpText);
                                                                                                            popup.Show();
                                                                                                        }
                                                                                                    }" />
                                                                                <Columns>
                                                                                    <dx:GridViewDataTextColumn Caption="Código Cliente" FieldName="CodigoCliente" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Contrato" FieldName="ClaveContrato" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataComboBoxColumn Caption="Tipo Contrato" FieldName="TipoContrato" VisibleIndex="2">
                                                                                        <EditFormSettings Visible="False" VisibleIndex="2" />
                                                                                    </dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Grupo de Modelo" FieldName="GrupoModelo" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Vendedor" FieldName="Vendedor" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Ejecutivo" FieldName="Ejecutivo" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataColumn Caption="Acciones">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxButton ID="btnDetalle" runat="server" Theme="Office2010Black" CommandName='<%# Eval("IdContrato") %>' CommandArgument='<%# Eval("CodigoCliente") %>' Text="Detalle" OnClick="btnDetalle_Click"></dx:ASPxButton>
                                                                                            <dx:ASPxButton ID="btnContactos" runat="server" Theme="Office2010Black" CommandName='<%# Eval("IdCliente") %>' CommandArgument='<%# Eval("CodigoCliente") %>' Text="Contactos" OnClick="btnContactos_Click"></dx:ASPxButton>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior ConfirmDelete="True" />
                                                                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                                                                                <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                                                                <SettingsPopup>
                                                                                    <EditForm HorizontalAlign="WindowCenter" VerticalAlign="Middle" Width="400px" />
                                                                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="Middle" />
                                                                                </SettingsPopup>
                                                                                <SettingsCommandButton>
                                                                                    <UpdateButton Text="Guardar">
                                                                                    </UpdateButton>
                                                                                    <CancelButton></CancelButton>
                                                                                    <EditButton Text="Detalles2">
                                                                                        <Image Height="20px" ToolTip="Modificar" Width="20px">
                                                                                        </Image>
                                                                                    </EditButton>
                                                                                    <DeleteButton>
                                                                                        <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                                                                        </Image>
                                                                                    </DeleteButton>
                                                                                </SettingsCommandButton>
                                                                            </dx:ASPxGridView>
                                                                            <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvCliente">
                                                                            </dx:ASPxGridViewExporter>
                                                                        </div>


                                                                    </div>
                                                                </div>

                                                                </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </asp:Panel>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </div>
                                        </Content>
                                    </cc1:AccordionPane>
                                    <cc1:AccordionPane ID="ADetalleContrato" runat="server" HeaderCssClass="accordionHeader" Visible="false"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                            <asp:Label ID="lblClaveCliente" runat="server" Text="Clave de Cliente: "></asp:Label>
                                            <asp:Label ID="lblResClaveCliente" runat="server" Text=""></asp:Label>
                                        </Header>
                                        <Content>
                                            <div id="divDatosCliente">
                                                <asp:Panel ID="Panel3" runat="server" align="center">
                                                    <div>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="panel panel-primary" style="width: 98%;">
                                                                                <div class="panel-heading" style="/* permalink - use to edit and share this gradient: http://colorzilla.com/gradient-editor/#45484d+0,565656+100 */
                                                                                        background: rgb(69,72,77); /* old browsers */
                                                                                        /* ie9 svg, needs conditional override of 'filter' to 'none' */
                                                                                        background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iIzQ1NDg0ZCIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiM1NjU2NTYiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+); background: -moz-linear-gradient(top,  rgba(69,72,77,1) 0%, rgba(86,86,86,1) 100%); /* ff3.6+ */
                                                                                        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(69,72,77,1)), color-stop(100%,rgba(86,86,86,1))); /* chrome,safari4+ */
                                                                                        background: -webkit-linear-gradient(top,  rgba(69,72,77,1) 0%,rgba(86,86,86,1) 100%); /* chrome10+,safari5.1+ */
                                                                                        background: -o-linear-gradient(top,  rgba(69,72,77,1) 0%,rgba(86,86,86,1) 100%); /* opera 11.10+ */
                                                                                        background: -ms-linear-gradient(top,  rgba(69,72,77,1) 0%,rgba(86,86,86,1) 100%); /* ie10+ */
                                                                                        background: linear-gradient(to bottom,  rgba(69,72,77,1) 0%,rgba(86,86,86,1) 100%); /* w3c */
                                                                                        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#45484d', endColorstr='#565656',GradientType=0 ); /* ie6-8 */">
                                                                                    Datos del Contrato
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <div class="col-md-4">
                                                                                        <dx:ASPxLabel runat="server" ID="lblCotrato" Text="Contrato:"></dx:ASPxLabel>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <dx:ASPxLabel runat="server" ID="lblTipoContrato" Text="Tipo de Contrato:"></dx:ASPxLabel>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <dx:ASPxLabel runat="server" ID="lblTipoEquipo" Text="Tipo de Equipo:"></dx:ASPxLabel>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-12">
                                                                                    <dx:ASPxLabel runat="server" Text="Preferencias"></dx:ASPxLabel>
                                                                                    <dx:ASPxMemo runat="server" ID="txtObservacion" Width="103%" ReadOnly="true" Height="50px" Style="margin-left: -30px;"></dx:ASPxMemo>
                                                                                    <br />
                                                                                    <dx:ASPxLabel runat="server" Text="Notas"></dx:ASPxLabel>
                                                                                    <dx:ASPxMemo runat="server" ID="txtNota" Width="103%" Height="50px" ReadOnly="true" Style="margin-left: -30px;"></dx:ASPxMemo>
                                                                                    <br />
                                                                                    <dx:ASPxLabel runat="server" Text="Otros"></dx:ASPxLabel>
                                                                                    <dx:ASPxMemo runat="server" ID="txtOtros" Width="103%" Height="50px" ReadOnly="true" Style="margin-left: -30px;"></dx:ASPxMemo>
                                                                                    <br />
                                                                                    <br />
                                                                                </div>
                                                                                <br />
                                                                                <div class="aspdetail">
                                                                                    <dx:ASPxLabel runat="server" ID="lblSolicitudes" Text="Solicitudes de Vuelo" Font-Size="Medium"></dx:ASPxLabel>
                                                                                    <br />
                                                                                    <br />
                                                                                    <div class="">
                                                                                        <dx:ASPxButton ID="btnNuevaSolicitud" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevaSolicitud_Click"></dx:ASPxButton>
                                                                                    </div>
                                                                                    <br />
                                                                                    <dx:ASPxGridView ID="gvDetalle" ClientInstanceName="grid" runat="server" OnRowCommand="gvDetalle_RowCommand" OnHtmlRowCreated="gvDetalle_HtmlRowCreated"
                                                                                        KeyFieldName="IdSolicitud" Width="100%" Theme="Office2010Black">
                                                                                        <ClientSideEvents EndCallback="function (s, e) {
                                                                                            if (s.cpShowPopup)
                                                                                            {
                                                                                                delete s.cpShowPopup;
                                                                                                lbl.SetText(s.cpText);
                                                                                                popup.Show();
                                                                                            }
                                                                                        }" />
                                                                                        <Columns>
                                                                                            <dx:GridViewDataTextColumn Caption="No. Solicitud" FieldName="IdSolicitud" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Fecha Vuelo" FieldName="FechaVuelo" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="TRIP" FieldName="TRIP" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataComboBoxColumn Caption="Ruta" FieldName="Ruta" VisibleIndex="2">
                                                                                                <EditFormSettings Visible="False" VisibleIndex="2" />
                                                                                            </dx:GridViewDataComboBoxColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Equipo" FieldName="Equipo" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                             <dx:GridViewDataTextColumn Caption="Estatus" FieldName="Estatus" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                             <dx:GridViewDataTextColumn Caption="Usuario" FieldName="Usuario" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataDateColumn Caption="Fecha" FieldName="FechaCreacion" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                            </dx:GridViewDataDateColumn>
                                                                                            
                                                                                            <dx:GridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="5">
                                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                <DataItemTemplate>
                                                                                                    <dx:ASPxButton Text="Editar" Theme="Office2010Black" ID="btn" runat="server" CommandArgument='<%# Eval("IdSolicitud") %>' CommandName="Detalle" AutoPostBack="true" ToolTip="Detalle">
                                                                                                    </dx:ASPxButton>
                                                                                                    <dx:ASPxButton Text="Cancelar" Theme="Office2010Black" ID="btnEliminarSolicitud" runat="server" CommandArgument='<%# Eval("IdSolicitud") %>' CommandName="Eliminar" AutoPostBack="true" ToolTip="Eliminar">
                                                                                                        <ClientSideEvents Click="function(s, e){  e.processOnServer = confirm('¿Está seguro que desea eliminar el registro?');}"/>
                                                                                                    </dx:ASPxButton>
                                                                                                </DataItemTemplate>
                                                                                                <EditFormSettings Visible="false" />
                                                                                            </dx:GridViewDataColumn>

                                                                                        </Columns>
                                                                                        <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                                                                        <Templates>
                                                                                            <DetailRow>
                                                                                                <dx:ASPxGridView ID="gvPiernas" ClientInstanceName="grid" runat="server"
                                                                                                    KeyFieldName="idSolicitud" Width="50%" Theme="Office2010Black" O
                                                                                                    nBeforePerformDataSelect="gvPiernas_BeforePerformDataSelect">
                                                                                                    <Columns>
                                                                                                        <dx:GridViewDataColumn Caption="Pierna" FieldName="Pierna" VisibleIndex="0" />
                                                                                                        <dx:GridViewDataColumn Caption="Pax" FieldName="NoPax" VisibleIndex="1" />
                                                                                                        <dx:GridViewDataColumn Caption="Acciones" VisibleIndex="2">
                                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                            <DataItemTemplate>
                                                                                                                <dx:ASPxButton ID="btnNewCaso" HorizontalAlign="Right" runat="server" Theme="Office2010Black" Text="Agregar Caso" CommandName='<%# Eval("IdTramo") %>' CommandArgument='<%# Eval("Pierna") %>' OnClick="btnNewCaso_Click"></dx:ASPxButton>
                                                                                                            </DataItemTemplate>
                                                                                                        </dx:GridViewDataColumn>
                                                                                                    </Columns>
                                                                                                </dx:ASPxGridView>
                                                                                            </DetailRow>
                                                                                        </Templates>
                                                                                        <SettingsDetail ShowDetailRow="true" />
                                                                                        <SettingsPager Position="TopAndBottom">
                                                                                            <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                                                            </PageSizeItemSettings>
                                                                                        </SettingsPager>
                                                                                    </dx:ASPxGridView>
                                                                                </div>
                                                                                <br />
                                                                                <br />
                                                                                <div class="">
                                                                                    <dx:ASPxLabel runat="server" ID="lblCasos" Text="Casos" Font-Size="Medium"></dx:ASPxLabel>
                                                                                    <br />
                                                                                    <br />
                                                                                    <dx:ASPxGridView ID="gvCasos" ClientInstanceName="grid" runat="server"
                                                                                        KeyFieldName="NoCaso" Width="100%" Theme="Office2010Black"
                                                                                        OnStartRowEditing="ASPxGridView1_StartRowEditing"
                                                                                        OnCellEditorInitialize="gvCasos_CellEditorInitialize"
                                                                                        Styles-LoadingDiv-Opacity="90"
                                                                                        OnRowCommand="gvCasos_RowCommand">
                                                                                        <Columns>
                                                                                            <dx:GridViewDataTextColumn Caption="No. Caso" FieldName="NoCaso" ShowInCustomizationForm="True" Width="20%" VisibleIndex="0">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="No. Solicitud" FieldName="NoSolicitud" ShowInCustomizationForm="True" VisibleIndex="1" Width="20%">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Tipo Caso" FieldName="TipoCaso" ShowInCustomizationForm="True" VisibleIndex="2" Width="20%">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Pierna" FieldName="Pierna" ShowInCustomizationForm="True" VisibleIndex="3" Width="20%">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataColumn Caption="ACCIONES" Width="20%">
                                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                <DataItemTemplate>
                                                                                                     <dx:ASPxButton Text="Editar" Theme="Office2010Black" ID="btnPreferencias" CommandArgument='<%# Eval("NoCaso") %>' runat="server" CommandName="ACTUALIZAR" AutoPostBack="true" ToolTip="Actualiza Caso">
                                                                                                    </dx:ASPxButton>
                                                                                                    <dx:ASPxButton Text="Cancelar" Theme="Office2010Black" ID="btn" runat="server" CommandArgument='<%# Eval("NoCaso") %>' CommandName="ELIMINAR" AutoPostBack="true" ToolTip="Eliminar Caso">
                                                                                                        <ClientSideEvents Click="function(s, e){  e.processOnServer = confirm('¿Está seguro que desea eliminar el registro?');}"/>
                                                                                                    </dx:ASPxButton>
                                                                                                </DataItemTemplate>
                                                                                            </dx:GridViewDataColumn>

                                                                                        </Columns>
                                                                                         <SettingsPager Position="TopAndBottom" Visible="true">
                                                                                            <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                                                            </PageSizeItemSettings>
                                                                                        </SettingsPager>
                                                                                        <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                                                                        <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                                                                                        <SettingsPopup>
                                                                                            <EditForm HorizontalAlign="Center" VerticalAlign="Above" Modal="true" CloseOnEscape="True" Width="400px" />
                                                                                        </SettingsPopup>
                                                                                    </dx:ASPxGridView>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </Content>
                                    </cc1:AccordionPane>
                                </Panes>
                            </cc1:Accordion>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

    <dx:ASPxPopupControl ID="popup" runat="server" ClientInstanceName="popup" CloseAction="CloseButton"  ShowCloseButton="true" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Below" HeaderText="Registro Modificado" AllowDragging="True" Theme="Office2010Black">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">

                <dx:ASPxPanel ID="ASPxPanel2" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxImage ID="ASPxImage2" runat="server" ShowLoadingImage="true" ImageUrl="~/img/iconos/Information2.ico"></dx:ASPxImage>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbl" runat="server" Theme="Office2010Black" ClientInstanceName="lbl" Text="ASPxLabel"></dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btOK" runat="server" Text="Salir" Theme="Office2010Black" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0"  OnClick="btOK_Click">
                                            <ClientSideEvents Click="function(){  ASPxClientEdit.ClearGroup('Grupo1');  popup.Hide(); ppAlert.Hide(); popupContactos.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="ppAlert" runat="server" ClientInstanceName="ppAlert" Text="" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true" Width="700px">
        <ClientSideEvents />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="col-md-12" style="padding-left: 0px; background-color: #B7B7B7; border-radius: 10px; padding: 3px; margin: 0 auto;">
                                <div class="cabecera">
                                    <br />
                                    <div class="col-sm-4">
                                        <dx:ASPxLabel runat="server" ID="pnllblCliente" Text="Cliente:"></dx:ASPxLabel>
                                    </div>
                                    <div class="col-sm-4">
                                        <dx:ASPxLabel runat="server" ID="pnllblContrato" Text="Contrato:"></dx:ASPxLabel>
                                    </div>
                                    <div class="col-sm-4">
                                        <dx:ASPxLabel runat="server" ID="pnllblTipoContrato" Text="Tipo Contrato:"></dx:ASPxLabel>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="col-sm-4">
                                        <dx:ASPxLabel runat="server" ID="pnllblTipoEquipo" Text="Tipo Equipo:"></dx:ASPxLabel>
                                    </div>
                                    <div class="col-sm-4">
                                        <dx:ASPxLabel runat="server" ID="pnllblCaso" Text="Caso:"></dx:ASPxLabel>
                                    </div>
                                    <div class="col-sm-4">
                                        <dx:ASPxLabel runat="server" ID="pnllblRuta" Text="Ruta:"></dx:ASPxLabel>
                                    </div>
                                    <br />
                                    <br />
                                </div>
                            </div>
                            <div class="cuerpo" style="padding-left: 0px;">
                                <div class="form-group col-md-12" style="padding-left: 0px;">
                                    <br />
                                    <br />
                                    <table width="100%" border="0">
                                        <tr>
                                            <td width="33%" style="text-align: left;" valign="top">
                                                <dx:ASPxLabel runat="server" ID="lblTipoCaso" Text="Tipo de Caso:" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td width="33%" style="text-align: left;">
                                                &nbsp;
                                            </td>
                                            <td width="34%" align="center">
                                                <div style="margin-left: 30px;">
                                                    <dx:ASPxComboBox runat="server" ID="ddlTipoCaso" Theme="Office2010Black" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCaso_SelectedIndexChanged">
                                                        <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="Grupo1">
                                                            <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:Panel runat="server" ID="pnlMotivo" Visible="false">
                                    <div class="form-group col-md-12" style="padding-left: 0px;">
                                        <table width="101%" border="0">
                                            <tr>
                                                <td width="33%" style="text-align: left;" valign="top">
                                                    <dx:ASPxLabel runat="server" ID="pmllblMotivo" Text="Motivo:" Theme="Office2010Black"></dx:ASPxLabel>
                                                </td>
                                                <td width="33%" style="text-align: left;">&nbsp;</td>
                                                <td width="34%" align="rigth" style="padding-left: 37px;">
                                                    <dx:ASPxComboBox runat="server" ID="ddlMotivo" Theme="Office2010Black" AutoPostBack="true" OnSelectedIndexChanged="ddlMotivo_SelectedIndexChanged">
                                                        <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="Grupo1">
                                                            <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlMinutos" Visible="false">
                                    <div class="form-group col-md-12" style="padding-left: 0px;">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td width="33%" style="text-align: left;">
                                                    <dx:ASPxLabel runat="server" ID="pnllblMinutos" Text="Minutos:" Theme="Office2010Black"></dx:ASPxLabel>
                                                </td>
                                                <td width="50%" style="text-align: left;">&nbsp;</td>
                                                <td width="34%" align="left"><!-- este no se deja mover -->
                                                    <%--<dx:ASPxSpinEdit ID="spnMinutos" runat="server" Number="0" ValidationSettings-RequiredField-IsRequired="true" NumberType="Integer" MinValue="0" MaxValue="1000">--%>
                                                    <dx:ASPxSpinEdit ID="spnMinutos" runat="server" Number="0" ValidationSettings-RequiredField-IsRequired="true" NumberType="Integer"
                                                        MinValue="0" MaxValue="1000" >
                                                    </dx:ASPxSpinEdit>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlArea" Visible="false">
                                    <div class="form-group col-md-12" style="padding-left: 0px;">
                                        <table width="101%" border="0">
                                            <tr>
                                                <td width="33%" style="text-align: left;" valign="top">
                                                    <dx:ASPxLabel runat="server" ID="lblArea" Text="Área:" Theme="Office2010Black"></dx:ASPxLabel>
                                                </td>
                                                <td width="33%" style="text-align: left;">&nbsp;</td>
                                                <td width="34%" align="rigth" style="padding-left:37px;">
                                                    <dx:ASPxComboBox runat="server" ID="ddlArea" Theme="Office2010Black" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                                        <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="Grupo1">
                                                            <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <table width="101%" border="0">
                                    <tr>
                                        <asp:Panel runat="server" ID="pnlOtorgado" Visible="false">
                                            <td width="15%" style="text-align: left;">
                                                <dx:ASPxLabel runat="server" ID="lblOtorgado" Text="Otorgado:" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td width="15%" style="text-align: left;">
                                                <dx:ASPxRadioButtonList ID="rblOtorgado" runat="server" Theme="Office2010Black" ToolTip="Otorgado"
                                                        ClientInstanceName="rblOtorgado" >
                                              <Items>
                                                  <dx:ListEditItem  Text="No" Value ="0"/>
                                                  <dx:ListEditItem  Text="Si" Value ="1"/>
                                              </Items>
                                                <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="Grupo1">
                                                    <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                </ValidationSettings>
                                                </dx:ASPxRadioButtonList>
                                               <%-- <dx:ASPxRadioButton runat="server" ID="rbtOtorgadoSi" ClientInstanceName="rbtOtorgadoSi" Text="Si">
                                                    <ClientSideEvents CheckedChanged="function(){ rbtOtorgadoNo.SetChecked(false); }" />
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="Grupo1">
                                                            <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                        </ValidationSettings>
                                                </dx:ASPxRadioButton>
                                                <dx:ASPxRadioButton runat="server" ID="rbtOtorgadoNo"  ClientInstanceName="rbtOtorgadoNo" Text="No">
                                                    <ClientSideEvents CheckedChanged="function(){ rbtOtorgadoSi.SetChecked(false); }" />
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="Grupo1">
                                                            <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                        </ValidationSettings>
                                                </dx:ASPxRadioButton>--%>
                                            </td>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="pnlSolicitud" Visible="false">
                                            <td width="15%" style="text-align:right;" align="rigth">
                                                <dx:ASPxLabel runat="server" ID="pnllblSolicitud" Text="Solicitud:" Theme="Office2010Black"></dx:ASPxLabel>
                                            </td>
                                            <td width="34%" style="text-align: left; padding-left: 135px;">
                                                <dx:ASPxComboBox runat="server" ID="ddlSolicitud" Theme="Office2010Black" OnSelectedIndexChanged="ddlSolicitud_SelectedIndexChanged">
                                                    <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" SetFocusOnError="True" ValidationGroup="Grupo1">
                                                            <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                        </asp:Panel>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <asp:Panel runat="server" ID="pnlDetalle" Visible="false">
                                    <div class="form-group col-lg-12" style="padding-left: 0px;">
                                        <div class="col-lg-2">
                                            <dx:ASPxLabel runat="server" ID="lblDetalle" Text="Detalle:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-lg-10">
                                            <dx:ASPxMemo runat="server" ID="mDetalle" Width="100%" Height="100px"></dx:ASPxMemo>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlAccionCorrectiva" Visible="false">
                                    <div class="form-group col-lg-12" style="padding-left: 0px;">
                                        <div class="col-lg-2">
                                            <dx:ASPxLabel runat="server" ID="pnllblAccionCorrec" Text="Acción Correctiva:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-lg-10">
                                            <dx:ASPxMemo runat="server" ID="mAccionCorrectiva" Width="100%" Height="100px"></dx:ASPxMemo>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-md-12">
                                    <table width="100%">
                                        <tr>
                                            <td width="50%" align="center">
                                                <dx:ASPxButton runat="server" ID="btnGuardarCaso" OnClick="btnGuardarCaso_Click" Text="Guardar" Theme="Office2010Black" ValidationGroup="Grupo1">
                                                    <ClientSideEvents  Click="function(){ if(ASPxClientEdit.ValidateGroup('Grupo1')) {  popup.Hide(); ppAlert.Hide(); popupContactos.Hide(); }}"/>
                                                </dx:ASPxButton>
                                            </td>
                                            <td width="50%" align="center">
                                                <dx:ASPxButton runat="server" ClientInstanceName="btnSalir" Text="Salir" Theme="Office2010Black" ID="btnSalir" OnClick="btnSalir_Click">
                                                    <ClientSideEvents Click="function(){  ASPxClientEdit.ClearGroup('Grupo1');  popup.Hide(); ppAlert.Hide(); popupContactos.Hide(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>



                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="popupContactos" runat="server" ClientInstanceName="popupContactos" Text="" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Contactos" AllowDragging="true" ShowCloseButton="true" Width="700px">
        <ClientSideEvents />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel4" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxGridView ID="gvContactos" ClientInstanceName="grid" runat="server"
                                KeyFieldName="IdContacto" Width="100%" Theme="Office2010Black"
                                Styles-LoadingDiv-Opacity="90">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Nombre" FieldName="Nombre" ShowInCustomizationForm="True" VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Correo Electronico" FieldName="CorreoElectronico" ShowInCustomizationForm="True" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Tel. Oficina" FieldName="TelOficina" ShowInCustomizationForm="True" VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Tel. Movil" FieldName="TelMovil" ShowInCustomizationForm="True" VisibleIndex="3">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Otro Movil" FieldName="OtroTel" ShowInCustomizationForm="True" VisibleIndex="4">
                                    </dx:GridViewDataTextColumn>
                                </Columns>

                                <Styles>
                                    <LoadingDiv Opacity="90"></LoadingDiv>
                                </Styles>
                            </dx:ASPxGridView>
                            <br />
                            <br />
                            <div class="text-right">
                                <dx:ASPxButton HorizontalAlign="Left" runat="server" ID="btnSalirContactos" Text="Salir" Theme="Office2010Black">
                                    <ClientSideEvents Click="function() {popupContactos.Hide(); }" />
                                </dx:ASPxButton>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>


