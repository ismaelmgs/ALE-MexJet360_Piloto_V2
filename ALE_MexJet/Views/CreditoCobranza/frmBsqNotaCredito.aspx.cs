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

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmBsqNotaCredito : System.Web.UI.Page, IViewNotaCredito
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.AltaNotasCredito);
                LoadActions(DrPermisos);
                oPresenter = new NotaCredito_Presenter(this, new DBNotaCredito());
                gvNotaCredito.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                DataTable dt = (DataTable)ViewState["dtObject"];
                if (dt != null && dt.Rows.Count > 0)
                    CargaGrid(dt);

                if (!IsPostBack)
                {
                    ConsultaDatos(sender, e);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            try
            { 
                ConsultaDatos(sender, e);
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso"); }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try 
            { 
                Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExcel_Click", "Aviso"); }
        }

        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            try 
            {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdatePanel1_Unload", "Aviso"); }
        } 
        #endregion

        #region METODOS

        private void ConsultaDatos(object sender, EventArgs e)
        {
            try
            {
                if (eSearchConsulta != null)
                    eSearchConsulta(sender, e);
            }
            catch (Exception x) { throw x; }
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                //mpeMensaje.ShowMessage(sMensaje, sCaption);
                gvNotaCredito.JSProperties["cpShowPopup"] = true;
                gvNotaCredito.JSProperties["cpText"] = sMensaje.S();
                lbl.Text = sMensaje.S();
                popup.ShowOnPageLoad = true;
            }
            catch (Exception x) { throw x; }
        }

        public void LoadObjects(DataTable dtObject)
        {
            try
            {
                ViewState["dtObject"] = dtObject;
                CargaGrid(dtObject);
            }
            catch (Exception x) { throw x; }
        }

        public void CargaGrid(DataTable DT)
        {
            try
            {
                gvNotaCredito.DataSource = DT;
                gvNotaCredito.DataBind();
            }
            catch (Exception x) { throw x; }
        }
        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                ddlTipoBusqueda.Enabled = false;
                btnBusqueda.Enabled = false;
                textTextoBusqueda.Enabled = false;
                deInicio.Enabled = false;
                deFin.Enabled = false;
                btnExcel.Enabled = false;
            }
            else
            {
                for (iPos = 0; iPos < DrActions[0].ItemArray.Length; iPos++)
                {
                    switch (iPos)
                    {
                        case 4: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                ddlTipoBusqueda.Enabled = true;
                                btnBusqueda.Enabled = true;
                                textTextoBusqueda.Enabled = true;
                                deInicio.Enabled = true;
                                deFin.Enabled = true;
                                btnExcel.Enabled = true;

                            }
                            else
                            {
                                ddlTipoBusqueda.Enabled = false;
                                btnBusqueda.Enabled = false;
                                textTextoBusqueda.Enabled = false;
                                deInicio.Enabled = false;
                                deFin.Enabled = false;
                                btnExcel.Enabled = false;

                            } break;
                    }
                }
            }

        }
        #endregion

        #region "Vars y Propiedades"

        private const string sClase = "frmBsqNotaCredito.aspx.cs";
        private const string sPagina = "frmBsqNotaCredito.aspx";

        NotaCredito_Presenter oPresenter;

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchConsulta;
        UserIdentity oUsuario = new UserIdentity();
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }
        public object[] oArrFiltros
        {
            get
            {
                return new object[]
                { 
                    "@IdRemision", string.Empty
                };
            }
        }

        public object oCrud
        {
            get { return ViewState["CrudPiloto"]; }
            set { ViewState["CrudPiloto"] = value; }
        }

        public ALE_MexJet.Objetos.Enumeraciones.TipoOperacion eCrud
        {
            get { return (ALE_MexJet.Objetos.Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }

        public NotaCredito oCatalogo
        {
            get
            {
                NotaCredito NT = new NotaCredito();
                return NT;
            }
        }

        public NotaCredito oBusqueda
        {
            get
            {
                NotaCredito NB = new NotaCredito();
                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "0":
                        NB.Opcion = "2";
                        break;
                    case "1":
                        NB.FolioNotaCredito = textTextoBusqueda.Text.S().I();
                        NB.iTipoNotaCredito = null;
                        break;
                    case "2":
                        NB.iTipoNotaCredito = textTextoBusqueda.Text.S();
                        break;
                    case "3":
                        NB.CodigoCliente = textTextoBusqueda.Text.S();
                        NB.iTipoNotaCredito = null;
                        break;
                    case "4":
                        NB.Clavecontrato = textTextoBusqueda.Text.S();
                        NB.iTipoNotaCredito = null;
                        break;
                    case "5":
                        NB.iIdFolioRemision = textTextoBusqueda.Text.S().I();
                        NB.iTipoNotaCredito = null;
                        break;
                }

                if (deInicio.Text.S() != string.Empty && deFin.Text.S() != string.Empty)
                { NB.FechaInicio = deInicio.Text.S(); NB.FechaFin = deFin.Text.S(); NB.Opcion = "1"; }
                

                return NB;
            }
        }
        #endregion
        protected void CancelEditing(CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                gvNotaCredito.CancelEdit();
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "CancelEditing", "Aviso"); }
        }
        protected void gvNotaCredito_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;

                if (eDeleteObj != null)
                    eDeleteObj(sender, e);

                CancelEditing(e);
            }
            catch (Exception x) { throw x; }
        }

        protected void gvNotaCredito_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }

    }
}