using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE_MexJet.Objetos;
using System.Data;

namespace ALE_MexJet.Interfaces
{
    public interface IViewTUA : IBaseView
    {
        object oCrud { get; set; }
        bool bDuplicado { get; set; }
        Enumeraciones.TipoOperacion eCrud { set; get; }
        object[] oArrFiltros { get; }
        object[] oArrFiltrosAereo { get; }
        object[] oArrFiltrosMes { get; }
        void ObtieneValores();
        void LoadObjects(DataTable dtObjCat);
        void LoadObjectsAereo(DataTable dtObjCat);
        void LoadObjectsMes(DataTable dtMes);
        void MostrarMensaje(string sMensaje, string sCaption);
        string sFiltroAeropuerto { get; set; }
        void LoadCatalogoAeropuerto(DataTable dtObjCat);
        event EventHandler eGetAeropuerto;
        event EventHandler eSearchObjMes;
        event EventHandler eGetAeropuertoFiltro;
    }
}
