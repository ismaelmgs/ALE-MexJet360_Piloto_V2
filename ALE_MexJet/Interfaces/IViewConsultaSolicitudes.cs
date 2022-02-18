using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewConsultaSolicitudes : IBaseView
    {

        event EventHandler eGetObjects;
        event EventHandler eObjConsultaSolicitudes;
        event EventHandler eGetContratos;

        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadEstatusSolicitud(DataTable dtObjCat);
        void LoadCliente(DataTable dtObjCat);
        void LoadConsultaSolicitudes(DataTable dtObjCat);
        void LoadContrato(DataTable dtObjCat);


        ConsultaSolicitudes oConsultaSolicitudesConsultaSolicitudes { get; }
        Bitacora oBitacoraContrato { get; }
    }
}