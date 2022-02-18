using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    public class FacturaProveedor : BaseObjeto
    {
            private int _IdFaturaProveedorDetalle = -1;
            private int _IdFaturaProveedor	=-1;
            private string _Provedor	= string.Empty;
            private string _Factura	=string.Empty;
            private string _Subtotal	= string.Empty;
            private string _IVA	= string.Empty;
            private string _Total	= string.Empty;
            private int _TipoMoneda	= -1;
            private int _Status	= -1;
            private string _UsuarioCreacion	= string.Empty;
            private DateTime _FechaCreacion;
            private string _UsuarioModificacion	= string.Empty;
            private DateTime _FechaModificacion;
            private string _IP = string.Empty;
            private string _Matricula = string.Empty;
            private int _FolioReal = -1;

            private DateTime _FechaFactura;
            private string _TipoCambio = string.Empty;

            public int IdFaturaProveedor { get { return _IdFaturaProveedor; } set { _IdFaturaProveedor = value; } }
            public string Provedor { get { return _Provedor; } set { _Provedor = value; } }
            public string Factura { get { return _Factura; } set { _Factura = value; } }
            public string Subtotal { get { return _Subtotal; } set { _Subtotal = value; } }
            public string IVA { get { return _IVA; } set { _IVA = value; } }
            public string Total { get { return _Total; } set { _Total = value; } }
            public int TipoMoneda { get { return _TipoMoneda; } set { _TipoMoneda = value; } }
            public int Status { get { return _Status; } set { _Status = value; } }
            public string UsuarioCreacion { get { return _UsuarioCreacion; } set { _UsuarioCreacion = value; } }
            public DateTime FechaCreacion { get { return _FechaCreacion; } set { _FechaCreacion = value; } }
            public string UsuarioModificacion { get { return _UsuarioModificacion; } set { _UsuarioModificacion = value; } }
            public DateTime FechaModificacion { get { return _FechaModificacion; } set { _FechaModificacion = value; } }
            public string IP { get { return _IP; } set { _IP = value; } }
            public string Matricula { get { return _Matricula; } set { _Matricula = value; } }
            public int FolioReal { get { return _FolioReal; } set { _FolioReal = value; } }
            public int IdFaturaProveedorDetalle { get { return _IdFaturaProveedorDetalle; } set { _IdFaturaProveedorDetalle = value; } }

            public string TipoCambio { get { return _TipoCambio; } set { _TipoCambio = value; } }
            public DateTime FechaFactura { get { return _FechaFactura; } set { _FechaFactura = value; } }
    }
}