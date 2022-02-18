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
    public class DBConsultaCasos : DBBase
    {
        public DataTable dtGetTipoCasos
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaTiposCasos]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable  dtGetArea
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaAreacasos]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
             
        public DataTable dtGetMotivos(int TipoCaso)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaMotivosCasos]", "@IdTipoCaso", TipoCaso);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtObjConsultaCasos(ConsultaCasos B)
        
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaCasos]",
                                                             "@IdCliente", B.IdCliente
                                                            , "@IdTipoCaso", B.IdTipoCaso
                                                            , "@IdArea", B.IdArea
                                                            , "@IdSolicitud", B.Idsolicitud
                                                            , "@IdMotivo", B.IdMotivo
                                                            , "@FechaIni", B.FechaIni
                                                            , "@FechaFin", B.FechaFin
                                                                                 );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtObjConsultaTop(ConsultaTop B)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaCasostop30]",
                                                             "@IdCliente", B.IdCliente
                                                            , "@CodigoCliente", B.CodigoCliente
                                                            , "@IdArea", B.IdArea
                                                            , "@DesTipoCaso", B.DesTipoCaso
                                                            , "@DesMotivoCaso", B.DesMotivoCaso
                                                            , "@Minutos", B.Minutos
                                                            , "@Detalle", B.Detalle
                                                            ,"@DescOtorgado", B.DescOtorgado
                                                            ,"@UsuarioCreacion", B.UsuarioCreacion
                                                            ,"@FechaCreacion", B.FechaCreacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}