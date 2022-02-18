using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewMonitorCambios : IBaseView  
    {
        object[] oArrActualizaStatus { get; }
        object[] oArrConsultaCambiosDetalle { get; }

        void CargaConsultaMonitorNotificaciones(DataTable dtObjCat);
        void CargaConsultaCambiosDetalle(DataTable dtObjCat);
        void loadConsultaMonitorNotificaciones();

        int iIdNotificacion { get; set; }
        int iIdCambio { get; set; }

        event EventHandler eActualizaStatus;
        event EventHandler eSearchDetalle;
    }
}
