using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.Data;
using ALE_MexJet.Objetos;
using ALE_MexJet.Clases;
using System.Configuration;
using System.Data.SqlClient;

namespace ALE_MexJet.DomainModel
{
    public class DBExtensionServiciosFPK : DBBaseFPK
    {
        SqlConnection _SqlConnection;
        private SqlCommand _SqlCommand = new SqlCommand();
        private SqlParameter _SqlParameter = new SqlParameter();
        private SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();

        public string DBObtieneLicenciaPiloto(string sCrewCode)
        {
            string Resultado = string.Empty;
            
            try
            {
                DataTable dtResultado = new DataTable();

                string sNombre = Utils.ObtieneParametroPorClave("101");

                string sQuery = "SELECT * FROM OPENQUERY(" + sNombre + ", 'SELECT CREWCODE,PILOTLIC FROM CREW WHERE CREWCODE =''" + sCrewCode + "''') ";

                dtResultado = oDB_SP.EjecutarDT_DeQuery(sQuery);

                foreach (DataRow Fila in dtResultado.Rows)
                {
                    Resultado = Fila["PILOTLIC"].S();
                }

                return Resultado;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}