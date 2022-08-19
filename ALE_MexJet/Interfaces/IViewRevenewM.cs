using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Interfaces
{
    public interface IViewRevenewM : IBaseView
    {
        DataTable dtConfigConceptos { get; set; }
        Concepto oC { get; }
        ParametrosGrales oP { get; }
        ParametrosAdicionales oPA { get; }
        string sOk { get; set; }
        void LoadConfiguracion(DataSet ds);

        string sNumCuenta { set; get; }
        void LoadDatosCuenta(DataTable dt);

        event EventHandler eSaveParametro;
        event EventHandler eSaveParametroAd;
        event EventHandler eSearchCuenta;
    }
}
