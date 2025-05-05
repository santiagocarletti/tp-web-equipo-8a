using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ClienteNegocio
    {
        public int agregar(Clientes cliente)
        {
            AccesoBD datos = new AccesoBD();
            try
            {
                datos.setearConsulta("INSERT INTO CLIENTES (Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP) " +
                                     "VALUES (@dni, @nombre, @apellido, @email, @direccion, @ciudad, @cp)" +
                                     "SELECT SCOPE_IDENTITY();"
                                    );

                datos.setearParametro("@dni", cliente.Documento);
                datos.setearParametro("@nombre", cliente.Nombre);
                datos.setearParametro("@apellido", cliente.Apellido);
                datos.setearParametro("@email", cliente.Email);
                datos.setearParametro("@direccion", cliente.Direccion);
                datos.setearParametro("@ciudad", cliente.Ciudad);
                datos.setearParametro("@cp", cliente.CP);

                int idNuevoCliente = datos.ejecutarAccionconreturn();
                return idNuevoCliente;
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

        public Clientes ChequearDNI(int dni)
        {
            AccesoBD datos = new AccesoBD();
            try
            {
                datos.setearConsulta("SELECT * FROM CLIENTES WHERE Documento = @dni");
                datos.setearParametro("@dni", dni);
                datos.ejecutarLectura();

                if (datos.Lectorbd.Read())
                {
                    Clientes cliente = new Clientes();
                    cliente.Id = Convert.ToInt32(datos.Lectorbd["Id"]);
                    cliente.Documento = Convert.ToString(datos.Lectorbd["Documento"]);
                    cliente.Nombre = Convert.ToString(datos.Lectorbd["Nombre"]);
                    cliente.Apellido = Convert.ToString(datos.Lectorbd["Apellido"]);
                    cliente.Email = Convert.ToString(datos.Lectorbd["Email"]);
                    cliente.Direccion = Convert.ToString(datos.Lectorbd["Direccion"]);
                    cliente.Ciudad = datos.Lectorbd["Ciudad"].ToString();
                    cliente.CP = Convert.ToInt32(datos.Lectorbd["CP"]);
                    return cliente;
                }
                else
                {
                    return null;
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

    }
}
