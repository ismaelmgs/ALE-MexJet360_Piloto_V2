using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace ALE_MexJet.DomainModel
{
    public class DBErroresBitacora : DBBase
    {
        public DataTable DBConsultaErrores(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[FileTransfer].[spS_MXJ_ConsultaErroresBitacora]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}