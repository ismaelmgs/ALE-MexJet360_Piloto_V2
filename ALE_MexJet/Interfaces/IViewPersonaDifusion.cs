using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Interfaces
{
    public interface IViewPersonaDifusion : IBaseView
    {
        object[] oArrFiltros { get; }
        object[] oArrFiltrosPersonaListaDifusion { get; }

        void CargarGridPersona(DataTable dtObjCat);
        void CargaTitulo(DataTable dtResultado);
        void CargaTipoPersona(DataTable dtResultado);
        void CargaTipoContacto(DataTable dtResultado);
        void CargaPersonaListaDifusion(DataTable dtResultado);
        void MostrarMensaje(string sMensaje, string sCaption);
        void ObtieneValores();
        PersonaDifusion oPersona { get; set; }

        event EventHandler eNewObj;
        event EventHandler eObjSelected;
        event EventHandler eSaveObj;
        event EventHandler eDeleteObj;
        event EventHandler eSearchObj;
        event EventHandler eUpdateObj;
        event EventHandler eDeletePersonaListaDifusion;
        event EventHandler eObtieneTitulo;
        event EventHandler eObtieneTipoPersona;
        event EventHandler eObtieneTipoContacto;
        event EventHandler eObtienePersonaListaDifusion;
        event EventHandler eObtieneDatosPersona;
        event EventHandler eSavePersonaListaDifusion;
    }
}
