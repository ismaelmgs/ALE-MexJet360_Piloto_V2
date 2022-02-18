using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.DomainModel
{
    public class DBResumenContrato : DBBase
    {

        public DataTable DBGetObtieneClientesActivosInactivos
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_CosultaClientesActivosInactivos]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet DBGetObtieneReporteResumen(int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaResumenContrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneTransferenciasPorPeriodo(int iIdResumen)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaTraspasosPorPeriodo]", "@IdResumen", iIdResumen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetObtieneResumenContratoRepor(int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaReporteResumenContrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetActualizaAnualidadesFacturas(List<ResumenContrato> oLst)
        {
            try
            {
                foreach(ResumenContrato o in oLst)
                {
                    oDB_SP.EjecutarSP("[Principales].[spU_MXJ_ActualizaMontosAnualidades]", "@IdResumen", o.iIdResumen,
                                                                                            "@Anualidades", o.dAnualidades,
                                                                                            "@NoFactura", o.sFacturas,
                                                                                            "@UsuarioModificacion", Utils.GetUser,
                                                                                            "@IP", Utils.GetIPAddress());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}