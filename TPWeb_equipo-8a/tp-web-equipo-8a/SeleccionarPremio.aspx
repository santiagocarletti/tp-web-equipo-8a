<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeleccionarPremio.aspx.cs" Inherits="tp_web_equipo_8a.SeleccionarPremio" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Seleccionar Premio</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .card-custom {
            min-height: 350px;
            border: 1px solid #ccc;
            padding: 1rem;
            position: relative;
        }

        .carousel-control-prev-icon,
        .carousel-control-next-icon {
            filter: invert(100%);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-dark bg-dark">
            <div class="container-fluid">
                <span class="navbar-brand mb-0 h1">Promo Ganá!</span>
            </div>
        </nav>

        <div class="container mt-5">
            <div class="row justify-content-center mb-4">
                <div class="col-md-8 text-center">
                    <h2 class="fw-bold">Elegí tu premio</h2>
                </div>
            </div>

            <div class="row justify-content-center">
                <% 
                    var articulos = new negocio.ArticuloNegocio().listarArticulosConImagen();
                    foreach (var articulo in articulos)
                    {
                %>
                <div class="col-md-4 mb-4">
                    <div class="card card-custom text-center">
                        <div id="carousel_<%= articulo.Id %>" class="carousel slide">
                            <div class="carousel-inner">
                                <% for (int i = 0; i < articulo.Imagen.Count; i++)
                                    { %>
                                <div class="carousel-item <%= i == 0 ? "active" : "" %>">
                                    <img
                                        src="<%= articulo.Imagen[i] %>"
                                        class="d-block w-100"
                                        style="max-height: 350px; object-fit: contain;" />
                                </div>
                                <% } %>
                            </div>

                            <% if (articulo.Imagen.Count > 1)
                                { %>
                            <button class="carousel-control-prev" type="button" data-bs-target="#carousel_<%= articulo.Id %>" data-bs-slide="prev">
                                <span class="carousel-control-prev-icon"></span>
                            </button>
                            <button class="carousel-control-next" type="button" data-bs-target="#carousel_<%= articulo.Id %>" data-bs-slide="next">
                                <span class="carousel-control-next-icon"></span>
                            </button>
                            <% } %>
                        </div>

                        <div class="card-body">
                            <h5 class="card-title"><%= articulo.Nombre %></h5>
                            <p class="card-text"><%= articulo.Descripcion %></p>
                            <a href='IngresarDatos.aspx?idArticulo=<%= articulo.Id %>' class="btn btn-primary">Elegir</a>
                        </div>
                    </div>
                </div>
                <% } %>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
