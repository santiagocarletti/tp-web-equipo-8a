<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IngresarDatos.aspx.cs" Inherits="tp_web_equipo_8a.IngresarDatos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>

<script>
    function validarCheckbox() {
        // esto para valida los asp priimero
        if (typeof (Page_ClientValidate) == 'function') {
            if (!Page_ClientValidate('registro')) {
                return false;
            }
        }

        var checkbox = document.getElementById('checkbox');
        var errorSpan = document.getElementById('checkboxError');

        if (!checkbox.checked) {
            errorSpan.style.display = 'inline';
            return false;
        }

        errorSpan.style.display = 'none';
        return true;
    }
</script>

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

                    <div class="mb-3 row">
                        <div class="col-md-8">
                            <label for="txtDNI" class="form-label">DNI</label>
                            <asp:RequiredFieldValidator 
                                ControlToValidate="txtDNI"
                                ErrorMessage="El DNI no puede estar en blanco"
                                runat="server"
                                ForeColor="Red"
                                Display="Dynamic"
                                ValidationGroup="GrupoDNI"/>

                            <asp:RegularExpressionValidator
                                ControlToValidate="txtDNI"
                                ErrorMessage=" Campo Inválido"
                                runat="server"
                                ValidationExpression="^[0-9.]+$"
                                ForeColor="Red"
                                Display="Dynamic"
                                ValidationGroup="GrupoDNI"/>
                            <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" placeholder="DNI" type="text" required="1"/>
                        </div>
                        <div class="col-md-4 d-flex align-items-end">
                            <asp:Button 
                                ID="btnBuscarDNI" 
                                runat="server" 
                                Text="Continuar" 
                                CssClass="btn btn-outline-primary w-100" 
                                style="height: 38px; margin-top: 8px;" 
                                ValidationGroup="GrupoDNI"
                                OnClick="btnBuscarDni_Click"
                                OnClientClick="this.form.noValidate = true;" 
                                />
                        </div>
                    </div>
<%
    if (dni)
    {%>
                    <div class="mb-3">
                        <label for="txtNombre" class="form-label">Nombre</label>
                        <asp:RegularExpressionValidator
                            ControlToValidate="txtNombre"
                            ErrorMessage=" Campo Inválido (Solo letras, mínimo 3)"
                            runat="server"
                            ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]{3,}$"
                            ForeColor="Red"
                            Display="Dynamic"
                            ValidationGroup="registro" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Juanito" required="1" />
                    </div>

                    <div class="mb-3">
                        <label for="txtApellido" class="form-label">Apellido</label>
                        <asp:RegularExpressionValidator
                            ControlToValidate="txtApellido"
                            ErrorMessage=" Campo Inválido (Solo letras, mínimo 3)"
                            runat="server"
                            ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]{3,}$"
                            ForeColor="Red"
                            Display="Dynamic"
                            ValidationGroup="registro" />
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Argento" required="1" />
                    </div>

                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">E-mail</label>
                        <asp:RegularExpressionValidator
                            ControlToValidate="txtEmail"
                            ErrorMessage="Correo electrónico inválido"
                            runat="server"
                            ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                            ForeColor="red"
                            Display="Dynamic"
                            ValidationGroup="registro" />
                        <div class="input-group">
                            <div class="input-group-text">@</div>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="email@email.com" required="1" />
                        </div>
                    </div>

                    <div class="mb-3">
                        <label for="txtDireccion" class="form-label">Dirección</label>
                        <asp:RegularExpressionValidator
                            ControlToValidate="txtDireccion"
                            ErrorMessage="Dirección Inválida (Mínimo 3 caracteres)"
                            runat="server"
                            ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s]{3,}$"
                            ForeColor="red"
                            Display="Dynamic"
                            ValidationGroup="registro" />
                        <div class="input-group">
                            <div class="input-group-text">@</div>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Calle" required="1" />
                        </div>
                    </div>

                    <div class="mb-3">
                        <label for="txtCiudad" class="form-label">Ciudad</label>
                        <asp:RegularExpressionValidator
                            ControlToValidate="txtCiudad"
                            ErrorMessage=" Campo Inválido (mínimo 3 caracteres)"
                            runat="server"
                            ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s]{3,}$"
                            ForeColor="Red"
                            Display="Dynamic"
                            ValidationGroup="registro" />
                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" placeholder="Ciudad" required="1" />
                    </div>

                    <div class="mb-3">
                        <label for="txtCp" class="form-label">Código Postal</label>
                        <asp:RegularExpressionValidator
                            ControlToValidate="txtCp"
                            ErrorMessage=" Campo Inválido"
                            runat="server"
                            ValidationExpression="^[0-9]+$"
                            ForeColor="Red"
                            Display="Dynamic"
                            ValidationGroup="registro"/>
                        <asp:TextBox ID="txtCp" runat="server" CssClass="form-control" placeholder="Código Postal" required="1" />
                    </div>

                    <div class="mb-3 form-check">
                        <input type="checkbox" id="checkbox" runat="server" class="form-check-input" />
                        <label class="form-check-label" for="checkbox">Acepto los términos y condiciones.</label>
                        <span id="checkboxError" class="text-danger" style="display: none;">¡Debes aceptar los términos y condiciones!</span>
                    </div>

                    <asp:Button
                        ID="btnParticipar"
                        runat="server"
                        Text="Participar"
                        CssClass="btn btn-primary w-100"
                        OnClientClick="return validarCheckbox();"
                        OnClick="btnParticipar_Click"
                        CausesValidation="true" />
                        <%}%>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
