using ALE_MexJet.Clases;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;

namespace ALE_MexJet.Views.JetSmart
{
    public partial class frmPubFerrys : System.Web.UI.Page, IViewPubFerrys
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            mpeMensaje.OkButtonPressed += mpeMensaje_OkButtonPressed;
            oPresenter = new PubFerrys_Presenter(this, new DBPubFerrys());

            if (!IsPostBack)
            {
                hfNacional["hfNacional"] = Utils.ObtieneParametroPorClave("9");
                hfExtranjero["hfExtranjero"] = Utils.ObtieneParametroPorClave("10");

                IniciaControles();
            }
        }

        private void mpeMensaje_OkButtonPressed(object sender, EventArgs args)
        {

        }

        protected void ddlOrigen_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                //Verificar si tiene pierna predecesora para obtener el ICAO y mandarlo como parametro.
                //DevExpress.Web.Bootstrap.BootstrapComboBox comboBox = (DevExpress.Web.Bootstrap.BootstrapComboBox)source;
                BootstrapComboBox comboBox = (BootstrapComboBox)source;
                sFiltroO = e.Filter;

                if (eLoadOrigDestFiltro != null)
                    eLoadOrigDestFiltro(source, e);

                CargaComboAeropuertosFV(comboBox, dtOrigen, sFiltroO);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlOrigenFV_ItemsRequestedByFilterCondition", "Aviso");
            }
        }

        protected void ddlOrigen_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
        {
            try
            {
                BootstrapComboBox comboBox = (BootstrapComboBox)source;
                sFiltroO = e.Text;

                dtOrigen = new DBPresupuesto().GetAeropuertosOrigen(sFiltroO, 2);

                CargaComboAeropuertosFV(comboBox, dtOrigen, sFiltroO);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlOrigen_ItemRequestedByValue", "Aviso");
            }
        }

        protected void ddlDestino_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                //Verificar si tiene pierna predecesora para obtener el ICAO y mandarlo como parametro.
                //DevExpress.Web.Bootstrap.BootstrapComboBox comboBox = (DevExpress.Web.Bootstrap.BootstrapComboBox)source;
                DevExpress.Web.Bootstrap.BootstrapComboBox comboBox = (DevExpress.Web.Bootstrap.BootstrapComboBox)source;
                sFiltroD = e.Filter;

                if (eLoadOrigDestFiltroDest != null)
                    eLoadOrigDestFiltroDest(source, e);

                CargaComboAeropuertosFV(comboBox, dtDestino, sFiltroD);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlDestinoFV_ItemsRequestedByFilterCondition", "Aviso");
            }
        }

        protected void ddlDestino_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
        {
            try
            {
                BootstrapComboBox comboBox = (BootstrapComboBox)source;
                sFiltroD = e.Text;

                dtDestino = new DBPresupuesto().GetAeropuertosDestino(sFiltroD, string.Empty, 2);
                hfNalExtFor["hfNalExtFor"] = dtDestino.Rows[0]["TipoDestino"].S();
                CargaComboAeropuertosFV(comboBox, dtDestino, sFiltroD);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlDestino_ItemRequestedByValue", "Aviso");
            }
        }

        protected void upaPrincipal_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }

        protected void imbAddFerryHijo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiaCamposModal();
            ppTramos.ShowOnPageLoad = true;
            txtNoTripFV.Text = txtTrip.Text;

            if (txtTrip.Text != string.Empty)
                txtNoTripFV.IsValid = true;

            ddlMatriculaFV.Value = ddlMatricula.Value;

            if (ddlMatriculaFV.Value.S() != string.Empty)
                ddlMatriculaFV.IsValid = true;

            ddlOrigenFV.Enabled = true;
            ddlOrigenFV.ValidationSettings.EnableCustomValidation = true;
        }

        protected void ddlOrigenFV_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                //Verificar si tiene pierna predecesora para obtener el ICAO y mandarlo como parametro.
                //DevExpress.Web.Bootstrap.BootstrapComboBox comboBox = (DevExpress.Web.Bootstrap.BootstrapComboBox)source;
                BootstrapComboBox comboBox = (BootstrapComboBox)source;
                sFiltroO = e.Filter;

                if (eLoadOrigDestFiltro != null)
                    eLoadOrigDestFiltro(source, e);

                CargaComboAeropuertosFV(comboBox, dtOrigen, sFiltroO);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlOrigenFV_ItemsRequestedByFilterCondition", "Aviso");
            }
        }

        protected void ddlOrigenFV_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            try
            {
                BootstrapComboBox comboBox = (BootstrapComboBox)source;
                sFiltroO = e.Text;

                dtOrigen = new DBPresupuesto().GetAeropuertosOrigen(sFiltroO, 2);

                CargaComboAeropuertosFV(comboBox, dtOrigen, sFiltroO);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlOrigenFV_ItemRequestedByValue", "Aviso");
            }
        }

        protected void ddlDestinoFV_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                //Verificar si tiene pierna predecesora para obtener el ICAO y mandarlo como parametro.
                //DevExpress.Web.Bootstrap.BootstrapComboBox comboBox = (DevExpress.Web.Bootstrap.BootstrapComboBox)source;
                DevExpress.Web.Bootstrap.BootstrapComboBox comboBox = (DevExpress.Web.Bootstrap.BootstrapComboBox)source;
                sFiltroD = e.Filter;

                if (eLoadOrigDestFiltroDest != null)
                    eLoadOrigDestFiltroDest(source, e);

                CargaComboAeropuertosFV(comboBox, dtDestino, sFiltroD);

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlDestinoFV_ItemsRequestedByFilterCondition", "Aviso");
            }
        }

        protected void ddlDestinoFV_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            try
            {
                BootstrapComboBox comboBox = (BootstrapComboBox)source;
                sFiltroD = e.Text;

                dtDestino = new DBPresupuesto().GetAeropuertosDestino(sFiltroD, string.Empty, 2);
                hfNalExtForFV["hfNalExtForFV"] = dtDestino.Rows[0]["TipoDestino"].S();
                CargaComboAeropuertosFV(comboBox, dtDestino, sFiltroD);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlDestinoFV_ItemRequestedByValue", "Aviso");
            }
        }

        protected void ddlDestinoFV_ValueChanged(object sender, EventArgs e)
        {
            if (ddlOrigenFV.Value.S().I() > 0 && ddlDestinoFV.Value.S().I() > 0)
            {
                DatosRemision oDatosContrato = new DatosRemision();
                string sTiempo = Utils.ObtieneTiempoTramosPresupuestos(oDatosContrato, ddlOrigenFV.Value.S().I(), ddlDestinoFV.Value.S().I(), iIdGrupoModelo);
                txtTiempoVueloFV.Text = sTiempo;
                txtTiempoVueloFV.IsValid = true;
            }

            if ((txtTiempoVueloFV.Text.S() != string.Empty && txtTiempoVueloFV.Text.S() != "00:00") && ddlMatriculaFV.Value.S().I() > 0 && hfNalExtForFV != null)
            {
                ObtieneCostosVueloFV();
            }
        }

        protected void ddlMatriculaFV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrigenFV.Value.S().I() > 0 && ddlDestinoFV.Value.S().I() > 0)
            {
                DatosRemision oDatosContrato = new DatosRemision();
                string sTiempo = Utils.ObtieneTiempoTramosPresupuestos(oDatosContrato, ddlOrigenFV.Value.S().I(), ddlDestinoFV.Value.S().I(), iIdGrupoModelo);
                txtTiempoVueloFV.Text = sTiempo;
                txtTiempoVueloFV.IsValid = true;
            }

            if ((txtTiempoVueloFV.Text.S() != string.Empty && txtTiempoVueloFV.Text.S() != "00:00") && ddlMatriculaFV.Value.S().I() > 0 && hfNalExtFor != null)
            {
                ObtieneCostosVueloFV();
            }
        }
        
        protected void btnAgregarFV_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaModal())
                {
                    //DateTime FechaSalida = txtFechaSalidaFV.Value.Dt();

                    //sTime = txtTiempoVueloFV.Text;

                    //FechaLlegada = FechaSalida.AddHours(sTime.Substring(0, 2).S().I());
                    //FechaLlegada = FechaLlegada.AddMinutes(sTime.Substring(3, 2).S().I());

                    //if (FechaSalida < FechaLlegada)
                    //{
                        if (dtFerrysHijos == null)
                        {
                            ArmaEstructuraFerrysHijos();
                        }

                        if (dtFerrysHijos != null)
                        {
                            DataRow row = dtFerrysHijos.NewRow();
                            int iIndex = dtFerrysHijos.Rows.Count;

                            row["IdIndex"] = iIndex;
                            row["Trip"] = txtNoTripFV.Text.S();
                            row["Origen"] = ddlOrigenFV.Text.S();
                            row["IdOrigen"] = ddlOrigenFV.Value.S();
                            row["Destino"] = ddlDestinoFV.Text.S();
                            row["IdDestino"] = ddlDestinoFV.Value.S();
                            row["Matricula"] = ddlMatriculaFV.Text.S();
                            //row["FechaInicio"] = txtFechaSalidaFV.Text.S().Dt();
                            row["TiempoVuelo"] = txtTiempoVueloFV.Text.S();
                            row["Costo"] = txtCostoFV.Text.S();
                            row["Iva"] = txtIvaCostoFV.Text.S();

                            dtFerrysHijos.Rows.Add(row);

                            gvFerrysHijos.DataSource = dtFerrysHijos;
                            gvFerrysHijos.DataBind();
                        }
                        
                        LimpiaCamposModal();
                        ppTramos.ShowOnPageLoad = false;
                    //}
                    //else
                    //{

                    //}
                }
                else
                    ppTramos.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregarFV_Click", "Aviso");
            }
        }

        protected void ddlMatricula_SelectedIndexChanged(object sender, EventArgs e)
        {
            BootstrapComboBox ddl = (BootstrapComboBox)sender;
            iIdGrupoModelo = dtMatriculas.Rows[ddlMatricula.SelectedIndex]["GrupoModeloId"].S().I();
            txtGrupoModelo.Text = dtMatriculas.Rows[ddlMatricula.SelectedIndex]["DescGrupoModelo"].S();
            txtNoPasajeros.Text = dtMatriculas.Rows[ddlMatricula.SelectedIndex]["NoPasajeros"].S();

            if (ddlOrigen.Value.S().I() > 0 && ddlDestino.Value.S().I() > 0)
            {
                DatosRemision oDatosContrato = new DatosRemision();
                string sTiempo = Utils.ObtieneTiempoTramosPresupuestos(oDatosContrato, ddlOrigen.Value.S().I(), ddlDestino.Value.S().I(), iIdGrupoModelo);
                txtTiempoVuelo.Text = sTiempo;
            }

            if ((txtTiempoVuelo.Text.S() != string.Empty && txtTiempoVuelo.Text.S() != "00:00:00") && ddl.Value.S().I() > 0 && hfNalExtFor != null)
            {
                ObtieneCostosVuelo();
            }
        }

        protected void btnCancelarFV_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiaCamposModal();
                ppTramos.ShowOnPageLoad = false;
            }
            catch (Exception ex)
            {

                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnCancelarFV_Click", "Aviso");
            }
        }

        protected void btnAddFerry_Click(object sender, EventArgs e)
        {
            LimpiaCamposModal();
            ppTramos.ShowOnPageLoad = true;
            ddlOrigenFV.Enabled = true;
            ddlOrigenFV.ValidationSettings.EnableCustomValidation = true;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtTrip.Text = string.Empty;
            ddlOrigen.SelectedIndex = -1;
            ddlDestino.SelectedIndex = -1;
            ddlMatricula.SelectedIndex = -1;
            txtTiempoVuelo.Text = string.Empty;
            txtNoPasajeros.Text = string.Empty;
            txtGrupoModelo.Text = string.Empty;
            txtHoraInicio.Text = string.Empty;
            txtHoraFin.Text = string.Empty;
            txtCosto.Text = string.Empty;
            txtIvaCosto.Text = string.Empty;

            dtFerrysHijos = null;
            gvFerrysHijos.DataSource = null;
            gvFerrysHijos.DataBind();

            gvMembresias.DataSource = null;
            gvMembresias.DataBind();

            txtTrip.Focus();
        }

        protected void btnGuardarFerryPadre_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaFechasFerry())
                {
                    txtFechaFin.IsValid = true;
                    txtFechaFin.ErrorText = string.Empty;

                    if (eNewObj != null)
                        eNewObj(sender, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtTiempoVuelo_TextChanged(object sender, EventArgs e)
        {
            if ((txtTiempoVuelo.Text.S() != string.Empty && txtTiempoVuelo.Text.S() != "00:00:00") && ddlMatricula.Value.S().I() > 0 && hfNalExtFor != null)
            {
                ObtieneCostosVuelo();
                txtNoPasajeros.Focus();
            }

            txtFechaInicio.Focus();
        }

        protected void gvFerrysHijos_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                string sOpcion = ((DevExpress.Web.Bootstrap.BootstrapButton)e.CommandSource).CommandName.S();
                int iIndex = e.CommandArgs.CommandArgument.S().I();

                if (sOpcion == "EliminarFerryHijo")
                {
                    dtFerrysHijos.Rows.RemoveAt(iIndex);

                    gvFerrysHijos.DataSource = dtFerrysHijos;
                    gvFerrysHijos.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void txtTiempoVueloFV_TextChanged(object sender, EventArgs e)
        {
            if ((txtTiempoVueloFV.Text.S() != string.Empty && txtTiempoVueloFV.Text.S() != "00:00") && ddlMatriculaFV.Value.S().I() > 0 && hfNalExtForFV != null)
            {
                ObtieneCostosVueloFV();
            }
        }

        protected void btnNoConfirmacion_Click(object sender, EventArgs e)
        {
            LimpiaCampos();
            ppConfirm.ShowOnPageLoad = false;
            LimpiaControlesFechas();
        }

        protected void btnSiConfirmacion_Click(object sender, EventArgs e)
        {
            ppConfirm.ShowOnPageLoad = false;
            if (new UtilsServicios().EnviaFerryEzJMexJet(iIdFerry))
            {
                LimpiaCampos();
                LimpiaControlesFechas();
                MostrarMensaje("Aviso", "Ferry publicado");
            }
        }

        protected void txtCosto_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DBVentaFerry().DBGetObtieneCostoVuelo(string.Empty, string.Empty, string.Empty);

            DataTable dtMembresias = new DataTable();
            dtMembresias.Columns.Add("Nombre");
            dtMembresias.Columns.Add("Precio");
            dtMembresias.Columns.Add("Descuento");

            decimal dImporte = txtCosto.Text.S().D() + txtIvaCosto.Text.S().D();
            foreach (DataRow row in ds.Tables[1].Rows)
            {
                DataRow dr = dtMembresias.NewRow();
                dr["Nombre"] = row["TipoCliente"].S();
                dr["Precio"] = "$" + Math.Round((dImporte * ((100 - row["Descuento"].S().D()) / 100)), 2).S();
                dr["Descuento"] = row["Descuento"].S() + "%";

                dtMembresias.Rows.Add(dr);
            }

            gvMembresias.DataSource = dtMembresias;
            gvMembresias.DataBind();
            
            txtTotalCosto.Text = dImporte.ToString("c");

            if (ds.Tables[2] != null)
            {
                string sMensaje = Utils.BuscaErrorEnTabla(ds.Tables[2]);
                if (sMensaje != string.Empty)
                    MostrarMensaje("Aviso", sMensaje);
            }
        }

        protected void txtCostoFV_TextChanged(object sender, EventArgs e)
        {
            decimal dImporte = txtCostoFV.Text.S().D() + txtIvaCostoFV.Text.S().D();
            txtTotalCostoFV.Text = dImporte.ToString("c");
        }

        
        #endregion


        #region METODOS

        private void IniciaControles()
        {
            DateTime dtHoy = DateTime.Now;
            DateTime dtH = new DateTime(dtHoy.Year, dtHoy.Month, dtHoy.Day, 8, 0, 0);

            txtFechaInicio.Date = dtHoy;
            txtFechaFin.Date = dtHoy;

            txtHoraInicio.DateTime = dtH;
            txtHoraFin.DateTime = dtH;

            if (SearchMatricula != null)
                SearchMatricula(null, EventArgs.Empty);

            ddlMatricula.DataSource = dtMatriculas;
            ddlMatricula.ValueField = "IdAeroave";
            ddlMatricula.TextField = "Matricula";
            ddlMatricula.DataBind();

            ddlMatriculaFV.DataSource = dtMatriculas;
            ddlMatriculaFV.ValueField = "IdAeroave";
            ddlMatriculaFV.TextField = "Matricula";
            ddlMatriculaFV.DataBind();

            txtTrip.Focus();
        }

        private bool ValidaModal()
        {
            bool ban = true;

            if (ddlOrigenFV.Text.S() == string.Empty)
            {
                ban = false;
                ddlOrigenFV.IsValid = false;
                ddlOrigenFV.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                ddlOrigenFV.ErrorText = "El campo es requerido.";
            }
            else
                ddlOrigenFV.IsValid = true;


            if (ddlDestinoFV.Text.S() == string.Empty)
            {
                ban = false;
                ddlDestinoFV.IsValid = false;
                ddlDestinoFV.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                ddlDestinoFV.ErrorText = "El campo es requerido.";
            }
            else
                ddlDestinoFV.IsValid = true;


            decimal dValorMinFerry = Utils.GetParametrosClave("119").S().D();
            if (txtCostoFV.Text.S().D() < dValorMinFerry)
            {
                txtCostoFV.IsValid = false;
                txtCostoFV.ErrorText = "El costo minimo de un ferry es " + dValorMinFerry.ToString("c") + ", favor de verificar";
                txtCostoFV.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;

                txtCostoFV.Focus();
                ban = false;
            }
            else
            {
                txtCostoFV.IsValid = true;
                txtCostoFV.ErrorText = string.Empty;
            }


            if (txtTiempoVueloFV.Text.S() == "00:00" || txtTiempoVueloFV.Text.S() == string.Empty)
            {
                ban = false;
                txtTiempoVueloFV.IsValid = false;
                txtTiempoVueloFV.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                txtTiempoVueloFV.ErrorText = "El campo es requerido.";
            }
            else
                txtTiempoVueloFV.IsValid = true;


            return ban;
        }

        private void LimpiaCamposModal()
        {
            txtNoTripFV.Text = string.Empty;
            ddlOrigenFV.DataSource = null;
            ddlOrigenFV.DataBind();
            ddlOrigenFV.Text = string.Empty;

            ddlDestinoFV.DataSource = null;
            ddlDestinoFV.DataBind();
            ddlDestinoFV.Text = string.Empty;

            ddlMatriculaFV.SelectedIndex = 0;
            ddlMatriculaFV.Text = string.Empty;

            txtCostoFV.Text = string.Empty;
            txtIvaCostoFV.Text = string.Empty;

            txtTiempoVueloFV.Text = string.Empty;

            txtNoTripFV.ReadOnly = false;
            ddlMatriculaFV.ReadOnly = false;

            txtTotalCostoFV.Text = string.Empty;
        }

        private void LimpiaCampos()
        {
            txtTrip.Text = string.Empty;
            ddlOrigen.DataSource = null;
            ddlOrigen.DataBind();
            ddlOrigen.Text = string.Empty;

            ddlDestino.DataSource = null;
            ddlDestino.DataBind();
            ddlDestino.Text = string.Empty;

            ddlMatricula.SelectedIndex = 0;
            ddlMatricula.Text = string.Empty;

            txtCosto.Text = string.Empty;
            txtIvaCosto.Text = string.Empty;

            txtTiempoVuelo.Text = string.Empty;

            gvMembresias.DataSource = null;
            gvMembresias.DataBind();

            gvFerrysHijos.DataSource = null;
            gvFerrysHijos.DataBind();

            txtNoPasajeros.Text = string.Empty;
            txtGrupoModelo.Text = string.Empty;

            txtTotalCosto.Text = string.Empty;

            
        }

        private void CargaComboAeropuertosFV(BootstrapComboBox ddl, DataTable dt, string sFiltro)
        {
            try
            {
                ddl.DataSource = dt;
                ddl.TextField = "AeropuertoICAO";
                ddl.ValueField = "idAeropuert";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConfimaPublicacion(string sTitulo, string sMensaje)
        {
            //ppConfirmacion.HeaderText = sTitulo;

            //ppTramos.ShowOnPageLoad = false;
            //ppConfirmacion.ShowOnPageLoad = true;

            //lblMensajeConfirmacion.Text = sMensaje;

            ppConfirm.HeaderText = sTitulo;
            ppConfirm.ShowOnPageLoad = true;
            lblTextoConfirmacion.Text = sMensaje;
        }

        public void MostrarMensaje(string sTitulo, string sMensaje)
        {
            ppMensaje.HeaderText = sTitulo;

            ppTramos.ShowOnPageLoad = false;
            ppMensaje.ShowOnPageLoad = true;

            lblMensaje.Text = sMensaje;

            LimpiaCampos();
        }

        public void ObtieneCostosVuelo()
        {
            DataSet ds = new DBVentaFerry().DBGetObtieneCostoVuelo(txtTiempoVuelo.Text.S(), ddlMatricula.Text.S(), hfNalExtFor["hfNalExtFor"].S());

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtCosto.Text = ds.Tables[0].Rows[0]["TarifaFinal"].S();
                txtIvaCosto.Text = ds.Tables[0].Rows[0]["IvaTarifa"].S();
                
                decimal dImporteAux = txtCosto.Text.S().D() + txtIvaCosto.Text.S().D();
                txtTotalCosto.Text = dImporteAux.ToString("c");
            }

            decimal dImporte = txtCosto.Text.S().D() + txtIvaCosto.Text.S().D();
            DataTable dtMembresias = new DataTable();
            dtMembresias.Columns.Add("Nombre");
            dtMembresias.Columns.Add("Precio");
            dtMembresias.Columns.Add("Descuento");
            foreach (DataRow row in ds.Tables[1].Rows)
            {
                DataRow dr = dtMembresias.NewRow();
                dr["Nombre"] = row["TipoCliente"].S();
                dr["Precio"] = "$" + Math.Round((dImporte * ((100 - row["Descuento"].S().D()) / 100)), 2).S();
                dr["Descuento"] = row["Descuento"].S() + "%";

                dtMembresias.Rows.Add(dr);
            }

            gvMembresias.DataSource = dtMembresias;
            gvMembresias.DataBind();

            if (ds.Tables[2] != null)
            {
                string sMensaje = Utils.BuscaErrorEnTabla(ds.Tables[2]);
                if (sMensaje != string.Empty)
                    MostrarMensaje("Aviso", sMensaje);
            }
        }

        public void ObtieneCostosVueloFV()
        {
            DataSet ds = new DBVentaFerry().DBGetObtieneCostoVuelo(txtTiempoVueloFV.Text.S(), ddlMatriculaFV.Text.S(), hfNalExtForFV["hfNalExtForFV"].S());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtCostoFV.Text = ds.Tables[0].Rows[0]["TarifaFinal"].S();
                txtIvaCostoFV.Text = ds.Tables[0].Rows[0]["IvaTarifa"].S();

                txtTotalCostoFV.Text = (txtCostoFV.Text.S().D() + txtIvaCostoFV.Text.S().D()).ToString("c");
            }

            string sMensaje = Utils.BuscaErrorEnTabla(ds.Tables[2]);
            if (sMensaje != string.Empty)
                MostrarMensaje("Aviso", sMensaje);
        }

        public void ArmaEstructuraFerrysHijos()
        {
            dtFerrysHijos = new DataTable();
            
            dtFerrysHijos.Columns.Add("IdIndex");
            dtFerrysHijos.Columns.Add("Trip");
            dtFerrysHijos.Columns.Add("Origen");
            dtFerrysHijos.Columns.Add("IdOrigen");
            dtFerrysHijos.Columns.Add("Destino");
            dtFerrysHijos.Columns.Add("IdDestino");
            dtFerrysHijos.Columns.Add("Matricula");
            //dtFerrysHijos.Columns.Add("FechaInicio");
            dtFerrysHijos.Columns.Add("TiempoVuelo");
            dtFerrysHijos.Columns.Add("Costo");
            dtFerrysHijos.Columns.Add("Iva");
        }

        private bool ValidaFechasFerry()
        {
            bool ban = true;

            if (txtFechaFin.Text.S().Dt() < txtFechaInicio.Text.S().Dt())
            {
                txtFechaFin.IsValid = false;
                txtFechaFin.ErrorText = "La fecha y hora fin deben ser mayor que la fecha hora del inicio";

                txtFechaFin.Focus();
                ban = false;
            }
            else
            {
                txtFechaFin.IsValid = true;
                txtFechaFin.ErrorText = string.Empty;
            }


            float dini = Utils.ConvierteTiempoaDecimal(txtHoraInicio.Text.S());
            float dfin = Utils.ConvierteTiempoaDecimal(txtHoraFin.Text.S());

            if (txtFechaFin.Text.S().Dt() == txtFechaInicio.Text.S().Dt() && (dfin <= dini))
            {
                txtFechaFin.IsValid = false;
                txtFechaFin.ErrorText = "La hora fin debe ser mayor que la hora inicio de publicacion.";

                txtHoraFin.Focus();

                ban = false;
            }
            else
            {
                txtFechaFin.IsValid = true;
                txtFechaFin.ErrorText = string.Empty;
            }


            if (txtTiempoVuelo.Text.S() == "00:00" || txtTiempoVuelo.Text == string.Empty)
            {
                txtTiempoVuelo.IsValid = false;
                txtTiempoVuelo.ErrorText = "El tiempo de vuelo no puede estar vacio.";

                txtTiempoVuelo.Focus();

                ban = false;
            }
            else
            {
                txtTiempoVuelo.IsValid = true;
                txtTiempoVuelo.ErrorText = string.Empty;
            }

            decimal dValorMinFerry = Utils.GetParametrosClave("119").S().D();
            if (txtCosto.Text.S().D() < dValorMinFerry)
            {
                txtCosto.IsValid = false;
                txtCosto.ErrorText = "El costo minimo de un ferry es " + dValorMinFerry.ToString("c") + ", favor de verificar";
                txtCosto.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;

                txtCosto.Focus();
                ban = false;
            }
            else
            {
                txtCosto.IsValid = true;
                txtCosto.ErrorText = string.Empty;
            }

            return ban;
        }

        private void LimpiaControlesFechas()
        {
            DateTime dtHoy = DateTime.Now;
            DateTime dtH = new DateTime(dtHoy.Year, dtHoy.Month, dtHoy.Day, 8, 0, 0);

            txtFechaInicio.Date = dtHoy;
            txtFechaFin.Date = dtHoy;

            txtHoraInicio.DateTime = dtH;
            txtHoraFin.DateTime = dtH;
        }
        #endregion


        #region VARIABLES Y PROPIEDAES

        private const string sPagina = "frmPubFerrys.aspx";
        private const string sClase = "frmPubFerrys.aspx.cs";
        
        PubFerrys_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eLoadOrigDestFiltro;
        public event EventHandler eLoadOrigDestFiltroDest;
        public event EventHandler eSavFerryPendiente;
        public event EventHandler SearchMatricula;

        public int iIdFerry
        {
            get { return (int)ViewState["VSiIdFerry"]; }
            set { ViewState["VSiIdFerry"] = value; }
        }
        public string sFiltroO
        {
            get { return (string)ViewState["VSFiltroO"]; }
            set { ViewState["VSFiltroO"] = value; }
        }
        public string sFiltroD
        {
            get { return (string)ViewState["VSFiltroD"]; }
            set { ViewState["VSFiltroD"] = value; }
        }
        public DataTable dtOrigen
        {
            get { return (DataTable)ViewState["VSOrigen"]; }
            set { ViewState["VSOrigen"] = value; }
        }
        public DataTable dtDestino
        {
            get { return (DataTable)ViewState["VSDestino"]; }
            set { ViewState["VSDestino"] = value; }
        }
        public string sTime = string.Empty;
        public DateTime FechaLlegada;
        public int iIdGrupoModelo
        {
            get { return ViewState["VSGrupoModelo"].S().I(); }
            set { ViewState["VSGrupoModelo"] = value; }
        }
        public DataTable dtMatriculas
        {
            get { return (DataTable)ViewState["VSMatriculas"]; }
            set { ViewState["VSMatriculas"] = value; }
        }
        public DataTable dtFerrysHijos
        {
            set { ViewState["VSFerrysHijos"] = value; }
            get { return (DataTable)ViewState["VSFerrysHijos"]; }
        }
        public OfertaFerry oFerrysPadre
        {
            get
            {
                OfertaFerry oF = new OfertaFerry();

                oF.iTrip = txtTrip.Text.S().I();
                oF.sOrigen = ddlOrigen.Value.S();
                oF.sDestino = ddlDestino.Value.S();
                oF.dCostoVuelo = txtCosto.Text.S().D();
                oF.dIvaVuelo = txtIvaCosto.Text.S().D();
                
                DateTime dtInicio = (txtFechaInicio.Text.S() + " " + txtHoraInicio.Text.S()).Dt();
                DateTime dtFin = (txtFechaFin.Text.S() + " " + txtHoraFin.Text.S()).Dt();
                
                oF.dtFechaInicio = dtInicio;
                oF.dtFechaFin = dtFin;
                oF.sTiempoVuelo = txtTiempoVuelo.Text.S();
                oF.sMatricula = ddlMatricula.Text.S();
                oF.sGrupoModelo = dtMatriculas.Rows[ddlMatricula.SelectedIndex]["DescGrupoModelo"].S();
                oF.iNoPax = txtNoPasajeros.Text.S().I();
                oF.iIdPadre = 0;
                oF.iIdFerry = 0;
                oF.iIdGrupoModelo = iIdGrupoModelo;
                oF.sReferencia = string.Empty;
                
                return oF;
            }
        }
        public List<OfertaFerry> oLstFerrysHijo
        {
            get
            {
                List<OfertaFerry> olst = new List<OfertaFerry>();

                if (dtFerrysHijos != null)
                {
                    foreach (DataRow row in dtFerrysHijos.Rows)
                    {
                        OfertaFerry oF = new OfertaFerry();

                        oF.iIdFerry = 0;
                        oF.iTrip = row["Trip"].S().I();
                        oF.sOrigen = row["IdOrigen"].S();
                        oF.sDestino = row["IdDestino"].S();
                        oF.dtFechaInicio = txtFechaInicio.Text.S().Dt();
                        oF.dtFechaFin = txtFechaFin.Text.S().Dt();
                        oF.sMatricula = row["Matricula"].S();
                        oF.iNoPax = txtNoPasajeros.Text.S().I();
                        oF.sTiempoVuelo = row["TiempoVuelo"].S();
                        oF.dCostoVuelo = row["Costo"].S().D();
                        oF.dIvaVuelo = row["Iva"].S().D();
                        oF.sReferencia = string.Empty;
                        oF.iIdPadre = 0;

                        olst.Add(oF);
                    }
                }

                return olst;
            }
        }


        #endregion

        
    }
}