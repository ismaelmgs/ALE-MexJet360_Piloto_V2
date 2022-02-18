using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.DomainModel
{
    public class DBDashboard : DBBase
    {
        public DataTable DBConsultaAvisos()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_DashboardAvisos]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBConsultaClientes()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_DashboardClientes]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBConsultaVuelos()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_DashboardVuelos]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}