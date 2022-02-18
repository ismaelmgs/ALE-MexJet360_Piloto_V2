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
    public class DBVentaFerry : DBBase
    {
        public DataTable DBGetFerrysVenta
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaFerrysVenta]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DBSetActualizaCostosFerry(VentaFerry oFerry)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaPreciosFerrys]", "@IdFerry", oFerry.iIdFerry,
                                                                                                    "@CostoVuelo", oFerry.dCostoVuelo,
                                                                                                    "@IvaVuelo", oFerry.dIvaVuelo,
                                                                                                    "@FechaReservar", oFerry.dtFechaReservar,
                                                                                                    "@StatusApp", oFerry.iStatusApp,
                                                                                                    "@StatusEZ", oFerry.iStatusEz,
                                                                                                    "@Prioridad", oFerry.iPrioridad,
                                                                                                    "@UsuarioModificacion", Utils.GetUser,
                                                                                                    "@IP", Utils.GetIPAddress());

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSaveControlServicio(int Id, string CadenaEntrada, string CadenaSalida)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaControlServicioAPI]",
                                                                                    "@Id", Id,
                                                                                    "@CadenaEntrada", CadenaEntrada,
                                                                                    "@CadenaSalida", CadenaSalida,
                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                    "@IP", ""
                                                                                    );

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdateControlServicio(int Id, string CadenaEntradaCancel, string CadenaSalidaCancel)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaControlServicioAPI]",
                                                                                    "@Id", Id,
                                                                                    "@CadenaEntradaCancel", CadenaEntradaCancel,
                                                                                    "@CadenaSalidaCancel", CadenaSalidaCancel,
                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                    "@IP", ""
                                                                                    );

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetInfoFerryToEZMexJet(int iIdFerry)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ObtieneFerrysEZAppByID]", "@IdFerry", iIdFerry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetObtieneCostoVuelo(string sTiempo, string sMatricula, string sTipoDestino)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ObtienePrecioFerryEzMexJet]", "@Tiempo", sTiempo,
                                                                                                "@Matricula", sMatricula,
                                                                                                "@TipoDestinoO", sTipoDestino);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneDatosFerry(int iIdFerry)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ObtieneEstatusFerryId]", "@IdFerry", iIdFerry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetInsertaBanderaFerryPublicado(int iIdFerry)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaFerryPublicado]", "@IdFerry", iIdFerry,
                                                                                                    "@UsuarioCreacion", Utils.GetUser);

                return oRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetValidaSiFerryPublicado(int iIdFerry)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ValidaSiFerryPublicado]", "@IdFerry", iIdFerry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetFerrysPeriodoPendiente(int iIdPadre)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaOfertaFerrysHijos]", "@IdPadre", iIdPadre);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DBSetActualizaInformacionFerry(int iIdFerry, int iIdVagap)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaEstatusFerry]", "@IdFerry", iIdFerry,
                                                                                                    "@IdVagap", iIdVagap,
                                                                                                    "@Usuario", Utils.GetUser);

                return oRes.S().I() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetActualizaEstatusFerry(int iIdFerry, int iIdStatus)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaStatusFerry]", "@IdFerry", iIdFerry,
                                                                                                    "@Status", iIdStatus,
                                                                                                    "@UsuarioModificacion", Utils.GetUser,
                                                                                                    "@IP", Utils.GetIPAddress());

                return oRes.S().I() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetCancelaFerryAppMexJet(int iIdFerry, int iIdStatus)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_CancelaFerryAPPMexJet]", "@IdFerry", iIdFerry,
                                                                                                    "@Status", iIdStatus,
                                                                                                    "@UsuarioModificacion", Utils.GetUser);

                return oRes.S().I() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetActualizaInformacionOfertaFerry(InformacionFerry oInf)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaInformacionOfertaFerry]", "@IdFerry", oInf.iIdFerry,
                                                                                                                "@FechaSalida", oInf.dtInicio,
                                                                                                                "@FechaLlegada", oInf.dtFin,
                                                                                                                "@NoPax", oInf.iNoPax,
                                                                                                                "@UsuarioModificacion", Utils.GetUser,
                                                                                                                "@IP", Utils.GetIPAddress());

                return oRes.S().I() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneHistoricoOfertaFerry(int iIdFerry)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaHistoricoOfertaFerry]", "@IdFerry", iIdFerry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}