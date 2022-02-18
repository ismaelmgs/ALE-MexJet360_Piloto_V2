<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucClienteContrato.ascx.cs" Inherits="ALE_MexJet.ControlesUsuario.ucClienteContrato" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<script type="text/javascript">
        var lastContrato = null;
        function OnComboClienteChanged(cmbCliente) {            
            document.getElementById("<%=HiddenField1.ClientID%>").value = cmbCliente.GetValue().toString();
            if (cmbContrato.InCallback())
                lastContrato = cmbCliente.GetValue().toString();
             else
                 cmbContrato.PerformCallback(cmbCliente.GetValue().toString());
        }

        function OnComboContratoChanged(cmbContrato) {
            debugger;
            document.getElementById("<%=HiddenField1.ClientID%>").value = cmbContrato.GetValue().toString();
            if (cmbApoyo.InCallback())
                lastContrato = cmbContrato.GetValue().toString();
            else
                cmbApoyo.PerformCallback();
        }

    
</script>
<table id="miTablaCombo" >
    <tr>   
        <td>
            <dx:ASPxLabel ID="lblCliente" runat="server" Text="Cliente:" Theme="Office2010Black"></dx:ASPxLabel>
        </td>  
        <td>
            <dx:ASPxComboBox runat="server" ID="cmbCliente" ClientInstanceName="cmbCliente" NullText="Seleccione una opción" Theme="Office2010Black"
                EnableTheming="True" DropDownStyle="DropDownList" OnSelectedIndexChanged="cmbCliente_SelectedIndexChanged" IncrementalFilteringMode="StartsWith" EnableSynchronization="False" >
                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnComboClienteChanged(s); }" />
            </dx:ASPxComboBox>             
        </td>
    </tr>
    <tr><td colspan="2" style="height:15px"></td></tr>
    <tr>  
        <td>
            <dx:ASPxLabel ID="lblContrato" runat="server" Text="Contrato: " Theme="Office2010Black"></dx:ASPxLabel>
        </td>                  
        <td>
            <dx:ASPxComboBox runat="server" ID="cmbContrato" ClientInstanceName="cmbContrato" NullText="Seleccione una opción" Theme="Office2010Black"
                EnableTheming="True" DropDownStyle="DropDownList" OnSelectedIndexChanged="cmbContrato_SelectedIndexChanged" IncrementalFilteringMode="StartsWith" EnableSynchronization="False">
               <ClientSideEvents SelectedIndexChanged="function(s, e) { OnComboContratoChanged(s); }" />
            </dx:ASPxComboBox>
        </td>         
    </tr>    
</table>

<div style="display:none">    
    <dx:ASPxComboBox runat="server" ID="cmbApoyo" ClientInstanceName="cmbApoyo" NullText="Seleccione una opción"  Theme="Office2010Black"
                EnableTheming="True" DropDownStyle="DropDownList"  IncrementalFilteringMode="StartsWith" EnableSynchronization="False">               
    </dx:ASPxComboBox> 
    <asp:HiddenField ID="HiddenField1" runat="server" />  
</div>



