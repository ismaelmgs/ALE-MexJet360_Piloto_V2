using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewMaster : IBaseView
    {
        void LoadObjects(DataTable dtObjCat);
        string sRolId { get;  }

        event EventHandler eGetMenu;
    }
}
