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

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmNotaCredito : System.Web.UI.Page, IViewNotaCredito
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                if (!IsPostBack)
                    btnAgregar.Visible = false;

                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.NotaCredito);
                LoadActions(DrPermisos);
                gvRemision.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                DataTable DT  = (DataTable)ViewState["dtObject"];
            if(DT != null && DT.Rows.Count > 0)
                CargaGrid(DT);
            
            oPresenter = new NotaCredito_Presenter(this, new DBNotaCredito());
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try 
            {
                DataTable dt = (DataTable)ViewState["dtObject"];
                if (dt == null || dt.Rows.Count == 0)
                    MostrarMensaje("Es necesario ingresar Folio de Remisión", "Atención");
                else
                {
                    ppAgregar.ShowOnPageLoad = true;
                    ppAgregar.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter;
                    ppAgregar.PopupVerticalAlign = PopupVerticalAlign.Below;
                }
            }
            catch (Exception ex) {Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregar_Click", "Aviso");}
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try 
            { 
                if (eSearchObj != null)
                    eSearchObj(null, EventArgs.Empty);
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscar_Click", "Aviso"); }
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            try 
            { 
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;

                if (eNewObj != null)
                    eNewObj(sender, e);

                rblTipoNotaCredito.Value = null;
                ppAgregar.ShowOnPageLoad = false;
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnguardar_Click", "Aviso"); }
        }
   
        #endregion

        #region METODOS

        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }
            catch (Exception x) { throw x; }
        }

        protected void UpdatePanel2_Unload(object sender, EventArgs e)
        {
            try 
            { 
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }
            catch (Exception x) { throw x; }
        }  

        protected void CargaGrid(DataTable DT)
        {
            try 
            {
                gvRemision.DataSource = DT;
                gvRemision.DataBind();
                gvRemision.Visible = true;
                    
             if (DT != null && DT.Rows.Count > 0)
                btnAgregar.Visible = true;
             else
                btnAgregar.Visible = false;
            }
            catch (Exception x) { throw x; }
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                lbl.Text = sMensaje.S();
                ppAlert.ShowOnPageLoad = true;
                //mpeMensaje.ShowMessage(sMensaje, sCaption);
            }
            catch (Exception x) { throw x; }
        }

        public void LoadObjects(DataTable dtObject)
        {
            try 
            { 
                ViewState["dtObject"] = dtObject;
                CargaGrid(dtObject);
            }
            catch (Exception x) { throw x; }
        }
        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                txtBuscaFolio.Enabled = false;
                btnBuscar.Enabled = false;
            }
            else
            {
                for (iPos = 0; iPos < DrActions[0].ItemArray.Length; iPos++)
                {
                    switch (iPos)
                    {
                        case 4: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                txtBuscaFolio.Enabled = true;
                                btnBuscar.Enabled = true;

                            }
                            else
                            {
                                txtBuscaFolio.Enabled = false;
                                btnBuscar.Enabled = false;

                            } break;
                        case 5: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                btnAgregar.Enabled = true;
                                //btnNuevo2.Enabled = true;
                            }
                            else
                            {
                                btnAgregar.Enabled = false;
                                //btnNuevo2.Enabled = false;
                            } break;
                    }
                }
            }

        }
        #endregion

        #region "Vars y Propiedades"

        private const string sClase = "frmNotaCredito.aspx.cs";
        private const string sPagina = "frmNotaCredito.aspx";

        NotaCredito_Presenter oPresenter;
        
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        event EventHandler _eSearchObj;
        public event EventHandler eSearchConsulta;
        UserIdentity oUsuario = new UserIdentity();
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }
        public object[] oArrFiltros
        {
            get
            {
                return new object[]
                { 
                    "@IdRemision", txtBuscaFolio.Text.S().I()
                };
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

        public NotaCredito oCatalogo
        {
            get
            {
                NotaCredito NT = new NotaCredito();
                DataTable dt = (DataTable)ViewState["dtObject"];
                NT.iIdFolioRemision = dt != null && dt.Rows.Count > 0 ? dt.Rows[0]["IdRemision"].S().I() : 0;
                NT.iTipoNotaCredito = rblTipoNotaCredito.Value == null || rblTipoNotaCredito.Value == null ? "Seleccione" : rblTipoNotaCredito.SelectedItem.Text.S();
                NT.iTiempo = txtTiempo.Text.S();
                NT.iCantidad = spCantidad.Text.S().D();
                return NT;
            }
        }

        public NotaCredito oBusqueda 
        {
            get
            {
                NotaCredito NB = new NotaCredito();
                return NB;
            }
        }
        #endregion

    }
}