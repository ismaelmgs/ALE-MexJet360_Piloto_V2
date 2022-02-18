<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmMonitorDespacho.aspx.cs" Inherits="ALE_MexJet.Views.FBO.frmMonitorDespacho" UICulture="es" Culture="es-MX" %>

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
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 25px;">&nbsp;&nbsp;Monitor Despacho</span>
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
                                            <dx:ASPxLabel runat="server" Text="Aeropuerto Base:"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" OnUnload="UpdatePanel1_Unload">
                                                <ContentTemplate>
                                                    <dx:ASPxComboBox ToolTip="Aeropuerto Base" ID="ddlBase" runat="server" Theme="Office2010Black" EnableTheming="True" NullText="Seleccione la Base"
                                                        ValueType="System.Int32" ValueField="idAeropuert">
                                                        <ValidationSettings ErrorDisplayMode="Text">
                                                            <RequiredField IsRequired="true" ErrorText="Seleccione la Base" />
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 20%">
                                            <dx:ASPxButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" Theme="Office2010Black">
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
                <div class="well-g">
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="margin-left: -15px; width: 103%;">
                            <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="UpdatePanel1_Unload">
                                <ContentTemplate>
                                    <div class="col-sm-12">
                                        <dx:ASPxGridView ID="gvMonitorDespacho" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvMonitorDespacho" EnableTheming="True" KeyFieldName="IdSolicitud"
                                            Styles-Header-HorizontalAlign="Center" Theme="Office2010Black" Width="100%" OnRowCommand="gvMonitorDespacho_RowCommand" OnHtmlRowPrepared="gvMonitorDespacho_HtmlRowPrepared">
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Solicitud" FieldName="IdSolicitud" VisibleIndex="0" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Contrato" FieldName="ClaveContrato" VisibleIndex="1" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Solicitante" FieldName="Membresia" VisibleIndex="2" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Tipo Aeronave" FieldName="DescripcionM" VisibleIndex="3" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataDateColumn Caption="Fecha  &nbsp; &nbsp; &nbsp; &nbsp; ETD" FieldName="FechaVuelo" VisibleIndex="4" Visible="true" HeaderStyle-HorizontalAlign="Center" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy HH:mm" CellStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataColumn Caption="ETD" FieldName="ETD" VisibleIndex="5" Visible="false">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="BASE" FieldName="BASE" VisibleIndex="6" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Origen Solicitud" FieldName="OrigenSol" VisibleIndex="7" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Usuario Registro" FieldName="UsuarioCreacion" VisibleIndex="8" Visible="false">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Estatus" FieldName="Descripcion" VisibleIndex="9" Visible="true">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Acciones" VisibleIndex="11" Width="180px">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <DataItemTemplate>
                                                        <%--<dx:ASPxButton Text="Notas" Theme="Office2010Black" ID="btnNotas" runat="server" CommandArgument='<%# Eval("IdSolicitud") %>' CommandName="Notas" AutoPostBack="true" ToolTip="Notas">
                                                        </dx:ASPxButton>--%>
                                                        <dx:ASPxButton Text="Dictamen" Theme="Office2010Black" ID="btnDictamen" ClientInstanceName="btnDictamen" CommandArgument='<%# Eval("IdMonitor") %>' runat="server" CommandName="Dictamen" AutoPostBack="true" ToolTip="Dictamen" Visible='<%# Eval("Descripcion").Equals("CANCELADO") ? false : true %>'>
                                                        </dx:ASPxButton>
                                                        <dx:ASPxButton Text="Enterado" Theme="Office2010Black" ID="btnEnterado" ClientInstanceName="btnEnterado" CommandArgument='<%# Eval("IdMonitor") %>' Visible='<%# Eval("Descripcion").Equals("CANCELADO") %>' runat="server" CommandName="Enterado" AutoPostBack="true" ToolTip="Enterado">
                                                        </dx:ASPxButton>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                            <Templates>
                                                <DetailRow>
                                                    <dx:ASPxGridView ID="gvPiernas" ClientInstanceName="gvPiernas" runat="server"
                                                        KeyFieldName="idSolicitud" Width="50%" Theme="Office2010Black" OnBeforePerformDataSelect="gvPiernas_BeforePerformDataSelect">
                                                        <Columns>
                                                            <dx:GridViewDataColumn Caption="Origen" FieldName="Origen" VisibleIndex="0" />
                                                            <dx:GridViewDataColumn Caption="Destino" FieldName="Destino" VisibleIndex="1" />
                                                            <dx:GridViewDataColumn Caption="Fecha" FieldName="Fecha" VisibleIndex="2" />
                                                            <dx:GridViewDataColumn Caption="ETD" FieldName="HoraVuelo" VisibleIndex="3" />
                                                            <dx:GridViewDataColumn Caption="No. Pasajeros" FieldName="NoPax" VisibleIndex="4" />
                                                            <dx:GridViewDataColumn Caption="Ultima Area que agrego Pax" FieldName="AreaAgregaPax" VisibleIndex="5" />
                                                        </Columns>
                                                    </dx:ASPxGridView>
                                                </DetailRow>
                                            </Templates>
                                            <SettingsSearchPanel Visible="true" />
                                            <Settings ShowGroupPanel="True" />
                                            <SettingsDetail ShowDetailRow="true" />
                                            <SettingsPager Position="TopAndBottom">
                                                <PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                        </dx:ASPxGridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
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
                                            <ClientSideEvents Click="function(){  ppDictamen.Hide(); popup.Hide();  ppNotas.Hide();  mNotas.SetText('');  mObservaiones.SetText(''); rblDictamen.SetValue(-1); }" />
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

    <dx:ASPxPopupControl ID="ppDictamen" runat="server" ClientInstanceName="ppDictamen" Text="" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true" Width="700px">
        <ClientSideEvents />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Contacto:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo runat="server" ID="txtContacto"   Theme="Office2010Black">
                                            </dx:ASPxMemo>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Motivo:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo runat="server" ID="txtMotivo" Theme="Office2010Black" ></dx:ASPxMemo>
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
                                            <dx:ASPxMemo runat="server" ID="txtTipoEquipo" Theme="Office2010Black" ></dx:ASPxMemo>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel runat="server" Text="Matricula:" Theme="Office2010Black"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxMemo runat="server" ID="txtMatricula" Theme="Office2010Black" ></dx:ASPxMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 15px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <dx:ASPxLabel runat="server" ClientInstanceName="lbl" Text="Notas:"></dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxMemo ID="mNotas" ClientInstanceName="mNotas" runat="server" Height="71px" Width="170px">
                                            
                                        </dx:ASPxMemo>
                                    </td>
                                        <td></td> <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 15px"></td>
                                    </tr>
                                    <tr>
                                        
                                        <td colspan="4" style="text-align:center">
                                            <dx:ASPxGridView ID="gvPiernaDic" ClientInstanceName="gvPiernaDic" runat="server"
                                                KeyFieldName="idSolicitud" Width="660px" Theme="Office2010Black">
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

                            <table width="100%" border="0">
                                <tr>
                                    <td style="text-align: left;">
                                        <dx:ASPxLabel runat="server" Text="Es viable el vuelo:" Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td style="text-align: left;">
                                        <dx:ASPxRadioButtonList ID="rblDictamen" runat="server" Theme="Office2010Black" ToolTip="Otorgado"
                                            ClientInstanceName="rblDictamen" RepeatDirection="Horizontal">
                                            <Items>
                                                <dx:ListEditItem Text="Si" Value="1" />
                                                <dx:ListEditItem Text="No" Value="2" />
                                                <dx:ListEditItem Text="Restringido" Value="3" />
                                            </Items>
                                        </dx:ASPxRadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 15px"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel runat="server" Text="Observaciones:" Theme="Office2010Black"></dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxMemo ID="mObservaiones" ClientInstanceName="mObservaiones" runat="server" Height="100px" Width="100%"></dx:ASPxMemo>
                                    </td>
                                </tr>
                            </table>
                            <div class="col-md-12">
                                <table width="100%">
                                    <tr>
                                        <td style="height: 15px"></td>
                                        <td width="50%" align="center">
                                            <dx:ASPxButton runat="server" ID="btnGuardarD" Text="Guardar" Theme="Office2010Black" OnClick="btnGuardarD_Click">
                                                <ClientSideEvents Click="function(){  ppDictamen.Hide(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td width="50%" align="center">
                                            <dx:ASPxButton runat="server" ClientInstanceName="btnSalir" Text="Salir" Theme="Office2010Black" ID="btnSalir">
                                                <ClientSideEvents Click="function(){  ppDictamen.Hide(); popup.Hide();  ppNotas.Hide();  mNotas.SetText('');  mObservaiones.SetText(''); rblDictamen.SetValue(-1); }" />
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

    <dx:ASPxPopupControl ID="ppNotas" runat="server" ClientInstanceName="ppNotas" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" HeaderText="Notas" AllowDragging="true" ShowCloseButton="true" Width="300">
        <ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel4" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table>
                                <tr>
                                    
                                </tr>

                                <tr>
                                    <td>
                                        <dx:ASPxButton runat="server" Text="Salir" Width="80px" Theme="Office2010Black" AutoPostBack="false">
                                            <ClientSideEvents Click="function(){  ppDictamen.Hide(); popup.Hide();  ppNotas.Hide();  mNotas.SetText('');  mObservaiones.SetText(''); rblDictamen.SetValue(-1); }" />
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
