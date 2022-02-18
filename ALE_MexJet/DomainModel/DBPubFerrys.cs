using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.Data;

namespace ALE_MexJet.DomainModel
{
    public class DBPubFerrys : DBBase
    {
        public int DBSetInsertaFerry(OfertaFerry oF, List<OfertaFerry> olst)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaOfertaFerry]", "@Trip", oF.iTrip,
                                                                                                "@IdOrigen", oF.sOrigen,
                                                                                                "@FechaSalida", oF.dtFechaInicio,
                                                                                                "@IdDestino", oF.sDestino,
                                                                                                "@FechaLlegada", oF.dtFechaFin,
                                                                                                "@Matricula", oF.sMatricula,
                                                                                                "@NoPax", oF.iNoPax,
                                                                                                "@TiempoVuelo", oF.sTiempoVuelo,
                                                                                                "@CostoVuelo", oF.dCostoVuelo,
                                                                                                "@IvaVuelo", oF.dIvaVuelo,
                                                                                                "@IdPadre", oF.iIdPadre,
                                                                                                "@Usuario", Utils.GetUser,
                                                                                                "@IP", Utils.GetIPAddress());
                int iRes = oRes.S().I();

                if (olst.Count > 0)
                {
                    foreach (OfertaFerry oFerry in olst)
                    {
                        oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaOfertaFerry]", "@Trip", oFerry.iTrip,
                                                                                            "@IdOrigen", oFerry.sOrigen,
                                                                                            "@FechaSalida", oFerry.dtFechaInicio,
                                                                                            "@IdDestino", oFerry.sDestino,
                                                                                            "@FechaLlegada", oFerry.dtFechaFin,
                                                                                            "@Matricula", oFerry.sMatricula,
                                                                                            "@NoPax", oFerry.iNoPax,
                                                                                            "@TiempoVuelo", oFerry.sTiempoVuelo,
                                                                                            "@CostoVuelo", oFerry.dCostoVuelo,
                                                                                            "@IvaVuelo", oFerry.dIvaVuelo,
                                                                                            "@IdPadre", iRes,
                                                                                            "@Usuario", Utils.GetUser,
                                                                                            "@IP", Utils.GetIPAddress());
                    }
                }

                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBSearchMatriculasMexJetExceptoLearJet45()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaAeronavesMexJetExceptoLearJet45]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}