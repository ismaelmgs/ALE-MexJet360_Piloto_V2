using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;

using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;

using NucleoBase.Core;
using System.Globalization;
using ALE_MexJet.Clases;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ALE_MexJet.Views.FBO
{
    public partial class frmConsultaVuelosUSA : System.Web.UI.Page, IViewConsultaVuelos
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new ConsultaVuelos_Presenter(this, new DBConsultaVuelos());

            if (!IsPostBack)
            {
                DateTime dtFechaActual = DateTime.Now;
                sFechaActual = dtFechaActual.Day.S().PadLeft(2, '0') + dtFechaActual.Month.S().PadLeft(2, '0') + dtFechaActual.Year.S();
                txtBusqueda.Text = string.Format("{0:yyyy-MM-dd}", dtFechaActual);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                sFecha = txtBusqueda.Text;
                dtVuelosUSA = null;

                if (eSearchVuelos != null)
                    eSearchVuelos(sender, e);

                if (eSearchParametros != null)
                    eSearchParametros(sender, e);

                if (dtVuelosUSA != null && dtVuelosUSA.Rows.Count > 0)
                {
                    gvVuelosUSA.DataSource = dtVuelosUSA;
                    gvVuelosUSA.DataBind();
                }
                else
                {
                    gvVuelosUSA.DataSource = null;
                    gvVuelosUSA.DataBind();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "Verificar");
            }
        }

        protected void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                int iCountSuccess = 0;
                foreach (GridViewRow row in gvVuelosUSA.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkVuelo");

                    if (chk != null)
                    {
                        if (chk.Checked)
                        {
                            int index = row.RowIndex;
                            string sNumVuelo = gvVuelosUSA.Rows[index].Cells[1].Text;
                            string sEdo = gvVuelosUSA.Rows[index].Cells[2].Text;
                            string sCdOrigen = gvVuelosUSA.Rows[index].Cells[3].Text;
                            string sCdDestino = gvVuelosUSA.Rows[index].Cells[4].Text;
                            string sOrigen = gvVuelosUSA.Rows[index].Cells[5].Text;
                            string sDestino = gvVuelosUSA.Rows[index].Cells[6].Text;
                            string sOrigenCalzo = gvVuelosUSA.Rows[index].Cells[7].Text;
                            string sDestinoCalzo = gvVuelosUSA.Rows[index].Cells[8].Text;
                            string sPasajeros = gvVuelosUSA.Rows[index].Cells[9].Text;
                            string sMatricula = gvVuelosUSA.Rows[index].Cells[10].Text;
                            string sTipoAvion = gvVuelosUSA.Rows[index].Cells[11].Text;
                            string sPiloto = gvVuelosUSA.Rows[index].Cells[12].Text;
                            string sContrato = gvVuelosUSA.Rows[index].Cells[13].Text;

                            DataTable dtDatos = new DataTable();
                            dtDatos = CrearConjuntoDatos(sEdo, sCdOrigen, sCdDestino, sOrigen, sDestino, sOrigenCalzo, sDestinoCalzo, sPasajeros, sNumVuelo, sMatricula, sTipoAvion, sPiloto, sContrato);

                            if (dtDatos != null && dtDatos.Rows.Count > 0)
                            {
                                GenerarReporte(dtDatos);
                                iCountSuccess += 1;
                            }
                        }
                    }
                }

                System.Threading.Thread.Sleep(5000);
                // iNICIAR DESCARGA
                DownloadFiles();

                //if (iCountSuccess > 0)
                //    MostrarMensaje("Se han creado " + iCountSuccess.S() + " Reportes de Vuelo, en la ruta " + ConfigurationManager.AppSettings["PathConsultaVuelos"].S(), "Correcto");
                //else
                //    MostrarMensaje("No ha seleccionado ningún vuelo para generar reporte.", "Verificar");
                
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }
        #endregion

        #region MÉTODOS

        public void LoadVuelos(DataTable dt)
        {
            try
            {
                dtVuelosUSA = null;
                dtVuelosUSA = dt;

                if (dtVuelosUSA != null && dtVuelosUSA.Rows.Count > 0)
                    btnGenerarReporte.Visible = true;
                else
                    btnGenerarReporte.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadParametros(DataTable dt) 
        {
            try
            {
                dtParametrosALE = null;
                dtParametrosALE = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CrearConjuntoDatos(string sEdo, string sCdOri, string sCdDes, string sOri, string sDes, string sOriCal, string sDesCal, string sPax, string sNumVuelo, string sMatricula, string sTipoAvion, string sPiloto, string sContrato) 
        {
            try
            {
                DataTable dtResult = new DataTable();
                dtResult.Columns.Add("Estado");
                dtResult.Columns.Add("CiudadOrigen");
                dtResult.Columns.Add("CiudadDestino");
                dtResult.Columns.Add("Origen");
                dtResult.Columns.Add("Destino");
                dtResult.Columns.Add("OrigenCalzo");
                dtResult.Columns.Add("OrigenCalzoFormatter");
                dtResult.Columns.Add("DestinoCalzo");
                dtResult.Columns.Add("DestinoCalzoFormatter");
                dtResult.Columns.Add("CantPax", typeof(int));
                dtResult.Columns.Add("Trip");
                dtResult.Columns.Add("ContactoEmpresa");
                dtResult.Columns.Add("TelefonoContactoEmpresa");
                dtResult.Columns.Add("Matricula");
                dtResult.Columns.Add("TipoAvion");
                dtResult.Columns.Add("Piloto");
                dtResult.Columns.Add("Tripulacion");
                dtResult.Columns.Add("Contrato");

                DataRow dr = dtResult.NewRow();
                dr["Estado"] = sEdo;
                dr["CiudadOrigen"] = sCdOri;
                dr["CiudadDestino"] = sCdDes;
                dr["Origen"] = sOri;
                dr["Destino"] = sDes;
                dr["OrigenCalzo"] = sOriCal.Dt().ToString("MM/dd/yyyy");
                dr["OrigenCalzoFormatter"] = sOriCal.Dt().ToString("MM/dd HH:mm");
                dr["DestinoCalzo"] = sDesCal.Dt().ToString("MM/dd/yyyy");
                dr["DestinoCalzoFormatter"] = sDesCal.Dt().ToString("MM/dd HH:mm");
                dr["CantPax"] = sPax.I();
                dr["Trip"] = sNumVuelo;
                dr["ContactoEmpresa"] = ConfigurationManager.AppSettings["ContactoEmpresa"].S();
                dr["TelefonoContactoEmpresa"] = ConfigurationManager.AppSettings["TelefonoContactoEmpresa"].S();
                dr["Matricula"] = sMatricula;
                dr["TipoAvion"] = sTipoAvion;
                dr["Piloto"] = sPiloto;
                dr["Tripulacion"] = ConfigurationManager.AppSettings["Tripulacion"].S();
                dr["Contrato"] = sContrato;

                dtResult.Rows.Add(dr);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CrearConjuntoParametros(DataTable dtSRCParam) 
        {
            try
            {
                DataTable dtResult = new DataTable();
                dtResult.Columns.Add("TelefonoALE");
                dtResult.Columns.Add("TelefonoDirectoALE");

                DataRow dr = dtResult.NewRow();
                dr["TelefonoALE"] = dtSRCParam.Rows[0]["Valor"].S();
                dr["TelefonoDirectoALE"] = dtSRCParam.Rows[1]["Valor"].S();
                dtResult.Rows.Add(dr);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GenerarReporte(DataTable dtDataSource)
        {
            try
            {
                string sRutaArchivos = System.Configuration.ConfigurationManager.AppSettings["RutaTempArchivos"].S();
                //ConfigurationManager.AppSettings["PathConsultaVuelos"] = sRutaArchivos;
                DataSet dsDatos = new DataSet();
                DataTable dtParam = new DataTable();
                dtDataSource.TableName = "DatosVuelo";
                dtDataSource.AcceptChanges();
                dsDatos.Tables.Add(dtDataSource);

                dtParam = CrearConjuntoParametros(dtParametrosALE);
                dtParam.TableName = "DatosParametrosALE";
                dtParam.AcceptChanges();
                dsDatos.Tables.Add(dtParam);

                rd.Load(HttpContext.Current.Server.MapPath("~/Views/FBO/CrystalReports") + @"/ReporteVuelosUSA.rpt");
                rd.FileName = HttpContext.Current.Server.MapPath("~/Views/FBO/CrystalReports") + @"/ReporteVuelosUSA.rpt";
                rd.SetDataSource(dsDatos);
                crv.ReportSource = rd;

                //Exportar a pdf
                //ExportFormatType formatType = ExportFormatType.NoFormat;
                //formatType = ExportFormatType.PortableDocFormat;
                //rd.ExportToHttpResponse(formatType, Response, true, "ReporteVuelo");
                //Response.End();
                string sPathSave = sRutaArchivos + "Reporte Vuelo " + dtDataSource.Rows[0]["Trip"].S() + " (" + dtDataSource.Rows[0]["Origen"].S() + "_" + dtDataSource.Rows[0]["Destino"].S() + ")" + ".pdf";

                if (Directory.Exists(sRutaArchivos))
                {
                    rd.ExportToDisk(ExportFormatType.PortableDocFormat, sPathSave);
                }
                else
                {
                    Directory.CreateDirectory(sRutaArchivos);
                    rd.ExportToDisk(ExportFormatType.PortableDocFormat, sPathSave);

                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        private void DownloadFiles()
        {
            string saveTo = System.Configuration.ConfigurationManager.AppSettings["RutaTempArchivos"].S();
            string[] sfiles = Directory.GetFiles(saveTo);

            foreach (string f in sfiles)
            {
                //System.IO.MemoryStream stream = new MemoryStream();
                //using (System.IO.FileStream file = new System.IO.FileStream(f, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                //{
                //    stream.WriteTo(file);
                //}

                string sNombre = Path.GetFileName(f);

                if (System.IO.File.Exists(f))
                {
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + sNombre);
                    Response.Charset = "";
                    try
                    {
                        Response.WriteFile(f);
                        Response.Flush();
                        System.IO.File.Delete(f);
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                        throw;
                    }
                    finally
                    {
                        //Response.End();
                        DownloadFiles();
                    }
                }
            }

        }

        #endregion

        #region VARIABLES Y PROPIEDADES

        ConsultaVuelos_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchVuelos;
        public event EventHandler eSearchParametros;

        ReportDocument rd = new ReportDocument();

        public DataTable dtVuelosUSA
        {
            get { return (System.Data.DataTable)ViewState["VSVuelosUSA"]; }
            set { ViewState["VSVuelosUSA"] = value; }
        }
        public DataTable dtParametrosALE
        {
            get { return (System.Data.DataTable)ViewState["VSParametrosALE"]; }
            set { ViewState["VSParametrosALE"] = value; }
        }
        public string sFechaActual
        {
            get { return ViewState["VSFechaActual"].S(); }
            set { ViewState["VSFechaActual"] = value; }
        }
        public string sFecha 
        {
            get { return ViewState["VSFecha"].S(); }
            set { ViewState["VSFecha"] = value; }
        }
        #endregion

        

        
    }
}