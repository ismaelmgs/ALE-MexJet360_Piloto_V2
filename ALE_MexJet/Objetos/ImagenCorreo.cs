using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class ImagenCorreo
    {
        private string _sClaveImagen = string.Empty;
        private string _sRutaIagen = string.Empty;

        public string sClaveImagen { get { return _sClaveImagen; } set { _sClaveImagen = value; }}
        public string sRutaIagen { get { return _sRutaIagen; } set { _sRutaIagen = value; } }
    }
}