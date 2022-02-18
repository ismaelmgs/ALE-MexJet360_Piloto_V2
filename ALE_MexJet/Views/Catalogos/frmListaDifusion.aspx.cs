using ALE_MexJet.Clases;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ALE_MexJet.Presenter;
using ALE_MexJet.Objetos;
using System.ComponentModel;
using System.Data;
using NucleoBase.Core;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using DevExpress.XtraPrinting;
using DevExpress.Export;

namespace ALE_MexJet.Views.Catalogos
{
    public partial class frmListaDifusion : System.Web.UI.Page, IViewCat
    {
        #region EVENTOS


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.ListaDifusion);
                LoadActions(DrPermisos);
                gvListaDifusion.Columns["Status"].Visible = true;
                oPresenter = new ListaDifusion_Presenter(this, new DBListaDifusion());
                gvListaDifusion.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvListaDifusion.SettingsPager.ShowDisabledButtons = true;
                gvListaDifusion.SettingsPager.ShowNumericButtons = true;
                gvListaDifusion.SettingsPager.ShowSeparators = true;
                gvListaDifusion.SettingsPager.Summary.Visible = true;
                gvListaDifusion.SettingsPager.PageSizeItemSettings.Visible = true;
                gvListaDifusion.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvListaDifusion.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                ObtieneValores();

                if (!IsPostBack)
                {
                    if (gvListaDifusion.VisibleRowCount < 1)
                    {

                        gvListaDifusion.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                        gvListaDifusion.Columns["Status"].Visible = false;
                        gvListaDifusion.AddNewRow();
                    }
                }

            }
            catch (Exception ex)
            {

                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                ObtieneValores();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso");
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                gvListaDifusion.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvListaDifusion.Columns["Status"].Visible = false;
                gvListaDifusion.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
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

        protected void gvListaDifusion_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvListaDifusion_RowDeleting", "Aviso");
            }
        }

        protected void gvListaDifusion_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;


                if (eNewObj != null)
                    eNewObj(sender, e);

                CancelEditing(e);
                gvListaDifusion.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvListaDifusion_RowInserting", "Aviso");
            }
        }

        protected void gvListaDifusion_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvListaDifusion_RowUpdating", "Aviso");
            }
        }

        protected void gvListaDifusion_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvListaDifusion_CommandButtonInitialize", "Aviso");
            }
        }

        protected void gvListaDifusion_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvListaDifusion.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvListaDifusion_StartRowEditing", "Aviso");
            }
        }

        protected void gvListaDifusion_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;

                if (eObjSelected != null)
                    eObjSelected(sender, e);

                if (bDuplicado)
                {
                    AddError(e.Errors, gvListaDifusion.Columns["Descripcion"], "Esta descripción de titulo ya existe, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvListaDifusion_RowValidating", "Aviso");
            }
        }

        protected void gvListaDifusion_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvListaDifusion.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvListaDifusion_CancelRowEditing", "Aviso");
            }
        }

        protected void gvListaDifusion_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    gvListaDifusion.Columns["Status"].Visible = false;

                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvListaDifusion_CellEditorInitialize", "Aviso");
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

        #region METODOS
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
                gvListaDifusion.DataSource = null;
                ViewState["oDatos"] = null;

                gvListaDifusion.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvListaDifusion.DataBind();
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
                gvListaDifusion.JSProperties["cpText"] = sMensaje;
                gvListaDifusion.JSProperties["cpShowPopup"] = true;
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
                gvListaDifusion.CancelEdit();
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


        #region VARIABLES

        private const string sClase = "frmListaDifusion.aspx.cs";
        private const string sPagina = "frmListaDifusion.aspx";

        ListaDifusion_Presenter oPresenter;

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        UserIdentity oUsuario = new UserIdentity();

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
                        sDescripcion = txtTextoBusqueda.Text.S();
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
                                        "@Descripcion", "%" + sDescripcion + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }
        #endregion
    }
}