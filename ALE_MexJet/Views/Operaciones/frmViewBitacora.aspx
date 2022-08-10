<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmViewBitacora.aspx.cs" Inherits="ALE_MexJet.Views.Operaciones.frmViewBitacora" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <!-- Compiled and minified Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <!-- Minified JS library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <!-- Compiled and minified Bootstrap JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <style>
        .carousel-indicators .active {
            width: 14px !important;
            height: 14px !important;
            margin: 0 !important;
            background-color: #50c878 !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 0 auto 0 auto;" id="divPrint" runat="server">
            <%--<asp:Image ID="imgFoto" runat="server" />--%>
        </div>
    </form>
</body>
</html>
