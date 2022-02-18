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
    public class DBMonitorTrafico : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaMonitorTrafico]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBConsultaNotas(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[SpS_MXJ_ConsultaNotas]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBConsultaDetalle(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaMonitorTraficoDetalle]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBConsultaPax(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaPaxMonitorTrafico]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Metodo inserta registro en monitor trafico
        /// </summary>
        /// <param name="iIdSolicitud">Id Solicitud de vuelo</param>
        /// <returns></returns>
        public int DBInsertaMonitorTrafico(int iIdSolicitud)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaMonitorTrafico]", 
                                                                                                "@IdSolicitud",iIdSolicitud, 
                                                                                                "@OrigenSol","MEXJET 360",
                                                                                                "@Status", 1,
                                                                                                "@UsuarioCreacion", Utils.GetUser,
                                                                                                "@FechaCreacion",null, 
                                                                                                "@UsuarioModificacion",null,
                                                                                                "@FechaModificacion",null,
                                                                                                "@IP", Utils.GetIPAddress());
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.MonitorTrafico), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
               return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBActualizaMonitorTrafico(params object[] oArrFiltros)
        {
            object oResult;
            oResult = oDB_SP.EjecutarValor("Principales.spU_MXJ_ActualizaMonitorTrafico", oArrFiltros);

            new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.MonitorTrafico), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
            return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
        }
        /*
        public int DBInsertaMonitorTrafico(params object[] oArrFiltros)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaMonitorTrafico]", oArrFiltros);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.MonitorTrafico), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        public DataTable DBBuscaAeropuertosBase()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaAeropuertosBASE]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable DBSearchTrips(int iIdSolicitud)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaTrips]", "@IdSolicitud", iIdSolicitud).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSaveTrip(Modelo oTrip)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaTRIPSolicitud]", "@IdSolicitud", oTrip.iId,
                                                                                    "@DescTRIP", oTrip.sDescripcion,
                                                                                    "@Status", oTrip.iStatus,
                                                                                    "@IP", oTrip.sIP,
                                                                                    "@UsuarioCreacion", oTrip.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.MonitorTrafico.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                DBSaveSeguimiento(oTrip.iId, "Se insertó trip de la solicitud: " + oTrip.iId, oTrip.sUsuarioCreacion, oTrip.sIP);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdateTrip(Modelo oTrip)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaTRIPSolicitud]", "@idTrip", oTrip.iId,
                                                                                    "@DescTRIP", oTrip.sDescripcion,
                                                                                    "@Usuario", oTrip.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.MonitorTrafico.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se modifico el registro: " + oResult.S());

                DBSaveSeguimiento(oResult.I(), "Se actualizó trip de la solicitud: " + oResult.S(), oTrip.sUsuarioCreacion, oTrip.sIP);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBEliminaTripSolicitud(Modelo oTrip)
        {
            try
            {
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaTripSolicitud]", "@IdTrip", oTrip.iId,
                                                                                            "@Usuario", oTrip.sUsuarioCreacion,
                                                                                            "@IP", "");

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.MonitorTrafico), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());

                DBSaveSeguimiento(oResult.I(), "Se eliminó trip de la solicitud: " + oResult.I(), Utils.GetUser, oTrip.sIP);
                
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(int desc)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaTripExiste]", "@Descripcion", desc);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSaveSeguimiento(int idSolicitud,string sNota, string sUsuarioCreacion, string sIP)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaHistorico]", "@idSolicitud", idSolicitud,
                                                                                            "@idAutor", 0,
                                                                                            "@Nota", sNota,
                                                                                            "@Usuario", sUsuarioCreacion,
                                                                                            "@Status", 1,
                                                                                            "@IP", sIP);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.MonitorDespacho), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro:: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}