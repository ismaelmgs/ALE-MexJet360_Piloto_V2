<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmConsultaTrafico.aspx.cs" Inherits="ALE_MexJet.Views.FBO.frmConsultaTrafico" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Src="~/ControlesUsuario/ucClienteContrato.ascx" TagPrefix="uc2" TagName="ucContratos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
                
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 25px;">&nbsp;&nbsp;Consulta Tráfico</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <fieldset class="Personal">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
                            </legend>
                            <div class="col-sm-12">
                                <table width="99%" border="0" style="text-align:center;">
                                    <tr>
                                        <td style="width: 25%">
                                            <uc2:ucContratos ID="cuClienteContrato" runat="server" Theme="Office2010Black"/>
                                        </td>                                        
                                        <td style="height:5%; text-align:right;"> 
                                            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;                                    
                                        </td>
                                        <td style="height:30%; text-align:right;">
                                            
                                            <dx:ASPxTextBox ID="txtNoSolicitud" runat="server" Caption="&nbsp;&nbsp;&nbsp;&nbsp; Solicitud" NullText="Ingrese número de solicitud" Theme="Office2010Black">
                                                <ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                    <RegularExpression ErrorText="El campo contiene informaci&#243;n inv&#225;lida." ValidationExpression="\d+" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox><br />                                                                                        
                                            <dx:ASPxDateEdit ID="deFechaInicial" runat="server" Caption="Fecha Inicial" NullText="Seleccione una fecha" Theme="Office2010Black"></dx:ASPxDateEdit><br />
                                            <dx:ASPxDateEdit ID="deFechaFinal" runat="server" Caption="&nbsp;&nbsp; Fecha Final" NullText="Seleccione una fecha" Theme="Office2010Black"></dx:ASPxDateEdit>                                                                                        
                                        </td>                                                                              
                                        <td style="width: 20%">
                                            <dx:ASPxButton CssClass="FBotton" ID="btnBuscar" runat="server"  Text="Buscar" Theme="Office2010Black" OnClick="btnBuscar_Click">
                                            </dx:ASPxButton> 
                                        </td>
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
                                        <dx:ASPxGridView ID="gvConsultaTrafico" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvConsultaTrafico" 
                                            EnableTheming="True" KeyFieldName="IdSolicitud" Styles-Header-HorizontalAlign="Center" Theme="Office2010Black" Width="100%" 
                                            OnRowCommand="gvConsultaTrafico_RowCommand">
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Sol." FieldName="IdSolicitud" VisibleIndex="0" Visible="true" Width="50px">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Cliente" FieldName="CodigoCliente" VisibleIndex="1" Visible="true" Width="70px">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Contrato" FieldName="ClaveContrato" VisibleIndex="2" Visible="true" Width="70px">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataDateColumn Caption="Fecha  &nbsp; &nbsp; &nbsp; &nbsp; ETD" FieldName="FechaVuelo" VisibleIndex="3" Visible="true" HeaderStyle-HorizontalAlign="Center" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy HH:mm" CellStyle-HorizontalAlign="Center" Width="120px">
                                                </dx:GridViewDataDateColumn>                                               
                                                <dx:GridViewDataColumn Caption="ETD" FieldName="ETD" VisibleIndex="4" Visible="false">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Grupo de Modelo" FieldName="GrupoModelo" VisibleIndex="5" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="BASE" FieldName="Base" VisibleIndex="6" Visible="true" Width="60px">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Origen <br /> Solicitud" FieldName="OrigenSol" VisibleIndex="7" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="TRIP" FieldName="TRIP" VisibleIndex="8" Visible="true" Width="95px">                                                   
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Usuario <br /> Registró" FieldName="UsuarioCreacion" VisibleIndex="9" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Usuario <br /> Confirmó" FieldName="UsuarioModificacionMT" VisibleIndex="10" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataDateColumn Caption="Fecha <br /> Confirmó" FieldName="FechaModificacionMT" VisibleIndex="11" Visible="true" >
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm">                                                        
                                                    </PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>


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

                                                <dx:GridViewDataColumn Caption="Acciones" CellStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <DataItemTemplate>                                                        
                                                        <dx:ASPxButton Text="Editar" Theme="Office2010Black" ID="btnEdicion" CommandArgument='<%# Eval("IdSolicitud") %>' runat="server" CommandName="Edicion" AutoPostBack="true" ToolTip="Edición">
                                                        </dx:ASPxButton>                                                          
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>                                            
                                            <Settings HorizontalScrollBarMode="Hidden" />
                                            <StylesPager  Pager-CssClass="FNumPag"></StylesPager>
                                            <SettingsDetail ShowDetailRow="false" />
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsSearchPanel Visible="true" />
                                            <Settings ShowGroupPanel="True" />
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvConsultaTrafico" ></dx:ASPxGridViewExporter>
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
                                            <dx:ASPxMemo CssClass="FExport" ID="mNotas" ClientInstanceName="mNotas" runat="server" Height="71px" Width="500px">
                                                <ClientSideEvents  Init="function(){mNotas.SetEnabled(false);}"/>
                                            </dx:ASPxMemo>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                             <dx:ASPxLabel CssClass="FExport" runat="server" ClientInstanceName="lblPreferencias" Text="Preferencias:"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo ID="mPreferencias" ClientInstanceName="mPreferencias" runat="server" Height="71px" Width="500px">
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

     <dx:ASPxPopupControl ID="ppEditar" runat="server" ClientInstanceName="ppEditar" Text="" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Formulario de edición" AllowDragging="true" ShowCloseButton="true" Width="700px">
        <ClientSideEvents />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel5" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div>
                                <table>
                                     <tr>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Contacto:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo runat="server" ID="txtContacto" Theme="Office2010Black" ClientEnabled="false"  ></dx:ASPxMemo>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Motivo:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo runat="server" ID="txtMotivo" Theme="Office2010Black" ClientEnabled="false" ></dx:ASPxMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 15px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Tipo Equipo:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo runat="server" ID="txtTipoEquipo" Theme="Office2010Black" ClientEnabled="false" ></dx:ASPxMemo>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Matricula:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo runat="server" ID="txtMatricula" Theme="Office2010Black" ClientEnabled="false" ></dx:ASPxMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 15px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Notas Solicitud:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo ID="mNotasSolicitud" ClientInstanceName="mNotasSolicitud" runat="server" Height="100px" Width="90%" ClientEnabled="false" ></dx:ASPxMemo>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="No Solicitud: " Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel runat="server" ID="lblSolicitud"  Font-Bold="true" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 15px"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <dx:ASPxGridView ID="gvPiernaDic" ClientInstanceName="gvPiernaDic" runat="server"
                                                KeyFieldName="idSolicitud" Width="650px" Theme="Office2010Black">
                                                <Columns>
                                                    <dx:GridViewDataColumn Caption="Origen" FieldName="Origen" VisibleIndex="0" />
                                                    <dx:GridViewDataColumn Caption="Destino" FieldName="Destino" VisibleIndex="1" />
                                                    <dx:GridViewDataColumn Caption="Fecha" FieldName="Fecha" VisibleIndex="2" />
                                                    <dx:GridViewDataColumn Caption="ETD" FieldName="HoraVuelo" VisibleIndex="3" />
                                                    <dx:GridViewDataColumn Caption="No. Pasajeros" FieldName="NoPax" VisibleIndex="4" />
                                                    <dx:GridViewDataColumn Caption="Ultima Area que agrego Pax" FieldName="AreaAgregaPax" VisibleIndex="5" />
                                                </Columns>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 15px"></td>
                                    </tr>
                                </table>
                            </div>

                            
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />
                                    <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click" ></dx:ASPxButton>
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
                            <br />
                            <div class="col-md-12">
                                <table width="100%">
                                    <tr>
                                        <td style="height: 15px"></td>
                                        <td width="50%" align="center">
                                            <dx:ASPxButton runat="server" ID="btnGuardarD" Text="Guardar" Theme="Office2010Black" Visible="false" >
                                                <ClientSideEvents Click="function(){  ppEditar.Hide(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td width="50%" align="center">
                                            <dx:ASPxButton runat="server" ClientInstanceName="btnSalir" Text="Salir" Theme="Office2010Black" ID="btnSalir" OnClick="btnSalir_Click">
                                                <ClientSideEvents Click="function(){  ppEditar.Hide(); popup.Hide();  ppNotas.Hide();  }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <br />

                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

</asp:Content>
