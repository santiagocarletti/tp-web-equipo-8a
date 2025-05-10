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
                accesoBD.setearParametro("@codigoVoucher", voucher.CodigoVoucher);
                accesoBD.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
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
                    "WHERE CodigoVoucher = @CodigoVoucher"
                );
                datos.setearParametro("@CodigoVoucher", codigoVoucher);
                datos.ejecutarLectura();
                if (datos.Lectorbd.Read())
                {
                    voucher = new Vouchers();
                    voucher.CodigoVoucher = datos.Lectorbd["CodigoVoucher"].ToString();
                    voucher.IdCliente = datos.Lectorbd["IdCliente"] != DBNull.Value? Convert.ToInt32(datos.Lectorbd["IdCliente"]) : 0;  
                    voucher.FechaCanje = datos.Lectorbd["FechaCanje"] != DBNull.Value ? Convert.ToDateTime(datos.Lectorbd["FechaCanje"]): DateTime.MinValue; 
                    voucher.IdArticulo = datos.Lectorbd["IdArticulo"] != DBNull.Value ? Convert.ToInt32(datos.Lectorbd["IdArticulo"]): 0;
                }
                return voucher;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



    }


}
