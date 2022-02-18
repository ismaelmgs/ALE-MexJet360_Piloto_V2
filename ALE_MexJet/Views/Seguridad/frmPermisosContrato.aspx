<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmPermisosContrato.aspx.cs" Inherits="ALE_MexJet.Views.Seguridad.frmPermisosContrato" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Styles/Controls.css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=flecha_abre1]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../../img/iconos/flecha_cierra.png");
        });
        $("[src*=flecha_cierra]").live("click", function () {
            $(this).attr("src", "../../img/iconos/flecha_abre1.png");
            $(this).closest("tr").next().remove();
        });

        function onSelectionGridViewAction(s, e) {
            $("#selectionLabel").html("Total rows selected: " + s.GetSelectedRowCount());
        }

        function OcultarModal() {
            var modalId = '<%=ppCamposSeccion.ClientID%>';
            var modal = $find(modalId);
            modal.hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<asp:UpdatePanel ID="upaPrincipal" runat="server" OnUnload="Unnamed_Unload">
        <ContentTemplate>--%>
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Permisos por Rol</span>
                    </div>
                </div>
                <div class="well-g">
                    <div class="row">
                        <div class="col-md-2">&nbsp;</div>
                        <div class="col-md-8" style="padding-left: 45px; padding-right: 40px;">
                            <uc1:ucModalMensaje runat="server" ID="mpeMensaje" />
                            <fieldset>
                                <legend>
                                    <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"></span>
                                </legend>
                                <div class="col-sm-12">
                                    <center>
                                    <div class="row">
                                        <div class="col-md-1">&nbsp;</div>
                                        <div class="col-md-1">
                                            Rol:
                                        </div>
                                        <div class="col-md-4">
                                            <dx:BootstrapComboBox ID="ddlRol" runat="server" AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlRol_SelectedIndexChanged" Width="100%">
                                                <ValidationSettings SetFocusOnError="true" ValidateOnLeave="true" ErrorDisplayMode="Text">
                                                    <RequiredField IsRequired="true" ErrorText="El campo es requerido" />
                                                </ValidationSettings>
                                            </dx:BootstrapComboBox>
                                        </div>
                                        <div class="col-md-1">
                                            Pestaña:
                                        </div>
                                        <div class="col-md-4">
                                            <dx:BootstrapComboBox ID="ddlPestana" runat="server" AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlPestana_SelectedIndexChanged" Width="100%">
                                                <ValidationSettings SetFocusOnError="true" ValidateOnLeave="true" ErrorDisplayMode="Text">
                                                    <RequiredField IsRequired="true" ErrorText="El campo es requerido" />
                                                </ValidationSettings>
                                            </dx:BootstrapComboBox>
                                        </div>
                                        <div class="col-md-1">&nbsp;</div>
                                    </div>
                                </center>
                                </div>
                            </fieldset>
                        </div>
                        <div class="col-md-2">&nbsp;</div>
                    </div>
                    <br />
                    <div class="row">

                        <div class="col-md-12" style="margin-left: -15px; width: 100%;">
                            <%--<asp:UpdatePanel ID="upaDetalle" runat="server" OnUnload="Unnamed_Unload">
                                        <ContentTemplate>--%>
                            <div class="row">
                                <div class="col-md-2">&nbsp;</div>
                                <div class="col-sm-8" style="padding-left: 50px;">
                                    <asp:GridView ID="gvSecciones" runat="server" AutoGenerateColumns="false" CssClass="table" Width="100%" HeaderStyle-HorizontalAlign="Center"
                                        Style="border-top: 1px solid #484848; border-left: 1px solid #484848; border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                        ShowFooter="true" OnRowDataBound="gvSecciones_RowDataBound" DataKeyNames="IdSeccion" OnRowCommand="gvSecciones_RowCommand">
                                        <HeaderStyle CssClass="celda2" HorizontalAlign="Center" />
                                        <RowStyle CssClass="celda6" Height="16px" />
                                        <FooterStyle CssClass="celda3" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Titulo sección" DataField="Descripcion" />
                                            <asp:TemplateField HeaderText="¿Acceso?">
                                                <ItemTemplate>
                                                    <dx:ASPxCheckBox ID="chkSeccion" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDetalle" runat="server" Text="Detalle" CommandName="Detalle"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'>
                                                    </asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdSeccion" runat="server" Text='<%# Eval("IdSeccion") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-2">&nbsp;</div>
                            </div>
                            <%--</ContentTemplate>
                                    </asp:UpdatePanel>--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            
                        </div>
                    </div>
                    <%--<div class="row">
                        <div class="col-md-12" style="margin-left: -15px; width: 100%;">    
                            <div class="row">
                                <div class="col-md-2">&nbsp;</div>
                                <div class="col-sm-8" style="padding-left: 50px;">
                                    <dx:BootstrapGridView ID="gvSecciones2" runat="server" KeyFieldName="IdSeccion" 
                                        OnDataBound="gvSecciones2_DataBound">                                                
                                        <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
                                        <Columns>
                                            <dx:BootstrapGridViewCommandColumn ShowSelectCheckbox="True" Caption="¿Acceso?"/>
                                            <dx:BootstrapGridViewDataColumn FieldName="Descripcion" Caption="Titulo sección"/>
                                        </Columns>
                                        <Templates>
                                            <DetailRow>
                                                <dx:BootstrapGridView ID="gvCampos" runat="server" KeyFieldName="IdCampo" OnDataBound="gvCampos_DataBound"
                                                    OnBeforePerformDataSelect="gvCampos_BeforePerformDataSelect">
                                                    <Columns>
                                                        <dx:BootstrapGridViewDataColumn FieldName="Descripcion" Caption="Nombre campo" VisibleIndex="1" CssClasses-DataCell="hideColumn" CssClasses-HeaderCell="hideColumn" HorizontalAlign="Center" />
                                                        <dx:BootstrapGridViewDataColumn FieldName="Clave" Caption="Clave" VisibleIndex="2" SortIndex="0" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                        <dx:BootstrapGridViewCommandColumn ShowSelectCheckbox="True" Caption="¿Acceso?" />
                                                    </Columns>
                                                    <SettingsPager PageSize="20"></SettingsPager>
                                                </dx:BootstrapGridView>
                                            </DetailRow>
                                        </Templates>
                                        <SettingsDetail ShowDetailRow="true" />
                                        <ClientSideEvents Init="onSelectionGridViewAction" SelectionChanged="onSelectionGridViewAction" EndCallback="onSelectionGridViewAction" />
                                    </dx:BootstrapGridView>
                                </div>
                                <div class="col-md-2">&nbsp;</div>
                            </div>
                        </div>
                    </div>--%>
                    <br />
                    <br />
                    <%--<dx:ASPxButton ID="btnGuardar2" runat="server" Text="Actualizar" OnClick="ASPxButton2_Click"></dx:ASPxButton>--%>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>

    

    <%--MODAL PARA VER LA INFORMACION DE UN FERRY--%>
    <dx:BootstrapPopupControl ID="ppCamposSeccion" runat="server" ClientInstanceName="ppCamposSeccion" 
        CloseAnimationType="Fade" PopupAnimationType="Fade"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="450px" Height="200px" HeaderText="Campos de la sección"
        AllowDragging="true" Modal="true" CloseAction="CloseButton" ShowCloseButton="true">
        <ContentCollection>
            <dx:ContentControl>
                <div class="row">
                    <div class="col-sm-12">
                        <%--<asp:UpdatePanel ID="upaModal" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                                <asp:Panel ID="pnlCampos" runat="server"  ScrollBars="Vertical">
                                    <asp:GridView ID="gvCampos" runat="server" AutoGenerateColumns="false" CCssClass="table" Width="100%" HeaderStyle-HorizontalAlign="Center"
                                        Style="border-top: 1px solid #484848; border-left: 1px solid #484848; border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                        EmptyDataText="No se encontraron resultados para mostrar" OnRowDataBound="gvCampos_RowDataBound" DataKeyNames="IdCampo">
                                        <HeaderStyle CssClass="celda2" HorizontalAlign="Center" />
                                        <RowStyle CssClass="celda6" Height="16px" />
                                        <FooterStyle CssClass="celda3" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Nombre campo" DataField="Descripcion" />
                                            <asp:BoundField HeaderText="Clave" DataField="Clave" />
                                            <asp:TemplateField HeaderText="¿Acceso?">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCampo" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="¿Visible?">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkVisible" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdCampo" runat="server" Text='<%# Eval("IdCampo") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-12" style="text-align: center">
                        <dx:ASPxButton ID="btnGuardar" runat="server" Text="Actualizar" Theme="Office2010Black"
                            OnClick="btnGuardar_Click">
                        </dx:ASPxButton>
                    </div>
                </div>
            </dx:ContentControl>
        </ContentCollection>
    </dx:BootstrapPopupControl>

</asp:Content>
