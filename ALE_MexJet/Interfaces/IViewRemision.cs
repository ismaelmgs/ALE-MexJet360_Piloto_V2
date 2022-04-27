using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Interfaces
{
    public interface IViewRemision : IBaseView
    {
        string sEmail { get; }
        string sapiKey { get; }
        string sEmailSoporte { get; }
        string sTemplate { get; }
        void isValidUser(string isValid);
        void setParameters(List<Parametros> lstParameters);

        object[] oArrFiltros { get; }
        void LoadObjects(DataTable dtObjCat);
        void LoadMotivos(DataTable dt);
        void MostrarMensaje(string sMensaje, string sCaption);
        Remision oRemision { get; }
        AjusteRemision oAjuste { get; }
        event EventHandler eSearchMotivos;
        event EventHandler eInsertAjuste;
        event EventHandler eValidateObj;
    }
}
