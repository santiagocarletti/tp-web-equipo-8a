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
    }
}
