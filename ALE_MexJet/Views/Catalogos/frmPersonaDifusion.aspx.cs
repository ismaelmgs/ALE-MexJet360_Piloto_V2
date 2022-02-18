using ALE_MexJet.Clases;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;

namespace ALE_MexJet.Views.Catalogos
{
    public partial class frmPersonaDifusion : System.Web.UI.Page, IViewPersonaDifusion
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.PersonaDifusion);
                LoadActions(DrPermisos);
                gvPersona.Columns["Status"].Visible = true;
                oPresenter = new PersonaDifusion_Presenter(this, new DBPersonaDifusion());
                gvPersona.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvPersona.SettingsPager.ShowDisabledButtons = true;
                gvPersona.SettingsPager.ShowNumericButtons = true;
                gvPersona.SettingsPager.ShowSeparators = true;
                gvPersona.SettingsPager.Summary.Visible = true;
                gvPersona.SettingsPager.PageSizeItemSettings.Visible = true;
                gvPersona.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvPersona.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";


                if (!IsPostBack)
                {
                    if (!bActualiza)
                    {
                        ObtieneValores();
                    }

                    if (gvPersona.VisibleRowCount < 1)
                    {
                        //Aqui debe aparecer el modal para dar de alta una persona.
                        ppPersonaDifusion.ShowOnPageLoad = true;
                        ppPersonaDifusion.HeaderText = "Formulario de Creación";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(ViewState["oDatos"].S()))
                    {
                        CargarGridPersona((DataTable)ViewState["oDatos"]);
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
                if (eSearchObj != null)
                    eSearchObj(sender, e);
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
                LimpiaCampos();

                ppPersonaDifusion.HeaderText = "Formulario de Creación";
                ppPersonaDifusion.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {

                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
            }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {

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
        protected void gvPersona_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName == "Edita")
                {

                    ASPxGridView grid = (ASPxGridView)sender;
                    iIdPersonaDifusion = (int)e.KeyValue;

                    if (eObtieneDatosPersona != null)
                        eObtieneDatosPersona(iIdPersonaDifusion, EventArgs.Empty);

                    if (eObtienePersonaListaDifusion != null)
                        eObtienePersonaListaDifusion(iIdPersonaDifusion, EventArgs.Empty);

                    bActualiza = true;

                    ppPersonaDifusion.HeaderText = "Formulario de Edición";
                    ppPersonaDifusion.ShowOnPageLoad = true;

                }
                else if (e.CommandArgs.CommandName == "Elimina")
                {
                    iIdPersonaDifusion = e.CommandArgs.CommandArgument.S().I();

                    iIdListaDifusion = -1;

                    if (eDeleteObj != null)
                        eDeleteObj(iIdPersonaDifusion, null);

                    //if (eDeletePersonaListaDifusion != null)
                    //    eDeletePersonaListaDifusion(sender, e);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPersona_RowCommand", "Aviso");
            }
        }
        protected void gvListaDifusion_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {

        }
        protected void btnGuardarPersona_Click(object sender, EventArgs e)
        {
            try
            {
                if (!bActualiza)
                {
                    if (eNewObj != null)
                        eNewObj(sender, e);
                }
                else
                {
                    if (eUpdateObj != null)
                        eUpdateObj(sender, e);
                }


                foreach (ListEditItem item in lbxLista.Items)
                {
                    iIdListaDifusion = item.Value.I();

                    if (item.Selected)
                    {

                        if (eSavePersonaListaDifusion != null)
                            eSavePersonaListaDifusion(sender, e);
                    }
                    else
                    {
                        if (eDeletePersonaListaDifusion != null)
                            eDeletePersonaListaDifusion(sender, e);
                    }
                }

                LimpiaCampos();

                ObtieneValores();

                ppPersonaDifusion.ShowOnPageLoad = false;
                ppPersonaDifusion.HeaderText = string.Empty;

                MostrarMensaje("El registro se guardo correctamente.", "Mensaje");

                bActualiza = false;

            }
            catch (Exception ex)
            {

                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardarPersona_Click", "Aviso");
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                ppPersonaDifusion.ShowOnPageLoad = false;
                LimpiaCampos();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region METODOS
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
        public void ObtieneValores()
        {
            try
            {

                if (eSearchObj != null)
                    eSearchObj(null, EventArgs.Empty);

                if (eObtieneTitulo != null)
                    eObtieneTitulo(null, EventArgs.Empty);

                if (eObtieneTipoPersona != null)
                    eObtieneTipoPersona(null, EventArgs.Empty);

                if (eObtieneTipoContacto != null)
                    eObtieneTipoContacto(null, EventArgs.Empty);

                if (eObtienePersonaListaDifusion != null)
                    eObtienePersonaListaDifusion(-1, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarGridPersona(DataTable dtResultado)
        {
            try
            {
                gvPersona.DataSource = null;
                ViewState["oDatos"] = null;

                gvPersona.DataSource = dtResultado;
                ViewState["oDatos"] = dtResultado;

                gvPersona.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargaTitulo(DataTable dtResultado)
        {
            try
            {
                cbxTitulo.DataSource = dtResultado;
                cbxTitulo.TextField = "TituloDescripcion";
                cbxTitulo.ValueField = "IdTitulo";
                cbxTitulo.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CargaTipoPersona(DataTable dtResultado)
        {
            try
            {
                cbxTipoPersona.DataSource = dtResultado;
                cbxTipoPersona.TextField = "Descripcion";
                cbxTipoPersona.ValueField = "IdTipoPersona";
                cbxTipoPersona.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CargaTipoContacto(DataTable dtResultado)
        {
            try
            {
                dtResultado.Rows.Add(-2, "Ninguno", "1", "", "1900-01-01 00:00:00.000", "", "1900-01-01 00:00:00.000", "");

                cbxTipoContacto.DataSource = dtResultado;
                cbxTipoContacto.TextField = "TipoContactoDescripcion";
                cbxTipoContacto.ValueField = "IdTipoContacto";

                cbxTipoContacto.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CargaPersonaListaDifusion(DataTable dtResultado)
        {
            try
            {
                //gvListaDifusion.DataSource = null;
                //gvListaDifusion.DataSource = dtResultado;
                //gvListaDifusion.DataBind();

                //ASPxListBox lbxLista = (ASPxListBox)(cbxLista.FindControl("lbxLista"));

                lbxLista.DataSource = null;
                lbxLista.DataSource = dtResultado;
                lbxLista.DataBind();

                int contador = 0;

                foreach (DataRow Fila in dtResultado.Rows)
                {
                    if (Fila["Existe"].I() == 1)
                    {
                        lbxLista.Items[contador].Selected = true;
                    }
                    contador++;
                }

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
                lbl.Text = sMensaje;
                popup.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LimpiaCampos()
        {
            try
            {
                bActualiza = false;

                iIdPersonaDifusion = -1;
                iIdListaDifusion = -1;

                cbxTitulo.SelectedIndex = -1;
                txtNombre.Text = string.Empty;
                txtApellidoPaterno.Text = string.Empty;
                txtApellidoMaterno.Text = string.Empty;
                cbxTipoPersona.SelectedIndex = -1;
                cbxTipoContacto.SelectedIndex = -1;
                cbxEstatus.SelectedIndex = -1;
                txtTelefonoMovil.Text = string.Empty;
                txtEmail.Text = string.Empty;
                chkbxRecibeSMS.Checked = false;
                chkbxRecibeMail.Checked = false;
                chkbxRecibePublicidad.Checked = false;

                foreach (ListEditItem item in lbxLista.Items)
                {
                    item.Selected = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region VARIBALES


        private const string sClase = "frmPersonaDifusion.aspx.cs";
        private const string sPagina = "frmPersonaDifusion.aspx";

        PersonaDifusion_Presenter oPresenter;

        public int iIdPersonaDifusion
        {
            get { return Session["iIdPersonaDifusion"].S().I(); }
            set { Session["iIdPersonaDifusion"] = value; }
        }
        public int iIdListaDifusion
        {
            get { return Session["iIdListaDifusion"].S().I(); }
            set { Session["iIdListaDifusion"] = value; }
        }
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
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
                                        "@Nombre", "%" + sDescripcion + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }
        public object[] oArrFiltrosPersonaListaDifusion
        {
            get
            {

                return new object[]{
                                        "@IdPersonaDifusion", iIdPersonaDifusion,
                                        "@IdListaDifusion", iIdListaDifusion
                                    };
            }
        }
        public bool bActualiza
        {
            get { return ViewState["VsbActualizacion"].S().B(); }
            set { ViewState["VsbActualizacion"] = value; }
        }

        public PersonaDifusion oPersona
        {
            get
            {
                PersonaDifusion oPersonaDifusion = new PersonaDifusion();

                if (!bActualiza)
                {
                    iIdPersonaDifusion = -1;
                }

                oPersonaDifusion.iId = iIdPersonaDifusion;
                oPersonaDifusion.sNombre = txtNombre.Text;
                oPersonaDifusion.sApellidoPaterno = txtApellidoPaterno.Text;
                oPersonaDifusion.sApellidoMaterno = txtApellidoMaterno.Text;
                oPersonaDifusion.iIdTitulo = cbxTitulo.SelectedItem.Value.S().I();
                oPersonaDifusion.iIdTipoPersona = cbxTipoPersona.SelectedItem.Value.S().I();
                oPersonaDifusion.iIdTipoContacto = cbxTipoContacto.SelectedItem.Value.S().I();
                oPersonaDifusion.sTelefonoMovil = txtTelefonoMovil.Text;
                oPersonaDifusion.sCorreoElectronico = txtEmail.Text;
                oPersonaDifusion.iLLamadas = chkxRecibeLlamada.Checked == true ? 1 : 0;
                oPersonaDifusion.iSMS = chkbxRecibeSMS.Checked == true ? 1 : 0;
                oPersonaDifusion.iCorreos = chkbxRecibeMail.Checked == true ? 1 : 0;
                oPersonaDifusion.iPublicidad = chkbxRecibePublicidad.Checked == true ? 1 : 0;
                oPersonaDifusion.iStatus = cbxEstatus.SelectedItem.Value.S().I();

                return oPersonaDifusion;
            }
            set
            {
                PersonaDifusion oPersonaDifusion = value;

                iIdPersonaDifusion = oPersonaDifusion.iId;
                txtNombre.Text = oPersonaDifusion.sNombre;
                txtApellidoPaterno.Text = oPersonaDifusion.sApellidoPaterno;
                txtApellidoMaterno.Text = oPersonaDifusion.sApellidoMaterno;
                cbxTitulo.SelectedIndex = cbxTitulo.Items.IndexOf(cbxTitulo.Items.FindByValue(oPersonaDifusion.iIdTitulo.S()));
                cbxTipoPersona.SelectedIndex = cbxTipoPersona.Items.IndexOf(cbxTipoPersona.Items.FindByValue(oPersonaDifusion.iIdTipoPersona.S()));
                cbxTipoContacto.SelectedIndex = cbxTipoContacto.Items.IndexOf(cbxTipoContacto.Items.FindByValue(oPersonaDifusion.iIdTipoContacto.S()));
                txtTelefonoMovil.Text = oPersonaDifusion.sTelefonoMovil;
                txtEmail.Text = oPersonaDifusion.sCorreoElectronico;
                chkxRecibeLlamada.Checked = oPersonaDifusion.iLLamadas == 1 ? true : false;
                chkbxRecibeSMS.Checked = oPersonaDifusion.iSMS == 1 ? true : false;
                chkbxRecibeMail.Checked = oPersonaDifusion.iCorreos == 1 ? true : false;
                chkbxRecibePublicidad.Checked = oPersonaDifusion.iPublicidad == 1 ? true : false;
                cbxEstatus.SelectedIndex = cbxEstatus.Items.IndexOf(cbxEstatus.Items.FindByValue(oPersonaDifusion.iStatus.S()));
            }
        }

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eUpdateObj;
        public event EventHandler eDeletePersonaListaDifusion;
        public event EventHandler eObtieneTitulo;
        public event EventHandler eObtieneTipoPersona;
        public event EventHandler eObtieneTipoContacto;
        public event EventHandler eObtienePersonaListaDifusion;
        public event EventHandler eObtieneDatosPersona;
        public event EventHandler eSavePersonaListaDifusion;

        UserIdentity oUsuario = new UserIdentity();

        #endregion



    }
}