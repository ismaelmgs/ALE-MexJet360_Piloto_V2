using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using NucleoBase.BaseDeDatos;
using System.Web.Script.Serialization;
using DevExpress.XtraPrinting.Native;

namespace ALE_MexJet.DomainModel
{
    public class DBReporteVuelos : DBBaseAleSuite
    {
        public DataTable DBGetVuelos(string sFecha, string sFecha2)
        {
            try
            {
                return oDB_SP.EjecutarDT("[OPER].[spS_ObtenerVuelosXFecha]", "@FechaIni", sFecha.Dt(), "@FechaFin", sFecha2.Dt());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetVuelos(List<Vuelo> oLst)
        {
            try
            {
                object Obj;
                int iCount = 0;
                foreach (Vuelo oV in oLst)
                {
                    Obj = oDB_SP.EjecutarDT("[OPER].[spI_InsertaVuelosSAT]", "@TripNum", oV.ITripNum,
                                                                             "@LegId", oV.ILegID,
                                                                             "@CveContrato", oV.SCveContrato,
                                                                             "@Matricula", oV.SMatricula,
                                                                             "@Origen", oV.SOrigen,
                                                                             "@Destino", oV.SDestino,
                                                                             "@OrigenVuelo", oV.DtOrigenVuelo,
                                                                             "@DestinoVuelo", oV.DtDestinoVuelo,
                                                                             "@Usuario", oV.SUsuario);
                    iCount += 1;                                              		
                }
                return iCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       

    }
}