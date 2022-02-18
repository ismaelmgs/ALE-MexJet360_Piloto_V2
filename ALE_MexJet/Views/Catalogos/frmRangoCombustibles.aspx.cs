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
    public partial class frmRangoCombustibles : System.Web.UI.Page, IViewRangoCombustible
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.RangoCombustible);
                LoadActions(DrPermisos);
                oPresenter = new RangoCombustible_Presenter(this, new DBCombustibleGrupoModelo(), new DBRangoCombustible());
                gvRangoCombustible.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvRangoCombustible.SettingsPager.ShowDisabledButtons = true;
                gvRangoCombustible.SettingsPager.ShowNumericButtons = true;
                gvRangoCombustible.SettingsPager.ShowSeparators = true;
                gvRangoCombustible.SettingsPager.Summary.Visible = true;
                gvRangoCombustible.SettingsPager.PageSizeItemSettings.Visible = true;
                gvRangoCombustible.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvRangoCombustible.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";


                ObtieneValoresCombustible();
                if (!IsPostBack)
                {
                    gvRangoCombustible.Columns["Status"].Visible = true;
                    if (gvRangoCombustible.VisibleRowCount < 1)
                    {
                        gvRangoCombustible.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                        gvRangoCombustible.Columns["Status"].Visible = false;
                        gvRangoCombustible.AddNewRow();
                    }
                }
                else
                {
                    if (Session["IndexGridDetail"] != null)
                    {
                        ObtieneValoresRango();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }

        }

        protected void gvRangoCombustible_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                if (e.Column.FieldName == "IdGrupoModelo")
                {
                    if (eSearchObjGru != null)
                        eSearchObjGru(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["oDatosGModelo"];
                    cmb.ValueField = "GrupoModeloId";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "Descripcion";
                    cmb.DataBindItems();
                }
                if (e.Column.FieldName == "IdTipoContrato")
                {
                    if (eSearchObjCon != null)
                        eSearchObjCon(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["oDatosContrato"];
                    cmb.ValueField = "IdTipoContrato";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "Descripcion";
                    cmb.DataBindItems();
                }
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangoCombustible_CellEditorInitialize", "Aviso");
            }
        }
        protected void gvRango_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                ASPxGridView gridView = (ASPxGridView)sender;

                int i = Session["IndexGridDetail"].S().I();
                DevExpress.Web.ASPxGridView gvRango = gvRangoCombustible.FindDetailRowTemplateControl(i, "gvRango") as DevExpress.Web.ASPxGridView;
                gvRango.Columns["Status"].Visible = true;

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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRango_CellEditorInitialize", "Aviso");
            }
        }

        protected void gvRangoCombustible_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangoCombustible_RowDeleting", "Aviso");
            }
        }
        protected void gvRango_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;
                CancelEditingRan(e);

                if (eDeleteObjRan != null)
                    eDeleteObjRan(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRango_RowDeleting", "Aviso");
            }
        }

        protected void gvRangoCombustible_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                gvRangoCombustible.Columns["Status"].Visible = false;
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;

                if (eNewObj != null)
                    eNewObj(sender, e);

                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangoCombustible_RowInserting", "Aviso");
            }

        }
        protected void gvRango_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                int i = Session["IndexGridDetail"].S().I();
                DevExpress.Web.ASPxGridView gvRango = gvRangoCombustible.FindDetailRowTemplateControl(i, "gvRango") as DevExpress.Web.ASPxGridView;

                gvRango.Columns["Status"].Visible = false;


                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;
                iIdCombustible = Session["IdCombustible"].S().I();


                if (eNewObjRan != null)
                    eNewObjRan(sender, e);

                CancelEditingRan(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRango_RowInserting", "Aviso");
            }
        }

        protected void gvRangoCombustible_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangoCombustible_RowUpdating", "Aviso");
            }
        }
        protected void gvRango_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvRango = gvRangoCombustible.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvRango") as DevExpress.Web.ASPxGridView;
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;

                if (eSaveObjRan != null)
                    eSaveObjRan(sender, e);

                CancelEditingRan(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRango_RowUpdating", "Aviso");
            }
        }
        protected void gvRangoCombustible_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvRangoCombustible.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangoCombustible_StartRowEditing", "Aviso");
            }
        }
        protected void gvRango_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvRango = gvRangoCombustible.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvRango") as DevExpress.Web.ASPxGridView;
                gvRango.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRango_StartRowEditing", "Aviso");
            }
        }
        protected void gvRangoCombustible_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            try
            {
                int iIndice = 0;
                int iIdCombustible = 0;
                if (e.Expanded)
                {
                    iIndice = e.VisibleIndex;
                    Session["IndexGridDetail"] = iIndice;
                    object oFila = new object();
                    oFila = gvRangoCombustible.GetRowValues(iIndice, gvRangoCombustible.KeyFieldName);
                    iIdCombustible = oFila.S().I();
                    Session["IdCombustible"] = iIdCombustible;
                    if (eSearchObjRan != null)
                        eSearchObjRan(null, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangoCombustible_DetailRowExpandedChanged", "Aviso");
            }
        }
        protected void gvRangoCombustible_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;

                if (eObjSelected != null)
                    eObjSelected(sender, e);

                if (bDuplicado)
                {
                    AddError(e.Errors, gvRangoCombustible.Columns["IdTipoContrato"], "Este combustible ya existe, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangoCombustible_RowValidating", "Aviso");
            }
        }
        protected void gvRango_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvRango = gvRangoCombustible.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvRango") as DevExpress.Web.ASPxGridView;
                oCrud = e;

                if (e.NewValues["Hasta"].S().D() <= e.NewValues["Desde"].S().D())
                {
                    AddError(e.Errors, gvRango.Columns["Desde"], "Hasta debe ser mayor que Desde, favor de validarlo.");
                    return;
                }

                iIdCombustible = Session["IdGrupoModelo"].S().I();
                if (eValRango != null)
                    eValRango(sender, e);


                //if (eObjSelectedRan != null)
                //    eObjSelectedRan(sender, e);

                if (bErrorRango)
                {
                    AddError(e.Errors, gvRango.Columns["Desde"], "Este rango ya existe,favor de validarlo.");
                }

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangoCombustible_RowValidating", "Aviso");
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ObtieneValoresCombustible();
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
                gvRangoCombustible.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvRangoCombustible.Columns["Status"].Visible = false;
                gvRangoCombustible.AddNewRow();
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
        protected void gvRangoCombustible_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvRangoCombustible.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangoCombustible_CancelRowEditing", "Aviso");
            }
        }
        protected void gvRangoCombustible_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRangoCombustible_CommandButtonInitialize", "Aviso");
            }
        }
        protected void gvRango_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvRango_CommandButtonInitialize", "Aviso");
            }
        }

        #endregion

        #region "METODOS"

        public void ObtieneValoresCombustible()
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
        public void ObtieneValoresRango()
        {
            try
            {
                if (eSearchObjRan != null)
                    eSearchObjRan(null, EventArgs.Empty);
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
                gvRangoCombustible.DataSource = null;
                ViewState["oDatos"] = null;

                gvRangoCombustible.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvRangoCombustible.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void LoadObjectsRangos(DataTable dtObject)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvRangos = gvRangoCombustible.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvRango") as DevExpress.Web.ASPxGridView;
                if (gvRangos != null)
                {
                    gvRangos.DataSource = dtObject;
                    ViewState["oDatosRangos"] = dtObject;
                    gvRangos.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void LoadObjectsGrupoModelo(DataTable dtObject)
        {
            try
            {
                ViewState["oDatosGModelo"] = dtObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadObjectsContrato(DataTable dtObject)
        {
            try
            {
                ViewState["oDatosContrato"] = dtObject;
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
                gvRangoCombustible.JSProperties["cpText"] = sMensaje;
                gvRangoCombustible.JSProperties["cpShowPopup"] = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MostrarMensajeRan(string sMensaje, string sCaption)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvRango = gvRangoCombustible.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvRango") as DevExpress.Web.ASPxGridView;
                popup.HeaderText = sCaption;
                gvRango.JSProperties["cpText"] = sMensaje;
                gvRango.JSProperties["cpShowPopup"] = true;
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
                gvRangoCombustible.CancelEdit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void CancelEditingRan(CancelEventArgs e)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvRango = gvRangoCombustible.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvRango") as DevExpress.Web.ASPxGridView;
                e.Cancel = true;
                gvRango.CancelEdit();
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

        private const string sClase = "frmRangoCombustibles.aspx.cs";
        private const string sPagina = "frmRangoCombustibles.aspx";

        RangoCombustible_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eNewObjRan;
        public event EventHandler eSaveObjRan;
        public event EventHandler eDeleteObjRan;
        public event EventHandler eSearchObjRan;
        public event EventHandler eObjSelectedRan;
        public event EventHandler eSearchObjGru;
        public event EventHandler eSearchObjCon;
        public event EventHandler eValRango;
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
        public bool bErrorRango
        {
            get
            {
                return (bool)ViewState["Rango"];
            }
            set
            {
                ViewState["Rango"] = value;
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
        public int iIdCombustible
        {
            get { return ViewState["IdCombusRan"].S().I(); }
            set { ViewState["IdCombusRan"] = value; }
        }
        public object[] oArrFiltros
        {
            get
            {
                string sGrupoModelo = string.Empty;
                int iTipoGrupo = 0;
                string sTipoContrato = string.Empty;
                int iEstatus = -1;

                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "1":
                        sGrupoModelo = txtTextoBusqueda.Text.S();
                        iTipoGrupo = 0;
                        sTipoContrato = string.Empty;
                        break;
                    case "2":
                        if (txtTextoBusqueda.Text.S() == "Nacional")
                            iTipoGrupo = 1;
                        else
                            iTipoGrupo = 2;

                        sGrupoModelo = string.Empty;                   
                        //iTipoGrupo = txtTextoBusqueda.Text.S() == "Nacional" ? 1 : 2;
                        sTipoContrato = string.Empty;
                        break;
                    case "3":
                        sGrupoModelo = string.Empty;
                        iTipoGrupo = 0;
                        sTipoContrato = txtTextoBusqueda.Text.S();
                        break;
                    case"4":
                        sGrupoModelo = string.Empty;
                        iTipoGrupo = 0;
                        sTipoContrato = string.Empty;
                        iEstatus = 1;// solo activos
                        break;
                    case "5":
                        sGrupoModelo = string.Empty;
                        iTipoGrupo = 0;
                        sTipoContrato = string.Empty;
                        iEstatus = 0;// solo inactivos
                        break;
                }

                return new object[]{
                                        "@GrupoModelo", "%" + sGrupoModelo + "%",
                                        "@IdTipoGrupo", iTipoGrupo,
                                        "@Contrato", "%" + sTipoContrato + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }

        public object[] oArrFiltrosGruModelo
        {
            get
            {
                int iIdGrupoModelo = 0;
                string sDescripcion = string.Empty;
                int iConsumoGalones = 0;
                int iEstatus = -1;

                return new object[]{ 
                                        "@GrupoModeloId", iIdGrupoModelo,
                                        "@Descripcion", "%" + sDescripcion + "%",
                                        "@ConsumoGalones", iConsumoGalones,
                                        "@estatus", iEstatus
                                    };
            }
        }

        public object[] oArrFiltrosCon
        {
            get
            {
                string sDescripcion = string.Empty;
                int iEstatus = 0;

                return new object[]{ 
                                        "@Descripcion", "%" + sDescripcion + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }

        public object[] oArrFiltrosRan
        {
            get
            {
                int iIdCombustible = Session["IdCombustible"].S().I(); ;
                decimal dDesde = 0.0M;
                decimal dHasta = 0.0M;
                decimal dAumento = 0.0M;
                int iEstatus = -1;

                return new object[]{ 
                                        "@IdCombustible", iIdCombustible,
                                        "@Desde", dDesde ,
                                        "@Hasta", dHasta,
                                        "@Aumento", dAumento,
                                        "@estatus", iEstatus
                                    };
            }
        }

        #endregion

       

    }
}