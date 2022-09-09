using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using DevExpress.Web;
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
        }

        protected void btnNuevaBitacora_Click(object sender, EventArgs e)
        {
            ppBitacora.ShowOnPageLoad = true;
        }

        protected void gvBitacoras_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Actualiza")
                {
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
                        txtFecha.Text = oB[6].S();
                        txtOrigen.Text = oB[7].S();
                        txtDestino.Text = oB[8].S();
                        txtOrigenVuelo.Text = oB[9].S();
                        txtOrigenCalzo.Text = oB[10].S();
                        txtConsumoOrigen.Text = oB[11].S();
                        txtCantPax.Text = oB[12].S();
                        txtTipo.Text = oB[13].S();
                        txtDestinoVuelo.Text = oB[14].S();
                        txtDestinoCalzo.Text = oB[15].S();
                        txtConsumoDestino.Text = oB[16].S();
                        txtTrip.Text = oB[17].S();
                        txtLegNum.Text = oB[18].S();
                        txtLongNum.Text = oB[19].S();
                        txtLegId.Text = oB[20].S();
                        txtFolioReal.Text = oB[21].S();
                        ppBitacora.ShowOnPageLoad = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadBitacoras(DataTable dt)
        {
            try
            {
                gvBitacoras.DataSource = dt;
                gvBitacoras.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Bitacoras_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

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
                oBit.DtFecha = txtFecha.Text.Dt();
                oBit.SOrigen = txtOrigen.Text;
                oBit.SDestino = txtDestino.Text;
                oBit.SOrigenVuelo = txtOrigenVuelo.Text;
                oBit.DtDestinoVuelo = txtDestinoVuelo.Text.Dt();
                oBit.DtOrigenCalzo = txtOrigenCalzo.Text.Dt();
                oBit.SDestinoCalzo = txtDestinoCalzo.Text;
                oBit.SConsumoOrigen = txtConsumoOrigen.Text;
                oBit.SConsumoDestino = txtConsumoDestino.Text;
                oBit.SCantPax = txtCantPax.Text;
                oBit.STipo = txtTipo.Text;
                oBit.SLongNum = txtLongNum.Text;
                oBit.LLegId = txtLegId.Text.L();
                oBit.SUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                return oBit;
            }
            set { }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eNewObj != null)
                    eNewObj(sender, e);

                ppBitacora.ShowOnPageLoad = false;
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
    }
}