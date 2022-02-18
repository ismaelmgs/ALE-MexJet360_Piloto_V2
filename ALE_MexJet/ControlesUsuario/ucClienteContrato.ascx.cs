using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALE_MexJet.ControlesUsuario
{
    public partial class ucClienteContrato : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }              

        protected void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (miEventoCliente != null)
                miEventoCliente(sender, e); 
        }

        protected void cmbContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (miEventoContrato != null)
                miEventoContrato(sender, e); 
        }

        public void llenarComboCliente(DataTable dtObjCat)
        {
            cmbCliente.DataSource = dtObjCat;
            cmbCliente.ValueField = "idCliente";
            cmbCliente.ValueType = typeof(Int32);
            cmbCliente.TextField = "CodigoCliente";
            cmbCliente.DataBind();
        }

        public void llenarComboContrato(DataTable dtObjCat)
        {
            cmbContrato.DataSource = dtObjCat;
            cmbContrato.ValueField = "idContrato";
            cmbContrato.ValueType = typeof(Int32);
            cmbContrato.TextField = "ClaveContrato";
            cmbContrato.DataBind();
        }

        public void LimpiarComboCliente()
        {
            cmbCliente.SelectedIndex = -1;
            cmbCliente.SelectedItem = null;
        }

        public void LimpiarComboContrato()
        {
            cmbContrato.SelectedIndex = -1;
            cmbContrato.SelectedItem = null;
        }

        public event EventHandler miEventoCliente;
        public event EventHandler miEventoContrato;
    }
}