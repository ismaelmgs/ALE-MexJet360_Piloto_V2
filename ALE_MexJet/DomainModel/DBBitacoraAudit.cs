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
    public class DBBitacoraAudit : DBBase
    {
        public DataTable dtObjAccion()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Seguridad].[spS_ConsultaAccionAudit]");
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
        public DataTable dtObjUsuario()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Seguridad].[spS_ConsultaUsuariosAudit]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtObjBitacora(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaBitacoraAudit]", oArrFiltros);
                                                             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}