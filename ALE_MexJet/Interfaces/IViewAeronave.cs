using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewAeronave : IBaseView
    {
        
        object oCrud { get; set; }

        Enumeraciones.TipoOperacion eCrud { set; get; }

        object[] oArrFiltros { get; }
        object[] oArrFiltrosModelo { get; }

        bool bDuplicado { get; set; }
        void ObtieneValores();
        void LoadObjects(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadCatalogoMarca(DataTable dtObjCat);
        void LoadCatalogoFlota(DataTable dtObjCat);
        void LoadCatalogoModelo(DataTable dtObjCat);
        void LoadCatalogoBase(DataTable dtObjCat);
        void LoadCatalogoMatriculaInfor(DataTable dtObjCat);
        void LoadCatalogoBaseInfor(DataTable dtObjCat);
        void LoadCatalogoUnidadNegocioInfor(DataTable dtObjCat);
        
        event EventHandler eGetMarca;
        event EventHandler eGetFlota;
        event EventHandler eGetModelo;
        event EventHandler eGetBase;
        event EventHandler eGetMatriculaInfor;
        event EventHandler eGetBaseInfor;
        event EventHandler eGetUnidadNegocioInfor;

        event EventHandler eGetCodigoUnidadDos;
        void LoadCodigoUnidadDos(DataTable dtCodigoUnitDos);
        object[] oArrFiltrosCodigoUnidadDos { get; }
    }
}
