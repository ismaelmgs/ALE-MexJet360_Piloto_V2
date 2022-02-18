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

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmConsultaContratos : System.Web.UI.Page, IViewGastoInterno
    {
        #region Eventos
        
        protected void Page_Load(object sender, EventArgs e)
        {          
            oPresenter = new GastoInterno_Presenter(this,new DBGastoInterno());
            gvConsultaContratos.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvConsultaContratos.SettingsPager.ShowDisabledButtons = true;
            gvConsultaContratos.SettingsPager.ShowNumericButtons = true;
            gvConsultaContratos.SettingsPager.ShowSeparators = true;
            gvConsultaContratos.SettingsPager.Summary.Visible = true;
            gvConsultaContratos.SettingsPager.PageSizeItemSettings.Visible = true;
            gvConsultaContratos.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvConsultaContratos.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

            ObtieneValores();
            if (!IsPostBack)
            {
                ObtieneDatosCliente();
            }                       
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                gvConsultaContratos.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvConsultaContratos.AddNewRow();

                //int iValor = 0;

                //int iPrueba = 100 / iValor;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
            }
        }

        protected void gvGastosInternos_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            e.Editor.ReadOnly = false;

            if (e.Column.FieldName == "IdConcepto")
            {
                if (eGetConcepto != null)
                    eGetConcepto(this, e);
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = (DataTable)ViewState["GastoInternoConcepto"];
                cmb.ValueField = "IdConcepto";
                cmb.ValueType = typeof(Int32);
                cmb.TextField = "Descripcion";
                cmb.DataBindItems();
            }

            if (e.Column.FieldName == "IdTipoMoneda")
            {
                if (eGetTipoMoneda != null)
                    eGetTipoMoneda(this, e);
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = (DataTable)ViewState["GastoInternoTipoMoneda"];
                cmb.ValueField = "IdTipoMoneda";
                cmb.ValueType = typeof(Int32);
                cmb.TextField = "Descripcion";
                cmb.DataBindItems();
            }


        }

        protected void gvGastosInternos_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {         
            eCrud = Enumeraciones.TipoOperacion.Insertar;
            oCrud = e;
                                                                 
            if (eNewObj != null)
                eNewObj(sender, e);
            CancelEditing(e);
        }

        protected void gvGastosInternos_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            gvConsultaContratos.SettingsText.PopupEditFormCaption = "Formulario de Edición";
        }

        protected void gvGastosInternos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            eCrud = Enumeraciones.TipoOperacion.Actualizar;
            oCrud = e;

            if (eSaveObj != null)
                eSaveObj(sender, e);

            CancelEditing(e);            
        }

        protected void gvGastosInternos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            eCrud = Enumeraciones.TipoOperacion.Eliminar;
            oCrud = e;
            
            if (eDeleteObj != null)
                eDeleteObj(sender, e);
            
            CancelEditing(e);
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlContrato.Items.Clear();
            if (eGetContrato != null)
                eGetContrato(null, EventArgs.Empty);
            ObtieneValores();           
        }

        protected void ddlContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetDetalleGastoInterno != null)
                eGetDetalleGastoInterno(null, EventArgs.Empty);
        }

        protected void Unnamed_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
        }

        #endregion Eventos

        #region Metodos
        public void ObtieneValores()
        {    
            if (eGetDetalleGastoInterno != null)
                eGetDetalleGastoInterno(null, EventArgs.Empty);                         
        }

        public void ObtieneDatosCliente()
        {
            if (eGetCliente != null)
                eGetCliente(null, EventArgs.Empty);
        }

        public void LoadObjectsConcepto(DataTable dtObject)
        {                                              
            ViewState["GastoInternoConcepto"] = dtObject;            
        }

        public void LoadClientes(DataTable dtObject)
        {            
            ddlCliente.Items.Clear();
            foreach (DataRow row in dtObject.Rows)
            {
                ddlCliente.Items.Add(row[1].S());
            }                                   
        }

        public void LoadContrato(DataTable dtObjCat)
        {                        
            ddlContrato.Items.Clear();            
            foreach (DataRow row in dtObjCat.Rows)
            {
                ddlContrato.Items.Add(row[2].S());

            }
            ViewState["Contrato"] = dtObjCat;    
        }

        public void LoadDetalleGastoInterno(DataTable dtObjCat)
        {
            gvConsultaContratos.DataSource = null;
            gvConsultaContratos.DataSource = dtObjCat;
            gvConsultaContratos.DataBind();
            ViewState["oDatos"] = dtObjCat;
        }

        public void LoadTipoMoneda(DataTable dtObjCat)
        {
            ViewState["GastoInternoTipoMoneda"] = dtObjCat;
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            popup.HeaderText = sCaption;
            gvConsultaContratos.JSProperties["cpText"] = sMensaje;
            gvConsultaContratos.JSProperties["cpShowPopup"] = true;            
        }
        public void MostrarMensajeError(string sMensaje, string sCaption)
        {
            mpeMensaje.ShowMessage(sMensaje, sCaption);
        }
        protected void CancelEditing(CancelEventArgs e)
        {
            e.Cancel = true;
            gvConsultaContratos.CancelEdit();
        }

        
        #endregion Metodos

        #region Propiedades

        private const string sClase = "frmGastoInterno.aspx.cs";
        private const string sPagina = "frmGastoInterno.aspx";
     
        GastoInterno_Presenter oPresenter;

        public object[] oArrFiltroConepto
        {
            get { return new object[] { "@idConcepto", 0 }; }
        }

        public object[] oArrFiltroCliente
        {
            get { return new object[] { "@idCliente", 0 }; }
        }
               
        public object[] oArrFiltroContrato
        {
            get { string sNombreCliente = ddlCliente.SelectedItem.S(); return new object[] { "@NombreCliente", sNombreCliente }; }
        }
       
        public object[] oArrFiltroDetalleGastoInterno
        {
            get { string sClaveContrato = ddlContrato.SelectedItem.S(); return new object[] { "@ClaveContrato", sClaveContrato }; }
        }
       
        public string oContrato
        {
            get { return ddlContrato.SelectedItem.S(); }
        }

        public string oNombreCliente
        {
            get { return ddlCliente.SelectedItem.S(); }
        }

        public object[] oArrFiltroTipoMoneda
        {
            get { return new object[] { "@IdTipoMoneda", 1 }; }
        }

        public Enumeraciones.TipoOperacion eCrud
        {
            get { return (ALE_MexJet.Objetos.Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }

        public object oCrud
        {
            get { return ViewState["CrudPiloto"]; }
            set { ViewState["CrudPiloto"] = value; }
        }

        public event EventHandler eGetConcepto;
        public event EventHandler eGetCliente;
        public event EventHandler eGetContrato;
        public event EventHandler eGetDetalleGastoInterno;
        public event EventHandler eGetTipoMoneda;          
        public event EventHandler eNewObj;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSearchObj;           
        #endregion Propiedades                                                   
    }
}