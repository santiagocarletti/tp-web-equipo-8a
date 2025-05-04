<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IngresarDatos.aspx.cs" Inherits="tp_web_equipo_8a.IngresarDatos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
      <h2 id="aspnetTitle" class="mb-4">Ingresar Datos:</h2>

      <div class="mb-3">
        <label for="txtDNI" class="form-label">DNI</label>
        <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" placeholder="XX.XXX.XXX" />
      </div>

      <div class="mb-3">
        <label for="txtNombre" class="form-label">Nombre</label>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Juanito" />
      </div>

      <div class="mb-3">
        <label for="txtApellido" class="form-label">Apellido</label>
        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Argento" />
      </div>

      <div class="mb-3">
        <label for="txtEmail" class="form-label">E-mail</label>
        <div class="input-group">
          <div class="input-group-text">@</div>
          <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="email@email.com" />
        </div>
      </div>

      <div class="mb-3">
        <label for="txtDireccion" class="form-label">Dirección</label>
        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Calle" />
      </div>

      <div class="mb-3">
        <label for="txtCiudad" class="form-label">Ciudad</label>
        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" placeholder="Ciudad" />
      </div>

      <div class="mb-3">
        <label for="txtCp" class="form-label">Código Postal</label>
        <asp:TextBox ID="txtCp" runat="server" CssClass="form-control" placeholder="Código Postal" />
      </div>

    <div class="mb-3 form-check">
      <input type="checkbox" class="form-check-input" id="checkbox" runat="server" />
      <label class="form-check-label" for="checkbox">Acepto los términos y condiciones.</label>
    </div>

      <asp:Button ID="btnParticipar" runat="server" Text="Participar" CssClass="btn btn-primary w-100" />
    </div>
  </div>
</div>    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>