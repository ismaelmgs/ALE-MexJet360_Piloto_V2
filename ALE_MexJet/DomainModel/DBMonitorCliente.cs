using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ALE_MexJet.Objetos;
using System.Data;
using ALE_MexJet.Clases;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBMonitorCliente : DBBase
    {
        public int DBSaveCasos(Casos oCaso)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaCaso]","@IdCaso", oCaso.iIdCaso,
                                                                                    "@IdTramo", oCaso.iIdTramo,
                                                                                    "@IdMotivo", oCaso.iIdMotivo,
                                                                                    "@IdTipoCaso", oCaso.iIdTipoCaso,
                                                                                    "@Minutos", oCaso.iMinutos,
                                                                                    "@IdArea", oCaso.iIdArea,
                                                                                    "@IdSolicitudEspecial", oCaso.iIdSolicitud,
                                                                                    "@Detalle", oCaso.sDetalle,
                                                                                    "@AccionCorrectiva", oCaso.sAccionCorrectiva,
                                                                                    "@Otorgado", oCaso.bOtorgado,
                                                                                    "@Status", oCaso.iStatus,
                                                                                    "@IP", oCaso.sIP,
                                                                                    "@Usuario", oCaso.sUsuarioCreacion );

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                DataSet dsResultado = oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaMonitorClientes]", oArrFiltros);
                return dsResultado.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchDetalle(int iIdContrato)
        {
            try
            {
                DataSet dsResultado = oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaDetalleContrato]", "@IdContrato", iIdContrato);
                return dsResultado.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchPiernas(int iIdSolicitud)
        {
            try
            {
                DataSet dsResultado = oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultarPiernas]", "@IdSolicitud", iIdSolicitud);
                return dsResultado.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchDetalleContrato(int idContrato, string CodigoCliente)
        {
            try
            {
                DataSet dsResultado = oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaMonitorClientes]", "@codigocliente", CodigoCliente, "@ClaveContrato",null, "@idcontrato", idContrato);
                return dsResultado.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObservaciones(int idCliente)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaObservacionesCliente]", "@idCliente", idCliente).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchCasos()
        {
            try
            {
                DataSet dsResultado = oDB_SP.EjecutarDS("[Catalogos].[spS_MXJ_ConsultarCasos]");
                return dsResultado.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchMotivos(int IdTipoCaso)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaMotivosCasos]", "@IdTipoCaso", IdTipoCaso).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchAreas()
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
        public DataTable DBSearchContactosCliente(int IdCliente)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaContactosCliente]", "@idCliente", IdCliente).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDeleteSolicitud(int iIdSolicitud)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaSolicitud]", "@IdSolicitud", iIdSolicitud);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchCasosTramo(int iIdCliente)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaCasosCliente]","@IdCliente", iIdCliente).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSerachSolicitudEspecial(int IdMotivo)
        {
            try
            {
                return oDB_SP.EjecutarDT("Catalogos.spS_MXJ_ConsultarSolicitudEspecial", "@IdMotivo", IdMotivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet DBObtieneCorreoCliente(int IdSolictud)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaCorreo]", "@Idsolicitud", IdSolictud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDeleteCaso(params object[] oArrEliminaCaso)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaCasosCliente]", oArrEliminaCaso);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.MonitorCliente.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBConsultaCasoEd(params object[] oArrConsultaCasoEd)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaCasoEd]", oArrConsultaCasoEd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBGuardaSeguimiento(params object[] oGuardaSeguimiento)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaHistorico]", oGuardaSeguimiento
                                                                                    );

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}