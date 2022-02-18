using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewHoraVueloMatricula : IBaseView  
    {
        event EventHandler eObjFlota;
        event EventHandler eObjCliente;
        event EventHandler eObjContrato;
        event EventHandler eObjConsultaCliente;
        event EventHandler eObjConsultaMatricula;
        event EventHandler eObjConsultaFlota;

        object[] oArrFiltros { get; }
        object[] oArrClient { get; }
        object[] oArrMatricula { get; }
        object[] oArrFlota { get; }


        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadCliente(DataTable dtObjCat);
        void LoadContrato(DataTable dtObjCat);
        void LoadConsultaCliente(DataTable dtObjCat);
        void LoadConsultaMatricula(DataTable dtObjCat);
        void LoadFlota(DataTable dtObjCat);
        void LoadConsultaFlota(DataTable dtObjCat);
    }
}