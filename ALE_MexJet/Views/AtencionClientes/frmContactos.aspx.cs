using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ALE_MexJet.Clases;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using ALE_MexJet.ControlesUsuario;
using System.Data;
using NucleoBase.Core;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using DevExpress.Web.Data;
using System.ComponentModel;
using NucleoBase.Seguridad;
using System.Text;
using System.Reflection;
using DevExpress.Utils;

namespace ALE_MexJet.Views.Catalogos
{
    public partial class frmContactos : System.Web.UI.Page, IViewContacto
    {
        #region Eventos
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["IdCliente"] = Encoding.UTF8.GetString(Convert.FromBase64String(Request.QueryString["IdCliente"]));
                Session["Cliente"] = Encoding.UTF8.GetString(Convert.FromBase64String(Request.QueryString["Cliente"]));
                oPresenter = new Contacto_Presenter(this, new DBContacto());
                gvContactos.SettingsPager.ShowDisabledButtons = true;
                gvContactos.SettingsPager.ShowNumericButtons = true;
                gvContactos.SettingsPager.ShowSeparators = true;
                gvContactos.SettingsPager.Summary.Visible = true;
                gvContactos.SettingsPager.PageSizeItemSettings.Visible = true;
                gvContactos.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvContactos.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                Llenagrid();

                if (!IsPostBack)
                    ObtieneValores();

                if (!IsPostBack)
                {
                    new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.ContactosPreferencias), Convert.ToInt32(Enumeraciones.TipoAccion.Acceso), "Acceso al modulo " + Enumeraciones.Pantallas.ContactosPreferencias.S());
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
         
        }

        protected void gvContactos_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                if (e.Column.FieldName == "IdTitulo")
                {
                    if (eGetTipoTitulo != null)
                        eGetTipoTitulo(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["TipoCliente"];
                    cmb.ValueField = "IdTitulo";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "TituloDescripcion";
                    cmb.DataBindItems();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvContactos_CellEditorInitialize", "Aviso");
            }

        }

        protected void gvContactos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;

                if (eDeleteObj != null)
                    eDeleteObj(sender, e);
                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvContactos_RowDeleting", "Aviso");
            }

        }

        protected void gvContactos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvContactos_RowInserting", "Aviso");
            }
        }

        protected void gvContactos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvContactos_RowUpdating", "Aviso");
            }
        }

        protected void gvContactos_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {

        }

        protected void gvContactos_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e.NewValues["Nombre"].S()))
                {
                    AddError(e.Errors, gvContactos.Columns["Nombre"], "El nombre es necesario.");
                }

                if (string.IsNullOrEmpty(e.NewValues["CorreoElectronico"].S()))
                {
                    AddError(e.Errors, gvContactos.Columns["CorreoElectronico"], "El correo electronico es necesario.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvContactos_RowValidating", "Aviso");
            }

        }

        protected void gvContactos_CellEditorInitialize1(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                gvContactos.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvContactos.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {

            new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.ContactosPreferencias), Convert.ToInt32(Enumeraciones.TipoAccion.Exportar), "Exportó los contactos de: " + Enumeraciones.Pantallas.ContactosPreferencias.S());
            Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware }); 
            
        }
        protected void btnGuardarComentarios_Click(object sender, EventArgs e)
        {
            try
            {
                string s = txtOtros.Text.S();
                if (eUpdateObservacion != null)
                    eUpdateObservacion(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardarComentarios_Click", "Aviso");
            }
        }
        
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl.Replace(Request.RawUrl, "~/Views/Catalogos/frmCliente.aspx"));
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnRegresar_Click", "Aviso");
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

        protected void Llenagrid()
        {
            try
            {
                if (Session["gvContactos"] != null)
                {
                    gvContactos.DataSource = null;
                    gvContactos.DataBind();
                    DataTable ds = (DataTable)Session["gvContactos"];
                    gvContactos.DataSource = ds;
                    gvContactos.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadObjects(DataSet dsObject)
        {
            try
            {
                gvContactos.DataSource = null;
                gvContactos.DataSource = dsObject.Tables[0];
                gvContactos.DataBind();
                ViewState["oDatos"] = dsObject;
                Session["gvContactos"] = dsObject.Tables[0];

                Cliente oCL = new Cliente();
                oCL.iId = dsObject.Tables[1].Rows[0][0].S().I();
                oCL.sObservaciones = dsObject.Tables[1].Rows[0][1].S();
                oCL.sNotas = dsObject.Tables[1].Rows[0][2].S();
                oCL.sOtros = dsObject.Tables[1].Rows[0][3].S();


                oCliente = oCL;
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
                gvContactos.CancelEdit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MuestraMensg(string sMensaje, string sCaption)
        {
            try
            {
                popup.HeaderText = sCaption;
                gvContactos.JSProperties["cpText"] = sMensaje;
                gvContactos.JSProperties["cpShowPopup"] = true;
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
                mpeMensaje.ShowMessage(sMensaje, sCaption);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void LoadCatalogoTipocliente(DataTable dtObjCat)
        {
            try
            {
                ViewState["TipoCliente"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateObservacion(int iObject)
        {
 
        }
        private void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            try
            {
                if (column != null)
                {
                    if (errors.ContainsKey(column)) return;
                    errors[column] = errorText;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Variables y Propiedades
        private const string sClase = "frmContactos .aspx.cs";
        private const string sPagina = "frmContactos .aspx";

        Contacto_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetTipoTitulo;
        public event EventHandler eUpdateObservacion;
        

        public object oCrud
        {
            get { return ViewState["CrudContacto"]; }
            set { ViewState["CrudContacto"] = value; }
        }
        public int iIdCliente { get { return Session["IdCliente"].S().I(); } }

        public ALE_MexJet.Objetos.Enumeraciones.TipoOperacion eCrud
        {
            get { return (ALE_MexJet.Objetos.Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }
        public object[] oArrFiltros
        {
            get { throw new NotImplementedException(); }
        }

        public Cliente oCliente
        {
            get
            {
                Cliente oCl = new Cliente();
                oCl.iId = Session["IdCliente"].S().I();
                oCl.sObservaciones = txtObservacion.Text;
                oCl.sNotas = txtNota.Text.S();
                oCl.sOtros = txtOtros.Text.S();
                return oCl;
            }
            set {
                Cliente oCl = value;
                txtObservacion.Text = oCl.sObservaciones;
                txtNota.Text = oCl.sNotas;
                txtOtros.Text = oCl.sOtros;
                lblCliente.Text = "Nombre del Cliente: " +Session["Cliente"].S();
            }
        }

        #endregion

            }
}