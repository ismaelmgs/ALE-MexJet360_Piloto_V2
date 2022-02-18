using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewRevenew : IBaseView
    {
        void LoadTiposCliente(DataTable dt);
        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadDescuento(DataTable dt);


        int iIdTipoCliente { set; get; }
        int iIdDescuento { set; get; }
        DataTable dtDescuentos { set; get; }
        Revenew oDescReve { set; get; }

        event EventHandler eGetDescuento;
        event EventHandler eUpdateDescuento;
    }
}
