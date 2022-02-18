using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ALE_MexJet.Objetos;
using ALE_MexJet.Clases;

namespace ALE_MexJet.DomainModel
{
    public class DBOfertaFerry : DBBase
    {

        public DataTable DBGetFerrysPeriodo
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaOfertaFerrys]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable DBGetFerrysEnviados
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaFerrysEnviados]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DBSetInsertaOfertaFerry(List<OfertaFerry> oLs)
        {
            try
            {
                bool ban = false;
                int iFerry = 0;

                foreach (OfertaFerry oF in oLs)
                {
                    //if (oF.bJetSmart || oF.bApp || oF.bEZMexJet)
                    //{
                    //    if (oF.iIdPadre == 0)
                    //    {
                    object oRes = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaOfertaFerry]","@JetSmart", oF.bJetSmart,
                                                                                                    "@StatusJetSmart", oF.iStatusJet,
                                                                                                    "@App", oF.bApp,
                                                                                                    "@StatusApp", oF.iStatusApp,
                                                                                                    "@EZ", oF.bEZMexJet,
                                                                                                    "@StatusEZ", oF.iStatusEZ,
                                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                                    "@IP", Utils.GetIPAddress(),
                                                                                                    "@IdFerry", oF.iIdPendiente,
                                                                                                    "@IdPadre", oF.iIdPadre);

                    iFerry = oRes.S().I();
                    //}
                    //else
                    //{
                    //    object oResHijo = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaOfertaFerry]",
                    //                                                                                "@JetSmart", oF.bJetSmart,
                    //                                                                                "@StatusJetSmart", oF.iStatusJet,
                    //                                                                                "@App", oF.bApp,
                    //                                                                                "@StatusApp", oF.iStatusApp,
                    //                                                                                "@EZ", oF.bEZMexJet,
                    //                                                                                "@StatusEZ", oF.iStatusEZ,
                    //                                                                                "@UsuarioCreacion", Utils.GetUser,
                    //                                                                                "@IP", Utils.GetIPAddress(),
                    //                                                                                "@IdFerry", oF.iIdPendiente,
                    //                                                                                "@IdPadre", iFerry);
                    //}

                    ////// Lista de Mail
                    ////foreach (ListaEnvios le in oF.oLstMail)
                    ////{
                    ////    DBSetInsertaRelacionFerryMail(iFerry, le.iIdLista);
                    ////}

                    ////// Lista de SMS
                    ////foreach (ListaEnvios le in oF.oLstSMS)
                    ////{
                    ////    DBSetInsertaRelacionFerrySMS(iFerry, le.iIdLista);
                    ////}

                    ban = iFerry > 0 ? true : false;

                    if (!ban)
                        break;
                    //}


                }

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetInsertaRelacionFerryMail(int iIdFerry, int iIdLista)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spI_MXJ_InsertaPersonaMailFerry]", "@IdFerry", iIdFerry,
                                                                                    "@ListaMail", iIdLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetInsertaRelacionFerrySMS(int iIdFerry, int iIdLista)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spI_MXJ_InsertaPersonaSMSFerry]", "@IdFerry", iIdFerry,
                                                                                    "@ListaMail", iIdLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetInsertaOfertaFerryManuales(FerryNuevos oF)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaOfertaFerry]", "@Trip", oF.iTrip,
                                                                                                "@NoPierna", oF.iNoPierna,
                                                                                                "@Origen", oF.sOrigen,
                                                                                                "@FechaSalida", oF.dtFechaSalida,
                                                                                                "@Destino", oF.sDestino,
                                                                                                "@FechaLlegada", oF.dtFechaLlegada,
                                                                                                "@Matricula", oF.sMatricula,
                                                                                                "@JetSmart", oF.bJetSmart,
                                                                                                "@StatusJetSmart", oF.iStatusJetSmart,
                                                                                                "@App", oF.bApp,
                                                                                                "@StatusApp", oF.iStatusApp,
                                                                                                "@EZ", oF.bEZMexJet,
                                                                                                "@StatusEZ", oF.iStatusEZ,
                                                                                                "@UsuarioCreacion", Utils.GetUser,
                                                                                                "@IP", Utils.GetIPAddress(),
                                                                                                "@IdFerry", oF.iIdFerry);

                return oRes.S().I() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetInsertaOfertaFerryPendiente(List<OfertaFerry> oLs)
        {
            try
            {
                bool ban = false;
                foreach (OfertaFerry oF in oLs)
                {
                    //if (oF.bJetSmart || oF.bApp || oF.bEZMexJet)
                    //{
                    object oRes = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaOfertaFerryPendiente]", "@Trip", oF.iTrip,
                                                                                                    "@NoPierna", oF.iNoPierna,
                                                                                                    "@Origen", oF.sOrigen,
                                                                                                    "@FechaSalida", oF.dtFechaSalida,
                                                                                                    "@FechaSalidaB", oF.dFechaSalidaB,
                                                                                                    "@FechaSalidaA", oF.sFechaSalidaA,
                                                                                                    "@Destino", oF.sDestino,
                                                                                                    "@FechaLlegada", oF.dtFechaLlegada,
                                                                                                    "@FechaLlegadaB", oF.dFechaLlegadaB,
                                                                                                    "@FechaLlegadaA", oF.sFechaLlegadaA,
                                                                                                    "@Matricula", oF.sMatricula,
                                                                                                    "@GrupoModelo", oF.sGrupoModelo,
                                                                                                    "@Diferencia", oF.iDiferencia,
                                                                                                    "@TiempoVuelo", oF.sTiempoVuelo,
                                                                                                    "@JetSmart", oF.bJetSmart,
                                                                                                    "@StatusJetSmart", oF.iStatusJet,
                                                                                                    "@App", oF.bApp,
                                                                                                    "@StatusApp", oF.iStatusApp,
                                                                                                    "@EZ", oF.bEZMexJet,
                                                                                                    "@StatusEZ", oF.iStatusEZ,
                                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                                    "@IP", Utils.GetIPAddress(),
                                                                                                    "@IdFerry", oF.iIdFerry,
                                                                                                    "@IdPadre", oF.iIdPadre,
                                                                                                    "@IdPendiente", oF.iIdPendiente);

                    int iFerry = oRes.S().I();

                    //// Lista de Mail
                    //foreach (ListaEnvios le in oF.oLstMail)
                    //{
                    //    DBSetInsertaRelacionFerryMail(iFerry, le.iIdLista);
                    //}

                    //// Lista de SMS
                    //foreach (ListaEnvios le in oF.oLstSMS)
                    //{
                    //    DBSetInsertaRelacionFerrySMS(iFerry, le.iIdLista);
                    //}

                    ban = iFerry > 0 ? true : false;

                    if (!ban)
                        break;
                    //}
                }

                return ban;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable DBGetFerrysPeriodoPendiente(int iIdPadre)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaOfertaFerrysPendiente]", "@IdPadre", iIdPadre);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable DBGetListaDifusionFerry(int iIdPendiente, string sTipoListaDifusion)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaListaDifusionOfertaFerryPendiente]", "@IdPendiente", iIdPendiente, "@TipoListaDifusion", sTipoListaDifusion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void DBUpdateListaDifusionFerry(int iIdPendiente, string sIdsListaDifusion, string sTipoListaDifusion)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spU_MXJ_ActualizaListaDifusionFerryPendiente]", "@IdPendiente", iIdPendiente, "@IdsListaDifusion", sIdsListaDifusion, "@TipoListaDifusion", sTipoListaDifusion, "@Usuario", Utils.GetUser);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DBDeleteOfertaFerryPendiente(int iIdPendiente)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spD_MXJ_EliminaFerryPendiente]", "@IdPendiente", iIdPendiente);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjListaDifusion(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaListaDifusionFerry]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetInsertaBitacoraWS(string sDesc, string sPagina, string sClase, string sMetodo)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Seguridad].[spI_MXJ_InsertaLogWS]", "@Descripcion", sDesc,
                                                                                        "@Pagina", sPagina,
                                                                                        "@Clase", sClase,
                                                                                        "@Metodo", sMetodo);

                return oRes.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneFerryPendientePadreHijo(int iPendiente)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ObtieneFerryPendientesPadreEHijos]", "@IdPendiente", iPendiente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}