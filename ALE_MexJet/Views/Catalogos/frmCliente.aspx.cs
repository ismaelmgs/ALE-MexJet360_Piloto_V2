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
using System.Text;


namespace ALE_MexJet.Views.Catalogos
{
    public partial class frmCliente : System.Web.UI.Page, IViewCliente
    {
        #region EVENTOS
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.Cliente);
                LoadActions(DrPermisos);
                gvCliente.Columns["Status"].Visible = true;
                oPresenter = new Cliente_Presenter(this, new DBCliente());
                gvCliente.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvCliente.SettingsPager.ShowDisabledButtons = true;
                gvCliente.SettingsPager.ShowNumericButtons = true;
                gvCliente.SettingsPager.ShowSeparators = true;
                gvCliente.SettingsPager.Summary.Visible = true;
                gvCliente.SettingsPager.PageSizeItemSettings.Visible = true;
                gvCliente.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvCliente.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";


                ObtieneValores();
                if (IsPostBack)
                {
                    if (!string.IsNullOrEmpty(ViewState["oDatos"].S()))
                    {
                        LoadObjects((DataTable)ViewState["oDatos"]);
                    }
                }

                if(!IsPostBack)
                {
                   new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Cliente), Convert.ToInt32(Enumeraciones.TipoAccion.Acceso), "Acceso al modulo " + Enumeraciones.Pantallas.Cliente.S());
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }

        }

        protected void gvClientes_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvClientes_RowDeleting", "Aviso");
            }
        }

        protected void gvCliente_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvCliente.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCliente_CancelRowEditing", "Aviso");
            }
        }

        protected void gvClientes_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvClientes_RowUpdating", "Aviso");
            }
        }

        protected void gvClientes_RowInserting(object sender, ASPxDataInsertingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvClientes_RowInserting", "Aviso");
            }
        }

        protected void gvClientes_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                if (e.Column.FieldName == "TipoCliente")
                {
                    if (eGetTipCliente != null)
                        eGetTipCliente(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["TipoCliente"];
                    cmb.ValueField = "IdTipoCliente";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "TipoClienteDescripcion";
                    cmb.DataBindItems();
                }

                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    //gvCliente.Columns["Status"].Visible = false;

                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvClientes_CellEditorInitialize", "Aviso");
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Cliente), Convert.ToInt32(Enumeraciones.TipoAccion.Exportar), "Exportó el listado de " + Enumeraciones.Pantallas.Cliente.S());
            Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
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
                gvCliente.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvCliente.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
            }
        }
        protected void gvCliente_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCliente_RowDeleting", "Aviso");
            }
        }
        protected void gvCliente_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvCliente.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCliente_StartRowEditing", "Aviso");
            }
        }
        protected void gvCliente_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCliente_RowUpdating", "Aviso");
            }

        }

        protected void gvCliente_RowInserting(object sender, ASPxDataInsertingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCliente_RowInserting", "Aviso");
            }
            

        }
        protected void gvClientes_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;

                if (eObjSelected != null)
                    eObjSelected(sender, e);

                if (bDuplicado)
                {
                    AddError(e.Errors, gvCliente.Columns["CodigoCliente"], "El Cliente ya existe, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvClientes_RowValidating", "Aviso");
            }

        }
        protected void gvCliente_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                object oIdCliente = gvCliente.GetRowValues(e.VisibleIndex, "IdCliente");
                object oNombre = gvCliente.GetRowValues(e.VisibleIndex, "Nombre");

                string sIdCliente = oIdCliente.ToString();
                string sNombreCliente = oNombre.ToString();

                sIdCliente = Convert.ToBase64String(Encoding.UTF8.GetBytes(sIdCliente));
                sNombreCliente = Convert.ToBase64String(Encoding.UTF8.GetBytes(sNombreCliente));

                string ruta = "~/Views/AtencionClientes/frmContactos.aspx?idcliente=" + sIdCliente + "&Cliente=" + sNombreCliente;
                ASPxWebControl.RedirectOnCallback(ruta);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCliente_CustomButtonCallback", "Aviso");
            }
        }
        protected void gvCliente_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCliente_CommandButtonInitialize", "Aviso");
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
        public void LoadObjects(DataTable dtObject)
        {
            try
            {
                gvCliente.DataSource = null;
                gvCliente.DataSource = dtObject;
                gvCliente.DataBind();
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
                gvCliente.JSProperties["cpText"] = sMensaje;
                gvCliente.JSProperties["cpShowPopup"] = true;
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
                gvCliente.CancelEdit();
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
        private const string sClase = "frmCliente.aspx.cs";
        private const string sPagina = "frmCliente.aspx";

        Cliente_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetTipCliente;
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
                string sCodigoCliente = string.Empty;
                string sTipoCliente = string.Empty;
                string sNombre = string.Empty;
                int iEstatus = -1;

                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "1":
                        sCodigoCliente = txtTextoBusqueda.Text.S();
                        iEstatus = -1;
                        sTipoCliente = string.Empty;
                        sNombre = string.Empty;
                        break;
                    case "2":
                        sCodigoCliente = string.Empty;
                        iEstatus = -1;
                        sTipoCliente = string.Empty;
                        sNombre = txtTextoBusqueda.Text.S();
                        break;
                    case "3":
                        sCodigoCliente = string.Empty;
                        iEstatus = -1;
                        sTipoCliente = txtTextoBusqueda.Text.S();
                        sNombre = string.Empty;
                        break;
                    case "4":
                        sCodigoCliente = string.Empty;
                        iEstatus = 1;
                        sTipoCliente = string.Empty;
                        sNombre = string.Empty;
                        break;
                    case "5":
                        sCodigoCliente = string.Empty;
                        iEstatus = 0;
                        sTipoCliente = string.Empty;
                        sNombre = string.Empty;
                        break;
                }

                return new object[]{
                                        "@CodigoCliente", "%" + sCodigoCliente + "%",
                                        "@Nombre", "%" + sNombre + "%",
                                        "@TipoCliente", "%" + sTipoCliente + "%",
                                        "@estatus", iEstatus
                                    };
            }

        }

        #endregion

        
    }
}