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
    public class DBVendedor : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaVendedor]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjBase()
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
        public int DBSave(Vendedor oVendedor)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaVendedor]", "@Nombre", oVendedor.sNombre,
                                                                                        "@Zona", oVendedor.sZona,
                                                                                        "@IdUnidad4", oVendedor.sIdUnidad4,
                                                                                        "@UnidadNegocio", oVendedor.sUnidadNegocio,
                                                                                        "@Login", oVendedor.sLogin,
                                                                                        "@IdUnidad1", oVendedor.sIdUnidad1,
                                                                                        "@DescripcionUnidad", oVendedor.sDescripcionUnidad,
                                                                                        "@CorreoElectronico", oVendedor.sCorreoElectronico,
                                                                                        "@IdBase", oVendedor.iIdBase,
                                                                                        "@Status", oVendedor.iStatus,
                                                                                        "@IP", oVendedor.sIP,
                                                                                        "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Vendedor.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se inserto el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(Vendedor oVendedor)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaVendedor]",   "@IdVendedor", oVendedor.iIdVendedor,
                                                                                            "@Nombre", oVendedor.sNombre,
                                                                                            "@Zona", oVendedor.sZona,
                                                                                            "@IdUnidad4", oVendedor.sIdUnidad4,
                                                                                            "@UnidadNegocio", oVendedor.sUnidadNegocio,
                                                                                            "@Login", oVendedor.sLogin,
                                                                                            "@IdUnidad1", oVendedor.sIdUnidad1,
                                                                                            "@DescripcionUnidad", oVendedor.sDescripcionUnidad,
                                                                                            "@CorreoElectronico", oVendedor.sCorreoElectronico,
                                                                                            "@IdBase", oVendedor.iIdBase,
                                                                                            "@estatus", oVendedor.iStatus,
                                                                                            "@Usuario", Utils.GetUser,
                                                                                            "@IP", oVendedor.sIP
                                                                                        );
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Vendedor.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizo el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(Vendedor oVendedor)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaVendedor]",  "@IdVendedor", oVendedor.iIdVendedor,
                                                                                            "@IP", oVendedor.sIP,
                                                                                            "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se elimino el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(Vendedor oVendedor)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaVendedorExiste]",  "@Login", oVendedor.sLogin,
                                                                                                "@estatus", oVendedor.iStatus);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetCodUnidad4()
        {
            DataTable DsUnidad4;
            //ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
            DsUnidad4 = new DBSAP().DBGetCodigoUnidad4; //wssyte.GetCodigoUnidad_Cuatro();
            return DsUnidad4;
        }
        public DataTable DBGetCodUnidad1V()
        {
            DataSet DsUnidad1V;
            ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
            DsUnidad1V = wssyte.GetCodigoUnidad_Uno_Vendedor();
            return DsUnidad1V.Tables[0];
        }
        public string DBGetDesUnidad4(string clave)
        {
            //ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
            return new DBSAP().DBGetDescripcionCodigoUnidad4(clave); //wssyte.GetDescripcion_CodigoUnidadCUATRO(clave);
        }
        public string DBGetDesUnidad1V(string clave)
        {
            //ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
            return new DBSAP().DBGetDescripcionCodigoUnidad1(clave); //wssyte.GetDescripcion_CodigoUnidadUNO(clave);
        }
    }
}