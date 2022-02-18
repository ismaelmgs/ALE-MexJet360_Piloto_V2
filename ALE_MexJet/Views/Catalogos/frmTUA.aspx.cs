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
using ALE_MexJet.Clases;
using DevExpress.Utils;
using System.Reflection;

namespace ALE_MexJet.Views.Catalogos
{
    public partial class frmTUA : System.Web.UI.Page, IViewTUA
    {
        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.TUA);
                LoadActions(DrPermisos);
                gvTUA.Columns["Status"].Visible = true;
                oPresenter = new TUA_Presenter(this, new DBTUA());
                gvTUA.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvTUA.SettingsPager.ShowDisabledButtons = true;
                gvTUA.SettingsPager.ShowNumericButtons = true;
                gvTUA.SettingsPager.ShowSeparators = true;
                gvTUA.SettingsPager.Summary.Visible = true;
                gvTUA.SettingsPager.PageSizeItemSettings.Visible = true;
                gvTUA.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvTUA.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                ObtieneValores();
                if (!IsPostBack)
                {
                    if (gvTUA.VisibleRowCount < 1)
                    {
                        gvTUA.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                        gvTUA.Columns["Status"].Visible = false;
                        gvTUA.AddNewRow();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }

        }

        protected void gvTUA_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvTUA.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTUA_CancelRowEditing", "Aviso");
            }
        }

        protected void gvTUA_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;

                if (e.Column.FieldName == "IdMes")
                {
                    if (eSearchObjMes != null)
                        eSearchObjMes(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["oDatosMes"];
                    cmb.ValueField = "IdMes";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "Nombre";
                    cmb.DataBindItems();
                }
                //if (e.Column.FieldName == "IdAeropuerto")
                //{
                //    eCrud = Enumeraciones.TipoOperacion.Insertar;
                //    if (eGetAeropuerto != null)
                //        eGetAeropuerto(this, e);
                //    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                //    cmb.DataSource = (DataTable)ViewState["oDatosAereo"];
                //    cmb.ValueField = "idAeropuert";
                //    cmb.ValueType = typeof(Int32);
                //    cmb.TextField = "AeropuertoIATA";
                //    cmb.DataBindItems();

                //}

                ASPxGridView gridView = (ASPxGridView)sender;
                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    //gvTUA.Columns["Status"].Visible = false;
                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTUA_CellEditorInitialize", "Aviso");
            }
        }

        protected void gvTUA_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTUA_RowDeleting", "Aviso");
            }
        }

        protected void gvTUA_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;


                if (eNewObj != null)
                    eNewObj(sender, e);

                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTUA_RowInserting", "Aviso");
            }

        }

        protected void gvTUA_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTUA_RowUpdating", "Aviso");
            }

        }

        protected void gvTUA_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvTUA.SettingsText.PopupEditFormCaption = "Formulario de Edición";
                gvTUA.SettingsPopup.EditForm.HorizontalAlign = PopupHorizontalAlign.Center;
                gvTUA.SettingsPopup.EditForm.VerticalAlign = PopupVerticalAlign.Middle;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTUA_StartRowEditing", "Aviso");
            }

        }

        protected void gvTUA_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;

                if (eObjSelected != null)
                    eObjSelected(sender, e);

                if (bDuplicado)
                {
                    AddError(e.Errors, gvTUA.Columns["IdAeropuerto"], "Este TUA ya existe, favor de validarlo.");
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
                gvTUA.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvTUA.Columns["Status"].Visible = false;
                gvTUA.AddNewRow();
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
        protected void gvTUA_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTUA_CommandButtonInitialize", "Aviso");
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
                gvTUA.DataSource = null;
                gvTUA.DataSource = dtObject;
                gvTUA.DataBind();
                ViewState["oDatos"] = dtObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadObjectsAereo(DataTable dtObject)
        {
            try
            {
                ViewState["oDatosAereo"] = dtObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadObjectsMes(DataTable dtObject)
        {
            try
            {
                ViewState["oDatosMes"] = dtObject;
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
                gvTUA.JSProperties["cpCaption"] = sCaption;
                gvTUA.JSProperties["cpText"] = sMensaje;
                gvTUA.JSProperties["cpShowPopup"] = true;
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
                gvTUA.CancelEdit();
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
                    textTextoBusqueda.Enabled = false;
                    ddTipoBusqueda.Enabled = false;
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
                                    ddTipoBusqueda.Enabled = true;
                                    btnExcel.Enabled = true;
                                    ASPxButton2.Enabled = true;
                                }
                                else
                                {
                                    btnBusqueda.Enabled = false;
                                    textTextoBusqueda.Enabled = false;
                                    ddTipoBusqueda.Enabled = false;
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

        private const string sPagina = "frmTUA.aspx";
        private const string sClase = "frmTUA.aspx.cs";

        TUA_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetAeropuerto;
        public event EventHandler eSearchObjMes;
        UserIdentity oUsuario = new UserIdentity();
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }

        public object oCrud
        {
            get { return ViewState["CrudTUA"]; }
            set { ViewState["CrudTUA"] = value; }
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

                int iIdAeropuerto = 0;
                string sMes = string.Empty;
                string sAeropuertoIATA = string.Empty;
                int iAnio = 0;
                decimal dNacional = 0.0M;
                decimal dInternacional = 0.0M;
                int iEstatus = -1;

                switch (ddTipoBusqueda.SelectedValue.S())
                {
                    case "1":
                        sAeropuertoIATA = textTextoBusqueda.Text.S();
                        break;
                    case "2":
                        sMes = textTextoBusqueda.Text.S();
                        break;
                    case "3":
                        iAnio = textTextoBusqueda.Text.S().I();
                        break;
                    case "4":
                        dNacional = textTextoBusqueda.Text.S().D();
                        break;
                    case "5":
                        dInternacional = textTextoBusqueda.Text.S().D();
                        break;
                    case "6":
                        iEstatus = 1;
                        break;
                    case "7":
                        iEstatus = 0;
                        break;

                }

                return new object[]{ 
                                        "@IdAeropuerto",  iIdAeropuerto ,
                                        "@AeropuertoIATA", "%" + sAeropuertoIATA + "%",
                                        "@Mes", "%" + sMes + "%",
                                        "@Anio",  iAnio ,
                                        "@Nacional",  dNacional ,
                                        "@Internacional",  dInternacional ,
                                        "@estatus", iEstatus
                                    };
            }
        }
        public object[] oArrFiltrosAereo
        {
            get
            {
                string sIATA = string.Empty;
                string sICAO = string.Empty;
                string sDescripcion = string.Empty;
                int iEstatus = -1;

                return new object[]{ 
                                        "@IATA", "%" + sIATA + "%",
                                        "@ICAO", "%" + sICAO + "%",
                                        "@Descripcion", "%" + sDescripcion + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }
        public object[] oArrFiltrosMes
        {
            get
            {
                int iIdMes = 0;
                string sNombre = string.Empty;
                int iEstatus = 0;

                return new object[]{ 
                                        "@IdMes", iIdMes,
                                        "@Nombre", "%" + sNombre + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }



        #endregion
        public event EventHandler eGetAeropuertoFiltro;

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



    }
}