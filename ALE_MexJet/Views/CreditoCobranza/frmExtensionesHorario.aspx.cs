using ALE_MexJet.Clases;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Data;
using ALE_MexJet.Presenter;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using CrystalDecisions.Shared;

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmExtensionesHorario : System.Web.UI.Page, IViewExtensionServicios, IViewExtensionServiciosFPK
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new ExtensionServicios_Presenter(this, new DBExtensionServicios());
            oPresenterFPK = new ExtensionServiciosFPK_Presenter(this, new DBExtensionServiciosFPK());

            if (!IsPostBack)
            {
                mpeMensaje.OkButtonPressed += mpeMensaje_OkButtonPressed;
                LoadDatosIniciales();

                if (ViewState["VSIdExtension"] == null)
                    iExtension = 0;

                if (Page.Request["Folio"] != null)
                {
                    iExtension = Page.Request["Folio"].S().I();

                    if (eObjSelected != null)
                        eObjSelected(sender, e);
                }
            }
        }
        protected void ddlOrigen_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
        {

        }
        protected void ddlOrigen_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                //Verificar si tiene pierna predecesora para obtener el ICAO y mandarlo como parametro.
                ASPxComboBox comboBox = (ASPxComboBox)source;

                if (eLoadOrigDestFiltro != null)
                    eLoadOrigDestFiltro(e.Filter, e);

                CargaComboAeropuertos(comboBox);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlOrigen_ItemsRequestedByFilterCondition", "Aviso");
            }
        }
        protected void upaPrincipal_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
        protected void mpeMensaje_OkButtonPressed(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OcultaError", "OcultaError();", true);

        }
        protected void txtMinutos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sTiempoAC = txtTiempoAC.Text.S();
                decimal dTiempoAC = Utils.ConvierteTiempoaDecimal(sTiempoAC).S().D();
                decimal dDuracion = txtMinutos.Text.S().D() / 60;

                if (ddlTipoOperacion.SelectedItem != null)
                {
                    if (ddlTipoOperacion.Value.S() == "1") //Llegada
                    {
                        //UTC Solicitado Llegada
                        txtTLlegada.Text = Utils.ConvierteDecimalATiempo(dTiempoAC + dDuracion);
                        txtTSalida.Text = "00:00";
                    }
                    else if (ddlTipoOperacion.Value.S() == "2")
                    {

                        //UTC Solicitado Salida
                        txtTSalida.Text = Utils.ConvierteDecimalATiempo(dTiempoAC + dDuracion);
                        txtTLlegada.Text = "00:00";
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtMinutos_TextChanged", "Aviso");
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (iExtension == 0)
                {
                    if (eNewObj != null)
                        eNewObj(sender, e);
                }
                else
                {
                    if (eSaveObj != null)
                        eSaveObj(sender, e);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardar_Click", "Aviso");
            }
        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (eLoadExtensionServicioImprimir != null)
                    eLoadExtensionServicioImprimir(null, null);

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnImprimir_Click", "Aviso");
            }
        }
        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            try
            {
                if (eLoadExtensionServicioEnviarMail != null)
                    eLoadExtensionServicioEnviarMail(null, null);
            }
            catch (Exception ex)
            {

                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnEnviarMail_Click", "Aviso");
            }
        }
        #endregion

        #region METODOS

        private void LoadDatosIniciales()
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ImprimirReporte(ExtensionServiciosReporte oExtensionServicioReporte)
        {
            //try
            //{
                ReportDocument rd = new ReportDocument();
                rd = LlenaReporteExtensionServicios(oExtensionServicioReporte);

                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "ExtensionServicios.pdf");

                //using (ReportDocument rd = LlenaReporteExtensionServicios(oExtensionServicioReporte))
                //{
                //    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "ExtensionServicios.pdf");

                    //using (var mStream = (MemoryStream)rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
                    //{
                    //    Response.Clear();
                    //    Response.ContentType = "application/pdf";
                    //    Response.AddHeader("content-disposition", "attachment;filename=ExtensionServicios.pdf");
                    //    Response.ContentType = "application/octet-stream";
                    //    Response.Buffer = true;
                    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    //    Response.BinaryWrite(mStream.ToArray());
                    //    Response.Flush();
                    //    Response.End();
                    //}
                //}
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
        }
        public void EnviarMailReporte(ExtensionServiciosReporte oExtensionServicioReporte)
        {
            try
            {
                using (ReportDocument rd = LlenaReporteExtensionServicios(oExtensionServicioReporte))
                {
                    using (var mStream = (MemoryStream)rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
                    {
                        string sContactoPara = txtPara.Text;
                        string Asunto = "Extensión de Servicio";
                        string sContactosCopia = txtConCopia.Text;
                        string sContactosCopiaOculta = txtConCopiaOculta.Text;

                        string scorreo = Utils.ObtieneParametroPorClave("4");
                        string sPass = Utils.ObtieneParametroPorClave("5");
                        string sservidor = Utils.ObtieneParametroPorClave("6");
                        string spuerto = Utils.ObtieneParametroPorClave("7");

                        Stream ArchivoAdjunto = mStream;
                        string Motivo = "Extensión de Servicio.";

                        string sNombreArchivoPDF = "ExtensionServicios.pdf";

                        Utils.EnvioCorreo(sservidor, spuerto.S().I(), Asunto, sContactoPara, Motivo, ArchivoAdjunto, scorreo, sPass, sContactosCopia, sNombreArchivoPDF, "", sContactosCopiaOculta);

                        LimpiaCamporEnviarMail();

                        mpeMensaje.ShowMessage("Envio de correo exitoso.", "");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private ReportDocument LlenaReporteExtensionServicios(ExtensionServiciosReporte oExtensionServicioReporte)
        {
            try
            {
                ReportDocument rd = new ReportDocument();

                string strPath = string.Empty;
                strPath = Server.MapPath("..\\Consultas\\CristalReport\\ExtensionServicios.rpt");
                rd.Load(strPath);

                rd.SetParameterValue("Fecha", DateTime.Now.ToString("dd-MM-yyyy"));
                rd.SetParameterValue("Hora", DateTime.Now.ToString("HH:mm:ss"));
                rd.SetParameterValue("Titulo", oExtensionServicioReporte.sTituloExtension);
                rd.SetParameterValue("Aeropuerto", oExtensionServicioReporte.sAeropuerto);
                rd.SetParameterValue("PropietarioAeronave", oExtensionServicioReporte.sNombreFiscalALE);
                rd.SetParameterValue("DomicilioFiscal", oExtensionServicioReporte.sDomicilioFiscalALE);
                rd.SetParameterValue("DomicilioParticular", oExtensionServicioReporte.sDomicilioParticularALE);
                rd.SetParameterValue("RFC", oExtensionServicioReporte.sRfcALE);
                rd.SetParameterValue("TEL", oExtensionServicioReporte.sTelefonoALE);
                rd.SetParameterValue("Directo", oExtensionServicioReporte.sTelefonoDirectoALE);
                rd.SetParameterValue("Matricula", oExtensionServicioReporte.sMatricula);
                rd.SetParameterValue("Equipo", oExtensionServicioReporte.sEquipo);
                rd.SetParameterValue("TipoVuelo", "G"); 
                rd.SetParameterValue("ClaseVuelo", "G"); 
                rd.SetParameterValue("NombrePiloto", oExtensionServicioReporte.sNombrePiloto);
                rd.SetParameterValue("NumeroLicenciaPiloto", ObtieneLicenciaPiloto(oExtensionServicioReporte.sCrewCodePiloto));  // FALTA POR OBTENER MEDIAN FPK
                rd.SetParameterValue("NombreCopiloto", oExtensionServicioReporte.sNombreCopiloto);
                rd.SetParameterValue("NumeroLicenciaCopiloto",  ObtieneLicenciaPiloto(oExtensionServicioReporte.sCrewCodeCopiloto)); // FALTA POR OBTENER MEDIAN FPK
                rd.SetParameterValue("FechaPropuestaOperacion", oExtensionServicioReporte.DtFecha.ToString("dd-MM-yyyy"));
                rd.SetParameterValue("Procedencia", oExtensionServicioReporte.sEstacionICAO);
                rd.SetParameterValue("Destino", oExtensionServicioReporte.sDestinoICAO);
                rd.SetParameterValue("Llegada", oExtensionServicioReporte.sUTCLlegada);
                rd.SetParameterValue("Salida", oExtensionServicioReporte.sUTCSalida);
                rd.SetParameterValue("MotivoSolicitud", oExtensionServicioReporte.sMotivo);
                rd.SetParameterValue("OperadorPropietarioExplotador", ObtieneNombreUsuario());

                return rd;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void LoadDatosControles(DataTable dtPiloto, DataTable dtMat, DataSet dsTiempo)
        {
            try
            {
                CargaComboAeropuertos(ddlEstacion);
                CargaComboAeropuertos(ddlOrigen);
                CargaComboAeropuertos(ddlDestino);


                ddlPiloto.DataSource = dtPiloto;
                ddlPiloto.ValueField = "IdPiloto";
                ddlPiloto.TextField = "ClavePiloto";
                ddlPiloto.DataBind();

                ddlCopiloto.DataSource = dtPiloto;
                ddlCopiloto.ValueField = "IdPiloto";
                ddlCopiloto.TextField = "ClavePiloto";
                ddlCopiloto.DataBind();

                dtMatriculas = dtMat;
                ddlMatricula.DataSource = dtMat;
                ddlMatricula.ValueField = "IdAeroave";
                ddlMatricula.TextField = "Matricula";
                ddlMatricula.DataBind();

                //ddlHorasA.DataSource = dsTiempo.Tables[0];
                //ddlHorasA.ValueField = "IdHora";
                //ddlHorasA.TextField = "DescHora";
                //ddlHorasA.DataBind();

                //ddlMinutosA.DataSource = dsTiempo.Tables[1];
                //ddlMinutosA.ValueField = "IdMinuto";
                //ddlMinutosA.TextField = "DescMinuto";
                //ddlMinutosA.DataBind();

                // HORAS
                //ddlTSalidaH.DataSource = dsTiempo.Tables[0];
                //ddlTSalidaH.ValueField = "IdHora";
                //ddlTSalidaH.TextField = "DescHora";
                //ddlTSalidaH.DataBind();

                //ddlTLlegadaH.DataSource = dsTiempo.Tables[0];
                //ddlTLlegadaH.ValueField = "IdHora";
                //ddlTLlegadaH.TextField = "DescHora";
                //ddlTLlegadaH.DataBind();

                // MINUTOS
                //ddlTSalidaM.DataSource = dsTiempo.Tables[1];
                //ddlTSalidaM.ValueField = "IdMinuto";
                //ddlTSalidaM.TextField = "DescMinuto";
                //ddlTSalidaM.DataBind();

                //ddlTLlegadaM.DataSource = dsTiempo.Tables[1];
                //ddlTLlegadaM.ValueField = "IdMinuto";
                //ddlTLlegadaM.TextField = "DescMinuto";
                //ddlTLlegadaM.DataBind();


                //ddlHorasA.SelectedIndex = 0;
                //ddlMinutosA.SelectedIndex = 0;

                //ddlTSalidaH.SelectedIndex = 0;
                //ddlTSalidaM.SelectedIndex = 0;

                //ddlTLlegadaH.SelectedIndex = 0;
                //ddlTLlegadaM.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargaComboAeropuertos(ASPxComboBox ddl)
        {
            try
            {
                ddl.DataSource = null;
                ddl.Text = string.Empty;
                ddl.Value = null;
                ddl.DataBind();

                ddl.DataSource = dtOrigenDestino;
                ddl.TextField = "AeropuertoICAO";
                ddl.ValueField = "idorigen";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            mpeMensaje.ShowMessage(sMensaje, sCaption);
        }
        private void LimpiaCamporEnviarMail()
        {
            try
            {
                txtPara.Text = string.Empty;
                txtConCopia.Text = string.Empty;
                txtConCopiaOculta.Text = string.Empty;
                memoObservaciones.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ObtieneNombreUsuario()
        {
            string sUsuario = string.Empty;
            try
            {
                UserIdentity objUsuario = (UserIdentity)Session["UserIdentity"];

                sUsuario = objUsuario.sName;

                return sUsuario;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private string ObtieneLicenciaPiloto(string CrewCode)
        {
            string Resultado = string.Empty;

            try
            {
                if (eObtieneLicenciaPiloto != null)
                    eObtieneLicenciaPiloto(CrewCode,null);

                Resultado = sLicenciaPiloto;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return Resultado;
        }
        protected void SetReadOnlyTrue()
        {
            foreach (Control ctrl in pnlPrincipalTrabajo.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).ReadOnly = true;
                }

                if (ctrl is ASPxTextBox)
                {
                    ((ASPxTextBox)ctrl).ReadOnly = true;
                }

                if (ctrl is ASPxComboBox)
                {
                    ((ASPxComboBox)ctrl).ReadOnly = true;
                }

                if (ctrl is ASPxDateEdit)
                {
                    ((ASPxDateEdit)ctrl).ReadOnly = true;
                }

                if (ctrl is ASPxMemo)
                {
                    ((ASPxMemo)ctrl).ReadOnly = true;
                }
            }
        }
        #endregion

        #region VARIABLES Y PROPIEDADES

        private const string sClase = "frmExtensionesHorario.aspx.cs";
        private const string sPagina = "frmExtensionesHorario.aspx";

        ExtensionServicios_Presenter oPresenter;
        ExtensionServiciosFPK_Presenter oPresenterFPK;

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eLoadOrigDestFiltro;
        public event EventHandler eLoadExtensionServicioImprimir;
        public event EventHandler eLoadExtensionServicioEnviarMail;
        public event EventHandler eObtieneLicenciaPiloto;

        public int iExtension
        {
            get { return (int)ViewState["VSIdExtension"]; }
            set { ViewState["VSIdExtension"] = value; }
        }
        public DataTable dtMatriculas
        {
            get { return (DataTable)ViewState["VSMatricula"]; }
            set { ViewState["VSMatricula"] = value; }
        }
        public DataTable dtOrigenDestino
        {
            get { return (DataTable)ViewState["VSOrigenDestino"]; }
            set { ViewState["VSOrigenDestino"] = value; }
        }
        public string sFiltro
        {
            get { return (string)ViewState["VSFiltro"]; }
            set { ViewState["VSFiltro"] = value; }
        }

        private string _sLicenciaPiloto;

        public string sLicenciaPiloto
        {
            get { return _sLicenciaPiloto; }
            set { _sLicenciaPiloto = value; }
        }
        
        public ExtensionServicios oExtensionServ
        {
            get
            {
                ExtensionServicios oExt = new ExtensionServicios();
                oExt.iIdExtension = iExtension;
                oExt.iIdEstacion = ddlEstacion.SelectedItem.Value.S().I();
                oExt.iIdOrigen = ddlOrigen.SelectedItem.Value.S().I();
                oExt.iIdDestino = ddlDestino.SelectedItem.Value.S().I();
                oExt.DtFecha = txtFechaVuelo.Text.S().Dt();
                oExt.iIdMatricula = ddlMatricula.SelectedItem.Value.S().I();
                oExt.iIdPiloto = ddlPiloto.SelectedItem.Value.S().I();
                oExt.iIdCopiloto = ddlCopiloto.SelectedItem.Value.S().I();
                oExt.iTipoSolicitud = ddlTipoSolicitud.SelectedItem.Value.S().I();
                oExt.iTipoOperacion = ddlTipoOperacion.SelectedItem.Value.S().I();
                oExt.sHorarioUTCcierre = txtTiempoAC.Text.S();
                oExt.iDuracion = txtMinutos.Text.S().I();
                oExt.sUTCLlegada = txtTLlegada.Text.S();
                oExt.sUTCSalida = txtTSalida.Text.S();
                oExt.sMotivo = txtMotivo.Text.S();
                oExt.sPuesto = txtPuesto.Text.S();
                oExt.iStatus = ddlEstatus.SelectedItem.Value.S().I();
                oExt.iIdCombustible = ddlCombustible.SelectedItem.Value.S().I();

                return oExt;
            }
            set
            {
                ExtensionServicios oExt = (ExtensionServicios)value;
                if (oExt != null)
                {
                    ddlOrigen_ItemsRequestedByFilterCondition((object)ddlEstacion, new ListEditItemsRequestedByFilterConditionEventArgs(0, 0, oExt.sEstacionICAO));
                    ddlOrigen_ItemsRequestedByFilterCondition((object)ddlOrigen, new ListEditItemsRequestedByFilterConditionEventArgs(0, 0, oExt.sOrigenICAO));
                    ddlOrigen_ItemsRequestedByFilterCondition((object)ddlDestino, new ListEditItemsRequestedByFilterConditionEventArgs(0, 0, oExt.sDestinoICAO));
                    ddlEstacion.SelectedIndex = 0;
                    ddlOrigen.SelectedIndex = 0;
                    ddlDestino.SelectedIndex = 0;
                    txtFechaVuelo.Value = oExt.DtFecha;
                    ddlMatricula.Value = oExt.iIdMatricula.S();
                    ddlPiloto.Value = oExt.iIdPiloto.S();
                    ddlCopiloto.Value = oExt.iIdCopiloto.S();
                    ddlTipoSolicitud.Value = oExt.iTipoSolicitud.S();
                    ddlTipoOperacion.Value = oExt.iTipoOperacion.S();
                    txtTiempoAC.Text = oExt.sHorarioUTCcierre;
                    txtMinutos.Text = oExt.iDuracion.S();
                    txtTLlegada.Text = oExt.sUTCLlegada;
                    txtTSalida.Text = oExt.sUTCSalida;
                    txtMotivo.Text = oExt.sMotivo;
                    txtPuesto.Text = oExt.sPuesto;
                    ddlEstatus.Visible = true;
                    lblEstatus.Visible = true;
                    ddlEstatus.SelectedIndex = ddlEstatus.Items.IndexOf(ddlEstatus.Items.FindByValue(oExt.iStatus.S()));
                    btnGuardar.Enabled = oExt.iStatus.S() != "3";
                    ddlCombustible.SelectedIndex =  ddlCombustible.Items.IndexOf(ddlCombustible.Items.FindByValue(oExt.iIdCombustible.S()));
                    
                    if(oExt.iStatus.S() == "3")
                        SetReadOnlyTrue();
                }
            }
        }
        #endregion




    }
}