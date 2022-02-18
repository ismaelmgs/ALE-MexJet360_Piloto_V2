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
    public class DBPasajero : DBBase
    {
        public DataTable DBConsultaCliente(params object[] oArrFiltrosCliente)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCliente]", oArrFiltrosCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBConsultaPasajeros(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaPasajero]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBEliminaPasajeroPorId(int iIdPasajero)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaPasajero]", "@IdPasajero", iIdPasajero,
                                                                                        "@Usuario", Utils.GetUser,
                                                                                        "@IP", "");

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public int DBInsertaPasajeroPerfil(Pasajero objPasajero)
        {
            try
            {
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaPerfilPasajero]", "@last_name", objPasajero.sLast_name,
                                                                                              "@first_name", objPasajero.sFirst_name,
                                                                                              "@Status", objPasajero.iStatus,
                                                                                              "@UsuarioCreacion", Utils.GetUser,
                                                                                              "@IP", "",
                                                                                              "@IdCliente", objPasajero.iIdCliente,
                                                                                              "@Pasatiempos", objPasajero.sPasatiempos,
                                                                                              "@PerfilLinkedin", objPasajero.sPerfilLinkedin,
                                                                                              "@PerfilFacebook", objPasajero.sPerfilFacebook,
                                                                                              "@PerfilInstagram", objPasajero.sPerfilInstagram,
                                                                                              "@PerfilTwitter", objPasajero.sPerfilTwitter,
                                                                                              "@EstadoCivil", objPasajero.iEstadoCivil,
                                                                                              "@ProtocoloCliente", objPasajero.iProtocoloCliente,
                                                                                              "@Alergias", objPasajero.iAlergias,
                                                                                              "@CualesAlergias", objPasajero.sCualesAlergias,
                                                                                              "@CondicionesMedicasEspeciales", objPasajero.iCondicionesMedicasEspeciales,
                                                                                              "@CualesCondicionesMedicasEspeciales", objPasajero.sCualesCondicionesMedicasEspeciales,
                                                                                              "@Fobias", objPasajero.iFobias,
                                                                                              "@CualesFobias", objPasajero.sCualesFobias,
                                                                                              "@MultiplesNacionalidades", objPasajero.iMultiplesNacionalidades,
                                                                                              "@CualesMultiplesNacionalidades", objPasajero.sCualesMultiplesNacionalidades,
                                                                                              "@RestriccionesAlimenticias", objPasajero.sRestriccionesAlimenticias,
                                                                                              "@Deportes", objPasajero.sDeportes);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();

            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public int DBActualizaPasajeroPerfil(Pasajero objPasajero)
        {
            try
            {
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaPerfilPasajero]",
                                                                                              "@IdPasajero", objPasajero.iIdPasajero,
                                                                                              "@last_name", objPasajero.sLast_name,
                                                                                              "@first_name", objPasajero.sFirst_name,
                                                                                              "@Status", objPasajero.iStatus,
                                                                                              "@UsuarioModificacion", Utils.GetUser,
                                                                                              "@IP", "",
                                                                                              "@Pasatiempos", objPasajero.sPasatiempos,
                                                                                              "@PerfilLinkedin", objPasajero.sPerfilLinkedin,
                                                                                              "@PerfilFacebook", objPasajero.sPerfilFacebook,
                                                                                              "@PerfilInstagram", objPasajero.sPerfilInstagram,
                                                                                              "@PerfilTwitter", objPasajero.sPerfilTwitter,
                                                                                              "@EstadoCivil", objPasajero.iEstadoCivil,
                                                                                              "@ProtocoloCliente", objPasajero.iProtocoloCliente,
                                                                                              "@Alergias", objPasajero.iAlergias,
                                                                                              "@CualesAlergias", objPasajero.sCualesAlergias,
                                                                                              "@CondicionesMedicasEspeciales", objPasajero.iCondicionesMedicasEspeciales,
                                                                                              "@CualesCondicionesMedicasEspeciales", objPasajero.sCualesCondicionesMedicasEspeciales,
                                                                                              "@Fobias", objPasajero.iFobias,
                                                                                              "@CualesFobias", objPasajero.sCualesFobias,
                                                                                              "@MultiplesNacionalidades", objPasajero.iMultiplesNacionalidades,
                                                                                              "@CualesMultiplesNacionalidades", objPasajero.sCualesMultiplesNacionalidades,
                                                                                              "@RestriccionesAlimenticias", objPasajero.sRestriccionesAlimenticias,
                                                                                              "@Deportes", objPasajero.sDeportes);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}