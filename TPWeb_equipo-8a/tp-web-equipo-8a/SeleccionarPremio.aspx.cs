using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_web_equipo_8a
{
    public partial class SeleccionarPremio : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["VoucherActivo"] == null)
                {
                    Response.Redirect("IngresarVoucher.aspx", false);
                }
            }
        }
    }
}