using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ALE_MexJet.Presenter;
using ALE_MexJet.DomainModel;
using System.Data;
using System.Reflection;
using ALE_MexJet.Clases;
using DevExpress.Web;
using System.IO;
using System.Web.UI.HtmlControls;
using ALE_MexJet.Objetos;
using System.Net;
using Newtonsoft.Json;
using System.Xml;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Threading;
using System.Drawing;


namespace ALE_MexJet.Views.AtencionClientes
{
    public partial class frmTripGuide : System.Web.UI.Page, IViewTripGuide, IViewTripGuideFPK
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            mpeMensaje.OkButtonPressed += mpeMensaje_OkButtonPressed;

            oPresenter = new TripGuide_Presenter(this, new DBTripGuide());
            oPresenterFPK = new TripGuideFPK_Presenter(this, new DBTripGuideFPK());

            if (!IsPostBack)
            {
                string sTrip = Request.QueryString["iIdTrip"];

                if (!string.IsNullOrEmpty(sTrip))
                {
                    iTrip = sTrip.S().I();

                    lblTrip2.Text = iTrip.S();
                }

                if (!string.IsNullOrEmpty(Request.QueryString["IdSolicitud"]))
                {
                    iIdSolicitud = Request.QueryString["IdSolicitud"].S().I();

                    lblSolicitud2.Text = iIdSolicitud.S();

                }

                if (eSearchLegsByTrip != null)
                    eSearchLegsByTrip(sender, e);
            }
        }

        protected void btnGenerarTripGuide_Click(object sender, EventArgs e)
        {
            try
            {
                GeneraTRIP();

            }
            catch (Exception ex)
            {

                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGenerarTripGuide_Click", "TripGuide");
            }
        }

        protected void upaTripGuide_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "upaTripGuide_Unload", "Aviso");
            }
        }

        protected void upaModal_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "upaModal_Unload", "Aviso");
            }
        }

        void mpeMensaje_OkButtonPressed(object sender, EventArgs args)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "OcultaError", "OcultaError();", true);
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            RegresaSolicitudVuelo();
        }

        protected void Enviar_Click(object sender, EventArgs e)
        {
            ppEnviarMailTripGuide.ShowOnPageLoad = true;

            txtPara.Text = string.Empty;
            txtConCopia.Text = string.Empty;

            txtPara.Text = dtContacto.Rows[0]["CorreoElectronico"].ToString();

            if (eConsultaVendedorSolicitud != null)
                eConsultaVendedorSolicitud(null, null);

        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            ppGuardarTripGuide.ShowOnPageLoad = true;

        }

        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {

            EnviarMail();

        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            RegresaSolicitudVuelo();
        }

        protected void btnGuardarTripGuide_Click(object sender, EventArgs e)
        {
            string htmlText = hdTablaTripGuideHtml["ValorHtml"].ToString();

            sObservaciones = memoObservacionesGuardar.Text;

            htmlText = ParsearHTML(htmlText);

            bPDF = Utils.ObtenerPDF(htmlText);

            sNombreArchivo = sNombreArchivo + iPierna.S();

            if (eGuardaTripGuide != null)
                eGuardaTripGuide(null, EventArgs.Empty);

            RegresaSolicitudVuelo();
        }

        #endregion

        #region METODOs

        public void ObtieneDatosTrip(DataTable dtTrip)
        {
            //lblSolicitante2En.Text = lblSolicitante2Es.Text = sNombreContacto = dtTrip.Rows[0]["reqname"].S();
            lblregistro2En.Text = lblregistro2Es.Text = dtTrip.Rows[0]["desc"].S();

            string Tipo = dtTrip.Rows[0]["type_code"].S();

            lblTipo2En.Text = lblTipo2Es.Text = sAeronave = ObtieneDescripcionAeronave(Tipo);

            imgAeronaveEn.ImageUrl = "../../img/iconos/" + ObtieneImagenAeronave(Tipo) + ".jpg";

            imgAeronaveEs.ImageUrl = "../../img/iconos/" + ObtieneImagenAeronave(Tipo) + ".jpg";

            //lblSuContacto2En.Text = lblSuContacto2Es.Text = dtTrip.Rows[0]["reqphone"].S();
        }

        public void CargaPiernas(DataTable dtPiernas)
        {
            try
            {
                gvPiernas.DataSource = dtPiernas;
                gvPiernas.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeneraTRIP()
        {
            try
            {
                pnlTripGuide.Visible = false;

                ObtieneIdPierna();

                if (iPierna > 0)
                {

                    hdBanderaTripGuideHtml["hidden_value"] = "1";
                    hdTablaTripGuideHtml["ValorHtml"] = string.Empty;

                    if (eConsultaContactoSolicitud != null)
                        eConsultaContactoSolicitud(null, null);

                    if (eBuscaDatosTrip != null)
                        eBuscaDatosTrip(null, null);

                    if (eBuscaDetallePierna != null)
                        eBuscaDetallePierna(null, null);
                }
                else
                {
                    mpeMensaje.ShowMessage("Para generar un TripGuide, debe seleccionar una pierna.", "Aviso");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ObtieneIdPierna()
        {
            foreach (GridViewRow row in gvPiernas.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    RadioButton rb = (RadioButton)row.FindControl("rbSeleccione");
                    if (rb != null)
                    {
                        if (rb.Checked)
                        {
                            iPierna = row.Cells[1].Text.S().I();

                            break;
                        }
                    }
                }
            }
        }

        public void CargaDetallePierna(DataTable dtDetallePierna)
        {
            try
            {
                if (dtDetallePierna.Rows.Count > 0)
                {

                    lblSolicitante2En.Text = lblSolicitante2Es.Text = sNombreContacto = dtContacto.Rows[0]["Nombre"].ToString();

                    string SuContactoTelOficina = dtContacto.Rows[0]["TelOficina"].ToString();
                    string SuContactoTelMovil = dtContacto.Rows[0]["TelOficina"].ToString();

                    //if (!string.IsNullOrEmpty(SuContactoTelOficina) && !string.IsNullOrEmpty(SuContactoTelMovil))
                    //{
                    //    lblSuContacto2En.Text = lblSuContacto2Es.Text = "Oficina: " + SuContactoTelOficina + " / Movil:" + SuContactoTelMovil;
                    //}
                    //else if (!string.IsNullOrEmpty(SuContactoTelOficina) && string.IsNullOrEmpty(SuContactoTelMovil))
                    //{
                    //    lblSuContacto2En.Text = lblSuContacto2Es.Text = SuContactoTelOficina;
                    //}
                    //else if (string.IsNullOrEmpty(SuContactoTelOficina) && !string.IsNullOrEmpty(SuContactoTelMovil))
                    //{
                    //    lblSuContacto2En.Text = lblSuContacto2Es.Text = SuContactoTelMovil;
                    //}

                    DateTime FecheActual = DateTime.Today;

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    lblFechaActualEn.Text = "Date: " + FecheActual.ToLongDateString();

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
                    lblFechaActualEs.Text = "Fecha: " + FecheActual.ToLongDateString();

                    lblDistancia2En.Text = lblDistancia2Es.Text = dtDetallePierna.Rows[0]["Distancia"].S() + " Km. ";

                    double tiempoDecimal = double.Parse(dtDetallePierna.Rows[0]["TiempoEstimado"].ToString());

                    TimeSpan timespan = TimeSpan.FromHours(tiempoDecimal);

                    string HoraMinutos = timespan.ToString("h\\:mm");

                    lblTiempoVueloEn.Text = lblTiempoVueloEs.Text = HoraMinutos;

                    DtFechaSalida = (DateTime)dtDetallePierna.Rows[0]["HoraOrigenLocal"];

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    lblFechaSalida2En.Text = DtFechaSalida.ToLongDateString() + " at " + DtFechaSalida.ToString("HH:mm") + " hrs.";

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
                    lblFechaSalida2Es.Text = DtFechaSalida.ToLongDateString() + " a las " + DtFechaSalida.ToString("HH:mm") + " hrs.";

                    lblOrigen2En.Text = lblOrigen2Es.Text = sNombreAeropuertoOrigen = dtDetallePierna.Rows[0]["AeropuertoOrigenNombre"].S() + " " + dtDetallePierna.Rows[0]["AeropuertoOrigenCiudad"].S() + " " + dtDetallePierna.Rows[0]["AeropuertoOrigenEstado"].S();
                    lblFBOSalida2En.Text = lblFBOSalida2Es.Text = dtDetallePierna.Rows[0]["FBOOrigenNombre"].S();

                    DtFechaLlegada = (DateTime)dtDetallePierna.Rows[0]["HoraDestinoLocal"];

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    lblFechallegada2En.Text = DtFechaLlegada.ToLongDateString() + ", at " + DtFechaLlegada.ToString("HH:mm") + " hrs.";

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
                    lblFechallegada2Es.Text = DtFechaLlegada.ToLongDateString() + ", a las " + DtFechaLlegada.ToString("HH:mm") + " hrs.";

                    lblDestino2En.Text = lblDestino2Es.Text = sNombreAeropuertoDestino = dtDetallePierna.Rows[0]["AeropuertoDestinoNombre"].S() + " " + dtDetallePierna.Rows[0]["AeropuertoDestinoCiudad"].S() + " " + dtDetallePierna.Rows[0]["AeropuertoDestinoEstado"].S();
                    lblFBOLlegada2En.Text = lblFBOLlegada2Es.Text = dtDetallePierna.Rows[0]["FBODestinoNombre"].S();

                    lblNoPassangerEn.Text = lblNoPassangerEs.Text = sNumeroPasajero = dtDetallePierna.Rows[0]["TotalPasajeros"].S();
                    lblCapitan2En.Text = lblCapitan2Es.Text = sPiloto = dtDetallePierna.Rows[0]["PilotoNombre"].S() + " " + dtDetallePierna.Rows[0]["PilotoApellido"].S();
                    lblCapitanTelNum2En.Text = lblCapitanTelNum2Es.Text = sPilotoTelefono = dtDetallePierna.Rows[0]["PilotoTelefono"].S();
                    lblCopiloto2En.Text = lblCopiloto2Es.Text = sCoPiloto = dtDetallePierna.Rows[0]["CopilotoNombre"].S() + " " + dtDetallePierna.Rows[0]["CopilotoApellido"].S();
                    lblCopiloto2NumEn.Text = lblCopiloto2NumEs.Text = sCoPilotoTelefono = dtDetallePierna.Rows[0]["CopilotoTelefono"].S();
                    lblComisariato2En.Text = lblComisariato2Es.Text = dtDetallePierna.Rows[0]["Comisariato"].S();
                    lblTransportacion2En.Text = lblTransportacion2Es.Text = dtDetallePierna.Rows[0]["Transportacion"].S();

                    sICAOOrigen = dtDetallePierna.Rows[0]["aeropuertoorigenicao"].S();

                    if (eBuscaCoordenadaOrigen != null)
                        eBuscaCoordenadaOrigen(null, null);

                    sICAODestino = dtDetallePierna.Rows[0]["aeropuertodestinoicao"].S();

                    if (eBuscaCoordenadaDestino != null)
                        eBuscaCoordenadaDestino(null, null);

                    if (eBuscaPasajero != null)
                        eBuscaPasajero(null, null);

                    //dtDetallePierna.Rows[0]["FBOOrigenDireccion"].S();
                    //dtDetallePierna.Rows[0]["FBOOrigenCiudad"].S();
                    //dtDetallePierna.Rows[0]["FBOOrigenEstado"].S();
                    //dtDetallePierna.Rows[0]["FBOOrigenCodigoPostal"].S();

                    //dtDetallePierna.Rows[0]["HoraOrigenUTC"].S();
                    //dtDetallePierna.Rows[0]["HoraDestinoUTC"].S();
                    //dtDetallePierna.Rows[0]["HoraOrigenHome"].S();
                    //dtDetallePierna.Rows[0]["HoraDestinoHome"].S();

                    //dtDetallePierna.Rows[0]["FBODestinoDireccion"].S();
                    //dtDetallePierna.Rows[0]["FBODestinoCiudad"].S();
                    //dtDetallePierna.Rows[0]["FBODestinoEstado"].S();
                    //dtDetallePierna.Rows[0]["FBODestinoCodigoPostal"].S();

                    MuestraMapa();

                    MuestraClimaEn();

                    MuestraClimaEs();

                    //MuestraRestauranteEn();

                    //MuestraRestauranteEs();

                    //MuestraTopActivitiesEn();

                    //MuestraTopActivitiesEs();

                    pnlTripGuide.Visible = true;

                    Enviar.Enabled = true;
                    Guardar.Enabled = true;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ObtienePasajeros(DataTable dtPasajero)
        {
            if (dtPasajero.Rows.Count > 0)
            {
                StringBuilder cadenaHtml = new StringBuilder();

                string NombrePasajero = string.Empty;

                foreach (DataRow Fila in dtPasajero.Rows)
                {
                    NombrePasajero = Fila["first_name"].S() + " " + Fila["last_name"].S();

                    cadenaHtml.Append("<tr><td style=\"font:14px arial; color:Black;\"><ul><li>" + NombrePasajero + ".</li></ul></td></tr>");
                }

                tblListaPasajerosEn.InnerHtml = "<table>" + cadenaHtml.ToString() + "</table>";

                tblListaPasajerosEs.InnerHtml = "<table>" + cadenaHtml.ToString() + "</table>";
            }
        }

        private string ObtieneDescripcionAeronave(string Tipo)
        {
            string Resultado = string.Empty;

            try
            {
                switch (Tipo)
                {
                    case "4000": Resultado = "Hawker 800"; break;
                    case "525A": Resultado = "Ale_otro"; break;
                    case "560XL": Resultado = "Ale_otro"; break;
                    case "A-109": Resultado = "Agusta 109"; break;
                    case "BE-200GT": Resultado = "Ale_otro"; break;
                    case "BE-C90": Resultado = "Ale_otro"; break;
                    case "BE200": Resultado = "Hawker 400XP"; break;
                    case "BE300": Resultado = "Hawker 400XP"; break;
                    case "BE400": Resultado = "Hawker 400XP"; break;
                    case "C-560": Resultado = "Ale_otro"; break;
                    case "CESSNA 680": Resultado = "Ale_otro"; break;
                    case "CESSNA 750": Resultado = "Ale_otro"; break;
                    case "CL 601": Resultado = "Challenger 605"; break;
                    case "CL-605": Resultado = "Challenger 605"; break;
                    case "CL300": Resultado = "Challenger 605"; break;
                    case "CL604": Resultado = "Challenger 605"; break;
                    case "CL605": Resultado = "Challenger 605"; break;
                    case "DA2000": Resultado = "Ale_otro"; break;
                    case "DA50": Resultado = "Ale_otro"; break;
                    case "E-480B": Resultado = "Ale_otro"; break;
                    case "G150": Resultado = "Ale_otro"; break;
                    case "G200G": Resultado = "Ale_otro"; break;
                    case "G5": Resultado = "Ale_otro"; break;
                    case "G58": Resultado = "Ale_otro"; break;
                    case "GIV": Resultado = "Ale_otro"; break;
                    case "H1000": Resultado = "Ale_otro"; break;
                    case "HS750": Resultado = "Hawker 800"; break;
                    case "HS800": Resultado = "Hawker 800"; break;
                    case "HS900": Resultado = "Hawker 800"; break;
                    case "K100": Resultado = "Ale_otro"; break;
                    case "L75": Resultado = "Learjet 75"; break;
                    case "LR-45": Resultado = "Learjet 45"; break;
                    case "LR-60": Resultado = "Learjet 75"; break;
                    case "LR55": Resultado = "Learjet 45"; break;
                    case "MD 900": Resultado = "Ale_otro"; break;
                    case "PA-31P": Resultado = "Ale_otro"; break;
                    case "PM": Resultado = "Ale_otro"; break;
                    case "PRM1": Resultado = "Premier 1"; break;
                    case "HA420": Resultado = "HondaJet"; break;
                    default:
                        Resultado = "Ale_otro";
                        break;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Resultado;
        }

        private string ObtieneImagenAeronave(string Tipo)
        {
            string Resultado = string.Empty;

            switch (Tipo)
            {
                case "4000": Resultado = "hawker750_800xp_900xp"; break;
                case "525A": Resultado = "Ale_otro"; break;
                case "560XL": Resultado = "Ale_otro"; break;
                case "A-109": Resultado = "agusta109"; break;
                case "BE-200GT": Resultado = "Ale_otro"; break;
                case "BE-C90": Resultado = "Ale_otro"; break;
                case "BE200": Resultado = "hawker400xp"; break;
                case "BE300": Resultado = "hawker400xp"; break;
                case "BE400": Resultado = "hawker400xp"; break;
                case "C-560": Resultado = "Ale_otro"; break;
                case "CESSNA 680": Resultado = "Ale_otro"; break;
                case "CESSNA 750": Resultado = "Ale_otro"; break;
                case "CL 601": Resultado = "challenger605"; break;
                case "CL-605": Resultado = "challenger605"; break;
                case "CL300": Resultado = "challenger605"; break;
                case "CL604": Resultado = "challenger605"; break;
                case "CL605": Resultado = "challenger605"; break;
                case "DA2000": Resultado = "Ale_otro"; break;
                case "DA50": Resultado = "Ale_otro"; break;
                case "E-480B": Resultado = "Ale_otro"; break;
                case "G150": Resultado = "Ale_otro"; break;
                case "G200G": Resultado = "Ale_otro"; break;
                case "G5": Resultado = "Ale_otro"; break;
                case "G58": Resultado = "Ale_otro"; break;
                case "GIV": Resultado = "Ale_otro"; break;
                case "H1000": Resultado = "Ale_otro"; break;
                case "HS750": Resultado = "hawker750_800xp_900xp"; break;
                case "HS800": Resultado = "hawker750_800xp_900xp"; break;
                case "HS900": Resultado = "hawker750_800xp_900xp"; break;
                case "K100": Resultado = "Ale_otro"; break;
                case "L75": Resultado = "learjet75"; break;
                case "LR-45": Resultado = "learjet45"; break;
                case "LR-60": Resultado = "learjet75"; break;
                case "LR55": Resultado = "learjet45"; break;
                case "MD 900": Resultado = "Ale_otro"; break;
                case "PA-31P": Resultado = "Ale_otro"; break;
                case "PM": Resultado = "Ale_otro"; break;
                case "PRM1": Resultado = "premier"; break;
                case "HA420": Resultado = "HondaJet"; break;
                default:
                    Resultado = "Ale_otro";
                    break;
            }

            return Resultado;

        }

        public void ObtieneCoordenadaOrigen(DataTable dtCoordenada)
        {
            if (dtCoordenada.Rows.Count > 0)
            {
                fOrigenLatitud = ObtieneLatitud(dtCoordenada.Rows[0]["lat_deg"].S(), dtCoordenada.Rows[0]["lat_min"].S(), dtCoordenada.Rows[0]["lat_ns"].S());
                fOrigenLongitud = ObtieneLongitud(dtCoordenada.Rows[0]["long_deg"].S(), dtCoordenada.Rows[0]["long_min"].S(), dtCoordenada.Rows[0]["long_ew"].S());

            }
        }

        public void ObtieneCoordenadaDestino(DataTable dtCoordenada)
        {
            if (dtCoordenada.Rows.Count > 0)
            {
                fDestinoLatitud = ObtieneLatitud(dtCoordenada.Rows[0]["lat_deg"].S(), dtCoordenada.Rows[0]["lat_min"].S(), dtCoordenada.Rows[0]["lat_ns"].S());
                fDestinoLongitud = ObtieneLongitud(dtCoordenada.Rows[0]["long_deg"].S(), dtCoordenada.Rows[0]["long_min"].S(), dtCoordenada.Rows[0]["long_ew"].S());

            }
        }

        public float ObtieneLatitud(string sGrados, string sMinutos, string sDireccion)
        {
            float fResultado = 0;

            try
            {
                float fGrados = float.Parse(sGrados);
                float fMinutos = float.Parse(sMinutos);

                fResultado = (fGrados + (fMinutos / 60));

                if (sDireccion.Equals("S"))
                {
                    fResultado = fResultado * (-1);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return fResultado;
        }

        public float ObtieneLongitud(string sGrados, string sMinutos, string sDireccion)
        {
            float fResultado = 0;

            try
            {
                float fGrados = float.Parse(sGrados);
                float fMinutos = float.Parse(sMinutos);

                fResultado = (fGrados + (fMinutos / 60));

                if (sDireccion.Equals("W"))
                {
                    fResultado = fResultado * (-1);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return fResultado;
        }

        private void MuestraMapa()
        {
            try
            {
                //string RutaAlMapa = "../../Scripts/mapsEs.html";

                //RutaAlMapa = RutaAlMapa +  "?origenLat=" + fOrigenLatitud.S() + "&origenLng=" + fOrigenLongitud.S() + "&destinoLat=" + fDestinoLatitud.S() + "&destinoLng=" + fDestinoLongitud.S();

                //ifrmMapaEn.Src = RutaAlMapa;

                string RutaAlMapa = "http://maps.google.com/maps/api/staticmap?sensor=false&center=" + fDestinoLatitud.S() + "," + fDestinoLongitud.S() + "&zoom=4&size=400x210&markers=color:red|" + fDestinoLatitud.S() + "," + fDestinoLongitud.S() + "&path=color:000FF|weight:6|" + fDestinoLatitud.S() + "," + fDestinoLongitud.S() + "|" + fOrigenLatitud.S() + "," + fOrigenLongitud.S() + "&key=" + "AIzaSyDmBIHNh8g3XIr5Q1H4YxbTajCyAhypLTI";

                imgMapaEn.ImageUrl = RutaAlMapa;
                //ifrmMapaEs.Src = RutaAlMapa;

                imgMapaEs.ImageUrl = RutaAlMapa;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void MuestraClimaEn()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    int Dia = 1;

                    StringBuilder cadenaHtml = new StringBuilder();

                    //string cadenaHtml = string.Empty;

                    string URL = "http://api.openweathermap.org/data/2.5/forecast/daily?lat=" + fDestinoLatitud + "&lon=" + fDestinoLongitud + "&cnt=3&mode=json&lang=en&units=metric&APPID=526489c72c6813a24a47a0ea8b9947a6";

                    string json = wc.DownloadString(URL);

                    dynamic CadenaJson = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                    var Climas = CadenaJson.list;

                    foreach (var Clima in Climas)
                    {
                        HtmlTableRow fila = new HtmlTableRow();
                        HtmlTableCell celda = new HtmlTableCell();

                        DateTime fecha = Convert.ToDateTime(DtFechaLlegada);

                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                        fecha = fecha.AddDays(Dia);

                        string Fecha = fecha.ToLongDateString();

                        Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");

                        string Temp = "Temp. : min. " + Clima.temp.min.Value + "° to max. " + Clima.temp.max.Value + "°";

                        string Humedad = "Humidity: " + Clima.humidity.Value + "%";

                        string des = Clima.weather[0].description.Value;

                        string icon = Clima.weather[0].icon.Value;

                        string PaginaIcon = "http://www.openweathermap.org/img/w/" + icon + ".png";

                        icon = "<img src='" + PaginaIcon + "' width='25px' height='25px'></img>";

                        double viento = Clima.speed.Value;

                        cadenaHtml.Append("<tr><td style='vertical-align:top; width:50%; font-size:7px;'>" + Fecha + "<br/>" + icon + "<br/>" + des + "<br/><br/></td><td style='vertical-align:top; width:50%; font-size:7px;'>" + Temp + "<br/> Wind: " + viento.S() + " Km/hr <br/>" + Humedad + "</td></tr>");

                        Dia = Dia + 1;
                    }

                    tblClimaDestinoEn.InnerHtml = "<table>" + cadenaHtml.ToString() + "</table>";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void MuestraClimaEs()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    int Dia = 1;

                    StringBuilder cadenaHtml = new StringBuilder();

                    //string cadenaHtml = string.Empty;

                    string URL = "http://api.openweathermap.org/data/2.5/forecast/daily?lat=" + fDestinoLatitud + "&lon=" + fDestinoLongitud + "&cnt=3&mode=json&lang=sp&units=metric&APPID=526489c72c6813a24a47a0ea8b9947a6";

                    string json = wc.DownloadString(URL);

                    dynamic CadenaJson = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                    var Climas = CadenaJson.list;

                    foreach (var Clima in Climas)
                    {
                        HtmlTableRow fila = new HtmlTableRow();
                        HtmlTableCell celda = new HtmlTableCell();

                        DateTime fecha = Convert.ToDateTime(DtFechaLlegada);

                        fecha = fecha.AddDays(Dia);

                        string Fecha = fecha.ToLongDateString();

                        string Temp = "Temp. : min. " + Clima.temp.min.Value + "° a max. " + Clima.temp.max.Value + "°";

                        string Humedad = "Humedad: " + Clima.humidity.Value + "%";

                        string des = Clima.weather[0].description.Value;

                        string icon = Clima.weather[0].icon.Value;

                        string PaginaIcon = "http://www.openweathermap.org/img/w/" + icon + ".png";

                        icon = "<img src='" + PaginaIcon + "' width='25px' height='25px'></img>";

                        double viento = Clima.speed.Value;

                        cadenaHtml.Append("<tr><td style='vertical-align:top; width:50%; font-size:7px;'>" + Fecha + "<br/>" + icon + "<br/>" + des + "<br/><br/></td><td style='vertical-align:top; width:50%; font-size:7px;'>" + Temp + "<br/> Vientos: " + viento.S() + " Km/hr <br/>" + Humedad + "</td></tr>");

                        Dia = Dia + 1;
                    }

                    tblClimaDestinoEs.InnerHtml = "<table>" + cadenaHtml.ToString() + "</table>";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void MuestraRestauranteEn()
        {
            try
            {

                using (WebClient wc = new WebClient())
                {
                    int NumeroActividad = 0;

                    StringBuilder cadenaHtml = new StringBuilder();

                    string TripAdvisorAPIKey = "98cafba6a0214931a02e0cbacf8aae54";
                    string URL = "http://api.tripadvisor.com/api/partner/2.0/map/" + fDestinoLatitud + "," + fDestinoLongitud + "/restaurants?key=" + TripAdvisorAPIKey + "&lang=en_CA&price_level=$$$$&minTripAdvisorRating=4.0&maxTripAdvisorRating=5.0&sort=TRIP_ADVISOR";

                    var json = wc.DownloadString(URL);

                    dynamic CadenaJson = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                    var Restaurantes = CadenaJson.data;

                    //foreach (var Restaurante in Restaurantes)
                    //{

                    //    string ccc = "Typical food";

                    //    if (Restaurante.cuisine != null && Restaurante.cuisine.Count > 0)
                    //        ccc = Restaurante.cuisine[0].localized_name;

                    //    if (ccc == "" && Restaurante != "")
                    //        ccc = "Typical food";

                    //    string dir = Restaurante.address_obj.address_string;

                    //    string nombre = Restaurante.name;

                    //    cadenaHtml.Append("<tr><td width='33%'  style='font-size:7px; vertical-align:top;'>" + nombre + "<br/>" + ccc + "<br/>" + dir + "<br/><br/><br/></td></tr>");

                    //    NumeroActividad = NumeroActividad + 1;

                    //    if (NumeroActividad == 3)
                    //    {
                    //        break;
                    //    }
                    //}

                    //tblRestaurantesDestinoEn.InnerHtml = "<table>" + cadenaHtml.ToString() + "</table>";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void MuestraRestauranteEs()
        {
            try
            {

                using (WebClient wc = new WebClient())
                {
                    int NumeroActividad = 0;

                    StringBuilder cadenaHtml = new StringBuilder();

                    string TripAdvisorAPIKey = "98cafba6a0214931a02e0cbacf8aae54";
                    string URL = "http://api.tripadvisor.com/api/partner/2.0/map/" + fDestinoLatitud + "," + fDestinoLongitud + "/restaurants?key=" + TripAdvisorAPIKey + "&lang=es_MX&price_level=$$$$&minTripAdvisorRating=4.0&maxTripAdvisorRating=5.0&sort=TRIP_ADVISOR";

                    var json = wc.DownloadString(URL);

                    dynamic CadenaJson = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                    var Restaurantes = CadenaJson.data;

                    //foreach (var Restaurante in Restaurantes)
                    //{

                    //    string ccc = "Comida típica del lugar";

                    //    if (Restaurante.cuisine != null && Restaurante.cuisine.Count > 0)
                    //        ccc = Restaurante.cuisine[0].localized_name;

                    //    if (ccc == "" && Restaurante != "")
                    //        ccc = "Comida típica del lugar";

                    //    string dir = Restaurante.address_obj.address_string;

                    //    string nombre = Restaurante.name;

                    //    cadenaHtml.Append("<tr><td width='33%'  style='font-size:7px; vertical-align:top;'>" + nombre + "<br/>" + ccc + "<br/>" + dir + "<br/><br/><br/></td></tr>");

                    //    NumeroActividad = NumeroActividad + 1;

                    //    if (NumeroActividad == 3)
                    //    {
                    //        break;
                    //    }
                    //}

                    //tblRestaurantesDestinoEs.InnerHtml = "<table>" + cadenaHtml.ToString() + "</table>";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void MuestraTopActivitiesEn()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    int NumeroActividad = 0;

                    StringBuilder cadenaHtml = new StringBuilder();

                    string TripAdvisorAPIKey = "98cafba6a0214931a02e0cbacf8aae54";
                    string URL = "http://api.tripadvisor.com/api/partner/2.0/map/" + fDestinoLatitud + "," + fDestinoLongitud + "/attractions?key=" + TripAdvisorAPIKey + "&lang=en_CA&minTripAdvisorRating=4.0&maxTripAdvisorRating=5.0&sort=TRIP_ADVISOR";

                    var json = wc.DownloadString(URL);

                    dynamic CadenaJson = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                    var Actividades = CadenaJson.data;

                    //foreach (var Actividad in Actividades)
                    //{

                    //    string ccc = Actividad.attraction_types[0].localized_name.Value;

                    //    string dir = Actividad.address_obj.address_string.Value;

                    //    string nombre = Actividad.name.Value;

                    //    cadenaHtml.Append("<tr><td width='33%'  style='vertical-align:top; font-size:7px;'>" + nombre + "<br/>" + ccc + "<br/>" + dir + "<br/><br/></td></tr>");

                    //    NumeroActividad = NumeroActividad + 1;

                    //    if (NumeroActividad == 3)
                    //    {
                    //        break;
                    //    }
                    //}

                    //tblActividadesDestinoEn.InnerHtml = "<table>" + cadenaHtml.ToString() + "</table>";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void MuestraTopActivitiesEs()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    int NumeroActividad = 0;

                    StringBuilder cadenaHtml = new StringBuilder();

                    string TripAdvisorAPIKey = "98cafba6a0214931a02e0cbacf8aae54";
                    string URL = "http://api.tripadvisor.com/api/partner/2.0/map/" + fDestinoLatitud + "," + fDestinoLongitud + "/attractions?key=" + TripAdvisorAPIKey + "&lang=es_MX&minTripAdvisorRating=4.0&maxTripAdvisorRating=5.0&sort=TRIP_ADVISOR";

                    var json = wc.DownloadString(URL);

                    dynamic CadenaJson = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                    var Actividades = CadenaJson.data;

                    //foreach (var Actividad in Actividades)
                    //{

                    //    string ccc = Actividad.attraction_types[0].localized_name.Value;

                    //    string dir = Actividad.address_obj.address_string.Value;

                    //    string nombre = Actividad.name.Value;

                    //    cadenaHtml.Append("<tr><td width='33%'  style='vertical-align:top; font-size:7px;'>" + nombre + "<br/>" + ccc + "<br/>" + dir + "<br/><br/></td></tr>");

                    //    NumeroActividad = NumeroActividad + 1;

                    //    if (NumeroActividad == 3)
                    //    {
                    //        break;
                    //    }
                    //}

                    //tblActividadesDestinoEs.InnerHtml = "<table>" + cadenaHtml.ToString() + "</table>";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public byte[] HTMLaPdf(string html)
        {

            byte[] bPDF = Utils.ObtenerPDF(html);

            return bPDF;

            //Response.Clear();
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=" + "PDFfile.pdf; " + "size=" + bPDF.Length.ToString());
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.BinaryWrite(bPDF);
            //Response.End();
        }

        private string ParsearHTML(string CadenaHtml)
        {
            string Resultado = string.Empty;

            string CadenaReemplazar = "src=\"../../img/iconos/MailMexJetHeadEn.png\"";
            string textoRemplazo = "src=\"" + Server.MapPath("../../img/iconos/MailMexJetHeadEn.png") + "\"";

            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "src=\"../../img/iconos/MailMexJetHeadEs.png\"";
            textoRemplazo = "src=\"" + Server.MapPath("../../img/iconos/MailMexJetHeadEs.png") + "\"";

            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "_imgAeronaveEn\" src=\"../../img/iconos/";
            textoRemplazo = "_imgAeronaveEn\" src=\"" + Server.MapPath("../../img/iconos/");
            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "style=\"height:300px;width:500px;\"";
            textoRemplazo = "width=\"410\" height=\"250\"";
            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "_imgAeronaveEs\" src=\"../../img/iconos/";
            textoRemplazo = "_imgAeronaveEs\" src=\"" + Server.MapPath("../../img/iconos/");
            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "_imgAeronaveEs\" src=\"../../img/iconos/";
            textoRemplazo = "_imgAeronaveEs\" src=\"" + Server.MapPath("../../img/iconos/");
            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "_lblObservaciones2En\" style=\"color:Black;font-family:Arial;font-size:16px;\">";
            textoRemplazo = "_lblObservaciones2En\" style=\"color:Black;font-family:Arial;font-size:16px;\">" + sObservaciones;
            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "_lblObservaciones2Es\" style=\"color:Black;font-family:Arial;font-size:16px;\">";
            textoRemplazo = "_lblObservaciones2Es\" style=\"color:Black;font-family:Arial;font-size:16px;\">" + sObservaciones;
            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "src=\"../../img/iconos/icon_openWeather.png\"";
            textoRemplazo = "src=\"" + Server.MapPath("../../img/iconos/icon_openWeather.png") + "\"";
            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "src=\"../../img/iconos/icon_restaurante.png\"";
            textoRemplazo = "src=\"" + Server.MapPath("../../img/iconos/icon_restaurante.png") + "\"";
            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "src=\"../../img/iconos/icon_actividades.png\"";
            textoRemplazo = "src=\"" + Server.MapPath("../../img/iconos/icon_actividades.png") + "\"";
            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "src=\"../../img/iconos/PiePagina.jpg\"";
            textoRemplazo = "src=\"" + Server.MapPath("../../img/iconos/PiePagina.jpg") + "\"";
            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "src=\"../../img/iconos/poweredBy.jpg\"";
            textoRemplazo = "src=\"" + Server.MapPath("../../img/iconos/poweredBy.jpg") + "\"";
            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "top; width:33.33%";
            textoRemplazo = "top; width:100px; height:15px";
            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "style=\"height:40%;\"";
            textoRemplazo = "Height=\"15\" Width=\"100\"";
            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);

            CadenaReemplazar = "style=\"height:45%;\"";
            textoRemplazo = "Height=\"15\" Width=\"100\"";
            CadenaHtml = CadenaHtml.Replace(CadenaReemplazar, textoRemplazo);


            Resultado = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                 <!DOCTYPE html 
                     PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN""
                    ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
                 <html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"">
                    <head>
                        <title></title>
                    </head>
                  <body>" + CadenaHtml + "</body></html>";

            return Resultado;
        }

        private void EnviarMail()
        {
            if (!string.IsNullOrEmpty(txtPara.Text))
            {

                sObservaciones = memoObservaciones.Text;

                string sContactoPara = txtPara.Text;
                string Motivo = Utils.CuerpoCorreoHtmlTripGuide(sNombreContacto, iTrip, sICAOOrigen, sNombreAeropuertoOrigen, sICAODestino, sNombreAeropuertoDestino, DtFechaSalida, sNumeroPasajero,
                                                                sAeronave, sPiloto, sPilotoTelefono, sCoPiloto, sCoPilotoTelefono, sObservaciones).ToString();
                string Asunto = "Confirmación del vuelo " + iTrip.S();
                string sContactosCopia = txtConCopia.Text;
                string sContactosCopiaOculta = txtConCopiaOculta.Text;

                sNombreArchivo = sNombreArchivo + iPierna.S();

                string scorreo = Utils.ObtieneParametroPorClave("4");
                string sPass = Utils.ObtieneParametroPorClave("5");
                string sservidor = Utils.ObtieneParametroPorClave("6");
                string spuerto = Utils.ObtieneParametroPorClave("7");

                string htmlText = hdTablaTripGuideHtml["ValorHtml"].ToString();

                htmlText = ParsearHTML(htmlText);

                bPDF = Utils.ObtenerPDF(htmlText);

                Stream ArchivoAdjunto = new MemoryStream(bPDF);

                Utils.EnvioCorreo(sservidor, spuerto.S().I(), Asunto, sContactoPara, Motivo, ArchivoAdjunto, scorreo, sPass, sContactosCopia, sNombreArchivo + ".pdf", "", sContactosCopiaOculta);

                if (eGuardaTripGuide != null)
                    eGuardaTripGuide(null, EventArgs.Empty);

                ppConfirmacionCorreo.ShowOnPageLoad = true;

                lbl.Text = "Envío de correo exitoso.";

            }
            else
            {
                mpeMensaje.ShowMessage("Ingrese un correo valido en el campo \"Para:\".", "");
            }
        }

        private string ParseaCorreo(string CadenaCorreos)
        {
            string Resultado = string.Empty;

            Resultado = CadenaCorreos;

            if (!string.IsNullOrEmpty(Resultado))
            {
                string CaracterFinal = Resultado.Substring(Resultado.Length - 1);

                if (CaracterFinal.Equals(";"))
                {
                    Resultado = Resultado.Substring(0, Resultado.Length - 1);
                }
            }

            return Resultado;
        }

        private void RegresaSolicitudVuelo()
        {
            MonitorCliente oMon = new DBSolicitudesVuelo().DBGetObtieneGeneralesSolicitud(iIdSolicitud);
            Session["Monitor"] = oMon;
            Response.Redirect("~/Views/AtencionClientes/frmSolicitudes.aspx?Id=" + iIdSolicitud.S() + "&Accion=1", false);
        }

        public void ConsultaContactoSolicitud(DataTable dtResultado)
        {
            dtContacto = dtResultado;
        }
        public void ConsultaVendedorSolicitud(DataTable dtResultado)
        {
            if (dtResultado.Rows.Count > 0)
            {
                txtConCopiaOculta.Text = dtResultado.Rows[0]["CorreoElectronico"].ToString();
            }
        }

        #endregion

        #region VARIABLES Y PROPIEDADES

        TripGuide_Presenter oPresenter;
        TripGuideFPK_Presenter oPresenterFPK;

        private const string sPagina = "frmTripGuide.aspx";
        private const string sClase = "frmTripGuide.aspx.cs";

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public event EventHandler eSearchLegsByTrip;

        public int iTrip
        {
            get { return ViewState["VSTrip"].S().I(); }
            set { ViewState["VSTrip"] = value; }
        }

        public int iPierna
        {
            get { return ViewState["VSiIdPierna"].S().I(); }
            set { ViewState["VSiIdPierna"] = value; }
        }

        public string sICAOOrigen
        {
            get { return ViewState["sICAOOrigen"].S(); }
            set { ViewState["sICAOOrigen"] = value; }
        }

        public string sICAODestino
        {
            get { return ViewState["sICAODestino"].S(); }
            set { ViewState["sICAODestino"] = value; }
        }

        public string sNombreContacto
        {
            get { return ViewState["VSsNombreContacto"].S(); }
            set { ViewState["VSsNombreContacto"] = value; }
        }
        public string sNombreAeropuertoOrigen
        {
            get { return ViewState["VSsNombreAeropuertoOrigen"].S(); }
            set { ViewState["VSsNombreAeropuertoOrigen"] = value; }
        }

        public string sNombreAeropuertoDestino
        {
            get { return ViewState["VSsNombreAeropuertoDestino"].S(); }
            set { ViewState["VSsNombreAeropuertoDestino"] = value; }
        }

        public string sNumeroPasajero
        {
            get { return ViewState["VSsNumeroPasajero"].S(); }
            set { ViewState["VSsNumeroPasajero"] = value; }
        }

        public string sAeronave
        {
            get { return ViewState["VSsAeronave"].S(); }
            set { ViewState["VSsAeronave"] = value; }
        }

        public string sMatriculaAeronave
        {
            get { return ViewState["VSsMatriculaAeronave"].S(); }
            set { ViewState["VSsMatriculaAeronave"] = value; }
        }

        public string sPiloto
        {
            get { return ViewState["VSsPiloto"].S(); }
            set { ViewState["VSsPiloto"] = value; }
        }

        public string sPilotoTelefono
        {
            get { return ViewState["VSsPilotoTelefono"].S(); }
            set { ViewState["VSsPilotoTelefono"] = value; }
        }

        public string sCoPiloto
        {
            get { return ViewState["VSsCoPiloto"].S(); }
            set { ViewState["VSsCoPiloto"] = value; }
        }

        public string sCoPilotoTelefono
        {
            get { return ViewState["VSsCoPilotoTelefono"].S(); }
            set { ViewState["VSsCoPilotoTelefono"] = value; }
        }

        public DateTime DtFechaSalida
        {
            get { return ViewState["VSDtFechaSalida"].Dt(); }
            set { ViewState["VSDtFechaSalida"] = value; }
        }
        public DateTime DtFechaLlegada
        {
            get { return ViewState["VSDtFechaLlegada"].Dt(); }
            set { ViewState["VSDtFechaLlegada"] = value; }
        }

        public int iIdSolicitud
        {
            get { return Session["idSolicitud"].S().I(); }
            set { Session["idSolicitud"] = value; }
        }

        private DataTable dtContacto
        {
            get { return (DataTable)Session["VSdtContacto"]; }
            set { Session["VSdtContacto"] = value; }
        }

        private string sObservaciones = string.Empty;

        private string sNombreArchivo = "TripGuide_";
        private byte[] bPDF = null;
        private string sUsuario = string.Empty;
        private string sIP = string.Empty;

        private float fOrigenLatitud = 0;
        private float fOrigenLongitud = 0;
        private float fDestinoLatitud = 0;
        private float fDestinoLongitud = 0;

        public event EventHandler eBuscaDatosTrip;
        public event EventHandler eGuardaTripGuide;
        public event EventHandler eBuscaDetallePierna;
        public event EventHandler eBuscaPasajero;
        public event EventHandler eBuscaCoordenadaOrigen;
        public event EventHandler eBuscaCoordenadaDestino;
        public event EventHandler eConsultaContactoSolicitud;
        public event EventHandler eConsultaVendedorSolicitud;

        public TripGuide TripGuide
        {
            get
            {
                TripGuide TripGuide = new TripGuide();

                TripGuide.iIdSolicitud = iIdSolicitud;
                TripGuide.iIdTrip = iTrip;
                TripGuide.iIdPierna = iPierna;
                TripGuide.sNombreArchivoPDF = sNombreArchivo;
                TripGuide.bPDF = bPDF;
                TripGuide.sUsuarioCreacion = sUsuario;
                TripGuide.sIP = sIP;
                TripGuide.sObservaciones = sObservaciones;
                TripGuide.sNombreContacto = sNombreContacto;
                TripGuide.sICAOOrigen = sICAOOrigen;
                TripGuide.sICAODestino = sICAODestino;
                TripGuide.sNombreAeropuertoOrigen = sNombreAeropuertoOrigen;
                TripGuide.sNombreAeropuertoDestino = sNombreAeropuertoDestino;
                TripGuide.dtFechaSalida = DtFechaSalida;
                TripGuide.iNumeroPasajero = sNumeroPasajero.I();
                TripGuide.sAeronave = sAeronave;
                TripGuide.sPiloto = sPiloto;
                TripGuide.sPilotoTelefono = sPilotoTelefono;
                TripGuide.sCoPiloto = sCoPiloto;
                TripGuide.sCoPilotoTelefono = sCoPilotoTelefono;

                return TripGuide;
            }
        }

        #endregion




    }
}