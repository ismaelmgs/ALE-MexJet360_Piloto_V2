using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALE_MexJet.Views.bitacoras
{
    public partial class AltaBitacora : System.Web.UI.Page, IViewBitacoras
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Bitacoras_Presenter(this, new DBBitacoras());
            gvBitacoras.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvBitacoras.SettingsPager.ShowDisabledButtons = true;
            gvBitacoras.SettingsPager.ShowNumericButtons = true;
            gvBitacoras.SettingsPager.ShowSeparators = true;
            gvBitacoras.SettingsPager.Summary.Visible = true;
            gvBitacoras.SettingsPager.PageSizeItemSettings.Visible = true;
            gvBitacoras.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvBitacoras.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";

            sParametro = "";
            if (eSearchObj != null)
                eSearchObj(sender, e);

            if (!IsPostBack)
            {
                if (eSearchTipo != null)
                    eSearchTipo(sender, e);
            }
        }
        protected void gvBitacoras_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Actualiza")
                {
                    LimpiarControles();
                    int index = e.VisibleIndex.I();
                    int iIdBitacora = gvBitacoras.GetRowValues(index, "IdBitacora").S().I();
                    string[] fieldValues = { "AeronaveSerie", "AeronaveMatricula", "VueloClienteId", "VueloContratoId", "PilotoId", "CopilotoId", "Fecha", "Origen", "Destino", "OrigenVuelo", "OrigenCalzo",
                                             "ConsumoOri", "CantPax", "Tipo", "DestinoVuelo", "DestinoCalzo", "ConsumoDes", "TripNum", "Leg_Num", "LogNum", "LegId", "FolioReal" };

                    object obj = gvBitacoras.GetRowValues(index, fieldValues);
                    object[] oB = (object[])obj;
                    if (oB.Length > 0)
                    {
                        txtMatricula.Text = oB[1].S();
                        txtVueloContratoId.Text = oB[3].S();
                        txtPilotoId.Text = oB[4].S();
                        txtCopilotoId.Text = oB[5].S();
                        txtFecha.Value = oB[6].S();
                        txtOrigen.Text = oB[7].S();
                        txtDestino.Text = oB[8].S();
                        txtOrigenVuelo.Value = oB[9].S();
                        txtOrigenCalzo.Value = oB[10].S();
                        txtConsumoOrigen.Text = oB[11].S();
                        txtCantPax.Text = oB[12].S();
                        ddlTipo.Value = oB[13].S();
                        txtDestinoVuelo.Value = oB[14].S();
                        txtDestinoCalzo.Value = oB[15].S();
                        txtConsumoDestino.Text = oB[16].S();
                        txtTrip.Text = oB[17].S();
                        txtLegNum.Text = oB[18].S();
                        txtLongNum.Text = oB[19].S();
                        txtLegId.Text = oB[20].S();
                        txtFolioReal.Text = oB[21].S();
                        CalcularTiempoConsumo(1);
                        CalcularTiempoConsumo(2);
                        CalcularTiempoConsumo(3);
                        ppBitacora.ShowOnPageLoad = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eNewObj != null)
                    eNewObj(sender, e);

                ppBitacora.ShowOnPageLoad = false;

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnBuscarBitacora_Click(object sender, EventArgs e)
        {
            try
            {
                //sParametro = ddlBusquedaBitacora.SelectedItem.Value.S();
                sParametro = txtBusqueda.Text;

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvBitacoras_HtmlDataCellPrepared(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewTableDataCellEventArgs e)
        {
            try
            {
                if (e.DataColumn.FieldName == "Remisionado")
                {

                    BootstrapGridView gvB = (BootstrapGridView)(sender as BootstrapGridView);
                    Label rdRem = gvB.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "readRemisionado") as Label;
                    ASPxButton btnActualizar = gvB.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btnActualizar") as ASPxButton;

                    if (rdRem.Text.I() > 0)
                    {
                        btnActualizar.Enabled = false;
                        btnActualizar.Text = "Remisionado";
                    }
                    else
                    {
                        btnActualizar.Enabled = true;
                        btnActualizar.Text = "Actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvBitacoras_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = gvBitacoras.PageIndex;
                gvBitacoras.PageIndex = pageIndex;
                gvBitacoras.DataSource = dtBitacoras;
                gvBitacoras.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnNuevaBitacora_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            ppBitacora.ShowOnPageLoad = true;
        }
        protected void txtOrigenCalzo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                CalcularTiempoConsumo(1);
                txtOrigenVuelo.Value = txtOrigenCalzo.Text.Dt().AddMinutes(5).S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtDestinoCalzo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                CalcularTiempoConsumo(1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtConsumoOrigen_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                CalcularTiempoConsumo(3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtOrigenVuelo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                CalcularTiempoConsumo(2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtDestinoVuelo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                CalcularTiempoConsumo(2);
                txtDestinoCalzo.Value = txtDestinoVuelo.Text.Dt().AddMinutes(5).S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtConsumoDestino_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                CalcularTiempoConsumo(3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MÉTODOS
        public void LoadBitacoras(DataTable dt)
        {
            try
            {
                dtBitacoras = null;
                dtBitacoras = dt;
                gvBitacoras.DataSource = dt;
                gvBitacoras.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadTipo(DataTable dt)
        {
            try
            {
                ddlTipo.DataSource = dt;
                ddlTipo.ValueField = "tipo";
                ddlTipo.TextField = "tipo";
                ddlTipo.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LimpiarControles()
        {
            txtMatricula.Text = string.Empty;
            txtVueloContratoId.Text = string.Empty;
            txtPilotoId.Text = string.Empty;
            txtCopilotoId.Text = string.Empty;
            txtFecha.Value = string.Empty;
            txtOrigen.Text = string.Empty;
            txtDestino.Text = string.Empty;
            txtOrigenVuelo.Value = string.Empty;
            txtOrigenCalzo.Value = string.Empty;
            txtConsumoOrigen.Text = string.Empty;
            txtCantPax.Text = string.Empty;
            ddlTipo.SelectedIndex = -1;
            txtDestinoVuelo.Value = string.Empty;
            txtDestinoCalzo.Value = string.Empty;
            txtConsumoDestino.Text = string.Empty;
            txtTrip.Text = string.Empty;
            txtLegNum.Text = string.Empty;
            txtLongNum.Text = string.Empty;
            txtLegId.Text = string.Empty;
            txtFolioReal.Text = string.Empty;
        }
        public void CalcularTiempoConsumo(int iOpcion)
        {
            try
            {
                switch (iOpcion)
                {
                    //Calcular Tiempo Calzo
                    case 1:
                        TimeSpan tsTiempoCalzo;

                        if (!string.IsNullOrEmpty(txtDestinoCalzo.Text) && !string.IsNullOrEmpty(txtOrigenCalzo.Text))
                        {
                            DateTime dtOrigen = txtOrigenCalzo.Text.Dt();
                            DateTime dtDestino = txtDestinoCalzo.Text.Dt();
                            tsTiempoCalzo = dtDestino.Subtract(dtOrigen).Duration();
                            txtTiempoCalzo.Text = tsTiempoCalzo.S().Substring(0,5);
                        }
                        break;
                    //Calcular Tiempo Vuelo
                    case 2:
                        TimeSpan tsTiempoVuelo;

                        if (!string.IsNullOrEmpty(txtDestinoVuelo.Text) && !string.IsNullOrEmpty(txtOrigenVuelo.Text))
                        {
                            DateTime dtOrigen = txtOrigenVuelo.Text.Dt();
                            DateTime dtDestino = txtDestinoVuelo.Text.Dt();
                            tsTiempoVuelo = dtDestino.Subtract(dtOrigen).Duration();
                            txtTiempoVuelo.Text = tsTiempoVuelo.S().Substring(0, 5);
                        }
                        break;
                    //Calcula consumo
                    case 3:
                        double dbOrigen = 0;
                        double dbDestino = 0;
                        double dbConsumoTotal = 0;

                        if (!string.IsNullOrEmpty(txtConsumoOrigen.Text))
                            dbOrigen = txtConsumoOrigen.Text.Db();

                        if (!string.IsNullOrEmpty(txtConsumoDestino.Text))
                            dbDestino = txtConsumoDestino.Text.Db();

                        dbConsumoTotal = dbOrigen - dbDestino;
                        txtCombustibleUsado.Text = dbConsumoTotal.S();

                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region VARIABLES Y PROPIEDADES
        Bitacoras_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchTipo;
        public int iOk
        {
            get { return (int)ViewState["VSOK"]; }
            set { ViewState["VSOK"] = value; }
        }
        public string sParametro
        {
            get { return (string)ViewState["VSParametro"]; }
            set { ViewState["VSParametro"] = value; }
        }
        public Bitacoras oBi
        {
            get
            {
                Bitacoras oBit = new Bitacoras();
                oBit.SMatricula = txtMatricula.Text;
                oBit.LTripNum = txtTrip.Text.L();
                oBit.ILegNum = txtLegNum.Text.I();
                oBit.SFolioReal = txtFolioReal.Text;
                oBit.SVueloContratoId = txtVueloContratoId.Text;
                oBit.SPilotoId = txtPilotoId.Text;
                oBit.SCopilotoId = txtCopilotoId.Text;
                oBit.DtFecha = txtFecha.Value.S().Dt();
                oBit.SOrigen = txtOrigen.Text;
                oBit.SDestino = txtDestino.Text;
                oBit.DtOrigenVuelo = txtOrigenVuelo.Value.S().Dt();
                oBit.DtDestinoVuelo = txtDestinoVuelo.Value.S().Dt();
                oBit.DtOrigenCalzo = txtOrigenCalzo.Value.S().Dt();
                oBit.DtDestinoCalzo = txtDestinoCalzo.Value.S().Dt();
                oBit.SConsumoOrigen = txtConsumoOrigen.Text;
                oBit.SConsumoDestino = txtConsumoDestino.Text;
                oBit.SCantPax = txtCantPax.Text;
                oBit.STipo = ddlTipo.Value.S();
                oBit.SLongNum = txtLongNum.Text;
                oBit.LLegId = txtLegId.Text.L();
                oBit.SUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                return oBit;
            }
            set { }
        }
        public DataTable dtBitacoras
        {
            get { return (DataTable)ViewState["VSBitacoras"]; }
            set { ViewState["VSBitacoras"] = value; }
        }

        #endregion

        protected void txtCantPax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCantPax.Text.I() == 0)
                    ddlTipo.Value = "CF";
                else if(txtCantPax.Text.I() > 0)
                    ddlTipo.Value = "CL";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}