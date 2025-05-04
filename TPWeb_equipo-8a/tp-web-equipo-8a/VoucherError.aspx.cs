using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_web_equipo_8a
{
    public partial class VoucherError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMensajeError.Text = "El código ingresado no es válido o ya ha sido utilizado.";
            }

        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("IngresarVoucher.aspx");
        }

    }
}