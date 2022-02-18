using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Presenter
{
    public class SolicitudesPorCaso_Presenter:BasePresenter<IViewSolicitudesPorCaso>
    {
        private readonly DBSolicitudesPorCaso oIGesCat;

        public SolicitudesPorCaso_Presenter(IViewSolicitudesPorCaso oView, DBSolicitudesPorCaso oBD):base(oView)
        {
            oIGesCat = oBD;
            oView.eSearchObj += oView_eSearchObj;
            oView.eSearchTiposCaso += oView_eSearchTiposCaso;  
        }

        void oView_eSearchTiposCaso(object sender, EventArgs e)
        {
            oIView.LoadTiposCaso(oIGesCat.DBSearchTiposCaso(oIView.oArrFiltroTiposCaso));
        }

        void oView_eSearchObj(object sender, EventArgs e)
        {
            oIView.LoadSolicitudesPorCaso(oIGesCat.DBSearchSolicitudesPorCaso(oIView.oArrFiltroSolicitudesPorCaso));
        }


    }
}