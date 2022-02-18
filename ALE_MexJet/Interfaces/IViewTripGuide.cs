using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Interfaces
{
    public interface IViewTripGuide: IBaseView
    {

        void ConsultaContactoSolicitud(DataTable dt);
        void ConsultaVendedorSolicitud(DataTable dtResultado);

        event EventHandler eGuardaTripGuide;
        event EventHandler eConsultaContactoSolicitud;
        event EventHandler eConsultaVendedorSolicitud;
        
        TripGuide TripGuide{ get; }
        int iIdSolicitud { get; set; }
      
    }
}
