using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ALE_MexJet.Clases;
using NucleoBase.Core;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.DomainModel
{
    public class DBCasos : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCasos]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public int DBSave(Cliente oCliente)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaCliente]", "@CodigoCliente", oCliente.sCodigoCliente,
                                                                                    "@TipoCliente", oCliente.iTipoCliente,
                                                                                    "@Nombre", oCliente.sNombre,
                                                                                    "@Observaciones", oCliente.sObservaciones,
                                                                                    "@Notas", oCliente.sNotas,
                                                                                    "@Otros", oCliente.sOtros,
                                                                                    "@Status", oCliente.iStatus,
                                                                                    "@IP", oCliente.sIP,
                                                                                    "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(Cliente oCliente)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaCliente]", "@IdCliente", oCliente.iId,
                                                                                        "@CodigoCliente", oCliente.sCodigoCliente,
                                                                                        "@TipoCliente", oCliente.iTipoCliente,
                                                                                        "@Nombre", oCliente.sNombre,
                                                                                        "@Observaciones", oCliente.sObservaciones,
                                                                                        "@Notas", oCliente.sNotas,
                                                                                        "@Otros", oCliente.sOtros,
                                                                                        "@Status", oCliente.iStatus,
                                                                                        "@IP", oCliente.sIP,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(Cliente oCliente)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaCliente]", "@IdCliente", oCliente.iId,
                                                                                        "@IP", oCliente.sIP,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBConsultarAreasCasos()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaAreas]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}