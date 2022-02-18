using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class MiembroEZ : BaseObjeto
    {
        private int _iIdMiembro = 0;
        private int _iIdMembresia = 0;
        private string _sNombre = string.Empty;
        private string _sCorreoElectronico = string.Empty;
        private string _sTelefono = string.Empty;
        private string _sTelefonoMovil = string.Empty;
        private string _sSubscripcion = string.Empty;
        private int _idescuentahoras = 0;

        

        public int iIdMiembro { get { return _iIdMiembro; } set { _iIdMiembro = value; } }
        public int iIdMembresia { get { return _iIdMembresia; } set { _iIdMembresia = value; } }
        public string sNombre { get { return _sNombre; } set { _sNombre = value; } }
        public string sCorreoElectronico { get { return _sCorreoElectronico; } set { _sCorreoElectronico = value; } }
        public string sTelefono { get { return _sTelefono; } set { _sTelefono = value; } }
        public string sTelefonoMovil { get { return _sTelefonoMovil; } set { _sTelefonoMovil = value; } } 
        public string sSubscripcion { get { return _sSubscripcion; } set { _sSubscripcion = value; } }
        public int idescuentahoras { get { return _idescuentahoras; } set { _idescuentahoras = value; } }
    }
}