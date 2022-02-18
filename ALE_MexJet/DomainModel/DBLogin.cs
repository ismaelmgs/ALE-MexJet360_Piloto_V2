using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBLogin : DBBase
    {
        public DataTable DBSaveToken(int Token, string Usuario)
        {
            try
            {
                return oDB_SP.EjecutarDT("Seguridad.spI_MXJ_InsertarToken", "@ClaveToken", Token,
                                                                            "@Usuario", Usuario,
                                                                            "@Sts", 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long DBUpdateToken(int IdToken, int Token)
        {
            try
            {
                object oBit = oDB_SP.EjecutarValor("Seguridad.spU_MXJ_UpdateToken", "@IdToken", IdToken,
                                                                                    "@Token", Token);

                return oBit != null ? oBit.S().L() : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBConsultaToken(string Usuario, string sToken)
        {
            try
            {
                object oBit = oDB_SP.EjecutarValor("Seguridad.spS_MXJ_ConsultaUsuarioToken", "@NombreEntrada", Usuario,
                                                                                            "@Token", sToken);

                return oBit.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}