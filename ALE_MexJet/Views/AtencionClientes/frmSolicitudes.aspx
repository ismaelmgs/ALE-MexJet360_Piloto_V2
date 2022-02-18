<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmSolicitudes.aspx.cs" Inherits="ALE_MexJet.Views.AtencionClientes.frmSolicitudes" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function cbTimeEditFormat_SelectedIndexChanged(s, e) {
            if (s.GetText() == "Custom") {
                cbTimeEditFormatString.SetEnabled(true);
                e.processOnServer = false;
            }
        }

        function Remplaza(s, e) {
            var x = mNota.GetValue();
            var y = x.replace(/</g, "");
            var z = y.replace(/>/g, "");

            mNota.SetText(z);

        }

        function Selrdbtn(id) {
            var rdBtn = document.getElementById(id);
            var List = document.getElementsByTagName("input");
            for (i = 0; i < List.length; i++) {
                if (List[i].type == "radio" && List[i].id != rdBtn.id) {
                    List[i].checked = false;
                }
            }
        }

        function CerrarModalCorreo() {
            ppEnviarMailTripGuide.Hide();
        }

    </script>
    <style type="text/css" media="screen">
        /* Accordion */

        fieldset {
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
    <div class="row header1">
        <div class="col-md-12">
            <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Solicitudes de Vuelo</span>
        </div>
    </div>
    <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="panel panel-primary">
                    <br />
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
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 5%"></td>
                                <td style="width: 15%">
                                    <dx:ASPxLabel runat="server" ID="lblCliente" Text="Cliente: "></dx:ASPxLabel>
                                </td>
                                <td style="width: 15%">
                                    <dx:ASPxLabel runat="server" ID="lblContrato" Text="Contrato: "></dx:ASPxLabel>
                                </td>
                                <td style="width: 25%">
                                    <dx:ASPxLabel runat="server" ID="lblTipoContrato" Text="Tipo de Contrato: "></dx:ASPxLabel>
                                </td>
                                <td style="width: 25%">
                                    <dx:ASPxLabel runat="server" ID="lblTipoEquipo" Text="Tipo de Equipo: "></dx:ASPxLabel>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel runat="server" ID="lblSolicitud" Text="No. Solicitud: "></dx:ASPxLabel>
                                </td>
                                <td style="width: 5%"></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <dx:ASPxPageControl ID="ASPxPageControl1" ActiveTabIndex="0" runat="server" Width="100%" Height="350px" TabAlign="Justify"
                    EnableTabScrolling="true" Theme="Office2010Black">
                    <TabStyle Paddings-PaddingLeft="50px" Paddings-PaddingRight="50px" />
                    <ContentStyle>
                        <%--OnTabClick="ASPxPageControl1_TabClick"--%>
                        <Paddings PaddingLeft="12px" />
                    </ContentStyle>
                    <TabPages>
                        <dx:TabPage Text="1. Alta de Solicitudes" Name="AltaSolicitud" Enabled="true">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <div class="well-g">

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <b>
                                                    <asp:Label ID="lblSeguimiento" runat="server" Text="Datos Generales de la Solicitud"></asp:Label>
                                                </b>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <br />
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel runat="server" ID="lblContacto" Text="Contacto:*" Theme="Office2010Black">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox runat="server" ID="cmbContacto" Theme="Office2010Black" Width="300px" NullText="Seleccione una opción">
                                                            <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                                <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxLabel runat="server" ID="lblMotivo" Text="Motivo:*" Theme="Office2010Black">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox runat="server" ID="cmbMotivo" Theme="Office2010Black" Width="300px" NullText="Seleccione una opción">
                                                            <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                                <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel runat="server" ID="lblOrigen" Text="Origen:*" Theme="Office2010Black"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox runat="server" ID="cmbOrigen" Theme="Office2010Black" Width="300px" NullText="Seleccione una opción">
                                                            <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                                <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxLabel runat="server" ID="lblEquipo" Text="Tipo de Equipo:*" Theme="Office2010Black"></dx:ASPxLabel>
                                                        <td>
                                                            <dx:ASPxComboBox runat="server" ID="cmbEquipo" Theme="Office2010Black" Width="300px" NullText="Seleccione una opción" AutoPostBack="true" OnSelectedIndexChanged="cmbEquipo_SelectedIndexChanged">
                                                                <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                                    <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                                </ValidationSettings>
                                                            </dx:ASPxComboBox>
                                                            <asp:UpdatePanel runat="server" OnUnload="Unnamed_Unload" UpdateMode="Always">
                                                                <ContentTemplate>
                                                                    <dx:ASPxLabel ID="lblIntercambio" runat="server" Theme="Office2010Black"></dx:ASPxLabel>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>

                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel runat="server" ID="lblEstatus" Text="Estatus:" Theme="Office2010Black"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" OnUnload="Unnamed_Unload">
                                                            <ContentTemplate>
                                                                <dx:ASPxComboBox runat="server" ID="cmbEstatus" Theme="Office2010Black" Width="300px" AutoPostBack="true"></dx:ASPxComboBox>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnNuevoTramo" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="gvTrip" EventName="RowInserting" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton runat="server" ID="btnGuardarSolicitud" Text="Guardar" Theme="Office2010Black" OnClick="btnGuardarSolicitud_Click" Visible="false"></dx:ASPxButton>
                                                        <dx:ASPxLabel runat="server" Text="Matricula: " Theme="Office2010Black"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txtMatricula" runat="server" Theme="Office2010Black"></dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel runat="server" Text="Nota: " Theme="Office2010Black"></dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxMemo ID="mNotaSolVuelo" runat="server" Width="300px" Height="50px">
                                                        </dx:ASPxMemo>
                                                    </td>
                                                    <td colspan="2"></td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                        </div>
                                        <div class="row">
                                            <br />
                                            <br />
                                            <div class="col-sm-12">
                                                <b>
                                                    <dx:ASPxCheckBox ID="chCorreo" runat="server" TextAlign="Left" Text="¿Enviar correo al cliente ? "></dx:ASPxCheckBox>
                                                </b>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <br />
                                            <br />
                                            <div class="col-sm-12">
                                                <b>
                                                    <asp:Label ID="Label1" runat="server" Text="TRIP  "></asp:Label>
                                                    <br />
                                                    <br />
                                                    <dx:ASPxGridView ID="gvTrip" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                        ClientInstanceName="gvTrip" EnableTheming="True" KeyFieldName="IdTrip"
                                                        OnCellEditorInitialize="gvTrip_CellEditorInitialize" OnRowDeleting="gvTrip_RowDeleting"
                                                        OnRowInserting="gvTrip_RowInserting" OnRowUpdating="gvTrip_RowUpdating"
                                                        OnStartRowEditing="gvTrip_StartRowEditing" OnRowValidating="gvTrip_RowValidating"
                                                        OnCustomButtonCallback="gvTrip_CustomButtonCallback" Styles-Header-HorizontalAlign="Center"
                                                        Theme="Office2010Black" Width="30%" StylesPopup-EditForm-ModalBackground-Opacity="90" OnCancelRowEditing="gvTrip_CancelRowEditing">
                                                        <ClientSideEvents EndCallback="function (s, e) {
                                                                                    if (s.cpShowPopup)
                                                                                    {
                                                                                        delete s.cpShowPopup;
                                                                                        lbl.SetText(s.cpText);
                                                                                        popup.Show();
                                                                                    }
                                                                                }" />
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Caption="TRIP" FieldName="Trip" ShowInCustomizationForm="True" VisibleIndex="0" CellStyle-CssClass="col-lg-6" PropertiesTextEdit-MaxLength="8">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <PropertiesTextEdit ValidationSettings-RegularExpression-ValidationExpression="^[1-9]+\d*$" ValidationSettings-RegularExpression-ErrorText="Sólo Números" ValidationSettings-ErrorDisplayMode="Text">
                                                            <ValidationSettings ErrorDisplayMode="Text">
                                                                <RegularExpression ErrorText="S&#243;lo N&#250;meros" ValidationExpression="^[1-9]+\d*$"></RegularExpression>
                                                            </ValidationSettings>
                                                        </PropertiesTextEdit>

                                                        
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Button" Caption="Acciones" ShowDeleteButton="True" CellStyle-CssClass="col-lg-6" ShowEditButton="True" ShowNewButton="false"
                                                        ShowInCustomizationForm="False" VisibleIndex="6" Visible="false">
                                                        <CellStyle CssClass="col-lg-6"></CellStyle>
                                                    </dx:GridViewCommandColumn>
                                                </Columns>
                                                <SettingsBehavior ConfirmDelete="True" />

                                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                                        <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />

                                                        <SettingsCommandButton>

                                                    <UpdateButton Text="Guardar">
                                                    </UpdateButton>
                                                    <CancelButton></CancelButton>
                                                    <EditButton>
                                                        <Image Height="20px" ToolTip="Modificar" Width="20px">
                                                        </Image>
                                                    </EditButton>
                                                    <DeleteButton>
                                                        <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                                        </Image>
                                                    </DeleteButton>
                                                </SettingsCommandButton>
                                            </dx:ASPxGridView>
                                                </b>
                                            </div>
                                            <br />
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <br />
                                                <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click" Visible="false"></dx:ASPxButton>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-sm-12">

                                            <br />
                                            <br />
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <b>
                                                    <asp:Label ID="Label2" runat="server" Text="Alta de Piernas"></asp:Label>
                                                </b>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <br />
                                                <dx:ASPxButton ID="btnNuevoTramo" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevoTramo_Click"></dx:ASPxButton>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                        <div class="row" style="width: 102%; margin-left: -15px;">
                                            <div class="col-sm-12">
                                                <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                                    <ContentTemplate>
                                                        <div class="col-sm-12">

                                                            <dx:ASPxGridView ID="gvTramos" runat="server" Styles-Header-HorizontalAlign="Center" Theme="Office2010Black" Font-Size="Small"
                                                                ClientInstanceName="gvTramos" EnableTheming="True" KeyFieldName="IdTramo" OnRowCommand="gvTramos_RowCommand"
                                                                OnCellEditorInitialize="gvTramos_CellEditorInitialize" OnRowDeleting="gvTramos_RowDeleting"
                                                                OnRowInserting="gvTramos_RowInserting" OnRowUpdating="gvTramos_RowUpdating" OnCancelRowEditing="gvTramos_CancelRowEditing"
                                                                OnStartRowEditing="gvTramos_StartRowEditing" OnRowValidating="gvTramos_RowValidating" StylesPopup-EditForm-ModalBackground-Opacity="90"
                                                                Width="100%" AutoGenerateColumns="False">

                                                                <ClientSideEvents EndCallback="function (s, e) {
                                                                                    if (s.cpShowPopup)
                                                                                    {
                                                                                        delete s.cpShowPopup;
                                                                                        lbl.SetText(s.cpText);
                                                                                        popup.Show();
                                                                                    }
                                                                                    if(s.cpMuestraPop)
                                                                                    {  
                                                                                        popup.Show();  
                                                                                        lbl.SetText(s.cpText);
                                                                                    }
                                                                                }" />
                                                                <Columns>
                                                                    <dx:GridViewDataColumn FieldName="aeropuertoo" Caption="Origen" Visible="true" VisibleIndex="1">
                                                                        <EditFormSettings Visible="False" />
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataComboBoxColumn FieldName="idaeropuertoo" Caption="Origen (ICAO)" VisibleIndex="1" PropertiesComboBox-ValueType="System.Int32" Visible="false">
                                                                        <PropertiesComboBox ValueType="System.Int32" ValueField="idaeropuertoo" ClientInstanceName="idaeropuertoo"
                                                                            IncrementalFilteringMode="Contains"
                                                                            TextFormatString="{1}"
                                                                            DropDownStyle="DropDown"
                                                                            OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition"
                                                                            OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue"
                                                                            FilterMinLength="0"
                                                                            NullText="Seleccione una opción"
                                                                            EnableCallbackMode="true" CallbackPageSize="10">
                                                                            <ValidationSettings ErrorDisplayMode="Text" CausesValidation="True" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                                                <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                                            </ValidationSettings>
                                                                            <ClientSideEvents EndCallback="function (s, e){
                                                                                 if(s.cpVal2 == '1'){  delete s.cpVal2; idaeropuertoo.SetText('');}
                                                                                 
                                                                                }"
                                                                                CallbackError="function(s,e){ idaeropuertoo.SetText(''); }" />
                                                                        </PropertiesComboBox>
                                                                        <EditFormSettings Visible="True"></EditFormSettings>
                                                                    </dx:GridViewDataComboBoxColumn>

                                                                    <dx:GridViewDataColumn FieldName="aeropuertod" Caption="Destino" VisibleIndex="2" Visible="true">
                                                                        <EditFormSettings Visible="False"></EditFormSettings>
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataComboBoxColumn FieldName="idaeropuertod" Caption="Destino (ICAO)" VisibleIndex="2" Visible="false">
                                                                        <PropertiesComboBox ValueType="System.Int32" ValueField="idaeropuertod" ClientInstanceName="idaeropuertod"
                                                                            IncrementalFilteringMode="Contains"
                                                                            TextFormatString="{1}"
                                                                            DropDownStyle="DropDown"
                                                                            OnItemsRequestedByFilterCondition="ASPxComboBox2_OnItemsRequestedByFilterCondition"
                                                                            OnItemRequestedByValue="ASPxComboBox2_OnItemRequestedByValue"
                                                                            FilterMinLength="0"
                                                                            NullText="Seleccione una opción"
                                                                            EnableCallbackMode="true" CallbackPageSize="10">
                                                                            <ValidationSettings ErrorDisplayMode="Text" CausesValidation="True" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                                                <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                                            </ValidationSettings>
                                                                            <ClientSideEvents EndCallback="function (s, e){
                                                                                 if(s.cpVal == '1'){  delete s.cpVal; idaeropuertod.SetText('');}
                                                                                }" />
                                                                        </PropertiesComboBox>
                                                                        <EditFormSettings Visible="True"></EditFormSettings>

                                                                    </dx:GridViewDataComboBoxColumn>

                                                                    <dx:GridViewDataDateColumn Caption="Fecha Vuelo" FieldName="FechaVuelo" VisibleIndex="3" PropertiesDateEdit-DisplayFormatInEditMode="true"
                                                                        ShowInCustomizationForm="True" PropertiesDateEdit-NullText="Seleccione fecha">
                                                                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" EditFormat="Custom">
                                                                            <DropDownButton>
                                                                                <Image IconID="scheduling_calendar_16x16"></Image>
                                                                            </DropDownButton>
                                                                        </PropertiesDateEdit>
                                                                    </dx:GridViewDataDateColumn>

                                                                    <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="HoraVuelo" Caption="Hora Vuelo" ShowInCustomizationForm="True">
                                                                        <PropertiesTextEdit NullText="Seleccione una Hora" ValidationSettings-ErrorDisplayMode="Text">
                                                                            <MaskSettings Mask="HH:mm" />
                                                                            <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </PropertiesTextEdit>
                                                                    </dx:GridViewDataTextColumn>

                                                                    <dx:GridViewDataTextColumn Caption="Transportación" FieldName="Transportacion" VisibleIndex="5" ShowInCustomizationForm="True">
                                                                        <PropertiesTextEdit ValidationSettings-ErrorDisplayMode="Text">
                                                                            <ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </PropertiesTextEdit>
                                                                    </dx:GridViewDataTextColumn>

                                                                    <dx:GridViewCommandColumn ButtonType="Button" Caption="Acciones" ShowDeleteButton="True" ShowEditButton="True" ShowNewButton="false"
                                                                        ShowInCustomizationForm="False" VisibleIndex="6">
                                                                    </dx:GridViewCommandColumn>

                                                                    <dx:GridViewDataColumn Caption="Pasajeros" Visible="true" VisibleIndex="7" CellStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <DataItemTemplate>
                                                                            <dx:ASPxButton Text='<%# "Pasajeros " + Eval("NoPax") %>' Theme="Office2010Black" ID="btn" runat="server" CommandArgument='<%# Eval("IdTramo") %>' CommandName="Pasajeros" AutoPostBack="true" ToolTip="Pasajeros" HorizontalAlign="Center">
                                                                            </dx:ASPxButton>
                                                                        </DataItemTemplate>
                                                                        <EditFormSettings Visible="false" />
                                                                    </dx:GridViewDataColumn>

                                                                    <dx:GridViewDataColumn Caption="Comisariato" Visible="true" VisibleIndex="8" CellStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <DataItemTemplate>

                                                                            <dx:ASPxButton Text='<%# "Comisariato " + Eval("NoComisariato") %>' Theme="Office2010Black" ID="btn" runat="server" CommandArgument='<%# Eval("IdTramo") %>' CommandName="Comisariato" AutoPostBack="true" ToolTip="Comisariato" HorizontalAlign="Center">
                                                                            </dx:ASPxButton>
                                                                        </DataItemTemplate>
                                                                        <EditFormSettings Visible="false" />
                                                                    </dx:GridViewDataColumn>

                                                                </Columns>
                                                                <Templates>
                                                                    <DetailRow>
                                                                        <br />
                                                                        <dx:ASPxGridView ID="gvDetalle" runat="server" KeyFieldName="IdPax" OnRowCommand="gvDetalle_RowCommand"
                                                                            OnBeforePerformDataSelect="gvDetalle_BeforePerformDataSelect"
                                                                            Styles-Header-HorizontalAlign="Center" Theme="Office2010Black" Font-Size="Small" AutoGenerateColumns="false"
                                                                            EnableRowsCache="false">
                                                                            <Columns>
                                                                                <dx:GridViewDataTextColumn Caption="NombrePax" FieldName="NombrePax" VisibleIndex="0" Visible="true" HeaderStyle-HorizontalAlign="Center">
                                                                                </dx:GridViewDataTextColumn>

                                                                            </Columns>
                                                                            <Settings ShowFooter="True" />
                                                                        </dx:ASPxGridView>
                                                                        <br />
                                                                    </DetailRow>
                                                                </Templates>
                                                                <SettingsDetail ShowDetailRow="true" />
                                                                <SettingsBehavior ConfirmDelete="True" />
                                                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                                                                <SettingsPopup>
                                                                    <EditForm HorizontalAlign="Center" VerticalAlign="Above" Modal="true" CloseOnEscape="True" />
                                                                </SettingsPopup>
                                                                <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                                                <SettingsCommandButton>
                                                                    <UpdateButton Text="Guardar">
                                                                    </UpdateButton>
                                                                    <CancelButton></CancelButton>
                                                                    <EditButton>
                                                                        <Image Height="20px" ToolTip="Modificar" Width="20px">
                                                                        </Image>
                                                                    </EditButton>
                                                                    <DeleteButton>
                                                                        <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                                                        </Image>
                                                                    </DeleteButton>
                                                                </SettingsCommandButton>
                                                            </dx:ASPxGridView>
                                                            <dx:ASPxLabel ID="lblMnsaj" runat="server"></dx:ASPxLabel>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnNuevoTramo" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-12 text-right">
                                                <dx:ASPxButton runat="server" ID="btnTerminar" Text="Guardar" Theme="Office2010Black" OnClick="btnTerminar_Click"></dx:ASPxButton>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <dx:ASPxButton runat="server" ID="btnSiguiente1" Text="Siguiente" Theme="Office2010Black" OnClick="btnSiguiente1_Click"></dx:ASPxButton>
                                            </div>
                                        </div>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                    <TabPages>
                        <dx:TabPage Text="2. Seguimiento a Solicitudes " Name="Historico" Enabled="true">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <div class="well-g">
                                        <div class="col-sm-12">
                                            <dx:ASPxLabel ID="lblHistorico" runat="server" Text="Histórico de Cambios"></dx:ASPxLabel>
                                        </div>

                                        <br />
                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-1">
                                                <dx:ASPxLabel runat="server" ID="lblAutor" Text="Autor:" Theme="Office2010Black"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-sm-11">
                                                <dx:ASPxComboBox ID="ddlAutor" runat="server" Theme="Office2010Black" EnableTheming="True">
                                                    <Items>
                                                        <dx:ListEditItem Text="ALE" Value="0" Selected="true"></dx:ListEditItem>
                                                        <dx:ListEditItem Text="Cliente" Value="1"></dx:ListEditItem>
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <br />
                                        <div class="col-sm-1">
                                            <dx:ASPxLabel runat="server" ID="lblNota" Text="Notas:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-sm-11">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" OnUnload="Unnamed_Unload">
                                                <ContentTemplate>
                                                    <dx:ASPxMemo ID="mNota" ClientInstanceName="mNota" runat="server" Width="100%" Height="150px">
                                                        <ClientSideEvents TextChanged="function(s,e) { Remplaza(s,e); }" />
                                                    </dx:ASPxMemo>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <br />
                                        </div>
                                        <div class="col-sm-1">
                                        </div>
                                        <div class="col-sm-11">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" OnUnload="Unnamed_Unload">
                                                <ContentTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxUploadControl ID="upcSeguimiento" runat="server" NullText="Solo PDF"
                                                                    OnFileUploadComplete="upcSeguimiento_FileUploadComplete" Theme="Office2010Black"
                                                                    ShowProgressPanel="true" ShowUploadButton="true" FileUploadMode="BeforePageLoad">
                                                                    <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".pdf" />
                                                                    <BrowseButton Text="Seleccione un Documento" />
                                                                    <UploadButton Text="Cargar Documento Seleccionado"></UploadButton>
                                                                    <ClientSideEvents FileUploadComplete="function(s,e) {
                                                                                                lblMensaje2.SetText('El documento se subio');
                                                                                        }" />
                                                                </dx:ASPxUploadControl>
                                                            </td>
                                                            <td style="width: 10px"></td>
                                                            <td style="vertical-align: top">
                                                                <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional" OnUnload="Unnamed_Unload">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="lbArchivo" runat="server" Visible="false" OnClick="lbArchivo_Click"></asp:LinkButton>
                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/img/iconos/Accept.png" Width="28" Height="28" Visible="false" ToolTip="Archivo guardado." />
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="lbArchivo" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">

                                                                <dx:ASPxLabel runat="server" ID="lblMensaje2" ClientInstanceName="lblMensaje2" Theme="Office2010Black"></dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <br />
                                        <div class="col-sm-12 text-right">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" OnUnload="Unnamed_Unload">
                                                <ContentTemplate>
                                                    <dx:ASPxButton runat="server" ID="btnGuardarSeguimiento" Text="Guardar" Theme="Office2010Black" OnClick="btnGuardarSeguimiento_Click"></dx:ASPxButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <br />
                                            <br />
                                        </div>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional" OnUnload="Unnamed_Unload">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="gvHistorico" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                    ClientInstanceName="gvHistorico" EnableTheming="True" KeyFieldName="IdSeguimiento" Styles-Header-HorizontalAlign="Center"
                                                    Theme="Office2010Black" Width="99%" StylesPopup-EditForm-ModalBackground-Opacity="90"
                                                    OnCellEditorInitialize="gvHistorico_CellEditorInitialize" OnRowDeleting="gvHistorico_RowDeleting"
                                                    OnRowInserting="gvHistorico_RowInserting" OnRowUpdating="gvHistorico_RowUpdating"
                                                    OnStartRowEditing="gvHistorico_StartRowEditing" OnRowValidating="gvHistorico_RowValidating"
                                                    OnCustomButtonCallback="gvHistorico_CustomButtonCallback" OnCancelRowEditing="gvHistorico_CancelRowEditing" OnRowCommand="gvHistorico_RowCommand">
                                                    <ClientSideEvents EndCallback="function (s, e) {
                                                                                    if (s.cpShowPopup)
                                                                                    {
                                                                                        delete s.cpShowPopup;
                                                                                        lbl.SetText(s.cpText);
                                                                                        popup.Show();
                                                                                    }
                                                                                }" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Descripción" FieldName="Nota" ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn Caption="Autor" FieldName="Autor" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn Caption="Usuario Creación" FieldName="UsuarioCreacion" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn Caption="Fecha Creación" FieldName="FechaCreacion" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataColumn Caption="Nombre Archivo" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <DataItemTemplate>
                                                                <asp:LinkButton Text='<%# Eval("NombreArchivo") %>' ID="ASPxButton2" runat="server" CommandArgument='<%# Eval("IdSeguimiento") %>' CommandName="Descarga" AutoPostBack="true" ToolTip="Descarga" HorizontalAlign="Center">
                                                                </asp:LinkButton>
                                                                <%-- <dx:ASPxButton Text="Descarga" Theme="Office2010Black" ID="ASPxButton2" runat="server" CommandArgument='<%# Eval("IdSeguimiento") %>' CommandName="Descarga" AutoPostBack="true" ToolTip="Descarga" HorizontalAlign="Center">
                                                               </dx:ASPxButton>--%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>


                                                        <dx:GridViewCommandColumn ButtonType="Button" Caption="Acciones" ShowDeleteButton="True" ShowEditButton="False" ShowNewButton="false"
                                                            ShowInCustomizationForm="False" VisibleIndex="5">
                                                        </dx:GridViewCommandColumn>

                                                        <dx:GridViewDataColumn Caption="Detalle" Visible="true" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <DataItemTemplate>
                                                                <dx:ASPxButton Text="Ver " Theme="Office2010Black" ID="btn" runat="server" CommandArgument='<%# Eval("IdSeguimiento") %>' CommandName="Detalle" AutoPostBack="true" ToolTip="Muestra el Detalle" HorizontalAlign="Center">
                                                                </dx:ASPxButton>
                                                                <%-- <dx:ASPxButton Text="Descarga" Theme="Office2010Black" ID="ASPxButton2" runat="server" CommandArgument='<%# Eval("IdSeguimiento") %>' CommandName="Descarga" AutoPostBack="true" ToolTip="Descarga" HorizontalAlign="Center">
                                                               </dx:ASPxButton>--%>
                                                            </DataItemTemplate>
                                                            <EditFormSettings Visible="false" />
                                                        </dx:GridViewDataColumn>

                                                    </Columns>
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsEditing Mode="Inline"></SettingsEditing>
                                                    <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                                    <SettingsCommandButton>
                                                        <UpdateButton Text="Guardar">
                                                        </UpdateButton>
                                                        <CancelButton></CancelButton>
                                                        <EditButton>
                                                            <Image Height="20px" ToolTip="Modificar" Width="20px">
                                                            </Image>
                                                        </EditButton>
                                                        <DeleteButton>
                                                            <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                                            </Image>
                                                        </DeleteButton>
                                                    </SettingsCommandButton>
                                                </dx:ASPxGridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="gvHistorico" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <br />
                                        <br />
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <div class="col-sm-12 text-right">
                                            <dx:ASPxButton runat="server" ID="btnAtras2" Text="Atrás" Theme="Office2010Black" OnClick="btnAtras2_Click"></dx:ASPxButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <dx:ASPxButton runat="server" ID="btnSiguiente2" Text="Siguiente" Theme="Office2010Black" OnClick="btnSiguiente2_Click"></dx:ASPxButton>
                                        </div>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>

                    <TabPages>
                        <dx:TabPage Text="3. Itinerario de Vuelo" Name="Itinerario" Enabled="true">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <div class="row col-md-12 well-g">
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Itinerario de Vuelo"></dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <table width="100%">
                                            <tr>
                                                <td width="25%">&nbsp;</td>
                                                <td width="25%">
                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel2" Text="Autor:" Theme="Office2010Black" Visible="false"></dx:ASPxLabel>
                                                </td>
                                                <td width="25%">
                                                    <dx:ASPxComboBox ID="ddlprueba" runat="server" Theme="Office2010Black" EnableTheming="True" Visible="false">
                                                        <Items>
                                                            <dx:ListEditItem Text="ALE" Value="0" Selected="true"></dx:ListEditItem>
                                                            <dx:ListEditItem Text="Cliente" Value="1"></dx:ListEditItem>
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td width="25%">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="25%">&nbsp;</td>
                                                <td>
                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel4" Text="Archivo: " Theme="Office2010Black"></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="panel" runat="server" OnUnload="Unnamed_Unload">
                                                        <ContentTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxUploadControl ID="UploadControl" runat="server" NullText="Solo PDF"
                                                                            OnFileUploadComplete="UploadControl_FileUploadComplete"
                                                                            ShowProgressPanel="true" ShowUploadButton="true" FileUploadMode="BeforePageLoad">
                                                                            <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".pdf" />
                                                                            <BrowseButton Text="Seleccione un Itinerario" />
                                                                            <UploadButton Text="Cargar Documento Seleccionado"></UploadButton>
                                                                            <ClientSideEvents FileUploadComplete="function(s,e) {
                                                                                                lblMensaje.SetText('El documento se subio');
                                                                                        }" />
                                                                        </dx:ASPxUploadControl>
                                                                    </td>
                                                                    <td style="width: 10px"></td>
                                                                    <td style="vertical-align: top">
                                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional" OnUnload="Unnamed_Unload">
                                                                            <ContentTemplate>
                                                                                <asp:LinkButton ID="lMsgArchivo" runat="server" Visible="false" OnClick="lMsgArchivo_Click"></asp:LinkButton>
                                                                                <asp:Image ID="imArchivo" runat="server" ImageUrl="~/img/iconos/Accept.png" Width="28" Height="28" Visible="false" ToolTip="Archivo guardado." />
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:PostBackTrigger ControlID="lMsgArchivo" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">

                                                                        <dx:ASPxLabel runat="server" ID="lblMensaje" ClientInstanceName="lblMensaje" Theme="Office2010Black"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td width="25%">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="25%">&nbsp;</td>
                                                <td>
                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel3" Text="Notas de Vuelo:" Theme="Office2010Black"></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxMemo ID="mPrueba" runat="server" EnableViewState="true" Width="500px" Height="200px"></dx:ASPxMemo>
                                                    <br />
                                                </td>
                                                <td width="25%">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="text-align: center;">
                                                    <dx:ASPxButton runat="server" ID="btnPrueba" Text="Guardar" Theme="Office2010Black" OnClick="btnGuardarItinerario_Click"></dx:ASPxButton>
                                                    <dx:ASPxLabel ID="lblDoc" runat="server"></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">&nbsp;
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional" OnUnload="Unnamed_Unload">
                                                        <ContentTemplate>
                                                            <dx:ASPxGridView ID="gvItinerario" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                                ClientInstanceName="gvItinerario" EnableTheming="True" KeyFieldName="IdItinerario" Styles-Header-HorizontalAlign="Center"
                                                                Theme="Office2010Black" Width="99%" StylesPopup-EditForm-ModalBackground-Opacity="90"
                                                                OnRowCommand="gvItinerario_RowCommand">
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="IdItinerario" FieldName="IdItinerario" ShowInCustomizationForm="True" VisibleIndex="0" Visible="false">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="IdSolicitud" FieldName="IdSolicitud" ShowInCustomizationForm="True" VisibleIndex="1" Visible="false">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Notas Vuelo" FieldName="NotasVuelo" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Nombre del Archivo" FieldName="NombreArchivo" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </dx:GridViewDataTextColumn>


                                                                    <dx:GridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="4" CellStyle-HorizontalAlign="Center">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <DataItemTemplate>
                                                                            <dx:ASPxButton Text="Descarga" Theme="Office2010Black" ID="btn" runat="server" CommandArgument='<%# Eval("IdItinerario") %>' CommandName="Descarga" AutoPostBack="true" ToolTip="Descarga" HorizontalAlign="Center">
                                                                            </dx:ASPxButton>
                                                                            <dx:ASPxButton Text="Ver Detalle" Theme="Office2010Black" ID="btnDetale" runat="server" CommandArgument='<%# Eval("IdItinerario") %>' CommandName="Detalle" AutoPostBack="true" ToolTip="Muestra el Detalle" HorizontalAlign="Center">
                                                                            </dx:ASPxButton>
                                                                            <dx:ASPxButton Text="Eliminar" Theme="Office2010Black" ID="btneliminaI" runat="server" CommandArgument='<%# Eval("IdItinerario") %>' CommandName="Elimina" AutoPostBack="true" ToolTip="Eliminar" HorizontalAlign="Center">
                                                                                <ClientSideEvents Click="function(s, e){  e.processOnServer = confirm('¿Está seguro que desea eliminar el registro?');}" />
                                                                            </dx:ASPxButton>
                                                                        </DataItemTemplate>
                                                                        <EditFormSettings Visible="false" />
                                                                    </dx:GridViewDataColumn>
                                                                </Columns>
                                                            </dx:ASPxGridView>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="gvItinerario" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <br />
                                    <div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12 text-right">
                                            <div style="text-align: left">
                                                <dx:ASPxButton runat="server" OnClick="btnTripGuide_Click" Theme="Office2010Black" ID="btnTripGuide" Text="TripGuide">
                                                </dx:ASPxButton>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 text-right">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Conditional" OnUnload="Unnamed_Unload">
                                                <ContentTemplate>
                                                    <dx:ASPxGridView ID="gvTripGuides" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                        ClientInstanceName="gvTripGuides" EnableTheming="True" KeyFieldName="IdTripGuide" Styles-Header-HorizontalAlign="Center"
                                                        Theme="Office2010Black" Width="99%" StylesPopup-EditForm-ModalBackground-Opacity="90"
                                                        OnRowCommand="gvTripGuides_RowCommand">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="IdTripGuide" FieldName="IdTripGuide" ShowInCustomizationForm="True" VisibleIndex="0" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="IdSolicitud" FieldName="IdSolicitud" ShowInCustomizationForm="True" VisibleIndex="1" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataTextColumn Caption="No." FieldName="NoTripG" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataTextColumn Caption="No. TRIP" FieldName="IdTrip" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Nombre del Archivo" FieldName="NombreArchivoPDF" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataTextColumn Caption="Fecha de Creación" FieldName="FechaTG" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <DataItemTemplate>
                                                                    <dx:ASPxButton Text="Descarga" Theme="Office2010Black" ID="btnDescargaTG" runat="server" CommandArgument='<%# Eval("IdTripGuide") %>' CommandName="Descarga" AutoPostBack="true" ToolTip="Descarga" HorizontalAlign="Center">
                                                                    </dx:ASPxButton>
                                                                    <dx:ASPxButton Text="Eliminar" Theme="Office2010Black" ID="btnEliminaTG" runat="server" CommandArgument='<%# Eval("IdTripGuide") %>' CommandName="Elimina" AutoPostBack="true" ToolTip="Eliminar" HorizontalAlign="Center">
                                                                        <ClientSideEvents Click="function(s, e){  e.processOnServer = confirm('¿Está seguro que desea eliminar el registro?');}" />
                                                                    </dx:ASPxButton>
                                                                    <dx:ASPxButton Text="Enviar e-mail" Theme="Office2010Black" ID="btnEnviaTG" runat="server" CommandArgument='<%# Eval("IdTripGuide") %>' CommandName="Enviar" ToolTip="Enviar" HorizontalAlign="Center">
                                                                    </dx:ASPxButton>
                                                                </DataItemTemplate>
                                                                <EditFormSettings Visible="false" />
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataBinaryImageColumn Caption="Binario" FieldName="PDF" Visible="false" VisibleIndex="5" CellStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataBinaryImageColumn>
                                                            <dx:GridViewDataTextColumn Caption="Observaciones" FieldName="Observaciones" Visible="false" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="NombreContacto" FieldName="NombreContacto" Visible="false" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="ICAOOrigen" FieldName="ICAOOrigen" Visible="false" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="NombreAeropuertoOrigen" FieldName="NombreAeropuertoOrigen" Visible="false" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="ICAODestino" FieldName="ICAODestino" Visible="false" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="NombreAeropuertoDestino" FieldName="NombreAeropuertoDestino" Visible="false" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="FechaSalida" FieldName="FechaSalida" Visible="false" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="NumeroPasajero" FieldName="NumeroPasajero" Visible="false" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Aeronave" FieldName="Aeronave" Visible="false" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Piloto" FieldName="Piloto" Visible="false" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="PilotoTelefono" FieldName="PilotoTelefono" Visible="false" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="CoPiloto" FieldName="CoPiloto" Visible="false" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="CoPilotoTelefono" FieldName="CoPilotoTelefono" Visible="false" VisibleIndex="6" CellStyle-HorizontalAlign="Center">
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                    </dx:ASPxGridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="gvTripGuides" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12 text-right">
                                            <dx:ASPxButton runat="server" ID="btnAtras3" Text="Atrás" Theme="Office2010Black" OnClick="btnAtras3_Click"></dx:ASPxButton>

                                            <dx:ASPxButton runat="server" ID="ASPxButton1" Text="Regresar" Theme="Office2010Black" OnClick="ASPxButton1_Click"></dx:ASPxButton>
                                        </div>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>

                </dx:ASPxPageControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

    <dx:ASPxPopupControl ID="popup" runat="server" ClientInstanceName="popup" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true" Width="300">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="pnlPopUp" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxImage ID="AspxImagen2" runat="server" ShowLoadingImage="true" ImageUrl="~/img/iconos/Information2.ico"></dx:ASPxImage>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lbl" runat="server" ClientInstanceName="lbl" Text="AspxLabel"></dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btOK" runat="server" Text="OK" Theme="Office2010Black" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) 
                                                                 {
                                                                    popup.Hide(); 
                                                                 }" />
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

    <dx:ASPxPopupControl ID="ppPasajeros" runat="server" ClientInstanceName="ppPasajeros" Width="600px" CloseAction="CloseButton" CloseOnEscape="true" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel2" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <dx:ASPxButton ID="bNewPasajero" runat="server" Text="Nuevo Pasajero" Theme="Office2010Black" OnClick="bNewPasajero_Click"></dx:ASPxButton>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                        <dx:ASPxGridView ID="gvPasajeros" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                            ClientInstanceName="gvPasajeros" EnableTheming="True" KeyFieldName="IdPax"
                                            OnCellEditorInitialize="gvPasajeros_CellEditorInitialize" OnRowDeleting="gvPasajeros_RowDeleting"
                                            OnRowInserting="gvPasajeros_RowInserting" OnRowUpdating="gvPasajeros_RowUpdating"
                                            OnStartRowEditing="gvPasajeros_StartRowEditing" OnRowValidating="gvPasajeros_RowValidating"
                                            OnCustomButtonCallback="gvPasajeros_CustomButtonCallback" Styles-Header-HorizontalAlign="Center"
                                            Theme="Office2010Black" Width="580px" OnCancelRowEditing="gvPasajeros_CancelRowEditing">
                                            <ClientSideEvents EndCallback="function (s, e) {
                                                                                    if (s.cpShowPopup)
                                                                                    {
                                                                                        delete s.cpShowPopup;
                                                                                        lbl.SetText(s.cpText);
                                                                                        popup.Show();
                                                                                    }
                                                                                }" />
                                            <Columns>

                                                <dx:GridViewDataComboBoxColumn FieldName="NombrePax" Caption="Nombre Pax" VisibleIndex="0" Visible="true">
                                                    <PropertiesComboBox ValueField="NombrePax" ClientInstanceName="NombrePax"
                                                        IncrementalFilteringMode="Contains"
                                                        TextFormatString="{1}"
                                                        DropDownStyle="DropDown"
                                                        OnItemsRequestedByFilterCondition="Pasajeros_OnItemsRequestedByFilterCondition"
                                                        OnItemRequestedByValue="Pasajeros_OnItemRequestedByValue"
                                                        FilterMinLength="0"
                                                        NullText="Seleccione una opción"
                                                        EnableCallbackMode="true" CallbackPageSize="10">
                                                        <ValidationSettings ErrorDisplayMode="Text" CausesValidation="True" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                            <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                        </ValidationSettings>
                                                        <ClientSideEvents EndCallback="function (s, e){
                                                                                 if(s.cpV2 == '1'){  delete s.cpV2; NombrePax.SetText('');}
                                                                                }" />
                                                    </PropertiesComboBox>
                                                    <EditFormSettings Visible="True"></EditFormSettings>
                                                </dx:GridViewDataComboBoxColumn>

                                                <dx:GridViewCommandColumn ButtonType="Button" Caption="Acciones" ShowDeleteButton="True" CellStyle-CssClass="col-lg-6" ShowEditButton="True" ShowNewButton="false"
                                                    ShowInCustomizationForm="False" VisibleIndex="6">
                                                </dx:GridViewCommandColumn>
                                            </Columns>
                                            <SettingsBehavior ConfirmDelete="True" />
                                            <SettingsEditing Mode="Inline"></SettingsEditing>
                                            <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                            <SettingsCommandButton>
                                                <UpdateButton Text="Guardar">
                                                </UpdateButton>
                                                <CancelButton></CancelButton>
                                                <EditButton>
                                                    <Image Height="20px" ToolTip="Modificar" Width="20px">
                                                    </Image>
                                                </EditButton>
                                                <DeleteButton>
                                                    <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                                    </Image>
                                                </DeleteButton>
                                            </SettingsCommandButton>
                                        </dx:ASPxGridView>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <br />
                                                <dx:ASPxButton ClientInstanceName="btnSalir" runat="server" Text="Salir" Theme="Office2010Black">
                                                    <ClientSideEvents Click="function() {ppPasajeros.Hide(); }" />
                                                </dx:ASPxButton>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <dx:ASPxButton ID="btnNuevoP" runat="server" Text="Alta Nuevo Pasajero" OnClick="btnNuevoP_Click" Theme="Office2010Black">
                                                </dx:ASPxButton>
                                                <br />

                                            </div>
                                        </div>
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

    <dx:ASPxPopupControl ID="ppComisariato" runat="server" ClientInstanceName="ppComisariato" Width="900px" CloseAction="CloseButton" CloseOnEscape="true" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <dx:ASPxButton ID="btnNuevoComisaiato" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevoComisaiato_Click"></dx:ASPxButton>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                        <dx:ASPxGridView ID="gvComisariato" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                            ClientInstanceName="gvComisariato" EnableTheming="True" KeyFieldName="IdComisariato;IdProveedor"
                                            OnCellEditorInitialize="gvComisariato_CellEditorInitialize" OnRowDeleting="gvComisariato_RowDeleting"
                                            OnRowInserting="gvComisariato_RowInserting" OnRowUpdating="gvComisariato_RowUpdating"
                                            OnStartRowEditing="gvComisariato_StartRowEditing" OnRowValidating="gvComisariato_RowValidating"
                                            OnCustomButtonCallback="gvComisariato_CustomButtonCallback" Styles-Header-HorizontalAlign="Center"
                                            Theme="Office2010Black" Width="100%" OnCancelRowEditing="gvComisariato_CancelRowEditing">
                                            <ClientSideEvents EndCallback="function (s, e) {
                                                                                    if (s.cpShowPopup)
                                                                                    {
                                                                                        delete s.cpShowPopup;
                                                                                        lbl.SetText(s.cpText);
                                                                                        popup.Show();
                                                                                    }
                                                                                }" />
                                            <Columns>

                                                <dx:GridViewDataComboBoxColumn Caption="Proveedor" FieldName="Descripcion" ShowInCustomizationForm="True" VisibleIndex="0" Visible="true" Width="30%">
                                                    <EditFormSettings Visible="True"></EditFormSettings>
                                                    <PropertiesComboBox NullText="Seleccione una opci&#243;n" NullDisplayText="Seleccione una opci&#243;n">
                                                        <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                            <RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
                                                            <RegularExpression ErrorText="El campo contiene informaci&#243;n inv&#225;lida."></RegularExpression>
                                                        </ValidationSettings>
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>


                                                <dx:GridViewDataMemoColumn Caption="Descripción" FieldName="ComisariatoDesc" ShowInCustomizationForm="True" VisibleIndex="1" Width="30%" EditCellStyle-Paddings-PaddingBottom="14px">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <PropertiesMemoEdit Width="100%" MaxLength="250">
                                                        <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#242;n ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                            <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </PropertiesMemoEdit>
                                                </dx:GridViewDataMemoColumn>

                                                <dx:GridViewDataTextColumn Caption="Importe" FieldName="PrecioCotizado" ShowInCustomizationForm="True" VisibleIndex="2" Width="15%" PropertiesTextEdit-DisplayFormatString="C">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <PropertiesTextEdit Width="15%">
                                                        <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#242;n ingresada" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                            <ErrorFrameStyle BackColor="Red"></ErrorFrameStyle>
                                                            <RegularExpression ValidationExpression="\d+(\.)?\d*" ErrorText="El campo permite solo números."></RegularExpression>
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewCommandColumn ButtonType="Button" Caption="Acciones" ShowDeleteButton="True" ShowEditButton="True" ShowNewButton="false" Width="25%"
                                                    ShowInCustomizationForm="False" VisibleIndex="6">
                                                </dx:GridViewCommandColumn>
                                            </Columns>
                                            <Settings VerticalScrollBarMode="Auto" />
                                            <SettingsBehavior ConfirmDelete="True" />
                                            <SettingsEditing Mode="Inline"></SettingsEditing>
                                            <SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
                                            <SettingsCommandButton>
                                                <UpdateButton Text="Guardar">
                                                </UpdateButton>
                                                <CancelButton></CancelButton>
                                                <EditButton>
                                                    <Image Height="20px" ToolTip="Modificar" Width="20px">
                                                    </Image>
                                                </EditButton>
                                                <DeleteButton>
                                                    <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                                    </Image>
                                                </DeleteButton>
                                            </SettingsCommandButton>
                                        </dx:ASPxGridView>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <br />
                                                <dx:ASPxButton ClientInstanceName="btnSalir" runat="server" Text="Salir" Theme="Office2010Black">
                                                    <ClientSideEvents Click="function() {ppComisariato.Hide(); }" />
                                                </dx:ASPxButton>
                                                <br />

                                            </div>
                                        </div>
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

    <dx:ASPxPopupControl ID="ppDetalle" runat="server" ClientInstanceName="ppDetalle" Width="600px" Height="300px" CloseAction="CloseButton" CloseOnEscape="true" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Detalle">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel4" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td>

                                        <dx:ASPxMemo ID="mmDetalle" runat="server" Width="100%" Height="300px">
                                        </dx:ASPxMemo>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <br />
                                                <dx:ASPxButton ClientInstanceName="btnSalir" runat="server" Text="Salir" Theme="Office2010Black">
                                                    <ClientSideEvents Click="function() {ppDetalle.Hide(); }" />
                                                </dx:ASPxButton>
                                                <br />

                                            </div>
                                        </div>
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

    <dx:ASPxPopupControl ID="ppAltaPasajeros" runat="server" ClientInstanceName="ppAltaPasajeros" Width="400px" Height="200px" CloseAction="CloseButton" CloseOnEscape="true" Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Alta Pasajero">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel5" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel runat="server" Text="Nombre Pasajero:" Theme="Office2010Black">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtNombreP" ClientInstanceName="txtNombreP" runat="server" NullText="Nombre Pasajero" Theme="Office2010Black">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 15px"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel runat="server" Text="Apellido Pasajero:" Theme="Office2010Black">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtApellidoP" ClientInstanceName="txtApellidoP" runat="server" NullText="Apellido Pasajero" Theme="Office2010Black">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 15px"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnguardaP" runat="server" Theme="Office2010Black" OnClick="btnguardaP_Click" Text="Guardar">
                                            <ClientSideEvents Click="function() { ppAltaPasajeros.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnCancelaP" runat="server" Theme="Office2010Black" OnClick="btnCancelaP_Click" Text="Cancelar">
                                            <ClientSideEvents Click="function() { ppAltaPasajeros.Hide(); txtApellidoP.SetText(''); txtNombreP.SetText('');  } " />
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

    <dx:ASPxPopupControl ID="ppTrips" runat="server" ClientInstanceName="ppTrips" Width="400px" CloseAction="CloseButton" CloseOnEscape="true" Modal="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Seleccione un Trip">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel6" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: center">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:GridView ID="gvTrips" runat="server" AutoGenerateColumns="false" CssClass="table"
                                                    Style="border-top: 1px solid #484848; border-left: 1px solid #484848; border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                    ShowFooter="true">
                                                    <HeaderStyle CssClass="celda2" />
                                                    <RowStyle CssClass="celda6" Height="16px" />
                                                    <FooterStyle CssClass="celda3" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:RadioButton ID="rbSeleccione" runat="server" OnClick="javascript:Selrdbtn(this.id)" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Trip" HeaderText="No. Trip" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div style="text-align: right">
                                                    <dx:ASPxButton ClientInstanceName="btnSeleccionarTrip" runat="server" Text="Seleccionar" Theme="Office2010Black"
                                                        OnClick="btnSeleccionarTrip_Click">
                                                        <ClientSideEvents Click="function() {ppTrips.Hide(); }" />
                                                    </dx:ASPxButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div style="text-align: left">
                                                    <dx:ASPxButton ClientInstanceName="btnSalir" runat="server" Text="Salir" Theme="Office2010Black">
                                                        <ClientSideEvents Click="function() {ppTrips.Hide(); }" />
                                                    </dx:ASPxButton>
                                                </div>
                                            </div>
                                        </div>
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

    <asp:HiddenField ID="hfOrigen" runat="server" />

    <dx:ASPxPopupControl ID="ppEnviarMailTripGuide" runat="server" ClientInstanceName="ppEnviarMailTripGuide" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Enviar a Contacto" AllowDragging="True" Theme="Office2010Black" Width="600px" Height="150px">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="pnlModal" runat="server" DefaultButton="btnEnviarMail">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <div style="margin-left: 6px; margin-right: auto; vertical-align: middle;">
                                            <dx:ASPxLabel runat="server" ID="lblPara" Text="Para:" Height="30px" Width="30px"></dx:ASPxLabel>
                                        </div>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtPara" runat="server" Width="100%" Theme="Office2010Black">
                                            <ValidationSettings SetFocusOnError="True" Display="Dynamic">
                                                <RegularExpression ErrorText="Correo invalido." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                            </ValidationSettings>
                                            <InvalidStyle BackColor="LightPink" />
                                        </dx:ASPxTextBox>
                                        <br />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="margin-left: 6px; margin-right: auto; vertical-align: middle;">
                                            <dx:ASPxLabel ID="lblConCopia" runat="server" Theme="Office2010Black" Text="CC:" Height="30px" Width="30px"></dx:ASPxLabel>
                                        </div>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtConCopia" runat="server" Width="100%" Theme="Office2010Black">
                                            <ValidationSettings SetFocusOnError="True" Display="Dynamic">
                                                <RegularExpression ErrorText="Correo invalido." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                            </ValidationSettings>
                                            <InvalidStyle BackColor="LightPink" />
                                        </dx:ASPxTextBox>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="margin-left: 6px; margin-right: auto; vertical-align: middle;">
                                            <dx:ASPxLabel ID="lblConCopiaOculta" runat="server" Theme="Office2010Black" Text="CCO:" Height="30px" Width="30px"></dx:ASPxLabel>
                                        </div>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtConCopiaOculta" runat="server" Width="100%" Theme="Office2010Black">
                                            <ValidationSettings SetFocusOnError="True" Display="Dynamic">
                                                <RegularExpression ErrorText="Correo invalido." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                            </ValidationSettings>
                                            <InvalidStyle BackColor="LightPink" />
                                        </dx:ASPxTextBox>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lblObservaciones" runat="server" Theme="Office2010Black" Text="Observaciones:" Visible="true"></dx:ASPxLabel>
                                    </td>
                                    <td style="width: 100%">
                                        <dx:ASPxMemo ID="memoObservaciones" runat="server" Text="" NullText="Agregue alguna observación." Width="100%" Height="100px" Theme="Office2010Black" Visible="true" MaxLength="1000" Enabled="false">
                                        </dx:ASPxMemo>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                            <div style="text-align: center; width: 100%">
                                <table style="margin: 0px 85px; width: 100%">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnCancelar" runat="server" Text="Cancelar" Theme="Office2010Black" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0" ClientSideEvents-Click="function(s, e) 
                                                                 {
                                                                    ppEnviarMailTripGuide.Hide(); 
                                                                 }">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnEnviarMail" runat="server" Text="Enviar" Theme="Office2010Black" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0"
                                                OnClick="btnEnviarMail_Click">
                                                <ClientSideEvents Click="CerrarModalCorreo" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>
