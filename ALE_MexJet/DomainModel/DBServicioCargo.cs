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
    public class DBServicioCargo : DBBase
    {
        public DataTable dtObjCliente()
        {
            try
            {
                
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
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaContratosDDL]",oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet dtObjServicioCargo(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaServiciosCargo]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}