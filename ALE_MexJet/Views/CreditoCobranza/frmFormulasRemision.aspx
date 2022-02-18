<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmFormulasRemision.aspx.cs" Inherits="ALE_MexJet.Views.CreditoCobranza.frmFormulasRemision" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxPanel ID="pnlPrincipal" runat="server" Width="100%" BackColor="White" Style="border-radius: 14px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 20px;">&nbsp;&nbsp;Generación de formulas para Remisiones</span>
                    </div>
                </div>
                <br />

                <uc1:ucModalMensaje ID="mpeMensaje" runat="server" />

                <asp:UpdatePanel ID="upaPrincipal" runat="server" OnUnload="Unnamed_Unload">
                    <ContentTemplate>

                        <div class="well-g">
                            <div class="row">
                                <div class="col-md-12">
                                    <uc1:ucModalMensaje ID="UcModalMensaje1" runat="server" />
                                    <fieldset class="Personal">
                                        <legend>
                                            <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Formulario</span>
                                        </legend>
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <asp:Label AssociatedControlID="ddlFactoresVariables" runat="server" ID="lbFactoresVariables" Text="Factores Variables "></asp:Label>
                                                <asp:DropDownList runat="server" CssClass="combo" ID="ddlFactoresVariables" AutoPostBack="true" OnSelectedIndexChanged="ddlFactoresVariables_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-4">
                                                <asp:Label AssociatedControlID="ddlFactoresFijos" runat="server" ID="lbFactoresFijos" Text="Factores Fijos "></asp:Label>
                                                <asp:DropDownList runat="server" CssClass="combo" ID="ddlFactoresFijos" AutoPostBack="true" OnSelectedIndexChanged="ddlFactoresFijos_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <br />
                                            <div class="col-lg-4">
                                                <asp:button CssClass="FBotton" ID="btnValidar" OnClick="btnValidar_Click" Text="Validar" runat="server" Theme="Office2010Black"></asp:button>
                                                <asp:button CssClass="FBotton" ID="btnActualizar" OnClick="btnActualizar_Click" Text="Guardar" runat="server" Theme="Office2010Black"></asp:button>
                                                <asp:button CssClass="FBotton" ID="btnInsertar" OnClick="btnInsertar_Click" Text="Guardar" runat="server" Theme="Office2010Black"></asp:button>
                                                <asp:button CssClass="FBotton" ID="btnCancelar" OnClick="btnCancelar_Click" Text="Cancelar" runat="server" Theme="Office2010Black"></asp:button>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:Label AssociatedControlID="tbFormula" autocomplete="off" runat="server" ID="lbFormula" Text="Generando Formula "></asp:Label>
                                                <asp:TextBox runat="server" ID="tbFormula" CssClass="form-control col-lg-10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <asp:Label AssociatedControlID="tbcodigo" autocomplete="off" runat="server" ID="lbcodigo" Text="Clave"></asp:Label>
                                                <asp:TextBox runat="server" ID="tbcodigo" CssClass="form-control col-lg-10"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Label AssociatedControlID="tbdescripcion" autocomplete="off" runat="server" ID="lbdescripcion" Text="Descripcion"></asp:Label>
                                                <asp:TextBox runat="server" ID="tbdescripcion" CssClass="form-control col-lg-10"></asp:TextBox>
                                            </div>
                                        </div>


                                    </fieldset>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:UpdatePanel runat="server" UpdateMode="Always" OnUnload="Unnamed_Unload">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvFormulas" runat="server" AutoGenerateColumns="false" CssClass="table"
                                                Style="border-top: 1px solid #484848; border-left: 1px solid #484848; border-right: 1px solid #484848; border-bottom: 1px solid #484848;"
                                                OnRowDataBound="gvFormulas_RowDataBound" DataKeyNames="idFormula" OnRowCommand="gvFormulas_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="codigoF" HeaderText="Codigo Formula" />
                                                    <asp:BoundField DataField="formula" HeaderText="Formula" />
                                                    <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                                                    <asp:TemplateField HeaderText="Acciones">
                                                        <ItemTemplate>
                                                            <asp:Button runat="server" ID="btnActualizar" Text="Actualizar" CommandName="actualizar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" OnClientClick="return confirm('¿Realmente deseas eliminar este registro?');" CommandName="eliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                            <br />
                        </div>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>
