<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="tp_web_equipo_8a.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form2" runat="server">

            <nav class="navbar navbar-dark bg-dark">
      <div class="container-fluid">
        <span class="navbar-brand mb-0 h1">Promo Ganá!</span>
      </div>
    </nav>
        
        <div class="container mt-5 text-center">

            <asp:Label   text="Error:"  runat="server"  CssClass="active" />
            <div class="alert alert-danger" role="alert">
                <asp:label text="Error" id="lblMensajeError" runat="server" />
            </div>
            <asp:Button ID="btnInicio" runat="server" Text="Volver al inicio" CssClass="btn btn-primary" OnClick="btnInicio_Click" />
        </div>
    </form>

  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
