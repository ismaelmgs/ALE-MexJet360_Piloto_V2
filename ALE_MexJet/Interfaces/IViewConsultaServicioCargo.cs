using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewConsultaServicioCargo : IBaseView
    {
        object[] oArrContrato { get; }
        object[] oArrServicio { get; }
        void CargaCliente(DataTable DT);
        void CargaContrato(DataTable DT);
        void CargaGrid(DataSet DT);
        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eObjContrato;
        event EventHandler eObjServico;
    }
}