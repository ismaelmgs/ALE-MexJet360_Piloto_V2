using ALE_MexJet.Clases;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Views.Principales;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using NucleoBase.Core;
using System.Configuration;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ALE_MexJet.Presenter
{
    public class VentaFerry_Presentar : BasePresenter<IViewVentaFerry>
    {
        private readonly DBVentaFerry oIGestCat;

        public VentaFerry_Presentar(IViewVentaFerry oView, DBVentaFerry oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eSearchFerryHijo += eSearchFerryHijo_Presenter;
            oIView.eLoadOrigDestFiltro += eLoadOrigDestFiltro_Presenter;
            oIView.eLoadOrigDestFiltroDest += eLoadOrigDestFiltroDest_Presenter;
            oIView.eSearchMatricula += eSearchMatricula_Presenter;
            oIView.eSaveFerryHijo += eSaveFerryHijo_Presenter;
            oIView.eSaveInformacionFerry += eSaveInformacionFerry_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadFerrys(oIGestCat.DBGetFerrysVenta);
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                VentaFerry oF = oIView.oVFerry;
                OfertaFerry oFe = oIView.oVFerry2;

                if (oIGestCat.DBSetActualizaCostosFerry(oF))
                {
                    oIView.LoadFerrys(oIGestCat.DBGetFerrysVenta);

                    // BANDERA PARA ENVIAR AL APP DE MEXJET
                    //if (oFe.bApp)
                    //{
                    //    if (oFe.iJsonEnviadoAnteriormente == 1)
                    //    {
                    //        //si es igual a 1 mandamos el update
                    //        string sUrl = Utils.ObtieneParametroPorClave("94");
                    //        JsonObjeto json = Utils.NotificaAppMovilActualizacion(sUrl, oF.dCostoVuelo, oF.dIvaVuelo, oFe.dtFechaSalida, oF.dtFechaReservar, oFe.sDestino, oFe.sOrigen, oFe.sTiempoVuelo, oFe.iIdGrupoModelo, oF.iIdFerry, oFe.iNoPierna);
                    //        oIGestCat.DBSaveControlServicio(json.iId._I(), json.sCadenaRequest, json.sCadenaResponse);
                    //    }
                    //    else
                    //    {
                    //        // si es igual 0 mandamos el create
                    //        string sUrl = Utils.ObtieneParametroPorClave("93");
                    //        JsonObjeto json = Utils.NotificaAppMovil(sUrl, oF.dCostoVuelo, oF.dIvaVuelo, oFe.dtFechaSalida, oF.dtFechaReservar, oFe.sDestino, oFe.sOrigen, oFe.sTiempoVuelo, oFe.iIdGrupoModelo, oF.iIdFerry, oFe.iNoPierna);
                    //        oIGestCat.DBSaveControlServicio(json.iId._I(), json.sCadenaRequest, json.sCadenaResponse);
                    //    }
                    //}

                    // BANDERA PARA ENVIAR A EXMEXJET
                    if (oF.bEzMexJet)
                    {
                        bool ban = EnviaFerryEzJMexJet(oF.iIdFerry);
                    }
                }

                oIView.MostrarMensaje("Los Ferrys se enviaron correctamente.", "Aviso");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private bool EnviaFerryEzJMexJet(int iIdFerry)
        {
            try
            {
                bool ban = false;

                using (var client = new HttpClient())
                {
                    DataSet ds = new DataSet();
                    ds = oIGestCat.DBGetInfoFerryToEZMexJet(iIdFerry);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow r = ds.Tables[0].Rows[0];

                        VentaFerrys oVF = new VentaFerrys();

                        oVF.iIdFerry = r.S("IdFerry").I();
                        oVF.iIdPadre = r.S("IdPadre").I();
                        oVF.sOrigen = r.S("Origen");
                        oVF.sDestino = r.S("Destino");
                        oVF.dtFechaInicioReservar = r.S("FechaSalida").Dt();
                        //oVF.dtFechaLlegada = r.S("FechaLlegada").Dt();
                        oVF.sDuracion = r.S("Duracion");
                        oVF.dtFechaLimiteReservar = r.S("FechaReservar").Dt();
                        //oVF.sMatricula = r.S("Matricula");
                        oVF.sGrupoModelo = r.S("GrupoModelo");
                        oVF.dPrecioLista = r.S("PrecioLista").D();
                        oVF.iPrioridad = r.S("Prioridad").I();
                        oVF.noPax = 5;

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

                        #region CODIGO COMENTADO
                        //DataRow[] rowsSMS = ds.Tables[2].Select("IdFerry = " + oVF.iIdFerry.S());
                        //if (rowsSMS.Length > 0)
                        //{
                        //    for (int i = 0; i < rowsSMS.Length; i++)
                        //    {
                        //        SMSDifusion oS = new SMSDifusion();
                        //        oS.sNombre = rowsSMS[i]["NombrePersona"].S();
                        //        oS.sNumero = rowsSMS[i]["TelefonoMovil"].S();

                        //        oVF.oLsSMS.Add(oS);
                        //    }
                        //}

                        //DataRow[] rowsMail = ds.Tables[3].Select("IdFerry = " + oVF.iIdFerry.S());
                        //if (rowsMail.Length > 0)
                        //{
                        //    for (int i = 0; i < rowsMail.Length; i++)
                        //    {
                        //        MailDifusion oM = new MailDifusion();
                        //        oM.sNombre = rowsMail[i]["NombrePersona"].S();
                        //        oM.sMail = rowsMail[i]["CorreoElectronico"].S();

                        //        oVF.oLsMail.Add(oM);
                        //    }
                        //}
#endregion

                        string sCad = JsonConvert.SerializeObject(oVF).ToString();

                        if (!VerificaFerryPublicado(iIdFerry))
                        {
                            // Enviamos el JSON al proveedor
                            string sPathWSApp = Utils.ObtieneParametroPorClave("108");
                            string[] sUrl = sPathWSApp.Split(',');

                            for (int i = 0; i < sUrl.Length; i++)
                            {
                                string sPathWS = sPathWSApp; //sUrl[i] + iIdFerry.S() + ".json";
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


                                string sToken = Utils.ObtieneTokenAppMexJet();

                                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + sToken);
                                var response = client.PostAsync(sPathWS, new StringContent(sCad, Encoding.UTF8, "application/json")).Result;

                                //if (response.IsSuccessStatusCode)
                                //{
                                //    new DBOfertaFerry().DBSetInsertaBitacoraWS("Exito ruta: " + sPathWS, "frmVentaFerry.aspx", "VentaFerry_Presentar", "EnviaFerryEzJMexJet");
                                //    ban = true;

                                //    if (i == 0)
                                //    {
                                //        int iRes = oIGestCat.DBSetInsertaBanderaFerryPublicado(iIdFerry);
                                //    }
                                //}
                                //else
                                //    new DBOfertaFerry().DBSetInsertaBitacoraWS("Error ruta: " + sPathWS, "frmVentaFerry.aspx", "VentaFerry_Presentar", "EnviaFerryEzJMexJet");
                            }

                            //string sToken = Utils.ObtieneTokenAppMexJet();
                            //PostAsync(sToken, sPathWSApp, sCad);
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

                DataTable dt = oIGestCat.DBGetValidaSiFerryPublicado(iIdFerry);
                if(dt != null && dt.Rows.Count > 0)
                    ban = true;

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void eSearchFerryHijo_Presenter(object sender, EventArgs e)
        {
            oIView.dtFerrysHijo = oIGestCat.DBGetFerrysPeriodoPendiente(oIView.iIdPadre);
        }

        protected void eLoadOrigDestFiltro_Presenter(object sender, EventArgs e)
        {
            oIView.dtOrigen = new DBPresupuesto().GetAeropuertosOrigen(oIView.sFiltroO, 2);
        }

        protected void eLoadOrigDestFiltroDest_Presenter(object sender, EventArgs e)
        {
            oIView.dtDestino = new DBPresupuesto().GetAeropuertosDestino(oIView.sFiltroD, string.Empty, 2);
        }

        protected void eSaveFerryHijo_Presenter(object sender, EventArgs e)
        {
            List<OfertaFerry> olsFe = new List<OfertaFerry>();
            int iRes = new DBPubFerrys().DBSetInsertaFerry(oIView.oFerrysHijo, olsFe);

            if (iRes < 1)
            {
                oIView.MostrarMensaje("Ocurrió un error al guardar el ferry.", "Aviso");
            }
        }

        protected void eSearchMatricula_Presenter(object sender, EventArgs e)
        {

            object[] oArrMat = new object[] { "@Serie", string.Empty,
                                            "@Matricula", string.Empty,
                                            "@estatus", 1
                                    };
            oIView.dtMatriculas = new DBAeronave().DBSearchObj(oArrMat);
        }

        protected void eSaveInformacionFerry_Presenter(object sender, EventArgs e)
        {
            if (oIGestCat.DBSetActualizaInformacionOfertaFerry(oIView.oInfoFerry))
            {

            }
            else
                oIView.MostrarMensaje("Ocurrió un error al guardar el ferry.", "Aviso");
        }
    }
}