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
    public class DBCliente : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCliente]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetTipoPaqueteRemision(long iFolio)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaPaqueteRemision]", "@idRemision", iFolio);
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

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Cliente), Convert.ToInt32(Enumeraciones.TipoAccion.Insertar), "Se insertó el registro: " + oResult.S());

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
                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Cliente), Convert.ToInt32(Enumeraciones.TipoAccion.Actualizar), "Se actualizó el registro: " + oResult.S());
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
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaCliente]",  "@IdCliente", oCliente.iId,
                                                                                        "@IP", oCliente.sIP,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Cliente), Convert.ToInt32(Enumeraciones.TipoAccion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtTipoCliente
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTipoCliente]","@TipoClienteDescripcion","%%",
                                                                                         "@estatus",1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int DBValida(Cliente  oCliente)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaClienteExiste]", "@CodigoCliente", oCliente.sCodigoCliente);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}