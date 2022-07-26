using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewConsultaBitacoras : IBaseView
    {
        bool bRes { set; get; }
        string sParametro { set; get; }
        BitacoraVuelo oBi { get; }
        void LoadBitacoras(DataTable dt);
        void LoadFoto(DataTable dt);

        event EventHandler eUpdateSts;
        event EventHandler eSearchPhoto;
    }
}
