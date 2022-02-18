using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ALE_MexJet.Clases;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBCombustibleSemanalDetalle : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltrosDet)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCombustibleSemDet]", oArrFiltrosDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        
        public int DBSave(CombustibleSemanalDetalle oDetalle)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaCombustibleSemDet]", "@IdCombustibleSem", oDetalle.iIdCombustibleSem,
                                                                                    "@Semana", oDetalle.iSemana,
                                                                                    "@CostoXLitro", oDetalle.dCostoXLitro,
                                                                                    "@Status", oDetalle.iStatus,
                                                                                    "@IP", oDetalle.sIP,
                                                                                    "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.RangoCombustible.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se inserto la semana: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(CombustibleSemanalDetalle oDetalle)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaCombustibleSemDet]", "@IdCombustibleSemDet", oDetalle.iIdCombustibleSemDet,
                                                                                        "@Semana", oDetalle.iSemana,
                                                                                        "@CostoXLitro", oDetalle.dCostoXLitro,
                                                                                        "@Usuario", Utils.GetUser,
                                                                                        "@IP", oDetalle.sIP,
                                                                                        "@Status" , oDetalle.iStatus
                                                                                        );
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.RangoCombustible.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizo la semana: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(CombustibleSemanalDetalle oDetalle)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaCombustibleSemDet]", "@IdCombustibleSemDet", oDetalle.iIdCombustibleSemDet,
                                                                                        "@IP", oDetalle.sIP,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se elimino la semana : " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(CombustibleSemanalDetalle oDetalle)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaCombustibleSemDetExiste]", "@IdCombustibleSem", oDetalle.iIdCombustibleSem,
                                                                                                        "@Semana", oDetalle.iSemana);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}