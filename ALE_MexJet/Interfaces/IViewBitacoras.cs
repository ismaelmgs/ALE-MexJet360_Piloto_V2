using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewBitacoras : IBaseView
    {
        Bitacoras oBi { get; set; }
        string sParametro { get; set; }
        int iOk { get; set; }
        long lLegIdMax { get; set; }
        void LoadBitacoras(DataTable dt);
        void LoadTipo(DataTable dt);

        event EventHandler eSearchTipo;
        event EventHandler eSearchMaxLegId;
    }
}
