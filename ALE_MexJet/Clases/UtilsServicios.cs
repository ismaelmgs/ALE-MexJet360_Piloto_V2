using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using NucleoBase.Core;
using ALE_MexJet.DomainModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace ALE_MexJet.Clases
{
    public class UtilsServicios
    {
        #region APP MEXJET

        /// <summary>
        /// Publica el vuelo en el app de mexjet
        /// </summary>
        /// <param name="iIdFerry">Identificador del ferry</param>
        /// <returns></returns>
        public bool EnviaFerryEzJMexJet(int iIdFerry)
        {
            try
            {
                bool ban = false;
                string sToken = string.Empty;
                using (var client = new HttpClient())
                {
                    DataSet ds = new DataSet();
                    ds = new DBVentaFerry().DBGetInfoFerryToEZMexJet(iIdFerry);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        DateTime dtInicio = DateTime.Now;
                        DateTime dtFin = DateTime.Now;
                        foreach (DataRow r in  ds.Tables[0].Rows)
                        {
                            if (r.S("IdPadre").I() == 0)
                            {
                                dtInicio = r.S("FechaSalida").Dt();
                                dtFin = r.S("FechaLlegada").Dt();
                            }
                        }

                        int iFila = 0;
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            VentaFerrys oVF = new VentaFerrys();
                            oVF.iIdFerry = r.S("IdFerry").I();
                            oVF.iIdPadre = r.S("IdPadre").I();
                            oVF.sOrigen = r.S("Origen");
                            oVF.sDestino = r.S("Destino");
                            oVF.dtFechaInicioReservar = dtInicio;
                            oVF.sDuracion = r.S("Duracion");
                            oVF.dtFechaLimiteReservar = dtFin;
                            oVF.sGrupoModelo = r.S("GrupoModelo");
                            oVF.dPrecioLista = r.S("PrecioLista").D();
                            oVF.iPrioridad = r.S("Prioridad").I();
                            oVF.noPax = r.S("NoPasajeros").I();

                            DataRow[] rowsPrecios = ds.Tables[1].Select("IdFerry = " + oVF.iIdFerry.S());
                            if (rowsPrecios.Length > 0)
                            {
                                for (int i = 0; i < rowsPrecios.Length; i++)
                                {
                                    PreciosTipoCliente oP = new PreciosTipoCliente();
                                    oP.sTipoCliente = rowsPrecios[i]["TipoCliente"].S();
                                    oP.dDescuento = rowsPrecios[i]["Descuento"].S().D();
                                    oP.dImporteFinal = rowsPrecios[i]["PrecioFinal"].S().D();

                                    oVF.oLstPrecios.Add(oP);
                                }
                            }

                            string sCad = JsonConvert.SerializeObject(oVF).ToString();
                            JavaScriptSerializer ser = new JavaScriptSerializer();
                            ResultPubFerry oRes = new ResultPubFerry();

                            if (!VerificaFerryPublicado(iIdFerry))
                            {
                                string sPathWSApp = Utils.ObtieneParametroPorClave("108");
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                                var request = (HttpWebRequest)WebRequest.Create(sPathWSApp);
                                request.Method = "POST";
                                
                                if (iFila == 0) 
                                    sToken = Utils.ObtieneTokenAppMexJet();
                                
                                request.Headers.Add("Authorization", "Bearer " + sToken);

                                if (!string.IsNullOrEmpty(sCad))
                                {
                                    byte[] byteArray = Encoding.UTF8.GetBytes(sCad);
                                    request.ContentType = "application/json; charset=utf-8";
                                    request.ContentLength = byteArray.Length;
                                    var dataStream = request.GetRequestStream();
                                    dataStream.Write(byteArray, 0, byteArray.Length);
                                    dataStream.Close();
                                }

                                using (WebResponse response = request.GetResponse())
                                {
                                    var responseStream = response.GetResponseStream();
                                    if (responseStream != null)
                                    {
                                        var reader = new StreamReader(responseStream);
                                        string receiveContent = reader.ReadToEnd();
                                        reader.Close();

                                        oRes = ser.Deserialize<ResultPubFerry>(receiveContent);
                                        ban = ((HttpWebResponse)response).StatusCode == HttpStatusCode.OK ? true : false;
                                    }

                                    // "codigo error 422"
                                }

                                if (ban)
                                {
                                    new DBVentaFerry().DBSetActualizaInformacionFerry(oVF.iIdFerry, oRes.idVagap);
                                }
                            }

                            iFila++;
                        }
                    }
                }

                return ban;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool VerificaFerryPublicado(int iIdFerry)
        {
            try
            {
                bool ban = false;

                DataTable dt = new DBVentaFerry().DBGetValidaSiFerryPublicado(iIdFerry);
                if (dt != null && dt.Rows.Count > 0)
                    ban = true;

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ApruebaRechazaMembresia(string sToken, string url, string content, int iStatus)
        {
            try
            {
                bool ban = false;
                using (var client = new HttpClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "POST";

                    request.Headers.Add("Authorization", "Bearer " + sToken);

                    if (!string.IsNullOrEmpty(content))
                    {
                        byte[] byteArray = Encoding.UTF8.GetBytes(content);
                        request.ContentType = "application/json; charset=utf-8";
                        request.ContentLength = byteArray.Length;
                        var dataStream = request.GetRequestStream();
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        dataStream.Close();
                    }

                    JavaScriptSerializer ser = new JavaScriptSerializer();

                    using (WebResponse response = request.GetResponse())
                    {
                        var responseStream = response.GetResponseStream();
                        if (responseStream != null)
                        {
                            var reader = new StreamReader(responseStream);
                            string receiveContent = reader.ReadToEnd();
                            reader.Close();

                            ResultPubFerry oRes = ser.Deserialize<ResultPubFerry>(receiveContent);
                            ban = ((HttpWebResponse)response).StatusCode == HttpStatusCode.OK ? true : false;

                            if (ban && oRes.message == "Membresia de cliente actualizada.")
                            {

                            }
                        }

                        // "codigo error 422"
                    }
                }

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public HttpStatusCode TimbraFactura(out string receiveContent, string api, string postContent)
        //{
        //    string requestUrl = "http://sistemademo.fpronto.com/morpheus/ng_jer/ws/timbrar33";
        //    string sPassSHA = FacturacionLinea.Clases.Utils.GetSHA1("ASKJHGD913$");

        //    var request = (HttpWebRequest)WebRequest.Create(requestUrl);
        //    request.Method = "POST";

        //    request.Headers.Add("user", "morato186@gmail.com");
        //    request.Headers.Add("pass", sPassSHA);
        //    request.Headers.Add("api", api);

        //    if (!string.IsNullOrEmpty(postContent))
        //    {
        //        byte[] byteArray = Encoding.UTF8.GetBytes(postContent);
        //        request.ContentType = "application/json; charset=utf-8";
        //        request.ContentLength = byteArray.Length;
        //        var dataStream = request.GetRequestStream();
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //        dataStream.Close();
        //    }

        //    try
        //    {
        //        using (WebResponse response = request.GetResponse())
        //        {
        //            var responseStream = response.GetResponseStream();
        //            if (responseStream != null)
        //            {
        //                var reader = new StreamReader(responseStream);
        //                receiveContent = reader.ReadToEnd();
        //                reader.Close();

        //                return ((HttpWebResponse)response).StatusCode;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        receiveContent = string.Format("{0}\n{1}\nposted content = \n{2}", ex, ex.Message);
        //        return HttpStatusCode.BadRequest;
        //    }

        //    receiveContent = null;
        //    return 0;
        //}


        public bool ActualizaEstatusFerryEzJMexJet(int iIdFerry, int iStatus)
        {
            try
            {
                bool ban = false;
                string sToken = string.Empty;
                using (var client = new HttpClient())
                {
                    EstatusFerry oEf = new EstatusFerry();
                    oEf.iIdFerry = iIdFerry;
                    oEf.status = iStatus;

                    string sCad = JsonConvert.SerializeObject(oEf).ToString();
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    ResultPubFerry oRes = new ResultPubFerry();

                    string sPathWSApp = string.Empty;

                    if(iStatus == 0)
                        sPathWSApp = Utils.ObtieneParametroPorClave("120");

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var request = (HttpWebRequest)WebRequest.Create(sPathWSApp);
                    request.Method = "POST";

                    sToken = Utils.ObtieneTokenAppMexJet();

                    request.Headers.Add("Authorization", "Bearer " + sToken);

                    if (!string.IsNullOrEmpty(sCad))
                    {
                        byte[] byteArray = Encoding.UTF8.GetBytes(sCad);
                        request.ContentType = "application/json; charset=utf-8";
                        request.ContentLength = byteArray.Length;
                        var dataStream = request.GetRequestStream();
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        dataStream.Close();
                    }

                    using (WebResponse response = request.GetResponse())
                    {
                        var responseStream = response.GetResponseStream();
                        if (responseStream != null)
                        {
                            var reader = new StreamReader(responseStream);
                            string receiveContent = reader.ReadToEnd();
                            reader.Close();

                            oRes = ser.Deserialize<ResultPubFerry>(receiveContent);
                            ban = ((HttpWebResponse)response).StatusCode == HttpStatusCode.OK ? true : false;
                        }

                        // "codigo error 422"
                    }

                    if (ban)
                    {
                        if (iStatus == 0)
                            new DBVentaFerry().DBSetCancelaFerryAppMexJet(iIdFerry, iStatus);
                    }
                }
                
                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


        #region  SMS - LABSMOBILE

        
        public void EnvioMensajeSMS(string Mensaje, string NumeroTel)
        {
            try
            {
                List<recipient> olst = new List<recipient>();
                recipient oRec1 = new recipient();
                oRec1.msisdn = NumeroTel;

                olst.Add(oRec1);

                MensajesTexto oMen = new MensajesTexto();
                oMen.message = Mensaje;
                oMen.tpoa = "Aerolíneas Ejecutivas";
                oMen.recipient = olst;


                string sUrl = Globales.GetConfigApp<string>("UrlSMS");
                string sUser = Globales.GetConfigApp<string>("UsuarioSMS");
                string sPass = Globales.GetConfigApp<string>("PassSMS");


                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                String username = sUser;
                String password = sPass;
                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));

                //Form Data
                string sContent = JsonConvert.SerializeObject(oMen).ToString();
                var encodedFormData = Encoding.ASCII.GetBytes(sContent);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sUrl);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                request.ContentLength = encodedFormData.Length;
                request.Headers.Add("Cache-Control", "no-cache");
                request.Headers.Add("Authorization", "Basic " + encoded);
                request.PreAuthenticate = true;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(encodedFormData, 0, encodedFormData.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();



                //bool ban = false;
                //using (var client = new HttpClient())
                //{
                //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //    string sUrl = Globales.GetConfigApp<string>("UrlSMS");
                //    string sUser = Globales.GetConfigApp<string>("UsuarioSMS");
                //    string sPass = Globales.GetConfigApp<string>("PassSMS");

                //    var request = (HttpWebRequest)WebRequest.Create(sUrl);
                //    request.Method = "POST";

                //    System.Net.CredentialCache credentialCache = new System.Net.CredentialCache();
                //    credentialCache.Add(new System.Uri(sUrl), "Basic", new System.Net.NetworkCredential(sUser, sPass));

                //    request.Credentials = credentialCache;
                //    request.Headers.Add("Cache-Control", "no-cache");

                //    string sContent = JsonConvert.SerializeObject(oMen).ToString();

                //    if (!string.IsNullOrEmpty(sContent))
                //    {
                //        byte[] byteArray = Encoding.UTF8.GetBytes(sContent);
                //        request.ContentType = "application/json; charset=utf-8";
                //        request.ContentLength = byteArray.Length;
                //        var dataStream = request.GetRequestStream();
                //        dataStream.Write(byteArray, 0, byteArray.Length);
                //        dataStream.Close();
                //    }

                //    JavaScriptSerializer ser = new JavaScriptSerializer();

                //    using (WebResponse response = request.GetResponse())
                //    {
                //        var responseStream = response.GetResponseStream();
                //        if (responseStream != null)
                //        {
                //            var reader = new StreamReader(responseStream);
                //            string receiveContent = reader.ReadToEnd();
                //            reader.Close();

                //            ResultPubFerry oRes = ser.Deserialize<ResultPubFerry>(receiveContent);
                //            ban = ((HttpWebResponse)response).StatusCode == HttpStatusCode.OK ? true : false;

                //            if (ban && oRes.message == "Membresia de cliente actualizada.")
                //            {

                //            }
                //        }

                //        // "codigo error 422"
                //    }
            }


                //var client = new RestClient("https://api.labsmobile.com/json/send");
                //client.Authenticator = new HttpBasicAuthenticator(usario, contrasena);
                //var request = new RestRequest(Method.POST);
                //request.AddHeader("Cache-Control", "no-cache");
                //request.AddHeader("Content-Type", "application/json");

                //string sJSON = JsonConvert.SerializeObject(oMen);
                //request.AddParameter("undefined", sJSON, ParameterType.RequestBody);
                //IRestResponse response = client.Execute(request);

            
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

    }
}