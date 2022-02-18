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
    public partial class frmModelo : System.Web.UI.Page, IViewModelo
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.Modelo);
                LoadActions(DrPermisos);
                gvModelo.Columns["Status"].Visible = true;
                oPresenter = new Modelo_Presenter(this, new DBModelo());
                gvModelo.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvModelo.SettingsPager.ShowDisabledButtons = true;
                gvModelo.SettingsPager.ShowNumericButtons = true;
                gvModelo.SettingsPager.ShowSeparators = true;
                gvModelo.SettingsPager.Summary.Visible = true;
                gvModelo.SettingsPager.PageSizeItemSettings.Visible = true;
                gvModelo.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvModelo.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                ObtieneValores();
                //}

                if (!IsPostBack)
                {
                    if (gvModelo.VisibleRowCount < 1)
                    {

                        gvModelo.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                        gvModelo.Columns["Status"].Visible = false;
                        gvModelo.AddNewRow();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }

        }

        protected void gvModelo_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvModelo.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvModelo_CancelRowEditing", "Aviso");
            }
        }

        protected void gvModelo_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    gvModelo.Columns["Status"].Visible = false;

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
                    cmb.ValueField = "IdMarca";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "DescripcionMarca";
                    cmb.DataBindItems();
                }
                if (e.Column.FieldName == "IdGrupoModelo")
                {
                    if (eGetGrupoModelo != null)
                        eGetGrupoModelo(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["GrupoModelo"];
                    cmb.ValueField = "GrupoModeloId";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "Descripcion";
                    cmb.DataBindItems();
                }
                if (e.Column.FieldName == "IdTipo")
                {
                    if (eGetTipo != null)
                        eGetTipo(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["Tipo"];
                    cmb.ValueField = "IdTipoModelo";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "TipoModeloDescripcion";
                    cmb.DataBindItems();
                }
                if (e.Column.FieldName == "IdGrupoTamaño")
                {
                    if (eGetGrupoEspacio != null)
                        eGetGrupoEspacio(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["GrupoEspacio"];
                    cmb.ValueField = "IdGrupoEspacio";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "GrupoEspacioDescripcion";
                    cmb.DataBindItems();
                }

                if (e.Column.FieldName == "IdDesignador")
                {
                    if (eGetDesignador != null)
                        eGetDesignador(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["Designador"];
                    cmb.ValueField = "IdDesignador";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "Descripcion";
                    cmb.DataBindItems();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvModelo_CellEditorInitialize", "Aviso");
            }
        }

        protected void gvModelo_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvModelo_RowDeleting", "Aviso");
            }
        }

        protected void gvModelo_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;



                if (eNewObj != null)
                    eNewObj(sender, e);

                CancelEditing(e);
                gvModelo.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvModelo_RowInserting", "Aviso");
            }
        }

        protected void gvModelo_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvModelo_RowUpdating", "Aviso");
            }

        }

        protected void gvModelo_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvModelo.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvModelo_StartRowEditing", "Aviso");
            }
        }


        protected void gvModelo_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {

            try
            {
                oCrud = e;

                if (eObjSelected != null)
                    eObjSelected(sender, e);

                if (bDuplicado)
                {
                    AddError(e.Errors, gvModelo.Columns["DescripcionModelo"], "El modelo ya existe, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvModelo_RowValidating", "Aviso");
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
                gvModelo.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvModelo.Columns["Status"].Visible = false;
                gvModelo.AddNewRow();
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
        protected void gvModelo_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvModelo_CommandButtonInitialize", "Aviso");
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
                gvModelo.DataSource = null;
                ViewState["oDatos"] = null;

                gvModelo.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvModelo.DataBind();
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
                gvModelo.JSProperties["cpText"] = sMensaje;
                gvModelo.JSProperties["cpShowPopup"] = true;
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
                gvModelo.CancelEdit();
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

        public void LoadCatalogoGrupoModelo(DataTable dtObjCat)
        {
            try
            {
                ViewState["GrupoModelo"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadCatalogoGrupoEspacio(DataTable dtObjCat)
        {
            try
            {
                ViewState["GrupoEspacio"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadCatalogoDesignador(DataTable dtObjCat)
        {
            try
            {
                ViewState["Designador"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadCatalogoTipo(DataTable dtObjCat)
        {
            try
            {
                ViewState["Tipo"] = dtObjCat;
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

        private const string sClase = "frmModelo.aspx.cs";
        private const string sPagina = "frmModelo.aspx";

        Modelo_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetMarca;
        public event EventHandler eGetTipo;
        public event EventHandler eGetGrupoModelo;
        public event EventHandler eGetGrupoEspacio;
        public event EventHandler eGetDesignador;
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
                int iEstatus = -1;
                string sDescripcion = string.Empty;

                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "1":
                        iEstatus = -1;
                        sDescripcion = txtTextoBusqueda.Text.S(); ;
                        break;
                    case "2":
                        iEstatus = 1;
                        sDescripcion = string.Empty;
                        break;
                    case "3":
                        iEstatus = 0;
                        sDescripcion = string.Empty;
                        break;

                }

                return new object[]{
                                        "@DescripcionModelo", "%" + sDescripcion + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }

        #endregion

        
    }
}