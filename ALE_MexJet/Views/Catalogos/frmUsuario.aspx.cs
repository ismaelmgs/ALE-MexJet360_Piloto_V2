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
using ALE_MexJet.Clases;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using System.ComponentModel;
using ALE_MexJet.ControlesUsuario;
using DevExpress.Web.Data;
using DevExpress.Web;
using System.Reflection;

namespace ALE_MexJet.Views.Catalogos
{
    public partial class frmUsuario : System.Web.UI.Page, IViewUsuario
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.Usuario);
                LoadActions(DrPermisos);
                gvUsuarios.Columns["Descripcion"].Visible = true;
                gvUsuarios.Columns["Status"].Visible = true;
                oPresenter = new Usuario_Presenter(this, new DBUsuarios());
                gvUsuarios.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvUsuarios.SettingsPager.ShowDisabledButtons = true;
                gvUsuarios.SettingsPager.ShowNumericButtons = true;
                gvUsuarios.SettingsPager.ShowSeparators = true;
                gvUsuarios.SettingsPager.Summary.Visible = true;
                gvUsuarios.SettingsPager.PageSizeItemSettings.Visible = true;
                gvUsuarios.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvUsuarios.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                ObtieneValores();

                if (IsPostBack)
                {
                    if (!string.IsNullOrEmpty(ViewState["oDatos"].S()))
                    {
                        LoadObjects((DataTable)ViewState["oDatos"]);
                    }
                }

                if (!IsPostBack)
                {
                    if (gvUsuarios.VisibleRowCount < 1)
                    {

                        gvUsuarios.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                        gvUsuarios.Columns["Status"].Visible = false;
                        gvUsuarios.AddNewRow();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }

        protected void gvUsuarios_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvUsuarios.Columns["Descripcion"].Visible = true;
                gvUsuarios.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvUsuarios_CancelRowEditing", "Aviso");
            }

        }

        protected void gvUsuarios_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                if (e.Column.FieldName == "IdBase")
                {
                    if (eGetAero != null)
                        eGetAero(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["Base"];
                    cmb.ValueField = "idAeropuert";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "AeropuertoIATA";
                    cmb.DataBindItems();
                    //cmb.Value = iBase;
                }

                if (e.Column.FieldName == "Rol")
                {
                    if (eGetAero != null)
                        eGetRol(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["Rol"];
                    cmb.ValueField = "RolId";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "RolDescripcion";
                    cmb.DataBindItems();
                    //cmb.Value = iRol;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvUsuarios_CellEditorInitialize", "Aviso");
            }
        }

        protected void gvUsuarios_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvUsuarios_RowDeleting", "Aviso");
            }
        }

        protected void gvUsuarios_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;

                if (eSaveObj != null)
                    eSaveObj(sender, e);

                //if (eNewObj != null)
                //    eNewObj(sender, e);

                CancelEditing(e);
                gvUsuarios.Columns["Status"].Visible = true;
                gvUsuarios.Columns["Descripcion"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvUsuarios_RowUpdating", "Aviso");
            }

        }

        protected void gvUsuarios_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;

                if (eNewObj != null)
                    eNewObj(sender, e);

                CancelEditing(e);
                gvUsuarios.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvUsuarios_RowInserting", "Aviso");
            }
        }

        protected void gvUsuarios_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvUsuarios.SettingsText.PopupEditFormCaption = "Formulario de Edición";
                gvUsuarios.Columns["Descripcion"].Visible = false;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvUsuarios_RowInserting", "Aviso");
            }
        }

        protected void gvUsuarios_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;
                eCrud = Enumeraciones.TipoOperacion.Validar;

                if (eObjSelected != null)
                    eObjSelected(sender, e);

                if (bDuplicado)
                {
                    AddError(e.Errors, gvUsuarios.Columns["Descripcion"], "Este usuario ya existe, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTUA_RowValidating", "Aviso");
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
                gvUsuarios.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvUsuarios.Columns["Status"].Visible = false;
                gvUsuarios.AddNewRow();
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
        protected void gvUsuarios_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvUsuarios_CommandButtonInitialize", "Aviso");
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

        public void LoadCatalogoBase(DataTable dtObject)
        {
            try
            {
                ViewState["Base"] = dtObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadCatalogoRol(DataTable dtObject)
        {
            try
            {
                ViewState["Rol"] = dtObject;
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
                gvUsuarios.DataSource = null;
                ViewState["oDatos"] = null;

                gvUsuarios.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvUsuarios.DataBind();
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
                gvUsuarios.JSProperties["cpText"] = sMensaje;
                gvUsuarios.JSProperties["cpShowPopup"] = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void CancelEditing(CancelEventArgs e)
        {
            e.Cancel = true;
            gvUsuarios.CancelEdit();
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
                    textTextoBusqueda.Enabled = false;
                    ddlTipoBusqueda.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnExcel.Enabled = false;
                    btnNuevo2.Enabled = false;
                    btnExportar.Enabled = false;
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
                                    textTextoBusqueda.Enabled = true;
                                    ddlTipoBusqueda.Enabled = true;
                                    btnExcel.Enabled = true;
                                    btnExportar.Enabled = true;
                                }
                                else
                                {
                                    btnBusqueda.Enabled = false;
                                    textTextoBusqueda.Enabled = false;
                                    ddlTipoBusqueda.Enabled = false;
                                    btnExcel.Enabled = false;
                                    btnExportar.Enabled = false;
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

        private const string sClase = "frmUsuario.aspx.cs";
        private const string sPagina = "frmUsuario.aspx";

        Usuario_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetAero;
        public event EventHandler eGetRol;
        UserIdentity oUsuario = new UserIdentity();
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
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

        public object oCrud
        {
            get { return ViewState["CrudPiloto"]; }
            set { ViewState["CrudPiloto"] = value; }
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
                int iIdRol = 0;
                int iEstatus = -1;
                string sDescripcion = string.Empty;

                switch (ddlTipoBusqueda.SelectedItem.Value.S())
                {
                    case "1":
                        iIdRol = textTextoBusqueda.Text.S().I();
                        iEstatus = -1;
                        sDescripcion = string.Empty;
                        break;
                    case "2":
                        iIdRol = 0;
                        iEstatus = -1;
                        sDescripcion = textTextoBusqueda.Text.S();
                        break;
                    case "3":
                        iIdRol = 0;
                        iEstatus = 1;
                        sDescripcion = string.Empty;
                        break;
                    case "4":
                        iEstatus = 0;
                        sDescripcion = string.Empty;
                        break;

                }

                return new object[]{
                                        "@Id", iIdRol,
                                        "@Descripcion", "%" + sDescripcion + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }

        #endregion



    }
}