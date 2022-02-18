using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class GastoInterno
    {
        private int _iIDCliente;        
        private int _iIdTipoMoneda;
        private int _iIdGastoInterno;
        private int _iIdAeronaveMatricula;
        private int _iIdTipoMovimiento;
        private int _iStatus = 1;

        private string _sTipoMoneda= string.Empty;
        private string _sClaveContrato= string.Empty;
        private string _sTipoMovimiento = string.Empty; 
        private string _sDescripcionConcepto= string.Empty;
        private string _sNombreCliente = string.Empty;
        private string _sUsuarioCreacion = string.Empty;
        private string _sUsuarioMod = string.Empty;
        private string _sIP = string.Empty;

        private decimal _dImporte;
        private decimal _dIVA;
        private decimal _dTotal;
                
        private DateTime _dtFechaCreacion = new DateTime();        
        private DateTime _dtFechaMod = new DateTime();
        private DateTime _dtFechaGasto = new DateTime();
       
        public string sNombreCliente
        {
            get { return _sNombreCliente; }
            set { _sNombreCliente = value; }
        }

        public decimal dIVA
        {
            get { return _dIVA; }
            set { _dIVA = value; }
        }

        public decimal dTotal
        {
            get { return _dTotal; }
            set { _dTotal = value; }
        }

        public int iIdTipoMovimiento
        {
            get { return _iIdTipoMovimiento; }
            set { _iIdTipoMovimiento = value; }
        }
        
        public string sDescripcionConcepto
        {
            get { return _sDescripcionConcepto; }
            set { _sDescripcionConcepto = value; }
        }

        public string sTipoMoneda
        {
            get { return _sTipoMoneda; }
            set { _sTipoMoneda = value; }
        }

        public int iIdGastoInterno
        {
            get { return _iIdGastoInterno; }
            set { _iIdGastoInterno = value; }
        }

        public int iIDCliente
        {
            get { return _iIDCliente; }
            set { _iIDCliente = value; }
        }

        public string sClaveContrato
        {
            get { return _sClaveContrato; }
            set { _sClaveContrato = value; }
        }       

        public string sTipoMovimiento
        {
            get { return _sTipoMovimiento; }
            set { _sTipoMovimiento = value;}
        }

        public decimal dImporte
        {
            get { return _dImporte; }
            set { _dImporte = value; }
        }

        public int iIdTipoMoneda
        {
            get { return _iIdTipoMoneda; }
            set { _iIdTipoMoneda = value; }
        }
        public int iIdAeronaveMatricula
        {
            get { return _iIdAeronaveMatricula; }
            set { _iIdAeronaveMatricula = value; }
        }
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public DateTime dtFechaGasto { get { return _dtFechaGasto; } set { _dtFechaGasto = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }
    }   
}