using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Views
{
    public partial class frmCorreo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                lblRespuesta.Text = "";
        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                Utils.EnvioCorreo(txtHost.Text, Convert.ToInt32(txtPuerto.Text.ToString()), txtAsunto.Text, txtFrom.Text, mmMensaje.Text, string.Empty, txtUsuario.Text, txtPass.Text, txtCC.Text, txtNombreRemisor.Text);
                lblRespuesta.Text = "El correo se envio";
            }
            catch (Exception x) { lblRespuesta.Text = x.Message; }
        }
    }
}