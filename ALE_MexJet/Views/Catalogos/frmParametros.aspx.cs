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
    public partial class frmParametros : System.Web.UI.Page, IViewCat
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.Parametros);
                LoadActions(DrPermisos);
                gvParametros.Columns["Status"].Visible = true;
                oPresenter = new Parametros_Presenter(this, new DBParametros());
                gvParametros.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvParametros.SettingsPager.ShowDisabledButtons = true;
                gvParametros.SettingsPager.ShowNumericButtons = true;
                gvParametros.SettingsPager.ShowSeparators = true;
                gvParametros.SettingsPager.Summary.Visible = true;
                gvParametros.SettingsPager.PageSizeItemSettings.Visible = true;
                gvParametros.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvParametros.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";


                ObtieneValores();
                if (IsPostBack)
                {
                    if (!string.IsNullOrEmpty(ViewState["oDatos"].S()))
                    {
                        LoadObjects((DataTable)ViewState["oDatos"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }

        }

        protected void gvParametros_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvParametros_RowDeleting", "Aviso");
            }
        }

        protected void gvParametros_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvParametros.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvParametros_CancelRowEditing", "Aviso");
            }
        }

        protected void gvParametros_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                DataTable dTa = (DataTable)ViewState["oDatos"];
                if (dTa.Select("Clave = '" + e.NewValues["Clave"].S() + "'").Length > 0 && e.NewValues["Clave"].S() != e.OldValues["Clave"].S())
                {
                    CancelEditing(e);
                    MostrarMensaje("La Clave: " + e.NewValues["Clave"] + " ya existe", "REGISTRO EXISTENTE");
                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                    oCrud = e;

                    if (eSaveObj != null)
                        eSaveObj(sender, e);

                    CancelEditing(e);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvParametros_RowUpdating", "Aviso");
            }
        }

        protected void gvParametros_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                DataTable dTa = (DataTable)ViewState["oDatos"];
                if (dTa.Select("Clave = '" + e.NewValues["Clave"].S() + "'").Length > 0)
                {
                    CancelEditing(e);
                    MostrarMensaje("La Clave: " + e.NewValues["Clave"] + " ya existe", "REGISTRO EXISTENTE");
                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    oCrud = e;

                    if (eNewObj != null)
                        eNewObj(sender, e);

                    CancelEditing(e);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvParametros_RowInserting", "Aviso");
            }
        }

        protected void gvParametros_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = true;
                if (e.Column.FieldName == "Descripcion" || e.Column.FieldName == "Valor")
                {
                    e.Editor.ReadOnly = false;
                }

                if (e.Column.FieldName == "TipoCliente")
                {
                    //if (eGetTipCliente != null)
                    //    eGetTipCliente(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["TipoCliente"];
                    cmb.ValueField = "TipoCliente";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "TipoClienteDescripcion";
                    cmb.DataBindItems();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvParametros_CellEditorInitialize", "Aviso");
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
                gvParametros.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvParametros.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
            }

        }

        protected void gvParametros_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e.NewValues["Clave"].S()))
                {
                    AddError(e.Errors, gvParametros.Columns["Clave"], "La clave es necesaria.");
                }

                if (string.IsNullOrEmpty(e.NewValues["Descripcion"].S()))
                {
                    AddError(e.Errors, gvParametros.Columns["Descripcion"], "La descripción es necesario.");
                }
                if (string.IsNullOrEmpty(e.NewValues["Valor"].S()))
                {
                    AddError(e.Errors, gvParametros.Columns["Valor"], "El Valor es necesario");
                }

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvParametros_RowValidating", "Aviso");
            }

        }
       
        protected void gvParametros_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvParametros.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvParametros_StartRowEditing", "Aviso");
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
        protected void gvParametros_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvParametros_CommandButtonInitialize", "Aviso");
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
                gvParametros.DataSource = null;
                gvParametros.DataSource = dtObject;
                gvParametros.DataBind();
                ViewState["oDatos"] = dtObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadCatalogoTipocliente(DataTable dtObject)
        {
            try
            {
                ViewState["TipoCliente"] = dtObject;
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
                gvParametros.JSProperties["cpText"] = sMensaje;
                gvParametros.JSProperties["cpShowPopup"] = true;
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
                gvParametros.CancelEdit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
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
                                    txtTextoBusqueda.Enabled = true;
                                    ddlTipoBusqueda.Enabled = true;
                                    btnExcel.Enabled = true;
                                    btnExportar.Enabled = true;
                                }
                                else
                                {
                                    btnBusqueda.Enabled = false;
                                    txtTextoBusqueda.Enabled = false;
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

        private const string sClase = "frmParametros.aspx.cs";
        private const string sPagina = "frmParametros.aspx";

        Parametros_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
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
                string sClave = string.Empty;
                string sDescripcion = string.Empty;
                string sValor = string.Empty;
                int iEstatus = -1;

                switch (ddlTipoBusqueda.SelectedItem.Value.S())
                {
                    case "1":
                        sClave = txtTextoBusqueda.Text.S();
                        iEstatus = -1;
                        sDescripcion = string.Empty;
                        sValor = string.Empty;
                        break;
                    case "2":
                        sClave = string.Empty;
                        iEstatus = -1;
                        sDescripcion = txtTextoBusqueda.Text.S();
                        sValor = string.Empty;
                        break;
                    case "3":
                        sClave = string.Empty;
                        iEstatus = -1;
                        sDescripcion = string.Empty;
                        sValor = txtTextoBusqueda.Text.S();
                        break;
                    
                    case "4":
                        sClave = string.Empty;
                        iEstatus = 1 ;
                        sDescripcion = string.Empty;
                        sValor = string.Empty;
                        break;
                    case "5":
                        sClave = string.Empty;
                        iEstatus = 0;
                        sDescripcion = string.Empty;
                        sValor = string.Empty;
                        break;
                }

                return new object[]{
                                        "@Clave", "%" + sClave + "%",
                                        "@Descripcion", "%" + sDescripcion + "%",
                                        "@Valor", "%" + sValor + "%",
                                        "@estatus", iEstatus
                                    };
            }

        }

        #endregion

        

        
    }
}