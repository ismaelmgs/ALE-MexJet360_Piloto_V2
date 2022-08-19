using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.DomainModel
{
    public class DBRevenewM : DBBase
    {
        public DataSet DBConsultaConfiguracionViaticos()
        {
            try
            {
                return oDB_SP.EjecutarDS("[VB].[spS_MXJ_ConsultaConfiguracion]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DBActualizaConfiguracionConceptos(Concepto oCon)
        {
            try
            {
                object obj = oDB_SP.EjecutarValor("[VB].[spU_MXJ_ActualizaConfiguracionConceptos]", "@IdConcepto", oCon.IIdConcepto,
                                                                                                    "@HorarioIni", oCon.SHorarioInicial,
                                                                                                    "@HorarioFin", oCon.SHorarioFinal,
                                                                                                    "@MontoMXN", oCon.DMontoMXN,
                                                                                                    "@MontoUSD", oCon.DMontoUSD);
                return obj.S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DBActualizaParametro(ParametrosGrales oPar)
        {
            try
            {
                object obj = oDB_SP.EjecutarValor("[VB].[spU_MXJ_ActualizaParametrosGenerales]", "@IdParametro", oPar.IIdParametro,
                                                                                                 "@Valor", oPar.SValor,
                                                                                                 "@Usuario", oPar.SUsuario);
                return obj.S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DBSetParametroAdicional(ParametrosAdicionales oPA)
        {
            try
            {
                object obj = oDB_SP.EjecutarValor("[VB].[spI_MXJ_InsertaParametrosCalculos]", "@IdParametro", oPA.IIdParametro,
                                                                                              "@Clave", oPA.SClave,
                                                                                              "@Descripcion", oPA.SDescripcion,
                                                                                              "@Valor", oPA.SValor,
                                                                                              "@Opcion", oPA.IOpcion,
                                                                                              "@Usuario", oPA.SUsuario);
                return obj.S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBConsultaDatosCuenta(string sNumCuenta)
        {
            try
            {
                return oDB_SP.EjecutarDT("[VB].[spS_MXJ_ConsultaDatosCuenta]", "@NoCuenta", sNumCuenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}