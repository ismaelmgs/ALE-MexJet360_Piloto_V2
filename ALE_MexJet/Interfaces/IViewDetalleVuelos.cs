using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ALE_MexJet.Interfaces
{
    public interface IViewDetalleVuelos : IBaseView  
    {
        event EventHandler eObjCliente;
        event EventHandler eObjContrato;
        event EventHandler eObjMatricula;

        object[] oArrFiltros { get; }
        object[] oArrCliente { get; }
        object[] oArrMatricula { get; }

        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadCliente(DataTable dtObjCat);
        void LoadContrato(DataTable dtObjCat);
        void LoadVueloCliente(DataTable dtObjCat);
        void LoadVueloMatricula(DataTable dtObjCat);
    }
}