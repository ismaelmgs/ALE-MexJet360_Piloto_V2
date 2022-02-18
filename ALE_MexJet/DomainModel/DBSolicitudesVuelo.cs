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
    public class DBSolicitudesVuelo : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultarSolicitud]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public int DBSave(SolicitudVuelo oSolicitud)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaSolicitudVuelo]", "@IdContrato", oSolicitud.iIdContrato,
                                                                                    "@IdContacto", oSolicitud.iIdContacto,
                                                                                    "@IdMotivo", oSolicitud.iIdMotivo,
                                                                                    "@IdOrigen", oSolicitud.iIdOrigen,
                                                                                    "@IdTipoEquipo", oSolicitud.iIdEquipo,
                                                                                    "@NotasVuelo", oSolicitud.sNotasVuelo,
                                                                                    "@Status", oSolicitud.iStatus,
                                                                                    "@IP", oSolicitud.sIP,
                                                                                    "@Usuario", oSolicitud.sUsuarioCreacion,
                                                                                    "@IdSolicitud" , oSolicitud.iIdSolicitud,
                                                                                    "@Matricula", oSolicitud.sMatricula,
                                                                                    "@Notas" , oSolicitud.sNotaSolVuelo
                                                                                    );

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se creó la solicitud de vuelo: " + oResult.S());

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
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaSolicituds]", "@IdSolicitud", oCliente.sCodigoCliente,
                                                                                    "@IdContacto", oCliente.iTipoCliente,
                                                                                    "@IdMotivo", oCliente.sNombre,
                                                                                    "@IdOrigen", oCliente.sObservaciones,
                                                                                    "@IdTipoEquipo", oCliente.sNotas,
                                                                                    "@NotasVuelo", oCliente.sOtros,
                                                                                    "@ItinerarioV", oCliente.sOtros,
                                                                                    "@Status", oCliente.iStatus,
                                                                                    "@IP", oCliente.sIP,
                                                                                    "@Usuario", oCliente.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se modificoó la solicitud de vuelo: " + oCliente.sCodigoCliente.S());
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
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaSolicitud]", "@IdSolicitud", oCliente.iId,
                                                                                        "@IP", oCliente.sIP,
                                                                                        "@Usuario", oCliente.sUsuarioCreacion);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se eliminó la solicitud de vuelo: " + oCliente.iId.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchContactos(int idCliente)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaContactosCliente]", "@IdCliente", idCliente).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchGrupoModelo()
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaGrupoModelo]").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchOrigen()
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultarOrigen]").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchEstatus()
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultarEstatus]").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchMotivos()
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaMotivos]").Tables[0];
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
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaTrips]","@IdSolicitud",iIdSolicitud).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchHistorico(int iIdSolicitud)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultarSeguimiento]", "@IdSolicitud", iIdSolicitud).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchOrigenVuelo(int IdCliente)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultarOrigenesFrecuentes]").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchOrigenVueloTodos(String sOrigen)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultarOrigenesFrecuentes]", "@Origen", sOrigen).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchDestinoVuelo()
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultarDestinoVuelo]").Tables[0];
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
                                                                                    "@UsuarioCreacion", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó trip de la solicitud: " + oTrip.iId.S());

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

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizó trip de la solicitud: " + oTrip.iId.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSaveTramo(TramoSolicitud oTramo)
        {
            try
            {
                DataTable oResult;
                oResult = oDB_SP.EjecutarDT("[Principales].[spI_MXJ_InsertaTramos]", "@IdSolicitud", oTramo.iIdSolicitud,
                                                                                    "@IdAeropuertoO", oTramo.iIdAeropuertoO,
                                                                                    "@IdAeropuertoD", oTramo.iIdAeropuertoD,
                                                                                    "@FechaVuelo", oTramo.dFechaVuelo.S().Dt(),
                                                                                    "@HoraVuelo", oTramo.sHoraVuelo,
                                                                                    "@Transportacion", oTramo.sTransportacion,
                                                                                    "@Status", oTramo.iStatus,
                                                                                    "@IP", oTramo.sIP,
                                                                                    "@UsuarioCreacion", oTramo.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se creó tramo de la solicitud: " + oTramo.iIdSolicitud.S());

                return oResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSaveSeguimiento(SolicitudVuelo oTramo)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaHistorico]", "@IdSolicitud", oTramo.iIdSolicitud,
                                                                                    "@idAutor", oTramo.iIdAutor,
                                                                                    "@Nota", oTramo.sNotasVuelo,
                                                                                    "@Status", oTramo.iStatus,
                                                                                    "@IP", oTramo.sIP,
                                                                                    "@Usuario", oTramo.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se creó historico de la solicitud: " + oTramo.iIdSolicitud.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSaveItinerario(SolicitudVuelo oTramo)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaHistorico]", "@IdSolicitud", oTramo.iIdSolicitud,
                                                                                    "@idAutor", oTramo.iIdAutor,
                                                                                    "@Nota", oTramo.sNotasVuelo,
                                                                                    "@Status", oTramo.iStatus,
                                                                                    "@IP", oTramo.sIP,
                                                                                    "@Usuario", oTramo.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se creó historico de la solicitud: " + oTramo.iIdSolicitud.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdateTramo(TramoSolicitud oTramo)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaTRIPSolicitud]", "@IdTramo", oTramo.iIdTramo,
                                                                                    "@DescTRIP", oTramo.iIdAeropuertoO,
                                                                                    "@Status", oTramo.iIdAeropuertoD,
                                                                                    "@IP", oTramo.dFechaVuelo,
                                                                                    "@IP", oTramo.sHoraVuelo,
                                                                                    "@IP", oTramo.sTransportacion,
                                                                                    "@IP", oTramo.iStatus,
                                                                                    "@IP", oTramo.sIP,
                                                                                    "@UsuarioCreacion", oTramo.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se modificó tramo de la solicitud: " + oTramo.iIdSolicitud.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchDestinoVuelo(int iIdSolicitud)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultarSeguimiento]",iIdSolicitud).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBObtieneDestinoOrigen()
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaAeropuertoTramoSol]").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBObtieneDestOrigFiltro(string filtro)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaAeropuertoTramoSolICAO]", "@ICAO", string.Format("{0}{1}", filtro, "%"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBObtieneTramoSol(int iIdSolicitud)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultarTramoSolicitud]", "@IdSolicitud", iIdSolicitud).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBEditaTramoSol(TramoSolicitud oTramo)
        {
            try
            {
                DataTable oResult;

                oResult = oDB_SP.EjecutarDT("[Principales].[spU_MXJ_ActualizaTramos]", "@IdTramo", oTramo.iIdTramo,
                                                                                            "@IdAeropuertoO", oTramo.iIdAeropuertoO,
                                                                                            "@IdAeropuertoD", oTramo.iIdAeropuertoD,
                                                                                            "@FechaVuelo", oTramo.dFechaVuelo.S().Dt(),
                                                                                            "@HoraVuelo", oTramo.sHoraVuelo,
                                                                                            "@Transportacion ", oTramo.sTransportacion,
                                                                                            "@UsuarioModifica", oTramo.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizó el registro: " + oResult.S());

                return oResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBEliminaTramoSol(TramoSolicitud oTramo)
        {
            try
            {
                DataTable oResult;

                oResult = oDB_SP.EjecutarDT("[Principales].[spD_MXJ_EliminaTramoSol]", "@IdTramo", oTramo.iIdTramo,
                                                                                            "@IP", "",
                                                                                            "@Usuario", oTramo.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se eliminó el registro: " + oResult.S());

                return oResult;
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

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se creó tramo de la solicitud: " + oTramo.iIdSolicitud.S());

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
        public int DBEditaPaxTramo(TramoSolicitud oTramo)
        {
            try
            {
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaPax]", "@IdPax", oTramo.iNoPax,
                                                                                            "@NombrePax", oTramo.sNombrePax,
                                                                                            "@UsuarioModificacion", oTramo.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se modificó pasajero, de la solicitud: " + oTramo.iIdSolicitud.S());

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

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se eliminó pasajero de la solicitud: " + oTramo.iIdSolicitud.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBObtieneComisariatoTramo(int idTramo)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultarComisariatoTramo]", "@IdTramo", idTramo).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBInsertaComisariatoTramo(TramoSolicitud oTramo)
        {
            try
            {
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaComisariatoTramo]", "@IdTramo", oTramo.iIdTramo,
                                                                                            "@IdProveedor", oTramo.iIdProveedor,
                                                                                            "@ComisariatoDesc", oTramo.sComisariatoDesc,
                                                                                            "@PrecioCotizado ", oTramo.dPrecioCotizado,
                                                                                            "@UsuarioCreacion", oTramo.sUsuarioCreacion,
                                                                                            "@IP", ""
                                                                                            );

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se creó comisariato de la solicitud: " + oTramo.iIdSolicitud.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBEditaComisariatoTramo(TramoSolicitud oTramo)
        {
            try
            {
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaComisariatoTramo]", "@IdComisariato", oTramo.iIdComisariato,
                                                                                            "@IdProveedor ", oTramo.iIdProveedor,
                                                                                            "@ComisariatoDesc", oTramo.sComisariatoDesc,
                                                                                            "@PrecioCotizado", oTramo.dPrecioCotizado,
                                                                                            "@UsuarioModificacion" , oTramo.sUsuarioCreacion,
                                                                                            "@IP", ""
                                                                                            );

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se modificó comisariato de la solicitud: " + oTramo.iIdSolicitud.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBEliminaComisariatoTramo(TramoSolicitud oTramo)
        {
            try
            {
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaComisariatoTramo]", "@IdComisariato", oTramo.iIdComisariato,
                                                                                            "@Usuario", oTramo.sUsuarioCreacion,
                                                                                            "@IP", "");

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se eliminó comisariato de la solicitud: " + oTramo.iIdSolicitud.S());

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

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se eliminó trip de la solicitud: " + oTrip.iId.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBObtieneProveedor()
        {
            try
            {
                return oDB_SP.EjecutarDS("[Catalogos].[spS_MXJ_ConsultaProveedor]").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBEliminaSeguimientosolicitud(SolicitudVuelo oTramo)
        {
            try
            {
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_EliminarHistorico]", "@idSeguimiento", oTramo.iIdSeguimiento,
                                                                                            "@Usuario", oTramo.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se eliminó historico de la solicitud: " + oTramo.iIdSolicitud.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBEditaSolVuelo(SolicitudVuelo oTramo)
        {
            try
            {
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_EditaSolicitudVuelo]", "@IdSolicitud", oTramo.iIdSolicitud,
                                                                                                "@NotasVuelo", oTramo.sNotasVuelo,
                                                                                                "@ItinerarioV" , oTramo.bARchivo,
                                                                                                "@UsuarioModifica", oTramo.sUsuarioCreacion,
                                                                                                "@NombreArchivo", oTramo.sNombreArchivo);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se modificó la solicitud: " + oTramo.iIdSolicitud.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBObtieneSolVueloByID(int Id)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaSolicitudById]", "@IdSolictud", Id).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet DBLoadCorreoAlta(int Id)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaCorreo]", "@Idsolicitud", Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SolicitudVuelo DBGetSolicitud(int iIdSolicitud)
        {
            return oDB_SP.EjecutarDT("", "", iIdSolicitud).AsEnumerable().Select(r => new SolicitudVuelo
            {
                iIdAutor = r[""].S().I(),
                iIdEquipo = r[""].S().I()
            }).First();
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
        public int DBValidaFechaHora(TramoSolicitud oTramo)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaFechaHoraTramSolVuelo]", "@IdSolicitud", oTramo.iIdSolicitud
                                                                                                      , "@FechaVuelo", oTramo.dFechaVuelo.S().Dt()
                                                                                                      , "@HoraVuelo" ,oTramo.sHoraVuelo
                                                                                                      , "@IdTramo ", oTramo.iIdTramo
                                                                                                        );
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet DBLoadAeTramo(string cadena)
        {
            var x = cadena.Split('|');
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaAerTramo]", "@ICAO", x[0].S() + "%"
                                                                                    , "@IdSolicitud", x[1].S().I()
                                                                                            );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetObtieneSeguimientoID(int iIdSeguimiento)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaSeguimientoID]", "@idSeguimiento", iIdSeguimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetSolPDFID(int IdSol)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultarSolPDF]", "@IdSolVuelo", IdSol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBConsultaGrupoModeloCon(params object[] oFiltroContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaGrupoModeloContrato]", oFiltroContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBConsultaItinerarioVuelo(params object[] oItinerario)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultarItinerarioSolicitud]", oItinerario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBEliminaItinerarioSolicitud(params object[] oEliminaItinerario)
        {
            try
            {
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaItinerariovuelo]", oEliminaItinerario);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
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
        public DataTable DBconsutaDetalleItinerario(params object[] oConsultaItinerario)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaItinerarioSolicitudId]", oConsultaItinerario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValidaVueloSimultaneo(string IdSolVuelo )
        {
            try
            {
                var Result = IdSolVuelo.Split('|');
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaVuelosSimultaneos]", "@IdSolVuelo", Result[0].I(),
                                                                                                    "@FechaVuelo", Result[1].Dt(),
                                                                                                    "@HORAVUELO", Result[2].S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBGurdaMonitorDespacho(SolicitudVuelo oSolV)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_MonitorDespacho]", "@IdSolicitud",oSolV.iIdSolicitud,
                                                                                        "@Dictamen", oSolV.idictamen,
                                                                                        "@OrigenSolicitud","Mex Jet 360",
                                                                                         "@Usuario",oSolV.sUsuarioCreacion,
                                                                                         "@IP",oSolV.sIP);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se creó dictamen de la solicitud: " + oSolV.iIdSolicitud.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
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
                return oDB_SP.EjecutarDT("Principales.spS_MXJ_ConsultaPasajeros", "@Filtro", string.Format("{0}{1}", filtro, "%" ));
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
        public DataTable DBViabilidad(params object[] oViabilidad)
        {
            try
            {
                return oDB_SP.EjecutarDT("Principales.SpS_MXJ_Viabilidades", ((object[])(((object[])(oViabilidad[0]))[0])));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBGurdaPDFSeguimieno(SolicitudVuelo oSolV)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaPDFSeguimiento]", "@IdSolicitud", oSolV.iIdSolicitud,
                                                                                                "@IdSeguimiento", oSolV.iIdSeguimiento,
                                                                                                "@ItinerarioV", oSolV.bARchivo,
                                                                                                "@NombreArchivo", oSolV.sNombreArchivo,
                                                                                                "@Usuario",  oSolV.sUsuarioCreacion,
                                                                                                "@IP", oSolV.sIP);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBConsultarPDFSeguimiento(int IdSeguimiento)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultarPDFSeguimiento]", "@IdSeguimiento" ,IdSeguimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBGetConsultaIsSolicitudMobile(int iIdSolicitud)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaSiSolicitudMobile]", "@IdSolicitud", iIdSolicitud);

                if (oRes != null)
                {
                    return oRes.S() == "1" ? true : false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetTripGuidesPorSolicitud(int iIdSolicitud)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ObtieneTripGuidesSolicitud]", "@IdSolicitud", iIdSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetEliminaTripGuide(int iIdTripGuide)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spD_MXJ_EliminaTripGuidesSolicitud]", "@IdTripGuide", iIdTripGuide);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public MonitorCliente DBGetObtieneGeneralesSolicitud(int iIdSolicitud)
        {
            try
            {
                MonitorCliente oMon = null;
                DataTable dtRes = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ObtieneDatosGeneralesSolicitudVuelo]", "@IdSolicitud", iIdSolicitud);

                if (dtRes.Rows.Count > 0)
                {
                    oMon = dtRes.AsEnumerable().Select(r => new MonitorCliente()
                    {
                        iIdContrato = r.S("IdContrato").I(),
                        iIdCliente = r.S("IdCliente").I(),
                        sClaveContrato = r.S("ClaveContrato"),
                        sCodigoCliente = r.S("CodigoCliente"),
                        sTipoContrato = r.S("TipoPaquete"),
                        sGrupoModelo = r.S("GrupoModelo"),
                        sVendedor = r.S("Vendedor")
                    }).First();
                }

                return oMon;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBConsultaContactoSolicitud(int iIdSolicitud)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaContactoSolicitud]", "@IdSolicitud", iIdSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBConsultaVendedorSolicitud(int iIdSolicitud)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaVendedorSolicitud]", "@IdSolicitud", iIdSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
