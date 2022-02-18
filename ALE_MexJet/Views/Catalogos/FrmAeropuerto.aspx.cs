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
using System.Reflection;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Views.Catalogos
{
    public partial class FrmAeropuerto : System.Web.UI.Page, IViewCat
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.Aeropuerto);
                LoadActions(DrPermisos);
                gvAeropuerto.Columns["Status"].Visible = true;
                oPresenter = new Aeropuerto_Presenter(this, new DBAeropuerto());
                gvAeropuerto.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvAeropuerto.SettingsPager.ShowDisabledButtons = true;
                gvAeropuerto.SettingsPager.ShowNumericButtons = true;
                gvAeropuerto.SettingsPager.ShowSeparators = true;
                gvAeropuerto.SettingsPager.Summary.Visible = true;
                gvAeropuerto.SettingsPager.PageSizeItemSettings.Visible = true;
                gvAeropuerto.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvAeropuerto.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                ObtieneValores();

                if (!IsPostBack)
                {
                    if (gvAeropuerto.VisibleRowCount < 1)
                    {

                        gvAeropuerto.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                        gvAeropuerto.Columns["Status"].Visible = false;
                        gvAeropuerto.AddNewRow();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }
        protected void Unnamed_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });

        }
        protected void gvAeropuerto_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
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
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeropuerto_CellEditorInitialize", "Aviso");
            }
        }
        protected void gvAeropuerto_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            gvAeropuerto.Columns["Status"].Visible = true;
        }
        protected void gvAeropuerto_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeropuerto_RowDeleting", "Aviso");
            }
        }
        protected void gvAeropuerto_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;

                if (eNewObj != null)
                    eNewObj(sender, e);

                CancelEditing(e);
                gvAeropuerto.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeropuerto_RowInserting", "Aviso");
            }
        }
        protected void gvAeropuerto_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeropuerto_RowUpdating", "Aviso");
            }
        }
        protected void gvAeropuerto_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvAeropuerto.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeropuerto_StartRowEditing", "Aviso");
            }
        }
        protected void gvAeropuerto_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;

                if (eObjSelected != null)
                    eObjSelected(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeropuerto_RowValidating", "Aviso");
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
                gvAeropuerto.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvAeropuerto.Columns["Status"].Visible = false;
                gvAeropuerto.AddNewRow();
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
        protected void gvAeropuerto_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeropuerto_CommandButtonInitialize", "Aviso");
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
                gvAeropuerto.DataSource = null;
                ViewState["oDatos"] = null;

                gvAeropuerto.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvAeropuerto.DataBind();
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
                gvAeropuerto.JSProperties["cpText"] = sMensaje;
                gvAeropuerto.JSProperties["cpShowPopup"] = true;
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
                gvAeropuerto.CancelEdit();
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
                    btnExcel.Enabled = false;
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

        private const string sClase = "FrmAeropuerto.aspx.cs";
        private const string sPagina = "FrmAeropuerto.aspx";

        Aeropuerto_Presenter oPresenter;
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
                string sDescripcion = string.Empty;
                string sIATA = string.Empty;
                string sICAO = string.Empty;


                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "2":
                        iEstatus = -1;
                        sDescripcion = txtTextoBusqueda.Text.S();
                        break;
                    case "3":
                        iEstatus = 1;
                        sIATA = txtTextoBusqueda.Text.S();
                        sDescripcion = string.Empty;
                        sICAO = string.Empty;
                        break;
                    case "4":
                        iEstatus = -1;
                        sICAO = txtTextoBusqueda.Text.S();
                        sDescripcion = string.Empty;
                        sIATA = string.Empty;
                        break;
                    case "5":
                        iEstatus = 1;
                        sDescripcion = string.Empty;
                        sIATA = string.Empty;
                        sICAO = string.Empty;
                        break;
                    case "6":
                        iEstatus = 0;
                        sDescripcion = string.Empty;
                        sIATA = string.Empty;
                        sICAO = string.Empty;
                        break;

                }

                return new object[]{
                                        "@Descripcion", "%" + sDescripcion + "%",
                                        "@ICAO", "%" + sICAO + "%",
                                        "@IATA", "%" + sIATA + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }

        #endregion
    }
}


