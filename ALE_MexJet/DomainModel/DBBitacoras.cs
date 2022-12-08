using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using NucleoBase.BaseDeDatos;
using ALE_MexJet.Objetos;
using System.Data.SqlClient;

namespace ALE_MexJet.DomainModel
{
    public class DBBitacoras : DBBaseFlex
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
                                                                          "@OrigenVuelo", oB.DtOrigenVuelo,
                                                                          "@OrigenCalzo", oB.DtOrigenCalzo,
                                                                          "@ConsumoOri", oB.SConsumoOrigen.I(),
                                                                          "@CantPax", oB.SCantPax.I(),
                                                                          "@Tipo", oB.STipo,
                                                                          "@DestinoVuelo", oB.DtDestinoVuelo,
                                                                          "@DestinoCalzo", oB.DtDestinoCalzo,
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

        internal void validateBitacoras()
        {
            try
            {
                string ConnectionString = Globales.GetConfigConnection("SqlMexJet360");
                string queryString = "exec [FileTransfer].[spS_MXJ_TMP_FileTransfer] 4, null,null,null, null,null";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryString, connection);

                    // Setting command timeout to 10 seconds
                    command.CommandTimeout = 300;
                    //command.ExecuteNonQuery();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Got expected SqlException due to command timeout ");
                        Console.WriteLine(e);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public DataTable DBGetTipo()
        {
            try
            {
                return oDB_SP.EjecutarDT("[FLX].[spS_Consulta_TIPO_Bitacora]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long DBGetLegIdMax()
        {
            try
            {
                object oBj = oDB_SP.EjecutarValor("[FLX].[spS_Consulta_MaxLegId_Bitacoras]");
                return oBj.S().L();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void saveBitacora(BitacoraFlex bit)
        {
            try
            {
                var x = oDB_SP.EjecutarDS("[FLX].[spI_Bitacora_Auxiliar]",
                               "@AeronaveMatricula", bit.AeronaveMatricula,
                               "@FolioReal", bit.FolioReal,
                               "@VueloContratoId", bit.VueloContratoId,
                               "@PilotoId", bit.PilotoId,
                               "@CopilotoId", bit.CopilotoId,
                               "@Fecha", bit.Fecha,
                               "@Origen", bit.Origen,
                               "@Destino", bit.Destino,
                               "@OrigenVuelo", bit.OrigenVuelo,
                               "@OrigenCalzo", bit.OrigenCalzo,
                               "@ConsumoOri", bit.ConsumoOri,
                               "@CantPax", bit.CantPax,
                               "@Tipo", bit.Tipo,
                               "@DestinoVuelo", bit.DestinoVuelo,
                               "@DestinoCalzo", bit.DestinoCalzo,
                               "@ConsumoDes", bit.ConsumoDes,
                               "@LogNum", bit.LogNum,
                               "@TripNum", bit.TripNum,
                               "@Leg_Num", bit.Leg_Num,
                               "@LegId", bit.LegId);
            }
            catch (Exception ex)
            {
            }
        }
    }
}