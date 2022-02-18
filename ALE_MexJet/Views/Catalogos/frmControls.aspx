<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmControls.aspx.cs" UICulture="es" Culture="es-MX" Inherits="ALE_MexJet.Views.Catalogos.frmControls" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


    <div class="panel panel-primary radius">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="panel-title">Catálogo de Controles<a class="anchorjs-link" href="#panel-title"><span class="anchorjs-icon"></span></a></h3>
                    </div>
                    <div class="panel-body">

                        <div class="form-horizontal well">

                            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

                        </div>

                    </div>
                </div>
            

</asp:Content>
