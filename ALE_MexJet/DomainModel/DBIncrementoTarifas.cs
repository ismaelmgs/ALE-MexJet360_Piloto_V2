using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.DomainModel
{
    public class DBIncrementoTarifas : DBBase
    {
        public DataTable DBGetCalculoIncrementoTarifas(int iTipo, int iMes)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaIncrementoTarifas]", "@Tipo", iTipo,
                                                                                                "@Mes", iMes);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetInsertaHistIncrementoTarifas(List<IncrementoTarifas> oLstIncremento)
        {
            try
            {
                foreach (IncrementoTarifas oIncremento in oLstIncremento)
                {
                    oDB_SP.EjecutarSP("[Principales].[spI_MXJ_InsertaHistIncrementoTarifas]", "@IdContrato", oIncremento.iIdContrato,
                                                                                                "@IdConcepto", oIncremento.iIdConcepto,
                                                                                                "@ImporteO", oIncremento.dImporteOri,
                                                                                                "@InflacionDesc", oIncremento.sInflacionDesc,
                                                                                                "@Porcentaje", oIncremento.dPorcentaje,
                                                                                                "@MasPuntos", oIncremento.dMasPuntos,
                                                                                                "@Tope", oIncremento.dTope,
                                                                                                "@Inflacion", oIncremento.dInflacion,
                                                                                                "@ImporteN", oIncremento.dImporteNuevo,
                                                                                                "@Anio",oIncremento.iAnio,
                                                                                                "@UsuarioCreacion",Utils.GetUser,
                                                                                                "@IP",Utils.GetIPAddress()
                                                                                                );

                    DBSetActualizaTarifasIncrementoContrato(oIncremento.iIdContrato, oIncremento.iIdConcepto, oIncremento.dImporteNuevo);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetActualizaTarifasIncrementoContrato(int iIdContrato, int iIdConcepto, decimal dImporteN)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spU_MXJ_ActualizaIncrementoTarifasContrato]", "@IdContrato", iIdContrato,
                                                                                                "@IdConcepto", iIdConcepto,
                                                                                                "@ImporteN", dImporteN);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}