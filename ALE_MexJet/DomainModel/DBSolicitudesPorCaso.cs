using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBSolicitudesPorCaso : DBBase
    {
        public DataTable DBSearchSolicitudesPorCaso(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaSolicitudesPorCaso]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBSearchTiposCaso(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaTiposCaso]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}