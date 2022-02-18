using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBExtensionServicios : DBBase
    {
        public DataTable DBGetObtienPilotosActivos
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ObtienePilotosActivos]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int DBSetInsertaExtensionServicios(ExtensionServicios oExtension)
        {
            try
            {
                return oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaExtensionServicios]", "@IdEstacion", oExtension.iIdEstacion,
                                                                                                "@IdOrigen", oExtension.iIdOrigen,
                                                                                                "@IdDestino", oExtension.iIdDestino,
                                                                                                "@Fecha", oExtension.DtFecha,
                                                                                                "@IdMatricula", oExtension.iIdMatricula,
                                                                                                "@IdPiloto", oExtension.iIdPiloto,
                                                                                                "@IdCopiloto", oExtension.iIdCopiloto,
                                                                                                "@TipoSolicitud", oExtension.iTipoSolicitud,
                                                                                                "@TipoOperacion", oExtension.iTipoOperacion,
                                                                                                "@HorarioUTCcierre", oExtension.sHorarioUTCcierre,
                                                                                                "@Duracion", oExtension.iDuracion,
                                                                                                "@UTCLlegada", oExtension.sUTCLlegada,
                                                                                                "@UTCSalida", oExtension.sUTCSalida,
                                                                                                "@Motivo", oExtension.sMotivo,
                                                                                                "@Puesto", oExtension.sPuesto,
                                                                                                "@UsuarioCreacion", Utils.GetUser,
                                                                                                "@IP", Utils.GetIPAddress(),
                                                                                                "@Combustible", oExtension.iIdCombustible).S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ExtensionServicios DBGetObtieneExtensionById(int iIdExtension)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaExtensionServiciosId]", "@IdExtenion", iIdExtension).AsEnumerable().
                    Select(r => new ExtensionServicios()
                    {
                        iIdExtension = r.S("IdExtenion").I(),
                        iIdEstacion = r.S("IdEstacion").I(),
                        iIdOrigen = r.S("IdOrigen").I(),
                        iIdDestino = r.S("IdDestino").I(),
                        DtFecha = r.S("Fecha").Dt(),
                        iIdMatricula = r.S("IdMatricula").I(),
                        iIdPiloto = r.S("IdPiloto").I(),
                        iIdCopiloto = r.S("IdCopiloto").I(),
                        iTipoSolicitud = r.S("TipoSolicitud").I(),
                        iTipoOperacion = r.S("TipoOperacion").I(),
                        sHorarioUTCcierre = r.S("HorarioUTCcierre"),
                        iDuracion = r.S("Duracion").I(),
                        sUTCLlegada = r.S("UTCLlegada").S(),
                        sUTCSalida = r.S("UTCSalida").S(),
                        sMotivo = r.S("Motivo"),
                        sPuesto = r.S("Puesto"),
                        iStatus = r.S("Status").I(),
                        sEstacionICAO = r.S("EstacionICAO"),
                        sOrigenICAO = r.S("OrigenICAO"),
                        sDestinoICAO = r.S("DestinoICAO"),
                        iIdCombustible = r.S("Combustible").I()
                    }
                    ).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetActualizaExtensionServicios(ExtensionServicios oExt)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaExtensionServicios]", "@IdExtension", oExt.iIdExtension,
                                                                                                            "@IdEstacion", oExt.iIdEstacion,
                                                                                                            "@IdOrigen", oExt.iIdOrigen,
                                                                                                            "@IdDestino", oExt.iIdDestino,
                                                                                                            "@Fecha", oExt.DtFecha,
                                                                                                            "@IdMatricula", oExt.iIdMatricula,
                                                                                                            "@IdPiloto", oExt.iIdPiloto,
                                                                                                            "@IdCopiloto", oExt.iIdCopiloto,
                                                                                                            "@TipoSolicitud", oExt.iTipoSolicitud,
                                                                                                            "@TipoOperacion", oExt.iTipoOperacion,
                                                                                                            "@HorarioUTCcierre", oExt.sHorarioUTCcierre,
                                                                                                            "@Duracion", oExt.iDuracion,
                                                                                                            "@UTCLlegada", oExt.sUTCLlegada,
                                                                                                            "@UTCSalida", oExt.sUTCSalida,
                                                                                                            "@Motivo", oExt.sMotivo,
                                                                                                            "@Puesto", oExt.sPuesto,
                                                                                                            "@Status", oExt.iStatus,
                                                                                                            "@UsuarioModificacion", Utils.GetUser,
                                                                                                            "@IP", Utils.GetIPAddress());

                if (oRes != null)
                    return oRes.S().I();
                else
                    return 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExtensionServiciosReporte DBGetObtieneReporteExtensionById(int iIdExtension)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaReporteExtensionServiciosId]", "@IdExtension", iIdExtension).AsEnumerable().
                    Select(r => new ExtensionServiciosReporte()
                    {
                        iIdExtension = r.S("IdExtenion").I(),
                        iIdEstacion = r.S("IdEstacion").I(),
                        iIdOrigen = r.S("IdOrigen").I(),
                        iIdDestino = r.S("IdDestino").I(),
                        DtFecha = r.S("Fecha").Dt(),
                        iIdMatricula = r.S("IdMatricula").I(),
                        iIdPiloto = r.S("IdPiloto").I(),
                        sNombrePiloto =r.S("NombrePiloto"),
                        sLicenciaPiloto = r.S("NumeroLicenciaPiloto"),
                        sCrewCodePiloto = r.S("CrewCodePiloto"),
                        iIdCopiloto = r.S("IdCopiloto").I(),
                        sNombreCopiloto = r.S("NombreCopiloto"),
                        sLicenciaCopiloto = r.S("NumeroLicenciaCopiloto"),
                        sCrewCodeCopiloto = r.S("CrewCodeCopiloto"),
                        iTipoSolicitud = r.S("TipoSolicitud").I(),
                        iTipoOperacion = r.S("TipoOperacion").I(),
                        sHorarioUTCcierre = r.S("HorarioUTCcierre"),
                        iDuracion = r.S("Duracion").I(),
                        sUTCLlegada = r.S("UTCLlegada").S(),
                        sUTCSalida = r.S("UTCSalida").S(),
                        sMotivo = r.S("Motivo"),
                        sPuesto = r.S("Puesto"),
                        iStatus = r.S("Status").I(),
                        sEstacionICAO = r.S("EstacionICAO"),
                        sOrigenICAO = r.S("OrigenICAO"),
                        sDestinoICAO = r.S("DestinoICAO"),
                        sNombreFiscalALE = r.S("NombreFiscalALE"),
                        sDomicilioFiscalALE = r.S("DomicilioFiscalALE"),
                        sDomicilioParticularALE = r.S("DomicilioParticularALE"),
                        sRfcALE = r.S("RfcALE"),
                        sTelefonoALE = r.S("TelefonoALE"),
                        sTelefonoDirectoALE = r.S("TelefonoDirectoALE"),
                        sAeropuerto = r.S("Aeropuerto"),
                        sMatricula = r.S("Matricula"),
                        sEquipo = r.S("Equipo"),
                        sTituloExtension = r.S("TituloExtension")
                    }
                    ).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}