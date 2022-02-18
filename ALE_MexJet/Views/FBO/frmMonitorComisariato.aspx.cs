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


using System.Collections.Specialized;

using System.IO;


namespace ALE_MexJet.Views.FBO
{
    public partial class MonitorComisariato : System.Web.UI.Page, IViewMonitorComisariato
    {
        #region EVENTOS 
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.MonitorComisariato);
                LoadActions(DrPermisos);
                oPresenter = new MonitorComisariato_Presenter(this, new DBMonitorComisariato());
                gvComisariato.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvComisariato.SettingsPager.ShowDisabledButtons = true;
                gvComisariato.SettingsPager.ShowNumericButtons = true;
                gvComisariato.SettingsPager.ShowSeparators = true;
                gvComisariato.SettingsPager.Summary.Visible = true;
                gvComisariato.SettingsPager.PageSizeItemSettings.Visible = true;
                gvComisariato.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvComisariato.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                //CargaGrid();
                //    if(!IsPostBack)
                ObtieneValores();

            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso"); }

        }
        protected void Unnamed_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                ObtieneValores();
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
        protected void gvComisariato_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try {

                LimpiaControles();


            e.Editor.ReadOnly = false;

            if (e.Column.FieldName == "Estaus")
            {
                if (eLoadStaus != null)
                    eLoadStaus(this, e);
                ASPxComboBox comboBox = e.Editor as ASPxComboBox;
                comboBox.DataSource = (DataTable)ViewState["dtObjSatus"];
                comboBox.ValueField = "IdEstatus";
                comboBox.ValueType = typeof(Int32);
                comboBox.TextField = "Descripcion";
                comboBox.DataBindItems();
            }

            if (e.Column.FieldName == "FechaVuelo")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "HoraVuelo")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "Trip")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "CodigoCliente")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "Matricula")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "Origen")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "ComisariatoDesc")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "Preferencias")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "Proveedor")
            { e.Editor.ReadOnly = true; }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvComisariato_CellEditorInitialize", "Aviso"); }

        }
        protected void gvComisariato_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            
        }
        protected void gvComisariato_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try { 
            eCrud = Enumeraciones.TipoOperacion.Actualizar;
            oCrud = e;

            GetComisariatoID(e.Keys[0].S().I());
            if (eActualizaStataus != null)
                eActualizaStataus(sender, e);

            LimpiaControles();

            if (eConsultaComisariatoDetalle != null)
                eConsultaComisariatoDetalle(null, null);


            if (e.NewValues["Estaus"].ToString() == "2")
            {
                ppRecibido.HeaderText = "Recibido";
                gvComisariato.JSProperties["cpShowPopup"] = true;
                gvComisariato.JSProperties["cpTipo"] = "Recibido";
            }
            else
            if (e.NewValues["Estaus"].ToString() == "4")
            {
                ppRecibido.HeaderText = "Rechazado";
                gvComisariato.JSProperties["cpShowPopup"] = true;
                gvComisariato.JSProperties["cpTipo"] = "Rechazado";
            }
            else if (e.NewValues["Estaus"].ToString() == "3")
            {
                gvComisariato.JSProperties["cpShowPopup"] = true;
                gvComisariato.JSProperties["cpTipo"] = "Abordo";
            }
            else
            if (e.NewValues["Estaus"].ToString() == "1")
            {
                if (eInsertaDetalle != null)
                    eInsertaDetalle(null, null);
            }

            CancelEditing(e);
            ObtieneValores();
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso"); }

        }
        protected void CancelEditing(CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                gvComisariato.CancelEdit();
            }
            catch (Exception x) { throw x; }
        }
        protected void btnGuardaComisariato_Click(object sender, EventArgs e)
        {
            try { 
            if (eInsertaDetalle != null)
                eInsertaDetalle(null, null);

            //ppRecibido.ShowOnPageLoad = false;

            LimpiaControles();
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardaComisariato_Click", "Aviso"); }
        }
        protected void btnCancelarCom_Click(object sender, EventArgs e)
        {
            try { 
            //ppRecibido.ShowOnPageLoad = false;
            LimpiaControles();
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnCancelarCom_Click", "Aviso"); }

        }
        protected void gvComisariato_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            int iPos = 0;
            for (iPos = 0; iPos < DrPermisos[0].ItemArray.Length; iPos++)
            {
                switch (iPos)
                {
                    case 5: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                        {
                            if (e.ButtonType == ColumnCommandButtonType.New)
                                e.Enabled = true;
                        }
                        else
                        {
                            if (e.ButtonType == ColumnCommandButtonType.New)
                                e.Enabled = false;
                        } break;
                    case 6: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                        {
                            if (e.ButtonType == ColumnCommandButtonType.Edit)
                                e.Enabled = true;
                        }
                        else
                        {
                            if (e.ButtonType == ColumnCommandButtonType.Edit)
                                e.Enabled = false;
                        } break;
                    case 7: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                        {
                            if (e.ButtonType == ColumnCommandButtonType.Delete)
                                e.Enabled = true;
                        }
                        else
                        {
                            if (e.ButtonType == ColumnCommandButtonType.Delete)
                                e.Enabled = false;
                        } break;
                }
            }
        }
        #endregion 

        #region METODOS
        public void ObtieneValores()
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, EventArgs.Empty);
            } catch (Exception x) { throw x; }
        }
        public void LoadObjects(DataTable dtObjMonitor)
        {
            try { 
            gvComisariato.DataSource = null;
            gvComisariato.DataSource = dtObjMonitor;
            gvComisariato.DataBind();
            ViewState["MonitorComisariato"] = null;
            ViewState["MonitorComisariato"] = dtObjMonitor;
            }
            catch (Exception x) { throw x; }
        }
        protected void CargaGrid()
        {
            try
            {
                DataTable DT = (DataTable)ViewState["MonitorComisariato"];
                if (DT != null)
                {
                    gvComisariato.DataSource = DT;
                    gvComisariato.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try { 
                //mpeMensaje.ShowMessage(sMensaje, sCaption);
            lbl.Text = sMensaje;
            popup.HeaderText = sCaption;
            popup.ShowOnPageLoad = true;
            gvComisariato.JSProperties["cpText"] = sMensaje;
            gvComisariato.JSProperties["cpShowPopup"] = true;
            }
            catch (Exception x) { throw x; }
        }
        public void LoadStatus(DataTable dtObjSatus)
        {
            try { 
            if (dtObjSatus != null && dtObjSatus.Rows.Count > 0)
                ViewState["dtObjSatus"] = dtObjSatus;
            }
            catch (Exception x) { throw x; }
        }
        public void GetComisariatoID(int IDComisariato)
        {
            try
            {
                Session["iIDComisariato"] = IDComisariato;
            }
            catch (Exception x) { throw x; }
        }
        protected void LimpiaControles()
        {
            try { 
            txtRemision.Text ="";
            txtRemision.ErrorText = "";
            txtRemision.ValidationSettings.ErrorText = "";

            txtSubTotal.Text ="";
            txtSubTotal.ErrorText = "";
            txtSubTotal.ValidationSettings.ErrorText = "";
            mNotas.Text = "";
            rbTipo.SelectedIndex = -1;
            rbTipo.ErrorText = "";
            rblRechazo.SelectedIndex = -1;
            rblRechazo.ErrorText = "";
            lblImpt.Text = "";

            }
            catch (Exception x) { throw x; }
        }
        public void LoadComisariatoDetalle(DataTable dtObjSatus)
        {
            if(dtObjSatus != null && dtObjSatus.Rows.Count > 0)
            {



                if (dtObjSatus.Rows[0]["StatusRecibido"].S() != "")
                    gvComisariato.JSProperties["cprbTipo"] = dtObjSatus.Rows[0]["StatusRecibido"].S() == "Completo" ? 1 : 0;
                else
                    gvComisariato.JSProperties["cprbTipo"] = -1;
               
                
                
                gvComisariato.JSProperties["cpRemision"] = dtObjSatus.Rows[0]["Remision"].S(); ;
                gvComisariato.JSProperties["cptxtSubTotal"] = dtObjSatus.Rows[0]["Subtotal"].S();
                gvComisariato.JSProperties["cpmNotas"] = dtObjSatus.Rows[0]["Notas"].S();
                gvComisariato.JSProperties["cpImpC"] = dtObjSatus.Rows[0]["PrecioCotizado"].S().D().ToString("C");

                
            }
        }
        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                btnBusqueda.Enabled = false;
                txtTrip.Enabled = false;
                txtCliente.Enabled = false;
                dFecha.Enabled = false;
                ASPxButton2.Enabled = false;
            }
            else
            {
                for (iPos = 0; iPos < DrActions[0].ItemArray.Length; iPos++)
                {
                    switch (iPos)
                    {
                        case 4: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                btnBusqueda.Enabled = true;
                                txtTrip.Enabled = true;
                                txtCliente.Enabled = true;
                                dFecha.Enabled = true;
                                ASPxButton2.Enabled = true;
                            }
                            else
                            {
                                btnBusqueda.Enabled = false;
                                txtTrip.Enabled = false;
                                txtCliente.Enabled = false;
                                dFecha.Enabled = false;
                                ASPxButton2.Enabled = false;
                            } break;
                    }
                }
            }

        }
        #endregion

        #region  VARIABLES
        private const string sClase = "MonitorComisariato.aspx.cs";
        private const string sPagina = "MonitorComisariato.aspx";

        MonitorComisariato_Presenter oPresenter;
        public int iIDComisariato
        {
            get { return this.Session ["iIDComisariato"].S().I(); }
            set { Session["iIDComisariato"] = iIDComisariato; }
        }
        public object oCrud
        {
            get { return ViewState["CrudComisariato"]; }
            set { ViewState["CrudComisariato"] = value; }
        }
        public ALE_MexJet.Objetos.Enumeraciones.TipoOperacion eCrud
        {
            get { return (ALE_MexJet.Objetos.Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eLoadStaus;
        public event EventHandler eActualizaStataus;
        public event EventHandler eInsertaDetalle;
        public event EventHandler eConsultaComisariatoDetalle;
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
                return new object[] { 
                                        "@FECHA", dFecha.Text == "" ? System.DateTime.Now.ToString("MM-dd-yyyy") : dFecha.Date.ToString("MM-dd-yyyy"), 
                                        "@TRIP", txtTrip.Text , 
                                        "@CLIENTE",txtCliente.Text,
                                        "@Usuario" , ((UserIdentity)Session["UserIdentity"]).sUsuario.S()
                                        //"@Usuario" , "f.suarez"
                                    };
            }
        }
        public object[] oArrFil
        {
            get
            {
                string sRecibido = string.Empty;
                string sRechazado = string.Empty;

                if (rbTipo.SelectedIndex != -1)
                    sRecibido = rbTipo.SelectedIndex == 1 ? "Incompleto" : "Completo";

                if (rblRechazo.SelectedIndex != -1)
                    sRechazado = rblRechazo.SelectedIndex == 0 ? "Por Demora" : "Por Presentación";

                return new object[] { 
                                      "@IdComisariato" , iIDComisariato
                                      ,"@Remision" , txtRemision.Text
                                      ,"@Subtotal", txtSubTotal.Text
                                      ,"@StatusRecibido" , sRecibido
                                      ,"@StatusRechazado" , sRechazado
                                      ,"@Notas" , mNotas.Text 
                                      ,"@UsuarioCreacion", ((UserIdentity)Session["UserIdentity"]).sUsuario
                                      ,"@IP",""
                                    };
            }
        }
        public object[] oArrID
        {
            get
            {
                return new object[] { 
                                      "@IdComisariato" , iIDComisariato
                                    };
            }
        }
        #endregion



    }
}