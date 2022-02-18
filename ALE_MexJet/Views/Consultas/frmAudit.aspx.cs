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

namespace ALE_MexJet.Views.Consultas
{
    public partial class frmAudit: System.Web.UI.Page, IViewAudit
    {

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
                oPresenter = new Audit_Presenter(this, new DBAudit());
                SettingsGridviews();
                ObtieneValores();                

                if (eGetSearchIndicadorOperaciones != null)
                    eGetSearchIndicadorOperaciones(sender, EventArgs.Empty);

                if (eGetSearchModulos != null)
                    eGetSearchModulos(sender, EventArgs.Empty);

                if (IsPostBack)
                {
                    if (!string.IsNullOrEmpty(ViewState["oUsuario"].S()))
                    {
                        LoadUsuarioAudit((DataSet)Session["oUsuario"]);
                    }
                }

                llenarGridsPageLoad();                                                
                
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }
       
        protected void btnBusqueda_Click(object sender, System.EventArgs e)
        {
           try
            {
                if (eSearchObj != null)
                    eSearchObj(null, null);
             
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso");
            }
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportar_Click", "Aviso");
            }
        }
        protected void gvUsuarioAudit_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    count = 0;
                }
                else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    if (!list.Contains(Convert.ToString(e.FieldValue)))
                    {
                        count++;
                        list.Add(Convert.ToString(e.FieldValue));
                    }
                }
                else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                {
                    e.TotalValue = count;
                    list.Clear();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvUsuarioAudit_CustomSummaryCalculate", "Aviso");
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdatePanel1_Unload", "Aviso");
            }
        }
        protected void btnActividadUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (eGetSearchUsuario != null)
                    eGetSearchUsuario(sender, EventArgs.Empty);

                cmbModulo.SelectedItem = null;
                cmbUsuariosActividad.SelectedItem = null;
                cmbAccion.SelectedItem = null;
                deFechaCreacion.Value = null;
                gvActividadUsuario.DataSource = null;
                gvActividadUsuario.DataBind();

                popupActividadUsuario.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnActividadUsuario_Click", "Aviso");
            }
        }
        protected void btnBuscarUsuariosActividad_Click(object sender, EventArgs e)
        {
            try
            {
                gvActividadUsuario.DataSource = null;
                gvActividadUsuario.DataBind();

                if (eGetSearchActividadUsuario != null)
                    eGetSearchActividadUsuario(sender, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscarUsuariosActividad_Click", "Aviso");
            }
        }

        protected void btnExportarRepContrato_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterReporteadorContrato.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarRepContrato_Click", "Aviso");
            }
        }

        protected void btnExportarRepRemisiones_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterReporteadorRemision.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarRepRemisiones_Click", "Aviso");
            }
        }

        protected void btnExportarRepSolicitudVuelo_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterReporteadorSolicitudVuelo.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarRepSolicitudVuelo_Click", "Aviso");
            }
        }

        protected void btnExportarRepBitacoras_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterReporteadorBitacoras.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarRepBitacoras_Click", "Aviso");
            }
        }

        protected void btnExportarRepPrefactura_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterReporteadorPrefactura.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarRepPrefactura_Click", "Aviso");
            }
        }

        protected void btnExportarRepComisariato_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterReporteadorComisariato.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarRepComisariato_Click", "Aviso");
            }
        }

        protected void btnBuscarReporteador_Click(object sender, EventArgs e)
        {
            try
            {
                if (rblReporteador.SelectedItem.Value.ToString() == "1")
                {
                    gvReporteadorContratos.Visible = true;
                    gvReporteadorBitacoras.Visible = false;
                    gvReporteadorComisariato.Visible = false;
                    gvReporteadorPrefactura.Visible = false;
                    gvReporteadorRemisiones.Visible = false;
                    gvReporteadorSolicitudVuelo.Visible = false;

                    btnExportarRepContrato.Visible = true;
                    btnExportarRepRemisiones.Visible = false;
                    btnExportarRepSolicitudVuelo.Visible = false;
                    btnExportarRepBitacoras.Visible = false;
                    btnExportarRepPrefactura.Visible = false;
                    btnExportarRepComisariato.Visible = false;

                    btnExportarRepContrato1.Visible = true;
                    btnExportarRepRemisiones1.Visible = false;
                    btnExportarRepSolicitudVuelo1.Visible = false;
                    btnExportarRepBitacoras1.Visible = false;
                    btnExportarRepPrefactura1.Visible = false;
                    btnExportarRepComisariato1.Visible = false;

                    if (eGetSearchReporteadorContrato != null)
                        eGetSearchReporteadorContrato(sender, EventArgs.Empty);
                }
                else if (rblReporteador.SelectedItem.Value.ToString() == "2")
                {
                    gvReporteadorContratos.Visible = false;
                    gvReporteadorBitacoras.Visible = false;
                    gvReporteadorComisariato.Visible = false;
                    gvReporteadorPrefactura.Visible = false;
                    gvReporteadorRemisiones.Visible = true;
                    gvReporteadorSolicitudVuelo.Visible = false;

                    btnExportarRepContrato.Visible = false;
                    btnExportarRepRemisiones.Visible = true;
                    btnExportarRepSolicitudVuelo.Visible = false;
                    btnExportarRepBitacoras.Visible = false;
                    btnExportarRepPrefactura.Visible = false;
                    btnExportarRepComisariato.Visible = false;

                    btnExportarRepContrato1.Visible = false;
                    btnExportarRepRemisiones1.Visible = true;
                    btnExportarRepSolicitudVuelo1.Visible = false;
                    btnExportarRepBitacoras1.Visible = false;
                    btnExportarRepPrefactura1.Visible = false;
                    btnExportarRepComisariato1.Visible = false;

                    if (eGetSearchReporteadorRemisiones != null)
                        eGetSearchReporteadorRemisiones(sender, EventArgs.Empty);
                }
                else if (rblReporteador.SelectedItem.Value.ToString() == "3")
                {
                    gvReporteadorContratos.Visible = false;
                    gvReporteadorBitacoras.Visible = false;
                    gvReporteadorComisariato.Visible = false;
                    gvReporteadorPrefactura.Visible = false;
                    gvReporteadorRemisiones.Visible = false;
                    gvReporteadorSolicitudVuelo.Visible = true;

                    btnExportarRepContrato.Visible = false;
                    btnExportarRepRemisiones.Visible = false;
                    btnExportarRepSolicitudVuelo.Visible = true;
                    btnExportarRepBitacoras.Visible = false;
                    btnExportarRepPrefactura.Visible = false;
                    btnExportarRepComisariato.Visible = false;

                    btnExportarRepContrato1.Visible = false;
                    btnExportarRepRemisiones1.Visible = false;
                    btnExportarRepSolicitudVuelo1.Visible = true;
                    btnExportarRepBitacoras1.Visible = false;
                    btnExportarRepPrefactura1.Visible = false;
                    btnExportarRepComisariato1.Visible = false;

                    if (eGetSearchReporteadorSolicitudVuelo != null)
                        eGetSearchReporteadorSolicitudVuelo(sender, EventArgs.Empty);
                }
                else if (rblReporteador.SelectedItem.Value.ToString() == "4")
                {
                    gvReporteadorContratos.Visible = false;
                    gvReporteadorBitacoras.Visible = true;
                    gvReporteadorComisariato.Visible = false;
                    gvReporteadorPrefactura.Visible = false;
                    gvReporteadorRemisiones.Visible = false;
                    gvReporteadorSolicitudVuelo.Visible = false;

                    btnExportarRepContrato.Visible = false;
                    btnExportarRepRemisiones.Visible = false;
                    btnExportarRepSolicitudVuelo.Visible = false;
                    btnExportarRepBitacoras.Visible = true;
                    btnExportarRepPrefactura.Visible = false;
                    btnExportarRepComisariato.Visible = false;

                    btnExportarRepContrato1.Visible = false;
                    btnExportarRepRemisiones1.Visible = false;
                    btnExportarRepSolicitudVuelo1.Visible = false;
                    btnExportarRepBitacoras1.Visible = true;
                    btnExportarRepPrefactura1.Visible = false;
                    btnExportarRepComisariato1.Visible = false;

                    if (eGetSearchReporteadorBitacora != null)
                        eGetSearchReporteadorBitacora(sender, EventArgs.Empty);
                }
                if (rblReporteador.SelectedItem.Value.ToString() == "5")
                {
                    gvReporteadorContratos.Visible = false;
                    gvReporteadorBitacoras.Visible = false;
                    gvReporteadorComisariato.Visible = false;
                    gvReporteadorPrefactura.Visible = true;
                    gvReporteadorRemisiones.Visible = false;
                    gvReporteadorSolicitudVuelo.Visible = false;

                    btnExportarRepContrato.Visible = false;
                    btnExportarRepRemisiones.Visible = false;
                    btnExportarRepSolicitudVuelo.Visible = false;
                    btnExportarRepBitacoras.Visible = false;
                    btnExportarRepPrefactura.Visible = true;
                    btnExportarRepComisariato.Visible = false;

                    btnExportarRepContrato1.Visible = false;
                    btnExportarRepRemisiones1.Visible = false;
                    btnExportarRepSolicitudVuelo1.Visible = false;
                    btnExportarRepBitacoras1.Visible = false;
                    btnExportarRepPrefactura1.Visible = true;
                    btnExportarRepComisariato1.Visible = false;

                    if (eGetSearchReporteadorPrefactura != null)
                        eGetSearchReporteadorPrefactura(sender, EventArgs.Empty);
                }
                if (rblReporteador.SelectedItem.Value.ToString() == "6")
                {
                    gvReporteadorContratos.Visible = false;
                    gvReporteadorBitacoras.Visible = false;
                    gvReporteadorComisariato.Visible = true;
                    gvReporteadorPrefactura.Visible = false;
                    gvReporteadorRemisiones.Visible = false;
                    gvReporteadorSolicitudVuelo.Visible = false;

                    btnExportarRepContrato.Visible = false;
                    btnExportarRepRemisiones.Visible = false;
                    btnExportarRepSolicitudVuelo.Visible = false;
                    btnExportarRepBitacoras.Visible = false;
                    btnExportarRepPrefactura.Visible = false;
                    btnExportarRepComisariato.Visible = true;

                    btnExportarRepContrato1.Visible = false;
                    btnExportarRepRemisiones1.Visible = false;
                    btnExportarRepSolicitudVuelo1.Visible = false;
                    btnExportarRepBitacoras1.Visible = false;
                    btnExportarRepPrefactura1.Visible = false;
                    btnExportarRepComisariato1.Visible = true;

                    if (eGetSearchReporteadorComisariato != null)
                        eGetSearchReporteadorComisariato(sender, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "rblReporteador_SelectedIndexChanged", "Aviso");
            }
        }

        protected void btnExportExcelUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterActividadUsuario.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportExcelUsuario_Click", "Aviso");
            }
        }

        protected void btnExportarVueloSinBitacora_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterVueloSinBitacora.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarVueloSinBitacora_Click", "Aviso");
            }
        }

        protected void btnExportarVencimientoContratos_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterVencimientoContratos.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarVencimientoContratos_Click", "Aviso");
            }
        }

        protected void btnExportarBitatorasSinRemisionar_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterBitatorasSinRemisionar.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarBitatorasSinRemisionar_Click", "Aviso");
            }
        }

        protected void btnExportarRemisionesSinPrefacturar_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterRemisionesSinPrefacturar.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarRemisionesSinPrefacturar_Click", "Aviso");
            }
        }

        protected void btnExportarPrefacturasSinFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterPrefacturasSinFacturar.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarPrefacturasSinFacturar_Click", "Aviso");
            }
        }

        protected void btnExportarRolUsuarioRolAudit_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterRolUsuarioRolAudit.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarRolUsuarioRolAudit_Click", "Aviso");
            }
        }
       
        protected void gvIndicadores_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Detalle")
                {
                    if (e.CommandArgs.CommandArgument.S().I() == 1)
                    {                        
                        if (eGetSearchIndicadoresFinanzasVuelosSinBitacora != null)
                            eGetSearchIndicadoresFinanzasVuelosSinBitacora(sender, EventArgs.Empty);

                        popupVuelosSinBitacora.ShowOnPageLoad = true;                        
                    }
                    else if (e.CommandArgs.CommandArgument.S().I() == 2)
                    {                        
                        if (eGetSearchIndicadoresFinanzasVencimientoDeContratos != null)
                            eGetSearchIndicadoresFinanzasVencimientoDeContratos(sender, EventArgs.Empty);

                        popupVencimientoContratos.ShowOnPageLoad = true;                        
                    }
                    else if (e.CommandArgs.CommandArgument.S().I() == 3)
                    {                        
                        if (eGetSearchIndicadoresFinanzasBitacorasSinRemisionar != null)
                            eGetSearchIndicadoresFinanzasBitacorasSinRemisionar(sender, EventArgs.Empty);

                        popupBitacorasSinRemisionar.ShowOnPageLoad = true;                        
                    }
                    else if (e.CommandArgs.CommandArgument.S().I() == 4)
                    {                        
                        if (eGetSearchIndicadoresFinanzasRemisionesSinPrefacturar != null)
                            eGetSearchIndicadoresFinanzasRemisionesSinPrefacturar(sender, EventArgs.Empty);

                        popupRemisionesSinPrefacturar.ShowOnPageLoad = true;                        
                    }
                    else if (e.CommandArgs.CommandArgument.S().I() == 5)
                    {                        
                        if (eGetSearchIndicadoresFinanzasPrefacturasSinFacturar != null)
                            eGetSearchIndicadoresFinanzasPrefacturasSinFacturar(sender, EventArgs.Empty);

                        popupPrefacturasSinFacturar.ShowOnPageLoad = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvIndicadores_RowCommand", "Aviso");
            }
        }

        protected void btnBuscarIndicadorFinanza_Click(object sender, EventArgs e)
        {
            try
            {
                if (eGetSearchIndicadorFinanzas != null)
                    eGetSearchIndicadorFinanzas(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscarIndicadorFinanza_Click", "Aviso");
            }
        }

        protected void btnExportarEstatusContratosDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterEstatusContratosDetalle.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarEstatusContratosDetalle_Click", "Aviso");
            }
        }
        protected void gvEstatusContratos_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Detalle")
                {
                    sIdTipoPaqueteEstatusContrato = e.CommandArgs.CommandArgument.S().I();

                    if (eGetSearchEstatusContratoDetalle != null)
                        eGetSearchEstatusContratoDetalle(sender, EventArgs.Empty);

                    popupEstatusContratosDetalle.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvEstatusContratos_RowCommand", "Aviso");
            }
        }
        protected void gvIntercambio_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Detalle")
                {
                    sIdContratoDetalleOperaciones = e.CommandArgs.CommandArgument.S().I();

                    if (eGetSearchIndicadoresOperacionIntercambios != null)
                        eGetSearchIndicadoresOperacionIntercambios(sender, EventArgs.Empty);

                    popupOperacionesIntercambios.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvIntercambio_RowCommand", "Aviso");
            }
        }

        protected void gvRentasAeronaves_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Detalle")
                {
                    sIdContratoDetalleOperaciones = e.CommandArgs.CommandArgument.S().I();

                    if (eGetSearchIndicadoresOperacionRentaAeronaves != null)
                        eGetSearchIndicadoresOperacionRentaAeronaves(sender, EventArgs.Empty);

                    popupOperacionRentasAeronaves.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRentasAeronaves_RowCommand", "Aviso");
            }
        }

        protected void gvVuelosVentas_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Detalle")
                {
                    sIdContratoDetalleOperaciones = e.CommandArgs.CommandArgument.S().I();

                    if (eGetSearchIndicadoresOperacionVuelosRentas != null)
                        eGetSearchIndicadoresOperacionVuelosRentas(sender, EventArgs.Empty);

                    popupVueloRentas.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvVuelosVentas_RowCommand", "Aviso");
            }
        }

        protected void btnExportarOperacionesIntercambios_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterOperacionesIntercambios.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarOperacionesIntercambios_Click", "Aviso");
            }
        }

        protected void btnExpartarOperacionRentas_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterOperacionRentas.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExpartarOperacionRentas_Click", "Aviso");
            }
        }

        protected void btnExportarVueloRentas_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterVueloRentas.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarVueloRentas_Click", "Aviso");
            }
        }

        #endregion "Eventos"


        #region "Metodos"

        private void SettingsGridviews()
        {
            try
            {
                gvUsuarioAudit.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvUsuarioAudit.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                gvUsuarioAudit.SettingsText.GroupPanel = "Arrastre un encabezado de columna aquí para agrupar por esa columna";
                gvUsuarioAudit.SettingsPager.PageSizeItemSettings.Visible = true;
                gvIndicadores.SettingsPager.PageSizeItemSettings.Visible = true;
                gvIndicadores.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvIndicadores.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                gvActividadUsuario.SettingsPager.PageSizeItemSettings.Visible = true;
                gvActividadUsuario.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvActividadUsuario.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                gvEstatusContratos.SettingsPager.PageSizeItemSettings.Visible = true;
                gvEstatusContratos.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvReporteadorContratos.SettingsPager.PageSizeItemSettings.Visible = true;
                gvReporteadorContratos.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvReporteadorContratos.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                gvReporteadorRemisiones.SettingsPager.PageSizeItemSettings.Visible = true;
                gvReporteadorRemisiones.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvReporteadorRemisiones.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                gvReporteadorSolicitudVuelo.SettingsPager.PageSizeItemSettings.Visible = true;
                gvReporteadorSolicitudVuelo.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvReporteadorSolicitudVuelo.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                gvReporteadorBitacoras.SettingsPager.PageSizeItemSettings.Visible = true;
                gvReporteadorBitacoras.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvReporteadorBitacoras.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                gvReporteadorPrefactura.SettingsPager.PageSizeItemSettings.Visible = true;
                gvReporteadorPrefactura.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvReporteadorPrefactura.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                
                gvReporteadorComisariato.SettingsPager.PageSizeItemSettings.Visible = true;
                gvReporteadorComisariato.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvReporteadorComisariato.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                gvVuelosSinBitacora.SettingsPager.PageSizeItemSettings.Visible = true;
                gvVuelosSinBitacora.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvVuelosSinBitacora.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                gvVencimientoContratos.SettingsPager.PageSizeItemSettings.Visible = true;
                gvVencimientoContratos.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvVencimientoContratos.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                gvBitatorasSinRemisionar.SettingsPager.PageSizeItemSettings.Visible = true;
                gvBitatorasSinRemisionar.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvBitatorasSinRemisionar.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                gvRemisionesSinPrefacturar.SettingsPager.PageSizeItemSettings.Visible = true;
                gvRemisionesSinPrefacturar.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvRemisionesSinPrefacturar.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                gvPrefacturasSinFacturar.SettingsPager.PageSizeItemSettings.Visible = true;
                gvPrefacturasSinFacturar.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvPrefacturasSinFacturar.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                gvEstatusContratosDetalle.SettingsPager.PageSizeItemSettings.Visible = true;
                gvEstatusContratosDetalle.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvEstatusContratosDetalle.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                gvRolUsuarioRolAudit.SettingsPager.PageSizeItemSettings.Visible = true;
                gvRolUsuarioRolAudit.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvRolUsuarioRolAudit.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                gvOperacionesIntercambios.SettingsPager.PageSizeItemSettings.Visible = true;
                gvOperacionesIntercambios.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvOperacionesIntercambios.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                gvOperacionRentas.SettingsPager.PageSizeItemSettings.Visible = true;
                gvOperacionRentas.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvOperacionRentas.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                gvVueloRentas.SettingsPager.PageSizeItemSettings.Visible = true;
                gvVueloRentas.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvVueloRentas.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                gvFinanzaDescuentos.SettingsPager.PageSizeItemSettings.Visible = true;
                gvFinanzaDescuentos.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvFinanzaDescuentos.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void llenarGridsPageLoad()
        {
            try
            {
                if (pcPaginaControl.ActiveTabIndex == 0)
                {
                    if (IsPostBack)
                    {
                        if (eGetSearchIndicadorFinanzas != null)
                            eGetSearchIndicadorFinanzas(null, EventArgs.Empty);
                    }
                }

                if (popupEstatusContratosDetalle.ShowOnPageLoad == true)
                {
                    if (eGetSearchEstatusContratoDetalle != null)
                        eGetSearchEstatusContratoDetalle(null, EventArgs.Empty);
                }
                else if(popupFinanzaDescuentos.ShowOnPageLoad == true)
                {
                    if (eGetSearchFinanzasDescuentos != null)
                        eGetSearchFinanzasDescuentos(null, EventArgs.Empty);                    
                }

                if (popupVuelosSinBitacora.ShowOnPageLoad == true)
                {
                    if (eGetSearchIndicadoresFinanzasVuelosSinBitacora != null)
                        eGetSearchIndicadoresFinanzasVuelosSinBitacora(null, EventArgs.Empty);
                }
                else if (popupVencimientoContratos.ShowOnPageLoad == true)
                {
                    if (eGetSearchIndicadoresFinanzasVencimientoDeContratos != null)
                        eGetSearchIndicadoresFinanzasVencimientoDeContratos(null, EventArgs.Empty);
                }
                else if (popupBitacorasSinRemisionar.ShowOnPageLoad == true)
                {
                    if (eGetSearchIndicadoresFinanzasBitacorasSinRemisionar != null)
                        eGetSearchIndicadoresFinanzasBitacorasSinRemisionar(null, EventArgs.Empty);
                }
                else if (popupRemisionesSinPrefacturar.ShowOnPageLoad == true)
                {
                    if (eGetSearchIndicadoresFinanzasRemisionesSinPrefacturar != null)
                        eGetSearchIndicadoresFinanzasRemisionesSinPrefacturar(null, EventArgs.Empty);
                }
                else if (popupPrefacturasSinFacturar.ShowOnPageLoad == true)
                {
                    if (eGetSearchIndicadoresFinanzasPrefacturasSinFacturar != null)
                        eGetSearchIndicadoresFinanzasPrefacturasSinFacturar(null, EventArgs.Empty);
                }
                else if (popupActividadUsuario.ShowOnPageLoad == true)
                {
                    if (eGetSearchActividadUsuario != null)
                        eGetSearchActividadUsuario(null, EventArgs.Empty);
                }
                else if (popupOperacionesIntercambios.ShowOnPageLoad == true)
                {
                    if (eGetSearchIndicadoresOperacionIntercambios != null)
                        eGetSearchIndicadoresOperacionIntercambios(null, EventArgs.Empty);
                }
                else if (popupOperacionRentasAeronaves.ShowOnPageLoad == true)
                {
                    if (eGetSearchIndicadoresOperacionRentaAeronaves != null)
                        eGetSearchIndicadoresOperacionRentaAeronaves(null, EventArgs.Empty);
                }
                else if (popupVueloRentas.ShowOnPageLoad == true)
                {
                    if (eGetSearchIndicadoresOperacionVuelosRentas != null)
                        eGetSearchIndicadoresOperacionVuelosRentas(null, EventArgs.Empty);
                }
                


                if (gvReporteadorContratos.Visible == true)
                {
                    if (eGetSearchReporteadorContrato != null)
                        eGetSearchReporteadorContrato(null, EventArgs.Empty);
                }
                else if (gvReporteadorRemisiones.Visible == true)
                {
                    if (eGetSearchReporteadorRemisiones != null)
                        eGetSearchReporteadorRemisiones(null, EventArgs.Empty);
                }
                else if (gvReporteadorSolicitudVuelo.Visible == true)
                {
                    if (eGetSearchReporteadorSolicitudVuelo != null)
                        eGetSearchReporteadorSolicitudVuelo(null, EventArgs.Empty);
                }
                else if (gvReporteadorBitacoras.Visible == true)
                {
                    if (eGetSearchReporteadorBitacora != null)
                        eGetSearchReporteadorBitacora(null, EventArgs.Empty);
                }
                else if (gvReporteadorPrefactura.Visible == true)
                {
                    if (eGetSearchReporteadorPrefactura != null)
                        eGetSearchReporteadorPrefactura(null, EventArgs.Empty);
                }
                else if (gvReporteadorComisariato.Visible == true)
                {
                    if (eGetSearchReporteadorComisariato != null)
                        eGetSearchReporteadorComisariato(null, EventArgs.Empty);
                }
                

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                mpeMensaje.ShowMessage(sMensaje, sCaption);
            }
            catch (Exception x) { throw x; }
        }
        
        public void LoadUsuarioAudit(DataSet dsObjCat)
        {
            try
            {
                Session["oUsuario"] = dsObjCat.Tables[0];
                gvUsuarioAudit.DataSource = null;
                gvUsuarioAudit.DataSource = dsObjCat.Tables[0];
                gvUsuarioAudit.DataBind();

                lblUsuNum.Text = "Numero de usuarios: " + dsObjCat.Tables[1].Rows[0].ItemArray[0].ToString();
                lblRolNum.Text = "Numero de roles:  " + dsObjCat.Tables[1].Rows[0].ItemArray[1].ToString();


                gvRolUsuarioRolAudit.DataSource = null;
                gvRolUsuarioRolAudit.DataSource = dsObjCat.Tables[2];
                gvRolUsuarioRolAudit.DataBind();
                dsObjCat = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadActividadUsuario(DataTable dtObj)
        {
            try
            {
                gvActividadUsuario.DataSource = null;
                gvActividadUsuario.DataSource = dtObj;
                gvActividadUsuario.DataBind();
                dtObj = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadUsuario(DataTable dtObj)
        {
            try
            {
                cmbUsuariosActividad.DataSource = dtObj;
                cmbUsuariosActividad.ValueField = "IdUsuario";
                cmbUsuariosActividad.ValueType = typeof(Int32);
                cmbUsuariosActividad.TextField = "Usuario";
                cmbUsuariosActividad.DataBindItems();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadIndicadoresFinanzas(DataSet dsIndicadorFinanzas)
        {
            try
            {
                gvIndicadores.DataSource = dsIndicadorFinanzas.Tables[0];
                gvIndicadores.DataBind();

                gvCondonaciones.DataSource = dsIndicadorFinanzas.Tables[1];
                gvCondonaciones.DataBind();

                gvEstatusContratos.DataSource = dsIndicadorFinanzas.Tables[2];
                gvEstatusContratos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadIndicadoresOperaciones(DataSet dsIndicadorOperaciones)
        {
            try
            {
                gvIndicadorOperacion.DataSource = dsIndicadorOperaciones.Tables[0];
                gvIndicadorOperacion.DataBind();

                gvIntercambio.DataSource = dsIndicadorOperaciones.Tables[1];
                gvIntercambio.DataBind();

                gvRentasAeronaves.DataSource = dsIndicadorOperaciones.Tables[2];
                gvRentasAeronaves.DataBind();

                gvVuelosVentas.DataSource = dsIndicadorOperaciones.Tables[3];
                gvVuelosVentas.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadModulos(DataTable dtObj)
        {
            try
            {
                cmbModulo.DataSource = dtObj;
                cmbModulo.ValueField = "ModuloId";
                cmbModulo.ValueType = typeof(Int32);
                cmbModulo.TextField = "Descripcion";
                cmbModulo.DataBindItems();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadReporteadorContrato(DataTable dtObj)
        {
            try
            {
                gvReporteadorContratos.DataSource = null;
                gvReporteadorContratos.DataSource = dtObj;
                gvReporteadorContratos.DataBind();
                dtObj = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadReporteadorRemisiones(DataTable dtObj)
        {
            try
            {
                gvReporteadorRemisiones.DataSource = null;
                gvReporteadorRemisiones.DataSource = dtObj;
                gvReporteadorRemisiones.DataBind();
                dtObj = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadReporteadorSolicitudVuelo(DataTable dtObj)
        {
            try
            {
                gvReporteadorSolicitudVuelo.DataSource = null;
                gvReporteadorSolicitudVuelo.DataSource = dtObj;
                gvReporteadorSolicitudVuelo.DataBind();
                dtObj = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadReporteadorBitacora(DataTable dtObj)
        {
            try
            {
                gvReporteadorBitacoras.DataSource = null;
                gvReporteadorBitacoras.DataSource = dtObj;
                gvReporteadorBitacoras.DataBind();
                dtObj = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadReporteadorPrefactura(DataTable dtObj)
        {
            try
            {
                gvReporteadorPrefactura.DataSource = null;
                gvReporteadorPrefactura.DataSource = dtObj;
                gvReporteadorPrefactura.DataBind();
                dtObj = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadReporteadorComisariato(DataTable dtObj)
        {
            try
            {
                gvReporteadorComisariato.DataSource = null;
                gvReporteadorComisariato.DataSource = dtObj;
                gvReporteadorComisariato.DataBind();
                dtObj = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadIndicadoresFinanzasVuelosSinBitacora(DataTable dsIndicadorFinanzas)
        {
            try
            {
                gvVuelosSinBitacora.DataSource = null;
                gvVuelosSinBitacora.DataSource = dsIndicadorFinanzas;
                gvVuelosSinBitacora.DataBind();
                dsIndicadorFinanzas = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadIndicadoresFinanzasVencimientoDeContratos(DataTable dsIndicadorFinanzas)
        {
            try
            {
                gvVencimientoContratos.DataSource = null;
                gvVencimientoContratos.DataSource = dsIndicadorFinanzas;
                gvVencimientoContratos.DataBind();
                dsIndicadorFinanzas = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public void LoadIndicadoresFinanzasBitacorasSinRemisionar(DataTable dsIndicadorFinanzas)
        {
            try
            {
                gvBitatorasSinRemisionar.DataSource = null;
                gvBitatorasSinRemisionar.DataSource = dsIndicadorFinanzas;
                gvBitatorasSinRemisionar.DataBind();
                dsIndicadorFinanzas = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void LoadIndicadoresFinanzasRemisionesSinPrefacturar(DataTable dsIndicadorFinanzas)
        {
            try
            {
                gvRemisionesSinPrefacturar.DataSource = null;
                gvRemisionesSinPrefacturar.DataSource = dsIndicadorFinanzas;
                gvRemisionesSinPrefacturar.DataBind();
                dsIndicadorFinanzas = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void LoadIndicadoresFinanzasPrefacturasSinFacturar(DataTable dsIndicadorFinanzas)
        {
            try
            {
                gvPrefacturasSinFacturar.DataSource = null;
                gvPrefacturasSinFacturar.DataSource = dsIndicadorFinanzas;
                gvPrefacturasSinFacturar.DataBind();
                dsIndicadorFinanzas = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadEstatusContratoDetalle(DataTable dsEstatusContratoDetalle)
        {
            try
            {
                gvEstatusContratosDetalle.DataSource = null;
                gvEstatusContratosDetalle.DataSource = dsEstatusContratoDetalle;
                gvEstatusContratosDetalle.DataBind();
                dsEstatusContratoDetalle = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadIndicadoresOperacionIntercambios(DataTable dtIndicadorOperacion)
        {
            try
            {
                gvOperacionesIntercambios.DataSource = null;
                gvOperacionesIntercambios.DataSource = dtIndicadorOperacion;
                gvOperacionesIntercambios.DataBind();
                dtIndicadorOperacion = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadIndicadoresOperacionRentaAeronaves(DataTable dtIndicadorOperacion)
        {
            try
            {
                gvOperacionRentas.DataSource = null;
                gvOperacionRentas.DataSource = dtIndicadorOperacion;
                gvOperacionRentas.DataBind();
                dtIndicadorOperacion = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadIndicadoresOperacionVuelosRentas(DataTable dtIndicadorOperacion)
        {
            try
            {
                gvVueloRentas.DataSource = null;
                gvVueloRentas.DataSource = dtIndicadorOperacion;
                gvVueloRentas.DataBind();
                dtIndicadorOperacion = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion "Metodos"


        #region "Vars y Propiedades"

        private const string sClase = "frmAudit.aspx.cs";
        private const string sPagina = "frmAudit.aspx";
        protected static int sIdTipoPaqueteEstatusContrato;
        protected static int sIdContratoDetalleOperaciones;

        Audit_Presenter oPresenter;
        
        public event EventHandler eNewObj;        
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eObjSelected;
        public event EventHandler eGetSearchModulos;
        public event EventHandler eGetSearchUsuario;
        public event EventHandler eGetSearchActividadUsuario;        
        public event EventHandler eGetSearchIndicadorFinanzas;
        public event EventHandler eGetSearchIndicadorOperaciones;

        public event EventHandler eGetSearchReporteadorContrato;
        public event EventHandler eGetSearchReporteadorRemisiones;
        public event EventHandler eGetSearchReporteadorSolicitudVuelo;
        public event EventHandler eGetSearchReporteadorBitacora;
        public event EventHandler eGetSearchReporteadorPrefactura;
        public event EventHandler eGetSearchReporteadorComisariato;

        public event EventHandler eGetSearchIndicadoresFinanzasVuelosSinBitacora;
        public event EventHandler eGetSearchIndicadoresFinanzasVencimientoDeContratos;
        public event EventHandler eGetSearchIndicadoresFinanzasBitacorasSinRemisionar;
        public event EventHandler eGetSearchIndicadoresFinanzasRemisionesSinPrefacturar;
        public event EventHandler eGetSearchIndicadoresFinanzasPrefacturasSinFacturar;

        public event EventHandler eGetSearchEstatusContratoDetalle;
        public event EventHandler eGetSearchIndicadoresOperacionIntercambios;
        public event EventHandler eGetSearchIndicadoresOperacionRentaAeronaves;
        public event EventHandler eGetSearchIndicadoresOperacionVuelosRentas;

        Int32 count = 0;
        List<string> list = new List<string>();

        public object[] oArrFiltrosActividadUsuario
        {
            get
            {                                                                
                return new object[] { 
                    "@IdModulo", cmbModulo.Value,
                    "@IdOperacion",cmbAccion.Value,
                    "@FechaCreacion",deFechaCreacion.Value,
                    "@UsuarioCreacion",cmbUsuariosActividad.Text == "Todos los Usuarios" ? string.Empty:cmbUsuariosActividad.Text
                };
            }
        }

        public object[] oArrFiltrosIndicadorOperaciones
        {
            get {

                return new object[] { 
                    "@FechaInicial",deFechaInicial.Value,
                    "@FechaFinal",deFechaFinal.Value
                };
            }
        }

        public object[] oArrFiltrosSearchReporteador
        {
            get
            {
                return new object[] { 
                    "@FechaInicial",deReporteadorFechaInicial.Value,
                    "@FechaFinal",deReporteadorFechaFinal.Value
                };
            }
        }

        public object[] oArrFiltrosSearchFinanza
        {
            get
            {
                return new object[] { 
                    "@FechaInicial",deFechaInicialFinanza.Value, 
                    "@FechaFinal",deFechaFinalFinanza.Value
                };
            }
        }
        
        public object[] oArrFiltrosSearchEstatusContrato
        {
            get
            {
                return new object[] { 
                    "@IdPaquete",sIdTipoPaqueteEstatusContrato
                    
                };
            }
        }
        public object[] oArrFiltrosSearchIndicadoresOperacionDetalles
        {
            get
            {
                return new object[] { 
                    "@IdContrato", sIdContratoDetalleOperaciones,
                    "@FechaInicial", deFechaInicial.Value,
                    "@FechaFinal", deFechaFinal.Value
                };
            }
        }
       
        #endregion "Vars y Propiedades"                               
                    
    

        public void LoadFinanzasDescuentos(DataTable dtIndicadorFinanzas)
        {
            try
            {
                gvFinanzaDescuentos.DataSource = null;
                gvFinanzaDescuentos.DataSource = dtIndicadorFinanzas;
                gvFinanzaDescuentos.DataBind();
                dtIndicadorFinanzas = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object[] oArrFiltrosSearchFinanzaDescuentos
        {
            get
            {
                return new object[] { 
                    "@IdRemision", iIdRemision
                    
                };
            } 
        }

        public event EventHandler eGetSearchFinanzasDescuentos;

        protected void btnExportaFinanzaDescuentos_Click(object sender, EventArgs e)
        {
            try
            {
                ExporterFinanzaDescuentos.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportaFinanzaDescuentos_Click", "Aviso");
            }
        }

        protected static int iIdRemision;

        protected void gvCondonaciones_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Detalle")
                {
                    iIdRemision = e.CommandArgs.CommandArgument.S().I();

                    if (eGetSearchFinanzasDescuentos != null)
                        eGetSearchFinanzasDescuentos(sender, EventArgs.Empty);

                    popupFinanzaDescuentos.ShowOnPageLoad = true;
                                                            
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCondonaciones_RowCommand", "Aviso");
            }
        }
    }
}



