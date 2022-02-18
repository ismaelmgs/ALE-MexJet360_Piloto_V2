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
using System.Text;
using DevExpress.XtraPrinting;

using System.Collections.Specialized;

using System.IO;

namespace ALE_MexJet.Views.FBO
{
    public partial class frmMonitorTrafico : System.Web.UI.Page, IViewMonitorTrafico
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new MonitorTrafico_Presenter(this, new DBMonitorTrafico());

                gvMonitorTrafico.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvMonitorTrafico.SettingsPager.ShowDisabledButtons = true;
                gvMonitorTrafico.SettingsPager.ShowNumericButtons = true;
                gvMonitorTrafico.SettingsPager.ShowSeparators = true;
                gvMonitorTrafico.SettingsPager.Summary.Visible = true;
                gvMonitorTrafico.SettingsPager.PageSizeItemSettings.Visible = true;
                gvMonitorTrafico.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvMonitorTrafico.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                if (!IsPostBack)
                {
                    if (eBuscaAropuertoBase != null)
                        eBuscaAropuertoBase(sender, EventArgs.Empty);

                    if (eSearchObj != null)
                        eSearchObj(sender, EventArgs.Empty);
                }

                RecargaGrid();              

                if (ppPasajeros.ShowOnPageLoad == true)
                {
                    if (eBuscaPax != null)
                        eBuscaPax(sender, EventArgs.Empty);
                }
                if (ppTrip.ShowOnPageLoad == true)
                    ObtieneTrips();    
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }

        protected void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {                                
                if (eSearchObj != null)
                    eSearchObj(sender, EventArgs.Empty);                
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdateTimer_Tick", "Aviso");
            }
        }

        protected void Unnamed_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }            
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Unnamed_Unload", "Aviso");
            }
        }
        protected void gvTrip_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;

                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    //gvTrip.Columns["Status"].Visible = false;

                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTrip_CellEditorInitialize", "Aviso");
            }
        }
        protected void gvTrip_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;

                if (eEliminaTripSolicitud != null)
                    eEliminaTripSolicitud(sender, e);

                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTrip_RowDeleting", "Aviso");
            }
        }
        protected void gvTrip_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;

                oCrud = e;

                if (eNewObj != null)
                    eNewObj(sender, e);

                e.NewValues["IdSolicitud"] = iIdSolicitud;
                e.NewValues["UserIdentity"] = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();

                if (eNewTrip != null)
                    eNewTrip(sender, e);               

                CancelEditing(e);
                //CargaMotivo();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTrip_RowInserting", "Aviso");
            }
        }
        protected void gvTrip_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;

                e.NewValues["UserIdentity"] = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();

                if (eSaveTrip != null)
                    eSaveTrip(sender, e);

                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTrip_RowUpdating", "Aviso");
            }
        }
        protected void gvTrip_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvTrip.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTrip_StartRowEditing", "Aviso");
            }
        }
        protected void gvTrip_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                string x = e.NewValues["Trip"].S();
                if (eValidaTrip != null)
                    eValidaTrip(x, e);

                if (Session["Validacion"] != null && Session["Validacion"].S().I() > 0)
                {
                    AddError(e.Errors, gvTrip.Columns["TRIP"], "Este Trip ya existe, favor de validarlo.");
                    Session["Validacion"] = null;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTrip_RowValidating", "Aviso");
            }
        }
        protected void CancelEditing(CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                gvTrip.CancelEdit();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "CancelEditing", "Aviso");
            }
        }
        protected void gvMonitorTrafico_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Trip")
                {
                    iIdSolicitud = e.CommandArgs.CommandArgument.S().I();
                    if (eLoadObjTrips != null)
                        eLoadObjTrips(null,null);
                    ppTrip.ShowOnPageLoad = true;
                }
                else if (e.CommandArgs.CommandName.S() == "Notas")
                {
                    iIdSolicitud = e.CommandArgs.CommandArgument.S().I();
                    if (eBuscaNotas != null)
                        eBuscaNotas(sender, EventArgs.Empty);
                    ppNotas.ShowOnPageLoad = true;
                }
                else if (e.CommandArgs.CommandName.S() == "Confirmacion")
                {
                    int iAux = 0;
                    DataTable dt = (DataTable)gvMonitorTrafico.DataSource;
                    iIdSolicitud = e.CommandArgs.CommandArgument.S().I();
                    
                    if (dt.Rows.Count > 0)
                    {
                        DataRow[] rows = dt.Select("IdSolicitud = " + iIdSolicitud.S());
                        if (rows.Length > 0)
                        {
                            if (rows[0]["EstatusSolicitud"].S() != "CANCELADO")
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    if (iIdSolicitud == Convert.ToInt32(row[0].S()) && row["ViabilidadDespacho"].S().I() == 2)
                                    {
                                        iAux = 2; // indicamos que no tiene viabilidad
                                        break;
                                    }
                                    else if (iIdSolicitud == Convert.ToInt32(row[0].S()) && row["TRIP"].S().Length == 0)
                                    {
                                        iAux = 3; // utilizamos la misma variable y le asigamos 3 para indicar que no cuenta con trip
                                        break;
                                    }
                                }

                                if (iAux == 2)
                                {
                                    lbl.Text = "No cuenta con viabilidad de vuelo.";
                                    popup.ShowOnPageLoad = true;
                                    return;
                                }
                                else if (iAux == 3)
                                {
                                    lbl.Text = "No cuenta con Trip, favor de asignar un  numero de trip.";
                                    popup.ShowOnPageLoad = true;
                                    return;
                                }
                            }
                        }
                    }

                    ppConfirmacion.ShowOnPageLoad = true;
                }                 
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvMonitorTrafico_RowCommand", "Aviso");
            }
        }

        protected void gvPiernas_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try 
            {
                if (e.CommandArgs.CommandName.S() == "Pasajeros")
                {                   
                    iIdTramo = e.CommandArgs.CommandArgument.S().I();

                    if (eBuscaPax != null)
                        eBuscaPax(sender, EventArgs.Empty);

                    ppPasajeros.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPiernas_RowCommand", "Aviso");
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                gvTrip.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvTrip.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
            }
        }
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eGuardaMonitorTrafico != null)
                    eGuardaMonitorTrafico(sender, EventArgs.Empty);

                if (eSearchObj != null)
                    eSearchObj(sender, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnConfirmar_Click", "Aviso");
            }
        }

        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdatePanel1_Unload", "Aviso");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(sender, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscar_Click", "Aviso");
            }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExcel_Click", "Aviso");
            }
        }
        protected void gvPiernas_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView gridDetalle = sender as ASPxGridView;

                iIdSolicitud = gridDetalle.GetMasterRowKeyValue().S().I();

                if (eSearchDetalle != null)
                    eSearchDetalle(sender, EventArgs.Empty);

                gridDetalle.DataSource = (DataTable)ViewState["CargaDetalle"];
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPiernas_BeforePerformDataSelect", "Aviso");
            }
        }
        #endregion 

        #region METODOS
        public void MostrarMensaje(string sMensaje, string sCaption)        
        {
            try
            {
                mpeMensaje.ShowMessage(sMensaje, sCaption);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaGrid(DataTable dtObjCat)
        {
            try
            {
                gvMonitorTrafico.DataSource = dtObjCat;
                gvMonitorTrafico.DataBind();
                ViewState["gvMonitorTrafico"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RecargaGrid()
        {
            try 
            {
                if (ViewState["gvMonitorTrafico"] != null)
                {
                    gvMonitorTrafico.DataSource = (DataTable)ViewState["gvMonitorTrafico"];
                    gvMonitorTrafico.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            try
            {
                if (errors.ContainsKey(column)) return;
                errors[column] = errorText;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void ObtieneTrips()
        {
            try
            {
                if (eLoadObjTrips != null)
                    eLoadObjTrips(null, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadObjectsTrips(DataTable dtObjCat)
        {
            try
            {
                gvTrip.DataSource = dtObjCat;
                gvTrip.DataBind();
                ViewState["gvTrip"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Validacion(int i)
        {
            try
            {
                ViewState["Validacion"] = i;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadDetalle(DataTable dtDetalle)
        {
            try
            {
                ViewState["CargaDetalle"] = dtDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargaAeropuertoBase(DataTable dtAeropuertoBase)
        {
            try
            {
                ddlBase.DataSource = dtAeropuertoBase;
                ddlBase.TextField = "AeropuertoICAO";
                ddlBase.ValueField = "idAeropuert";
                ddlBase.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargaPax(DataTable dtPax)
        {
            try
            {
                gvPasajeros.DataSource = null;
                gvPasajeros.DataSource = dtPax;
                gvPasajeros.DataBind();
                dtPax = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargaNotas(DataSet dsNotas)
        {
            try
            {
                DataTable dtNota = new DataTable();
                DataTable dtPreferencia = new DataTable();

                dtNota = dsNotas.Tables[0];
                dtPreferencia = dsNotas.Tables[1];

                mNotas.Text = dtNota.Rows[0]["Descripcion"].ToString() + " " + dtNota.Rows[0]["Notas"].ToString() + "\n\n"
                + dtNota.Rows[1]["Descripcion"].ToString() + " " + dtNota.Rows[1]["Notas"].ToString() + "\n\n"
                + dtNota.Rows[2]["Descripcion"].ToString() + " " + dtNota.Rows[2]["Notas"].ToString();
                
                //mNotas.Font.Bold = true;
                mNotas.ForeColor = System.Drawing.Color.Black;

                mPreferencias.Text = dtPreferencia.Rows[0]["Observaciones"].ToString();
                mPreferencias.ForeColor = System.Drawing.Color.Black;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  VARIABLES
        private const string sClase = "MonitorTrafico.aspx.cs";
        private const string sPagina = "MonitorTrafico.aspx";

        MonitorTrafico_Presenter oPresenter;
        public int iIdSolicitud
        {
            get { return ViewState["idSolicitud"].S().I(); }
            set { ViewState["idSolicitud"] = value; }
        }

        public int iIdTramo
        {
            get { return ViewState["idTramo"].S().I(); }
            set { ViewState["idTramo"] = value; }
        }

        public object oCrud
        {
            get { return ViewState["CrudTrip"]; }
            set { ViewState["CrudTrip"] = value; }
        }

        public object[] oArrDetalle
        {
            get {
                    return new object[] { 
                    "@IdSolicitud",iIdSolicitud
                };
            }
        }

        public object[] oArrMonitorTrafico
        {
            get {

                DataTable dt = (DataTable)gvMonitorTrafico.DataSource;

                string sOrigenSol = "", sUsuarioCreacion = "";
                DateTime dtFechaVuelo = System.DateTime.Now;
                
                int iIdMonitorTrafico = 0;
                foreach (DataRow row in dt.Rows)
                {                                       
                    sOrigenSol = row[6].S();                                       
                    sUsuarioCreacion = row[9].S();
                    iIdMonitorTrafico = row[18].S().I();
                    if (iIdSolicitud == Convert.ToInt32(row[0].S()))
                        break;                     
                }
              
                return new object[]{
                    "@IdMonitorTrafico",iIdMonitorTrafico,
                    "@IdSolicitud",iIdSolicitud,                                        		                    		
                    "@UsuarioModificacion",	((UserIdentity)Session["UserIdentity"]).sUsuario.S(),                 
                };
            }
        }

        public object[] oArrConsultaMonitorTrafico
        {
            get {

                int iIdBase;

                if (ddlBase.SelectedIndex == -1)
                {
                    iIdBase = -1;
                }
                else
                {
                    iIdBase = Convert.ToInt32(ddlBase.SelectedItem.Value);
                }

                return new object[] { 
                    "@IdBase",iIdBase
                };
            }
        }
        public object[] oArrConsultaPax
        {
            get
            {
                return new object[] { 
                    "@IdTramo",iIdTramo
                };
            }
        }

        public ALE_MexJet.Objetos.Enumeraciones.TipoOperacion eCrud
        {
            get { return (ALE_MexJet.Objetos.Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }
        public object[] oArrConsultaNotas
        {
            get
            {
                return new object[] { 
                    "@IdSolicitud",iIdSolicitud
                };
            }
        }
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eNewTrip;
        public event EventHandler eSaveTrip;
        public event EventHandler eLoadObjTrips;
        public event EventHandler eEliminaTripSolicitud;
        public event EventHandler eValidaTrip;
        public event EventHandler eGuardaMonitorTrafico;
        public event EventHandler eSearchDetalle;
        public event EventHandler eBuscaAropuertoBase;
        public event EventHandler eBuscaPax;
        public event EventHandler eBuscaNotas;

        

        #endregion               

        
        
    }
}