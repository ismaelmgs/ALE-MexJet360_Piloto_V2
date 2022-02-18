using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
	[Serializable, Bindable(BindableSupport.Yes)]
	public class CorreoMasivo : BaseObjeto
	{
		private int _iIdCorreoMasivo = -1;
		private string _sMotivo = string.Empty;
		private string _sAsunto = string.Empty;
		private string _sDestinatarios = string.Empty;
		private string _sCopiados = string.Empty;
		private string _sContenido = string.Empty;
		private int _iStatus = 1;
		private string _sUsuarioCreacion = string.Empty;
		private DateTime _dtFechaCreacion = new DateTime();
		private string _sUsuarioMod = string.Empty;
		private DateTime _dtFechaMod = new DateTime();
		private string _sIP = string.Empty;

		[Display(AutoGenerateField = false), ScaffoldColumn(false)]
		public int iIdCorreoMasivo { get { return _iIdCorreoMasivo; } set { _iIdCorreoMasivo = value; } }

		public string sMotivo { get { return _sMotivo; } set { _sMotivo = value; } }

		public string sAsunto { get { return _sAsunto; } set { _sAsunto = value; } }

		public string sDestinatarios { get { return _sDestinatarios; } set { _sDestinatarios = value; } }

		public string sCopiados { get { return _sCopiados; } set { _sCopiados = value; } }

		public string sContenido { get { return _sContenido; } set { _sContenido = value; } }

		[Display(Name = "¿Activo? "), Required]
		public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

		public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

		public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

		public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

		public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

		public string sIP { get { return _sIP; } set { _sIP = value; } }
	}
}