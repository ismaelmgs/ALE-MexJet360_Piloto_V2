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
    public class DBImagen : DBBase
    {
        public DataTable DBSearchObj(Imagen I)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaImagen]","@ClaveContrato", I.ClaveContrato
                                                                                 ,"@Matricula",I.Matricula
                                                                                 ,"@Cliente ",I.Cliente
                                                                                 ,"@Fecha", I.Fecha
                                                                                 ,"@Usuario", I.UsuarioModificacion
                                                                                  );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjP(Imagen I)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaPaxTramo]", "@IdTramo", I.IdTramo
                                                                                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdate(Imagen I)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaPaxTramo]", "@IdPax ", I.IdPax,
                                                                                        "@Arrivo", I.Arrivo,
                                                                                        "@HoraLlegada", I.HoraLlegada
                                                                                        );
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.MonitorImagen), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizo el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjC(Imagen I)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaClienteImagen]", "@IdSolicitud", I.IdSolicitud
                                                                                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjI(Imagen I)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaImagenDetalle]", "@IdPadre", I.IdPadre
                                                                                      ,"@Opcion",I.Opcion
                                                                                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjID(Imagen I)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaImagenSolicitud]", "@IdPadre",I.IdPadre
                                                                                          , "@IdTramo", I.IdTramo
                                                                                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(Imagen I)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaControlImagen]",
                                                                                  "@IdControl", I.IdControl,
                                                                                  "@IdImagen", I.IdImagen,
                                                                                  "@IdTramo", I.IdTramo,
                                                                                  "@AbordadoPre", I.AbordadoPre,
                                                                                  "@AbordadoPos", I.AbordadoPos,
                                                                                  "@ObservacionesPre", I.ObservacionesPre,
                                                                                  "@ObservacionesPos", I.ObservacionesPos,
                                                                                  "@UsuarioModificacion", Utils.GetUser,
                                                                                  "@IP" , "",
                                                                                  "@Opcion", I.Opcion
                                                                                  );

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.MonitorImagen), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se inserto el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjPilot(Imagen I)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaPiloto]", "@IdSolicitud", I.IdSolicitud
                                                                                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBEditaPaxTramo(TramoSolicitud oTramo)
        {
            try
            {
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaPax]", "@IdPax", oTramo.iNoPax,
                                                                                            "@NombrePax", oTramo.sNombrePax,
                                                                                            "@UsuarioModificacion", oTramo.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.MonitorImagen.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBEliminaPaxTramo(TramoSolicitud oTramo)
        {
            try
            {
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaPax]", "@IdPax", oTramo.iNoPax,
                                                                                            "@Usuario", oTramo.sUsuarioCreacion,
                                                                                            "@IP", "");

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.MonitorImagen.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se elimino el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBGurdaPasajero(params object[] oPasajero)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaPasajero]", oPasajero);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBInsertaPaxTramo(TramoSolicitud oTramo)
        {
            try
            {
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaPaxTramo]", "@IdTramo", oTramo.iIdTramo,
                                                                                            "@NombrePax", oTramo.sNombrePax,
                                                                                            "@Usuario ", oTramo.sUsuarioCreacion,
                                                                                            "@IP", ""
                                                                                            );

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.MonitorImagen.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se inserto el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBObtienePaxTramo(int idTramo)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultarPaxTramo]", "@IdTramo", idTramo).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBObtienePasajeroFiltro(string filtro)
        {
            try
            {
                return oDB_SP.EjecutarDT("Principales.spS_MXJ_ConsultaPasajeros", "@Filtro", string.Format("{0}{1}", filtro, "%"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBInsertaNotas(params object[] oNotas)
        {
            try
            {
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaInteriores]", oNotas);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.MonitorImagen.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se inserto el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBObtieneArea(string filtro)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaAreaId]", "@IdArea", filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdateMonitorDespacho(params object[] oArrFiltros)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_EnviaNotificacionDespacho]", oArrFiltros);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.MonitorImagen.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}




