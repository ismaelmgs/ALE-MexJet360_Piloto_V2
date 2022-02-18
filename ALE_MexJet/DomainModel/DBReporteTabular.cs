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
    public class DBReporteTabular : DBBase
    {
        public DataTable dtMatricula
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoMatriculaReporteTabulado]");
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }
        
        public DataTable dtCliente
        {
           get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoClienteReporteTabulado]");
                }
               catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtContrato
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoClavesContratoReporteTabulado]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtGrupoModelo
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoGrupoModeloReporteTabulado]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtBases
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoAeropuertoBasesReporteTabulado]");
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable DBGetConsultaReporteTabulado(object[] oArrFiltros)
        {
            try
            {
                //return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaReporteTabulado]", oArrFiltros);
                SqlConnection conn = new SqlConnection();

                try
                {
                    DataSet dsRes = new DataSet();
                    conn.ConnectionString = oDB_SP.sConexionSQL;
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("[Principales].[spS_MXJ_ConsultaReporteTabulado]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdAeronave", oArrFiltros[1]);
                    cmd.Parameters.AddWithValue("@IdCliente", oArrFiltros[3]);
                    cmd.Parameters.AddWithValue("@IdContrato", oArrFiltros[5]);
                    cmd.Parameters.AddWithValue("@IdGrupoModelo", oArrFiltros[7]);
                    cmd.Parameters.AddWithValue("@IdAeropuerto", oArrFiltros[9]);
                    cmd.Parameters.AddWithValue("@FechaIni", oArrFiltros[11]);
                    cmd.Parameters.AddWithValue("@fechaFin", oArrFiltros[13]);

                    cmd.CommandTimeout = 0;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dsRes);
                    conn.Close();

                    return dsRes.Tables[0];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Dispose();
                        conn = null;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string DBGetTiposFactura
        {
            get
            {
                try
                {
                    DataTable dt = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaTiposFacturaTabulado]");
                    if (dt.Rows.Count > 0)
                        return dt.Rows[0][0].S();
                    else
                        return string.Empty;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}