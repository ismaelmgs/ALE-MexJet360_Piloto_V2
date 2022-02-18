using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewEstadoCuenta : IBaseView
    {
        event EventHandler eObjCliente;
        event EventHandler eObjContrato;
        event EventHandler eObjVueloHhead;
        event EventHandler eObjVueloHDetail;
        event EventHandler eObjVueloEqDif;
        event EventHandler eObjHeadEdoCnta;

        object[] oArrFiltros { get; }
        object[] oArrVuelosHead { get; }

        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadCliente(DataTable dtObjCat);
        void LoadContrato(DataTable dtObjCat);
        void LoadVueloHead(DataTable dtObjCat);
        void LoadVueloDetail(DataTable dtObjCat);
        void LoadVueloEqDif(DataTable dtObjCat);
        void LoadHeadEdoCnta(DataTable dtObjCat);
    }
}