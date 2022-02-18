using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class Encuesta : BaseObjeto
    {
        private int _iIdEncuesta = 0;
        private int _iIdPadre = 0;
        private string _sDescripcionNodo = string.Empty;


        public int iIdEncuesta
        {
            get { return _iIdEncuesta; }
            set { _iIdEncuesta = value; }
        }
        public string sDescripcionNodo
        {
            get { return _sDescripcionNodo; }
            set { _sDescripcionNodo = value; }
        }

        public int iIdPadre
        {
            get { return _iIdPadre; }
            set { _iIdPadre = value; }
        }
    }
}