using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Objetos;
using System.Data;
using ALE_MexJet.Clases;
using NucleoBase.Core;
using NucleoBase.BaseDeDatos;

namespace ALE_MexJet.DomainModel
{
    public class DBHoraVueloMatricula : DBBase
    {
        public DataTable dtObjCliente()
        {
            try
            {
                DocPendientes B = new DocPendientes();
                return oDB_SP.EjecutarDT("Catalogos.spS_MXJ_ConsultaClienteDDL", "@status", 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjContrato(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaContratosDDL]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjConsultaCliente(params object[] oArrClient)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaHorasVoladasCliente]", oArrClient);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjConsultaMatricula(params object[] oArrMatricula)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaHorasVoladasMatrucula]", oArrMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjFlota()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaFlotaDDL]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjConsultaFlota(params object[] oArrFlota)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaHorasVoladasFlota]", oArrFlota);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}