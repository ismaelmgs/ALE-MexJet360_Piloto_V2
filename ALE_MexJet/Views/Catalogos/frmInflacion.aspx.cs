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
    public partial class frmInflacion : System.Web.UI.Page, IViewInflacion
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.Inflacion);
                LoadActions(DrPermisos);
                gvInflacion.Columns["Status"].Visible = true;
                oPresenter = new Inflacion_Presenter(this, new DBInflacion());
                gvInflacion.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvInflacion.SettingsPager.ShowDisabledButtons = true;
                gvInflacion.SettingsPager.ShowNumericButtons = true;
                gvInflacion.SettingsPager.ShowSeparators = true;
                gvInflacion.SettingsPager.Summary.Visible = true;
                gvInflacion.SettingsPager.PageSizeItemSettings.Visible = true;
                gvInflacion.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvInflacion.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                ObtieneValores();


                if (!IsPostBack)
                {
                    if (gvInflacion.VisibleRowCount < 1)
                    {


                        gvInflacion.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                        gvInflacion.Columns["Status"].Visible = false;
                        gvInflacion.AddNewRow();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }

        protected void gvInflacion_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvInflacion.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvInflacion_CancelRowEditing", "Aviso");
            }
        }

        protected void gvInflacion_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    gvInflacion.Columns["Status"].Visible = false;

                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvInflacion_CellEditorInitialize", "Aviso");
            }
        }

        protected void gvInflacion_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvInflacion_RowDeleting", "Aviso");
            }
        }

        protected void gvInflacion_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;


                if (eNewObj != null)
                    eNewObj(sender, e);

                CancelEditing(e);
                gvInflacion.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvInflacion_RowInserting", "Aviso");
            }

        }

        protected void gvInflacion_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvInflacion_RowUpdating", "Aviso");
            }
        }

        protected void gvInflacion_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvInflacion.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvInflacion_StartRowEditing", "Aviso");
            }
        }


        protected void gvInflacion_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;

                if (eObjSelected != null)
                    eObjSelected(sender, e);

                if (bDuplicado)
                {
                    AddError(e.Errors, gvInflacion.Columns["TipoInflacion"], "Esta Inflación ya existe, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvInflacion_RowValidating", "Aviso");
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
                gvInflacion.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvInflacion.Columns["Status"].Visible = false;
                gvInflacion.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscar_Click", "Aviso");
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
        protected void gvInflacion_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvInflacion_CommandButtonInitialize", "Aviso");
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
                gvInflacion.DataSource = null;
                ViewState["oDatos"] = null;

                gvInflacion.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvInflacion.DataBind();
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
                gvInflacion.JSProperties["cpText"] = sMensaje;
                gvInflacion.JSProperties["cpShowPopup"] = true;
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
                gvInflacion.CancelEdit();
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

        private const string sClase = "frmInflacion.aspx.cs";
        private const string sPagina = "frmInflacion.aspx";

        Inflacion_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetSearchDetalle;
        public event EventHandler eNewObjDet;
        public event EventHandler eSaveObjDet;
        public event EventHandler eObjSelectedDet;
        public event EventHandler eDeleteObjDet;

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
                int iAño = 0;
                int iEstatus = -1;
                string sDescripcion = string.Empty;

                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "1":
                        iAño = txtTextoBusqueda.Text.S().I();
                        iEstatus = -1;
                        sDescripcion = string.Empty;
                        break;
                    case "2":
                        iAño = 0;
                        iEstatus = -1;
                        sDescripcion = txtTextoBusqueda.Text.S();
                        break;
                    case "3":
                        iAño = 0;
                        iEstatus = 1;
                        sDescripcion = string.Empty;
                        break;
                    case "4":
                        iEstatus = 0;
                        sDescripcion = string.Empty;
                        break;
                }

                return new object[]{
                                        "@Año", iAño,
                                        "@TipoInflacion", "%" + sDescripcion + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }


        public object[] oArrFiltrosDetalle
        {
            get
            {
                int iIdInflacion = Session["iIdInflacion"].S().I();

                return new object[]{
                                        "@idInflcacionDetalle", -1,
                                        "@idInflacion",iIdInflacion
                                    };
            }
        }

        #endregion

        protected void gvDetalle_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetalle_CommandButtonInitialize", "Aviso");
            }
        }

        protected void gvDetalle_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvDetalle = gvInflacion.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvDetalle") as DevExpress.Web.ASPxGridView;
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;

                if (eSaveObjDet != null)
                    eSaveObjDet(sender, e);

                CancelEditingDet(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetalle_RowUpdating", "Aviso");
            }
        }

        protected void gvDetalle_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                int i = Session["IndexGridDetail"].S().I();

                DevExpress.Web.ASPxGridView gvDetalle = gvInflacion.FindDetailRowTemplateControl(i, "gvDetalle") as DevExpress.Web.ASPxGridView;
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;
                iIdInflacion = Session["iIdInflacion"].S().I();


                if (eNewObjDet != null)
                    eNewObjDet(sender, e);

                CancelEditingDet(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetalle_RowInserting", "Aviso");
            }
        }

        protected void CancelEditingDet(CancelEventArgs e)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvDetalle = gvInflacion.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvDetalle") as DevExpress.Web.ASPxGridView;
                e.Cancel = true;
                gvDetalle.CancelEdit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int iIdInflacion
        {
            get { return ViewState["iIdInflacion"].S().I(); }
            set { ViewState["iIdInflacion"] = value; }
        }

        protected void gvDetalle_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvDetalle = gvInflacion.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvDetalle") as DevExpress.Web.ASPxGridView;
                gvDetalle.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetalle_StartRowEditing", "Aviso");
            }
        }

        protected void gvDetalle_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetalle_CellEditorInitialize", "Aviso");
            }
        }

        protected void gvInflacion_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            try
            {
                int iIndice = 0;
                iIdInflacion = 0;

                if (e.Expanded)
                {
                    iIndice = e.VisibleIndex;
                    Session["IndexGridDetail"] = iIndice;
                    object oFila = new object();
                    oFila = gvInflacion.GetRowValues(iIndice, gvInflacion.KeyFieldName);
                    iIdInflacion = oFila.S().I();
                    Session["iIdInflacion"] = iIdInflacion;

                    if (eGetSearchDetalle != null)
                        eGetSearchDetalle(null, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvInflacion_DetailRowExpandedChanged", "Aviso");
            }
        }

        public void LoadObjectsDetalle(DataTable dtObject)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvDetalle = gvInflacion.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvDetalle") as DevExpress.Web.ASPxGridView;
                if (gvDetalle != null)
                {
                    gvDetalle.DataSource = dtObject;
                    ViewState["oDatosDetalle"] = dtObject;
                    gvDetalle.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public void ObtieneValoresDetalles()
        {
            try
            {
                if (eGetSearchDetalle != null)
                    eGetSearchDetalle(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MostrarMensajeDet(string sMensaje, string sCaption)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvDetalle = gvInflacion.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvDetalle") as DevExpress.Web.ASPxGridView;
                popup.HeaderText = sCaption;
                gvDetalle.JSProperties["cpText"] = sMensaje;
                gvDetalle.JSProperties["cpShowPopup"] = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDetalle_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvDetalle = gvInflacion.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvDetalle") as DevExpress.Web.ASPxGridView;
                oCrud = e;
                iIdInflacion = Session["iIdInflacion"].S().I();
                
                if (eObjSelectedDet != null)
                    eObjSelectedDet(sender, e);
                /*
                if (gvDetalle.VisibleRowCount == 6)
                    AddError(e.Errors, gvDetalle.Columns["Semana"], "No se pueden agregar mas semanas.");
                */
                if (bDuplicado)
                {
                    AddError(e.Errors, gvDetalle.Columns["Año"], "Este año ya existe, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetalle_RowValidating", "Aviso");
            }
        }

        protected void gvDetalle_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;
                CancelEditingDet(e);

                if (eDeleteObjDet != null)
                    eDeleteObjDet(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetalle_RowDeleting", "Aviso");
            }
        }        
    }
}