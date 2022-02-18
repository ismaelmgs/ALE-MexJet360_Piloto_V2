using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewGeneracionPagosAPHIS:IBaseView
    {
        void LoadPagosAPHIS(DataTable dtObjCat);
        object[] oArrFiltroPagosAPHIS { get; }
        event EventHandler eGetPagosAPHIS;

        void obtieneValores();
    }
}
