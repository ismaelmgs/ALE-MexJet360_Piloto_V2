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
using System.Collections.Specialized;


namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class AeronaveRen : System.Web.UI.Page, IViewAeronaveRen
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvAeronave.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
            oPresenter = new AeronaveRen_Presenter(this, new DBAeronaveRen());

            if (!IsPostBack)
            {
                if (eSearchObj != null)
                    eSearchObj(null,null);
            }

            RecargaGrid();
        }

        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
         //       Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdatePanel1_Unload", "Aviso");
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //LoadCorreoAlta();
            Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
        }

        #region METODOS
        public void LoadAeronaveRen(DataTable dtObjCat)
        {
            gvAeronave.DataSource = dtObjCat;
            gvAeronave.DataBind();
            ViewState["gvAeronave"] = dtObjCat;
        }
        protected void RecargaGrid()
        {
            DataTable DT = (DataTable)ViewState["gvAeronave"];
            if (DT != null)
            {
                gvAeronave.DataSource = DT;
                gvAeronave.DataBind();
            }
        }

        public void LoadCorreoAlta()
        {
            try
            {
                string Mensaje = string.Empty;
                
                gvAeronave.DataBind();
                DataTable table = (DataTable)gvAeronave.DataSource;
                if (table == null || table.Rows.Count == 0)
                    return;

                int columnscount = table.Columns.Count;

                Mensaje += "<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'><TR>";

                for (int j = 0; j < columnscount; j++)
                {
                    Mensaje += "<Td><B>";
                    Mensaje += gvAeronave.Columns[j].Caption.S();
                    Mensaje += "</B></Td>";
                }
                Mensaje += "</TR>";

                foreach (DataRow row in table.Rows)
                {
                    Mensaje += "<TR>";
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        Mensaje += "<Td>";
                        Mensaje += row[i].ToString();
                        Mensaje += "</Td>";
                    }
                    Mensaje += "</TR>";
                }
             
                //string scorreo = Utils.ObtieneParametroPorClave("4");
                //string sPass = Utils.ObtieneParametroPorClave("5");
                //string sservidor = Utils.ObtieneParametroPorClave("6");
                //string spuerto = Utils.ObtieneParametroPorClave("7");

                
                string Vuelo = string.Empty;
                string From = "raulm@backandfront.com.mx";
                string CC = string.Empty;


                string scorreo = "mexjet123@gmail.com";
                string sPass = "a1a1b2b2";
                string sservidor = "SMTP.GMAIL.COM";
                string spuerto = "25";

                Utils.EnvioCorreo(sservidor, spuerto.S().I(), "VUELOS RENTADOS A TERCEROS", From, Mensaje, "", scorreo, sPass, CC, "Atención a Clientes");
            }
            catch (Exception x) { throw x; }
        }
        #endregion
        #region VARIABLES
        AeronaveRen_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public object oCrud
        {
            get { return ViewState["CrudPiloto"]; }
            set { ViewState["CrudPiloto"] = value; }
        }

        public object[] oArrFiltros
        {
            get
            {
                return new object[]
                { 
                    
                    "@FECHAINICIO",dFechaIni.Text != string.Empty ? dFechaIni.Date.ToString("MM-dd-yyyy") : string.Empty,
                    "@FECHAFIN",dFechaFin.Text != string.Empty ? dFechaFin.Date.ToString("MM-dd-yyyy") : string.Empty,
                    "@MATRICULA", "%"  + txtTextoBusqueda.Text + "%"
                };
            }
        }
        #endregion

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            if (eSearchObj != null)
                eSearchObj(null, null);
        }
    }
}