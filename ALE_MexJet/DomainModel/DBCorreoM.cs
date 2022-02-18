using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ALE_MexJet.Clases;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBCorreoM : DBBase
    {
        public DataTable DBGetContacto()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaContactosCorreo]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(CorreoM c)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaCorreosMasivos]",
                                                                                    "@Motivo", c.sMotivo,
                                                                                    "@Asunto", c.sAsunto,
                                                                                    "@Destinatarios",c.sDestinatarios,
                                                                                    "@Copiados",c.sCopiados,
                                                                                    "@Contenido", c.sContenido,
                                                                                    "@Status", c.iStatus,
                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                    "@IP", c.sIP,
                                                                                    "@IdCorreo",c.iIdCorreo);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetCorreo(int x )
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaCorreoMasivo]", "@IdCorreoMasivo" , x);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}