using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Interfaces
{
    public interface IViewServicioCargo : IBaseView
    {
        DataTable dtCuentas { get; set; }
        DataTable dtCodUnidadUno { get; set; }
        DataTable dtArticulos { set; get; }

        event EventHandler eGetCuentas;
        event EventHandler eGetArticulos;

        object oCrud { get; set; }

        bool bDuplicado { get; set; }
        Enumeraciones.TipoOperacion eCrud { set; get; }

        object[] oArrFiltros { get; }
        //string sCaptionBtnGuardar { set; }
        //string sNumRegistros { get; set; }

        void ObtieneValores();
        void LoadObjects(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);

    }
}
