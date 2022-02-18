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
    public class DBAudit : DBBase
    {
      

        public DataSet dtObjUsuariosReport()
        {
            try
            {
                return oDB_SP.EjecutarDS("[Seguridad].[spS_ConsultaUsuariosAuditReporte]");
                                                             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtActividadUsuarios(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaActividadUsuario]", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtUsuarios()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaUsuarios]");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public DataSet dsIndicadoresOperaciones(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[SpS_MXJ_ConsultaIndicadoresOperacionAudit]", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtModulos()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaModulosAudit]");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtReporteadorContrato(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaReporteadorContratoAudit]", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtReporteadorRemisiones(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaReporteadorRemisionesAudit]", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtReporteadorSolicitudVuelo(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaReporteadorSolicitudVueloAudit]", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtReporteadorBitacora(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaReporteadorBitacoraAudit]", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtReporteadorPrefactura(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaReporteadorPrefacturaAudit]", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtReporteadorComisariato(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaReporteadorComisariatoAudit]", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet dsIndicadoresFinanzas(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[SpS_MXJ_ConsultaIndicadoresFinanzasAudit]", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtIndicadoresFinanzasVuelosSinBitacora(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("Principales.SpS_MXJ_ConsultaVuelosSinBitacoraAudit", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtIndicadoresFinanzasVencimientoDeContratos(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("Principales.SpS_MXJ_ConsultaVencimientoDeContratosAudit", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtIndicadoresFinanzasBitacorasSinRemisionar(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("Principales.SpS_MXJ_ConsultaBitacorasSinRemisionarAudit", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtIndicadoresFinanzasRemisionesSinPrefacturar(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("Principales.SpS_MXJ_ConsultaRemisionesSinPrefacturarAudit", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtIndicadoresFinanzasPrefacturasSinFacturar(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("Principales.SpS_MXJ_ConsultaPrefacturasSinFacturarAudit", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtFinanzasDescuentos(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaDescuentoRemisionAudit]", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtEstatusContratoDetalle(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("Principales.SpS_MXJ_ConsultaEstatusContratoDetalleAudit", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtIndicadoresOperacionIntercambios(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("Principales.SpS_MXJ_ConsultaIndicadoresOperacionIntercambiosAudit", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtIndicadoresOperacionRentaAeronaves(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("Principales.SpS_MXJ_ConsultaIndicadoresOperacionRentaAeronavesAudit", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtIndicadoresOperacionVuelosRentas(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("Principales.SpS_MXJ_ConsultaIndicadoresOperacionVuelosRentasAudit", oArrFiltros);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}