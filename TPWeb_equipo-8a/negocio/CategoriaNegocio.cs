using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoBD datos = new AccesoBD();

            try
            {
                datos.setearConsulta("Select Id, Descripcion from CATEGORIAS");
                datos.ejecutarLectura();

                while (datos.Lectorbd.Read())
                {
                    Categoria aux = new Categoria();

                    aux.Id = (int)datos.Lectorbd["Id"];
                    aux.Descripcion = (string)datos.Lectorbd["Descripcion"];
                    lista.Add(aux);
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

        public void Agregar(string nombreCategoria)
        {
            AccesoBD datos = new AccesoBD();
            try
            {
                datos.setearConsulta("INSERT INTO CATEGORIAS (Descripcion) " +
                                     "VALUES (@nombre)");

                datos.setearParametro("@nombre", nombreCategoria);

                datos.ejecutarAccion();
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

        public void Eliminar(int idCategoria)
        {
            AccesoBD datos = new AccesoBD();
            try
            {
                datos.setearConsulta("DELETE FROM CATEGORIAS WHERE Id = @id");

                datos.setearParametro("@id", idCategoria);

                datos.ejecutarAccion();
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
