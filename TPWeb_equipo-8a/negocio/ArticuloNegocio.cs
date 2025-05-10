using dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ArticuloNegocio
    {

        public Articulo obtenerPorId(int idArticulo)
        {
            AccesoBD datos = new AccesoBD();
            Articulo articulo = new Articulo();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, " +
                                     "C.Descripcion AS Categoria, M.Descripcion AS Marca, I.ImagenUrl " +
                                     "FROM Articulos A " +
                                     "LEFT JOIN Categorias C ON A.IdCategoria = C.Id " +
                                     "LEFT JOIN Marcas M ON A.IdMarca = M.Id " +
                                     "LEFT JOIN Imagenes I ON I.IdArticulo = A.Id " +
                                     "WHERE A.Id = @idArticulo");
                datos.setearParametro("@idArticulo", idArticulo);
                datos.ejecutarLectura();

                bool banderaImagenes = false;

                while (datos.Lectorbd.Read())
                {
                    if (banderaImagenes)
                    {
                        articulo.Imagen.Add(Convert.ToString(datos.Lectorbd["ImagenUrl"]));
                        continue;
                    }

                    articulo.Id = Convert.ToInt32(datos.Lectorbd["Id"]);
                    articulo.Codigo = Convert.ToString(datos.Lectorbd["Codigo"]);
                    articulo.Nombre = Convert.ToString(datos.Lectorbd["Nombre"]);
                    articulo.Descripcion = Convert.ToString(datos.Lectorbd["Descripcion"]);
                    articulo.Precio = Convert.ToDecimal(datos.Lectorbd["Precio"]);

                    articulo.Marca = new Marca
                    {
                        Descripcion = Convert.ToString(datos.Lectorbd["Marca"])
                    };

                    articulo.Categoria = new Categoria
                    {
                        Descripcion = Convert.ToString(datos.Lectorbd["Categoria"])
                    };

                    // Manejo de imágenes
                    articulo.Imagen = new List<string>();
                    articulo.Imagen.Add(Convert.ToString(datos.Lectorbd["ImagenUrl"]));

                    banderaImagenes = true;
                }

                return articulo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoBD datos = new AccesoBD();

            try
            {
                datos.setearConsulta("SELECT " +
                    "A.Id, " +
                    "A.Codigo, " +
                    "A.Nombre, " +
                    "A.Descripcion, " +
                    "A.Precio, " +
                    "C.Id AS IdCategoria, " +
                    "C.Descripcion AS Categoria, " +
                    "M.Id AS IdMarca, " +
                    "M.Descripcion AS Marca, " +
                    "I.ImagenUrl " +
                    "FROM Articulos A " +
                    "LEFT JOIN Categorias C ON A.IdCategoria = C.Id " +
                    "LEFT JOIN Marcas M ON A.IdMarca = M.Id " +
                    "LEFT JOIN Imagenes I ON I.IdArticulo = A.Id " +
                    "ORDER BY A.Id, I.ImagenUrl");

                datos.ejecutarLectura();

                Articulo ultCarga = null;

                while (datos.Lectorbd.Read())
                {
                    int idArtBD = Convert.ToInt32(datos.Lectorbd["Id"]);

                    //para no cargar duplicado, si es el mismo solo agrego imagen
                    if (ultCarga != null && ultCarga.Id == idArtBD)
                    {
                        string imagenUrl = Convert.ToString(datos.Lectorbd["ImagenUrl"]);
                        ultCarga.Imagen.Add(imagenUrl);
                        continue;
                    }

                    Articulo aux = new Articulo();
                    aux.Id = idArtBD;
                    aux.Codigo = Convert.ToString(datos.Lectorbd["Codigo"]);
                    aux.Nombre = Convert.ToString(datos.Lectorbd["Nombre"]);
                    aux.Descripcion = Convert.ToString(datos.Lectorbd["Descripcion"]);

                    aux.Marca = new Marca();

                    if (datos.Lectorbd["IdMarca"] != DBNull.Value)
                    {
                        aux.Marca.Id = Convert.ToInt32(datos.Lectorbd["IdMarca"]);
                        aux.Marca.Descripcion = Convert.ToString(datos.Lectorbd["Marca"]);
                    }
                    else
                    {
                        aux.Marca.Id = 0;
                    }

                    aux.Categoria = new Categoria();
                    if (datos.Lectorbd["IdCategoria"] != DBNull.Value)
                    {
                        aux.Categoria.Id = Convert.ToInt32(datos.Lectorbd["IdCategoria"]);
                        aux.Categoria.Descripcion = Convert.ToString(datos.Lectorbd["Categoria"]);
                    }
                    else
                    {
                        aux.Categoria.Id = 0;
                    }

                    aux.Precio = Decimal.Round(Convert.ToDecimal(datos.Lectorbd["Precio"]), 2);

                    aux.Imagen = new List<string>();
                    aux.Imagen.Add(Convert.ToString(datos.Lectorbd["ImagenUrl"]));
                    aux.Precio = Decimal.Round((decimal)datos.Lectorbd["Precio"], 2);

                    lista.Add(aux);
                    ultCarga = aux;
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Articulo newArticulo)
        {
            AccesoBD datos = new AccesoBD();
            try
            {
                //datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria) " +
                //                     "VALUES (@codigo, @nombre, @descripcion, @precio, @idMarca, @idCategoria)");

                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria) " +
                                     "VALUES (@codigo, @nombre, @descripcion, @precio, @idMarca, @idCategoria); " +
                                     "SELECT SCOPE_IDENTITY();"
                                    );


                datos.setearParametro("@codigo", newArticulo.Codigo);
                datos.setearParametro("@nombre", newArticulo.Nombre);
                datos.setearParametro("@descripcion", newArticulo.Descripcion);
                datos.setearParametro("@precio", newArticulo.Precio);
                datos.setearParametro("@idMarca", newArticulo.Marca.Id);
                datos.setearParametro("@idCategoria", newArticulo.Categoria.Id);

                //el ej accion con return da el id pero el id de la bd, el q asigfna automaticamente
                //q lo necesitamos para agregar las imagenes
                //se le agrego a la consulta:
                // SELECT SCOPE_IDENTITY(); devuelve el último ID autogenerado(IDENTITY) insertado en la misma conexión y el mismo scope.
                //                          Se usa después de un INSERT para obtener el ID recién creado.
                //SIN el scope devolvia null 

                newArticulo.Id = datos.ejecutarAccionconreturn();

                datos.limpiarParametros();

                //no hace falta borrar imahenes cono con el modi, pq el id no tiene ninguna
                //por cada imagen cargada en la lsita, insert a la bd tabla de imagenes
                foreach (string imagen in newArticulo.Imagen)
                {
                    if (imagen == "")
                    { continue; }
                    datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@idarticulo, @imagenurl)");
                    datos.setearParametro("@imagenurl", imagen);
                    datos.setearParametro("@idarticulo", newArticulo.Id);
                    datos.ejecutarMasAcciones();
                    datos.limpiarParametros();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Articulo articulo)
        {
            AccesoBD datosTablaArticulos = new AccesoBD();

            try
            {
                //Esta era la consulta original antes de agregar la posibilidad de borrar las marcas y categorias

                //datosTablaArticulos.setearConsulta("UPDATE ARTICULOS SET " +
                //    "Codigo = @codigo, " +
                //    "Nombre = @nombre, " +
                //    "Descripcion = @descripcion, " +
                //    "IdMarca = @idmarca, " +
                //    "IdCategoria = @idcategoria, " +
                //    "Precio = @precio " +
                //    "WHERE Id = @id");


                //SE CAMBIO POR SI esta null la marca o la categoria, (si fuer borrada), entonces para q no se mande a la bd 

                string consulta = "UPDATE ARTICULOS SET " +
                                  "Codigo = @codigo, " +
                                  "Nombre = @nombre, " +
                                  "Descripcion = @descripcion, ";

                if (articulo.Marca != null)
                {
                    consulta += "IdMarca = @idmarca, ";
                }

                if (articulo.Categoria != null)
                {
                    consulta += "IdCategoria = @idcategoria, ";
                }

                consulta += "Precio = @precio " + "WHERE Id = @id";

                datosTablaArticulos.setearConsulta(consulta);
                datosTablaArticulos.setearParametro("@id", articulo.Id);
                datosTablaArticulos.setearParametro("@codigo", articulo.Codigo);
                datosTablaArticulos.setearParametro("@nombre", articulo.Nombre);
                datosTablaArticulos.setearParametro("@descripcion", articulo.Descripcion);
                datosTablaArticulos.setearParametro("@precio", articulo.Precio);

                //CONSULTA ORIGINAL
                //datosTablaArticulos.setearParametro("@idmarca", articulo.Marca.Id);
                //datosTablaArticulos.setearParametro("@idcategoria", articulo.Categoria.Id);

                if (articulo.Marca != null)
                {
                    datosTablaArticulos.setearParametro("@idmarca", articulo.Marca.Id);
                }

                if (articulo.Categoria != null)
                {
                    datosTablaArticulos.setearParametro("@idcategoria", articulo.Categoria.Id);
                }

                datosTablaArticulos.ejecutarAccion();
                datosTablaArticulos.limpiarParametros();

                datosTablaArticulos.setearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @idarticulo");
                datosTablaArticulos.setearParametro("@idarticulo", articulo.Id);
                datosTablaArticulos.ejecutarMasAcciones();
                datosTablaArticulos.limpiarParametros();

                foreach (string imagen in articulo.Imagen)
                {
                    if (imagen == "")
                    { continue; }

                    datosTablaArticulos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@idarticulo, @imagenurl)");
                    datosTablaArticulos.setearParametro("@imagenurl", imagen);
                    datosTablaArticulos.setearParametro("@idarticulo", articulo.Id);
                    datosTablaArticulos.ejecutarMasAcciones();
                    datosTablaArticulos.limpiarParametros();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datosTablaArticulos.cerrarConexion();
            }
        }


        public void eliminar(int Id)
        {
            try
            {
                AccesoBD datos = new AccesoBD();
                datos.setearConsulta("delete from articulos where Id = @Id");
                datos.setearParametro("@Id", Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoBD datos = new AccesoBD();

            try
            {
                string consulta = "SELECT " +
                    "A.Id, " +
                    "A.Codigo, " +
                    "A.Nombre, " +
                    "A.Descripcion, " +
                    "A.Precio, " +
                    "C.Id AS IdCategoria, " +
                    "C.Descripcion AS Categoria, " +
                    "M.Id AS IdMarca, " +
                    "M.Descripcion AS Marca, " +
                    "I.ImagenUrl " +
                    "FROM Articulos A " +
                    "LEFT JOIN Categorias C ON A.IdCategoria = C.Id " +
                    "LEFT JOIN Marcas M ON A.IdMarca = M.Id " +
                    "LEFT JOIN Imagenes I ON I.IdArticulo = A.Id where ";

                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Precio > " + filtro;
                            break;

                        case "Menor a":
                            consulta += "Precio < " + filtro;
                            break;

                        default:
                            consulta += "Precio = " + filtro;
                            break;
                    }

                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "A.Nombre like '" + filtro + "%'";
                            break;

                        case "Termina con":
                            consulta += "A.Nombre like '%" + filtro + "'";
                            break;

                        default:
                            consulta += "A.Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "M.Descripcion like '" + filtro + "%'";
                            break;

                        case "Termina con":
                            consulta += "M.Descripcion like '%" + filtro + "'";
                            break;

                        default:
                            consulta += "M.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                Articulo ultCarga = null;

                while (datos.Lectorbd.Read())
                {
                    int idArtBD = Convert.ToInt32(datos.Lectorbd["Id"]);

                    //para no cargar duplicado, si es el mismo solo agrego imagen
                    if (ultCarga != null && ultCarga.Id == idArtBD)
                    {
                        string imagenUrl = Convert.ToString(datos.Lectorbd["ImagenUrl"]);
                        ultCarga.Imagen.Add(imagenUrl);
                        continue;
                    }

                    Articulo aux = new Articulo();
                    aux.Id = idArtBD;
                    aux.Codigo = Convert.ToString(datos.Lectorbd["Codigo"]);
                    aux.Nombre = Convert.ToString(datos.Lectorbd["Nombre"]);
                    aux.Descripcion = Convert.ToString(datos.Lectorbd["Descripcion"]);

                    aux.Marca = new Marca();

                    if (datos.Lectorbd["IdMarca"] != DBNull.Value)
                    {
                        aux.Marca.Id = Convert.ToInt32(datos.Lectorbd["IdMarca"]);
                        aux.Marca.Descripcion = Convert.ToString(datos.Lectorbd["Marca"]);
                    }
                    else
                    {
                        aux.Marca.Id = 0;
                    }

                    aux.Categoria = new Categoria();
                    if (datos.Lectorbd["IdCategoria"] != DBNull.Value)
                    {
                        aux.Categoria.Id = Convert.ToInt32(datos.Lectorbd["IdCategoria"]);
                        aux.Categoria.Descripcion = Convert.ToString(datos.Lectorbd["Categoria"]);
                    }
                    else
                    {
                        aux.Categoria.Id = 0;
                    }

                    aux.Precio = Decimal.Round(Convert.ToDecimal(datos.Lectorbd["Precio"]), 2);

                    aux.Imagen = new List<string>();
                    aux.Imagen.Add(Convert.ToString(datos.Lectorbd["ImagenUrl"]));
                    aux.Precio = Decimal.Round((decimal)datos.Lectorbd["Precio"], 2);

                    lista.Add(aux);
                    ultCarga = aux;
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //Para elegirPremio
        public List<Articulo> listarArticulosConImagen()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoBD datos = new AccesoBD();

            try
            {
                datos.setearConsulta(
                    "SELECT A.Id, A.Nombre, A.Descripcion, I.ImagenUrl " +
                    "FROM ARTICULOS A " +
                    "LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id " +
                    "ORDER BY A.Id, I.ImagenUrl"
                );
                datos.ejecutarLectura();



                Articulo ultCarga = null;

                while (datos.Lectorbd.Read())
                {
                    int idArtBD = Convert.ToInt32(datos.Lectorbd["Id"]);

                    if (ultCarga != null && ultCarga.Id == idArtBD)
                    {
                        string imagenUrl = Convert.ToString(datos.Lectorbd["ImagenUrl"]);
                        ultCarga.Imagen.Add(imagenUrl);
                        continue;
                    }

                    Articulo aux = new Articulo();
                    aux.Id = Convert.ToInt32(datos.Lectorbd["Id"]);
                    aux.Nombre = Convert.ToString(datos.Lectorbd["Nombre"]);
                    aux.Descripcion = Convert.ToString(datos.Lectorbd["Descripcion"]);
                    aux.Imagen = new List<string> { Convert.ToString(datos.Lectorbd["ImagenUrl"]) };
                    lista.Add(aux);

                    ultCarga = aux;
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }

}