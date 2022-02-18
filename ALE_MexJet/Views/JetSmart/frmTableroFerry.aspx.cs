using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using DevExpress.Web;
using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using System.Drawing;
using System.Text;
using System.IO;

namespace ALE_MexJet.Views.JetSmart
{
    public partial class frmTableroFerry : System.Web.UI.Page, IViewTableroFerry
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {            
            mpeMensaje.OkButtonPressed += mpeMensaje_OkButtonPressed;
            oPresenter = new TableroFerry_Presenter(this, new DBTableroFerry());

            if (!IsPostBack)
            {
                sRgbG = Utils.GetParametrosClave("88");
                sRgbM = Utils.GetParametrosClave("89");
                sRgbP = Utils.GetParametrosClave("90");
            }
        }
        private void mpeMensaje_OkButtonPressed(object sender, EventArgs args)
        {

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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso");
            }
        }
        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
        protected void gvFerrys_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ASPxComboBox ddlJet = (ASPxComboBox)e.Row.FindControl("ddlSmart");
                    if (ddlJet != null)
                    {
                        ddlJet.Value = dtFerrys.Rows[e.Row.RowIndex]["StatusJetSmart"].S();
                    }

                    ASPxComboBox ddlApp = (ASPxComboBox)e.Row.FindControl("ddlApp");
                    if (ddlApp != null)
                    {
                        ddlApp.Value = dtFerrys.Rows[e.Row.RowIndex]["StatusApp"].S();
                    }
                    ASPxComboBox ddlEz = (ASPxComboBox)e.Row.FindControl("ddlEz");
                    if (ddlEz != null)
                    {
                        ddlEz.Value = dtFerrys.Rows[e.Row.RowIndex]["StatusEZ"].S();
                    }



                    if (dtFerrys.Rows[e.Row.RowIndex]["Diferencia"].S().I() <= 36)
                    {
                        string hex = sRgbP;
                        Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
                        e.Row.BackColor = _color;

                        ASPxButton btn = (ASPxButton)e.Row.FindControl("btnGuardar");
                        if (btn != null)
                        {
                            btn.Enabled = false;
                        }
                    }
                    if (dtFerrys.Rows[e.Row.RowIndex]["Diferencia"].S().I() > 36 && dtFerrys.Rows[e.Row.RowIndex]["Diferencia"].S().I() <= 48)
                    {
                        string hex = sRgbM;
                        Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
                        e.Row.BackColor = _color;
                    }
                    if (dtFerrys.Rows[e.Row.RowIndex]["Diferencia"].S().I() > 48)
                    {
                        string hex = sRgbG;
                        Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
                        e.Row.BackColor = _color;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {

        }
        protected void gvFerrys_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Guardar")
            {
                int iIndex = e.CommandArgument.S().I();
                TableroFerry oF = new TableroFerry();

                oF.iIdFerry = dtFerrys.Rows[iIndex]["IdFerry"].S().I();

                ASPxComboBox ddlJet = (ASPxComboBox)gvFerrys.Rows[iIndex].FindControl("ddlSmart");
                if(ddlJet != null)
                    oF.iStatusJetSmart = ddlJet.Value.S().I();
                
                ASPxComboBox ddlApp = (ASPxComboBox)gvFerrys.Rows[iIndex].FindControl("ddlApp");
                if(ddlApp != null)
                    oF.iStatusApp = ddlApp.Value.S().I();

                oTFerry = oF;
                // Lo comentamos momentariamente ,,, Descomentar despues de esta prueba 10/05/2016                
                if (eSaveObj != null)
                    eSaveObj(sender, e);
                             
                if (oF.iStatusJetSmart == 0)
                {
                    // Validamos si ya estaba cancelado anteriormente para no enviar el correo nuevamente
                    DataTable dtOF = new DataTable();                    
                    dtOF = (DataTable)ViewState["Ferrys"];

                    bool bStatusJM = false;
                    

                    foreach (DataRow row in dtOF.Rows)
                    {
                        if (oTFerry.iIdFerry == row["idFerry"].S().I())
                        {
                            if (row["StatusJetSmart"].S().I() != 0)
                            {
                                bStatusJM = true;
                                break;
                            }
                        }                        
                    }
                    if (bStatusJM)
                    {
                        List<OfertaFerry> oLst = CargaCadena();
                        string cadena = ArmaCSV(oLst);
                        CreaCSV(cadena);
                        // Enviamos el correo de cancelacion a JetSMart
                        enviaCorreoCancelacionJetSmarter();
                    }
                    
                }
                
                if(oF.iStatusApp == 0)
                {
                    // Validamos si ya estaba cancelado anteriormente para no enviar el correo nuevamente
                    DataTable dtOF = new DataTable();
                    dtOF = (DataTable)ViewState["Ferrys"];
                    bool bStatusAPP = false;

                    foreach (DataRow row in dtOF.Rows)
                    {
                        if (oTFerry.iIdFerry == row["idFerry"].S().I())
                        {
                            if (row["StatusApp"].S().I() != 0)
                                bStatusAPP = true;
                        }
                        break;
                    }
                    if (bStatusAPP)
                    {
                        DBVentaFerry dbFerry = new DBVentaFerry();
                        // Enviamos el Json de Cancelacion APP
                        string sUrl = Utils.ObtieneParametroPorClave("94");
                        JsonObjeto json = Utils.NotificaAppMovilCancelacion(sUrl, 1, "Cancelled");
                        if (json.iId._I()!=0)
                            dbFerry.DBSaveControlServicio(json.iId._I(), json.sCadenaRequestCancela, json.sCadenaResponseCancela);
                            //dbFerry.DBUpdateControlServicio(json.iId._I(), json.sCadenaRequestCancela, json.sCadenaResponseCancela);
                    }
                }
            }
        }
        public void CreaCSV(string cadena)
        {
            sCadena = cadena;
            //tmDescarga.Enabled = true;
        }
        #endregion

        #region METODOS
        public void LoadFerrys(DataTable dt)
        {
            dtFerrys = dt;
            gvFerrys.DataSource = dt;
            ViewState["Ferrys"] = dt;
            gvFerrys.DataBind();
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            mpeMensaje.ShowMessage(sMensaje, sCaption);
        }

        public void enviaCorreoCancelacionJetSmarter()
        {
            try
            {
                //Exportamos el CVS ...
                StringBuilder tmpCSV = new StringBuilder();

                string strPath = string.Empty;
                strPath = Server.MapPath("~\\App_Data\\UploadTemp\\OfertaFerrys.csv");

                using (StreamWriter sw = new StreamWriter(@strPath, false, System.Text.Encoding.UTF8))
                {
                    sw.Write(sCadena);                    
                    sw.Close();
                }

                //Cargamos el archivo en memoria ...               
                byte[] MyData = (byte[])System.IO.File.ReadAllBytes(@strPath);
                MemoryStream strmArchivo = new MemoryStream(MyData);

                //Stream sAdjunto = strmArchivo;
                string Mensaje = string.Empty;
                string Vuelo = string.Empty;
                string From = Utils.ObtieneParametroPorClave("92");
                string CC = string.Empty;

                Vuelo = Vuelo + " </table> ";
                Mensaje = Mensaje + Vuelo;

                string scorreo = Utils.ObtieneParametroPorClave("4");
                string sPass = Utils.ObtieneParametroPorClave("5");
                string sservidor = Utils.ObtieneParametroPorClave("6");
                string spuerto = Utils.ObtieneParametroPorClave("7");

                Mensaje = "<table width='800' border='0' cellspacing='0' cellpadding='10' align='center' bgcolor='#f8f8f8' style='border-radius: 20px;'>" +
                              "<tr><td><strong> This is an empty leg cancellation message </strong></td></tr>" +
                              "<tr><td>&nbsp;</td></tr>" +
                          "</table>";

                Utils.EnvioCorreo(sservidor, spuerto.S().I(), "This is an empty leg cancellation message", From, Mensaje, strmArchivo, scorreo, sPass, CC, "Oferta Ferrys: ");

                // Validamos si existe el archivo y lo eliminamos, para no cargar con muchos archivos el servidor
                if (System.IO.File.Exists(strPath))
                    System.IO.File.Delete(strPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       // protected static int iIndexFerry;
        public List<OfertaFerry> CargaCadena()
        {
         
           DataTable dt = new DataTable();
           dt = (DataTable)ViewState["Ferrys"];

           List<OfertaFerry> lst = new List<OfertaFerry>();
           OfertaFerry oF = new OfertaFerry();

           foreach (DataRow row in dt.Rows)
           {
               if (oTFerry.iIdFerry == row["IdFerry"].S().I())
               {
                   //oF.iIdFerry = row["IdFerry"].S().I();
                   //oF.iTrip = row["Trip"].S().I();
                   //oF.iNoPierna = row["NoPierna"].S().I();
                   //oF.sOrigen = row["Origen"].S();
                   //oF.dtFechaSalida = row["FechaSalidaB"].S().Dt();
                   //oF.sFechaSalida = row["FechaSalidaA"].S();                   
                   //oF.sDestino = row["Destino"].S();
                   //oF.dtFechaLlegada = row["FechaLlegadaB"].S().Dt();
                   //oF.sFechaLlegada = row["FechaLlegadaA"].S();
                   //oF.sMatricula    =row["Matricula"].S();
                   //oF.sTiempoVuelo = row["TiempoVuelo"].S();
                   //oF.sGrupoModelo = row["GrupoModelo"].S();
                   //oF.iLegId = row["LegId"].S().I();
                   //lst.Add(oF);
               }                              
           }
           return lst;
        }

        private string ArmaCSV(List<OfertaFerry> oLst)
        {
            string cadena = string.Empty;
            foreach (OfertaFerry oF in oLst)
            {
                //cadena +=
                //        oF.sOrigen + "," +
                //        oF.sDestino + "," +
                //        oF.sFechaSalida + ":00" + "," +
                //        oF.sFechaLlegada + ":00" + "," +
                //        Math.Round(Utils.ConvierteTiempoaDecimal(oF.sTiempoVuelo), 2).ToString().Replace(",", ".") + "," +                        
                //        oF.sGrupoModelo + "," +
                //        oF.sMatricula + "," +
                //        oF.iLegId + "," +
                //        oF.sReferencia;                        
                //    cadena += "\r\n";
            }

            return cadena;
        }

        #endregion

        #region VARIABLES Y PROPIEDAES

        TableroFerry_Presenter oPresenter;
        private const string sPagina = "frmTableroFerry.aspx";
        private const string sClase = "frmTableroFerry.aspx.cs";

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public object[] oArrFiltros
        {
            get
            {
                return new object[]
                { 
                    "@FechaInicio", dFechaIni.Value.S().Dt(),
                    "@FechaFin", dFechaFin.Value.S().Dt()
                };
            }
        }
        public DataTable dtFerrys
        {
            get { return (DataTable)ViewState["VSFerrys"]; }
            set { ViewState["VSFerrys"] = value; }
        }
        public TableroFerry oTFerry
        {
            get { return (TableroFerry)ViewState["VSTFerry"]; }
            set { ViewState["VSTFerry"] = value; }
        }

        public string sCadena
        {
            get { return ViewState["VSCadena"].S(); }
            set { ViewState["VSCadena"] = value; }
        }
        public string sRgbG
        {
            get { return ViewState["VSRGBG"].S(); }
            set { ViewState["VSRGBG"] = value; }
        }
        public string sRgbM
        {
            get { return ViewState["VSRGBM"].S(); }
            set { ViewState["VSRGBM"] = value; }
        }
        public string sRgbP
        {
            get { return ViewState["VSRGBP"].S(); }
            set { ViewState["VSRGBP"] = value; }
        }

        #endregion


       

    }
}