﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using NucleoBase.BaseDeDatos;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.DomainModel
{
    public class DBBitacoras : DBBase
    {
        public DataTable DBGetBitacoras(string sParametro)
        {
            try
            {
                return oDB_SP.EjecutarDT("[FLX].[spS_Consulta_Bitacora_Sistemas]", "@TripNum", sParametro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSetBitacoras(Bitacoras oB)
        {
            try
            {
                object oBj = oDB_SP.EjecutarValor("[FLX].[spI_Bitacora]", "@AeronaveMatricula", oB.SMatricula,
                                                                          "@FolioReal", oB.SFolioReal,
                                                                          "@VueloContratoId", oB.SVueloContratoId,
                                                                          "@PilotoId", oB.SPilotoId,
                                                                          "@CopilotoId", oB.SCopilotoId,
                                                                          "@Fecha", oB.DtFecha,
                                                                          "@Origen", oB.SOrigen,
                                                                          "@Destino", oB.SDestino,
                                                                          "@OrigenVuelo", oB.SOrigenVuelo.Dt(),
                                                                          "@OrigenCalzo", oB.DtOrigenCalzo,
                                                                          "@ConsumoOri", oB.SConsumoOrigen.I(),
                                                                          "@CantPax", oB.SCantPax.I(),
                                                                          "@Tipo", oB.STipo,
                                                                          "@DestinoVuelo", oB.DtDestinoVuelo,
                                                                          "@DestinoCalzo", oB.SDestinoCalzo,
                                                                          "@ConsumoDes", oB.SConsumoDestino.I(),
                                                                          "@LogNum", oB.SLongNum,
                                                                          "@TripNum", oB.LTripNum,
                                                                          "@Leg_Num", oB.ILegNum,
                                                                          "@LegId", oB.LLegId);
                return oBj.S().I();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}