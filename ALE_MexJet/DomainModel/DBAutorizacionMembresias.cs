using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.DomainModel
{
    public class DBAutorizacionMembresias : DBBase
    {
        public DataTable ConsultaMiembrosPorStatus(object[] oArr)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaMembresias]", oArr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ConsultaTiposSubscripcionesDisponiebles
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaPaquetesEzMexJet]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable ConsultaContratosEzMexJet
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaContratosActivosEzMexJet]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActualizaMiembro(MiembroEZ oM)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaInformacionDeMiembro]", "@IdMiembro", oM.iIdMiembro,
                                                                                                    "@IdMembresia", oM.iIdMembresia,
                                                                                                    "@Nombre", oM.sNombre,
                                                                                                    "@CorreoElectronico", oM.sCorreoElectronico,
                                                                                                    "@Telefono", oM.sTelefono,
                                                                                                    "@TelefonoMovil", oM.sTelefonoMovil,
                                                                                                    "@subscripcion", oM.sSubscripcion,
                                                                                                    "@descuentahoras", oM.idescuentahoras,
                                                                                                    "@UsuarioModificacion", Utils.GetUser,
                                                                                                    "@IP", Utils.GetIPAddress());

                return oRes != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaEstatusMiembro(int iIdMiembro, int iIdEstatus)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spU_MXJ_CambioEstatusMiembro]", "@IdMiembro", iIdMiembro, "@IdEstatus", iIdEstatus);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}