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
    public class DBTramoPactado : DBBase
    {
        public int DbSearchIdAeropuertoIata(string sIATA)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaIdAeropuertoIata]", "@IATA", sIATA);
                return oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtObjsOrigen
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaOrigenAeropuertoContrato]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable DBCargaDestino(int idOrigen)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaDestinoAeropuertoTramos]", "@IdOrigen", idOrigen);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBFiltraAeropuertoIATA(string letra)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoAeropuertoIATA]", "@IATA", letra + "%");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBFiltraAeropuertoIATADestino(string letra, int iOrigen)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoAeropuertoIATADestino]", "@IATA", letra + "%",
                                                                                                         "@IdOrigen", iOrigen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable dtObjsCat
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaGrupoModelo]", "@GrupoModeloId" , 0,
                                                                                            "@Descripcion", "%%",
                                                                                            "@ConsumoGalones",0,
                                                                                            "@estatus",1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjsCatAeropuerto
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaAeropuerto]", "@IATA", "%%",
                                                                                        "@ICAO","%%",
                                                                                        "@Descripcion","%%",
                                                                                            "@estatus", -1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTramoPactado]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(TramoPactado oTramoPactado)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaTramoPactado]", "@IdGrupoModelo", oTramoPactado.iIdGrupoModelo,
                                                                                            "@IdOrigen", oTramoPactado.iIdOrigen,
                                                                                            "@IdDestino", oTramoPactado.iIdDestino,
                                                                                            "@TiempoDeVuelo", oTramoPactado.sTiempoVuelo,
                                                                                            "@Status", oTramoPactado.iStatus,
                                                                                            "@IP", oTramoPactado.sIP,
                                                                                            "@Usuario", oTramoPactado.sUsuarioCreacion);
                //"@fc_IP	", "101.1.15.2");//piloto.sIP);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdate(TramoPactado oTramoPactado)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaTramoPactado]",   "@Id", oTramoPactado.iId, 
                                                                                                "@IdGrupoModelo", oTramoPactado.iIdGrupoModelo,
                                                                                                "@IdOrigen", oTramoPactado.iIdOrigen,
                                                                                                "@IdDestino", oTramoPactado.iIdDestino,
                                                                                                "@TiempoDeVuelo", oTramoPactado.sTiempoVuelo,
                                                                                                "@Status", oTramoPactado.iStatus,
                                                                                                "@IP", oTramoPactado.sIP,
                                                                                                "@Usuario", oTramoPactado.sUsuarioCreacion);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(TramoPactado oTramoPactado)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaTramoPactado]", "@Id", oTramoPactado.iId,
                                                                                    "@IP", null,
                                                                                    "@Usuario", Utils.GetUser);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(TramoPactado oTramoPactado)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaTramoPactadoExiste]", "@IdOrigen", oTramoPactado.iIdOrigen,
                                                                                                    "@IdDestino", oTramoPactado.iIdDestino,
                                                                                                    "@IdGrupoModelo",oTramoPactado.iIdGrupoModelo);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}