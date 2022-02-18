using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using System.ComponentModel;
using ALE_MexJet.ControlesUsuario;
using DevExpress.Web.Data;
using DevExpress.Web;
using ALE_MexJet.Clases;
using DevExpress.Utils;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;


namespace ALE_MexJet.Views.Consultas
{
    public partial class frmConsultaTarifas : System.Web.UI.Page,  IViewConsultaTarifa
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.ConsultaTarifas);
            LoadActions(DrPermisos);
            oPresenter = new ConsultaTarifa_Presenter(this, new DBConsultaTarifa());

             //ObtieneValores();
            if (!IsPostBack)
            {
                ObtieneDatosCliente();
            }
        }
   
        public void ObtieneValores()
        {
            if (eGetDetalleGastoInterno != null)
                eGetDetalleGastoInterno(null, EventArgs.Empty);
        }
              
        public void ObtieneDatosCliente()
        {
            if (eGetCliente != null)
                eGetCliente(null, EventArgs.Empty);
        }
        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlContrato.Items.Clear();
            if (eGetContrato != null)
                eGetContrato(null, EventArgs.Empty);
            
        }
        protected void ddlContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetDetalleGastoInterno != null)
                eGetDetalleGastoInterno(null, EventArgs.Empty);
            ObtieneValores();
        }
        public void LoadClientes(DataTable dtObject)
        {
            ddlCliente.Items.Clear();
            foreach (DataRow row in dtObject.Rows)
            {
                ddlCliente.Items.Add(row[1].S());
            }
        }

        public void LoadContrato(DataTable dtObjCat)
        {
            ddlContrato.Items.Clear();
            foreach (DataRow row in dtObjCat.Rows)
            {
                ddlContrato.Items.Add(row[2].S());

            }
            ViewState["Contrato"] = dtObjCat;
        }

        ConsultaTarifa_Presenter oPresenter;
        private const string sClase = "frmConsultaTarifas.aspx.cs";
        private const string sPagina = "frmConsultaTarifas.aspx";
        protected void Unnamed_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //GridView gv = new GridView();
            //gv.AutoGenerateColumns = true;

            //gv.DataSource = dtTarifas;
            //gv.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=TarifasClliente.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            this.EnableViewState = false;

            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            pnlExport.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
   
       
        public object[] oArrFiltroCliente
        {
            get { return new object[] { "@idCliente", 0 }; }
        }
               
        public object[] oArrFiltroContrato
        {
            get { string sNombreCliente = ddlCliente.SelectedItem.S(); return new object[] { "@NombreCliente", sNombreCliente }; }
        }

        public string oContrato
        {
            get { return ddlContrato.SelectedItem.S(); }
        }

        public string oNombreCliente
        {
            get { return ddlCliente.SelectedItem.S(); }
        }
        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                ddlCliente.Enabled = false;
                ddlContrato.Enabled = false;
                Button2.Enabled = false;
            }
            else
            {
                for (iPos = 0; iPos < DrActions[0].ItemArray.Length; iPos++)
                {
                    switch (iPos)
                    {
                        case 4: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                ddlCliente.Enabled = true;
                                ddlContrato.Enabled = true;
                                Button2.Enabled = true;
                            }
                            else
                            {
                                ddlCliente.Enabled = false;
                                ddlContrato.Enabled = false;
                                Button2.Enabled = false;
                            } break;
                    }
                }
            }

        }

        public event EventHandler eGetConcepto;
        public event EventHandler eGetDetalleGastoInterno;
        public event EventHandler eGetTipoMoneda;
        public event EventHandler eNewObj;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSearchObj; 
        public event EventHandler eGetCliente;
        public event EventHandler eGetContrato;
        UserIdentity oUsuario = new UserIdentity();
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }



        public void LoadTarifas(DataTable dtObjCat)
        {
            dtTarifas = dtObjCat;
            lblRespContratoId.Text = string.Empty;
            lblRespClienteId.Text = string.Empty;
            lblRespstatus.Text = string.Empty;
            lblRespTipodepaquete.Text = string.Empty;
            lblRespModeloContratado.Text = string.Empty;
            lblRespCostodirectonacional.Text = string.Empty;
            lblRespCostodirectointernacional.Text = string.Empty;
            lblRespCobrocombustible.Text = string.Empty;
            lblRespTipocobrocombustible.Text = string.Empty;
            lblRespPreciodecombustibleporgalon.Text = string.Empty;
            lblRespConsumogalonesporhora.Text = string.Empty;
            lblRespTarifadevuelonacional.Text = string.Empty;
            lblRespTarifadevuelointernacional.Text = string.Empty;
            lblRespTarifadeesperanacional.Text = string.Empty;
            lblRespTarifadeesperainternacional.Text = string.Empty;
            lblRespTarifadepernoctanacional.Text = string.Empty;
            lblRespTarifapernoctaternacional.Text = string.Empty;

            foreach (DataRow row in dtObjCat.Rows)
            {
                lblRespContratoId.Text = row[0].S();
                lblRespClienteId.Text = row[1].S();
                lblRespTipodepaquete.Text = row[2].S();
                lblRespstatus.Text = row[3].S();
                lblRespModeloContratado.Text = row[6].S();
                lblRespCostodirectonacional.Text = Convert.ToDecimal(row[7].S()).ToString("C");
                lblRespCostodirectointernacional.Text = Convert.ToDecimal(row[8].S()).ToString("C");
                lblRespCobrocombustible.Text = row[9].S();
                lblRespTipocobrocombustible.Text = row[11].S();
                lblRespPreciodecombustibleporgalon.Text = Convert.ToDecimal(row[10].S()).ToString("C");
                lblRespConsumogalonesporhora.Text = row[12].S();
                lblRespTarifadevuelonacional.Text = Convert.ToDecimal(row[13].S()).ToString("C");
                lblRespTarifadevuelointernacional.Text = Convert.ToDecimal(row[14].S()).ToString("C");
                lblRespTarifadeesperanacional.Text = Convert.ToDecimal(row[15].S()).ToString("C"); ;
                lblRespTarifadeesperainternacional.Text = Convert.ToDecimal(row[16].S()).ToString("C"); ;
                lblRespTarifadepernoctanacional.Text = Convert.ToDecimal(row[17].S()).ToString("C"); ;
                lblRespTarifapernoctaternacional.Text = Convert.ToDecimal(row[18].S()).ToString("C"); ;
            }
        }

        public object[] oArrFiltroTarifas
        {
            get {
                string sNombreCliente = ddlContrato.SelectedItem.S();
                return new object[] { "@ClaveContrato", sNombreCliente }; 
            }
        }

        public DataTable dtTarifas
        {
            get { return (DataTable)ViewState["VSTarifas"]; }
            set { ViewState["VSTarifas"] = value; }
        }
    }
}