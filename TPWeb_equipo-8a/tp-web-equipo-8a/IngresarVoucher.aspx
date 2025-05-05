<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IngresarVoucher.aspx.cs" Inherits="tp_web_equipo_8a.IngresarVoucher" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Ingresar Voucher</title>
    
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
     
    <nav class="navbar navbar-dark bg-dark">
      <div class="container-fluid">
        <span class="navbar-brand mb-0 h1">Promo Ganá!</span>
      </div>
    </nav>
    
    <div class="container mt-5">
      <div class="row justify-content-center">
        <div class="col-md-6">
         
          <label for="voucherInput" class="form-label">Ingresá el código de tu voucher!</label>
          <asp:TextBox ID="voucherInput" runat="server" CssClass="form-control"  placeholder="XXXXXXXXXXXXXXXXXX" />
          
        <asp:Button  ID="btnSiguiente"  runat="server"  Text="Siguiente" CssClass="btn btn-primary w-100 mt-3" OnClick="btnSiguiente_Click" />

             <asp:Label   ID="lblMessage"  runat="server"  CssClass="text-danger mt-2" />
        </div>
      </div>
    </div>
    </form>
   
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
