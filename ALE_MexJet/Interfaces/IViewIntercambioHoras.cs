using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewIntercambioHoras:IBaseView
    {
        void LoadClientes(DataTable dtObjCat);
        object[] oArrFiltroCliente { get; }
        event EventHandler eGetCliente;

        void LoadIntercambioHoras(DataTable dtObjCat);
        object[] oArrFiltroIntercambioHoras { get; }
        event EventHandler eGetIntercambioHoras;

        void ObtieneValores();

        void LoadHorasDisponiblesOrigen(string dtObjHr);
        object[] oArrFiltroHorasDisponiblesOrigen { get; }
        event EventHandler eGetHorasDisponiblesOrigen;

        void LoadHorasDisponiblesDestino(string dtObjHr);
        object[] oArrFiltroHorasDisponiblesDestino { get; }
        event EventHandler eGetHorasDisponiblesDestino;

        void LoadContrato(DataTable dtObjCat);
        object[] oArrFiltroContrato { get; }
        event EventHandler eGetContrato;

        Enumeraciones.TipoOperacion eCrud { set; get; }
        object oCrud { get; set; }

        void MostrarMensaje(string sMensaje, string sCaption);

        TraspasoHoras oTraspasoHoras { get; }
    }
}
