using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_web_equipo_8a
{
	public partial class IngresarDatos : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void btnParticipar_Click(object sender, EventArgs e)
        {
            Clientes cliente = new Clientes();
            cliente.Documento = EliminarPuntos(txtDNI.Text);
            cliente.Nombre = txtNombre.Text;
            cliente.Apellido = txtApellido.Text;
            cliente.Email = txtEmail.Text;
            cliente.Direccion = txtDireccion.Text;
            cliente.Ciudad = txtCiudad.Text;
            cliente.CP = int.Parse(txtCp.Text);

            ClienteNegocio clienteNegocio = new ClienteNegocio();
            clienteNegocio.agregar(cliente);
        }


        private string EliminarPuntos(string dni)
        {
            return dni.Replace(".", "");
        }
    }
}