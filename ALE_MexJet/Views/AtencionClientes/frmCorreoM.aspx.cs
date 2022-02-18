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
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace ALE_MexJet.Views.AtencionClientes
{
    public partial class frmCorreoM : System.Web.UI.Page, IVIewCorreoM
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DemoHtmlEditor.SettingsDialogs.InsertImageDialog.ShowInsertFromWebSection = false;
                oPresenter = new CorreoM_Presenter(this, new DBCorreoM());
                DemoHtmlEditor.SettingsDialogs.InsertImageDialog.ShowMoreOptionsButton = false;
                DemoHtmlEditor.SettingsDialogs.InsertImageDialog.ShowInsertFromWebSection = false;
                RecargaGrid();

                if (!IsPostBack)
                {

                    iIdCorreo = Page.Request["Id"].S().I();
                    if (eSearchCorreo != null && iIdCorreo != 0)
                        eSearchCorreo(iIdCorreo, null);

                    if (eSearchObj != null)
                        eSearchObj(null, null);
                }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso"); }
        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                 ViewState["Status"] = "2";
                if (eSaveObj != null)
                    eSaveObj(null, null);

                EnvioCorreo();

                LimpiaControles();

                Response.Redirect("~/Views/AtencionClientes/frmCorreosMasivos.aspx", false);
            }
            catch (Exception ex) 
            { 
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnEnviar_Click", "Aviso"); 
            }
        }
        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
            new object[] { sender as UpdatePanel });
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvcontactos.Selection.Count > 0)
                {

                    List<object> fieldValues = gvcontactos.GetSelectedFieldValues(new string[] { "CorreoElectronico" });

                    if (fieldValues.Count > 0)
                    {
                        string correo = string.Empty;
                        foreach (var idbitacora in fieldValues)
                        {

                            if (correo == string.Empty)
                                correo = idbitacora.S();
                            else
                                correo = correo + "," + idbitacora;
                        }

                        if (ViewState["P/c"].S() == "1")
                        {
                            txtPara.Text = correo;
                            txtPara.ErrorText = "";
                        }
                        else if (ViewState["P/c"].S() == "2")
                        {
                            txtCc.Text = correo;
                            txtCc.ErrorText = "";
                        }
                    }

                    gvcontactos.Selection.UnselectAll();
                }
                else
                {
                    MostrarMensaje("Se deben seleccionar contactos.", "Mensaje");
                }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAceptar_Click", "Aviso"); }
        }
        protected void btnCancelaP_Click(object sender, EventArgs e)
        {

        }
        protected void btnSeleccioneP_Click(object sender, EventArgs e)
        {
            try
            {
                ppContactos.ShowOnPageLoad = true;
                ViewState["P/c"] = "1";
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSeleccioneP_Click", "Aviso"); }
        }
        protected void btnSeleccionCc_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, null);

                ppContactos.ShowOnPageLoad = true;
                ViewState["P/c"] = "2";
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSeleccionCc_Click", "Aviso"); }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["Status"] = "1";
                if (eSaveObj != null)
                    eSaveObj(null, null);

                LimpiaControles();
                Response.Redirect("~/Views/AtencionClientes/frmCorreosMasivos.aspx", false);
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardar_Click", "Aviso"); }
        }
        protected void chNombre_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chNombre.Checked)
                {
                    DemoHtmlEditor.Html = DemoHtmlEditor.Html + " [NOMBRE]";
                }
            }
            catch (Exception x) { throw x; }
        }
        protected void chContrato_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chContrato.Checked)
                {
                    DemoHtmlEditor.Html = DemoHtmlEditor.Html + " [CLIENTE]";
                }
            }
            catch (Exception x) { throw x; }
        }
        protected void chRazonSocial_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chRazonSocial.Checked)
                {
                    DemoHtmlEditor.Html = DemoHtmlEditor.Html + " [RAZONSOCIAL]";
                }
            }
            catch (Exception x) { throw x; }
        }

        #endregion

        #region Metodos
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
        public void LoadContactos(DataTable dtObjCat)
        {
            try
            {
                if (dtObjCat != null)
                {
                    gvcontactos.DataSource = dtObjCat;
                    gvcontactos.DataBind();

                    ViewState["gvcontactos"] = dtObjCat;
                }
            }
            catch (Exception x) { throw x; }
        }
        protected void RecargaGrid()
        {
            try
            {
                if ((DataTable)ViewState["gvcontactos"] != null)
                {
                    gvcontactos.DataSource = (DataTable)ViewState["gvcontactos"];
                    gvcontactos.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        protected void LimpiaControles()
        {
            try
            {
                txtMotivo.Text = "";
                txtAsunto.Text = "";
                txtPara.Text = "";
                txtCc.Text = "";
                DemoHtmlEditor.Html = "";
                gvcontactos.Selection.UnselectAll();
            }
            catch (Exception x) { throw x; }
        }
        protected void EnvioCorreo()
            {
            try
            {
                lstErrores = new List<string>();
                objImagenesCorreo = new List<ImagenCorreo>();
                string Motivo = txtMotivo.Text;
                string Asunto = txtAsunto.Text;

                string[] sContactoPara = txtPara.Text.Split(',');

                string scorreo = Utils.ObtieneParametroPorClave("4");
                string sPass = Utils.ObtieneParametroPorClave("5");
                string sservidor = Utils.ObtieneParametroPorClave("6");
                string spuerto = Utils.ObtieneParametroPorClave("7");
                string sContactosCopia = txtCc.Text;
                foreach (var sContacto in sContactoPara)
                {
                    string sMensaje = GetBodyHtml(DemoHtmlEditor.Html);
                    DataTable dt = (DataTable)ViewState["gvcontactos"];
                    DataRow dr = dt.Select("CorreoElectronico = '" + sContacto + "'").FirstOrDefault();
                    if (dr != null)
                    {
                        sMensaje = sMensaje.Replace("[NOMBRE]", dr["Nombre"].S()).Replace("[CLIENTE]", dr["CodigoCliente"].S()).Replace("[RAZONSOCIAL]", dr["RazonSocial"].S());
                    }
                    try
                    {
                        Utils.EnvioCorreo(sservidor, spuerto.S().I(), Asunto, sContacto, sMensaje, "", scorreo, sPass, "", Motivo,objImagenesCorreo);
                    }
                    catch (Exception ex) 
                    {
                        lstErrores.Add(Utils.SaveErrorEnBitacora(ex.Message, sPagina, sClase, "btnEnviar_Click")); 
                    }

                    sContactosCopia = sContactosCopia.ToString().Replace(sContacto, " ");
                }

                if (sContactosCopia.ToString() != string.Empty)
                {
                    string[] sContactosConCopias = sContactosCopia.ToString().Split(',');
                    foreach (string sContactoCopia in sContactosConCopias)
                    {
                        string sMensaje = GetBodyHtml(DemoHtmlEditor.Html);

                        DataTable dt = (DataTable)ViewState["gvcontactos"];
                        DataRow dr = dt.Select("CorreoElectronico = '" + sContactoCopia + "'").FirstOrDefault();
                        if (dr != null)
                        {
                            sMensaje = sMensaje.Replace("[NOMBRE]", dr["Nombre"].S()).Replace("[[CLIENTE]]", dr["ClaveContrato"].S()).Replace("[RAZONSOCIAL]", dr["RazonSocial"].S());
                        }
                        try
                        {
                            Utils.EnvioCorreo(sservidor, spuerto.S().I(), Asunto, "ejemplo@ejemplo.com", sMensaje, "", scorreo, sPass, sContactoCopia, Motivo, objImagenesCorreo);
                        }
                        catch (Exception ex) 
                        {
                            lstErrores.Add(Utils.SaveErrorEnBitacora(ex.Message, sPagina, sClase, "btnEnviar_Click")); 
                        }
                    }
                }
                if(lstErrores.Count() > 0)
                {
                    mpeMensaje.ShowMessage(lstErrores.ToString(), "Aviso");
                }
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }
        public void LoadCorreo(DataTable dtObjCat)
        {
            try
            {
                txtMotivo.Text = dtObjCat.Rows[0]["Motivo"].S();
                txtAsunto.Text = dtObjCat.Rows[0]["Asunto"].S();
                txtPara.Text = dtObjCat.Rows[0]["Destinatarios"].S();
                txtCc.Text = dtObjCat.Rows[0]["Copiados"].S();
                DemoHtmlEditor.Html = dtObjCat.Rows[0]["Contenido"].S();
            }
            catch (Exception x) { throw x; }
        }

        private string GetBodyHtml(string htmlString)
        {
            try
            {
                objImagenesCorreo = new List<ImagenCorreo>();
                Regex rgx = new Regex(@"<(img)\b[^>]*>", RegexOptions.IgnoreCase);
                MatchCollection matches = rgx.Matches(htmlString);
                List<ImagenCorreo> lstImagenes = new List<ImagenCorreo>();
                ImagenCorreo objImagen = new ImagenCorreo();
                string img;
                for (int i = 0, l = matches.Count; i < l; i++)
                {
                    objImagen = GetImageName(matches[i].Value);
                    string imgName = objImagen.sClaveImagen;
                    img = string.Format("<img src=\"cid:{0}\">", imgName);
                    htmlString = htmlString.Replace(matches[i].Value, img);
                    lstImagenes.Add(objImagen);
                }
                objImagenesCorreo = lstImagenes;
                return htmlString;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ImagenCorreo GetImageName(string imgSource)
        {
            try
            {
                ImagenCorreo objImagen = new ImagenCorreo();
                string src = XElement.Parse(imgSource).Attribute("src").Value;
                objImagen.sClaveImagen = Path.GetFileNameWithoutExtension(MapPath(src));
                objImagen.sRutaIagen = MapPath(src);
                return objImagen;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Variables
        private const string sClase = "frmCorreoM.aspx.cs";
        private const string sPagina = "frmCorreoM.aspx";

        CorreoM_Presenter oPresenter;

        public CorreoM oCorreo
        {
            get
            {
                CorreoM oC = new CorreoM();
                oC.sMotivo = txtMotivo.Text;
                oC.sAsunto = txtAsunto.Text;
                oC.sDestinatarios = txtPara.Text;
                oC.sCopiados = txtCc.Text;
                oC.sContenido = DemoHtmlEditor.Html.ToString();
                oC.iStatus = ViewState["Status"].S().I();
                oC.iIdCorreo = iIdCorreo;
                return oC;
            }
        }

        List<ImagenCorreo> objImagenesCorreo
        {
            get
            {
                return (List<ImagenCorreo>)ViewState["objImagenesCorreo"];
            }
            set
            {
                ViewState["objImagenesCorreo"] = value;
            }
        }
        List<string> lstErrores
        {
            get
            {
                return (List<string>)ViewState["lstErrores"];
            }
            set
            {
                ViewState["lstErrores"] = value;
            }
        }

        public int iIdCorreo
        {
            get { return ViewState["iIdCorreo"].S().I(); }
            set { ViewState["iIdCorreo"] = value; }
        }

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchCorreo;
        #endregion

    }
}