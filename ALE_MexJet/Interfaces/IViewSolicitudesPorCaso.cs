using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewSolicitudesPorCaso: IBaseView
    {
        void ObtieneSolicitudesPorCaso();
        void LoadSolicitudesPorCaso(DataTable dtObjSolicitudes);
        object[] oArrFiltroSolicitudesPorCaso { get; }

        void ObtieneTiposCaso();
        void LoadTiposCaso(DataTable dtObjTiposCaso);
        object[] oArrFiltroTiposCaso { get; }

        event EventHandler eSearchTiposCaso;
    }
}
