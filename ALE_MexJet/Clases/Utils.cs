using ALE_MexJet.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.Net.Mail;
using ALE_MexJet.Objetos;
using System.Web.UI.WebControls;
using System.Net.Mime;
using System.IO;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net.Http.Headers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Threading;
using System.Globalization;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;

namespace ALE_MexJet.Clases
{
    public static class Utils
    {
        public static DataTable ConvertListToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static int _I(this object o)
        {
            return Convert.ToInt32(o);
        }

        public static DateTime ObtieneFechaServidor
        {
            get
            {
                try
                {
                    return new DBUtils().DBGetFechaServidor;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static string SaveErrorEnBitacora(string sError, string sPagina, string sClase, string sMetodo)
        {
            try
            {
                object[] obj = new object[]
                                        {
                                            "@Descripcion", sError, 
                                            "@Pagina", sPagina, 
                                            "@Clase", sClase, 
                                            "@Metodo", sMetodo
                                        };

                DataTable dtErrores = new DBUtils().DBSaveBitacoraError(obj);
                return dtErrores.Rows[0][0].S();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveErrorEnBitacora(ControlesUsuario.ucModalMensaje oModal, string sError, string sPagina, string sClase, string sMetodo, string sCaption)
        {
            try
            {
                object[] obj = new object[]
                                        {
                                            "@Descripcion", sError, 
                                            "@Pagina", sPagina, 
                                            "@Clase", sClase, 
                                            "@Metodo", sMetodo
                                        };

                DataTable dtErrores = new DBUtils().DBSaveBitacoraError(obj);

                if (dtErrores.Rows.Count > 0)
                {
                    oModal.ShowMessage(dtErrores.Rows[0][0].S(), sCaption);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void EnvioCorreo(string Host, int Puerto, string Asusnto, string Receptor, string Mensaje_html, string ArchivoAdjunto, string username, string pasword, string CC, string DisplayName)
        {
            try
            {
                MailMessage msg = new MailMessage();
                Attachment Adj;
                SmtpClient clienteSmtp = new SmtpClient();

                clienteSmtp = new SmtpClient(Host, Puerto);
                clienteSmtp.EnableSsl = true;


                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(username, pasword);
                clienteSmtp.Credentials = credentials;
                //clienteSmtp.UseDefaultCredentials = true;
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.BodyEncoding = System.Text.Encoding.UTF8;

                msg.To.Add(Receptor);

                if (DisplayName != string.Empty)
                    msg.From = new MailAddress(username, DisplayName);
                else
                    msg.From = new MailAddress(username);

                msg.Subject = Asusnto;
                msg.Priority = MailPriority.High;
                msg.IsBodyHtml = true;
                msg.Body = Mensaje_html;

                if (CC != null && CC != String.Empty)
                    msg.CC.Add(CC);

                if (ArchivoAdjunto != "")
                {
                    Adj = new Attachment(ArchivoAdjunto);
                    msg.Attachments.Add(Adj);
                }

                AlternateView plainTextView = AlternateView.CreateAlternateViewFromString(Mensaje_html.Trim(), null, "text/plain");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Mensaje_html.Trim(), null, "text/html");

                //LinkedResource sampleImage = new LinkedResource(@"C:\Users\Cesar\Pictures\Gay.png", MediaTypeNames.Image.Jpeg);
                //sampleImage.ContentId = "Gay";
                //htmlView.LinkedResources.Add(sampleImage);

                msg.AlternateViews.Add(plainTextView);
                msg.AlternateViews.Add(htmlView);



                clienteSmtp.Send(msg);

                msg.Dispose();
                msg = null;
                clienteSmtp = null;
            }
            catch (Exception x) { throw x; }
        }

        public static void EnvioCorreo(string Host, int Puerto, string Asusnto, string Receptor, string Mensaje_html, MemoryStream ArchivoAdjunto, string username, string pasword, string CC, string DisplayName)
        {
            try
            {
                MailMessage msg = new MailMessage();
                Attachment Adj;
                SmtpClient clienteSmtp = new SmtpClient();

                clienteSmtp = new SmtpClient(Host, Puerto);
                clienteSmtp.EnableSsl = true;


                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(username, pasword);
                clienteSmtp.Credentials = credentials;
                //clienteSmtp.UseDefaultCredentials = true;
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.BodyEncoding = System.Text.Encoding.UTF8;

                msg.To.Add(Receptor);

                if (DisplayName != string.Empty)
                    msg.From = new MailAddress(username, DisplayName);
                else
                    msg.From = new MailAddress(username);

                msg.Subject = Asusnto;
                msg.Priority = MailPriority.High;
                msg.IsBodyHtml = true;
                msg.Body = Mensaje_html;

                if (CC != null && CC != String.Empty)
                    msg.CC.Add(CC);

                if (ArchivoAdjunto != null)
                {
                    Adj = new Attachment(ArchivoAdjunto, "OfertaFerrys.csv");
                    msg.Attachments.Add(Adj);
                }

                AlternateView plainTextView = AlternateView.CreateAlternateViewFromString(Mensaje_html.Trim(), null, "text/plain");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Mensaje_html.Trim(), null, "text/html");

                //LinkedResource sampleImage = new LinkedResource(@"C:\Users\Cesar\Pictures\Gay.png", MediaTypeNames.Image.Jpeg);
                //sampleImage.ContentId = "Gay";
                //htmlView.LinkedResources.Add(sampleImage);

                msg.AlternateViews.Add(plainTextView);
                msg.AlternateViews.Add(htmlView);



                clienteSmtp.Send(msg);

                msg.Dispose();
                msg = null;
                clienteSmtp = null;
            }
            catch (Exception x) { throw x; }
        }

        public static void EnvioCorreo(string Host, int Puerto, string Asusnto, string Receptor, string Mensaje_html, Stream ArchivoAdjunto, string username, string pasword, string CC, string DisplayName)
        {
            try
            {
                MailMessage msg = new MailMessage();
                Attachment Adj;
                SmtpClient clienteSmtp = new SmtpClient();

                clienteSmtp = new SmtpClient(Host, Puerto);
                clienteSmtp.EnableSsl = true;


                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(username, pasword);
                clienteSmtp.Credentials = credentials;
                //clienteSmtp.UseDefaultCredentials = true;
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.BodyEncoding = System.Text.Encoding.UTF8;

                msg.To.Add(Receptor);

                if (DisplayName != string.Empty)
                    msg.From = new MailAddress(username, DisplayName);
                else
                    msg.From = new MailAddress(username);

                msg.Subject = Asusnto;
                msg.Priority = MailPriority.High;
                msg.IsBodyHtml = true;
                msg.Body = Mensaje_html;

                if (CC != null && CC != String.Empty)
                    msg.CC.Add(CC);

                if (ArchivoAdjunto != null)
                {
                    Adj = new Attachment(ArchivoAdjunto, "Presupuesto.pdf");
                    msg.Attachments.Add(Adj);
                }

                AlternateView plainTextView = AlternateView.CreateAlternateViewFromString(Mensaje_html.Trim(), null, "text/plain");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Mensaje_html.Trim(), null, "text/html");

                //LinkedResource sampleImage = new LinkedResource(@"C:\Users\Cesar\Pictures\Gay.png", MediaTypeNames.Image.Jpeg);
                //sampleImage.ContentId = "Gay";
                //htmlView.LinkedResources.Add(sampleImage);

                msg.AlternateViews.Add(plainTextView);
                msg.AlternateViews.Add(htmlView);



                clienteSmtp.Send(msg);

                msg.Dispose();
                msg = null;
                clienteSmtp = null;
            }
            catch (Exception x) { throw x; }
        }

        public static void EnvioCorreo(string Host, int Puerto, string Asusnto, string Receptor, string Mensaje_html, string ArchivoAdjunto, string username, string pasword, string CC, string DisplayName, List<ImagenCorreo> lstImagenes)
        {
            try
            {
                MailMessage msg = new MailMessage();
                Attachment Adj;
                SmtpClient clienteSmtp = new SmtpClient();

                clienteSmtp = new SmtpClient(Host, Puerto);
                clienteSmtp.EnableSsl = true;


                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(username, pasword);
                clienteSmtp.Credentials = credentials;
                //clienteSmtp.UseDefaultCredentials = true;
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.BodyEncoding = System.Text.Encoding.UTF8;

                msg.To.Add(Receptor);

                if (DisplayName != string.Empty)
                    msg.From = new MailAddress(username, DisplayName);
                else
                    msg.From = new MailAddress(username);

                msg.Subject = Asusnto;
                msg.Priority = MailPriority.High;
                msg.IsBodyHtml = true;
                msg.Body = Mensaje_html;

                if (CC != null && CC != String.Empty)
                    msg.CC.Add(CC);

                if (ArchivoAdjunto != "")
                {
                    Adj = new Attachment(ArchivoAdjunto);
                    msg.Attachments.Add(Adj);
                }

                AlternateView plainTextView = AlternateView.CreateAlternateViewFromString(Mensaje_html.Trim(), null, "text/plain");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Mensaje_html.Trim(), null, "text/html");

                foreach(ImagenCorreo objimagen in lstImagenes)
                {
                    LinkedResource sampleImage = new LinkedResource(objimagen.sRutaIagen, MediaTypeNames.Image.Jpeg);
                    sampleImage.ContentId = objimagen.sClaveImagen;
                    htmlView.LinkedResources.Add(sampleImage);
                }
                

                msg.AlternateViews.Add(plainTextView);
                msg.AlternateViews.Add(htmlView);



                clienteSmtp.Send(msg);

                msg.Dispose();
                msg = null;
                clienteSmtp = null;
            }
            catch (Exception x) { throw x; }
        }

        public static void EnvioCorreo(string Host, int Puerto, string Asusnto, string Receptor, string Mensaje_html, Stream ArchivoAdjunto, string username, string pasword, string CC, string NombreArchivo, string DisplayName, string CCO)
        {
            try
            {
                MailMessage msg = new MailMessage();
                Attachment Adj;
                SmtpClient clienteSmtp = new SmtpClient();

                clienteSmtp = new SmtpClient(Host, Puerto);
                clienteSmtp.EnableSsl = true;


                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(username, pasword);
                clienteSmtp.Credentials = credentials;
                //clienteSmtp.UseDefaultCredentials = true;
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.BodyEncoding = System.Text.Encoding.UTF8;

                foreach (string enviar_a in Receptor.Split(';'))
                {
                    msg.To.Add(new MailAddress(enviar_a));
                }

                if (CC != null && CC != String.Empty)
                    foreach (string enviar_a in CC.Split(';'))
                    {
                        msg.To.Add(new MailAddress(enviar_a));
                    }

                if (CCO != null && CCO != String.Empty)
                    foreach (string enviar_a in CCO.Split(';'))
                    {
                        msg.To.Add(new MailAddress(enviar_a));
                    }


                msg.From = new MailAddress(username);

                msg.Subject = Asusnto;
                msg.Priority = MailPriority.High;
                msg.IsBodyHtml = true;
                msg.Body = Mensaje_html;

                if (ArchivoAdjunto != null)
                {
                    Adj = new Attachment(ArchivoAdjunto, NombreArchivo);
                    msg.Attachments.Add(Adj);
                }

                //AlternateView plainTextView = AlternateView.CreateAlternateViewFromString(Mensaje_html.Trim(), null, "text/plain");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Mensaje_html.Trim(), null, "text/html");

                //msg.AlternateViews.Add(plainTextView);
                msg.AlternateViews.Add(htmlView);

                clienteSmtp.Send(msg);

                msg.Dispose();
                msg = null;
                clienteSmtp = null;
            }
            catch (Exception x) { throw x; }
        }

        public static DataTable CalculaCostosRemision(DatosRemision oRem, ref DataTable dtPres2)
        {
            try
            {
                bool bBaseBase = false;
                string[] sTramos = oRem.sRuta.Split('-');

                string sAeropuertoSal = sTramos[0];
                bool banBaseInicio = false;
                foreach (DataRow dr in oRem.dtBases.Rows)
                {
                    if (sAeropuertoSal == dr.S("AeropuertoICAO") && (dr.S("IdTipoBase") == "1" || dr.S("IdTipoBase") == "2"))
                    {
                        banBaseInicio = true;
                        break;
                    }
                }

                if (banBaseInicio && sTramos[0] == sTramos[sTramos.Length - 1])
                {
                    bBaseBase = true;
                }

                DataTable dtTramos = new DataTable();
                oRem.dtTramos.Columns.Add("TiempoCobrar", typeof(string));

                string sTipoVuelo = oRem.iCobroTiempo == 1 ? "TotalTiempoVuelo" : "TotalTiempoCalzo";
                string sTipoIntercambioHeli = "TotalTiempoCalzo";

                bool bEsHelicoptero = false;
                DataTable dt = new DBRemision().DBGetObtieneMatriculasHelicoptero;
                if (dt != null)
                {
                    string sMatricula = oRem.dtTramos.Rows[0]["Matricula"].S();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Matricula"].S() == sMatricula)
                        {
                            bEsHelicoptero = true;
                            break;
                        }
                    }
                }

                if (bEsHelicoptero)
                {
                    if(oRem.iCobraFerryHelicoptero == 1)
                        oRem.eSeCobraFerry = Enumeraciones.SeCobraFerrys.Todos;
                }

                switch (oRem.eSeCobraFerry)
                {
                    case Enumeraciones.SeCobraFerrys.Todos:
                        #region SE COBRAN 'TODOS' LOS FERRYS
                        if (bBaseBase)
                        {
                            if (sTramos.Length == 3)
                            {
                                // Validación de 24 horas entre tramos para ver si se hacen 2 presupuestos
                                bool bAplicaDosPresupuestos = false;
                                bAplicaDosPresupuestos = oRem.dtTramos.Rows[0]["TiempoEspera"].S().Substring(0, 2).S().I() >= 24 ? true : false;

                                if (oRem.bTiemposPactados)
                                {
                                    foreach (DataRow row in oRem.dtTramos.Rows)
                                    {
                                        string sTiempo = ObtieneTramoPactado(oRem, row.S("Origen"), row.S("Destino"), row.S("Matricula"));

                                        if (sTiempo == "00:00:00" || sTiempo == string.Empty)
                                        {
                                            row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                        }
                                        else
                                            row["TiempoCobrar"] = sTiempo;
                                    }
                                }
                                else
                                {
                                    foreach (DataRow row in oRem.dtTramos.Rows)
                                    {
                                        row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                    }
                                }

                                if (bAplicaDosPresupuestos)
                                {
                                    //SI Aplica dos Presupuestos
                                    oRem.dtTramos = AgregaFerryTabla(4, oRem, 0, string.Empty);
                                    dtPres2 = oRem.dtTramos.Copy();

                                    foreach (DataRow drw in oRem.dtTramos.Rows)
                                    {
                                        drw["TiempoEspera"] = "00:00";
                                    }
                                }
                            }
                            else
                            {
                                // No aplican dos presupuestos
                                if (oRem.bTiemposPactados)
                                {
                                    foreach (DataRow row in oRem.dtTramos.Rows)
                                    {
                                        string sTiempo = ObtieneTramoPactado(oRem, row.S("Origen"), row.S("Destino"), row.S("Matricula"));

                                        if (sTiempo == "00:00:00" || sTiempo == string.Empty || sTiempo == "00:00")
                                        {
                                            row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                        }
                                        else
                                            row["TiempoCobrar"] = sTiempo;
                                    }
                                }
                                else
                                {
                                    foreach (DataRow row in oRem.dtTramos.Rows)
                                    {
                                        row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                    }
                                }
                            }


                            if (dtPres2.Rows.Count == 4)
                            {
                                dtPres2.Rows.RemoveAt(2);
                                dtPres2.Rows.RemoveAt(1);
                            }

                            // Tramos finales
                            oRem.dtTramos = AplicaFactoresATiemposRemision(oRem.dtTramos, oRem, oRem.iIdContrato);
                            dtTramos = CalculaCostosRemision(oRem.iCobroTiempo, oRem.dtTramos, oRem);
                        }
                        else
                        {
                            string sTiempo = string.Empty;

                            if (oRem.bTiemposPactados)
                            {
                                foreach (DataRow row in oRem.dtTramos.Rows)
                                {
                                    sTiempo = ObtieneTramoPactado(oRem, row.S("Origen"), row.S("Destino"), row.S("Matricula"));

                                    if (sTiempo == "00:00:00" || sTiempo == string.Empty)
                                    {
                                        row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                    }
                                    else
                                        row["TiempoCobrar"] = sTiempo;
                                }
                            }
                            else
                            {
                                foreach (DataRow row in oRem.dtTramos.Rows)
                                {
                                    row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                    //row["TiempoCobrar"] = sTiempo == "00:00:00" || sTiempo == string.Empty ? row[sTipoVuelo].S() : sTiempo;
                                }
                            }

                            // agregar ferrys virtuales
                            string sInicio = sTramos[0];
                            string sFin = sTramos[sTramos.Length - 1];
                            string sBasePre = string.Empty;
                            string sBaseFin = string.Empty;
                            int iIdAeropuerto = 0;
                            int iInicioFin = 0;  //  1.- Inicio   2.- Fin

                            foreach (DataRow dr in oRem.dtBases.Rows)
                            {
                                sBaseFin = dr.S("AeropuertoICAO");


                                if (dr.S("IdTipoBase") == "1")
                                    sBasePre = dr.S("AeropuertoICAO");


                                if (sInicio == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "1")
                                {
                                    iIdAeropuerto = dr.S("IdAeropuerto").I();
                                    iInicioFin = 1;
                                    break;
                                }
                                else if (sFin == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "1")
                                {
                                    iIdAeropuerto = dr.S("IdAeropuerto").I();
                                    iInicioFin = 2;
                                    break;
                                }
                                else if (sInicio == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "2")
                                {
                                    iIdAeropuerto = dr.S("IdAeropuerto").I();
                                    iInicioFin = 1;
                                }
                                else if (sFin == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "2")
                                {
                                    iIdAeropuerto = dr.S("IdAeropuerto").I();
                                    iInicioFin = 2;
                                }
                            }

                            DateTime dtFechaLlegada;
                            TimeSpan ts;
                            int iCont = 1;
                            if (iInicioFin != 0 && iIdAeropuerto != 0)
                            {
                                DataRow dr;

                                switch (iInicioFin)
                                {
                                    case 1: // Agregar Ferry Final
                                        #region FERRY FINAL
                                        sInicio = sTramos[sTramos.Length - 1];
                                        sFin = sBaseFin;

                                        sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["Matricula"].S());
                                        dr = oRem.dtTramos.NewRow();
                                        dr["IdBitacora"] = iCont;
                                        dr["Matricula"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["Matricula"].S();
                                        dr["IdTipoPierna"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["IdTipoPierna"].S();
                                        dr["Origen"] = new DBRemision().DBGetIATAporICAO(sInicio);
                                        dr["Destino"] = new DBRemision().DBGetIATAporICAO(sFin);
                                        dr["OrigenICAO"] = sInicio;
                                        dr["DestinoICAO"] = sFin;

                                        dr["FechaSalida"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"];

                                        dtFechaLlegada = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"].Dt();
                                        string[] sTiempos = sTiempo.Split(':');
                                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]));
                                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]));

                                        dr["FechaLlegada"] = dtFechaLlegada;

                                        dr["CantPax"] = 0;
                                        dr["TotalTiempoCalzo"] = "00:00";
                                        dr["TotalTiempoVuelo"] = "00:00";

                                        ts = dtFechaLlegada - oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"].Dt();

                                        dr["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S() : sTiempo;
                                        dr["RealVirtual"] = "Virtual";
                                        dr["VueloClienteId"] = oRem.dtTramos.Rows[0]["VueloClienteId"].S();
                                        dr["VueloContratoId"] = oRem.dtTramos.Rows[0]["VueloContratoId"].S();
                                        dr["TiempoEspera"] = "00:00";
                                        dr["SeCobra"] = 1;

                                        oRem.dtTramos.Rows.Add(dr);
                                        #endregion
                                        break;

                                    case 2: // Agregar Ferry Inicial
                                        #region FERRY INICIAL

                                        sInicio = sBaseFin;
                                        sFin = sTramos[0];

                                        DataRow drI = oRem.dtTramos.NewRow();
                                        sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["Matricula"].S());
                                        drI = oRem.dtTramos.NewRow();
                                        drI["IdBitacora"] = iCont;
                                        drI["Matricula"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["Matricula"].S();
                                        drI["IdTipoPierna"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["IdTipoPierna"].S();
                                        drI["Origen"] = new DBRemision().DBGetIATAporICAO(sInicio);
                                        drI["Destino"] = new DBRemision().DBGetIATAporICAO(sFin);
                                        drI["OrigenICAO"] = sInicio;
                                        drI["DestinoICAO"] = sFin;

                                        drI["FechaLlegada"] = oRem.dtTramos.Rows[0]["FechaSalida"];

                                        dtFechaLlegada = oRem.dtTramos.Rows[0]["FechaSalida"].Dt();

                                        sTiempos = sTiempo.Split(':');

                                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]) * -1);
                                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]) * -1);

                                        drI["FechaSalida"] = dtFechaLlegada;

                                        ts = oRem.dtTramos.Rows[0]["FechaSalida"].Dt() - dtFechaLlegada;


                                        drI["CantPax"] = 0;
                                        drI["TotalTiempoCalzo"] = "00:00";
                                        drI["TotalTiempoVuelo"] = "00:00";

                                        drI["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S() : sTiempo;

                                        drI["RealVirtual"] = "Virtual";
                                        drI["VueloClienteId"] = oRem.dtTramos.Rows[0]["VueloClienteId"].S();
                                        drI["VueloContratoId"] = oRem.dtTramos.Rows[0]["VueloContratoId"].S();
                                        drI["TiempoEspera"] = "00:00";
                                        drI["SeCobra"] = 1;

                                        oRem.dtTramos.Rows.Add(drI);

                                        DataTable dtInicio = oRem.dtTramos.Clone();
                                        dtInicio.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]);

                                        for (int i = 0; i < oRem.dtTramos.Rows.Count - 1; i++)
                                        {
                                            dtInicio.ImportRow(oRem.dtTramos.Rows[i]);
                                        }

                                        oRem.dtTramos = null;
                                        oRem.dtTramos = dtInicio.Copy();
                                        dtInicio.Dispose();
                                        #endregion
                                        break;
                                }
                            }
                            else
                            {
                                // Agregar Ferry de Inicio y Final con la base Predeterminada
                                #region FERRY INICIAL Y FINAL
                                sInicio = sBasePre;
                                sFin = oRem.dtTramos.Rows[0]["OrigenICAO"].S();

                                // FERRY INICIAL
                                DataRow drI = oRem.dtTramos.NewRow();
                                sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, oRem.dtTramos.Rows[0]["Matricula"].S());
                                drI = oRem.dtTramos.NewRow();
                                drI["IdBitacora"] = iCont;
                                drI["Matricula"] = oRem.dtTramos.Rows[0]["Matricula"].S();
                                drI["IdTipoPierna"] = oRem.dtTramos.Rows[0]["IdTipoPierna"].S();
                                drI["Origen"] = new DBRemision().DBGetIATAporICAO(sInicio);
                                drI["Destino"] = new DBRemision().DBGetIATAporICAO(sFin);
                                drI["OrigenICAO"] = sInicio;
                                drI["DestinoICAO"] = sFin;

                                drI["FechaLlegada"] = oRem.dtTramos.Rows[0]["FechaSalida"];

                                dtFechaLlegada = oRem.dtTramos.Rows[0]["FechaSalida"].Dt();

                                string[] sTiempos = sTiempo.Split(':');

                                dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]) * -1);
                                dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]) * -1);

                                drI["FechaSalida"] = dtFechaLlegada;

                                ts = oRem.dtTramos.Rows[0]["FechaSalida"].Dt() - dtFechaLlegada;

                                drI["CantPax"] = 0;
                                drI["TotalTiempoCalzo"] = "00:00";
                                drI["TotalTiempoVuelo"] = "00:00";

                                drI["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S() : sTiempo;

                                drI["RealVirtual"] = "Virtual";
                                drI["VueloClienteId"] = oRem.dtTramos.Rows[0]["VueloClienteId"].S();
                                drI["VueloContratoId"] = oRem.dtTramos.Rows[0]["VueloContratoId"].S();
                                drI["TiempoEspera"] = "00:00";
                                drI["SeCobra"] = 1;

                                oRem.dtTramos.Rows.Add(drI);


                                // FERRY FINAL
                                DataRow drF = oRem.dtTramos.NewRow();
                                sInicio = sTramos[sTramos.Length - 1];
                                sFin = sBasePre;

                                sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["Matricula"].S());
                                drF = oRem.dtTramos.NewRow();
                                drF["IdBitacora"] = iCont;
                                drF["Matricula"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["Matricula"].S();
                                drF["IdTipoPierna"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["IdTipoPierna"].S();
                                drF["Origen"] = new DBRemision().DBGetIATAporICAO(sInicio);
                                drF["Destino"] = new DBRemision().DBGetIATAporICAO(sFin);
                                drF["OrigenICAO"] = sInicio;
                                drF["DestinoICAO"] = sFin;

                                drF["FechaSalida"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"];

                                dtFechaLlegada = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"].Dt();

                                sTiempos = sTiempo.Split(':');
                                dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]));
                                dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]));

                                drF["FechaLlegada"] = dtFechaLlegada;

                                drF["CantPax"] = 0;
                                drF["TotalTiempoCalzo"] = "00:00";
                                drF["TotalTiempoVuelo"] = "00:00";

                                ts = dtFechaLlegada - oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"].Dt();

                                drF["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S() : sTiempo;
                                drF["RealVirtual"] = "Virtual";
                                drF["VueloClienteId"] = oRem.dtTramos.Rows[0]["VueloClienteId"].S();
                                drF["VueloContratoId"] = oRem.dtTramos.Rows[0]["VueloContratoId"].S();
                                drF["TiempoEspera"] = "00:00";
                                drF["SeCobra"] = 1;

                                oRem.dtTramos.Rows.Add(drF);



                                DataTable dtFin = oRem.dtTramos.Clone();
                                dtFin.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]);

                                for (int i = 0; i < oRem.dtTramos.Rows.Count - 2; i++)
                                {
                                    dtFin.ImportRow(oRem.dtTramos.Rows[i]);
                                }

                                dtFin.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]);


                                oRem.dtTramos = null;
                                oRem.dtTramos = dtFin.Copy();
                                dtFin.Dispose();
                                #endregion
                            }

                            // Tramos finales
                            oRem.dtTramos = AplicaFactoresATiemposRemision(oRem.dtTramos, oRem, oRem.iIdContrato);
                            dtTramos = CalculaCostosRemision(oRem.iCobroTiempo, oRem.dtTramos, oRem);
                        }
                        #endregion
                        break;

                    case Enumeraciones.SeCobraFerrys.Reposicionamiento:
                        #region SE COBRAN FERRYS DE 'REPOSICIONAMIENTO'

                        if (oRem.bTiemposPactados)
                        {
                            foreach (DataRow row in oRem.dtTramos.Rows)
                            {
                                string sTiempo = ObtieneTramoPactado(oRem, row.S("Origen"), row.S("Destino"), row.S("Matricula"));

                                if (sTiempo == "00:00:00" || sTiempo == string.Empty || sTiempo == "00:00")
                                {
                                    row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                }
                                else
                                    row["TiempoCobrar"] = sTiempo;
                            }
                        }
                        else
                        {
                            foreach (DataRow row in oRem.dtTramos.Rows)
                            {
                                row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                            }
                        }

                        string sBasePreFR = string.Empty;
                        string sInicioFR = string.Empty;
                        string sFinFR = string.Empty;
                        int iAeropuertoFR = 0;
                        int iInicioFinFR = 0;

                        sInicioFR = sTramos[0];
                        sFinFR = sTramos[sTramos.Length - 1];

                        bool bAgregaFV = true;
                        foreach (DataRow dr in oRem.dtBases.Rows)
                        {
                            if (sInicioFR == dr.S("AeropuertoICAO") || sFinFR == dr.S("AeropuertoICAO"))
                            {
                                bAgregaFV = false;
                                break;
                            }
                        }

                        if (bAgregaFV)
                        {
                            foreach (DataRow dr in oRem.dtBases.Rows)
                            {
                                //sBaseFin = dr.S("AeropuertoICAO");

                                if (dr.S("IdTipoBase") == "1")
                                    sBasePreFR = dr.S("AeropuertoICAO");

                                if (sInicioFR == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "1")
                                {
                                    iAeropuertoFR = dr.S("IdAeropuerto").I();
                                    iInicioFinFR = 1;
                                    break;
                                }
                                else if (sFinFR == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "1")
                                {
                                    iAeropuertoFR = dr.S("IdAeropuerto").I();
                                    iInicioFinFR = 2;
                                    break;
                                }
                                else if (sInicioFR == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "2")
                                {
                                    iAeropuertoFR = dr.S("IdAeropuerto").I();
                                    iInicioFinFR = 1;
                                }
                                else if (sFinFR == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "2")
                                {
                                    iAeropuertoFR = dr.S("IdAeropuerto").I();
                                    iInicioFinFR = 2;
                                }
                            }

                            if (iInicioFinFR != 0 && iAeropuertoFR != 0)
                            {
                                switch (iInicioFinFR)
                                {
                                    case 1:
                                        AgregaFerryTabla(iInicioFinFR, oRem, iAeropuertoFR, string.Empty);
                                        break;
                                    case 2:
                                        AgregaFerryTabla(iInicioFinFR, oRem, iAeropuertoFR, string.Empty);
                                        break;
                                }
                            }
                            else
                            {
                                // Agregar ferrys virtuales 
                                // Si no sale de base y no regresa a base   --->   Agrega ferry inicial y final y cobrar el mas corto
                                AgregaFerryTabla(3, oRem, iAeropuertoFR, sBasePreFR);
                            }
                        }

                        oRem.dtTramos = AplicaFactoresATiemposRemision(oRem.dtTramos, oRem, oRem.iIdContrato);
                        dtTramos = CalculaCostosRemision(oRem.iCobroTiempo, oRem.dtTramos, oRem);

                        // Si no sale de base y no regresa a base -> Agrega ferry inicial y final y cobrar el mas corto

                        // Si, sale de base o regresa a base con pax, agregar el ferry pero no cobrar.

                        // Si, los ferrys de llegada o salida son reales -> cobrar el mas corto

                        #endregion
                        break;

                    case Enumeraciones.SeCobraFerrys.Ninguno:
                        #region NO SE COBRAN LOS FERRYS

                        if (oRem.bTiemposPactados)
                        {
                            foreach (DataRow row in oRem.dtTramos.Rows)
                            {
                                string sTiempo = ObtieneTramoPactado(oRem, row.S("Origen"), row.S("Destino"), row.S("Matricula"));

                                if (sTiempo == "00:00:00" || sTiempo == string.Empty || sTiempo == "00:00")
                                {
                                    row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                }
                                else
                                    row["TiempoCobrar"] = sTiempo;
                            }
                        }
                        else
                        {
                            foreach (DataRow row in oRem.dtTramos.Rows)
                            {
                                row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                            }
                        }

                        oRem.dtTramos = AplicaFactoresATiemposRemision(oRem.dtTramos, oRem, oRem.iIdContrato);
                        dtTramos = CalculaCostosRemision(oRem.iCobroTiempo, oRem.dtTramos, oRem);

                        #endregion
                        break;
                }

                return dtTramos.Copy();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string ObtieneTramoPactado(DatosRemision oRem, string sOrigen, string sDestino, string sMatricula)
        {
            try
            {
                //1.- Primero se consulta en los del contrato
                //2.- Buscar dentro de la tabla general
                //3.- Promedio de los ultimos 10 vuelos y si no existe... solicitarlo

                string sTiempo = string.Empty;

                if (oRem.dtTramosPactadosEsp.Rows.Count > 0)
                {
                    sOrigen = new DBRemision().DBGetICAOporIATA(sOrigen);
                    sDestino = new DBRemision().DBGetICAOporIATA(sDestino);

                    foreach (DataRow row in oRem.dtTramosPactadosEsp.Rows)
                    {
                        if (row.S("OrigenICAO") == sOrigen && row.S("DestinoICAO") == sDestino)
                        {
                            sTiempo = row.S("TiempoVuelo");
                            break;
                        }
                    }
                }

                oRem.dtTramosPactadosGen = new DBRemision().DBGetObtieneTramosPactadosGeneralesMatricula(sMatricula);
                if (sTiempo == string.Empty && oRem.dtTramosPactadosGen.Rows.Count > 0)
                {
                    foreach (DataRow row in oRem.dtTramosPactadosGen.Rows)
                    {
                        if ((row.S("AeropuertoICAO") == sOrigen && row.S("AeropuertoICAOD") == sDestino) || (row.S("AeropuertoIATA") == sOrigen && row.S("AeropuertoIATAD") == sDestino))
                        {
                            sTiempo = row.S("TiempoDeVuelo");
                            break;
                        }
                    }
                }

                if (sTiempo == string.Empty)
                {
                    // Usar Tiempo Real de la Bitácora
                    sTiempo = "00:00:00";
                }

                return sTiempo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string ObtieneTiempoFerrys(DatosRemision oRem, string sOrigen, string sDestino, string sMatricula)
        {
            try
            {
                //1.- Primero se consulta en los del contrato
                //2.- Buscar dentro de la tabla general
                //3.- Promedio de los ultimos 10 vuelos y si no existe... solicitarlo

                string sTiempo = string.Empty;

                if (oRem.dtTramosPactadosEsp.Rows.Count > 0)
                {
                    foreach (DataRow row in oRem.dtTramosPactadosEsp.Rows)
                    {
                        if (row.S("OrigenICAO") == sOrigen && row.S("DestinoICAO") == sDestino)
                        {
                            sTiempo = row.S("TiempoVuelo");
                            break;
                        }
                    }
                }

                oRem.dtTramosPactadosGen = new DBRemision().DBGetObtieneTramosPactadosGeneralesMatricula(sMatricula);
                if (sTiempo == string.Empty && oRem.dtTramosPactadosGen.Rows.Count > 0)
                {
                    foreach (DataRow row in oRem.dtTramosPactadosGen.Rows)
                    {
                        if ((row.S("AeropuertoICAO") == sOrigen && row.S("AeropuertoICAOD") == sDestino) || (row.S("AeropuertoIATA") == sOrigen && row.S("AeropuertoIATAD") == sDestino))
                        {
                            sTiempo = row.S("TiempoDeVuelo");
                            break;
                        }
                    }
                }

                if (sTiempo == string.Empty)
                {
                    // Obtiene promedio
                    sTiempo = new DBUtils().DBGetPromedioVuelo(sOrigen, sDestino);
                }

                if (sTiempo == string.Empty)
                    sTiempo = "00:00";

                if (sTiempo.Length == 5)
                    sTiempo = sTiempo + ":00";

                return sTiempo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable CalculaCostosRemision(int iCobroTiempo, DataTable dtTramos, DatosRemision oRem)
        {
            try
            {
                SnapshotRemision oSnap = (SnapshotRemision)System.Web.HttpContext.Current.Session["SnapshotRem"];

                // SE PREPARAN LAS TABLAS PARA LOS TIEMPOS DE VUELOS
                DataTable dtTramosNal = new DataTable();
                DataTable dtTramosInt = new DataTable();


                dtTramosInt = dtTramos.Clone();
                dtTramosNal = dtTramos.Clone();

                foreach (DataRow dr in dtTramos.Rows)
                {
                    if (dr["SeCobra"].S().I() == 1)
                    {
                        DataTable dt = new DBRemision().DBGetObtieneTipoDestinoAeropuertoPorICAO(dr.S("OrigenICAO"));
                        switch (dt.Rows[0]["TipoDestino"].S().ToUpper())
                        {
                            case "N":
                            case "F":
                                dtTramosNal.ImportRow(dr);
                                break;
                            case "E":
                                dtTramosInt.ImportRow(dr);
                                break;
                        }
                    }
                }


                // SE PREPARAN LAS TABLAS PARA LOS TIEMPOS DE ESPERA Y PARA LAS PERNOCTAS
                DataTable dtTramosNalEP = new DataTable();
                DataTable dtTramosIntEP = new DataTable();

                dtTramosIntEP = dtTramos.Clone();
                dtTramosNalEP = dtTramos.Clone();

                foreach (DataRow dr in dtTramos.Rows)
                {
                    if (dr["SeCobra"].S().I() == 1)
                    {
                        DataTable dt = new DBRemision().DBGetObtieneTipoDestinoAeropuertoPorICAO(dr.S("DestinoICAO"));
                        switch (dt.Rows[0]["TipoDestino"].S().ToUpper())
                        {
                            case "N":
                            case "F":
                                dtTramosNalEP.ImportRow(dr);
                                break;
                            case "E":
                                dtTramosIntEP.ImportRow(dr);
                                break;
                        }
                    }
                }

                // AJUSTA FACTORES DE TRAMOS, ES DECIR, SOLO SE MARCA EL FACTOR CUANDO REALMENTE APLICA
                oRem.bAplicaFactorTramoNacional = false;
                oRem.bAplicaFactorTramoInternacional = false;

                if (oRem.dFactorTramosInt > 0 && oRem.dFactorTramosInt != 1)
                {
                    if (dtTramosInt.Rows.Count > 0)
                        oRem.bAplicaFactorTramoInternacional = true;
                    else
                        oRem.bAplicaFactorTramoInternacional = false;
                }
                else
                    oRem.bAplicaFactorTramoInternacional = false;

                if(oRem.dFactorTramosNal > 0 && oRem.dFactorTramosNal != 1)
                {
                    if (dtTramosNal.Rows.Count > 0)
                        oRem.bAplicaFactorTramoNacional = true;
                    else
                        oRem.bAplicaFactorTramoNacional = false;
                }
                else
                    oRem.bAplicaFactorTramoNacional = false;


                // SE PREPARA LA TABLA CON LOS CONCEPTOS EN COBROS
                DataTable dtConceptos = new DBRemision().DBGetConceptosRemision;
                DataTable dtFinal = new DataTable();
                dtFinal.Columns.Add("IdConcepto");
                dtFinal.Columns.Add("Concepto");
                dtFinal.Columns.Add("Cantidad");
                dtFinal.Columns.Add("CostoDirecto");
                dtFinal.Columns.Add("CostoComb");
                dtFinal.Columns.Add("CombustibleAumento");
                dtFinal.Columns.Add("TarifaDlls", typeof(decimal));
                dtFinal.Columns.Add("Importe", typeof(decimal));
                dtFinal.Columns.Add("HrDescontar");

                float fTiempoVueloN = 0;
                float fTiempoVueloI = 0;

                float fTiempoEsperaGlobal = 0;
                float fTiempoEsperaN = 0;
                float fTiempoEsperaI = 0;

                int iNoPernoctasGlobal = 0;
                float fNoPernoctasN = 0;
                float fNoPernoctasI = 0;

                string sAirPortO = dtTramos.Rows[0]["OrigenICAO"].S();
                DataTable dtComb = new DBRemision().DBGetConsultaTarifasVuelo(oRem.iIdContrato, oRem.lIdRemision, sAirPortO);
                // Agregar consulta de Tarifas para la Remisión. y asignar en casos 1 y 2

                if (dtComb.Rows.Count > 0)
                {
                    oRem.dTarifaVueloNal = dtComb.Rows[0]["TarifaVueloNal"].S().D();
                    oRem.dTarifaVueloInt = dtComb.Rows[0]["TarifaVueloInt"].S().D();

                    oSnap.oDatosRem.dTarifaVloNalComb = oRem.dTarifaVueloNal;
                    oSnap.oDatosRem.dTarifaVloIntComb = oRem.dTarifaVueloInt;
                }


                float fTiempoEsperaReal = 0;
                float fTotalTiempoEspera = 0;
                float fFactorNal = 0;
                float fFactorInt = 0;

                decimal dCombN = 0;
                decimal dCombI = 0;

                foreach (DataRow dr in dtConceptos.Rows)
                {
                    DataRow row = dtFinal.NewRow();
                    row["IdConcepto"] = dr.S("IdConcepto");
                    row["Concepto"] = dr.S("Descripcion");

                    string sCantidad = string.Empty;
                    float fTiempoT = 0;
                    float fTV = 0;
                    decimal dTarifaTEN = 0;
                    decimal dTarifaP = 0;

                    string dTarifaDlls = string.Empty;


                    switch (dr.S("IdConcepto"))
                    {
                        case "1":
                            #region TIEMPO VUELO NACIONAL

                            decimal dTarifaCostoDirN = 0;
                            decimal dCostoCombuN = 0;
                            decimal dCostoDirectoN = 0;

                            if (oRem.oLstIntercambios.Count > 0)
                            {
                                if (oRem.oLstIntercambios[0].dTarifaNalInter > 0)
                                {
                                    dTarifaCostoDirN = oRem.oLstIntercambios[0].dTarifaNalInter;
                                }
                                else
                                {
                                    decimal dCombusN = dtComb.Rows[0]["CombusN"].S().D();
                                    dCombN = dCombusN;
                                    dCombusN = (dCombusN * oRem.oLstIntercambios[0].dGaolnesInter);

                                    dCostoCombuN = dCombusN;

                                    if (oRem.oLstIntercambios[0].dCostoDirNalInter > 0)
                                    {
                                        dCostoDirectoN = oRem.oLstIntercambios[0].dCostoDirNalInter;
                                        dTarifaCostoDirN = oRem.oLstIntercambios[0].dCostoDirNalInter + dCombusN;
                                    }
                                }
                            }
                            else
                            {
                                dTarifaCostoDirN = oRem.dTarifaVueloNal;

                                dCombN = dtComb.Rows[0]["CombusN"].S().D();
                                dCostoCombuN = dtComb.Rows[0]["CombustibleNal"].S().D();
                                dCostoDirectoN = dtComb.Rows[0]["CostoDirectoNalV"].S().D();
                            }

                            oRem.dTarifaVueloNal = dTarifaCostoDirN;

                            sCantidad = ObtieneTotalTiempo(dtTramosNal, "TiempoCobrar", ref fTiempoT);
                            fTiempoVueloN = fTiempoT;

                            row["Cantidad"] = sCantidad;

                            row["CostoDirecto"] = dCostoDirectoN;

                            row["CostoComb"] = dCostoCombuN;

                            row["CombustibleAumento"] = dCombN;

                            row["TarifaDlls"] = Math.Round(dTarifaCostoDirN, 2);

                            row["Importe"] = fTiempoT.S().D() * dTarifaCostoDirN;

                            row["HrDescontar"] = sCantidad;

                            #endregion
                            break;
                        case "2":
                            #region TIEMPO VUELO INTERNACIONAL

                            decimal dTarifaCostoDirI = 0;
                            decimal dCostoCombuI = 0;
                            decimal dCostoDirectoI = 0;

                            if (oRem.oLstIntercambios.Count > 0)
                            {
                                if (oRem.oLstIntercambios[0].dTarifaIntInter > 0)
                                    dTarifaCostoDirI = oRem.oLstIntercambios[0].dTarifaIntInter;
                                else
                                {
                                    decimal CombusI = dtComb.Rows[0]["CombusI"].S().D();
                                    dCombI = CombusI;
                                    CombusI = (CombusI * oRem.oLstIntercambios[0].dGaolnesInter);

                                    dCostoCombuI = CombusI;
                                    dCostoDirectoI = oRem.oLstIntercambios[0].dCostoDirNalInter;

                                    if (oRem.oLstIntercambios[0].dCostoDirNalInter > 0)
                                    {
                                        dTarifaCostoDirI = oRem.oLstIntercambios[0].dCostoDirNalInter + CombusI;
                                    }
                                }
                            }
                            else
                            {
                                dTarifaCostoDirI = oRem.dTarifaVueloInt;

                                dCombI = dtComb.Rows[0]["CombusI"].S().D();
                                dCostoCombuI = dtComb.Rows[0]["CombustibleInt"].S().D();
                                dCostoDirectoI = dtComb.Rows[0]["CostoDirectoIntV"].S().D();
                            }

                            oRem.dTarifaVueloInt = dTarifaCostoDirI;

                            sCantidad = ObtieneTotalTiempo(dtTramosInt, "TiempoCobrar", ref fTiempoT);
                            fTiempoVueloI = fTiempoT;

                            row["Cantidad"] = sCantidad;

                            row["CostoDirecto"] = dCostoDirectoI;

                            row["CostoComb"] = dCostoCombuI;

                            row["CombustibleAumento"] = dCombI;

                            row["TarifaDlls"] = Math.Round(dTarifaCostoDirI, 2);

                            row["Importe"] = fTiempoT.S().D() * dTarifaCostoDirI;

                            row["HrDescontar"] = sCantidad;
                            #endregion
                            break;
                        case "3":
                            #region TIEMPO DE ESPERA NACIONAL

                            float fEsperaN = 0;
                            float fEsperaI = 0;
                            float fTotalTiempoCobrar = 0;


                            // SE SUMA EL TOTAL DEL TIEMPO DE ESPERA EN NACIONALES E INTERNACIONALES
                            ObtieneTotalTiempo(dtTramos, "TiempoEspera", ref fTotalTiempoEspera);

                            if (fTotalTiempoEspera > 0)
                            {
                                ObtieneTotalTiempo(dtTramos, "TiempoOriginal", ref fTotalTiempoCobrar);

                                oSnap.oDatosRem.dTiempoEsperaGeneral = fTotalTiempoEspera.S().D();
                                oSnap.oDatosRem.dTiempoVueloGeneral = fTotalTiempoCobrar.S().D();

                                //sCantidad = ObtieneTotalTiempo(dtTramosNalEP, "TiempoEspera", ref fEsperaN);
                                sCantidad = ObtieneTotalTiempo(dtTramosIntEP, "TiempoEspera", ref fEsperaI);

                                fFactorInt = fEsperaI / fTotalTiempoEspera;
                                fFactorNal = (1 - fFactorInt);


                                if (oRem.bAplicaEsperaLibre)
                                {
                                    if (oRem.dHorasPorVuelo > 0)
                                    {
                                        fTV = float.Parse(oRem.dHorasPorVuelo.S());
                                    }
                                    else if (oRem.dFactorHrVuelo > 0)
                                        fTV = (fTotalTiempoCobrar * float.Parse(oRem.dFactorHrVuelo.S()));

                                    sCantidad = RestaTiempoString(fTotalTiempoEspera, fTV);

                                    oSnap.oDatosRem.sTotalTiempoEsperaCobrar = sCantidad;

                                    fTiempoEsperaReal = ConvierteTiempoaDecimal(sCantidad);
                                }
                                else
                                {
                                    fTiempoEsperaReal = fTotalTiempoEspera;
                                }

                                oSnap.oDatosRem.sTiempoEsperaGeneral = ConvierteDecimalATiempo(oSnap.oDatosRem.dTiempoEsperaGeneral);
                                oSnap.oDatosRem.sTiempoVueloGeneral = ConvierteDecimalATiempo(oSnap.oDatosRem.dTiempoVueloGeneral);
                                oSnap.oDatosRem.dTotalTiempoEsperaCobrar = fTiempoEsperaReal.S().D();

                                // SE DETERMINA EL NUMERO DE PERNOCTAS DE TODO EL TIEMPO DE ESPERA
                                for (int i = 0; i < 100; i++)
                                {
                                    if (fTiempoEsperaReal >= oRem.iHorasPernocta)
                                    {
                                        iNoPernoctasGlobal++;
                                        fTiempoEsperaReal = fTiempoEsperaReal - 24;
                                    }
                                    else
                                    {
                                        if (fTiempoEsperaReal > 0 && fTiempoEsperaReal < oRem.iHorasPernocta)
                                        {
                                            fTiempoEsperaGlobal = fTiempoEsperaReal;
                                            break;
                                        }
                                        else
                                        {
                                            fTiempoEsperaReal = 0;
                                            fTiempoEsperaGlobal = fTiempoEsperaReal;
                                            break;
                                        }
                                    }
                                }

                                //int iPernoctasLibres = new DBRemision().DBGetObtienePernoctasLibres(oRem.iIdContrato);
                                //if (iPernoctasLibres > 0)
                                //{
                                //    if (iNoPernoctasGlobal == iPernoctasLibres)
                                //    {
                                //        iNoPernoctasGlobal = 0;
                                //        iPernoctasLibres = 0;
                                //    }
                                //    else if (iNoPernoctasGlobal < iPernoctasLibres)
                                //    {
                                //        iPernoctasLibres = iPernoctasLibres - iNoPernoctasGlobal;
                                //        iNoPernoctasGlobal = 0;
                                //    }
                                //    else if (iNoPernoctasGlobal > iPernoctasLibres)
                                //    {
                                //        iNoPernoctasGlobal = iNoPernoctasGlobal - iPernoctasLibres;
                                //        iPernoctasLibres = 0;
                                //    }

                                //    // Actualiza Pernoctas en el periodo actual
                                //    long iResult = new DBRemision().DBSetActualizaPernoctasDisponibles(oRem.iIdContrato, iPernoctasLibres);
                                //}

                                // Termina el proceso y asigna cantidades

                                fTiempoEsperaN = fTiempoEsperaGlobal * fFactorNal;
                                sCantidad = ConvierteDecimalATiempo(fTiempoEsperaN.S().D());

                                if (oRem.bSeCobreEspera)
                                {
                                    sCantidad = AgregaFactoresEsperaPernoctas(sCantidad, 1, oRem);
                                    fTiempoEsperaN = ConvierteTiempoaDecimal(sCantidad);

                                    row["Cantidad"] = sCantidad;

                                    if (oRem.dTarifaNalEspera > 0)
                                        dTarifaTEN = oRem.dTarifaNalEspera;
                                    else
                                    {
                                        dTarifaTEN = (oRem.dTarifaVueloNal * oRem.dPorTarifaNalEspera);
                                    }
                                }
                                else
                                {
                                    oSnap.oDatosRem.sTotalTiempoEsperaCobrar = "00:00";

                                    row["Cantidad"] = "00:00";
                                    dTarifaTEN = 0;
                                }
                            }
                            else
                            {
                                oSnap.oDatosRem.sTotalTiempoEsperaCobrar = "00:00";

                                row["Cantidad"] = "00:00";
                                dTarifaTEN = 0;

                                if (oRem.dTarifaNalEspera > 0)
                                    dTarifaTEN = oRem.dTarifaNalEspera;
                                else
                                {
                                    dTarifaTEN = (oRem.dTarifaVueloNal * oRem.dPorTarifaNalEspera);
                                }

                                fTiempoEsperaN = 0;
                            }

                            row["TarifaDlls"] = Math.Round(dTarifaTEN, 2);

                            row["Importe"] = fTiempoEsperaN.S().D() * dTarifaTEN;

                            if (oRem.bSeDescuentaEsperaNal)
                            {
                                decimal dHr = (fTiempoEsperaN.S().D() * oRem.dFactorEHrVueloNal);
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }
                            else
                                row["HrDescontar"] = "00:00";

                            #endregion
                            break;
                        case "4":
                            #region TIEMPO DE ESPERA INTERNACIONAL

                            if (fTotalTiempoEspera > 0)
                            {
                                fTiempoEsperaI = fTiempoEsperaGlobal * fFactorInt;
                                sCantidad = ConvierteDecimalATiempo(fTiempoEsperaI.S().D());

                                if (oRem.bSeCobreEspera)
                                {
                                    sCantidad = AgregaFactoresEsperaPernoctas(sCantidad, 1, oRem);
                                    fTiempoEsperaI = ConvierteTiempoaDecimal(sCantidad);

                                    row["Cantidad"] = sCantidad;

                                    if (oRem.dTarifaNalEspera > 0)
                                        dTarifaTEN = oRem.dTarifaIntEspera;
                                    else
                                    {
                                        dTarifaTEN = (oRem.dTarifaVueloInt * oRem.dPorTarifaNalEspera);
                                    }
                                }
                                else
                                {
                                    row["Cantidad"] = "00:00";
                                    dTarifaTEN = 0;
                                }

                            }
                            else
                            {
                                row["Cantidad"] = "00:00";
                                dTarifaTEN = 0;

                                if (oRem.dTarifaNalEspera > 0)
                                    dTarifaTEN = oRem.dTarifaIntEspera;
                                else
                                {
                                    dTarifaTEN = (oRem.dTarifaVueloInt * oRem.dPorTarifaNalEspera);
                                }

                                fTiempoEsperaI = 0;
                            }

                            row["TarifaDlls"] = Math.Round(dTarifaTEN, 2);

                            row["Importe"] = fTiempoEsperaI.S().D() * dTarifaTEN;

                            if (oRem.bSeDescuentaEsperaInt)
                            {
                                decimal dHr = (fTiempoEsperaI.S().D() * oRem.dFactorEHrVueloInt);
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }
                            else
                                row["HrDescontar"] = "00:00";

                            #endregion
                            break;
                        case "5":
                            #region PERNOCTA NACIONAL

                            if (fTotalTiempoEspera > 0)
                            {
                                fNoPernoctasI = float.Parse(Math.Round(iNoPernoctasGlobal * fFactorInt, 0).S());

                                if ((iNoPernoctasGlobal - fNoPernoctasI) > 0)
                                {
                                    fNoPernoctasN = iNoPernoctasGlobal - fNoPernoctasI;
                                }
                                else
                                    fNoPernoctasN = 0;


                                if (oRem.bSeCobraPernoctas)
                                {
                                    string sNoPernoctas = AgregaFactoresEsperaPernoctas(fNoPernoctasN.S(), 2, oRem);
                                    fNoPernoctasN = float.Parse(sNoPernoctas);

                                    row["Cantidad"] = fNoPernoctasN.S();

                                    if (oRem.dTarifaDolaresNal > 0)
                                        dTarifaP = oRem.dTarifaDolaresNal;
                                    else
                                    {
                                        dTarifaP = (oRem.dTarifaVueloNal * oRem.dPorTarifaVueloNal);
                                    }
                                }
                                else
                                {
                                    row["Cantidad"] = "0";
                                    dTarifaP = 0;
                                }
                            }
                            else
                            {
                                row["Cantidad"] = "0";
                                dTarifaP = 0;

                                if (oRem.dTarifaDolaresNal > 0)
                                    dTarifaP = oRem.dTarifaDolaresNal;
                                else
                                {
                                    dTarifaP = (oRem.dTarifaVueloNal * oRem.dPorTarifaVueloNal);
                                }

                                fNoPernoctasN = 0;
                            }

                            row["TarifaDlls"] = Math.Round(dTarifaP, 2);

                            row["Importe"] = decimal.Parse(fNoPernoctasN.S()) * dTarifaP;

                            if (oRem.bSeDescuentanPerNal)
                            {
                                decimal dHr = (decimal.Parse(fNoPernoctasN.S()) * oRem.dFactorConvHrVueloNal);
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }
                            else
                                row["HrDescontar"] = "00:00";
                            #endregion
                            break;
                        case "6":
                            #region PERNOCTA INTERNACIONAL

                            if (fTotalTiempoEspera > 0)
                            {
                                //fNoPernoctasI = iNoPernoctasGlobal * fFactorInt;

                                if (oRem.bSeCobraPernoctas)
                                {
                                    string sNoPernoctas = AgregaFactoresEsperaPernoctas(fNoPernoctasI.S(), 2, oRem);
                                    fNoPernoctasI = float.Parse(sNoPernoctas);

                                    row["Cantidad"] = fNoPernoctasI.S();

                                    if (oRem.dTarifaDolaresInt > 0)
                                        dTarifaP = oRem.dTarifaDolaresInt;
                                    else
                                    {
                                        dTarifaP = (oRem.dTarifaVueloInt * oRem.dPorTarifaVueloInt);
                                    }
                                }
                                else
                                {
                                    row["Cantidad"] = "0";
                                    dTarifaP = 0;
                                }
                            }
                            else
                            {
                                row["Cantidad"] = "0";
                                dTarifaP = 0;

                                if (oRem.dTarifaDolaresInt > 0)
                                    dTarifaP = oRem.dTarifaDolaresInt;
                                else
                                {
                                    dTarifaP = (oRem.dTarifaVueloInt * oRem.dPorTarifaVueloInt);
                                }

                                fNoPernoctasI = 0;
                            }

                            row["TarifaDlls"] = Math.Round(dTarifaP, 2);

                            row["Importe"] = decimal.Parse(fNoPernoctasI.S()) * dTarifaP;

                            if (oRem.bSeDescuentanPerInt)
                            {
                                decimal dHr = (decimal.Parse(fNoPernoctasI.S()) * oRem.dFactorConvHrVueloInt);
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }
                            else
                                row["HrDescontar"] = "00:00";
                            #endregion
                            break;
                    }

                    dtFinal.Rows.Add(row);
                }

                return dtFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string RestaTiempoString(float fTiempoVuelo, float fTiempoResta)
        {
            try
            {
                if (fTiempoResta < fTiempoVuelo)
                {
                    float dTotal = (fTiempoVuelo - fTiempoResta);
                    return ConvierteDecimalATiempo(dTotal.S().D());
                }
                else
                    return "00:00";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string DefineTiempoCobrar(int iCobroTiempo, string sTiempoVuelo, float fTiempoSuma, string sMatricula, string sTiempoCalzo, DatosRemision odRem)
        {
            try
            {   //iCobroTiempo  ===>    1.- Tiempo de Vuelo     2.- Tiempo de Calzo
                //sTiempoVuelo  ===>    Tiempo a utilizar
                //fTiempoSuma   ===>    iMasMinutos
                //sMatricula    ===>    Matricula volada
                if (iCobroTiempo == 1)
                {
                    bool bEsHelicoptero = false;
                    DataTable dt = new DBRemision().DBGetObtieneMatriculasHelicoptero;
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["Matricula"].S() == sMatricula)
                            {
                                bEsHelicoptero = true;
                                break;
                            }
                        }
                    }

                    if (!bEsHelicoptero)
                    {
                        if (fTiempoSuma > 0)
                        {
                            float dTiempoV = ConvierteTiempoaDecimal(sTiempoVuelo);
                            float dTotal = (dTiempoV + (fTiempoSuma / 60));

                            return ConvierteDecimalATiempo(dTotal.S().D());
                        }
                    }
                    else
                    {
                        //sTiempoVuelo = sTiempoCalzo;
                        if (odRem.iMasMinutosHelicoptero > 0)
                        {
                            float dTiempoV = ConvierteTiempoaDecimal(sTiempoVuelo);
                            float dTotal = (dTiempoV + (float.Parse(odRem.iMasMinutosHelicoptero.S()) / 60));

                            sTiempoVuelo = ConvierteDecimalATiempo(dTotal.S().D());
                        }
                    }
                }

                return sTiempoVuelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string DefineTiempoCobrar(int iCobroTiempo, string sTiempoVuelo, float fTiempoSuma)
        {
            try
            {   //iCobroTiempo  ===>    1.- Tiempo de Vuelo     2.- Tiempo de Calzo
                //sTiempoVuelo  ===>    Tiempo a utilizar
                //fTiempoSuma   ===>    iMasMinutos
                //sMatricula    ===>    Matricula volada

                iCobroTiempo = 2;
                if (iCobroTiempo == 1)
                {
                    if (fTiempoSuma > 0)
                    {
                        float dTiempoV = ConvierteTiempoaDecimal(sTiempoVuelo);
                        float dTotal = (dTiempoV + (fTiempoSuma / 60));

                        return ConvierteDecimalATiempo(dTotal.S().D());
                    }
                }

                return sTiempoVuelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static float RestaTiempoFloat(float fTiempo, float fTiempoResta)
        {
            try
            {
                if (fTiempoResta <= fTiempo)
                {
                    return (fTiempo - fTiempoResta);
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DataTable CreaEstructuraFinal()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdTramo");
                dt.Columns.Add("Matricula");
                dt.Columns.Add("Origen");
                dt.Columns.Add("Destino");
                dt.Columns.Add("NalInt"); // 1.- Nacional    2.- Internacional
                dt.Columns.Add("FechaSalida");
                dt.Columns.Add("FechaLlegada");
                dt.Columns.Add("Pax");
                dt.Columns.Add("TiempoVueloReal");
                dt.Columns.Add("TiempoCobrar");
                dt.Columns.Add("TiempoEspera");
                dt.Columns.Add("TipoPierna");
                dt.Columns.Add("RealVirtual");

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void HabilitaTexto(TextBox txt, string stexto)
        {
            switch (stexto.S())
            {
                case "[Sin Filtro]":
                    txt.Enabled = false;
                    txt.Text = string.Empty;
                    break;
                case "Solo activos":
                    txt.Enabled = false;
                    txt.Text = string.Empty;
                    break;
                case "Solo inactivos":
                    txt.Enabled = false;
                    txt.Text = string.Empty;
                    break;
                default:
                    txt.Enabled = true;
                    break;
            }
        }

        public static string ObtieneTotalTiempo(DataTable dtTramos, string sColumna, ref float fTiempoT)
        {
            try
            {
                string sCantidad = string.Empty;
                int iHoras = 0;
                double iMinutos = 0;
                double fTiempo = 0;

                foreach (DataRow row1 in dtTramos.Rows)
                {
                    if (row1[sColumna].S() != string.Empty)
                    {
                        string[] sPartes = row1[sColumna].S().Split(':');

                        iHoras += sPartes[0].S().I();
                        iMinutos += sPartes[1].S().I();
                        //iHoras += row1[sColumna].S().Substring(0, 2).S().I();
                        //iMinutos += row1[sColumna].S().Substring(3, 2).S().I();
                    }
                }

                iMinutos = Math.Round(iMinutos, 2);


                fTiempo = iHoras + (iMinutos / 60);
                fTiempoT = float.Parse(fTiempo.S());

                sCantidad = ConvierteDecimalATiempo(fTiempo.S().D());

                return sCantidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal ObtieneTotalTabla(DataTable dtTabla, string sColumna)
        {
            try
            {
                decimal dColumna = 0;

                foreach (DataRow row1 in dtTabla.Rows)
                {
                    if (row1[sColumna].S() != string.Empty)
                    {
                        decimal dImp = row1[sColumna].S().D();
                        dColumna += dImp;
                    }
                }

                return dColumna;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ConvierteDecimalATiempo(decimal fTiempo)
        {
            try
            {
                if (fTiempo != 0)
                {
                    bool ban = false;

                    if (fTiempo < 0)
                    {
                        ban = true;
                        fTiempo = fTiempo * (-1);
                    }

                    int iHoras = 0;
                    int iMinutos = 0;

                    iHoras = Math.Truncate(fTiempo).S().I();
                    iMinutos = Math.Round(((fTiempo - Math.Truncate(fTiempo)).S().D() * 60)).S().I();

                    if (iMinutos == 60)
                    {
                        iHoras++;
                        iMinutos = 0;
                    }

                    return ban ? "-" + iHoras.S().PadLeft(2, '0') + ":" + iMinutos.S().PadLeft(2, '0') : iHoras.S().PadLeft(2, '0') + ":" + iMinutos.S().PadLeft(2, '0');
                }
                else
                    return "00:00";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ConvierteDoubleATiempo(double fTiempo)
        {
            try
            {
                if (fTiempo != 0)
                {
                    int iHoras = 0;
                    int iMinutos = 0;

                    iHoras = Math.Truncate(fTiempo).S().I();

                    double dbMinutos = ((fTiempo - Math.Truncate(fTiempo).S().Db()) * 60).S().Db();

                    iMinutos = Math.Truncate(decimal.Round(dbMinutos.ToString().S().D(), 4)).S().I();

                    if (iMinutos == 60)
                    {
                        iHoras++;
                        iMinutos = 0;
                    }

                    return iHoras.S().PadLeft(2, '0') + ":" + iMinutos.S().PadLeft(2, '0');
                }
                else
                    return "00:00";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ObtieneParametroPorClave(string sClave)
        {
            try
            {
                return new DBParametros().DBGetObtieneValorParametroPorClave(sClave);
            }
            catch (Exception x) { throw x; }
        }

        public static DataTable AgregaFerryTabla(int iPosicion, DatosRemision oRem, int iIdAeropuerto, string sBasePre)
        {
            try
            {
                string sBase = string.Empty;
                string sInicio = string.Empty;
                string sFin = string.Empty;

                DataTable dtAero = new DBRemision().DBGetConsultaAeropuertoId(iIdAeropuerto);
                if (dtAero.Rows.Count > 0)
                    sBase = dtAero.Rows[0]["AeropuertoICAO"].S();


                string sTiempo = string.Empty;
                DateTime dtFechaLlegada;
                TimeSpan ts;
                DataRow drI;
                DataRow drF;
                int iCont = 1;
                DataTable dtFin;

                switch (iPosicion)
                {
                    case 1: // Agregar Ferry Final
                        #region Ferry Final
                        sInicio = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["DestinoICAO"].S();
                        sFin = sBase;

                        sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["Matricula"].S());
                        drF = oRem.dtTramos.NewRow();
                        drF["IdBitacora"] = iCont;
                        drF["Matricula"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["Matricula"].S();
                        drF["IdTipoPierna"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["IdTipoPierna"].S();
                        drF["Origen"] = new DBRemision().DBGetIATAporICAO(sInicio);
                        drF["Destino"] = new DBRemision().DBGetIATAporICAO(sFin);
                        drF["OrigenICAO"] = sInicio;
                        drF["DestinoICAO"] = sFin;

                        drF["FechaSalida"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"];

                        dtFechaLlegada = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"].Dt();

                        string[] sTiempos = sTiempo.Split(':');
                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]));
                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]));

                        drF["FechaLlegada"] = dtFechaLlegada;

                        drF["CantPax"] = 0;
                        drF["TotalTiempoCalzo"] = "00:00";
                        drF["TotalTiempoVuelo"] = "00:00";

                        ts = dtFechaLlegada - oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"].Dt();

                        drF["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S().Substring(0, 5) : sTiempo;
                        drF["RealVirtual"] = "Virtual";
                        drF["VueloClienteId"] = oRem.dtTramos.Rows[0]["VueloClienteId"].S();
                        drF["VueloContratoId"] = oRem.dtTramos.Rows[0]["VueloContratoId"].S();
                        drF["TiempoEspera"] = "00:00";
                        drF["SeCobra"] = 0;

                        oRem.dtTramos.Rows.Add(drF);
                        #endregion

                        break;

                    case 2: // Agregar Ferry Inicial
                        #region Ferry Inicial
                        sInicio = sBase;
                        sFin = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["OrigenICAO"].S();

                        drI = oRem.dtTramos.NewRow();
                        sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["Matricula"].S());
                        drI = oRem.dtTramos.NewRow();
                        drI["IdBitacora"] = iCont;
                        drI["Matricula"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["Matricula"].S();
                        drI["IdTipoPierna"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["IdTipoPierna"].S();
                        drI["Origen"] = new DBRemision().DBGetIATAporICAO(sInicio);
                        drI["Destino"] = new DBRemision().DBGetIATAporICAO(sFin);
                        drI["OrigenICAO"] = sInicio;
                        drI["DestinoICAO"] = sFin;

                        drI["FechaLlegada"] = oRem.dtTramos.Rows[0]["FechaSalida"];

                        dtFechaLlegada = oRem.dtTramos.Rows[0]["FechaSalida"].Dt();
                        sTiempos = sTiempo.Split(':');
                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]) * -1);
                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]) * -1);

                        drI["FechaSalida"] = dtFechaLlegada;

                        ts = oRem.dtTramos.Rows[0]["FechaSalida"].Dt() - dtFechaLlegada;

                        drI["CantPax"] = 0;
                        drI["TotalTiempoCalzo"] = "00:00";
                        drI["TotalTiempoVuelo"] = "00:00";

                        drI["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S().Substring(0,5) : sTiempo.Substring(0,5);

                        drI["RealVirtual"] = "Virtual";
                        drI["VueloClienteId"] = oRem.dtTramos.Rows[0]["VueloClienteId"].S();
                        drI["VueloContratoId"] = oRem.dtTramos.Rows[0]["VueloContratoId"].S();
                        drI["TiempoEspera"] = "00:00";
                        drI["SeCobra"] = 0;

                        oRem.dtTramos.Rows.Add(drI);

                        DataTable dtInicio = oRem.dtTramos.Clone();
                        dtInicio.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]);

                        for (int i = 0; i < oRem.dtTramos.Rows.Count - 1; i++)
                        {
                            dtInicio.ImportRow(oRem.dtTramos.Rows[i]);
                        }

                        oRem.dtTramos = null;
                        oRem.dtTramos = dtInicio.Copy();
                        dtInicio.Dispose();
                        #endregion

                        break;

                    case 3: // Ferry Inicial y Final
                        #region Ferry Inicial y Final

                        // FERRY INICIAL
                        sInicio = sBasePre;
                        sFin = oRem.dtTramos.Rows[0]["OrigenICAO"].S();

                        drI = oRem.dtTramos.NewRow();
                        sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, oRem.dtTramos.Rows[0]["Matricula"].S());
                        drI = oRem.dtTramos.NewRow();
                        drI["IdBitacora"] = iCont;
                        drI["Matricula"] = oRem.dtTramos.Rows[0]["Matricula"].S();
                        drI["IdTipoPierna"] = oRem.dtTramos.Rows[0]["IdTipoPierna"].S();
                        drI["Origen"] = new DBRemision().DBGetIATAporICAO(sInicio);
                        drI["Destino"] = new DBRemision().DBGetIATAporICAO(sFin);
                        drI["OrigenICAO"] = sInicio;
                        drI["DestinoICAO"] = sFin;

                        drI["FechaLlegada"] = oRem.dtTramos.Rows[0]["FechaSalida"];

                        dtFechaLlegada = oRem.dtTramos.Rows[0]["FechaSalida"].Dt();
                        sTiempos = sTiempo.Split(':');

                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]) * -1);
                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]) * -1);

                        drI["FechaSalida"] = dtFechaLlegada;

                        ts = oRem.dtTramos.Rows[0]["FechaSalida"].Dt() - dtFechaLlegada;

                        drI["CantPax"] = 0;
                        drI["TotalTiempoCalzo"] = "00:00";
                        drI["TotalTiempoVuelo"] = "00:00";

                        drI["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S().Substring(0,5) : sTiempo.Substring(0, 5);

                        drI["RealVirtual"] = "Virtual";
                        drI["VueloClienteId"] = oRem.dtTramos.Rows[0]["VueloClienteId"].S();
                        drI["VueloContratoId"] = oRem.dtTramos.Rows[0]["VueloContratoId"].S();
                        drI["TiempoEspera"] = "00:00";
                        drI["SeCobra"] = 0;

                        oRem.dtTramos.Rows.Add(drI);


                        // FERRY FINAL
                        drF = oRem.dtTramos.NewRow();
                        sInicio = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["DestinoICAO"].S();
                        sFin = sBasePre;

                        sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["Matricula"].S());
                        drF = oRem.dtTramos.NewRow();
                        drF["IdBitacora"] = iCont;
                        drF["Matricula"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["Matricula"].S();
                        drF["IdTipoPierna"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["IdTipoPierna"].S();
                        drF["Origen"] = new DBRemision().DBGetIATAporICAO(sInicio);
                        drF["Destino"] = new DBRemision().DBGetIATAporICAO(sFin);
                        drF["OrigenICAO"] = sInicio;
                        drF["DestinoICAO"] = sFin;

                        drF["FechaSalida"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"];

                        dtFechaLlegada = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"].Dt();

                        string[] sTiempos3 = sTiempo.Split(':');
                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos3[0]));
                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos3[1]));

                        drF["FechaLlegada"] = dtFechaLlegada;

                        drF["CantPax"] = 0;
                        drF["TotalTiempoCalzo"] = "00:00";
                        drF["TotalTiempoVuelo"] = "00:00";

                        ts = dtFechaLlegada - oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"].Dt();

                        drF["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S().Substring(0, 5) : sTiempo.Substring(0, 5);
                        drF["RealVirtual"] = "Virtual";
                        drF["VueloClienteId"] = oRem.dtTramos.Rows[0]["VueloClienteId"].S();
                        drF["VueloContratoId"] = oRem.dtTramos.Rows[0]["VueloContratoId"].S();
                        drF["TiempoEspera"] = "00:00";
                        drF["SeCobra"] = 0;

                        oRem.dtTramos.Rows.Add(drF);


                        dtFin = oRem.dtTramos.Clone();
                        dtFin.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]);

                        for (int i = 0; i < oRem.dtTramos.Rows.Count - 2; i++)
                        {
                            dtFin.ImportRow(oRem.dtTramos.Rows[i]);
                        }

                        dtFin.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]);

                        //float dTiempo1 = ConvierteTiempoaDecimal(dtFin.Rows[0]["TiempoCobrar"].S());
                        //float dTiempo2 = ConvierteTiempoaDecimal(dtFin.Rows[3]["TiempoCobrar"].S());

                        //if (dTiempo1 <= dTiempo2)
                        //{
                        //    dtFin.Rows[0]["SeCobra"] = "1";
                        //    dtFin.Rows[3]["SeCobra"] = "0";
                        //}
                        //else if (dTiempo1 > dTiempo2)
                        //{
                        //    dtFin.Rows[0]["SeCobra"] = "0";
                        //    dtFin.Rows[3]["SeCobra"] = "1";
                        //}

                        oRem.dtTramos = null;
                        oRem.dtTramos = dtFin.Copy();
                        dtFin.Dispose();
                        #endregion

                        break;

                    case 4: // Ferry's VS Pernoctas
                        #region Ferry's VS Pernoctas
                        // FERRY INICIAL
                        sInicio = oRem.dtTramos.Rows[1]["OrigenICAO"].S(); ;
                        sFin = oRem.dtTramos.Rows[1]["DestinoICAO"].S();

                        drI = oRem.dtTramos.NewRow();
                        sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, oRem.dtTramos.Rows[0]["Matricula"].S());
                        drI = oRem.dtTramos.NewRow();
                        drI["IdBitacora"] = iCont;
                        drI["Matricula"] = oRem.dtTramos.Rows[0]["Matricula"].S();
                        drI["IdTipoPierna"] = oRem.dtTramos.Rows[0]["IdTipoPierna"].S();
                        drI["Origen"] = new DBRemision().DBGetIATAporICAO(sInicio);
                        drI["Destino"] = new DBRemision().DBGetIATAporICAO(sFin);
                        drI["OrigenICAO"] = sInicio;
                        drI["DestinoICAO"] = sFin;


                        drI["FechaSalida"] = oRem.dtTramos.Rows[0]["FechaLlegada"];

                        dtFechaLlegada = oRem.dtTramos.Rows[0]["FechaLlegada"].Dt();

                        string[] sTiempos2 = sTiempo.Split(':');
                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos2[0]));
                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos2[1]));
                        drI["FechaLlegada"] = dtFechaLlegada;

                        ts = dtFechaLlegada - oRem.dtTramos.Rows[0]["FechaLlegada"].Dt();


                        drI["CantPax"] = 0;
                        drI["TotalTiempoCalzo"] = "00:00";
                        drI["TotalTiempoVuelo"] = "00:00";

                        drI["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S() : sTiempo;

                        drI["RealVirtual"] = "Virtual";
                        drI["VueloClienteId"] = oRem.dtTramos.Rows[0]["VueloClienteId"].S();
                        drI["VueloContratoId"] = oRem.dtTramos.Rows[0]["VueloContratoId"].S();
                        drI["TiempoEspera"] = "00:00";
                        drI["SeCobra"] = 1;

                        oRem.dtTramos.Rows.Add(drI);


                        // Ferry Tercera posición
                        drF = oRem.dtTramos.NewRow();
                        sInicio = oRem.dtTramos.Rows[0]["OrigenICAO"].S(); ;
                        sFin = oRem.dtTramos.Rows[0]["DestinoICAO"].S();

                        sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["Matricula"].S());
                        drF = oRem.dtTramos.NewRow();
                        drF["IdBitacora"] = iCont;
                        drF["Matricula"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["Matricula"].S();
                        drF["IdTipoPierna"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["IdTipoPierna"].S();
                        drF["Origen"] = new DBRemision().DBGetIATAporICAO(sInicio);
                        drF["Destino"] = new DBRemision().DBGetIATAporICAO(sFin);
                        drF["OrigenICAO"] = sInicio;
                        drF["DestinoICAO"] = sFin;


                        drF["FechaLlegada"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaSalida"];

                        dtFechaLlegada = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaSalida"].Dt();
                        sTiempos = sTiempo.Split(':');
                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]) * -1);
                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]) * -1);

                        drF["FechaSalida"] = dtFechaLlegada;



                        drF["CantPax"] = 0;
                        drF["TotalTiempoCalzo"] = "00:00";
                        drF["TotalTiempoVuelo"] = "00:00";

                        ts = dtFechaLlegada - oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaSalida"].Dt();

                        drF["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S() : sTiempo;
                        drF["RealVirtual"] = "Virtual";
                        drF["VueloClienteId"] = oRem.dtTramos.Rows[0]["VueloClienteId"].S();
                        drF["VueloContratoId"] = oRem.dtTramos.Rows[0]["VueloContratoId"].S();
                        drF["TiempoEspera"] = "00:00";
                        drF["SeCobra"] = 1;

                        oRem.dtTramos.Rows.Add(drF);

                        dtFin = oRem.dtTramos.Clone();
                        if (oRem.dtTramos.Rows.Count == 4)
                        {
                            dtFin.ImportRow(oRem.dtTramos.Rows[0]);
                            dtFin.ImportRow(oRem.dtTramos.Rows[2]);
                            dtFin.ImportRow(oRem.dtTramos.Rows[3]);
                            dtFin.ImportRow(oRem.dtTramos.Rows[1]);
                        }
                        oRem.dtTramos = null;
                        oRem.dtTramos = dtFin.Copy();
                        dtFin.Dispose();
                        #endregion

                        break;
                }

                if (oRem.dtTramos.Rows.Count > 0)
                {
                    if (oRem.eSeCobraFerry == Enumeraciones.SeCobraFerrys.Reposicionamiento)
                    {
                        if (oRem.dtTramos.Rows[0]["CantPax"].S() == "0" && oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["CantPax"].S() == "0")
                        {
                            float fInicio = Utils.ConvierteTiempoaDecimal(oRem.dtTramos.Rows[0]["TiempoCobrar"].S());
                            float fFin = Utils.ConvierteTiempoaDecimal(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["TiempoCobrar"].S());

                            if (fInicio < fFin)
                                oRem.dtTramos.Rows[0]["SeCobra"] = 1;
                            else
                                oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["SeCobra"] = 1;
                        }
                    }
                }

                return oRem.dtTramos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static DataTable CalculaServiciosRemision(DataTable dtCostos, long lIdRemision, int iIdContrato, ref decimal iFactorVuelo)
        {
            try
            {
                DataTable dtSC = new DataTable();
                dtSC.Columns.Add("IdConcepto");
                dtSC.Columns.Add("Concepto");
                dtSC.Columns.Add("CostoDirecto", typeof(decimal));
                dtSC.Columns.Add("TarifaVuelo", typeof(decimal));
                dtSC.Columns.Add("CombustibleAumento", typeof(decimal));
                dtSC.Columns.Add("TarifaDlls", typeof(decimal));
                dtSC.Columns.Add("Cantidad");
                dtSC.Columns.Add("ImporteDlls", typeof(decimal));
                dtSC.Columns.Add("HrDescontar");

                DataTable dtServ = new DBRemision().DBGetConsultaConceptosServiciosVuelo;

                bool IsVueloInt = false;
                DataTable dtTr = new DBRemision().DBGetConsultaTramosRemisionExistentes(lIdRemision);

                string sAirPortO = dtTr.Rows[0]["OrigenICAO"].S();
                DataTable dtComb = new DBRemision().DBGetConsultaTarifasVuelo(iIdContrato, lIdRemision, sAirPortO);

                foreach (DataRow drT in dtTr.Rows)
                {
                    DataTable dtO = new DBRemision().DBGetObtieneTipoDestinoAeropuertoPorICAO(drT.S("OrigenICAO"));

                    switch (dtO.Rows[0]["TipoDestino"].S().ToUpper())
                    {
                        case "F":
                        case "E":
                            IsVueloInt = true;
                            break;
                    }

                    DataTable dtD = new DBRemision().DBGetObtieneTipoDestinoAeropuertoPorICAO(drT.S("DestinoICAO"));

                    switch (dtD.Rows[0]["TipoDestino"].S().ToUpper())
                    {
                        case "F":
                        case "E":
                            IsVueloInt = true;
                            break;
                    }

                    if (IsVueloInt)
                        break;
                }

                decimal dFactorIVA = IsVueloInt ? Utils.ObtieneParametroPorClave("10").S().D() : Utils.ObtieneParametroPorClave("9").S().D();
                iFactorVuelo = dFactorIVA;

                for (int i = 0; i < dtCostos.Rows.Count; i++)
                {
                    DataRow row = dtCostos.Rows[i];
                    DataRow dr = dtSC.NewRow();

                    dr["IdConcepto"] = dtServ.Rows[i]["IdConcepto"].S();
                    dr["Concepto"] = dtServ.Rows[i]["Descripcion"].S();
                    dr["TarifaDlls"] = row["TarifaDlls"].S();
                    dr["Cantidad"] = row["Cantidad"].S();
                    dr["HrDescontar"] = row["HrDescontar"].S();

                    dr["ImporteDlls"] = row["Importe"].S().D();
                    dr["TarifaVuelo"] = row["CostoComb"].S().D();
                    dr["CombustibleAumento"] = row["CombustibleAumento"].S().D();
                    dr["CostoDirecto"] = row["CostoDirecto"].S().D();


                    //switch (i)
                    //{
                    //    case 0:
                    //        dr["CostoDirecto"] = dtComb.Rows[0]["CostoDirectoNalV"].S().D();
                    //        dr["TarifaVuelo"] = dtComb.Rows[0]["CombustibleNal"].S().D();
                    //        dr["TarifaDlls"] = dtComb.Rows[0]["TarifaVueloNal"].S().D();

                    //        dr["ImporteDlls"] = ConvierteTiempoaDecimal(dr.S("Cantidad")).S().D() * dtComb.Rows[0]["TarifaVueloNal"].S().D();
                    //        break;
                    //    case 1:
                    //        dr["CostoDirecto"] = dtComb.Rows[0]["CostoDirectoIntV"].S().D();
                    //        dr["TarifaVuelo"] = dtComb.Rows[0]["CombustibleInt"].S().D();
                    //        dr["TarifaDlls"] = dtComb.Rows[0]["TarifaVueloInt"].S().D();

                    //        dr["ImporteDlls"] = ConvierteTiempoaDecimal(dr.S("Cantidad")).S().D() * dtComb.Rows[0]["TarifaVueloInt"].S().D();
                    //        break;
                    //    case 2:
                    //        dr["TarifaDlls"] = row["TarifaDlls"].S().D();
                    //        dr["ImporteDlls"] = ConvierteTiempoaDecimal(dr.S("Cantidad")).S().D() * row["TarifaDlls"].S().D();

                    //        break;
                    //    case 3:
                    //        dr["TarifaDlls"] = row["TarifaDlls"].S().D();
                    //        dr["ImporteDlls"] = ConvierteTiempoaDecimal(dr.S("Cantidad")).S().D() * row["TarifaDlls"].S().D();

                    //        break;
                    //    case 4:
                    //        dr["TarifaDlls"] = row["TarifaDlls"].S().D();
                    //        dr["ImporteDlls"] = dr.S("Cantidad").S().D() * row["TarifaDlls"].S().D();

                    //        break;
                    //    case 5:
                    //        dr["TarifaDlls"] = row["TarifaDlls"].S().D();
                    //        dr["ImporteDlls"] = dr.S("Cantidad").S().D() * row["TarifaDlls"].S().D();
                    //        break;
                    //}

                    dtSC.Rows.Add(dr);
                }

                decimal SumaDlls = 0;
                decimal SumaIVA = 0;
                decimal Total = 0;

                foreach (DataRow rowS in dtSC.Rows)
                {
                    SumaDlls += rowS["ImporteDlls"].S().D();
                }

                SumaIVA = SumaDlls * dFactorIVA;
                Total = SumaDlls * (1 + dFactorIVA);

                float dTiempoT = 0;

                DataRow drSub = dtSC.NewRow();
                drSub["IdConcepto"] = "999";
                drSub["Cantidad"] = "SubTotal";
                drSub["ImporteDlls"] = SumaDlls;
                drSub["HrDescontar"] = ObtieneTotalTiempo(dtSC, "HrDescontar", ref dTiempoT);
                dtSC.Rows.Add(drSub);

                DataRow drDesc = dtSC.NewRow();
                drDesc["IdConcepto"] = "996";
                drDesc["Cantidad"] = "Descuento 0.00%";
                drDesc["ImporteDlls"] = 0;
                dtSC.Rows.Add(drDesc);

                DataRow drIva = dtSC.NewRow();
                drIva["IdConcepto"] = "998";
                drIva["Cantidad"] = "IVA " + (dFactorIVA * 100).S() + "%";
                drIva["ImporteDlls"] = SumaIVA;
                dtSC.Rows.Add(drIva);

                DataRow drTotal = dtSC.NewRow();
                drTotal["IdConcepto"] = "997";
                drTotal["Cantidad"] = "Total";
                drTotal["ImporteDlls"] = Total;
                dtSC.Rows.Add(drTotal);

                return dtSC;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static float ConvierteTiempoaDecimal(string sTiempo)
        {
            try
            {
                int iHoras = 0;
                double iMinutos = 0;
                float fTiempo = 0;

                if (sTiempo.Length >= 5)
                {
                    string[] sPartes = sTiempo.Split(':');

                    iHoras = sPartes[0].S().I();
                    iMinutos = sPartes[1].S().I();
                    //iHoras = sTiempo.Substring(0, 2).S().I();
                    //iMinutos = sTiempo.Substring(3, 2).S().I();

                    float TotMin = float.Parse((iMinutos / 60).S());

                    return (iHoras + TotMin);

                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable CalculoServiciosCargo(int iIdContrato, long iIdRemision)
        {
            try
            {
                DataTable dtSCC = new DBRemision().DBGetServiciosCargoContrato(iIdContrato, iIdRemision);

                dtSCC.Columns["Importe"].ReadOnly = false;

                bool bPorTramo = false;
                bool bPorPax = false;

                DataTable dtCobros = new DBRemision().DBGetImportesTUA(iIdRemision);
                DataTable dtA = new DBRemision().DBGetObtieneAterrizajesPorDocumento(1, iIdRemision.S().I(), GetTipoCambioDia.S().D());

                foreach (DataRow row in dtSCC.Rows)
                {
                    decimal dImp = 0;
                    decimal dSum = 0;

                    bPorTramo = row["PorPierna"].S().B();
                    bPorPax = row["PorPasajero"].S().B();

                    switch (row.S("IdServicioConCargo"))
                    {
                        case "1": // DSM
                            #region DSM
                            /*
                            El Aeropuerto Origen es TipoDestino Nacional o Fronterizo y TipoAeropuerto
                            Internacional y el Aeropuerto Destino es TipoDestino Extranjero del país EUA 
                            */
                            //DataTable dtS = new DBRemision().DBGetImportesTUA(iIdRemision);

                            foreach (DataRow drS in dtCobros.Rows)
                            {
                                if (((drS.S("TipoDestinoO") == "N" || drS.S("TipoDestinoO") == "F") && drS.S("TipoAeropuertoO") == "2") && (drS.S("TipoDestinoD") == "E"))
                                {
                                    dImp = drS.S("ImporteDSM").D();
                                    if (bPorPax)
                                    {
                                        dSum += (dImp * drS.S("Pax").I());
                                    }
                                    else if (bPorTramo)
                                    {
                                        dSum += dImp;
                                    }
                                }
                            }

                            row["Importe"] = dSum;

                            #endregion
                            break;
                        case "2": // APHIS
                            #region APHIS
                            /*
                             * Son Dolares y se cobran a la entrada a Estados Unidos desde cualquier origen, el cobro es por pasajero.
                            */

                            foreach (DataRow drA in dtCobros.Rows)
                            {
                                if (drA.S("ClavePaisO") != "US" && drA.S("ClavePaisD") == "US")
                                {
                                    dImp = drA.S("ImporteAPHIS").D();

                                    if (bPorPax)
                                    {
                                        dSum += (dImp * drA.S("Pax").I());
                                    }
                                }
                            }

                            row["Importe"] = dSum;

                            #endregion
                            break;
                        case "3":
                            #region COMISARIATO
                            string sImporte = new DBRemision().DBGetConsultaComisariatoRemision(iIdRemision);
                            row["Importe"] = sImporte.S().D();
                            #endregion
                            break;
                        case "4": // TUA NACIONAL
                            #region TUA NACIONAL
                            /*
                                1. El Aeropuerto Origen es Tipo de Destino Nacional o Fronterizo y Tipo de
                                    Aeropuerto Internacional y el Aeropuerto Destino es Tipo de Destino
                                    Nacional
                                2. El Aeropuerto Origen es Tipo de Destino Nacional o Fronterizo y Tipo de
                                    Aeropuerto Internacional y el Aeropuerto Destino es Tipo de Destino
                                    Fronterizo.
                            */

                            foreach (DataRow drN in dtCobros.Rows)
                            {
                                if (((drN.S("TipoDestinoO") == "N" || drN.S("TipoDestinoO") == "F") && drN.S("TipoAeropuertoO") == "2") && drN.S("TipoDestinoD") == "N")
                                {
                                    dImp = drN.S("NacionalO").D();

                                    if (bPorPax)
                                    {
                                        dSum += (dImp * drN.S("Pax").I());
                                    }
                                    else if (bPorTramo)
                                    {
                                        dSum += dImp;
                                    }
                                }
                                else if (((drN.S("TipoDestinoO") == "N" || drN.S("TipoDestinoO") == "F") && drN.S("TipoAeropuertoO") == "2") && drN.S("TipoDestinoD") == "F")
                                {
                                    dImp = drN.S("NacionalO").D();

                                    if (bPorPax)
                                    {
                                        dSum += (dImp * drN.S("Pax").I());
                                    }
                                    else if (bPorTramo)
                                    {
                                        dSum += dImp;
                                    }
                                }
                            }
                            row["Importe"] = dSum;

                            #endregion
                            break;
                        case "5": // TUA INTERNACIONAL
                            #region TUA INTERNACIONAL
                            /*
                                El Aeropuerto Origen es Tipo de Destino Nacional o Fronterizo y Tipo de
                                Aeropuerto Internacional y el Aeropuerto Destino es Tipo de Destino
                                Extranjero.  
                            */

                            foreach (DataRow drI in dtCobros.Rows)
                            {
                                if (((drI.S("TipoDestinoO") == "N" || drI.S("TipoDestinoO") == "F") && drI.S("TipoAeropuertoO") == "2") && drI.S("TipoDestinoD") == "E")
                                {
                                    dImp = drI.S("InternacionalO").D();

                                    if (bPorPax)
                                    {
                                        dSum += (dImp * drI["Pax"].S().I());
                                    }
                                    else if (bPorTramo)
                                    {
                                        dSum += dImp;
                                    }
                                }
                            }

                            row["Importe"] = dSum;

                            #endregion
                            break;
                        case "7": // ATERRIZAJE NACIONAL
                            #region ATERRIZAJE NACIONAL
                            row["Importe"] = dtA.Rows[0]["TotalCobrarMXN"].S().D();
                            #endregion
                            break;
                        case "19": // ATERRIZAJE INTERNACIONAL
                            #region ATERRIZAJE INTERNACIONAL
                            row["Importe"] = dtA.Rows[0]["TotalCobrarDlls"].S().D();
                            #endregion
                            break;
                        case "10": // SENEAM
                            #region SENEAM
                            /*
                             * El importe de SENEAM se calcula de la tarifa cobrada por grupo de modelo
                             * multiplicada por el numero de tramos.
                            */
                            row["Importe"] = dtCobros.Rows[0]["TarifaSENEAM"].S().D();

                            #endregion
                            break;
                        case "11": // MIGRACION
                            #region MIGRACION
                            /*
                                Migración. Es el cobro por servicios migratorios que se cobra en aeropuertos con
                                TipoDestino Nacional cuando el origen de la pierna o el destino es de TipoDestino Extranjero.
                                Para llegada o para salida se cobra $3,239 MN
                            */

                            foreach (DataRow drI in dtCobros.Rows)
                            {
                                if (((drI.S("TipoDestinoO") == "N" || drI.S("TipoDestinoO") == "F") && drI.S("TipoDestinoD") == "E") ||
                                        ((drI.S("TipoDestinoD") == "N" || drI.S("TipoDestinoD") == "F") && drI.S("TipoDestinoO") == "E"))
                                {
                                    dImp = drI.S("ImporteMigracion").D();
                                    if (bPorPax)
                                    {
                                        dSum += (dImp * drI["Pax"].S().I());
                                    }
                                    else if (bPorTramo)
                                    {
                                        dSum += dImp;
                                    }
                                }
                            }

                            row["Importe"] = dSum;

                            break;
                            #endregion
                        default:   // OTROS
                            row["Importe"] = 0.0000;
                            break;
                    }
                }

                return dtSCC;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetParametrosClave(string sClave)
        {
            try
            {
                return new DBParametros().DBSearchObj("@Clave", sClave,
                                                        "@Descripcion", string.Empty,
                                                        "@Valor", string.Empty,
                                                        "@estatus", -1).Rows[0]["Valor"].S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal GetTipoCambioDia
        {
            get
            {
                return new DBUtils().DBGettipoCambio;
                //return (20.69).S().D();
            }
        }
        /// <summary>
        /// Obtiene la suma de dos tiempos
        /// </summary>
        /// <param name="sTiempo1">Representa el tiempo 1 en el siguiente formato: HH:mm</param>
        /// <param name="sTiempo2">Representa el tiempo 2 en el siguiente formato: HH:mm</param>
        /// <returns></returns>
        public static string GetSumaTiempos(string sTiempo1, string sTiempo2)
        {
            try
            {
                bool bNegativo1 = false;
                bool bNegativo2 = false;

                bNegativo1 = sTiempo1.Contains('-');
                bNegativo2 = sTiempo2.Contains('-');

                sTiempo1 = sTiempo1.Replace("-", "");
                sTiempo2 = sTiempo2.Replace("-", "");

                string [] sTiempos1 = sTiempo1.Split(':');
                string [] sTiempos2 = sTiempo2.Split(':');

                int iMinutos1 = (sTiempos1[0].S().I() * 60) + sTiempos1[1].S().I();
                int iMinutos2 = (sTiempos2[0].S().I() * 60) + sTiempos2[1].S().I();

                if (bNegativo1)
                    iMinutos1 = (iMinutos1 * -1);

                if (bNegativo2)
                    iMinutos2 = (iMinutos2 * -1);


                string sCantidad = string.Empty;
                decimal dTiempoTotal = (iMinutos1 + iMinutos2).S().D() / 60;

                sCantidad = ConvierteDecimalATiempo(dTiempoTotal);

                return sCantidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable ReCalculaServiciosRemision(DataTable dtCostos, long lIdRemision, int iIdContrato)
        {
            try
            {
                DatosRemision oRem = new DBRemision().DBGetObtieneDatosRemision(lIdRemision, iIdContrato);
                BorraTotalesTabla(dtCostos);

                for (int i = 0; i < dtCostos.Rows.Count; i++)
                {
                    DataRow row = dtCostos.Rows[i];
                    float fTiempoT = 0;

                    switch (row.S("IdConcepto"))
                    {
                        case "1":
                            row["ImporteDlls"] = ConvierteTiempoaDecimal(row.S("Cantidad")).S().D() * row["TarifaDlls"].S().D();
                            row["HrDescontar"] = row.S("Cantidad");

                            break;
                        case "2":
                            row["ImporteDlls"] = ConvierteTiempoaDecimal(row.S("Cantidad")).S().D() * row["TarifaDlls"].S().D();
                            row["HrDescontar"] = row.S("Cantidad");

                            break;
                        case "3":
                            row["ImporteDlls"] = ConvierteTiempoaDecimal(row.S("Cantidad")).S().D() * row["TarifaDlls"].S().D();

                            if (oRem.bSeDescuentaEsperaNal)
                            {
                                fTiempoT = ConvierteTiempoaDecimal(row.S("Cantidad"));
                                decimal dHr = (fTiempoT.S().D() * (oRem.dFactorEHrVueloNal / 100));
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }

                            break;
                        case "4":
                            row["ImporteDlls"] = ConvierteTiempoaDecimal(row.S("Cantidad")).S().D() * row["TarifaDlls"].S().D();

                            if (oRem.bSeDescuentaEsperaInt)
                            {
                                fTiempoT = ConvierteTiempoaDecimal(row.S("Cantidad"));
                                decimal dHr = (fTiempoT.S().D() * (oRem.dFactorEHrVueloInt / 100));
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }

                            break;
                        case "5":
                            row["ImporteDlls"] = row.S("Cantidad").S().D() * row["TarifaDlls"].S().D();

                            if (oRem.bSeDescuentanPerNal)
                            {
                                decimal dHr = (row.S("Cantidad").I() * oRem.dFactorConvHrVueloNal);
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }

                            break;
                        case "6":
                            row["ImporteDlls"] = row.S("Cantidad").S().D() * row["TarifaDlls"].S().D();

                            if (oRem.bSeDescuentanPerInt)
                            {
                                decimal dHr = (row.S("Cantidad").I() * oRem.dFactorConvHrVueloInt);
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }
                            break;
                    }
                }

                CalculaTotalesTable(dtCostos);
                #region COMENTADO
                /*
                decimal SumaDlls = 0;
                decimal SumaIVA = 0;
                decimal Total = 0;
                decimal dFactorIVA = dtSC.Rows[1]["Cantidad"].S() != "00:00" ? Utils.ObtieneParametroPorClave("10").S().D() : Utils.ObtieneParametroPorClave("9").S().D();

                foreach (DataRow rowS in dtSC.Rows)
                {
                    SumaDlls += rowS["ImporteDlls"].S().D();
                }

                SumaIVA = SumaDlls * dFactorIVA;
                Total = SumaDlls * (1 + dFactorIVA);

                float dTiempoT = 0;

                DataRow drSub = dtSC.NewRow();
                drSub["IdConcepto"] = "999";
                drSub["Cantidad"] = "SubTotal";
                drSub["ImporteDlls"] = SumaDlls;
                drSub["HrDescontar"] = ObtieneTotalTiempo(dtSC, "HrDescontar", ref dTiempoT);
                dtSC.Rows.Add(drSub);

                DataRow drIva = dtSC.NewRow();
                drIva["IdConcepto"] = "998";
                drIva["Cantidad"] = "IVA " + (dFactorIVA * 100).S() + "%";
                drIva["ImporteDlls"] = SumaIVA;
                dtSC.Rows.Add(drIva);

                DataRow drTotal = dtSC.NewRow();
                drTotal["IdConcepto"] = "997";
                drTotal["Cantidad"] = "Total";
                drTotal["ImporteDlls"] = Total;
                dtSC.Rows.Add(drTotal);
                */
                #endregion

                return dtCostos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void BorraTotalesTabla(DataTable dtCostos)
        {
            try
            {
                DataRow[] drSer = new DataRow[3];
                foreach (DataRow row in dtCostos.Rows)
                {
                    if (row["IdConcepto"].S() == "999")
                        drSer[0] = row;

                    else if (row["IdConcepto"].S() == "998")
                        drSer[1] = row;

                    else if (row["IdConcepto"].S() == "997")
                        drSer[2] = row;
                }

                for (int i = 0; i < drSer.Length; i++)
                {
                    dtCostos.Rows.Remove(drSer[i]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CalculaTotalesTable(DataTable dtSC)
        {
            try
            {
                decimal SumaDlls = 0;
                decimal SumaIVA = 0;
                decimal Total = 0;
                decimal dFactorIVA = dtSC.Rows[1]["Cantidad"].S() != "00:00" ? Utils.ObtieneParametroPorClave("10").S().D() : Utils.ObtieneParametroPorClave("9").S().D();

                foreach (DataRow rowS in dtSC.Rows)
                {
                    SumaDlls += rowS["ImporteDlls"].S().D();
                }

                SumaIVA = SumaDlls * dFactorIVA;
                Total = SumaDlls * (1 + dFactorIVA);

                float dTiempoT = 0;

                DataRow drSub = dtSC.NewRow();
                drSub["IdConcepto"] = "999";
                drSub["Cantidad"] = "SubTotal";
                drSub["ImporteDlls"] = SumaDlls;
                drSub["HrDescontar"] = ObtieneTotalTiempo(dtSC, "HrDescontar", ref dTiempoT);
                dtSC.Rows.Add(drSub);

                DataRow drIva = dtSC.NewRow();
                drIva["IdConcepto"] = "998";
                drIva["Cantidad"] = "IVA " + (dFactorIVA * 100).S() + "%";
                drIva["ImporteDlls"] = SumaIVA;
                dtSC.Rows.Add(drIva);

                DataRow drTotal = dtSC.NewRow();
                drTotal["IdConcepto"] = "997";
                drTotal["Cantidad"] = "Total";
                drTotal["ImporteDlls"] = Total;
                dtSC.Rows.Add(drTotal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataRow[] ObtenerPermisos(Enumeraciones.Pantallas eModulo)
        {
            UserIdentity oUsuario;
            HttpContext Context = HttpContext.Current;
            if (Context.Session["UserIdentity"] == null)
            {
                Context.Response.Redirect("~/Views/frmLogin.aspx");
            }
            else
            {
                oUsuario = (UserIdentity)Context.Session["UserIdentity"];
                return oUsuario.dTPermisos.Select("ModuloId = " + Convert.ToInt32(eModulo));
            }
            return null;
        }

        public static int DeteminaFerrysAgregar(DataTable dtBases, string sInicio, string sFin, ref string sBasePre, ref int iAeropuerto)
        {
            try
            {
                int iInicioFin = 0;
                foreach (DataRow dr in dtBases.Rows)
                {
                    if (dr.S("IdTipoBase") == "1")
                        sBasePre = dr.S("AeropuertoICAO");

                    if (sInicio == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "1")
                    {
                        iAeropuerto = dr.S("IdAeropuerto").I();
                        iInicioFin = 1;
                        break;
                    }
                    else if (sFin == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "1")
                    {
                        iAeropuerto = dr.S("IdAeropuerto").I();
                        iInicioFin = 2;
                        break;
                    }
                    else if (sInicio == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "2")
                    {
                        iAeropuerto = dr.S("IdAeropuerto").I();
                        iInicioFin = 1;
                    }
                    else if (sFin == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "2")
                    {
                        iAeropuerto = dr.S("IdAeropuerto").I();
                        iInicioFin = 2;
                    }
                }

                return iInicioFin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool DeterminaSaleBaseYRegresaBase(DataTable dtBases, string sInicio, string sFin)
        {
            try
            {
                bool bResult = false;

                if (sInicio == sFin)
                {
                    foreach (DataRow dr in dtBases.Rows)
                    {
                        if ((sInicio == dr.S("AeropuertoICAO") || sInicio == dr.S("AeropuertoIATA")))
                        {
                            bResult = true;
                            break;
                        }
                    }
                }
                else
                    bResult = false;

                return bResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable AplicaFactoresATiemposRemision(DataTable dtTramos, DatosRemision oRem, int iIdContrato)
        {
            try
            {
                SnapshotRemision oSnap = (SnapshotRemision)System.Web.HttpContext.Current.Session["SnapshotRem"];
                oSnap.oFactoresTramos.Clear();
                // ALMACENA TIEMPOS ORIGINALES ANTES DE APLICAR FACTORES
                dtTramos.Columns.Add("TiempoOriginal");
                foreach (DataRow row in dtTramos.Rows)
                {
                    row["TiempoOriginal"] = row.S("TiempoCobrar");

                    float dTiempoCobrar = ConvierteTiempoaDecimal(row.S("TiempoCobrar"));
                    row["TiempoCobrar"] = dTiempoCobrar.S();
                }

                // Aplica Factor de Tramos
                #region FACTOR DE TRAMOS
                if (oRem.eCobroCombustible == Enumeraciones.CobroCombustible.HorasDescuento)
                {
                    foreach (DataRow dr in dtTramos.Rows)
                    {
                        string sTiempoCobrar = ConvierteDecimalATiempo(dr.S("TiempoCobrar").D());
                        
                        double dFactor = 0;
                        double dTotal = 0;

                        if (dr["SeCobra"].S().I() == 1)
                        {
                            oRem.bAplicaFactorTramoNacional = true;
                            oRem.bAplicaFactorTramoInternacional = true;

                            DataTable dt = new DBRemision().DBGetObtieneTipoDestinoAeropuertoPorICAO(dr.S("OrigenICAO"));
                            FactoresTramoSnapshot oFactorTramos = new FactoresTramoSnapshot();
                            oFactorTramos.iNoTramo = oSnap.oFactoresTramos.Count + 1;
                            oFactorTramos.sMatricula = dr["Matricula"].S();
                            oFactorTramos.sOrigen = dr["OrigenICAO"].S();
                            oFactorTramos.sDestino = dr["DestinoICAO"].S();
                            oFactorTramos.sTiempoOriginal = sTiempoCobrar;

                            switch (dt.Rows[0]["TipoDestino"].S().ToUpper())
                            {
                                case "N":
                                case "F":
                                    dFactor = oRem.dFactorTramosNal.S().Db();
                                    dTotal = dr.S("TiempoCobrar").Db() * dFactor;
                                    oFactorTramos.dAplicaFactorTramoNacional = dFactor;
                                    break;
                                case "E":
                                    dFactor = oRem.dFactorTramosInt.S().Db();
                                    dTotal = dr.S("TiempoCobrar").Db() * dFactor;
                                    oFactorTramos.dAplicaFactorTramoInternacional = dFactor;
                                    break;
                            }

                            sTiempoCobrar = ConvierteDoubleATiempo(dTotal);
                            dr["TiempoCobrar"] = dTotal;

                            oFactorTramos.sTiempoFinal = sTiempoCobrar;
                            oSnap.oFactoresTramos.Add(oFactorTramos);
                        }
                    }
                }
                #endregion

                //¿Aplica Factor de Remision?
                #region FACTOR DE REMISION
                Remision oR = new DBRemision().DBGetRemisionId(oRem.lIdRemision);
                if (oR.dFactorEspecial != 0)
                {
                    oRem.bAplicoFactorEspecial = true;
                    oRem.dFactorEspecialF = oR.dFactorEspecial;

                    foreach (DataRow row in dtTramos.Rows)
                    {
                        if (row.S("SeCobra") == "1")
                        {
                            FactoresTramoSnapshot oFactorTramos;
                            oFactorTramos = oSnap.oFactoresTramos.Where(r => r.sOrigen == row["OrigenICAO"].S()
                                                                            && r.sDestino == row["DestinoICAO"].S()
                                                                            && r.sMatricula == row["Matricula"].S()).FirstOrDefault();
                            if (oFactorTramos == null)
                                oFactorTramos = new FactoresTramoSnapshot();

                            oFactorTramos.iNoTramo = oSnap.oFactoresTramos.Count + 1;
                            oFactorTramos.sMatricula = row["Matricula"].S();
                            oFactorTramos.sOrigen = row["OrigenICAO"].S();
                            oFactorTramos.sDestino = row["DestinoICAO"].S();
                            oFactorTramos.sTiempoOriginal = row.S("TiempoCobrar");

                            //Aplicar factor pierna por pierna al Tiempo a Cobrar
                            float dTiempoCobrar = float.Parse(row.S("TiempoCobrar"));
                            //float dTiempoCobrar = ConvierteTiempoaDecimal(sTiempoCobrar);
                            double dFactor = oR.dFactorEspecial.S().Db();

                            double dTotal = dTiempoCobrar * dFactor;
                            string sTiempoCobrar = ConvierteDoubleATiempo(dTotal);

                            row["TiempoCobrar"] = dTotal.S();

                            oFactorTramos.dFactorEspeciaRem = dFactor;
                            oFactorTramos.sTiempoFinal = sTiempoCobrar;
                            oSnap.oFactoresTramos.Add(oFactorTramos);
                        }
                    }
                }
                #endregion

                // ¿Aplica Factor de Intercambio?
                #region APLICA INTERCAMBIO

                DataSet dsI = new DBRemision().DBGetConsultaIntercambiosVueloRemision(oRem.lIdRemision);
                bool bAplicaInter = false;

                if (dsI.Tables[3].Rows.Count > 0)
                    bAplicaInter = dsI.Tables[3].Rows[0]["AplicaIntercambio"].S().B();

                if (bAplicaInter)
                {
                    if (oR.dFactorEspecial == 0 && dsI.Tables.Count == 4 && dsI.Tables[1].Rows.Count > 0)
                    {
                        oRem.bAplicoIntercambio = true;

                        foreach (DataRow row in dsI.Tables[1].Rows)
                        {
                            string sIdGrupoModelo = row.S("IdGrupoModelo");
                            DataRow[] drIs = dsI.Tables[2].Select("IdGrupoModelo = " + sIdGrupoModelo);
                            if (drIs.Length > 0)
                            {
                                foreach (DataRow dr in dtTramos.Rows)
                                {
                                    if (dr.S("SeCobra") == "1")
                                    {
                                        if (row.S("Matricula") == dr.S("Matricula"))
                                        {
                                            bool bNuevo = false;
                                            FactoresTramoSnapshot oFactorTramos;
                                            oFactorTramos = oSnap.oFactoresTramos.Where(r => r.sOrigen == dr["OrigenICAO"].S()
                                                                                            && r.sDestino == dr["DestinoICAO"].S()
                                                                                            && r.sMatricula == dr["Matricula"].S()).FirstOrDefault();
                                            if (oFactorTramos == null)
                                            {
                                                bNuevo = true;
                                                oFactorTramos = new FactoresTramoSnapshot();
                                                oFactorTramos.iNoTramo = oSnap.oFactoresTramos.Count + 1;
                                                oFactorTramos.sMatricula = dr["Matricula"].S();
                                                oFactorTramos.sOrigen = dr["OrigenICAO"].S();
                                                oFactorTramos.sDestino = dr["DestinoICAO"].S();
                                                oFactorTramos.sTiempoOriginal = dr.S("TiempoCobrar");
                                            }
                                        
                                            string dTiempoCobrar = dr.S("TiempoCobrar");
                                            //float dTiempoCobrar = ConvierteTiempoaDecimal(sTiempoCobrar);
                                            double dFactor = drIs[0]["Factor"].S().Db();

                                            if (dFactor != 0)
                                            {
                                                double dTotal = dTiempoCobrar.S().Db() * dFactor;
                                                
                                                dr["TiempoCobrar"] = dTotal.S();
                                                string sTiempoCobrar = ConvierteDoubleATiempo(dTotal);

                                                oRem.dFactorIntercambioF = decimal.Parse(dFactor.S());

                                                oFactorTramos.dAplicoIntercambio = dFactor;
                                                oFactorTramos.sTiempoFinal = sTiempoCobrar;

                                                if(bNuevo)
                                                    oSnap.oFactoresTramos.Add(oFactorTramos);

                                            }
                                            else
                                            {
                                                IntercambioRemision oInt = new IntercambioRemision();
                                                oInt.sMatriculaInter = row.S("Matricula");
                                                oInt.dTarifaNalInter = drIs[0]["TarifaNal"].S().D();
                                                oInt.dTarifaIntInter = drIs[0]["TarifaInt"].S().D();
                                                oInt.dGaolnesInter = drIs[0]["Galones"].S().D();
                                                oInt.dCostoDirNalInter = drIs[0]["CostoDirectoNal"].S().D();
                                                oInt.dCostoDirIntInter = drIs[0]["CostoDirectoInt"].S().D();

                                                oRem.oLstIntercambios.Add(oInt);
                                            }

                                            oRem.bAplicoIntercambio = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                oRem.oErr.bExisteError = true;
                                if (oRem.oErr.sMsjError == string.Empty)
                                    oRem.oErr.sMsjError = "El Intercambio con el grupo de modelo: " + row.S("GrupoModeloDesc") + " no existe, favor de verificar.";
                                else
                                    oRem.oErr.sMsjError += "\n" + "El Intercambio con el grupo de modelo: " + row.S("GrupoModeloDesc") + " no existe, favor de verificar.";
                                break;
                            }
                        }
                    }
                }
                #endregion

                // Aplica Gira
                #region APLICA GIRA
                Contrato_GirasFechasPico oGira = new DBContrato().DBGetGiras(iIdContrato);

                if (dtTramos.Rows.Count > 1)
                {
                    if (oGira.bAplicaGiraEspera || oGira.bAplicaGiraHora)
                    {
                        bool banGira = false;
                        foreach (DataRow row in dtTramos.Rows)
                        {
                            if (row.S("CantPax").I() > 0)
                            {
                                banGira = true;
                            }
                            else
                            {
                                banGira = false;
                                oRem.bAplicaGiraEspera = false;
                                oRem.bAplicaGiraHorario = false;
                                break;
                            }
                        }

                        if (banGira)
                        {
                            //string sBasePre = string.Empty;
                            //int iIdAeropuerto = 0;
                            string[] sTramos = oRem.sRuta.Split('-');

                            //int iInicioFin = DeteminaFerrysAgregar(oRem.dtBases, sTramos[0], sTramos[sTramos.Length - 1], ref sBasePre, ref iIdAeropuerto);

                            if (DeterminaSaleBaseYRegresaBase(oRem.dtBases, sTramos[0], sTramos[sTramos.Length - 1]))
                            {
                                //if (sTramos[0] == sTramos[sTramos.Length - 1])
                                //{
                                DateTime dtInicio = dtTramos.Rows[0]["FechaSalida"].S().Dt();
                                DateTime dtFin = dtTramos.Rows[dtTramos.Rows.Count - 1]["FechaLlegada"].S().Dt();

                                if (dtInicio.Date == dtFin.Date)
                                {
                                    // ¿Aplica Gira Espera?
                                    if (oGira.bAplicaGiraEspera)
                                    {
                                        // Tiempo total de espera no excede del doble o triple del tiempo de vuelo.
                                        float fTotalEspera = 0;
                                        string sTotalEspera = ObtieneTotalTiempo(dtTramos, "TiempoEspera", ref fTotalEspera);

                                        float fTotalVuelo = 0;
                                        string sTotalVuelo = ObtieneTotalTiempo(dtTramos, "TiempoCobrar", ref fTotalVuelo);

                                        fTotalVuelo = (fTotalVuelo * oGira.iNumeroVeces);

                                        if (fTotalEspera <= fTotalVuelo)
                                        {
                                            oRem.bAplicaGiraEspera = true;
                                        }
                                    }

                                    // ¿Aplica Gira de Horario?
                                    if (oGira.bAplicaGiraHora)
                                    {
                                        string[] sHorasInicio = oGira.sHoraInicio.Split(':');
                                        string[] sHorasFin = oGira.sHoraFin.Split(':');

                                        // Vuelo se debe realizar dentro del horario establecido.
                                        if (dtInicio.Hour >= sHorasInicio[0].S().I())
                                        {
                                            if (dtInicio.Hour == sHorasInicio[0].S().I())
                                            {
                                                if (dtInicio.Minute >= sHorasInicio[1].S().I())
                                                {
                                                    if (dtFin.Hour <= sHorasFin[0].S().I())
                                                    {
                                                        if (dtFin.Hour == sHorasFin[0].S().I())
                                                        {
                                                            if (dtFin.Minute <= sHorasFin[1].S().I())
                                                            {
                                                                oRem.bAplicaGiraHorario = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            oRem.bAplicaGiraHorario = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (dtInicio.Minute >= sHorasInicio[1].S().I())
                                                {
                                                    if (dtFin.Hour <= sHorasFin[0].S().I())
                                                    {
                                                        if (dtFin.Hour == sHorasFin[0].S().I())
                                                        {
                                                            if (dtFin.Minute <= sHorasFin[1].S().I())
                                                            {
                                                                oRem.bAplicaGiraHorario = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            oRem.bAplicaGiraHorario = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                //}
                            }
                        }

                        if (oRem.bAplicaGiraEspera)
                        {
                            foreach (DataRow row in dtTramos.Rows)
                            {
                                if (row.S("SeCobra") == "1")
                                {
                                    bool bNuevo = false;
                                    FactoresTramoSnapshot oFactorTramos;
                                    oFactorTramos = oSnap.oFactoresTramos.Where(r => r.sOrigen == row["OrigenICAO"].S()
                                                                                    && r.sDestino == row["DestinoICAO"].S()
                                                                                    && r.sMatricula == row["Matricula"].S()).FirstOrDefault();
                                    if (oFactorTramos == null)
                                    {
                                        bNuevo = true;
                                        oFactorTramos = new FactoresTramoSnapshot();
                                        oFactorTramos.iNoTramo = oSnap.oFactoresTramos.Count + 1;
                                        oFactorTramos.sMatricula = row["Matricula"].S();
                                        oFactorTramos.sOrigen = row["OrigenICAO"].S();
                                        oFactorTramos.sDestino = row["DestinoICAO"].S();
                                        oFactorTramos.sTiempoOriginal = row.S("TiempoCobrar");
                                    }

                                    //float dTiempoCobrar = ConvierteTiempoaDecimal(row.S("TiempoCobrar"));
                                    float dTiempoCobrar = float.Parse(row.S("TiempoCobrar"));
                                    double dTotal = dTiempoCobrar.S().Db() * (1 - oGira.dPorcentajeDescuento).S().Db();
                                    oRem.dFactorGiraEsperaF = (1 - oGira.dPorcentajeDescuento).S().D();

                                    row["TiempoCobrar"] = dTotal;
                                    string sTiempoCobrar = ConvierteDoubleATiempo(dTotal);
                                    
                                    oFactorTramos.dAplicaGiraEspera = oRem.dFactorGiraEsperaF.S().Db();
                                    oFactorTramos.sTiempoFinal = sTiempoCobrar;

                                    if(bNuevo)
                                        oSnap.oFactoresTramos.Add(oFactorTramos);
                                }
                            }
                        }

                        if (oRem.bAplicaGiraHorario)
                        {
                            foreach (DataRow row in dtTramos.Rows)
                            {
                                if (row.S("SeCobra") == "1")
                                {
                                    bool bNuevo = false;
                                    FactoresTramoSnapshot oFactorTramos;
                                    oFactorTramos = oSnap.oFactoresTramos.Where(r => r.sOrigen == row["OrigenICAO"].S()
                                                                                    && r.sDestino == row["DestinoICAO"].S()
                                                                                    && r.sMatricula == row["Matricula"].S()).FirstOrDefault();
                                    if (oFactorTramos == null)
                                    {
                                        bNuevo = true;
                                        oFactorTramos = new FactoresTramoSnapshot();
                                        oFactorTramos.iNoTramo = oSnap.oFactoresTramos.Count + 1;
                                        oFactorTramos.sMatricula = row["Matricula"].S();
                                        oFactorTramos.sOrigen = row["OrigenICAO"].S();
                                        oFactorTramos.sDestino = row["DestinoICAO"].S();
                                        oFactorTramos.sTiempoOriginal = row.S("TiempoCobrar");
                                    }

                                    //float dTiempoCobrar = ConvierteTiempoaDecimal(row.S("TiempoCobrar"));
                                    float dTiempoCobrar = float.Parse(row.S("TiempoCobrar"));
                                    double dTotal = dTiempoCobrar.S().Db() * (1 - oGira.dPorcentajeDescuento).S().Db();
                                    oRem.dFactorGiraHorarioF = (1 - oGira.dPorcentajeDescuento).S().D();

                                    row["TiempoCobrar"] = dTotal;
                                    string sTiempoCobrar = ConvierteDoubleATiempo(dTotal);
                                    
                                    oFactorTramos.dAplicaGiraHorario = oRem.dFactorGiraHorarioF.S().Db();
                                    oFactorTramos.sTiempoFinal = sTiempoCobrar;

                                    if(bNuevo)
                                        oSnap.oFactoresTramos.Add(oFactorTramos);
                                }
                            }
                        }
                    }
                }
                #endregion

                // Factor de Fecha Pico
                #region FACTOR DE FECHA PICO
                if (oGira.bAplicaFactorFechaPico)
                {
                    DataTable dtFechasPico = new DBFechaPico().DBSearchObj("@Fecha", DBNull.Value, "@estatus", 1);
                    foreach (DataRow row in dtTramos.Rows)
                    {
                        if (row.S("SeCobra") == "1")
                        {
                            //validar por pierna
                            DateTime dtPierna = row["FechaSalida"].S().Dt();
                            int iAnio = dtPierna.Year;
                            int iMes = dtPierna.Month;
                            int iDia = dtPierna.Day;

                            DataTable dtFecha = new DBRemision().DBGetObtieneFechasPicoPorAnio(iAnio, iMes);

                            foreach (DataRow dr in dtFecha.Rows)
                            {
                                DateTime dtFP = dr["Fecha"].S().Dt();
                                int iAnioFP = dtFP.Year;
                                int iMesFP = dtFP.Month;
                                int iDiaFP = dtFP.Day;

                                if (iAnio == iAnioFP && iMes == iMesFP && iDia == iDiaFP)
                                {
                                    bool bNuevo = false;
                                    FactoresTramoSnapshot oFactorTramos;
                                    oFactorTramos = oSnap.oFactoresTramos.Where(r => r.sOrigen == row["OrigenICAO"].S()
                                                                                    && r.sDestino == row["DestinoICAO"].S()
                                                                                    && r.sMatricula == row["Matricula"].S()).FirstOrDefault();
                                    if (oFactorTramos == null)
                                    {
                                        bNuevo = true;
                                        oFactorTramos = new FactoresTramoSnapshot();
                                        oFactorTramos.iNoTramo = oSnap.oFactoresTramos.Count + 1;
                                        oFactorTramos.sMatricula = row["Matricula"].S();
                                        oFactorTramos.sOrigen = row["OrigenICAO"].S();
                                        oFactorTramos.sDestino = row["DestinoICAO"].S();
                                        oFactorTramos.sTiempoOriginal = row.S("TiempoCobrar");
                                    }

                                    //float dTiempo = ConvierteTiempoaDecimal(row["TiempoCobrar"].S());
                                    float dTiempo =  float.Parse(row["TiempoCobrar"].S());

                                    double dTotalT = dTiempo.S().Db() * oGira.dFactorFechaPico.S().Db();
                                    string sTiempoCobrar = ConvierteDoubleATiempo(dTotalT.S().Db());
                                    row["TiempoCobrar"] = dTotalT;

                                    oRem.dFactorFechaPicoF = oGira.dFactorFechaPico;
                                    oRem.bAplicaFactorFechaPico = true;

                                    oFactorTramos.dAplicaFactorFechaPico = oGira.dFactorFechaPico.S().Db();
                                    oFactorTramos.sTiempoFinal = sTiempoCobrar;

                                    if(bNuevo)
                                        oSnap.oFactoresTramos.Add(oFactorTramos);
                                }
                            }
                        }
                    }
                }

                #endregion

                // Factor de Vuelo Simultaneo
                #region FACTOR DE VUELO SIMULTANEO

                // TABLA DE VUELOS SIMULTANEOS
                DataTable dtVloSim = new DataTable();
                dtVloSim.Columns.Add("Trip");

                DateTime dtPrimerTramoR = dtTramos.Rows[0]["FechaSalida"].S().Dt();

                DateTime dtPriTramRem = dtPrimerTramoR;
                DateTime dtUltTramRem = dtTramos.Rows[dtTramos.Rows.Count - 1]["FechaLlegada"].S().Dt();

                foreach (DataRow row in dtTramos.Rows)
                {
                    DateTime dtFechaVuelo = row["FechaSalida"].S().Dt();
                    DataTable dtVuelos = new DBRemision().DBGetConsultaVuelosDelMes(iIdContrato, dtFechaVuelo.Year, dtFechaVuelo.Month, oRem.lIdRemision);

                    foreach (DataRow dr in dtVuelos.Rows)
                    {
                        DateTime dtFP = dr["OrigenCalzo"].S().Dt();
                        TimeSpan ts = dtFechaVuelo - dtFP;

                        if (dtFechaVuelo.Year == dtFP.Year && dtFechaVuelo.Month == dtFP.Month && dtFechaVuelo.Day == dtFP.Day)
                        {
                            // Verifica si el otro vuelo es antes o despues para cobrar o no el vuelo presente.
                            DataTable dtPrimerT = new DBRemision().DBGetConsultaBitacoraUnVuelo(iIdContrato, dr["FolioReal"].S().I(), dr["AeronaveMatricula"].S(), dr.S("TripNum").I());
                            if (dtPrimerT.Rows.Count > 0)
                            {
                                DateTime dtPrimerTramo = dtPrimerT.Rows[0]["FechaMin"].S().Dt();
                                DateTime dtUltimoTramo = dtPrimerT.Rows[0]["FechaMax"].S().Dt();

                                if (dtPrimerTramoR >= dtPrimerTramo)
                                {

                                    if (dtPrimerTramo < dtPrimerTramoR && dtPrimerTramoR < dtUltimoTramo)
                                    {
                                        // AgregaTripAVuelosSimultaneo(dtVloSim, dr.S("TripNum").I());
                                        if (dr["Remisionado"].S() == "0")
                                        {
                                            if (oRem.oErr.sMsjError == string.Empty)
                                                oRem.oErr.sMsjError = "Existe un vuelo previo en menos de 24 hrs. por lo cual debe Remisionar la Bitácora: " + dr["FolioReal"].S() +
                                                    " Trip: " + dr.S("TripNum");
                                            else
                                                oRem.oErr.sMsjError += "\n" + "Existe un vuelo previo en menos de 24 hrs. por lo cual debe Remisionar la Bitácora: " + dr["FolioReal"].S() +
                                                    " Trip: " + dr.S("TripNum");

                                            oRem.bAplicaFactorVueloSimultaneo = true;
                                            oRem.oErr.bExisteError = true;

                                            break;
                                        }
                                        else
                                        {
                                            DataTable dtBit = new DBRemision().DBGetBitacorasPorRemision(dr["Remisionado"].S().L());
                                            if (dtBit.Rows.Count > 0)
                                            {
                                                DateTime dtUPR = dtBit.Rows[0]["DestinoCalzo"].S().Dt();

                                                if (dtUPR > dtFechaVuelo)
                                                {
                                                    AgregaTripAVuelosSimultaneo(dtVloSim, dr.S("TripNum").I());
                                                }
                                            }
                                        }
                                    }

                                    oRem.bAplicaFactorVueloSimultaneo = true;


                                }
                            }
                        }
                    }

                    if (oRem.bAplicaFactorVueloSimultaneo && oRem.oErr.bExisteError)
                        break;
                }


                if (!oRem.oErr.bExisteError)
                {
                    if (oRem.bAplicaFactorVueloSimultaneo)
                    {
                        bool bSiAplicaVloSim = true;

                        if (oRem.bPermiteVloSimultaneo)
                        {
                            if (dtVloSim.Rows.Count <= oRem.iCuantosVloSimultaneo)
                            {
                                oRem.bAplicaFactorVueloSimultaneo = false;
                                oRem.oErr.bExisteError = false;
                                oRem.oErr.sMsjError = string.Empty;

                                bSiAplicaVloSim = false;
                            }
                        }

                        if (bSiAplicaVloSim)
                        {
                            foreach (DataRow row in dtTramos.Rows)
                            {
                                if (row.S("SeCobra") == "1")
                                {
                                    bool bNuevo = false;
                                    FactoresTramoSnapshot oFactorTramos;
                                    oFactorTramos = oSnap.oFactoresTramos.Where(r => r.sOrigen == row["OrigenICAO"].S()
                                                                                    && r.sDestino == row["DestinoICAO"].S()
                                                                                    && r.sMatricula == row["Matricula"].S()).FirstOrDefault();
                                    if (oFactorTramos == null)
                                    {
                                        bNuevo = true;
                                        oFactorTramos = new FactoresTramoSnapshot();
                                        oFactorTramos.iNoTramo = oSnap.oFactoresTramos.Count + 1;
                                        oFactorTramos.sMatricula = row["Matricula"].S();
                                        oFactorTramos.sOrigen = row["OrigenICAO"].S();
                                        oFactorTramos.sDestino = row["DestinoICAO"].S();
                                        oFactorTramos.sTiempoOriginal = row.S("TiempoCobrar");
                                    }

                                    //string sTiempoCobrar = row.S("TiempoCobrar");

                                    double dTiempoCobrar = row.S("TiempoCobrar").Db();
                                    double dFactor = oRem.dFactorVloSimultaneo.S().Db();

                                    double dTotal = 0;
                                    if (dFactor != 1)
                                        dTotal = dTiempoCobrar.S().Db() * dFactor;
                                    else
                                        dTotal = dTiempoCobrar;

                                    string sTiempoCobrar = ConvierteDoubleATiempo(dTotal);
                                    row["TiempoCobrar"] = dTotal.S();

                                    oFactorTramos.dAplicaFactorVueloSimultaneo = dFactor;
                                    oFactorTramos.sTiempoFinal = sTiempoCobrar;

                                    if(bNuevo)
                                        oSnap.oFactoresTramos.Add(oFactorTramos);
                                }
                            }
                        }
                    }
                }

                #endregion

                foreach (DataRow row in dtTramos.Rows)
                {
                    row["TiempoCobrar"] = ConvierteDecimalATiempo(row["TiempoCobrar"].S().D());

                    if (row.S("OrigenICAO").IndexOf('@') != 0)
                    {
                        float fTMinimo = float.Parse(ObtieneParametroPorClave("121").S());
                        float fTiempoCobrar = ConvierteTiempoaDecimal(row.S("TiempoCobrar"));

                        if (fTMinimo > fTiempoCobrar)
                        {
                            row["TiempoCobrar"] = ConvierteDecimalATiempo(fTMinimo.S().D());
                        }
                    }
                }

                System.Web.HttpContext.Current.Session["SnapshotRem"] = oSnap;

                return dtTramos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void AgregaTripAVuelosSimultaneo(DataTable dtVlo, int iTrip)
        {
            try
            {
                DataRow[] Trips = dtVlo.Select("Trip = " + iTrip.S());
                if (Trips.Length == 0)
                {
                    DataRow row = dtVlo.NewRow();
                    row["Trip"] = iTrip;

                    dtVlo.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FactoresTramos ObtieneFactoresDeUnaRemision(DataTable dtTramos, DatosRemision oRem, int iIdContrato)
        {
            try
            {
                FactoresTramos oFactores = new FactoresTramos();


                if (dtTramos.Columns["TiempoOriginal"] == null)
                {
                    // ALMACENA TIEMPOS ORIGINALES ANTES DE APLICAR FACTORES
                    dtTramos.Columns.Add("TiempoOriginal");
                    foreach (DataRow row in dtTramos.Rows)
                    {
                        row["TiempoOriginal"] = row.S("TiempoCobrar");
                    }
                }


                //¿Aplica Factor de Remision?
                #region FACTOR DE REMISION
                Remision oR = new DBRemision().DBGetRemisionId(oRem.lIdRemision);
                if (oR.dFactorEspecial != 0)
                {
                    oFactores.bAplicoFactorEspecial = true;
                    oFactores.dFactorFactorEspecial = oR.dFactorEspecial;
                }
                #endregion

                // ¿Aplica Factor de Intercambio?
                #region APLICA INTERCAMBIO

                DataSet dsI = new DBRemision().DBGetConsultaIntercambiosVueloRemision(oRem.lIdRemision);
                bool bAplicaInter = false;

                if (dsI.Tables[3].Rows.Count > 0)
                    bAplicaInter = dsI.Tables[3].Rows[0]["AplicaIntercambio"].S().B();

                if (bAplicaInter)
                {
                    if (oR.dFactorEspecial == 0 && dsI.Tables.Count == 4 && dsI.Tables[1].Rows.Count > 0)
                    {
                        oFactores.bAplicoIntercambio = true;

                        foreach (DataRow row in dsI.Tables[1].Rows)
                        {
                            string sIdGrupoModelo = row.S("IdGrupoModelo");
                            DataRow[] drIs = dsI.Tables[2].Select("IdGrupoModelo = " + sIdGrupoModelo);
                            if (drIs.Length > 0)
                            {
                                foreach (DataRow dr in dtTramos.Rows)
                                {
                                    if (dr.S("SeCobra") == "1")
                                    {
                                        if (row.S("Matricula") == dr.S("Matricula"))
                                        {
                                            double dFactor = drIs[0]["Factor"].S().Db();

                                            if (dFactor != 0)
                                            {
                                                oFactores.dFactorIntercambio = decimal.Parse(dFactor.S());
                                                break;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                // Aplica Gira
                #region APLICA GIRA
                Contrato_GirasFechasPico oGira = new DBContrato().DBGetGiras(iIdContrato);

                if (dtTramos.Rows.Count > 1)
                {
                    if (oGira.bAplicaGiraEspera || oGira.bAplicaGiraHora)
                    {
                        bool banGira = false;
                        foreach (DataRow row in dtTramos.Rows)
                        {
                            if (row.S("CantPax").I() > 0)
                            {
                                banGira = true;
                            }
                            else
                            {
                                banGira = false;
                                oRem.bAplicaGiraEspera = false;
                                oRem.bAplicaGiraHorario = false;
                                break;
                            }
                        }

                        if (banGira)
                        {
                            string[] sTramos = oRem.sRuta.Split('-');

                            if (DeterminaSaleBaseYRegresaBase(oRem.dtBases, sTramos[0], sTramos[sTramos.Length - 1]))
                            {

                                DateTime dtInicio = dtTramos.Rows[0]["FechaSalida"].S().Dt();
                                DateTime dtFin = dtTramos.Rows[dtTramos.Rows.Count - 1]["FechaLlegada"].S().Dt();

                                if (dtInicio.Date == dtFin.Date)
                                {
                                    // ¿Aplica Gira Espera?
                                    if (oGira.bAplicaGiraEspera)
                                    {
                                        // Tiempo total de espera no excede del doble o triple del tiempo de vuelo.
                                        float fTotalEspera = 0;
                                        string sTotalEspera = ObtieneTotalTiempo(dtTramos, "TiempoEspera", ref fTotalEspera);

                                        float fTotalVuelo = 0;
                                        string sTotalVuelo = ObtieneTotalTiempo(dtTramos, "TiempoCobrar", ref fTotalVuelo);

                                        fTotalVuelo = (fTotalVuelo * oGira.iNumeroVeces);

                                        if (fTotalEspera <= fTotalVuelo)
                                        {
                                            oFactores.bAplicaGiraEspera = true;
                                        }
                                    }

                                    // ¿Aplica Gira de Horario?
                                    if (oGira.bAplicaGiraHora)
                                    {
                                        string[] sHorasInicio = oGira.sHoraInicio.Split(':');
                                        string[] sHorasFin = oGira.sHoraFin.Split(':');

                                        // Vuelo se debe realizar dentro del horario establecido.
                                        if (dtInicio.Hour >= sHorasInicio[0].S().I())
                                        {
                                            if (dtInicio.Hour == sHorasInicio[0].S().I())
                                            {
                                                if (dtInicio.Minute >= sHorasInicio[1].S().I())
                                                {
                                                    if (dtFin.Hour <= sHorasFin[0].S().I())
                                                    {
                                                        if (dtFin.Hour == sHorasFin[0].S().I())
                                                        {
                                                            if (dtFin.Minute <= sHorasFin[1].S().I())
                                                            {
                                                                oFactores.bAplicaGiraHorario = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            oFactores.bAplicaGiraHorario = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (dtInicio.Minute >= sHorasInicio[1].S().I())
                                                {
                                                    if (dtFin.Hour <= sHorasFin[0].S().I())
                                                    {
                                                        if (dtFin.Hour == sHorasFin[0].S().I())
                                                        {
                                                            if (dtFin.Minute <= sHorasFin[1].S().I())
                                                            {
                                                                oFactores.bAplicaGiraHorario = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            oFactores.bAplicaGiraHorario = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (oRem.bAplicaGiraEspera)
                        {
                            oFactores.dFactorGiraEspera = (1 - oGira.dPorcentajeDescuento).S().D();
                        }

                        if (oRem.bAplicaGiraHorario)
                        {
                            oFactores.dFactorGiraHorario = (1 - oGira.dPorcentajeDescuento).S().D();
                        }

                    }
                }
                #endregion

                // Factor de Fecha Pico
                #region FACTOR DE FECHA PICO
                if (oGira.bAplicaFactorFechaPico)
                {
                    DataTable dtFechasPico = new DBFechaPico().DBSearchObj("@Fecha", DBNull.Value, "@estatus", 1);
                    foreach (DataRow row in dtTramos.Rows)
                    {
                        if (row.S("SeCobra") == "1")
                        {
                            //validar por pierna
                            DateTime dtPierna = row["FechaSalida"].S().Dt();
                            int iAnio = dtPierna.Year;
                            int iMes = dtPierna.Month;
                            int iDia = dtPierna.Day;

                            DataTable dtFecha = new DBRemision().DBGetObtieneFechasPicoPorAnio(iAnio, iMes);

                            foreach (DataRow dr in dtFecha.Rows)
                            {
                                DateTime dtFP = dr["Fecha"].S().Dt();
                                int iAnioFP = dtFP.Year;
                                int iMesFP = dtFP.Month;
                                int iDiaFP = dtFP.Day;

                                if (iAnio == iAnioFP && iMes == iMesFP && iDia == iDiaFP)
                                {
                                    oFactores.bAplicaFactorFechaPico = true;
                                    oFactores.dFactorFechaPico = oGira.dFactorFechaPico;

                                }
                            }
                        }
                    }
                }

                #endregion

                // Factor de Vuelo Simultaneo
                #region FACTOR DE VUELO SIMULTANEO

                // TABLA DE VUELOS SIMULTANEOS
                DataTable dtVloSim = new DataTable();
                dtVloSim.Columns.Add("Trip");

                DateTime dtPrimerTramoR = dtTramos.Rows[0]["FechaSalida"].S().Dt();

                DateTime dtPriTramRem = dtPrimerTramoR;
                DateTime dtUltTramRem = dtTramos.Rows[dtTramos.Rows.Count - 1]["FechaLlegada"].S().Dt();

                foreach (DataRow row in dtTramos.Rows)
                {
                    DateTime dtFechaVuelo = row["FechaSalida"].S().Dt();
                    DataTable dtVuelos = new DBRemision().DBGetConsultaVuelosDelMes(iIdContrato, dtFechaVuelo.Year, dtFechaVuelo.Month, oRem.lIdRemision);

                    foreach (DataRow dr in dtVuelos.Rows)
                    {
                        DateTime dtFP = dr["OrigenCalzo"].S().Dt();
                        TimeSpan ts = dtFechaVuelo - dtFP;

                        if ((ts.Days == 0 && ts.Hours != 0) || (ts.Days == 0 && ts.Hours == 0 && ts.Minutes != 0))
                        {
                            // Verifica si el otro vuelo es antes o despues para cobrar o no el vuelo presente.
                            // ¿Es Antes?
                            if (dr["Remisionado"].S() == "0")
                            {
                                DataTable dtPrimerT = new DBRemision().DBGetConsultaBitacoraUnVuelo(iIdContrato, dr["FolioReal"].S().I(), dr["AeronaveMatricula"].S(), dr.S("TripNum").I());
                                if (dtPrimerT.Rows.Count > 0)
                                {
                                    DateTime dtPrimerTramo = dtPrimerT.Rows[0]["FechaMin"].S().Dt();

                                    if (dtPrimerTramoR >= dtPrimerTramo)
                                    {
                                        if (dtPrimerTramo > dtPriTramRem && dtPrimerTramo < dtUltTramRem)
                                            AgregaTripAVuelosSimultaneo(dtVloSim, dr.S("TripNum").I());

                                        oFactores.bAplicaFactorVueloSimultaneo = true;
                                        oRem.oErr.bExisteError = true;

                                        if (oRem.oErr.sMsjError == string.Empty)
                                            oRem.oErr.sMsjError = "Existe un vuelo previo en menos de 24 hrs. por lo cual debe Remisionar la Bitácora: " + dr["FolioReal"].S();
                                        else
                                            oRem.oErr.sMsjError += "\n" + "Existe un vuelo previo en menos de 24 hrs. por lo cual debe Remisionar la Bitácora: " + dr["FolioReal"].S();
                                        break;
                                    }
                                    else if (dtPrimerTramo > dtPriTramRem && dtPrimerTramo < dtUltTramRem)
                                    {
                                        AgregaTripAVuelosSimultaneo(dtVloSim, dr.S("TripNum").I());
                                    }
                                }
                            }
                            else
                            {
                                // Aplicar el factor
                                oFactores.bAplicaFactorVueloSimultaneo = true;

                                if (dtFP > dtPriTramRem && dtFP < dtUltTramRem)
                                    AgregaTripAVuelosSimultaneo(dtVloSim, dr.S("TripNum").I());
                            }
                        }
                    }

                    if (oFactores.bAplicaFactorVueloSimultaneo && oRem.oErr.bExisteError)
                        break;
                }


                if (!oRem.oErr.bExisteError)
                {
                    if (oRem.bAplicaFactorVueloSimultaneo)
                    {
                        bool bSiAplicaVloSim = true;

                        if (oRem.bPermiteVloSimultaneo)
                        {
                            if (dtVloSim.Rows.Count <= oRem.iCuantosVloSimultaneo)
                            {
                                oRem.oErr.bExisteError = false;
                                oRem.oErr.sMsjError = string.Empty;
                                bSiAplicaVloSim = false;

                                oFactores.bAplicaFactorVueloSimultaneo = false;
                                oFactores.dFactorVloSimultaneo = oRem.dFactorVloSimultaneo.S().D();
                            }
                        }

                        if (bSiAplicaVloSim)
                        {
                            oFactores.bAplicaFactorVueloSimultaneo = true;
                        }
                    }
                }
                else
                    oFactores.bAplicaFactorVueloSimultaneo = true;
                #endregion


                return oFactores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void AplicaIntercambioTarifasSiExiste(DatosRemision oRem, DataTable dtTramos)
        {
            try
            {
                DataSet dsI = new DBRemision().DBGetConsultaIntercambiosVueloRemision(oRem.lIdRemision);
                bool bAplicaInter = false;

                if (dsI.Tables[3].Rows.Count > 0)
                    bAplicaInter = dsI.Tables[3].Rows[0]["AplicaIntercambio"].S().B();

                if (bAplicaInter)
                {
                    if (oRem.dFactorEspecialF == 0 && dsI.Tables.Count == 4 && dsI.Tables[1].Rows.Count > 0)
                    {
                        oRem.bAplicoIntercambio = true;

                        foreach (DataRow row in dsI.Tables[1].Rows)
                        {
                            string sIdGrupoModelo = row.S("IdGrupoModelo");
                            DataRow[] drIs = dsI.Tables[2].Select("IdGrupoModelo = " + sIdGrupoModelo);
                            if (drIs.Length > 0)
                            {
                                foreach (DataRow dr in dtTramos.Rows)
                                {
                                    if (dr.S("SeCobra") == "1")
                                    {
                                        if (row.S("Matricula") == dr.S("Matricula"))
                                        {
                                            string sTiempoCobrar = dr.S("TiempoOriginal");

                                            if(sTiempoCobrar == "00:00:00" || sTiempoCobrar == "00:00")
                                            {
                                                string sTipoCobro = oRem.iCobroTiempo == 1 ? "TotalTiempoVuelo" : "TotalTiempoCalzo";
                                                sTiempoCobrar = dr.S(sTipoCobro);
                                            }

                                            float dTiempoCobrar = ConvierteTiempoaDecimal(sTiempoCobrar);
                                            float dFactor = float.Parse(drIs[0]["Factor"].S());

                                            //if (dFactor == 0)
                                            //    dFactor = 1;

                                            if (dFactor != 0 || (dFactor == 0 && drIs[0]["TarifaNal"].S().D() == 0 && drIs[0]["CostoDirectoNal"].S().D() == 0))
                                            {
                                                if ((dFactor == 0 && drIs[0]["TarifaNal"].S().D() == 0 && drIs[0]["CostoDirectoNal"].S().D() == 0))
                                                    dFactor = 1;

                                                decimal dTotal = (dTiempoCobrar * dFactor).S().D();
                                                sTiempoCobrar = ConvierteDecimalATiempo(dTotal);

                                                dr["TiempoCobrar"] = sTiempoCobrar;

                                                oRem.dFactorIntercambioF = decimal.Parse(dFactor.S());
                                            }
                                            else
                                            {
                                                IntercambioRemision oInt = new IntercambioRemision();
                                                oInt.sMatriculaInter = row.S("Matricula");
                                                oInt.dTarifaNalInter = drIs[0]["TarifaNal"].S().D();
                                                oInt.dTarifaIntInter = drIs[0]["TarifaInt"].S().D();
                                                oInt.dGaolnesInter = drIs[0]["Galones"].S().D();
                                                oInt.dCostoDirNalInter = drIs[0]["CostoDirectoNal"].S().D();
                                                oInt.dCostoDirIntInter = drIs[0]["CostoDirectoInt"].S().D();

                                                oRem.oLstIntercambios.Add(oInt);
                                            }

                                            oRem.bAplicoIntercambio = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                oRem.oErr.bExisteError = true;
                                if (oRem.oErr.sMsjError == string.Empty)
                                    oRem.oErr.sMsjError = "El Intercambio con el grupo de modelo: " + row.S("GrupoModeloDesc") + " no existe, favor de verificar.";
                                else
                                    oRem.oErr.sMsjError += "\n" + "El Intercambio con el grupo de modelo: " + row.S("GrupoModeloDesc") + " no existe, favor de verificar.";
                                break;
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["UserIdentity"] == null)
                {
                    UserIdentity oUsuario = new UserIdentity();
                    oUsuario.sUsuario = "system";
                    System.Web.HttpContext.Current.Session["UserIdentity"] = oUsuario;
                }

                return ((UserIdentity)System.Web.HttpContext.Current.Session["UserIdentity"]).sUsuario;
            }
        }

        public static int GetRolUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["UserIdentity"] == null)
                {
                    UserIdentity oUsuario = new UserIdentity();
                    oUsuario.iRol = -1;
                    System.Web.HttpContext.Current.Session["UserIdentity"] = oUsuario;
                }

                return ((UserIdentity)System.Web.HttpContext.Current.Session["UserIdentity"]).iRol;
            }
        }

        public static DataTable CalculaCostosRemisionEdoCuenta(DatosRemision oRem, ref DataTable dtPres2)
        {
            try
            {
                bool bBaseBase = false;
                string[] sTramos = oRem.sRuta.Split('-');
                if (sTramos[0] == sTramos[sTramos.Length - 1])
                {
                    bBaseBase = true;
                }

                DataTable dtTramos = new DataTable();
                oRem.dtTramos.Columns.Add("TiempoCobrar", typeof(string));

                string sTipoVuelo = oRem.iCobroTiempo == 1 ? "TotalTiempoVuelo" : "TotalTiempoCalzo";
                string sTipoIntercambioHeli = "TotalTiempoCalzo";
                switch (oRem.eSeCobraFerry)
                {
                    case Enumeraciones.SeCobraFerrys.Todos:
                        #region SE COBRAN 'TODOS' LOS FERRYS
                        if (bBaseBase)
                        {
                            if (sTramos.Length == 3)
                            {
                                // Validación de 24 horas entre tramos para ver si se hacen 2 presupuestos
                                bool bAplicaDosPresupuestos = false;

                                string[] sTiempos = oRem.dtTramos.Rows[0]["TiempoEspera"].S().Split(':');

                                bAplicaDosPresupuestos = sTiempos[0].S().I() >= 24 ? true : false;

                                if (oRem.bTiemposPactados)
                                {
                                    foreach (DataRow row in oRem.dtTramos.Rows)
                                    {
                                        string sTiempo = ObtieneTramoPactado(oRem, row.S("Origen"), row.S("Destino"), row.S("Matricula"));

                                        if (sTiempo == "00:00:00" || sTiempo == string.Empty || sTiempo == "00:00")
                                        {
                                            row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                        }
                                        else
                                            row["TiempoCobrar"] = sTiempo;
                                    }
                                }
                                else
                                {
                                    foreach (DataRow row in oRem.dtTramos.Rows)
                                    {
                                        row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                    }
                                }

                                if (bAplicaDosPresupuestos)
                                {
                                    //SI Aplica dos Presupuestos
                                    oRem.dtTramos = AgregaFerryTabla(4, oRem, 0, string.Empty);
                                    dtPres2 = oRem.dtTramos.Copy();

                                    foreach (DataRow drw in oRem.dtTramos.Rows)
                                    {
                                        drw["TiempoEspera"] = "00:00";
                                    }
                                }
                            }
                            else
                            {
                                // No aplican dos presupuestos
                                if (oRem.bTiemposPactados)
                                {
                                    foreach (DataRow row in oRem.dtTramos.Rows)
                                    {
                                        string sTiempo = ObtieneTramoPactado(oRem, row.S("Origen"), row.S("Destino"), row.S("Matricula"));

                                        if (sTiempo == "00:00:00" || sTiempo == string.Empty)
                                        {
                                            row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                        }
                                        else
                                            row["TiempoCobrar"] = sTiempo;
                                    }
                                }
                                else
                                {
                                    foreach (DataRow row in oRem.dtTramos.Rows)
                                    {
                                        row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                    }
                                }
                            }


                            if (dtPres2.Rows.Count == 4)
                            {
                                dtPres2.Rows.RemoveAt(2);
                                dtPres2.Rows.RemoveAt(1);
                            }

                            // Tramos finales
                            //oRem.dtTramos = AplicaFactoresATiemposRemision(oRem.dtTramos, oRem, oRem.iIdContrato);
                            dtTramos = CalculaCostosRemisionEdoCuenta(oRem.iCobroTiempo, oRem.dtTramos, oRem);
                        }
                        else
                        {
                            string sTiempo = string.Empty;

                            if (oRem.bTiemposPactados)
                            {
                                foreach (DataRow row in oRem.dtTramos.Rows)
                                {
                                    sTiempo = ObtieneTramoPactado(oRem, row.S("OrigenICAO"), row.S("DestinoICAO"), row.S("Matricula"));

                                    if (sTiempo == "00:00:00" || sTiempo == string.Empty)
                                    {
                                        row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                    }
                                    else
                                        row["TiempoCobrar"] = sTiempo;
                                }
                            }
                            else
                            {
                                foreach (DataRow row in oRem.dtTramos.Rows)
                                {
                                    row["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == string.Empty || sTiempo == "00:00") ? row[sTipoVuelo].S() : sTiempo;
                                }
                            }

                            // agregar ferrys virtuales
                            string sInicio = sTramos[0];
                            string sFin = sTramos[sTramos.Length - 1];
                            string sBasePre = string.Empty;
                            string sBaseFin = string.Empty;
                            int iIdAeropuerto = 0;
                            int iInicioFin = 0;  //  1.- Inicio   2.- Fin

                            foreach (DataRow dr in oRem.dtBases.Rows)
                            {
                                sBaseFin = dr.S("AeropuertoICAO");


                                if (dr.S("IdTipoBase") == "1")
                                    sBasePre = dr.S("AeropuertoICAO");


                                if (sInicio == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "1")
                                {
                                    iIdAeropuerto = dr.S("IdAeropuerto").I();
                                    iInicioFin = 1;
                                    break;
                                }
                                else if (sFin == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "1")
                                {
                                    iIdAeropuerto = dr.S("IdAeropuerto").I();
                                    iInicioFin = 2;
                                    break;
                                }
                                else if (sInicio == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "2")
                                {
                                    iIdAeropuerto = dr.S("IdAeropuerto").I();
                                    iInicioFin = 1;
                                }
                                else if (sFin == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "2")
                                {
                                    iIdAeropuerto = dr.S("IdAeropuerto").I();
                                    iInicioFin = 2;
                                }
                            }

                            DateTime dtFechaLlegada;
                            TimeSpan ts;
                            int iCont = 1;
                            if (iInicioFin != 0 && iIdAeropuerto != 0)
                            {
                                DataRow dr;

                                switch (iInicioFin)
                                {
                                    case 1: // Agregar Ferry Final
                                        #region FERRY FINAL
                                        sInicio = sTramos[sTramos.Length - 1];
                                        sFin = sBaseFin;

                                        sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["Matricula"].S());
                                        dr = oRem.dtTramos.NewRow();
                                        dr["IdBitacora"] = iCont;
                                        dr["Matricula"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["Matricula"].S();
                                        dr["IdTipoPierna"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["IdTipoPierna"].S();
                                        dr["Origen"] = new DBRemision().DBGetIATAporICAO(sInicio);
                                        dr["Destino"] = new DBRemision().DBGetIATAporICAO(sFin);
                                        dr["OrigenICAO"] = sInicio;
                                        dr["DestinoICAO"] = sFin;

                                        dr["FechaSalida"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"];

                                        dtFechaLlegada = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"].Dt();
                                        string[] sTiempos = sTiempo.Split(':');
                                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]));
                                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]));

                                        dr["FechaLlegada"] = dtFechaLlegada;

                                        dr["CantPax"] = 0;
                                        dr["TotalTiempoCalzo"] = "00:00";
                                        dr["TotalTiempoVuelo"] = "00:00";

                                        ts = dtFechaLlegada - oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"].Dt();

                                        dr["TiempoCobrar"] = (sTiempo == "00:00" || sTiempo == "00:00:00") ? ts.S() : sTiempo;
                                        dr["RealVirtual"] = "Virtual";
                                        dr["VueloClienteId"] = oRem.dtTramos.Rows[0]["VueloClienteId"].S();
                                        dr["VueloContratoId"] = oRem.dtTramos.Rows[0]["VueloContratoId"].S();
                                        dr["TiempoEspera"] = "00:00";
                                        dr["SeCobra"] = 1;

                                        oRem.dtTramos.Rows.Add(dr);
                                        #endregion
                                        break;

                                    case 2: // Agregar Ferry Inicial
                                        #region FERRY INICIAL

                                        sInicio = sBaseFin;
                                        sFin = sTramos[0];

                                        DataRow drI = oRem.dtTramos.NewRow();
                                        sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["Matricula"].S());
                                        drI = oRem.dtTramos.NewRow();
                                        drI["IdBitacora"] = iCont;
                                        drI["Matricula"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["Matricula"].S();
                                        drI["IdTipoPierna"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["IdTipoPierna"].S();
                                        drI["Origen"] = new DBRemision().DBGetIATAporICAO(sInicio);
                                        drI["Destino"] = new DBRemision().DBGetIATAporICAO(sFin);
                                        drI["OrigenICAO"] = sInicio;
                                        drI["DestinoICAO"] = sFin;

                                        drI["FechaLlegada"] = oRem.dtTramos.Rows[0]["FechaSalida"];

                                        dtFechaLlegada = oRem.dtTramos.Rows[0]["FechaSalida"].Dt();
                                        sTiempos = sTiempo.Split(':');
                                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]) * -1);
                                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]) * -1);

                                        drI["FechaSalida"] = dtFechaLlegada;

                                        ts = oRem.dtTramos.Rows[0]["FechaSalida"].Dt() - dtFechaLlegada;


                                        drI["CantPax"] = 0;
                                        drI["TotalTiempoCalzo"] = "00:00";
                                        drI["TotalTiempoVuelo"] = "00:00";

                                        drI["TiempoCobrar"] = (sTiempo == "00:00" || sTiempo == "00:00:00") ? ts.S() : sTiempo;

                                        drI["RealVirtual"] = "Virtual";
                                        drI["VueloClienteId"] = oRem.dtTramos.Rows[0]["VueloClienteId"].S();
                                        drI["VueloContratoId"] = oRem.dtTramos.Rows[0]["VueloContratoId"].S();
                                        drI["TiempoEspera"] = "00:00";
                                        drI["SeCobra"] = 1;

                                        oRem.dtTramos.Rows.Add(drI);

                                        DataTable dtInicio = oRem.dtTramos.Clone();
                                        dtInicio.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]);

                                        for (int i = 0; i < oRem.dtTramos.Rows.Count - 1; i++)
                                        {
                                            dtInicio.ImportRow(oRem.dtTramos.Rows[i]);
                                        }

                                        oRem.dtTramos = null;
                                        oRem.dtTramos = dtInicio.Copy();
                                        dtInicio.Dispose();
                                        #endregion
                                        break;
                                }
                            }
                            else
                            {
                                // Agregar Ferry de Inicio y Final con la base Predeterminada
                                #region FERRY INICIAL Y FINAL
                                sInicio = sBasePre;
                                sFin = oRem.dtTramos.Rows[0]["OrigenICAO"].S();

                                // FERRY INICIAL
                                DataRow drI = oRem.dtTramos.NewRow();
                                sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, oRem.dtTramos.Rows[0]["Matricula"].S());
                                drI = oRem.dtTramos.NewRow();
                                drI["IdBitacora"] = iCont;
                                drI["Matricula"] = oRem.dtTramos.Rows[0]["Matricula"].S();
                                drI["IdTipoPierna"] = oRem.dtTramos.Rows[0]["IdTipoPierna"].S();
                                drI["Origen"] = new DBRemision().DBGetIATAporICAO(sInicio);
                                drI["Destino"] = new DBRemision().DBGetIATAporICAO(sFin);
                                drI["OrigenICAO"] = sInicio;
                                drI["DestinoICAO"] = sFin;

                                drI["FechaLlegada"] = oRem.dtTramos.Rows[0]["FechaSalida"];

                                dtFechaLlegada = oRem.dtTramos.Rows[0]["FechaSalida"].Dt();
                                string[] sTiempos1 = sTiempo.Split(':');
                                dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos1[0]) * -1);
                                dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos1[1]) * -1);

                                drI["FechaSalida"] = dtFechaLlegada;

                                ts = oRem.dtTramos.Rows[0]["FechaSalida"].Dt() - dtFechaLlegada;

                                drI["CantPax"] = 0;
                                drI["TotalTiempoCalzo"] = "00:00";
                                drI["TotalTiempoVuelo"] = "00:00";

                                drI["TiempoCobrar"] = (sTiempo == "00:00" || sTiempo == "00:00:00") ? ts.S() : sTiempo;

                                drI["RealVirtual"] = "Virtual";
                                drI["VueloClienteId"] = oRem.dtTramos.Rows[0]["VueloClienteId"].S();
                                drI["VueloContratoId"] = oRem.dtTramos.Rows[0]["VueloContratoId"].S();
                                drI["TiempoEspera"] = "00:00";
                                drI["SeCobra"] = 1;

                                oRem.dtTramos.Rows.Add(drI);


                                // FERRY FINAL
                                DataRow drF = oRem.dtTramos.NewRow();
                                sInicio = sTramos[sTramos.Length - 1];
                                sFin = sBasePre;

                                sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["Matricula"].S());
                                drF = oRem.dtTramos.NewRow();
                                drF["IdBitacora"] = iCont;
                                drF["Matricula"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["Matricula"].S();
                                drF["IdTipoPierna"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["IdTipoPierna"].S();
                                drF["Origen"] = new DBRemision().DBGetIATAporICAO(sInicio);
                                drF["Destino"] = new DBRemision().DBGetIATAporICAO(sFin);
                                drF["OrigenICAO"] = sInicio;
                                drF["DestinoICAO"] = sFin;

                                drF["FechaSalida"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"];

                                dtFechaLlegada = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"].Dt();
                                sTiempos1 = sTiempo.Split(':');
                                dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos1[0]));
                                dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos1[1]));

                                drF["FechaLlegada"] = dtFechaLlegada;

                                drF["CantPax"] = 0;
                                drF["TotalTiempoCalzo"] = "00:00";
                                drF["TotalTiempoVuelo"] = "00:00";

                                ts = dtFechaLlegada - oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"].Dt();

                                drF["TiempoCobrar"] = (sTiempo == "00:00" || sTiempo == "00:00:00") ? ts.S() : sTiempo;
                                drF["RealVirtual"] = "Virtual";
                                drF["VueloClienteId"] = oRem.dtTramos.Rows[0]["VueloClienteId"].S();
                                drF["VueloContratoId"] = oRem.dtTramos.Rows[0]["VueloContratoId"].S();
                                drF["TiempoEspera"] = "00:00";
                                drF["SeCobra"] = 1;

                                oRem.dtTramos.Rows.Add(drF);



                                DataTable dtFin = oRem.dtTramos.Clone();
                                dtFin.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]);

                                for (int i = 0; i < oRem.dtTramos.Rows.Count - 2; i++)
                                {
                                    dtFin.ImportRow(oRem.dtTramos.Rows[i]);
                                }

                                dtFin.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]);


                                oRem.dtTramos = null;
                                oRem.dtTramos = dtFin.Copy();
                                dtFin.Dispose();
                                #endregion
                            }

                            // Tramos finales
                            //oRem.dtTramos = AplicaFactoresATiemposRemision(oRem.dtTramos, oRem, oRem.iIdContrato);
                            dtTramos = CalculaCostosRemisionEdoCuenta(oRem.iCobroTiempo, oRem.dtTramos, oRem);
                        }
                        #endregion
                        break;

                    case Enumeraciones.SeCobraFerrys.Reposicionamiento:
                        #region SE COBRAN FERRYS DE 'REPOSICIONAMIENTO'

                        if (oRem.bTiemposPactados)
                        {
                            foreach (DataRow row in oRem.dtTramos.Rows)
                            {
                                string sTiempo = ObtieneTramoPactado(oRem, row.S("Origen"), row.S("Destino"), row.S("Matricula"));

                                if (sTiempo == "00:00:00" || sTiempo == string.Empty || sTiempo == "00:00:00")
                                {
                                    row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                }
                                else
                                    row["TiempoCobrar"] = sTiempo;
                            }
                        }
                        else
                        {
                            foreach (DataRow row in oRem.dtTramos.Rows)
                            {
                                row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                            }
                        }

                        string sBasePreFR = string.Empty;
                        string sInicioFR = string.Empty;
                        string sFinFR = string.Empty;
                        int iAeropuertoFR = 0;
                        int iInicioFinFR = 0;

                        sInicioFR = sTramos[0];
                        sFinFR = sTramos[sTramos.Length - 1];

                        bool bAgregaFV = true;
                        foreach (DataRow dr in oRem.dtBases.Rows)
                        {
                            if (sInicioFR == dr.S("AeropuertoICAO") || sFinFR == dr.S("AeropuertoICAO"))
                            {
                                bAgregaFV = false;
                                break;
                            }
                        }

                        if (bAgregaFV)
                        {
                            foreach (DataRow dr in oRem.dtBases.Rows)
                            {
                                //sBaseFin = dr.S("AeropuertoICAO");

                                if (dr.S("IdTipoBase") == "1")
                                    sBasePreFR = dr.S("AeropuertoICAO");

                                if (sInicioFR == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "1")
                                {
                                    iAeropuertoFR = dr.S("IdAeropuerto").I();
                                    iInicioFinFR = 1;
                                    break;
                                }
                                else if (sFinFR == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "1")
                                {
                                    iAeropuertoFR = dr.S("IdAeropuerto").I();
                                    iInicioFinFR = 2;
                                    break;
                                }
                                else if (sInicioFR == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "2")
                                {
                                    iAeropuertoFR = dr.S("IdAeropuerto").I();
                                    iInicioFinFR = 1;
                                }
                                else if (sFinFR == dr.S("AeropuertoICAO") && dr.S("IdTipoBase") == "2")
                                {
                                    iAeropuertoFR = dr.S("IdAeropuerto").I();
                                    iInicioFinFR = 2;
                                }
                            }

                            if (iInicioFinFR != 0 && iAeropuertoFR != 0)
                            {
                                switch (iInicioFinFR)
                                {
                                    case 1:
                                        AgregaFerryTabla(iInicioFinFR, oRem, iAeropuertoFR, string.Empty);
                                        break;
                                    case 2:
                                        AgregaFerryTabla(iInicioFinFR, oRem, iAeropuertoFR, string.Empty);
                                        break;
                                }
                            }
                            else
                            {
                                // Agregar ferrys virtuales 
                                // Si no sale de base y no regresa a base   --->   Agrega ferry inicial y final y cobrar el mas corto
                                AgregaFerryTabla(3, oRem, iAeropuertoFR, sBasePreFR);
                            }
                        }

                        //oRem.dtTramos = AplicaFactoresATiemposRemision(oRem.dtTramos, oRem, oRem.iIdContrato);
                        dtTramos = CalculaCostosRemisionEdoCuenta(oRem.iCobroTiempo, oRem.dtTramos, oRem);

                        // Si no sale de base y no regresa a base -> Agrega ferry inicial y final y cobrar el mas corto

                        // Si, sale de base o regresa a base con pax, agregar el ferry pero no cobrar.

                        // Si, los ferrys de llegada o salida son reales -> cobrar el mas corto

                        #endregion
                        break;

                    case Enumeraciones.SeCobraFerrys.Ninguno:
                        #region NO SE COBRAN LOS FERRYS

                        if (oRem.bTiemposPactados)
                        {
                            foreach (DataRow row in oRem.dtTramos.Rows)
                            {
                                string sTiempo = ObtieneTramoPactado(oRem, row.S("Origen"), row.S("Destino"), row.S("Matricula"));

                                if (sTiempo == "00:00:00" || sTiempo == string.Empty || sTiempo == "00:00")
                                {
                                    row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                                }
                                else
                                    row["TiempoCobrar"] = sTiempo;
                            }
                        }
                        else
                        {
                            foreach (DataRow row in oRem.dtTramos.Rows)
                            {
                                row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos, row.S("Matricula"), row.S(sTipoIntercambioHeli), oRem);
                            }
                        }

                        //oRem.dtTramos = AplicaFactoresATiemposRemision(oRem.dtTramos, oRem, oRem.iIdContrato);
                        dtTramos = CalculaCostosRemisionEdoCuenta(oRem.iCobroTiempo, oRem.dtTramos, oRem);

                        #endregion
                        break;
                }

                return dtTramos.Copy();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable CalculaCostosRemisionEdoCuenta(int iCobroTiempo, DataTable dtTramos, DatosRemision oRem)
        {
            try
            {

                // SE PREPARAN LAS TABLAS PARA LOS TIEMPOS DE VUELOS
                DataTable dtTramosNal = new DataTable();
                DataTable dtTramosInt = new DataTable();


                dtTramosInt = dtTramos.Clone();
                dtTramosNal = dtTramos.Clone();

                foreach (DataRow dr in dtTramos.Rows)
                {
                    if (dr["SeCobra"].S().I() == 1)
                    {
                        DataTable dt = new DBRemision().DBGetObtieneTipoDestinoAeropuertoPorICAO(dr.S("OrigenICAO"));
                        switch (dt.Rows[0]["TipoDestino"].S().ToUpper())
                        {
                            case "N":
                            case "F":
                                dtTramosNal.ImportRow(dr);
                                break;
                            case "E":
                                dtTramosInt.ImportRow(dr);
                                break;
                        }
                    }
                }


                // SE PREPARAN LAS TABLAS PARA LOS TIEMPOS DE ESPERA Y PARA LAS PERNOCTAS
                DataTable dtTramosNalEP = new DataTable();
                DataTable dtTramosIntEP = new DataTable();

                dtTramosIntEP = dtTramos.Clone();
                dtTramosNalEP = dtTramos.Clone();

                foreach (DataRow dr in dtTramos.Rows)
                {
                    if (dr["SeCobra"].S().I() == 1)
                    {
                        DataTable dt = new DBRemision().DBGetObtieneTipoDestinoAeropuertoPorICAO(dr.S("DestinoICAO"));
                        switch (dt.Rows[0]["TipoDestino"].S().ToUpper())
                        {
                            case "N":
                            case "F":
                                dtTramosNalEP.ImportRow(dr);
                                break;
                            case "E":
                                dtTramosIntEP.ImportRow(dr);
                                break;
                        }
                    }
                }


                // SE PREPARA LA TABLA CON LOS CONCEPTOS EN COBROS
                DataTable dtConceptos = new DBRemision().DBGetConceptosRemision;
                DataTable dtFinal = new DataTable();
                dtFinal.Columns.Add("IdConcepto");
                dtFinal.Columns.Add("Concepto");
                dtFinal.Columns.Add("Cantidad");
                dtFinal.Columns.Add("CostoDirecto");
                dtFinal.Columns.Add("CostoComb");
                dtFinal.Columns.Add("CombustibleAumento");
                dtFinal.Columns.Add("TarifaDlls", typeof(decimal));
                dtFinal.Columns.Add("Importe", typeof(decimal));
                dtFinal.Columns.Add("HrDescontar");

                float fTiempoVueloN = 0;
                float fTiempoVueloI = 0;

                float fTiempoEsperaGlobal = 0;
                float fTiempoEsperaN = 0;
                float fTiempoEsperaI = 0;

                int iNoPernoctasGlobal = 0;
                float fNoPernoctasN = 0;
                float fNoPernoctasI = 0;

                string sAirPortO = dtTramos.Rows[0]["OrigenICAO"].S();
                DataTable dtComb = new DBRemision().DBGetConsultaTarifasVuelo(oRem.iIdContrato, oRem.lIdRemision, sAirPortO);
                // Agregar consulta de Tarifas para la Remisión. y asignar en casos 1 y 2

                if (dtComb.Rows.Count > 0)
                {
                    oRem.dTarifaVueloNal = dtComb.Rows[0]["TarifaVueloNal"].S().D();
                    oRem.dTarifaVueloInt = dtComb.Rows[0]["TarifaVueloInt"].S().D();
                }


                float fTiempoEsperaReal = 0;
                float fTotalTiempoEspera = 0;
                float fFactorNal = 0;
                float fFactorInt = 0;

                decimal dCombN = 0;
                decimal dCombI = 0;

                foreach (DataRow dr in dtConceptos.Rows)
                {
                    DataRow row = dtFinal.NewRow();
                    row["IdConcepto"] = dr.S("IdConcepto");
                    row["Concepto"] = dr.S("Descripcion");

                    string sCantidad = string.Empty;
                    float fTiempoT = 0;
                    float fTV = 0;
                    decimal dTarifaTEN = 0;
                    decimal dTarifaP = 0;

                    string dTarifaDlls = string.Empty;


                    switch (dr.S("IdConcepto"))
                    {
                        case "1":
                            #region TIEMPO VUELO NACIONAL

                            decimal dTarifaCostoDirN = 0;
                            decimal dCostoCombuN = 0;
                            decimal dCostoDirectoN = 0;

                            if (oRem.oLstIntercambios.Count > 0)
                            {
                                if (oRem.oLstIntercambios[0].dTarifaNalInter > 0)
                                {
                                    dTarifaCostoDirN = oRem.oLstIntercambios[0].dTarifaNalInter;
                                }
                                else
                                {
                                    decimal dCombusN = dtComb.Rows[0]["CombusN"].S().D();
                                    dCombN = dCombusN;
                                    dCombusN = (dCombusN * oRem.oLstIntercambios[0].dGaolnesInter);

                                    dCostoCombuN = dCombusN;

                                    if (oRem.oLstIntercambios[0].dCostoDirNalInter > 0)
                                    {
                                        dCostoDirectoN = oRem.oLstIntercambios[0].dCostoDirNalInter;
                                        dTarifaCostoDirN = oRem.oLstIntercambios[0].dCostoDirNalInter + dCombusN;
                                    }
                                }
                            }
                            else
                            {
                                dTarifaCostoDirN = oRem.dTarifaVueloNal;

                                dCombN = dtComb.Rows[0]["CombusN"].S().D();
                                dCostoCombuN = dtComb.Rows[0]["CombustibleNal"].S().D();
                                dCostoDirectoN = dtComb.Rows[0]["CostoDirectoNalV"].S().D();
                            }

                            oRem.dTarifaVueloNal = dTarifaCostoDirN;

                            sCantidad = ObtieneTotalTiempo(dtTramosNal, "TiempoCobrar", ref fTiempoT);
                            fTiempoVueloN = fTiempoT;

                            row["Cantidad"] = sCantidad;

                            row["CostoDirecto"] = dCostoDirectoN;

                            row["CostoComb"] = dCostoCombuN;

                            row["CombustibleAumento"] = dCombN;

                            row["TarifaDlls"] = Math.Round(dTarifaCostoDirN, 2);

                            row["Importe"] = fTiempoT.S().D() * dTarifaCostoDirN;

                            row["HrDescontar"] = sCantidad;

                            #endregion
                            break;
                        case "2":
                            #region TIEMPO VUELO INTERNACIONAL

                            decimal dTarifaCostoDirI = 0;
                            decimal dCostoCombuI = 0;
                            decimal dCostoDirectoI = 0;

                            if (oRem.oLstIntercambios.Count > 0)
                            {
                                if (oRem.oLstIntercambios[0].dTarifaIntInter > 0)
                                    dTarifaCostoDirI = oRem.oLstIntercambios[0].dTarifaIntInter;
                                else
                                {
                                    decimal CombusI = dtComb.Rows[0]["CombusI"].S().D();
                                    dCombI = CombusI;
                                    CombusI = (CombusI * oRem.oLstIntercambios[0].dGaolnesInter);

                                    dCostoCombuI = CombusI;
                                    dCostoDirectoI = oRem.oLstIntercambios[0].dCostoDirNalInter;

                                    if (oRem.oLstIntercambios[0].dCostoDirNalInter > 0)
                                    {
                                        dTarifaCostoDirI = oRem.oLstIntercambios[0].dCostoDirNalInter + CombusI;
                                    }
                                }
                            }
                            else
                            {
                                dTarifaCostoDirI = oRem.dTarifaVueloInt;

                                dCombI = dtComb.Rows[0]["CombusI"].S().D();
                                dCostoCombuI = dtComb.Rows[0]["CombustibleInt"].S().D();
                                dCostoDirectoI = dtComb.Rows[0]["CostoDirectoIntV"].S().D();
                            }

                            oRem.dTarifaVueloInt = dTarifaCostoDirI;

                            sCantidad = ObtieneTotalTiempo(dtTramosInt, "TiempoCobrar", ref fTiempoT);
                            fTiempoVueloI = fTiempoT;

                            row["Cantidad"] = sCantidad;

                            row["CostoDirecto"] = dCostoDirectoI;

                            row["CostoComb"] = dCostoCombuI;

                            row["CombustibleAumento"] = dCombI;

                            row["TarifaDlls"] = Math.Round(dTarifaCostoDirI, 2);

                            row["Importe"] = fTiempoT.S().D() * dTarifaCostoDirI;

                            row["HrDescontar"] = sCantidad;
                            #endregion
                            break;
                        case "3":
                            #region TIEMPO DE ESPERA NACIONAL

                            float fEsperaN = 0;
                            float fEsperaI = 0;
                            float fTotalTiempoCobrar = 0;


                            // SE SUMA EL TOTAL DEL TIEMPO DE ESPERA EN NACIONALES E INTERNACIONALES
                            ObtieneTotalTiempo(dtTramos, "TiempoEspera", ref fTotalTiempoEspera);

                            if (fTotalTiempoEspera > 0)
                            {
                                ObtieneTotalTiempo(dtTramos, "TiempoCobrar", ref fTotalTiempoCobrar);

                                //sCantidad = ObtieneTotalTiempo(dtTramosNalEP, "TiempoEspera", ref fEsperaN);
                                sCantidad = ObtieneTotalTiempo(dtTramosIntEP, "TiempoEspera", ref fEsperaI);

                                fFactorInt = fEsperaI / fTotalTiempoEspera;
                                fFactorNal = (1 - fFactorInt);


                                if (oRem.bAplicaEsperaLibre)
                                {
                                    if (oRem.dHorasPorVuelo > 0)
                                    {
                                        fTV = float.Parse(oRem.dHorasPorVuelo.S());
                                    }
                                    else if (oRem.dFactorHrVuelo > 0)
                                        fTV = (fTotalTiempoCobrar * float.Parse(oRem.dFactorHrVuelo.S()));

                                    sCantidad = RestaTiempoString(fTotalTiempoEspera, fTV);

                                    fTiempoEsperaReal = ConvierteTiempoaDecimal(sCantidad);
                                }
                                else
                                {
                                    fTiempoEsperaReal = fTotalTiempoEspera;
                                }


                                // SE DETERMINA EL NUMERO DE PERNOCTAS DE TODO EL TIEMPO DE ESPERA
                                for (int i = 0; i < 100; i++)
                                {
                                    if (fTiempoEsperaReal >= oRem.iHorasPernocta)
                                    {
                                        iNoPernoctasGlobal++;
                                        fTiempoEsperaReal = fTiempoEsperaReal - 24;
                                    }
                                    else
                                    {
                                        if (fTiempoEsperaReal > 0 && fTiempoEsperaReal < oRem.iHorasPernocta)
                                        {
                                            fTiempoEsperaGlobal = fTiempoEsperaReal;
                                            break;
                                        }
                                        else
                                        {
                                            fTiempoEsperaReal = 0;
                                            fTiempoEsperaGlobal = fTiempoEsperaReal;
                                            break;
                                        }
                                    }
                                }

                                //int iPernoctasLibres = new DBRemision().DBGetObtienePernoctasLibres(oRem.iIdContrato);
                                //if (iPernoctasLibres > 0)
                                //{
                                //    if (iNoPernoctasGlobal == iPernoctasLibres)
                                //    {
                                //        iNoPernoctasGlobal = 0;
                                //        iPernoctasLibres = 0;
                                //    }
                                //    else if (iNoPernoctasGlobal < iPernoctasLibres)
                                //    {
                                //        iPernoctasLibres = iPernoctasLibres - iNoPernoctasGlobal;
                                //        iNoPernoctasGlobal = 0;
                                //    }
                                //    else if (iNoPernoctasGlobal > iPernoctasLibres)
                                //    {
                                //        iNoPernoctasGlobal = iNoPernoctasGlobal - iPernoctasLibres;
                                //        iPernoctasLibres = 0;
                                //    }

                                //    // Actualiza Pernoctas en el periodo actual
                                //    long iResult = new DBRemision().DBSetActualizaPernoctasDisponibles(oRem.iIdContrato, iPernoctasLibres);
                                //}

                                // Termina el proceso y asigna cantidades

                                fTiempoEsperaN = fTiempoEsperaGlobal * fFactorNal;
                                sCantidad = ConvierteDecimalATiempo(fTiempoEsperaN.S().D());

                                if (oRem.bSeCobreEspera)
                                {
                                    row["Cantidad"] = sCantidad;

                                    if (oRem.dTarifaNalEspera > 0)
                                        dTarifaTEN = oRem.dTarifaNalEspera;
                                    else
                                    {
                                        dTarifaTEN = (oRem.dTarifaVueloNal * oRem.dPorTarifaNalEspera);
                                    }
                                }
                                else
                                {
                                    row["Cantidad"] = "00:00";
                                    dTarifaTEN = 0;
                                }
                            }
                            else
                            {
                                row["Cantidad"] = "00:00";
                                dTarifaTEN = 0;

                                if (oRem.dTarifaNalEspera > 0)
                                    dTarifaTEN = oRem.dTarifaNalEspera;
                                else
                                {
                                    dTarifaTEN = (oRem.dTarifaVueloNal * oRem.dPorTarifaNalEspera);
                                }

                                fTiempoEsperaN = 0;
                            }

                            row["TarifaDlls"] = Math.Round(dTarifaTEN, 2);

                            row["Importe"] = fTiempoEsperaN.S().D() * dTarifaTEN;

                            if (oRem.bSeDescuentaEsperaNal)
                            {
                                decimal dHr = (fTiempoEsperaN.S().D() * oRem.dFactorEHrVueloNal);
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }
                            else
                                row["HrDescontar"] = "00:00";

                            #endregion
                            break;
                        case "4":
                            #region TIEMPO DE ESPERA INTERNACIONAL

                            if (fTotalTiempoEspera > 0)
                            {
                                fTiempoEsperaI = fTiempoEsperaGlobal * fFactorInt;
                                sCantidad = ConvierteDecimalATiempo(fTiempoEsperaI.S().D());

                                if (oRem.bSeCobreEspera)
                                {
                                    row["Cantidad"] = sCantidad;

                                    if (oRem.dTarifaNalEspera > 0)
                                        dTarifaTEN = oRem.dTarifaIntEspera;
                                    else
                                    {
                                        dTarifaTEN = (oRem.dTarifaVueloInt * oRem.dPorTarifaNalEspera);
                                    }
                                }
                                else
                                {
                                    row["Cantidad"] = "00:00";
                                    dTarifaTEN = 0;
                                }

                            }
                            else
                            {
                                row["Cantidad"] = "00:00";
                                dTarifaTEN = 0;

                                if (oRem.dTarifaNalEspera > 0)
                                    dTarifaTEN = oRem.dTarifaIntEspera;
                                else
                                {
                                    dTarifaTEN = (oRem.dTarifaVueloInt * oRem.dPorTarifaNalEspera);
                                }

                                fTiempoEsperaI = 0;
                            }

                            row["TarifaDlls"] = Math.Round(dTarifaTEN, 2);

                            row["Importe"] = fTiempoEsperaI.S().D() * dTarifaTEN;

                            if (oRem.bSeDescuentaEsperaInt)
                            {
                                decimal dHr = (fTiempoEsperaI.S().D() * oRem.dFactorEHrVueloInt);
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }
                            else
                                row["HrDescontar"] = "00:00";

                            #endregion
                            break;
                        case "5":
                            #region PERNOCTA NACIONAL

                            if (fTotalTiempoEspera > 0)
                            {
                                fNoPernoctasN = iNoPernoctasGlobal * fFactorNal;

                                if (oRem.bSeCobraPernoctas)
                                {
                                    row["Cantidad"] = fNoPernoctasN.S();

                                    if (oRem.dTarifaDolaresNal > 0)
                                        dTarifaP = oRem.dTarifaDolaresNal;
                                    else
                                    {
                                        dTarifaP = (oRem.dTarifaVueloNal * oRem.dPorTarifaVueloNal);
                                    }
                                }
                                else
                                {
                                    row["Cantidad"] = "0";
                                    dTarifaP = 0;
                                }
                            }
                            else
                            {
                                row["Cantidad"] = "0";
                                dTarifaP = 0;

                                if (oRem.dTarifaDolaresNal > 0)
                                    dTarifaP = oRem.dTarifaDolaresNal;
                                else
                                {
                                    dTarifaP = (oRem.dTarifaVueloNal * oRem.dPorTarifaVueloNal);
                                }

                                fNoPernoctasN = 0;
                            }

                            row["TarifaDlls"] = Math.Round(dTarifaP, 2);

                            row["Importe"] = decimal.Parse(fNoPernoctasN.S()) * dTarifaP;

                            if (oRem.bSeDescuentanPerNal)
                            {
                                decimal dHr = (decimal.Parse(fNoPernoctasN.S()) * oRem.dFactorConvHrVueloNal);
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }
                            else
                                row["HrDescontar"] = "00:00";
                            #endregion
                            break;
                        case "6":
                            #region PERNOCTA INTERNACIONAL

                            if (fTotalTiempoEspera > 0)
                            {
                                fNoPernoctasI = iNoPernoctasGlobal * fFactorInt;

                                if (oRem.bSeCobraPernoctas)
                                {
                                    row["Cantidad"] = fNoPernoctasI.S();

                                    if (oRem.dTarifaDolaresInt > 0)
                                        dTarifaP = oRem.dTarifaDolaresInt;
                                    else
                                    {
                                        dTarifaP = (oRem.dTarifaVueloInt * oRem.dPorTarifaVueloInt);
                                    }
                                }
                                else
                                {
                                    row["Cantidad"] = "0";
                                    dTarifaP = 0;
                                }
                            }
                            else
                            {
                                row["Cantidad"] = "0";
                                dTarifaP = 0;

                                if (oRem.dTarifaDolaresInt > 0)
                                    dTarifaP = oRem.dTarifaDolaresInt;
                                else
                                {
                                    dTarifaP = (oRem.dTarifaVueloInt * oRem.dPorTarifaVueloInt);
                                }

                                fNoPernoctasI = 0;
                            }

                            row["TarifaDlls"] = Math.Round(dTarifaP, 2);

                            row["Importe"] = decimal.Parse(fNoPernoctasI.S()) * dTarifaP;

                            if (oRem.bSeDescuentanPerInt)
                            {
                                decimal dHr = (decimal.Parse(fNoPernoctasI.S()) * oRem.dFactorConvHrVueloInt);
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }
                            else
                                row["HrDescontar"] = "00:00";
                            #endregion
                            break;
                    }

                    dtFinal.Rows.Add(row);
                }

                return dtFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string AgregaFactoresEsperaPernoctas(string sCantidad, int iTipoValor, DatosRemision odRem)
        {
            try
            {
                string sResult = string.Empty;
                decimal dTemp = 0;
                switch (iTipoValor)
                {
                    case 1: // Tiempo
                        dTemp = ConvierteTiempoaDecimal(sCantidad).S().D();
                        break;
                    case 2: // unidad
                        dTemp = sCantidad.S().D();
                        break;
                }

                if (odRem.bAplicoFactorEspecial)
                {
                    if (odRem.dFactorEspecialF > 0)
                        dTemp = dTemp * odRem.dFactorEspecialF;
                }

                if (odRem.bAplicoIntercambio)
                {
                    if (odRem.dFactorIntercambioF > 0)
                        dTemp = dTemp * odRem.dFactorIntercambioF;
                }

                //if (odRem.bAplicaFactorFechaPico)
                //{
                //    if (odRem.dFactorFechaPicoF > 0)
                //        dTemp = dTemp * odRem.dFactorFechaPicoF;
                //}

                if (odRem.bAplicaGiraEspera)
                {
                    if (odRem.dFactorGiraEsperaF > 0)
                        dTemp = dTemp * odRem.dFactorGiraEsperaF;
                }

                if (odRem.bAplicaGiraHorario)
                {
                    if (odRem.dFactorGiraHorarioF > 0)
                        dTemp = dTemp * odRem.dFactorGiraHorarioF;
                }

                switch (iTipoValor)
                {
                    case 1: // Tiempo
                        sResult = ConvierteDecimalATiempo(dTemp);
                        break;
                    case 2: // unidad
                        sResult = dTemp.S();
                        break;
                }

                return sResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /*
         * 
         *--=============================================================================================== 
         *                              EMPIEZA CODIGO DE PRESUPUESTOS
         *--===============================================================================================
         * 
         */
        public static DataTable CalculaCostosRemisionPresupuestos(DatosRemision oRem, ref DataTable dtPres2, int iIdTipoAeropuerto, Presupuesto oPres)
        {
            try
            {
                bool bBaseBase = false;
                string[] sTramos = oRem.sRuta.Split('-');
                if (sTramos[0] == sTramos[sTramos.Length - 1])
                {
                    bBaseBase = true;
                }

                DataTable dtTramos = new DataTable();

                if (oRem.dtTramos.Columns["RealVirtual"] == null)
                    oRem.dtTramos.Columns.Add("RealVirtual", typeof(int));

                string sTipoVuelo = "TiempoVuelo";
                string sSiglasAeropuerto = string.Empty;


                switch (oRem.eSeCobraFerry)
                {
                    case Enumeraciones.SeCobraFerrys.Todos:
                        #region SE COBRAN 'TODOS' LOS FERRYS
                        if (bBaseBase)
                        {
                            //if (sTramos.Length == 3)
                            //{
                            //    // Validación de 24 horas entre tramos para ver si se hacen 2 presupuestos
                            //    bool bAplicaDosPresupuestos = false;
                            //    bAplicaDosPresupuestos = oRem.dtTramos.Rows[0]["TiempoEspera"].S().Substring(0, 2).S().I() >= 24 ? true : false;

                            if (oRem.bTiemposPactados)
                            {
                                foreach (DataRow row in oRem.dtTramos.Rows)
                                {
                                    string sTiempo = ObtieneTramoPactadoPresupuestos(oRem, row.S("Origen"), row.S("Destino"), oPres.iIdGrupoModeloSol);

                                    if (sTiempo == "00:00:00" || sTiempo == string.Empty || sTiempo == "00:00")
                                    {
                                        row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos);
                                    }
                                    else
                                        row["TiempoCobrar"] = sTiempo;
                                }
                            }
                            else
                            {
                                foreach (DataRow row in oRem.dtTramos.Rows)
                                {
                                    row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos);
                                }
                            }


                            if (oRem.bPermiteDoblePresupuesto)
                            {
                                bool bAplicaDosPresupuestos = false;
                                dtPres2 = AgregaFerrysVirtualesCotizacion(oRem.dtTramos, ref bAplicaDosPresupuestos, oRem, oPres);
                            }

                            // Tramos finales
                            oRem.dtTramos = AplicaFactoresATiemposRemisionPresupuestos(oRem.dtTramos, oRem, oRem.iIdContrato);
                            dtTramos = CalculaCostosRemisionPresupuestos(oRem.iCobroTiempo, oRem.dtTramos, oRem);
                        }
                        else
                        {
                            string sTiempo = string.Empty;

                            if (oRem.bTiemposPactados)
                            {
                                foreach (DataRow row in oRem.dtTramos.Rows)
                                {
                                    sTiempo = ObtieneTramoPactadoPresupuestos(oRem, row.S("Origen"), row.S("Destino"), oPres.iIdGrupoModeloSol);

                                    if (sTiempo == "00:00:00" || sTiempo == string.Empty || sTiempo == "00:00")
                                    {
                                        row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos);
                                    }
                                    else
                                        row["TiempoCobrar"] = sTiempo;
                                }
                            }
                            else
                            {
                                foreach (DataRow row in oRem.dtTramos.Rows)
                                {
                                    row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos);
                                }
                            }

                            #region CODIGO PARA ADICIONAR FERRYS VIRTUALES
                            /*
                            // agregar ferrys virtuales
                            string sInicio = sTramos[0];
                            string sFin = sTramos[sTramos.Length - 1];
                            string sBasePre = string.Empty;
                            string sBaseFin = string.Empty;
                            int iIdAeropuerto = 0;
                            int iInicioFin = 0;  //  1.- Inicio   2.- Fin

                            sSiglasAeropuerto = iIdTipoAeropuerto == 2 ? "AeropuertoICAO" : "AeropuertoIATA"; // IATA 1     ICAO 2

                            foreach (DataRow dr in oRem.dtBases.Rows)
                            {
                                sBaseFin = dr.S(sSiglasAeropuerto);


                                if (dr.S("IdTipoBase") == "1")
                                    sBasePre = dr.S(sSiglasAeropuerto);


                                if (sInicio == dr.S(sSiglasAeropuerto) && dr.S("IdTipoBase") == "1")
                                {
                                    iIdAeropuerto = dr.S("IdAeropuerto").I();
                                    iInicioFin = 1;
                                    break;
                                }
                                else if (sFin == dr.S(sSiglasAeropuerto) && dr.S("IdTipoBase") == "1")
                                {
                                    iIdAeropuerto = dr.S("IdAeropuerto").I();
                                    iInicioFin = 2;
                                    break;
                                }
                                else if (sInicio == dr.S(sSiglasAeropuerto) && dr.S("IdTipoBase") == "2")
                                {
                                    iIdAeropuerto = dr.S("IdAeropuerto").I();
                                    iInicioFin = 1;
                                }
                                else if (sFin == dr.S(sSiglasAeropuerto) && dr.S("IdTipoBase") == "2")
                                {
                                    iIdAeropuerto = dr.S("IdAeropuerto").I();
                                    iInicioFin = 2;
                                }
                            }

                            int iIdOrigen = 0;
                            int iIdDestino = 0;

                            DateTime dtFechaLlegada;
                            TimeSpan ts;
                            int iCont = 1;
                            if (iInicioFin != 0 && iIdAeropuerto != 0)
                            {
                                DataRow dr = oRem.dtTramos.NewRow();

                                switch (iInicioFin)
                                {
                                    case 1: // Agregar Ferry Final
                                        #region FERRY FINAL
                                        sInicio = sTramos[sTramos.Length - 1];
                                        sFin = sBaseFin;

                                        iIdOrigen = new DBPresupuesto().GetIdAeropuertoPresupuestos(sInicio);
                                        iIdDestino = new DBPresupuesto().GetIdAeropuertoPresupuestos(sFin);

                                        sTiempo = ObtieneTiempoTramosPresupuestos(oRem, iIdOrigen, iIdDestino, oRem.iIdGrupoModeloPres);
                                        dr["IdOrigen"] = iIdOrigen;
                                        dr["IdDestino"] = iIdDestino;
                                        dr["Origen"] = sInicio;
                                        dr["Destino"] = sFin;
                                        dr["CantPax"] = "0";

                                        dr["FechaSalida"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"];
                                        dtFechaLlegada = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"].Dt();
                                        string[] sTiempos = sTiempo.Split(':');
                                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]));
                                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]));
                                        dr["FechaLlegada"] = dtFechaLlegada;
                                        
                                        ts = dtFechaLlegada - oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"].Dt();
                                        dr["TiempoCobrar"] = sTiempo == "00:00:00" ? ts.S() : sTiempo;

                                        dr["TiempoVuelo"] = "00:00:00";
                                        dr["TiempoEspera"] = "00:00:00";
                                        dr["SeCobra"] = 1;
                                        dr["RealVirtual"] = 1;


                                        oRem.dtTramos.Rows.Add(dr);

                                        #endregion
                                        break;

                                    case 2: // Agregar Ferry Inicial
                                        #region FERRY INICIAL

                                        sInicio = sBaseFin;
                                        sFin = sTramos[0];

                                        DataRow drI = oRem.dtTramos.NewRow();

                                        iIdOrigen = new DBPresupuesto().GetIdAeropuertoPresupuestos(sInicio);
                                        iIdDestino = new DBPresupuesto().GetIdAeropuertoPresupuestos(sFin);

                                        sTiempo = ObtieneTiempoTramosPresupuestos(oRem, iIdOrigen, iIdDestino, oRem.iIdGrupoModeloPres);
                                        drI = oRem.dtTramos.NewRow();
                                        drI["IdOrigen"] = iIdOrigen;
                                        drI["IdDestino"] = iIdDestino;
                                        drI["Origen"] = sInicio;
                                        drI["Destino"] = sFin;
                                        drI["CantPax"] = "0";


                                        drI["FechaLlegada"] = oRem.dtTramos.Rows[0]["FechaSalida"];
                                        dtFechaLlegada = oRem.dtTramos.Rows[0]["FechaSalida"].Dt();
                                        sTiempos = sTiempo.Split(':');
                                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]) * -1);
                                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]) * -1);
                                        drI["FechaSalida"] = dtFechaLlegada;

                                        ts = dtFechaLlegada - oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"].Dt();
                                        drI["TiempoCobrar"] = sTiempo == "00:00:00" ? ts.S() : sTiempo;


                                        drI["TiempoVuelo"] = "00:00:00";
                                        drI["TiempoEspera"] = "00:00:00";
                                        drI["RealVirtual"] = 1;
                                        drI["SeCobra"] = 1;

                                        oRem.dtTramos.Rows.Add(drI);

                                        DataTable dtInicio = oRem.dtTramos.Clone();
                                        dtInicio.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]);

                                        for (int i = 0; i < oRem.dtTramos.Rows.Count - 1; i++)
                                        {
                                            dtInicio.ImportRow(oRem.dtTramos.Rows[i]);
                                        }

                                        oRem.dtTramos = null;
                                        oRem.dtTramos = dtInicio.Copy();
                                        dtInicio.Dispose();
                                        #endregion
                                        break;
                                }
                            }
                            else
                            {
                                // Agregar Ferry de Inicio y Final con la base Predeterminada
                                #region FERRY INICIAL Y FINAL
                                sInicio = sBasePre;
                                sFin = oRem.dtTramos.Rows[0]["Origen"].S();

                                // FERRY INICIAL
                                DataRow drI = oRem.dtTramos.NewRow();
                                iIdOrigen = new DBPresupuesto().GetIdAeropuertoPresupuestos(sInicio);
                                iIdDestino = new DBPresupuesto().GetIdAeropuertoPresupuestos(sFin);

                                sTiempo = ObtieneTiempoTramosPresupuestos(oRem, iIdOrigen, iIdDestino, oRem.iIdGrupoModeloPres);
                                drI = oRem.dtTramos.NewRow();
                                drI["IdOrigen"] = iIdOrigen;
                                drI["IdDestino"] = iIdDestino;
                                drI["Origen"] = sInicio;
                                drI["Destino"] = sFin;
                                drI["CantPax"] = "0";


                                drI["FechaLlegada"] = oRem.dtTramos.Rows[0]["FechaSalida"];
                                dtFechaLlegada = oRem.dtTramos.Rows[0]["FechaSalida"].Dt();
                                string[] sTiempos = sTiempo.Split(':');
                                dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]) * -1);
                                dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]) * -1);
                                drI["FechaSalida"] = dtFechaLlegada;

                                ts = oRem.dtTramos.Rows[0]["FechaSalida"].Dt() - dtFechaLlegada;
                                drI["TiempoCobrar"] = sTiempo == "00:00:00" ? ts.S() : sTiempo;


                                drI["TiempoVuelo"] = "00:00:00";
                                drI["TiempoEspera"] = "00:00:00";
                                drI["RealVirtual"] = 1;
                                drI["SeCobra"] = 1;

                                oRem.dtTramos.Rows.Add(drI);


                                // FERRY FINAL
                                DataRow drF = oRem.dtTramos.NewRow();
                                sInicio = sTramos[sTramos.Length - 1];
                                sFin = sBasePre;

                                iIdOrigen = new DBPresupuesto().GetIdAeropuertoPresupuestos(sInicio);
                                iIdDestino = new DBPresupuesto().GetIdAeropuertoPresupuestos(sFin);

                                sTiempo = ObtieneTiempoTramosPresupuestos(oRem, iIdOrigen, iIdDestino, oRem.iIdGrupoModeloPres);
                                drF = oRem.dtTramos.NewRow();
                                drF["IdOrigen"] = iIdOrigen;
                                drF["IdDestino"] = iIdDestino;
                                drF["Origen"] = sInicio;
                                drF["Destino"] = sFin;
                                drF["CantPax"] = "0";


                                drF["FechaSalida"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"];
                                dtFechaLlegada = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"].Dt();
                                sTiempos = sTiempo.Split(':');
                                dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]));
                                dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]));
                                drF["FechaLlegada"] = dtFechaLlegada;

                                ts = dtFechaLlegada - oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"].Dt();
                                drF["TiempoCobrar"] = sTiempo == "00:00:00" ? ts.S() : sTiempo;


                                drF["TiempoVuelo"] = "00:00:00";
                                drF["TiempoEspera"] = "00:00:00";
                                drF["RealVirtual"] = 1;
                                drF["SeCobra"] = 1;
                                
                                oRem.dtTramos.Rows.Add(drF);


                                DataTable dtFin = oRem.dtTramos.Clone();
                                dtFin.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]);

                                for (int i = 0; i < oRem.dtTramos.Rows.Count - 2; i++)
                                {
                                    dtFin.ImportRow(oRem.dtTramos.Rows[i]);
                                }

                                dtFin.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]);


                                oRem.dtTramos = null;
                                oRem.dtTramos = dtFin.Copy();
                                dtFin.Dispose();
                                #endregion
                            }
                            */
                            #endregion

                            // Tramos finales
                            oRem.dtTramos = AplicaFactoresATiemposRemisionPresupuestos(oRem.dtTramos, oRem, oRem.iIdContrato);
                            dtTramos = CalculaCostosRemisionPresupuestos(oRem.iCobroTiempo, oRem.dtTramos, oRem);
                        }
                        #endregion
                        break;

                    case Enumeraciones.SeCobraFerrys.Reposicionamiento:
                        #region SE COBRAN FERRYS DE 'REPOSICIONAMIENTO'

                        if (oRem.bTiemposPactados)
                        {
                            foreach (DataRow row in oRem.dtTramos.Rows)
                            {
                                string sTiempo = ObtieneTramoPactadoPresupuestos(oRem, row.S("Origen"), row.S("Destino"), oPres.iIdGrupoModeloSol);

                                if (sTiempo == "00:00:00" || sTiempo == string.Empty || sTiempo == "00:00")
                                {
                                    row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos);
                                }
                                else
                                    row["TiempoCobrar"] = sTiempo;
                            }
                        }
                        else
                        {
                            foreach (DataRow row in oRem.dtTramos.Rows)
                            {
                                row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos);
                            }
                        }

                        #region CODIGO PARA ADICIONAR FERRYS VIRTUALES
                        /*
                        string sBasePreFR = string.Empty;
                        string sInicioFR = string.Empty;
                        string sFinFR = string.Empty;
                        int iAeropuertoFR = 0;
                        int iInicioFinFR = 0;


                        sSiglasAeropuerto = iIdTipoAeropuerto == 2 ? "AeropuertoICAO" : "AeropuertoIATA"; // IATA 1     ICAO 2


                        sInicioFR = sTramos[0];
                        sFinFR = sTramos[sTramos.Length - 1];

                        bool bAgregaFV = true;
                        foreach (DataRow dr in oRem.dtBases.Rows)
                        {
                            if (sInicioFR == dr.S("AeropuertoICAO") || sFinFR == dr.S("AeropuertoICAO"))
                            {
                                bAgregaFV = false;
                                break;
                            }
                        }

                        if (bAgregaFV)
                        {
                            foreach (DataRow dr in oRem.dtBases.Rows)
                            {
                                //sBaseFin = dr.S("AeropuertoICAO");

                                if (dr.S("IdTipoBase") == "1")
                                    sBasePreFR = dr.S(sSiglasAeropuerto);

                                if (sInicioFR == dr.S(sSiglasAeropuerto) && dr.S("IdTipoBase") == "1")
                                {
                                    iAeropuertoFR = dr.S("IdAeropuerto").I();
                                    iInicioFinFR = 1;
                                    break;
                                }
                                else if (sFinFR == dr.S(sSiglasAeropuerto) && dr.S("IdTipoBase") == "1")
                                {
                                    iAeropuertoFR = dr.S("IdAeropuerto").I();
                                    iInicioFinFR = 2;
                                    break;
                                }
                                else if (sInicioFR == dr.S(sSiglasAeropuerto) && dr.S("IdTipoBase") == "2")
                                {
                                    iAeropuertoFR = dr.S("IdAeropuerto").I();
                                    iInicioFinFR = 1;
                                }
                                else if (sFinFR == dr.S(sSiglasAeropuerto) && dr.S("IdTipoBase") == "2")
                                {
                                    iAeropuertoFR = dr.S("IdAeropuerto").I();
                                    iInicioFinFR = 2;
                                }
                            }

                            if (iInicioFinFR != 0 && iAeropuertoFR != 0)
                            {
                                switch (iInicioFinFR)
                                {
                                    case 1:
                                        AgregaFerryTablaPresupuestos(iInicioFinFR, oRem, iAeropuertoFR, sBasePreFR);
                                        break;
                                    case 2:
                                        AgregaFerryTablaPresupuestos(iInicioFinFR, oRem, iAeropuertoFR, sBasePreFR);
                                        break;
                                }
                            }
                            else
                            {
                                // Agregar ferrys virtuales 
                                // Si no sale de base y no regresa a base   --->   Agrega ferry inicial y final y cobrar el mas corto
                                AgregaFerryTablaPresupuestos(3, oRem, iAeropuertoFR, sBasePreFR);
                            }
                        }
                        */
                        #endregion

                        oRem.dtTramos = AplicaFactoresATiemposRemisionPresupuestos(oRem.dtTramos, oRem, oRem.iIdContrato);
                        dtTramos = CalculaCostosRemisionPresupuestos(oRem.iCobroTiempo, oRem.dtTramos, oRem);

                        // Si no sale de base y no regresa a base -> Agrega ferry inicial y final y cobrar el mas corto

                        // Si, sale de base o regresa a base con pax, agregar el ferry pero no cobrar.

                        // Si, los ferrys de llegada o salida son reales -> cobrar el mas corto

                        #endregion
                        break;

                    case Enumeraciones.SeCobraFerrys.Ninguno:
                        #region NO SE COBRAN LOS FERRYS

                        if (oRem.bTiemposPactados)
                        {
                            foreach (DataRow row in oRem.dtTramos.Rows)
                            {
                                string sTiempo = ObtieneTramoPactadoPresupuestos(oRem, row.S("Origen"), row.S("Destino"), oPres.iIdGrupoModeloSol);

                                if (sTiempo == "00:00:00" || sTiempo == string.Empty || sTiempo == "00:00")
                                {
                                    row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos);
                                }
                                else
                                    row["TiempoCobrar"] = sTiempo;
                            }
                        }
                        else
                        {
                            foreach (DataRow row in oRem.dtTramos.Rows)
                            {
                                row["TiempoCobrar"] = DefineTiempoCobrar(oRem.iCobroTiempo, row[sTipoVuelo].S(), oRem.iMasMinutos);
                            }
                        }

                        oRem.dtTramos = AplicaFactoresATiemposRemisionPresupuestos(oRem.dtTramos, oRem, oRem.iIdContrato);

                        if (oRem.bAplicaFerryIntercambio)
                        {
                            foreach (DataRow row in oRem.dtTramos.Rows)
                            {
                                if (row.S("CantPax") == "0")
                                {
                                    row["SeCobra"] = 1;
                                }
                            }
                        }
                        else
                        {
                            foreach (DataRow row in oRem.dtTramos.Rows)
                            {
                                if (row.S("CantPax") == "0")
                                {
                                    row["SeCobra"] = 0;
                                }
                            }
                        }

                        dtTramos = CalculaCostosRemisionPresupuestos(oRem.iCobroTiempo, oRem.dtTramos, oRem);

                        #endregion
                        break;
                }

                return dtTramos.Copy();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string ObtieneTiempoFerrysPresupuestos(DatosRemision oRem, string sOrigen, string sDestino, int iIdGrupoModelo)
        {
            try
            {
                //1.- Primero se consulta en los del contrato
                //2.- Buscar dentro de la tabla general
                //3.- Promedio de los ultimos 10 vuelos y si no existe... solicitarlo

                string sTiempo = string.Empty;

                if (oRem.dtTramosPactadosEsp.Rows.Count > 0)
                {
                    foreach (DataRow row in oRem.dtTramosPactadosEsp.Rows)
                    {
                        if ((row.S("OrigenICAO") == sOrigen || row.S("OrigenIATA") == sOrigen) && (row.S("DestinoICAO") == sDestino || row.S("DestinoIATA") == sDestino))
                        {
                            sTiempo = row.S("TiempoVuelo");
                            break;
                        }
                    }
                }

                oRem.dtTramosPactadosGen = new DBRemision().DBGetObtieneTramosPactadosGeneralesGrupoModelo(iIdGrupoModelo);
                if (sTiempo == string.Empty && oRem.dtTramosPactadosGen.Rows.Count > 0)
                {
                    foreach (DataRow row in oRem.dtTramosPactadosGen.Rows)
                    {
                        if ((row.S("AeropuertoICAO") == sOrigen || row.S("AeropuertoIATA") == sOrigen) && (row.S("AeropuertoICAOD") == sDestino || row.S("AeropuertoIATAD") == sDestino))
                        {
                            sTiempo = row.S("TiempoDeVuelo");
                            break;
                        }
                    }
                }

                if (sTiempo == string.Empty)
                {
                    // Obtiene promedio
                    sTiempo = new DBUtils().DBGetPromedioVuelo(sOrigen, sDestino);
                }

                if (sTiempo == string.Empty)
                    sTiempo = "00:00:00";

                if (sTiempo.Length == 5)
                    sTiempo = sTiempo + ":00";

                return sTiempo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ObtieneTiempoTramosPresupuestos(DatosRemision oRem, int iIdOrigen, int iIdDestino, int iIdGrupoModelo)
        {
            try
            {
                //1.- Primero se consulta en los del contrato
                //2.- Buscar dentro de la tabla general
                //3.- Promedio de los ultimos 10 vuelos y si no existe... solicitarlo

                string sTiempo = string.Empty;

                if (oRem.bTiemposPactados)
                {
                    if (oRem.dtTramosPactadosEsp.Rows.Count > 0)
                    {
                        foreach (DataRow row in oRem.dtTramosPactadosEsp.Rows)
                        {
                            if (row.S("IdAeropuertoO").I() == iIdOrigen && row.S("IdAeropuertoD").I() == iIdDestino)
                            {
                                sTiempo = row.S("TiempoVuelo");
                                break;
                            }
                        }
                    }

                    oRem.dtTramosPactadosGen = new DBRemision().DBGetObtieneTramosPactadosGeneralesGrupoModelo(iIdGrupoModelo);
                    if (sTiempo == string.Empty && oRem.dtTramosPactadosGen.Rows.Count > 0)
                    {
                        foreach (DataRow row in oRem.dtTramosPactadosGen.Rows)
                        {
                            if (row.S("IdOrigen").I() == iIdOrigen && row.S("IdDestino").I() == iIdDestino)
                            {
                                sTiempo = row.S("TiempoDeVuelo");
                                break;
                            }
                        }
                    }
                }

                if (sTiempo == string.Empty)
                {
                    // Obtiene promedio
                    sTiempo = new DBUtils().DBGetPromedioVueloID(iIdOrigen, iIdDestino);
                }

                if (sTiempo == string.Empty)
                    sTiempo = "00:00:00";

                if (sTiempo.Length == 5)
                    sTiempo = sTiempo + ":00";

                return sTiempo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable AgregaFerryTablaPresupuestos(int iPosicion, DatosRemision oRem, int iIdAeropuerto, string sBasePre)
        {
            try
            {
                string sBase = string.Empty;
                string sInicio = string.Empty;
                string sFin = string.Empty;

                //DataTable dtAero = new DBRemision().DBGetConsultaAeropuertoId(iIdAeropuerto);
                //if (dtAero.Rows.Count > 0)
                //    sBase = dtAero.Rows[0]["AeropuertoICAO"].S();
                sBase = sBasePre;


                string sTiempo = string.Empty;
                DateTime dtFechaLlegada;
                TimeSpan ts;
                DataRow drI;
                DataRow drF;
                int iCont = 1;
                DataTable dtFin;

                int iIdOrigen = 0;
                int iIdDestino = 0;

                switch (iPosicion)
                {
                    case 1: // Agregar Ferry Final
                        #region Ferry Final
                        sInicio = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["Destino"].S();
                        sFin = sBase;

                        iIdOrigen = new DBPresupuesto().GetIdAeropuertoPresupuestos(sInicio);
                        iIdDestino = new DBPresupuesto().GetIdAeropuertoPresupuestos(sFin);

                        sTiempo = ObtieneTiempoTramosPresupuestos(oRem, iIdOrigen, iIdDestino, oRem.iIdGrupoModeloPres);


                        drF = oRem.dtTramos.NewRow();
                        drF["IdOrigen"] = iIdOrigen;
                        drF["IdDestino"] = iIdDestino;
                        drF["Origen"] = sInicio;
                        drF["Destino"] = sFin;
                        drF["CantPax"] = "0";

                        drF["FechaSalida"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"];
                        dtFechaLlegada = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"].Dt();
                        string[] sTiempos = sTiempo.Split(':');
                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]));
                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]));
                        drF["FechaLlegada"] = dtFechaLlegada;

                        ts = dtFechaLlegada - oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"].Dt();
                        drF["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S() : sTiempo;

                        drF["TiempoVuelo"] = "00:00";
                        drF["TiempoEspera"] = "00:00";
                        drF["RealVirtual"] = 1;
                        drF["SeCobra"] = 0;

                        oRem.dtTramos.Rows.Add(drF);
                        #endregion

                        break;

                    case 2: // Agregar Ferry Inicial
                        #region Ferry Inicial
                        sInicio = sBase;
                        sFin = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["Origen"].S();

                        iIdOrigen = new DBPresupuesto().GetIdAeropuertoPresupuestos(sInicio);
                        iIdDestino = new DBPresupuesto().GetIdAeropuertoPresupuestos(sFin);

                        sTiempo = ObtieneTiempoTramosPresupuestos(oRem, iIdOrigen, iIdDestino, oRem.iIdGrupoModeloPres);
                        drI = oRem.dtTramos.NewRow();
                        drI["IdOrigen"] = iIdOrigen;
                        drI["IdDestino"] = iIdDestino;
                        drI["Origen"] = sInicio;
                        drI["Destino"] = sFin;
                        drI["CantPax"] = "0";


                        drI["FechaLlegada"] = oRem.dtTramos.Rows[0]["FechaSalida"];
                        dtFechaLlegada = oRem.dtTramos.Rows[0]["FechaSalida"].Dt();
                        sTiempos = sTiempo.Split(':');
                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]) * -1);
                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]) * -1);
                        drI["FechaSalida"] = dtFechaLlegada;

                        ts = dtFechaLlegada - oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["FechaLlegada"].Dt();
                        drI["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S() : sTiempo;


                        drI["TiempoVuelo"] = "00:00";
                        drI["TiempoEspera"] = "00:00";
                        drI["RealVirtual"] = 1;
                        drI["SeCobra"] = 0;

                        oRem.dtTramos.Rows.Add(drI);

                        DataTable dtInicio = oRem.dtTramos.Clone();
                        dtInicio.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]);

                        for (int i = 0; i < oRem.dtTramos.Rows.Count - 1; i++)
                        {
                            dtInicio.ImportRow(oRem.dtTramos.Rows[i]);
                        }

                        oRem.dtTramos = null;
                        oRem.dtTramos = dtInicio.Copy();
                        dtInicio.Dispose();
                        #endregion

                        break;

                    case 3: // Ferry Inicial y Final
                        #region Ferry Inicial y Final

                        // FERRY INICIAL
                        sInicio = sBasePre;
                        sFin = oRem.dtTramos.Rows[0]["Origen"].S();

                        iIdOrigen = new DBPresupuesto().GetIdAeropuertoPresupuestos(sInicio);
                        iIdDestino = new DBPresupuesto().GetIdAeropuertoPresupuestos(sFin);

                        sTiempo = ObtieneTiempoTramosPresupuestos(oRem, iIdOrigen, iIdDestino, oRem.iIdGrupoModeloPres);

                        drI = oRem.dtTramos.NewRow();
                        drI["IdOrigen"] = iIdOrigen;
                        drI["IdDestino"] = iIdDestino;
                        drI["Origen"] = sInicio;
                        drI["Destino"] = sFin;
                        drI["CantPax"] = "0";


                        drI["FechaLlegada"] = oRem.dtTramos.Rows[0]["FechaSalida"];
                        dtFechaLlegada = oRem.dtTramos.Rows[0]["FechaSalida"].Dt();
                        sTiempos = sTiempo.Split(':');
                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]) * -1);
                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]) * -1);
                        drI["FechaSalida"] = dtFechaLlegada;

                        ts = oRem.dtTramos.Rows[0]["FechaSalida"].Dt() - dtFechaLlegada;
                        drI["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S() : sTiempo;


                        drI["TiempoVuelo"] = "00:00";
                        drI["TiempoEspera"] = "00:00";
                        drI["RealVirtual"] = 1;
                        drI["SeCobra"] = 0;

                        oRem.dtTramos.Rows.Add(drI);


                        // FERRY FINAL
                        drF = oRem.dtTramos.NewRow();
                        sInicio = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["Destino"].S();
                        sFin = sBasePre;

                        iIdOrigen = new DBPresupuesto().GetIdAeropuertoPresupuestos(sInicio);
                        iIdDestino = new DBPresupuesto().GetIdAeropuertoPresupuestos(sFin);

                        sTiempo = ObtieneTiempoTramosPresupuestos(oRem, iIdOrigen, iIdDestino, oRem.iIdGrupoModeloPres);

                        drF = oRem.dtTramos.NewRow();
                        drF["IdOrigen"] = iIdOrigen;
                        drF["IdDestino"] = iIdDestino;
                        drF["Origen"] = sInicio;
                        drF["Destino"] = sFin;
                        drF["CantPax"] = "0";


                        drF["FechaSalida"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"];
                        dtFechaLlegada = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"].Dt();
                        sTiempos = sTiempo.Split(':');
                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]));
                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]));
                        drF["FechaLlegada"] = dtFechaLlegada;

                        ts = dtFechaLlegada - oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaLlegada"].Dt();
                        drF["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S() : sTiempo;


                        drF["TiempoVuelo"] = "00:00";
                        drF["TiempoEspera"] = "00:00";
                        drF["RealVirtual"] = 1;
                        drF["SeCobra"] = 0;

                        oRem.dtTramos.Rows.Add(drF);


                        dtFin = oRem.dtTramos.Clone();
                        dtFin.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]);

                        for (int i = 0; i < oRem.dtTramos.Rows.Count - 2; i++)
                        {
                            dtFin.ImportRow(oRem.dtTramos.Rows[i]);
                        }

                        dtFin.ImportRow(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]);


                        oRem.dtTramos = null;
                        oRem.dtTramos = dtFin.Copy();
                        dtFin.Dispose();
                        #endregion

                        break;

                    case 4: // Ferry's VS Pernoctas
                        #region Ferry's VS Pernoctas
                        // FERRY INICIAL
                        sInicio = oRem.dtTramos.Rows[1]["Origen"].S(); ;
                        sFin = oRem.dtTramos.Rows[1]["Destino"].S();

                        iIdOrigen = new DBPresupuesto().GetIdAeropuertoPresupuestos(sInicio);
                        iIdDestino = new DBPresupuesto().GetIdAeropuertoPresupuestos(sFin);

                        sTiempo = ObtieneTiempoTramosPresupuestos(oRem, iIdOrigen, iIdDestino, oRem.iIdGrupoModeloPres);


                        drI = oRem.dtTramos.NewRow();
                        drI["IdOrigen"] = iIdOrigen;
                        drI["IdDestino"] = iIdDestino;
                        drI["Origen"] = sInicio;
                        drI["Destino"] = sFin;
                        drI["CantPax"] = "0";

                        drI["FechaSalida"] = oRem.dtTramos.Rows[0]["FechaLlegada"];
                        dtFechaLlegada = oRem.dtTramos.Rows[0]["FechaLlegada"].Dt();
                        string[] sTiempos2 = sTiempo.Split(':');
                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos2[0]));
                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos2[1]));
                        drI["FechaLlegada"] = dtFechaLlegada;

                        ts = dtFechaLlegada - oRem.dtTramos.Rows[0]["FechaLlegada"].Dt();
                        drI["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S() : sTiempo;


                        drI["TiempoVuelo"] = "00:00";
                        drI["TiempoEspera"] = "00:00";
                        drI["RealVirtual"] = 1;
                        drI["SeCobra"] = 1;

                        oRem.dtTramos.Rows.Add(drI);


                        // Ferry Tercera posición
                        drF = oRem.dtTramos.NewRow();
                        sInicio = oRem.dtTramos.Rows[0]["Origen"].S(); ;
                        sFin = oRem.dtTramos.Rows[0]["Destino"].S();

                        iIdOrigen = new DBPresupuesto().GetIdAeropuertoPresupuestos(sInicio);
                        iIdDestino = new DBPresupuesto().GetIdAeropuertoPresupuestos(sFin);

                        sTiempo = ObtieneTiempoTramosPresupuestos(oRem, iIdOrigen, iIdDestino, oRem.iIdGrupoModeloPres);

                        drF = oRem.dtTramos.NewRow();
                        drF["IdOrigen"] = iIdOrigen;
                        drF["IdDestino"] = iIdDestino;
                        drF["Origen"] = sInicio;
                        drF["Destino"] = sFin;
                        drF["CantPax"] = "0";


                        drF["FechaLlegada"] = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaSalida"];
                        dtFechaLlegada = oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaSalida"].Dt();
                        sTiempos = sTiempo.Split(':');
                        dtFechaLlegada = dtFechaLlegada.AddHours(double.Parse(sTiempos[0]) * -1);
                        dtFechaLlegada = dtFechaLlegada.AddMinutes(double.Parse(sTiempos[1]) * -1);
                        drF["FechaSalida"] = dtFechaLlegada;

                        ts = dtFechaLlegada - oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 2]["FechaSalida"].Dt();
                        drF["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? ts.S() : sTiempo;


                        drF["TiempoVuelo"] = "00:00";
                        drF["TiempoEspera"] = "00:00";
                        drF["RealVirtual"] = 1;
                        drF["SeCobra"] = 1;

                        oRem.dtTramos.Rows.Add(drF);

                        dtFin = oRem.dtTramos.Clone();
                        if (oRem.dtTramos.Rows.Count == 4)
                        {
                            dtFin.ImportRow(oRem.dtTramos.Rows[0]);
                            dtFin.ImportRow(oRem.dtTramos.Rows[2]);
                            dtFin.ImportRow(oRem.dtTramos.Rows[3]);
                            dtFin.ImportRow(oRem.dtTramos.Rows[1]);
                        }
                        oRem.dtTramos = null;
                        oRem.dtTramos = dtFin.Copy();
                        dtFin.Dispose();
                        #endregion

                        break;
                }

                if (oRem.dtTramos.Rows.Count > 0)
                {
                    if (oRem.eSeCobraFerry == Enumeraciones.SeCobraFerrys.Reposicionamiento)
                    {
                        if (oRem.dtTramos.Rows[0]["CantPax"].S() == "0" && oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["CantPax"].S() == "0")
                        {
                            float fInicio = Utils.ConvierteTiempoaDecimal(oRem.dtTramos.Rows[0]["TiempoCobrar"].S());
                            float fFin = Utils.ConvierteTiempoaDecimal(oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["TiempoCobrar"].S());

                            if (fInicio < fFin)
                                oRem.dtTramos.Rows[0]["SeCobra"] = 1;
                            else
                                oRem.dtTramos.Rows[oRem.dtTramos.Rows.Count - 1]["SeCobra"] = 1;
                        }
                    }
                }

                return oRem.dtTramos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable AplicaFactoresATiemposRemisionPresupuestos(DataTable dtTramos, DatosRemision oRem, int iIdContrato)
        {
            try
            {
                // Aplica Factor de Tramos
                #region FACTOR DE TRAMOS
                if (oRem.eCobroCombustible == Enumeraciones.CobroCombustible.HorasDescuento)
                {
                    foreach (DataRow dr in dtTramos.Rows)
                    {
                        string sTiempoCobrar = dr.S("TiempoCobrar");
                        float dTiempoCobrar = ConvierteTiempoaDecimal(sTiempoCobrar);

                        double dFactor = 0;
                        double dTotal = 0;

                        if (dr["SeCobra"].S().I() == 1)
                        {
                            oRem.bAplicaFactorTramoNacional = true;
                            oRem.bAplicaFactorTramoInternacional = true;
                            DataTable dt = new DBRemision().DBGetConsultaAeropuertoId(dr.S("IdOrigen").I());
                            switch (dt.Rows[0]["TipoDestino"].S().ToUpper())
                            {
                                case "N":
                                case "F":
                                    dFactor = oRem.dFactorTramosNal.S().Db();
                                    dTotal = dTiempoCobrar * dFactor;
                                    break;
                                case "E":
                                    dFactor = oRem.dFactorTramosInt.S().Db();
                                    dTotal = dTiempoCobrar * dFactor;
                                    break;
                            }
                        }

                        sTiempoCobrar = ConvierteDoubleATiempo(dTotal);
                        dr["TiempoCobrar"] = sTiempoCobrar;
                    }
                }
                #endregion

                // ¿Aplica Factor de Intercambio?
                #region APLICA INTERCAMBIO

                DataTable dtI = new DBContrato().DBGetIntercambios(iIdContrato);

                if (oRem.iIdGrupoModelo != oRem.iIdGrupoModeloPres)
                {
                    oRem.bAplicoIntercambio = true;

                    DataRow[] drIs = dtI.Select("IdGrupoModelo = " + oRem.iIdGrupoModeloPres);
                    if (drIs.Length > 0)
                    {
                        foreach (DataRow dr in dtTramos.Rows)
                        {
                            string sTiempoCobrar = dr.S("TiempoCobrar");
                            float dTiempoCobrar = ConvierteTiempoaDecimal(sTiempoCobrar);
                            double dFactor = drIs[0]["Factor"].S().Db();
                            oRem.bAplicaFerryIntercambio = drIs[0]["AplicaFerry"].S().B();

                            if (dFactor != 0)
                            {
                                double dTotal = dTiempoCobrar.S().Db() * dFactor;
                                sTiempoCobrar = ConvierteDoubleATiempo(dTotal);

                                dr["TiempoCobrar"] = sTiempoCobrar;

                                oRem.dFactorIntercambioF = decimal.Parse(dFactor.S());
                            }
                            else
                            {
                                IntercambioRemision oInt = new IntercambioRemision();
                                oInt.sMatriculaInter = oRem.sIdGrupoModeloPres; //row.S("Matricula");
                                oInt.dTarifaNalInter = drIs[0]["TarifaNal"].S().D();
                                oInt.dTarifaIntInter = drIs[0]["TarifaInt"].S().D();
                                oInt.dGaolnesInter = drIs[0]["Galones"].S().D();
                                oInt.dCostoDirNalInter = drIs[0]["CostoDirectoNal"].S().D();
                                oInt.dCostoDirIntInter = drIs[0]["CostoDirectoInt"].S().D();

                                oRem.oLstIntercambios.Add(oInt);
                            }

                            oRem.bAplicoIntercambio = true;

                        }
                    }
                    else
                    {
                        oRem.oErr.bExisteError = true;
                        if (oRem.oErr.sMsjError == string.Empty)
                            oRem.oErr.sMsjError = "El Intercambio con el grupo de modelo: " + oRem.sIdGrupoModeloPres + " no existe, favor de verificar.";
                        else
                            oRem.oErr.sMsjError += "\n" + "El Intercambio con el grupo de modelo: " + oRem.sIdGrupoModeloPres + " no existe, favor de verificar.";
                    }
                }

                #endregion

                // Aplica Gira
                #region APLICA GIRA
                Contrato_GirasFechasPico oGira = new DBContrato().DBGetGiras(iIdContrato);

                if (dtTramos.Rows.Count > 1)
                {
                    if (oGira.bAplicaGiraEspera || oGira.bAplicaGiraHora)
                    {
                        bool banGira = false;
                        foreach (DataRow row in dtTramos.Rows)
                        {
                            if (row.S("CantPax").I() > 0)
                            {
                                banGira = true;
                            }
                            else
                            {
                                banGira = false;
                                oRem.bAplicaGiraEspera = false;
                                oRem.bAplicaGiraHorario = false;
                                break;
                            }
                        }

                        if (banGira)
                        {
                            string[] sTramos = oRem.sRuta.Split('-');

                            if (DeterminaSaleBaseYRegresaBase(oRem.dtBases, sTramos[0], sTramos[sTramos.Length - 1]))
                            {
                                DateTime dtInicio = dtTramos.Rows[0]["FechaSalida"].S().Dt();
                                DateTime dtFin = dtTramos.Rows[dtTramos.Rows.Count - 1]["FechaLlegada"].S().Dt();

                                if (dtInicio.Date == dtFin.Date)
                                {
                                    // ¿Aplica Gira Espera?
                                    if (oGira.bAplicaGiraEspera)
                                    {
                                        // Tiempo total de espera no excede del doble o triple del tiempo de vuelo.
                                        float fTotalEspera = 0;
                                        string sTotalEspera = ObtieneTotalTiempo(dtTramos, "TiempoEspera", ref fTotalEspera);

                                        float fTotalVuelo = 0;
                                        string sTotalVuelo = ObtieneTotalTiempo(dtTramos, "TiempoCobrar", ref fTotalVuelo);

                                        fTotalVuelo = (fTotalVuelo * oGira.iNumeroVeces);

                                        if (fTotalEspera <= fTotalVuelo)
                                        {
                                            oRem.bAplicaGiraEspera = true;
                                        }
                                    }

                                    // ¿Aplica Gira de Horario?
                                    if (oGira.bAplicaGiraHora)
                                    {
                                        string[] sHorasInicio = oGira.sHoraInicio.Split(':');
                                        string[] sHorasFin = oGira.sHoraFin.Split(':');

                                        // Vuelo se debe realizar dentro del horario establecido.
                                        if (dtInicio.Hour >= sHorasInicio[0].S().I())
                                        {
                                            if (dtInicio.Hour == sHorasInicio[0].S().I())
                                            {
                                                if (dtInicio.Minute >= sHorasInicio[1].S().I())
                                                {
                                                    if (dtFin.Hour <= sHorasFin[0].S().I())
                                                    {
                                                        if (dtFin.Hour == sHorasFin[0].S().I())
                                                        {
                                                            if (dtFin.Minute <= sHorasFin[1].S().I())
                                                            {
                                                                oRem.bAplicaGiraHorario = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            oRem.bAplicaGiraHorario = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (dtInicio.Minute >= sHorasInicio[1].S().I())
                                                {
                                                    if (dtFin.Hour <= sHorasFin[0].S().I())
                                                    {
                                                        if (dtFin.Hour == sHorasFin[0].S().I())
                                                        {
                                                            if (dtFin.Minute <= sHorasFin[1].S().I())
                                                            {
                                                                oRem.bAplicaGiraHorario = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            oRem.bAplicaGiraHorario = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (oRem.bAplicaGiraEspera)
                        {
                            foreach (DataRow row in dtTramos.Rows)
                            {
                                if (row.S("SeCobra") == "1")
                                {
                                    float dTiempoCobrar = ConvierteTiempoaDecimal(row.S("TiempoCobrar"));

                                    double dTotal = dTiempoCobrar.S().Db() * (1 - oGira.dPorcentajeDescuento).S().Db();

                                    oRem.dFactorGiraEsperaF = (1 - oGira.dPorcentajeDescuento).S().D();

                                    row["TiempoCobrar"] = ConvierteDoubleATiempo(dTotal);
                                }
                            }
                        }

                        if (oRem.bAplicaGiraHorario)
                        {
                            foreach (DataRow row in dtTramos.Rows)
                            {
                                if (row.S("SeCobra") == "1")
                                {
                                    float dTiempoCobrar = ConvierteTiempoaDecimal(row.S("TiempoCobrar"));

                                    double dTotal = dTiempoCobrar.S().Db() * (1 - oGira.dPorcentajeDescuento).S().Db();

                                    oRem.dFactorGiraHorarioF = (1 - oGira.dPorcentajeDescuento).S().D();

                                    row["TiempoCobrar"] = ConvierteDoubleATiempo(dTotal);
                                }
                            }
                        }
                    }
                }
                #endregion

                // Factor de Fecha Pico
                #region FACTOR DE FECHA PICO
                if (oGira.bAplicaFactorFechaPico)
                {
                    DataTable dtFechasPico = new DBFechaPico().DBSearchObj("@Fecha", DBNull.Value, "@estatus", 1);
                    foreach (DataRow row in dtTramos.Rows)
                    {
                        //if (row.S("SeCobra") == "1")
                        //{ 
                        //validar por pierna
                        DateTime dtPierna = row["FechaSalida"].S().Dt();
                        int iAnio = dtPierna.Year;
                        int iMes = dtPierna.Month;
                        int iDia = dtPierna.Day;

                        DataTable dtFecha = new DBRemision().DBGetObtieneFechasPicoPorAnio(iAnio, iMes);

                        foreach (DataRow dr in dtFecha.Rows)
                        {
                            DateTime dtFP = dr["Fecha"].S().Dt();
                            int iAnioFP = dtFP.Year;
                            int iMesFP = dtFP.Month;
                            int iDiaFP = dtFP.Day;

                            if (iAnio == iAnioFP && iMes == iMesFP && iDia == iDiaFP)
                            {
                                float dTiempo = ConvierteTiempoaDecimal(row["TiempoCobrar"].S());
                                double dTotalT = dTiempo.S().Db() * oGira.dFactorFechaPico.S().Db();

                                row["TiempoCobrar"] = ConvierteDoubleATiempo(dTotalT.S().Db());

                                oRem.dFactorFechaPicoF = oGira.dFactorFechaPico;
                                oRem.bAplicaFactorFechaPico = true;
                            }
                        }
                        //}
                    }
                }

                #endregion


                return dtTramos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable CalculaCostosRemisionPresupuestos(int iCobroTiempo, DataTable dtTramos, DatosRemision oRem)
        {
            try
            {

                // SE PREPARAN LAS TABLAS PARA LOS TIEMPOS DE VUELOS
                DataTable dtTramosNal = new DataTable();
                DataTable dtTramosInt = new DataTable();


                dtTramosInt = dtTramos.Clone();
                dtTramosNal = dtTramos.Clone();

                foreach (DataRow dr in dtTramos.Rows)
                {
                    if (dr["SeCobra"].S().I() == 1)
                    {
                        DataTable dt = new DBRemision().DBGetConsultaAeropuertoId(dr.S("IdOrigen").I());
                        switch (dt.Rows[0]["TipoDestino"].S().ToUpper())
                        {
                            case "N":
                            case "F":
                                dtTramosNal.ImportRow(dr);
                                break;
                            case "E":
                                dtTramosInt.ImportRow(dr);
                                break;
                        }
                    }
                }


                // SE PREPARAN LAS TABLAS PARA LOS TIEMPOS DE ESPERA Y PARA LAS PERNOCTAS
                DataTable dtTramosNalEP = new DataTable();
                DataTable dtTramosIntEP = new DataTable();

                dtTramosIntEP = dtTramos.Clone();
                dtTramosNalEP = dtTramos.Clone();

                foreach (DataRow dr in dtTramos.Rows)
                {
                    if (dr["SeCobra"].S().I() == 1)
                    {
                        DataTable dt = new DBRemision().DBGetConsultaAeropuertoId(dr.S("IdDestino").I());
                        switch (dt.Rows[0]["TipoDestino"].S().ToUpper())
                        {
                            case "N":
                            case "F":
                                dtTramosNalEP.ImportRow(dr);
                                break;
                            case "E":
                                dtTramosIntEP.ImportRow(dr);
                                break;
                        }
                    }
                }


                // SE PREPARA LA TABLA CON LOS CONCEPTOS EN COBROS
                DataTable dtConceptos = new DBRemision().DBGetConceptosRemision;
                DataTable dtFinal = new DataTable();
                dtFinal.Columns.Add("IdConcepto");
                dtFinal.Columns.Add("Concepto");
                dtFinal.Columns.Add("Cantidad");
                dtFinal.Columns.Add("CostoDirecto");
                dtFinal.Columns.Add("CostoComb");
                dtFinal.Columns.Add("CombustibleAumento");
                dtFinal.Columns.Add("TarifaDlls", typeof(decimal));
                dtFinal.Columns.Add("Importe", typeof(decimal));
                dtFinal.Columns.Add("HrDescontar");

                float fTiempoVueloN = 0;
                float fTiempoVueloI = 0;

                float fTiempoEsperaGlobal = 0;
                float fTiempoEsperaN = 0;
                float fTiempoEsperaI = 0;

                int iNoPernoctasGlobal = 0;
                float fNoPernoctasN = 0;
                float fNoPernoctasI = 0;

                string sAirPortO = new DBRemision().DBGetConsultaAeropuertoId(dtTramos.Rows[0].S("IdOrigen").I()).Rows[0].S("AeropuertoICAO");
                DataTable dtComb = new DBRemision().DBGetConsultaTarifasVuelo(oRem.iIdContrato, oRem.lIdRemision, sAirPortO);
                // Agregar consulta de Tarifas para la Remisión. y asignar en casos 1 y 2

                if (dtComb.Rows.Count > 0)
                {
                    oRem.dTarifaVueloNal = dtComb.Rows[0]["TarifaVueloNal"].S().D();
                    oRem.dTarifaVueloInt = dtComb.Rows[0]["TarifaVueloInt"].S().D();
                }


                float fTiempoEsperaReal = 0;
                float fTotalTiempoEspera = 0;
                float fFactorNal = 0;
                float fFactorInt = 0;

                decimal dCombN = 0;
                decimal dCombI = 0;

                foreach (DataRow dr in dtConceptos.Rows)
                {
                    DataRow row = dtFinal.NewRow();
                    row["IdConcepto"] = dr.S("IdConcepto");
                    row["Concepto"] = dr.S("Descripcion");

                    string sCantidad = string.Empty;
                    float fTiempoT = 0;
                    float fTV = 0;
                    decimal dTarifaTEN = 0;
                    decimal dTarifaP = 0;

                    string dTarifaDlls = string.Empty;


                    switch (dr.S("IdConcepto"))
                    {
                        case "1":
                            #region TIEMPO VUELO NACIONAL

                            decimal dTarifaCostoDirN = 0;
                            decimal dCostoCombuN = 0;
                            decimal dCostoDirectoN = 0;

                            if (oRem.oLstIntercambios.Count > 0)
                            {
                                if (oRem.oLstIntercambios[0].dTarifaNalInter > 0)
                                {
                                    dTarifaCostoDirN = oRem.oLstIntercambios[0].dTarifaNalInter;
                                }
                                else
                                {
                                    decimal dCombusN = dtComb.Rows[0]["CombusN"].S().D();
                                    dCombN = dCombusN;
                                    dCombusN = (dCombusN * oRem.oLstIntercambios[0].dGaolnesInter);

                                    dCostoCombuN = dCombusN;

                                    if (oRem.oLstIntercambios[0].dCostoDirNalInter > 0)
                                    {
                                        dCostoDirectoN = oRem.oLstIntercambios[0].dCostoDirNalInter;
                                        dTarifaCostoDirN = oRem.oLstIntercambios[0].dCostoDirNalInter + dCombusN;
                                    }
                                }
                            }
                            else
                            {
                                dTarifaCostoDirN = oRem.dTarifaVueloNal;

                                dCombN = dtComb.Rows[0]["CombusN"].S().D();
                                dCostoCombuN = dtComb.Rows[0]["CombustibleNal"].S().D();
                                dCostoDirectoN = dtComb.Rows[0]["CostoDirectoNalV"].S().D();
                            }

                            oRem.dTarifaVueloNal = dTarifaCostoDirN;

                            sCantidad = ObtieneTotalTiempo(dtTramosNal, "TiempoCobrar", ref fTiempoT);
                            fTiempoVueloN = fTiempoT;

                            row["Cantidad"] = sCantidad;

                            row["CostoDirecto"] = dCostoDirectoN;

                            row["CostoComb"] = dCostoCombuN;

                            row["CombustibleAumento"] = dCombN;

                            row["TarifaDlls"] = Math.Round(dTarifaCostoDirN, 2);

                            row["Importe"] = fTiempoT.S().D() * dTarifaCostoDirN;

                            row["HrDescontar"] = sCantidad;

                            #endregion
                            break;
                        case "2":
                            #region TIEMPO VUELO INTERNACIONAL

                            decimal dTarifaCostoDirI = 0;
                            decimal dCostoCombuI = 0;
                            decimal dCostoDirectoI = 0;

                            if (oRem.oLstIntercambios.Count > 0)
                            {
                                if (oRem.oLstIntercambios[0].dTarifaIntInter > 0)
                                    dTarifaCostoDirI = oRem.oLstIntercambios[0].dTarifaIntInter;
                                else
                                {
                                    decimal CombusI = dtComb.Rows[0]["CombusI"].S().D();
                                    dCombI = CombusI;
                                    CombusI = (CombusI * oRem.oLstIntercambios[0].dGaolnesInter);

                                    dCostoCombuI = CombusI;
                                    dCostoDirectoI = oRem.oLstIntercambios[0].dCostoDirNalInter;

                                    if (oRem.oLstIntercambios[0].dCostoDirNalInter > 0)
                                    {
                                        dTarifaCostoDirI = oRem.oLstIntercambios[0].dCostoDirNalInter + CombusI;
                                    }
                                }
                            }
                            else
                            {
                                dTarifaCostoDirI = oRem.dTarifaVueloInt;

                                dCombI = dtComb.Rows[0]["CombusI"].S().D();
                                dCostoCombuI = dtComb.Rows[0]["CombustibleInt"].S().D();
                                dCostoDirectoI = dtComb.Rows[0]["CostoDirectoIntV"].S().D();
                            }

                            oRem.dTarifaVueloInt = dTarifaCostoDirI;

                            sCantidad = ObtieneTotalTiempo(dtTramosInt, "TiempoCobrar", ref fTiempoT);
                            fTiempoVueloI = fTiempoT;

                            row["Cantidad"] = sCantidad;

                            row["CostoDirecto"] = dCostoDirectoI;

                            row["CostoComb"] = dCostoCombuI;

                            row["CombustibleAumento"] = dCombI;

                            row["TarifaDlls"] = Math.Round(dTarifaCostoDirI, 2);

                            row["Importe"] = fTiempoT.S().D() * dTarifaCostoDirI;

                            row["HrDescontar"] = sCantidad;
                            #endregion
                            break;
                        case "3":
                            #region TIEMPO DE ESPERA NACIONAL

                            float fEsperaN = 0;
                            float fEsperaI = 0;
                            float fTotalTiempoCobrar = 0;


                            // SE SUMA EL TOTAL DEL TIEMPO DE ESPERA EN NACIONALES E INTERNACIONALES
                            ObtieneTotalTiempo(dtTramos, "TiempoEspera", ref fTotalTiempoEspera);

                            if (fTotalTiempoEspera > 0)
                            {
                                ObtieneTotalTiempo(dtTramos, "TiempoVuelo", ref fTotalTiempoCobrar);

                                //sCantidad = ObtieneTotalTiempo(dtTramosNalEP, "TiempoEspera", ref fEsperaN);
                                sCantidad = ObtieneTotalTiempo(dtTramosIntEP, "TiempoEspera", ref fEsperaI);

                                fFactorInt = fEsperaI / fTotalTiempoEspera;
                                fFactorNal = (1 - fFactorInt);


                                if (oRem.bAplicaEsperaLibre)
                                {
                                    if (oRem.dHorasPorVuelo > 0)
                                    {
                                        fTV = float.Parse(oRem.dHorasPorVuelo.S());
                                    }
                                    else if (oRem.dFactorHrVuelo > 0)
                                        fTV = (fTotalTiempoCobrar * float.Parse(oRem.dFactorHrVuelo.S()));

                                    sCantidad = RestaTiempoString(fTotalTiempoEspera, fTV);

                                    fTiempoEsperaReal = ConvierteTiempoaDecimal(sCantidad);
                                }
                                else
                                {
                                    fTiempoEsperaReal = fTotalTiempoEspera;
                                }


                                // SE DETERMINA EL NUMERO DE PERNOCTAS DE TODO EL TIEMPO DE ESPERA
                                for (int i = 0; i < 100; i++)
                                {
                                    if (fTiempoEsperaReal >= oRem.iHorasPernocta)
                                    {
                                        iNoPernoctasGlobal++;
                                        fTiempoEsperaReal = fTiempoEsperaReal - 24;
                                    }
                                    else
                                    {
                                        if (fTiempoEsperaReal > 0 && fTiempoEsperaReal < oRem.iHorasPernocta)
                                        {
                                            fTiempoEsperaGlobal = fTiempoEsperaReal;
                                            break;
                                        }
                                        else
                                        {
                                            fTiempoEsperaReal = 0;
                                            fTiempoEsperaGlobal = fTiempoEsperaReal;
                                            break;
                                        }
                                    }
                                }


                                fTiempoEsperaN = fTiempoEsperaGlobal * fFactorNal;
                                sCantidad = ConvierteDecimalATiempo(fTiempoEsperaN.S().D());

                                if (oRem.bSeCobreEspera)
                                {
                                    sCantidad = AgregaFactoresEsperaPernoctas(sCantidad, 1, oRem);
                                    fTiempoEsperaN = ConvierteTiempoaDecimal(sCantidad);

                                    row["Cantidad"] = sCantidad;

                                    if (oRem.dTarifaNalEspera > 0)
                                        dTarifaTEN = oRem.dTarifaNalEspera;
                                    else
                                    {
                                        dTarifaTEN = (oRem.dTarifaVueloNal * oRem.dPorTarifaNalEspera);
                                    }
                                }
                                else
                                {
                                    row["Cantidad"] = "00:00";
                                    dTarifaTEN = 0;
                                }
                            }
                            else
                            {
                                row["Cantidad"] = "00:00";
                                dTarifaTEN = 0;

                                if (oRem.dTarifaNalEspera > 0)
                                    dTarifaTEN = oRem.dTarifaNalEspera;
                                else
                                {
                                    dTarifaTEN = (oRem.dTarifaVueloNal * oRem.dPorTarifaNalEspera);
                                }

                                fTiempoEsperaN = 0;
                            }

                            row["TarifaDlls"] = Math.Round(dTarifaTEN, 2);

                            row["Importe"] = fTiempoEsperaN.S().D() * dTarifaTEN;

                            if (oRem.bSeDescuentaEsperaNal)
                            {
                                decimal dHr = (fTiempoEsperaN.S().D() * oRem.dFactorEHrVueloNal);
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }
                            else
                                row["HrDescontar"] = "00:00";

                            #endregion
                            break;
                        case "4":
                            #region TIEMPO DE ESPERA INTERNACIONAL

                            if (fTotalTiempoEspera > 0)
                            {
                                fTiempoEsperaI = fTiempoEsperaGlobal * fFactorInt;
                                sCantidad = ConvierteDecimalATiempo(fTiempoEsperaI.S().D());

                                if (oRem.bSeCobreEspera)
                                {
                                    sCantidad = AgregaFactoresEsperaPernoctas(sCantidad, 1, oRem);
                                    fTiempoEsperaI = ConvierteTiempoaDecimal(sCantidad);

                                    row["Cantidad"] = sCantidad;

                                    if (oRem.dTarifaNalEspera > 0)
                                        dTarifaTEN = oRem.dTarifaIntEspera;
                                    else
                                    {
                                        dTarifaTEN = (oRem.dTarifaVueloInt * oRem.dPorTarifaNalEspera);
                                    }
                                }
                                else
                                {
                                    row["Cantidad"] = "00:00";
                                    dTarifaTEN = 0;
                                }

                            }
                            else
                            {
                                row["Cantidad"] = "00:00";
                                dTarifaTEN = 0;

                                if (oRem.dTarifaNalEspera > 0)
                                    dTarifaTEN = oRem.dTarifaIntEspera;
                                else
                                {
                                    dTarifaTEN = (oRem.dTarifaVueloInt * oRem.dPorTarifaNalEspera);
                                }

                                fTiempoEsperaI = 0;
                            }

                            row["TarifaDlls"] = Math.Round(dTarifaTEN, 2);

                            row["Importe"] = fTiempoEsperaI.S().D() * dTarifaTEN;

                            if (oRem.bSeDescuentaEsperaInt)
                            {
                                decimal dHr = (fTiempoEsperaI.S().D() * oRem.dFactorEHrVueloInt);
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }
                            else
                                row["HrDescontar"] = "00:00";

                            #endregion
                            break;
                        case "5":
                            #region PERNOCTA NACIONAL

                            if (fTotalTiempoEspera > 0)
                            {
                                fNoPernoctasI = float.Parse(Math.Round(iNoPernoctasGlobal * fFactorInt, 0).S());

                                if ((iNoPernoctasGlobal - fNoPernoctasI) > 0)
                                {
                                    fNoPernoctasN = iNoPernoctasGlobal - fNoPernoctasI;
                                }
                                else
                                    fNoPernoctasN = 0;


                                if (oRem.bSeCobraPernoctas)
                                {
                                    string sNoPernoctas = AgregaFactoresEsperaPernoctas(fNoPernoctasN.S(), 2, oRem);
                                    fNoPernoctasN = float.Parse(sNoPernoctas);

                                    row["Cantidad"] = fNoPernoctasN.S();

                                    if (oRem.dTarifaDolaresNal > 0)
                                        dTarifaP = oRem.dTarifaDolaresNal;
                                    else
                                    {
                                        dTarifaP = (oRem.dTarifaVueloNal * oRem.dPorTarifaVueloNal);
                                    }
                                }
                                else
                                {
                                    row["Cantidad"] = "0";
                                    dTarifaP = 0;
                                }
                            }
                            else
                            {
                                row["Cantidad"] = "0";
                                dTarifaP = 0;

                                if (oRem.dTarifaDolaresNal > 0)
                                    dTarifaP = oRem.dTarifaDolaresNal;
                                else
                                {
                                    dTarifaP = (oRem.dTarifaVueloNal * oRem.dPorTarifaVueloNal);
                                }

                                fNoPernoctasN = 0;
                            }

                            row["TarifaDlls"] = Math.Round(dTarifaP, 2);

                            row["Importe"] = decimal.Parse(fNoPernoctasN.S()) * dTarifaP;

                            if (oRem.bSeDescuentanPerNal)
                            {
                                decimal dHr = (decimal.Parse(fNoPernoctasN.S()) * oRem.dFactorConvHrVueloNal);
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }
                            else
                                row["HrDescontar"] = "00:00";
                            #endregion
                            break;
                        case "6":
                            #region PERNOCTA INTERNACIONAL

                            if (fTotalTiempoEspera > 0)
                            {
                                //fNoPernoctasI = iNoPernoctasGlobal * fFactorInt;

                                if (oRem.bSeCobraPernoctas)
                                {
                                    string sNoPernoctas = AgregaFactoresEsperaPernoctas(fNoPernoctasI.S(), 2, oRem);
                                    fNoPernoctasI = float.Parse(sNoPernoctas);

                                    row["Cantidad"] = fNoPernoctasI.S();

                                    if (oRem.dTarifaDolaresInt > 0)
                                        dTarifaP = oRem.dTarifaDolaresInt;
                                    else
                                    {
                                        dTarifaP = (oRem.dTarifaVueloInt * oRem.dPorTarifaVueloInt);
                                    }
                                }
                                else
                                {
                                    row["Cantidad"] = "0";
                                    dTarifaP = 0;
                                }
                            }
                            else
                            {
                                row["Cantidad"] = "0";
                                dTarifaP = 0;

                                if (oRem.dTarifaDolaresInt > 0)
                                    dTarifaP = oRem.dTarifaDolaresInt;
                                else
                                {
                                    dTarifaP = (oRem.dTarifaVueloInt * oRem.dPorTarifaVueloInt);
                                }

                                fNoPernoctasI = 0;
                            }

                            row["TarifaDlls"] = Math.Round(dTarifaP, 2);

                            row["Importe"] = decimal.Parse(fNoPernoctasI.S()) * dTarifaP;

                            if (oRem.bSeDescuentanPerInt)
                            {
                                decimal dHr = (decimal.Parse(fNoPernoctasI.S()) * oRem.dFactorConvHrVueloInt);
                                row["HrDescontar"] = ConvierteDecimalATiempo(dHr);
                            }
                            else
                                row["HrDescontar"] = "00:00";
                            #endregion
                            break;
                    }

                    dtFinal.Rows.Add(row);
                }

                return dtFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable CalculoServiciosCargoPresupuestos(int iIdContrato, DataTable dtTramos, int iIdGrupoModelo)
        {
            try
            {
                DataTable dtSCC = new DBRemision().DBGetServiciosCargoContrato(iIdContrato, 0);

                dtSCC.Columns["Importe"].ReadOnly = false;

                bool bPorTramo = false;
                bool bPorPax = false;

                DataSet dsE = new DataSet();
                dsE.Tables.Add(dtTramos.Copy());
                string sXML = dsE.GetXml();

                DataTable dtCobros = new DBPresupuesto().GetConsultaCobrosServiciosPresupuestos(sXML);

                DataTable dtA = CalculaAterrizajes(dtTramos);

                foreach (DataRow row in dtSCC.Rows)
                {
                    decimal dImp = 0;
                    decimal dSum = 0;

                    bPorTramo = row["PorPierna"].S().B();
                    bPorPax = row["PorPasajero"].S().B();

                    switch (row.S("IdServicioConCargo"))
                    {
                        case "1": // DSM
                            #region DSM
                            /*
                            El Aeropuerto Origen es TipoDestino Nacional o Fronterizo y TipoAeropuerto
                            Internacional y el Aeropuerto Destino es TipoDestino Extranjero del país EUA 
                            */
                            //DataTable dtS = new DBRemision().DBGetImportesTUA(iIdRemision);

                            foreach (DataRow drS in dtCobros.Rows)
                            {
                                if (((drS.S("TipoDestinoO") == "N" || drS.S("TipoDestinoO") == "F") && drS.S("TipoAeropuertoO") == "2") && (drS.S("TipoDestinoD") == "E"))
                                {
                                    dImp = drS.S("ImporteDSM").D();
                                    if (bPorPax)
                                    {
                                        dSum += (dImp * drS.S("Pax").I());
                                    }
                                    else if (bPorTramo)
                                    {
                                        dSum += dImp;
                                    }
                                }
                            }

                            row["Importe"] = dSum;

                            #endregion
                            break;
                        case "2": // APHIS
                            #region APHIS
                            /*
                             * Son Dolares y se cobran a la entrada a Estados Unidos desde cualquier origen, el cobro es por pasajero.
                            */

                            foreach (DataRow drA in dtCobros.Rows)
                            {
                                if (drA.S("ClavePaisO") != "US" && drA.S("ClavePaisD") == "US")
                                {
                                    dImp = drA.S("ImporteAPHIS").D();

                                    if (bPorPax)
                                    {
                                        dSum += (dImp * drA.S("Pax").I());
                                    }
                                }
                            }

                            row["Importe"] = dSum;

                            #endregion
                            break;
                        case "4": // TUA NACIONAL
                            #region TUA NACIONAL
                            /*
                                1. El Aeropuerto Origen es Tipo de Destino Nacional o Fronterizo y Tipo de
                                    Aeropuerto Internacional y el Aeropuerto Destino es Tipo de Destino
                                    Nacional
                                2. El Aeropuerto Origen es Tipo de Destino Nacional o Fronterizo y Tipo de
                                    Aeropuerto Internacional y el Aeropuerto Destino es Tipo de Destino
                                    Fronterizo.
                            */

                            foreach (DataRow drN in dtCobros.Rows)
                            {
                                if (((drN.S("TipoDestinoO") == "N" || drN.S("TipoDestinoO") == "F") && drN.S("TipoAeropuertoO") == "2") && drN.S("TipoDestinoD") == "N")
                                {
                                    dImp = drN.S("NacionalO").D();

                                    if (bPorPax)
                                    {
                                        dSum += (dImp * drN.S("Pax").I());
                                    }
                                    else if (bPorTramo)
                                    {
                                        dSum += dImp;
                                    }
                                }
                                else if (((drN.S("TipoDestinoO") == "N" || drN.S("TipoDestinoO") == "F") && drN.S("TipoAeropuertoO") == "2") && drN.S("TipoDestinoD") == "F")
                                {
                                    dImp = drN.S("NacionalO").D();

                                    if (bPorPax)
                                    {
                                        dSum += (dImp * drN.S("Pax").I());
                                    }
                                    else if (bPorTramo)
                                    {
                                        dSum += dImp;
                                    }
                                }
                            }
                            row["Importe"] = dSum;

                            #endregion
                            break;
                        case "5": // TUA INTERNACIONAL
                            #region TUA INTERNACIONAL
                            /*
                                El Aeropuerto Origen es Tipo de Destino Nacional o Fronterizo y Tipo de
                                Aeropuerto Internacional y el Aeropuerto Destino es Tipo de Destino
                                Extranjero.  
                            */

                            foreach (DataRow drI in dtCobros.Rows)
                            {
                                if (((drI.S("TipoDestinoO") == "N" || drI.S("TipoDestinoO") == "F") && drI.S("TipoAeropuertoO") == "2") && drI.S("TipoDestinoD") == "E")
                                {
                                    dImp = drI.S("InternacionalO").D();

                                    if (bPorPax)
                                    {
                                        dSum += (dImp * drI["Pax"].S().I());
                                    }
                                    else if (bPorTramo)
                                    {
                                        dSum += dImp;
                                    }
                                }
                            }

                            row["Importe"] = dSum;

                            #endregion
                            break;
                        case "7": // ATERRIZAJE NACIONAL
                            #region ATERRIZAJE NACIONAL
                            row["Importe"] = dtA.Rows[0]["TotalCobrarMXN"].S().D();
                            #endregion
                            break;
                        case "19": // ATERRIZAJE INTERNACIONAL
                            #region ATERRIZAJE INTERNACIONAL
                            row["Importe"] = dtA.Rows[0]["TotalCobrarDlls"].S().D();
                            #endregion
                            break;
                        case "10": // SENEAM
                            #region SENEAM
                            /*
                             * Cuando un Tramo tiene un origen o destino Nacional o Fronterizo
                             * Se cobran $1,000 MN en el primer tramo y $500 MN en tramos subsecuentes.
                            */

                            decimal dTotalSENEAM = 0;
                            object[] oFil = new object[]{
                                                            "@GrupoModeloId", iIdGrupoModelo,
                                                            "@Descripcion", string.Empty,
                                                            "@ConsumoGalones", 0,
                                                            "@Tarifa", 0,
                                                            "@estatus", -1
                                                        };

                            DataTable dtSEN = new DBGrupoModelo().DBSearchObj(oFil);
                            if (dtSEN.Rows.Count > 0)
                                dTotalSENEAM = dtSEN.Rows[0]["Tarifa"].S().D() * dtTramos.Rows.Count;

                            row["Importe"] = dTotalSENEAM;

                            #endregion
                            break;
                        case "11": // MIGRACION
                            #region MIGRACION
                            /*
                                Migración. Es el cobro por servicios migratorios que se cobra en aeropuertos con
                                TipoDestino Nacional cuando el origen de la pierna o el destino es de TipoDestino Extranjero.
                                Para llegada o para salida se cobra $3,239 MN
                            */

                            foreach (DataRow drI in dtCobros.Rows)
                            {
                                if (((drI.S("TipoDestinoO") == "N" || drI.S("TipoDestinoO") == "F") && drI.S("TipoDestinoD") == "E") ||
                                        ((drI.S("TipoDestinoD") == "N" || drI.S("TipoDestinoD") == "F") && drI.S("TipoDestinoO") == "E"))
                                {
                                    dImp = drI.S("ImporteMigracion").D();
                                    if (bPorPax)
                                    {
                                        dSum += (dImp * drI["Pax"].S().I());
                                    }
                                    else if (bPorTramo)
                                    {
                                        dSum += dImp;
                                    }
                                }
                            }

                            row["Importe"] = dSum;

                            break;
                            #endregion
                        default:   // OTROS
                            row["Importe"] = 0.0000;
                            break;
                    }
                }

                return dtSCC;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal CalculaIVAServiciosPresupuestos(DataTable dtTramos)
        {
            try
            {
                bool IsVueloInt = false;

                foreach (DataRow drT in dtTramos.Rows)
                {
                    DataTable dtO = new DBRemision().DBGetConsultaAeropuertoId(drT.S("IdOrigen").I());

                    switch (dtO.Rows[0]["TipoDestino"].S().ToUpper())
                    {
                        case "F":
                        case "E":
                            IsVueloInt = true;
                            break;
                    }

                    DataTable dtD = new DBRemision().DBGetConsultaAeropuertoId(drT.S("IdOrigen").I());

                    switch (dtD.Rows[0]["TipoDestino"].S().ToUpper())
                    {
                        case "F":
                        case "E":
                            IsVueloInt = true;
                            break;
                    }

                    if (IsVueloInt)
                        break;
                }

                return IsVueloInt ? Utils.ObtieneParametroPorClave("10").S().D() : Utils.ObtieneParametroPorClave("9").S().D();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ObtieneTramoPactadoPresupuestos(DatosRemision oRem, string sOrigen, string sDestino, int iIdGrupoModeloSol)
        {
            try
            {
                //1.- Primero se consulta en los del contrato
                //2.- Buscar dentro de la tabla general
                //3.- Promedio de los ultimos 10 vuelos y si no existe... solicitarlo

                string sTiempo = string.Empty;

                if (oRem.dtTramosPactadosEsp.Rows.Count > 0)
                {
                    foreach (DataRow row in oRem.dtTramosPactadosEsp.Rows)
                    {
                        if ((row.S("OrigenICAO") == sOrigen || row.S("OrigenIATA") == sOrigen) && (row.S("DestinoICAO") == sDestino || row.S("DestinoIATA") == sDestino))
                        {
                            sTiempo = row.S("TiempoVuelo");
                            break;
                        }
                    }
                }

                oRem.dtTramosPactadosGen = new DBRemision().DBGetObtieneTramosPactadosGeneralesGrupoModelo(iIdGrupoModeloSol);
                if (sTiempo == string.Empty && oRem.dtTramosPactadosGen.Rows.Count > 0)
                {
                    foreach (DataRow row in oRem.dtTramosPactadosGen.Rows)
                    {
                        if ((row.S("AeropuertoICAO") == sOrigen && row.S("AeropuertoICAOD") == sDestino) || (row.S("AeropuertoIATA") == sOrigen && row.S("AeropuertoIATAD") == sDestino))
                        {
                            sTiempo = row.S("TiempoDeVuelo");
                            break;
                        }
                    }
                }

                if (sTiempo == string.Empty)
                {
                    // Usar Tiempo Real de la Bitácora
                    sTiempo = "00:00:00";
                }

                return sTiempo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DataTable CalculaAterrizajes(DataTable dtTramos)
        {
            try
            {
                DataTable dtA = new DataTable();
                dtA.Columns.Add("TotalCobrarMXN", typeof(decimal));
                dtA.Columns.Add("TotalCobrarDlls", typeof(decimal));
                DataRow dr = dtA.NewRow();

                decimal dTotalNal = 0;
                decimal dTotalInt = 0;

                foreach (DataRow row in dtTramos.Rows)
                {
                    int iTipoTarifa = 0;
                    decimal dMontoTramo = 0;
                    decimal dMontoTramoDlls = 0;
                    DataTable dtO = new DBRemision().DBGetConsultaAeropuertoId(row.S("IdOrigen").I());
                    DataTable dtD = new DBRemision().DBGetConsultaAeropuertoId(row.S("IdDestino").I());

                    if ((dtD.Rows[0].S("AeropuertoICAO") != "MMTO") && (dtD.Rows[0].S("AeropuertoICAO").Substring(0, 1) == "@" && dtD.Rows[0].S("AeropuertoIATA").Substring(0, 1) == "@"))
                    {
                        iTipoTarifa = 1;
                    }
                    else if (dtD.Rows[0].S("AeropuertoICAO") == "MMTO")
                    {
                        iTipoTarifa = 2;
                    }
                    else if (
                        (dtD.Rows[0].S("AeropuertoICAO") != "MMTO") && 
                        (dtD.Rows[0].S("AeropuertoICAO").Substring(0, 1) != "@" && 
                        dtD.Rows[0].S("AeropuertoIATA") != string.Empty ? dtD.Rows[0].S("AeropuertoIATA").Substring(0, 1) != "@" : dtD.Rows[0].S("AeropuertoICAO").Substring(0, 1) != "@")
                        && (
                        (dtO.Rows[0].S("TipoDestino") == "N" || dtO.Rows[0].S("TipoDestino") == "F") && 
                        (dtD.Rows[0].S("TipoDestino") == "N" || dtD.Rows[0].S("TipoDestino") == "F")))
                    {
                        iTipoTarifa = 3;
                    }
                    else if ((dtD.Rows[0].S("AeropuertoICAO") != "MMTO") && 
                        (dtD.Rows[0].S("AeropuertoICAO").Substring(0, 1) != "@" &&
                        dtD.Rows[0].S("AeropuertoIATA") != string.Empty ? dtD.Rows[0].S("AeropuertoIATA").Substring(0, 1) != "@" : dtD.Rows[0].S("AeropuertoICAO").Substring(0, 1) != "@")
                        && (dtO.Rows[0].S("TipoDestino") == "N") && (dtD.Rows[0].S("TipoDestino") == "N" || dtD.Rows[0].S("TipoDestino") == "F"))
                    {
                        iTipoTarifa = 4;
                    }
                    else if (dtD.Rows[0].S("TipoDestino") == "E")
                    {
                        iTipoTarifa = 5;
                    }
                    else
                    {
                        dMontoTramo = 0;
                    }

                    switch (iTipoTarifa)
                    {
                        case 1:
                            dMontoTramo = dtD.Rows[0].S("AeropuertoHelipuertoTarifa").D();
                            break;
                        case 2:
                            dMontoTramo = ObtieneParametroPorClave("96").S().D();
                            break;
                        case 3:
                            dMontoTramo = ObtieneParametroPorClave("97").S().D();
                            break;
                        case 4:
                            dMontoTramo = ObtieneParametroPorClave("98").S().D();
                            break;
                        case 5:
                            decimal dDolares = ObtieneParametroPorClave("99").S().D();
                            dMontoTramoDlls = dDolares * GetTipoCambioDia;
                            break;
                    }


                    dTotalNal += dMontoTramo;
                    dTotalInt += dMontoTramoDlls;

                }

                dr["TotalCobrarMXN"] = dTotalNal;
                dr["TotalCobrarDlls"] = dTotalInt;
                dtA.Rows.Add(dr);

                return dtA;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DataTable AgregaFerrysVirtualesCotizacion(DataTable dtTramos, ref bool bAplica, DatosRemision oRem, Presupuesto oPres)
        {
            try
            {
                DataTable dtRes = new DataTable();
                dtRes = dtTramos.Clone();

                if (dtTramos.Rows.Count >= 2)
                {
                    for (int i = 1; i <= dtTramos.Rows.Count - 1; i++)
                    {
                        // Calcula Tiempo de Espera entre la pierna nueva y la anterior.

                        DateTime dtFechaSalida;
                        DateTime dtFechaLlegada;

                        TimeSpan ts;
                        ts = dtTramos.Rows[i]["FechaSalida"].S().Dt() - dtTramos.Rows[i - 1]["FechaLlegada"].S().Dt();

                        double dHoras = ts.TotalHours.S().Replace("-", "").S().Db();
                        double dMinutos = ts.Minutes.S().Replace("-", "").S().Db();
                        double iHoras = Math.Truncate(dHoras);
                        double iMinutos = Math.Truncate(dMinutos);

                        if (iHoras >= 24)
                        {
                            bAplica = true;

                            if (dtRes.Rows.Count == 0)
                            {
                                for (int j = 0; j < i; j++)
                                {
                                    dtRes.ImportRow(dtTramos.Rows[j]);
                                }
                            }
                            else
                            {
                                dtRes.ImportRow(dtTramos.Rows[i]);
                            }


                            #region FERRY IDA

                            // FERRY INICIAL
                            string sInicio = dtTramos.Rows[i - 1]["Destino"].S(); ;
                            string sFin = dtTramos.Rows[0]["Origen"].S();

                            DataRow drI = dtRes.NewRow();

                            //string sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, dtTramos.Rows[0]["Matricula"].S());
                            string sTiempo = ObtieneTiempoTramosPresupuestos(oRem, dtTramos.Rows[i]["IdOrigen"].S().I(), dtTramos.Rows[0]["IdOrigen"].S().I(), oPres.iIdGrupoModeloSol);

                            if (dtRes.Columns.Contains("IdTramo"))
                                drI["IdTramo"] = i + 10;

                            if (dtRes.Columns.Contains("IdPresupuesto"))
                                drI["IdPresupuesto"] = dtTramos.Rows[0]["IdPresupuesto"].S();

                            drI["IdOrigen"] = dtTramos.Rows[i - 1]["IdDestino"].S();
                            drI["IdDestino"] = dtTramos.Rows[0]["IdOrigen"].S();
                            drI["Origen"] = sInicio;
                            drI["Destino"] = sFin;
                            drI["CantPax"] = 0;


                            drI["FechaSalida"] = dtTramos.Rows[i - 1]["FechaLlegada"].Dt();
                            dtFechaSalida = dtTramos.Rows[i - 1]["FechaLlegada"].Dt();

                            string[] sTiempos = sTiempo.Split(':');

                            dtFechaLlegada = dtFechaSalida.AddHours(double.Parse(sTiempos[0]));
                            dtFechaLlegada = dtFechaSalida.AddMinutes(double.Parse(sTiempos[1]));

                            drI["FechaLlegada"] = dtFechaLlegada;

                            TimeSpan tsI;
                            tsI = dtFechaLlegada - dtFechaSalida;


                            drI["TiempoVuelo"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? tsI.S() : sTiempo;

                            dtRes.Rows[dtRes.Rows.Count - 1]["TiempoEspera"] = "00:00";

                            drI["TiempoEspera"] = "00:00";
                            drI["TiempoCobrar"] = sTiempo == "00:00" ? tsI.S() : sTiempo;
                            //drI["RealVirtual"] = "Virtual";
                            drI["SeCobra"] = 1;

                            //drI["Status"] = 1;
                            //drI["UsuarioCreacion"] = GetUser;
                            //drI["FechaCreacion"] = DateTime.Now;
                            //drI["UsuarioModificacion"] = string.Empty;
                            //drI["FechaModificacion"] = DateTime.Now;
                            //drI["IP"] = GetIPAddress();

                            dtRes.Rows.Add(drI);
                            #endregion

                            #region FERRY VUELTA
                            // Ferry Tercera posición
                            DataRow drF = dtRes.NewRow();
                            sInicio = dtTramos.Rows[0]["Origen"].S();
                            sFin = dtTramos.Rows[i]["Origen"].S();

                            //sTiempo = ObtieneTiempoFerrys(oRem, sInicio, sFin, dtTramos.Rows[0]["Matricula"].S());
                            sTiempo = ObtieneTiempoTramosPresupuestos(oRem, dtTramos.Rows[0]["IdOrigen"].S().I(), dtTramos.Rows[i]["IdOrigen"].S().I(), oPres.iIdGrupoModeloSol);

                            if (dtRes.Columns.Contains("IdTramo"))
                                drF["IdTramo"] = i + 11;

                            if (dtRes.Columns.Contains("IdPresupuesto"))
                                drF["IdPresupuesto"] = dtTramos.Rows[0]["IdPresupuesto"].S();

                            drF["IdOrigen"] = dtTramos.Rows[0]["IdOrigen"].S();
                            drF["IdDestino"] = dtTramos.Rows[i]["IdOrigen"].S();
                            drF["Origen"] = sInicio;
                            drF["Destino"] = sFin;
                            drF["CantPax"] = 0;


                            dtFechaLlegada = dtTramos.Rows[i]["FechaSalida"].Dt();
                            drF["FechaLlegada"] = dtFechaLlegada;

                            string[] sTiempos2 = sTiempo.Split(':');
                            dtFechaSalida = dtFechaLlegada.AddHours(double.Parse(sTiempos2[0]) * (-1));
                            dtFechaSalida = dtFechaLlegada.AddMinutes(double.Parse(sTiempos2[1]) * (-1));
                            drF["FechaSalida"] = dtFechaSalida;

                            TimeSpan tsF;
                            tsF = dtFechaLlegada - dtFechaSalida;


                            drF["TiempoVuelo"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? tsF.S() : sTiempo;
                            drF["TiempoEspera"] = "00:00";
                            drF["TiempoCobrar"] = (sTiempo == "00:00:00" || sTiempo == "00:00") ? tsF.S() : sTiempo;
                            //drF["RealVirtual"] = "Virtual";
                            drF["SeCobra"] = 1;

                            //drF["Status"] = 1;
                            //drF["UsuarioCreacion"] = GetUser;
                            //drF["FechaCreacion"] = DateTime.Now;
                            //drF["UsuarioModificacion"] = string.Empty;
                            //drF["FechaModificacion"] = DateTime.Now;
                            //drF["IP"] = GetIPAddress();

                            dtRes.Rows.Add(drF);
                            #endregion


                            dtRes.ImportRow(dtTramos.Rows[i]);
                        }
                        //else
                        //{
                        //    if (dtRes.Rows.Count > 0)
                        //    {
                        //        dtRes.ImportRow(dtTramos.Rows[i + 1]);
                        //    }
                        //}
                    }
                }

                //return dtRes.Rows.Count > 0 ? dtRes : dtTramos;
                return dtRes;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /*
         * 
         *--=============================================================================================== 
         *                                 EMPIEZAN NOTIFICACIONES APP MOBILE
         *--===============================================================================================
         * 
         */
        public static bool IsSolicitudMobile(int iIdSolicitud)
        {
            try
            {
                return new DBSolicitudesVuelo().DBGetConsultaIsSolicitudMobile(iIdSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void NotificaAppMobile(int iIdSolicitud, string sStatus)
        {
            using (var client = new HttpClient())
            {
                var request = new AppNotification()
                {
                    id = iIdSolicitud,
                    status = sStatus
                };

                string sPathWSApp = ConfigurationManager.AppSettings["PathWSMobile"].S();

                var response = client.PostAsync(sPathWSApp,
                    new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    Utils.SaveErrorEnBitacora(response.S(), "PruebaApp", "PruebaApp", "Dictamen");
                }
            }
        }

        public static JsonObjeto NotificaAppMovil(string sURL, decimal dCostoVuelo, decimal dCostoIVA, DateTime dtFechaSalida, DateTime dtFechaReserva,
            string sAeropuertoLlegada, string sAeropuertoSalida, string sTiempoVuelo, int iIdTipoEquipo, int iIdFerry, int iLegId)
        {
            try
            {
                JsonObjeto oJson = new JsonObjeto();
                // Convertirmos fechas en TimeStamp
                double dFechaSalida = DateTimeToUnixTimestamp(dtFechaSalida);
                double dFechaReserva = DateTimeToUnixTimestamp(dtFechaReserva);

                using (var client = new HttpClient())
                {
                    // Establecer la url que proporciona acceso al servidor que publica la API 
                    client.BaseAddress = new Uri(sURL);

                    // Configurar encabezados para que la petición de realice en formato JSON
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Obtener el texto JSON con los datos 
                    string request = JsonConvert.SerializeObject(
                        new
                        {
                            id = iIdFerry,
                            code = "",
                            list_price = dCostoVuelo,
                            state = "pending",
                            taxes = dCostoIVA,
                            attachments = new[] { new { itinerary = new { url = "null" } } },
                            legs = new[] {
                                        new {
                                                id = iLegId,
                                                layover = false,
                                                passengers = 1,
                                                departure_at_timestamp = dFechaSalida,
                                                suggested_departure_at_timestamp = dFechaReserva,
                                                 arrival_airport = new {icao_code = sAeropuertoLlegada},
                                                 aircraft_id = iIdTipoEquipo,
                                                 departure_airport = new {icao_code = sAeropuertoSalida},
                                                 notes = "",
                                                 flight_duration = sTiempoVuelo
                                            }
                                        }
                        });


                    //object resultado = JsonConvert.DeserializeObject(request);                
                    // Serializar el Json para obtener el response
                    var response = client.PostAsync(sURL, new StringContent(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(request)), Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        //var result = response.Content.ReadAsStringAsync().Result;                    
                        // objenemos el Response en JSon
                        object jsonResponse = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                        // Deserializamos con Anonymous Type
                        var definition = new
                        {
                            route = new
                            {
                                id = "",
                                identifier = "",
                                code = "",
                                state = "",
                                list_price = "",
                                list_price_cents = "",
                                taxes = "",
                                taxes_cents = "",
                                attachments = new
                                {
                                    details = new { url = "" },
                                    itinerary = new { url = "" }
                                },
                                legs = new[] { new {
                                                id =  "",
                                                identifier = "",
                                                departure_at_timestamp = "",
                                                suggested_departure_at_timestamp = "",
                                                flight_duration = "",                                                
                                                layover = "",
                                                passengers = "",
                                                notes = "",
                                                aircraft_id = "",
                                                departure_airport = new {   
                                                                            code = "",
                                                                            icao_code = ""
                                                                        },

                                                arrival_airport = new {
                                                                        code = "",
                                                                        icao_code = ""
                                                                        }                                                                                                                                                                                                    
                                                }
                                            }
                            }
                        };

                        var jResponse = JsonConvert.DeserializeAnonymousType(@jsonResponse.ToString(), definition);

                        oJson.iId = jResponse.route.id.S().I();
                        oJson.iIdLeg = jResponse.route.legs[0].id.S().I();
                        oJson.sCadenaRequest = request;
                        oJson.sCadenaResponse = @jsonResponse.ToString();
                    }
                }
                return oJson;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static JsonObjeto NotificaAppMovilActualizacion(string sURL, decimal dCostoVuelo, decimal dCostoIVA, DateTime dtFechaSalida, DateTime dtFechaReserva,
            string sAeropuertoLlegada, string sAeropuertoSalida, string sTiempoVuelo, int iIdTipoEquipo, int iIdFerry, int iLegId)
        {
            try
            {
                JsonObjeto oJson = new JsonObjeto();

                double dFechaSalida = DateTimeToUnixTimestamp(dtFechaSalida);
                double dFechaReserva = DateTimeToUnixTimestamp(dtFechaReserva);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(sURL + iIdFerry);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string request = JsonConvert.SerializeObject(
                        new
                        {
                            id = iIdFerry,
                            code = "",
                            list_price = dCostoVuelo,
                            state = "pending",
                            taxes = dCostoIVA,
                            // attachments = new[] { new { itinerary = new { url = "null" } } },
                            legs = new[] {
                                        new {
                                                id = iLegId,
                                                layover = false,
                                                passengers = 1,
                                                departure_at_timestamp = dFechaSalida,
                                                suggested_departure_at_timestamp = dFechaReserva,
                                                 arrival_airport = new {icao_code = sAeropuertoLlegada},
                                                 aircraft_id = iIdTipoEquipo,
                                                 departure_airport = new {icao_code = sAeropuertoSalida},
                                                 notes = "",
                                                 flight_duration = sTiempoVuelo
                                            }
                                        }
                        });

                    var response = client.PutAsync(sURL + iIdFerry, new StringContent(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(request)), Encoding.UTF8, "application/json")).Result;
                    //var response = client.PostAsync(sURL,new StringContent(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(request)), Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        //var result = response.Content.ReadAsStringAsync().Result;                    
                        // objenemos el Response en JSon
                        object jsonResponse = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                        // Deserializamos con Anonymous Type
                        var definition = new
                        {
                            route = new
                            {
                                id = "",
                                identifier = "",
                                code = "",
                                state = "",
                                list_price = "",
                                list_price_cents = "",
                                taxes = "",
                                taxes_cents = "",
                                attachments = new
                                {
                                    details = new { url = "" },
                                    itinerary = new { url = "" }
                                },
                                claims_count = "",
                                legs = new[] { new {
                                                id =  "",
                                                identifier = "",
                                                departure_at_timestamp = "",
                                                suggested_departure_at_timestamp = "",
                                                flight_duration = "",                                                
                                                layover = "",
                                                passengers = "",
                                                notes = "",
                                                aircraft_id = "",
                                                departure_airport = new {   
                                                                            code = "",
                                                                            icao_code = ""
                                                                        },

                                                arrival_airport = new {
                                                                        code = "",
                                                                        icao_code = ""
                                                                        }                                                                                                                                                                                                    
                                                },                                            
                                                new{
                                                    id = "",
                                                    identifier = "",
                                                    departure_at_timestamp = "",
                                                    suggested_departure_at_timestamp = "",
                                                    flight_duration = "",
                                                    layover = "",
                                                    passengers = "",
                                                    notes = "",
                                                    aircraft_id = "",
                                                    departure_airport = new {
                                                                                code = "",
                                                                                icao_code = ""
                                                                            },
                                                    arrival_airport = new {
                                                                                code = "",
                                                                                icao_code = ""
                                                                            }   
                                                }                                            
                                }
                            }
                        };

                        var jResponse = JsonConvert.DeserializeAnonymousType(@jsonResponse.ToString(), definition);

                        oJson.iId = jResponse.route.id.S().I();
                        oJson.iIdLeg = jResponse.route.legs[0].id.S().I();
                        oJson.sCadenaRequest = request;
                        oJson.sCadenaResponse = @jsonResponse.ToString();

                    }
                }
                return oJson;
            }
            catch (OperationCanceledException oce)
            {
                throw oce;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static JsonObjeto NotificaAppMovilCancelacion(string sURL, int iId, string sStatus)
        {
            try
            {
                JsonObjeto oJson = new JsonObjeto();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(sURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.DeleteAsync(sURL + iId);

                    object jsonResponse = JsonConvert.DeserializeObject(response.Result.Content.ReadAsStringAsync().Result);
                    oJson.iId = iId;

                    oJson.sCadenaRequestCancela = sURL + iId;
                    oJson.sCadenaResponseCancela = @jsonResponse.ToString();

                }
                return oJson;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            try
            {
                return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                       new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static byte[] ObtenerPDF(string HTML)
        {
            try
            {
                byte[] bPDF = null;

                MemoryStream ms = new MemoryStream();
                TextReader txtReader = new StringReader(HTML);

                // 1: create object of a itextsharp document class
                Document doc = new Document(PageSize.A2, 100, 30, 130, 0);

                // 2: we create a itextsharp pdfwriter that listens to the document and directs a XML-stream to a file
                PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);

                // 3: we create a worker parse the document
                HTMLWorker htmlWorker = new HTMLWorker(doc);

                // 4: we open document and start the worker on the document
                doc.Open();
                htmlWorker.StartDocument();

                // 5: parse the html into the document
                htmlWorker.Parse(txtReader);

                // 6: close the document and the worker
                htmlWorker.EndDocument();
                htmlWorker.Close();
                doc.Close();

                bPDF = ms.ToArray();

                return bPDF;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static StringBuilder CuerpoCorreoHtmlTripGuide(string sNombreContacto, int iTrip, string sICAOOrigen, string sNombreAeropuertoOrigen, string sICAODestino,
                                                        string sNombreAeropuertoDestino, DateTime DtFechaSalida, string sNumeroPasajero, string sAeronave, string sPiloto,
                                                        string sPilotoTelefono, string sCoPiloto, string sCoPilotoTelefono, string sObservaciones)
        {
            StringBuilder sbCadenaHtml = new StringBuilder();
            try
            {
                sbCadenaHtml.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                sbCadenaHtml.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
                sbCadenaHtml.Append("<head>");
                sbCadenaHtml.Append("<meta name=\"viewreport\" content=\"width=device-width, initial scale=1.0\">");
                sbCadenaHtml.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />");
                sbCadenaHtml.Append("<title>Solicitudes MexJet</title>");
                sbCadenaHtml.Append("<style type=\"text/css\">");
                sbCadenaHtml.Append("body p {");
                sbCadenaHtml.Append("font-family: Arial, Helvetica, sans-serif;");
                sbCadenaHtml.Append("}");
                sbCadenaHtml.Append(".tblc{");
                sbCadenaHtml.Append("border-collapse:collapse;");
                sbCadenaHtml.Append("}");

                sbCadenaHtml.Append(".heads{");
                sbCadenaHtml.Append("color:#193D71;");
                sbCadenaHtml.Append("font-family:Arial, Helvetica, sans-serif;");
                sbCadenaHtml.Append("font-size:16px;");
                sbCadenaHtml.Append("font-weight:bold;");
                sbCadenaHtml.Append("}");
                sbCadenaHtml.Append(".textoinf{");
                sbCadenaHtml.Append("font-weight:bold;");
                sbCadenaHtml.Append("font-family:Verdana, Geneva, sans-serif;");
                sbCadenaHtml.Append("font-size:14px;");
                sbCadenaHtml.Append("}");
                sbCadenaHtml.Append(".texto_data{");
                sbCadenaHtml.Append("font-weight:bold;");
                sbCadenaHtml.Append("font-family:Verdana, Geneva, sans-serif;");
                sbCadenaHtml.Append("font-size:14px;");
                sbCadenaHtml.Append("color:#999999;");
                sbCadenaHtml.Append("}");
                sbCadenaHtml.Append(".nom{");
                sbCadenaHtml.Append("color:#00CCFF;");
                sbCadenaHtml.Append("font-size:24px;");
                sbCadenaHtml.Append("}");
                sbCadenaHtml.Append(".letra{");
                sbCadenaHtml.Append("font-family:Arial, Helvetica, sans-serif;");
                sbCadenaHtml.Append("font-size:12px;");
                sbCadenaHtml.Append("color:#999999;");
                sbCadenaHtml.Append("}");
                sbCadenaHtml.Append("</style>");
                sbCadenaHtml.Append("</head>");

                sbCadenaHtml.Append("<body>");
                sbCadenaHtml.Append("<img src=\"http://www.portal-ale.com/comunicados/plantillasapp/MailMexJetHead.png\"  width=\"100%\">");

                sbCadenaHtml.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 100%;\" align=\"center\">");
                sbCadenaHtml.Append("<tbody>");
                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td colspan=\"4\"></td>");
                sbCadenaHtml.Append("</tr>");
                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td width=\"33\" style=\"width: 20px;\"></td>");
                sbCadenaHtml.Append("<td colspan=\"2\"><p>Estimado (a):" + sNombreContacto + "</p>");
                sbCadenaHtml.Append("<p><span id=\"m_5962371973682545954gmail-:9e.co\" dir=\"ltr\"><br />");
                sbCadenaHtml.Append("Por favor encuentre adjunta la confirmación de vuelo</span> <span id=\"m_4373959120150324763gmail-:7y.co\" dir=\"ltr\"># " + iTrip.S() + "<strong></strong>.</span></p>");

                sbCadenaHtml.Append("<table class=\"tblc\">");

                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td class=\"heads\" align=\"center\" rowspan=\"2\" style=\"border-right:2px solid #CCC;\"><p>Origen</p></td>");
                sbCadenaHtml.Append("<td class=\"textoinf nom\" align=\"center\" style=\"border-bottom:1px solid #CCC;\">" + sICAOOrigen + "</td>");
                sbCadenaHtml.Append("</tr>");
                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td class=\"texto_data\" align=\"center\">" + sNombreAeropuertoOrigen + "</td>");
                sbCadenaHtml.Append("</tr>");

                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td colspan=\"2\">&nbsp;</td>");
                sbCadenaHtml.Append("</tr>");

                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td class=\"heads\" align=\"center\" rowspan=\"2\" style=\"border-right:2px solid #CCC;\"><p>Destino</p></td>");
                sbCadenaHtml.Append("<td class=\"textoinf nom\" align=\"center\" style=\"border-bottom:1px solid #CCC;\">" + sICAODestino + "</td>");
                sbCadenaHtml.Append("</tr>");
                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td class=\"texto_data\" align=\"center\">" + sNombreAeropuertoDestino + "</td>");
                sbCadenaHtml.Append("</tr>");

                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td colspan=\"2\">&nbsp;</td>");
                sbCadenaHtml.Append("</tr>");

                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");

                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td class=\"heads\" align=\"center\"><p>Fecha:</p></td>");
                sbCadenaHtml.Append("<td class=\"texto_data\" align=\"left\">" + DtFechaSalida.ToLongDateString() + "</td>");
                sbCadenaHtml.Append("</tr>");
                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td class=\"heads\" align=\"center\"><p>Hora:</p></td>");
                sbCadenaHtml.Append("<td class=\"texto_data\" align=\"left\">" + DtFechaSalida.ToString("HH:mm") + " hrs.</td>");
                sbCadenaHtml.Append("</tr>");

                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td class=\"heads\" align=\"left\"><p>Pasajeros:</p></td>");
                sbCadenaHtml.Append("<td class=\"texto_data\" align=\"left\">" + sNumeroPasajero + "</td>");
                sbCadenaHtml.Append("</tr>");
                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td class=\"heads\" align=\"left\"><p>Aeronave:</p></td>");
                sbCadenaHtml.Append("<td class=\"texto_data\" align=\"left\">" + sAeronave + "</td>");
                sbCadenaHtml.Append("</tr>");

                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td colspan=\"2\" style=\"border-bottom:1px solid #CCC;\">&nbsp;</td>");
                sbCadenaHtml.Append("</tr>");

                sbCadenaHtml.Append("</table>");
                sbCadenaHtml.Append("<br />");
                sbCadenaHtml.Append("<br />");
                sbCadenaHtml.Append("<br />");

                sbCadenaHtml.Append("<table width=\"100%\" class=\"tblc\">");
                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td class=\"heads\" align=\"left\" rowspan=\"2\">Piloto</td>");
                sbCadenaHtml.Append("<td class=\"textoinf\" align=\"left\">" + sPiloto + "</td>");
                sbCadenaHtml.Append("</tr>");
                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td class=\"textoinf\" align=\"left\">cel. " + sPilotoTelefono + "</td>");
                sbCadenaHtml.Append("</tr>");
                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td colspan=\"2\">&nbsp;</td>");
                sbCadenaHtml.Append("</tr>");
                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td class=\"heads\" align=\"left\" rowspan=\"2\">Copiloto</td>");
                sbCadenaHtml.Append("<td class=\"textoinf\" align=\"left\">" + sCoPiloto + "</td>");
                sbCadenaHtml.Append("</tr>");
                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td class=\"textoinf\" align=\"left\">cel. " + sCoPilotoTelefono + "</td>");
                sbCadenaHtml.Append("</tr>");
                sbCadenaHtml.Append("</table>");

                sbCadenaHtml.Append("<div class=\"row\">");
                sbCadenaHtml.Append("<div class=\"col-sm-4\">");
                sbCadenaHtml.Append("<h4>");
                sbCadenaHtml.Append("<ul>");
                sbCadenaHtml.Append("<li>Observaciones:</li>");
                sbCadenaHtml.Append("</ul>");
                sbCadenaHtml.Append("</h4>");
                sbCadenaHtml.Append("<p>"+ sObservaciones +"</p>");
                sbCadenaHtml.Append("</div>");
                sbCadenaHtml.Append("</div>");
                sbCadenaHtml.Append("<br /><br />");

                sbCadenaHtml.Append("<p> Es un placer atenderle.</p>");
                sbCadenaHtml.Append("<p>Atentamente </p>");
                sbCadenaHtml.Append("<p><strong>Aerolíneas Ejecutivas <br />");
                sbCadenaHtml.Append("</strong><em>Acercando líderes a su destino.  </em></p></td>");
                sbCadenaHtml.Append("<td width=\"22\" style=\"width: 20px;\">&nbsp;</td>");
                sbCadenaHtml.Append("</tr>");
                sbCadenaHtml.Append("<tr>");
                sbCadenaHtml.Append("<td style=\"height: 75px; background-color: #0c1d36;\">&nbsp;</td>");
                sbCadenaHtml.Append("<td width=\"247\" style=\"background-color: #0c1d36;\">");
                sbCadenaHtml.Append("<p><a href=\"https://www.facebook.com/pages/Aerolíneas-Ejecutivas/382333655148718?fref=ts\"><img src=\"http://www.portal-ale.com/comunicados/plantillasapp/Facebook-logo.png\"  alt=\"Facebook\" /></a> <a href=\"https://twitter.com/VuelaALE\"><img src=\"http://www.portal-ale.com/comunicados/plantillasapp/Twitter-logo.png\"  alt=\"Twitter\" /></a> <a href=\"https://www.youtube.com/channel/UCcL42CDQabiEQ-6N03-dZ5g\"> <img src=\"http://www.portal-ale.com/comunicados/plantillasapp/Youtube-logo.png\" alt=\"Youtube\"  /></a></p>");
                sbCadenaHtml.Append("</td>");
                sbCadenaHtml.Append("<td width=\"428\" style=\"background-color: #0c1d36;\">");
                sbCadenaHtml.Append("<p style=\"text-align: right;\"><span style=\"font-size: 12px; color: #ffffff;\"><strong>¿Necesita ayuda adicional?</strong><br />");
                sbCadenaHtml.Append("Tel: +52 (722) 279.1625 ó 1620</span><br />");
                sbCadenaHtml.Append("<span style=\"font-size: 12px; color: #ffffff;\"><a href=\"mailto:ac.tlc@aerolineasejecutivas.com\"><span style=\"font-size: 12px; color: #ffffff; text-decoration:none;\">ac.tlc@aerolineasejecutivas.com</span></a></span></p>");

                sbCadenaHtml.Append("</body>");
                sbCadenaHtml.Append("</html>");

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return sbCadenaHtml;
        }

        public static string ObtieneTokenAppMexJet()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();


            string sToken = string.Empty;
            string url = Utils.ObtieneParametroPorClave("117");
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

            Token oToken = ser.Deserialize<Token>(sToken);
            string sTok = oToken.token;

            return sTok;
        }

        public static string BuscaErrorEnTabla(DataTable dt)
        {
            try
            {
                string sMensaje = string.Empty;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["sts"].S() == "error")
                    {
                        sMensaje = row["mensaje"].S();
                    }
                }
                
                return sMensaje;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ConvertStringToBase64(this string cadena)
        {
            try
            {
                var ConversionCadena = Encoding.UTF8.GetBytes(cadena);              
                var PrimeraCadenaBase64 = Convert.ToBase64String(ConversionCadena);
                var SegundaConversionCadena = Encoding.UTF8.GetBytes(PrimeraCadenaBase64);
                return Convert.ToBase64String(SegundaConversionCadena);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public static string ConvertBase64ToString(this string cadena)
        {
            try
            {
                var ConversionCadena = Convert.FromBase64String(cadena);
                var PrimeraCadenaString = System.Text.Encoding.UTF8.GetString(ConversionCadena);
                var SegundaConversionCadena = Convert.FromBase64String(PrimeraCadenaString);
                return System.Text.Encoding.UTF8.GetString(SegundaConversionCadena);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        //Recibir horas contratadas
        //Monto de horas contratadas
        public static string ObtenerHorasServicioConCargo(string sMonto, int iIdContrato) 
        {
            try
            {
                DataTable dtHrsCon = new DataTable();
                dtHrsCon = new DBRemision().DBGetObtieneHorasContratadas(iIdContrato);

                string sHoras = string.Empty;
                decimal dMonto = sMonto.Replace("$", "").ToString().D();
                decimal dHrsCon = 0;
                decimal dCostoHrsCon = 0; //Anticipo inicial
                decimal dCostoXHora = 0;
                decimal dRes = 0;
                int iMoneda = 0;

                decimal dTipoC = Utils.GetTipoCambioDia;

                if (dtHrsCon != null && dtHrsCon.Rows.Count > 0)
                {
                    dHrsCon = dtHrsCon.Rows[0]["HorasContratadasTotal"].S().D();
                    dCostoHrsCon = dtHrsCon.Rows[0]["AnticipoInicial"].S().D();
                    //1 = Pesos, 2 = Dolares
                    iMoneda = dtHrsCon.Rows[0]["TipoCambio"].S().I();

                    if(iMoneda == 2)
                        dCostoHrsCon = dCostoHrsCon * dTipoC;

                    //Calcula Costo por hora
                    dCostoXHora = dCostoHrsCon / dHrsCon;
                }

                dRes = dMonto / dCostoXHora;
                sHoras = ConvierteDecimalATiempo(dRes);
                return sHoras;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}