<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoucherError.aspx.cs" Inherits="tp_web_equipo_8a.VoucherError" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5 text-center">
            <div class="alert alert-danger" role="alert">
                <asp:Label ID="lblMensajeError" runat="server" Text="El código ingresado no es válido o ya ha sido utilizado." />
            </div>
            <asp:Button ID="btnInicio" runat="server" Text="Volver al inicio" CssClass="btn btn-primary" OnClick="btnInicio_Click" />
        </div>
    </form>

  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
