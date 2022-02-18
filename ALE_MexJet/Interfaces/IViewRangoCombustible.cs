using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Interfaces
{
    public interface IViewRangoCombustible : IBaseView
    {

        object oCrud { get; set; }

        Enumeraciones.TipoOperacion eCrud { set; get; }

        object[] oArrFiltros { get; }
        object[] oArrFiltrosRan { get; }
        object[] oArrFiltrosGruModelo { get; }
        object[] oArrFiltrosCon { get; }
        int iIdCombustible { get; }
        bool bDuplicado { get; set; }
        bool bErrorRango { get; set; }
        

        void ObtieneValoresCombustible();
        void ObtieneValoresRango();
        void LoadObjectsGrupoModelo(DataTable dtGrupoModelo);
        void LoadObjectsContrato(DataTable dtContrato);

        

        void LoadObjects(DataTable dtObjCat);
        void LoadObjectsRangos(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);
        void MostrarMensajeRan(string sMensaje, string sCaption);

        event EventHandler eSearchObjGru;
        event EventHandler eSearchObjCon;
        event EventHandler eNewObjRan;
        event EventHandler eObjSelectedRan;
        event EventHandler eSaveObjRan;
        event EventHandler eDeleteObjRan;
        event EventHandler eSearchObjRan;
        event EventHandler eValRango;

    }
}

