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


using System.IO;

namespace ALE_MexJet.Views.AtencionClientes
{
    public partial class frmSolicitudes : System.Web.UI.Page, IViewSolicitudVuelo
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string s = hfOrigen.Value.S();

                oPresenter = new SolicitudVuelo_Presenter(this, new DBSolicitudesVuelo());
                MonitorCliente monitor = new MonitorCliente();
                monitor = (MonitorCliente)(Session["Monitor"]);
                if (monitor != null)
                {
                    if (!IsPostBack)
                    {
                        lblIntercambio.Text = String.Empty;
                        Session["ItinerarioV"] = null;
                        Session["FileBytes"] = null;
                        GetIdSolicitud(Page.Request["Id"] != null ? Page.Request["Id"].S().I() : -1);
                        lblCliente.Text += monitor.sCodigoCliente;
                        lblContrato.Text += monitor.sClaveContrato.S();
                        lblTipoContrato.Text += monitor.sTipoContrato.S();
                        lblTipoEquipo.Text += monitor.sGrupoModelo.S();
                        iIdCliente = monitor.iIdCliente;
                        iIdContrato = monitor.iIdContrato;
                        ObtieneContactos();
                        ObtieneEquipos();
                        ObtieneEstatus();
                        ObtieneOrigen();
                        ObtieneMotivo();
                        CargaModeloContrtrato();
                        CargaControles();
                        CargaItinerarioGV();

                        if (eConsultaTripGuides != null)
                            eConsultaTripGuides(sender, e);

                        if (Page.Request["Accion"] != null)
                        {
                            switch (Page.Request["Accion"].S())
                            {
                                case "1": // Habilita la pestaña 3 (Itinerario)
                                    RedireccionWizard(2);
                                    break;
                            }
                        }
                    }

                    //CargaNombreArchivo();
                    ObtieneTrips();
                    ObtieneTramoSol();

                    CargaMotivo();
                    //ValidaModelo();
                    lblMnsaj.Text = "";
                    lblDoc.Text = "";
                    RecargaGvHistorico();
                }
                else
                {
                    Response.Redirect("~/Views/AtencionClientes/frmMonitorClientes.aspx");
                }
                if (ppPasajeros.ShowOnPageLoad == true)
                {
                    ObtinePaxTramo2(Session["iIdTramo2"].S().I());
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }
        protected void cmbEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbEquipo.SelectedItem.Value.S().I() != iIdEquipo)
                {
                    lblIntercambio.Text = "Intercambio";
                    lblIntercambio.ForeColor = System.Drawing.Color.Red;
                }
                else
                    lblIntercambio.Text = "";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "cmbEquipo_SelectedIndexChanged", "Aviso");
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
                CargaMotivo();
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
        protected void gvTrip_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {

        }
        protected void gvTrip_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {

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
        protected void CancelEditing(CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                gvTrip.CancelEdit();
                gvTramos.CancelEdit();
                gvPasajeros.CancelEdit();
                gvComisariato.CancelEdit();
                gvHistorico.CancelEdit();
                Session["Val"] = null;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "CancelEditing", "Aviso");
            }
        }
        protected void btnGuardarSolicitud_Click(object sender, EventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;
                if (eNewObj != null)
                    eNewObj(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardarSolicitud_Click", "Aviso");
            }
        }
        protected void btnNuevoTramo_Click(object sender, EventArgs e)
        {
            try
            {
                gvTramos.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvTramos.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevoTramo_Click", "Aviso");
            }
        }
        protected void gvTramos_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;

                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                else
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramos_CellEditorInitialize", "Aviso");
            }
        }
        protected void gvTramos_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;

                if (eEliminaTramoSol != null)
                    eEliminaTramoSol(sender, e);

                CancelEditing(e);

                ObtieneTramoSol();

                //ObtieneViabilidad();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramos_RowDeleting", "Aviso");
            }
        }
        protected void gvTramos_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                ViewState["VueloSimultaneo"] = null;
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;

                if (eEditaTramoSol != null)
                    eEditaTramoSol(sender, e);

                ObtieneTramoSol();
                CancelEditing(e);

                cmbEstatus.Items.Clear();
                cmbEstatus.Items.Add("MODIFICADO", 2);
                cmbEstatus.SelectedIndex = 0;
                Session["hfEdicion"] = "2";

                Session["Val"] = null;

                //if (e.NewValues["idaeropuertoo"].S() != e.OldValues["idaeropuertoo"].S() ||
                //    e.NewValues["idaeropuertod"].S() != e.OldValues["idaeropuertod"].S() ||
                //    e.NewValues["HoraVuelo"].S() != e.OldValues["HoraVuelo"].S() ||
                //    e.NewValues["FechaVuelo"].S() != e.OldValues["FechaVuelo"].S())
                //{
                //    ObtieneViabilidad();
                //}

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramos_RowUpdating", "Aviso");
            }
        }
        protected void gvTramos_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                if (eNewObj != null)
                    eNewObj(sender, e);

                eCrud = Enumeraciones.TipoOperacion.Insertar;

                e.NewValues["IdSolicitud"] = iIdSolicitud;
                e.NewValues["UserIdentity"] = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();

                oCrud = e;

                if (eNewTramo != null)
                    eNewTramo(sender, e);

                CancelEditing(e);
                ObtieneTramoSol();
                Session["hfEdicion"] = "1";
                //ObtieneViabilidad();

                ViewState["VueloSimultaneo"] = null;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramos_RowInserting", "Aviso");
            }
        }
        protected void gvTramos_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                gvTramos.SettingsText.PopupEditFormCaption = "Formulario de Edición";
                Session["Val"] = "1";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramos_StartRowEditing", "Aviso");
            }
        }
        protected void gvTramos_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                if (eNewObj != null)
                    eNewObj(sender, e);

                e.NewValues["IdSolicitud"] = iIdSolicitud;
                oCrud = e;

                eCrud = Enumeraciones.TipoOperacion.Validar;
                oCrud = e;

                string IdSol_Fecha = iIdSolicitud + "|" + e.NewValues["FechaVuelo"].S().Dt() + "|" + e.NewValues["HoraVuelo"].S().Dt().ToString("HH:mm"); ;

                if (eValidaVuelosimultaneo != null)
                    eValidaVuelosimultaneo(IdSol_Fecha, null);

                if (eValidaFechaHora != null)
                    eValidaFechaHora(sender, e);

                if (Session["Validacion"] != null && Session["Validacion"].S().I() > 0)
                {
                    AddError(e.Errors, gvTramos.Columns["HoraVuelo"], "La fecha y hora son iguales o menores de una solicitud ya ingresada, favor de validarlo.");
                    Session["Validacion"] = null;
                }

                if (e.NewValues["idaeropuertoo"].S() == e.NewValues["idaeropuertod"].S())
                {
                    AddError(e.Errors, gvTramos.Columns["idaeropuertod"], "Aeropuerto Origen y Aeropuerto Destino no pueden ser iguales.");
                }

                //if (ViewState["VueloSimultaneo"].S().I() == 2)
                //{
                //    MostrarMensaje("Se le recuerda que a ocupado el total de vuelos simultaneos.", "Vuelos Simultaneos");
                //    //popup.HeaderText = "Vuelos simultaneos";
                //    //gvTramos.JSProperties["cpText"] = "Se le recuerda que a ocupado el total de vuelos simultaneos.";
                //    //gvTramos.JSProperties["cpMuestraPop"] = true;
                //}

                //if (Session["Val"] != null && Session["Val"].S() == "1")
                //    if (e.NewValues["idaeropuertoo"].S() != e.OldValues["idaeropuertoo"].S())
                //        AddError(e.Errors, gvTramos.Columns["idaeropuertoo"], "Aeropuerto Origen  no pueden ser editado.");
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramos_RowValidating", "Aviso");
            }
        }
        protected void gvTramos_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {

        }
        protected void gvTramos_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Pasajeros")
                {
                    Session["iIdTramo2"] = e.CommandArgs.CommandArgument.S().I();
                    ObtinePaxTramo2(Session["iIdTramo2"].S().I());
                    ppPasajeros.HeaderText = "Pasajeros";
                    ppPasajeros.ShowOnPageLoad = true;
                }
                else if (e.CommandArgs.CommandName.S() == "Comisariato")
                {
                    Session["iIdTramo"] = e.CommandArgs.CommandArgument.S().I();
                    ObtineComisariatoTramo();
                    ppComisariato.HeaderText = "Comisariato";
                    ppComisariato.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramos_RowCommand", "Aviso");
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
        protected void Origenid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnGuardarSeguimiento_Click(object sender, EventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;

                if (eNewSeguimiento != null)
                    eNewSeguimiento(null, EventArgs.Empty);

                if (Session["Bytes"] != null)
                {
                    if (ePDFSeguimiento != null)
                        ePDFSeguimiento(null, null);
                }

                ObtieneHistorico();
                mPrueba.Text = string.Empty;
                mNota.Text = "";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardarSeguimiento_Click", "Aviso");
            }
        }
        protected void btnGuardarItinerario_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["FileBytes"] != null)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    if (eEditarSolVuelo != null)
                        eEditarSolVuelo(null, EventArgs.Empty);

                    //CargaNombreArchivo();
                    //imArchivo.Visible = true;
                    CargaItinerarioGV();
                    mPrueba.Text = string.Empty;

                    if (eLoadCorreoAlta != null)
                        eLoadCorreoAlta(iIdSolicitud, e);

                    CorreoVentas((DataSet)ViewState["Correos"]);
                }
                else
                {
                    lblDoc.Text = "Es necesario subir un PDF.";
                    lblDoc.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardarItinerario_Click", "Aviso");
            }
        }
        protected void FillCombo(ASPxComboBox cmb, string country)
        {
            try
            {
                if (country != "")
                    sOrigen += country;
                if (sOrigen.Length > 1)
                {
                    if (string.IsNullOrEmpty(country)) return;
                    if (eLoadOrigenVueloTodos != null)
                        eLoadOrigenVueloTodos(this, EventArgs.Empty);
                    cmb.Items.Clear();
                    cmb.DataSource = (DataTable)ViewState["Origenes"];
                    cmb.ValueField = "IdOrigen";
                    cmb.TextField = "Origen";
                    cmb.DataBindItems();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "FillCombo", "Aviso");
            }
        }
        void cmbOrigen_OnKeyPress(object source, CallbackEventArgsBase e)
        {
            try
            {
                FillCombo(source as ASPxComboBox, e.Parameter);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "cmbOrigen_OnKeyPress", "Aviso");
            }
        }
        protected void ddlPruebaASP_TextChanged(object sender, EventArgs e)
        {

        }
        protected void gvTramos_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            //gvTramos.Columns["Status"].Visible = true;
        }
        protected void ASPxComboBox_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                ASPxComboBox comboBox = (ASPxComboBox)source;
                string sFiltroAeropuerto = e.Filter;
                //if (gvTramos.VisibleRowCount == 0 || gvTramos.SettingsText.PopupEditFormCaption == "Formulario de Edición" || (Session["Val"] != null && Session["Val"].S() == "1"))
                //{
                if (sFiltroAeropuerto.Length < 1)
                {
                    if (eLoadOrigenDestino != null)
                        eLoadOrigenDestino(this, e);
                }
                else
                {
                    if (eLoadOrigDestFiltro != null)
                        eLoadOrigDestFiltro(sFiltroAeropuerto, e);
                }

                DataTable DD = (DataTable)ViewState["OrigenDestino"];
                if (DD.Rows.Count > 0)
                {
                    comboBox.DataSource = (DataTable)ViewState["OrigenDestino"];
                    comboBox.ValueField = "idorigen";
                    comboBox.ValueType = typeof(Int32);
                    comboBox.TextField = "AeropuertoICAO";
                    comboBox.DataBindItems();
                }
                else
                    comboBox.JSProperties["cpVal2"] = "1";

                //}
                //else
                //{
                //    string var = sFiltroAeropuerto + "|" + iIdSolicitud.S();
                //    if (eAerTramo != null)
                //        eAerTramo(var, e);

                //    DataTable DT = (DataTable)ViewState["Ori"];
                //    if (DT.Rows.Count > 0)
                //    {
                //        comboBox.DataSource = (DataTable)ViewState["Ori"];
                //        comboBox.ValueField = "idorigen";
                //        comboBox.ValueType = typeof(Int32);
                //        comboBox.TextField = "AeropuertoICAO";
                //        comboBox.DataBindItems();
                //    }
                //    else
                //    {
                //        comboBox.JSProperties["cpVal2"] = "1";
                //    }
                //}
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ASPxComboBox_OnItemsRequestedByFilterCondition", "Aviso");
            }
        }
        protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {

        }
        protected void ASPxComboBox2_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                ASPxComboBox comboBox = (ASPxComboBox)source;
                string sFiltroAeropuerto = e.Filter;
                if (gvTramos.VisibleRowCount == 0 || gvTramos.SettingsText.PopupEditFormCaption == "Formulario de Edición" || (Session["Val"] != null && Session["Val"].S() == "1"))
                {
                    if (sFiltroAeropuerto.Length < 1)
                    {
                        if (eLoadOrigenDestino != null)
                            eLoadOrigenDestino(this, e);
                    }
                    else
                    {
                        if (eLoadOrigDestFiltro != null)
                            eLoadOrigDestFiltro(sFiltroAeropuerto, e);
                    }

                    DataTable DD = (DataTable)ViewState["OrigenDestino"];
                    if (DD.Rows.Count > 0)
                    {
                        comboBox.DataSource = (DataTable)ViewState["OrigenDestino"];
                        comboBox.ValueField = "idorigen";
                        comboBox.ValueType = typeof(Int32);
                        comboBox.TextField = "AeropuertoICAO";
                        comboBox.DataBindItems();
                    }
                    else
                        comboBox.JSProperties["cpVal"] = "1";

                }
                else
                {
                    string var = sFiltroAeropuerto + "|" + iIdSolicitud.S();
                    if (eAerTramo != null)
                        eAerTramo(var, e);

                    DataTable DT = (DataTable)ViewState["Des"];
                    if (DT.Rows.Count > 0)
                    {
                        comboBox.DataSource = (DataTable)ViewState["Des"];
                        comboBox.ValueField = "iddestino";
                        comboBox.ValueType = typeof(Int32);
                        comboBox.TextField = "AeropuertoICAO";
                        comboBox.DataBindItems();
                    }
                    else
                    {
                        comboBox.JSProperties["cpVal"] = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ASPxComboBox2_OnItemsRequestedByFilterCondition", "Aviso");
            }
        }
        protected void ASPxComboBox2_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {

        }
        protected void gvPasajeros_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;

                ASPxGridView gridView = (ASPxGridView)sender;

                if (gvPasajeros.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPasajeros_CellEditorInitialize", "Aviso");
            }
        }
        protected void gvPasajeros_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;

                if (eEliminaPaxTramo != null)
                    eEliminaPaxTramo(sender, e);

                CancelEditing(e);

                ObtinePaxTramo(Session["iIdTramo2"].S().I());

                gvPasajeros.DataSource = (DataTable)ViewState["LoadPaxTramo"];
                gvPasajeros.DataBind();

                //ObtieneViabilidad();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPasajeros_RowDeleting", "Aviso");
            }
        }
        protected void gvPasajeros_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                e.NewValues["IdTramo"] = Session["iIdTramo2"].S().I();
                e.NewValues["UserIdentity"] = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();
                oCrud = e;

                if (eInsertaPaxTramo != null)
                    eInsertaPaxTramo(sender, e);
                ObtinePaxTramo2(Session["iIdTramo2"].S().I());
                CancelEditing(e);

                Session["hfEdicion"] = "2";

                //   ObtieneViabilidad();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPasajeros_RowInserting", "Aviso");
            }
        }
        protected void gvPasajeros_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;
                e.NewValues["UserIdentity"] = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();
                if (eEditaPaxTramo != null)
                    eEditaPaxTramo(sender, e);
                ObtinePaxTramo2(Session["iIdTramo2"].S().I());
                CancelEditing(e);

                Session["hfEdicion"] = "2";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPasajeros_RowUpdating", "Aviso");
            }
        }
        protected void gvPasajeros_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvPasajeros.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPasajeros_StartRowEditing", "Aviso");
            }
        }
        protected void gvPasajeros_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {

        }
        protected void gvPasajeros_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {

        }
        protected void gvPasajeros_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {

        }
        protected void bNewPasajero_Click(object sender, EventArgs e)
        {
            try
            {
                gvPasajeros.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvPasajeros.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "bNewPasajero_Click", "Aviso");
            }
        }
        protected void gvComisariato_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;

                ASPxGridView gridView = (ASPxGridView)sender;

                if (gvComisariato.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }


                if (e.Column.FieldName == "Descripcion")
                {
                    if (eConsultaProveedor != null)
                        eConsultaProveedor(this, e);
                    ASPxComboBox comboBox = e.Editor as ASPxComboBox;
                    comboBox.DataSource = (DataTable)ViewState["LoadProveedor"];
                    comboBox.ValueField = "IdProveedor";
                    comboBox.ValueType = typeof(Int32);
                    comboBox.TextField = "Descripcion";
                    comboBox.DataBindItems();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvComisariato_CellEditorInitialize", "Aviso");
            }
        }
        protected void gvComisariato_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;

                if (eEliminaComisariatoTramo != null)
                    eEliminaComisariatoTramo(sender, e);

                ObtineComisariatoTramo();
                CancelEditing(e);

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvComisariato_RowDeleting", "Aviso");
            }
        }
        protected void gvComisariato_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                e.NewValues["IdTramo"] = Session["iIdTramo"].S().I();
                e.NewValues["UserIdentity"] = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();
                oCrud = e;

                if (eInsertaComisariatoTramo != null)
                    eInsertaComisariatoTramo(sender, e);
                ObtineComisariatoTramo();
                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvComisariato_RowInserting", "Aviso");
            }
        }
        protected void gvComisariato_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;
                e.NewValues["UserIdentity"] = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();
                if (eEditaComisariatoTramo != null)
                    eEditaComisariatoTramo(sender, e);

                ObtineComisariatoTramo();
                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvComisariato_RowUpdating", "Aviso");
            }
        }
        protected void gvComisariato_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvComisariato.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvComisariato_StartRowEditing", "Aviso");
            }
        }
        protected void gvComisariato_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {

        }
        protected void gvComisariato_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {

        }
        protected void gvComisariato_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {

        }
        protected void btnNuevoComisaiato_Click(object sender, EventArgs e)
        {
            try
            {
                gvComisariato.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvComisariato.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevoComisaiato_Click", "Aviso");
            }
        }
        protected void gvDetalle_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView grid = sender as ASPxGridView;
                Session["iIdTramo"] = grid.GetMasterRowKeyValue().S();

                ObtinePaxTramo(Session["iIdTramo"].S().I());
                grid.DataSource = (DataTable)ViewState["LoadPaxTramo"];
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetalle_BeforePerformDataSelect", "Aviso");
            }
        }
        protected void Proveedor_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                ASPxComboBox comboBox = (ASPxComboBox)source;

                if (eConsultaProveedor != null)
                    eConsultaProveedor(this, e);

                comboBox.DataSource = (DataTable)ViewState["LoadProveedor"];
                comboBox.ValueField = "IdProveedor";
                comboBox.ValueType = typeof(Int32);
                comboBox.TextField = "Descripcion";
                comboBox.DataBindItems();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Proveedor_OnItemsRequestedByFilterCondition", "Aviso");
            }
        }
        protected void gvHistorico_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;

                if (eEliminaHistorico != null)
                    eEliminaHistorico(sender, e);

                CancelEditing(e);

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvHistorico_RowDeleting", "Aviso");
            }
        }
        protected void gvHistorico_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;

                ASPxGridView gridView = (ASPxGridView)sender;

                if (gvHistorico.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvHistorico_CellEditorInitialize", "Aviso");
            }
        }
        protected void gvHistorico_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {

        }
        protected void gvHistorico_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
        }
        protected void gvHistorico_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvPasajeros.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvHistorico_StartRowEditing", "Aviso");
            }
        }
        protected void gvHistorico_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {

        }
        protected void gvHistorico_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {

        }
        protected void gvHistorico_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {

        }
        protected void UploadControl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            try
            {
                if (e.IsValid)
                {
                    Session["FileBytes"] = e.UploadedFile.FileBytes;
                    Session["Nombre"] = e.UploadedFile.FileName.S();
                    lblMensaje.Text = "El archivo" + e.UploadedFile.FileName.S() + " se adjunto.";
                    lblMensaje.BackColor = System.Drawing.Color.Green;
                    UploadControl.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UploadControl_FileUploadComplete", "Aviso");
            }
        }
        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            try
            {
                ObtieneTramoSol();
                CargaMotivo();

                if (eNewObj != null)
                    eNewObj(sender, e);

                if (Session["hfEdicion"] != null && (Session["hfEdicion"].S().Equals("1") || Session["hfEdicion"].S().Equals("2")))
                {
                    if (eGuardaSeguimientoHistorico != null)
                        eGuardaSeguimientoHistorico(null, null);

                    if (Utils.IsSolicitudMobile(iIdSolicitud))
                        Utils.NotificaAppMobile(iIdSolicitud, "Pendiente");

                    ObtieneViabilidad();

                    new DBMonitorTrafico().DBInsertaMonitorTrafico(iIdSolicitud);
                    new DBMonitorAtencionClientes().DBSetInsertaMonitorAtencionClientes(iIdSolicitud);

                    if (eLoadCorreoAlta != null)
                        eLoadCorreoAlta(iIdSolicitud, e);
                    CorreobtnTerminar((DataSet)ViewState["Correos"]);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnTerminar_Click", "Aviso");
            }
        }
        protected void btnSiguiente1_Click(object sender, EventArgs e)
        {
            try
            {

                if (iIdSolicitud != 0 || iIdSolicitud != -1)
                {
                    ASPxPageControl1.ActiveTabIndex = 1;
                    ASPxPageControl1.TabPages.FindByName("Historico").ClientEnabled = true;
                    ASPxPageControl1.TabPages.FindByName("AltaSolicitud").ClientEnabled = true;
                    ASPxPageControl1.TabPages.FindByName("Itinerario").ClientEnabled = false;
                }
                else
                {
                    lblMnsaj.Text = "Es necesario ingresar almenos un tramo.";
                    lblMnsaj.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSiguiente1_Click", "Aviso");
            }
        }
        protected void btnSiguiente2_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxPageControl1.ActiveTabIndex = 2;
                ASPxPageControl1.TabPages.FindByName("Itinerario").ClientEnabled = true;
                ASPxPageControl1.TabPages.FindByName("AltaSolicitud").ClientEnabled = true;
                ASPxPageControl1.TabPages.FindByName("Historico").ClientEnabled = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSiguiente2_Click", "Aviso");
            }
        }
        protected void btnAtras2_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxPageControl1.ActiveTabIndex = 0;
                ASPxPageControl1.TabPages.FindByName("AltaSolicitud").ClientEnabled = true;
                ASPxPageControl1.TabPages.FindByName("Historico").ClientEnabled = true;
                ASPxPageControl1.TabPages.FindByName("Itinerario").ClientEnabled = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAtras2_Click", "Aviso");
            }
        }
        protected void btnAtras3_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxPageControl1.ActiveTabIndex = 1;
                ASPxPageControl1.TabPages.FindByName("Historico").ClientEnabled = true;
                ASPxPageControl1.TabPages.FindByName("AltaSolicitud").ClientEnabled = true;
                ASPxPageControl1.TabPages.FindByName("Itinerario").ClientEnabled = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAtras3_Click", "Aviso");
            }
        }
        protected void gvDetalle_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName == "Acciones")
                {
                    ObtinePaxTramo2(Session["iIdTramo"].S().I());
                    ppPasajeros.HeaderText = "Pasajeros";
                    ppPasajeros.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetalle_RowCommand", "Aviso");
            }
        }
        protected void ASPxPageControl1_TabClick(object source, TabControlCancelEventArgs e)
        {
            try
            {
                if (gvTramos.VisibleRowCount == 0)
                    ASPxPageControl1.ActiveTabIndex = 0;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ASPxPageControl1_TabClick", "Aviso");
            }
        }
        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Views/AtencionClientes/frmMonitorClientes.aspx");
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ASPxButton1_Click", "Aviso");
            }
        }
        protected void gvHistorico_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName == "Detalle")
                {
                    object x = e.CommandArgs.CommandArgument.S();
                    CargaDetalle(x);

                    ppDetalle.ShowOnPageLoad = true;
                }
                else if (e.CommandArgs.CommandName == "Descarga")
                {
                    object x = e.CommandArgs.CommandArgument.S();

                    if (eConsultaPDFSeguimiento != null)
                        eConsultaPDFSeguimiento(x, null);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvHistorico_RowCommand", "Aviso");
            }
        }
        protected void lMsgArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                Byte[] drPDF = (Byte[])Session["ItinerarioV"];
                if (drPDF != null)
                {
                    MemoryStream ms = new MemoryStream(drPDF);
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + NombreA.S());
                    Response.ContentType = "application/octet-stream";
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //ms.WriteTo(Response.OutputStream);
                    Response.BinaryWrite(drPDF);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "lMsgArchivo_Click", "Aviso");
            }
        }
        protected void gvItinerario_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            if (e.CommandArgs.CommandName == "Descarga")
            {

                DataTable DT = (DataTable)ViewState["LoadItinerario"];
                DataTable D = DT.Select("IdItinerario in ( " + e.CommandArgs.CommandArgument.ToString() + ")").CopyToDataTable();

                //Byte[] drPDF = (Byte[])Session["ItinerarioV"];
                Byte[] drPDF = (Byte[])D.Rows[0]["ItinerarioV"];
                if (drPDF != null)
                {
                    MemoryStream ms = new MemoryStream(drPDF);
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + D.Rows[0]["NombreArchivo"].S());
                    Response.ContentType = "application/octet-stream";
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //ms.WriteTo(Response.OutputStream);
                    Response.BinaryWrite(drPDF);
                    Response.Flush();
                    Response.End();
                }
            }
            else if (e.CommandArgs.CommandName == "Elimina")
            {
                iIditinerario = e.CommandArgs.CommandArgument.S().I();
                if (eEliminaItinerario != null)
                    eEliminaItinerario(null, null);

                CargaItinerarioGV();
            }
            else if (e.CommandArgs.CommandName == "Detalle")
            {
                iIditinerario = e.CommandArgs.CommandArgument.S().I();

                if (eConsultaDetalleItinerario != null)
                    eConsultaDetalleItinerario(null, null);

                ppDetalle.ShowOnPageLoad = true;
            }
        }
        protected void Pasajeros_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                ASPxComboBox comboBox = (ASPxComboBox)source;
                string sFiltroAeropuerto = e.Filter;
                //if (gvTramos.VisibleRowCount == 0 || gvTramos.SettingsText.PopupEditFormCaption == "Formulario de Edición" || (Session["Val"] != null && Session["Val"].S() == "1"))
                //{
                if (sFiltroAeropuerto.Length < 1)
                {
                    if (eBuscaPasajero != null)
                        eBuscaPasajero(string.Empty, e);
                }
                else
                {
                    if (eBuscaPasajero != null)
                        eBuscaPasajero(sFiltroAeropuerto, e);
                }

                DataTable DD = (DataTable)ViewState["CargaPasajeros"];
                if (DD.Rows.Count > 0)
                {
                    comboBox.DataSource = (DataTable)ViewState["CargaPasajeros"];
                    comboBox.ValueField = "NombrePax";
                    comboBox.TextField = "NombrePax";
                    comboBox.DataBindItems();
                }
                else
                    comboBox.JSProperties["cpV2"] = "1";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ASPxComboBox_OnItemsRequestedByFilterCondition", "Aviso");
            }
        }
        protected void Pasajeros_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {

        }
        protected void btnguardaP_Click(object sender, EventArgs e)
        {
            if (eInsertaPasajero != null)
                eInsertaPasajero(null, null);

            txtApellidoP.Text = "";
            txtNombreP.Text = "";

        }
        protected void btnCancelaP_Click(object sender, EventArgs e)
        {

        }
        protected void btnNuevoP_Click(object sender, EventArgs e)
        {

            ppAltaPasajeros.ShowOnPageLoad = true;
        }
        protected void upcSeguimiento_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            try
            {
                if (e.IsValid)
                {
                    Session["Bytes"] = e.UploadedFile.FileBytes;
                    Session["NombreArchivo"] = e.UploadedFile.FileName.S();
                    lblMensaje.Text = "El archivo" + e.UploadedFile.FileName.S() + " se adjunto.";
                    lblMensaje.BackColor = System.Drawing.Color.Green;
                    UploadControl.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "upcSeguimiento_FileUploadComplete", "Aviso");
            }
        }
        protected void lbArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                Byte[] drPDF = (Byte[])ViewState["Bytes"];
                if (drPDF != null)
                {
                    MemoryStream ms = new MemoryStream(drPDF);
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + NombreA.S());
                    Response.ContentType = "application/octet-stream";
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //ms.WriteTo(Response.OutputStream);
                    Response.BinaryWrite(drPDF);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "lbArchivo_Click", "Aviso");
            }
        }
        protected void btnTripGuide_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTrips = (DataTable)ViewState["oTrips"];
                if (dtTrips != null)
                {
                    if (dtTrips.Rows.Count == 1)
                        Response.Redirect("~/Views/AtencionClientes/frmTripGuide.aspx?iIdTrip=" + dtTrips.Rows[0].S("Trip") + "&IdSolicitud=" + iIdSolicitud.S());
                    else if (dtTrips.Rows.Count > 1)
                    {
                        gvTrips.DataSource = dtTrips;
                        gvTrips.DataBind();
                        ppTrips.ShowOnPageLoad = true;
                    }
                    else if (dtTrips.Rows.Count == 0)
                    {
                        lbl.Text = "Debe tener un número de Trip.";
                        popup.ShowOnPageLoad = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnTripGuide_Click", "Aviso");
            }
        }
        protected void gvTripGuides_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName == "Descarga")
                {

                    ASPxGridView grid = (ASPxGridView)sender;
                    object id = e.KeyValue;
                    bPDF = (Byte[])grid.GetRowValuesByKeyValue(id, "PDF");

                    sNombreArchivoPDF = grid.GetRowValuesByKeyValue(id, "NombreArchivoPDF").ToString() + ".pdf";

                    if (bPDF != null)
                    {
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=" + sNombreArchivoPDF);
                        Response.ContentType = "application/octet-stream";
                        Response.Buffer = true;
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);

                        Response.BinaryWrite(bPDF);
                        Response.Flush();
                        Response.End();
                    }
                    //}
                }
                else if (e.CommandArgs.CommandName == "Elimina")
                {
                    int iIdTripGuide = e.CommandArgs.CommandArgument.S().I();

                    if (eDeleteTripGuide != null)
                        eDeleteTripGuide(iIdTripGuide, null);

                    //if (eConsultaTripGuides != null)
                    //    eConsultaTripGuides(sender, e);
                }
                else if (e.CommandArgs.CommandName == "Enviar")
                {

                    ASPxGridView grid = (ASPxGridView)sender;
                    object id = e.KeyValue;
                    bPDF = (Byte[])grid.GetRowValuesByKeyValue(id, "PDF");

                    sNombreArchivoPDF = grid.GetRowValuesByKeyValue(id, "NombreArchivoPDF").ToString();

                    ppEnviarMailTripGuide.ShowOnPageLoad = true;

                    txtPara.Text = string.Empty;
                    txtConCopia.Text = string.Empty;

                    if (eConsultaContactoSolicitud != null)
                        eConsultaContactoSolicitud(iIdSolicitud, null);

                    if (eConsultaVendedorSolicitud != null)
                        eConsultaVendedorSolicitud(iIdSolicitud, null);

                    iTrip = grid.GetRowValuesByKeyValue(id, "IdTrip").S().I();
                    sNombreContacto = grid.GetRowValuesByKeyValue(id, "NombreContacto").ToString();
                    sICAOOrigen = grid.GetRowValuesByKeyValue(id, "ICAOOrigen").ToString();
                    sNombreAeropuertoOrigen = grid.GetRowValuesByKeyValue(id, "NombreAeropuertoOrigen").ToString();
                    sICAODestino = grid.GetRowValuesByKeyValue(id, "ICAODestino").ToString();
                    sNombreAeropuertoDestino = grid.GetRowValuesByKeyValue(id, "NombreAeropuertoDestino").ToString();
                    DtFechaSalida = (DateTime)grid.GetRowValuesByKeyValue(id, "FechaSalida");
                    sNumeroPasajero = grid.GetRowValuesByKeyValue(id, "NumeroPasajero").ToString();
                    sAeronave = grid.GetRowValuesByKeyValue(id, "Aeronave").ToString();
                    sPiloto = grid.GetRowValuesByKeyValue(id, "Piloto").ToString();
                    sPilotoTelefono = grid.GetRowValuesByKeyValue(id, "PilotoTelefono").ToString();
                    sCoPiloto = grid.GetRowValuesByKeyValue(id, "CoPiloto").ToString();
                    sCoPilotoTelefono = grid.GetRowValuesByKeyValue(id, "CoPilotoTelefono").ToString();
                    memoObservaciones.Text = grid.GetRowValuesByKeyValue(id, "Observaciones").ToString();
                    sObservaciones = memoObservaciones.Text;

                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTripGuides_RowCommand", "Aviso");
            }
        }
        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            try
            {
                EnviarMail();
            }
            catch (Exception ex)
            {

                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnEnviarMail_Click", "Aviso");
            }
        }
        protected void btnSeleccionarTrip_Click(object sender, EventArgs e)
        {
            int iTrip = 0;
            foreach (GridViewRow row in gvTrips.Rows)
            {
                RadioButton rb = (RadioButton)row.FindControl("rbSeleccione");
                if (rb != null)
                {
                    if (rb.Checked)
                    {
                        iTrip = row.Cells[1].Text.S().I();
                        break;
                    }
                }
            }

            if (iTrip > 0)
                Response.Redirect("~/Views/AtencionClientes/frmTripGuide.aspx?iIdTrip=" + iTrip.S() + "&IdSolicitud=" + iIdSolicitud.S());
            else
                mpeMensaje.ShowMessage("Debe seleccionar al menos un Trip.", "Aviso");
        }

        #endregion

        #region Metodos
        public void ObtieneViabilidad()
        {
            if (gvTramos.VisibleRowCount > 0)
            {
                DataTable Tramo = (DataTable)gvTramos.DataSource;

                object[] oViabilidad;
                for (int i = 0; i < Tramo.Rows.Count; i++)
                {
                    oViabilidad = new object[] { 
                            new object[] 
                                                    { 
                                                        "@FechaVuelo", Tramo.Rows[i]["FechaVuelo"].Dt().ToString("MM/dd/yyyy") + " " + Tramo.Rows[i]["HoraVuelo"].S(),
                                                        "@Origen", Tramo.Rows[i]["aeropuertoo"].S(),
                                                        "@Destino", Tramo.Rows[i]["aeropuertod"].S(),
                                                        "@NumPax", Tramo.Rows[i]["NoPax"].S(),
                                                        "@IdSolicitud", iIdSolicitud
                                                    } 
                                                };


                    if (eViabilidad != null)
                        eViabilidad(oViabilidad, null);

                    if (!bViabilidad)
                        break;
                }

                if (Utils.IsSolicitudMobile(iIdSolicitud))
                    Utils.NotificaAppMobile(iIdSolicitud, "Pendiente");

                if (eInsertaMonitorDespacho != null)
                    eInsertaMonitorDespacho(null, null);

                if (bViabilidad)
                {
                    if (eGuardaSeguimientoHistorico != null)
                        eGuardaSeguimientoHistorico(null, null);
                }

                bViabilidad = false;
            }
        }
        protected void CargaItinerarioGV()
        {
            if (eConsultaItinerario != null)
                eConsultaItinerario(null, null);
        }
        public void LoadItinerario(DataTable dtObjCat)
        {
            gvItinerario.DataSource = dtObjCat;
            gvItinerario.DataBind();
            ViewState["LoadItinerario"] = dtObjCat;
        }
        protected void CargaModeloContrtrato()
        {
            try
            {
                if (eConsultaModCon != null)
                    eConsultaModCon(null, null);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void Modelo(DataTable DT)
        {
            try
            {
                if (DT != null && DT.Rows.Count > 0)
                {
                    GetEquipoID(DT.Rows[0]["GrupoModeloId"].S().I());
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadSolPDF(DataTable dtObjCat)
        {
            try
            {
                if (dtObjCat != null && dtObjCat.Rows.Count > 0 && dtObjCat.Rows[0]["NombreArchivo"].S().EstaVacio() == false)
                {
                    NombreA = dtObjCat.Rows[0]["NombreArchivo"].S();
                    lMsgArchivo.Text = "El nombre del archivo " + dtObjCat.Rows[0]["NombreArchivo"].S() + " existe en la Base de Datos.";
                    lMsgArchivo.ForeColor = System.Drawing.Color.Green;
                    //ViewState["dtPDF"] = dtObjCat;

                    //Session["ItinerarioV"] = dtObjCat.Rows[0]["ItinerarioV"];
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void CargaNombreArchivo()
        {
            try
            {
                object x = iIdSolicitud;
                if (eConsultaSolPDF != null)
                    eConsultaSolPDF(x, null);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadAerTramo(DataSet dtObjCat)
        {
            try
            {
                ViewState["Ori"] = dtObjCat.Tables[0];
                ViewState["Des"] = dtObjCat.Tables[1];
            }
            catch (Exception x)
            {
                throw x;
            }
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
        public void ObtieneValores()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void ObtieneContactos()
        {
            try
            {
                if (eLoadContactosCliente != null)
                    eLoadContactosCliente(null, EventArgs.Empty);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void ObtieneEquipos()
        {
            try
            {
                if (eLoadGrupoModelo != null)
                    eLoadGrupoModelo(null, EventArgs.Empty);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void ObtieneOrigen()
        {
            try
            {
                if (eLoadOrigen != null)
                    eLoadOrigen(null, EventArgs.Empty);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void ObtieneEstatus()
        {
            try
            {
                if (eLoadEstatus != null)
                    eLoadEstatus(null, EventArgs.Empty);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void ObtieneMotivo()
        {
            try
            {
                if (eLoadMotivos != null)
                    eLoadMotivos(null, EventArgs.Empty);
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
                    eLoadObjTrips(null, EventArgs.Empty);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void ObtieneHistorico()
        {
            try
            {
                if (eLoadHistorico != null)
                    eLoadHistorico(null, EventArgs.Empty);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void ObtieneOrigenVuelo()
        {
            try
            {
                if (eLoadOrigenVuelo != null)
                    eLoadOrigenVuelo(null, EventArgs.Empty);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void ObtieneOrigenVueloTodos()
        {
            try
            {
                if (eLoadOrigenVueloTodos != null)
                    eLoadOrigenVueloTodos(null, EventArgs.Empty);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadObjects(DataTable dtObject)
        {


        }
        public void LoadContactosCliente(DataTable dtObject)
        {
            try
            {
                cmbContacto.DataSource = dtObject;
                cmbContacto.TextField = "Nombre";
                cmbContacto.ValueField = "idContacto";
                cmbContacto.DataBind();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadGrupoModelo(DataTable dtObject)
        {
            try
            {
                cmbEquipo.DataSource = dtObject;
                cmbEquipo.TextField = "Descripcion";
                cmbEquipo.ValueField = "GrupoModeloId";
                cmbEquipo.DataBind();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadEstatus(DataTable dtObject)
        {
            try
            {
                cmbEstatus.DataSource = dtObject;
                cmbEstatus.TextField = "Descripcion";
                cmbEstatus.ValueField = "IdEstatus";
                cmbEstatus.DataBind();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadOrigen(DataTable dtObject)
        {
            try
            {
                cmbOrigen.DataSource = dtObject;
                cmbOrigen.TextField = "Descripcion";
                cmbOrigen.ValueField = "IdOrigen";
                cmbOrigen.DataBind();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadMotivos(DataTable dtObject)
        {
            try
            {
                cmbMotivo.DataSource = dtObject;
                cmbMotivo.TextField = "DescripcionMotivo";
                cmbMotivo.ValueField = "IdMotivo";
                cmbMotivo.DataBind();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadObjectsTrips(DataTable dtObject)
        {
            try
            {
                gvTrip.DataSource = null;
                gvTrip.DataSource = dtObject;
                gvTrip.DataBind();
                ViewState["oTrips"] = dtObject;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void CargaTrip()
        {
            try
            {
                if ((DataTable)ViewState["oTrips"] != null)
                {
                    gvTrip.DataSource = (DataTable)ViewState["oTrips"];
                    gvTrip.DataBind();
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadOrigenDestino(DataTable dtObject)
        {
            try
            {
                ViewState["OrigenDestino"] = dtObject;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadOrigDestFiltro(DataTable dtObject)
        {
            try
            {
                ViewState["OrigenDestino"] = dtObject;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadHistorico(DataTable dtObject)
        {
            try
            {
                gvHistorico.DataSource = null;
                gvHistorico.DataSource = dtObject;
                gvHistorico.DataBind();
                ViewState["oHistorico"] = dtObject;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void RecargaGvHistorico()
        {
            if ((DataTable)ViewState["oHistorico"] != null)
            {
                gvHistorico.DataSource = null;
                gvHistorico.DataSource = (DataTable)ViewState["oHistorico"];
                gvHistorico.DataBind();
            }
        }
        public void MostrarMensajeSolicitud(string sMensaje, string sCaption)
        {
            try
            {
                popup.HeaderText = sCaption;
                popup.ShowOnPageLoad = true;
                //mpeMensaje.ShowMessage(sMensaje, sCaption);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                popup.HeaderText = sCaption.Replace(".", "");
                if (Utils.GetParametrosClave("81").S().Equals("1"))
                    sMensaje = ViewState["VueloSimultaneo"].S().I() == 2 || ViewState["VueloSimultaneo"].S().I() == 3 ? sMensaje + "</br></br>Esta solicitud sobrepasa el número de vuelos simultáneos configurados en este contrato, favor de revisar." : sMensaje;

                lbl.Text = sMensaje;
                popup.ShowOnPageLoad = true;
                //mpeMensaje.ShowMessage(sMensaje, sCaption);
                gvTrip.JSProperties["cpText"] = sMensaje;
                gvTrip.JSProperties["cpShowPopup"] = true;
                gvTramos.JSProperties["cpText"] = sMensaje;
                gvTramos.JSProperties["cpShowPopup"] = true;
                gvHistorico.JSProperties["cpText"] = sMensaje;
                gvHistorico.JSProperties["cpShowPopup"] = true;
                gvPasajeros.JSProperties["cpText"] = sMensaje;
                gvPasajeros.JSProperties["cpShowPopup"] = true;
                gvComisariato.JSProperties["cpText"] = sMensaje;
                gvComisariato.JSProperties["cpShowPopup"] = true;

            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadOrigenVuelo(DataTable dtOBject)
        {
            try
            {
                ViewState["Origenes"] = dtOBject;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadOrigenVueloTodos(DataTable dtOBject)
        {
            try
            {
                ViewState["Origenes"] = dtOBject;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadDestinoVuelo(DataTable dtOBject)
        {
            try
            {
                ViewState["Destino"] = dtOBject;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void GetIdSolicitud(int iobjIdSolicitud)
        {
            try
            {
                Session["idSolicitud"] = iobjIdSolicitud;
                iIdSolicitud = Session["idSolicitud"].S().I();

                if (iobjIdSolicitud != 0 && iobjIdSolicitud != -1)
                {
                    lblSolicitud.Text = string.Empty;
                    lblSolicitud.Text += iobjIdSolicitud.S();
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void GetEquipoID(int IDEquipo)
        {
            try
            {
                this.Session["iIdEquipo"] = IDEquipo;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadTramoSol(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadTramoSol"] = dtObjCat;
                gvTramos.DataSource = dtObjCat;
                gvTramos.DataBind();

                if (dtObjCat == null || dtObjCat.Rows.Count <= 0)
                {
                    ASPxPageControl1.TabPages.FindByName("Historico").ClientEnabled = false;
                    ASPxPageControl1.TabPages.FindByName("Itinerario").ClientEnabled = false;
                }
                else
                {
                    ASPxPageControl1.TabPages.FindByName("Historico").ClientEnabled = true;
                    ASPxPageControl1.TabPages.FindByName("Itinerario").ClientEnabled = true;

                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void ObtieneTramoSol()
        {
            try
            {
                int IdSol = Session["idSolicitud"].S().I();

                if (eLoadTramoSol != null)
                    eLoadTramoSol(IdSol, EventArgs.Empty);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void ObtinePaxTramo(int IdTramo)
        {
            try
            {
                if (eObtienePaxTramo != null)
                    eObtienePaxTramo(IdTramo, EventArgs.Empty);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadPaxTramo(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadPaxTramo"] = dtObjCat;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void ObtinePaxTramo2(int IdTramo)
        {
            try
            {
                if (eLoadPaxTramo2 != null)
                    eLoadPaxTramo2(IdTramo, EventArgs.Empty);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadPaxTramo2(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadPaxTramo2"] = dtObjCat;
                gvPasajeros.DataSource = dtObjCat;
                gvPasajeros.DataBind();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadComisariatoTramo(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadComisariatoTramo"] = dtObjCat;
                gvComisariato.DataSource = dtObjCat;
                gvComisariato.DataBind();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void ObtineComisariatoTramo()
        {
            try
            {
                int IdTramo = Session["iIdTramo"].S().I();

                if (eConsultaComisariatoTramo != null)
                    eConsultaComisariatoTramo(IdTramo, EventArgs.Empty);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadProveedor(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadProveedor"] = dtObjCat;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadSolicitud(DataSet ds)
        {
            //gv1.datasource = ds.Tables[0];
            //gv1.databind();
        }
        public void LoadSolVueloByID(DataTable ds)
        {
            try
            {
                ViewState["LoadSolVueloByID"] = ds;
                cmbContacto.Value = ds.Rows[0]["IdContacto"].S();
                cmbMotivo.Value = ds.Rows[0]["IdMotivo"].S();
                cmbOrigen.Value = ds.Rows[0]["IdOrigen"].S();
                cmbEquipo.Value = ds.Rows[0]["IdTipoEquipo"].S();
                cmbEstatus.Value = ds.Rows[0]["Status"].S();
                mNotaSolVuelo.Text = ds.Rows[0]["Notas"].S();
                HabilitaInhabilitaControles(ds.Rows[0]["Status"].S() == "6" ? false : true);

                //mPrueba.Text = ds.Rows[0]["NotasVuelo"].S();
                txtMatricula.Text = ds.Rows[0]["Matricula"].S();

                if (iIdEquipo.I() != ds.Rows[0]["IdTipoEquipo"].S().I())
                {
                    lblIntercambio.Text = "Intercambio";
                    lblIntercambio.ForeColor = System.Drawing.Color.Red;
                }
                else
                    lblIntercambio.Text = "";

                //imArchivo.Visible = ds.Rows[0]["ItinerarioV"].S() != string.Empty ? true : false;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void CargaControles()
        {
            try
            {
                if (iIdSolicitud > 0 && iIdSolicitud != -1)
                {
                    if (eConsultasolVueloByID != null)
                        eConsultasolVueloByID(iIdSolicitud, EventArgs.Empty);
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void CargaMotivo()
        {
            try
            {
                DataTable dt = (DataTable)gvTrip.DataSource;
                if (iIdSolicitud == 0 || iIdSolicitud == -1 || (dt != null && dt.Rows.Count <= 0))
                {
                    cmbEstatus.DataSource = null;
                    cmbEstatus.Items.Clear();
                    cmbEstatus.Items.Add("NUEVO", 1);
                    cmbEstatus.SelectedIndex = 0;
                }
                else
                {

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cmbEstatus.DataSource = null;
                        cmbEstatus.Items.Clear();
                        cmbEstatus.Items.Add("TRABAJANDO", 3);
                        cmbEstatus.SelectedIndex = 0;
                    }
                }
                cmbEstatus.DataBind();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadCorreoAlta(DataSet dtObjCat)
        {
            try
            {
                ViewState["Correos"] = dtObjCat;

                //if (Session["hfEdicion"].S().Equals("1"))
                //{
                //    Mensaje = "<table style='border : 1px'><tr><td colspan='2'>Solicitudes de Vuelo</td></tr> " +
                //    "<tr><td>Nueva Solicitud de vuelo N° </td> <td> " + iIdSolicitud.S() + "</td> </tr>" +
                //    " <tr><td>Ejecutivo</td> " +
                //    "    <td>" + ((UserIdentity)Session["UserIdentity"]).sName + "</td></tr> <tr> <td>Cliente</td> " +
                //    "    <td>" + uno.Rows[0]["CodigoCliente"].S() + "/" + uno.Rows[0]["ClaveContrato"].S() + "</td> " +
                //    "</tr> <tr><td>Contacto</td> <td> " + uno.Rows[0]["Contacto"].S() + "</td></tr> " +
                //    " <tr><td>Modelo Aeronave</td> <td> " + uno.Rows[0]["Descripcion"].S() + "</td> </tr>" +
                //    "</table>";

                //    From = cero.Rows[0]["AreaEmail"].S();

                //    CC = cero.Rows[0]["AreaEmailCC"].S();
                //}
                //else if (Session["hfEdicion"].S().Equals("2"))
                //{
                //    Mensaje = "<table style='border : 1px'><tr><td colspan='2'>Solicitudes de Vuelo</td></tr>" +
                //        "<tr><td>Modificación a Solicitud de vuelo N° </td> <td> " + iIdSolicitud.S() + "</td> </tr>" +
                //        " <tr><td>Ejecutivo</td><td>" + ((UserIdentity)Session["UserIdentity"]).sName + "</td></tr> " +
                //        " <tr> <td>Cliente</td> <td>" + uno.Rows[0]["CodigoCliente"].S() + "/" + uno.Rows[0]["ClaveContrato"].S() + "</td> " +
                //    "</tr> <tr><td>Contacto</td> <td> " + uno.Rows[0]["Contacto"].S() + "</td> </tr>" +
                //    "</tr> <tr><td>Modelo Aeronave</td> <td> " + uno.Rows[0]["Descripcion"].S() + "</td> </tr>" +
                //    "</table>";
                //    From = cero.Rows[0]["AreaEmail"].S();
                //    CC = cero.Rows[0]["AreaEmailCC"].S();
                //}

                //Vuelo = "<br/><br/> <table><tr><td style='width: 45px'>Origen</td><td style='width: 45px'>Destino</td><td style='width: 45px'>Fecha</td><td style='width: 45px'>Hora</td><td style='width: 45px'>Pax</td></tr> ";
                //for (int x = 0; x < dos.Rows.Count; x++)
                //{
                //    Vuelo = Vuelo + "<tr style=\"border: medium groove #808080;\">" +
                //    "<td > " + dos.Rows[x]["AeropuertoO"].S() + "</td>" +
                //    "<td > " + dos.Rows[x]["AeropuertoD"].S() + "</td>" +
                //    "<td > " + dos.Rows[x]["Fechavuelo"].S() + "</td>" +
                //    "<td > " + dos.Rows[x]["HoraVuelo"].S() + "</td>" +
                //    "<td > " + dos.Rows[x]["NoPax"].S() + "</td>" +
                //    "</tr>";
                //}
                //Vuelo = Vuelo + " </table> ";
                //Mensaje = Mensaje + Vuelo;

                //string scorreo = Utils.ObtieneParametroPorClave("4");
                //string sPass = Utils.ObtieneParametroPorClave("5");
                //string sservidor = Utils.ObtieneParametroPorClave("6");
                //string spuerto = Utils.ObtieneParametroPorClave("7");

                //Utils.EnvioCorreo(sservidor, spuerto.S().I(), "SOLICITUD NUEVA.", From, Mensaje, "", scorreo, sPass, CC, "Atención a Clientes");
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void CorreobtnTerminar(DataSet DS)
        {
            try
            {
                if (DS != null)
                {
                    CorreoCliente(DS);
                    CorreoAsignacion(DS);
                }
            }
            catch (Exception x) { throw x; }
        }
        protected void CorreoCliente(DataSet DS)
        {
            try
            {
                DataTable uno = DS.Tables[1];

                string Mensaje = string.Empty;
                string From = string.Empty;
                string CC = string.Empty;

                if (chCorreo.Checked)
                {
                    Mensaje = " <br/> " +
                                " Estimado/a: " + uno.Rows[0]["Contacto"].S() + " " +
                                " <br/><br/> " +
                                " Por este conducto le informamos que su solicitud ha sido recibida, en este momento nuestro personal esta trabajando en ella para dar seguimiento en tiempo y forma. " +
                                " <br/><br/> " +
                                " Para Aerolíneas Ejecutivas su satisfacción es lo más importante, agradecemos su confianza. " +
                                " <br/><br/> " +
                                " Saludos cordiales. " +
                                " <br/><br/> " +
                                " Nota: Este aviso no es una confirmación. " +
                                " <br/><br/> " +
                                " Departamento de Atención a Clientes.";

                    From = uno.Rows[0]["ContacCorreo"].S();

                    string scorreo = Utils.ObtieneParametroPorClave("4");
                    string sPass = Utils.ObtieneParametroPorClave("5");
                    string sservidor = Utils.ObtieneParametroPorClave("6");
                    string spuerto = Utils.ObtieneParametroPorClave("7");

                    Utils.EnvioCorreo(sservidor, spuerto.S().I(), "SOLICITUD NUEVA.", From, Mensaje, "", scorreo, sPass, CC, "Atención a Clientes");
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void CorreoAsignacion(DataSet DataS)
        {
            DataSet ds = DataS;
            DataTable cero = ds.Tables[3];
            DataTable uno = ds.Tables[1];
            DataTable dos = ds.Tables[2];
            string Mensaje = string.Empty;
            string Vuelo = string.Empty;
            string From = string.Empty;
            string CC = string.Empty;

            if (Session["hfEdicion"].S().Equals("1") && txtMatricula.Text != string.Empty)
            {
                Mensaje = "<table style='border : 1px'><tr><td colspan='2'>Solicitudes de Vuelo</td></tr> " +
                "<tr><td>Nueva Solicitud de vuelo N° </td> <td> " + iIdSolicitud.S() + "</td> </tr>" +
                "<tr> <td>Contrato</td> " +
                "    <td>" + uno.Rows[0]["ClaveContrato"].S() + "</td></tr> " +
                " <tr><td>Modelo Aeronave</td> <td> " + uno.Rows[0]["Descripcion"].S() + "</td> </tr>" +
                " <tr><td>Matricula </td> <td> " + uno.Rows[0]["Matricula"].S() + "</td> </tr>" +
                "</table>";

                From = cero.Rows[0]["AreaEmail"].S();

                CC = cero.Rows[0]["AreaEmailCC"].S();

                Vuelo = "<br/><br/> <table><tr><td style='width: 45px'>Origen</td><td style='width: 45px'>Destino</td><td style='width: 45px'>Fecha</td><td style='width: 45px'>Hora</td><td style='width: 45px'>Pax</td></tr> ";
                for (int x = 0; x < dos.Rows.Count; x++)
                {
                    Vuelo = Vuelo + "<tr style=\"border: medium groove #808080;\">" +
                    "<td > " + dos.Rows[x]["AeropuertoO"].S() + "</td>" +
                    "<td > " + dos.Rows[x]["AeropuertoD"].S() + "</td>" +
                    "<td > " + dos.Rows[x]["Fechavuelo"].S() + "</td>" +
                    "<td > " + dos.Rows[x]["HoraVuelo"].S() + "</td>" +
                    "<td > " + dos.Rows[x]["NoPax"].S() + "</td>" +
                    "</tr>";
                }
                Vuelo = Vuelo + " </table> ";
                Mensaje = Mensaje + Vuelo;

                string scorreo = Utils.ObtieneParametroPorClave("4");
                string sPass = Utils.ObtieneParametroPorClave("5");
                string sservidor = Utils.ObtieneParametroPorClave("6");
                string spuerto = Utils.ObtieneParametroPorClave("7");

                Utils.EnvioCorreo(sservidor, spuerto.S().I(), "SOLICITUD NUEVA.", From, Mensaje, "", scorreo, sPass, CC, "Atención a Clientes");
            }
        }
        protected void CorreoVentas(DataSet DataS)
        {
            if (Utils.ObtieneParametroPorClave("61").Equals("1"))
            {
                DataSet ds = DataS;
                DataTable cero = ds.Tables[3];
                DataTable uno = ds.Tables[1];
                DataTable dos = ds.Tables[2];
                string Mensaje = string.Empty;
                string Vuelo = string.Empty;
                string From = string.Empty;
                string CC = string.Empty;

                Mensaje = "<table style='border : 1px'><tr><td colspan='2'>Solicitudes de Vuelo</td></tr> " +
                "<tr><td>Nueva Solicitud de vuelo N° </td> <td> " + iIdSolicitud.S() + "</td> </tr>" +
                "<tr> <td>Contrato</td> " +
                "    <td>" + uno.Rows[0]["ClaveContrato"].S() + "</td></tr> " +
                " <tr><td>Modelo Aeronave</td> <td> " + uno.Rows[0]["Descripcion"].S() + "</td> </tr>" +
                " <tr><td>Matricula </td> <td> " + uno.Rows[0]["Matricula"].S() + "</td> </tr>" +
                "</table>";

                From = uno.Rows[0]["CorreoVendedor"].S();

                // CC = cero.Rows[0]["AreaEmailCC"].S();

                Vuelo = "<br/><br/> <table><tr><td style='width: 45px'>Origen</td><td style='width: 45px'>Destino</td><td style='width: 45px'>Fecha</td><td style='width: 45px'>Hora</td><td style='width: 45px'>Pax</td></tr> ";
                for (int x = 0; x < dos.Rows.Count; x++)
                {
                    Vuelo = Vuelo + "<tr style=\"border: medium groove #808080;\">" +
                    "<td > " + dos.Rows[x]["AeropuertoO"].S() + "</td>" +
                    "<td > " + dos.Rows[x]["AeropuertoD"].S() + "</td>" +
                    "<td > " + dos.Rows[x]["Fechavuelo"].S() + "</td>" +
                    "<td > " + dos.Rows[x]["HoraVuelo"].S() + "</td>" +
                    "<td > " + dos.Rows[x]["NoPax"].S() + "</td>" +
                    "</tr>";
                }
                Vuelo = Vuelo + " </table> ";
                Mensaje = Mensaje + Vuelo;

                string scorreo = Utils.ObtieneParametroPorClave("4");
                string sPass = Utils.ObtieneParametroPorClave("5");
                string sservidor = Utils.ObtieneParametroPorClave("6");
                string spuerto = Utils.ObtieneParametroPorClave("7");

                Utils.EnvioCorreo(sservidor, spuerto.S().I(), "SOLICITUD NUEVA.", From, Mensaje, "", scorreo, sPass, CC, "Atención a Clientes");
            }
        }
        public void Validacion(int i)
        {
            try
            {
                Session["Validacion"] = i;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void ValidaFechaHora(int i)
        {
            try
            {
                Session["Validacion"] = i;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadConsultaDetalle(DataTable dtObjCat)
        {
            try
            {
                if (dtObjCat != null && dtObjCat.Rows.Count > 0)
                {
                    mmDetalle.Text = dtObjCat.Rows[0]["Nota"].S();
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void CargaDetalle(object x)
        {
            try
            {
                if (eConsultaDetalle != null)
                    eConsultaDetalle(x, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void HabilitaInhabilitaControles(bool ban)
        {
            try
            {
                cmbContacto.ReadOnly = !ban;
                cmbMotivo.ReadOnly = !ban;
                cmbOrigen.ReadOnly = !ban;
                cmbEquipo.ReadOnly = !ban;
                cmbEstatus.ReadOnly = !ban;
                btnGuardarSolicitud.Enabled = ban;
                btnNuevo.Enabled = ban;
                gvTrip.Enabled = ban;
                btnNuevoTramo.Enabled = ban;
                gvTramos.Enabled = ban;
                btnTerminar.Enabled = ban;
                ddlAutor.ReadOnly = !ban;
                mNota.ReadOnly = !ban;
                btnGuardarSeguimiento.Enabled = ban;
                gvHistorico.Enabled = ban;
                UploadControl.Enabled = ban;
                mPrueba.Enabled = ban;
                btnPrueba.Enabled = ban;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void ConsultaModContrato(DataTable DT)
        {
            try
            {
                if (DT != null && DT.Rows.Count > 0 && DT.Rows[0]["Resultado"].S() == "1")
                {
                    lblIntercambio.Text = "Intercambio";
                    lblIntercambio.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void ObtieneDetalle(DataTable Detalle)
        {
            try
            {
                mmDetalle.Text = string.Empty;
                mmDetalle.Text = Detalle.Rows[0]["NotasVuelo"].S();
            }
            catch (Exception x) { throw x; }
        }
        public void ValidaVueloSimultaneo(int iResultado)
        {
            ViewState["VueloSimultaneo"] = iResultado;
        }
        public void CargaPasajeros(DataTable DT)
        {
            ViewState["CargaPasajeros"] = DT;
        }
        public void ConsultaPDFSeguimiento(DataTable DT)
        {
            if (DT != null && DT.Rows.Count > 0)
            {

                //Byte[] drPDF = (Byte[])Session["ItinerarioV"];
                Byte[] drPDF = (Byte[])DT.Rows[0]["SegumientoPDF"];
                if (drPDF != null)
                {
                    MemoryStream ms = new MemoryStream(drPDF);
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + DT.Rows[0]["NombreArchivo"].S());
                    Response.ContentType = "application/octet-stream";
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //ms.WriteTo(Response.OutputStream);
                    Response.BinaryWrite(drPDF);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        public void CargaTripGuides(DataTable dt)
        {
            try
            {
                gvTripGuides.DataSource = dt;
                gvTripGuides.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RedireccionWizard(int iIndex)
        {
            //iIndicePestana = iIndex;
            if (iIndex > 0)
            {
                for (int i = iIndex; i > 0; i--)
                {
                    ASPxPageControl1.TabPages[i].Enabled = true;
                }
            }

            ASPxPageControl1.ActiveTabIndex = iIndex;
        }
        public void ConsultaContactoSolicitud(DataTable dtResultado)
        {
            txtPara.Text = dtResultado.Rows[0]["CorreoElectronico"].ToString().Trim();

            sNombreContacto = dtResultado.Rows[0]["Nombre"].ToString();
        }
        public void ConsultaVendedorSolicitud(DataTable dtResultado)
        {
            if (dtResultado.Rows.Count > 0)
            {
                txtConCopiaOculta.Text = dtResultado.Rows[0]["CorreoElectronico"].ToString();
            }
        }
        private void EnviarMail()
        {
            try
            {


                if (!string.IsNullOrEmpty(txtPara.Text))
                {

                    if (eBuscaDetallePierna != null)
                        eBuscaDetallePierna(null, null);

                    string sContactoPara = txtPara.Text;
                    string Asunto = "Tripguide";
                    string sContactosCopia = txtConCopia.Text;
                    string sContactosCopiaOculta = txtConCopiaOculta.Text;

                    string scorreo = Utils.ObtieneParametroPorClave("4");
                    string sPass = Utils.ObtieneParametroPorClave("5");
                    string sservidor = Utils.ObtieneParametroPorClave("6");
                    string spuerto = Utils.ObtieneParametroPorClave("7");

                    Stream ArchivoAdjunto = new MemoryStream(bPDF);

                    string Motivo = Utils.CuerpoCorreoHtmlTripGuide(sNombreContacto, iTrip, sICAOOrigen, sNombreAeropuertoOrigen, sICAODestino, sNombreAeropuertoDestino, DtFechaSalida,
                                                                    sNumeroPasajero, sAeronave, sPiloto, sPilotoTelefono, sCoPiloto, sCoPilotoTelefono, sObservaciones).ToString();

                    Utils.EnvioCorreo(sservidor, spuerto.S().I(), Asunto, sContactoPara, Motivo, ArchivoAdjunto, scorreo, sPass, sContactosCopia, sNombreArchivoPDF + ".pdf", "", sContactosCopiaOculta);

                    mpeMensaje.ShowMessage("Envio de correo exitoso.", "");
                }
                else
                {
                    mpeMensaje.ShowMessage("Ingrese un correo valido en el campo \"Para:\".", "");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string ParseaCorreo(string CadenaCorreos)
        {
            string Resultado = string.Empty;

            Resultado = CadenaCorreos;

            if (!string.IsNullOrEmpty(Resultado))
            {
                string CaracterFinal = Resultado.Substring(Resultado.Length - 1);

                if (CaracterFinal.Equals(";"))
                {
                    Resultado = Resultado.Substring(0, Resultado.Length - 1);
                }
            }

            return Resultado;
        }
        #endregion

        #region Variables y Atributos

        private const string sClase = "frmSolicitudes.aspx.cs";
        private const string sPagina = "frmSolicitudes.aspx";

        public object oCrud
        {
            get { return ViewState["CrudContacto"]; }
            set { ViewState["CrudContacto"] = value; }
        }
        public object oCrudTrip
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public object[] oArrFiltros
        {
            get { throw new NotImplementedException(); }
        }
        public object[] oFiltroContrato
        {
            get
            {
                return new object[] { 
                                        "@IdContrato", iIdContrato
                                    };
            }
        }
        public object[] oItinerario
        {
            get
            {
                return new object[] { 
                                        "@IdSolicitud", iIdSolicitud
                                    };
            }
        }
        public object[] oEliminaItinerario
        {
            get
            {
                return new object[] { 
                                        "@IdItinerario", iIditinerario
                                    };
            }
        }
        public object[] oGuardaSeguimiento
        {
            get
            {
                return new object[] { 
                                        "@IdSolicitud", iIdSolicitud,
                                        "@idAutor", bViabilidad == true ? 3 : 0,
                                        "@Nota", bViabilidad == true ? "La solicitud " + iIdSolicitud + "es viable." : Session["hfEdicion"].S().Equals("1") ? "Se generó la solicitud No. " + iIdSolicitud : "Se modifico la solicitud No. " + iIdSolicitud ,
                                        "@Status", 1,
                                        "@IP", "",
                                        "@Usuario", ((UserIdentity)Session["UserIdentity"]).sUsuario.S()
                                    };
            }
        }
        public object[] oConsultaDetalleItinerario
        {
            get
            {
                return new object[] { 
                                        "@IdItinerario", iIditinerario
                                    };
            }
        }
        public object[] oInsertaPasajero
        {
            get
            {
                return new object[] { 
                                        "@last_name",  txtApellidoP.Text,
                                        "@first_name",txtNombreP.Text,
                                        "@UsuarioCreacion", "",
                                        "@IP", ""
                                    };
            }
        }

        public string NombreA
        {
            get { return Session["NombreA"].S(); }
            set { Session["NombreA"] = value; }
        }
        public int iIditinerario
        {
            get { return ViewState["IdCliente"].S().I(); }
            set { ViewState["IdCliente"] = value; }
        }
        public int iIdCliente
        {
            get { return Session["IdCliente"].S().I(); }
            set { Session["IdCliente"] = value; }
        }
        public int iIdEquipo
        {
            get { return Session["iIdEquipo"].S().I(); }
            set { Session["iIdEquipo"] = value; }
        }
        public int iIdContrato
        {
            get { return Session["idContrato"].S().I(); }
            set { Session["idContrato"] = value; }
        }
        public int iIdSolicitud
        {
            get { return Session["idSolicitud"].S().I(); }
            set { Session["idSolicitud"] = value; }
        }
        public int iIdSeguimiento
        {
            get { return Session["iIdSeguimiento"].S().I(); }
            set { Session["iIdSeguimiento"] = value; }
        }
        public string sOrigen
        {
            get { return ViewState["sOrigen"].S(); }
            set { ViewState["sOrigen"] = value; }
        }
        public bool bViabilidad
        {
            get { return ViewState["bViabilidad"].S().B(); }
            set { ViewState["bViabilidad"] = value; }
        }
        public int iTrip
        {
            get { return ViewState["VSTrip"].S().I(); }
            set { ViewState["VSTrip"] = value; }
        }
        public int iPierna
        {
            get { return ViewState["VSiIdPierna"].S().I(); }
            set { ViewState["VSiIdPierna"] = value; }
        }
        public string sObservaciones
        {
            get { return ViewState["VSsObservaciones"].S(); }
            set { ViewState["VSsObservaciones"] = value; }
        }
        public string sNombreContacto
        {
            get { return ViewState["VSsNombreContacto"].S(); }
            set { ViewState["VSsNombreContacto"] = value; }
        }
        public string sICAOOrigen
        {
            get { return ViewState["VSsICAOOrigen"].S(); }
            set { ViewState["VSsICAOOrigen"] = value; }
        }
        public string sICAODestino
        {
            get { return ViewState["VSsICAODestino"].S(); }
            set { ViewState["VSsICAODestino"] = value; }
        }
        public string sNombreAeropuertoOrigen
        {
            get { return ViewState["VSsNombreAeropuertoOrigen"].S(); }
            set { ViewState["VSsNombreAeropuertoOrigen"] = value; }
        }
        public string sNombreAeropuertoDestino
        {
            get { return ViewState["VSsNombreAeropuertoDestino"].S(); }
            set { ViewState["VSsNombreAeropuertoDestino"] = value; }
        }
        public DateTime DtFechaSalida
        {
            get { return (DateTime)ViewState["VSDtFechaSalida"]; }
            set { ViewState["VSDtFechaSalida"] = value; }
        }
        public string sNumeroPasajero
        {
            get { return ViewState["VSsNumeroPasajero"].S(); }
            set { ViewState["VSsNumeroPasajero"] = value; }
        }
        public string sAeronave
        {
            get { return ViewState["VSsAeronave"].S(); }
            set { ViewState["VSsAeronave"] = value; }
        }
        public string sPiloto
        {
            get { return ViewState["VSsPiloto"].S(); }
            set { ViewState["VSsPiloto"] = value; }
        }
        public string sPilotoTelefono
        {
            get { return ViewState["VSsPilotoTelefono"].S(); }
            set { ViewState["VSsPilotoTelefono"] = value; }
        }
        public string sCoPiloto
        {
            get { return ViewState["VSsCoPiloto"].S(); }
            set { ViewState["VSsCoPiloto"] = value; }
        }
        public string sCoPilotoTelefono
        {
            get { return ViewState["VSsCoPilotoTelefono"].S(); }
            set { ViewState["VSsCoPilotoTelefono"] = value; }
        }

        public SolicitudVuelo oCatalogo
        {
            get
            {
                SolicitudVuelo oSolicitud = new SolicitudVuelo();

                oSolicitud.idictamen = bViabilidad == true ? 1 : 2;
                oSolicitud.iIdSolicitud = iIdSolicitud != null ? iIdSolicitud : 0;
                oSolicitud.iIdContrato = iIdContrato;
                oSolicitud.iIdContacto = cmbContacto.SelectedItem.Value.S().I();
                oSolicitud.iIdMotivo = cmbMotivo.SelectedItem.Value.S().I();
                oSolicitud.iIdOrigen = cmbOrigen.SelectedItem.Value.S().I();
                oSolicitud.iIdEquipo = cmbEquipo.SelectedItem.Value.S().I();
                oSolicitud.sNotasVuelo = "";
                oSolicitud.sItinerariov = "null";
                oSolicitud.sUsuarioCreacion = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                oSolicitud.sMatricula = txtMatricula.Text;
                oSolicitud.sNotaSolVuelo = mNotaSolVuelo.Text;

                return oSolicitud;
            }
            set
            {
                SolicitudVuelo oSolicitud = value;

                cmbContacto.SelectedItem.Value = oSolicitud.iIdContacto.S();
                cmbMotivo.SelectedItem.Value = oSolicitud.iIdMotivo.S();
                cmbOrigen.SelectedItem.Value = oSolicitud.iIdOrigen.S();
                cmbEquipo.SelectedItem.Value = oSolicitud.iIdEquipo.S();
            }
        }
        public SolicitudVuelo oCatalogoSeguimiento
        {
            get
            {
                SolicitudVuelo oHistorico = new SolicitudVuelo();

                oHistorico.iIdSolicitud = iIdSolicitud.S().I();
                oHistorico.iIdAutor = ddlAutor.SelectedItem.Value.S().I();
                oHistorico.sNotasVuelo = mNota.Text.S();
                oHistorico.iStatus = 1;
                oHistorico.sUsuarioCreacion = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();


                return oHistorico;
            }
        }
        public SolicitudVuelo oCatalogoItinerario
        {
            get
            {
                SolicitudVuelo oItinerario = new SolicitudVuelo();

                oItinerario.iIdSolicitud = iIdSolicitud.S().I();
                oItinerario.sItinerariov = UploadControl.UploadedFiles[0].FileName;
                oItinerario.iIdAutor = ddlprueba.SelectedItem.Value.S().I();
                oItinerario.sNotasVuelo = mPrueba.Value.S();
                oItinerario.iStatus = 1;
                oItinerario.sUsuarioCreacion = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();


                return oItinerario;
            }
        }
        public SolicitudVuelo oCat
        {
            get
            {
                SolicitudVuelo oItinerario = new SolicitudVuelo();
                oItinerario.sUsuarioCreacion = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();
                oItinerario.iIdSolicitud = iIdSolicitud.S().I();
                //oItinerario.sItinerariov = UploadControl.UploadedFiles[0].FileName;
                oItinerario.bARchivo = (byte[])Session["FileBytes"];
                oItinerario.sNotasVuelo = mPrueba.Value.S();
                oItinerario.iStatus = 1;
                oItinerario.sNombreArchivo = Session["Nombre"].S();
                Session["FileBytes"] = null;
                Session["Nombre"] = null;
                return oItinerario;
            }
        }

        public SolicitudVuelo oInsertaPDFSeguimiento
        {
            get
            {
                SolicitudVuelo oSolicitud = new SolicitudVuelo();

                oSolicitud.iIdSolicitud = iIdSolicitud != null ? iIdSolicitud : 0;
                oSolicitud.iIdSeguimiento = iIdSeguimiento;
                oSolicitud.bARchivo = (Byte[])Session["Bytes"];
                oSolicitud.sNombreArchivo = Session["NombreArchivo"].S();
                oSolicitud.sUsuarioCreacion = ((UserIdentity)Session["UserIdentity"]).sUsuario;

                Session["Bytes"] = null;
                Session["NombreArchivo"] = null;
                return oSolicitud;
            }
        }
        SolicitudVuelo_Presenter oPresenter;
        public ALE_MexJet.Objetos.Enumeraciones.TipoOperacion eCrud
        {
            get { return (ALE_MexJet.Objetos.Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }

        public string sNombreArchivoPDF
        {
            get { return ViewState["sNombreArchivoPDF"].S(); }
            set { ViewState["sNombreArchivoPDF"] = value; }
        }




        public byte[] bPDF
        {
            get { return (byte[])ViewState["bPDF"]; }
            set { ViewState["bPDF"] = value; }
        }
        public event EventHandler eLoadContactosCliente;
        public event EventHandler eLoadGrupoModelo;
        public event EventHandler eLoadOrigen;
        public event EventHandler eLoadEstatus;
        public event EventHandler eLoadMotivos;
        public event EventHandler eNewTrip;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eSaveTrip;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eLoadObjTrips;
        public event EventHandler eLoadOrigenVuelo;
        public event EventHandler eLoadDestinoVuelo;
        public event EventHandler eNewTramo;
        public event EventHandler eNewSeguimiento;
        public event EventHandler eLoadHistorico;
        public event EventHandler eNewItinerario;
        public event EventHandler eLoadOrigenVueloTodos;
        public event EventHandler eLoadOrigenDestino;
        public event EventHandler eLoadOrigDestFiltro;
        public event EventHandler eLoadTramoSol;
        public event EventHandler eEditaTramoSol;
        public event EventHandler eEliminaTramoSol;
        public event EventHandler eInsertaPaxTramo;
        public event EventHandler eObtienePaxTramo;
        public event EventHandler eEditaPaxTramo;
        public event EventHandler eEliminaPaxTramo;
        public event EventHandler eConsultaComisariatoTramo;
        public event EventHandler eInsertaComisariatoTramo;
        public event EventHandler eEditaComisariatoTramo;
        public event EventHandler eEliminaComisariatoTramo;
        public event EventHandler eEliminaTripSolicitud;
        public event EventHandler eConsultaProveedor;
        public event EventHandler eEliminaHistorico;
        public event EventHandler eEditarSolVuelo;
        public event EventHandler eConsultasolVueloByID;
        public event EventHandler eLoadCorreoAlta;
        public event EventHandler eValidaTrip;
        public event EventHandler eLoadPaxTramo2;
        public event EventHandler eValidaFechaHora;
        public event EventHandler eAerTramo;
        public event EventHandler eConsultaDetalle;
        public event EventHandler eConsultaSolPDF;
        public event EventHandler eConsultaModCon;
        public event EventHandler eConsultaItinerario;
        public event EventHandler eEliminaItinerario;
        public event EventHandler eGuardaSeguimientoHistorico;
        public event EventHandler eConsultaDetalleItinerario;
        public event EventHandler eValidaVuelosimultaneo;
        public event EventHandler eInsertaMonitorDespacho;
        public event EventHandler eBuscaPasajero;
        public event EventHandler eInsertaPasajero;
        public event EventHandler eViabilidad;
        public event EventHandler ePDFSeguimiento;
        public event EventHandler eConsultaPDFSeguimiento;
        public event EventHandler eConsultaTripGuides;
        public event EventHandler eDeleteTripGuide;
        public event EventHandler eConsultaContactoSolicitud;
        public event EventHandler eConsultaVendedorSolicitud;
        public event EventHandler eBuscaDetallePierna;

        #endregion







    }
}