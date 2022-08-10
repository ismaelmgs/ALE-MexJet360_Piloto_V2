using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.DomainModel
{
    public class DBConsultaViaticosVuelos : DBBaseAleSuite
    {
        public DataTable GetVuelosViaticos(string sFechaDesde, string sFechaHasta, string sParametro) 
        {
            try
            {
                DataTable dt = new DataTable();
                dt = oDB_SP.EjecutarDT("[VB].[spS_MXJ_ConsultaVuelos]", "@FechaDesde", sFechaDesde.Dt(), "@FechaHasta", sFechaHasta.Dt(), "@Parametro", sParametro);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}