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
    public partial class frmRemisiones : System.Web.UI.Page, IViewRemision
    {

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.Remisiones);
            LoadActions(DrPermisos);
            gvRemisiones.Columns["Estatus"].Visible = true;
            oPresenter = new Remision_Presenter(this, new DBRemision());
            gvRemisiones.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvRemisiones.SettingsPager.ShowDisabledButtons = true;
            gvRemisiones.SettingsPager.ShowNumericButtons = true;
            gvRemisiones.SettingsPager.ShowSeparators = true;
            gvRemisiones.SettingsPager.Summary.Visible = true;
            gvRemisiones.SettingsPager.PageSizeItemSettings.Visible = true;
            gvRemisiones.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvRemisiones.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";

            if (eSearchMotivos != null)
                eSearchMotivos(sender, e);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("frmGRemision.aspx");
            }
            catch (Exception ex)
            {

            }
        }

        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }


        protected void gvRemisiones_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            if (e.CommandArgs.CommandName.S() == "Detalle")
            {
                Session["Matricula"] = string.Empty;
                Session["FechaVuelo"] = string.Empty;
                int index = e.VisibleIndex.I();
                string sIdRemision = e.CommandArgs.CommandArgument.S();
                string sMatricula = gvRemisiones.GetRowValues(index, "Matricula").ToString();
                string sFechaVuelo = gvRemisiones.GetRowValues(index, "FechaVuelo").ToString();

                if (!string.IsNullOrEmpty(sMatricula))
                    Session["Matricula"] = sMatricula;
                if (!string.IsNullOrEmpty(sFechaVuelo))
                    Session["FechaVuelo"] = sFechaVuelo;

                Response.Redirect("~/Views/CreditoCobranza/frmGRemision.aspx?Folio=" + sIdRemision, false);
            }

            else if (e.CommandArgs.CommandName.S() == "Eliminar")
            {
                if (hfValor.Value == "true")
                {
                    iIdRemision = e.CommandArgs.CommandArgument.I();

                    if (eDeleteObj != null)
                        eDeleteObj(null, EventArgs.Empty);
                }
            }

            else if (e.CommandArgs.CommandName.S() == "Ajuste")
            {
                string sIdRemision = e.CommandArgs.CommandArgument.S();
                hdnIdRemision.Value = sIdRemision;
                pnlAjuste.Visible = true;
            }
        }        

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
        }
       

        #endregion

        #region METODOS
        
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                popup.HeaderText = sCaption;
                gvRemisiones.JSProperties["cpText"] = sMensaje;
                gvRemisiones.JSProperties["cpShowPopup"] = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void LoadObjects(DataTable dt)
        {
            gvRemisiones.DataSource = dt;
            gvRemisiones.DataBind();
        }
        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                btnBusqueda.Enabled = false;
                txtTextoBusqueda.Enabled = false;
                ddlTipoBusqueda.Enabled = false;
                btnExcel.Enabled = false;
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
                                ddlTipoBusqueda.Enabled = true;
                                btnExcel.Enabled = true;

                            }
                            else
                            {
                                btnBusqueda.Enabled = false;
                                ddlTipoBusqueda.Enabled = false;
                                btnExcel.Enabled = false;

                            } break;
                        case 5: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                btnNuevo.Enabled = true;
                            }
                            else
                            {
                                btnNuevo.Enabled = false;
                            } break;
                    }
                }
            }

        }

        public void LoadMotivos(DataTable dt)
        {
            try
            {
                if(dt != null && dt.Rows.Count > 0)
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
        public void isValidUser(System.Data.DataTable dtDatos)
        {
            try
            {
                dtDatosAutorizador = null;
                dtDatosAutorizador = dtDatos;

                if (dtDatosAutorizador != null && dtDatosAutorizador.Rows.Count > 0 && !string.IsNullOrEmpty(hdnIdRemision.Value.S()))
                {
                    NameValueCollection values = new NameValueCollection();
                    values.Add("apikey", sapiKey);//ConfigurationManager.AppSettings["apiKey"]);
                    values.Add("from", sEmailSoporte);//onfigurationManager.AppSettings["EmailSoporte"]);
                    values.Add("fromName", "ALE Management");
                    values.Add("to", dtDatosAutorizador.Rows[0]["Correo"].S());
                    values.Add("subject", "Autorizar Ajuste de Remisión");
                    values.Add("isTransactional", "true");
                    values.Add("template", sTemplate);// ConfigurationManager.AppSettings["template"]);
                    values.Add("merge_firstname", dtDatosAutorizador.Rows[0]["Autorizador"].S());
                    values.Add("merge_email", dtDatosAutorizador.Rows[0]["Correo"].S());
                    //values.Add("merge_timeInterval", DateTime.Now.AddHours(2).ToString("ddMMyyHHmm"));
                    //values.Add("merge_accountaddress", sEmail);
                    values.Add("merge_IdRemision", hdnIdRemision.Value.S());
                    values.Add("merge_IdAjuste", iIdAjuste.S());

                    string address = "https://api.elasticemail.com/v2/email/send";

                    string response = Send(address, values);

                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    Success s = new Success();
                    s = ser.Deserialize<Success>(response);

                    if (s.success)
                    {
                        lblMsg.Text = "Se ha enviado el correo satisfactoriamente";
                        //msgAlert.ShowOnPageLoad = true;
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

        Remision_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchMotivos;
        public event EventHandler eInsertAjuste;
        public event EventHandler eValidateObj;
        UserIdentity oUsuario = new UserIdentity();
        public DataTable dtDatosAutorizador
        {
            get { return (DataTable)ViewState["VSDatosAutorizador"]; }
            set { ViewState["VSDatosAutorizador"] = value; }
        }

        protected static int iIdRemision;
        public Remision oRemision
        {
            get
            {
                Remision oRem = new Remision();
                oRem.iIdRemision = iIdRemision;
                return oRem;
            }
        }

        public AjusteRemision oAjuste
        {
            get
            {
                try
                {
                    AjusteRemision oAj = new AjusteRemision();
                    oAj.IIdRemision = hdnIdRemision.Value.I();
                    oAj.IIdMotivo = (int)ccbMotivo.SelectedItem.GetValue("IdMotivo"); //ccbMotivo.Value != null ? ccbMotivo.Value.ToString().I() : 0;
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
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }
        public object[] oArrFiltros
        {
            get
            {
                long iFolio = 0;
                string sCliente = string.Empty;
                string sContrato = string.Empty;
                string sPrimeros = "0";


                switch (ddlTipoBusqueda.Value.S())
                {
                    case "0":
                        sPrimeros = "1";
                        break;
                    case "1":
                        iFolio = txtTextoBusqueda.Text.S().L();
                        break;
                    case "2":
                        sCliente = txtTextoBusqueda.Text.S();
                        break;
                    case "3":
                        sContrato = txtTextoBusqueda.Text.S();
                        break;
                }

                return new object[]{
                                        "@FolioRemision", iFolio,
                                        "@Cliente", "%" + sCliente + "%",
                                        "@Contrato", "%" + sContrato + "%",
                                        "@Primeros", sPrimeros
                                    };
            }
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

        public string sTemplate
        {
            get { return ViewState["sVSsTemplate"].S(); }
            set { ViewState["sVSsTemplate"] = value; }
        }

        public string sEmailSoporte
        {
            get { return ViewState["sVsEmailSoporte"].S(); }
            set { ViewState["sVsEmailSoporte"] = value; }
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

        public int iIdAjuste
        {
            get { return (int)ViewState["VSIdAjuste"]; }
            set { ViewState["VSIdAjuste"] = value; }
        }
        #endregion               

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

        protected void btn_Init(object sender, EventArgs e)
        {

            ASPxButton btn = (ASPxButton)sender;
            GridViewDataItemTemplateContainer bl = (GridViewDataItemTemplateContainer)btn.NamingContainer;
            ASPxButton btnEdicion = (ASPxButton)bl.Controls[3];
            int idRemision = bl.KeyValue.S().I();
            object status = gvRemisiones.GetRowValuesByKeyValue(bl.KeyValue, "Estatus");
                if (status.S() == "Cancelada")
            {
                btn.Enabled = false;
                btnEdicion.Enabled = false;
            }
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            hdnIdRemision.Value = string.Empty;
            ccbMotivo.SelectedIndex = -1;
            txtHoras.Text = string.Empty;
            txtComentarios.Text = string.Empty;
            pnlAjuste.Visible = false;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                iIdAjuste = 1;

                if (eInsertAjuste != null)
                    eInsertAjuste(sender, e);

                if (iIdAjuste != 0)
                {
                    if (eValidateObj != null)
                        eValidateObj(sender, e);

                    lblMsg.Text = "Se registro la solicitud de ajuste correctamente. El autorizador la revisará a la brevedad.";
                    msgAlert.ShowOnPageLoad = true;
                }

                //btnCancelar_Click(null, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void bt_OK_Click(object sender, EventArgs e)
        {
            hdnIdRemision.Value = string.Empty;
            ccbMotivo.SelectedIndex = -1;
            txtHoras.Text = string.Empty;
            txtComentarios.Text = string.Empty;
            pnlAjuste.Visible = false;
            msgAlert.ShowOnPageLoad = false;
        }
    }
}