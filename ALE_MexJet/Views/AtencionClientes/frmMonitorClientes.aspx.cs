using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using ALE_MexJet.ControlesUsuario;
using System.Data;
using NucleoBase.Core;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using DevExpress.Web.Data;
using System.ComponentModel;
using NucleoBase.Seguridad;
using System.Text;
using NucleoBase.Core;
using DevExpress.Web.Internal;
using System.Reflection;
using System.Text;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Views.AtencionClientes
{
    public partial class frmMonitorClientes : System.Web.UI.Page, IViewMonitorCliente
    {

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.MonitorCliente);
                LoadActions(DrPermisos);
                oPresenter = new MonitorCliente_Presenter(this, new DBMonitorCliente());
                gvMonitor.SettingsPager.ShowDisabledButtons = true;
                gvMonitor.SettingsPager.ShowNumericButtons = true;
                gvMonitor.SettingsPager.ShowSeparators = true;
                gvMonitor.SettingsPager.Summary.Visible = true;
                gvMonitor.SettingsPager.PageSizeItemSettings.Visible = true;
                gvMonitor.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvMonitor.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                if (!IsPostBack)
                {
                    ObtieneValores();

                }
                else
                {
                    if (!string.IsNullOrEmpty(ViewState["oDatos"].S()))
                    {
                        LoadObjects((DataTable)ViewState["oDatos"]);
                    }
                    if (!string.IsNullOrEmpty(ViewState["oDetalle"].S()))
                    {
                        LoadDetalleContrato((DataTable)ViewState["oDetalle"]);
                    }
                    if (!string.IsNullOrEmpty(ViewState["oCasosTramo"].S()))
                    {
                        LoadCasosTramo((DataTable)ViewState["oCasosTramo"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ObtieneValores();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscar_Click", "Aviso");
            }
        }
        protected void gvMonitor_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {

            //ASPxButton boton = (ASPxButton);
            //int id = boton.CommandName.S().I();
            //string codigoCliente = boton.CommandArgument.S();

            //AcPrincipal.SelectedIndex = 1;
            //iIdContrato = id;
            //sCodigoCliente = codigoCliente;


            //if (eSearchDetalle != null)
            //    eSearchDetalle(null, EventArgs.Empty);

            //if (eSearchDatosContrato != null)
            //    eSearchDatosContrato(null, EventArgs.Empty);

        }
        protected void gvDetalle_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Detalle")
                {
                    string IdSolicitud = e.CommandArgs.CommandArgument.S();
                    Response.Redirect("~/Views/AtencionClientes/frmSolicitudes.aspx?Id=" + IdSolicitud, false);
                }
                if (e.CommandArgs.CommandName.S() == "Eliminar")
                {
                    iIdSolicitud = e.CommandArgs.CommandArgument.S().I();

                    this.Session["TRIP"] = ((System.Data.DataRowView)(gvDetalle.GetRow(e.VisibleIndex.S().I()))).Row.ItemArray[3].S();

                    if (eGuardaSeguimientoHistorico != null)
                        eGuardaSeguimientoHistorico(null, null);

                    if (eSearchDetalle != null)
                        eSearchDetalle(null, EventArgs.Empty);
                    
                    if (eDeleteSolicitud != null)
                    {
                        //gvDetalle.DeleteRow(e.KeyValue.S().I());
                        eDeleteSolicitud(null, EventArgs.Empty);
                    }

                    if (Utils.IsSolicitudMobile(iIdSolicitud))
                        Utils.NotificaAppMobile(iIdSolicitud, "Cancelado");

                    if (eConsultaCorreo != null)
                        eConsultaCorreo(e.CommandArgs.CommandArgument.S().I(), null);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetalle_RowCommand", "Aviso");
            }
        }
        protected void gvMonitor_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
        }
        protected void btnDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                ADetalleContrato.Visible = true;
                ASPxButton boton = (ASPxButton)sender;
                int id = boton.CommandName.S().I();
                string codigoCliente = boton.CommandArgument.S();
                AcPrincipal.SelectedIndex = 1;
                iIdContrato = id;
                sCodigoCliente = codigoCliente;
                if (eSearchDetalle != null)
                    eSearchDetalle(null, EventArgs.Empty);
                if (eSearchDatosContrato != null)
                    eSearchDatosContrato(null, EventArgs.Empty);
                if (eSearchObservaciones != null)
                    eSearchObservaciones(null, EventArgs.Empty);
                ObtieneCasosTramo();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnDetalle_Click", "Aviso");
            }
        }
        protected void gvDetalle_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            try
            {
                lblClaveCliente.Text = sCodigoCliente.S();
                int x = 0;
                if (e.Expanded)
                {
                    x = e.VisibleIndex;
                    ViewState["IndexGridDetail"] = x;
                    object o = new object();
                    o = gvDetalle.GetRowValues(x, gvDetalle.KeyFieldName);
                    iIdSolicitud = o.S().I();
                    if (eSearchPiernas != null)
                        eSearchPiernas(null, EventArgs.Empty);
                }

                if (gvDetalle.SettingsDetail.AllowOnlyOneMasterRowExpanded)
                {
                    gvDetalle.DetailRows.CollapseAllRows();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetalle_DetailRowExpandedChanged", "Aviso");
            }
        }
        protected void btnNuevaSolicitud_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Views/AtencionClientes/frmSolicitudes.aspx",false);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevaSolicitud_Click", "Aviso");
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
        protected void btnNuevoCaso_Click(object sender, EventArgs e)
        {
            try
            {
                pnlMinutos.Visible = false;
                pnlMotivo.Visible = false;
                pnlArea.Visible = false;
                pnlOtorgado.Visible = false;
                pnlSolicitud.Visible = false;
                pnlDetalle.Visible = false;
                pnlAccionCorrectiva.Visible = false;
                ddlMotivo.SelectedIndex = 0;
                ddlTipoCaso.SelectedIndex = 0;
                ddlArea.SelectedIndex = 0;
                gvCasos.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                ppAlert.HeaderText = "Formulario de Creación";

                ppAlert.ShowOnPageLoad = true;
                if (eSearchCasos != null)
                    eSearchCasos(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevoCaso_Click", "Aviso");
            }
        }
        protected void ASPxGridView1_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvCasos.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ASPxGridView1_StartRowEditing", "Aviso");
            }
        }
        protected void gvCasos_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                if (e.Column.FieldName == "TipoCaso")
                {
                    if (eSearchCasos != null)
                        eSearchCasos(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["oCasos"];
                    cmb.ValueField = "IdCaso";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "Descripcion";
                    cmb.DataBindItems();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCasos_CellEditorInitialize", "Aviso");
            }
        }
        protected void ddlTipoCaso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlMinutos.Visible = false;
                pnlMotivo.Visible = false;
                pnlArea.Visible = false;
                pnlOtorgado.Visible = false;
                pnlSolicitud.Visible = false;
                pnlDetalle.Visible = false;
                pnlAccionCorrectiva.Visible = false;
                ddlMotivo.Value = null;
                ddlArea.Value = null;

                string valor = ddlTipoCaso.SelectedItem != null ? ddlTipoCaso.SelectedItem.Value.S() : "0";
                iIdCaso = valor.S().I();

                if (eSearchMotivo != null)
                    eSearchMotivo(null, EventArgs.Empty);
                if (eSearchAreas != null)
                    eSearchAreas(null, EventArgs.Empty);
                if (eSearchSolicitudEspecial != null)
                    eSearchSolicitudEspecial(null, EventArgs.Empty);

                switch (valor)
                {
                    case "1":
                        pnlMotivo.Visible = true;
                        pnlMinutos.Visible = true;
                        pnlDetalle.Visible = true;
                        pnlAccionCorrectiva.Visible = true;
                        break;
                    case "2":
                        pnlMotivo.Visible = true;
                        pnlArea.Visible = true;
                        pnlDetalle.Visible = true;
                        pnlAccionCorrectiva.Visible = true;
                        break;
                    case "3":
                        pnlMotivo.Visible = true;
                        pnlArea.Visible = true;
                        pnlDetalle.Visible = true;
                        pnlAccionCorrectiva.Visible = true;
                        break;
                    case "4":
                        pnlMotivo.Visible = true;
                        pnlOtorgado.Visible = true;
                        pnlDetalle.Visible = true;
                        pnlSolicitud.Visible = true;
                        break;

                }

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlTipoCaso_SelectedIndexChanged", "Aviso");
            }
        }
        protected void btnGuardarCaso_Click(object sender, EventArgs e)
        {
            try
            {
                if (eNewObj != null)
                    eNewObj(null, EventArgs.Empty);
                pnllblCaso.Text = "Caso: " + CasoID.S();
                CasoID = 0;
                LimpiaCasos();
                gvDetalle.JSProperties["cpShowPopup"] = false;
                //ScriptManager.RegisterClientScriptBlock(this, typeof(frmMonitorClientes), "function", "Hide();", true);   
                //ppAlert.ShowOnPageLoad = false;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardarCaso_Click", "Aviso");
                CasoID = 0;
            }
        }
        protected void btnNewCaso_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxButton boton = (ASPxButton)sender;
                int id = boton.CommandName.S().I();
                string codigoCliente = boton.CommandArgument.S();
                ViewState["idTramo"] = boton.CommandName.S().I();
                ViewState["sPierna"] = boton.CommandArgument.S();
                gvCasos.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                ppAlert.HeaderText = "Formulacrion de Creación";
                ppAlert.ShowOnPageLoad = true;
                if (eSearchCasos != null)
                    eSearchCasos(null, EventArgs.Empty);

                pnllblCaso.Text = "Caso:";
                pnllblCliente.Text = "Cliente: " + sCodigoCliente;
                pnllblContrato.Text = "Contrato: " + ViewState["sContrato"].S();
                pnllblTipoContrato.Text = "Tipo Contrato: " + ViewState["sTipoContrato"].S();
                pnllblTipoEquipo.Text = "Tipo de Equipo: " + ViewState["sTipoEquipo"].S();
                pnllblRuta.Text = "Ruta: " + ViewState["sPierna"].S();
                pnlMinutos.Visible = false;
                pnlMotivo.Visible = false;
                pnlArea.Visible = false;
                pnlOtorgado.Visible = false;
                pnlSolicitud.Visible = false;
                pnlDetalle.Visible = false;
                pnlAccionCorrectiva.Visible = false;
                ddlMotivo.Value = null;
                ddlTipoCaso.Value = null;
                ddlArea.Value = null;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNewCaso_Click", "Aviso");
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                gvDetalle.JSProperties["cpShowPopup"] = false;
                //ppAlert.ShowOnPageLoad = false;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnCancelar_Click", "Aviso");
            }
        }
        protected void ddlMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String valor = ddlMotivo.SelectedItem != null ? ddlMotivo.SelectedItem.Value.S() : "0";
                ViewState["idMotivo"] = valor;
                if (eSearchSolicitudEspecial != null)
                    eSearchSolicitudEspecial(null, EventArgs.Empty);

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlMotivo_SelectedIndexChanged", "Aviso");
            }
        }
        protected void btnContactos_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxButton boton = (ASPxButton)sender;
                int id = boton.CommandName.S().I();
                Session["IdCliente"] = id;
                popupContactos.ShowOnPageLoad = true;
                if (eSearchContactosCliente != null)
                    eSearchContactosCliente(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnContactos_Click", "Aviso");
            }
        }
        protected void btnSalirContactos_Click(object sender, EventArgs e)
        {
            try
            {
                popupContactos.ShowOnPageLoad = false;
                Response.Redirect("~/Views/AtencionClientes/frmMonitorClientes.aspx");
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSalirContactos_Click", "Aviso");
            }
        }
        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String valor = ddlArea.SelectedItem != null ? ddlArea.SelectedItem.Value.S() : "0";
                ViewState["idArea"] = valor;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlArea_SelectedIndexChanged", "Aviso");
            }

        }
        protected void ddlSolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String valor = ddlSolicitud.SelectedItem != null ? ddlSolicitud.SelectedItem.Value.S() : "0";
                ViewState["IdSolicitudEspecial"] = valor;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlSolicitud_SelectedIndexChanged", "Aviso");
            }
        }
        protected void gvDetalle_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                int iPos = 0;
                if (e.RowType == GridViewRowType.Data)
                {
                    for (iPos = 0; iPos < DrPermisos[0].ItemArray.Length; iPos++)
                    {

                        switch (iPos)
                        {
                            case 6: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                                {
                                    ASPxButton btn = gvDetalle.FindRowCellTemplateControl(e.VisibleIndex, null, "btn") as ASPxButton;
                                    btn.Enabled = true;
                                }
                                else
                                {
                                    ASPxButton btn = gvDetalle.FindRowCellTemplateControl(e.VisibleIndex, null, "btn") as ASPxButton;
                                    btn.Enabled = false;
                                } break;
                            case 7: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                                {
                                    ASPxButton btnEliminarSolicitud = gvDetalle.FindRowCellTemplateControl(e.VisibleIndex, null, "btnEliminarSolicitud") as ASPxButton;
                                    btnEliminarSolicitud.Enabled = true;
                                }
                                else
                                {
                                    ASPxButton btnEliminarSolicitud = gvDetalle.FindRowCellTemplateControl(e.VisibleIndex, null, "btnEliminarSolicitud") as ASPxButton;
                                    btnEliminarSolicitud.Enabled = false;
                                } break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetalle_HtmlRowCreated", "Aviso");
            }
        }
        protected void gvPiernas_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                lblClaveCliente.Text = sCodigoCliente.S();
                //int x = 0;

                ASPxGridView grid = sender as ASPxGridView;
                //x = e.VisibleIndex;
                //ViewState["IndexGridDetail"] = x;

                //object o = new object();
                //o = gvDetalle.GetRowValues(x, gvDetalle.KeyFieldName);
                iIdSolicitud = grid.GetMasterRowKeyValue().S().I();
                if (eSearchPiernas != null)
                    eSearchPiernas(null, EventArgs.Empty);

                grid.DataSource = (DataTable)Session["oPiernas"];
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPiernas_BeforePerformDataSelect", "Aviso");
            }
        }
        protected void gvCasos_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            CasoID = e.CommandArgs.CommandArgument.S().I();
            if (e.CommandArgs.CommandName == "ELIMINAR")
            {
                CasoID = e.CommandArgs.CommandArgument.S().I();

                if (eEliminaCaso != null)
                    eEliminaCaso(null, null);

                CasoID = 0;
            }
            else if (e.CommandArgs.CommandName == "ACTUALIZAR")
            {
                if (eConsultaCasoEd != null)
                    eConsultaCasoEd(null, null);
            }
            ObtieneCasosTramo();
        }
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            gvDetalle.JSProperties["cpShowPopup"] = false;
            //ppAlert.ShowOnPageLoad = false;
            LimpiaCasos();
        }
        protected void btOK_Click(object sender, EventArgs e)
        {
            gvDetalle.JSProperties["cpShowPopup"] = false;
        }

        #endregion

        #region METODOS
        public void CargaCasoID(int ID)
        {
            CasoID = ID;
        }
        public void ObtieneValores()
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadObjects(DataTable dtObject)
        {
            try
            {
                gvMonitor.DataSource = null;
                gvMonitor.DataSource = dtObject;
                gvMonitor.DataBind();
                ViewState["oDatos"] = dtObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadDetalleContrato(DataTable dtObjectDetalle)
        {
            try
            {
                gvDetalle.DataSource = null;
                gvDetalle.DataSource = dtObjectDetalle;
                gvDetalle.DataBind();
                ViewState["oDetalle"] = dtObjectDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadDatosContrato(DataTable dtObjectDetalle)
        {
            try
            {
                ViewState["oDatosContrato"] = dtObjectDetalle;
                MonitorCliente monitor = new MonitorCliente();
                monitor.iIdContrato = dtObjectDetalle.Rows[0][0].S().I();

                monitor.iIdCliente = dtObjectDetalle.Rows[0][1].S().I();
                Session["IdCliente"] = dtObjectDetalle.Rows[0][1].S();
                monitor.sCodigoCliente = dtObjectDetalle.Rows[0][2].S();
                monitor.sClaveContrato = dtObjectDetalle.Rows[0][3].S();
                monitor.sTipoContrato = dtObjectDetalle.Rows[0][4].S();
                monitor.sGrupoModelo = dtObjectDetalle.Rows[0][5].S();
                Session["Monitor"] = monitor;
                lblCotrato.Text = "Contrato: " + monitor.sClaveContrato.S();
                ViewState["sContrato"] = monitor.sClaveContrato.S();
                lblTipoContrato.Text = "Tipo de Contrato: " + monitor.sTipoContrato.S();
                ViewState["sTipoContrato"] = monitor.sTipoContrato.S();
                lblTipoEquipo.Text = "Tipo de Equipo: " + monitor.sGrupoModelo.S();
                ViewState["sTipoEquipo"] = monitor.sGrupoModelo.S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadDetallePiernasSolicitud(DataTable dtPiernas)
        {
            try
            {

                Session["oPiernas"] = dtPiernas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void CargagvPiernas()
        {
            try
            {
                DataTable dt = (DataTable)Session["oPiernas"];
                if (dt != null)
                {
                    DevExpress.Web.ASPxGridView gvPiernas = gvDetalle.FindDetailRowTemplateControl(ViewState["IndexGridDetail"].S().I(), "gvPiernas") as DevExpress.Web.ASPxGridView;
                    gvPiernas.DataSource = dt;
                    gvPiernas.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadObservaciones(DataTable dtObjCat)
        {
            try
            {
                lblClaveCliente.Text = "Clave del Cliente: " + sCodigoCliente;
                txtObservacion.Text = dtObjCat.Rows[0][2].S();
                txtNota.Text = dtObjCat.Rows[0][3].S();
                txtOtros.Text = dtObjCat.Rows[0][4].S();
                ViewState["oObservaciones"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadCasos(DataTable dtObjCat)
        {
            try
            {
                ViewState["oCasos"] = dtObjCat;
                ddlTipoCaso.DataSource = null;
                ddlTipoCaso.DataSource = dtObjCat;
                ddlTipoCaso.ValueField = "IdCaso";
                ddlTipoCaso.TextField = "Descripcion";
                ddlTipoCaso.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadContactosCliente(DataTable dtObjCat)
        {
            try
            {
                ViewState["oContactosCliente"] = dtObjCat;
                gvContactos.DataSource = dtObjCat;
                gvContactos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadMotivos(DataTable dtObjCat)
        {
            try
            {
                ViewState["oMotivos"] = dtObjCat;
                ddlMotivo.DataSource = null;
                ddlMotivo.DataSource = dtObjCat;
                ddlMotivo.ValueField = "IdMotivo";
                ddlMotivo.TextField = "Descripcion";
                ddlMotivo.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                popup.HeaderText = sCaption;
                lbl.Text = sMensaje;
                gvDetalle.JSProperties["cpText"] = sMensaje;
                gvDetalle.JSProperties["cpShowPopup"] = true;
                //ppAlert.ShowOnPageLoad = false;
                popup.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ObtieneCasosTramo()
        {
            try
            {
                if (eSearchCasosTramo != null)
                    eSearchCasosTramo(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadCasosTramo(DataTable dtObjCat)
        {
            try
            {
                gvCasos.DataSource = null;
                gvCasos.DataSource = dtObjCat;
                gvCasos.DataBind();
                ViewState["oCasosTramo"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadAreas(DataTable dtObjCat)
        {
            try
            {
                ViewState["oAreas"] = dtObjCat;
                ddlArea.DataSource = null;
                ddlArea.DataSource = dtObjCat;
                ddlArea.ValueField = "IdArea";
                ddlArea.TextField = "AreaDescripcion";
                ddlArea.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadSolicitudEspecial(DataTable dtObjCat)
        {
            try
            {
                ViewState["oSolicitudEspecial"] = dtObjCat;
                ddlSolicitud.DataSource = null;
                ddlSolicitud.DataSource = dtObjCat;
                ddlSolicitud.ValueField = "IdSolicitudEspecial";
                ddlSolicitud.TextField = "Descripcion";
                ddlSolicitud.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadCorreo(DataSet DS)
        {
            try
            {
                CorreoAsignacion(DS);
                DataTable DT = DS.Tables[1];
                string Mensaje = string.Empty;
                string From = string.Empty;
                string CC = string.Empty;

                if (DT != null && DT.Rows.Count > 0)
                {
                    Mensaje = " <br/> " +
                            " Estimado/a: " + DT.Rows[0]["Contacto"].S() + " " +
                            " <br/><br/> " +
                            " De acuerdo a su solicitud le informamos que su vuelo No. " + Session["TRIP"].S() + " ha sido cancelado, seguimos atentos a sus requerimientos. " +
                            " <br/><br/> " +
                            " Para Aerolíneas Ejecutivas su satisfacción es lo más importante, agradecemos su confianza. " +
                            " <br/><br/> " +
                            " Atentamente. " +
                            " <br/><br/> " +
                            " Departamento de Atención a Clientes. ";

                    From = DT.Rows[0]["ContacCorreo"].S();

                    string scorreo = Utils.ObtieneParametroPorClave("4");
                    string sPass = Utils.ObtieneParametroPorClave("5");
                    string sservidor = Utils.ObtieneParametroPorClave("6");
                    string spuerto = Utils.ObtieneParametroPorClave("7");

                    Utils.EnvioCorreo(sservidor, spuerto.S().I(), "Cancelación de Solicitud.", From, Mensaje, "", scorreo, sPass, CC, "Atención a Clientes");

                    Session["TRIP"] = null;
                }
            }
            catch (Exception x) { throw x; }
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

            if (Session["hfEdicion"].S().Equals("1") && uno.Rows[0]["Matricula"].S() != string.Empty)
            {
                Mensaje = "<table style='border : 1px'><tr><td colspan='2'>Solicitudes de Vuelo</td></tr> " +
                "<tr><td>Solicitud de vuelo N° </td> <td> " + iIdSolicitud.S() + "</td> </tr>" +
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

                Utils.EnvioCorreo(sservidor, spuerto.S().I(), "Cancelación de Solicitud.", From, Mensaje, "", scorreo, sPass, CC, "Atención a Clientes");
            }
        }
        public void LoadActions(DataRow[] DrActions)
        {
            try
            {
                int iPos = 0;
                if (DrActions.Length == 0)
                {
                    ASPxButton1.Enabled = false;
                    txtTextoBusqueda.Enabled = false;
                    ddlTipoBusqueda.Enabled = false;
                    btnNuevaSolicitud.Enabled = false;
                }
                else
                {
                    for (iPos = 0; iPos < DrActions[0].ItemArray.Length; iPos++)
                    {
                        switch (iPos)
                        {
                            case 4: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                                {
                                    ASPxButton1.Enabled = true;
                                    txtTextoBusqueda.Enabled = true;
                                    ddlTipoBusqueda.Enabled = true;
                                }
                                else
                                {
                                    ASPxButton1.Enabled = false;
                                    txtTextoBusqueda.Enabled = false;
                                    ddlTipoBusqueda.Enabled = false;
                                } break;
                            case 5: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                                {
                                    btnNuevaSolicitud.Enabled = true;
                                }
                                else
                                {
                                    btnNuevaSolicitud.Enabled = false;
                                } break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public void CargaDatosCaso(DataTable DT)
        {
            try
            {
                if (eSearchCasos != null)
                    eSearchCasos(null, EventArgs.Empty);

                ViewState["idTramo"] = DT.Rows[0]["IdTramo"].S().I();

                pnllblCliente.Text = "Cliente: " + sCodigoCliente;
                pnllblContrato.Text = "Contrato: " + ViewState["sContrato"].S();
                pnllblTipoContrato.Text = "Tipo Contrato: " + ViewState["sTipoContrato"].S();
                pnllblTipoEquipo.Text = "Tipo de Equipo: " + ViewState["sTipoEquipo"].S();
                pnllblRuta.Text = "Ruta: " + ViewState["sPierna"].S();
                pnllblCaso.Text = "Caso: " + DT.Rows[0]["IdCaso"].S();
                pnlMinutos.Visible = false;
                pnlMotivo.Visible = false;
                pnlArea.Visible = false;
                pnlOtorgado.Visible = false;
                pnlSolicitud.Visible = false;
                pnlDetalle.Visible = false;
                pnlAccionCorrectiva.Visible = false;
                ddlMotivo.Value = null;
                //ddlTipoCaso.Value = null;
                ddlArea.Value = null;
                ddlTipoCaso.Value = DT.Rows[0]["IdTipoCaso"].S();
                ddlTipoCaso.ValidationSettings.ErrorText = "";
                //ddlTipoCaso.SelectedItem.Value = DT.Rows[0]["IdTipoCaso"].S();
                ppAlert.ShowOnPageLoad = true;

                iIdCaso = DT.Rows[0]["IdTipoCaso"].S().I();

                if (eSearchMotivo != null)
                    eSearchMotivo(null, EventArgs.Empty);
                if (eSearchAreas != null)
                    eSearchAreas(null, EventArgs.Empty);
                if (eSearchSolicitudEspecial != null)
                    eSearchSolicitudEspecial(null, EventArgs.Empty);

                ddlMotivo.Value = DT.Rows[0]["IdMotivo"].S() != string.Empty ? DT.Rows[0]["IdMotivo"].S() : "-1";
                ddlArea.Value = DT.Rows[0]["IdArea"].S() != string.Empty ? DT.Rows[0]["IdArea"].S() : "-1";
                ddlSolicitud.Value= DT.Rows[0]["IdSolicitudEspecial"].S() != string.Empty ? DT.Rows[0]["IdSolicitudEspecial"].S() : "-1";
                spnMinutos.Value = DT.Rows[0]["Minutos"].S() != string.Empty ? DT.Rows[0]["Minutos"].S().I() : 0;

                if (DT.Rows[0]["Otorgado"].S().B())
                {
                    rblOtorgado.SelectedIndex = 1;
                    //rbtOtorgadoSi.Checked = true;
                    //rbtOtorgadoNo.Checked = false;
                }
                else
                {
                    rblOtorgado.SelectedIndex = 0;
                    //rbtOtorgadoNo.Checked = true;
                    //rbtOtorgadoSi.Checked = false;
                }
                mAccionCorrectiva.Text = DT.Rows[0]["AccionCorrectiva"].S();
                mDetalle.Text = DT.Rows[0]["Detalle"].S();


                switch (DT.Rows[0]["IdTipoCaso"].S())
                {
                    case "1":
                        pnlMotivo.Visible = true;
                        pnlMinutos.Visible = true;
                        pnlDetalle.Visible = true;
                        pnlAccionCorrectiva.Visible = true;
                        break;
                    case "2":
                        pnlMotivo.Visible = true;
                        pnlArea.Visible = true;
                        pnlDetalle.Visible = true;
                        pnlAccionCorrectiva.Visible = true;
                        break;
                    case "3":
                        pnlMotivo.Visible = true;
                        pnlArea.Visible = true;
                        pnlDetalle.Visible = true;
                        pnlAccionCorrectiva.Visible = true;
                        break;
                    case "4":
                        pnlMotivo.Visible = true;
                        pnlOtorgado.Visible = true;
                        pnlDetalle.Visible = true;
                        pnlSolicitud.Visible = true;
                        break;

                }
            }
            catch (Exception x) { throw x; }
        }
        protected void LimpiaCasos()
        {

            //ddlMotivo.ValidationSettings.RequiredField.ErrorText = string.Empty;
            ddlMotivo.SelectedIndex = -1;
            //ddlArea.ValidationSettings.RequiredField.ErrorText = string.Empty;
            ddlArea.SelectedIndex = -1;
            ddlSolicitud.SelectedIndex = -1;
            spnMinutos.Value = 0;
            rblOtorgado.SelectedItem = null;
            //rbtOtorgadoNo.Checked = false;
            //rbtOtorgadoSi.Checked = false;
            mAccionCorrectiva.Text = string.Empty;
            mDetalle.Text = string.Empty;
        }
        #endregion

        #region VARIABLES Y PROPIEDADES

        private const string sClase = "frmMonitosClientes.aspx.cs";
        private const string sPagina = "frmMonitosClientes.aspx";

        MonitorCliente_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchDetalle;
        public event EventHandler eUpdateObservacion;
        public event EventHandler eSearchPiernas;
        public event EventHandler eSearchDatosContrato;
        public event EventHandler eSearchCasos;
        public event EventHandler eSearchMotivo;
        public event EventHandler eSearchObservaciones;
        public event EventHandler eSearchCasosTramo;
        public event EventHandler eSearchContactosCliente;
        public event EventHandler eDeleteSolicitud;
        public event EventHandler eSearchAreas;
        public event EventHandler eSearchSolicitudEspecial;
        public event EventHandler eConsultaCorreo;
        public event EventHandler eEliminaCaso;
        public event EventHandler eConsultaCasoEd;
        public event EventHandler eGuardaSeguimientoHistorico;

        UserIdentity oUsuario = new UserIdentity();
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }
        public object oCrud
        {
            get { return ViewState["CrudContacto"]; }
            set { ViewState["CrudContacto"] = value; }
        }
        public int iIdContrato
        {
            get { return ViewState["IdContrato"].S().I(); }
            set { ViewState["IdContrato"] = value; }
        }
        public string sCodigoCliente
        {
            get { return ViewState["CodigoCliente"].S(); }
            set { ViewState["CodigoCliente"] = value; }
        }
        public string sContrato
        {
            get { return ViewState["sContrato"].S(); }
            set { ViewState["sContrato"] = value; }
        }
        public int iIdSolicitud
        {
            get { return ViewState["IdSolicitud"].S().I(); }
            set { ViewState["IdSolicitud"] = value; }
        }
        public int iIdCliente { get { return Session["IdCliente"].S().I(); } }
        public ALE_MexJet.Objetos.Enumeraciones.TipoOperacion eCrud
        {
            get { return (ALE_MexJet.Objetos.Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }
        public object[] oArrEliminaCaso
        {
            get
            {
                return new object[]
            {
                "@IdCaso",CasoID ,
                "@Usuario" , ((UserIdentity)Session["UserIdentity"]).sUsuario.S(),
                "@IP", ""
            };
            }
        }
        public object[] oArrConsultaCasoEd
        {
            get
            {
                return new object[]
            {
                "@Id",CasoID ,
            };
            }
        }
        public object[] oArrFiltros
        {
            get
            {


                string sClaveCliente = string.Empty;
                string sClaveContrato = string.Empty;

                switch (ddlTipoBusqueda.SelectedItem.Value.S())
                {
                    case "1":
                        sClaveCliente = txtTextoBusqueda.Text;
                        iIdContrato = 0;
                        break;
                    case "2":
                        iIdContrato = 0;
                        sClaveContrato = txtTextoBusqueda.Text;
                        break;

                }

                return new object[]{
                                        "@CodigoCliente", "%" + sClaveCliente + "%",
                                        "@idContrato", 0,
                                        "@ClaveContrato", "%" + sClaveContrato + "%"
                                    };
            }

        }
        public Casos oCatalogoCasos
        {
            get
            {
                Casos oCasos = new Casos();
                oCasos.iIdCaso = CasoID.S().I();
                oCasos.iIdTramo = ViewState["idTramo"].S().I();
                oCasos.iIdTipoCaso = ViewState["idTipoCaso"].S().I();
                oCasos.iIdMotivo = ViewState["idMotivo"].S().I();
                oCasos.iMinutos = spnMinutos.Value.S().I();
                oCasos.iIdArea = ViewState["idArea"].S().I();
                oCasos.iIdSolicitud = ViewState["IdSolicitudEspecial"].S().I();

                if (mDetalle.Text.Length > 0)
                    oCasos.sDetalle = mDetalle.Text;
                else
                    oCasos.sDetalle = "";
                if (mAccionCorrectiva.Text.Length > 0)
                    oCasos.sAccionCorrectiva = mAccionCorrectiva.Text;
                else
                    oCasos.sAccionCorrectiva = "";

                if (rblOtorgado.SelectedItem != null)
                {
                    if (rblOtorgado.SelectedItem.Index == 0)
                        oCasos.bOtorgado = false;
                    else if (rblOtorgado.SelectedItem.Index == 1)
                        oCasos.bOtorgado = true;
                    
                }
                else
                    oCasos.bOtorgado = false;

                oCasos.sUsuarioCreacion = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();

                return oCasos;
            }
        }
        public int iIdCaso
        {

            get
            {
                return ViewState["idTipoCaso"].S().I();
            }
            set
            {
                ViewState["idTipoCaso"] = value;
            }
        }
        public int CasoID
        {
            get { return ViewState["CasoID"].S().I(); }
            set { ViewState["CasoID"] = value; }
        }

        public int iIdMotivo
        {

            get
            {
                return ViewState["idMotivo"].S().I();
            }
            set
            {
                ViewState["idMotivo"] = value;
            }
        }

        public object[] oGuardaSeguimiento
        {
            get
            {
                return new object[] { 
                                        "@IdSolicitud", iIdSolicitud,
                                        "@idAutor", 0,
                                        "@Nota", "Se cancelo la solicitud No. " + iIdSolicitud,
                                        "@Status", 1,
                                        "@IP", "",
                                        "@Usuario", ((UserIdentity)Session["UserIdentity"]).sUsuario.S()
                                    };
            }
        }

        #endregion

    }
}