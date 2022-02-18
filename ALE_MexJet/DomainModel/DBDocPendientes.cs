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
    public class DBDocPendientes : DBBase
    {
        public DataTable dtObjCliente()
        {
            try
            {
                DocPendientes B = new DocPendientes();
                return oDB_SP.EjecutarDT("Catalogos.spS_MXJ_ConsultaClienteDDL", "@status", B.status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjContrato(DocPendientes B)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaContratosDDL]", "@IdCliente", B.IdCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjBitPen(DocPendientes B)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaBitacoraPendiente]"
                                                            , "@IdCliente", B.IdCliente
                                                            , "@IdContrato", B.IdContrato
                                                            , "@FechaIni", B.FechaIni
                                                            , "@FechaFin", B.FechaFin
                                                                                 );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjRemPen(DocPendientes B)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaRemisionesPendientes]"
                                                                            , "@IdCliente", B.IdCliente
                                                                            , "@IdContrato", B.IdContrato
                                                                            , "@FechaIni", B.FechaIni
                                                                            , "@FechaFin", B.FechaFin
                                                                                                 );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjFacPen(DocPendientes B)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaPrefacturaPendientes]"
                                                                            , "@IdCliente", B.IdCliente
                                                                            , "@IdContrato", B.IdContrato
                                                                            , "@FechaIni", B.FechaIni
                                                                            , "@FechaFin", B.FechaFin
                                                                                                 );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}