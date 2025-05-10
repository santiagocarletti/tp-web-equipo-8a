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
            if (!IsPostBack)
            {
                if (Session["VoucherActivo"] == null)
                {
                    Response.Redirect("IngresarVoucher.aspx", false);
                }
            }
            
            
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

            if (Session["VoucherActivo"] == null || Request.QueryString["idArticulo"] == null)
            {
                Session.Add("error", "Hubo un Error, intente nuevamente");
                Response.Redirect("Error.aspx", false);
                Session.Clear();
                return;
            }

            Vouchers voucher = new Vouchers();
            voucher = (Vouchers)Session["VoucherActivo"];
            voucher.IdCliente = cliente.Id;
            voucher.FechaCanje = DateTime.Now;
            voucher.IdArticulo = int.Parse(Request.QueryString["idArticulo"].ToString());

            VoucherNegocio voucherNegocio = new VoucherNegocio();
            try
            {
                voucherNegocio.canjear(voucher);
                Response.Redirect("CanjeoExitoso.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
                throw;
            }           
        }

        protected void btnBuscarDni_Click(object sender, EventArgs e)
        {
            if (txtDNI.Enabled == false)
            {
                Response.Redirect("IngresarDatos.aspx", false);
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