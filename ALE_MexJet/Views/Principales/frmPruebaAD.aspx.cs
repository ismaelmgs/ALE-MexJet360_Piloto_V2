using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ALE_MexJet.Objetos;
using ALE_MexJet.Clases;
using System.Configuration;

namespace ALE_MexJet.Views.Principales
{
    public partial class frmPruebaAD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            UserIdentity oUsuario = ClsSecurity.IsValidAD(txtUsuario.Text.S(), txtPassword.Text.S());
            if (oUsuario.bEncontrado)
            {
                //mpeMensaje.Visible = true;
                //mpeMensaje.ShowMessage(oUsuario.sEstatus, "Inicio de sesión");
                alert(oUsuario.sEstatus);
            }

            else
            {
                Session["nombreI"] = oUsuario;


                //oUsuario.sUsuario = "i.morato";
                //oUsuario.sName = "Ismael Morato Gallegos";
                //oUsuario.sRolDescripcion = "Pruebas";
                //oUsuario.iRol = 1;

                //ObtieneValores();
                //oUsuario.dTPermisos = (DataTable)Session["oDatos"];
                //Session["UserIdentity"] = oUsuario;

                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirecciona", "window.location.href = 'frmDefault.aspx'", true);

                string sSitio = ConfigurationManager.AppSettings["NombreSitio"].ToString();
                string sFinal = ConvertRelativeUrlToAbsoluteUrl("/" + sSitio + "/Views/frmDefault.aspx");

                Response.Redirect(sFinal);
            }
        }

        private void alert(string m)
        {
            m = "alert('" + m + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", m, true);
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
    }
}