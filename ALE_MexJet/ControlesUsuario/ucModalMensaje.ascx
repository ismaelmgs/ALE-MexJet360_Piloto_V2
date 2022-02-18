<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucModalMensaje.ascx.cs" Inherits="ALE_MexJet.ControlesUsuario.ucModalMensaje" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<dx:ASPxPopupControl ID="ppMensaje" runat="server" ClientInstanceName="ppMensaje" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" Theme="Office2010Black"
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
                                    <dx:ASPxButton ID="btOK" runat="server" Text="OK" Theme="Office2010Black" Width="80px" AutoPostBack="false" 
                                        Style="float: left; margin-right: 8px" TabIndex="0">
                                        <ClientSideEvents Click="function(s, e) 
                                                                 {
                                                                    ppMensaje.Hide(); 
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



<%--<ajax:ModalPopupExtender ID="mpext" runat="server" BackgroundCssClass="overlayy"
    TargetControlID="pnlPopup" PopupControlID="pnlPopup" OkControlID="btnOk" CancelControlID="btnOk">
</ajax:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server" BackColor="White" Style="display: none;" DefaultButton="btnOk">
    <table width="100%">
        <tr >
            <td colspan="2" align="left" runat="server" id="tdCaption" class="thGrv">
                &nbsp; <asp:Label ID="lblCaption" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 60px" valign="middle" align="center">
                <asp:Image ID="imgInfo" runat="server" ImageUrl="~/img/iconos/Information2.ico" />
            </td>
            <td valign="middle" align="left">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblError"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnOk" runat="server" Text="Aceptar" OnClientClick="OculaModal();" OnClick="btnOk_Click" CssClass="button" />
            </td>
        </tr>
    </table>
</asp:Panel>--%>

<script type="text/javascript">
    function fnClickOK(sender, e) {
        __doPostBack(sender, e);
    }

    <%--function OculaModal()
    {
        var modalId = '<%=mpext.ClientID%>';
        var modal = $find(modalId);
        modal.hide();
    }--%>
</script>