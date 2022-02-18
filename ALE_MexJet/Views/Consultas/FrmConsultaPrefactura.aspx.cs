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
using System.Text;

namespace ALE_MexJet.Views.Consultas
{
    public partial class FrmConsultaPrefactura : System.Web.UI.Page, iViewConsultaPrefactura
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gvPrefactura.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvPrefactura.SettingsPager.ShowDisabledButtons = true;
                gvPrefactura.SettingsPager.ShowNumericButtons = true;
                gvPrefactura.SettingsPager.ShowSeparators = true;
                gvPrefactura.SettingsPager.Summary.Visible = true;
                gvPrefactura.SettingsPager.PageSizeItemSettings.Visible = true;
                gvPrefactura.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvPrefactura.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                oPresenter = new ConsultaPrefacturas_Presenter(this, new DBConsultaPrefactura());
                LoadClientes();
                if (eGetPrefacturas != null)
                    eGetPrefacturas(sender, e);
                gvPrefactura.DataSource = dtPrefactura;
                gvPrefactura.DataBind();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }

        }

        protected void cboCveCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eSearchContratosCliente != null)
                    eSearchContratosCliente(sender, e);
                ClaveContrato.DataSource = dtContratosCliente;
                ClaveContrato.ValueField = "IdContrato";
                ClaveContrato.TextField = "ClaveContrato";
                ClaveContrato.DataBind();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvBase_CellEditorInitialize", "Aviso");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            try
            {
                if (eGetPrefacturas != null)
                    eGetPrefacturas(sender, e);

                gvPrefactura.DataSource = dtPrefactura;
                gvPrefactura.DataBind();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvBase_CellEditorInitialize", "Aviso");
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/CreditoCobranza/frmPrefactura.aspx");
        }

        protected void gvPrefactura_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                sFacturaScc = e.Values["IdFacturaSCC"].S();
                sFacturaVuelo = e.Values["IdFactura"].S();
                iIdPrefactura = e.Keys[0].S().I();
                if (eValidaFacturasCanceladas != null)
                    eValidaFacturasCanceladas(sender, e);
                if(!bFacturaSccCancelda || !bFacturaVueloCancelada)
                {
                    MostrarMensaje("Las facturas deben estar canceladas", "Aviso");
                }
                else
                {
                    if (eCancelaPrefactura != null)
                        eCancelaPrefactura(sender, e);
                    if (eGetPrefacturas != null)
                        eGetPrefacturas(sender, e);
                    gvPrefactura.DataSource = dtPrefactura;
                    gvPrefactura.DataBind();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
            finally
            {
                CancelEditing(e);
            }

        }

        protected void gvPrefactura_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            object value = (sender as DevExpress.Web.ASPxGridView).GetRowValues(e.VisibleIndex, "Status");
            //e.Visible = value.S().I() == -1;
            e.Enabled = value.S().I() > 0;
        }

        protected void gvPrefactura_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            object value = (sender as DevExpress.Web.ASPxGridView).GetRowValues(e.VisibleIndex, "Status");
            e.Enabled = value.S().I() > 0;

        }

        protected void gvPrefactura_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {


            object value = (sender as DevExpress.Web.ASPxGridView).GetRowValues(e.VisibleIndex, "IdPrefactura");
            object oFactSerC = (sender as DevExpress.Web.ASPxGridView).GetRowValues(e.VisibleIndex, "IdFacturaSCC");
            object oFactSerV = (sender as DevExpress.Web.ASPxGridView).GetRowValues(e.VisibleIndex, "IdFactura");


            int iTipoEdicion = 0; //    0.- Completa   1.- Servicios de Vuelo   2.- Servicios con cargo

            if (oFactSerC.S() == string.Empty && oFactSerV.S() == string.Empty)
                iTipoEdicion = 0;
            else if (oFactSerC.S() != string.Empty && oFactSerV.S() == string.Empty)
                iTipoEdicion = 1;
            else if (oFactSerC.S() == string.Empty && oFactSerV.S() != string.Empty)
                iTipoEdicion = 2;


            string sIdContrato;
            string sTipoEdicion = string.Empty;
            sIdContrato = Convert.ToBase64String(Encoding.UTF8.GetBytes(value.S()));
            sTipoEdicion = Convert.ToBase64String(Encoding.UTF8.GetBytes(iTipoEdicion.S()));

            string ruta = "~/Views/CreditoCobranza/frmPrefactura.aspx?Pref=" + sIdContrato + "&Edi=" + sTipoEdicion;
            ASPxWebControl.RedirectOnCallback(ruta);
        }


        #endregion

        #region "METODOS"
        protected void LoadClientes()
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(new object(), EventArgs.Empty);
                cboCveCliente.DataSource = dtClientes;
                cboCveCliente.ValueField = "IdCliente";
                cboCveCliente.TextField = "CodigoCliente";
                cboCveCliente.DataBind();
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
                gvPrefactura.CancelEdit();
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
                gvPrefactura.JSProperties["cpText"] = sMensaje;
                gvPrefactura.JSProperties["cpShowPopup"] = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "Vars y Propiedades"

        ConsultaPrefacturas_Presenter oPresenter;
        private const string sClase = "frmConsultaContratos.aspx.cs";
        private const string sPagina = "frmConsultaContratos.aspx";
        public object[] oArrFiltros
        {
            get
            {
                return new object[]{
                                        "@IdPrefactura", Folio.Text,
                                        "@IdCliente", cboCveCliente.Value,
                                        "@IdContrato", ClaveContrato.Value
                                    };
            }


        }

        public int iIdCliente
        {
            get { return cboCveCliente.Value.S().I(); }
        }

        public int iIdContrato
        {
            get
            {
                return ClaveContrato.Value.S().I();
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

        public DataTable dtContratosCliente
        {
            get
            {
                return (DataTable)ViewState["dtContratosCliente"];
            }
            set
            {
                ViewState["dtContratosCliente"] = value;
            }
        }

        public DataTable dtPrefactura
        {
            get
            {
                return (DataTable)ViewState["dtPrefactura"];
            }
            set
            {
                ViewState["dtPrefactura"] = value;
            }
        }

        public bool bFacturaSccCancelda
        {
            get
            {
                return (bool)ViewState["FacturaSccCancelda"];
            }
            set
            {
                ViewState["FacturaSccCancelda"] = value;
            }
        }

        public bool bFacturaVueloCancelada
        {
            get
            {
                return (bool)ViewState["bFacturaVueloCancelada"];
            }
            set
            {
                ViewState["bFacturaVueloCancelada"] = value;
            }
        }

        public string sFacturaVuelo
        {
            get
            {
                return (string)ViewState["sFacturaVuelo"];
            }
            set
            {
                ViewState["sFacturaVuelo"] = value;
            }
        }
        public string sFacturaScc
        {
            get
            {
                return (string)ViewState["sFacturaScc"];
            }
            set
            {
                ViewState["sFacturaScc"] = value;
            }
        }

        public int iIdPrefactura
        {
            get
            {
                return (int)ViewState["iIdPrefactura"];
            }
            set
            {
                ViewState["iIdPrefactura"] = value;
            }
        }
        public event EventHandler eCancelaPrefactura;
        public event EventHandler eValidaFacturasCanceladas;
        public event EventHandler eSearchContratosCliente;

        public event EventHandler eGetPrefacturas;

        public event EventHandler eNewObj;

        public event EventHandler eObjSelected;

        public event EventHandler eSaveObj;

        public event EventHandler eDeleteObj;

        public event EventHandler eSearchObj;

        #endregion
    }
}