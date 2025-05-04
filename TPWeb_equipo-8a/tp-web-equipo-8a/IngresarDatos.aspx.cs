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

        public bool dni { get; set; }

        protected void Page_Load(object sender, EventArgs e)
		{
            dni = false;
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





        protected void btnBuscarDni_Click(object sender, EventArgs e)
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            Clientes cliente = clienteNegocio.ChequearDNI(Convert.ToInt32(EliminarPuntos(txtDNI.Text)));

            if (cliente != null)
            {
                txtDNI.Text = cliente.Documento;
                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtEmail.Text = cliente.Email;
                txtDireccion.Text = cliente.Direccion;
                txtCiudad.Text = cliente.Ciudad;
                txtCp.Text = cliente.CP.ToString();
            }

            dni = true;

        }













    }
}