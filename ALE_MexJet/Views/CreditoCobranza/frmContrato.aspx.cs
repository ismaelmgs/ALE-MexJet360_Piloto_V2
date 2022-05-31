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

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmContrato : System.Web.UI.Page, IViewContrato
    {
        #region EVENTOS

        protected void cboGeneralesClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow[] drResults = dtCliente.Select("IdCliente = " + cboGeneralesClientes.SelectedItem.Value.S());
            lblGeneralesRazonSocial.Text = drResults[0].ItemArray[4].S();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!bCanEdit)
            {
                DisableControls(ASPxPageControl1);
                upContrato.Enabled = false;
                lkbtnDownloadPDF.Enabled = false;


                int idRollDigital = 0;
                idRollDigital = ((UserIdentity)Session["UserIdentity"]).iRol.I();

                char[] sDelimmitador = { ',' };
                string sParamArchivosDig = Utils.ObtieneParametroPorClave("63");

                string[] sValor = sParamArchivosDig.Split(sDelimmitador);
                foreach (string s in sValor)
                {
                    // si existe el valor del rol en el parametro, activamos el link
                    if (Convert.ToInt32(s) == idRollDigital)
                        lkbtnDownloadPDF.Enabled = true;
                }
            }

            if (eGetPermisosContrato != null)
                eGetPermisosContrato(sender, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new Contrato_Presenter(this, new DBContrato());
                gvTramoPactado.SettingsPager.ShowDisabledButtons = true;
                gvTramoPactado.SettingsPager.ShowNumericButtons = true;
                gvTramoPactado.SettingsPager.ShowSeparators = true;
                gvTramoPactado.SettingsPager.Summary.Visible = true;
                gvTramoPactado.SettingsPager.PageSizeItemSettings.Visible = true;
                gvTramoPactado.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvTramoPactado.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                if (Session["UserIdentity"] == null)
                    Response.Redirect("../frmLogin.aspx");

                if (!IsPostBack)
                {
                    string sContratoRecibido = string.Empty;
                    string sAccionRecibido = string.Empty;
                    bCanEdit = false;
                    GeneraDataTableAeronabeBase();
                    GeneraDataTableTramosPactado();

                    if (eGetDatosFacturacion != null)
                        eGetDatosFacturacion(sender, e);

                    if (Request.QueryString.Count > 0)
                    {
                        string sContrato = Request.QueryString["Contrato"];
                        if (!string.IsNullOrEmpty(sContrato))
                        {
                            sContratoRecibido = Encoding.UTF8.GetString(Convert.FromBase64String(Request.QueryString["Contrato"]));
                        }
                        sAccionRecibido = Encoding.UTF8.GetString(Convert.FromBase64String(Request.QueryString["Accion"]));
                    }
                    if (!string.IsNullOrEmpty(sAccionRecibido))
                    {
                        bCanEdit = sAccionRecibido.S() == "Editar" || sAccionRecibido.S() == "Nuevo" ? true : false;
                    }
                    if (!string.IsNullOrEmpty(sContratoRecibido))
                    {
                        iIdContrato = sContratoRecibido.I();
                        bActualizaGenerales = true;
                        bActualizaTarifa = true;
                        bActualizaCobro = true;
                        bActualizaGiras = true;
                        bActualizaCaracteristicas = true;
                        bActualizaPreferencias = true;

                        ASPxPageControl1.TabPages[0].Enabled = true;
                        ASPxPageControl1.TabPages[1].Enabled = true;
                        ASPxPageControl1.TabPages[2].Enabled = true;
                        ASPxPageControl1.TabPages[3].Enabled = true;
                        ASPxPageControl1.TabPages[4].Enabled = true;
                        ASPxPageControl1.TabPages[5].Enabled = true;
                        ASPxPageControl1.TabPages[6].Enabled = true;
                        ASPxPageControl1.TabPages[7].Enabled = true;

                        if (eGetContratoEdicion != null)
                            eGetContratoEdicion(sender, e);

                        gvServicioConCargo.DataSource = dtServicioConCargo;
                        gvServicioConCargo.DataBind();
                        ObtieneValores();

                        DataRow[] drResults = dtCliente.Select("IdCliente = " + cboGeneralesClientes.Value.S());
                        lblGeneralesRazonSocial.Text = drResults[0].ItemArray[4].S();
                        ValidaRecuperacionContrato();
                    }
                    else
                    {
                        iIdContrato = -1;
                        bActualizaGenerales = false;
                        bActualizaTarifa = false;
                        bActualizaCobro = false;
                        bActualizaGiras = false;
                        bActualizaCaracteristicas = false;
                        bActualizaPreferencias = false;

                        //DtGeneralesFechaContrato.Date = DateTime.Now;
                        //dtGeneralesFechaInicioVuelo.Date = DateTime.Now;
                        spinGeneralesAñosContrato.Value = 1;
                        spinGeneralesMesesGracia.Value = 0;
                    }

                    btnTerminar.Visible = bCanEdit;
                }

                ObtieneValores();
                gvTarifaCombustible.DataSource = dtTarifaCombustible;
                gvTarifaCombustible.DataBind();
                gvTramoPactado.DataSource = dtTramosPactados;
                gvTramoPactado.DataBind();
                gvBase.DataSource = dtBasesSeleccionadas;
                gvBase.DataBind();
                gvIntercambios.DataSource = dtIntercambios;
                gvIntercambios.DataBind();
                gvRangosCombustible.DataSource = dtRangos;
                gvRangosCombustible.DataBind();

                if (cboGeneralesPaquete.SelectedItem != null)
                    lblTarifasTipoPaquete.Text = cboGeneralesPaquete.SelectedItem.Text;
                if (cboGeneralesClientes.SelectedItem != null)
                    lblTarifasClaveCliente.Text = cboGeneralesClientes.SelectedItem.Text;
                lblTarifasClaveContrato.Text = txtGeneralesContrato.Text;
                if (cboGeneralesModelo.SelectedItem != null)
                    lblTarifasGrupoAeronave.Text = cboGeneralesModelo.SelectedItem.Text;

                lblCobrosTipoPaquete.Text = lblTarifasTipoPaquete.Text;
                lblIntercambiosTipoPaquete.Text = lblTarifasTipoPaquete.Text;
                lblGirasTipoPaquete.Text = lblTarifasTipoPaquete.Text;
                lblRangosTipoPaquete.Text = lblTarifasTipoPaquete.Text;
                lblCaracteristicasTipoPaquete.Text = lblTarifasTipoPaquete.Text;
                lblPreferenciasTipoPaquete.Text = lblTarifasTipoPaquete.Text;
                lblCobrosClaveCliente.Text = lblTarifasClaveCliente.Text;
                lblIntercambiosClaveCliente.Text = lblTarifasClaveCliente.Text;
                lblGirasClaveCliente.Text = lblTarifasClaveCliente.Text;
                lblRangosClaveCliente.Text = lblTarifasClaveCliente.Text;
                lblCaracteristicasClaveCliente.Text = lblTarifasClaveCliente.Text;
                lblPreferenciasClaveCliente.Text = lblTarifasClaveCliente.Text;
                lblCobrosClaveContrato.Text = lblTarifasClaveContrato.Text;
                lblIntercambiosClaveContrato.Text = lblTarifasClaveContrato.Text;
                lblGirasClaveContrato.Text = lblTarifasClaveContrato.Text;
                lblRangosClaveContrato.Text = lblTarifasClaveContrato.Text;
                lblCaracteristicasClaveContrato.Text = lblTarifasClaveContrato.Text;
                lblPreferenciasClaveContrato.Text = lblTarifasClaveContrato.Text;
                lblCobrosGrupoAeronave.Text = lblTarifasGrupoAeronave.Text;
                lblIntercambiosGrupoAeronave.Text = lblTarifasGrupoAeronave.Text;
                lblGirasGrupoAeronave.Text = lblTarifasGrupoAeronave.Text;
                lblRangosGrupoAeronave.Text = lblTarifasGrupoAeronave.Text;
                lblCaracteristicasGrupoAeronave.Text = lblTarifasGrupoAeronave.Text;
                lblPreferenciasGrupoAeronave.Text = lblTarifasGrupoAeronave.Text;

                

                if (!IsPostBack)
                {
                    new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Acceso), "Acceso al modulo " + Enumeraciones.Pantallas.Contrato.S());
                }

            }
            catch (Exception ex)
            {
                //Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
                throw ex;
            }

        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            
        }

        protected void ASPxPageControl1_ActiveTabChanged(object source, TabControlEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ASPxPageControl1_ActiveTabChanged", "Aviso");
            }
        }

        protected void rdlTarifasCombustible_CheckedChanged1(object sender, EventArgs e)
        {

            cboTarifaCalculoPrecioCombustible.ValidationSettings.RequiredField.IsRequired = rdlTarifasCombustible.Checked;
            cboTarifaCalculoPrecioCombustible.IsValid = true;
            cboTarifaCalculoPrecioCombustible.Value = "-1";

            if (cboTarifaCalculoPrecioCombustible.ValidationSettings.RequiredField.IsRequired)
            {
                lblTarifasCalculoPrecioCombustible.Text = "Cálculo precio de combustible:*";
            }
            else
            {
                lblTarifasCalculoPrecioCombustible.Text = "Cálculo precio de combustible:";
                txtTarifaConsumo.Text = "0";
            }
        }

        protected void rdlTarifasNoCombustible_CheckedChanged1(object sender, EventArgs e)
        {
            cboTarifaCalculoPrecioCombustible.ValidationSettings.RequiredField.IsRequired = rdlTarifasCombustible.Checked;
            cboTarifaCalculoPrecioCombustible.Value = null;
            cboTarifaCalculoPrecioCombustible.IsValid = true;
            if (cboTarifaCalculoPrecioCombustible.ValidationSettings.RequiredField.IsRequired)
            {
                lblTarifasCalculoPrecioCombustible.Text = "Cálculo precio de combustible:*";
            }
            else
            {
                lblTarifasCalculoPrecioCombustible.Text = "Cálculo precio de combustible:";
                txtTarifaConsumo.Text = "0";
            }

        }

        protected void chkCobrosAplicaTramo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gvTramoPactado.Visible = chkCobrosAplicaTramo.Checked;
                gvTramoPactado.Focus();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "chkCobrosAplicaTramo_CheckedChanged", "Aviso");
            }

        }

        protected void rdlDescuentanPernoctasNacional_CheckedChanged(object sender, EventArgs e)
        {
            txtPernoctaFactorConversionNacional.IsValid = true;
            if (rdlDescuentanPernoctasNacional.Checked)
            {
                txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.IsRequired = true;
                txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtPernoctaFactorConversionNacional.ReadOnly = false;
            }
            else
            {
                txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.IsRequired = false;
                txtPernoctaFactorConversionNacional.ReadOnly = true;
            }
            txtPernoctaFactorConversionNacional.Text = string.Empty;
        }

        protected void rdlDescuentanPernoctasInternacional_CheckedChanged(object sender, EventArgs e)
        {
            txtPernoctaFactorConversionNacional.IsValid = true;
            if (rdlDescuentanPernoctasInternacional.Checked)
            {
                txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.IsRequired = true;
                txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtPernoctaFactorConversionInternacional.ReadOnly = false;
            }
            else
            {
                txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.IsRequired = false;
                txtPernoctaFactorConversionInternacional.ReadOnly = true;

            }
            txtPernoctaFactorConversionInternacional.Text = string.Empty;
        }

        protected void rdlNoDescuentanPernoctasNacional_CheckedChanged(object sender, EventArgs e)
        {
            txtPernoctaFactorConversionNacional.IsValid = true;
            if (rdlDescuentanPernoctasNacional.Checked)
            {
                txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.IsRequired = true;
                txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtPernoctaFactorConversionNacional.ReadOnly = false;
            }
            else
            {
                txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.IsRequired = false;
                txtPernoctaFactorConversionNacional.ReadOnly = true;

            }
            txtPernoctaFactorConversionNacional.Text = string.Empty;
        }

        protected void rdlNoDescuentanPernoctasInternacional_CheckedChanged(object sender, EventArgs e)
        {
            txtPernoctaFactorConversionNacional.IsValid = true;
            if (rdlDescuentanPernoctasInternacional.Checked)
            {
                txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.IsRequired = true;
                txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtPernoctaFactorConversionInternacional.ReadOnly = false;
            }
            else
            {
                txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.IsRequired = false;
                txtPernoctaFactorConversionInternacional.ReadOnly = true;

            }
            txtPernoctaFactorConversionInternacional.Text = "0";
        }

        protected void rdlDescuentanTiempoEsperaNacional_CheckedChanged(object sender, EventArgs e)
        {
            txtTiempoEsperaFactorHrsVueloNal.IsValid = true;
            if (rdlDescuentanTiempoEsperaNacional.Checked)
            {

                txtTiempoEsperaFactorHrsVueloNal.ValidationSettings.RequiredField.IsRequired = true;
                txtTiempoEsperaFactorHrsVueloNal.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtTiempoEsperaFactorHrsVueloNal.ReadOnly = false;
            }
            else
            {
                txtTiempoEsperaFactorHrsVueloNal.ValidationSettings.RequiredField.IsRequired = false;
                txtTiempoEsperaFactorHrsVueloNal.ReadOnly = true;

            }
            txtTiempoEsperaFactorHrsVueloNal.Text = string.Empty;

        }

        protected void rdlNoDescuentanTiempoEsperaNacional_CheckedChanged(object sender, EventArgs e)
        {
            txtTiempoEsperaFactorHrsVueloNal.IsValid = true;
            if (rdlDescuentanTiempoEsperaNacional.Checked)
            {
                txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.IsRequired = true;
                txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtPernoctaFactorConversionInternacional.ReadOnly = false;
            }
            else
            {
                txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.IsRequired = false;
                txtPernoctaFactorConversionInternacional.ReadOnly = true;

            }
            txtPernoctaFactorConversionInternacional.Text = string.Empty;
        }

        protected void rdlDescuentanTiempoEsperaInternaacional_CheckedChanged(object sender, EventArgs e)
        {
            if (rdlDescuentanTiempoEsperaInternaacional.Checked)
            {
                txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.IsRequired = true;
                txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtTiempoEsperaFactorHrsVueloInt.ReadOnly = false;
            }
            else
            {
                txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.IsRequired = false;
                txtTiempoEsperaFactorHrsVueloInt.ReadOnly = true;

            }
            txtTiempoEsperaFactorHrsVueloInt.Text = string.Empty;
        }

        protected void rdlNoDescuentanTiempoEsperaInternaacional_CheckedChanged(object sender, EventArgs e)
        {
            if (rdlDescuentanTiempoEsperaInternaacional.Checked)
            {
                txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.IsRequired = true;
                txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                txtTiempoEsperaFactorHrsVueloInt.ReadOnly = false;
            }
            else
            {
                txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.IsRequired = false;
                txtTiempoEsperaFactorHrsVueloInt.ReadOnly = true;

            }
            txtTiempoEsperaFactorHrsVueloInt.Text = string.Empty;
        }

        protected void gvBase_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;

                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }

                if (e.Column.FieldName == "Aeropuerto")
                {
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = dtBase;
                    cmb.ValueField = "AeropuertoIATA";
                    cmb.ValueType = typeof(string);
                    cmb.TextField = "AeropuertoIATA";
                    cmb.DataBindItems();
                }

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvBase_CellEditorInitialize", "Aviso");
            }
        }

        protected void gvBase_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
        }

        protected void gvBase_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {

                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrudBases = e;
                DataTable dtBasesSelecionados = dtBasesSeleccionadas;
                for (int iIndice = 0; iIndice <= dtBasesSelecionados.Rows.Count - 1; iIndice++)
                {
                    if (dtBasesSelecionados.Rows[iIndice]["IdBase"].S().I() == e.Keys[0].S().I())
                    {
                        dtBasesSelecionados.Rows[iIndice].Delete();
                    }
                }

                if (iIdContrato > 0)
                {
                    if (eDeleteObj != null)
                        eDeleteObj(sender, e);
                }
                dtBasesSeleccionadas = dtBasesSelecionados;
                gvBase.DataSource = dtBasesSeleccionadas;
                gvBase.DataBind();

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvBase_RowDeleting", "Aviso");
            }
            finally
            {
                CancelEditing(e);
            }
        }

        protected void gvBase_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {

                eCrud = Enumeraciones.TipoOperacion.Insertar;
                ASPxGridView grid = (ASPxGridView)sender;
                DataTable dtBaseGeerales = dtBasesSeleccionadas;
                DataRow drBase = dtBaseGeerales.NewRow();
                drBase["IdBase"] = dtBaseGeerales.Rows.Count + 1;
                drBase["TipoBase"] = e.NewValues["TipoBase"];
                drBase["Aeropuerto"] = e.NewValues["Aeropuerto"];
                dtBaseGeerales.Rows.Add(drBase);
                dtBasesSeleccionadas = dtBaseGeerales;

                if (iIdContrato > 0)
                {
                    oCrudBases = e;
                    if (eSaveBases != null)
                        eSaveBases(sender, e);
                }

                gvBase.DataSource = dtBasesSeleccionadas;
                gvBase.DataBind();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvBase_RowInserting", "Aviso");
            }
            finally
            {
                CancelEditing(e);

            }
        }

        protected void gvBase_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {

                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;
                DataTable dtBasesSelecionados = dtBasesSeleccionadas;
                for (int iIndice = 0; iIndice <= dtBasesSelecionados.Rows.Count - 1; iIndice++)
                {
                    if (dtBasesSelecionados.Rows[iIndice]["IdBase"].S().I() == e.Keys[0].S().I())
                    {
                        dtBasesSelecionados.Rows[iIndice]["TipoBase"] = e.NewValues["TipoBase"];
                        dtBasesSelecionados.Rows[iIndice]["Aeropuerto"] = e.NewValues["Aeropuerto"];
                        break;
                    }
                }

                if (iIdContrato > 0)
                {
                    oCrudBases = e;
                    if (eUpdateBases != null)
                        eUpdateBases(sender, e);

                    gvBase.DataSource = dtBasesSeleccionadas;
                    gvBase.DataBind();
                }

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvBase_RowUpdating", "Aviso");
            }
            finally
            {
                CancelEditing(e);
            }
        }

        protected void gvBase_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvBase.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvBase_StartRowEditing", "Aviso");
            }
        }

        protected void gvBase_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {

                oCrud = e;
                int iIdBase = 0;
                if (e.Keys.Count != 0)
                {
                    iIdBase = e.Keys[0].S().I();
                }
                bool bAeropuertoBase = false;
                bool bAeropuerto = false;

                foreach (DataRow row in dtBasesSeleccionadas.Rows)
                {
                    if (row["TipoBase"].S().I() == 1 && row["IdBase"].S().I() != iIdBase)
                    {
                        bAeropuertoBase = true;
                    }
                    if (row["Aeropuerto"].S() == e.NewValues["Aeropuerto"].S() && row["IdBase"].S().I() != iIdBase)
                    {
                        bAeropuerto = true;
                    }
                }

                if (e.NewValues["TipoBase"].S().I() == 1 && bAeropuertoBase)
                {
                    AddError(e.Errors, gvBase.Columns["TipoBase"], "Solo puede haber una base predeterminada");
                }

                if (bAeropuerto)
                {
                    AddError(e.Errors, gvBase.Columns["Aeropuerto"], "Este aeropuerto ya está asociado a un tipo de base");
                }

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvBase_RowValidating", "Aviso");
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

        protected void gvIntercambios_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrudCombustible = e;
                if (eDeleteCombustibleInternacional != null)
                    eDeleteCombustibleInternacional(sender, e);
                gvTarifaCombustible.DataSource = dtTarifaCombustible;
                gvTarifaCombustible.DataBind();
                CancelEditing(e);

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvIntercambios_StartRowEditing", "Aviso");
            }
        }

        protected void pageControl_Load(object sender, EventArgs e)
        {
            try
            {
                ASPxPageControl pg = (ASPxPageControl)sender;
                ASPxComboBox cmb = pg.FindControl("cboIntercambioEditorGrupoModelo") as ASPxComboBox;
                cmb.DataSource = dtGrupoModelo;
                cmb.ValueField = "GrupoModeloId";
                cmb.ValueType = typeof(int);
                cmb.TextField = "Descripcion";
                cmb.DataBindItems();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "pageControl_Load", "Aviso");
            }
        }

        protected void gvTarifaCombustible_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTarifaCombustible_RowDeleting", "Aviso");
            }


        }

        void cmbModelo_OnCallback(object source, CallbackEventArgsBase e)
        {
            try
            {
                FillCityModelo(source as ASPxComboBox, e.Parameter);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "cmbModelo_OnCallback", "Aviso");
            }

        }

        protected void FillCityModelo(ASPxComboBox cmb, string IdMarca)
        {
            try
            {
                if (string.IsNullOrEmpty(IdMarca)) return;
                iIdMarca = IdMarca.I();
                if (eSearchModelos != null)
                    eSearchModelos(null, EventArgs.Empty);
                cmb.DataSource = dtModelo;
                cmb.ValueField = "IdModelo";
                cmb.ValueType = typeof(int);
                cmb.TextField = "DescripcionModelo";
                cmb.DataBind();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "FillCityModelo", "Aviso");
            }
        }

        protected void spinGeneralesMesesGracia_Validation(object sender, ValidationEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "spinGeneralesMesesGracia_Validation", "Aviso");
            }
        }

        protected void txtTarifaNacionalFija_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTarifaNacionalFija.Text) && txtTarifaNacionalFija.Text.D() > 0)
                {
                    txtTarifaNacionalVarable.ReadOnly = true;
                    txtTarifaNacionalVarable.Text = "0";
                }
                else
                {
                    txtTarifaNacionalFija.Text = "0";
                    txtTarifaNacionalVarable.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtTarifaNacionalFija_TextChanged", "Aviso");
            }

        }

        protected void txtTarifaNacionalVarable_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTarifaNacionalVarable.Text) && txtTarifaNacionalVarable.Text.D() > 0)
                {
                    txtTarifaNacionalFija.ReadOnly = true;
                    txtTarifaNacionalFija.Text = "0";
                }
                else
                {
                    txtTarifaNacionalVarable.Text = "0";
                    txtTarifaNacionalFija.ReadOnly = false;
                }

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtTarifaNacionalVarable_TextChanged", "Aviso");
            }

        }

        protected void txtTarifaInternacionalFija_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTarifaInternacionalFija.Text) && txtTarifaInternacionalFija.Text.D() > 0)
                {
                    txtTarifaInternacionalVariable.ReadOnly = true;
                    txtTarifaInternacionalVariable.Text = "0";
                }
                else
                {
                    txtTarifaInternacionalFija.Text = "0";
                    txtTarifaInternacionalVariable.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtTarifaInternacionalFija_TextChanged", "Aviso");
            }
        }

        protected void txtTarifaInternacionalVariable_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(txtTarifaInternacionalVariable.Text) && txtTarifaInternacionalVariable.Text.D() > 0)
                {
                    txtTarifaInternacionalFija.ReadOnly = true;
                    txtTarifaInternacionalFija.Text = "0";
                }
                else
                {
                    txtTarifaInternacionalVariable.Text = "0";
                    txtTarifaInternacionalFija.ReadOnly = false;
                    txtTarifaInternacionalFija.Text = "0";
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtTarifaInternacionalVariable_TextChanged", "Aviso");
            }


        }

        protected void txtTarifaTiempoEsperaNacionaFija_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtTarifaTiempoEsperaNacionaFija.Text) && txtTarifaTiempoEsperaNacionaFija.Text.D() > 0)
                {
                    txtTarifaTiempoEsperaNacionaVariable.ReadOnly = true;
                    txtTarifaTiempoEsperaNacionaVariable.Text = "0";

                }
                else
                {
                    txtTarifaTiempoEsperaNacionaFija.Text = "0";
                    txtTarifaTiempoEsperaNacionaVariable.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvBase_CellEditorInitialize", "Aviso");
            }

        }

        protected void txtTarifaTiempoEsperaNacionaVariable_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtTarifaTiempoEsperaNacionaVariable.Text) && txtTarifaTiempoEsperaNacionaVariable.Text.D() > 0)
                {
                    txtTarifaTiempoEsperaNacionaFija.ReadOnly = true;
                    txtTarifaTiempoEsperaNacionaFija.Text = "0";
                }
                else
                {
                    txtTarifaTiempoEsperaNacionaVariable.Text = "0";
                    txtTarifaTiempoEsperaNacionaFija.ReadOnly = false;
                }

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtTarifaTiempoEsperaNacionaVariable_TextChanged", "Aviso");
            }

        }

        protected void txtTarifaTiempoEsperaInternacinalFija_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtTarifaTiempoEsperaInternacinalFija.Text) && txtTarifaTiempoEsperaInternacinalFija.Text.D() > 0)
                {
                    txtTarifaTiempoEsperaInternacinalNacional.ReadOnly = true;
                }
                else
                {
                    txtTarifaTiempoEsperaInternacinalFija.Text = "0";
                    txtTarifaTiempoEsperaInternacinalNacional.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtTarifaTiempoEsperaInternacinalFija_TextChanged", "Aviso");
            }
        }

        protected void txtTarifaTiempoEsperaInternacinalNacional_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtTarifaTiempoEsperaInternacinalNacional.Text) && txtTarifaTiempoEsperaInternacinalNacional.Text.D() > 0)
                {
                    txtTarifaTiempoEsperaInternacinalFija.ReadOnly = true;
                }
                else
                {
                    txtTarifaTiempoEsperaInternacinalNacional.Text = "0";
                    txtTarifaTiempoEsperaInternacinalFija.ReadOnly = false;
                }


            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtTarifaTiempoEsperaInternacinalNacional_TextChanged", "Aviso");
            }
        }

        protected void chkGirasAplicaEspera_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (chkGirasAplicaEspera.Checked)
                {
                    chkGiraAplicaHorario.Checked = false;
                    chkGiraAplicaHorario.ReadOnly = true;
                    txtGiraHoraInicio.ReadOnly = true;
                    txtGiraHoraFin.ReadOnly = true;
                    lblGiraNumeroVesesTiempoVuelo.Text = "Número de veces de tiempo de vuelo*";
                    spnGiraNumeroVesesTiempoVuelo.ValidationSettings.RequiredField.IsRequired = true;
                    spnGiraNumeroVesesTiempoVuelo.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                }
                else
                {
                    chkGiraAplicaHorario.Checked = false;
                    chkGiraAplicaHorario.ReadOnly = false;
                    txtGiraHoraInicio.ReadOnly = false;
                    txtGiraHoraFin.ReadOnly = false;
                    lblGiraNumeroVesesTiempoVuelo.Text = "Número de veces de tiempo de vuelo";
                    spnGiraNumeroVesesTiempoVuelo.ValidationSettings.RequiredField.IsRequired = false;
                    spnGiraNumeroVesesTiempoVuelo.IsValid = true;
                }
                txtGiraHoraInicio.Text = string.Empty;
                txtGiraHoraFin.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "chkGirasAplicaEspera_CheckedChanged", "Aviso");
            }

        }

        protected void chkGiraAplicaHorario_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (chkGiraAplicaHorario.Checked)
                {
                    chkGirasAplicaEspera.ReadOnly = true;
                    spnGiraNumeroVesesTiempoVuelo.ReadOnly = true;
                    txtGiraHoraFin.ValidationSettings.RequiredField.IsRequired = true;
                    txtGiraHoraFin.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                    txtGiraHoraInicio.ValidationSettings.RequiredField.IsRequired = true;
                    txtGiraHoraInicio.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                    lblGiraHoraInicio.Text = "Hora de Inicio*";
                    lblGiraHoraFin.Text = "Hora de Fin*";

                }
                else
                {
                    chkGiraAplicaHorario.Checked = false;
                    chkGiraAplicaHorario.ReadOnly = false;
                    txtGiraHoraInicio.ReadOnly = false;
                    txtGiraHoraFin.ReadOnly = false;
                    txtGiraHoraInicio.ValidationSettings.RequiredField.IsRequired = false;
                    txtGiraHoraFin.ValidationSettings.RequiredField.IsRequired = false;
                    txtGiraHoraInicio.IsValid = true;
                    txtGiraHoraFin.IsValid = true;
                    lblGiraHoraInicio.Text = "Hora de Inicio";
                    lblGiraHoraFin.Text = "Hora de Fin";
                    chkGirasAplicaEspera.ReadOnly = false;
                    spnGiraNumeroVesesTiempoVuelo.ReadOnly = false;
                }
                spnGiraNumeroVesesTiempoVuelo.Value = null;

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "chkGiraAplicaHorario_CheckedChanged", "Aviso");
            }
        }

        protected void ChkGiraAplicaFactorFechaPico_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (ChkGiraAplicaFactorFechaPico.Checked)
                    lblGiraFactorFechaPico.Text = "Factor x Fecha Pico*";
                else
                {
                    lblGiraFactorFechaPico.Text = "Factor x Fecha Pico";
                    txtGiraFactorFechaPico.IsValid = true;

                }
                txtGiraFactorFechaPico.ReadOnly = !ChkGiraAplicaFactorFechaPico.Checked;
                txtGiraFactorFechaPico.Text = string.Empty;
                txtGiraFactorFechaPico.ValidationSettings.RequiredField.IsRequired = ChkGiraAplicaFactorFechaPico.Checked;
                txtGiraFactorFechaPico.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ChkGiraAplicaFactorFechaPico_CheckedChanged", "Aviso");
            }
        }

        protected void cboTarifaCalculoPrecioCombustible_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cboTarifaCalculoPrecioCombustible.SelectedIndex == 1 || cboTarifaCalculoPrecioCombustible.SelectedIndex == 2)
                {
                    txtTarifaConsumo.ValidationSettings.RequiredField.IsRequired = true;
                    txtTarifaConsumo.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
                    txtTarifaConsumo.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                    txtTarifaConsumo.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                    txtTarifaConsumo.ValidationSettings.ErrorText = "Error en la información ingresada";
                    lblTarifaConsumo.Text = "Consumo Galones/Hr:*";
                }
                else if (cboTarifaCalculoPrecioCombustible.SelectedIndex == 4)
                {
                    txtFactorTramNal.ValidationSettings.RequiredField.IsRequired = true;
                    txtFactorTramNal.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
                    txtFactorTramNal.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                    txtFactorTramNal.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                    txtFactorTramNal.ValidationSettings.ErrorText = "Error en la información ingresada";

                    txtFactorTramInt.ValidationSettings.RequiredField.IsRequired = true;
                    txtFactorTramInt.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
                    txtFactorTramInt.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                    txtFactorTramInt.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                    txtFactorTramInt.ValidationSettings.ErrorText = "Error en la información ingresada";
                }
                else
                {
                    txtTarifaConsumo.ValidationSettings.RequiredField.IsRequired = false;
                    lblTarifaConsumo.Text = "Consumo Galones/Hr:";
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "cboTarifaCalculoPrecioCombustible_SelectedIndexChanged", "Aviso");
            }
        }

        protected void rdlTarifasCombustible_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                rdlTarifasNoCombustible.Checked = false;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "rdlTarifasCombustible_CheckedChanged", "Aviso");
            }

        }

        protected void rdlTarifasNoCombustible_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                rdlTarifasCombustible.Checked = false;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvBase_CellEditorInitialize", "Aviso");
            }

        }

        protected void txtGeneralesHorasContratadasTot_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (!txtGeneralesHorasContratadasTot.IsValid)
                {
                    return;
                }
                int iHorasContratadas = txtGeneralesHorasContratadasTot.Value.S().I();
                int iAñosContratados = spinGeneralesAñosContrato.Value.S().I();
                int iHorasPorAño = iHorasContratadas / iAñosContratados;
                txtGeneralesHorasContratadasAño.Value = iHorasPorAño;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtGeneralesHorasContratadasTot_ValueChanged", "Aviso");
            }
        }

        protected void btnTarifaNuevoCombustible_Click(object sender, EventArgs e)
        {
            try
            {
                gvTarifaCombustible.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvBase_CellEditorInitialize", "Aviso");
            }


        }

        protected void gvTarifaCombustible_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {

                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrudCombustible = e;
                if (eSaveCombustibleInternacional != null)
                    eSaveCombustibleInternacional(sender, e);
                gvTarifaCombustible.DataSource = dtTarifaCombustible;
                gvTarifaCombustible.DataBind();

                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTarifaCombustible_RowInserting", "Aviso");
            }
        }

        protected void gvTarifaCombustible_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {

                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrudCombustible = e;
                if (eUpdateCombustibleInternacional != null)
                    eUpdateCombustibleInternacional(sender, e);

                gvTarifaCombustible.DataSource = dtTarifaCombustible;
                gvTarifaCombustible.DataBind();

                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTarifaCombustible_RowUpdating", "Aviso");
            }
        }

        protected void gvTarifaCombustible_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {

                e.Editor.ReadOnly = false;
                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTarifaCombustible_CellEditorInitialize", "Aviso");
            }
        }

        protected void gvTarifaCombustible_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTarifaCombustible_StartRowEditing", "Aviso");
            }
        }

        protected void gvTramoPactado_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {

                e.Editor.ReadOnly = false;
                if (e.Column.FieldName == "Destino")
                {
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.Callback += new CallbackEventHandlerBase(cmbBase_OnCallback);
                }

                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramoPactado_CellEditorInitialize", "Aviso");
            }
        }

        protected void ASPxComboBox_OnItemsRequestedByFilterConditionOrigen(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {

                ASPxComboBox comboBox = (ASPxComboBox)source;
                sFiltroAeropuerto = e.Filter;
                if (sFiltroAeropuerto.Length < 1)
                {
                    if (eGetAeropuertoOrigen != null)
                        eGetAeropuertoOrigen(this, e);
                }
                else
                {
                    if (eGetAeropuertoOrigenFiltrado != null)
                        eGetAeropuertoOrigenFiltrado(source, e);
                }

                comboBox.DataSource = dtTarifaOrigen;
                comboBox.ValueField = "AeropuertoIATA";
                comboBox.ValueType = typeof(string);
                comboBox.TextField = "AeropuertoIATA";
                comboBox.DataBindItems();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ASPxComboBox_OnItemsRequestedByFilterConditionOrigen", "Aviso");
            }
        }

        protected void ASPxComboBox_OnItemsRequestedByFilterConditionDestino(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {

                ASPxComboBox comboBox = (ASPxComboBox)source;
                sFiltroAeropuerto = e.Filter;
                if (sFiltroAeropuerto.Length < 1)
                {
                    if (eGetAeropuertoDestino != null)
                        eGetAeropuertoDestino(this, e);
                }
                else
                {
                    if (eGetAeropuertoDestinoFiltrado != null)
                        eGetAeropuertoDestinoFiltrado(source, e);
                }

                comboBox.DataSource = dtTarifaDestino;
                comboBox.ValueField = "AeropuertoIATA";
                comboBox.ValueType = typeof(string);
                comboBox.TextField = "AeropuertoIATA";
                comboBox.DataBindItems();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ASPxComboBox_OnItemsRequestedByFilterConditionDestino", "Aviso");
            }
        }

        protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(e.Value.S()))
                {
                    e.Value.S();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvBase_CellEditorInitialize", "Aviso");
            }

        }

        protected void FillCombo(ASPxComboBox cmb, string country)
        {
            try
            {

                if (string.IsNullOrEmpty(country)) return;
                if (eGetAeropuertoDestino != null)
                    eGetAeropuertoDestino(this, EventArgs.Empty);
                cmb.Items.Clear();
                cmb.DataSource = dtTarifaDestino;
                cmb.ValueField = "AeropuertoIATA";
                cmb.ValueType = typeof(string);
                cmb.TextField = "AeropuertoIATA";
                cmb.DataBindItems();

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "FillCombo", "Aviso");
            }
        }

        protected void gvTramoPactado_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                oCrudTamoPactado = e;
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                if (eSaveTramo != null)
                    eSaveTramo(sender, e);
                gvTramoPactado.DataSource = dtTramosPactados;
                gvTramoPactado.DataBind();
                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramoPactado_RowInserting", "Aviso");
            }
        }

        protected void gvTramoPactado_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrudTamoPactado = e;

                if (eUpdateTramo != null)
                    eUpdateTramo(sender, e);
                gvTramoPactado.DataSource = dtTramosPactados;
                gvTramoPactado.DataBind();
                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramoPactado_RowUpdating", "Aviso");
            }

        }

        protected void gvTramoPactado_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {

                oCrudTamoPactado = e;
                if (eValidaTramo != null)
                    eValidaTramo(sender, e);

                if (bDuplicaTramo)
                {
                    AddError(e.Errors, gvTramoPactado.Columns["Origen"], "El tramo ya existe favor de validarlo, favor de validarlo.");
                }

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramoPactado_RowValidating", "Aviso");
            }
        }

        protected void gvTramoPactado_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {

                oCrudTamoPactado = e;
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                if (eDeleteTramo != null)
                    eDeleteTramo(sender, e);
                CancelEditing(e);
                gvTramoPactado.DataSource = dtTramosPactados;
                gvTramoPactado.DataBind();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramoPactado_RowDeleting", "Aviso");
            }

        }

        protected void gvTramoPactado_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramoPactado_CancelRowEditing", "Aviso");
            }
        }

        protected void chkTarifasPrecioEspecial_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                gvTarifaCombustible.Visible = chkTarifasPrecioEspecial.Checked;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "chkTarifasPrecioEspecial_CheckedChanged", "Aviso");
            }
        }

        protected void rdlListTarifaCoroEspera_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                if (rdlListTarifaCoroEspera.Value.S().I() == 0)
                {
                    txtTarifaTiempoEsperaNacionaFija.ReadOnly = true;
                    txtTarifaTiempoEsperaNacionaVariable.ReadOnly = true;
                    txtTarifaTiempoEsperaInternacinalFija.ReadOnly = true;
                    txtTarifaTiempoEsperaInternacinalNacional.ReadOnly = true;

                    txtTarifaTiempoEsperaNacionaFija.Text = "0";
                    txtTarifaTiempoEsperaNacionaVariable.Text = "0";
                    txtTarifaTiempoEsperaInternacinalFija.Text = "0";
                    txtTarifaTiempoEsperaInternacinalNacional.Text = "0";
                }
                else
                {
                    txtTarifaTiempoEsperaNacionaFija.ReadOnly = false;
                    txtTarifaTiempoEsperaNacionaVariable.ReadOnly = false;
                    txtTarifaTiempoEsperaInternacinalFija.ReadOnly = false;
                    txtTarifaTiempoEsperaInternacinalNacional.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "rdlListTarifaCoroEspera_SelectedIndexChanged", "Aviso");
            }
        }

        protected void rdlListTarifaCobro_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {

                if (rdlListTarifaCobro.Value.S().I() == 0)
                {
                    txtTarifaNacionalFija.ReadOnly = true;
                    txtTarifaNacionalVarable.ReadOnly = true;
                    txtTarifaInternacionalFija.ReadOnly = true;
                    txtTarifaInternacionalVariable.ReadOnly = true;

                    txtTarifaNacionalFija.Text = "0";
                    txtTarifaNacionalVarable.Text = "0";
                    txtTarifaInternacionalFija.Text = "0";
                    txtTarifaInternacionalVariable.Text = "0";
                }
                else
                {
                    txtTarifaNacionalFija.ReadOnly = false;
                    txtTarifaNacionalVarable.ReadOnly = false;
                    txtTarifaInternacionalFija.ReadOnly = false;
                    txtTarifaInternacionalVariable.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "rdlListTarifaCobro_SelectedIndexChanged1", "Aviso");
            }
        }

        protected void gvTarifaCombustible_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrudCombustible = e;
                if (eValidaCombustible != null)
                    eValidaCombustible(sender, e);

                if (bDuplicaCombustibleInternacional)
                {
                    AddError(e.Errors, gvTarifaCombustible.Columns["Fecha"], "La fecha ya existe, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTarifaCombustible_RowValidating", "Aviso");
            }
        }

        protected void gvIntercambios_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {

                oCrudIntercambio = e;
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                if (eDeleteIntercambio != null)
                    eDeleteIntercambio(sender, e);
                gvIntercambios.DataSource = dtIntercambios;
                gvIntercambios.DataBind();
                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvIntercambios_RowDeleting", "Aviso");
            }
        }

        protected void gvIntercambios_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {

                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                ASPxPageControl pageControl = gvIntercambios.FindEditFormTemplateControl("gvIntercambioEdit") as ASPxPageControl;
                ASPxComboBox cmb = pageControl.FindControl("cboIntercambioEditorGrupoModelo") as ASPxComboBox;
                if (!cmb.IsValid)
                {
                    CancelEditing(e);
                    return;
                }
                e.NewValues["IdGrupoModelo"] = cmb.SelectedItem.Value;
                ASPxTextBox txtFactor = pageControl.FindControl("txtIntercambiosEditorFactor") as ASPxTextBox;
                e.NewValues["Factor"] = txtFactor.Text;
                ASPxCheckBox chkFerry = pageControl.FindControl("chkIntercambiosEditorFerry") as ASPxCheckBox;
                e.NewValues["Ferry"] = chkFerry.Checked;
                ASPxTextBox txtTarifaNal = pageControl.FindControl("txtIntercambiosEditorTarifaNacionalDll") as ASPxTextBox;
                e.NewValues["TarifaNal"] = txtTarifaNal.Text;
                ASPxTextBox txtTarifaInt = pageControl.FindControl("txtIntercambiosTarifaInternacionalDll") as ASPxTextBox;
                e.NewValues["TarifaInt"] = txtTarifaInt.Text;
                ASPxTextBox txtGalones = pageControl.FindControl("txtTarifasEditorCostoDirecto") as ASPxTextBox;
                e.NewValues["Galones"] = txtGalones.Text;
                ASPxTextBox txtCDN = pageControl.FindControl("txtCostoDirectoNacional") as ASPxTextBox;
                e.NewValues["CDN"] = txtCDN.Text;
                ASPxTextBox txtCDI = pageControl.FindControl("txtCostoDirectointernacional") as ASPxTextBox;
                e.NewValues["CDI"] = txtCDI.Text;
                oCrudIntercambio = e;
                if (eUpdateIntercambio != null)
                    eUpdateIntercambio(sender, e);
                CancelEditing(e);
                gvIntercambios.DataSource = dtIntercambios;
                gvIntercambios.DataBind();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvIntercambios_RowUpdating", "Aviso");
            }
        }

        protected void gvIntercambios_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                ASPxPageControl pageControl = gvIntercambios.FindEditFormTemplateControl("gvIntercambioEdit") as ASPxPageControl;
                ASPxComboBox cmb = pageControl.FindControl("cboIntercambioEditorGrupoModelo") as ASPxComboBox;
                if (!cmb.IsValid)
                {
                    e.Cancel = true;
                    gvIntercambios.CancelEdit();
                    return;
                }
                e.NewValues["IdGrupoModelo"] = cmb.SelectedItem.Value;
                ASPxTextBox txtFactor = pageControl.FindControl("txtIntercambiosEditorFactor") as ASPxTextBox;
                e.NewValues["Factor"] = txtFactor.Text;
                ASPxCheckBox chkFerry = pageControl.FindControl("chkIntercambiosEditorFerry") as ASPxCheckBox;
                e.NewValues["Ferry"] = chkFerry.Checked;
                ASPxTextBox txtTarifaNal = pageControl.FindControl("txtIntercambiosEditorTarifaNacionalDll") as ASPxTextBox;
                e.NewValues["TarifaNal"] = txtTarifaNal.Text;
                ASPxTextBox txtTarifaInt = pageControl.FindControl("txtIntercambiosTarifaInternacionalDll") as ASPxTextBox;
                e.NewValues["TarifaInt"] = txtTarifaInt.Text;
                ASPxTextBox txtGalones = pageControl.FindControl("txtTarifasEditorCostoDirecto") as ASPxTextBox;
                e.NewValues["Galones"] = txtGalones.Text;
                ASPxTextBox txtCDN = pageControl.FindControl("txtCostoDirectoNacional") as ASPxTextBox;
                e.NewValues["CDN"] = txtCDN.Text;
                ASPxTextBox txtCDI = pageControl.FindControl("txtCostoDirectointernacional") as ASPxTextBox;
                e.NewValues["CDI"] = txtCDI.Text;
                oCrudIntercambio = e;
                if (eSaveIntercambio != null)
                    eSaveIntercambio(sender, e);
                CancelEditing(e);
                gvIntercambios.DataSource = dtIntercambios;
                gvIntercambios.DataBind();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvIntercambios_RowInserting", "Aviso");
            }

        }

        protected void gvIntercambios_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {

                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvIntercambios_CellEditorInitialize", "Aviso");
            }
        }

        protected void gvIntercambios_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {

                ASPxPageControl pageControl = gvIntercambios.FindEditFormTemplateControl("gvIntercambioEdit") as ASPxPageControl;
                ASPxComboBox cmb = pageControl.FindControl("cboIntercambioEditorGrupoModelo") as ASPxComboBox;
                if (cmb.SelectedIndex < 0)
                {
                    AddError(e.Errors, gvIntercambios.Columns["IdGrupoModelo"], "Hasta debe ser mayor que Desde, favor de validarlo.");

                    cmb.IsValid = false;
                    cmb.ErrorText = "El campo es requerido";
                    return;
                }

                bool bTarifa = false;
                bool bCobro = false;

                decimal dTN = 0m;
                decimal dTI = 0m;
                decimal dGal = 0m;
                decimal dCDN = 0m;
                decimal dCDI = 0m;
                ASPxTextBox txtTarifaNal = pageControl.FindControl("txtIntercambiosEditorTarifaNacionalDll") as ASPxTextBox;
                ASPxTextBox txtTarifaInt = pageControl.FindControl("txtIntercambiosTarifaInternacionalDll") as ASPxTextBox;
                ASPxTextBox txtGalones = pageControl.FindControl("txtTarifasEditorCostoDirecto") as ASPxTextBox;
                ASPxTextBox txtCDN = pageControl.FindControl("txtCostoDirectoNacional") as ASPxTextBox;
                ASPxTextBox txtCDI = pageControl.FindControl("txtCostoDirectointernacional") as ASPxTextBox;

                dTN = txtTarifaNal.Text.D();
                dTI = txtTarifaInt.Text.D();
                dGal = txtGalones.Text.D();
                dCDN = txtCDN.Text.D();
                dCDI = txtCDI.Text.D();

                bTarifa = ((dTN > 0) || (dTI > 0));
                bCobro = ((dGal > 0) || (dCDN > 0) || (dCDI > 0));
                if (bTarifa && bCobro)
                {
                    txtTarifaNal.IsValid = false;
                    txtTarifaInt.IsValid = false;
                    txtGalones.IsValid = false;
                    txtCDN.IsValid = false;
                    txtCDI.IsValid = false;
                    cmb.IsValid = false;

                    txtTarifaNal.ErrorText = "Solo se puede seleccionar un método de descuento, favor de validarlo";
                    txtTarifaInt.ErrorText = "Solo se puede seleccionar un método de descuento, favor de validarlo";
                    txtGalones.ErrorText = "Solo se puede seleccionar un método de descuento, favor de validarlo";
                    txtCDN.ErrorText = "Solo se puede seleccionar un método de descuento, favor de validarlo";
                    txtCDI.ErrorText = "Solo se puede seleccionar un método de descuento, favor de validarlo";
                    cmb.ErrorText = "Solo se puede seleccionar un método de descuento, favor de validarlo";
                    AddError(e.Errors, gvIntercambios.Columns["IdGrupoModelo"], "Hasta debe ser mayor que Desde, favor de validarlo.");
                    return;
                }
                if (cmb.SelectedIndex > 0)
                {
                    e.NewValues["IdGrupoModelo"] = cmb.SelectedItem.Value;
                    oCrudIntercambio = e;
                    if (eValidaIntercambio != null)
                        eValidaIntercambio(sender, e);

                    if (bDuplicaIntercambio)
                    {
                        cmb.IsValid = false;
                        cmb.ErrorText = "Ya existe un intercambio con este grupo modelo, favor de validarlo";
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvIntercambios_RowValidating", "Aviso");
            }
        }

        protected void gvIntercambios_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvIntercambios_CancelRowEditing", "Aviso");
            }
        }

        protected void gvRangosCombustible_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {

                e.Editor.ReadOnly = false;
                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    if (e.Column.FieldName == "IdModelo")
                    {
                        ASPxComboBox cmb = e.Editor as ASPxComboBox;
                        if (eSearchModelos != null)
                            eSearchModelos(sender, e);
                    }
                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }
                if (e.Column.FieldName == "IdGrupoModelo")
                {
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.Callback += new CallbackEventHandlerBase(cmbModelo_OnCallback);
                    if (eSearchModelos != null)
                        eSearchModelos(sender, e);
                    cmb.DataSource = dtModelo;
                    cmb.ValueField = "GrupoModeloId";
                    cmb.ValueType = typeof(int);
                    cmb.TextField = "Descripcion";
                    cmb.DataBind();
                }


            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangosCombustible_CellEditorInitialize", "Aviso");
            }
        }

        protected void gvRangosCombustible_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {

                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrudIntercambio = e;

                if (eDeleteRangos != null)
                    eDeleteRangos(sender, e);
                gvRangosCombustible.DataSource = dtRangos;
                gvRangosCombustible.DataBind();
                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangosCombustible_RowDeleting", "Aviso");
            }
        }

        protected void gvRangosCombustible_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrudRangos = e;

                if (eUpdateRangos != null)
                    eUpdateRangos(sender, e);
                gvRangosCombustible.DataSource = dtRangos;
                gvRangosCombustible.DataBind();
                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangosCombustible_RowUpdating", "Aviso");
            }

        }

        protected void gvRangosCombustible_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {

                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrudRangos = e;

                if (eSaveRangos != null)
                    eSaveRangos(sender, e);
                gvRangosCombustible.DataSource = dtRangos;
                gvRangosCombustible.DataBind();
                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangosCombustible_RowInserting", "Aviso");
            }
        }

        protected void gvRangosCombustible_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {

                oCrudRangos = e;
                if (eValidaRangos != null)
                    eValidaRangos(sender, e);
                if (e.NewValues["Hasta"].S().D() <= e.NewValues["Desde"].S().D())
                {
                    AddError(e.Errors, gvRangosCombustible.Columns["Desde"], "Hasta debe ser mayor que Desde, favor de validarlo.");
                    return;
                }
                if (bduplicaRango)
                {

                    AddError(e.Errors, gvRangosCombustible.Columns["IdMarca"], "El rango ya existe, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangosCombustible_RowValidating", "Aviso");
            }

        }

        void cmbBase_OnCallback(object source, CallbackEventArgsBase e)
        {
            try
            {
                sOrigen = e.Parameter.S();
                FillCombo(source as ASPxComboBox, e.Parameter);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "cmbBase_OnCallback", "Aviso");
            }
        }

        protected void btnGeneralesSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (!bCanEdit)
                {
                    ASPxPageControl1.TabPages[1].Enabled = true;
                    ASPxPageControl1.ActiveTabIndex = 1;
                }
                else
                {
                    DataRow[] drResults = dtBasesSeleccionadas.Select("TipoBase = 1");
                    if (drResults.Length == 0)
                    {
                        mpeMensaje.ShowMessage("Favor de ingresar la base predeterminada", "Aviso");
                        return;
                    }
                    if (!bActualizaGenerales)
                    {
                        if (eValidaContrato != null)
                            eValidaContrato(sender, e);
                        if (bDuplicaClaveContrato)
                        {
                            txtGeneralesContrato.IsValid = false;
                            txtGeneralesContrato.ValidationSettings.ErrorText = "El contrato ya existe.";
                            return;
                        }

                        if (txtGeneralesContrato.Text.Length != 5)
                        {
                            txtGeneralesContrato.IsValid = false;
                            return;
                        }

                        if (txtGeneralesHorasAcumulables.Text.D() > 100)
                        {
                            txtGeneralesHorasAcumulables.IsValid = false;
                            return;
                        }

                        if (eSaveGenerales != null)
                            eSaveGenerales(sender, e);
                    }
                    else
                    {
                        if (eUpdateGenerales != null)
                            eUpdateGenerales(sender, e);

                    }

                    // Le indicamos que vuelva a reargar los datos ya que no recuperaba el contrato que se habia insertado o acualizado
                    if (bCanEdit)
                    {
                        if (eGetContratoEdicion != null)
                            eGetContratoEdicion(sender, EventArgs.Empty);
                    }

                    cboTarifaCalculoPrecioCombustible.ValidationSettings.RequiredField.IsRequired = true;
                    cboTarifaCalculoPrecioCombustible.IsValid = true;

                    ASPxPageControl1.TabPages[1].Enabled = true;
                    ASPxPageControl1.ActiveTabIndex = 1;
                    bActualizaGenerales = true;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvBase_CellEditorInitialize", "Aviso");
            }
        }

        protected void btnTarifaAnterior_Click(object sender, EventArgs e)
        {
            try
            {


                ASPxPageControl1.ActiveTabIndex = 0;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnTarifaAnterior_Click", "Aviso");
            }
        }

        protected void btnTarifaSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (!bCanEdit)
                {
                    ASPxPageControl1.TabPages[2].Enabled = true;
                    ASPxPageControl1.ActiveTabIndex = 2;
                }
                else
                {
                    if (!bActualizaTarifa)
                    {
                        if (eSaveTarifa != null)
                            eSaveTarifa(sender, e);
                    }
                    else
                    {
                        if (eUpdateTarifas != null)
                            eUpdateTarifas(sender, e);
                    }

                    if (dtServicioConCargo == null)
                    {
                        if (eGetServicioConCargo != null)
                            eGetServicioConCargo(sender, e);
                    }


                    gvServicioConCargo.DataSource = dtServicioConCargo;
                    gvServicioConCargo.DataBind();
                    bActualizaTarifa = true;
                    ASPxPageControl1.TabPages[2].Enabled = true;
                    ASPxPageControl1.ActiveTabIndex = 2;
                }

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnTarifaSiguiente_Click", "Aviso");
            }
        }

        protected void btnAnteriorCobros_Click(object sender, EventArgs e)
        {
            try
            {

                ASPxPageControl1.TabPages[1].Enabled = true;
                ASPxPageControl1.ActiveTabIndex = 1;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAnteriorCobros_Click", "Aviso");
            }
        }

        protected void btnSigCobros_Click(object sender, EventArgs e)
        {
            try
            {
                if (!bCanEdit)
                {
                    ASPxPageControl1.TabPages[3].Enabled = true;
                    ASPxPageControl1.ActiveTabIndex = 3;
                }
                else
                {
                    if (!bActualizaCobro)
                    {
                        if (eSaveCobros != null)
                            eSaveCobros(sender, e);
                    }
                    else
                    {
                        if (eUpdateCobros != null)
                            eUpdateCobros(sender, e);
                    }

                    bActualizaCobro = true;
                    ASPxPageControl1.TabPages[3].Enabled = true;
                    ASPxPageControl1.ActiveTabIndex = 3;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSigCobros_Click", "Aviso");
            }

        }

        protected void btnAntIntercambio_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxPageControl1.TabPages[2].Enabled = true;
                ASPxPageControl1.ActiveTabIndex = 2;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAntIntercambio_Click", "Aviso");
            }
        }

        protected void btnSigIntercambio_Click(object sender, EventArgs e)
        {
            try
            {

                bActualizaIntercambio = true;
                ASPxPageControl1.TabPages[4].Enabled = true;
                ASPxPageControl1.ActiveTabIndex = 4;


                // AGREGAMOS EL SIGUIENTE BLOQUE DE CODIGO PARA INSERTAR LAS NOTAS EN CONTRATOS
                if (!bActualizaGenerales)
                {
                    if (eValidaContrato != null)
                        eValidaContrato(sender, e);
                    if (bDuplicaClaveContrato)
                    {
                        txtGeneralesContrato.IsValid = false;
                        txtGeneralesContrato.ValidationSettings.ErrorText = "El contrato ya existe.";
                        return;
                    }

                    if (txtGeneralesContrato.Text.Length != 5)
                    {
                        txtGeneralesContrato.IsValid = false;
                        return;
                    }

                    if (txtGeneralesHorasAcumulables.Text.D() > 100)
                    {
                        txtGeneralesHorasAcumulables.IsValid = false;
                        return;
                    }

                    if (eSaveGenerales != null)
                        eSaveGenerales(sender, e);
                }
                else
                {
                    if (eUpdateGenerales != null)
                        eUpdateGenerales(sender, e);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSigIntercambio_Click", "Aviso");
            }

        }

        protected void btnAntGiras_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxPageControl1.TabPages[3].Enabled = true;
                ASPxPageControl1.ActiveTabIndex = 3;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAntGiras_Click", "Aviso");
            }

        }

        protected void btnSigGiras_Click(object sender, EventArgs e)
        {
            try
            {
                if (!bCanEdit)
                {
                    ASPxPageControl1.TabPages[5].Enabled = true;
                    ASPxPageControl1.ActiveTabIndex = 5;
                }
                else
                {
                    if (!bActualizaGiras)
                    {
                        if (eSaveGiras != null)
                            eSaveGiras(sender, null);
                    }
                    else
                    {
                        if (eUpdateGiras != null)
                            eUpdateGiras(sender, e);
                    }
                    bActualizaGiras = true;
                    ASPxPageControl1.TabPages[5].Enabled = true;
                    ASPxPageControl1.ActiveTabIndex = 5;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSigGiras_Click", "Aviso");
            }

        }

        protected void btnAntRango_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxPageControl1.TabPages[4].Enabled = true;
                ASPxPageControl1.ActiveTabIndex = 4;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAntRango_Click", "Aviso");
            }

        }

        protected void btnSigRango_Click(object sender, EventArgs e)
        {
            try
            {

                bActualizaRangos = true;
                ASPxPageControl1.TabPages[6].Enabled = true;
                ASPxPageControl1.ActiveTabIndex = 6;


                // AGREGAMOS EL SIGUIENTE BLOQUE DE CODIGO PARA INSERTAR LAS NOTAS EN CONTRATOS
                if (!bActualizaGenerales)
                {
                    if (eValidaContrato != null)
                        eValidaContrato(sender, e);
                    if (bDuplicaClaveContrato)
                    {
                        txtGeneralesContrato.IsValid = false;
                        txtGeneralesContrato.ValidationSettings.ErrorText = "El contrato ya existe.";
                        return;
                    }

                    if (txtGeneralesContrato.Text.Length != 5)
                    {
                        txtGeneralesContrato.IsValid = false;
                        return;
                    }

                    if (txtGeneralesHorasAcumulables.Text.D() > 100)
                    {
                        txtGeneralesHorasAcumulables.IsValid = false;
                        return;
                    }

                    if (eSaveGenerales != null)
                        eSaveGenerales(sender, e);
                }
                else
                {
                    if (eUpdateGenerales != null)
                        eUpdateGenerales(sender, e);
                }

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSigRango_Click", "Aviso");
            }
        }

        protected void btnAntCaracteristicas_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxPageControl1.TabPages[5].Enabled = true;
                ASPxPageControl1.ActiveTabIndex = 5;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAntCaracteristicas_Click", "Aviso");
            }

        }

        protected void btnSigCaracteristicas_Click(object sender, EventArgs e)
        {
            try
            {
                bActualizaCaracteristicas = true;
                ASPxPageControl1.TabPages[7].Enabled = true;
                ASPxPageControl1.ActiveTabIndex = 7;

                if (!bCanEdit)
                {

                }
                else
                {
                    if (!bActualizaCaracteristicas)
                    {
                        if (eSaveCaracteristicas != null)
                            eSaveCaracteristicas(sender, e);
                    }
                    else
                    {
                        if (eUpdateCaracteristicas != null)
                            eUpdateCaracteristicas(sender, e);
                    }

                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSigCaracteristicas_Click", "Aviso");
            }
        }

        protected void btnAntPreferencias_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxPageControl1.TabPages[6].Enabled = true;
                ASPxPageControl1.ActiveTabIndex = 6;
            }
            catch (Exception ex)
            {

                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAntPreferencias_Click", "Aviso");
            }
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!bActualizaPreferencias)
                {
                    if (eSavePreferencias != null)
                        eSavePreferencias(sender, e);
                }
                else
                {
                    if (eUpdatePreferencias != null)
                        eUpdatePreferencias(sender, e);
                }

                Response.Redirect("~/Views/Consultas/frmConsultaContrato.aspx");
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnTerminar_Click", "Aviso");
            }

        }

        protected void gvBase_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            e.Enabled = bCanEdit;
        }

        protected void gvTramoPactado_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            e.Enabled = bCanEdit;
        }

        protected void gvIntercambios_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            e.Enabled = bCanEdit;
        }

        protected void gvRangosCombustible_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            e.Enabled = bCanEdit;
        }

        protected void gvTarifaCombustible_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            e.Enabled = bCanEdit;
        }

        protected void upContrato_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            try
            {
                if (e.IsValid)
                {
                    Session["VSbArchivo"] = null;
                    Session["VSNombreArchivo"] = null;
                    Session["VSbArchivo"] = e.UploadedFile.FileBytes;
                    Session["VSNombreArchivo"] = e.UploadedFile.FileName.S();
                    lblMensajeFile.Text = "El archivo" + e.UploadedFile.FileName.S() + " se adjunto.";
                    lblMensajeFile.BackColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "upContrato_FileUploadComplete", "Aviso");
            }
        }

        protected void lkbtnDownloadPDF_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bPDF = (byte[])Session["VSbArchivo"];
                if (bPDF != null)
                {
                    MemoryStream ms = new MemoryStream(bPDF);
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + lkbtnDownloadPDF.Text);
                    Response.ContentType = "application/octet-stream";
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //ms.WriteTo(Response.OutputStream);
                    Response.BinaryWrite(bPDF);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "lkbtnDownloadPDF_Click", "Aviso");
            }
        }

        protected void rblFactorCombustible_ValueChanged(object sender, EventArgs e)
        {
            if (rblFactorCombustible.Value.S() == "2")
            {
                txtFactorTramNal.Text = "1";
                txtFactorTramInt.Text = "1";

                txtFactorTramNal.ReadOnly = true;
                txtFactorTramInt.ReadOnly = true;
            }
            else
            {
                txtFactorTramNal.ReadOnly = false;
                txtFactorTramInt.ReadOnly = false;
            }
        }

        #endregion

        #region "METODOS"
        public void ObtieneValores()
        {
            try
            {

                if (eSearchObj != null)
                    eSearchObj(null, EventArgs.Empty);

                cboGeneralesClientes.DataSource = dtCliente;
                cboGeneralesClientes.ValueField = "IdCliente";
                cboGeneralesClientes.TextField = "CodigoCliente";
                cboGeneralesClientes.DataBind();

                cboGeneralesEjcutivo.DataSource = dtEjecutivo;
                cboGeneralesEjcutivo.ValueField = "IdEjecutivo";
                cboGeneralesEjcutivo.TextField = "Nombre";
                cboGeneralesEjcutivo.DataBind();

                cboGeneralesModelo.DataSource = dtGrupoModelo;
                cboGeneralesModelo.ValueField = "GrupoModeloId";
                cboGeneralesModelo.TextField = "Descripcion";
                cboGeneralesModelo.DataBind();

                cboGeneralesVendedor.DataSource = dtVendedor;
                cboGeneralesVendedor.ValueField = "IdVendedor";
                cboGeneralesVendedor.TextField = "Nombre";
                cboGeneralesVendedor.DataBind();

                cboGeneralesPaquete.DataSource = dtPaquete;
                cboGeneralesPaquete.ValueField = "IdTipoPaquete";
                cboGeneralesPaquete.TextField = "Descripcion";
                cboGeneralesPaquete.DataBind();

                cboTarifaCostoDirectoInternacional.DataSource = dtInflacion;
                cboTarifaCostoDirectoInternacional.ValueField = "id";
                cboTarifaCostoDirectoInternacional.TextField = "Descripcion";
                cboTarifaCostoDirectoInternacional.DataBind();

                cboTarifaCostoDirectro.DataSource = dtInflacion;
                cboTarifaCostoDirectro.ValueField = "id";
                cboTarifaCostoDirectro.TextField = "Descripcion";
                cboTarifaCostoDirectro.DataBind();

                cboTarifaFijoAnual.DataSource = dtInflacion;
                cboTarifaFijoAnual.ValueField = "id";
                cboTarifaFijoAnual.TextField = "Descripcion";
                cboTarifaFijoAnual.DataBind();

                cboGeneralesEstatusCliente.DataSource = dtEstatusContratos;
                cboGeneralesEstatusCliente.ValueField = "IdEstatus";
                cboGeneralesEstatusCliente.TextField = "Descripcion";
                cboGeneralesEstatusCliente.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void LlenaControlesFacturacion()
        {
            try
            {
                ddlMetodoPago.DataSource = dsCatFacturacion.Tables[0];
                ddlMetodoPago.ValueField = "Clave";
                ddlMetodoPago.TextField = "Descripcion";
                ddlMetodoPago.DataBind();

                ddlFormaPago.DataSource = dsCatFacturacion.Tables[1];
                ddlFormaPago.ValueField = "Clave";
                ddlFormaPago.TextField = "Descripcion";
                ddlFormaPago.DataBind();

                ddlUsoCFDI.DataSource = dsCatFacturacion.Tables[2];
                ddlUsoCFDI.ValueField = "Clave";
                ddlUsoCFDI.TextField = "Descripcion";
                ddlUsoCFDI.DataBind();

                ddlFormatoEdoCta.DataSource = dsCatFacturacion.Tables[3];
                ddlFormatoEdoCta.ValueField = "Clave";
                ddlFormatoEdoCta.TextField = "Descripcion";
                ddlFormatoEdoCta.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GeneraDataTableAeronabeBase()
        {
            try
            {
                DataTable dtAeronave = new DataTable();
                dtAeronave.Columns.Add("IdBase");
                dtAeronave.Columns.Add("TipoBase");
                dtAeronave.Columns.Add("Aeropuerto");
                dtBasesSeleccionadas = dtAeronave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeneraDataTableTramosPactado()
        {
            try
            {

                DataTable dtTramo = new DataTable();
                dtTramo.Columns.Add("IdTramo");
                dtTramo.Columns.Add("Origen");
                dtTramo.Columns.Add("Destino");
                dtTramo.Columns.Add("Tiempo");
                dtTramosPactados = dtTramo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeneraDataTableTarifaCombustible()
        {
            try
            {

                DataTable dtTarifaCombustibleNuevo = new DataTable();
                dtTarifaCombustibleNuevo.Columns.Add("IdCombustible");
                dtTarifaCombustibleNuevo.Columns.Add("Fecha");
                dtTarifaCombustibleNuevo.Columns.Add("Importe");
                dtTarifaCombustible = dtTarifaCombustibleNuevo;
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
                gvBase.DataSource = null;
                ViewState["oDatos"] = null;

                gvBase.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvBase.DataBind();
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
                gvBase.JSProperties["cpCaption"] = sCaption;
                gvBase.JSProperties["cpText"] = sMensaje;
                gvBase.JSProperties["cpShowPopup"] = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void CancelEditing(CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                gvBase.CancelEdit();
                gvTarifaCombustible.CancelEdit();
                gvTramoPactado.CancelEdit();
                gvIntercambios.CancelEdit();
                gvRangosCombustible.CancelEdit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            try
            {
                if (errors.ContainsKey(column)) return;
                errors[column] = errorText;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void ValidaRecuperacionContrato()
        {
            try
            {

                txtPernoctaFactorConversionNacional.IsValid = true;
                if (rdlDescuentanPernoctasInternacional.Checked)
                {
                    txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.IsRequired = true;
                    txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                    txtPernoctaFactorConversionInternacional.ReadOnly = false;
                }
                else
                {
                    txtPernoctaFactorConversionInternacional.ValidationSettings.RequiredField.IsRequired = false;
                    txtPernoctaFactorConversionInternacional.ReadOnly = true;

                }

                txtPernoctaFactorConversionNacional.IsValid = true;
                if (rdlDescuentanPernoctasNacional.Checked)
                {
                    txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.IsRequired = true;
                    txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                    txtPernoctaFactorConversionNacional.ReadOnly = false;
                }
                else
                {
                    txtPernoctaFactorConversionNacional.ValidationSettings.RequiredField.IsRequired = false;
                    txtPernoctaFactorConversionNacional.ReadOnly = true;
                }
                txtTiempoEsperaFactorHrsVueloNal.IsValid = true;
                if (rdlDescuentanTiempoEsperaNacional.Checked)
                {

                    txtTiempoEsperaFactorHrsVueloNal.ValidationSettings.RequiredField.IsRequired = true;
                    txtTiempoEsperaFactorHrsVueloNal.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                    txtTiempoEsperaFactorHrsVueloNal.ReadOnly = false;
                }
                else
                {
                    txtTiempoEsperaFactorHrsVueloNal.ValidationSettings.RequiredField.IsRequired = false;
                    txtTiempoEsperaFactorHrsVueloNal.ReadOnly = true;

                }

                txtTiempoEsperaFactorHrsVueloInt.IsValid = true;
                if (rdlDescuentanTiempoEsperaInternaacional.Checked)
                {
                    txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.IsRequired = true;
                    txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                    txtTiempoEsperaFactorHrsVueloInt.ReadOnly = false;
                }
                else
                {
                    txtTiempoEsperaFactorHrsVueloInt.ValidationSettings.RequiredField.IsRequired = false;
                    txtTiempoEsperaFactorHrsVueloInt.ReadOnly = true;

                }


                if (chkGirasAplicaEspera.Checked)
                {
                    chkGiraAplicaHorario.Checked = false;
                    chkGiraAplicaHorario.ReadOnly = true;
                    txtGiraHoraInicio.ReadOnly = true;
                    txtGiraHoraFin.ReadOnly = true;
                    lblGiraNumeroVesesTiempoVuelo.Text = "Número de veces de tiempo de vuelo*";
                    spnGiraNumeroVesesTiempoVuelo.ValidationSettings.RequiredField.IsRequired = true;
                    spnGiraNumeroVesesTiempoVuelo.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                }
                else
                {
                    chkGiraAplicaHorario.ReadOnly = false;
                    txtGiraHoraInicio.ReadOnly = false;
                    txtGiraHoraFin.ReadOnly = false;
                    lblGiraNumeroVesesTiempoVuelo.Text = "Número de veces de tiempo de vuelo";
                    spnGiraNumeroVesesTiempoVuelo.ValidationSettings.RequiredField.IsRequired = false;
                    spnGiraNumeroVesesTiempoVuelo.IsValid = true;
                }


                if (chkGiraAplicaHorario.Checked)
                {
                    chkGirasAplicaEspera.ReadOnly = true;
                    spnGiraNumeroVesesTiempoVuelo.ReadOnly = true;
                    txtGiraHoraFin.ValidationSettings.RequiredField.IsRequired = true;
                    txtGiraHoraFin.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                    txtGiraHoraInicio.ValidationSettings.RequiredField.IsRequired = true;
                    txtGiraHoraInicio.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                    lblGiraHoraInicio.Text = "Hora de Inicio*";
                    lblGiraHoraFin.Text = "Hora de Fin*";

                }
                else
                {
                    chkGiraAplicaHorario.ReadOnly = true;
                    txtGiraHoraInicio.ReadOnly = true;
                    txtGiraHoraFin.ReadOnly = true;
                    txtGiraHoraInicio.ValidationSettings.RequiredField.IsRequired = false;
                    txtGiraHoraFin.ValidationSettings.RequiredField.IsRequired = false;
                    txtGiraHoraInicio.IsValid = true;
                    txtGiraHoraFin.IsValid = true;
                    lblGiraHoraInicio.Text = "Hora de Inicio";
                    lblGiraHoraFin.Text = "Hora de Fin";
                }


                if (ChkGiraAplicaFactorFechaPico.Checked)
                    lblGiraFactorFechaPico.Text = "Factor x Fecha Pico*";
                else
                {
                    lblGiraFactorFechaPico.Text = "Factor x Fecha Pico";
                    txtGiraFactorFechaPico.IsValid = true;
                }


                txtGiraFactorFechaPico.ValidationSettings.RequiredField.IsRequired = ChkGiraAplicaFactorFechaPico.Checked;
                txtGiraFactorFechaPico.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";


                if (cboTarifaCalculoPrecioCombustible.SelectedIndex == 1 || cboTarifaCalculoPrecioCombustible.SelectedIndex == 2)
                {
                    txtTarifaConsumo.ValidationSettings.RequiredField.IsRequired = true;
                    txtTarifaConsumo.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
                    txtTarifaConsumo.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                    txtTarifaConsumo.ValidationSettings.RequiredField.ErrorText = "El campo es requerido";
                    txtTarifaConsumo.ValidationSettings.ErrorText = "Error en la información ingresada";
                    lblTarifaConsumo.Text = "Consumo Galones/Hr:*";
                }
                else
                {
                    txtTarifaConsumo.ValidationSettings.RequiredField.IsRequired = false;
                    lblTarifaConsumo.Text = "Consumo Galones/Hr:";
                }

                ValidaRecuperacionContratoTarifasTiempoEspera();
                ValidaRecuperacionContratosPernoctas();
                gvTramoPactado.Visible = chkCobrosAplicaTramo.Checked;



                txtGiraFactorFechaPico.ReadOnly = !ChkGiraAplicaFactorFechaPico.Checked;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidaRecuperacionContratosPernoctas()
        {
            if (rdlListTarifaCobro.Value.S().I() == 0)
            {
                txtTarifaNacionalFija.ReadOnly = true;
                txtTarifaNacionalVarable.ReadOnly = true;
                txtTarifaInternacionalFija.ReadOnly = true;
                txtTarifaInternacionalVariable.ReadOnly = true;

                txtTarifaNacionalFija.Text = "0";
                txtTarifaNacionalVarable.Text = "0";
                txtTarifaInternacionalFija.Text = "0";
                txtTarifaInternacionalVariable.Text = "0";
            }
            else
            {

                if (!string.IsNullOrEmpty(txtTarifaNacionalFija.Text) && txtTarifaNacionalFija.Text.D() > 0)
                {
                    txtTarifaNacionalVarable.ReadOnly = true;

                }
                else
                {

                    txtTarifaNacionalVarable.ReadOnly = false;
                }

                if (!string.IsNullOrEmpty(txtTarifaNacionalVarable.Text) && txtTarifaNacionalVarable.Text.D() > 0)
                {
                    txtTarifaNacionalFija.ReadOnly = true;

                }
                else
                {

                    txtTarifaNacionalFija.ReadOnly = false;
                }


                if (!string.IsNullOrEmpty(txtTarifaInternacionalFija.Text) && txtTarifaInternacionalFija.Text.D() > 0)
                {
                    txtTarifaInternacionalVariable.ReadOnly = true;
                }
                else
                {
                    txtTarifaInternacionalVariable.ReadOnly = false;
                }

                if (!string.IsNullOrEmpty(txtTarifaInternacionalVariable.Text) && txtTarifaInternacionalVariable.Text.D() > 0)
                {
                    txtTarifaInternacionalFija.ReadOnly = true;
                }
                else
                {
                    txtTarifaInternacionalFija.ReadOnly = false;
                }


            }

        }

        public void ValidaRecuperacionContratoTarifasTiempoEspera()
        {
            if (rdlListTarifaCoroEspera.Value.S().I() == 0)
            {
                txtTarifaTiempoEsperaNacionaFija.ReadOnly = true;
                txtTarifaTiempoEsperaNacionaVariable.ReadOnly = true;
                txtTarifaTiempoEsperaInternacinalFija.ReadOnly = true;
                txtTarifaTiempoEsperaInternacinalNacional.ReadOnly = true;

                txtTarifaTiempoEsperaNacionaFija.Text = "0";
                txtTarifaTiempoEsperaNacionaVariable.Text = "0";
                txtTarifaTiempoEsperaInternacinalFija.Text = "0";
                txtTarifaTiempoEsperaInternacinalNacional.Text = "0";
            }
            else
            {

                if (!string.IsNullOrEmpty(txtTarifaTiempoEsperaNacionaFija.Text) && txtTarifaTiempoEsperaNacionaFija.Text.D() > 0)
                {
                    txtTarifaTiempoEsperaNacionaVariable.ReadOnly = true;
                    txtTarifaTiempoEsperaNacionaVariable.Text = "0";

                }
                else
                {
                    txtTarifaTiempoEsperaNacionaFija.Text = "0";
                    txtTarifaTiempoEsperaNacionaVariable.ReadOnly = false;
                }


                if (!string.IsNullOrEmpty(txtTarifaTiempoEsperaNacionaVariable.Text) && txtTarifaTiempoEsperaNacionaVariable.Text.D() > 0)
                {
                    txtTarifaTiempoEsperaNacionaFija.ReadOnly = true;
                    txtTarifaTiempoEsperaNacionaFija.Text = "0";
                }
                else
                {
                    txtTarifaTiempoEsperaNacionaVariable.Text = "0";
                    txtTarifaTiempoEsperaNacionaFija.ReadOnly = false;
                }


                if (!string.IsNullOrEmpty(txtTarifaTiempoEsperaInternacinalFija.Text) && txtTarifaTiempoEsperaInternacinalFija.Text.D() > 0)
                {
                    txtTarifaTiempoEsperaInternacinalNacional.ReadOnly = true;
                }
                else
                {
                    txtTarifaTiempoEsperaInternacinalFija.Text = "0";
                    txtTarifaTiempoEsperaInternacinalNacional.ReadOnly = false;
                }

                if (!string.IsNullOrEmpty(txtTarifaTiempoEsperaInternacinalNacional.Text) && txtTarifaTiempoEsperaInternacinalNacional.Text.D() > 0)
                {
                    txtTarifaTiempoEsperaInternacinalFija.ReadOnly = true;
                }
                else
                {
                    txtTarifaTiempoEsperaInternacinalNacional.Text = "0";
                    txtTarifaTiempoEsperaInternacinalFija.ReadOnly = false;
                }
            }
        }

        protected void DisableControls(Control c, bool status)
        {
            gvServicioConCargo.Enabled = false;
            foreach (Control ctrl in c.Controls)
            {
                if (ctrl is ASPxTextBox)
                    ((ASPxTextBox)ctrl).Enabled = status;
                else if (ctrl is ASPxRadioButton)
                    ((ASPxRadioButton)ctrl).Enabled = status;
                else if (ctrl is ASPxRadioButtonList)
                    ((ASPxRadioButtonList)ctrl).Enabled = status;
                else if (ctrl is ASPxCheckBox)
                    ((ASPxCheckBox)ctrl).Enabled = status;
                else if (ctrl is ASPxCheckBoxList)
                    ((ASPxCheckBoxList)ctrl).Enabled = status;
                else if (ctrl is ASPxComboBox)
                    ((ASPxComboBox)ctrl).Enabled = status;
                else if (ctrl is ASPxHyperLink)
                    ((ASPxHyperLink)ctrl).Enabled = status;
                else if (ctrl is ASPxSpinEdit)
                    ((ASPxSpinEdit)ctrl).Enabled = status;
                else if (ctrl is ASPxMemo)
                    ((ASPxMemo)ctrl).Enabled = status;
                else if (ctrl is ASPxDateEdit)
                    ((ASPxDateEdit)ctrl).Enabled = status;

                if (ctrl.HasControls())
                    DisableControls(ctrl, status);
            }
        }

        private void DisableControls(Control ctrl)
        {
            DisableControls(ctrl, false);
        }

        public void HabilitaPermisosContrato(DataTable dt)
        {
            foreach (DataRow item in dt.Rows)
            {
                bool bEnabled = item["AccesoSeccion"].S() == "1" ? true : false;
                Control ctl = FindControlRecursive(ASPxPageControl1, item["NombreSeccion"].S());

                if (ctl != null)
                {
                    if (ctl is Panel)
                        ((Panel)ctl).Enabled = bEnabled;
                }
            }


            foreach (DataRow item in dt.Rows)
            {
                bool bEnabled = item["AccesoCampo"].S() == "1" ? true : false;
                Control ctl = FindControlRecursive(ASPxPageControl1, item["NombreControl"].S());

                if (ctl != null)
                {
                    if (ctl is ASPxTextBox)
                        ((ASPxTextBox)ctl).Enabled = bEnabled;
                    else if (ctl is ASPxRadioButton)
                        ((ASPxRadioButton)ctl).Enabled = bEnabled;
                    else if (ctl is ASPxRadioButtonList)
                        ((ASPxRadioButtonList)ctl).Enabled = bEnabled;
                    else if (ctl is ASPxCheckBox)
                        ((ASPxCheckBox)ctl).Enabled = bEnabled;
                    else if (ctl is ASPxCheckBoxList)
                        ((ASPxCheckBoxList)ctl).Enabled = bEnabled;
                    else if (ctl is ASPxComboBox)
                        ((ASPxComboBox)ctl).Enabled = bEnabled;
                    else if (ctl is ASPxHyperLink)
                        ((ASPxHyperLink)ctl).Enabled = bEnabled;
                    else if (ctl is ASPxSpinEdit)
                        ((ASPxSpinEdit)ctl).Enabled = bEnabled;
                    else if (ctl is ASPxMemo)
                        ((ASPxMemo)ctl).Enabled = bEnabled;
                    else if (ctl is ASPxDateEdit)
                        ((ASPxDateEdit)ctl).Enabled = bEnabled;
                    else if (ctl is ASPxGridView)
                        ((ASPxGridView)ctl).Enabled = bEnabled;
                    else if (ctl is ASPxUploadControl)
                        ((ASPxUploadControl)ctl).Enabled = bEnabled;
                    else if (ctl is LinkButton)
                        ((LinkButton)ctl).Enabled = bEnabled;

                }
            }
        }

        public static Control FindControlRecursive(Control Root, string Id)
        {
            if (Root.ID == Id)
                return Root;
            
            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Ctl, Id);
                if (FoundCtl != null)
                {
                    return FoundCtl;
                }
            }

            return null;
        }

        #endregion

        #region "Vars y Propiedades"
        Contrato_Presenter oPresenter;
        public event EventHandler eGetContratoEdicion;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetServicioConCargo;
        public event EventHandler eGetEjecutivo;
        public event EventHandler eGetVendedor;
        public event EventHandler eGetPaquete;
        public event EventHandler eGetGrupoModelo;
        public event EventHandler eGetBase;
        public event EventHandler eSaveGenerales;
        public event EventHandler eSaveTarifa;
        public event EventHandler eGetAeropuertoOrigen;
        public event EventHandler eGetAeropuertoDestino;
        public event EventHandler eGetAeropuertoOrigenFiltrado;
        public event EventHandler eGetAeropuertoDestinoFiltrado;
        public event EventHandler eValidaContrato;
        public event EventHandler eSaveTarifas;
        public event EventHandler eSaveTramo;
        public event EventHandler eSaveIntercambio;
        public event EventHandler eSaveRangos;
        public event EventHandler eUpdateTarifas;
        public event EventHandler eNewTramo;
        public event EventHandler eNewIntercambio;
        public event EventHandler eNewRangos;
        public event EventHandler eDeleteTarifas;
        public event EventHandler eDeleteTramo;
        public event EventHandler eDeleteIntercambio;
        public event EventHandler eDeleteRangos;
        public event EventHandler eSaveCobros;
        public event EventHandler eUpdateCobros;
        public event EventHandler eSaveCombustible;
        public event EventHandler eUpdateCombustible;
        public event EventHandler eDeleteCombustible;
        public event EventHandler eUpdateGenerales;
        public event EventHandler eSaveBases;
        public event EventHandler eUpdateBases;
        public event EventHandler eSearchCombustibleInternacional;
        public event EventHandler eSaveCombustibleInternacional;
        public event EventHandler eUpdateCombustibleInternacional;
        public event EventHandler eDeleteCombustibleInternacional;
        public event EventHandler eSearchTramo;
        public event EventHandler eUpdateTramo;
        public event EventHandler eSearchIntercambio;
        public event EventHandler eUpdateIntercambio;
        public event EventHandler eSaveGiras;
        public event EventHandler eUpdateGiras;
        public event EventHandler eSearchRangos;
        public event EventHandler eUpdateRangos;
        public event EventHandler eSaveCaracteristicas;
        public event EventHandler eUpdateCaracteristicas;
        public event EventHandler eValidaCombustible;
        public event EventHandler eValidaTramo;
        public event EventHandler eValidaIntercambio;
        public event EventHandler eValidaRangos;
        public event EventHandler eSearchModelos;
        public event EventHandler eSavePreferencias;
        public event EventHandler eUpdatePreferencias;
        public event EventHandler eGetDatosFacturacion;
        public event EventHandler eGetPermisosContrato;

        public DataTable dtEstatusContratos
        {
            get
            {
                return (DataTable)ViewState["dtEstatusContratos"];
            }
            set
            {
                ViewState["dtEstatusContratos"] = value;
            }
        }
        public bool bDuplicaCombustibleInternacional
        {
            get
            {
                return (bool)ViewState["bDuplicaCombustibleInternacional"];
            }
            set { ViewState["bDuplicaCombustibleInternacional"] = value; }
        }
        public bool bDuplicaTramo
        {
            get
            {
                return (bool)ViewState["bDuplicaTramo"];
            }
            set { ViewState["bDuplicaTramo"] = value; }
        }
        public bool bDuplicaIntercambio
        {
            get
            {
                return (bool)ViewState["bDuplicaIntercambio"];
            }
            set { ViewState["bDuplicaIntercambio"] = value; }
        }
        public int iIdMarca
        {
            get
            {
                return (int)ViewState["iIdMarca"];
            }
            set
            {
                ViewState["iIdMarca"] = value;
            }
        }
        public object oCrud
        {
            get { return ViewState["CrudPiloto"]; }
            set { ViewState["CrudPiloto"] = value; }
        }
        public object oCrudTarifa
        {
            get { return ViewState["CrudTarifa"]; }
            set { ViewState["CrudTarifa"] = value; }
        }
        public object oCrudTamoPactado
        {
            get { return ViewState["CrudTamoPactado"]; }
            set { ViewState["CrudTamoPactado"] = value; }
        }
        public object oCrudIntercambio
        {
            get { return ViewState["CrudIntercambio"]; }
            set { ViewState["CrudIntercambio"] = value; }
        }
        public object oCrudRangos
        {
            get { return ViewState["CrudRangos"]; }
            set { ViewState["CrudRangos"] = value; }
        }
        public object oCrudBases
        {
            get { return ViewState["oCrudBases"]; }
            set { ViewState["oCrudBases"] = value; }
        }
        public bool bDuplicado
        {
            get
            {
                return (bool)ViewState["RegistroDuplicado"];
            }
            set
            {
                ViewState["RegistroDuplicado"] = value;
            }
        }
        public ALE_MexJet.Objetos.Enumeraciones.TipoOperacion eCrud
        {
            get { return (ALE_MexJet.Objetos.Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }
        public object[] oArrFiltros
        {
            get
            {
                int iEstatus = -1;
                string sDescripcion = string.Empty;

                //switch (ddlTipoBusqueda.SelectedValue.S())
                //{
                //    case "1":
                //        iEstatus = -1;
                //        sDescripcion = txtTextoBusqueda.Text.S();
                //        break;
                //    case "2":
                //        iEstatus = 1;
                //        sDescripcion = string.Empty;
                //        break;
                //    case "3":
                //        iEstatus = 0;
                //        sDescripcion = string.Empty;
                //        break;

                //}
                return new object[]{
                                        "@Descripcion", "%" + sDescripcion + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }
        private const string sPagina = "frmContratos.aspx";
        private const string sClase = "frmContratos.aspx.cs";
        public bool bCanEdit
        {
            get
            {
                return (bool)ViewState["IsEdit"];
            }
            set
            {
                ViewState["IsEdit"] = value;
            }
        }
        public DataTable dtCliente
        {
            get
            {
                return (DataTable)ViewState["Cliente"];
            }
            set
            {
                ViewState["Cliente"] = value;
            }
        }
        public DataTable dtBase
        {
            get
            {
                return (DataTable)ViewState["Base"];
            }
            set
            {
                ViewState["Base"] = value;
            }
        }
        public DataTable dtEjecutivo
        {
            get
            {
                return (DataTable)ViewState["Ejecutivo"];
            }
            set
            {
                ViewState["Ejecutivo"] = value;
            }
        }
        public DataTable dtVendedor
        {
            get
            {
                return (DataTable)ViewState["Vendedor"];
            }
            set
            {
                ViewState["Vendedor"] = value;
            }
        }
        public DataTable dtRangos
        {
            get
            {
                return (DataTable)ViewState["dtRangos"];
            }
            set
            {
                ViewState["dtRangos"] = value;
            }
        }
        public DataTable dtMarca
        {
            get
            {
                return (DataTable)ViewState["dtMarca"];
            }
            set
            {
                ViewState["dtMarca"] = value;
            }
        }
        public DataTable dtModelo
        {
            get
            {
                return (DataTable)ViewState["dtModelo"];
            }
            set
            {
                ViewState["dtModelo"] = value;
            }
        }
        public DataTable dtInflacion
        {
            get
            {
                return (DataTable)ViewState["Inflacion"];
            }
            set
            {
                ViewState["Inflacion"] = value;
            }
        }
        public DataTable dtIntercambios
        {
            get
            {
                return (DataTable)ViewState["dtIntercambios"];
            }
            set
            {
                ViewState["dtIntercambios"] = value;
            }
        }
        public DataTable dtPaquete
        {
            get
            {
                return (DataTable)ViewState["Paquete"];
            }
            set
            {
                ViewState["Paquete"] = value;
            }
        }
        public DataTable dtGrupoModelo
        {
            get
            {
                return (DataTable)ViewState["GrupoModelo"];
            }
            set
            {
                ViewState["GrupoModelo"] = value;
            }
        }
        public DataTable dtServicioConCargo
        {
            get
            {
                return (DataTable)ViewState["ServicioConCargo"];
            }
            set
            {
                ViewState["ServicioConCargo"] = value;
            }
        }
        public DataTable dtBasesSeleccionadas
        {
            get
            {
                return (DataTable)Session["GridBases"];
            }
            set
            {
                Session["GridBases"] = value;
            }
        }
        public DataTable dtTarifaCombustible
        {
            get
            {
                return (DataTable)Session["TarifaCombustible"];
            }
            set
            {
                Session["TarifaCombustible"] = value;
            }
        }
        public DataTable dtTarifaOrigen
        {
            get
            {
                return (DataTable)Session["Origen"];
            }
            set
            {
                Session["Origen"] = value;
            }
        }
        public DataTable dtTarifaDestino
        {
            get
            {
                return (DataTable)Session["Destino"];
            }
            set
            {
                Session["Destino"] = value;
            }
        }
        public DataTable dtTramosPactados
        {
            get
            {
                return (DataTable)Session["TramosPactados"];
            }
            set
            {
                Session["TramosPactados"] = value;
            }
        }

        public DataSet dsCatFacturacion
        {
            get
            {
                return (DataSet)ViewState["VSCatsFacturacion"];
            }
            set
            {
                ViewState["VSCatsFacturacion"] = value;
            }
        }
        public int iIdContrato
        {
            get
            {
                return (int)ViewState["iIdContrato"];
            }
            set
            {
                ViewState["iIdContrato"] = value;
            }
        }
        public Contrato_Generales objContratosGenerales
        {
            get
            {
                Contrato_Generales objContratosGenerales = new Contrato_Generales();
                objContratosGenerales.sContrato = txtGeneralesContrato.Text;
                objContratosGenerales.iIdCliente = cboGeneralesClientes.SelectedItem.Value.S().I();
                objContratosGenerales.iiDVendedor = cboGeneralesVendedor.SelectedItem.Value.S().I();
                objContratosGenerales.dtFechaContrato = DtGeneralesFechaContrato.Date;
                objContratosGenerales.iIdEjecutivo = cboGeneralesEjcutivo.SelectedItem.Value.S().I();
                objContratosGenerales.dtFechaInicioVuelo = dtGeneralesFechaInicioVuelo.Date;
                objContratosGenerales.iIdPAquete = cboGeneralesPaquete.SelectedItem.Value.S().I();
                objContratosGenerales.iIdGrupoModelo = cboGeneralesModelo.SelectedItem.Value.S().I();
                objContratosGenerales.iAñoContratados = spinGeneralesAñosContrato.Text.S().I();
                objContratosGenerales.iMesesGracia = spinGeneralesMesesGracia.Text.I();
                objContratosGenerales.iHorasContratadasTotal = txtGeneralesHorasContratadasTot.Text.I();
                objContratosGenerales.iHorasContratadasAño = txtGeneralesHorasContratadasAño.Text.I();
                objContratosGenerales.dHorasNoUsadasAcumulables = txtGeneralesHorasAcumulables.Text.D();
                objContratosGenerales.sMatricula = txtGeneralesMatricula.Text;
                objContratosGenerales.iIdTipoCambio = cboGeneralesMonedaPago.SelectedItem.Value.S().I();
                objContratosGenerales.dAnticipioInicial = txtGeneralesAnticipo.Text.D();
                objContratosGenerales.dFijoAnual = txtGeneralesFijoAnual.Text.D();
                objContratosGenerales.dRenovacion = txtGeneralesRenovacion.Text.D();
                objContratosGenerales.dPrenda = txtGeneralesPrenda.Text.D();
                objContratosGenerales.dIncrementoCostoDirectoRenovacion = txtGeneraesCostoDirPorRenovacion.Text.D();
                objContratosGenerales.bReasigna = chkReasigna.Checked;

                List<Contratos_Bases> lstbases = new List<Contratos_Bases>();
                Contratos_Bases objBase = new Contratos_Bases();

                foreach (DataRow drRow in dtBasesSeleccionadas.Rows)
                {
                    objBase = new Contratos_Bases();
                    objBase.sAeropuerto = drRow[2].S();
                    objBase.iPredeterminada = drRow[1].S().I();
                    lstbases.Add(objBase);
                }
                objContratosGenerales.lstBases = lstbases;
                objContratosGenerales.sNotas = txtGeneralesMemo.Text;
                objContratosGenerales.sNombreArchivo = Session["VSNombreArchivo"] != null ? Session["VSNombreArchivo"].S() : string.Empty;
                objContratosGenerales.bContratoD = Session["VSbArchivo"] != null ? (byte[])Session["VSbArchivo"] : new byte[1];
                objContratosGenerales.iStatus = cboGeneralesEstatusCliente.Value.S().I();

                objContratosGenerales.sNotasIntercambios = txtIntercambioMemo.Text.S();
                objContratosGenerales.sNotasRangoCombustible = txtRangosCombustibleMemo.Text.S();

                objContratosGenerales.sMetodoPagoFact = ddlMetodoPago.SelectedItem.Value.S();
                objContratosGenerales.sFormaPago = ddlFormaPago.SelectedItem.Value.S();
                objContratosGenerales.sUsoCFDI = ddlUsoCFDI.SelectedItem.Value.S();
                objContratosGenerales.sFormatoEdoCta = ddlFormatoEdoCta.SelectedItem.Value.S();

                return objContratosGenerales;
            }
            set
            {
                try
                {
                    Contrato_Generales objContratosGenerales = value;

                    txtGeneralesContrato.Text = objContratosGenerales.sContrato;
                    //cboGeneralesClientes.SelectedIndex=cboGeneralesClientes.Items.IndexOfValue(objContratosGenerales.iIdCliente.S());
                    cboGeneralesClientes.Value = objContratosGenerales.iIdCliente.S();
                    cboGeneralesVendedor.Value = objContratosGenerales.iiDVendedor.S();
                    DtGeneralesFechaContrato.Date = objContratosGenerales.dtFechaContrato;
                    cboGeneralesEjcutivo.Value = objContratosGenerales.iIdEjecutivo.S();
                    dtGeneralesFechaInicioVuelo.Date = objContratosGenerales.dtFechaInicioVuelo;
                    cboGeneralesPaquete.Value = objContratosGenerales.iIdPAquete.S();
                    cboGeneralesModelo.Value = objContratosGenerales.iIdGrupoModelo.S();
                    spinGeneralesAñosContrato.Text = objContratosGenerales.iAñoContratados.S();
                    spinGeneralesMesesGracia.Text = objContratosGenerales.iMesesGracia.S();
                    txtGeneralesHorasContratadasTot.Text = objContratosGenerales.iHorasContratadasTotal.S();
                    txtGeneralesHorasContratadasAño.Text = objContratosGenerales.iHorasContratadasAño.S();
                    txtGeneralesHorasAcumulables.Text = objContratosGenerales.dHorasNoUsadasAcumulables.S();
                    txtGeneralesMatricula.Text = objContratosGenerales.sMatricula;
                    cboGeneralesMonedaPago.Value = objContratosGenerales.iIdTipoCambio.S();
                    txtGeneralesAnticipo.Text = objContratosGenerales.dAnticipioInicial.S();
                    txtGeneralesFijoAnual.Text = objContratosGenerales.dFijoAnual.S();
                    txtGeneralesRenovacion.Text = objContratosGenerales.dRenovacion.S();
                    txtGeneralesPrenda.Text = objContratosGenerales.dPrenda.S();
                    txtGeneraesCostoDirPorRenovacion.Text = objContratosGenerales.dIncrementoCostoDirectoRenovacion.S();
                    chkReasigna.Checked = objContratosGenerales.bReasigna;

                    List<Contratos_Bases> lstbases = value.lstBases;

                    DataTable dtBaseGeerales = dtBasesSeleccionadas;
                    DataRow drBase = dtBaseGeerales.NewRow();
                    foreach (Contratos_Bases objBases in lstbases)
                    {
                        drBase = dtBaseGeerales.NewRow();
                        drBase["IdBase"] = objBases.iId;
                        drBase["Aeropuerto"] = objBases.sAeropuerto;
                        drBase["Tipobase"] = objBases.iPredeterminada;

                        dtBaseGeerales.Rows.Add(drBase);
                    }
                    dtBasesSeleccionadas = dtBaseGeerales;
                    txtGeneralesMemo.Text = objContratosGenerales.sNotas;
                    cboGeneralesEstatusCliente.Value = objContratosGenerales.iStatus.S();

                    //Cargamos el PDF
                    lkbtnDownloadPDF.Text = objContratosGenerales.sNombreArchivo;
                    Session["VSbArchivo"] = objContratosGenerales.bContratoD;

                    txtIntercambioMemo.Text = objContratosGenerales.sNotasIntercambios;
                    txtRangosCombustibleMemo.Text = objContratosGenerales.sNotasRangoCombustible;

                    //if (objContratosGenerales.sMetodoPagoFact != string.Empty)
                    ddlMetodoPago.Value = objContratosGenerales.sMetodoPagoFact;

                    //if (objContratosGenerales.sFormaPago != string.Empty)
                    ddlFormaPago.Value = objContratosGenerales.sFormaPago;

                    //if (objContratosGenerales.sUsoCFDI != string.Empty)
                    ddlUsoCFDI.Value = objContratosGenerales.sUsoCFDI;

                    ddlFormatoEdoCta.Value = objContratosGenerales.sFormatoEdoCta;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public Contrato_Tarifas objContratosTarifas
        {
            get
            {
                Contrato_Tarifas objTarifa = new Contrato_Tarifas();
                objTarifa.iIdContrato = iIdContrato;
                objTarifa.dCostoDirNal = txtTarifasCostoDirNac.Text.D();
                objTarifa.dCostoDirInt = txtTarifasCostoDirInter.Text.D();
                objTarifa.bCombustible = rdlTarifasCombustible.Checked;
                objTarifa.iTipoCalculo = cboTarifaCalculoPrecioCombustible.Value.S().I();
                objTarifa.dConsumoGalones = txtTarifaConsumo.Text.D();
                objTarifa.dFactorTramosNal = txtFactorTramNal.Text.S().D();
                objTarifa.dFactorTramosInt = txtFactorTramInt.Text.S().D();

                objTarifa.bAplicaFactorCombustible = rblFactorCombustible.Value.S().I();

                objTarifa.bPrecioInternacionalEspecial = chkTarifasPrecioEspecial.Checked;
                objTarifa.bCobraTiempoEspera = rdlListTarifaCoroEspera.Value == "1";
                objTarifa.dTiempoEsperaFijaNal = txtTarifaTiempoEsperaNacionaFija.Text.D();
                objTarifa.dTiempoEsperaFijaInt = txtTarifaTiempoEsperaInternacinalFija.Text.D();
                objTarifa.dTiempoEsperaVarNal = txtTarifaTiempoEsperaNacionaVariable.Text.D();
                objTarifa.dTiempoEsperaVarInt = txtTarifaTiempoEsperaInternacinalNacional.Text.D();
                objTarifa.bCobraPernoctas = rdlListTarifaCobro.Value == "1";
                objTarifa.dPernoctasFijaNal = txtTarifaNacionalFija.Text.D();
                objTarifa.dPernoctasFijaInt = txtTarifaInternacionalFija.Text.D();
                objTarifa.dPernoctasVarNal = txtTarifaNacionalVarable.Text.D();
                objTarifa.dPernoctasVarInt = txtTarifaInternacionalVariable.Text.D();

                objTarifa.iCDNBaseInflacion = cboTarifaCostoDirectro.Value.S().I();
                objTarifa.dCDNPorcentaje = txtTarifaCostoDirectoNacionalPorcentajeNacional.Text.D();
                objTarifa.dCDNPuntos = txtTarifaCostoDirectoNacionalPuntos.Text.D();
                objTarifa.dCDNTopeMAximo = txtTarifaCostoDirectoNacionalTopeMaximo.Text.D();

                objTarifa.iCDIBaseInflacion = cboTarifaCostoDirectoInternacional.Value.S().I();
                objTarifa.dCDIPorcentaje = txtTarifaCostoDirectoInternacionalPorcentaje.Text.D();
                objTarifa.dCDIPuntos = txtTarifaCostoDirectoInternacionalPuntos.Text.D();
                objTarifa.dCDITopeMAximo = txtTarifaCostoDirectoInternacionalTopeMaximo.Text.D();

                objTarifa.iFABaseInflacion = cboTarifaFijoAnual.Value.S().I();
                objTarifa.dFAPorcentaje = txtTarifaFijoanualPorcentaje.Text.D();
                objTarifa.dFAPuntos = txtTarifaFijoAnualPuntos.Text.D();
                objTarifa.dFATopeMAximo = txtTarfaFijoAnualMaximo.Text.D();

                objTarifa.sNotas = txtTarifasMemo.Text.S();
                objTarifa.iAplicaIncremento = rblIncrementoTarifa.Value.I();

                /*
                if (rdbIncrementoEnero.Checked)
                    objTarifa.iAplicaIncremento = 1;
                if (rdbIncrementoAniversario.Checked)
                    objTarifa.iAplicaIncremento = 2;
                if (rdbIncrementoNunca.Checked)
                    objTarifa.iAplicaIncremento = 3;
                */

                return objTarifa;
            }
            set
            {
                Contrato_Tarifas objTarifa = value;
                if (objTarifa.iIdContrato < 0)
                {
                    bActualizaTarifa = false;
                    return;
                }
                txtTarifasCostoDirNac.Text = objTarifa.dCostoDirNal.S();
                txtTarifasCostoDirInter.Text = objTarifa.dCostoDirInt.S();
                rdlTarifasCombustible.Checked = objTarifa.bCombustible;
                rdlTarifasNoCombustible.Checked = !objTarifa.bCombustible;
                cboTarifaCalculoPrecioCombustible.Value = objTarifa.iTipoCalculo.S();
                txtTarifaConsumo.Text = objTarifa.dConsumoGalones.S();

                txtFactorTramNal.Text = objTarifa.dFactorTramosNal.S();
                txtFactorTramInt.Text = objTarifa.dFactorTramosInt.S();

                chkTarifasPrecioEspecial.Checked = objTarifa.bPrecioInternacionalEspecial;
                gvTarifaCombustible.Visible = chkTarifasPrecioEspecial.Checked;

                if (objTarifa.bCobraTiempoEspera)
                {
                    rdlListTarifaCoroEspera.Items[0].Selected = true;
                }
                else
                {
                    rdlListTarifaCoroEspera.Items[1].Selected = true;
                }

                txtTarifaTiempoEsperaNacionaFija.Text = objTarifa.dTiempoEsperaFijaNal.S();
                txtTarifaTiempoEsperaInternacinalFija.Text = objTarifa.dTiempoEsperaFijaInt.S();
                txtTarifaTiempoEsperaNacionaVariable.Text = objTarifa.dTiempoEsperaVarNal.S();
                txtTarifaTiempoEsperaInternacinalNacional.Text = objTarifa.dTiempoEsperaVarInt.S();

                if (objTarifa.bCobraPernoctas)
                {
                    rdlListTarifaCobro.Items[0].Selected = true;
                }
                else
                {
                    rdlListTarifaCobro.Items[1].Selected = true;
                }

                txtTarifaNacionalFija.Text = objTarifa.dPernoctasFijaNal.S();
                txtTarifaInternacionalFija.Text = objTarifa.dPernoctasFijaInt.S();
                txtTarifaNacionalVarable.Text = objTarifa.dPernoctasVarNal.S();
                txtTarifaInternacionalVariable.Text = objTarifa.dPernoctasVarInt.S();

                cboTarifaCostoDirectro.Value = objTarifa.iCDNBaseInflacion.S();
                if (objTarifa.iCDNBaseInflacion < 1)
                    cboTarifaCostoDirectro.Value = null;
                txtTarifaCostoDirectoNacionalPorcentajeNacional.Text = objTarifa.dCDNPorcentaje.S();
                txtTarifaCostoDirectoNacionalPuntos.Text = objTarifa.dCDNPuntos.S();
                txtTarifaCostoDirectoNacionalTopeMaximo.Text = objTarifa.dCDNTopeMAximo.S();
                cboTarifaCostoDirectoInternacional.Value = objTarifa.iCDIBaseInflacion.S();
                if (objTarifa.iCDIBaseInflacion < 1)
                    cboTarifaCostoDirectoInternacional.Value = null;
                txtTarifaCostoDirectoInternacionalPorcentaje.Text = objTarifa.dCDIPorcentaje.S();
                txtTarifaCostoDirectoInternacionalPuntos.Text = objTarifa.dCDIPuntos.S();
                txtTarifaCostoDirectoInternacionalTopeMaximo.Text = objTarifa.dCDITopeMAximo.S();
                cboTarifaFijoAnual.Value = objTarifa.iFABaseInflacion.S();
                if (objTarifa.iFABaseInflacion < 1)
                    cboTarifaFijoAnual.Value = null;
                txtTarifaFijoanualPorcentaje.Text = objTarifa.dFAPorcentaje.S();
                txtTarifaFijoAnualPuntos.Text = objTarifa.dFAPuntos.S();
                txtTarfaFijoAnualMaximo.Text = objTarifa.dFATopeMAximo.S();

                txtTarifasMemo.Text = objTarifa.sNotas.S();

                rblFactorCombustible.Value = objTarifa.bAplicaFactorCombustible.S();

                /*
                if (objTarifa.bCobraTiempoEspera)
                {
                    rdlListTarifaCoroEspera.Items[0].Selected = true;
                }
                else
                {
                    rdlListTarifaCoroEspera.Items[1].Selected = true;
                }*/

                if (objTarifa.iAplicaIncremento == 1)
                {
                    rblIncrementoTarifa.Items[0].Selected = true;
                }
                else if (objTarifa.iAplicaIncremento == 2)
                {
                    rblIncrementoTarifa.Items[1].Selected = true;
                }
                else if (objTarifa.iAplicaIncremento == 3)
                {
                    rblIncrementoTarifa.Items[2].Selected = true;
                }
                /*
                rblIncremento.Value = objTarifa.iAplicaIncremento;                
                if (rblIncremento.SelectedItem.Value.ToString() == "1")
                    rblIncremento.SelectedItem.Selected = true;
                if (rblIncremento.SelectedItem.Value.ToString() == "2")
                    rblIncremento.SelectedItem.Selected = true;
                if (rblIncremento.SelectedItem.Value.ToString() == "3")
                    rblIncremento.SelectedItem.Selected = true;
                */

            }
        }
        public Contrato_CobrosDescuentos objCobrosDesc
        {
            get
            {
                Contrato_CobrosDescuentos objCobros = new Contrato_CobrosDescuentos();
                objCobros.iIdContrato = iIdContrato;
                if (rdlCobroFerrysNinguno.Checked)
                    objCobros.iFerrysConCargo = 0;
                if (rdlCobroFerrysTodos.Checked)
                    objCobros.iFerrysConCargo = 1;
                if (rdlCobroFerrysReposicionamiento.Checked)
                    objCobros.iFerrysConCargo = 2;
                objCobros.bAplicaEsperaLibre = chkCobroAplicaEsperaLibre.Checked;
                objCobros.dHorasVuelo = txtCobroHorasVuelo.Text.D();
                objCobros.dFactorHorasVuelo = txtFactorHrsVuelo.Text.D();
                objCobros.bPernoctaNal = rdlDescuentanPernoctasNacional.Checked;
                objCobros.bPernoctaInt = rdlDescuentanPernoctasInternacional.Checked;
                objCobros.dPernoctaFactorConversionNal = txtPernoctaFactorConversionNacional.Text.D();
                objCobros.dPernoctaFactorConversionInt = txtPernoctaFactorConversionInternacional.Text.D();
                objCobros.dNumeroPernoctasLibreAnual = txtDescuentoPernoctasLibres.Text.D();
                objCobros.bPernoctasDescuento = chkListDescuentosPrecnoctas.Items[0].Selected;
                objCobros.bPernoctasCobro = chkListDescuentosPrecnoctas.Items[1].Selected;
                objCobros.bTiempoEsperaNal = rdlDescuentanTiempoEsperaNacional.Checked;
                objCobros.bTiempoEsperaInt = rdlDescuentanTiempoEsperaInternaacional.Checked;
                objCobros.dTiempoEsperaFactorConversionNal = txtTiempoEsperaFactorHrsVueloNal.Text.D();
                objCobros.dTiempoEsperaFactorConversionInt = txtTiempoEsperaFactorHrsVueloInt.Text.D();
                objCobros.bAplicaTramos = chkCobrosAplicaTramo.Checked;

                List<object> lstObj = gvServicioConCargo.GetSelectedFieldValues("IdServicioConCargo");
                List<int> lstIds = new List<int>();
                foreach (object Obj in lstObj)
                {
                    lstIds.Add(Obj.S().I());
                }
                objCobros.lstIdServiciosConCargo = lstIds;

                objCobros.iTiempoFatura = rdlTiempoFacturarCalzo.Checked == true ? 0 : 1;
                objCobros.dMinutos = txtTiempoFacturarMasMinutos.Text.D();

                objCobros.sNotas = txtCobrosDescuentosMemo.Text.S();

                objCobros.iCobroFerrysHelicoptero = rblSeCobraFerryHelicoptero.Value.S().I();
                objCobros.iMasMinutosHelicoptero = txtHelicopteroMasMinutos.Text.S().I();

                return objCobros;

            }
            set
            {
                Contrato_CobrosDescuentos objCobros = value;
                if (objCobros.iIdContrato < 0)
                {
                    bActualizaCobro = false;
                    return;
                }
                switch (objCobros.iFerrysConCargo)
                {
                    case 0:
                        rdlCobroFerrysNinguno.Checked = true;
                        break;
                    case 1:
                        rdlCobroFerrysTodos.Checked = true;
                        break;
                    case 2:
                        rdlCobroFerrysReposicionamiento.Checked = true;
                        break;
                }
                chkCobroAplicaEsperaLibre.Checked = objCobros.bAplicaEsperaLibre;
                txtCobroHorasVuelo.Text = objCobros.dHorasVuelo.S();
                txtFactorHrsVuelo.Text = objCobros.dFactorHorasVuelo.S();
                rdlDescuentanPernoctasNacional.Checked = objCobros.bPernoctaNal;
                rdlNoDescuentanPernoctasNacional.Checked = !objCobros.bPernoctaNal;

                rdlDescuentanPernoctasInternacional.Checked = objCobros.bPernoctaInt;
                rdlNoDescuentanPernoctasInternacional.Checked = !objCobros.bPernoctaInt;

                txtPernoctaFactorConversionNacional.Text = objCobros.dPernoctaFactorConversionNal.S();
                txtPernoctaFactorConversionInternacional.Text = objCobros.dPernoctaFactorConversionInt.S();
                txtDescuentoPernoctasLibres.Text = objCobros.dNumeroPernoctasLibreAnual.S();
                chkListDescuentosPrecnoctas.Items[0].Selected = objCobros.bPernoctasDescuento;
                chkListDescuentosPrecnoctas.Items[1].Selected = objCobros.bPernoctasCobro;
                rdlDescuentanTiempoEsperaNacional.Checked = objCobros.bTiempoEsperaNal;
                rdlNoDescuentanTiempoEsperaNacional.Checked = !objCobros.bTiempoEsperaNal;
                rdlDescuentanTiempoEsperaInternaacional.Checked = objCobros.bTiempoEsperaInt;
                rdlNoDescuentanTiempoEsperaInternaacional.Checked = !objCobros.bTiempoEsperaInt;
                txtTiempoEsperaFactorHrsVueloNal.Text = objCobros.dTiempoEsperaFactorConversionNal.S();
                txtTiempoEsperaFactorHrsVueloInt.Text = objCobros.dTiempoEsperaFactorConversionInt.S();
                foreach (int id in objCobros.lstIdServiciosConCargo)
                {
                    gvServicioConCargo.Selection.SelectRowByKey(id);
                }

                chkCobrosAplicaTramo.Checked = objCobros.bAplicaTramos;
                rdlTiempoFacturarCalzo.Checked = objCobros.iTiempoFatura == 0;
                rdlTiempoFacturarVuelo.Checked = !rdlTiempoFacturarCalzo.Checked;
                txtTiempoFacturarMasMinutos.Text = objCobros.dMinutos.S();

                txtCobrosDescuentosMemo.Text = objCobros.sNotas.S();

                if(objCobros.iCobroFerrysHelicoptero > 0)
                    rblSeCobraFerryHelicoptero.Value = objCobros.iCobroFerrysHelicoptero.S();

                txtHelicopteroMasMinutos.Text = objCobros.iMasMinutosHelicoptero.S();
            }
        }
        public Contrato_GirasFechasPico objGirasFechas
        {
            get
            {
                Contrato_GirasFechasPico objGiras = new Contrato_GirasFechasPico();
                objGiras.iIdContrato = iIdContrato;
                objGiras.bAplicaGiraEspera = chkGirasAplicaEspera.Checked;
                objGiras.bAplicaGiraHora = chkGiraAplicaHorario.Checked;
                objGiras.iNumeroVeces = spnGiraNumeroVesesTiempoVuelo.Text.I();
                objGiras.sHoraInicio = txtGiraHoraInicio.Text.S();
                objGiras.sHoraFin = txtGiraHoraFin.Text.S();
                objGiras.dPorcentajeDescuento = txtGirasPorcentajesDescuentoHoras.Text.D();
                objGiras.bAplicaFactorFechaPico = ChkGiraAplicaFactorFechaPico.Checked;
                objGiras.dFactorFechaPico = txtGiraFactorFechaPico.Text.D();
                objGiras.sNotas = txtGirasFechaPicoMemo.Text.S();
                return objGiras;

            }
            set
            {
                Contrato_GirasFechasPico objGiras = value;
                if (objGiras.iIdContrato < 0)
                {
                    bActualizaGiras = false;
                    return;
                }
                chkGirasAplicaEspera.Checked = objGiras.bAplicaGiraEspera;
                chkGiraAplicaHorario.Checked = objGiras.bAplicaGiraHora;
                spnGiraNumeroVesesTiempoVuelo.Text = objGiras.iNumeroVeces.S();
                txtGiraHoraInicio.Text = objGiras.sHoraInicio;
                txtGiraHoraFin.Text = objGiras.sHoraFin;
                spnGiraNumeroVesesTiempoVuelo.Text = objGiras.iNumeroVeces.S();
                txtGirasPorcentajesDescuentoHoras.Text = objGiras.dPorcentajeDescuento.S();
                ChkGiraAplicaFactorFechaPico.Checked = objGiras.bAplicaFactorFechaPico;
                txtGiraFactorFechaPico.Text = objGiras.dFactorFechaPico.S();
                txtGirasFechaPicoMemo.Text = objGiras.sNotas.S();
            }
        }
        public Contrato_CaracteristicasEspeciales objCaracteristicasEspeciales
        {
            get
            {
                Contrato_CaracteristicasEspeciales objCaracterisiticas = new Contrato_CaracteristicasEspeciales();
                objCaracterisiticas.iIdContrato = iIdContrato;
                objCaracterisiticas.sTiempoMinimoSolicitud = txtCaracteristicasTMSV.Text;
                objCaracterisiticas.dTiempoMinimoSolicitudFBN = Utils.ConvierteTiempoaDecimal(txtTiempoMinSolVloFueraBase.Text.S()).S().D();           // NUEVO
                objCaracterisiticas.sTiempoMinimoSolicitudCA = txtCaracteristicasTMSVCA.Text;
                objCaracterisiticas.sTiempoMinimoSolicitudFeriado = txtCaracteristicasTiempoMinimoAnticipoSolicitud.Text;
                objCaracterisiticas.dRangoAcomodoVuelosFeriado = Utils.ConvierteTiempoaDecimal(txtRangoAcomodoVloDiaFeriado.Text.S()).S().D();         // NUEVO
                objCaracterisiticas.dReprogramarSalidaAntesProgramada = Utils.ConvierteTiempoaDecimal(txtReprogSalidaAntesProg.Text.S()).S().D();      // NUEVO
                objCaracterisiticas.bPenalizacion = rdlCaracteristicasPenalizacionCancelacionExtemporaneaVuelo.Checked;
                objCaracterisiticas.dCancelacionAnticipadaSB = Utils.ConvierteTiempoaDecimal(txtCancelacionAntSalBase.Text.S()).S().D();               // NUEVO
                objCaracterisiticas.dCancelacionAnticipadaFB = Utils.ConvierteTiempoaDecimal(txtCancelacionAntSalFueraBase.Text.S()).S().D();           // NUEVO
                objCaracterisiticas.sPenalizacionAleIncuplimiento = txtCaracterisitcasCaracterisiticasPenalizacionAle.Text;
                objCaracterisiticas.sPenalizacionClienteRetraso = txtCaracterisiticasPenalizacionCliente.Text;
                objCaracterisiticas.sAcuerdosEspeciales = txtCaracteristicasAcuerdosEspeciales.Text;
                objCaracterisiticas.sAntiguedadAeronave = txtCaracterisiticasAñosAntiguedad.Text;
                objCaracterisiticas.bVuelosSimultaneos = chkCaracteristicasDerechosVuelosSimultaneos.Checked;
                objCaracterisiticas.iVuelosSimultaneos = txtCaracteristicasCuantos.Text.I();
                objCaracterisiticas.dFactorVloSim = txtFactorVueloSim.Text.S().D();
                objCaracterisiticas.sComentarios = txtCaracteristicasComentarios.Text;
                objCaracterisiticas.sTiempoMinimoCancelarVuelo = txtCaracteristicasTMCV.Text;
                objCaracterisiticas.sNotas = txtCaracteristicasEspecialesMemo.Text;

                return objCaracterisiticas;
            }
            set
            {
                Contrato_CaracteristicasEspeciales objCaracterisiticas = value;
                if (objGirasFechas.iIdContrato < 0)
                {
                    bActualizaCaracteristicas = false;
                    return;
                }

                txtCaracteristicasTMSV.Text = objCaracterisiticas.sTiempoMinimoSolicitud;
                txtTiempoMinSolVloFueraBase.Text = Utils.ConvierteDecimalATiempo(objCaracterisiticas.dTiempoMinimoSolicitudFBN);       // NUEVO
                txtCaracteristicasTMSVCA.Text = objCaracterisiticas.sTiempoMinimoSolicitudCA;
                txtCaracteristicasTiempoMinimoAnticipoSolicitud.Text = objCaracterisiticas.sTiempoMinimoSolicitudFeriado;
                txtRangoAcomodoVloDiaFeriado.Text = Utils.ConvierteDecimalATiempo(objCaracterisiticas.dRangoAcomodoVuelosFeriado);         // NUEVO
                txtReprogSalidaAntesProg.Text = Utils.ConvierteDecimalATiempo(objCaracterisiticas.dReprogramarSalidaAntesProgramada);      // NUEVO
                rdlCaracteristicasPenalizacionCancelacionExtemporaneaVuelo.Checked = objCaracterisiticas.bPenalizacion;
                txtCancelacionAntSalBase.Text = Utils.ConvierteDecimalATiempo(objCaracterisiticas.dCancelacionAnticipadaSB);               // NUEVO
                txtCancelacionAntSalFueraBase.Text = Utils.ConvierteDecimalATiempo(objCaracterisiticas.dCancelacionAnticipadaFB);           // NUEVO
                txtCaracterisitcasCaracterisiticasPenalizacionAle.Text = objCaracterisiticas.sPenalizacionAleIncuplimiento;
                txtCaracterisiticasPenalizacionCliente.Text = objCaracterisiticas.sPenalizacionClienteRetraso;
                txtCaracteristicasAcuerdosEspeciales.Text = objCaracterisiticas.sAcuerdosEspeciales;
                txtCaracterisiticasAñosAntiguedad.Text = objCaracterisiticas.sAntiguedadAeronave;
                rdlCaracteristicasSinPenalizacionCancelacionExtemporaneaVuelo.Checked = !rdlCaracteristicasPenalizacionCancelacionExtemporaneaVuelo.Checked;
                chkCaracteristicasDerechosVuelosSimultaneos.Checked = objCaracterisiticas.bVuelosSimultaneos;
                txtCaracteristicasCuantos.Text = objCaracterisiticas.iVuelosSimultaneos.S();
                txtFactorVueloSim.Text = objCaracterisiticas.dFactorVloSim.S();
                txtCaracteristicasComentarios.Text = objCaracterisiticas.sComentarios;
                txtCaracteristicasTMCV.Text = objCaracterisiticas.sTiempoMinimoCancelarVuelo;
                txtCaracteristicasEspecialesMemo.Text = objCaracterisiticas.sNotas.S();

            }
        }
        public Contrato_Preferencias objPreferencias
        {
            get
            {
                Contrato_Preferencias objPreferencia = new Contrato_Preferencias();

                objPreferencia.iIdContrato = iIdContrato;
                objPreferencia.iPuntualidad = cbxPreferenciaPuntualidad.SelectedItem != null ? cbxPreferenciaPuntualidad.SelectedItem.Value.S().I() : 0;
                objPreferencia.iTipoProteccion = cbxPreferenciaTipoProteccion.SelectedItem != null ? cbxPreferenciaTipoProteccion.SelectedItem.Value.S().I() : 0;
                objPreferencia.iFlexibilidadCambios = cbxPreferenciasFlexibilidadCambios.SelectedItem != null ? cbxPreferenciasFlexibilidadCambios.SelectedItem.Value.S().I() : 0;
                objPreferencia.iMomentoSolicitaVuelo = cbxPreferenciasMomentoSolicitaVuelo.SelectedItem != null ? cbxPreferenciasMomentoSolicitaVuelo.SelectedItem.Value.S().I() : 0;
                objPreferencia.iTamanioFamilia = cbxPreferenciasTamanioFamilia.SelectedItem != null ? cbxPreferenciasTamanioFamilia.SelectedItem.Value.S().I() : 0;
                objPreferencia.iPreferenciaFBOAeropuerto = cbxPreferenciasFBOAeropuertos.SelectedItem != null ? cbxPreferenciasFBOAeropuertos.SelectedItem.Value.S().I() : 0;
                objPreferencia.iRealizaReservaciones = cbxPreferenciasQuienRealizaReservaciones.SelectedItem != null ? cbxPreferenciasQuienRealizaReservaciones.SelectedItem.Value.S().I() : 0;
                objPreferencia.iPreferenciaContacto = cbxPreferenciasPreferenciaContacto.SelectedItem != null ? cbxPreferenciasPreferenciaContacto.SelectedItem.Value.S().I() : 0;
                objPreferencia.iFavoresClienteALE = cbxPreferenciasFavoresClienteALE.SelectedItem != null ? cbxPreferenciasFavoresClienteALE.SelectedItem.Value.S().I() : 0;
                objPreferencia.iFavoresALECliente = cbxPreferenciasFavoresALECliente.SelectedItem != null ? cbxPreferenciasFavoresALECliente.SelectedItem.Value.S().I() : 0;
                objPreferencia.iAnticipaServicios = rdlPreferenciasAnticipaServicios.SelectedItem != null ? rdlPreferenciasAnticipaServicios.SelectedItem.Value.S().I() : 0;
                objPreferencia.iComisariatoEspecial = rdlPreferenciasComisariatoEspecial.SelectedItem != null ? rdlPreferenciasComisariatoEspecial.SelectedItem.Value.S().I() : 0;
                objPreferencia.iPreocupaCosto = rdlPreferenciaPreocupaCosto.SelectedItem != null ? rdlPreferenciaPreocupaCosto.SelectedItem.Value.S().I() : 0;
                objPreferencia.iTransporteTerrestreEspecial = rdlPreferenciasTransporteTerrestreEspecial.SelectedItem != null ? rdlPreferenciasTransporteTerrestreEspecial.SelectedItem.Value.S().I() : 0;
                objPreferencia.iMascotas = rdlPreferenciasTransporteTerrestreEspecial.SelectedItem != null ? rdlPreferenciasMascotas.SelectedItem.Value.S().I() : 0;
                objPreferencia.iServiciosOtroProveedor = rdlPreferenciaServiciosOtroProveedor.SelectedItem != null ? rdlPreferenciaServiciosOtroProveedor.SelectedItem.Value.S().I() : 0;
                objPreferencia.iAeronavePropia = rdlPreferenciaServiciosOtroProveedor.SelectedItem != null ? rdlPreferenciasAeronavePropia.SelectedItem.Value.S().I() : 0;
                objPreferencia.iIntercambioPagoServicios = rdlPreferenciasIntercambiospagosServicios.SelectedItem != null ? rdlPreferenciasIntercambiospagosServicios.SelectedItem.Value.S().I() : 0;

                return objPreferencia;
            }
            set
            {

                Contrato_Preferencias objPreferencia = value;
                if (objPreferencia.iIdContrato < 0)
                {
                    bActualizaPreferencias = false;
                    return;
                }

                cbxPreferenciaPuntualidad.SelectedIndex = cbxPreferenciaPuntualidad.Items.IndexOf(cbxPreferenciaPuntualidad.Items.FindByValue(objPreferencia.iPuntualidad.S()));
                cbxPreferenciaTipoProteccion.SelectedIndex = cbxPreferenciaTipoProteccion.Items.IndexOf(cbxPreferenciaTipoProteccion.Items.FindByValue(objPreferencia.iTipoProteccion.S()));
                cbxPreferenciasFlexibilidadCambios.SelectedIndex = cbxPreferenciasFlexibilidadCambios.Items.IndexOf(cbxPreferenciasFlexibilidadCambios.Items.FindByValue(objPreferencia.iFlexibilidadCambios.S()));
                cbxPreferenciasMomentoSolicitaVuelo.SelectedIndex = cbxPreferenciasMomentoSolicitaVuelo.Items.IndexOf(cbxPreferenciasMomentoSolicitaVuelo.Items.FindByValue(objPreferencia.iMomentoSolicitaVuelo.S()));
                cbxPreferenciasTamanioFamilia.SelectedIndex = cbxPreferenciasTamanioFamilia.Items.IndexOf(cbxPreferenciasTamanioFamilia.Items.FindByValue(objPreferencia.iTamanioFamilia.S()));
                cbxPreferenciasFBOAeropuertos.SelectedIndex = cbxPreferenciasFBOAeropuertos.Items.IndexOf(cbxPreferenciasFBOAeropuertos.Items.FindByValue(objPreferencia.iPreferenciaFBOAeropuerto.S()));
                cbxPreferenciasQuienRealizaReservaciones.SelectedIndex = cbxPreferenciasQuienRealizaReservaciones.Items.IndexOf(cbxPreferenciasQuienRealizaReservaciones.Items.FindByValue(objPreferencia.iRealizaReservaciones.S()));
                cbxPreferenciasPreferenciaContacto.SelectedIndex = cbxPreferenciasPreferenciaContacto.Items.IndexOf(cbxPreferenciasPreferenciaContacto.Items.FindByValue(objPreferencia.iPreferenciaContacto.S()));
                cbxPreferenciasFavoresClienteALE.SelectedIndex = cbxPreferenciasFavoresClienteALE.Items.IndexOf(cbxPreferenciasFavoresClienteALE.Items.FindByValue(objPreferencia.iFavoresClienteALE.S()));
                cbxPreferenciasFavoresALECliente.SelectedIndex = cbxPreferenciasFavoresALECliente.Items.IndexOf(cbxPreferenciasFavoresALECliente.Items.FindByValue(objPreferencia.iFavoresALECliente.S()));

                rdlPreferenciasAnticipaServicios.SelectedIndex = rdlPreferenciasAnticipaServicios.Items.IndexOf(rdlPreferenciasAnticipaServicios.Items.FindByValue(objPreferencia.iAnticipaServicios.S()));
                rdlPreferenciasComisariatoEspecial.SelectedIndex = rdlPreferenciasComisariatoEspecial.Items.IndexOf(rdlPreferenciasComisariatoEspecial.Items.FindByValue(objPreferencia.iComisariatoEspecial.S()));
                rdlPreferenciaPreocupaCosto.SelectedIndex = rdlPreferenciaPreocupaCosto.Items.IndexOf(rdlPreferenciaPreocupaCosto.Items.FindByValue(objPreferencia.iPreocupaCosto.S()));
                rdlPreferenciasTransporteTerrestreEspecial.SelectedIndex = rdlPreferenciasTransporteTerrestreEspecial.Items.IndexOf(rdlPreferenciasTransporteTerrestreEspecial.Items.FindByValue(objPreferencia.iTransporteTerrestreEspecial.S()));
                rdlPreferenciasMascotas.SelectedIndex = rdlPreferenciasMascotas.Items.IndexOf(rdlPreferenciasMascotas.Items.FindByValue(objPreferencia.iMascotas.S()));
                rdlPreferenciaServiciosOtroProveedor.SelectedIndex = rdlPreferenciaServiciosOtroProveedor.Items.IndexOf(rdlPreferenciaServiciosOtroProveedor.Items.FindByValue(objPreferencia.iServiciosOtroProveedor.S()));
                rdlPreferenciasAeronavePropia.SelectedIndex = rdlPreferenciasAeronavePropia.Items.IndexOf(rdlPreferenciasAeronavePropia.Items.FindByValue(objPreferencia.iAeronavePropia.S()));
                rdlPreferenciasIntercambiospagosServicios.SelectedIndex = rdlPreferenciasIntercambiospagosServicios.Items.IndexOf(rdlPreferenciasIntercambiospagosServicios.Items.FindByValue(objPreferencia.iIntercambioPagoServicios.S()));

            }
        }
        public string sOrigen
        {
            get
            {
                return (string)Session["idOrigen"];
            }
            set
            {
                Session["idOrigen"] = value;
            }
        }
        public string sFiltroAeropuerto
        {
            get
            {
                return ViewState["FiltroAeropuerto"].S();
            }
            set
            {
                ViewState["FiltroAeropuerto"] = value;
            }
        }
        public bool bActualizaGenerales
        {
            set
            {
                ViewState["bActualizaGenerales"] = value;
            }
            get
            {
                return (bool)ViewState["bActualizaGenerales"];
            }
        }
        public bool bActualizaTarifa
        {
            set
            {
                ViewState["bActualizaTarifa"] = value;
            }
            get
            {
                return (bool)ViewState["bActualizaTarifa"];
            }
        }
        public bool bActualizaCobro
        {
            set
            {
                ViewState["bActualizaCobro"] = value;
            }
            get
            {
                return (bool)ViewState["bActualizaCobro"];
            }
        }
        public bool bActualizaIntercambio
        {
            set
            {
                ViewState["bActualizaIntercambio"] = value;
            }
            get
            {
                return (bool)ViewState["bActualizaIntercambio"];
            }
        }
        public bool bActualizaGiras
        {
            set
            {
                ViewState["bActualizaGiras"] = value;
            }
            get
            {
                return (bool)ViewState["bActualizaGiras"];
            }
        }
        public bool bActualizaRangos
        {
            set
            {
                ViewState["bActualizaRangos"] = value;
            }
            get
            {
                return (bool)ViewState["bActualizaRangos"];
            }
        }
        public bool bActualizaCaracteristicas
        {
            set
            {
                ViewState["bActualizaCaracteristicas"] = value;
            }
            get
            {
                return (bool)ViewState["bActualizaCaracteristicas"];
            }
        }
        public bool bActualizaPreferencias
        {
            set
            {
                ViewState["bActualizaPreferencias"] = value;
            }
            get
            {
                return (bool)ViewState["bActualizaPreferencias"];
            }
        }
        public bool bDuplicaClaveContrato
        {
            set
            {
                ViewState["bDuplicaClaveContrato"] = value;
            }
            get
            {
                return (bool)ViewState["bDuplicaClaveContrato"];
            }
        }
        public bool bduplicaRango
        {
            set
            {
                ViewState["bduplicaRango"] = value;
            }
            get
            {
                return (bool)ViewState["bduplicaRango"];
            }
        }
        public object oCrudCombustible
        {
            get { return ViewState["oCrudCombustible"]; }
            set { ViewState["oCrudCombustible"] = value; }
        }



        #endregion

        protected void imbContratosNvos_Click(object sender, ImageClickEventArgs e)
        {
            lblTituloFactores.Text = "Factor combustible Nuevos Contratos";
            DataTable dtFactor = new DBContrato().DBGetObtieneFactoresCombustible(2);
            gvFactoresComb.DataSource = dtFactor;
            gvFactoresComb.DataBind();

            ppFactorCombustible.ShowOnPageLoad = true;
        }

        protected void imbComunicao_Click(object sender, ImageClickEventArgs e)
        {
            lblTituloFactores.Text = "Factor comunicado 1ro de Mayo";
            DataTable dtFactor = new DBContrato().DBGetObtieneFactoresCombustible(1);
            gvFactoresComb.DataSource = dtFactor;
            gvFactoresComb.DataBind();

            ppFactorCombustible.ShowOnPageLoad = true;
        }
    }
}
