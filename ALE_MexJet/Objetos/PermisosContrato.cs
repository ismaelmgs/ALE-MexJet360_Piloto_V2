using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class PermisosContrato
    {
        public int iRol { set; get; }
        public int iPestana { set; get; }

        private List<SeccionesPestana> _secciones = new List<SeccionesPestana>();
        public List<SeccionesPestana> secciones { set { _secciones = value; } get { return _secciones; } }
    }

    public class SeccionesPestana
    {
        public int iSeccion { set; get; }
        public int iAcceso { set; get; }
        public List<CamposPestana> campos { set; get; }

        private List<CamposPestana> _campos = new List<CamposPestana>();
        public List<CamposPestana> secciones { set { _campos = value; } get { return _campos; } }
    }
    
    public class CamposPestana
    {
        public int iCampo { set; get; }
        public int iAcceso { set; get; }
    }
}