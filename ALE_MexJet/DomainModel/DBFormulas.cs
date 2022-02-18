using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.DomainModel
{
    public class DBFormulas : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaDescFormulas]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetFactores()
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaFactoresFijosVariables]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdateObj(FormulaRem oForm)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_ActualizaDescFormulas]", "@IdFormula", oForm.iId,
                                                                                                "@Formula", oForm.sFormula,
                                                                                                "@Desc", oForm.sDescripcion,
                                                                                                "@Codigo", oForm.CodigoF,
                                                                                                "@usuario", Utils.GetUser);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSaveObj(FormulaRem oForm)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaDescFormulas]", "@Formula", oForm.sFormula,
                                                                                                "@Desc", oForm.sDescripcion,
                                                                                                "@Codigo", oForm.CodigoF,
                                                                                                "@usuario", Utils.GetUser);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(FormulaRem oForm)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_EliminaDescFormulas]", "@IdFormula", oForm.iId,
                                                                                                "@usuario", Utils.GetUser);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}