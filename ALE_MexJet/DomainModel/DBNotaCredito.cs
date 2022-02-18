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
    public class DBNotaCredito : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaFolioRemision]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(NotaCredito NT)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaNotaCredito]", "@IdRemision", NT.iIdFolioRemision,
                                                                                  "@TipoNotaCredito", NT.iTipoNotaCredito,
                                                                                  "@Cantidad", NT.iCantidad,
                                                                                  "@Tiempo", NT.iTiempo,
                                                                                  "@Status" , NT.iStatus,
                                                                                  "@UsuarioCreacion", NT.iUsuarioCreacion,
                                                                                  "@IP", NT.iIP);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.NotaCredito), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjP(NotaCredito NB)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaNotaCredito]", "@FolioNotaCredito" ,NB.FolioNotaCredito
                                                                                        , "@TipoNotaCredito",NB.iTipoNotaCredito
                                                                                        , "@CodigoCliente",NB.CodigoCliente
                                                                                        , "@ClaveContrato",NB.Clavecontrato
                                                                                        , "@IdRemision" ,NB.iIdFolioRemision
                                                                                        , "@FechaInicio" , NB.FechaInicio
                                                                                        , "@FechaFin" , NB.FechaFin
                                                                                        , "@Opcion", NB.Opcion
                                                                                        );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(NotaCredito NT)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaNotaCredito]", "@IdNota", NT.IdNotaCredito,
                                                                                  "@Usuario", NT.iUsuarioCreacion,
                                                                                  "@IP", NT.iIP);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.NotaCredito), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se elimino el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}