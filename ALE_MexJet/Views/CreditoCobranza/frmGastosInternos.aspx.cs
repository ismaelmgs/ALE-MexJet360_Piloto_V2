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
    public partial class frmGastosInternos : System.Web.UI.Page, IViewGastoInterno
    {
        #region Eventos
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.GastosInternos);
                LoadActions(DrPermisos);
                oPresenter = new GastoInterno_Presenter(this, new DBGastoInterno());
                gvGastosInternos.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvGastosInternos.SettingsPager.ShowDisabledButtons = true;
                gvGastosInternos.SettingsPager.ShowNumericButtons = true;
                gvGastosInternos.SettingsPager.ShowSeparators = true;
                gvGastosInternos.SettingsPager.Summary.Visible = true;
                gvGastosInternos.SettingsPager.PageSizeItemSettings.Visible = true;
                gvGastosInternos.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvGastosInternos.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                ObtieneValores();
                if (!IsPostBack)
                {
                    ObtieneDatosCliente();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                gvGastosInternos.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvGastosInternos.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
            }
        }

        protected void gvGastosInternos_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;               
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

                else if (e.Column.FieldName == "IdMatricula")
                {
                    if (eGetMatricula != null)
                        eGetMatricula(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["Matricula"];
                    cmb.ValueField = "IdAeroave";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "Matricula";
                    cmb.DataBindItems();

                    if (cmb.SelectedIndex == -1)
                    {
                        cmb.Value = 0;
                        cmb.Text = "Ninguno";
                    }
                }

                else if (e.Column.FieldName == "IdTipoMovimiento")
                {
                    if (eGetTipoFactura != null)
                        eGetTipoFactura(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["TipoFactura"];
                    cmb.ValueField = "idTipoFactura";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "Descripcion";
                    cmb.DataBindItems();                    
                }
                else if (e.Column.FieldName == "DescripcionPaquete")
                {
                    if (eGetPaqueteGrupoModelo != null)
                        eGetPaqueteGrupoModelo(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;              
                    if (cmb.SelectedIndex == -1)
                    {
                        DataTable dtCombo = (DataTable)ViewState["PaqueteGrupoModelo"];
                        if (dtCombo != null)
                        {
                            foreach (DataRow row in dtCombo.Rows)
                            {
                                cmb.Value = row[0].ToString();      // LE ASIGNAMOS EL ID 
                                cmb.Text = row[1].ToString();       // LE ASIGNAMOS LA DESCRIPCION
                            }
                        }
                    }
                    cmb.ReadOnly = true;
                }
                else if (e.Column.FieldName == "DescripcionGrupoModelo")
                {
                    if (eGetPaqueteGrupoModelo != null)
                        eGetPaqueteGrupoModelo(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    
                    if (cmb.SelectedIndex == -1)
                    {
                        DataTable dtCombo = (DataTable)ViewState["PaqueteGrupoModelo"];
                        if (dtCombo != null)
                        {
                            foreach (DataRow row in dtCombo.Rows)
                            {
                                cmb.Value = row[2].ToString();      // LE ASIGNAMOS EL ID 
                                cmb.Text = row[3].ToString();       // LE ASIGNAMOS LA DESCRIPCION
                            }
                        }
                    }
                    cmb.ReadOnly = true;
                }
                else if (e.Column.FieldName == "Total")
                {                    
                    ASPxTextEdit txt = e.Editor as ASPxTextEdit;
                    txt.ReadOnly = true;
                }
                else if (e.Column.FieldName == "FechaGasto")
                {
                    ASPxDateEdit dte = e.Editor as ASPxDateEdit;
                    dte.MinDate = DateTime.Now.Subtract(TimeSpan.FromDays(diaInicial()));
                    dte.MaxDate = DateTime.Now.AddDays(diaFinal());
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvGastosInternos_CellEditorInitialize", "Aviso");
            }
        }       

        protected void gvGastosInternos_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Validar;
                oCrud = e;

                if (eObjSelected != null)
                    eObjSelected(sender, e);

                if (GastoInterno_Presenter.iValorInsertado == -1)
                    AddError(e.Errors, gvGastosInternos.Columns["GastoInternoImporte"], "Excede el Top maximo.");
                else if (GastoInterno_Presenter.iValorInsertado == -2)
                    AddError(e.Errors, gvGastosInternos.Columns["GastoInternoImporte"], "Clave contrato no pertenece a cliente.");
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvGastosInternos_RowValidating", "Aviso");
            }
        }

        protected void gvGastosInternos_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {                
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;

                if (eNewObj != null)
                    eNewObj(sender, e);
                                
                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvGastosInternos_RowInserting", "Aviso");
            }
        }

        protected void gvGastosInternos_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvGastosInternos.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvGastosInternos_StartRowEditing", "Aviso");
            }
        }        

        protected void gvGastosInternos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;

                if (eSaveObj != null)
                    eSaveObj(sender, e);

                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvGastosInternos_RowUpdating", "Aviso");
            }
        }

        protected void gvGastosInternos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;

                if (eDeleteObj != null)
                    eDeleteObj(sender, e);

                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvGastosInternos_RowDeleting", "Aviso");
            }
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlContrato.Items.Clear();
                ddlContrato.Text = "";

                gvGastosInternos.DataSource = null;
                gvGastosInternos.DataBind();                

                if (ddlContrato.Text == "")
                {
                    btnNuevo.Visible = false;
                    btnNuevo2.Visible = false;
                }

                if (eGetContrato != null)
                    eGetContrato(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlCliente_SelectedIndexChanged", "Aviso");
            }
        }

        protected void ddlContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eGetDetalleGastoInterno != null)
                    eGetDetalleGastoInterno(null, EventArgs.Empty);

                btnNuevo.Visible = true;
                btnNuevo2.Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlContrato_SelectedIndexChanged", "Aviso");
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

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {                
                Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExcel_Click", "Aviso");
            }
        }

        #endregion Eventos

        #region Metodos

        private int diaInicial()
        {
            try
            {
                int mesActual;
                int AñoActual;
                int DiasMesActual;
                int DiasMesAnterior;
                int DiaActual;
                int TotalDiasAnteriores;

                mesActual = DateTime.Now.Month;
                AñoActual = DateTime.Now.Year;

                if (mesActual > 1)
                    mesActual = mesActual - 1;
                else
                    AñoActual = AñoActual - 1;

                DiasMesActual = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                DiasMesAnterior = DateTime.DaysInMonth(AñoActual, mesActual);
                DiaActual = DateTime.Now.Day;

                if (DiasMesActual <= 10)
                    TotalDiasAnteriores = DiaActual + DiasMesAnterior; // feha minima a mostrar            
                else
                    TotalDiasAnteriores = DiaActual; // feha minima a mostrar

                return TotalDiasAnteriores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private int diaFinal()
        {
            try
            {
                int DiasMesActual;
                int DiaActual;
                int DiasMaximo;

                DiasMesActual = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                DiaActual = DateTime.Now.Day;

                DiasMaximo = DiasMesActual - DiaActual; //Dias a mostrar del mes actual
                return DiasMaximo;
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

        public void ObtieneValores()
        {
            try
            {
                if (eGetDetalleGastoInterno != null)
                    eGetDetalleGastoInterno(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadTipoFactura(DataTable dtTipoFactura)
        {
            try
            {
                ViewState["TipoFactura"] = dtTipoFactura;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneTipoFactura()
        {
            try 
            {
                if (eGetTipoFactura != null)
                    eGetTipoFactura(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadPaqueteGrupoModelo(DataTable dtPaqueteGrupoModelo)
        {
            try
            {
                ViewState["PaqueteGrupoModelo"] = dtPaqueteGrupoModelo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtienePaqueteGrupoModelo()
        {
            try
            {
                if (eGetPaqueteGrupoModelo != null)
                    eGetPaqueteGrupoModelo(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneDatosCliente()
        {
            try
            {
                ddlCliente.Text = "";
                if (eGetCliente != null)
                    eGetCliente(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadObjectsConcepto(DataTable dtObject)
        {
            try
            {
                ViewState["GastoInternoConcepto"] = dtObject;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadClientes(DataTable dtObject)
        {
            try
            {
                ddlCliente.Items.Clear();
                foreach (DataRow row in dtObject.Rows)
                {
                    ddlCliente.Items.Add(row[2].S());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }

        public void LoadContrato(DataTable dtObjCat)
        {
            try
            {
                ddlContrato.Items.Clear();
                foreach (DataRow row in dtObjCat.Rows)
                {
                    ddlContrato.Items.Add(row[2].S());
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadDetalleGastoInterno(DataTable dtObjCat)
        {
            try
            {
                gvGastosInternos.DataSource = null;              
                gvGastosInternos.DataSource = dtObjCat;
                gvGastosInternos.DataBind();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadTipoMoneda(DataTable dtObjCat)
        {
            try
            {
                ViewState["GastoInternoTipoMoneda"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadMatricula(DataTable dtObjMatricula)
        {
            try
            {
                ViewState["Matricula"] = dtObjMatricula;
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
                gvGastosInternos.JSProperties["cpText"] = sMensaje;
                gvGastosInternos.JSProperties["cpShowPopup"] = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MostrarMensajeError(string sMensaje, string sCaption)
        {
            try
            {
                mpeMensaje.ShowMessage(sMensaje, sCaption);
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
                gvGastosInternos.CancelEdit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadActions(DataRow[] DrActions)
        {
            try
            {
                int iPos = 0;
                if (DrActions.Length == 0)
                {
                    btnExcel.Enabled = false;
                    btnExportar.Enabled = false;
                    btnExcel.Enabled = false;
                    ddlCliente.Enabled = false;
                    ddlContrato.Enabled = false;
                }
                else
                {
                    for (iPos = 0; iPos < DrActions[0].ItemArray.Length; iPos++)
                    {
                        switch (iPos)
                        {
                            case 4: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                                {
                                    btnExcel.Enabled = true;
                                    btnExportar.Enabled = true;
                                    btnExcel.Enabled = true;
                                    ddlCliente.Enabled = true;
                                    ddlContrato.Enabled = true;

                                }
                                else
                                {
                                    btnExcel.Enabled = false;
                                    btnExportar.Enabled = false;
                                    btnExcel.Enabled = false;
                                    ddlCliente.Enabled = false;
                                    ddlContrato.Enabled = false;
                                } break;
                            case 5: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                                {
                                    btnNuevo.Enabled = true;
                                    btnNuevo2.Enabled = true;
                                }
                                else
                                {
                                    btnNuevo.Enabled = false;
                                    btnNuevo2.Enabled = false;
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
        
        #endregion Metodos

        #region Propiedades
        
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }
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
            get 
            {
                string sCodigoCliente = "";
                sCodigoCliente = ddlCliente.SelectedItem.S(); 
                return new object[] {  "@NombreCliente", "",
                                       "@CodigoCliente ", sCodigoCliente};
            }
        }
       
        public object[] oArrFiltroDetalleGastoInterno
        {
            get 
            {
                string sClaveContrato = "";
                sClaveContrato = ddlContrato.SelectedItem.S(); 
                return new object[] { "@ClaveContrato", sClaveContrato }; 
            }
        }

        public object[] oArrFiltroPaqueteGrupoModelo
        {
            get
            {
                string sCodigoCliente = "";
                string sClaveContrato = "";

                sCodigoCliente = ddlCliente.SelectedItem.S();                
                sClaveContrato = ddlContrato.SelectedItem.S();

                return new object[] { "@ClaveContrato", sClaveContrato, "@CodigoCliente", sCodigoCliente }; 
            }
        }

        public object[] oArrFiltroMatricula
        {
            get 
            { 
                return new object[] {   "@Serie", "%%", 
                                        "@Matricula","%%", 
                                        "@estatus",-1 }; 
            }
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

        GastoInterno_Presenter oPresenter;
        UserIdentity oUsuario = new UserIdentity();
        private const string sClase = "frmGastoInterno.aspx.cs";
        private const string sPagina = "frmGastoInterno.aspx";        
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
        public event EventHandler eGetMatricula;
        public event EventHandler eGetTipoFactura;
        public event EventHandler eGetPaqueteGrupoModelo;
       
        #endregion Propiedades                                                                                                               
                                
    }
}