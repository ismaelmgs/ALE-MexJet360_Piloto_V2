<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmReporteVuelos.aspx.cs" Inherits="ALE_MexJet.Views.Operaciones.frmReporteVuelos" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-md-12">
            <br />
            <fieldset class="Personal">
                <legend>
                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda de Vuelos</span>
                </legend>
                <div class="col-sm-12">


                    <table width="50%" style="text-align: left; margin:0 auto;">
                        <tr>
                            <td>Período:</td>
                            <td>                   
                                <dx:ASPxTextBox ID="txtFecha" runat="server">
                                    <MaskSettings Mask="dd/MM/yyyy" IncludeLiterals="All" ShowHints="true" />
                                </dx:ASPxTextBox>

                            </td>
                            <td align="left" valign="bottom">&nbsp;
                                <dx:ASPxButton ID="btnConsultaVuelos" runat="server" Text="Consulta vuelos" Theme="Office2010Black"></dx:ASPxButton>
                            </td>
                        </tr>
                    </table>

                </div>
            </fieldset>
        </div>
    </div>
    <br />
    <asp:Panel ID="pnlVuelos" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-12" style="margin-left: -15px; width: 102%;">
                <asp:UpdatePanel  ID="UpdatePanel1" runat="server" UpdateMode="Always" OnUnload="UpdatePanel1_Unload">
                    <ContentTemplate>
                        <div class="col-sm-12">

                            <dx:ASPxGridView ID="gvVuelos" runat="server" AutoGenerateColumns="False" Font-Size="Small"
                                    ClientInstanceName="gvVuelos" EnableTheming="True" Styles-Header-HorizontalAlign ="Center"
                                    Theme="Office2010Black" Width="100%" OnRowCommand="gvVuelos_RowCommand" KeyFieldName ="IdVuelo"
                                    OnCommandButtonInitialize ="gvVuelos_CommandButtonInitialize"   
                                    OnCustomButtonInitialize ="gvVuelos_CustomButtonInitialize"                                                         
                                    >                                                        
                                    <ClientSideEvents EndCallback="function (s, e) {
                                        if (s.cpShowPopup)
                                        {
                                            delete s.cpShowPopup;
                                            lbl.SetText(s.cpText);
                                            popup.Show();
                                        }
                                    }" />
                                    <Columns>
                                        <dx:GridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="1">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <DataItemTemplate>

                                                <%--<dx:ASPxButton Text="Ajuste" Theme="Office2010Black" ID="btnAjuste" runat="server" CommandArgument='<%# Eval("IdRemision") %>' CommandName="Ajuste" AutoPostBack="true" 
                                                    ToolTip="Agregar Ajuste"></dx:ASPxButton>--%>

                                                <dx:ASPxCheckBox ID="chkSelecciona" runat="server" Theme="Office2010Black" ToolTip="Seleccionar vuelo"></dx:ASPxCheckBox>

                                            </DataItemTemplate>
                                            <EditFormSettings Visible="false" />
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataTextColumn Caption="Folio Vuelo" FieldName="IdVuelo" ShowInCustomizationForm="True" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Clave Contrato" FieldName="ClaveContrato" ShowInCustomizationForm="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Matrícula" FieldName="Matricula" ShowInCustomizationForm="True" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Origen" FieldName="Origen" ShowInCustomizationForm="True" VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Destino" FieldName="Destino" ShowInCustomizationForm="True" VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Fecha y Hora" FieldName="FechaVuelo" ShowInCustomizationForm="True" VisibleIndex="7">
                                        </dx:GridViewDataTextColumn>
                                        <%--<dx:GridViewDataTextColumn Caption="Contrato" FieldName="ClaveContrato" ShowInCustomizationForm="False" VisibleIndex="7" EditFormSettings-Visible="False">
                                        </dx:GridViewDataTextColumn>--%>                                                           

                                        <%--<dx:GridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="8">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <DataItemTemplate>

                                                <dx:ASPxButton Text="Ajuste" Theme="Office2010Black" ID="btnAjuste" runat="server" CommandArgument='<%# Eval("IdRemision") %>' CommandName="Ajuste" AutoPostBack="true" 
                                                    ToolTip="Agregar Ajuste"></dx:ASPxButton>

                                            </DataItemTemplate>
                                            <EditFormSettings Visible="false" />
                                        </dx:GridViewDataColumn>--%>

                                    </Columns>
                                    <SettingsBehavior ConfirmDelete="True" />
                                    <SettingsPager Position="TopAndBottom">
                                        <PageSizeItemSettings Items="1, 10, 20, 50, 100">
                                        </PageSizeItemSettings>
                                    </SettingsPager>
                                    <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                                    <Settings ShowGroupPanel="True" />
                                    <%--<SettingsText ConfirmDelete="¿Desea eliminar?" />--%>
                                    <SettingsPopup>
                                        <EditForm HorizontalAlign="Center" VerticalAlign="Below" Width="400px" />
                                    </SettingsPopup>
                                    <SettingsSearchPanel Visible="false" />
                                    <SettingsCommandButton>

                                        <NewButton ButtonType="Link">
                                            <Image ToolTip="New">
                                            </Image>
                                        </NewButton>
                                        <UpdateButton Text="Guardar"></UpdateButton>
                                        <CancelButton Text ="Cancelar"></CancelButton>
                                        <EditButton Text="Editar"></EditButton>
                                    </SettingsCommandButton>
                                </dx:ASPxGridView>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
