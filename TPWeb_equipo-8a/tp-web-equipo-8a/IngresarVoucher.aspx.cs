using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace tp_web_equipo_8a
{
    public partial class IngresarVoucher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
          
            string codigoVoucher = voucherInput.Text.Trim();
            if (string.IsNullOrEmpty(codigoVoucher))
            {
                lblMessage.Text = "Por favor, ingrese un código de voucher.";
                return;
            }

            try
            {
                VoucherNegocio negocioVoucher = new VoucherNegocio();
                Vouchers voucher = negocioVoucher.ObtenerPorCodigo(codigoVoucher);

                if (voucher == null)
                {
                    Session.Add("error", "El Voucher Ingresado es inválido");
                    Response.Redirect("Error.aspx", false);
                    return;
                }

                if (voucher.IdCliente > 0)
                {
                    Session.Add("error", "El Voucher Ingresado ya ha sido canjeado por otro usuario el dia " + voucher.FechaCanje.ToShortDateString());
                    Response.Redirect("Error.aspx", false);
                    return;
                }

                voucher.FechaCanje = DateTime.Now;
                Session["VoucherActivo"] = voucher;
                Response.Redirect("SeleccionarPremio.aspx", false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
                throw;
            }
        }
    }
}
