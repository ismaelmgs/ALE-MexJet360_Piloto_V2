using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Reflection;
using ALE_MexJet.Objetos;
using DevExpress.Web;
using ALE_MexJet.ControlesUsuario;
using ALE_MexJet.Clases;
using System.ComponentModel;
using DevExpress.Web.Data;
using DevExpress.XtraPrinting;
using System.Text;
using System.IO;

namespace ALE_MexJet.Views.Reportes
{
    public partial class frmReporteTabulado : System.Web.UI.Page, IViewReporteTabular
    {
        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new ReporteTabular_Presenter(this,new DBReporteTabular());
                if(!IsPostBack)
                {
                    LoadObjects();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                pnlReporteTab.Visible = true;
                if (dtFechaDesde.Text.ToString() == "" || dtFechaHasta.Text.ToString() == "")
                    lblFechaRep.Text = "";
                else
                    lblFechaRep.Text = "Periodo: " + dtFechaDesde.Text + " - " + dtFechaHasta.Text;
                lblSeleccionMatricula.Text = cboMatricula.Text;
                lblSeleccionCliente.Text = cboCiente.Text;
                lblSeleccionContrato.Text = cboContrato.Text;
                lblSeleccionGrupoModelo.Text = cboGrupoModelo.Text;
                lblSeleccionBase.Text = cboBase.Text;

                if (eGeneraRep != null)
                    eGeneraRep(sender, e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteTabulado_" + DateTime.Now.ToShortDateString() + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            this.EnableViewState = false;

            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            pnlReporteTab.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void upaReport_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });

        }

        #endregion

        #region "METODOS"
       
        private void LoadObjects()
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, EventArgs.Empty);

                cboMatricula.DataSource = dtMatricula;
                cboMatricula.ValueField = "IdAeroave";
                cboMatricula.TextField = "Matricula";
                cboMatricula.DataBind();

                cboCiente.DataSource = dtCliente;
                cboCiente.ValueField = "IdCliente";
                cboCiente.TextField = "CodigoCliente";
                cboCiente.DataBind();

                cboContrato.DataSource = dtContrato;
                cboContrato.ValueField = "IdContrato";
                cboContrato.TextField = "ClaveContrato";
                cboContrato.DataBind();

                cboGrupoModelo.DataSource = dtGrupoModelo;
                cboGrupoModelo.ValueField = "GrupoModeloId";
                cboGrupoModelo.TextField = "Descripcion";
                cboGrupoModelo.DataBind();

                cboBase.DataSource = dtBase;
                cboBase.ValueField = "idAeropuert";
                cboBase.TextField = "AeropuertoIATA";
                cboBase.DataBind();

                cboMatricula.Value = "-2";
                cboCiente.Value = "-2";
                cboContrato.Value = "-2";
                cboGrupoModelo.Value = "-2";
                cboBase.Value = "-2";


            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void LoadGrid(DataTable dt)
        {
            try
            {
                DataSet ds = new DataSet();

                using (wsSyteline.Iws_SyteLineClient ws = new wsSyteline.Iws_SyteLineClient())
                {
                    ws.Open();
                    string xmlData = ws.GetFacturasTabulado(sTiposFactura, dtFechaDesde.Value.S().Dt(), dtFechaHasta.Value.S().Dt());
                    StringReader reader = new StringReader(xmlData);
                    ds.ReadXml(reader);
                    ws.Close();
                }

                DataTable dtFact = new DataTable();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 1)
                {
                    dtFact = ds.Tables[0].Copy();
                }

                if (dtFact.Rows.Count > 0)
                    dt.Merge(dtFact);

                gvTabulado.DataSource = dt;
                gvTabulado.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "Vars y Propiedades"
        ReporteTabular_Presenter oPresenter;
        private const string sPagina = "frmReporteTabulado.aspx";
        private const string sClase = "frmReporteTabulado.aspx.cs";

        public DataTable dtMatricula
        {
            get
            {
                return (DataTable)ViewState["dtMatricula"];
            }
            set
            {
                ViewState["dtMatricula"] = value;
            }
        }

        public DataTable dtContrato
        {
            get
            {
                return (DataTable)ViewState["dtContrato"];
            }
            set
            {
                ViewState["dtContrato"] = value;
            }
        }

        public DataTable dtGrupoModelo
        {
            get
            {
                return (DataTable)ViewState["dtGrupoModelo"];
            }
            set
            {
                ViewState["dtGrupoModelo"] = value;
            }
        }

        public DataTable dtCliente
        {
            get
            {
                return (DataTable)ViewState["dtCliente"];
            }
            set
            {
                ViewState["dtCliente"] = value;
            }
        }

        public DataTable dtBase
        {
            get
            {
                return (DataTable)ViewState["dtBase"];
            }
            set
            {
                ViewState["dtBase"] = value;
            }
        }

        public string sTiposFactura
        {
            get { return ViewState["VSTiposFactura"].S(); }
            set { ViewState["VSTiposFactura"] = value; }
        }

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGeneraRep;

        private string GetFechaConsulta(DateTime Fecha)
        {
            try
            {
                string sCad = Fecha.Year.S() + "-" + Fecha.Month.S().PadLeft(2, '0') + "-" + Fecha.Day.S().PadLeft(2, '0');
                return sCad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object[] oArrFiltros
        {
            get
            {

                return new object[]{
                                        "@IdAeronave", cboMatricula.SelectedItem.Value.S().I(),
                                        "@IdCliente", cboCiente.SelectedItem.Value.S().I(),
                                        "@IdContrato", cboContrato.SelectedItem.Value.S().I(),
                                        "@IdGrupoModelo", cboGrupoModelo.SelectedItem.Value.S().I(),
                                        "@IdAeropuerto", cboBase.SelectedItem.Value.S().I(),
                                        "@FechaIni", GetFechaConsulta(dtFechaDesde.Value.S().Dt()),
                                        "@fechaFin", GetFechaConsulta(dtFechaHasta.Value.S().Dt())
                                    };
            }

        }
        #endregion

        

    }
}