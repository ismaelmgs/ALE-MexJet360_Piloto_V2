using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBGeneracionPagosAPHIS:DBBase
    {
        public DataTable GeneracionCalculoPagoAPHIS(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("Catalogos.spS_MXJ_GeneracionCalculoPagoAPHIS", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }            
    }
}