using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ALE_MexJet.Interfaces
{
    public interface IViewConsultaExtension : IBaseView
    {
        object[] oArrFil { get; }

        void LoadExtensiones(DataTable dtExt);
    }
}
