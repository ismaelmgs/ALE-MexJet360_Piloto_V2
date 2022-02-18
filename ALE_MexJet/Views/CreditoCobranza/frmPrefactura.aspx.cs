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

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmPrefactura : System.Web.UI.Page, iViewPrefactura
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new Prefactura_Presenter(this, new DBPrefactura());

                gvRemisionesDeVuelo.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvRemisionesDeVuelo.SettingsPager.ShowDisabledButtons = true;
                gvRemisionesDeVuelo.SettingsPager.ShowNumericButtons = true;
                gvRemisionesDeVuelo.SettingsPager.ShowSeparators = true;
                gvRemisionesDeVuelo.SettingsPager.Summary.Visible = true;
                gvRemisionesDeVuelo.SettingsPager.PageSizeItemSettings.Visible = true;
                gvRemisionesDeVuelo.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvRemisionesDeVuelo.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                if (Session["UserIdentity"] == null)
                    Response.Redirect("../frmLogin.aspx");

                ObtieneValores();

                if (IsPostBack)
                {
                    gvDetallesRemision.DataSource = dtDetalleRemisiones;
                    gvDetallesRemision.DataBind();
                    gvRemisionesDeVuelo.DataSource = dtRemisiones;
                    gvRemisionesDeVuelo.DataBind();

                    gvServiciovuelos.DataSource = dtSV;
                    gvServiciovuelos.DataBind();
                    ASPxGridView3.DataSource = dtSC;
                    ASPxGridView3.DataBind();
                }
                else
                {
                    iIdPrefactura = 0;
                    bActualizaFinal = false;
                    sFacturaSCC = string.Empty;
                    sFacturaVuelo = string.Empty;
                    bActualizaGeneral = false;
                    bActualizaSeleccion = false;
                    TipoCambioPrefactura = Utils.GetTipoCambioDia.S().D();
                    string sContratoRecibido = string.Empty;
                    string sTipoEdicion = "0";

                    if (Request.QueryString.Count > 0)
                    {
                        sContratoRecibido = Encoding.UTF8.GetString(Convert.FromBase64String(Request.QueryString["Pref"]));
                        sTipoEdicion = Encoding.UTF8.GetString(Convert.FromBase64String(Request.QueryString["Edi"]));
                    }

                    if (!string.IsNullOrEmpty(sContratoRecibido))
                    {

                        iIdPrefactura = sContratoRecibido.S().I();

                        if (eGetPrefacturas != null)
                            eGetPrefacturas(sender, e);
                        if (eGetContratos != null)
                            eGetContratos(sender, e);

                        cboClaveContrato.DataSource = dtContrato;
                        cboClaveContrato.TextField = "ClaveContrato";
                        cboClaveContrato.ValueField = "IdContrato";
                        cboClaveContrato.DataBind();

                        if (eGetRecuperaRemisionesPrefactura != null)
                            eGetRecuperaRemisionesPrefactura(sender, e);
                        
                        DataRow[] rows = dtRemisiones.Select("IdPrefactura = " + iIdPrefactura.S());
                        List<int> idRemisionSeleccionada = new List<int>();
                        gvRemisionesDeVuelo.DataSource = dtRemisiones;
                        gvRemisionesDeVuelo.DataBind();

                        foreach (DataRow row in rows)
                        {
                            gvRemisionesDeVuelo.Selection.SelectRowByKey(row["IdRemision"].S().I());
                            bActualizaSeleccion = true;
                        }

                        if (bActualizaSeleccion)
                        {
                            ChangeTab(1);
                            ChangeTab(2);
                            
                            if (eGetDetalle != null)
                                eGetDetalle(sender, e);

                            gvDetallesRemision.DataSource = dtDetalleRemisiones;
                            gvDetallesRemision.DataBind();
                        }

                        if (eGetRecuperaPRefacturaServicios != null)
                            eGetRecuperaPRefacturaServicios(sender, e);

                        if (bActualizaFinal)
                        {
                            ChangeTab(3);
                            dFactorIVAV = rows.Length > 0 ? rows[0]["IVA"].S().Replace("%","").S().D() : 0;
                            gvServiciovuelos.DataSource = dtSV;
                            gvServiciovuelos.DataBind();

                            ASPxGridView3.DataSource = dtSC;
                            ASPxGridView3.DataBind();
                        }

                        if (ViewState["VSFactorIVAV"] == null)
                        {
                            if(rows.Length > 0)
                                dFactorIVAV = rows.Length > 0 ? rows[0]["IVA"].S().Replace("%", "").S().D() : 0;
                        }

                        CargaHeader();
                        ValidaTipoPaquete();
                        PreparaEnvioSoloUnaFactura(sTipoEdicion.S().I());
                    }

                }
                bloqueaEdicion();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }
        protected void btnSigRemDet_Click(object sender, EventArgs e)
        {
            try
            {
                if (eGetServiciosVuelo != null)
                    eGetServiciosVuelo(sender, e);
                if (eGetServiciosCargo != null)
                    eGetServiciosCargo(sender, e);
                ActualiaServiciosVuelosMostrar();
                ActualiaServiciosCargosMostrar();
                gvServiciovuelos.DataSource = dtSV;
                gvServiciovuelos.DataBind();
                ASPxGridView3.DataSource = dtSC;
                ASPxGridView3.DataBind();
                if (eUpdateBasePrefactura != null)
                    eUpdateBasePrefactura(sender, e);
                ChangeTab(3);

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSigRemDet_Click", "Aviso");
            }

        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eUpdateBasePrefactura != null)
                    eUpdateBasePrefactura(sender, e);
                if (eSaveServicos != null)
                    eSaveServicos(sender, e);

                Response.Redirect("~/Views/Consultas/frmConsultaPrefactura.aspx");

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAceptar_Click", "Aviso");
            }

        }
        protected void btnSigNueva_Click(object sender, EventArgs e)
        {
            try
            {
                if (iIdPrefactura < 1)
                {
                    if (eSaveBasePrefactura != null)
                        eSaveBasePrefactura(sender, e);
                    if (eGetRemisiones != null)
                        eGetRemisiones(sender, e);
                }
                else
                {
                    if (eUpdateBasePrefactura != null)
                        eUpdateBasePrefactura(sender, e);
                }



                gvRemisionesDeVuelo.DataSource = dtRemisiones;
                gvRemisionesDeVuelo.DataBind();
                ChangeTab(1);
                CargaHeader();
                ValidaTipoPaquete();

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSigNueva_Click", "Aviso");
            }
        }
        protected void btnSigRem_Click1(object sender, EventArgs e)
        {

            try
            {

                if (eGetDetalle != null)
                    eGetDetalle(sender, e);

                gvDetallesRemision.DataSource = dtDetalleRemisiones;
                gvDetallesRemision.DataBind();
                if (eUpdateBasePrefactura != null)
                    eUpdateBasePrefactura(sender, e);
                ChangeTab(2);

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSigRem_Click1", "Aviso");
            }

        }
        protected void cboClaveCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboClaveContrato.DataSource = null;
                cboClaveContrato.Value = null;
                cboClaveContrato.Text = string.Empty;

                cboFacturanteServiciosCargo.DataSource = null;
                cboFacturanteServiciosCargo.Text = string.Empty;
                cboFacturanteServiciosCargo.Value = null;

                cboFacturanteVuelo.DataSource = null;
                cboFacturanteVuelo.Text = string.Empty;
                cboFacturanteVuelo.Value = null;

                eGetContratos(sender, e);

                cboClaveContrato.DataSource = dtContrato;
                cboClaveContrato.TextField = "ClaveContrato";
                cboClaveContrato.ValueField = "IdContrato";
                cboClaveContrato.DataBind();

                GetFacturantes();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "cboClaveCliente_SelectedIndexChanged", "Aviso");
            }

        }
        //protected void cboClaveContrato_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GetFacturantes();

        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "cboClaveContrato_SelectedIndexChanged", "Aviso");
        //    }

        //}
        //protected void cboMonedaVuelo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GetFacturantes();

        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "cboMonedaVuelo_SelectedIndexChanged", "Aviso");
        //    }

        //}
        //protected void cboMonedaServiciosCargo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GetFacturantes();

        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "cboMonedaServiciosCargo_SelectedIndexChanged", "Aviso");
        //    }

        //}
        protected void btnCancelarNueva_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Views/Consultas/frmConsultaPrefactura.aspx");
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnCancelarNueva_Click", "Aviso");
            }
        }
        protected void btnCancelarRem_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeTab(0);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnCancelarRem_Click", "Aviso");
            }
        }
        protected void btnCancelarRemDet_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeTab(1);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnCancelarRemDet_Click", "Aviso");
            }
        }
        protected void btnCancelarPref_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeTab(2);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnCancelarPref_Click", "Aviso");
            }
        }
        protected void btnEnviaPrefactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (objPrefactura.dSubMXNC == 0 && (rdlServicioCargo.Checked && !rdlServiciosAmbos.Checked) && (rdlListCantidadFacturas.Value.S().I() != 1))
                {
                    mpeMensaje.ShowMessage("La factura de servicios con cargo no se generará debido a que no tiene importes", "Alerta");
                    return;
                }

                if (objPrefactura.dSubMXNC == 0 && (rdlServicioCargo.Checked || rdlServiciosAmbos.Checked) && (rdlListCantidadFacturas.Value.S().I() != 1))
                {
                    //mpeMensaje.ShowMessage("La factura de servicios con cargo no se generará debido a que no tiene importes", "Alerta");
                    hdTipoFactura.Value = "2";
                    lblConfirmacion.Text = "La factura de servicios con cargo no generará debido a que tiene importe 0., ¿Desea continuar?";
                    ppConfirmacion.ShowOnPageLoad = true;
                    return;
                }

                if (objPrefactura.dSubDllV == 0 && (rdlServicioVuelo.Checked && !rdlServiciosAmbos.Checked) && (rdlListCantidadFacturas.Value.S().I() != 1))
                {
                    mpeMensaje.ShowMessage("La factura de servicios de vuelo no se generará debido a que no tiene importes.", "Alerta");
                    return;
                }

                if (objPrefactura.dSubDllV == 0 && (rdlServicioVuelo.Checked || rdlServiciosAmbos.Checked) && (rdlListCantidadFacturas.Value.S().I() != 1))
                {
                    //mpeMensaje.ShowMessage("La factura de servicios de vuelo no se generará debido a que no tiene importes", "Alerta");
                    hdTipoFactura.Value = "1";
                    lblConfirmacion.Text = "La factura de servicios de vuelo no generará debido a que tiene importe 0, ¿Desea continuar?";
                    ppConfirmacion.ShowOnPageLoad = true;
                    return;
                }

                //// HACER CAMBIOS AQUI PARA APUNTAR A SAP
                //if (eValidaFacturante != null)
                //    eValidaFacturante(sender, e);

                //if (!bExisteFacturanteSCC)
                //{
                //    mpeMensaje.ShowMessage("El facturante " + cboFacturanteServiciosCargo.Text + " no existe en Syteline", "Alerta");
                //    return;
                //}

                //if (!bExisteFacturanteVuelo)
                //{
                //    mpeMensaje.ShowMessage("El facturante " + cboFacturanteVuelo.Text + " no existe en Syteline", "Alerta");
                //    return;
                //}


                //if (eGetFacturanteSC != null)
                //    eGetFacturanteSC(null, EventArgs.Empty);

                //if (eGetFacturanteSV != null)
                //    eGetFacturanteSV(null, EventArgs.Empty);

                if (eGetInformacionFactura != null)
                    eGetInformacionFactura(sender, e);


                if (dsInformacionContrato != null)
                {
                    if (dsInformacionContrato.Tables[4].Rows.Count < 1)
                    {
                        mpeMensaje.ShowMessage("El paquete no contiene cuentas configuradas, favor de validarlo.", "Alerta");
                        return;
                    }
                }

                if ((rdlListCantidadFacturas.Value.S().I() == 1) && (rdlServiciosAmbos.Checked) && (string.IsNullOrEmpty(sFacturaSCC) && string.IsNullOrEmpty(sFacturaVuelo)))
                {
                    if (cboFacturanteVuelo.Value.S() != cboFacturanteServiciosCargo.Value.S())
                    {
                        mpeMensaje.ShowMessage("Los Facturantes deben ser el mismo.", "Alerta");
                        return;
                    }
                    if (iIdMonedaSV != iIdMonedaSC)
                    {
                        mpeMensaje.ShowMessage("La moneda para ambas facturas debe ser la misma.", "Alerta");
                        return;
                    }

                    if (eGeneraUnaFactura != null)
                        eGeneraUnaFactura(sender, e);
                }


                if ((objPrefactura.dSubMXNC> 0) &&(rdlServiciosAmbos.Checked || rdlServicioCargo.Checked) && string.IsNullOrEmpty(sFacturaSCC) && (rdlListCantidadFacturas.Value.S().I() == 0))
                {
                    if (eGeneraFacturaSCC != null)
                        eGeneraFacturaSCC(sender, e);
                }
                if ((objPrefactura.dSubDllV > 0) && (rdlServiciosAmbos.Checked || rdlServicioVuelo.Checked) && string.IsNullOrEmpty(sFacturaVuelo) && (rdlListCantidadFacturas.Value.S().I() == 0))
                {
                    if (eGeneraFacturaVuelo != null)
                        eGeneraFacturaVuelo(sender, e);
                }

                if (eUpdateBasePrefactura != null)
                    eUpdateBasePrefactura(sender, e);

                if (eSaveServicos != null)
                    eSaveServicos(sender, e);

                Response.Redirect("~/Views/Consultas/frmConsultaPrefactura.aspx");
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSigNueva_Click", "Aviso");
            }
        }
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                switch(hdTipoFactura.Value.S())
                {
                    // Fatura de Vuelo Sin importes
                    case "1":
                        rdlServicioVuelo.Checked = false;
                        rdlServiciosAmbos.Checked = false;
                        rdlServicioCargo.Checked = true;
                        break;
                    // Factura de SCC Sin importes
                    case "2":
                        rdlServicioVuelo.Checked = true;
                        rdlServiciosAmbos.Checked = false;
                        rdlServicioCargo.Checked = false;
                        break;
                }

                btnEnviaPrefactura_Click(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnConfirmar_Click", "Aviso");
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

                cboClaveCliente.DataSource = dtClientes;
                cboClaveCliente.TextField = "CodigoCliente";
                cboClaveCliente.ValueField = "IdCliente";
                cboClaveCliente.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void CargaHeader()
        {
            try
            {
                lblSeleccionCliente.Text = cboClaveCliente.Text;
                lblConsultaCliente.Text = lblSeleccionCliente.Text;
                lblPrefacturaCliente.Text = lblConsultaCliente.Text;

                lblSeleccionContrato.Text = cboClaveContrato.Text;
                lblConsultaContrato.Text = lblSeleccionContrato.Text;
                lblPrefacturaContrato.Text = lblConsultaContrato.Text;

                lblSeleccionMonedaVuelo.Text = cboMonedaVuelo.Text;
                lblConsultaMonedaVuelo.Text = lblSeleccionMonedaVuelo.Text;
                lblPrefacturaMonedaVuelo.Text = lblConsultaMonedaVuelo.Text;

                lblSeleccionFacturanteV.Text = cboFacturanteVuelo.Text;
                lblConsultaFacturanteV.Text = lblSeleccionFacturanteV.Text;
                lblPrefacturaFacturanteV.Text = lblConsultaFacturanteV.Text;

                lblSeleccionMSC.Text = cboMonedaServiciosCargo.Text;
                lblConsultaMSC.Text = lblSeleccionMSC.Text;
                lblPrefacturaMSC.Text = lblConsultaMSC.Text;

                lblSeleccionFacturanteSC.Text = cboFacturanteServiciosCargo.Text;
                lblConsultaFacturanteSC.Text = lblSeleccionFacturanteSC.Text;
                lblPrefacturaFacturanteSC.Text = lblConsultaFacturanteSC.Text;

                lblSeleccionTipoCambio.Text = TipoCambioPrefactura.S();
                lblConsultaTipoCambio.Text = lblSeleccionTipoCambio.Text;
                lblPrefacturaTipoCambio.Text = lblConsultaTipoCambio.Text;

                DataRow[] rows = dtContrato.Select("IdContrato = " + cboClaveContrato.Value);
                lblSeleccionTipoContrato.Text = rows[0]["DescripcionPaquete"].S();
                lblConsultaTipoContrato.Text = lblSeleccionTipoContrato.Text;
                lblPrefacturaTipoContrato.Text = lblConsultaTipoContrato.Text;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string sRemisiones
        {
            get
            {
                string sRemisiones = "0";

                List<object> lstIdsSelected = gvRemisionesDeVuelo.GetSelectedFieldValues("IdRemision");


                foreach (object a in lstIdsSelected)
                {

                    sRemisiones = string.Format("{0},{1}", sRemisiones, a.S());
                }
                return sRemisiones;
            }
        }
        private void GetFacturantes()
        {
            try
            {
                cboFacturanteServiciosCargo.Text = string.Empty;
                cboFacturanteServiciosCargo.Value = null;
                cboFacturanteVuelo.Text = string.Empty;
                cboFacturanteVuelo.Value = null;

                #region CODIGO COMENTADO
                //string sFacturanteProvSC = sClaveContrato;
                //string sFacturanteProvSV = sClaveContrato;
                //switch (iIdMonedaSC)
                //{
                //    case 1:
                //        sFacturanteProvSC = string.Format("{0}P", sFacturanteProvSC);
                //        break;
                //    case 2:
                //        sFacturanteProvSC = string.Format("{0}D", sFacturanteProvSC);
                //        break;
                //}

                //switch (iIdMonedaSV)
                //{
                //    case 1:
                //        sFacturanteProvSV = string.Format("{0}P", sFacturanteProvSV);
                //        break;
                //    case 2:
                //        sFacturanteProvSV = string.Format("{0}D", sFacturanteProvSV);
                //        break;
                //}

                //if (eGetFacturanteSC != null)
                //    eGetFacturanteSC(null, EventArgs.Empty);

                //if (eGetFacturanteSV != null)
                //    eGetFacturanteSV(null, EventArgs.Empty);

                //if (dtFacturanteSC.Rows.Count < 1)
                //{

                //    DataRow dtSC = dtFacturanteSC.NewRow();
                //    dtSC["cust_num"] = sFacturanteProvSC;
                //    dtFacturanteSC.Rows.Add(dtSC);
                //}

                //if (dtFacturanteSV.Rows.Count < 1)
                //{

                //    DataRow dtSV = dtFacturanteSV.NewRow();
                //    dtSV["cust_num"] = sFacturanteProvSV;
                //    dtFacturanteSV.Rows.Add(dtSV);
                //}
                #endregion

                eGetFacturantesSAP(null, EventArgs.Empty);

                cboFacturanteServiciosCargo.DataSource = dtFacturantesSAP;
                cboFacturanteServiciosCargo.ValueField = "cust_num";
                cboFacturanteServiciosCargo.TextField = "cust_num";
                cboFacturanteServiciosCargo.DataBind();

                cboFacturanteVuelo.DataSource = dtFacturantesSAP;
                cboFacturanteVuelo.ValueField = "cust_num";
                cboFacturanteVuelo.TextField = "cust_num";
                cboFacturanteVuelo.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private decimal SumaImportes(DataTable dt, string sColumna, string sColumnaFiltro, int iId)
        {
            try
            {
                decimal dImpTotal = 0m;
                DataRow[] row = dt.Select(sColumnaFiltro + " =" + iId);
                int iIndice = 0;
                for (iIndice = 0; iIndice <= row.Length - 1; iIndice++)
                {
                    dImpTotal += row[iIndice][sColumna].S().D();
                }

                return dImpTotal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private int SumaCantidades(DataTable dt, string sColumna, string sColumnaFiltro, int iId)
        {
            try
            {
                int dImpTotal = 0;
                DataRow[] row = dt.Select(sColumnaFiltro + " =" + iId);
                int iIndice = 0;
                for (iIndice = 0; iIndice <= row.Length - 1; iIndice++)
                {
                    dImpTotal += row[iIndice][sColumna].S().I();
                }

                return dImpTotal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string SumaHoras(DataTable dt, string sColumna, string sColumnaFiltro, int iId)
        {
            try
            {
                string dTiempo = "00:00";
                DataRow[] row = dt.Select(sColumnaFiltro + " =" + iId);
                int iIndice = 0;
                for (iIndice = 0; iIndice <= row.Length - 1; iIndice++)
                {
                    dTiempo = Utils.GetSumaTiempos(dTiempo, row[iIndice][sColumna].S());

                }

                return dTiempo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ChangeTab(int iIdndex)
        {
            try
            {
                ASPxPageControl1.TabPages[iIdndex].Enabled = true;
                ASPxPageControl1.ActiveTabIndex = iIdndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ActualiaServiciosVuelosMostrar()
        {
            try
            {
                DataTable SVtmp = dtSV;
                DataColumn col = new DataColumn { Caption = "TotHoras", ColumnName = "TotHoras" };
                SVtmp.Columns.Add(col);
                List<int> lstIdConceptos = new List<int>();
                foreach (DataRow row in SVtmp.Rows)
                {
                    if (lstIdConceptos.Where(X => X == row["IdConcepto"].S().I()).Count() < 1)
                    {
                        lstIdConceptos.Add(row["IdConcepto"].S().I());
                    }

                }
                DataTable dtSVW = new DataTable();
                Decimal dimporte = 0m;
                dtSVW = SVtmp.Clone();
                DataRow rowtmp = dtSVW.NewRow();
                string dSumHoras = "00:00";
                foreach (int idConcepto in lstIdConceptos)
                {
                    rowtmp = dtSVW.NewRow();
                    DataRow[] row = SVtmp.Select("IdConcepto =" + idConcepto);
                    rowtmp["IdRemision"] = row[0]["IdRemision"];
                    rowtmp["ImporteDlls"] = SumaImportes(SVtmp, "ImporteDlls", "IdConcepto", idConcepto);
                    rowtmp["IVADLL"] = SumaImportes(SVtmp, "IVADLL", "IdConcepto", idConcepto);
                    rowtmp["TotalDLL"] = SumaImportes(SVtmp, "TotalDLL", "IdConcepto", idConcepto);
                    rowtmp["ImporteMXN"] = SumaImportes(SVtmp, "ImporteMXN", "IdConcepto", idConcepto);
                    rowtmp["IVAMXN"] = SumaImportes(SVtmp, "IVAMXN", "IdConcepto", idConcepto);
                    rowtmp["TotalMXN"] = SumaImportes(SVtmp, "TotalMXN", "IdConcepto", idConcepto);
                    rowtmp["HrDescontar"] = SumaHoras(SVtmp, "HrDescontar", "IdConcepto", idConcepto);
                    rowtmp["Descripcion"] = row[0]["Descripcion"];
                    rowtmp["IdConcepto"] = row[0]["IdConcepto"];
                    dSumHoras = Utils.GetSumaTiempos(dSumHoras, rowtmp["HrDescontar"].S());
                    rowtmp["TotHoras"] = dSumHoras;

                    rowtmp["ImpDescuentoDlls"] = SumaImportes(SVtmp, "ImpDescuentoDlls", "IdConcepto", idConcepto); ;
                    rowtmp["ImpDescuentoMXN"] = SumaImportes(SVtmp, "ImpDescuentoMXN", "IdConcepto", idConcepto); ;

                    if (idConcepto == 5 || idConcepto == 6)
                    {
                        rowtmp["Cantidad"] = SumaCantidades(SVtmp, "Cantidad", "IdConcepto", idConcepto);
                    }
                    else
                    {
                        rowtmp["Cantidad"] = SumaHoras(SVtmp, "Cantidad", "IdConcepto", idConcepto);


                    }

                    dtSVW.Rows.Add(rowtmp);
                }
                dtSV = dtSVW;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ActualiaServiciosCargosMostrar()
        {
            try
            {
                DataTable SCtmp = dtSC;
                List<int> lstIdConceptos = new List<int>();
                foreach (DataRow row in SCtmp.Rows)
                {
                    if (lstIdConceptos.Where(X => X == row["IdServicioConCargo"].S().I()).Count() < 1)
                    {
                        lstIdConceptos.Add(row["IdServicioConCargo"].S().I());
                    }

                }
                DataTable dtScW = new DataTable();
                Decimal dimporte = 0m;
                dtScW = SCtmp.Clone();
                DataRow rowtmp = dtScW.NewRow();
                string dSumHoras = "00:00";
                foreach (int idConcepto in lstIdConceptos)
                {
                    rowtmp = dtScW.NewRow();
                    DataRow[] row = SCtmp.Select("IdServicioConCargo =" + idConcepto);
                    rowtmp["IdServicio"] = row[0]["IdServicio"];
                    rowtmp["SubtotalMXN"] = SumaImportes(SCtmp, "SubtotalMXN", "IdServicioConCargo", idConcepto);
                    rowtmp["IVAMXN"] = SumaImportes(SCtmp, "IVAMXN", "IdServicioConCargo", idConcepto);
                    rowtmp["TotalMXN"] = SumaImportes(SCtmp, "TotalMXN", "IdServicioConCargo", idConcepto);
                    rowtmp["SubtotalUSD"] = SumaImportes(SCtmp, "SubtotalUSD", "IdServicioConCargo", idConcepto);
                    rowtmp["IVAUSD"] = SumaImportes(SCtmp, "IVAUSD", "IdServicioConCargo", idConcepto);
                    rowtmp["TotalUSD"] = SumaImportes(SCtmp, "TotalUSD", "IdServicioConCargo", idConcepto);
                    rowtmp["IdServicioConCargo"] = row[0]["IdServicioConCargo"];
                    rowtmp["ServicioConCargoDescripcion"] = row[0]["ServicioConCargoDescripcion"];

                    dtScW.Rows.Add(rowtmp);
                }
                dtSC = dtScW;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ValidaTipoPaquete()
        {
            try
            {
                if (eVerificaPaquete != null)
                    eVerificaPaquete(null, EventArgs.Empty);

                rdlListCantidadFacturas.Visible = bUnaFactura;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void bloqueaEdicion()
        {
            if (!string.IsNullOrEmpty(sFacturaSCC))
            {
                cboFacturanteServiciosCargo.ReadOnly = true;
                cboMonedaServiciosCargo.ReadOnly = true;
            }

            if (!string.IsNullOrEmpty(sFacturaVuelo))
            {
                cboFacturanteVuelo.ReadOnly = true;
                cboMonedaVuelo.ReadOnly = true;
            }

            if (!string.IsNullOrEmpty(sFacturaSCC) || !string.IsNullOrEmpty(sFacturaVuelo))
            {
                cboClaveCliente.ReadOnly = true;
                cboClaveContrato.ReadOnly = true;
                gvRemisionesDeVuelo.Enabled = false;
                gvDetallesRemision.Enabled = false;
            }

            if ((!string.IsNullOrEmpty(sFacturaSCC) && sFacturaSCC != "En proceso") && (!string.IsNullOrEmpty(sFacturaVuelo) && sFacturaVuelo != "En proceso"))
            {
                btnSigRemDet.Enabled = false;
                btnSigRem.Enabled = false;
                btnSigNueva.Enabled = false;
                rdlServicioCargo.ReadOnly = true;
                rdlServiciosAmbos.ReadOnly = true;
                rdlServicioVuelo.ReadOnly = true;
                btnEnviaPrefactura.Enabled = false;
                rdlListCantidadFacturas.ReadOnly = true;
            }
        }
        private void PreparaEnvioSoloUnaFactura(int iTipoEdicion)
        {
            try
            {
                switch (iTipoEdicion)
                {
                    case 1:
                        rdlServicioVuelo.Checked = true;
                        rdlServicioCargo.Checked = false;
                        rdlServiciosAmbos.Checked = false;

                        rdlServicioVuelo.ReadOnly = true;
                        rdlServicioCargo.ReadOnly = true;
                        rdlServiciosAmbos.ReadOnly = true;
                        break;
                    case 2:
                        rdlServicioVuelo.Checked = false;
                        rdlServicioCargo.Checked = true;
                        rdlServiciosAmbos.Checked = false;

                        rdlServicioVuelo.ReadOnly = true;
                        rdlServicioCargo.ReadOnly = true;
                        rdlServiciosAmbos.ReadOnly = true;
                        break;
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "Vars y Propiedades"
        Prefactura_Presenter oPresenter;
        private const string sPagina = "frmPrefactura.aspx";
        private const string sClase = "frmPrefactura.aspx.cs";

        public event EventHandler eUpdateBasePrefactura;
        public event EventHandler eGetFacturanteSV;
        public event EventHandler eGetFacturanteSC;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetContratos;
        public event EventHandler eSaveBasePrefactura;
        public event EventHandler eGetRemisiones;
        public event EventHandler eGetDetalle;
        public event EventHandler eGetServiciosVuelo;
        public event EventHandler eGetServiciosCargo;
        public event EventHandler eSaveServicos;
        public event EventHandler eGetPrefacturas;
        public event EventHandler eGetRecuperaRemisionesPrefactura;
        public event EventHandler eGetRecuperaPRefacturaServicios;
        public event EventHandler eGetInformacionFactura;
        public event EventHandler eValidaFacturante;
        public event EventHandler eGeneraFacturaVuelo;
        public event EventHandler eGeneraFacturaSCC;
        public event EventHandler eGeneraUnaFactura;
        public event EventHandler eVerificaPaquete;
        public event EventHandler eGetFacturantesSAP;

        public Prefactura objPrefactura
        {
            get
            {
                Prefactura objPref = new Prefactura();
                objPref.iIdCliente = cboClaveCliente.Value.I();
                objPref.iIdContrato = cboClaveContrato.Value.I();
                objPref.iIdMonedaVuelo = iIdMonedaSV;
                objPref.sClaveFacturanteVuelo = cboFacturanteVuelo.Value.S();
                objPref.iIdMonedaSCC = iIdMonedaSC;
                objPref.sClaveFacturanteSCC = cboFacturanteServiciosCargo.Value.S();
                objPref.sCliente = cboClaveCliente.Text.S();



                // -- SERVICIOS DE VUELO
                objPref.dSubDllV = Convert.ToString(gvServiciovuelos.GetTotalSummaryValue(
                    gvServiciovuelos.TotalSummary["ImporteDlls", DevExpress.Data.SummaryItemType.Sum])).D();
                objPref.dDescDllV = Convert.ToString(gvServiciovuelos.GetTotalSummaryValue(
                    gvServiciovuelos.TotalSummary["ImpDescuentoDlls", DevExpress.Data.SummaryItemType.Sum])).S().D();
                objPref.dIVADllV = Convert.ToString(gvServiciovuelos.GetTotalSummaryValue(
                    gvServiciovuelos.TotalSummary["IVADLL", DevExpress.Data.SummaryItemType.Sum])).D();
                objPref.dTotalDllV = Convert.ToString(gvServiciovuelos.GetTotalSummaryValue(
                    gvServiciovuelos.TotalSummary["TotalDLL", DevExpress.Data.SummaryItemType.Sum])).D();

                objPref.dSubMXNV = Convert.ToString(gvServiciovuelos.GetTotalSummaryValue(
                    gvServiciovuelos.TotalSummary["ImporteMXN", DevExpress.Data.SummaryItemType.Sum])).D();
                objPref.dDescMXNV = Convert.ToString(gvServiciovuelos.GetTotalSummaryValue(
                    gvServiciovuelos.TotalSummary["ImpDescuentoMXN", DevExpress.Data.SummaryItemType.Sum])).S().D();
                objPref.dIVAMXNV = Convert.ToString(gvServiciovuelos.GetTotalSummaryValue(
                    gvServiciovuelos.TotalSummary["IVAMXN", DevExpress.Data.SummaryItemType.Sum])).D();
                objPref.dTotalMXNV = Convert.ToString(gvServiciovuelos.GetTotalSummaryValue(
                    gvServiciovuelos.TotalSummary["TotalMXN", DevExpress.Data.SummaryItemType.Sum])).D();



                // -- SERVICIOS CON CARGO
                objPref.sSubHorasC = Convert.ToString(gvServiciovuelos.GetTotalSummaryValue(
                    gvServiciovuelos.TotalSummary["TotHoras", DevExpress.Data.SummaryItemType.Max]));

                objPref.dSubDllC = Convert.ToString(ASPxGridView3.GetTotalSummaryValue(
                    ASPxGridView3.TotalSummary["SubtotalUSD", DevExpress.Data.SummaryItemType.Sum])).D();
                objPref.dIVADllC = Convert.ToString(ASPxGridView3.GetTotalSummaryValue(
                    ASPxGridView3.TotalSummary["IVAUSD", DevExpress.Data.SummaryItemType.Sum])).D();
                objPref.dTotalDllC = Convert.ToString(ASPxGridView3.GetTotalSummaryValue(
                    ASPxGridView3.TotalSummary["TotalUSD", DevExpress.Data.SummaryItemType.Sum])).D();

                objPref.dSubMXNC = Convert.ToString(ASPxGridView3.GetTotalSummaryValue(
                    ASPxGridView3.TotalSummary["SubtotalMXN", DevExpress.Data.SummaryItemType.Sum])).D();
                objPref.dIVAMXNC = Convert.ToString(ASPxGridView3.GetTotalSummaryValue(
                    ASPxGridView3.TotalSummary["IVAMXN", DevExpress.Data.SummaryItemType.Sum])).D();
                objPref.dTotalMXNC = Convert.ToString(ASPxGridView3.GetTotalSummaryValue(
                    ASPxGridView3.TotalSummary["TotalMXN", DevExpress.Data.SummaryItemType.Sum])).D();

                
                if (rdlListCantidadFacturas.Value == "1" && rdlServiciosAmbos.Checked)
                {
                    if (ViewState["VSFactorIVAV"] == null)
                    {
                        DataTable dtRems = (DataTable)gvDetallesRemision.DataSource;
                        if (dtRems != null && dtRems.Rows.Count > 0)
                        {
                            int iRem = dtRems.Rows[0]["IdRemision"].S().I();
                            DataRow[] rows = dtRemisiones.Select("IdRemision = " + iRem.S());
                            if(rows != null && rows.Length > 0)
                                dFactorIVAV = rows.Length > 0 ? rows[0]["IVA"].S().Replace("%", "").S().D() : 0;
                        }
                    }

                    objPref.dIVADllC = objPref.dSubDllC * (dFactorIVAV / 100);
                    objPref.dIVAMXNC = objPref.dSubMXNC * (dFactorIVAV / 100);
                }

                objPref.bCobroAmbos = rdlServiciosAmbos.Checked;
                objPref.bCobroSCC = rdlServicioCargo.Checked;
                objPref.bCobroV = rdlServicioVuelo.Checked;

                objPref.iIdPorcentaje = iPorcentajeAvance;
                objPref.dTipoCambio = TipoCambioPrefactura;
                objPref.sUsuarioCreacion = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                objPref.sUsuarioMod = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                objPref.sFacturaVuelo = sFacturaVuelo;
                objPref.sFacturaSCC = sFacturaSCC;
                objPref.UnaFactura = rdlListCantidadFacturas.Value.I();
                return objPref;
            }
            set
            {
                Prefactura objPref = value;
                cboClaveCliente.Value = objPref.iIdCliente.S();
                cboClaveContrato.Value = objPref.iIdContrato.S();
                cboMonedaVuelo.Value = objPref.iIdMonedaVuelo.S();
                cboMonedaServiciosCargo.Value = objPref.iIdMonedaSCC.S();
                cboFacturanteVuelo.Value = objPref.sClaveFacturanteVuelo;
                cboFacturanteServiciosCargo.Value = objPref.iIdMonedaSCC.S();
                cboFacturanteServiciosCargo.Value = objPref.sClaveFacturanteSCC;
                rdlServiciosAmbos.Checked = objPref.bCobroAmbos;
                rdlServicioCargo.Checked = objPref.bCobroSCC;
                rdlServicioVuelo.Checked = objPref.bCobroV;
                TipoCambioPrefactura = objPref.dTipoCambio;
                sFacturaSCC = objPref.sFacturaSCC;
                sFacturaVuelo = objPref.sFacturaVuelo;
                rdlListCantidadFacturas.Value = objPref.UnaFactura.S();
            }
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
        public DataTable dtFacturantesSAP
        {
            get { return (DataTable)ViewState["VSFacturantesSAP"]; }
            set { ViewState["VSFacturantesSAP"] = value; }
        }
        public DataTable dtFacturanteSV
        {
            get { return (DataTable)ViewState["dtFacturanteSV"]; }
            set { ViewState["dtFacturanteSV"] = value; }
        }
        public DataTable dtFacturanteSC
        {
            get { return (DataTable)ViewState["dtFacturanteSC"]; }
            set { ViewState["dtFacturanteSC"] = value; }
        }
        public int iIdContrato
        {
            get { return cboClaveContrato.Value.I(); }
        }
        public int iIdMonedaSV
        {
            get
            {
                return cboMonedaVuelo.Value.I();
            }
        }
        public int iIdMonedaSC
        {
            get
            {
                return cboMonedaServiciosCargo.Value.I();
            }
        }
        public int iIdPrefactura
        {
            get { return (int)ViewState["iIdPrefactura"]; }
            set { ViewState["iIdPrefactura"] = value; }
        }
        private int iPorcentajeAvance
        {
            get
            {
                int ipor = 0;
                foreach (TabPage page in ASPxPageControl1.TabPages)
                {
                    if (page.Enabled == true)
                    {
                        if (page.Index != 3)
                            ipor += 25;
                        else
                            ipor += 20;
                    }
                }
                return ipor;
            }
        }
        public string sClaveContrato
        {
            get
            {
                return cboClaveContrato.Text;
            }
        }
        public string sClaveCliente
        {
            get
            {
                return cboClaveCliente.Text;
            }
        }
        public DataTable dtRemisiones
        {
            get
            {
                return (DataTable)ViewState["dtRemisiones"];
            }
            set
            {
                ViewState["dtRemisiones"] = value;
            }
        }
        public DataTable dtDetalleRemisiones
        {
            get
            {
                return (DataTable)ViewState["dtDetalleRemisiones"];
            }
            set
            {
                ViewState["dtDetalleRemisiones"] = value;
            }
        }
        public int iIdCliente
        {
            get
            {
                return cboClaveCliente.SelectedItem.Value.I();
            }
        }
        public string sIdFacturaSCC
        {
            get
            {
                return (string)ViewState["FacturaSCC"];
            }
            set
            {
                ViewState["FacturaSCC"] = value;
            }
        }
        public string sIdFacturaVuelo
        {
            get
            {
                return (string)ViewState["sIdFacturaVuelo"];
            }
            set
            {
                ViewState["sIdFacturaVuelo"] = value;
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
        public DataTable dtSC
        {
            get
            {
                return (DataTable)ViewState["dtSC"];
            }
            set
            {
                ViewState["dtSC"] = value;
            }
        }
        public DataTable dtSV
        {
            get
            {
                return (DataTable)ViewState["dtSV"];
            }
            set
            {
                ViewState["dtSV"] = value;
            }
        }
        public decimal dFactorIVAV
        {
            get { return (decimal)ViewState["VSFactorIVAV"]; }
            set { ViewState["VSFactorIVAV"] = value; }
        }
        public DataSet dsInformacionContrato
        {
            get
            {
                return (DataSet)ViewState["dsInformacionContrato"];
            }
            set
            {
                ViewState["dsInformacionContrato"] = value;
            }
        }
        public Decimal TipoCambioPrefactura
        {
            get
            {
                return (Decimal)ViewState["TipoCambioPrefactura"];
            }
            set
            {
                ViewState["TipoCambioPrefactura"] = value;
            }
        }
        public bool bActualizaGeneral
        {
            get
            {
                return (bool)ViewState["bActualizaGeneral"];
            }
            set
            {
                ViewState["bActualizaGeneral"] = value;
            }
        }
        public bool bActualizaSeleccion
        {
            get
            {
                return (bool)ViewState["bActualizaSeleccion"];
            }
            set
            {
                ViewState["bActualizaSeleccion"] = value;
            }
        }
        public bool bActualizaFinal
        {
            get
            {
                return (bool)ViewState["bActualizaFinal"];
            }
            set
            {
                ViewState["bActualizaFinal"] = value;
            }
        }
        public string sFacturaVuelo
        {
            get
            {
                return (string)Session["sFacturaVuelo"];
            }
            set
            {
                Session["sFacturaVuelo"] = value;
            }
        }
        public string sFacturaSCC
        {
            get
            {
                return (string)Session["sFacturaSCC"];
            }
            set
            {
                Session["sFacturaSCC"] = value;
            }
        }
        public bool bExisteFacturanteVuelo
        {
            get
            {
                return (bool)ViewState["bExisteFacturanteVuelo"];
            }
            set
            {
                ViewState["bExisteFacturanteVuelo"] = value;
            }
        }
        public bool bExisteFacturanteSCC
        {
            get
            {
                return (bool)ViewState["bExisteFacturanteSCC"];
            }
            set
            {
                ViewState["bExisteFacturanteSCC"] = value;
            }
        }
        public bool bUnaFactura
        {
            get
            {
                return (bool)ViewState["bUnaFactura"];
            }
            set
            {
                ViewState["bUnaFactura"] = value;
            }
        }
        #endregion

        protected void cboClaveContrato_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string sMensajeEnviar = string.Empty;

                Contrato_Generales oGenerales = new DBContrato().DBGetContratoGenerales(cboClaveContrato.Value.S().I());
                if (oGenerales.sMetodoPagoFact.S() == string.Empty)
                    sMensajeEnviar = "El método de pago en el Contrato es requerido, ¿desea capturarlo ahora?";

                if (oGenerales.sFormaPago.S() == string.Empty)
                    sMensajeEnviar = "La forma de pago en el Contrato es requerida, ¿Desea capturala ahora?";

                if (oGenerales.sUsoCFDI.S() == string.Empty)
                    sMensajeEnviar = "El CFDI en el Contrato es requerido, ¿Desea capturala ahora?";

                if (sMensajeEnviar != string.Empty)
                {
                    lblConfirmacionFactura.Text = sMensajeEnviar;
                    ppConfirmacionFact.ShowOnPageLoad = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnIrContrato_Click(object sender, EventArgs e)
        {
            string sIdContrato;
            string sAccion = Convert.ToBase64String(Encoding.UTF8.GetBytes("Editar"));
            sIdContrato = Convert.ToBase64String(Encoding.UTF8.GetBytes(iIdContrato.S()));
            string ruta = "~/Views/CreditoCobranza/frmcontrato.aspx?Contrato=" + sIdContrato + "&Accion=" + sAccion;
            Response.Redirect(ruta);
        }

        
    }
}