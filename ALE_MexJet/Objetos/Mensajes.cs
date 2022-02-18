using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using ALE_MexJet.DomainModel;
using NucleoBase.Core;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public  class Mensajes
    {
        public static List<Mensajes> LMensajes
        {
            get
            {
                List<Mensajes> LMensajes = new List<Mensajes>();
                DataTable dt = new DBUsuarios().DBGetMessages;

                foreach (DataRow row in dt.Rows)
                {
                    LMensajes.Add(new Mensajes { IdMensaje = row.S("IdMensaje").I(), sMensaje = row.S("Descripcion") });
                }

                return LMensajes;
            }
        }

        private int _IdMensaje = 0;
        private string _sMensaje = string.Empty;

        public int IdMensaje
        {
            get { return _IdMensaje; }
            set { _IdMensaje = value; }
        }
        
        public string sMensaje
        {
            get { return _sMensaje; }
            set { _sMensaje = value; }
        }
    }
}