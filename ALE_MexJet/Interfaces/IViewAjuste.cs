using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewAjuste : IBaseView
    {
        int iIdAjuste { get; set; }
        int iIdContrato { set; get; }
        string sClaveContrato { set; get; }
        void LoadContratos(DataTable dt);
        void LoadRemisiones(DataTable dt);
        void LoadMotivos(DataTable dt);
        AjusteRemision oAjuste { get; }
        void setParameters(List<Parametros> lstParameters);
        void setParametersNot(List<Parametros> lstParameters);
        void isValidUser(DataSet ds);

        event EventHandler eValidateObj;
    }
}
