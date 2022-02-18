using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class ReporteTabular
    {
        private int _iIdMatricula = 0;
        private int _iIdCliente = 0;
        private int _iIdContrato = 0;
        private int _iIdGrupoModelo = 0;
        private int _iIdBase = 0;
        private bool _bDetalle = false;

        public int iIdMatricula { get { return _iIdMatricula; } set { _iIdMatricula = value; } }
        public int iIdCliente { get { return _iIdCliente; } set { _iIdCliente = value; } }
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public int iIdGrupoModelo { get { return _iIdGrupoModelo; } set { _iIdGrupoModelo = value; } }
        public int iIdBase { get { return _iIdBase; } set { _iIdBase = value; } }
        public bool bDetalle { get { return _bDetalle; } set { _bDetalle = value; } }


    }
}