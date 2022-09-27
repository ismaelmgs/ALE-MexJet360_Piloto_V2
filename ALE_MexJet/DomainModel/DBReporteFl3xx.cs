using ALE_MexJet.Objetos;
using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.DomainModel
{
    public class DBReporteFl3xx
    {
        public DataTable GetReporteFl3xx(string sFechaInicio, string sFechaFin)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = new DBBaseAleSuite().oDB_SP.EjecutarDT("[FLX].[spS_ConsultaReporteFl3xx]", "@FechaInicio", sFechaInicio.Dt().ToString("yyyy-MM-dd"), "@FechaFinal", sFechaFin.Dt().ToString("yyyy-MM-dd"));
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}