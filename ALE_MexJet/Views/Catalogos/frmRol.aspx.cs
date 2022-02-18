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

namespace ALE_MexJet.Views.Catalogos
{
    public partial class frmRol : System.Web.UI.Page, IViewRol
    {
        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.Rol);
                LoadActions(DrPermisos);
                oPresenter = new Rol_Presenter(this, new DBRol());
                gvRol.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvRol.SettingsPager.ShowDisabledButtons = true;
                gvRol.SettingsPager.ShowNumericButtons = true;
                gvRol.SettingsPager.ShowSeparators = true;
                gvRol.SettingsPager.Summary.Visible = true;
                gvRol.SettingsPager.PageSizeItemSettings.Visible = true;
                gvRol.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvRol.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                ObtieneValores();
                if (!IsPostBack)
                {
                    if (gvRol.VisibleRowCount < 1)
                    {
                        gvRol.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                        gvRol.Columns["Status"].Visible = false;
                        gvRol.AddNewRow();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
           

        }

        protected void gvRol_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;

                if (e.Column.FieldName == "ModuloId")
                {
                    if (eSearchModDef != null)
                        eSearchModDef(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["oDatosMod"];
                    cmb.ValueField = "ModuloId";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "Descripcion";
                    cmb.DataBindItems();
                }
                if (e.Column.FieldName == "RolIdO")
                {
                    if (e.Column.Visible == false)
                    {
                        e.Column.EditFormSettings.Visible = DefaultBoolean.False;
                        e.Column.EditFormSettings.Caption = "";
                    }
                    else
                    {
                        if (eSearchCloneO != null)
                            eSearchCloneO(this, e);
                        ASPxComboBox cmb = e.Editor as ASPxComboBox;
                        cmb.DataSource = (DataTable)ViewState["oDatosO"];
                        cmb.ValueField = "RolIdO";
                        cmb.ValueType = typeof(Int32);
                        cmb.TextField = "RolDescripcion";
                        cmb.DataBindItems();
                    }
                }
                if (e.Column.FieldName == "RolIdD")
                {
                    if (e.Column.Visible == false)
                    {
                        e.Column.EditFormSettings.Visible = DefaultBoolean.False;
                        e.Column.EditFormSettings.Caption = "";
                    }
                    else
                    {
                        if (eSearchCloneD != null)
                            eSearchCloneD(this, e);
                        ASPxComboBox cmb = e.Editor as ASPxComboBox;
                        cmb.DataSource = (DataTable)ViewState["oDatosD"];
                        cmb.ValueField = "RolIdD";
                        cmb.ValueType = typeof(Int32);
                        cmb.TextField = "RolDescripcion";
                        cmb.DataBindItems();
                    }
                }
                ASPxGridView gridView = (ASPxGridView)sender;
                if (gvRol.SettingsText.PopupEditFormCaption == "Formulario de Clonación")
                {
                    eCrud = Enumeraciones.TipoOperacion.Clonar;
                    gvRol.Columns["Status"].Visible = false;
                }
                else
                {

                    if (gridView.IsNewRowEditing)
                    {
                        eCrud = Enumeraciones.TipoOperacion.Insertar;
                    }
                    else
                    {
                        eCrud = Enumeraciones.TipoOperacion.Actualizar;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRol_CellEditorInitialize", "Aviso");
            }

        }
        protected void gvRol_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvRol.Columns["Descripcion"].Visible = true;
                gvRol.Columns["ModuloId"].Visible = false;
                gvRol.Columns["RolIdO"].Visible = false;
                gvRol.Columns["RolIdD"].Visible = false;
                gvRol.Columns["RolDescripcion"].Visible = true;
                gvRol.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRol_CancelRowEditing", "Aviso");
            }
        }

        protected void gvRol_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRol_RowDeleting", "Aviso");
            }
        }

        protected void gvRol_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                if (gvRol.SettingsText.PopupEditFormCaption != "Formulario de Clonación")
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    oCrud = e;

                    if (eNewObj != null)
                        eNewObj(sender, e);
                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Clonar;
                    oCrud = e;

                    if (eCloneObj != null)
                        eCloneObj(sender, e);
                }

                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRol_RowInserting", "Aviso");
            }
        }

        protected void gvRol_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRol_RowUpdating", "Aviso");
            }


        }
       
        
        protected void gvRol_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvRol.SettingsText.PopupEditFormCaption = "Formulario de Edición";
                gvRol.Columns["ModuloId"].Visible = false;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRol_StartRowEditing", "Aviso");
            }
        }

        protected void gvRol_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;

                if (eObjSelected != null)
                    eObjSelected(sender, e);

                if (bDuplicado)
                {
                    AddError(e.Errors, gvRol.Columns["RolDescripcion"], "Este rol ya existe, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRol_RowValidating", "Aviso");
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
                gvRol.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvRol.Columns["ModuloId"].Visible = false;
                gvRol.Columns["Status"].Visible = false;
                gvRol.Columns["RolIdO"].Visible = false;
                gvRol.Columns["RolIdD"].Visible = false;
                gvRol.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
            }

        }
        protected void btnClonar_Click(object sender, EventArgs e)
        {
            try
            {
                gvRol.SettingsText.PopupEditFormCaption = "Formulario de Clonación";
                gvRol.Columns["RolDescripcion"].Visible = false;
                gvRol.Columns["ModuloId"].Visible = false;
                gvRol.Columns["RolIdO"].Visible = true;
                gvRol.Columns["RolIdD"].Visible = true;
                gvRol.Columns["Status"].Visible = false;
                gvRol.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnClonar_Click", "Aviso");
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
        protected void gvRol_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            try
            {
                int iPos = 0;
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRol_CommandButtonInitialize", "Aviso");
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
        public void ObtieneValoresOrigen()
        {
            try
            {
                if (eSearchCloneO != null)
                    eSearchCloneO(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ObtieneValoresDestino()
        {
            try
            {
                if (eSearchCloneD != null)
                    eSearchCloneD(null, EventArgs.Empty);
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
                gvRol.DataSource = null;
                ViewState["oDatos"] = null;

                gvRol.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvRol.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void LoadObjectsO(DataTable dtObjectO)
        {
            try
            {
                ViewState["oDatosO"] = dtObjectO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadObjectsD(DataTable dtObjectD)
        {
            try
            {
                ViewState["oDatosD"] = dtObjectD;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadModulo(DataTable dtModulo)
        {
            try
            {
                ViewState["oDatosMod"] = dtModulo;
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
                gvRol.JSProperties["cpCaption"] = sCaption;
                gvRol.JSProperties["cpText"] = sMensaje;
                gvRol.JSProperties["cpShowPopup"] = true;
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
                gvRol.CancelEdit();
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
        #endregion

        #region "Vars y Propiedades"
        Rol_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchCloneO;
        public event EventHandler eSearchCloneD;
        public event EventHandler eSearchModDef;
        public event EventHandler eCloneObj;
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
                string sModuloDefault = string.Empty;
                string sDescripcion = string.Empty;

                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "1":
                        iEstatus = -1;
                        sModuloDefault = string.Empty;
                        sDescripcion = txtTextoBusqueda.Text.S();
                        break;
                    case "2":
                        iEstatus = 1;
                        sModuloDefault = string.Empty;
                        sDescripcion = string.Empty;
                        break;
                    case "3":
                        iEstatus = 0;
                        sModuloDefault = string.Empty;
                        sDescripcion = string.Empty;
                        break;
                    case "4":
                        iEstatus = -1;
                        sModuloDefault = txtTextoBusqueda.Text.S();
                        sDescripcion = string.Empty;
                        break;

                }

                return new object[]{
                                        "@Descripcion", "%" + sDescripcion + "%",
                                        "@ModuloDefault", "%" + sModuloDefault + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }
        public object[] oArrFiltrosDll
        {
            get
            {
                int iEstatus = -1;
                string sModuloDefault = string.Empty;
                string sDescripcion = string.Empty;

                return new object[]{
                                        "@Descripcion", "%" + sDescripcion + "%",
                                        "@ModuloDefault", "%" + sModuloDefault + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }
        private const string sPagina = "frmRol.aspx";
        private const string sClase = "frmRol.aspx.cs";

        #endregion

    }
}