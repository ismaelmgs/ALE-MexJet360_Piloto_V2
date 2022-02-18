using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ALE_MexJet.Clases;
using NucleoBase.Core;
using System.ServiceModel;
using ALE_MexJet.wsSyteline;

namespace ALE_MexJet.DomainModel
{
    public class DBTipoCliente : DBBase
    {
        public DataTable dtObjsCat
        {
            get
            {
                try
                {
                    //wsSyteline.Iws_SyteLineClient wsSyt = new Iws_SyteLineClient();
                    //DataSet dsCodUniCuatro = wsSyt.GetCodigoUnidad_Cuatro();
                    DataTable dtCodUniCuatro = new DBSAP().DBGetCodigoUnidad4; //dsCodUniCuatro.Tables[0];
                    return dtCodUniCuatro;
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTipoCliente]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(TipoCliente oTipoCliente)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaTipoCliente]", "@TipoClienteDescripcion", oTipoCliente.sTipoClienteDescripcion,
                                                                                           "@CodigoDeUnidad4", oTipoCliente.sCodigoDeUnidad4,
                                                                                           "@CodigoDeUnidad4Descripcion", oTipoCliente.sCodigoDeUnidad4Descripcion,
                                                                                           "@HrsPernocta",oTipoCliente.iHrsPernocta,
                                                                                           "@IP", oTipoCliente.sIP,
                                                                                           "@Usuario", Utils.GetUser);//piloto.sUsuarioCreacion,
                //"@fc_IP	", "101.1.15.2");//piloto.sIP);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdate(TipoCliente oTipoCliente)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaTipoCliente]",    "@Id", oTipoCliente.iId,
                                                                                                "@TipoClienteDescripcion", oTipoCliente.sTipoClienteDescripcion,
                                                                                                "@CodigoDeUnidad4", oTipoCliente.sCodigoDeUnidad4,
                                                                                                "@CodigoDeUnidad4Descripcion", oTipoCliente.sCodigoDeUnidad4Descripcion,
                                                                                                "@HrsPernocta",oTipoCliente.iHrsPernocta,
                                                                                                "@Status", oTipoCliente.iStatus,
                                                                                                "@IP", oTipoCliente.sIP,
                                                                                                "@Usuario", Utils.GetUser);
                                                                    
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(TipoCliente oTipoCliente)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaTipoCliente]",  "@Id", oTipoCliente.iId,
                                                                                            "@IP", null,
                                                                                            "@Usuario", Utils.GetUser);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetCodUnit4Descripcion(string sClave)
        {
            //wsSyteline.Iws_SyteLineClient wsSyt = new Iws_SyteLineClient();
            //return wsSyt.GetDescripcion_CodigoUnidadCUATRO(sClave);
            return new DBSAP().DBGetDescripcionCodigoUnidad4(sClave);

        }
        public int DBValida(TipoCliente oTipoCliente)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaTipoClienteExiste]", "@TipoClienteDescripcion", oTipoCliente.sTipoClienteDescripcion,
                    "@CodigoDeUnidad4", oTipoCliente.sCodigoDeUnidad4);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}