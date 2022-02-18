using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ALE_MexJet.DomainModel
{
    public class DBConsultaExtension : DBBase
    {
        public DataTable DBGetObtieneExtensionesServicio( object [] oArrFil)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaExtensionesServicio]", oArrFil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}