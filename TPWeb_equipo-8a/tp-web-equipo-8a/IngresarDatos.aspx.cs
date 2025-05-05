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
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            Clientes cliente = clienteNegocio.ChequearDNI(Convert.ToInt32(EliminarPuntos(txtDNI.Text)));

            if (cliente == null)
            {
                cliente = new Clientes();
                cliente.Documento = EliminarPuntos(txtDNI.Text);
                cliente.Nombre = txtNombre.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.Email = txtEmail.Text;
                cliente.Direccion = txtDireccion.Text;
                cliente.Ciudad = txtCiudad.Text;
                cliente.CP = int.Parse(txtCp.Text);

                //si es nuevo lo agregamos a la BD
                clienteNegocio = new ClienteNegocio();
                cliente.Id = clienteNegocio.agregar(cliente);
            }

            if (Session["voucherId"] == null || Request.QueryString["idArticulo"] == null)
            {
                Session.Add("error", "Error, intente nuevamente");
                Response.Redirect("Error.aspx");
                return;
            }

            Vouchers voucher = new Vouchers();
            voucher.CodigoVoucher = Session["voucherId"].ToString();
            voucher.IdCliente = cliente.Id;
            voucher.FechaCanje = DateTime.Now;
            voucher.IdArticulo = int.Parse(Request.QueryString["idArticulo"].ToString());

            VoucherNegocio voucherNegocio = new VoucherNegocio();
            voucherNegocio.canjear(voucher);

        }

        protected void btnBuscarDni_Click(object sender, EventArgs e)
        {
            if (txtDNI.Enabled == false)
            {
                Response.Redirect("IngresarDatos.aspx");
                return;
            }

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

                txtNombre.Enabled = false;
                txtApellido.Enabled = false;
                txtEmail.Enabled = false;
                txtDireccion.Enabled = false;
                txtCiudad.Enabled = false;
                txtCp.Enabled = false;
                dni = true;
            }

            btnBuscarDNI.Text = "Cambiar";
            dni = true;
            txtDNI.Enabled = false;
        }

        private string EliminarPuntos(string dni)
        {
            return dni.Replace(".", "");
        }


    }
}