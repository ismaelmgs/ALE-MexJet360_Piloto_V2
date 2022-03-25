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
using System.Collections.Specialized;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ALE_MexJet.Views.Consultas
{
    public partial class frmConsultaContrato : System.Web.UI.Page, iViewConsultaContrato
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.ConsultaContrato);
            LoadActions(DrPermisos);
            oPresenter = new ConsultaContratos_Presenter(this, new DBConsultaContrato());
            ObtieneValores();

            if (!IsPostBack)
            {
                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.ConsultaContrato), Convert.ToInt32(Enumeraciones.TipoAccion.Acceso), "Acceso al modulo " + Enumeraciones.Pantallas.ConsultaContrato.S());
            }

        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlContrato_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvConsultaContratos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

        }

        protected void gvConsultaContratos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

        }

        protected void gvConsultaContratos_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {

        }

        protected void gvConsultaContratos_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {

        }

        protected void gvConsultaContratos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Exportar), "Exportó los contratos de: " + Enumeraciones.Pantallas.Contrato.S());
            Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
			string sAccion = Convert.ToBase64String(Encoding.UTF8.GetBytes("Nuevo"));
			string ruta = "~/Views/CreditoCobranza/frmcontrato.aspx?Accion=" + sAccion;
            Response.Redirect(ruta);
        }

        protected void Unnamed_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });

        }

        protected void gvConsultaContratos_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            object value = (sender as DevExpress.Web.ASPxGridView).GetRowValues(e.VisibleIndex, "IdContrato");
            string sIdContrato;
            string sAccion = Convert.ToBase64String(Encoding.UTF8.GetBytes("Editar"));

            if (e.ButtonID == "btnConsulta")
            {
                sAccion = Convert.ToBase64String(Encoding.UTF8.GetBytes("Consultar"));
            }

			sIdContrato = Convert.ToBase64String(Encoding.UTF8.GetBytes(value.S()));
            string ruta = "~/Views/CreditoCobranza/frmcontrato.aspx?Contrato=" + sIdContrato + "&Accion=" + sAccion;
            ASPxWebControl.RedirectOnCallback(ruta);
        }

        protected void gvConsultaContratos_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            int iPos = 0;
            //DrPermisos = (DataRow[])Session["DrPermisos"];
            for (iPos = 0; iPos < DrPermisos[0].ItemArray.Length; iPos++)
            {
                switch (iPos)
                {
                    case 6: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                        {
                            e.Enabled = true;
                        }
                        else
                        {
                            e.Enabled = false;
                        } break;

                }
            }
        }

        protected void gvConsultaContratos_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Exportar")
                {
                    iIdContrato = e.CommandArgs.CommandArgument.S().I();
                    ViewReport(iIdContrato);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "METODOS"

        public void ObtieneValores()
        {
            if (eSearchObj != null)
                eSearchObj(null, EventArgs.Empty);
            
            gvConsultaContratos.DataSource = dtContratos;
            gvConsultaContratos.DataBind();

            ddlCliente.DataSource = dtClientes;
            ddlCliente.TextField = "CodigoCliente";
            ddlCliente.ValueField = "IdCliente";
            ddlCliente.DataBind();
        }
        public void LoadObjects(DataTable dtObject)
        {
            gvConsultaContratos.DataSource = null;
            ViewState["oDatos"] = null;

            gvConsultaContratos.DataSource = dtObject;
            ViewState["oDatos"] = dtObject;

            gvConsultaContratos.DataBind();

        }
        public void LoadKardex(DataSet ds)
        {
            try
            {
                dsKardexResume = null;
                dsKardexResume = ds;

                if(dsKardexResume != null)
                {
                    dsKardexResume.Tables[0].TableName = "ResumenKardex";
                    dsKardexResume.Tables[1].TableName = "EstatusFinal";
                    dsKardexResume.Tables[2].TableName = "ResumenDatosGenerales";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            popup.HeaderText = sCaption;
            gvConsultaContratos.JSProperties["cpText"] = sMensaje;
            gvConsultaContratos.JSProperties["cpShowPopup"] = true;
        }
        protected void CancelEditing(CancelEventArgs e)
        {
            e.Cancel = true;
            gvConsultaContratos.CancelEdit();
        }

        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        public void ViewReport(int iContrato)
        {
            try
            {
                iIdContrato = iContrato;

                if (eSearchKardex != null)
                    eSearchKardex(null, null);

                DataSet dsK = new DataSet();
                dsK = dsKardexResume;

                string strPath = string.Empty;
                using (ReportDocument rd = new ReportDocument())
                {
                    strPath = Server.MapPath("CristalReport\\ResumenKardex.rpt");
                    rd.Load(strPath, OpenReportMethod.OpenReportByDefault);

                    rd.SetDataSource(dsKardexResume.Tables[2]);
                    //rd.SetDataSource(dsKardexResume);
                    rd.Subreports["ReporteHorasRemision.rpt"].SetDataSource(dsKardexResume);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "KardexContrato");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "Vars y Propiedades"
        ConsultaContratos_Presenter oPresenter;
        private const string sClase = "frmConsultaContratos.aspx.cs";
        private const string sPagina = "frmConsultaContratos.aspx";
        public object[] oArrFiltros
        {
            get 
            {
                int iIdCliente = -1;
                int iIdContrato = -1;

                if(ddlCliente.SelectedIndex > 0)
                {
                    iIdCliente = ddlCliente.SelectedItem.Value.I();
                }
                if(ddlContrato.SelectedIndex > 0)
                {
                    iIdContrato = ddlContrato.SelectedItem.I();
                }

                return new object[]{
                                        "@idCliente", iIdCliente,
                                        "@IdContrato", iIdContrato
                                    };
            }
        }

        public int iIdCliente
        {
            get { throw new NotImplementedException(); }
        }

        public int iIdContrato
        {
            get { return (int)ViewState["ViIdContrato"]; }
            set { ViewState["ViIdContrato"] = value; }
        }

        public DataTable dtClientes
        {
            get
            {
                return (DataTable)ViewState["dtClientes"];
            }
            set
            {
                ViewState["dtClientes"] = value;
            }
        }

        public DataTable dtContratosCliente
        {
            get
            {
                return (DataTable)ViewState["dtContratosCliente"];
            }
            set
            {
                ViewState["dtContratosCliente"] = value;
            }
        }

        public DataTable dtContratos
        {
            get
            {
                return (DataTable)ViewState["dtContratos"];
            }
            set
            {
                ViewState["dtContratos"] = value;
            }
        }
        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                ddlCliente.Enabled = false;
                ddlContrato.Enabled = false;
                btnNuevo.Enabled = false;
                btnExcel.Enabled = false;
                btnNuevo2.Enabled = false;
                btnExportar.Enabled = false;
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
                                btnExcel.Enabled = true;
                                btnExportar.Enabled = true;
                            }
                            else
                            {
                                ddlCliente.Enabled = false;
                                ddlContrato.Enabled = false;
                                btnExcel.Enabled = false;
                                btnExportar.Enabled = false;
                            } break;
                        case 5: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                btnNuevo.Enabled = true;
                                btnNuevo2.Enabled = true;
                            }
                            else
                            {
                                btnNuevo.Enabled = false;
                                btnNuevo2.Enabled = false;
                            } break;
                    }
                }
            }

        }

        public event EventHandler eNewObj;

        public event EventHandler eObjSelected;

        public event EventHandler eSaveObj;

        public event EventHandler eDeleteObj;

        public event EventHandler eSearchObj;
        
        public event EventHandler eSearchContratosCliente;
        public event EventHandler eSearchKardex;
        UserIdentity oUsuario = new UserIdentity();
        public DataRow[] DrPermisos
        {
            get { 
                    return (DataRow[])Session["DrPermisos"];
                }
            set { Session["DrPermisos"] = value; }
        }

        public DataSet dsKardexResume
        {
            get
            {
                return (DataSet)ViewState["dsKardexResume"];
            }
            set
            {
                ViewState["dsKardexResume"] = value;
            }
        }
        #endregion

        protected void gvConsultaContratos_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName == "Reportes") 
            {
                ASPxButton btnPDF = gvConsultaContratos.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btnExportarPDF") as ASPxButton;
                if (Convert.ToString(e.GetValue("Paquete")).Equals("JETCARD DIRECT") || Convert.ToString(e.GetValue("Paquete")).Equals("JETCARD EFFICIENT"))
                    btnPDF.Visible = true;
                else 
                    btnPDF.Visible = false;
            }
        }
    }
}