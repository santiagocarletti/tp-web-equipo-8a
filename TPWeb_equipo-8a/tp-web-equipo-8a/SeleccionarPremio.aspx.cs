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
        protected void SeleccionarPremio_Click(object sender, EventArgs e)
        {
            
            Button btn = (Button)sender;
            string idArticulo = btn.CommandArgument;

          
            Response.Redirect("IngresarDatos.aspx?idArticulo=" + idArticulo);
        }
    }
}