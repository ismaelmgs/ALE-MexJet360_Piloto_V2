<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmMonitorTrafico.aspx.cs" Inherits="ALE_MexJet.Views.FBO.frmMonitorTrafico" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                <asp:Timer runat="server" ID="UpdateTimer" Interval="35000" OnTick="UpdateTimer_Tick" />
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 25px;">&nbsp;&nbsp;Monitor Tráfico</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <fieldset class="Personal">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                            </legend>
                            <div class="col-sm-12">
                                <table cellpsdding="5" width="99%" border="0">
                                    <tr>
                                        <td style="width: 20%"></td>
                                        <td>
                                            <dx:ASPxLabel CssClass="FExport" runat="server" Text="Aeropuerto Base:"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" OnUnload="UpdatePanel1_Unload">
                                                <ContentTemplate>
                                                    <dx:ASPxComboBox ToolTip="Aeropuerto Base" ID="ddlBase" runat="server" Theme="Office2010Black" EnableTheming="True" NullText="Seleccione la Base"
                                                        ValueType="System.Int32" ValueField="idAeropuert">
                                                        <ValidationSettings ErrorDisplayMode="Text">
                                                            
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 20%">
                                            <dx:ASPxButton CssClass="FBotton" ID="btnBuscar" runat="server"  Text="Buscar" OnClick="btnBuscar_Click" Theme="Office2010Black">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 15px"></td>
                                    </tr>
                                </table>
                            </div>
                        </fieldset>
                    </div>
                </div>

                <br />
                    <div class="row">
                        <div class="col-md-6">
                            
                        </div>
                        <div class="col-md-6" style="text-align: right;">
                            <dx:ASPxLabel CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                            &nbsp;<dx:ASPxButton CssClass="FBotton" ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExcel_Click"  ></dx:ASPxButton>
                        </div>
                    </div>

                <div class="well-g">
                    <br />
                    <div class="row">
                        <div class="col-md-12" >
                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                <ContentTemplate>
                                    <div class="col-sm-12">
                                        <dx:ASPxGridView ID="gvMonitorTrafico" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvMonitorTrafico" EnableTheming="True" KeyFieldName="IdSolicitud"
                                            Styles-Header-HorizontalAlign="Center" Theme="Office2010Black" Width="100%" OnRowCommand="gvMonitorTrafico_RowCommand" >
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Sol." FieldName="IdSolicitud" VisibleIndex="0" Visible="true" Width="50px">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Cliente" FieldName="CodigoCliente" VisibleIndex="1" Visible="true" Width="70px">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Contrato" FieldName="ClaveContrato" VisibleIndex="2" Visible="true" Width="70px">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Solicitante" FieldName="Membresia" VisibleIndex="3" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataDateColumn Caption="Fecha  &nbsp; &nbsp; &nbsp; &nbsp; ETD" FieldName="FechaVuelo" VisibleIndex="4" Visible="true" HeaderStyle-HorizontalAlign="Center" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy HH:mm" CellStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataDateColumn>                                               
                                                <dx:GridViewDataColumn Caption="ETD" FieldName="ETD" VisibleIndex="5" Visible="false">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Grupo de Modelo" FieldName="GrupoModelo" VisibleIndex="6" Visible="true" Width="150PX">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="BASE" FieldName="Base" VisibleIndex="7" Visible="true" Width="60px">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Origen <br /> Solicitud" FieldName="OrigenSol" VisibleIndex="8" Visible="true">                                                    
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Contacto" FieldName="NombreContacto" VisibleIndex="9" Width="150px" Visible="true"> 
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="TRIP" FieldName="TRIP" VisibleIndex="10" Visible="true" Width="130px">                                                   
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Usuario <br /> Registro" FieldName="UsuarioCreacion" VisibleIndex="11" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Estatus" FieldName="EstatusSolicitud" VisibleIndex="12" Visible="true">
                                                </dx:GridViewDataColumn>                                                
                                                <dx:GridViewDataColumn Caption="ViabilidadDespacho" FieldName="ViabilidadDespacho" VisibleIndex="13" Visible="false">
                                                </dx:GridViewDataColumn>  
                                                <dx:GridViewDataColumn FieldName="Estatus"  VisibleIndex="14" Caption="Viabilidad" HeaderStyle-HorizontalAlign="Center" Width="120px"> 
                                                <DataItemTemplate>

                                                <dx:ASPxImage runat="server" ID="imgTemplate" Width="15px" Height="15px" ImageAlign="AbsMiddle"
                                                    ImageUrl='<%# "~/img/iconos/" + Eval("EstatusImg") %>'>
                                                </dx:ASPxImage>
                                                 
                                                <dx:ASPxLabel runat="server" ID="lblStatus" Text='<%#Eval("Estatus")%>' CssClass="FGrid" ></dx:ASPxLabel>
                                                </DataItemTemplate>
                                                </dx:GridViewDataColumn> 

                                                <dx:GridViewDataColumn Caption="Acciones" Width="300px" CellStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxButton Text="Notas" Theme="Office2010Black" ID="btnNotas" runat="server" CommandArgument='<%# Eval("IdSolicitud") %>' CommandName="Notas" AutoPostBack="true" ToolTip="Notas">
                                                        </dx:ASPxButton>
                                                        <dx:ASPxButton Text="Trip" Theme="Office2010Black" ID="btnTrip" CommandArgument='<%# Eval("IdSolicitud") %>' runat="server" CommandName="Trip" AutoPostBack="true" ToolTip="Trip">
                                                        </dx:ASPxButton>
                                                        <dx:ASPxButton  Text="Pasajeros" Theme="Office2010Black" ID="btnSolicitud" CommandArgument='<%# Eval("IdSolicitud") %>' runat="server" CommandName="Solicitud" AutoPostBack="true" ToolTip="Pasajeros" Visible="false">
                                                        </dx:ASPxButton>
                                                        <dx:ASPxButton Text="Confirmación" Theme="Office2010Black" ID="btnConfirmacion" CommandArgument='<%# Eval("IdSolicitud") %>' runat="server" CommandName="Confirmacion" AutoPostBack="true" ToolTip="Confirmación">
                                                        </dx:ASPxButton>                                                        
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                            <Templates>
                                                <DetailRow>
                                                    <dx:ASPxGridView CssClass="FGrid" ID="gvPiernas" ClientInstanceName="gvPiernas" runat="server"
                                                        KeyFieldName="idSolicitud" Width="50%" Theme="Office2010Black" OnBeforePerformDataSelect="gvPiernas_BeforePerformDataSelect" Styles-DetailCell-HorizontalAlign="Center"
                                                         Styles-Header-HorizontalAlign="Center" OnRowCommand="gvPiernas_RowCommand">
                                                        <Columns>
                                                            
                                                            <dx:GridViewDataColumn Caption="Origen" FieldName="Origen" VisibleIndex="1" />
                                                            <dx:GridViewDataColumn Caption="Destino" FieldName="Destino" VisibleIndex="2" />
                                                            <dx:GridViewDataColumn Caption="Fecha de vuelo" FieldName="FechaVuelo" VisibleIndex="3" />
                                                            <dx:GridViewDataColumn Caption="Pax" FieldName="NoPax" VisibleIndex="4" />
                                                            <dx:GridViewDataColumn Caption="Acciones" Width="200px" CellStyle-HorizontalAlign="Center" VisibleIndex="5">
                                                                <DataItemTemplate>
                                                                    <dx:ASPxButton Text="Pasajeros" Theme="Office2010Black" ID="btnPasajeros" CommandArgument='<%# Eval("IdTramo") %>' runat="server" CommandName="Pasajeros" AutoPostBack="true" ToolTip="Pasajeros">
                                                                        </dx:ASPxButton>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:ASPxGridView>
                                                </DetailRow>
                                            </Templates>
                                            <Settings HorizontalScrollBarMode="Auto" />
                                            <StylesPager  Pager-CssClass="FNumPag"></StylesPager>
                                            <SettingsDetail ShowDetailRow="true" />
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="true" />
                                            <Settings ShowGroupPanel="True" />
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvMonitorTrafico" ></dx:ASPxGridViewExporter>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnExcel" />
                                    <asp:PostBackTrigger ControlID="btnExportar" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col-sm-6">
                           
                        </div>
                        <div class="col-sm-6" style="text-align: right;">
                            <dx:ASPxLabel CssClass="FExport" runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
                            &nbsp;<dx:ASPxButton CssClass="FBotton" ID="btnExportar" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExcel_Click" ></dx:ASPxButton>
                        </div>
                    </div>
                    <br />

                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>

    <dx:ASPxPopupControl ID="popup" runat="server" ClientInstanceName="popup" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true" Width="300">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
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
                                        <dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px" Theme="Office2010Black" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
                                            <ClientSideEvents Click="function(s, e) {popup.Hide(); ppRecibido.Hide(); }" />
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

    <dx:ASPxPopupControl ID="ppTrip" runat="server" ClientInstanceName="ppTrip" CloseAction="CloseButton" Width="700px" CloseOnEscape="true" Modal="true" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Asignación de TRIP">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel4" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />
                                    <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click"></dx:ASPxButton>
                                    <br />
                                    <br />
                                </div>
                            </div>
                            <div>
                                <dx:ASPxGridView CssClass="FGrid" ID="gvTrip" runat="server" AutoGenerateColumns="False" 
                                    ClientInstanceName="gvTrip" EnableTheming="True" KeyFieldName="IdTrip"
                                    OnCellEditorInitialize="gvTrip_CellEditorInitialize" OnRowDeleting="gvTrip_RowDeleting"
                                    OnRowInserting="gvTrip_RowInserting" OnRowUpdating="gvTrip_RowUpdating"
                                    OnStartRowEditing="gvTrip_StartRowEditing" OnRowValidating="gvTrip_RowValidating"
                                    Styles-Header-HorizontalAlign="Center"
                                    Theme="Office2010Black" Width="100%" StylesPopup-EditForm-ModalBackground-Opacity="90">
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

                                            <CellStyle CssClass="col-lg-6"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button" Caption="Acciones" ShowDeleteButton="True" CellStyle-CssClass="col-lg-6" ShowEditButton="True" ShowNewButton="false"
                                            ShowInCustomizationForm="False" VisibleIndex="6">
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
                                        <EditButton Styles-Style-CssClass="FBotton">
                                            <Image Height="20px" ToolTip="Modificar" Width="20px">
                                            </Image>
                                        </EditButton>
                                        <DeleteButton Styles-Style-CssClass="FBotton">
                                            <Image Height="20px" ToolTip="Eliminar" Width="20px">
                                            </Image>
                                        </DeleteButton>
                                    </SettingsCommandButton>


                                </dx:ASPxGridView>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />
                                    <dx:ASPxButton CssClass="FBotton" ClientInstanceName="btnSalir" runat="server" Text="Salir" Theme="Office2010Black">
                                        <ClientSideEvents Click="function() {ppTrip.Hide(); }" />
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

    <dx:ASPxPopupControl ID="ppNotas" runat="server" ClientInstanceName="ppNotas" CloseAction="CloseButton" Width="700px" CloseOnEscape="true" Modal="true" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Notas">
         <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />
                                    
                                    <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel runat="server" ClientInstanceName="lblNotas" Text="Notas:"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo CssClass="FExport" ID="mNotas" ClientInstanceName="mNotas" runat="server" Height="100px" Width="500px">
                                                <ClientSideEvents  Init="function(){mNotas.SetEnabled(false);  }"/>
                                            </dx:ASPxMemo>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                             <dx:ASPxLabel CssClass="FExport" runat="server" ClientInstanceName="lblPreferencias" Text="Preferencias:"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo ID="mPreferencias" ClientInstanceName="mPreferencias" runat="server" Height="100px" Width="500px">
                                                <ClientSideEvents  Init="function(){mPreferencias.SetEnabled(false);}"/>
                                            </dx:ASPxMemo>
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
                                    <dx:ASPxButton CssClass="FBotton" ClientInstanceName="btnSalir" runat="server" Text="Aceptar" Theme="Office2010Black">
                                        <ClientSideEvents Click="function() {ppNotas.Hide(); }" />
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

    <dx:ASPxPopupControl ID="ppConfirmacion" runat="server" ClientInstanceName="ppConfirmacion" CloseAction="CloseButton" Width="400px" CloseOnEscape="true" Modal="true" Theme="Office2010Black"
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
                                            <dx:ASPxLabel CssClass="FExport" ID="lblConfirmacion" runat="server" Text="¿Realmente está seguro de confirmar la solicitud?"></dx:ASPxLabel>
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
                                    
                                    <dx:ASPxButton CssClass="FBotton" ClientInstanceName="btnConfirmar" ID="btnConfirmar" runat="server" Text="Aceptar" Theme ="Office2010Black" OnClick="btnConfirmar_Click">
                                        <ClientSideEvents Click="function() {ppConfirmacion.Hide(); }" />
                                    </dx:ASPxButton>

                                    <dx:ASPxButton CssClass="FBotton" ClientInstanceName="btnSalir" runat="server" Text="Cancelar" Theme="Office2010Black">
                                        <ClientSideEvents Click="function() {ppConfirmacion.Hide(); }" />
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

    <dx:ASPxPopupControl ID="ppPasajeros" runat="server" ClientInstanceName="ppPasajeros" CloseAction="CloseButton" Width="700px" CloseOnEscape="true" Modal="true" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" AllowDragging="true" HeaderText="Pasajeros">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel6" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="row">
                               <dx:ASPxGridView ID="gvPasajeros" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                                        ClientInstanceName="gvPasajeros" EnableTheming="True" KeyFieldName="IdPax"
                                                        Styles-Header-HorizontalAlign="Center"  StylesPopup-EditForm-ModalBackground-Opacity="90" 
                                                        Theme="Office2010Black" Width="580px" >                                                       
                                                        <Columns>

                                                            <dx:GridViewDataComboBoxColumn FieldName="NombrePax" Caption="Nombre Pax" VisibleIndex="0" Visible="true">                                                                
                                                                <EditFormSettings Visible="True"></EditFormSettings>
                                                            </dx:GridViewDataComboBoxColumn>
                                                                                                                     

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
                            </div>
                            <div>
                                
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />                                                                        

                                    <dx:ASPxButton CssClass="FBotton" ClientInstanceName="btnSalir" runat="server" Text="Salir" Theme="Office2010Black">
                                        <ClientSideEvents Click="function() {ppPasajeros.Hide(); }" />
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
