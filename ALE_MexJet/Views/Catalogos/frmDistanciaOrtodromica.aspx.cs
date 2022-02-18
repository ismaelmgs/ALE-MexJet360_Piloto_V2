﻿using ALE_MexJet.DomainModel;
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
    public partial class frmDistanciaOrtodromica : System.Web.UI.Page, IViewDistanciaOrtodromica
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.DistanciasOrtodromicas);
                LoadActions(DrPermisos);
                gvDistanciaOrtodromica.Columns["Status"].Visible = true;
                oPresenter = new DistanciaOrtodromica_Presenter(this, new DBDistanciaOrtodromica());
                gvDistanciaOrtodromica.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvDistanciaOrtodromica.SettingsPager.ShowDisabledButtons = true;
                gvDistanciaOrtodromica.SettingsPager.ShowNumericButtons = true;
                gvDistanciaOrtodromica.SettingsPager.ShowSeparators = true;
                gvDistanciaOrtodromica.SettingsPager.Summary.Visible = true;
                gvDistanciaOrtodromica.SettingsPager.PageSizeItemSettings.Visible = true;
                gvDistanciaOrtodromica.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvDistanciaOrtodromica.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                ObtieneValores();

                if (!IsPostBack)
                {
                    if (gvDistanciaOrtodromica.VisibleRowCount < 1)
                    {

                        gvDistanciaOrtodromica.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                        gvDistanciaOrtodromica.Columns["Status"].Visible = false;
                        gvDistanciaOrtodromica.AddNewRow();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }

        protected void gvDistanciaOrtodromica_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvDistanciaOrtodromica.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDistanciaOrtodromica_CancelRowEditing", "Aviso");
            }
        }

        protected void gvDistanciaOrtodromica_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    gvDistanciaOrtodromica.Columns["Status"].Visible = false;

                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDistanciaOrtodromica_CellEditorInitialize", "Aviso");
            }
        }

        protected void gvDistanciaOrtodromica_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDistanciaOrtodromica_RowDeleting", "Aviso");
            }
        }

        protected void gvDistanciaOrtodromica_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;


                if (eNewObj != null)
                    eNewObj(sender, e);

                CancelEditing(e);
                gvDistanciaOrtodromica.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDistanciaOrtodromica_RowInserting", "Aviso");
            }
        }

        protected void gvDistanciaOrtodromica_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDistanciaOrtodromica_RowUpdating", "Aviso");
            }

        }

        protected void gvDistanciaOrtodromica_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvDistanciaOrtodromica.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDistanciaOrtodromica_StartRowEditing", "Aviso");
            }
        }


        protected void gvDistanciaOrtodromica_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;

                if (eObjSelected != null)
                    eObjSelected(sender, e);

                if (bDuplicado)
                {
                    AddError(e.Errors, gvDistanciaOrtodromica.Columns["IdOrigen"], "Esta Destino ya existe, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDistanciaOrtodromica_RowValidating", "Aviso");
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
                gvDistanciaOrtodromica.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvDistanciaOrtodromica.Columns["Status"].Visible = false;
                gvDistanciaOrtodromica.AddNewRow();
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

        protected void ASPxComboBox_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                ASPxComboBox comboBox = (ASPxComboBox)source;
                sFiltroAeropuerto = e.Filter;
                if (sFiltroAeropuerto.Length < 1)
                {
                    if (eGetAeropuerto != null)
                        eGetAeropuerto(this, e);
                }
                else
                {
                    if (eGetAeropuertoFiltro != null)
                        eGetAeropuertoFiltro(source, e);
                }

                comboBox.DataSource = (DataTable)ViewState["Aeropuerto"];
                comboBox.ValueField = "idAeropuert";
                comboBox.ValueType = typeof(Int32);
                comboBox.TextField = "AeropuertoIATA";
                comboBox.DataBindItems();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ASPxComboBox_OnItemsRequestedByFilterCondition", "Aviso");
            }
        }

        protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            //long value = 0;
            //if(e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            //    return;
            //ASPxComboBox comboBox = (ASPxComboBox)source;
            //SqlDataSource1.SelectCommand = @"SELECT ID, LastName, [Phone], FirstName FROM Persons WHERE (ID = @ID) ORDER BY FirstName";

            //SqlDataSource1.SelectParameters.Clear();
            //SqlDataSource1.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            //comboBox.DataSource = SqlDataSource1;
            //comboBox.DataBind();
        }
        protected void gvDistanciaOrtodromica_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDistanciaOrtodromica_CommandButtonInitialize", "Aviso");
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
                gvDistanciaOrtodromica.DataSource = null;
                ViewState["oDatos"] = null;

                gvDistanciaOrtodromica.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvDistanciaOrtodromica.DataBind();
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
                gvDistanciaOrtodromica.JSProperties["cpText"] = sMensaje;
                gvDistanciaOrtodromica.JSProperties["cpShowPopup"] = true;
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
                gvDistanciaOrtodromica.CancelEdit();
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

        public void LoadCatalogoAeropuerto(DataTable dtObjCat)
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
                                    textTextoBusqueda.Enabled = true;
                                    ddlTipoBusqueda.Enabled = true;
                                    btnExcel.Enabled = true;
                                    ASPxButton2.Enabled = true;
                                }
                                else
                                {
                                    btnBusqueda.Enabled = false;
                                    textTextoBusqueda.Enabled = false;
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

        private const string sClase = "frmDistanciaOrtodromica.aspx.cs";
        private const string sPagina = "frmDistanciaOrtodromica.aspx";

        DistanciaOrtodromica_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetAeropuerto;
        public event EventHandler eGetAeropuertoFiltro;
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
                int iIdDistanciaOrtodromica = 0;
                int iEstatus = -1;
                string sDescripcion = string.Empty;

                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "1":
                        iIdDistanciaOrtodromica = textTextoBusqueda.Text.S().I();
                        iEstatus = -1;
                        sDescripcion = string.Empty;
                        break;
                    case "2":
                        iIdDistanciaOrtodromica = 0;
                        iEstatus = -1;
                        sDescripcion = textTextoBusqueda.Text.S();
                        break;
                    case "3":
                        iIdDistanciaOrtodromica = 0;
                        iEstatus = 1;
                        sDescripcion = string.Empty;
                        break;
                    case "4":
                        iEstatus = 0;
                        sDescripcion = string.Empty;
                        break;

                }

                return new object[]{
                                        "@IdDestino", 0,
                                        "@IdOrigen", 0,
                                        "@estatus", iEstatus
                                    };
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

        #endregion

        


     




        
    }
}