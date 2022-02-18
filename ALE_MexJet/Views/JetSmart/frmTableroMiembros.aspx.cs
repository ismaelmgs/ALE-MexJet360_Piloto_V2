using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ALE_MexJet.Clases;
using System.Reflection;
using System.Data;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using DevExpress.Web;
using ALE_MexJet.Objetos;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ALE_MexJet.Views.JetSmart
{
    public partial class frmTableroMiembros : System.Web.UI.Page, IViewAutorizacionMembresias
    {

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new AutorizacionMembresias_Presenter(this, new DBAutorizacionMembresias());

            if (!IsPostBack)
            {
                if (eObjSelected != null)
                    eObjSelected(sender, e);
            }

            if (eSearchObj != null)
                eSearchObj(sender, e);
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase,"btnBusqueda_Click", "Aviso");
            }
        }
        protected void upGv_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                ddlMembresiaM.DataSource = dtContratos;
                ddlMembresiaM.ValueField = "IdContrato";
                ddlMembresiaM.TextField = "ClaveContrato";
                ddlMembresiaM.DataBind();

                ddlSubscripcion.DataSource = dtTiposClientes;
                ddlSubscripcion.ValueField = "IdTipoPaquete";
                ddlSubscripcion.TextField = "Descripcion";
                ddlSubscripcion.DataBind();
                    
                ppEditarMiembro.ShowOnPageLoad = true;

                ASPxButton boton = (ASPxButton)sender;
                iIdMiembro = boton.CommandArgument.S().I();

                DataRow[] rows = dtMiembros.Select("IdMiembro = " + iIdMiembro.S());
                if (rows.Length > 0)
                {
                    ddlMembresiaM.Value = rows[0]["IdMembresia"].S();
                    txtNombreM.Text = rows[0]["Nombre"].S();
                    txtCorreoM.Text = rows[0]["CorreoElectronico"].S();
                    txtTelefonoM.Text = rows[0]["Telefono"].S();
                    txtTelefonoMo.Text = rows[0]["TelefonoMovil"].S();
                    ddlSubscripcion.Value = rows[0]["subscripcion"].S();
                    chkDescuenta.Checked = rows[0]["descuentahoras"].S() == "1" ? true : false;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnEditar_Click", "Aviso");
            }
        }
        protected void ddlMembresiaM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = dtContratos.Select("ClaveContrato = '" + ddlMembresiaM.Text.S() + "'");
                if(rows.Length > 0)
                    lblRazonSocial.Text = rows[0]["RazonSocial"].S();
                else
                    lblRazonSocial.Text = "[No se encontró la razón social]";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlMembresiaM_SelectedIndexChanged", "Aviso");
            }
        }
        protected void btnAceptarM_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSaveObj != null)
                    eSaveObj(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAceptarM_Click", "Aviso");
            }
        }
        protected void btnAcepRec_Click(object sender, EventArgs e)
        {
            try
            {
                //JavaScriptSerializer ser = new JavaScriptSerializer();
                //string sToken = ObtieneToken();
                //Token oToken = ser.Deserialize<Token>(sToken);
                //string sTok = oToken.token;

                string sTok = Utils.ObtieneTokenAppMexJet();

                ASPxButton boton = (ASPxButton)sender;
                iIdMiembro = boton.CommandArgument.S().I();
                DataRow[] rows = dtMiembros.Select("IdMiembro = " + iIdMiembro.S());
                if (rows.Length > 0)
                {
                    if (rows[0]["Membresia"].S() == "(NO ASIGNADO)")
                    {
                        mpeMensaje.ShowMessage("No se puede aprobar a una persona si no tiene membresia, favor de verificar", "Aviso");
                        return;
                    }
                    else if (rows[0]["subscripcion"].S() == string.Empty)
                    {
                        mpeMensaje.ShowMessage("No se puede aprobar a una persona si no tiene subscripción, favor de verificar", "Aviso");
                        return;
                    }
                    else
                    {
                        if (ConfirmaUsuario("Aprobado", sender, sTok))
                        {
                            iIdStatus = 2;

                            if (eNewObj != null)
                                eNewObj(sender, e);

                            if (eSearchObj != null)
                                eSearchObj(sender, e);

                            MostrarMensaje("Se aprobó el usuario correctamente.", "Aviso");
                        }
                    }
                }

                
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAcepRec_Click", "Aviso");
            }
        }
        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                string sTok = Utils.ObtieneTokenAppMexJet();

                if (RechazaUsuario("Rechazado", sender, sTok))
                {
                    iIdStatus = 0;

                    if (eNewObj != null)
                        eNewObj(sender, e);

                    if (eSearchObj != null)
                        eSearchObj(sender, e);

                    MostrarMensaje("Se rechazó el usuario correctamente.", "Aviso");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region METODOS
        public void LoadObjects(DataTable dtObjCat)
        {
            dtMiembros = dtObjCat;
            gvMiembros.DataSource = dtObjCat;
            gvMiembros.DataBind();
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            mpeMensaje.ShowMessage(sMensaje, sCaption);
        }
        private bool ConfirmaUsuario(string sStatus, object sender, string sToken)
        {
            try
            {
                bool ban = false;

                ASPxButton boton = (ASPxButton)sender;
                iIdMiembro = boton.CommandArgument.S().I();
                DataRow[] rows = dtMiembros.Select("IdMiembro = " + iIdMiembro.S());
                if (rows.Length > 0)
                {
                    ConfirmacionMiembro oC = new ConfirmacionMiembro();
                    oC.nombre = rows[0]["Nombre"].S();
                    oC.telefono = rows[0]["Telefono"].S();
                    oC.estatus = sStatus;
                    oC.membresia = rows[0]["Membresia"].S();
                    oC.tipoMembresia = rows[0]["subscripcion"].S();
                    oC.descuentaHoras = rows[0]["descuentahoras"].S();

                    string sCad = JsonConvert.SerializeObject(oC).ToString();
                    string url = Utils.ObtieneParametroPorClave("118");
                    string sPathWSApp = url + rows[0]["urlactualizacion"].S() + ".json";
                                         
                    // Enviamos el JSON al proveedor

                    //if (HttpPatch(sPathWSApp, sCad, sToken))
                    //    ban = true;

                    ban = new UtilsServicios().ApruebaRechazaMembresia(sToken, sPathWSApp, sCad, 1);

                    //PostAsync(sToken, sPathWSApp, sCad);
                }

                return ban;
            }
            catch (Exception) 
            {

                throw;
            }
        }

        private bool RechazaUsuario(string sStatus, object sender, string sToken)
        {
            try
            {
                bool ban = false;

                ASPxButton boton = (ASPxButton)sender;
                iIdMiembro = boton.CommandArgument.S().I();
                DataRow[] rows = dtMiembros.Select("IdMiembro = " + iIdMiembro.S());
                if (rows.Length > 0)
                {
                    RechazaUsuario oC = new RechazaUsuario();
                    oC.tipoMembresia = "Usuario rechazado";
                    oC.membresia = null;
                    oC.estatus = "Rechazado";

                    string sCad = JsonConvert.SerializeObject(oC).ToString();
                    string url = Utils.ObtieneParametroPorClave("118");
                    string sPathWSApp = url + rows[0]["urlactualizacion"].S() + ".json";

                    // Enviamos el JSON al proveedor
                    ban = new UtilsServicios().ApruebaRechazaMembresia(sToken, sPathWSApp, sCad, 0);
                }

                return ban;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string ObtieneToken()
        {
            string sToken = string.Empty;
            string url = "https://mexjet.chudro.com/api/rest-ws/firm-token";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new HttpClient())
            {
                var request = new DatosToken()
                {
                    privateKey = "1Pk0tlR1xcO2kinPeRZ5"
                };

                string sPathWSApp = url;

                var response = client.PostAsync(sPathWSApp,
                    new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    sToken = response.Content.ReadAsStringAsync().Result.S();
                }
            }

            return sToken;
        }

        //public bool ObtieneToken(string url, string content)
        //{
        //    //string contentType = "application / x-www-form-urlencoded";
        //    string contentType = "x-www-form-urlencoded";

        //    DatosToken oTk = new DatosToken();
        //    oTk.privateKey = "1Pk0tlR1xcO2kinPeRZ5";
        //    content = JsonConvert.SerializeObject(oTk).ToString();

        //    url = "https://mexjet.chudro.com/api/rest-ws/firm-token";


        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    var request = (HttpWebRequest)WebRequest.Create(Uri.EscapeUriString(url));
        //    if (request == null)
        //        throw new ApplicationException(string.Format("Could not create the httprequest from the url:{0}", url));

        //    request.Method = "POST"; // "PATCH";
        //    //request.Headers.Add("privateKey", "1Pk0tlR1xcO2kinPeRZ5");


        //    UTF8Encoding encoding = new UTF8Encoding();
        //    var byteArray = Encoding.ASCII.GetBytes(content);

        //    request.ContentLength = byteArray.Length;
        //    request.ContentType = contentType;

        //    Stream dataStream = request.GetRequestStream();
        //    dataStream.Write(byteArray, 0, byteArray.Length);
        //    dataStream.Close();

        //    try
        //    {
        //        var response = (HttpWebResponse)request.GetResponse();
        //        return response.StatusCode.S() == "OK" ? true : false;
        //    }
        //    catch (WebException ex)
        //    {
        //        return false;
        //    }
        //}

        public void PostAsync(string token, string url, string content)
        {
            byte[] data = Encoding.UTF8.GetBytes(content);
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Bearer " + token);
            request.ContentLength = data.Length;


            Stream dataStream = request.GetRequestStream();
            dataStream.Write(data, 0, data.Length);
            dataStream.Close();

            try
            {
                WebResponse response = (HttpWebResponse) request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseContent = reader.ReadToEnd();
                    JObject adResponse =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(responseContent);
                    
                }
                //if (response.StatusCode.S() == "OK")
                //{

                //}
            }
            catch (WebException ex)
            {
                string sError = ex.Message;
            }

            //using (Stream stream = request.GetRequestStream())
            //{
            //    stream.Write(data, 0, data.Length);
            //}

            //try
            //{
            //    WebResponse response = await request.GetResponseAsync();
            //    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            //    {
            //        string responseContent = reader.ReadToEnd();
            //        JObject adResponse =
            //            Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(responseContent);
            //        return adResponse;
            //    }
            //}
            //catch (WebException webException)
            //{
            //    if (webException.Response != null)
            //    {
            //        using (StreamReader reader = new StreamReader(webException.Response.GetResponseStream()))
            //        {
            //            string responseContent = reader.ReadToEnd();
            //            return Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(responseContent); ;
            //        }
            //    }
            //}

            //return null;
        }

        public bool HttpPatch(string url, string content, string sToken, string contentType = "application/x-www-form-urlencoded")
        {
            contentType = "application/json";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var request = (HttpWebRequest)WebRequest.Create(Uri.EscapeUriString(url));
            if (request == null)
                throw new ApplicationException(string.Format("Could not create the httprequest from the url:{0}", url));

            request.Method = "POST"; // "PATCH";

            UTF8Encoding encoding = new UTF8Encoding();
            var byteArray = Encoding.ASCII.GetBytes(content);

            request.ContentLength = byteArray.Length;
            request.ContentType = contentType;
            request.Headers.Add("Authorization", "Bearer " + sToken);

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                return response.StatusCode.S() == "OK" ? true : false;
            }
            catch (WebException ex)
            {
                return false;
            }
        }


        #endregion

        #region VARIABLES Y PROPIEDADES
        AutorizacionMembresias_Presenter oPresenter;

        private const string sPagina = "frmTableroMiembros.aspx";
        private const string sClase = "frmTableroMiembros.aspx.cs";

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;


        public MiembroEZ oMiembro
        {
            get
            {
                MiembroEZ oM = new MiembroEZ();
                oM.iIdMembresia = ddlMembresiaM.Value.S().I();
                oM.iIdMiembro = iIdMiembro;
                oM.sNombre = txtNombreM.Text.S();
                oM.sCorreoElectronico = txtCorreoM.Text.S();
                oM.sTelefono = txtTelefonoM.Text.S();
                oM.sTelefonoMovil = txtTelefonoMo.Text.S();
                oM.sSubscripcion = ddlSubscripcion.Text.S();
                oM.idescuentahoras = chkDescuenta.Checked ? 1 : 0;

                return oM;
            }
        }
        public int iIdStatus
        {
            get { return (int)ViewState["VSiIdStatus"]; }
            set { ViewState["VSiIdStatus"] = value; }
        }
        public object[] oArrFiltros
        {
            get
            {
                return new object[] {
                                        "@Status",rblFiltro.Value.S().I(),
                                        "@FechaInicial" , dFechaIni.Value.S() != string.Empty ? dFechaIni.Value.S().Dt().Year.S() + "-" + dFechaIni.Value.S().Dt().Month.S().PadLeft(2,'0') + "-" + dFechaIni.Value.S().Dt().Day.S().PadLeft(2,'0') : string.Empty,
                                        "@FechaFinal", dFechaFin.Value.S() != string.Empty ? dFechaFin.Value.S().Dt().Year.S() + "-" + dFechaFin.Value.S().Dt().Month.S().PadLeft(2,'0') + "-" + dFechaFin.Value.S().Dt().Day.S().PadLeft(2,'0') : string.Empty,
                                    };
            }
        }
        public int iIdMiembro
        {
            get { return (int)ViewState["VSIdMiembro"]; }
            set { ViewState["VSIdMiembro"] = value; }
        }
        public DataTable dtMiembros
        {
            get { return (DataTable)ViewState["VSMiembros"]; }
            set { ViewState["VSMiembros"] = value; }
        }
        public DataTable dtTiposClientes
        {
            get { return (DataTable)ViewState["VSTiposClientes"]; }
            set { ViewState["VSTiposClientes"] = value; }
        }
        public DataTable dtContratos
        {
            get { return (DataTable)ViewState["VSContratos"]; }
            set { ViewState["VSContratos"] = value; }
        }
        #endregion

    }

    public class ConfirmacionMiembro
    {
        private string _nombre = string.Empty;
        private string _telefono = string.Empty;
        private string _estatus = string.Empty;
        private string _membresia = string.Empty;
        private string _tipoMembresia = string.Empty;
        private string _descuentaHoras = string.Empty;



        public string nombre { get { return _nombre; } set { _nombre = value; } }
        public string telefono { get { return _telefono; } set { _telefono = value; } }
        public string estatus { get { return _estatus; } set { _estatus = value; } }
        public string membresia { get { return _membresia; } set { _membresia = value; } }
        public string tipoMembresia { get { return _tipoMembresia; } set { _tipoMembresia = value; } }
        public string descuentaHoras { get { return _descuentaHoras; } set { _descuentaHoras = value; } }
    }

    public class RechazaUsuario
    {
        private string _tipoMembresia = string.Empty;
        private string _membresia = string.Empty;
        private string _estatus = string.Empty;

        public string tipoMembresia { get { return _tipoMembresia; } set { _tipoMembresia = value; } }
        public string membresia { get { return _membresia; } set { _membresia = value; } }
        public string estatus { get { return _estatus; } set { _estatus = value; } }
    }

    
}