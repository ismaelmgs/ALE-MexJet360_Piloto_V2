using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.DomainModel
{
    public class DBTableroFerry : DBBase
    {
        public DataTable DBGetFerrysPeriodo(object[] oArr)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaFerrysTableroControl]", oArr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetActualizaEstatusFerry(TableroFerry oFerry)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaStatusFerry]", "@IdFerry", oFerry.iIdFerry,
                                                                                                    "@StatusJetSmart", oFerry.iStatusJetSmart,
                                                                                                    "@StatusApp", oFerry.iStatusApp,
                                                                                                    "@UsuarioModificacion", Utils.GetUser,
                                                                                                    "@IP", Utils.GetIPAddress());
                
                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}