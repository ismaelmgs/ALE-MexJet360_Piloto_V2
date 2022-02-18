using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Clases;
using System.Data;
using NucleoBase.Core;

namespace ALE_MexJet.Presenter
{
    public class VuelosPorCliente_Presenter:BasePresenter<IViewVuelosPorCliente>
    {
        private readonly DBVuelosPorCliente oIGestCat;

        public VuelosPorCliente_Presenter(IViewVuelosPorCliente oView, DBVuelosPorCliente oBC):base(oView)
        {
            oIGestCat = oBC;
            oIView.eGetClientes += oIView_eGetClientes;
            oIView.eGetContrato += oIView_eGetContrato;
            oIView.eGetReporteVuelosPorCliente += oIView_eGetReporteVuelosPorCliente;
        }
       
        void oIView_eGetClientes(object sender, EventArgs e)
        {
            oIView.LoadClientes(oIGestCat.DBSearchCliente(oIView.oArrFiltroClientes));
        }
        void oIView_eGetContrato(object sender, EventArgs e)
        {
            oIView.LoadContrato(oIGestCat.DBSearchContrato(oIView.oArrFiltroContrato));
        }
        void oIView_eGetReporteVuelosPorCliente(object sender, EventArgs e)
        {
            DataTable dtReporteVuelos;
            DataTable dtTiempoManto;
            
            int posicionRow;
            float tiempo;
            
            posicionRow = 0;
            tiempo = 0;
            
            dtReporteVuelos = oIGestCat.DBSearchReporteVuelosPorCliente(oIView.oArrFiltroReporteVuelosPorCliente);

            foreach (DataRow row in dtReporteVuelos.Rows)
            {
                int remision = row[0].S().I();
                dtTiempoManto = oIGestCat.DBSearchTiempoMantenimiento(remision);
                string HorasManto = Utils.ObtieneTotalTiempo(dtTiempoManto, "HORAS", ref tiempo);
                dtReporteVuelos.Rows[posicionRow][13] = HorasManto;
                
                posicionRow += 1;
            }
            oIView.LoadReporteVuelosPorCliente(dtReporteVuelos);                                                                                      
        }       
    }
}