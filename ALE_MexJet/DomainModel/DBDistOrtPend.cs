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
    public class DBDistOrtPend : DBBase
    {
        public DataTable dtObjDistOrtPend()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaDistOrtPen]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}