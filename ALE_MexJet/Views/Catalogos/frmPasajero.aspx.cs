using ALE_MexJet.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Data;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Objetos;
using DevExpress.Web;

namespace ALE_MexJet.Views.Catalogos
{
    public partial class frmPasajero : System.Web.UI.Page, IViewPasajero
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new Pasajero_Presenter(this, new DBPasajero());

                //gvPasajero.Columns["Status"].Visible = true;
                gvPasajero.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvPasajero.SettingsPager.ShowDisabledButtons = true;
                gvPasajero.SettingsPager.ShowNumericButtons = true;
                gvPasajero.SettingsPager.ShowSeparators = true;
                gvPasajero.SettingsPager.Summary.Visible = true;
                gvPasajero.SettingsPager.PageSizeItemSettings.Visible = true;
                gvPasajero.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;

                if (IsPostBack)
                {
                    if (!string.IsNullOrEmpty(ViewState["VSdtPasajero"].S()))
                    {
                        ConsultaPasajeros((DataTable)ViewState["VSdtPasajero"]);
                    }
                }
                else
                {

                    CargaPasajeros();

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
                CargaPasajeros();
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
                LimpiarCampos();

                bActualiza = false;

                cbxCodigoCliente.Visible = true;

                lblClaveCliente2.Visible = false;

                txtNombrePasajero.Visible = true;
                txtApellidosPasajero.Visible = true;

                lblNombrePasajero2.Visible = false;
                lblApellidosPasajero2.Visible = false;

                if (eConsultaCliente != null)
                    eConsultaCliente(null, null);

                ModalPerfilPasajero.ShowOnPageLoad = true;

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
            }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {

        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {

        }
        protected void gvPasajero_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName == "Edita")
                {
                    LimpiarCampos();

                    ASPxGridView grid = (ASPxGridView)sender;
                    object id = e.KeyValue;

                    iIdPasajero = grid.GetRowValuesByKeyValue(id, "IdPasajero").S().I();

                    bActualiza = true;

                    txtNombrePasajero.Visible = false;
                    txtApellidosPasajero.Visible = false;

                    cbxCodigoCliente.Visible = false;

                    lblNombrePasajero2.Visible = true;
                    lblApellidosPasajero2.Visible = true;
                    lblClaveCliente2.Visible = true;

                    ConsultaPasajeroId();

                    ModalPerfilPasajero.ShowOnPageLoad = true;

                }
                else if (e.CommandArgs.CommandName == "Elimina")
                {
                    ASPxGridView grid = (ASPxGridView)sender;
                    object id = e.KeyValue;

                    iIdPasajero = grid.GetRowValuesByKeyValue(id, "IdPasajero").S().I();

                    EliminaPasajeroporId();
                }
            }
            catch (Exception ex)
            {

                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPasajero_RowCommand", "Aviso");
            }
        }
        protected void btnGuardarPerfilPasajero_Click(object sender, EventArgs e)
        {
            try
            {
                GuardaPasajeroPerfil();
            }
            catch (Exception ex)
            {

                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardarPerfilPasajero_Click", "Aviso");
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCampos();
            }
            catch (Exception ex)
            {

                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnCancelar_Click", "Aviso");
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

        private void CargaPasajeros()
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, null);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void GuardaPasajeroPerfil()
        {
            try
            {
                if (!bActualiza)
                {
                    if (eGuardaPasajeroPerfil != null)
                        eGuardaPasajeroPerfil(null, null);

                    popup.ShowOnPageLoad = true;
                    lbl.Text = "El registro se guardo correctamente.";

                    LimpiarCampos();

                }
                else
                {
                    if (eActualizaPasajeroPerfil != null)
                        eActualizaPasajeroPerfil(iIdPasajero, null);

                    popup.ShowOnPageLoad = true;
                    lbl.Text = "El registro se guardo correctamente.";

                    LimpiarCampos();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EliminaPasajeroporId()
        {
            try
            {
                if (eEliminaPasajeroPorId != null)
                    eEliminaPasajeroPorId(iIdPasajero, null);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ConsultaPasajeros(DataTable dtResultado)
        {
            try
            {
                gvPasajero.DataSource = null;
                gvPasajero.DataSource = dtResultado;
                gvPasajero.DataBind();
                ViewState["VSdtPasajero"] = dtResultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ConsultaPasajeroId()
        {
            try
            {
                if (eConsultaPasajeroId != null)
                    eConsultaPasajeroId(null, null);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ConsultaPasajeroId(DataTable dtResultado)
        {
            try
            {
                foreach (DataRow Fila in dtResultado.Rows)
                {
                    iIdPasajero = Fila["IdPasajero"].S().I();

                    lblClaveCliente2.Text = Fila["CodigoCliente"].S();
                    lblApellidosPasajero2.Text = Fila["last_name"].S();
                    lblNombrePasajero2.Text = Fila["first_name"].S();
                    cbxEstatus.SelectedIndex = cbxEstatus.Items.IndexOf(cbxEstatus.Items.FindByValue(Fila["Status"].S()));
                    txtPasatiempo.Text = Fila["Pasatiempos"].S();
                    txtPerfilLinkedin.Text = Fila["PerfilLinkedin"].S();
                    txtPerfilFacebook.Text = Fila["PerfilFacebook"].S();
                    txtPerfilInstragram.Text = Fila["PerfilInstagram"].S();
                    txtPerfilTwitter.Text = Fila["PerfilTwitter"].S();
                    rdlEstadoCivil.SelectedIndex = rdlEstadoCivil.Items.IndexOf(rdlEstadoCivil.Items.FindByValue(Fila["EstadoCivil"].S()));
                    rdlProtocoloCliente.SelectedIndex = rdlProtocoloCliente.Items.IndexOf(rdlProtocoloCliente.Items.FindByValue(Fila["ProtocoloCliente"].S()));
                    rdlAlergias.SelectedIndex = rdlAlergias.Items.IndexOf(rdlAlergias.Items.FindByValue(Fila["Alergias"].S()));
                    txtAlergias.Text = Fila["CualesAlergias"].S();
                    rdlCondicionesMedicasEspeciales.SelectedIndex = rdlCondicionesMedicasEspeciales.Items.IndexOf(rdlCondicionesMedicasEspeciales.Items.FindByValue(Fila["CondicionesMedicasEspeciales"].S()));
                    txtCondicionesMedicasEspeciales.Text = Fila["CualesCondicionesMedicasEspeciales"].S();
                    rdlFobias.SelectedIndex = rdlFobias.Items.IndexOf(rdlFobias.Items.FindByValue(Fila["Fobias"].S()));
                    txtFobias.Text = Fila["CualesFobias"].S();
                    rdlMultipleNacionalidades.SelectedIndex = rdlFobias.Items.IndexOf(rdlFobias.Items.FindByValue(Fila["MultiplesNacionalidades"].S()));
                    txtMultipleNacionalidades.Text = Fila["CualesMultiplesNacionalidades"].S();

                    EstableceRestriccionesAlimentacias(Fila["RestriccionesAlimenticias"].S());
                    EstableceDeportes(Fila["Deportes"].S());

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ConsultaCliente(DataTable dtResultado)
        {
            try
            {
                cbxCodigoCliente.DataSource = dtResultado;
                cbxCodigoCliente.ValueField = "IdCliente";
                cbxCodigoCliente.TextField = "CodigoCliente";
                cbxCodigoCliente.DataBind();
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

                //popup.HeaderText = sCaption;
                //gvPasajero.JSProperties["cpText"] = sMensaje;
                //gvPasajero.JSProperties["cpShowPopup"] = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ObtieneRestriccionesAlimentaciones()
        {
            string CadenaValores = string.Empty;
            try
            {
                foreach (ListEditItem item in ckblRestriccionesAlimenticias.Items)
                {
                    if (item.Selected == true)
                    {
                        CadenaValores = CadenaValores + item.Value + "/";
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return CadenaValores;
        }
        private string ObtieneDeportes()
        {
            string CadenaValores = string.Empty;
            try
            {
                foreach (ListEditItem item in ckblDeportes.Items)
                {
                    if (item.Selected == true)
                    {
                        CadenaValores = CadenaValores + item.Value + "/";
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return CadenaValores;
        }
        private void EstableceRestriccionesAlimentacias(string Cadena)
        {
            try
            {
                string[] Valor = Cadena.Split('/');

                for (int i = 0; i < Valor.Length; i++)
                {
                    if (!string.IsNullOrEmpty(Valor[i]))
                    {
                        //ckblRestriccionesAlimenticias.SelectedIndex = ckblRestriccionesAlimenticias.Items.IndexOf(ckblRestriccionesAlimenticias.Items.FindByValue(Valor[i]));

                        switch (Valor[i])
                        {
                            case "1":
                                ckblRestriccionesAlimenticias.Items[0].Selected = true;
                                break;
                            case "2":
                                ckblRestriccionesAlimenticias.Items[1].Selected = true;
                                break;
                            case "3":
                                ckblRestriccionesAlimenticias.Items[2].Selected = true;
                                break;
                            case "4":
                                ckblRestriccionesAlimenticias.Items[3].Selected = true;
                                break;
                            case "5":
                                ckblRestriccionesAlimenticias.Items[4].Selected = true;
                                break;
                            case "6":
                                ckblRestriccionesAlimenticias.Items[5].Selected = true;
                                break;
                            case "7":
                                ckblRestriccionesAlimenticias.Items[6].Selected = true;
                                break;
                            case "8":
                                ckblRestriccionesAlimenticias.Items[7].Selected = true;
                                break;
                            case "9":
                                ckblRestriccionesAlimenticias.Items[8].Selected = true;
                                break;
                            case "10":
                                ckblRestriccionesAlimenticias.Items[9].Selected = true;
                                break;
                            default:
                                break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EstableceDeportes(string Cadena)
        {
            try
            {
                string[] Valor = Cadena.Split('/');

                for (int i = 0; i < Valor.Length; i++)
                {
                    if (!string.IsNullOrEmpty(Valor[i]))
                    {
                        //ckblDeportes.SelectedIndex = ckblDeportes.Items.IndexOf(ckblDeportes.Items.FindByValue(Valor[i]));

                        switch (Valor[i])
                        {
                            case "1":
                                ckblDeportes.Items[0].Selected = true;
                                break;
                            case "2":
                                ckblDeportes.Items[1].Selected = true;
                                break;
                            case "3":
                                ckblDeportes.Items[2].Selected = true;
                                break;
                            case "4":
                                ckblDeportes.Items[3].Selected = true;
                                break;
                            case "5":
                                ckblDeportes.Items[4].Selected = true;
                                break;
                            case "6":
                                ckblDeportes.Items[5].Selected = true;
                                break;
                            case "7":
                                ckblDeportes.Items[6].Selected = true;
                                break;
                            case "8":
                                ckblDeportes.Items[7].Selected = true;
                                break;
                            case "9":
                                ckblDeportes.Items[8].Selected = true;
                                break;
                            case "10":
                                ckblDeportes.Items[9].Selected = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void LimpiarCampos()
        {
            try
            {
                bActualiza = false;

                lblNombrePasajero2.Text = string.Empty;
                lblApellidosPasajero2.Text = string.Empty;

                txtNombrePasajero.Text = string.Empty;
                txtApellidosPasajero.Text = string.Empty;

                cbxCodigoCliente.SelectedIndex = 0;
                cbxEstatus.SelectedIndex = 0;

                txtAlergias.Text = string.Empty;
                txtCondicionesMedicasEspeciales.Text = string.Empty;
                txtFobias.Text = string.Empty;
                txtMultipleNacionalidades.Text = string.Empty;
                txtNombrePasajero.Text = string.Empty;
                txtPasatiempo.Text = string.Empty;
                txtPerfilFacebook.Text = string.Empty;
                txtPerfilInstragram.Text = string.Empty;
                txtPerfilLinkedin.Text = string.Empty;
                txtPerfilTwitter.Text = string.Empty;
                txtTextoBusqueda.Text = string.Empty;

                rdlAlergias.SelectedIndex = 0;
                rdlCondicionesMedicasEspeciales.SelectedIndex = 0;
                rdlEstadoCivil.SelectedIndex = 0;
                rdlFobias.SelectedIndex = 0;
                rdlMultipleNacionalidades.SelectedIndex = 0;
                rdlProtocoloCliente.SelectedIndex = 0;

                foreach (ListEditItem item in ckblDeportes.Items)
                {
                    item.Selected = false;
                }
                foreach (ListEditItem item in ckblRestriccionesAlimenticias.Items)
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

        #region VARIABLES

        private const string sClase = "frmPasajero.aspx.cs";
        private const string sPagina = "frmPasajero.aspx";

        public int iIdPasajero
        {
            get { return ViewState["VSiIdPasajero"].S().I(); }
            set { ViewState["VSiIdPasajero"] = value; }
        }
        public bool bActualiza
        {
            get { return ViewState["VsbActualizacion"].S().B(); }
            set { ViewState["VsbActualizacion"] = value; }
        }

        public Pasajero objPasajero
        {
            get
            {
                Pasajero objPasajer = new Pasajero();


                if (!bActualiza)
                {
                    objPasajer.iIdCliente = cbxCodigoCliente.SelectedItem != null ? cbxCodigoCliente.SelectedItem.Value.S().I() : 0;
                    objPasajer.sLast_name = txtApellidosPasajero.Text;
                    objPasajer.sFirst_name = txtNombrePasajero.Text;
                }
                else
                {
                    objPasajer.iIdPasajero = iIdPasajero;
                }

                objPasajer.iStatus = cbxEstatus.SelectedItem != null ? cbxEstatus.SelectedItem.Value.S().I() : 0;
                objPasajer.sPasatiempos = txtPasatiempo.Text;
                objPasajer.sPerfilLinkedin = txtPerfilLinkedin.Text;
                objPasajer.sPerfilFacebook = txtPerfilFacebook.Text;
                objPasajer.sPerfilInstagram = txtPerfilInstragram.Text;
                objPasajer.sPerfilTwitter = txtPerfilTwitter.Text;
                objPasajer.iEstadoCivil = rdlEstadoCivil.SelectedItem != null ? rdlEstadoCivil.SelectedItem.Value.S().I() : 0;
                objPasajer.iProtocoloCliente = rdlProtocoloCliente.SelectedItem != null ? rdlProtocoloCliente.SelectedItem.Value.S().I() : 0;
                if (rdlAlergias.SelectedItem != null)
                {
                    objPasajer.iAlergias = rdlAlergias.SelectedItem.Value.S().I();
                    objPasajer.sCualesAlergias = txtAlergias.Text;
                }
                if (rdlCondicionesMedicasEspeciales.SelectedItem != null)
                {
                    objPasajer.iCondicionesMedicasEspeciales = rdlCondicionesMedicasEspeciales.SelectedItem.Value.S().I();
                    objPasajer.sCualesCondicionesMedicasEspeciales = txtCondicionesMedicasEspeciales.Text;
                }
                if (rdlFobias.SelectedItem != null)
                {
                    objPasajer.iFobias = rdlFobias.SelectedItem.Value.S().I();
                    objPasajer.sCualesFobias = txtFobias.Text;
                }
                if (rdlMultipleNacionalidades.SelectedItem != null)
                {
                    objPasajer.iMultiplesNacionalidades = rdlMultipleNacionalidades.SelectedItem.Value.S().I();
                    objPasajer.sCualesMultiplesNacionalidades = txtMultipleNacionalidades.Text;
                }
                objPasajer.sRestriccionesAlimenticias = ObtieneRestriccionesAlimentaciones();
                objPasajer.sDeportes = ObtieneDeportes();

                return objPasajer;
            }
            set
            {
                objPasajero = value;
            }
        }

        public object[] oArrFiltros
        {
            get
            {
                string sNombre = string.Empty;
                string sCodigoCliente = string.Empty;
                int iEstatus = -1;

                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "1":
                        sNombre = txtTextoBusqueda.Text.S();
                        sCodigoCliente = string.Empty;
                        iEstatus = -1;
                        break;
                    case "2":
                        sNombre = string.Empty;
                        sCodigoCliente = txtTextoBusqueda.Text.S();
                        iEstatus = -1;
                        break;
                    case "3":
                        sCodigoCliente = string.Empty;
                        iEstatus = 1;
                        sNombre = string.Empty;
                        break;
                    case "4":
                        sCodigoCliente = string.Empty;
                        iEstatus = 0;
                        sNombre = string.Empty;
                        break;
                }

                return new object[]{
                                        "@IdPasajero", iIdPasajero,
                                        "@Nombre", "%" + sNombre + "%",
                                        "@CodigoCliente", "%" + sCodigoCliente + "%",
                                        "@estatus", iEstatus
                                    };
            }

        }
        public object[] oArrFiltrosCliente
        {
            get
            {
                string sNombre = string.Empty;
                string sCodigoCliente = string.Empty;
                string sTipoCliente = string.Empty;
                int iEstatus = 1;

                return new object[]{
                                        "@CodigoCliente", sCodigoCliente,
                                        "@Nombre", "%" + sNombre + "%",
                                        "@TipoCliente", "%" + sTipoCliente + "%",
                                        "@estatus", iEstatus
                                    };
            }

        }

        Pasajero_Presenter oPresenter;

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public event EventHandler eConsultaCliente;
        public event EventHandler eConsultaPasajeroId;
        public event EventHandler eGuardaPasajeroPerfil;
        public event EventHandler eEliminaPasajeroPorId;
        public event EventHandler eActualizaPasajeroPerfil;

        #endregion


    }
}