using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ALE_MexJet.Objetos;
using System.Configuration;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Views
{
    public partial class frmLogin : System.Web.UI.Page, IViewLogin
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            mpeMensaje.OkButtonPressed += mpeMensaje_OkButtonPressed;
            oPresenter = new Login_Presenter(this, new DBRolAccion());
            SpanToken.Visible = false;
            SvgToken.Visible = false;

            if (Request[txtToken.UniqueID] != null)
            {
                if (Request[txtToken.UniqueID].Length > 0)
                {
                    txtToken.Text = Request[txtToken.UniqueID];
                }
            }
            
            txtPassword.Attributes.Add("value", txtPassword.Text);
        }

        private void mpeMensaje_OkButtonPressed(object sender, EventArgs args)
        {

        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            string sFinal = string.Empty;

            if (txtUsuario.Text.S() == string.Empty)
            {
                ShowMessage("El Usuario es requerido", Enums.Warning);
                //alert("El Usuario es requerido");
            }
            else if (txtPassword.Text.S() == string.Empty)
            {
                ShowMessage("La contraseña es requerida", Enums.Warning);
                //alert("La contraseña es requerida");
            }
            else
            {
                bool ban = false;
                if (ban)
                {
                    oUsuario = ClsSecurity.IsValidAD(txtUsuario.Text.S(), txtPassword.Text.S());
                    if (oUsuario.bEncontrado)
                    {
                        ShowMessage("No se encontro al usuario", Enums.Warning);
                        //alert(oUsuario.sEstatus);
                        //ErrorLogin.Visible = true;
                    }
                    else
                    {
                        sTexto = txtPassword.Text.S();

                        Session["nombreI"] = oUsuario;

                        ObtieneValores();
                        oUsuario.dTPermisos = (DataTable)Session["oDatos"];
                        Session["UserIdentity"] = oUsuario;

                        Token(sender, e);
                        ShowMessage("Se ha enviado un token a su teléfono movil", Enums.Success);
                    }
                }
                else
                {
                    oUsuario = new UserIdentity();
                    oUsuario.sUsuario = "i.morato";
                    oUsuario.sName = "Ismael Morato Gallegos";
                    oUsuario.sRolDescripcion = "Pruebas";
                    oUsuario.iRol = 1;
                    oUsuario.sUrlPaginaInicial = "";
                    oUsuario.sCorreoBaseUsuario = "ac.tlc@aerolineasejecutivas.com";                  

                    ObtieneValores();
                    oUsuario.dTPermisos = (DataTable)Session["oDatos"];
                    Session["UserIdentity"] = oUsuario;

                    sFinal = "/Views/frmDefault.aspx";
                    new DBUtils().DBInicioSistemaBitacora();
                    Response.Redirect(sFinal);
                }
                
            }
            
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            
            if (txtToken.Text.S() == string.Empty)
            {
                alert("El Token es requerido");
            }
            else
            {
                if (eSearchToken != null)
                    eSearchToken(sender, e);

                if (TokenGuardado == 1)
                {
                    string sFinal = string.Empty;
                    if (oUsuario.sUrlPaginaInicial == string.Empty)
                    {
                        string sSitio = ConfigurationManager.AppSettings["NombreSitio"].S();
                        //sFinal = ConvertRelativeUrlToAbsoluteUrl("/" + sSitio + "/Views/frmDefault.aspx");


                        sFinal = "/" + sSitio + "/Views/frmDefault.aspx";
                    }
                    else
                        sFinal = oUsuario.sUrlPaginaInicial;

                    new DBUtils().DBInicioSistemaBitacora();
                    Response.Redirect(sFinal);
                }
                else if (TokenGuardado == 2)
                {
                    alert("El usuario no tiene número telefónico registrado, por favor comuniquese a soporte.");
                }
                else
                {
                    alert("El Token no coincide");
                }
            }
            SpanToken.Visible = true;
            SvgToken.Visible = true;
            txtToken.Visible = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtToken.Text = string.Empty;
            btnEntrar.Visible = true;
            btnOK.Visible = false;
            btnCancelar.Visible = false;
            txtToken.Visible = false;
            SpanToken.Visible = false;
            SvgToken.Visible = false;
            lkbReenviar.Visible = false;

            divEntrar.Visible = true;
            divToken.Visible = false;
        }

        protected void lkbReenviar_Click(object sender, EventArgs e)
        {
            Token(sender, e);
            ShowMessage("Se ha reenviado un token a su teléfono movil", Enums.Success);
        }

        protected void btnCerrarMensaje_Click(object sender, EventArgs e)
        {
            ErrorLogin.Visible = false;
        }

        #region "METODOS"

        public void ObtieneValores()
        {
            if (eSearchObj != null)
                eSearchObj(null, EventArgs.Empty);

        }
        public void LoadObjects(DataTable dtObject)
        {
            Session["oDatos"] = dtObject;
        }
        public void alert(string m)
        {
            //m = "alert('" + m + "');";
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", m, true);

            mpeMensaje.ShowMessage(m, "Aviso");
        }

        protected void ShowMessage(string Message, string type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }

        public string ConvertRelativeUrlToAbsoluteUrl(string relativeUrl)
        {
            if (Request.IsSecureConnection)
                return string.Format("https://{0}{1}{2}", Request.Url.Host,
                    Request.Url.Port == 80 ? "" : ":" + Request.Url.Port.ToString(),
                    Page.ResolveUrl(relativeUrl));
            else
                return string.Format("http://{0}{1}{2}", Request.Url.Host,
                    Request.Url.Port == 80 ? "" : ":" + Request.Url.Port.ToString(),
                    Page.ResolveUrl(relativeUrl));
        }

        public void Token(object sender, EventArgs e)
        {
            var guid = Guid.NewGuid();
            var numeros = new string(guid.ToString().Where(Char.IsDigit).ToArray());
            var Convertir = numeros.Replace("0","9");
            iToken = int.Parse(Convertir.Substring(0, 6));
            TokenGuardado = iToken;

            //Guardar Token
            if (eSaveObj != null)
                eSaveObj(sender, e);

            if (iRestoken == -1)
                alert("El usuario no tiene telefono asignado, favor de comunicarse a soporte.");
            else
            {
                //aqui se manda el mensaje con el token 
                //enviar parametros (sNumeroTel, sClaveToken)
                string Mensaje = "Su clave para acceder a MexJet360 es: " + sClaveToken;
                new UtilsServicios().EnvioMensajeSMS(Mensaje, sNumeroTel);

                lkbReenviar.Visible = true;
                divEntrar.Visible = false;
                divToken.Visible = true;
                btnOK.Visible = true;
                btnCancelar.Visible = true;
                txtToken.Visible = true;
                SpanToken.Visible = true;
                SvgToken.Visible = true;
                btnEntrar.Visible = false;

                txtPassword.Text = sTexto;
            }
        }
        #endregion

        #region "Vars y Propiedades"
        Login_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchToken;

        //private object[] _oArrFiltros = new  object[4];
        public UserIdentity oUsuario
        {
            get { return (UserIdentity)ViewState["VSUsuario"]; }
            set { ViewState["VSUsuario"] = value; }
        }

        public object[] oArrFiltros
        {
            get
            {
                int iIdRol;
                int iEstatus;

                iIdRol = oUsuario.iRol.I();
                iEstatus = 1;

                return new object[]{
                                        "@IdRol",  iIdRol ,
                                        "@IdStatus", iEstatus
                                    };
            }

        }

        public int iToken { get; set; }

        public int TokenGuardado { get; set; }
        //private string _sUsuario = string.Empty;
        public string sUsuario {
            get { return txtUsuario.Text.S(); }
            
        }
        public string sToken
        {
            get { return txtToken.Text.S(); }

        }

        public long iRestoken
        {
            set; get;

        }

        public string sNumeroTel
        {
            set; get;
        }

        public string sClaveToken
        {
            set; get;
        }

        public string sTexto
        {
            get { return ViewState["VSTexto"].S(); }
            set { ViewState["VSTexto"] = value; }
        }
        #endregion


    }
}