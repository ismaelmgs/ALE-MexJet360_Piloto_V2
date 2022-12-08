using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewBitacoraPeticion : IBaseView
    {
        event EventHandler eObjProcesar;

        string sFechaDesde { get; set; }
        string sFechaHasta { get; set; }
        string stripNumber { get; set; }
        DataTable dtFl3xx { get; set; }
        List<FlightsFlexx> flights { get; set; }
        List<PostFlightFlexx> lstPostFlights { get; set; }
        void LoadFl3xx(List<FlightsFlexx> flights);
        void getPostflight(List<PostFlightFlexx> postFlights);
        void vuelosProcesados(bool procesado);
    }
}
