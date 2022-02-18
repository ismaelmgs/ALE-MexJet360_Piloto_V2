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
using System.Reflection;



namespace ALE_MexJet.Views.Catalogos
{
    public partial class frmAeronave : System.Web.UI.Page, IViewAeronave
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.Aeronaves);
                LoadActions(DrPermisos);
                gvAeronave.Columns["Status"].Visible = true;
                oPresenter = new Aeronave_Presenter(this, new DBAeronave());
                gvAeronave.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvAeronave.SettingsPager.ShowDisabledButtons = true;
                gvAeronave.SettingsPager.ShowNumericButtons = true;
                gvAeronave.SettingsPager.ShowSeparators = true;
                gvAeronave.SettingsPager.Summary.Visible = true;
                gvAeronave.SettingsPager.PageSizeItemSettings.Visible = true;
                gvAeronave.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvAeronave.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                ObtieneValores();
                if (!IsPostBack)
                {
                    if (gvAeronave.VisibleRowCount < 1)
                    {

                        gvAeronave.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                        gvAeronave.Columns["Status"].Visible = false;
                        gvAeronave.AddNewRow();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }

        }

        protected void gvAeronave_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
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



                if (e.Column.FieldName == "IdMarca")
                {
                    if (eGetMarca != null)
                        eGetMarca(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["Marca"];
                    cmb.ValueField = "idMarca";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "DescripcionMarca";
                    cmb.DataBindItems();
                    cmb.Callback += new CallbackEventHandlerBase(cmbMarca_OnCallback);
                }

                if (e.Column.FieldName == "IdModelo")
                {
                    if (HiddenField1.Value != "")
                    {
                        if (eGetModelo != null)
                            eGetModelo(this, e);
                        ASPxComboBox cmb = e.Editor as ASPxComboBox;
                        cmb.DataSource = (DataTable)ViewState["Modelo"];
                        cmb.ValueField = "idModelo";
                        cmb.ValueType = typeof(Int32);
                        cmb.TextField = "DescripcionModelo";
                        cmb.DataBindItems();
                        cmb.Callback += new CallbackEventHandlerBase(cmbMarca_OnCallback);
                    }
                }


                if (e.Column.FieldName == "IdFlota")
                {
                    if (eGetFlota != null)
                        eGetFlota(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["Flota"];
                    cmb.ValueField = "idFlota";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "DescripcionFlota";
                    cmb.DataBindItems();
                    cmb.Callback += new CallbackEventHandlerBase(cmbBase_OnCallback);
                }

                if (e.Column.FieldName == "IdAeropuertoBase")
                {
                    if (eGetBase != null)
                        eGetBase(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["Base"];
                    cmb.ValueField = "idAeropuert";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "AeropuertoIATA";
                    cmb.DataBindItems();
                }

                if (e.Column.FieldName == "MatriculaInfo")
                {
                    if (HiddenField2.Value != "")
                    {
                        if (eGetCodigoUnidadDos != null)
                            eGetCodigoUnidadDos(this, e);

                        DataTable dt1 = (DataTable)ViewState["CodigoUnidadDosFlota"];
                        if (dt1 != null)
                        {
                            foreach (DataRow r in dt1.Rows)
                            {
                                if (r[2].S() != "")
                                {
                                    ASPxComboBox combo = e.Editor as ASPxComboBox;
                                    DataTable dt = (DataTable)ViewState["CodigoUnidadDosFlota"];

                                    if (dt != null)
                                    {
                                        foreach (DataRow row in dt.Rows)
                                        {
                                            combo.Items.Add(row["FlotaCU2"].S() + " " + row["DescripcionFlotaCU2"].S());
                                        }

                                    }

                                }
                                else
                                {
                                    if (eGetMatriculaInfor != null)
                                        eGetMatriculaInfor(this, e);
                                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                                    DataTable dt = (DataTable)ViewState["MatriculaInfor"];
                                    if (dt != null)
                                    {
                                        foreach (DataRow row in dt.Rows)
                                        {
                                            cmb.Items.Add(row["unit2"].S() + " " + row["description"].S());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (e.Column.FieldName == "IdBaseInfo")
                {
                    if (eGetBaseInfor != null)
                        eGetBaseInfor(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["BaseInfor"];
                    cmb.ValueField = "Unit3";
                    cmb.ValueType = typeof(string);
                    cmb.TextField = "Description";
                    cmb.DataBindItems();
                }

                if (e.Column.FieldName == "IdUnidadNegocioInfo")
                {
                    if (eGetUnidadNegocioInfor != null)
                        eGetUnidadNegocioInfor(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["UnidadNegocioInfor"];
                    cmb.ValueField = "Unit4";
                    cmb.ValueType = typeof(string);
                    cmb.TextField = "Description";
                    cmb.DataBindItems();
                }

                if (!gvAeronave.IsEditing || e.Column.FieldName != "IdModelo") return;
                if (e.KeyValue == DBNull.Value || e.KeyValue == null) return;
                object val = gvAeronave.GetRowValuesByKeyValue(e.KeyValue, "IdMarca");
                if (val != DBNull.Value)
                {
                    HiddenField1.Value = val.S();
                    if (HiddenField1.Value != "")
                    {
                        if (eGetModelo != null)
                            eGetModelo(this, e);
                        ASPxComboBox cmb = e.Editor as ASPxComboBox;
                        cmb.DataSource = (DataTable)ViewState["Modelo"];
                        cmb.ValueField = "idModelo";
                        cmb.ValueType = typeof(Int32);
                        cmb.TextField = "DescripcionModelo";
                        cmb.DataBindItems();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeronave_CellEditorInitialize", "Aviso");
            }
        }
       
        protected void gvAeronave_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;
                CancelEditing(e);

                if (eDeleteObj != null)
                    eDeleteObj(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeronave_RowDeleting", "Aviso");
            }
        }

        protected void gvAeronave_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;

                if (eNewObj != null)
                    eNewObj(sender, e);

                CancelEditing(e);
                gvAeronave.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeronave_RowInserting", "Aviso");
            }

        }

        protected void gvAeronave_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeronave_RowUpdating", "Aviso");
            }


        }

        protected void gvAeronave_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvAeronave.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeronave_StartRowEditing", "Aviso");
            }
        }

        protected void gvAeronave_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;

                if (eObjSelected != null)
                    eObjSelected(sender, e);

                if (bDuplicado)
                {
                    AddError(e.Errors, gvAeronave.Columns["Serie"], "La serie ya existe, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeronave_RowValidating", "Aviso");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ObtieneValores();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscar_Click", "Aviso");
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                gvAeronave.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvAeronave.Columns["Status"].Visible = false;
                gvAeronave.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
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

        protected void gvAeronave_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvAeronave.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeronave_CancelRowEditing", "Aviso");
            }
        }

        protected void gvAeronave_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            try
            {
                int iPos = 0;
                //DrPermisos = (DataRow[])Session["DrPermisos"];
                for (iPos = 0; iPos < DrPermisos[0].ItemArray.Length; iPos++)
                {
                    switch (iPos)
                    {
                        case 5: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                if (e.ButtonType == ColumnCommandButtonType.New)
                                    e.Enabled = true;
                            }
                            else
                            {
                                if (e.ButtonType == ColumnCommandButtonType.New)
                                    e.Enabled = false;
                            } break;
                        case 6: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                if (e.ButtonType == ColumnCommandButtonType.Edit)
                                    e.Enabled = true;
                            }
                            else
                            {
                                if (e.ButtonType == ColumnCommandButtonType.Edit)
                                    e.Enabled = false;
                            } break;
                        case 7: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                if (e.ButtonType == ColumnCommandButtonType.Delete)
                                    e.Enabled = true;
                            }
                            else
                            {
                                if (e.ButtonType == ColumnCommandButtonType.Delete)
                                    e.Enabled = false;
                            } break;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeronave_CommandButtonInitialize", "Aviso");
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

        #endregion

        #region "METODOS"

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
        public void LoadObjects(DataTable dtObject)
        {
            try
            {
                gvAeronave.DataSource = null;
                ViewState["oDatos"] = null;

                gvAeronave.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvAeronave.DataBind();
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
                gvAeronave.JSProperties["cpText"] = sMensaje;
                gvAeronave.JSProperties["cpShowPopup"] = true;
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
                gvAeronave.CancelEdit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadCatalogoMarca(DataTable dtObjCat)
        {
            try
            {
                ViewState["Marca"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadCatalogoFlota(DataTable dtObjCat)
        {
            try
            {
                ViewState["Flota"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadCatalogoModelo(DataTable dtObjCat)
        {
            try
            {
                ViewState["Modelo"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadCatalogoBase(DataTable dtObjCat)
        {
            try
            {
                ViewState["Base"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadCatalogoMatriculaInfor(DataTable dtObjCat)
        {
            try
            {
                ViewState["MatriculaInfor"] = dtObjCat;
                ViewState["MatriculaInfor"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadCatalogoBaseInfor(DataTable dtObjCat)
        {
            try
            {
                ViewState["BaseInfor"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadCatalogoUnidadNegocioInfor(DataTable dtObjCat)
        {
            try
            {
                ViewState["UnidadNegocioInfor"] = dtObjCat;
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
        public void LoadActions(DataRow[] DrActions)
        {
            try
            {
                int iPos = 0;
                if (DrActions.Length == 0)
                {
                    btnBusqueda.Enabled = false;
                    txtTextoBusqueda.Enabled = false;
                    ddlTipoBusqueda.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnExcel.Enabled = false;
                    btnNuevo2.Enabled = false;
                    ASPxButton2.Enabled = false;
                }
                else
                {
                    for (iPos = 0; iPos < DrActions[0].ItemArray.Length; iPos++)
                    {
                        switch (iPos)
                        {
                            case 4: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                                {
                                    btnBusqueda.Enabled = true;
                                    txtTextoBusqueda.Enabled = true;
                                    ddlTipoBusqueda.Enabled = true;
                                    btnExcel.Enabled = true;
                                    ASPxButton2.Enabled = true;
                                }
                                else
                                {
                                    btnBusqueda.Enabled = false;
                                    txtTextoBusqueda.Enabled = false;
                                    ddlTipoBusqueda.Enabled = false;
                                    btnExcel.Enabled = false;
                                    ASPxButton2.Enabled = false;
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
        protected void FillComboMarca(ASPxComboBox cmb, string country)
        {
            try
            {
                if (string.IsNullOrEmpty(country)) return;
                HiddenField1.Value = country;
                if (eGetModelo != null)
                    eGetModelo(null,EventArgs.Empty);
                cmb.DataSource = (DataTable)ViewState["Modelo"];
                cmb.ValueField = "idModelo";
                cmb.ValueType = typeof(Int32);
                cmb.TextField = "DescripcionModelo";
                cmb.DataBindItems();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void FillCombo(ASPxComboBox cmb, string country)
        {
            try
            {
                if (string.IsNullOrEmpty(country)) return;
                if (eGetBaseInfor != null)
                    eGetBaseInfor(this, EventArgs.Empty);
                cmb.Items.Clear();
                cmb.DataSource = (DataTable)ViewState["BaseInfor"];
                cmb.ValueField = "Unit3";
                cmb.ValueType = typeof(string);
                cmb.TextField = "Description";
                cmb.DataBindItems();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void LoadCodigoUnidadDos(DataTable dtCodigoUnitDos)
        {
            ViewState["CodigoUnidadDosFlota"] = dtCodigoUnitDos;
        }
        void cmbBase_OnCallback(object source, CallbackEventArgsBase e)
        {
            try
            {
                FillCombo(source as ASPxComboBox, e.Parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void cmbMarca_OnCallback(object source, CallbackEventArgsBase e)
        {
            try
            {
                FillComboMarca(source as ASPxComboBox, e.Parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region "Vars y Propiedades"
        
        Aeronave_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eGetTipCliente;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetMarca;
        public event EventHandler eGetFlota;
        public event EventHandler eGetCodigoUnidadDos;
        public event EventHandler eGetModelo;
        public event EventHandler eGetBase;
        public event EventHandler eGetMatriculaInfor;
        public event EventHandler eGetBaseInfor;
        public event EventHandler eGetUnidadNegocioInfor;

        UserIdentity oUsuario = new UserIdentity();

        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }

        public object oCrud
        {
            get { return ViewState["CrudPiloto"]; }
            set { ViewState["CrudPiloto"] = value; }
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
                string sSerie= string.Empty;
                string sMatricula = string.Empty;

                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "1":
                        sSerie = txtTextoBusqueda.Text.S();
                        iEstatus = -1;
                        sMatricula = string.Empty;
                        break;
                    case "2":
                        sMatricula = txtTextoBusqueda.Text.S();
                        iEstatus = -1;
                        sSerie = string.Empty;
                        break;
                    case "3":
                        sSerie = string.Empty;
                        iEstatus = 1;
                        sMatricula = string.Empty;
                        break;
                    case "4":
                        sSerie =  string.Empty;
                        iEstatus = 0;
                        sMatricula = string.Empty;
                        break;

                }

                return new object[]{
                                        "@Serie", "%" + sSerie + "%",
                                        "@Matricula", "%" + sMatricula + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }

        public object[] oArrFiltrosCodigoUnidadDos
        {
            get
            {
                return new object[]{
                                        "@IdFlota", HiddenField2.Value ,
                                        "@Descripcion", "%%",
                                        "@estatus",1
                                    };
            }
        }

        public object[] oArrFiltrosModelo
        {
            get
            {
                return new object[]{
                                        "@DescripcionModelo", "%%",
                                        "@estatus", "1",
                                        "@idMarca", HiddenField1.Value
                                    };
            }
        }

        public int iIdModelo
        {
            get { return (int)ViewState["iIdModelo"]; }
            set {
                ViewState["iIdModelo"] = value;
            }
        }

        private const string sPagina = "frmAeronave.aspx";
        private const string sClase = "frmAeronave.aspx.cs";

        #endregion
    }
}