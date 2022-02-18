using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class FacturaPaso3
    {
        private string _sInvNum = string.Empty;
        private int _iLine = 0;
        private decimal _dQty = 0m;
        private string _sUM = string.Empty;
        private string _sdescription = string.Empty;
        private decimal _dPrice = 0m;
        private int _iIsWorkflow = 0;
        private int _NoteExist = 0;
        private string _sUsuario = string.Empty;

        public string sInvNum { get { return _sInvNum; } set { _sInvNum = value; } }
        public int iLine { get { return _iLine; } set { _iLine = value; } }
        public decimal dQty { get { return _dQty; } set { _dQty = value; } }
        public string sUM { get { return _sUM; } set { _sUM = value; } }
        public string sdescription { get { return _sdescription; } set { _sdescription = value; } }
        public decimal dPrice { get { return _dPrice; } set { _dPrice = value; } }
        public int iIsWorkflow { get { return _iIsWorkflow; } set { _iIsWorkflow = value; } }
        public int NoteExist { get { return _NoteExist; } set { _NoteExist = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }
    }
}