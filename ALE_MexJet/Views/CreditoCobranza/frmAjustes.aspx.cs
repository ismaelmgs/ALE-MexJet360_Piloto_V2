using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using ALE_MexJet.Objetos;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Reflection;
using ALE_MexJet.Clases;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using System.Text;
using System.Net;

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmAjustes : System.Web.UI.Page, IViewAjuste
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            oPresenter = new Ajuste_Presenter(this, new DBAjuste());

            gvRemisiones.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvRemisiones.SettingsPager.ShowDisabledButtons = true;
            gvRemisiones.SettingsPager.ShowNumericButtons = true;
            gvRemisiones.SettingsPager.ShowSeparators = true;
            gvRemisiones.SettingsPager.Summary.Visible = true;
            gvRemisiones.SettingsPager.PageSizeItemSettings.Visible = true;
            gvRemisiones.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvRemisiones.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";

            //if (!IsPostBack)
            //{
            if (eSearchObj != null)
                eSearchObj(sender, e);
            //}
        }

        protected void ddlContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                iIdContrato = 0;
                iIdContrato = ddlContrato.SelectedItem.Value.I();

                if (iIdContrato != 0)
                {
                    //if (eObjSelected != null)
                    //    eObjSelected(sender, e);
                    string sClaveContrato = string.Empty;
                    sClaveContrato = ddlContrato.SelectedItem.Value.S();

                    readContrato.Text = sClaveContrato;
                    pnlAjuste.Visible = true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region MÉTODOS
        public void LoadContratos(DataTable dt)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlContrato.DataSource = dt;
                    ddlContrato.ValueField = "ClaveContrato";
                    ddlContrato.TextField = "DesContrato";
                    ddlContrato.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadRemisiones(DataTable dt)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ccbListaRemisiones.DataSource = dt;
                    ccbListaRemisiones.ValueField = "IdRemision";
                    ccbListaRemisiones.TextField = "Remision";
                    ccbListaRemisiones.DataBind(); 
                    //gvRemisiones.DataSource = dt;
                    //gvRemisiones.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadMotivos(DataTable dt)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    ccbMotivo.DataSource = dt;
                    ccbMotivo.ValueField = "IdMotivo";
                    ccbMotivo.TextField = "DesMotivo";
                    ccbMotivo.DataBind();
                    //ccbMotivo.Items.Add(new ListEditItem("Seleccione", "0"));
                    //ccbMotivo.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void setParameters(List<Parametros> lstParameteres)
        {
            foreach (Parametros p in lstParameteres)
            {
                if (p.Nombre == "apiKey")
                    sapiKey = p.Valor;

                if (p.Nombre == "EmailSoporte")
                    sEmailSoporte = p.Valor;

                if (p.Nombre == "template")
                    sTemplate = p.Valor;
            }
        }
        public void setParametersNot(List<Parametros> lstParameteres)
        {
            foreach (Parametros p in lstParameteres)
            {
                if (p.Nombre == "apiKey")
                    sapiKeyNot = p.Valor;

                if (p.Nombre == "EmailSoporte")
                    sEmailSoporteNot = p.Valor;

                if (p.Nombre == "template")
                    sTemplateNot = p.Valor;
            }
        }
        public void isValidUser(System.Data.DataSet dsDatos)
        {
            try
            {
                dtDatosAutorizador = null;
                dtDatosNotificacion = null;

                if (dsDatos != null && dsDatos.Tables[0].Rows.Count > 0)
                    dtDatosAutorizador = dsDatos.Tables[0];

                if (dsDatos != null && dsDatos.Tables[1].Rows.Count > 0)
                    dtDatosNotificacion = dsDatos.Tables[1];


                if (dtDatosAutorizador != null && dtDatosAutorizador.Rows.Count > 0)
                {
                    NameValueCollection values = new NameValueCollection();
                    values.Add("apikey", sapiKey);//ConfigurationManager.AppSettings["apiKey"]);
                    values.Add("from", sEmailSoporte);//onfigurationManager.AppSettings["EmailSoporte"]);
                    values.Add("fromName", "ALE Management - Contrato " + sClaveContrato);
                    values.Add("to", dtDatosAutorizador.Rows[0]["Correo"].S());
                    values.Add("subject", "Autorizar Ajuste");
                    values.Add("isTransactional", "true");
                    values.Add("template", sTemplate);// ConfigurationManager.AppSettings["template"]);
                    values.Add("merge_firstname", dtDatosAutorizador.Rows[0]["Autorizador"].S());
                    values.Add("merge_email", dtDatosAutorizador.Rows[0]["Correo"].S());
                    //values.Add("merge_timeInterval", DateTime.Now.AddHours(2).ToString("ddMMyyHHmm"));
                    //values.Add("merge_accountaddress", sEmail);
                    //values.Add("merge_IdRemision", readNumRemision.Value.S());
                    values.Add("merge_IdAjuste", iIdAjuste.S());

                    string address = "https://api.elasticemail.com/v2/email/send";

                    string response = Send(address, values);

                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    Success s = new Success();
                    s = ser.Deserialize<Success>(response);

                    if (s.success)
                    {
                        if (NotificarEjecutivo(dtDatosNotificacion))
                        {
                            if (NotificarVendedor(dtDatosNotificacion))
                                lblMsg.Text = "Se ha notificado correctamente";
                        }
                    }
                    Console.WriteLine(response);
                }
                else
                {
                    lblMsg.Text = "No se pudo enviar el correo, favor de verificar";
                    msgAlert.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool NotificarEjecutivo(DataTable dtNotificador) 
        {
            try
            {
                bool bRes = false;
                NameValueCollection values = new NameValueCollection();
                values.Add("apikey", sapiKeyNot);//ConfigurationManager.AppSettings["apiKey"]);
                values.Add("from", sEmailSoporteNot);//onfigurationManager.AppSettings["EmailSoporte"]);
                values.Add("fromName", "ALE Management - Contrato " + sClaveContrato);

                //if(!string.IsNullOrEmpty(dtNotificador.Rows[0]["CorreoEjecutivo"].S()))
                //    values.Add("to", dtNotificador.Rows[0]["CorreoEjecutivo"].S());
                //else
                //    values.Add("to", "jimmymh87@gmail.com");
                values.Add("to", "jimmymh87@gmail.com"); //Prueba

                values.Add("subject", "Notificación de ajuste al ejecutivo");
                values.Add("isTransactional", "true");
                values.Add("template", sTemplateNot);// ConfigurationManager.AppSettings["template"]);
                values.Add("merge_firstname", dtNotificador.Rows[0]["NombreEjecutivo"].S());
                values.Add("merge_email", dtNotificador.Rows[0]["CorreoEjecutivo"].S());

                values.Add("merge_Contrato", readContrato.Text);
                //values.Add("merge_Remision", readNumRemision.Text);

                string address = "https://api.elasticemail.com/v2/email/send";
                string response = Send(address, values);

                JavaScriptSerializer ser = new JavaScriptSerializer();
                Success s = new Success();
                s = ser.Deserialize<Success>(response);

                if (s.success)
                    bRes = true;

                return bRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool NotificarVendedor(DataTable dtNotificador)
        {
            try
            {
                bool bRes = false;
                NameValueCollection values = new NameValueCollection();
                values.Add("apikey", sapiKeyNot);//ConfigurationManager.AppSettings["apiKey"]);
                values.Add("from", sEmailSoporteNot);//onfigurationManager.AppSettings["EmailSoporte"]);
                values.Add("fromName", "ALE Management - Contrato " + sClaveContrato);

                //if (!string.IsNullOrEmpty(dtNotificador.Rows[0]["CorreoVendedor"].S()))
                //    values.Add("to", dtNotificador.Rows[0]["CorreoVendedor"].S());
                //else
                //    values.Add("to", "jimmymh87@gmail.com");
                values.Add("to", "jimmymh87@gmail.com"); //Prueba

                values.Add("subject", "Notificación de ajuste al vendedor");
                values.Add("isTransactional", "true");
                values.Add("template", sTemplateNot);// ConfigurationManager.AppSettings["template"]);
                values.Add("merge_firstname", dtNotificador.Rows[0]["NombreVendedor"].S());
                values.Add("merge_email", dtNotificador.Rows[0]["CorreoVendedor"].S());

                values.Add("merge_Contrato", readContrato.Text);
                //values.Add("merge_Remision", readNumRemision.Text);

                string address = "https://api.elasticemail.com/v2/email/send";
                string response = Send(address, values);

                JavaScriptSerializer ser = new JavaScriptSerializer();
                Success s = new Success();
                s = ser.Deserialize<Success>(response);

                if (s.success)
                    bRes = true;

                return bRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string Send(string address, NameValueCollection values)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] apiResponse = client.UploadValues(address, values);
                    return Encoding.UTF8.GetString(apiResponse);

                }
                catch (Exception ex)
                {
                    return "Exception caught: " + ex.Message + "\n" + ex.StackTrace;
                }
            }
        }
        #endregion

        #region VARIABLES Y PROPIEDADES
        private const string sClase = "frmAjustes.aspx.cs";
        private const string sPagina = "frmAjustes.aspx";

        Ajuste_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eValidateObj;

        public DataTable dtDatosAutorizador
        {
            get { return (DataTable)ViewState["VSDatosAutorizador"]; }
            set { ViewState["VSDatosAutorizador"] = value; }
        }
        public DataTable dtDatosNotificacion
        {
            get { return (DataTable)ViewState["VSDatosNotificacion"]; }
            set { ViewState["VSDatosNotificacion"] = value; }
        }
        public int iIdContrato
        {
            get { return (int)ViewState["VSIdContrato"]; }
            set { ViewState["VSIdContrato"] = value; }
        }
        public string sClaveContrato
        {
            get { return (string)ViewState["VSsClaveContrato"]; }
            set { ViewState["VSsClaveContrato"] = value; }
        }
        //sClaveContrato
        public int iIdAjuste
        {
            get { return (int)ViewState["VSIdAjuste"]; }
            set { ViewState["VSIdAjuste"] = value; }
        }
        public string sEmailSoporte
        {
            get { return ViewState["sVsEmailSoporte"].S(); }
            set { ViewState["sVsEmailSoporte"] = value; }
        }
        public string sEmailSoporteNot
        {
            get { return ViewState["sVsEmailSoporteNot"].S(); }
            set { ViewState["sVsEmailSoporteNot"] = value; }
        }
        public string sEmail
        {
            //get { return txtEmail.Text.S(); }
            get { return ""; }
        }
        public string sapiKey
        {
            get { return ViewState["sVSsapiKey"].S(); }
            set { ViewState["sVSsapiKey"] = value; }
        }
        public string sapiKeyNot
        {
            get { return ViewState["sVSsapiKeyNot"].S(); }
            set { ViewState["sVSsapiKeyNot"] = value; }
        }

        public string sTemplate
        {
            get { return ViewState["sVSsTemplate"].S(); }
            set { ViewState["sVSsTemplate"] = value; }
        }
        public string sTemplateNot
        {
            get { return ViewState["sVSsTemplateNot"].S(); }
            set { ViewState["sVSsTemplateNot"] = value; }
        }
        public class Success
        {
            public bool success { get; set; }
            public Data data { get; set; }

        }
        public class Data
        {
            public string transactionid { get; set; }
            public string messageid { get; set; }
        }
        public AjusteRemision oAjuste
        {
            get
            {
                try
                {
                    AjusteRemision oAj = new AjusteRemision();
                    oAj.IIdRemision = ccbMotivo.SelectedItem.Value.I() == 15 ? ccbListaRemisiones.SelectedItem.Value.I() : 0;
                    oAj.SCveContrato = sClaveContrato;
                    oAj.ITipo = ccbTipo.SelectedItem.Value.I();
                    oAj.IIdMotivo = ccbMotivo.SelectedItem.Value.I(); //(int)ccbMotivo.SelectedItem.GetValue("IdMotivo"); //ccbMotivo.Value != null ? ccbMotivo.Value.ToString().I() : 0;
                    oAj.SHoras = txtHoras.Text;
                    oAj.SComentarios = txtComentarios.Text;
                    oAj.IEstatus = 1; //Registrado
                    oAj.SUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                    return oAj;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        #endregion

        protected void gvRemisiones_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Ajuste")
                {
                    int index = e.VisibleIndex.I();
                    string sIdRemision = e.CommandArgs.CommandArgument.S();
                    string sClaveContrato = gvRemisiones.GetRowValues(index, "ClaveContrato").ToString();

                    //readNumRemision.Text = sIdRemision;
                    readContrato.Text = sClaveContrato;

                    pnlRemisiones.Visible = false;
                    pnlAjuste.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                iIdAjuste = 0;

                if (eNewObj != null)
                    eNewObj(sender, e);

                if (iIdAjuste != 0)
                {
                    if (eValidateObj != null)
                        eValidateObj(null, null);

                    lblMsg.Text = "Se registró la solicitud de ajuste correctamente. El autorizador la revisará a la brevedad.";
                    msgAlert.ShowOnPageLoad = true;
                    readContrato.Text = string.Empty;
                    //readNumRemision.Text = string.Empty;
                    hdnIdRemision.Value = string.Empty;
                    ccbTipo.SelectedIndex = -1;
                    ccbMotivo.SelectedIndex = -1;
                    ccbListaRemisiones.SelectedIndex = -1;
                    txtHoras.Text = string.Empty;
                    txtComentarios.Text = string.Empty;
                    pnlAjuste.Visible = false;
                    //pnlRemisiones.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            readContrato.Text = string.Empty;
            //readNumRemision.Text = string.Empty;
            hdnIdRemision.Value = string.Empty;
            ccbTipo.SelectedIndex = -1;
            ccbMotivo.SelectedIndex = -1;
            txtHoras.Text = string.Empty;
            txtComentarios.Text = string.Empty;
            pnlAjuste.Visible = false;
            pnlRemisiones.Visible = false;
        }

        protected void bt_OK_Click(object sender, EventArgs e)
        {
            readContrato.Text = string.Empty;
            //readNumRemision.Text = string.Empty;
            hdnIdRemision.Value = string.Empty;
            ccbTipo.SelectedIndex = -1;
            ccbMotivo.SelectedIndex = -1;
            txtHoras.Text = string.Empty;
            txtComentarios.Text = string.Empty;
            pnlAjuste.Visible = false;
            pnlRemisiones.Visible = false;
            msgAlert.ShowOnPageLoad = false;
        }

        protected void gvRemisiones_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            object value = (sender as DevExpress.Web.ASPxGridView).GetRowValues(e.VisibleIndex, "Status");
            e.Enabled = value.S().I() > 0;

        }

        protected void gvRemisiones_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            object value = (sender as DevExpress.Web.ASPxGridView).GetRowValues(e.VisibleIndex, "Status");
            e.Enabled = value.S().I() > 0;
        }

        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }

        protected void btnSolicitarAjuste_Click(object sender, EventArgs e)
        {
            readContrato.Text = ddlContrato.SelectedItem.Text.S();
            sClaveContrato = ddlContrato.SelectedItem.Value.S();
            pnlAjuste.Visible = true;
        }

        protected void ccbMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ccbMotivo.SelectedItem.Value.I() != 15)
                {
                    pnlAgregarRem.Visible = false;
                }
                else
                {
                    sClaveContrato = ddlContrato.SelectedItem.Value.S();

                    if (eObjSelected != null)
                        eObjSelected(sender, e);

                    pnlAgregarRem.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}