using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ALE_MexJet.Objetos;
using System.Data;
using ALE_MexJet.Clases;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBContacto : DBBase
    {
        public DataSet DBSearchObj(int idCliente)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaContactosCliente]", "@IdCliente", idCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(Contacto oContacto)
        {
            try
            {
                object oResult = new object();
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaContacto]", "@idCliente", oContacto.iIdCliente
                                                                                     , "@Nombre", oContacto.sNombre
                                                                                     , "@IdTitulo", oContacto.iIdTitulo
                                                                                     , "@CorreoElectronico", oContacto.sCorreoElectronico
                                                                                     , "@TelOficina", oContacto.sTelOficina
                                                                                     , "@TelMovil", oContacto.sTelMovil
                                                                                     , "@OtroTel", oContacto.sOtroTel
                                                                                     , "@Usuario", Utils.GetUser
                                                                                     , "@Ip", oContacto.sIP);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.ContactosPreferencias), Convert.ToInt32(Enumeraciones.TipoAccion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 :oResult.S().I();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int BDUpdate(Contacto oContacto)
        {
            try 
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaContacto]", "@IdContacto", oContacto.iIdContacto
                                                                                        , "@IdTitulo", oContacto.iIdTitulo
                                                                                        , "@Nombre", oContacto.sNombre
                                                                                        , "@CorreoElectronico", oContacto.sCorreoElectronico
                                                                                        , "@TelOficina", oContacto.sTelOficina
                                                                                        , "@TelMovil", oContacto.sTelMovil
                                                                                        , "@OtroTel", oContacto.sOtroTel
                                                                                        , "@Status", oContacto.iStatus
                                                                                        , "@UsuarioModificacion", Utils.GetUser
                                                                                        , "@Ip", oContacto.sIP);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.ContactosPreferencias), Convert.ToInt32(Enumeraciones.TipoAccion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(Contacto oContacto)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaContacto]", "@id", oContacto.iIdContacto
                                                                                     , "@Usuario", Utils.GetUser
                                                                                     , "@IP", oContacto.sIP);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.ContactosPreferencias), Convert.ToInt32(Enumeraciones.TipoAccion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();

            }
            catch (Exception ex)
            {
                throw ex;
            }
 
        }

        public int DBUpdateComentarios(Cliente oCLiente)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spu_MXJ_ActualizaComentarios]", "@IdCliente", oCLiente.iId
                                                                                     , "@Observaciones", oCLiente.sObservaciones
                                                                                     , "@Notas", oCLiente.sNotas
                                                                                     , "@Otros", oCLiente.sOtros
                                                                                     , "@Usuario", Utils.GetUser
                                                                                     , "@IP", oCLiente.sIP);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.ContactosPreferencias), Convert.ToInt32(Enumeraciones.TipoAccion.Actualizar), "Se actualizó la Información del Cliente: " + oResult.S());
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
                    return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaTitulos]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}