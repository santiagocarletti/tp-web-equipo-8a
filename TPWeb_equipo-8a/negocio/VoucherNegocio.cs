using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class VoucherNegocio
    {
        public void canjear(Vouchers voucher)
        {
            AccesoBD accesoBD = new AccesoBD();

            try
            {
                string consulta = "UPDATE VOUCHERS SET " +
                                  "IdCliente = @idCliente, " +
                                  "FechaCanje = @fecha, " +
                                  "IdArticulo = @idArticulo " +
                                  "WHERE CodigoVoucher = @codigoVoucher";

                accesoBD.setearConsulta(consulta);
                accesoBD.setearParametro("@idCliente", voucher.IdCliente);
                accesoBD.setearParametro("@fecha", voucher.FechaCanje);
                accesoBD.setearParametro("@IdArticulo", voucher.IdArticulo);

                accesoBD.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                accesoBD.cerrarConexion();
            }
        }

        public Vouchers ObtenerPorCodigo(string codigoVoucher)
        {
            AccesoBD datos = new AccesoBD();
            Vouchers voucher = null;
            try
            {
                datos.setearConsulta(
                    "SELECT CodigoVoucher, IdCliente, FechaCanje, IdArticulo " +
                    "FROM Vouchers " +
                    "WHERE CodigoVoucher = @codigoVoucher"
                );
                datos.setearParametro("@codigoVoucher", codigoVoucher);
                datos.ejecutarLectura();
                if (datos.Lectorbd.Read())
                {
                    voucher = new Vouchers();
                    voucher.CodigoVoucher = Convert.ToString(datos.Lectorbd["CodigoVoucher"]);
                    voucher.IdCliente = Convert.ToInt32(datos.Lectorbd["IdCliente"]);
                    voucher.FechaCanje = Convert.ToDateTime(datos.Lectorbd["FechaCanje"]);
                    voucher.IdArticulo = Convert.ToInt32(datos.Lectorbd["IdArticulo"]);
                }
                return voucher;
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
