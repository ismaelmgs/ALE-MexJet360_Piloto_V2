using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewBitacorasRemision : IBaseView
    {
        void LoadBitacoras(DataTable dt);
    }
}
