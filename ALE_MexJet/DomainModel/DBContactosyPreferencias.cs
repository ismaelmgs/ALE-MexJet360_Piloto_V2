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
    public class DBContactosyPreferencias : DBBase
    {
        public DataTable DBSearchObj(int oIdCliente)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principal].[spS_MXJ_ConsultaContactosCliente]", "@IdCliente",10);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(ContactosyPreferencias oContacto)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("Principal.spI_MXJ_InsertaContacto", "@idCliente", oContacto.iIdCliente,
                                                                                    "@Nombre",oContacto.sNombre,
                                                                                    "@idTitulo", oContacto.iIdTitulo,
                                                                                    "@CorreoElectronico", oContacto.sCorreoElectronico,
                                                                                    "@TelOficina", oContacto.sTelOficina,
                                                                                    "@TelMovil", oContacto.sTelMovil,
                                                                                    "@OtroTel", oContacto.sOtroTel,
                                                                                    "@IP", oContacto.sIP,
                                                                                    "@Usuario", Utils.GetUser);//piloto.sUsuarioCreacion
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(ContactosyPreferencias oContacto)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principal].[spU_MXJ_ActualizaContacto]", "@idContacto", oContacto.iIdContacto,
                                                                                    "@Nombre", oContacto.sNombre,
                                                                                    "@idTitulo", oContacto.iIdTitulo,
                                                                                    "@CorreoElectronico", oContacto.sCorreoElectronico,
                                                                                    "@TelOficina", oContacto.sTelOficina,
                                                                                    "@TelMovil", oContacto.sTelMovil,
                                                                                    "@OtroTel", oContacto.sOtroTel,
                                                                                    "@Status", oContacto.iStatus,
                                                                                    "@IP", oContacto.sIP,
                                                                                    "@Usuario", Utils.GetUser
                                                                                        );//piloto.sUsuarioCreacion
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(ContactosyPreferencias oContacto)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("Principal.spD_MXJ_EliminaContacto", "@Id", oContacto.iIdContacto,
                                                                                      "@IP", oContacto.sIP,
                                                                                      "@Usuario", Utils.GetUser);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtTipoTitulo
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Principal].[spS_MXJ_ConsultaTitulos]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}