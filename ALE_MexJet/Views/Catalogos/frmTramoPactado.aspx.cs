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
using ALE_MexJet.Clases;
using DevExpress.Utils;
using System.Reflection;

namespace ALE_MexJet.Views.Catalogos
{
    public partial class frmTramoPactado : System.Web.UI.Page, IViewTramoPactado
    {
        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.TramoPactado);
                LoadActions(DrPermisos);
                gvTramoPactado.Columns["Status"].Visible = true;
                oPresenter = new TramoPactado_Presenter(this, new DBTramoPactado());
                gvTramoPactado.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvTramoPactado.SettingsPager.ShowDisabledButtons = true;
                gvTramoPactado.SettingsPager.ShowNumericButtons = true;
                gvTramoPactado.SettingsPager.ShowSeparators = true;
                gvTramoPactado.SettingsPager.Summary.Visible = true;
                gvTramoPactado.SettingsPager.PageSizeItemSettings.Visible = true;
                gvTramoPactado.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvTramoPactado.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                ObtieneValores();
                if (!IsPostBack)
                {
                    iOrigen = 0;
                    if (gvTramoPactado.VisibleRowCount < 1)
                    {
                        gvTramoPactado.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                        gvTramoPactado.Columns["Status"].Visible = false;
                        gvTramoPactado.AddNewRow();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }

        protected void gvTramoPactado_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvTramoPactado.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramoPactado_CancelRowEditing", "Aviso");
            }

        }

        protected void gvTramoPactado_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;

                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    gvTramoPactado.Columns["Status"].Visible = false;

                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }


                if (e.Column.FieldName == "IdDestino")
                {

                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.Callback += new CallbackEventHandlerBase(cmbBase_OnCallback);
                    //}

                    //if (eGetAeropuerto != null)
                    //    eGetAeropuerto(this, e);
                    //ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    //cmb.DataSource = (DataTable)ViewState["Aeropuerto"];
                    //cmb.ValueField = "idAeropuert";
                    //cmb.ValueType = typeof(Int32);
                    //cmb.TextField = "AeropuertoIATA";
                    //cmb.DataBindItems();

                }

                if (e.Column.FieldName == "IdGrupoModelo")
                {
                    if (eGetGrupoModelo != null)
                        eGetGrupoModelo(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    DataTable dtGrupoModelo = (DataTable)ViewState["GrupoModelo"];
                    int id = cmb.Value.I();

                    DataRow[] drResults = dtGrupoModelo.Select("GrupoModeloId = " + id.S());
                    if (drResults.Length == 0)
                        cmb.Value = null;

                    cmb.DataSource = dtGrupoModelo;
                    cmb.ValueField = "GrupoModeloId";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "Descripcion";
                    cmb.DataBindItems();

                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramoPactado_CellEditorInitialize", "Aviso");
            }

        }

        protected void gvTramoPactado_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramoPactado_RowDeleting", "Aviso");
            }
        }

        protected void gvTramoPactado_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;


                if (eNewObj != null)
                    eNewObj(sender, e);

                CancelEditing(e);
                gvTramoPactado.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramoPactado_RowInserting", "Aviso");
            }

        }

        protected void gvTramoPactado_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramoPactado_RowUpdating", "Aviso");
            }

        }

        protected void gvTramoPactado_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvTramoPactado.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramoPactado_StartRowEditing", "Aviso");
            }
        }

        protected void gvTramoPactado_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;

                if (e.NewValues["IdOrigen"].S() == e.NewValues["IdDestino"].S())
                {
                    AddError(e.Errors, gvTramoPactado.Columns["IdOrigen"], "El Origen y Destino no pueden ser iguales, favor de validarlo.");
                }

                if (eObjSelected != null)
                    eObjSelected(sender, e);


                if (bDuplicado)
                {
                    AddError(e.Errors, gvTramoPactado.Columns["IdGrupoModelo"], "Este TramoPactado ya existe, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramoPactado_RowValidating", "Aviso");
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
                gvTramoPactado.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvTramoPactado.Columns["Status"].Visible = false;
                gvTramoPactado.AddNewRow();
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
        protected void ASPxComboBox_OnItemsRequestedByFilterConditionOrigen(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {

                ASPxComboBox comboBox = (ASPxComboBox)source;
                sFiltroAeropuerto = e.Filter;
                if (sFiltroAeropuerto.Length < 1)
                {
                    if (eGetAeropuertoOrigen != null)
                        eGetAeropuertoOrigen(this, e);
                }
                else
                {
                    if (eGetAeropuertoOrigenFiltrado != null)
                        eGetAeropuertoOrigenFiltrado(source, e);
                }

                comboBox.DataSource = dtTarifaOrigen;
                comboBox.ValueField = "idAeropuert";
                comboBox.ValueType = typeof(int);
                comboBox.TextField = "AeropuertoIATA";
                comboBox.DataBindItems();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ASPxComboBox_OnItemsRequestedByFilterConditionOrigen", "Aviso");
            }
        }

        protected void ASPxComboBox_OnItemsRequestedByFilterConditionDestino(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {

                ASPxComboBox comboBox = (ASPxComboBox)source;
                sFiltroAeropuerto = e.Filter;
                if (sFiltroAeropuerto.Length < 1)
                {
                    if (eGetAeropuertoDestino != null)
                    {
                        eGetAeropuertoDestino(this, e);
                    }
                    else
                    {
                        return;
                    }
                        

                }
                else
                {
                    if (eGetAeropuertoDestinoFiltrado != null)
                        eGetAeropuertoDestinoFiltrado(source, e);
                }


                if (dtTarifaDestino.Rows.Count == 0)
                {
                    comboBox.Text = "";
                    comboBox.Value = null;
                    dtTarifaDestino = null;
                    comboBox.SelectedIndex = 0;
                    comboBox.SelectedItem = null;
                    comboBox.DataBind();
                }
                else
                {
                    comboBox.DataSource = dtTarifaDestino;
                    comboBox.ValueField = "idAeropuert";
                    comboBox.ValueType = typeof(int);
                    comboBox.TextField = "AeropuertoIATA";
                    comboBox.DataBind();
                }


            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ASPxComboBox_OnItemsRequestedByFilterConditionDestino", "Aviso");
            }
        }

        protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(e.Value.S()))
                {
                    e.Value.S();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ASPxComboBox_OnItemRequestedByValue", "Aviso");
            }

        }

        protected void gvTramoPactado_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramoPactado_CommandButtonInitialize", "Aviso");
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
                if (eGetGrupoModelo != null)
                    eGetGrupoModelo(null, EventArgs.Empty);
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
                gvTramoPactado.DataSource = null;
                ViewState["oDatos"] = null;

                gvTramoPactado.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvTramoPactado.DataBind();
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
                gvTramoPactado.JSProperties["cpCaption"] = sCaption;
                gvTramoPactado.JSProperties["cpText"] = sMensaje;
                gvTramoPactado.JSProperties["cpShowPopup"] = true;
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
                gvTramoPactado.CancelEdit();
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
        public void LoadCatalogoAeropuertos(DataTable dtObjCat)
        {
            try
            {
                ViewState["Aeropuerto"] = dtObjCat;
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
        TramoPactado_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetAeropuerto;
        public event EventHandler eGetGrupoModelo;
        public event EventHandler eGetAeropuertoOrigen;
        public event EventHandler eGetAeropuertoDestino;
        public event EventHandler eGetAeropuertoOrigenFiltrado;
        public event EventHandler eGetAeropuertoDestinoFiltrado;
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
         
                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "1":
                        iEstatus = -1;
                        sDescripcion = txtTextoBusqueda.Text;
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
                                        "@GrupoModelo","%" + sDescripcion + "%",
                                        "@estatus", iEstatus
                                    };

                //return new object[]{
                //                        "@IdGrupoModelo", iGrupoModelo,
                //                        "@estatus", iEstatus
                //                    };
            }
        }

       

        private const string sPagina = "frmTramoPactado.aspx";
        private const string sClase = "frmTramoPactado.aspx.cs";
 

        public DataTable dtTarifaOrigen
        {
            get
            {
                return (DataTable)Session["Origen"];
            }
            set
            {
                Session["Origen"] = value;
            }
        }
        public DataTable dtTarifaDestino
        {
            get
            {
                return (DataTable)Session["Destino"];
            }
            set
            {
                Session["Destino"] = value;
            }
        }

        #endregion   

        public int iOrigen
        {
            get
            {
                return (int)Session["idOrigen"];
            }
            set
            {
                Session["idOrigen"] = value;
            }
        }
        public string sFiltroAeropuerto
        {
            get
            {
                return ViewState["FiltroAeropuerto"].S();
            }
            set
            {
                ViewState["FiltroAeropuerto"] = value;
            }
        }

        void cmbBase_OnCallback(object source, CallbackEventArgsBase e)
        {
            try
            {
                //iOrigen = e.Parameter.S().I();
                //FillCombo(source as ASPxComboBox, e.Parameter);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "cmbBase_OnCallback", "Aviso");
            }
        }

        protected void FillCombo(ASPxComboBox cmb, string country)
        {
            try
            {

                if (string.IsNullOrEmpty(country)) return;
                if (eGetAeropuertoDestino != null)
                    eGetAeropuertoDestino(this, EventArgs.Empty);
                cmb.Items.Clear();
                cmb.DataSource = dtTarifaDestino;
                cmb.ValueField = "idAeropuert";
                cmb.ValueType = typeof(int);
                cmb.TextField = "AeropuertoIATA";
                cmb.DataBindItems();

            }
            catch (Exception ex)
            {
                throw ex;
                //Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "FillCombo", "Aviso");
            }
        }

         

    }
}