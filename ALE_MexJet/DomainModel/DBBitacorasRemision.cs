using ALE_MexJet.Objetos;
using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ALE_MexJet.DomainModel
{
    public class DBBitacorasRemision
    {
        public DataTable GetBitacorasSinRemisiones()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = new DBBase().oDB_SP.EjecutarDT("[Principales].[spS_MXJ_BitacorasNoRemisionadas]");
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}