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
    public class DBRol : DBBase
    {
        public DataTable dtObjsCat
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaRol]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable DBSearchObj(params object[] oArrFiltros)
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

        public int DBSave(Rol oRol)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaRol]","@Descripcion", oRol.sDescripcion,
                                                                                  "@IdModuloDefault", oRol.iIdModuloDefault,
                                                                                  "@Status", oRol.iStatus,
                                                                                  "@IP", oRol.sIP,
                                                                                  "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Rol), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBClone(Rol oRol)
        {
            object oResult;
            SqlDataAdapter dAdapterC = new SqlDataAdapter();
            SqlDataAdapter dAdapterU = new SqlDataAdapter();
            SqlConnection cOnnecU = new SqlConnection();
            SqlConnection cOnnecC = new SqlConnection();
            try
            {
                DataSet dsClone = oDB_SP.EjecutarDS("[Catalogos].[spS_MXJ_ConsultaClonarRol]", "@IdRolOri", oRol.iIdRolO,
                                                                                               "@IdRolDes", oRol.iIdRolD,
                                                                                               "@Usuario", Utils.GetUser);
                DataRow FilaOri;
                DataRow FilaDest;                                                               
                dsClone.Tables[0].TableName= "dtRolOri";
                dsClone.Tables[1].TableName= "dtRolDes";
                DataTable dtCRolOriCopy = dsClone.Tables[0].Clone();
                DataTable dtCRolDestCopy = dsClone.Tables[1].Clone();
                foreach (DataRow Fila in dsClone.Tables[0].Rows)
                {
                    FilaOri = dtCRolOriCopy.NewRow();
                    FilaOri["IdRolAccion"] = Fila["IdRolAccion"];
                    FilaOri["IdRol"] = Fila["IdRol"];
                    FilaOri["IdModulo"] = Fila["IdModulo"];
                    FilaOri["IdAccion"] = Fila["IdAccion"];
                    FilaOri["Permitido"] = Fila["Permitido"];
                    FilaOri["status"] = Fila["status"];
                    FilaOri["UsuarioCreacion"] = Fila["UsuarioCreacion"];
                    FilaOri["FechaCreacion"] = Fila["FechaCreacion"];
                    FilaOri["UsuarioModificacion"] = Fila["UsuarioModificacion"];
                    FilaOri["FechaModificacion"] = Fila["FechaModificacion"];
                    FilaOri["IP"] = Fila["IP"];
                    FilaOri["IdRolDes"] = Fila["IdRolDes"];
                    dtCRolOriCopy.Rows.Add(FilaOri);
                }
                foreach (DataRow Fila in dsClone.Tables[1].Rows)
                {
                    FilaDest = dtCRolDestCopy.NewRow();
                    FilaDest["IdRolAccion"] = Fila["IdRolAccion"];
                    FilaDest["IdRol"] = Fila["IdRol"];
                    FilaDest["IdModulo"] = Fila["IdModulo"];
                    FilaDest["IdAccion"] = Fila["IdAccion"];
                    FilaDest["Permitido"] = Fila["Permitido"];
                    FilaDest["status"] = Fila["status"];
                    FilaDest["UsuarioCreacion"] = Fila["UsuarioCreacion"];
                    FilaDest["FechaCreacion"] = Fila["FechaCreacion"];
                    FilaDest["UsuarioModificacion"] = Fila["UsuarioModificacion"];
                    FilaDest["FechaModificacion"] = Fila["FechaModificacion"];
                    FilaDest["IP"] = Fila["IP"];
                    dtCRolDestCopy.Rows.Add(FilaDest);
                }
                cOnnecU.ConnectionString = oDB_SP.sConexionSQL;
                if (dtCRolDestCopy.Rows.Count > 0)
                {
                    dAdapterU.InsertCommand = new SqlCommand("[Catalogos].[spU_MXJ_ActualizaRoldDestino]", cOnnecU);
                    dAdapterU.InsertCommand.CommandType = CommandType.StoredProcedure;
                    dAdapterU.InsertCommand.UpdatedRowSource = UpdateRowSource.None;

                    dAdapterU.InsertCommand.Parameters.Add("@IdRol", SqlDbType.Int, 32, dtCRolDestCopy.Columns[1].ColumnName);
                    dAdapterU.InsertCommand.Parameters.Add("@IdModulo", SqlDbType.Int, 32, dtCRolDestCopy.Columns[2].ColumnName);
                    dAdapterU.InsertCommand.Parameters.Add("@IdAccion", SqlDbType.Int, 32, dtCRolDestCopy.Columns[3].ColumnName);
                    dAdapterU.InsertCommand.Parameters.Add("@Usuario", SqlDbType.VarChar, 20, dtCRolDestCopy.Columns[6].ColumnName);
                    dAdapterU.InsertCommand.Parameters.Add("@IP", SqlDbType.VarChar, 20, dtCRolDestCopy.Columns[10].ColumnName);

                    dAdapterU.UpdateBatchSize = dtCRolDestCopy.Rows.Count;
                    cOnnecU.Open();
                    oResult = dAdapterU.Update(dtCRolDestCopy);
                    cOnnecU.Close();
                }

                cOnnecC.ConnectionString = oDB_SP.sConexionSQL;

                dAdapterC.InsertCommand = new SqlCommand("[Catalogos].[spI_MXJ_InsertaRolAccion]", cOnnecC);
                dAdapterC.InsertCommand.CommandType = CommandType.StoredProcedure;
                dAdapterC.InsertCommand.UpdatedRowSource = UpdateRowSource.None;

                dAdapterC.InsertCommand.Parameters.Add("@IdRol", SqlDbType.Int, 32, dtCRolOriCopy.Columns[11].ColumnName);
                dAdapterC.InsertCommand.Parameters.Add("@IdModulo", SqlDbType.Int, 32, dtCRolOriCopy.Columns[2].ColumnName);
                dAdapterC.InsertCommand.Parameters.Add("@IdAccion", SqlDbType.Int, 32, dtCRolOriCopy.Columns[3].ColumnName);
                dAdapterC.InsertCommand.Parameters.Add("@Permitido", SqlDbType.TinyInt, 1, dtCRolOriCopy.Columns[4].ColumnName);
                dAdapterC.InsertCommand.Parameters.Add("@Status", SqlDbType.TinyInt, 1, dtCRolOriCopy.Columns[5].ColumnName);
                dAdapterC.InsertCommand.Parameters.Add("@UsuarioCreacion", SqlDbType.VarChar, 20, dtCRolOriCopy.Columns[6].ColumnName);
                dAdapterC.InsertCommand.Parameters.Add("@IP", SqlDbType.VarChar, 20, dtCRolOriCopy.Columns[10].ColumnName);

                dAdapterC.UpdateBatchSize = dtCRolOriCopy.Rows.Count;
                cOnnecC.Open();
                oResult = dAdapterC.Update(dtCRolOriCopy);
                cOnnecC.Close();

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Rol), Convert.ToInt32(Enumeraciones.TipoOperacion.Clonar), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdate(Rol oRol)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaRol]", "@RolId", oRol.iId,
                                                                                     "@Descripcion", oRol.sDescripcion,
                                                                                     "@IdModuloDefault", oRol.iIdModuloDefault,
                                                                                     "@Status", oRol.iStatus,
                                                                                     "@IP", null,
                                                                                     "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Rol), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(Rol oRol)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaRol]", "@RolId", oRol.iId,
                                                                                    "@IP", null,
                                                                                    "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Rol), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValida(Rol oRol)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaRolExiste]", "@Descripcion", oRol.sDescripcion);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjModulos()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaModulosNoHeader]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}