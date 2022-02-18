using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IVIewDistOrtPend : IBaseView
    {
        void LoadGv(DataTable dtObjCat);

    }
}