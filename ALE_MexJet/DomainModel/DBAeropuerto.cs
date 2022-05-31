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
    public class DBAeropuerto : DBBase
    {
        public DataTable dtObjsCat
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaPilotos]");
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaAeropuerto]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(Aeropuerto oAeropuerto)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaAeropuerto]", "@Descripcion", oAeropuerto.sDescripcion,
                                                                                    "@AeropuertoEmailCC", oAeropuerto.sCiudad,
                                                                                    "@AeropuertoEmail", oAeropuerto.sCiudad,
                                                                                    "@Status", oAeropuerto.iStatus,
                                                                                    "@IP", oAeropuerto.sIP,
                                                                                    "@Usuario", Utils.GetUser);//piloto.sUsuarioCreacion,
                //"@fc_IP	", "101.1.15.2");//piloto.sIP);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(Aeropuerto oAeropuerto)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaAeropuerto]", "@Id", oAeropuerto.iId,
                                                                                            "@Base", oAeropuerto.bBase,
                                                                                            "@TipoDestino", oAeropuerto.TipoDestino,
                                                                                            "@AeropuertoHelipuerto", oAeropuerto.bAeropuertoHelipuerto,
                                                                                            "@AeropuertoHelipuertoTarifa", oAeropuerto.dAeropuertoHelipuertoTarifa,
                                                                                            "@SeCobraAterrizaje", oAeropuerto.bCobraAterrizaje,
                                                                                            "@AterrizajeNal", oAeropuerto.dAterrizajeNal,
                                                                                            "@AterrizajeInt", oAeropuerto.dAterrizajeInt,
                                                                                            "@Status", oAeropuerto.iStatus,
                                                                                            "@IP", oAeropuerto.sIP,
                                                                                            "@Usuario", oAeropuerto.sUsuarioMod,
                                                                                            "@TipoAeropuerto", oAeropuerto.iTipoAeropuerto);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(Aeropuerto oAeropuerto)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaAeropuerto]", "@Id", oAeropuerto.iId,
                                                                                    "@IP", null,
                                                                                    "@Usuario", Utils.GetUser);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}