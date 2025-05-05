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

            
            VoucherNegocio negocioVoucher = new VoucherNegocio();
            Vouchers voucher = negocioVoucher.ObtenerPorCodigo(codigoVoucher);

            
            if (voucher == null || voucher.IdCliente > 0)
            {
                Response.Redirect("VoucherError.aspx");
                return;
            }

          
            voucher.FechaCanje = DateTime.Now;

            
            Session["VoucherActivo"] = voucher;
            Session["voucherId"] = voucher.CodigoVoucher;

           
            Response.Redirect("SeleccionarPremio.aspx");
        }
    }
}
