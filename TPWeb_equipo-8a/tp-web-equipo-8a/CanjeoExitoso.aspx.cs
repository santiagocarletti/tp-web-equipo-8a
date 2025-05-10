using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

namespace tp_web_equipo_8a
{
    public partial class CanjeoExitoso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["VoucherActivo"] == null)
                {
                    Response.Redirect("IngresarVoucher.aspx", false);
                }
                else
                {
                    Vouchers voucher = (Vouchers)Session["VoucherActivo"];
                    lblMensaje.Text = "El canjeo del voucher " + voucher.CodigoVoucher + " fue exitoso.";
                    Session.Clear();
                }
            }
        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("IngresarVoucher.aspx", false);
        }




    }
}