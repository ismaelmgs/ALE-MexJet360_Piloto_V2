using ALE_MexJet.Clases;
using ALE_MexJet.ControlesUsuario;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using ALE_MexJet.Objetos;
using NucleoBase.Core;
using NucleoBase.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.Shared;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace ALE_MexJet.Views.Consultas
{
    public partial class frmReportePresupuesto : System.Web.UI.Page, IViewConsultaPresupuestos
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new ConsultaPresupuestos_Presenter(this, new DBConsultaPresupuestos());
            if (Request.QueryString.Count > 0)
            {
                string sPresupuestoRecibido = Request.QueryString["Presupuesto"];
                if (!string.IsNullOrEmpty(sPresupuestoRecibido))
                {
                    string sPresupuesto = Encoding.UTF8.GetString(Convert.FromBase64String(Request.QueryString["Presupuesto"]));
                    int iOp = Request.QueryString["Op"].S().I();
                    if(iOp == 0)
                    {
                        GenerarReporte(sPresupuesto.I());
                    }
                    else
                    {
                        EnviaPorCorreo(sPresupuesto.I());
                    }
                }
            }
        }
        #endregion Eventos

        #region Metodos
        private void GenerarReporte(int idPresupuesto)
        {
            try
            {
                iIdPresupuesto = idPresupuesto;
                eObjPresupuesto(null, EventArgs.Empty);

                ReportDocument rd = new ReportDocument();

                string strPath = string.Empty;
                strPath = Server.MapPath("CristalReport\\Presupuesto_CR.rpt");
                rd.Load(strPath);

                UserIdentity oUsuario = (UserIdentity)Session["UserIdentity"];

                DataTable DatosUsuario = new DataTable("Orders");
                DatosUsuario.Columns.Add("Nombre", Type.GetType("System.String"));
                DatosUsuario.Columns.Add("Correo", Type.GetType("System.String"));

                DataRow newRow = DatosUsuario.NewRow();
                newRow["Nombre"] = oUsuario.sName.S();
                newRow["Correo"] = oUsuario.sCorreoBaseUsuario.S();
                DatosUsuario.Rows.Add(newRow);

                DataSet dsPresupuesto = (DataSet)ViewState["dsPresupuesto"];

                if (dsPresupuesto.Tables[0].Rows.Count > 0)
                {
                    rd.Database.Tables["Presupuestos"].SetDataSource(dsPresupuesto.Tables[0]);
                    rd.Database.Tables["ServiciosCargoPresupuestos"].SetDataSource(dsPresupuesto.Tables[1]);
                    rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D"].SetDataSource(dsPresupuesto.Tables[2]);
                    rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D1"].SetDataSource(dsPresupuesto.Tables[3]);
                    rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D2"].SetDataSource(dsPresupuesto.Tables[4]);
                    rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D3"].SetDataSource(dsPresupuesto.Tables[5]);
                    rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D4"].SetDataSource(dsPresupuesto.Tables[6]);
                    rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D5"].SetDataSource(dsPresupuesto.Tables[7]);
                    rd.Database.Tables["ServiciosVueloConceptosPresupuesto_H"].SetDataSource(dsPresupuesto.Tables[8]);
                    rd.Database.Tables["TramosPresupuesto"].SetDataSource(dsPresupuesto.Tables[9]);
                    rd.Database.Tables["TotalHrsDescontar"].SetDataSource(dsPresupuesto.Tables[10]);
                    rd.Database.Tables["DatosUsuario"].SetDataSource(DatosUsuario);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Presupuesto " + idPresupuesto);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EnviaPorCorreo(int idPresupuesto)
        {
            iIdPresupuesto = idPresupuesto;
            eObjPresupuesto(null, EventArgs.Empty);

            ReportDocument rd = new ReportDocument();

            string strPath = string.Empty;
            strPath = Server.MapPath("CristalReport\\Presupuesto_CR.rpt");
            rd.Load(strPath);

            UserIdentity oUsuario = (UserIdentity)Session["UserIdentity"];

            DataTable DatosUsuario = new DataTable("Orders");
            DatosUsuario.Columns.Add("Nombre", Type.GetType("System.String"));
            DatosUsuario.Columns.Add("Correo", Type.GetType("System.String"));

            DataRow newRow = DatosUsuario.NewRow();
            newRow["Nombre"] = oUsuario.sName.S();
            newRow["Correo"] = oUsuario.sCorreoBaseUsuario.S();
            DatosUsuario.Rows.Add(newRow);

            DataSet dsPresupuesto = (DataSet)ViewState["dsPresupuesto"];

            if (dsPresupuesto.Tables[0].Rows.Count > 0)
            {
                rd.Database.Tables["Presupuestos"].SetDataSource(dsPresupuesto.Tables[0]);
                rd.Database.Tables["ServiciosCargoPresupuestos"].SetDataSource(dsPresupuesto.Tables[1]);
                rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D"].SetDataSource(dsPresupuesto.Tables[2]);
                rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D1"].SetDataSource(dsPresupuesto.Tables[3]);
                rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D2"].SetDataSource(dsPresupuesto.Tables[4]);
                rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D3"].SetDataSource(dsPresupuesto.Tables[5]);
                rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D4"].SetDataSource(dsPresupuesto.Tables[6]);
                rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D5"].SetDataSource(dsPresupuesto.Tables[7]);
                rd.Database.Tables["ServiciosVueloConceptosPresupuesto_H"].SetDataSource(dsPresupuesto.Tables[8]);
                rd.Database.Tables["TramosPresupuesto"].SetDataSource(dsPresupuesto.Tables[9]);
                rd.Database.Tables["TotalHrsDescontar"].SetDataSource(dsPresupuesto.Tables[10]);
                rd.Database.Tables["DatosUsuario"].SetDataSource(DatosUsuario);
                Stream strmReporte = rd.ExportToStream(ExportFormatType.PortableDocFormat);
                
                string sNombreUsuario = dsPresupuesto.Tables[0].Rows[0]["NombreSolicitante"].S();
                if (strmReporte != null)
                {
                    Stream sAdjunto = strmReporte;
                    string Mensaje = string.Empty;
                    string Vuelo = string.Empty;
                    string From = dsPresupuesto.Tables[0].Rows[0]["Email"].S();
                    string CC = string.Empty;

                    Vuelo = Vuelo + " </table> ";
                    Mensaje = Mensaje + Vuelo;

                    string scorreo = Utils.ObtieneParametroPorClave("4");
                    string sPass = Utils.ObtieneParametroPorClave("5");
                    string sservidor = Utils.ObtieneParametroPorClave("6");
                    string spuerto = Utils.ObtieneParametroPorClave("7");

                    Mensaje = "<table width='800' border='0' cellspacing='0' cellpadding='10' align='center' bgcolor='#f8f8f8' style='border-radius: 20px;'>" +
                                  "<tr>" +
                                    "<td>Estimado (a): <strong> Solicitante </strong></td>" +
                                  "</tr>" +
                                  "<tr>" +
                                    "<td>&nbsp;</td>" +
                                  "</tr>" +
                                  "<tr>" +
                                    "<td><p>Adjunto encontrará la cotización correspondiente a su solicitud de vuelo.</p>" +
                                    "<p>Cualquier duda o aclaración, por favor comuníquese con su asesor de Atención a clientes.</p></td>" +
                                  "</tr>" +
                                  "<tr>" +
                                    "<td>&nbsp;</td>" +
                                  "</tr>" +
                                  "<tr>" +
                                    "<td><p>Atentamente:</p>" +
                                    "<p><strong>Aerolíneas Ejecutivas S.A de C.V.</strong></p></td>" +
                                  "</tr>" +
                                "</table>";
                    if (sNombreUsuario != string.Empty)
                    {
                        Mensaje = Mensaje.Replace("Solicitante", sNombreUsuario);
                    }
                    Utils.EnvioCorreo(sservidor, spuerto.S().I(), "Cotización con folio: " + idPresupuesto.S(), From, Mensaje, sAdjunto, scorreo, sPass, CC, "Solicitud de cotización");
                }
                Response.Redirect("~/Views/Consultas/frmConsultaPresupuestos.aspx");
            }
        }
        public void CargaDSPresupuesto(DataSet dsPresupuesto)
        {
            ViewState["dsPresupuesto"] = dsPresupuesto;
        }

        public void CargaGrid(DataTable DT)
        {
            throw new NotImplementedException();
        }

        public void LoadClientes(DataTable dtObjCat)
        {
            throw new NotImplementedException();
        }

        public void LoadContratos(DataTable dtObjCat)
        {
            throw new NotImplementedException();
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            throw new NotImplementedException();
        }

        public void ObtieneValores()
        {
            throw new NotImplementedException();
        }
        #endregion Metodos

        #region Variables
        ConsultaPresupuestos_Presenter oPresenter;
        private const string sClase = "frmReportePresupuesto.aspx.cs";
        private const string sPagina = "frmReportePresupuesto.aspx";

        public Enumeraciones.TipoOperacion eCrud
        {
            get { return (Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }

        public int iIdPresupuesto
        {
            get { return ViewState["IdPresupuesto"].I(); }
            set { ViewState["IdPresupuesto"] = value; }
        }

        public object[] oArrFiltroContratos
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public object[] oArrFiltros
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public object oCrud
        {
            get { return ViewState["CrudPresupuesto"]; }
            set { ViewState["CrudPresupuesto"] = value; }
        }

        public event EventHandler eDeleteObj;
        public event EventHandler eNewObj;
        public event EventHandler eObjCliente;
        public event EventHandler eObjContrato;
        public event EventHandler eObjPresupuesto;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eSearchObj;
        #endregion Variables
    }
}