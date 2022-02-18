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
    public class DBAeronave : DBBase
    {

        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaAeronave]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBSearchCodigoUnidadDosFlota(params object[] oArrFiltrosCodigoUnidadDos)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaFlota]", oArrFiltrosCodigoUnidadDos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtObjModelo(params object[] oArrFiltrosModelo)
        {
            //get
            //{
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaModelo]", oArrFiltrosModelo);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            //}
        }

        public int DBSave(Aeronave oAeronave)
        {
            try
            {
    
                object oResult;

                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaAeronave]",  "@IdMarca", oAeronave.iIdMarca,
                                                                                         "@IdModelo", oAeronave.iIdModelo,
                                                                                         "@IdFlota",oAeronave.iIFlota,
                                                                                         "@IdTipo", oAeronave.iIdTipo,
                                                                                         "@Matricula", oAeronave.sMatricula,
                                                                                         "@Serie", oAeronave.sSerie,
                                                                                         "@IdAeropuertoBase", oAeronave.iIdAeropuertoBase == 0 ? null : oAeronave.iIdAeropuertoBase,
                                                                                         "@AñoFabricacion", oAeronave.iAñoFabricacion,
                                                                                         "@CapacidadPasajeros", oAeronave.iCapacidadPasajero,
                                                                                         "@IdMatriculaInfo", oAeronave.sIdMAtriculaInfo,
                                                                                         "@MatriculaInfo", oAeronave.sMAtriculaInfo,
                                                                                         "@IdBaseInfo", oAeronave.sIdBaseInfo,
                                                                                         "@BaseInfo", oAeronave.sBaseInfo,
                                                                                         "@IdUnidadNegocioInfo", oAeronave.sIdUnidadNegocio,
                                                                                         "@UnidadNegocioInfo", oAeronave.sUnidadNegocio,
                                                                                         "@ReporteSENEAM", oAeronave.bReporteSENEAM,
                                                                                         "@Status", oAeronave.iStatus,
                                                                                         "@Usuario", oAeronave.sUsuarioMod,
                                                                                         "@IP", oAeronave.sIP);

                int iIdAeronave  = oResult.S().I();
                if (iIdAeronave > 0)
                {
                    oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaHistoricoAeronave]", "@Id", iIdAeronave,
                                                                                                     "@Matricula", oAeronave.sMatricula,
                                                                                                     "@Serie", oAeronave.sSerie,
                                                                                                     "@IdFlota", oAeronave.iIFlota,
                                                                                                     "@Status", 1,
                                                                                                     "@Usuario", oAeronave.sUsuarioMod,
                                                                                                     "@IP", oAeronave.sIP,
                                                                                                     "@FechaInicio", oAeronave.dtFechaInicio,
                                                                                                     "@FechaFin", null
                                                                                                     );
                }

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdate(Aeronave oAeronave)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaAeronave]","@Id", oAeronave.iId,
                                                                                         "@IdMarca", oAeronave.iIdMarca,
                                                                                         "@IdModelo", oAeronave.iIdModelo,
                                                                                         "@IdFlota", oAeronave.iIFlota,
                                                                                         "@IdTipo", oAeronave.iIdTipo,
                                                                                         "@Matricula", oAeronave.sMatricula,
                                                                                         "@Serie", oAeronave.sSerie,
                                                                                         "@IdAeropuertoBase", oAeronave.iIdAeropuertoBase,
                                                                                         "@AñoFabricacion", oAeronave.iAñoFabricacion,
                                                                                         "@CapacidadPasajeros", oAeronave.iCapacidadPasajero,
                                                                                         "@IdMatriculaInfo", oAeronave.sIdMAtriculaInfo,
                                                                                         "@MatriculaInfo", oAeronave.sMAtriculaInfo,
                                                                                         "@IdBaseInfo", oAeronave.sIdBaseInfo,
                                                                                         "@BaseInfo", oAeronave.sBaseInfo,
                                                                                         "@IdUnidadNegocioInfo", oAeronave.sIdUnidadNegocio,
                                                                                         "@UnidadNegocioInfo", oAeronave.sUnidadNegocio,
                                                                                         "@ReporteSENEAM", oAeronave.bReporteSENEAM,
                                                                                         "@Status", oAeronave.iStatus,
                                                                                         "@Usuario", oAeronave.sUsuarioMod,
                                                                                         "@IP", oAeronave.sIP);

                int iIdAeronave = oResult.S().I();
                if (iIdAeronave > 0)
                {
                    oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaHistoricoAeronave]", "@Id", iIdAeronave,
                                                                                                     "@Matricula", oAeronave.sMatricula,
                                                                                                     "@Serie", oAeronave.sSerie,
                                                                                                     "@IdFlota", oAeronave.iIFlota,
                                                                                                     "@Status", 1,
                                                                                                     "@Usuario", oAeronave.sUsuarioMod,
                                                                                                     "@IP", oAeronave.sIP,
                                                                                                     "@FechaInicio", oAeronave.dtFechaInicio,
                                                                                                     "@FechaFin", oAeronave.dtFechaFin
                                                                                                     );
                }
                    
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(Aeronave oAeronave)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaAeronave]", "@Id", oAeronave.iId,
                                                                                    "@IP", null,
                                                                                    "@Usuario", Utils.GetUser);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(Aeronave oAeronave) 
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaAeronaveExiste]", "@Serie", oAeronave.sSerie);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DBGetCodUnidad2(string clave)
        {
            //ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
            //return wssyte.GetDescripcion_CodigoUnidadDOS(clave);
            return new DBSAP().DBGetDescripcionCodigoUnidad2(clave);
        }

        public string DBGetCodUnidad3(string clave)
        {
            //ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
            //return wssyte.GetDescripcion_CodigoUnidadTRES(clave);
            return new DBSAP().DBGetDescripcionCodigoUnidad3(clave);
        }
        public string DBGetCodUnidad4(string clave)
        {
            try
            {
                //ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                //return wssyte.GetDescripcion_CodigoUnidadCUATRO(clave);
                return new DBSAP().DBGetDescripcionCodigoUnidad4(clave);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable dtObjMarca
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaMarcas]","@IdMArca", 0,
                                                                                   "@Descripcion","%%",
                                                                                   "@Estatus", 1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjFlota
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaFlota]","@IdFlota",0,
                                                                                    "@Descripcion","%%",
                                                                                    "@Estatus",1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjBase
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaAeropuertoBase]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        

        public DataTable dtObjMatriculaInfor
        {
            get
            {
                try
                {
                    //ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                    //DataSet dsMatriculaInfor = wssyte.GetCodigoUnidad_Dos();
                    return new DBSAP().DBGetCodigoUnidad2; //dsMatriculaInfor.Tables[0];

                   
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjBaseInfor
        {
            get
            {
                try
                {
                    //ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                    //DataSet dsMatriculaInfor = wssyte.GetCodigoUnidad_Tres();
                    return new DBSAP().DBGetCodigoUnidad3; //dsMatriculaInfor.Tables[0];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjUnidadNegocioInfor
        {
            get
            {
                try
                {
                    //ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                    //DataSet dsMatriculaInfor = wssyte.GetCodigoUnidad_Cuatro();
                    return new DBSAP().DBGetCodigoUnidad4; //dsMatriculaInfor.Tables[0];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


    }
}