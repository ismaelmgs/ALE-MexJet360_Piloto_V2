using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using DevExpress.Web;
using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALE_MexJet.Views.viaticos
{
    public partial class frmCalculoPagos : System.Web.UI.Page, IViewCalculoPagos
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new CalculoPagos_Presenter(this, new DBCalculoPagos());
            gvCalculo.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvCalculo.SettingsPager.ShowDisabledButtons = true;
            gvCalculo.SettingsPager.ShowNumericButtons = true;
            gvCalculo.SettingsPager.ShowSeparators = true;
            gvCalculo.SettingsPager.Summary.Visible = true;
            gvCalculo.SettingsPager.PageSizeItemSettings.Visible = true;
            gvCalculo.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvCalculo.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";

            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(date1.Text))
                    sFechaDesde = DateTime.Now.ToShortDateString();
                if (string.IsNullOrEmpty(date2.Text))
                    sFechaHasta = DateTime.Now.ToShortDateString();

                sParametro = txtParametro.Text;

                if (eSearchObj != null)
                    eSearchObj(sender, e);

                if (eSearchConceptos != null)
                    eSearchConceptos(sender, e);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                sFechaDesde = date1.Text;
                sFechaHasta = date2.Text;
                sParametro = txtParametro.Text;

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvCalculo_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Ver")
                {
                    int index = e.VisibleIndex.I();
                    int iIdBitacora = gvCalculo.GetRowValues(index, "idBitacora").S().I();
                    string[] fieldValues = { "Piloto", "cvePiloto", "Fecha1", "Fecha2", "idBitacora" };
                    object obj = gvCalculo.GetRowValues(index, fieldValues);
                    object[] oB = (object[])obj;

                    if (oB.Length > 0)
                    {
                        hdnIdBitacora.Value = iIdBitacora.S();
                        readPiloto.Text = oB[0].S();
                        readCvePiloto.Text = oB[1].S();
                        readFechaInicio.Text = oB[2].S().Dt().ToShortDateString();
                        readFechaFin.Text = oB[3].S().Dt().ToShortDateString();

                        if (eSearchCalculos != null)
                            eSearchCalculos(sender, e);

                        sParametro = hdnIdBitacora.Value.S();
                        //if (eSearchVuelos != null)
                        //    eSearchVuelos(sender, e);

                        string sPlantilla = CalcularViaticos();
                        divViaticos.InnerHtml = sPlantilla;

                        pnlBusqueda.Visible = false;
                        pnlVuelos.Visible = false;
                        pnlCalcularViaticos.Visible = true;
                        upaVuelos.Update();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CalcularViaticos()
        {
            try
            {
                string sHtml = string.Empty;
                string sTablaNacional = string.Empty;
                string sTablaExtranjera = string.Empty;

                for (int x = 0; x < dtVuelosXPiloto.Rows.Count; x++)
                {
                    string sHrCheckIn = string.Empty;
                    string sHrCheckOut = string.Empty;
                    string[] sArrCheckIn = null;
                    string[] sArrCheckOut = null;
                    TimeSpan tsCheckInMXN = new TimeSpan();
                    TimeSpan tsCheckOutMXN = new TimeSpan();
                    TimeSpan tsCheckInUSD;
                    TimeSpan tsCheckOutUSD;
                    string sCvePais = string.Empty;

                    sArrCheckIn = dtVuelosXPiloto.Rows[x]["CheckIn"].S().Split(' ');
                    sArrCheckOut = dtVuelosXPiloto.Rows[x]["CheckOut"].S().Split(' ');
                    sCvePais = dtVuelosXPiloto.Rows[x]["CvePaisDestino"].S();

                    if (dtConceptos != null && dtConceptos.Rows.Count > 0) 
                    {
                        if(dtConceptos.Columns.Contains("CantidadMXN") == false)
                            dtConceptos.Columns.Add("CantidadMXN");

                        if (dtConceptos.Columns.Contains("CantidadUSD") == false)
                            dtConceptos.Columns.Add("CantidadUSD");

                        string sTimeIni = string.Empty;
                        string sTimeFin = string.Empty;
                        string sConcepto = string.Empty;

                        decimal dMontoMXN = 0;
                        decimal dMontoUSD = 0;

                        TimeSpan tsIni;
                        TimeSpan tsFin;

                        for (int i = 0; i < dtConceptos.Rows.Count; i++)
                        {
                            sConcepto = dtConceptos.Rows[i]["DesConcepto"].S();
                            sTimeIni = dtConceptos.Rows[i]["HoraInicial"].S().Substring(0, 5);
                            sTimeFin = dtConceptos.Rows[i]["HoraFinal"].S().Substring(0, 5);
                            dMontoMXN = dtConceptos.Rows[i]["MontoMXN"].S().D();
                            dMontoUSD = dtConceptos.Rows[i]["MontoUSD"].S().D();
                            TimeSpan timeIni = TimeSpan.Parse(sTimeIni);
                            TimeSpan timeFin = TimeSpan.Parse(sTimeFin);

                            int iCountMXN = 0;
                            int iCountUSD = 0;
                            decimal dTotalMXN = 0;
                            decimal dTotalUSD = 0;

                            #region Viáticos Nacionales
                            if (sCvePais == "MX")
                            {
                                if (sArrCheckIn.Length > 1)
                                {
                                    sHrCheckIn = sArrCheckIn[1].S().Substring(0, 5);
                                    tsCheckInMXN = TimeSpan.Parse(sHrCheckIn);
                                }
                                if (sArrCheckOut.Length > 1)
                                {
                                    sHrCheckOut = sArrCheckOut[1].S().Substring(0, 5);
                                    tsCheckOutMXN = TimeSpan.Parse(sHrCheckOut);
                                }

                                if (tsCheckInMXN >= timeIni && tsCheckInMXN <= timeFin)
                                {

                                    if (!string.IsNullOrEmpty(dtConceptos.Rows[i]["CantidadMXN"].S()))
                                    {
                                        int iCantidad = dtConceptos.Rows[i]["CantidadMXN"].I();
                                        iCountMXN += 1;
                                        dtConceptos.Rows[i]["CantidadMXN"] = iCountMXN + iCantidad;
                                    }
                                    else
                                    {
                                        iCountMXN += 1;
                                        dtConceptos.Rows[i]["CantidadMXN"] = iCountMXN;
                                    }
                                }
                                else
                                {
                                    dtConceptos.Rows[i]["CantidadMXN"] = 0;
                                }
                            }
                            #endregion

                            #region Viáticos Internacionales
                            else if (sCvePais != "MX")
                            {
                                if (sArrCheckIn.Length > 1)
                                {
                                    sHrCheckIn = sArrCheckIn[1].S().Substring(0, 5);
                                    tsCheckInMXN = TimeSpan.Parse(sHrCheckIn);
                                }
                                if (sArrCheckOut.Length > 1)
                                {
                                    sHrCheckOut = sArrCheckOut[1].S().Substring(0, 5);
                                    tsCheckOutMXN = TimeSpan.Parse(sHrCheckOut);
                                }

                                if (tsCheckInMXN >= timeIni && tsCheckInMXN <= timeFin)
                                {

                                    if (!string.IsNullOrEmpty(dtConceptos.Rows[i]["CantidadUSD"].S()))
                                    {
                                        int iCantidad = dtConceptos.Rows[i]["CantidadUSD"].I();
                                        iCountUSD += 1;
                                        dtConceptos.Rows[i]["CantidadUSD"] = iCountUSD + iCantidad;
                                    }
                                    else
                                    {
                                        iCountUSD += 1;
                                        dtConceptos.Rows[i]["CantidadUSD"] = iCountUSD;
                                    }
                                }
                                else
                                {
                                    dtConceptos.Rows[i]["CantidadUSD"] = 0;
                                }
                            }
                            #endregion


                        }
                    }
                }


                #region Creación de viáticos Nacionales e Internacionales

                decimal _dMontoMXN = 0;
                decimal _dTotalMXN = 0;
                int iCantidadMXN = 0;

                sTablaNacional = "<table border='1' width='40%' style='border-radius:4px 4px 4px 4px; border: 1px solid #ccc;'>";
                sTablaNacional += "  <tr>";
                sTablaNacional += "      <td colspan='2' style='background-color:#ccc; text-align:center;'><label>NACIONALES</label></td>";
                sTablaNacional += "  </tr>";

                for (int i = 0; i < dtConceptos.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dtConceptos.Rows[i]["CantidadMXN"].S()))
                        iCantidadMXN = dtConceptos.Rows[i]["CantidadMXN"].S().I();
                    else
                        iCantidadMXN = 0;

                    _dMontoMXN = dtConceptos.Rows[i]["MontoMXN"].S().D();
                    _dTotalMXN += _dMontoMXN * iCantidadMXN;

                    sTablaNacional += "  <tr>";
                    sTablaNacional += "  <td><label>" + dtConceptos.Rows[i]["DesConcepto"].S().ToUpper() + "</label></td>";
                    sTablaNacional += "  <td align='center'><label>" + iCantidadMXN.S() + "</label></td>";
                    sTablaNacional += "  </tr>";
                }
                sTablaNacional += "  <tr>";
                sTablaNacional += "     <td><span><b>TOTAL:</b></span></td>";
                sTablaNacional += "     <td align='right'><span>" + _dTotalMXN.ToString("C") + "</span></td>";
                sTablaNacional += "  </tr>";
                sTablaNacional += "  </table>";



                decimal _dMontoUSD = 0;
                decimal _dTotalUSD = 0;
                int iCantidadUSD = 0;

                sTablaExtranjera = "<table border='1' width='40%' style='border-radius:4px 4px 4px 4px; border: 1px solid #ccc;'>";
                sTablaExtranjera += "  <tr>";
                sTablaExtranjera += "      <td colspan='2' style='background-color:#ccc; text-align:center;'><label>INTERNACIONALES</label></td>";
                sTablaExtranjera += "  </tr>";

                for (int i = 0; i < dtConceptos.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dtConceptos.Rows[i]["CantidadUSD"].S()))
                        iCantidadUSD = dtConceptos.Rows[i]["CantidadUSD"].S().I();
                    else
                        iCantidadUSD = 0;

                    _dMontoUSD = dtConceptos.Rows[i]["MontoUSD"].S().D();
                    _dTotalUSD += _dMontoUSD * iCantidadUSD;

                    sTablaExtranjera += "  <tr>";
                    sTablaExtranjera += "  <td><label>" + dtConceptos.Rows[i]["DesConcepto"].S().ToUpper() + "</label></td>";
                    sTablaExtranjera += "  <td align='center'><label>" + iCantidadUSD.S() + "</label></td>";
                    sTablaExtranjera += "  </tr>";
                }
                sTablaExtranjera += "  <tr>";
                sTablaExtranjera += "     <td><span><b>TOTAL:</b></span></td>";
                sTablaExtranjera += "     <td align='right'><span>" + _dTotalUSD.ToString("C") + "</span></td>";
                sTablaExtranjera += "  </tr>";
                sTablaExtranjera += "  </table>";


                #endregion

                sHtml += "<div class='row'>";
                sHtml += "  <div class='col-md-6' align='center'>";
                sHtml += sTablaNacional;
                sHtml += "  </div>";
                sHtml += "  <div class='col-md-6' align='center'>";
                sHtml += sTablaExtranjera;
                sHtml += "  </div>";
                sHtml += "</div>";

                return sHtml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadConceptos(DataTable dt)
        {
            try
            {
                dtConceptos = null;
                dtConceptos = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadPilotos(DataTable dt)
        {
            try
            {
                dtVuelos = null;
                dtVuelos = dt;

                if (dtVuelos != null && dtVuelos.Rows.Count > 0)
                {
                    gvCalculo.DataSource = dtVuelos;
                    gvCalculo.DataBind();
                    pnlVuelos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadVuelos(DataTable dt)
        {
            try
            {
                dtVuelosXPiloto = null;
                dtVuelosXPiloto = dt;

                if (dtVuelosXPiloto != null && dtVuelosXPiloto.Rows.Count > 0)
                {
                    gvVuelos.DataSource = dtVuelosXPiloto;
                    gvVuelos.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region PROPIEDADES Y VARIABLES
        CalculoPagos_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchConceptos;
        public event EventHandler eSearchVuelos;
        public event EventHandler eSearchCalculos;
        public DataTable dtConceptos
        {
            get { return (DataTable)ViewState["VSConceptos"]; }
            set { ViewState["VSConceptos"] = value; }
        }
        public DataTable dtVuelosXPiloto
        {
            get { return (DataTable)ViewState["VSVuelosXPiloto"]; }
            set { ViewState["VSVuelosXPiloto"] = value; }
        }
        public DataTable dtVuelos
        {
            get { return (DataTable)ViewState["VSdtVuelos"]; }
            set { ViewState["VSdtVuelos"] = value; }
        }
        public string sParametro
        {
            get { return (string)ViewState["VSParametro"]; }
            set { ViewState["VSParametro"] = value; }
        }
        public string sFechaDesde
        {
            get { return (string)ViewState["VSFechaDesde"]; }
            set { ViewState["VSFechaDesde"] = value; }
        }
        public string sFechaHasta
        {
            get { return (string)ViewState["VSFechaHasta"]; }
            set { ViewState["VSFechaHasta"] = value; }
        }
        public List<CantidadComidas> oLstCant
        {
            get { return (List<CantidadComidas>)ViewState["VSCantidadComidas"]; }
            set { ViewState["VSCantidadComidas"] = value; }
        }
        #endregion

        protected void btnGuardarPeriodo_Click(object sender, EventArgs e)
        {

        }

        public void LlenaCalculoPilotos(DataTable dt)
        {
            try
            {
                dtCalculos = dt;
                //GeneraCalculo();
                //gvPilotos.DataSource = dtCalculos;
                //gvPilotos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtCalculos
        {
            get { return (DataTable)ViewState["VSCalculos"]; }
            set { ViewState["VSCalculos"] = value; }
        }

        //private void GeneraCalculo()
        //{
        //    try
        //    {
        //        decimal dDesNal = 0;
        //        decimal dDesInt = 0;
        //        decimal dComNal = 0;
        //        decimal dComInt = 0;
        //        decimal dCenNal = 0;
        //        decimal dCenInt = 0;

        //        foreach (DataRow row in dsParams.Tables[0].Rows)
        //        {
        //            if (row["Concepto"].S() == "Desayuno")
        //            {
        //                dDesNal = row["MontoMXN"].S().D();
        //                dDesInt = row["MontoUSD"].S().D();
        //            }
        //            if (row["Concepto"].S() == "Comida")
        //            {
        //                dComNal = row["MontoMXN"].S().D();
        //                dComInt = row["MontoUSD"].S().D();
        //            }
        //            if (row["Concepto"].S() == "Cena")
        //            {
        //                dCenNal = row["MontoMXN"].S().D();
        //                dCenInt = row["MontoUSD"].S().D();
        //            }
        //        }

        //        dtCalculos.Columns.Add("TotalPesos", typeof(decimal));
        //        dtCalculos.Columns.Add("TotalUSD", typeof(decimal));

        //        dtCalculos.Columns["TotalPesos"].ReadOnly = false;
        //        dtCalculos.Columns["TotalUSD"].ReadOnly = false;


        //        foreach (DataRow row in dtCalculos.Rows)
        //        {
        //            decimal dTotalNal = 0;
        //            decimal dTotalInt = 0;

        //            dTotalNal += row["DesayunosNal"].S().D() * dDesNal;
        //            dTotalNal += row["ComidasNal"].S().D() * dComNal;
        //            dTotalNal += row["CenasNal"].S().D() * dCenNal;

        //            dTotalInt += row["DesayunosInt"].S().D() * dDesInt;
        //            dTotalInt += row["ComidasInt"].S().D() * dComInt;
        //            dTotalInt += row["CenasInt"].S().D() * dCenInt;


        //            row["TotalPesos"] = dTotalNal;
        //            row["TotalUSD"] = dTotalInt;
        //        }



        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}