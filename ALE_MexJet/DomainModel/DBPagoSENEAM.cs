using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.DomainModel
{
    public class DBPagoSENEAM:DBBase
    {
        public DataTable DBSearchPagoSENEAM(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaPagoSENEAM]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}