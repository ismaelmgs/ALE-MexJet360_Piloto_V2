using ALE_MexJet.Objetos;
using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ALE_MexJet.DomainModel
{
    public class DBAeropuertosEspeciales
    {
        public DataTable GetAeropuertos()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = new DBBase().oDB_SP.EjecutarDT("[VB].[spS_MXJ_ConsultaAeropuertos]");
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetAeropuertosEspeciales()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = new DBBase().oDB_SP.EjecutarDT("[VB].[spS_MXJ_ConsultaAeropuertosEspeciales]");
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int SetInsertaAeropuertosEspeciales(string sPOA)
        {
            try
            {
                object oRes = new DBBase().oDB_SP.EjecutarValor("[VB].[spS_MXJ_InsertaAeropuertosEspeciales]", "@POA", sPOA);
                return oRes.S().I();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public bool SetEliminarAeropuertoEspecial(int IdEspecial)
        {
            try
            {
                bool bRes = false;
                object oRes;
                oRes = new DBBase().oDB_SP.EjecutarValor("[VB].[spD_MXJ_EliminarAeropuertosEspeciales]", "@IdEspecial", IdEspecial);
                if (oRes.S().I() != 0)
                    bRes = true;

                return bRes;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}