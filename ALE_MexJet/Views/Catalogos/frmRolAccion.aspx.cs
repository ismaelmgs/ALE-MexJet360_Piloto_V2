using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using System;
using System.Data;
using System.Web.UI;
using NucleoBase.Core;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using System.ComponentModel;
using ALE_MexJet.ControlesUsuario;
using DevExpress.Web;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DevExpress.Web.Data;
using DevExpress.Data;
using System.Collections.Specialized;
using System.Reflection;
using ALE_MexJet.Clases;


namespace ALE_MexJet.Views.Catalogos
{
    public partial class frmRolAccion: System.Web.UI.Page, IViewRolAccion
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new RolAccion_Presenter(this, new DBRolAccion());
                gvRolAccion.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvRolAccion.SettingsPager.ShowDisabledButtons = false;
                gvRolAccion.SettingsPager.ShowNumericButtons = true;
                gvRolAccion.SettingsPager.ShowSeparators = true;
                gvRolAccion.SettingsPager.Summary.Visible = true;
                gvRolAccion.SettingsPager.PageSizeItemSettings.Visible = true;
                gvRolAccion.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvRolAccion.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                ObtieneValoresRol();
                if (IsPostBack)
                {
                    LoadObjects((DataTable)Session["oDatos"]);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }
        protected void gvRolAccion_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRolAccion_RowUpdating", "Aviso");
            }
        }
        protected void gvRolAccion_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;
                oSelec = ddlRol;
                if (eUpdateObj != null)
                    eUpdateObj(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRolAccion_BatchUpdate", "Aviso");
            }  
        }
        protected void gvRolAccion_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRolAccion_CellEditorInitialize", "Aviso");
            }

        }
        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ObtieneValores();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlRol_SelectedIndexChanged", "Aviso");
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
        public void ObtieneValoresRol()
        {
            try
            {
                if (eSearchObjdll != null)
                    eSearchObjdll(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GuardarValoresRol()
        {
            try
            {
                if (eNewObj != null)
                    eNewObj(null, EventArgs.Empty);
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
                gvRolAccion.DataSource = null;
                gvRolAccion.DataSource = dtObject;
                gvRolAccion.DataBind();
                Session["oDatos"] = dtObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadCatalogoRol(DataTable dtRol)
        {
            try
            {
                ddlRol.DataSource = null;
                ddlRol.DataSource = dtRol;
                ddlRol.ValueField = "RolId";
                ddlRol.TextField = "RolDescripcion";
                ddlRol.DataBind();
                ViewState["oDatosddl"] = dtRol;
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
                gvRolAccion.JSProperties["cpText"] = sMensaje;
                gvRolAccion.JSProperties["cpShowPopup"] = true;
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
                gvRolAccion.CancelEdit();
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


        #endregion

        #region "Vars y Propiedades"
        RolAccion_Presenter oPresenter;

        private const string sClase = "frmRolAccion.aspx.cs";
        private const string sPagina = "frmRolAccion.aspx";

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchObjdll;
        public event EventHandler eUpdateObj;

        public object oCrud
        {
            get { return ViewState["CrudRolAccion"]; }
            set { ViewState["CrudRolAccion"] = value; }
        }

        public object oSelec
        {
            get { return ddlRol; }
            set { ddlRol = (ASPxComboBox)value; }
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
                int iIdRol;
                int iEstatus;

                iIdRol = ddlRol.Value.S().I();
                iEstatus = 1;

                return new object[]{
                                        "@IdRol",  iIdRol ,
                                        "@IdStatus", iEstatus
                                    };
            }

        }
        public object[] oArrFiltrosdll
        {
            get
            {
                string sModuloDefault = string.Empty;
                string sDescripcion = string.Empty;
                int iEstatus = 1;

                return new object[]{
                                        //"@RolId",  iIdRol ,
                                        "@Descripcion","%" + sDescripcion + "%",
                                        "@ModuloDefault", "%" + sModuloDefault + "%",
                                        "@estatus", iEstatus
                                    };
            }

        }
        public DataTable CrearDataTable
        {
            get
            {
                DataTable dtRolAccion = new DataTable();
                dtRolAccion.TableName = "dtRolAccion";
                dtRolAccion.Columns.Add("IdRol", typeof(int));
                dtRolAccion.Columns.Add("IdModulo", typeof(int));
                dtRolAccion.Columns.Add("IdAccion", typeof(int));
                dtRolAccion.Columns.Add("Permitido", typeof(byte));
                dtRolAccion.Columns.Add("Status", typeof(byte));
                dtRolAccion.Columns.Add("UsuarioCreacion", typeof(string));
                dtRolAccion.Columns.Add("UsuarioModificacion", typeof(string));
                dtRolAccion.Columns.Add("IP", typeof(string));

                return dtRolAccion;
            }

        }
        #endregion

        
    }
}