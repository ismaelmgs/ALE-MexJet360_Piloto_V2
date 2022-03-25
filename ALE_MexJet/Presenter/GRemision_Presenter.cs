using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using DevExpress.Web.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.Web.UI.WebControls;
using ALE_MexJet.Clases;
using DevExpress.Web;

namespace ALE_MexJet.Presenter
{
    public class GRemision_Presenter: BasePresenter<IViewGRemision>
    {
        private readonly DBRemision oIGestCat;

        public GRemision_Presenter(IViewGRemision oView, DBRemision oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eGetContracts += eGetContracts_Presenter;
            oIView.eGetTiposPierna += eGetTiposPierna_Presenter;
            oIView.eSetTramosRem += eSetTramosRem_Presenter;
            oIView.eSaveImportesR += eSaveImportesR_Presenter;
            oIView.eGetServiciosC += eGetServiciosC_Presenter;
            oIView.eSetFinalizaR += eSetFinalizaR_Presenter;
            oIView.eGetPasoUno += eGetPasoUno_Presenter;
            oIView.eLoadOrigDestFiltro += eLoadOrigDestFiltro_Presenter;
            oIView.eSaveImportesOpc2 += eSaveImportesOpc2_Presenter;
            oIView.eGetNotasTrip += eGetNotasTrip_Presenter;
            oIView.eGetContractsDates += eGetContractsDates_Presenter;
            oIView.eSetTramosCotizacion += eSetTramosCotizacion_Presenter;

            //LoadObjects_Presenter();

            //oIView.CargaTramos(oIGestCat.DBGetObtieneBitacorasPendientes("ORO00", "WAL40"));
            //LoadPasoTres(31);
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(new DBCliente().DBSearchObj("@CodigoCliente", string.Empty,
                                                            "@Nombre", string.Empty,
                                                            "@TipoCliente", string.Empty,
                                                            "@estatus", 1));
        }

        protected void eGetContracts_Presenter(object sender, EventArgs e)
        {
            oIView.LoadContracts(oIGestCat.DBGetContracts(oIView.IdCliente));
        }

        protected void eGetTiposPierna_Presenter(object sender, EventArgs e) 
        {
            oIView.dtTiposPierna = new DBTipoPierna().DBSearchObj("@Id", 0, "@Descripcion", string.Empty, "@estatus", 1);
            oIView.sTipoPierna = oIGestCat.DBGetTipoPiernaContrato(oIView.IdContrato);
        }

        protected void eSetTramosRem_Presenter(object sender, EventArgs e)
        {
            try
            {
                List<BitacoraRemision> oLst = oIView.oLstBit;

                //bool ban = true;
                //Enumeraciones.SeCobraFerrys eCobroFerry = Enumeraciones.SeCobraFerrys.Ninguno;
                //foreach (BitacoraRemision oBR in oLst)
                //{
                //    if (oBR.iPax == 0 && oBR.iSeCobra == 1)
                //    {
                //        DatosRemision oRem = oIGestCat.DBGetObtieneDatosRemision(oIView.iIdRemision, oIView.IdContrato);
                //        if (oRem.eSeCobraFerry == Enumeraciones.SeCobraFerrys.Reposicionamiento || oRem.eSeCobraFerry == Enumeraciones.SeCobraFerrys.Ninguno)
                //        {
                //            ban = false;
                //            eCobroFerry = oRem.eSeCobraFerry;
                //            break;
                //        }
                //    }
                //}

                //if (ban)
                //{
                    oIGestCat.DBSetInsertaTramosRemision(oLst);
                    oIView.RedireccionWizard(2);
                    string sMensaje = LoadPasoTres();
                    if(sMensaje == string.Empty)
                        oIView.MostrarMensaje("Los tramos se agregaron correctamente", "REGISTRO GENERADO");
            //    }
            //    else
            //    {
            //        if(eCobroFerry == Enumeraciones.SeCobraFerrys.Ninguno)
            //            oIView.MostrarMensaje("Los tramos no se pueden agregar, ya que el contrato no cobra Ningun Ferry, favor de verificar.", "Aviso");
            //        else
            //            oIView.MostrarMensaje("Los tramos no se pueden agregar, ya que el contrato solo cobra Ferrys de Reposicionamiento, favor de verificar", "Aviso");
            //    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int iRem = 0;
                Remision oRem = oIView.oRemision;
                if (oIView.iIdRemision > 0)
                {
                    oRem.iIdRemision = oIView.iIdRemision;
                    iRem = oIGestCat.DBSetActualizaRemision(oRem);
                }
                else
                {
                    iRem = oIGestCat.DBSetInsertaRemision(oRem);
                }
                if (iRem > 0)
                {
                    oIView.iNoContrato = oIView.IdContrato;
                    oIView.iIdRemision = iRem;
                    DatosRemision odRem = oIGestCat.DBGetObtieneDatosRemision(oIView.iIdRemision, oRem.iIdContrato);
                    oIView.CargaHeaders(odRem.sGrupoModeloDesc);
                    //oIView.LlenaModalCaracteristicasContrato(odRem);
                    oIView.CargaTramos(oIGestCat.DBGetObtieneBitacorasPendientes(oRem.sCliente, oRem.sContrato));
                    oIView.RedireccionWizard(1);
                    oIView.MostrarMensaje("Se guardo la Remisión con Folio: " + iRem, "REGISTRO GENERADO");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void eSaveImportesR_Presenter(object sender, EventArgs e)
        {
            //Guardar piernas en Tramos-Remisión
            oIGestCat.DBSetInsertaTramosRemisionReales(oIView.dtTramosRem, oIView.iIdRemision);
            oIGestCat.DBSetActualizaFactoresRemision(oIView.oDatosFactor);

            SnapshotRemision oSnap = (SnapshotRemision)System.Web.HttpContext.Current.Session["SnapshotRem"];
            if (oSnap != null)
                oIGestCat.DBSetInsertaSnapshotRemision(oSnap.oFactoresTramos, oSnap.oDatosRem);

            LoadPasoCuatro();
        }

        protected void eSaveImportesOpc2_Presenter(object sender, EventArgs e)
        {
            //Guardar piernas en Tramos-Remisión
            oIGestCat.DBSetInsertaTramosRemisionReales(oIView.dtTramosRemOpc2, oIView.iIdRemision);
            oIGestCat.DBSetActualizaFactoresRemision(oIView.oDatosFactor);
            LoadPasoCuatro();
        }

        private string LoadPasoTres()
        {
            try
            {
                SnapshotRemision oSnap = new SnapshotRemision();
                DataTable dtTr = oIGestCat.DBGetConsultaTramosRemisionExistentes(oIView.iIdRemision);
                DatosRemision odRem = oIGestCat.DBGetObtieneDatosRemision(oIView.iIdRemision, oIView.IdContrato);
                //oIView.LlenaModalCaracteristicasContrato(odRem);
                oIView.eSeCobraFerrys = odRem.eSeCobraFerry;
                DataSet ds = oIGestCat.DBGetObtieneTiemposRemision(oIView.iIdRemision);

                oSnap.oDatosRem = odRem;
                System.Web.HttpContext.Current.Session["SnapshotRem"] = oSnap;

                if(ds.Tables[0].Rows.Count > 1)
                {
                    DataRow[] drs = ds.Tables[0].Select("SeCobra = 1");

                    for (int i = 0; i < drs.Length; i++)
                    {
                        if (i < drs.Length - 1)
                        {
                            TimeSpan ts;

                            if (odRem.iCobroTiempo == 0)
                                ts = drs[i]["DestinoCalzo"].S().Dt() - drs[i + 1]["OrigenCalzo"].S().Dt();
                            else
                                ts = drs[i]["DestinoVuelo"].S().Dt() - drs[i + 1]["OrigenVuelo"].S().Dt();

                            double dHoras = ts.TotalHours.S().Replace("-", "").S().Db();
                            double dMinutos = ts.Minutes.S().Replace("-", "").S().Db();
                            double iHoras = Math.Truncate(dHoras);
                            double iMinutos = Math.Truncate(dMinutos);

                            drs[i]["TiempoEspera"] = iHoras.S().PadLeft(2, '0') + ":" + iMinutos.S().PadLeft(2, '0');
                        }
                    }


                    for (int i = 0; i < drs.Length; i++)
                    {
                        long iIdBitacora = drs[i]["IdBitacora"].S().L();
                        
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            if (r["IdBitacora"].S().L() == iIdBitacora)
                            {
                                r["TiempoEspera"] = drs[i]["TiempoEspera"].S();
                            }
                        }
                    }
                }

                ds.Tables[0].TableName = "dtHeader";
                ds.Tables[1].TableName = "dtTotales";

                if (dtTr.Rows.Count == 0)
                {
                    odRem.dtTramos = ds.Tables[0].Copy();
                    DataTable dtPres2 = new DataTable();
                    DataTable dtCon = Utils.CalculaCostosRemision(odRem, ref dtPres2).Copy();

                    if (dtPres2.Rows.Count > 0)
                    {
                        dtPres2 = Utils.AplicaFactoresATiemposRemision(dtPres2, odRem, odRem.iIdContrato);
                        DataTable dtConcepto2 = Utils.CalculaCostosRemision(odRem.iCobroTiempo, dtPres2, odRem);
                        oIView.CargaGridDoblePresupuesto(dtPres2, dtConcepto2);
                    }

                    dtCon.TableName = "dtCon";
                    ds.Tables.Add(dtCon);
                }
                else
                {
                    odRem.dtTramos = dtTr.Copy();
                    //odRem.dtTramos = Utils.AplicaFactoresATiemposRemision(odRem.dtTramos, odRem, odRem.iIdContrato);
                    ds.Tables.Add(Utils.CalculaCostosRemision(odRem.iCobroTiempo, odRem.dtTramos, odRem));
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    odRem.sTotalTiempoVuelo = ds.Tables[1].Rows[0]["TotalTiempoVuelo"].S();
                    odRem.sTotalTiempoCalzo = ds.Tables[1].Rows[0]["TotalTiempoCalzo"].S();
                }

                oIView.oDatosFactor = odRem;
                oIView.LoadHeaders(ds, odRem.dtTramos, odRem);

                return odRem.oErr.sMsjError;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void LoadPasoCuatro()
        {
            try
            {
                DataTable dtServ = new DBRemision().DBGetConsultaConceptosServiciosVuelo;
                DataTable dtSCC = Utils.CalculoServiciosCargo(oIView.IdContrato, oIView.iIdRemision);

                decimal iFactorVuelo = 0;
                oIView.LoadServiciosV(Utils.CalculaServiciosRemision(oIView.dtConceptosR, oIView.iIdRemision, oIView.IdContrato, ref iFactorVuelo), dtServ, dtSCC);
                oIView.iFactorIVASV = iFactorVuelo;
                oIView.RedireccionWizard(3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void eGetServiciosC_Presenter(object sender, EventArgs e)
        {
            oIView.dtServCargo = new DBServicioConCargo().DBSearchObj("@Descripcion", string.Empty, "@estatus", 1);
        }

        protected void eSetFinalizaR_Presenter(object sender, EventArgs e)
        {
            //oIGestCat.DBSetInsertaHeaderServiciosVuelo(oIView.oServiciosV);
            //oIGestCat.DBSetInsertaDetalleServiciosVuelo(oIView.oLstSV);
            //oIGestCat.DBSetInsertaHeaderServiciosCargo(oIView.oServiciosC);
            //oIGestCat.DBSetInsertaDetalleServiciosCargo(oIView.oLstD);

            //Inserta registros a kardex
            oIGestCat.DBSetInsertaRemisionKardex(oIView.OLstKardex);

            DataTable dt = oIGestCat.DBSetCambiaStatusRemision(oIView.oRemGrals);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].S() == "0")
                {
                    string Motivo = "Notificación";
                    string Asunto = "Notificación de Contrato Finalizado";

                    string sDestinatarios = Utils.ObtieneParametroPorClave("100");
                    if (sDestinatarios != string.Empty)
                    {
                        string[] sContactoPara = (sDestinatarios).Split(',');

                        string scorreo = Utils.ObtieneParametroPorClave("4");
                        string sPass = Utils.ObtieneParametroPorClave("5");
                        string sservidor = Utils.ObtieneParametroPorClave("6");
                        string spuerto = Utils.ObtieneParametroPorClave("7");
                        string sContactosCopia = string.Empty;
                        foreach (var sContacto in sContactoPara)
                        {
                            string sMensaje = GetObtieneHTMLContratoFinalizado(dt.Rows[0]["ClaveContrato"].S());
                            //DataRow dr = dt.Select("CorreoElectronico = '" + sContacto + "'").FirstOrDefault();

                            Utils.EnvioCorreo(sservidor, spuerto.S().I(), Asunto, sContacto, sMensaje, "", scorreo, sPass, "", Motivo);
                        }
                    }
                }
            }
        }

        private string GetObtieneHTMLContratoFinalizado(string sContrato)
        {
            try
            {
                string sCad = string.Empty;

                sCad = "<html>";
	            sCad +="        <body>";
		        sCad +="            <div>";
			    sCad +="                <table border='0' cellpadding='0' cellspacing='0' style='width:650px' align='center'>";
			    sCad +="                <tbody>";
			    sCad +="                <tr>";
				sCad +="                    <td colspan='4'><img src='http://www.portal-ale.com/comunicados/plantillasapp/MailMexJetHead.png' width='730' height='126'></td>";
			    sCad +="                </tr>";
			    sCad +="                <tr>";
		  		sCad +="                    <td width='65' style='width:20px'> </td>";
  				sCad +="                    <td colspan='2'>";
  				sCad +="	                    <p>Estimado (a): Ejecutivo</p>";
                sCad +="	                    <p>Le notificamos que el contrato:" + sContrato + " ha finalizado sus horas disponibles para viajar. </p>";
  				sCad +="	                    <p>Para mayor información consulte su reporte Resumen de Horas.</p>";
  				sCad +="	                    <p>Atentamente </p>";
  				sCad +="	                    <p><strong>Aerolíneas Ejecutivas <br>";
  				sCad +="	                    </strong><em>Acercando líderes a su destino.  </em></p></td>";
  				sCad +="                    <td width='65' style='width:20px'> </td>";
			    sCad +="                </tr>";
			    sCad +="                <tr>";
  				sCad +="                    <td style='height:75px;background-color:#0c1d36'> </td>";
  				sCad +="                    <td width='225' style='background-color:#0c1d36'>";
  				sCad +="	                    <p><a href='https://www.facebook.com/pages/Aerolíneas-Ejecutivas/382333655148718?fref=ts' target='_blank'><img src='http://www.portal-ale.com/comunicados/plantillasapp/Facebook-logo.png' width='32' height='32' alt='Facebook'></a> <a href='https://twitter.com/VuelaALE' target='_blank'><img src='http://www.portal-ale.com/comunicados/plantillasapp/Twitter-logo.png' width='32' height='32' alt='Twitter'></a> <a href='https://www.youtube.com/channel/UCcL42CDQabiEQ-6N03-dZ5g' target='_blank'> <img src='http://www.portal-ale.com/comunicados/plantillasapp/Youtube-logo.png' alt='Youtube' width='32' height='32'></a></p>";
  				sCad +="                    </td>";
  				sCad +="                    <td width='375' style='background-color:#0c1d36'>";
  				sCad +="	                    <p style='text-align:right'><span style='font-size:12px;color:#ffffff'><strong>¿Necesita ayuda adicional?</strong><br>";
    			sCad +="		                    Tel: +52 (722) 279.1625 ó 1620</span><br>";
  				sCad +="	                    <span style='font-size:12px;color:#ffffff'><a href='mailto:ac.tlc@aerolineasejecutivas.com' target='_blank'><span style='font-size:12px;color:#ffffff;text-decoration:none'>ac.tlc@aerolineasejecutivas.<wbr>com</span></a></span></p>";
  				sCad +="                    </td>";
  				sCad +="                    <td style='background-color:#0c1d36'> </td>";
			    sCad +="                </tr>";
			    sCad +="                </tbody>";
			    sCad +="                </table>";
		        sCad +="            </div>";

	            sCad +="        </body>";
                sCad +="    </html>";


                return sCad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void eGetPasoUno_Presenter(object sender, EventArgs e)
        {
            try
            {
                Remision oRem = oIGestCat.DBGetRemisionId(oIView.iIdRemision);
                oIView.iNoContrato = oRem.iIdContrato;
                oIView.oRemision = oRem;
                if (oRem.iStatus == 1)
                {
                    oIView.CargaHeaders(oRem.sGrupoModeloDesc);
                    oIView.CargaTramos(oIGestCat.DBGetObtieneBitacorasPendientes(oRem.sCliente, oRem.sContrato));
                    DataSet ds = oIGestCat.DBGetObtieneTiemposRemision(oIView.iIdRemision);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        oIView.RedireccionWizard(2);
                        LoadPasoTres();

                        DataTable dtTr = oIGestCat.DBGetConsultaTramosRemisionExistentes(oIView.iIdRemision);
                        if (dtTr.Rows.Count > 0)
                        {
                            oIView.RedireccionWizard(3);
                            LoadPasoCuatro();
                        }
                    }
                    else
                    {
                        DatosRemision odRem = oIGestCat.DBGetObtieneDatosRemision(oIView.iIdRemision, oRem.iIdContrato);
                        //oIView.LlenaModalCaracteristicasContrato(odRem);
                        oIView.RedireccionWizard(1);
                    }
                }
                else if (oRem.iStatus == 2)
                {
                    DataSet ds = oIGestCat.DBGetTotalesRemisionTerminada(oRem.iIdRemision);
                    DatosRemision odRem = oIGestCat.DBGetObtieneDatosRemision(oIView.iIdRemision, oIView.iNoContrato);
                    oIView.CargaHeaders(odRem.sGrupoModeloDesc);
                    oIView.dtTiposPierna = new DBTipoPierna().DBSearchObj("@Id", 0, "@Descripcion", string.Empty, "@estatus", 1);
                    oIView.LoadRemisionesTerminadas(ds, odRem);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void eLoadOrigDestFiltro_Presenter(object sender, EventArgs e)
        {
            string[] sValores = sender.S().Split('|');
            DataSet ds = oIGestCat.DBGetAeropuertosFerrysVirtuales(sValores[0], sValores[1].S().I());
            oIView.dtOrigenDestino = ds.Tables[0];
            oIView.dtOrigenDestino2 = ds.Tables[1];
        }

        protected void eGetNotasTrip_Presenter(object sender, EventArgs e)
        {
            ASPxButton btn = (ASPxButton)sender;
            if (btn != null)
            {
                oIView.dsNotas = oIGestCat.DBGetConsultaNotasYCasosBitacora(btn.CommandArgument.S().L());
                oIView.LlenaNotasYCasos();
            }
        }

        protected void eGetContractsDates_Presenter(object sender, EventArgs e)
        {
            DatosRemision odRem = oIGestCat.DBGetObtieneDatosRemision(oIView.iIdRemision, oIView.iNoContrato);
            oIView.LlenaModalCaracteristicasContrato(odRem);
        }

        protected void eSetTramosCotizacion_Presenter(object sender, EventArgs e)
        {
            List<BitacoraRemision> oLst = oIView.oLstBit;
            oIGestCat.DBSetInsertaTramosRemision(oLst);
            oIGestCat.DBSetCreaRemisionAPartirCotizacion(oIView.iIdPresupuesto, oIView.iIdRemision);

            DataSet ds = oIGestCat.DBGetTotalesRemisionTerminada(oIView.iIdRemision);
            DatosRemision odRem = oIGestCat.DBGetObtieneDatosRemision(oIView.iIdRemision, oIView.iNoContrato);
            oIView.CargaHeaders(odRem.sGrupoModeloDesc);
            oIView.dtTiposPierna = new DBTipoPierna().DBSearchObj("@Id", 0, "@Descripcion", string.Empty, "@estatus", 1);
            oIView.LoadRemisionesTerminadas(ds, odRem);
            oIView.RedireccionWizard(3);
        }

    }
}