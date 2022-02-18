using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IDBCat<TObjCat>
    {
        List<TObjCat> oLstObjsCat { get; }
        DataTable dtObjsCat { get; }
        TObjCat DBGetObj(int iId);
        bool DBObjExists(int iId);
        void DBSaveObj(ref TObjCat oCat);
        void DBDeleteObj(ref TObjCat oCat);
        List<TObjCat> DBSearchObj(object[] oArrFiltros);
    }
}
