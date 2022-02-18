using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using DevExpress.Web;
using ALE_MexJet.Clases;
using System.Text.RegularExpressions;
using DevExpress.Web.Bootstrap;

namespace ALE_MexJet.Views.JetSmart
{
    public partial class frmVentaFerrys : System.Web.UI.Page, IViewVentaFerry
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mpeMensaje.OkButtonPressed += mpeMensaje_OkButtonPressed;
                oPresenter = new VentaFerry_Presentar(this, new DBVentaFerry());

                if (!IsPostBack)
                {
                    hfNacionalFV["hfNacionalFV"] = Utils.ObtieneParametroPorClave("9");
                    hfExtranjeroFV["hfExtranjeroFV"] = Utils.ObtieneParametroPorClave("10");

                    IniciaControles();
                    iIndex = 0;
                }

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception x) { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "Page_Load", "Aviso"); }
        }
        private void mpeMensaje_OkButtonPressed(object sender, EventArgs args)
        {

        }
        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //ASPxButton btn = (ASPxButton)sender;
                //GridViewRow row = (GridViewRow)btn.NamingContainer;
                //int iIndex = row.RowIndex;
                
                //lblTRIP.Text = dtFerrys.Rows[iIndex]["Trip"].S();
                //lblMatricula.Text = dtFerrys.Rows[iIndex]["Matricula"].S();
                //lblOrigen.Text = dtFerrys.Rows[iIndex]["Origen"].S();
                //lblFechaSalida.Text = dtFerrys.Rows[iIndex]["FechaSalida"].S();
                //lblDestino.Text = dtFerrys.Rows[iIndex]["Destino"].S();
                //lblDestino.Text = dtFerrys.Rows[iIndex]["Destino"].S();
                //iIdFerry = dtFerrys.Rows[iIndex]["IdFerry"].S().I();
                //hfNalExtFor["hfNalExtFor"] = dtFerrys.Rows[iIndex]["TipoDestino"].S();

                //LimpiaControlesVenta();

                ////DataTable dtCosto = new DBVentaFerry().DBGetObtieneCostoVuelo(iIdFerry, 0);
                ////if (dtCosto != null)
                ////{
                ////    txtCostoVuelo.Text = dtCosto.Rows[0]["TarifaFinal"].S().D().S();
                ////    txtCostoIVA.Text = dtCosto.Rows[0]["IvaTarifa"].S().D().S();
                ////}

                //string sHotDeal = ddlPrioridad.Text == "Baja" ? "No Hot Deal" : "Hot Deal";

                //lblMsg.Text = "El Ferry se ofertará del " + dtFerrys.Rows[iIndex]["FechaSalida"].S().Dt().ToLongDateString() + " a las " + 
                //    dtFerrys.Rows[iIndex]["FechaSalida"].S().Dt().ToShortTimeString() + " al ____ con un costo de:" +
                //    (txtCostoVuelo.Text.S().D() + txtCostoIVA.Text.S().D()).ToString("c") + " y será: " + sHotDeal + ".";

                //ppVenta.ShowOnPageLoad = true;
            }
            catch (Exception x)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "btnGuardar_Click", "Aviso");
            }
        }
        protected void ddlPrioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //DataTable dtCosto = new DBVentaFerry().DBGetObtieneCostoVuelo(iIdFerry, ddlPrioridad.Value.S().I());
                //if (dtCosto != null)
                //{
                //    txtCostoVuelo.Text = dtCosto.Rows[0]["TarifaFinal"].S().D().S();
                //    txtCostoIVA.Text = dtCosto.Rows[0]["IvaTarifa"].S().D().S();

                //    string sCad = string.Empty;
                //    sCad = ddlPrioridad.Text == "Baja" ? "No Hot Deal" : "Hot Deal";

                //    int index = lblMsg.Text.IndexOf("costo de:");
                //    lblMsg.Text = lblMsg.Text.Substring(0, (index + 9)) + 
                //        (txtCostoVuelo.Text.S().D() + txtCostoIVA.Text.S().D()).ToString("c") + " y será: " + sCad + ".";
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void gvFerrysVenta_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            string sOpcion = ((DevExpress.Web.ASPxButton)e.CommandSource).CommandName.S();
            iIdFerry = e.CommandArgs.CommandArgument.S().I();

            object cellValues = gvFerrysVenta.GetRowValuesByKeyValue(iIdFerry, "Trip" //0
                                                                                , "Origen" //1
                                                                                , "FechaSalida" //2
                                                                                , "Destino" //3
                                                                                , "FechaLlegada" //4
                                                                                , "TiempoVuelo" //5
                                                                                , "Matricula" //6
                                                                                , "GrupoModelo" //7
                                                                                , "IdFerry" //8
                                                                                , "TipoDestino" //9
                                                                                , "Status" // 10
                                                                                , "FechaCancelacion" //11
                                                                                , "UsuarioCancelacion" //12
                                                                                , "UsuarioCreacion" //13
                                                                                , "FechaCreacion" //14

                                                                                , "UsuarioPay" // 15
                                                                                , "FechaCompra" //16
                                                                                , "ImporteFinal" //17
                                                                                , "MetodoPago" //18
                                                                                , "TipoCambio" //19
                                                                                , "Membresia" //20
                                                                                , "NoPax" //21
            );

            if (sOpcion == "PublicarFerry")
            {
                if (new UtilsServicios().EnviaFerryEzJMexJet(iIdFerry))
                {
                    if (eSearchObj != null)
                        eSearchObj(sender, e);
                }
            }

            if (sOpcion == "AgregarFerry")
            {
                dtFechaIniFV = ((object[])cellValues)[2].S().Dt();
                dtFechaFinFV = ((object[])cellValues)[4].S().Dt();

                txtNoTripFV.Text = ((object[])cellValues)[0].S();
                ddlMatriculaFV.Value = ((object[])cellValues)[6].S();
                string sMat = ((object[])cellValues)[6].S();

                int iIndex = 0;
                for (int i = 0; i < dtMatriculas.Rows.Count; i++)
                {
                    if (sMat == dtMatriculas.Rows[i]["Matricula"].S())
                    {
                        iIndex = i;
                        break;
                    }
                }

                ddlMatriculaFV.SelectedIndex = iIndex;
                ddlMatriculaFV_SelectedIndexChanged(sender, e);

                ppTramos.ShowOnPageLoad = true;
            }

            if (sOpcion == "EliminarFerry")
            {
                if (new UtilsServicios().ActualizaEstatusFerryEzJMexJet(iIdFerry, 0))
                {
                    if (eSearchObj != null)
                        eSearchObj(sender, e);

                    MostrarMensaje("Ferry eliminado correctamente.", "Aviso");
                }
            }

            if (sOpcion == "Info")
            {
                int iStatus = ((object[])cellValues)[10].S().I();
                if (iStatus == 2)
                    tabConFacturacion.ActiveTabIndex = 1;
                if (iStatus == 0)
                    tabConFacturacion.ActiveTabIndex = 2;

                txtUsuarioRegistro.Text = ((object[])cellValues)[13].S();
                txtFechaRegistro.Text = ((object[])cellValues)[14].S();

                txtFechaCancelacion.Text = ((object[])cellValues)[11].S();
                txtUsuarioCancelacion.Text = ((object[])cellValues)[12].S();
                
                txtUsuarioCompra.Text = ((object[])cellValues)[15].S();
                txtFechaCompra.Text = ((object[])cellValues)[16].S();
                txtPrecioCompra.Text = ((object[])cellValues)[17].S();
                txtMetodoPago.Text = ((object[])cellValues)[18].S();
                txtTipoCambio.Text = ((object[])cellValues)[19].S();
                txtMembresiaCliente.Text = ((object[])cellValues)[20].S();

                dtLog = new DBVentaFerry().DBGetObtieneHistoricoOfertaFerry(iIdFerry);
                gvLogModificaciones.DataSource = dtLog;
                gvLogModificaciones.DataBind();

                ppInformacionFerry.ShowOnPageLoad = true;
            }

            if (sOpcion == "Edit")
            {
                IniciaControlesEdit(((object[])cellValues)[2].S().Dt(), ((object[])cellValues)[4].S().Dt());
                txtNoPax.Text = ((object[])cellValues)[21].S();
                ppEdicionFerry.ShowOnPageLoad = true;
            }
        }
        protected void gvFerrysVenta_PageIndexChanged(object sender, EventArgs e)
        {
            

        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            gvFerrysVenta.ExportXlsToResponse();
        }
        protected void gvFerrysDetalle_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView grid = sender as ASPxGridView;

                iIdPadre = grid.GetMasterRowKeyValue().S().I();

                if (eSearchFerryHijo != null)
                {
                    eSearchFerryHijo(sender, e);
                    grid.DataSource = dtFerrysHijo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvFerrysDetalle_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            string sOpcion = ((DevExpress.Web.ASPxButton)e.CommandSource).CommandName.S();
            iIdFerry = e.CommandArgs.CommandArgument.S().I();

            if (sOpcion == "EliminarFerry")
            {
                if (new UtilsServicios().ActualizaEstatusFerryEzJMexJet(iIdFerry, 0))
                {
                    MostrarMensaje("Ferry eliminado correctamente.", "Aviso");
                }
            }
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
                string sTiempo = Utils.ObtieneTiempoTramosPresupuestos(oDatosContrato, ddlOrigenFV.Value.S().I(), ddlDestinoFV.Value.S().I(), iIdGrupoModeloFV);
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
            try
            {
                //iIdGrupoModeloFV = dtMatriculas.Rows[ddlMatriculaFV.SelectedIndex]["GrupoModeloId"].S().I();

                //if (ddlOrigenFV.Value.S().I() > 0 && ddlDestinoFV.Value.S().I() > 0)
                //{
                //    DatosRemision oDatosContrato = new DatosRemision();
                //    string sTiempo = Utils.ObtieneTiempoTramosPresupuestos(oDatosContrato, ddlOrigenFV.Value.S().I(), ddlDestinoFV.Value.S().I(), iIdGrupoModeloFV);
                //    txtTiempoVueloFV.Text = sTiempo;
                //    txtTiempoVueloFV.IsValid = true;
                //}

                //if ((txtTiempoVueloFV.Text.S() != string.Empty && txtTiempoVueloFV.Text.S() != "00:00") && ddlMatriculaFV.SelectedIndex > 0 && hfNalExtForFV != null)
                //{
                //    ObtieneCostosVueloFV();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtTiempoVueloFV_ValueChanged(object sender, EventArgs e)
        {
            if ((txtTiempoVueloFV.Text.S() != string.Empty && txtTiempoVueloFV.Text.S() != "00:00") && ddlMatriculaFV.SelectedIndex > 0 && hfNalExtForFV != null)
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
                    if (eSaveFerryHijo != null)
                        eSaveFerryHijo(sender, e);

                    if (eSearchObj != null)
                        eSearchObj(sender, e);

                    LimpiaCamposModal();
                    ppTramos.ShowOnPageLoad = false;
                }
                else
                    ppTramos.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregarFV_Click", "Aviso");
            }
        }
        protected void btnAceptarEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSaveInformacionFerry != null)
                    eSaveInformacionFerry(sender, e);
                
                ppEdicionFerry.ShowOnPageLoad = false;

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvLogModificaciones_PageIndexChanged(object sender, EventArgs e)
        {

        }
        protected void txtCostoFV_TextChanged(object sender, EventArgs e)
        {
            //decimal dIvaNal = 0;
            //decimal dIvaInt = 0;
            //if (hfNalExtForFV.Value.S() == "F" || hfNalExtForFV.Value.S() == "N")
            //{
            //    dIvaNal = hfNacionalFV["hfNacionalFV"].S().D();
            //    txtIvaCostoFV.Text = (dIvaNal * txtCostoFV.Text.S().D()).S();
            //}
            //else
            //{
            //    dIvaInt = hfExtranjeroFV["hfExtranjeroFV"].S().D();
            //    txtIvaCostoFV.Text = (dIvaInt * txtCostoFV.Text.S().D()).S();
            //}

            decimal dImporte = txtCostoFV.Text.S().D() + txtIvaCostoFV.Text.S().D();
            txtTotalCostoFV.Text = dImporte.ToString("c");
        }
        
        #endregion

        #region METODOS
        public void LoadFerrys(DataTable dt)
        {
            try
            {
                dtFerrys = dt;
                //gvFerrys.DataSource = dt;
                //gvFerrys.DataBind();

                ViewState["Ferrys"] = dt;
                
                gvFerrysVenta.DataSource = dt;
                gvFerrysVenta.DataBind();

            }
            catch (Exception x) { throw x; }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                mpeMensaje.ShowMessage(sMensaje, sCaption);
            }
            catch (Exception x)
            {
                throw x;
            }
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
        private void IniciaControles()
        {
            if (eSearchMatricula != null)
                eSearchMatricula(null, EventArgs.Empty);
            
            ddlMatriculaFV.DataSource = dtMatriculas;
            ddlMatriculaFV.ValueField = "IdAeroave";
            ddlMatriculaFV.TextField = "Matricula";
            ddlMatriculaFV.DataBind();
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
        public void ObtieneCostosVueloFV()
        {
            DataSet ds = new DBVentaFerry().DBGetObtieneCostoVuelo(txtTiempoVueloFV.Text.S(), ddlMatriculaFV.Text.S(), hfNalExtForFV["hfNalExtForFV"].S());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtCostoFV.Text = ds.Tables[0].Rows[0]["TarifaFinal"].S();
                txtIvaCostoFV.Text = ds.Tables[0].Rows[0]["IvaTarifa"].S();

                txtCostoFV.IsValid = true;
                txtIvaCostoFV.IsValid = true;

                txtTotalCostoFV.Text = (txtCostoFV.Text.S().D() + txtIvaCostoFV.Text.S().D()).ToString("c");

                if (ds.Tables[2] != null)
                {
                    string sMensaje = Utils.BuscaErrorEnTabla(ds.Tables[2]);
                    if (sMensaje != string.Empty)
                        MostrarMensaje(sMensaje, "Aviso");
                }
            }
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

        }
        private void IniciaControlesEdit(DateTime dtInicio, DateTime dtFin)
        {
            txtFechaInicio.Date = dtInicio;
            txtFechaFin.Date = dtFin;

            txtHoraInicio.DateTime = dtInicio;
            txtHoraFin.DateTime = dtFin;
        }
        #endregion

        #region VARIABLES Y PROPIEDADES
        VentaFerry_Presentar oPresenter;
        private const string sPagina = "frmVentaFerrys.aspx";
        private const string sClase = "frmVentaFerrys.aspx.cs";

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchFerryHijo;
        public event EventHandler eLoadOrigDestFiltro;
        public event EventHandler eLoadOrigDestFiltroDest;
        public event EventHandler eSearchMatricula;
        public event EventHandler eSaveFerryHijo;
        public event EventHandler eSaveInformacionFerry;

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
        public DataTable dtMatriculas
        {
            get { return (DataTable)ViewState["VSMatriculas"]; }
            set { ViewState["VSMatriculas"] = value; }
        }
        public DataTable dtFerrys
        {
            get { return (DataTable)ViewState["VSOfertaFerry"]; }
            set { ViewState["VSOfertaFerry"] = value; }
        }
        public VentaFerry oVFerry
        {
            get { return (VentaFerry)ViewState["VSTFerry"]; }
            set { ViewState["VSTFerry"] = value; }
        }
        public OfertaFerry oVFerry2
        {
            get { return (OfertaFerry)ViewState["VSTFerry2"]; }
            set { ViewState["VSTFerry2"] = value; }
        }
        public int iIdFerry
        {
            get { return ViewState["iIdFerry"].S().I(); }
            set { ViewState["iIdFerry"] = value; }
        }
        public string sTipoDestino
        {
            get { return ViewState["sTipoDestino"].S(); }
            set { ViewState["sTipoDestino"] = value; }
        }
        public int iIndex
        {
            get { return ViewState["VSIndex"].S().I(); }
            set { ViewState["VSIndex"] = value; }
        }
        public int iIdPadre
        {
            get { return ViewState["VSiIdPadre"].I(); }
            set { ViewState["VSiIdPadre"] = value; }
        }
        public DataTable dtFerrysHijo
        {
            get { return (DataTable)ViewState["VSFerrysHijo"]; }
            set { ViewState["VSFerrysHijo"] = value; }
        }
        public int iIdGrupoModeloFV
        {
            get { return ViewState["VSiIdGrupoModeloFV"].I(); }
            set { ViewState["VSiIdGrupoModeloFV"] = value; }
        }
        public DateTime dtFechaIniFV
        {
            get { return (DateTime)ViewState["VSFechaIni"]; }
            set { ViewState["VSFechaIni"] = value; }
        }
        public DateTime dtFechaFinFV
        {
            get { return (DateTime)ViewState["VSFechaFinFV"]; }
            set { ViewState["VSFechaFinFV"] = value; }
        }
        public OfertaFerry oFerrysHijo
        {
            get
            {
                OfertaFerry oF = new OfertaFerry();
                
                oF.iIdFerry = 0;
                oF.iTrip = txtNoTripFV.Text.S().I();
                oF.sOrigen = ddlOrigenFV.Value.S();
                oF.sDestino = ddlDestinoFV.Value.S();
                oF.dtFechaInicio = dtFechaIniFV;
                oF.dtFechaFin = dtFechaFinFV;
                oF.sMatricula = ddlMatriculaFV.Text.S();
                oF.sTiempoVuelo = txtTiempoVueloFV.Text.S();
                oF.dCostoVuelo = txtCostoFV.Text.S().D();
                oF.dIvaVuelo = txtIvaCostoFV.Text.S().D();
                oF.iIdPadre = iIdFerry;
                oF.sReferencia = string.Empty;

                return oF;
            }
        }
        public InformacionFerry oInfoFerry
        {
            get
            {
                InformacionFerry oInfFerry = new InformacionFerry();
                oInfFerry.iIdFerry = iIdFerry;
                oInfFerry.dtInicio = (txtFechaInicio.Text.S() + " " + txtHoraInicio.Text.S()).Dt();
                oInfFerry.dtFin = (txtFechaFin.Text.S() + " " + txtHoraFin.Text.S()).Dt();
                oInfFerry.iNoPax = txtNoPax.Text.S().I();

                return oInfFerry;
            }
        }
        public DataTable dtLog
        {
            get { return (DataTable)ViewState["VSdtLog"]; }
            set { ViewState["VSdtLog"] = value; }
        }




        #endregion
    }
}