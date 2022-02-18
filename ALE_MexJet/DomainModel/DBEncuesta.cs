using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.DomainModel
{
    public class DBEncuesta : DBBase
    {
        public void InsertaNodoEncuesta(Encuesta oEn)
        {
            try
            {
                oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaNodoEncuesta]", "@IdPadre", oEn.iIdPadre,
                                                                                    "@Descripcion", oEn.sDescripcionNodo,
                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                    "@IP", Utils.GetIPAddress());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaNodoEncuesta(Encuesta oEn)
        {
            try
            {
                oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaNodoEncuesta]", "@IdEncuentas", oEn.iIdEncuesta,
                                                                                    "@Descripcion", oEn.sDescripcionNodo,
                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                    "@IP", Utils.GetIPAddress());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}