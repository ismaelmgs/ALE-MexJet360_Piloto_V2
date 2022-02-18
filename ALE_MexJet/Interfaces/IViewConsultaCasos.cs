using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewConsultaCasos : IBaseView
    {

        event EventHandler eGetObjects;
        event EventHandler eObjConsultaCasos;
        event EventHandler eGetMotivos;
        event EventHandler eGetArea;
        event EventHandler eObjConsultaTop;

        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadCliente(DataTable dtObjCat);
        void LoadTipoCaso(DataTable dtObjCat);

        void LoadArea(DataTable dtObjCat); 
        void LoadMotivo(DataTable dtObjCat);
        void LoadConsultaCasos(DataTable dtObjCat);
        void LoadConsultaTop(DataTable dtObjCat);
        ConsultaCasos oConsultaCasosConsultaCasos { get; }
        ConsultaCasos oConsultaCasosConsultaTop { get; }
        int iIdTipoCaso { get; }
    }
}