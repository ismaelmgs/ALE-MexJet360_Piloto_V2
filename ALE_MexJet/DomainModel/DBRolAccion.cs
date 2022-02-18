using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ALE_MexJet.Clases;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBRolAccion : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaRolAccion]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(RolAccion oRolAccion)
        {
            object oResult;
            SqlDataAdapter dAdapter = new SqlDataAdapter();
            SqlConnection cOnnec = new SqlConnection();
            try
            {
                cOnnec.ConnectionString = oDB_SP.sConexionSQL;
                dAdapter.InsertCommand = new SqlCommand("[Catalogos].[spI_MXJ_InsertaRolAccion]", cOnnec);
                dAdapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                dAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;

                dAdapter.InsertCommand.Parameters.Add("@IdRol", SqlDbType.Int, 32, oRolAccion.dtaRolAccion.Columns[0].ColumnName);
                dAdapter.InsertCommand.Parameters.Add("@IdModulo", SqlDbType.Int, 32, oRolAccion.dtaRolAccion.Columns[1].ColumnName);
                dAdapter.InsertCommand.Parameters.Add("@IdAccion",SqlDbType.Int,32,oRolAccion.dtaRolAccion.Columns[2].ColumnName);
                dAdapter.InsertCommand.Parameters.Add("@Permitido",SqlDbType.TinyInt,1,oRolAccion.dtaRolAccion.Columns[3].ColumnName);
                dAdapter.InsertCommand.Parameters.Add("@Status", SqlDbType.TinyInt, 1, oRolAccion.dtaRolAccion.Columns[4].ColumnName);
                dAdapter.InsertCommand.Parameters.Add("@UsuarioCreacion", SqlDbType.VarChar, 20, oRolAccion.dtaRolAccion.Columns[5].ColumnName);
                dAdapter.InsertCommand.Parameters.Add("@IP", SqlDbType.VarChar, 20, oRolAccion.dtaRolAccion.Columns[7].ColumnName);

                dAdapter.UpdateBatchSize = oRolAccion.dtaRolAccion.Rows.Count;
                cOnnec.Open();
                oResult = dAdapter.Update(oRolAccion.dtaRolAccion);
                cOnnec.Close();
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(RolAccion oRolAccion)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaRolAccion]", "@IdRol", oRolAccion.iIdRol,
                                                                                    "@IP", oRolAccion.sIP,
                                                                                    "@Usuario", Utils.GetUser);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtTipoRolAccion(params object[] oArrFiltros)
        {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaRol]", oArrFiltros);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }
        
    }
    
}