using ALE_MexJet.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ALE_MexJet.Clases
{
    public class DBSAP : DBBaseSAP
    {
        #region CONSULTAS
        public DataTable DBGetCuentas
        {
            get
            {
                try
                {
                    string sCad = string.Empty;
                    sCad = "SELECT AcctCode, AcctName, acct = AcctCode, CveDesc = ISNULL(AcctCode,'') + ' - ' + ISNULL(AcctName, ''), ";
                    sCad += "CurrTotal, EndTotal, Finanse, Groups, Budget, ActId FROM OACT (NOLOCK)";

                    return oDB_SP.EjecutarDT_DeQuery(sCad);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Obtiene el listado de los códigos de unidad 1
        /// </summary>
        public DataTable DBGetCodigoUnidad1
        {
            get
            {
                try
                {
                    string sCad = string.Empty;
                    sCad = "SELECT OcrCode, unit1 = OcrCode, OcrName, [Description] = OcrName, ";
                    sCad += "OcrTotal, Direct, Locked DataSource, DimCode FROM OOCR (NOLOCK) WHERE DimCode = 1 AND Active = 'Y'";

                    return oDB_SP.EjecutarDT_DeQuery(sCad);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Obtiene el listado de los códigos de unidad 2
        /// </summary>
        public DataTable DBGetCodigoUnidad2
        {
            get
            {
                try
                {
                    string sCad = string.Empty;
                    sCad = "SELECT unit2 = OcrCode, [description] = OcrName, OcrCode,  OcrName, ";
                    sCad += "OcrTotal, Direct, Locked DataSource, DimCode FROM OOCR (NOLOCK) WHERE DimCode = 2 AND Active = 'Y'";

                    return oDB_SP.EjecutarDT_DeQuery(sCad);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Obtiene el listado de los códigos de unidad 3
        /// </summary>
        public DataTable DBGetCodigoUnidad3
        {
            get
            {
                try
                {
                    string sCad = string.Empty;
                    sCad = "SELECT OcrCode, unit3 = OcrCode, OcrName, [description] = OcrName, ";
                    sCad += "OcrTotal, Direct, Locked DataSource, DimCode FROM OOCR (NOLOCK) WHERE DimCode = 3 AND Active = 'Y'";

                    return oDB_SP.EjecutarDT_DeQuery(sCad);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Obtiene el listado de los códigos de unidad 4
        /// </summary>
        public DataTable DBGetCodigoUnidad4
        {
            get
            {
                try
                {
                    string sCad = string.Empty;
                    sCad = "SELECT OcrCode, unit4 = OcrCode, OcrName, [description] = OcrName, ";
                    sCad += "OcrTotal, Direct, Locked DataSource, DimCode FROM OOCR (NOLOCK) WHERE DimCode = 4 AND Active = 'Y'";

                    return oDB_SP.EjecutarDT_DeQuery(sCad);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Obtiene el tipo de cambio del día
        /// </summary>
        public object DBGetTipoCambio
        {
            get
            {
                try
                {
                    string sCad = string.Empty;
                    sCad = "SELECT TOP 1 Rate AS buy_rate FROM ORTT (NOLOCK) WHERE Currency = 'USD' ORDER BY RateDate DESC";
                    return oDB_SP.EjecutarValor_DeQuery(sCad);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Obtiene la descripción del código de unidad 1
        /// </summary>
        /// <param name="sClave">Clave del código de unidad a buscar</param>
        /// <returns></returns>
        public string DBGetDescripcionCodigoUnidad1(string sClave)
        {
            try
            {
                string sCad = string.Empty;
                sCad = "SELECT OcrName AS [description] FROM OOCR (NOLOCK) ";
                sCad += "WHERE DimCode = 1 AND Active = 'Y' AND OcrCode = '" + sClave + "'";

                return oDB_SP.EjecutarValor_DeQuery(sCad).S();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Obtiene la descripción del código de unidad 2
        /// </summary>
        /// <param name="sClave">Clave del código de unidad a buscar</param>
        /// <returns></returns>
        public string DBGetDescripcionCodigoUnidad2(string sClave)
        {
            try
            {
                string sCad = string.Empty;
                sCad = "SELECT OcrName AS [description] FROM OOCR (NOLOCK) ";
                sCad += "WHERE DimCode = 2 AND Active = 'Y' AND OcrCode = '" + sClave + "'";

                return oDB_SP.EjecutarValor_DeQuery(sCad).S();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Obtiene la descripción del código de unidad 3
        /// </summary>
        /// <param name="sClave">Clave del código de unidad a buscar</param>
        /// <returns></returns>
        public string DBGetDescripcionCodigoUnidad3(string sClave)
        {
            try
            {
                string sCad = string.Empty;
                sCad = "SELECT OcrName AS [description] FROM OOCR (NOLOCK) ";
                sCad += "WHERE DimCode = 3 AND Active = 'Y' AND OcrCode = '" + sClave + "'";

                return oDB_SP.EjecutarValor_DeQuery(sCad).S();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Obtiene la descripción del código de unidad 4
        /// </summary>
        /// <param name="sClave">Clave del código de unidad a buscar</param>
        /// <returns></returns>
        public string DBGetDescripcionCodigoUnidad4(string sClave)
        {
            try
            {
                string sCad = string.Empty;
                sCad = "SELECT OcrName AS [description] FROM OOCR (NOLOCK) ";
                sCad += "WHERE DimCode = 4 AND Active = 'Y' AND OcrCode = '" + sClave + "'";

                return oDB_SP.EjecutarValor_DeQuery(sCad).S();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Obtiene el código de unidad dos con una opcion de seleccione
        /// </summary>
        public DataTable DBGetCodigoUnidad2_Union
        {
            get
            {
                try
                {
                    string sCad = string.Empty;
                    sCad = "SELECT '' AS unit2,'Ninguno' AS Descripcion,'' AS OcrCode, '' AS OcrName, 0 AS OcrTotal, ";
                    sCad += "'' AS Direct, '' AS Locked, '' AS DataSource, 0 AS DimCode UNION ALL ";
                    sCad += "SELECT OcrCode, OcrName, OcrCode, OcrName, OcrTotal, Direct, Locked, DataSource, DimCode ";
                    sCad += "FROM OOCR (NOLOCK) WHERE DimCode = 2 AND Active = 'Y' ";

                    return oDB_SP.EjecutarDT_DeQuery(sCad);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Obtiene la lista de articulos de SAP
        /// </summary>
        public DataTable DBGetObtieneArticulos
        {
            get
            {
                try
                {
                    string sCad = string.Empty;
                    sCad = "SELECT ItemCode, ItemName, FrgnName, ItmsGrpCod,CstGrpCode, ";
                    sCad += "VatGourpSa, CodeBars FROM OITM (NOLOCK)";

                    return oDB_SP.EjecutarDT_DeQuery(sCad);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public DataTable DBGetFacturantes(string sCliente)
        {
            try
            {
                string sCad = string.Empty;
                sCad = "SELECT CardCode cust_num, CardCode, CardName, CardType, GroupCode, CmpPrivate, ";
                sCad += "Address, ZipCode, MailAddres FROM OCRD(NOLOCK) WHERE U_CLICORP = '" + sCliente + "'";

                return oDB_SP.EjecutarDT_DeQuery(sCad);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable DBGetServiciosCC
        {
            get
            {
                try
                {
                    string sCad = string.Empty;
                    sCad = "SELECT unit1 = FldValue, [Description] = Descr FROM UFD1 (NOLOCK) WHERE FieldID=9 AND TableID='RDR1'";

                    return oDB_SP.EjecutarDT_DeQuery(sCad);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public DataTable DBGetProyectos
        {
            get
            {
                try
                {
                    string sCad = string.Empty;
                    sCad = "SELECT PrcCode, PrcName FROM OPRC WHERE DimCode = 5 AND Locked = 'N'";

                    return oDB_SP.EjecutarDT_DeQuery(sCad);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Obtiene el tipo de cambio del día anterior a la fecha de salida
        /// </summary>
        public object DBGetTipoCambioSalida(DateTime dtFechaSalida)
        {
            try
            {
                string sCad = string.Empty;
                sCad = "SELECT TOP 1 Rate AS buy_rate FROM ORTT (NOLOCK) WHERE Currency = 'USD' AND RateDate = '" + dtFechaSalida.ToString("yyyy-MM-dd") + "' ORDER BY RateDate DESC";
                return oDB_SP.EjecutarValor_DeQuery(sCad);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}